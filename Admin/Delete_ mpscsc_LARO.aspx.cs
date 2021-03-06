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
public partial class mpscsc_LARO : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    Districts DObj = null;
    DistributionCenters distobj = null;
    Transporter tobj = null;
    protected Common ComObj = null, cmn = null;
    LARO obj = null;
    LARO objt = null;
    LARO objo = null;
    public string distid = "";
    string roqty = null;
    MoveChallan mobj = null;
    MoveChallan mobjro = null;
    public string getdatef = "";
    public string gateno = "";
    public string amonth = "";
    public string ayear = "";
    public string adminid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {
            adminid = Session["st_id"].ToString();
           
            //txtqtysend.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            //txtnobags.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            //txtmoisture.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

            //txtchallan.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            //txtqtysend.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            //txtnobags.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtrodate.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtroqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtbalqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            //txtmoisture.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtcomdty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtscheme.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
           
            //string dbname = "Warehouse";
            ComObj = new Common(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
           
                    

            if (!IsPostBack)
            {
                //GetRO();
                //Transport();
                ////GetGunny();
                GetDist();
                //GetCategory();
                //GetFCIdist();
                //GetDCName();
               
                //GetdepotType();

            }
            //GetDetails();
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");


        }

    }
    void GetRO()
    {

        ddlrono.Items.Clear();
        string distid = ddldistrictmp.SelectedValue;   
        ddlrono.Items.Insert(0, "--Select--");
        int month =int.Parse (DateTime.Today.Month.ToString());
        int year = int.Parse(DateTime.Today.Year.ToString());
        string qry = "SELECT RO_No,Allot_month FROM dbo.RO_of_FCI where Distt_Id='" + distid + "'";
        cmd.Connection = con;
        cmd.CommandText = qry;
        con.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ddlrono.Items.Add(dr["RO_No"].ToString());



        }
        dr.Close();
        con.Close();

    }
   

    void GetData()
    {
        string distid = ddldistrictmp.SelectedValue;
        if (ddlrono.SelectedItem.Text != "--Select--")
        {
            obj = new LARO(ComObj);
            string qryall = "SELECT RO_of_FCI.Commodity , RO_of_FCI.Distt_Id,RO_of_FCI.RO_Validity, RO_of_FCI.RO_No, RO_of_FCI.RO_date, RO_of_FCI.RO_qty,RO_of_FCI.RO_district, RO_of_FCI.Scheme as Scheme, RO_of_FCI.Rate, RO_of_FCI.Amount, RO_of_FCI.Allot_month,RO_of_FCI.Allot_year, RO_of_FCI.DD_chk_no, RO_of_FCI.DD_chk_date, RO_of_FCI.Remarks, RO_of_FCI.Created_date,RO_of_FCI.updated_date, RO_of_FCI.deleted_date, RO_of_FCI.Balance_Qty,dbo.tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name,dbo.tbl_MetaData_SCHEME.Scheme_Name as Scheme_Name  From dbo.RO_of_FCI Left JOIN dbo.tbl_MetaData_STORAGE_COMMODITY  ON RO_of_FCI.Commodity = dbo.tbl_MetaData_STORAGE_COMMODITY .Commodity_Id left join dbo.tbl_MetaData_SCHEME on RO_of_FCI.Scheme=dbo.tbl_MetaData_SCHEME.Scheme_id  where RO_of_FCI.RO_No='" + ddlrono.SelectedItem + "' and RO_of_FCI.Distt_Id='" + distid + "'";
            DataSet ds = obj.selectAny(qryall);
            DataRow dr = ds.Tables[0].Rows[0];

            string rdate = dr["RO_date"].ToString();
            string rodate = getdate(rdate);
            txtrodate.Text = rodate;
            txtrodate.ReadOnly = true;
            txtrodate.BackColor = System.Drawing.Color.Wheat;

            roqty = dr["RO_qty"].ToString();
            txtroqty.Text = System.Math.Round (CheckNull(dr["RO_qty"].ToString()),5).ToString();
           
            txtroqty.ReadOnly = true;
            

            txtcomdty.Text = dr["Commodity_Name"].ToString();
            txtcomdty.ReadOnly = true;
            txtcomdty.BackColor = System.Drawing.Color.Wheat;

            txtscheme.Text = dr["Scheme_Name"].ToString();
            lblscheme.Text = dr["Scheme"].ToString();
            lblcomdty.Text = dr["Commodity"].ToString();
            txtscheme.ReadOnly = true;
            txtscheme.BackColor = System.Drawing.Color.Wheat;
            txtbalqty.Text = System.Math.Round(CheckNull (dr["Balance_Qty"].ToString()), 5).ToString();
            txtbalqty.ReadOnly = true;
            txtbalqty.BackColor = System.Drawing.Color.Wheat;
            lblmonth.Text = dr["Allot_month"].ToString();
            lblyear.Text=dr["Allot_year"].ToString();
       

        }
        else
        {
            txtrodate.Text = "";
            txtroqty.Text = "";
            txtcomdty.Text = "";
            txtbalqty.Text = "";
        }

    }

    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }
      
   
    void GetDist()
    {
        DObj = new Districts(ComObj);
        DataSet ds = DObj.selectAll(" order by district_name");
        ddldistrictmp.DataSource = ds.Tables[0];
        ddldistrictmp.DataTextField = "district_name";
        ddldistrictmp.DataValueField = "district_Code";
        ddldistrictmp.DataBind();
        ddldistrictmp.Items.Insert(0, "--Select--");
      
    }
  
  

    void fillgrid()
    {
        mobjro = new MoveChallan(ComObj);
        string qryro = "SELECT Transport_Order_againstRo.Trunsuction_Id,Lift_A_RO.* ,dbo.tbl_MetaData_DEPOT.DepotName as Depot_Name,dbo.tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name  from dbo.Lift_A_RO left join  dbo.tbl_MetaData_DEPOT on Lift_A_RO.Issue_center=dbo.tbl_MetaData_DEPOT.DepotID left join dbo.tbl_MetaData_STORAGE_COMMODITY  on Lift_A_RO.Commodity=dbo.tbl_MetaData_STORAGE_COMMODITY .Commodity_Id  left join   dbo.Transport_Order_againstRo ON Lift_A_RO.Dist_Id = Transport_Order_againstRo.Distt_Id AND Lift_A_RO.RO_No = Transport_Order_againstRo.RO_No AND Lift_A_RO.TO_Number = Transport_Order_againstRo.TO_Number AND Lift_A_RO.Send_District = Transport_Order_againstRo.toDistrict AND Lift_A_RO.Issue_center = Transport_Order_againstRo.toIssueCenter  where Lift_A_Ro.RO_No='" + ddlrono.SelectedItem + "' and Lift_A_Ro.Dist_Id='" + ddldistrictmp.SelectedValue  + "'";
        DataSet dsro = mobjro.selectAny(qryro);
        GridView1.DataSource = dsro.Tables[0];
        GridView1.DataBind();
    }

    protected void ddlrono_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetData();
        fillgrid();
        GetTO();
        Label2.Visible = false;
    }
    void GetTO()
    {
        ddltono.Items.Clear();
        string distid = ddldistrictmp.SelectedValue;
        string mro = ddlrono.SelectedValue;
       ddltono.Items.Insert(0, "--Select--");
        string qryro = "SELECT distinct(TO_Number) From dbo.Transport_Order_againstRo where Distt_Id='" + distid + "' and RO_No='" + mro + "'";
        cmd.Connection = con;
        cmd.CommandText = qryro;
        con.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ddltono.Items.Add(dr["TO_Number"].ToString());



        }
        dr.Close();
        con.Close();

    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GetGunny();

    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRO();
        ////GetDCName();
        //distobj = new DistributionCenters(ComObj);
        //string ord = "Districtid='23" + ddldistrict.SelectedValue.ToString() + "' order by DepotName";
        ////string ord = "Select DepotName,DepotId from dbo.tbl_MetaData_DEPOT order by DepotName ";
        //DataSet ds = distobj.select(ord);

        //ddlissue.DataSource = ds.Tables[0];
        //ddlissue.DataTextField = "DepotName";
        //ddlissue.DataValueField = "DepotId";

        //ddlissue.DataBind();
        //ddlissue.Items.Insert(0, "--Select--");
    }
    protected void ddlgtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GetDist();
    }
   
    public string getmmddyy(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("MM/d/yyyy");
    }
    //void GetTotal()
    //{
    //    string mscheme = lblscheme.Text;
    //    string mcomdty = lblcomdty.Text;
    //    int month = int.Parse(lblmonth.Text);
    //    int year = int.Parse(lblyear.Text);
    //    string qryGD = "Select Sum(Qty_send) as Qty_send   from dbo.Lift_A_RO where Scheme='" + mscheme + "'and Commodity='" + mcomdty + "' and Dist_Id='" + distid + "' and Month=" + month + "and Year=" + year;

    //    DObj = new Districts(ComObj);
    //    DataSet dsGD = DObj.selectAny(qryGD);
    //    if (dsGD.Tables[0].Rows.Count==0)
    //    {           


    //    }
    //    else
    //    {
    //        DataRow drGD = dsGD.Tables[0].Rows[0];
    //        float liftq = CheckNull(drGD["Qty_send"].ToString());

    //        string qrydallocU = "Update dbo.District_Alloc set Lifted_Qty =" + liftq + " where Scheme_ID='" + mscheme + "'and Commodity_ID='" + mcomdty + "' and district_code='" + distid + "'and Month=" + month + "and Year=" + year;
    //        cmd.Connection = con;
    //        cmd.CommandText = qrydallocU;
    //        try
    //        {

                
    //            cmd.ExecuteNonQuery();
               
    //        }
    //        catch (Exception ex)
    //        {
    //        }
    //        finally
    //        {
    //            con.Close();
    //            ComObj.CloseConnection();
               

    //        }

    //    }

    //}

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalamt"));

            string griddate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Challan_Date"));

            getdatef = getdate(griddate);


            Label lbl = (Label)e.Row.FindControl("lblChallan");
            lbl.Text = getdatef;

            //decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalamt"));
            //decimal rowTotalQty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "quantity"));

            //grdTotal = grdTotal + rowTotal;
            //grdTotalQty = grdTotalQty + rowTotalQty;

        }
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        decimal doqty = 0;
        Label2.Visible = false;
        string qrydoqty = "Select sum(qty_issue) as qty_issue from dbo.issue_against_do where district_code='" + ddldistrictmp.SelectedValue + "'and  Godown='" + ddlrono.SelectedValue + "'";
        mobjro = new MoveChallan(ComObj);
        DataSet dsdo = mobjro.selectAny(qrydoqty);
        if (dsdo == null)
        {

        }
        else
        {
            if (dsdo.Tables[0].Rows.Count == 0)
            {

            }
            else
            {
                DataRow drid = dsdo.Tables[0].Rows[0];

                doqty = CheckNull(drid["qty_issue"].ToString());

            }


        }


        if (ddlrono.SelectedItem.Text == "--Select--" || ddltono.SelectedItem.Text == "--Select--")
        {
            Label2.Visible = true;
            Label2.Text = "Please Select R.O/T.O. Number";

        }
        else
        {
            Label2.Visible = false;
            string gchalan = "Select challan_no from dbo.tbl_Receipt_Details where RO_No='" + ddlrono.SelectedValue + "'and TO_Number='" + ddltono.SelectedValue + "'";
            mobjro = new MoveChallan(ComObj);
            DataSet dsch = mobjro.selectAny(gchalan);
            if (dsch == null)
            {

            }
            else
            {
                if (dsch.Tables[0].Rows.Count == 0)
                {
                    string distid = ddldistrictmp.SelectedValue;
                    string ro_no = ddlrono.SelectedValue;
                    string to_no = ddltono.SelectedValue;
                    string qrydlt = "delete from dbo.Transport_Order_againstRo where RO_No='" + ro_no + "' and Distt_Id='" + distid + "' and TO_Number='" + to_no + "'";
                    cmd.CommandText = qrydlt;

                    try
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        string qrydltlift = "delete from dbo.Lift_A_Ro where RO_No='" + ro_no + "' and Dist_Id='" + distid + "' and TO_Number='" + to_no + "'";
                        cmd.CommandText = qrydltlift;

                        cmd.CommandText = qrydltlift;
                        cmd.ExecuteNonQuery();

                        string qrytoalot = "delete from dbo.TO_Allot_Lift where RO_No='" + ro_no + "' and Distt_Id='" + distid + "'";
                        cmd.CommandText = qrytoalot;

                        cmd.ExecuteNonQuery();
                        string qryrou = "Update RO_of_FCI set Balance_Qty=" + (CheckNull(txtroqty.Text) - doqty) + "  where RO_No='" + ro_no + "' and Distt_Id='" + distid + "'";
                        cmd.CommandText = qryrou;

                        cmd.ExecuteNonQuery();

                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Deleted Successfully..'); </script> ");
                        btnsubmit.Enabled = false;



                    }
                    catch (Exception ex)
                    {

                        lbldisply.Visible = true;
                        lbldisply.Text = ex.Message;
                    }
                    finally
                    {
                        con.Close();
                    }

                }
                else
                {
                    Label2.Visible = true;
                    Label2.Text = "Sorry You Can't Delete This R.O. because some details has beeen deposited at issue Center";

                }

            }
        }
       
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string distid = ddldistrictmp.SelectedValue;
        gateno = GridView1.SelectedRow.Cells[3].Text;
        Session["RO_No"] = gateno;
        Session["TO_No"] = GridView1.SelectedRow.Cells[4].Text;
        Session["Trans"] = GridView1.SelectedRow.Cells[11].Text;
        Session["TIC"] = GridView1.SelectedRow.Cells[12].Text;
        Session["Challan"] = GridView1.SelectedRow.Cells[1].Text;
        Session["Dist"] =distid;       

        //string tag = "Y";
        //mobj = new MoveChallan(ComObj);
        //string tono = GridView1.SelectedRow.Cells[4].Text;
        //string mchallan = GridView1.SelectedRow.Cells[1].Text;
        //string qry = "SELECT IsRecieved FROM dbo.Lift_A_RO where Dist_Id='" + distid + "'and RO_No='" + gateno + "'and Challan_No='" + mchallan + "'and TO_Number='" + tono + "'";
        //DataSet ds = mobj.selectAny(qry);
        //DataRow dr = ds.Tables[0].Rows[0];
        //string st = dr["IsRecieved"].ToString().Trim();
        //if (st == tag)
        //{
        //    Label2.Visible = true;
        //    Label2.Text = "Sorry You Can't Edit This Details ,It has been Deposited";
        //}
        //else
        //{
            Label2.Visible = false;
            Response.Redirect("~/Admin/Edit_LARO_Page.aspx");
        //}


       
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
       
       
    }
    decimal CheckNull(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        decimal ValF = decimal.Parse(ValS);
        return ValF;

    }
    decimal CheckNullDecimal(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");

        decimal ValF = decimal.Parse(ValS);
        return ValF;

    }
    Int32 CheckNullInt(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        int ValF = int.Parse(ValS);
        return ValF;

    }
    protected void ddltono_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetToDetail();


    }
   
    void GetToDetail()
    {
        string mtono = ddltono.SelectedValue;
        string mro = ddlrono.SelectedValue;
        string distid = ddldistrictmp.SelectedValue;
        mobjro = new MoveChallan(ComObj);
        string qryro = "SELECT DepoCode.DepoName,DepoCode.District , Transport_Order_againstRo.toDistrict,Transport_Order_againstRo.Trunsuction_Id,Transport_Order_againstRo.toIssueCenter,Transport_Order_againstRo.FCI_district,Transport_Order_againstRo.FCI_Depot,Transport_Order_againstRo.TO_Number,Transport_Order_againstRo.Transporter_Name,Round(Transport_Order_againstRo.Cumulative_Qty,5) as Cumulative_Qty ,Round(Transport_Order_againstRo.Pending_Qty,5) as Pending_Qty ,Transport_Order_againstRo.Quantity,toDistrict,toIssueCenter,Transporter_Table.Transporter_Name as Tname,tbl_MetaData_DEPOT.DepotName as DepotName ,districtsmp.district_name as district_name From dbo.Transport_Order_againstRo left join dbo.Transporter_Table on Transport_Order_againstRo.Transporter_Name=Transporter_Table.Transporter_ID  left join pds.districtsmp on Transport_Order_againstRo.toDistrict=districtsmp.district_code left join dbo.tbl_MetaData_DEPOT on Transport_Order_againstRo.toIssueCenter=tbl_MetaData_DEPOT.DepotID  left join dbo.DepoCode on Transport_Order_againstRo.FCI_district=DepoCode.District_Code  and Transport_Order_againstRo.FCI_depot=DepoCode.DepoCode    where Transport_Order_againstRo.Distt_Id='" + distid + "' and Transport_Order_againstRo.RO_No='" + mro + "'and Transport_Order_againstRo.TO_Number='" + mtono + "'";
        //string todata = "Select Transport_Order_againstRo.TO_Number,Transport_Order_againstRo.Transporter_Name,Transport_Order_againstRo.Quantity,FCI_district,FCI_Depot,toDistrict,toIssueCenter from dbo.Transport_Order_againstRo  where Transport_Order_againstRo.Distt_Id='" + distid + "' and Transport_Order_againstRo.RO_No='" + mro + "'and Transport_Order_againstRo.TO_Number='" + mtono + "'";

        DataSet dstod = mobjro.selectAny(qryro);

       this.GridView2.DataSource = dstod;
       this.GridView2.DataBind();
       //GridView2.Columns[6].Visible = false;
       //GridView2.Columns[8].Visible = false;

       

    }
    protected void ddltono_SelectedIndexChanged1(object sender, EventArgs e)
    {
        GetToDetail();
       
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/Admin/AdminWelcome.aspx");
    }
    protected void ddlfcidepo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddldepottype_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        //GetDetails();
    }
    //void GetDetails()
    //{
       
    //        if (GridView2.Rows.Count == 0)
    //        {
    //        }
    //        else
    //        {

    //            foreach (GridViewRow gr in GridView2.Rows)
    //            {
    //                CheckBox GchkBx = (CheckBox)gr.FindControl("cbSelectAll");

    //                if (GchkBx.Checked == true)
    //                {
    //                    string todist = gr.Cells[6].Text;
    //                    string tdepot = gr.Cells[8].Text;
    //                    //txtqtysend.Text = gr.Cells[11].Text;
    //                    lblpendqty.Text = gr.Cells[11].Text;
    //                    lbltid.Text = gr.Cells[12].Text;
    //                    //txtqtysend.Text = gr.Cells[11].Text;
    //                    string mtono = ddltono.SelectedValue;
    //                    string mro = ddlrono.SelectedValue;
    //                    mobjro = new MoveChallan(ComObj);
    //                    string qryro = "SELECT DepoCode.DepoName as FDepo,DepoCode.District,Transport_Order_againstRo.toDistrict,Transport_Order_againstRo.toIssueCenter,Transport_Order_againstRo.FCI_district,Transport_Order_againstRo.FCI_Depot,Transport_Order_againstRo.TO_Number,Transport_Order_againstRo.Transporter_Name,Transport_Order_againstRo.Cumulative_Qty,Transport_Order_againstRo.Pending_Qty,Transport_Order_againstRo.Quantity,toDistrict,toIssueCenter,Transporter_Table.Transporter_Name as Tname,tbl_MetaData_DEPOT.DepotName as DepotName ,districtsmp.district_name as district_name From dbo.Transport_Order_againstRo left join dbo.Transporter_Table on Transport_Order_againstRo.Transporter_Name=Transporter_Table.Transporter_ID  left join pds.districtsmp on Transport_Order_againstRo.toDistrict=districtsmp.district_code left join dbo.tbl_MetaData_DEPOT on Transport_Order_againstRo.toIssueCenter=tbl_MetaData_DEPOT.DepotID left join dbo.DepoCode on DepoCode.District_Code=Transport_Order_againstRo.FCI_district and DepoCode.DepoCode=Transport_Order_againstRo.FCI_depot   where Transport_Order_againstRo.Distt_Id='" + distid + "' and Transport_Order_againstRo.RO_No='" + mro + "'and Transport_Order_againstRo.TO_Number='" + mtono + "' and Transport_Order_againstRo.toDistrict='" + todist + "'and Transport_Order_againstRo.toIssueCenter='" + tdepot + "'and Transport_Order_againstRo.IsLifted='N'";
    //                    //string todata = "Select Transport_Order_againstRo.TO_Number,Transport_Order_againstRo.Transporter_Name,Transport_Order_againstRo.Quantity,FCI_district,FCI_Depot,toDistrict,toIssueCenter from dbo.Transport_Order_againstRo  where Transport_Order_againstRo.Distt_Id='" + distid + "' and Transport_Order_againstRo.RO_No='" + mro + "'and Transport_Order_againstRo.TO_Number='" + mtono + "'";

    //                    DataSet dstod = mobjro.selectAny(qryro);
    //                    if (dstod.Tables[0].Rows.Count==0)
    //                    {
    //                    }
    //                    else
    //                    {
    //                        DataRow dr = dstod.Tables[0].Rows[0];
    //                        txttrans.SelectedItem.Text = dr["Tname"].ToString();
    //                        txttrans.SelectedValue = dr["Transporter_Name"].ToString();
    //                        //txtqtysend.Text = dr["Quantity"].ToString();
    //                        ddlfcidist.SelectedItem.Text = dr["District"].ToString();
    //                        ddlfcidepo.SelectedItem.Text = dr["FDepo"].ToString();
    //                        lblfdepo.Text = dr["FCI_district"].ToString();
    //                        lblfdist.Text = dr["FCI_depot"].ToString();
    //                        ddldistrict.SelectedItem.Text = dr["district_name"].ToString();
    //                        ddldistrict.SelectedValue = dr["toDistrict"].ToString();
    //                        ddlissue.SelectedItem.Text = dr["DepotName"].ToString();
    //                        ddlissue.SelectedValue = dr["toIssueCenter"].ToString();

    //                        ddldistrict.BackColor = System.Drawing.Color.Wheat;
    //                        ddlissue.BackColor = System.Drawing.Color.Wheat;
    //                        ddlfcidist.BackColor = System.Drawing.Color.Wheat;
    //                        ddlfcidepo.BackColor = System.Drawing.Color.Wheat;
    //                        //txtqtysend.ReadOnly = true;
    //                        txttrans.BackColor = System.Drawing.Color.Wheat;
    //                        ddldistrict.Enabled = false;
    //                        ddlissue.Enabled = false;
    //                        ddlfcidist.Enabled = false;
    //                        ddlfcidepo.Enabled = false;
    //                        txtvehno.Focus();
    //                        GetChallan();
                            

    //                    }



    //                }
    //                else
    //                {
    //                    if (GchkBx.Checked == false)
    //                    {
    //                        GetChallan();
    //                    }
    //                    else
    //                    {
                            
    //                        //Transport();
    //                        //GetDist();
    //                        //GetCategory();
    //                        //GetFCIdist();
    //                        //GetDCName();
    //                        //ddldistrict.BackColor = System.Drawing.Color.White;
    //                        //ddlissue.BackColor = System.Drawing.Color.White;
    //                        //ddlfcidist.BackColor = System.Drawing.Color.White;
    //                        //ddlfcidepo.BackColor = System.Drawing.Color.White;
    //                        ////txtqtysend.ReadOnly = true;
    //                        //txtqtysend.BackColor = System.Drawing.Color.White;
    //                        //txttrans.BackColor = System.Drawing.Color.White;
    //                        //ddldistrict.Enabled = true;
    //                        //ddlissue.Enabled = true;
    //                        //ddlfcidist.Enabled = true;
    //                        //ddlfcidepo.Enabled = true;
    //                        //lblfdepo.Text = "";
    //                        //lblfdist.Text = "";
    //                    }
    //                }


                
    //        }
            
    //    }
    //}
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)

    {
        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    //Find the checkbox control in header and add an attribute
        //    ((CheckBox)e.Row.FindControl("cbSelectAll")).Attributes.Add("onclick", "javascript:SelectAll('" + ((CheckBox)e.Row.FindControl("cbSelectAll")).ClientID + "')");
        //}
    }
    protected void btnGet_Click(object sender, EventArgs e)
    {
              
    }



    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    public void FooterPagerClick(object sender, CommandEventArgs e)
    {
        if (GridView1.PageCount == 0)
        {
        }
        else
        {
            //Used by external paging
            string arg;
            arg = e.CommandArgument.ToString();

            switch (arg)
            {
                case "next":
                    //The next Button was Clicked
                    if ((GridView1.PageIndex < (GridView1.PageCount - 1)))
                    {
                        GridView1.PageIndex += 1;
                    }

                    break;

                case "prev":
                    //The prev button was clicked
                    if ((GridView1.PageIndex > 0))
                    {
                        GridView1.PageIndex -= 1;
                    }

                    break;

                case "last":
                    //The Last Page button was clicked
                    GridView1.PageIndex = (GridView1.PageCount - 1);
                    break;

                default:
                    //The First Page button was clicked
                    GridView1.PageIndex = Convert.ToInt32(arg);
                    break;
            }
            fillgrid();
        }
    }
    protected void ddlchallan_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        GetChallanData();
    }
    void GetChallan()
    {
      

    }
    void GetChallanData()
    {
      
    }
    protected void ddldistrictmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRO();
    }
    protected void btnaddmore_Click(object sender, EventArgs e)
    {

    }
    protected void Lastbutton_Click(object sender, EventArgs e)
    {

    }
}
