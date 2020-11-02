using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Data;
using DataAccess;
using System.Data.SqlClient;

public partial class State_Del_AcceptanceNote : System.Web.UI.Page
{
    public SqlConnection cons = new SqlConnection(ConfigurationManager.ConnectionStrings["connstorage"].ToString());
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_WPMS = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2015"].ToString());
    public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_PPMS2014"].ToString());
    public SqlConnection con_Maze = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_MPMS2014"].ToString());


    SqlCommand cmd = new SqlCommand();
    DistributionCenters distobj = null;
    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    public string time;
    public string sid = "";
    public string did = "";
    public string snid = "";
    public string getdatef = "";
    public static string distid;

    public static string mscheme = "101";

    public static string mcomdtyu;
    protected void Page_Load(object sender, EventArgs e)
    {


        ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
        distobj = new DistributionCenters(ComObj);

        if (!IsPostBack)
        {

            GetCommodity();

            bn_del.Visible = false;
            GetDist();
        }



    }
    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }
    void fillgrid()
    {
        con.Open();
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT SCSC_Procurement.*,tbl_MetaData_GODOWN.Godown_Name,tbl_MetaData_DEPOT.DepotName, tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name,Society.Society_Name +','+ SocPlace +',' + Society_Id as PurchaseCenterName FROM dbo.SCSC_Procurement left join dbo.tbl_MetaData_STORAGE_COMMODITY on SCSC_Procurement.Commodity_Id=tbl_MetaData_STORAGE_COMMODITY .Commodity_Id left join dbo.Society on SCSC_Procurement.Purchase_Center= Society.Society_Id inner join tbl_MetaData_DEPOT on SCSC_Procurement.IssueCenter_ID=tbl_MetaData_DEPOT.DepotID inner join tbl_MetaData_GODOWN on tbl_MetaData_GODOWN.Godown_ID=SCSC_Procurement.Recd_Godown where Distt_ID='" + ddl_district.SelectedValue.ToString() + "' and Acceptance_No='" + ddlAccptNumber.SelectedValue.ToString() + "' and IssueCenter_ID ='" + ddl_IssueCentre.SelectedValue.ToString() + "' order by SCSC_Procurement.Recd_date desc";
        DataSet ds = mobj.selectAny(qry);
        if (ds.Tables[0].Rows.Count == 0)
        {
               
            Label1.Visible = true;
            Label1.Text = "Currently no Record is present";
            dgridchallan.DataBind();

        }
        else
        {
            Label1.Visible = false;
            dgridchallan.DataSource = ds.Tables[0];
            dgridchallan.DataBind();

            lbl_Accp_Date.Text = getdate(ds.Tables[0].Rows[0]["Acceptance_Date"].ToString()) ;
            lbl_Purchase.Text = ds.Tables[0].Rows[0]["PurchaseCenterName"].ToString(); ;

        }
        con.Close();


    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    protected void dgridchallan_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalamt"));

            string griddate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Recd_Date"));

            getdatef = getdate(griddate);


            Label lbl = (Label)e.Row.FindControl("lblChallan");
            lbl.Text = getdatef;

       

        }
    }

    public void FooterPagerClick(object sender, CommandEventArgs e)
    {
        
        string arg;
        arg = e.CommandArgument.ToString();

        switch (arg)
        {
            case "next":
                
                if ((dgridchallan.PageIndex < (dgridchallan.PageCount - 1)))
                {
                    dgridchallan.PageIndex += 1;
                }

                break;

            case "prev":
               
                if ((dgridchallan.PageIndex > 0))
                {
                    dgridchallan.PageIndex -= 1;
                }

                break;

            case "last":
             
                dgridchallan.PageIndex = (dgridchallan.PageCount - 1);
                break;

            default:
              
                dgridchallan.PageIndex = Convert.ToInt32(arg);
                break;
        }
        fillgrid();


    }

    protected void dgridchallan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void dgridchallan_PageIndexChanged(object sender, EventArgs e)
    {

    }


    protected void btn_search_Click1(object sender, EventArgs e)
    {

    }
    public void GetDist()
    {
        try
        {
            con.Open();
            string qry = "SELECT district_code ,district_name FROM pds.districtsmp  order by district_name";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_district.DataSource = ds.Tables[0];
                ddl_district.DataTextField = "district_name";
                ddl_district.DataValueField = "district_code";
                ddl_district.DataBind();
                ddl_district.Items.Insert(0, "--Select--");

            }
            con.Close();
        }
        catch (Exception ex)
        {
            lbl_message.Text = ex.ToString(); ;
        }
    }
   
    protected void ddl_district_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_district.SelectedValue == "0" || ddl_district.SelectedItem.Text == "--Select--")
        {

        }

        else
        {
            distid = ddl_district.SelectedValue;


            try
            {
                 GetDepot();

            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.ToString();
            }
        }
    }
    public void GetDepot()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        string ord = "Select DepotID , DepotName from tbl_MetaData_DEPOT where DistrictId = '23" + ddl_district.SelectedValue.ToString() + "' order by DepotName";
        SqlCommand cmd = new SqlCommand(ord, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
        }
        else
        {
            ddl_IssueCentre.DataSource = ds.Tables[0];
            ddl_IssueCentre.DataTextField = "DepotName";
            ddl_IssueCentre.DataValueField = "DepotId";

            ddl_IssueCentre.DataBind();

            ddl_IssueCentre.Items.Insert(0, "--Select--");
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }
    public void check_whr()
{

    
    }
 





    protected void btn_search_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        bn_del.Visible = true;
            fillgrid();
   
    }
    decimal CheckNull(string Val)
    {

        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        decimal ValF = decimal.Parse(ValS);
        return ValF;


    }

    void UpdateCBalance()
    {

        string query = "Update dbo.issue_opening_balance set Current_Balance = convert(decimal(18,5), Current_Balance)-" + CheckNull(hd_qty.Value) + ",Current_Bags=Current_Bags-'" + hd_bag.Value + "' where District_Id='" + ddl_district.SelectedValue.ToString() + "'and Depotid='" + ddl_IssueCentre.SelectedValue + "' and Commodity_Id='" + hd_commodity.Value + "'and Godown='" + hd_godown.Value + "' and Scheme_Id='" + mscheme + "' and Source='01' ";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = query;
        cmd.Connection = con;

        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            lbl_message.Text = ex.ToString();
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

        }

    }

    protected void ddl_IssueCentre_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

            string qry = "Select distinct Acceptance_No from Acceptance_Note_Detail where Distt_ID = '23" + ddl_district.SelectedValue.ToString() + "' and IssueCenter_ID = '" + ddl_IssueCentre.SelectedValue.ToString() + "' order by Acceptance_No";

            SqlCommand cmdAcc = new SqlCommand(qry, con_WPMS);

            SqlDataAdapter daAcc = new SqlDataAdapter(cmdAcc);

            DataSet dsAcc = new DataSet();
            daAcc.Fill(dsAcc);

            if (dsAcc != null)
            {
                if (dsAcc.Tables[0].Rows.Count > 0)
                {
                    ddlAccptNumber.DataSource = dsAcc.Tables[0];
                    ddlAccptNumber.DataTextField = "Acceptance_No";

                    ddlAccptNumber.DataBind();
                    ddlAccptNumber.Items.Insert(0, "--Select--");

                }
            }
         
            con.Close();
        }
    void UpdateStock()
    {

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        string mfyear = DateTime.Today.Year.ToString();

        int monthu = int.Parse(DateTime.Today.Month.ToString());
        int yearu = int.Parse(DateTime.Today.Year.ToString());
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();


        decimal mrfci = CheckNull(hd_qty.Value);


        string qryinsU = "update dbo.tbl_Stock_Registor set Recieved_Procure= convert(decimal(18,5), Recieved_Procure)-" + mrfci + " where Commodity_Id ='" + hd_commodity.Value + "' and Scheme_ID='" + mscheme + "' and DistrictId='" + ddl_district.SelectedValue.ToString() + "'and DepotID='" + ddl_IssueCentre.SelectedValue + "'";

        SqlCommand cmd = new SqlCommand(qryinsU, con);
        try
        {
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            lbl_message.Text = ex.ToString();
        }
        finally
        {


        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

    }


    protected void bn_del_Click(object sender, EventArgs e)
    {
       
        for (int i = 0; i < dgridchallan.Rows.Count; i++)
        {


            if (((CheckBox)dgridchallan.Rows[i].FindControl("chk_del")).Checked == true || (((CheckBox)dgridchallan.Rows[i].FindControl("chk_receipt")).Checked == true && ((CheckBox)dgridchallan.Rows[i].FindControl("chk_del")).Checked == true))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                if (con_WPMS.State == ConnectionState.Closed)
                {
                    con_WPMS.Open();
                }

                string distid = ddl_district.SelectedValue;

                string issuecenerid = ddl_IssueCentre.SelectedValue;

                string challan = dgridchallan.Rows[i].Cells[2].Text.ToString();

                string acceptnum = ddlAccptNumber.SelectedValue;

                string trucknum = dgridchallan.Rows[i].Cells[4].Text.ToString();
                hd_bag.Value=dgridchallan.Rows[i].Cells[6].Text.ToString();
                hd_qty.Value=dgridchallan.Rows[i].Cells[7].Text.ToString();

                hd_godown.Value = dgridchallan.DataKeys[i].Values[1].ToString();
                hd_commodity.Value = dgridchallan.DataKeys[i].Values[0].ToString();
                if (cons.State == ConnectionState.Closed)
                {
                    cons.Open();
                }


          string mystr = "SELECT *FROM [tbl_Storage_Arrival_Stock] where [DepotId]='" + issuecenerid + "' and AcceptanceNo='" + acceptnum + "' and IssueID='" + dgridchallan.Rows[i].Cells[11].Text.ToString() + "' and Commodity_Id='" + hd_commodity.Value + "' and Truck_No='" + trucknum + "' and Challan_No='" + challan + "'";

     SqlCommand   cmdwhr = new SqlCommand(mystr, cons);
        SqlDataReader sqldr = cmdwhr.ExecuteReader();
        sqldr.Read();

        if (sqldr.HasRows)
        {
            Label1.Visible = true;
           Label1.Text="whr created!Entry cannot deleted ";

        }
        else
        {

            string inslog = "Insert into Acceptance_Note_Detail_DelLog select * from Acceptance_Note_Detail where Distt_ID = '" + distid + "' and IssueCenter_ID = '" + issuecenerid + "' and TC_Number = '" + challan + "' and Truck_No = '" + trucknum + "' and Acceptance_No = '" + ddlAccptNumber.SelectedValue + "'";

            SqlCommand cmd = new SqlCommand(inslog, con);


            string delcon = "delete from Acceptance_Note_Detail where Distt_ID = '" + distid + "' and IssueCenter_ID = '" + issuecenerid + "' and TC_Number = '" + challan + "' and Truck_No = '" + trucknum + "' and Acceptance_No = '" + ddlAccptNumber.SelectedValue + "'";

            SqlCommand cmdcon = new SqlCommand(delcon, con);

            string inslog1 = "Insert into Acceptance_Note_Detail_DelLog select * from Acceptance_Note_Detail where Distt_ID = '23" + distid + "' and IssueCenter_ID = '" + issuecenerid + "' and TC_Number = '" + challan + "' and Truck_No = '" + trucknum + "' and Acceptance_No = '" + ddlAccptNumber.SelectedValue + "'";

            SqlCommand cmd1 = new SqlCommand(inslog1, con_WPMS);

            string delwpms = "delete from Acceptance_Note_Detail where Distt_ID = '23" + distid + "' and IssueCenter_ID = '" + issuecenerid + "' and TC_Number = '" + challan + "' and Truck_No = '" + trucknum + "' and Acceptance_No = '" + ddlAccptNumber.SelectedValue + "'";

            SqlCommand cmdwpms = new SqlCommand(delwpms, con_WPMS);

            try
            {
                int x = cmd.ExecuteNonQuery();

                int y = cmd1.ExecuteNonQuery();

                int a = cmdcon.ExecuteNonQuery();

                int b = cmdwpms.ExecuteNonQuery();


                string log = "Insert into SCSC_Procurement_dellog select * from SCSC_Procurement where Distt_ID = '" + distid + "' and IssueCenter_ID = '" + issuecenerid + "' and TC_Number = '" + challan + "' and Truck_Number = '" + trucknum + "' and Acceptance_No = '" + ddlAccptNumber.SelectedItem.Text + "'";
                SqlCommand dellog = new SqlCommand(log, con);

                dellog.ExecuteNonQuery();

                string update = "Update SCSC_Procurement set Acceptance_No = '' , AN_Status = 'N' where Distt_ID = '" + distid + "' and IssueCenter_ID = '" + issuecenerid + "' and TC_Number = '" + challan + "' and Truck_Number = '" + trucknum + "' and Acceptance_No = '" + ddlAccptNumber.SelectedItem.Text + "'";
                SqlCommand updcmd = new SqlCommand(update, con);

                updcmd.ExecuteNonQuery();


                string logwp = "Insert into IssueCenterReceipt_Online_Log select * from IssueCenterReceipt_Online where DistrictId = '23" + distid + "' and IssueCenter_ID = '" + issuecenerid + "' and TruckChalanNo = '" + challan + "' and TruckNo = '" + trucknum + "' and IssueID = '" + dgridchallan.Rows[i].Cells[11].Text.ToString() + "'";
                SqlCommand inslogwpms = new SqlCommand(logwp, con_WPMS);

                inslogwpms.ExecuteNonQuery();

                string updateWpms = "Update IssueCenterReceipt_Online set  AN_Status = 'N' where DistrictId = '23" + distid + "' and IssueCenter_ID = '" + issuecenerid + "' and TruckChalanNo = '" + challan + "' and TruckNo = '" + trucknum + "' and IssueID = '" + dgridchallan.Rows[i].Cells[11].Text.ToString() + "'";
                SqlCommand uptwpms = new SqlCommand(updateWpms, con_WPMS);

                uptwpms.ExecuteNonQuery();

                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Deleted Sucessfully'); </script> ");

                if (((CheckBox)dgridchallan.Rows[i].FindControl("chk_receipt")).Checked == true)
                {

                    try
                    {
                        string reclog = "Insert into SCSC_Procurement_dellog select * from SCSC_Procurement where Receipt_Id = '" + dgridchallan.Rows[i].Cells[11].Text.ToString() + "' and Distt_ID = '" + ddl_district.SelectedValue.ToString() + "' and IssueCenter_ID = '" + ddl_IssueCentre.SelectedValue + "'  and TC_Number = '" + dgridchallan.Rows[i].Cells[2].Text.ToString() + "' ";

                        SqlCommand cmdlog = new SqlCommand(reclog, con);

                        int a_recept = cmdlog.ExecuteNonQuery();

                        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                        string delqry = "delete from SCSC_Procurement where Receipt_Id = '" + dgridchallan.Rows[i].Cells[11].Text.ToString() + "' and Distt_ID = '" + ddl_district.SelectedValue.ToString() + "' and IssueCenter_ID = '" + ddl_IssueCentre.SelectedValue + "'  and TC_Number = '" + dgridchallan.Rows[i].Cells[2].Text.ToString() + "' ";

                        SqlCommand cmddel = new SqlCommand(delqry, con);

                        int b_recept = cmddel.ExecuteNonQuery();

                        if (b_recept == 0)
                        {
                            Label1.Text = "Not Del from CSMS";
                        }

                        else
                        {
                            Label1.Text = "";

                            string updateqry = "Update SCSC_Procurement_dellog set OperatorID = 'Del by HO' , Deleted_Date = GETDATE(),IP_Address = '" + ip + "'  where Receipt_Id = '" + dgridchallan.Rows[i].Cells[11].Text.ToString() + "' and Distt_ID = '" + ddl_district.SelectedValue.ToString() + "' and IssueCenter_ID = '" + ddl_IssueCentre.SelectedValue + "'  and TC_Number = '" + dgridchallan.Rows[i].Cells[2].Text.ToString() + "' ";

                            SqlCommand cmdup = new SqlCommand(updateqry, con);

                            int c = cmdup.ExecuteNonQuery();
                        }


                        string insWlog = "Insert into IssueCenterReceipt_Online_Log select * from IssueCenterReceipt_Online where IssueID = '" + dgridchallan.Rows[i].Cells[11].Text.ToString() + "' and DistrictId = '23" + ddl_district.SelectedValue.ToString() + "' and IssueCenter_ID = '" + ddl_IssueCentre.SelectedValue + "'  and TruckChalanNo = '" + dgridchallan.Rows[i].Cells[2].Text.ToString() + "' ";

                        SqlCommand cmdWlog = new SqlCommand(insWlog, con_WPMS);

                        int x_recept = cmdWlog.ExecuteNonQuery();


                        string delWqry = "delete from IssueCenterReceipt_Online where IssueID = '" + dgridchallan.Rows[i].Cells[11].Text.ToString() + "' and DistrictId = '23" + ddl_district.SelectedValue.ToString() + "' and IssueCenter_ID = '" + ddl_IssueCentre.SelectedValue + "'  and TruckChalanNo = '" + dgridchallan.Rows[i].Cells[2].Text.ToString() + "' ";

                        SqlCommand cmdWdel = new SqlCommand(delWqry, con_WPMS);

                        int y_recept = cmdWdel.ExecuteNonQuery();

                        if (y_recept == 0)
                        {
                            Label1.Text = "Not Del from WPMS";
                        }
                        else
                        {
                            Label1.Text = "";
                        }


                        UpdateCBalance();

                        UpdateStock();

                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Deleted Sucessfully'); </script> ");
                    }

                    catch (Exception ex)
                    {
                        Label1.Text = ex.Message;
                        Label1.Visible = true;

                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error! To Delete This Entry'); </script> ");
                        return;
                    }

                    finally
                    {


                        if (con_WPMS.State == ConnectionState.Open)
                        {
                            con_WPMS.Close();
                        }

                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                        if (cons.State == ConnectionState.Open)
                        {
                            cons.Close();
                        }
                    }




                }
            }

            catch
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error Occured'); </script> ");
                return;
            }


            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                if (con_WPMS.State == ConnectionState.Open)
                {
                    con_WPMS.Close();
                }
                if (cons.State == ConnectionState.Open)
                {
                    cons.Close();
                }
            }
        }


        }

               
            else
            {

                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Both for Deletion'); </script> ");
 

            }
        }
        dgridchallan.DataBind();

        ddl_district_SelectedIndexChanged(sender, e);
        ddl_IssueCentre_SelectedIndexChanged(sender, e);
        Panel1.Visible = false;
        bn_del.Visible = false;
     
    }

    protected void chk_del_CheckedChanged(object sender, EventArgs e)
    {


      
        foreach (GridViewRow row in dgridchallan.Rows)
        {

            if (((CheckBox)row.FindControl("chk_del")).Checked == true)
            {
                ((CheckBox)row.FindControl("chk_receipt")).Enabled = true;
            }
            else {


                ((CheckBox)row.FindControl("chk_receipt")).Enabled = false;
            }

        }
         
        }

    void GetCommodity()
    {

        try
        {
            if (con_paddy != null)
            {

                if (con_paddy.State == ConnectionState.Closed)
                {

                    con_paddy.Open();   /// con_paddy karna hai
                }

                string qrysel = "select crop,crpcode from Crop_Master where crpcode not in ('8')";

                SqlDataAdapter da = new SqlDataAdapter(qrysel, con_paddy);   /// con_paddy karna hai 
                /// 
                DataSet ds = new DataSet();

                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlcommodtiy.DataSource = ds.Tables[0];
                        ddlcommodtiy.DataTextField = "crop";
                        ddlcommodtiy.DataValueField = "crpcode";
                        ddlcommodtiy.DataBind();
                        ddlcommodtiy.Items.Insert(0, "--Select--");

                    }
                }

            }
            else
            {
            }
        }

        catch (Exception)
        {

            if (con_paddy.State == ConnectionState.Open)
            {

                con_paddy.Close();   /// con_paddy karna hai
            }
        }
        finally
        {
            if (con_paddy.State == ConnectionState.Open)
            {

                con_paddy.Close();   /// con_paddy karna hai
            }
        }
    }
}

  
