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


public partial class IssueCenter_Delete_Request : System.Web.UI.Page
{

    public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_PPMS2013"].ToString());
    public SqlConnection con_Maze = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_MPMS2013"].ToString());
    public SqlConnection con_WPMS = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2014"].ToString());
    public SqlConnection cons = new SqlConnection(ConfigurationManager.ConnectionStrings["connstorage"].ToString());

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    MoveChallan mobj2 = null;
    DataTable dtEstimateItems = new DataTable();

    MoveChallan mobj1 = null;
    protected Common ComObj = null, cmn = null;
    public static string distname = "";
    public static string dist = "";
    public string sid = "";
    public string ssid = "";
    public static string dname = "";
    public string snid = "";
    public static string distid = "";
    public string dipotid = "";
    public string cdate = "";
    public static string issueid = "";

    int totalSB;

    double totalSQ;

    int totalRB;

    double totalRQ;

    int totalRjB;

    double totalRjQ;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (con_WPMS.State == ConnectionState.Closed)
            {
                con_WPMS.Open();
            }

            if (!IsPostBack)
            {



                issueid = Session["issue_id"].ToString();
                dist = Session["dist_id"].ToString();

                txtdist.Text = Session["dist_id"].ToString();
                txtissue.Text = Session["issue_id"].ToString();

                string qrysel = "select crop,crpcode from Crop_Master where crpcode not in ('8')";

                SqlDataAdapter da = new SqlDataAdapter(qrysel, con_WPMS);

                DataSet ds1 = new DataSet();
                da.Fill(ds1);
                if (ds1 != null)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        ddlcrop.DataSource = ds1.Tables[0];
                        ddlcrop.DataTextField = "crop";
                        ddlcrop.DataValueField = "crpcode";
                        ddlcrop.DataBind();
                        ddlcrop.Items.Insert(0, "--Select--");

                    }
                }
                else
                {

                }

                string qrey = "select DistrictId,DepotName  from dbo.tbl_MetaData_DEPOT where DepotID='" + txtissue.Text + "'";
                SqlCommand cmd1 = new SqlCommand(qrey, con);

                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);

                DataSet ds = new DataSet();

                da1.Fill(ds);

                DataRow dr = ds.Tables[0].Rows[0];
                distid = dr["DistrictId"].ToString();
                dname = dr["DepotName"].ToString();
                //dist = distid.Substring(2, 2);

                string qrey2 = "select district_name from pds.districtsmp where district_code='" + txtdist.Text + "'";

                SqlCommand cmddist = new SqlCommand(qrey2, con);

                SqlDataAdapter dadist = new SqlDataAdapter(cmddist);

                DataSet dsdist = new DataSet();

                dadist.Fill(dsdist);


                DataRow dr2 = dsdist.Tables[0].Rows[0];
                distname = dr2["district_name"].ToString();


            txtdist.Text =distname ;
            txtissue.Text = dname;

            }

            if (con_WPMS.State == ConnectionState.Open)
            {
                con_WPMS.Close();
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

        }

        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

    }

    void GetName()
    {
        mobj2 = new MoveChallan(ComObj);
        string qry1dt = "select district_name  from pds.districtsmp where district_code='" + dist + "'";
        DataSet ds1dt = mobj2.selectAny(qry1dt);
        DataRow dr1dt = ds1dt.Tables[0].Rows[0];
        txtdist.Text = dr1dt["district_name"].ToString();


        mobj2 = new MoveChallan(ComObj);
        string qryissue = "select DepotName  from dbo.tbl_MetaData_DEPOT where DepotID='" + issueid + "'";
        DataSet dsic = mobj2.selectAny(qryissue);
        DataRow dric = dsic.Tables[0].Rows[0];
        txtissue.Text = dric["DepotName"].ToString();

      

    }

    protected void ddlcrop_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (con_WPMS.State == ConnectionState.Closed)
        {
            con_WPMS.Open();
        }

        if (ddlcrop.SelectedValue == "0" || ddlcrop.SelectedItem.Text == "--Select--")
        {

        }

        else
        {
            Session["Commodity_Id"] = ddlcrop.SelectedValue;

            string qry = "Select distinct Acceptance_No from Acceptance_Note_Detail where Distt_ID = '23" + txtdist.Text + "' and IssueCenter_ID = '" + txtissue.Text + "' order by Acceptance_No";

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
            else
            {

            }
        }

        if (con_WPMS.State == ConnectionState.Open)
        {
            con_WPMS.Close();
        }

    }

    protected void ddlAccptNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAccptNumber.SelectedItem.Text == "--Select--")
        {

        }

        else
        {
         
            GetDistt();
        }
    }

    void GetDistt()
    {
        if (con_WPMS.State == ConnectionState.Closed)
        {
            con_WPMS.Open();
        }

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }



        //lblgno.Text = sid;
        string daten = DateTime.Now.ToString();
        string gdaten = getdate(daten);
        //lblgdtae.Text = gdaten;

        # region Wheat
        if (Session["Commodity_Id"].ToString() == "1")
        {
            try
            {
                if (con_WPMS.State == ConnectionState.Closed)
                {
                    con_WPMS.Open();
                }

                string str = "SELECT IssueCenterReceipt_Online.*,tbl_MPWLC_Godown_Storage.GodownNO,CONVERT(varchar,IssueCenterReceipt_Online.DateOfIssue,106)as DateOfIssue1,Acceptance_Note_Detail.Bags as Acc_Bag,Acceptance_Note_Detail.Accept_Qty,Acceptance_Note_Detail.Reject_Qty,Acceptance_Note_Detail.Acceptance_No,Acceptance_Note_Detail.Acceptance_Date,Crop_Master.crop as Commodity_Name,TransportMaster.Transporter_Name,Districts.District_Name,(Society.Society_Name+','+Society.SocPlace)as Society_Name , isnull(Acceptance_Note_Detail.Stiching_bags_Good,0)Stiching_bags_Good ,isnull(Acceptance_Note_Detail.Stiching_bags_Bad,0)Stiching_bags_Bad , isnull(Acceptance_Note_Detail.Stencile_bags_Good,0)Stencile_bags_Good ,ISNULL(Acceptance_Note_Detail.Stencile_bags_Bad,0)Stencile_bags_Bad  FROM IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId left join TransportMaster on TransportMaster.Transporter_ID=IssueCenterReceipt_Online.TransporterId and TransportMaster.SocietyCode = IssueCenterReceipt_Online.SocietyID left join Acceptance_Note_Detail on Acceptance_Note_Detail.TC_Number = IssueCenterReceipt_Online.TruckChalanNo and Acceptance_Note_Detail.IssueID = IssueCenterReceipt_Online.IssueID left join Districts on Districts.District_Code = IssueCenterReceipt_Online.Sending_District left join Society on Society.Society_Id= IssueCenterReceipt_Online.SocietyID  left join tbl_MPWLC_Godown_Storage on IssueCenterReceipt_Online.Recd_Godown = tbl_MPWLC_Godown_Storage.DepotGodownID where IssueCenterReceipt_Online.DistrictId='23" + dist + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and Acceptance_Note_Detail.Acceptance_No='" + ddlAccptNumber.SelectedItem.Text + "'";

                SqlDataAdapter daP = new SqlDataAdapter(str, con_WPMS);
                DataSet dsP = new DataSet();
                daP.Fill(dsP);
                grd_viewDepot.DataSource = dsP.Tables[0];
                grd_viewDepot.DataBind();
           	 hd_sendingdist.Value   = dsP.Tables[0].Rows[0]["Sending_District"].ToString();
              hd_creat.Value   = dsP.Tables[0].Rows[0]["CreatedDate"].ToString();

              hd_truck.Value = dsP.Tables[0].Rows[0]["TruckNo"].ToString();
              hd_quantity.Value = dsP.Tables[0].Rows[0]["Accept_Qty"].ToString();

               hd_dispach.Value   = dsP.Tables[0].Rows[0]["Recd_Date"].ToString();
                lblsenddist.Text = dsP.Tables[0].Rows[0]["District_Name"].ToString();
                HiddenField1.Value = dsP.Tables[0].Rows[0]["PCID"].ToString();
                lblpccenter.Text = dsP.Tables[0].Rows[0]["Society_Name"].ToString();
                cdate = dsP.Tables[0].Rows[0]["DateOfIssue"].ToString();
                string gdate = getdateM(cdate);

                lblcrop.Text = dsP.Tables[0].Rows[0]["CropYear"].ToString();
                hd_accpdate.Value = dsP.Tables[0].Rows[0]["Acceptance_Date"].ToString();

                lblmoisture.Text = getdateM(dsP.Tables[0].Rows[0]["Acceptance_Date"].ToString());
      
            }
            catch (Exception ex)
            {
                lbleror.Visible = true;
                lbleror.Text = ex.Message;

            }
            finally
            {

                con_WPMS.Close();
            }
        }
        # endregion

        # region Paddy
        else if (Session["Commodity_Id"].ToString() == "2" || Session["Commodity_Id"].ToString() == "3")
        {
            try
            {
                if (con_paddy.State == ConnectionState.Closed)
                {
                    con_paddy.Open();
                }

                string str = " SELECT IssueCenterReceipt_Online.*,CONVERT(varchar,IssueCenterReceipt_Online.DateOfIssue,106)as DateOfIssue1,Acceptance_Note_Detail.Bags as Acc_Bag,Acceptance_Note_Detail.Accept_Qty,Acceptance_Note_Detail.Reject_Qty,Acceptance_Note_Detail.Acceptance_No,Acceptance_Note_Detail.Acceptance_Date,Crop_Master.crop as Commodity_Name,TransportMaster.Transporter_Name,Districts.District_Name,(Society.Society_Name+','+Society.SocPlace)as Society_Name FROM IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId left join TransportMaster on TransportMaster.Transporter_ID=IssueCenterReceipt_Online.TransporterId and TransportMaster.SocietyCode = IssueCenterReceipt_Online.SocietyID left join Acceptance_Note_Detail on Acceptance_Note_Detail.TC_Number = IssueCenterReceipt_Online.TruckChalanNo left join Districts on Districts.District_Code = IssueCenterReceipt_Online.Sending_District left join Society on Society.Society_Id= IssueCenterReceipt_Online.SocietyID where IssueCenterReceipt_Online.DistrictId='" + distid + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and Acceptance_Note_Detail.Acceptance_No='" + sid + "'";
                SqlDataAdapter daP = new SqlDataAdapter(str, con_paddy);
                DataSet dsP = new DataSet();
                daP.Fill(dsP);
                grd_viewDepot.DataSource = dsP.Tables[0];
                grd_viewDepot.DataBind();
                lblsenddist.Text = dsP.Tables[0].Rows[0]["District_Name"].ToString();
                lblpccenter.Text = dsP.Tables[0].Rows[0]["Society_Name"].ToString();
                cdate = dsP.Tables[0].Rows[0]["DateOfIssue"].ToString();
                string gdate = getdateM(cdate);


                lblcrop.Text = dsP.Tables[0].Rows[0]["CropYear"].ToString();

                lblmoisture.Text = getdateM(dsP.Tables[0].Rows[0]["Acceptance_Date"].ToString());
            }
            catch (Exception ex)
            {
                lbleror.Visible = true;
                lbleror.Text = ex.Message;
                lbleror.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                con_paddy.Close();
            }
        }

        # endregion

        # region Maize
        else if (Session["Commodity_Id"].ToString() == "4" || Session["Commodity_Id"].ToString() == "5" || Session["Commodity_Id"].ToString() == "6" || Session["Commodity_Id"].ToString() == "7" || Session["Commodity_Id"].ToString() == "8")
        {
            try
            {
                if (con_Maze.State == ConnectionState.Closed)
                {
                    con_Maze.Open();
                }
                string str = " SELECT IssueCenterReceipt_Online.*,CONVERT(varchar,IssueCenterReceipt_Online.DateOfIssue,106)as DateOfIssue1,Acceptance_Note_Detail.Bags as Acc_Bag,Acceptance_Note_Detail.Accept_Qty,Acceptance_Note_Detail.Reject_Qty,Acceptance_Note_Detail.Acceptance_No,Acceptance_Note_Detail.Acceptance_Date,Crop_Master.crop as Commodity_Name,TransportMaster.Transporter_Name,Districts.District_Name,(Society.Society_Name+','+Society.SocPlace)as Society_Name FROM IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId left join TransportMaster on TransportMaster.Transporter_ID=IssueCenterReceipt_Online.TransporterId and TransportMaster.SocietyCode = IssueCenterReceipt_Online.SocietyID left join Acceptance_Note_Detail on Acceptance_Note_Detail.TC_Number = IssueCenterReceipt_Online.TruckChalanNo left join Districts on Districts.District_Code = IssueCenterReceipt_Online.Sending_District left join Society on Society.Society_Id= IssueCenterReceipt_Online.SocietyID where IssueCenterReceipt_Online.DistrictId='" + distid + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and Acceptance_Note_Detail.Acceptance_No='" + sid + "'";
                SqlDataAdapter daP = new SqlDataAdapter(str, con_Maze);
                DataSet dsP = new DataSet();
                daP.Fill(dsP);
                grd_viewDepot.DataSource = dsP.Tables[0];
                grd_viewDepot.DataBind();

                lblsenddist.Text = dsP.Tables[0].Rows[0]["District_Name"].ToString();
                lblpccenter.Text = dsP.Tables[0].Rows[0]["Society_Name"].ToString();
                cdate = dsP.Tables[0].Rows[0]["DateOfIssue"].ToString();
                string gdate = getdateM(cdate);

                lblcrop.Text = dsP.Tables[0].Rows[0]["CropYear"].ToString();
             


                lblmoisture.Text = getdateM(dsP.Tables[0].Rows[0]["Acceptance_Date"].ToString());
               
            }
            catch (Exception ex)
            {
                lbleror.Visible = true;
                lbleror.Text = ex.Message;

            }
            finally
            {
                con_Maze.Close();
            }
        }

        # endregion

        # region Others
        else
        {

            mobj1 = new MoveChallan(ComObj);
            string qrey4 = "SELECT SCSC_Procurement.*,(SCSC_Procurement.No_of_Bags)as Bags,(SCSC_Procurement.Quantity)as Quantity,(SCSC_Procurement.TC_Number)as TruckChalanNo,(SCSC_Procurement.Truck_Number)as TruckNo ,CONVERT(varchar,SCSC_Procurement.Dispatch_Date,106)as DateOfIssue1,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name,(Acceptance_Note_Detail.Acceptance_No)as Acceptance_No1,Acceptance_Note_Detail.Bags as Acc_Bag,Acceptance_Note_Detail.Acceptance_Date,Acceptance_Note_Detail.Accept_Qty,Acceptance_Note_Detail.Reject_Qty,tbl_MetaData_Purchase_Center.PurchaseCenterName,Transporter_Table.Transporter_Name as Transporter_Name,tbl_MetaData_DEPOT.DepotName as DepotName,districtsmp.district_name as district_name   FROM dbo.SCSC_Procurement left join dbo.tbl_MetaData_STORAGE_COMMODITY on SCSC_Procurement.Commodity_Id=tbl_MetaData_STORAGE_COMMODITY .Commodity_Id left join dbo.Transporter_Table on SCSC_Procurement.Transporter_ID =Transporter_Table.Transporter_ID left join pds.districtsmp on SCSC_Procurement.Sending_District=districtsmp.district_code left join dbo.tbl_MetaData_DEPOT on SCSC_Procurement.Purchase_Center=tbl_MetaData_DEPOT.DepotID left join Acceptance_Note_Detail on Acceptance_Note_Detail.TC_Number = SCSC_Procurement.TC_Number left join tbl_MetaData_Purchase_Center on tbl_MetaData_Purchase_Center.PcId = SCSC_Procurement.Purchase_Center where SCSC_Procurement.Distt_ID='" + distid + "' and  SCSC_Procurement.IssueCenter_ID='" + issueid + "' and Acceptance_Note_Detail.Acceptance_No='" + sid + "'";

            DataSet ds4 = mobj1.selectAny(qrey4);
            DataRow dr4 = ds4.Tables[0].Rows[0];
            grd_viewDepot.DataSource = ds4.Tables[0];
            grd_viewDepot.DataBind();
          
            lblsenddist.Text = dr4["district_name"].ToString();
            lblpccenter.Text = dr4["PurchaseCenterName"].ToString();
            cdate = dr4["Dispatch_Date"].ToString();
            string gdate = getdate(cdate);
      

            
            lblmoisture.Text = getdateM(dr4["Acceptance_Date"].ToString());
        }

        # endregion
    
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

        if (con_WPMS.State == ConnectionState.Open)
        {
            con_WPMS.Close();
        }
    }

    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MM/yyyy-hh:mm tt");
    }
    public string getdateM(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

  

    protected void btn_delreq_Click(object sender, EventArgs e)
   {


      
       string todaydate = DateTime.Now.ToString();
       string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
       string opid = Session["OperatorId"].ToString();
 for (int i = 0; i < grd_viewDepot.Rows.Count; i++)
     

      
        {
            string challan = grd_viewDepot.Rows[i].Cells[2].Text.ToString();


            string issue = grd_viewDepot.Rows[i].Cells[4].Text.ToString();
         
      if (cons.State == ConnectionState.Closed)
            {
                cons.Open();
            }


            string mystr = "SELECT *FROM [tbl_Storage_Arrival_Stock] where [DepotId]='" + txtissue.Text + "' and AcceptanceNo='" + ddlAccptNumber.SelectedValue.ToString() + "' and IssueID='" + issue + "' and Commodity_Id='" + ddlcrop.SelectedValue.ToString() + "' and Challan_No='" + challan + "'";

            SqlCommand cmdwhr = new SqlCommand(mystr, cons);
            SqlDataReader sqldr = cmdwhr.ExecuteReader();
            sqldr.Read();

            if (sqldr.HasRows)
            {
                Label1.Visible = true;
                Label1.Text = "whr created!Entry cannot deleted ";
                btn_print.Visible = false;

            }
            else
            {


                if (((CheckBox)grd_viewDepot.Rows[i].FindControl("chk_Select")).Checked == true)
                {
                    grd_viewDepot.Rows[i].Visible = true;

               
          
                

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    string inslog = "Insert into tbl_Delete_Request([Distt_ID],[IssueCenter_ID] ,[Purchase_Center] ,[Dispatch_Date] ,[TC_Number] ,[Truck_No] ,[Acceptance_No] ,[Acceptance_Date] ,[Created_Date],[IP_Address] ,[OperatorID] ,[Accept_Qty],[CommodityId] ,[IssueID] ,[Sending_District] ,[Reason] ,[Description],[Requested_Date],[Operator_name], [Operator_mono])values('" + distid + "','" + issueid + "','" + HiddenField1.Value + "','" + hd_dispach.Value + "','" + challan + "','" + hd_truck.Value + "','" + ddlAccptNumber.SelectedValue.ToString() + "','" + hd_accpdate.Value + "','" + hd_creat.Value + "','" + ip + "','" + opid + "','" + hd_quantity.Value + "','" + ddlcrop.Text + "','" + issue + "','" + hd_sendingdist.Value + "',N'" + ddl_del_reason.SelectedValue.ToString() + "',N'" + txt_other.Text + "','" + todaydate + "','" + txt_opertorname.Text + "','" + txt_operatermono.Text + "') ";

                    SqlCommand cmd = new SqlCommand(inslog, con);
                    int x = cmd.ExecuteNonQuery();

                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Request send Sucessfully'); </script> ");

                    btn_print.Visible = true;



                }

                else
                {
                    grd_viewDepot.Rows[i].Visible = false;

                    
                }
            }
            sqldr.Close();
            



            }
 if (cons.State == ConnectionState.Open)
 {
     cons.Close();
 }

        }
 

    protected void ddlcomdty_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (con_WPMS.State == ConnectionState.Closed)
        {
            con_WPMS.Open();
        }

        if (ddlcrop.SelectedValue == "0" || ddlcrop.SelectedItem.Text == "--Select--")
        {

        }

        else
        {
            Session["Commodity_Id"] = ddlcrop.SelectedValue;

            string qry = "Select distinct Acceptance_No from Acceptance_Note_Detail where Distt_ID = '23" + dist + "' and IssueCenter_ID = '" + issueid + "' order by Acceptance_No";

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
            else
            {

            }
        }

        if (con_WPMS.State == ConnectionState.Open)
        {
            con_WPMS.Close();
        }
    }
    protected void btn_print_Click(object sender, EventArgs e)
    {
        
        Session["reason"] = ddl_del_reason.SelectedValue.ToString(); 
        Session["descrip"]=txt_other.Text;

        Session["distname"] = txtdist.Text;
      Session["depotname"]=txtissue.Text ;
     

        Session["cropyear"] = lblcrop.Text;
        Session["pcid"] = HiddenField1.Value; 
        Session["commodity"]=ddlcrop.SelectedItem.ToString();

        Session["Acceptance"] = ddlAccptNumber.SelectedValue.ToString();
        Session["sendingdist"]= lblsenddist.Text;


        Session["purchaseCentre"] = lblpccenter.Text;
        Session["opratername"] = txt_opertorname.Text;

        Session["grid"] = grd_viewDepot;

        
            txtdist.Text =distname ;
            txtissue.Text = dname;
       Response.Redirect("PrintDeleteRequest.aspx");
    }
  
}