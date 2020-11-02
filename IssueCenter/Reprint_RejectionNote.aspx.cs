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

public partial class IssueCenter_Reprint_RejectionNote : System.Web.UI.Page
{
    public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_PPMS2013"].ToString());
    public SqlConnection con_Maze = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_MPMS2013"].ToString());
    public SqlConnection con_WPMS = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2014"].ToString());

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    MoveChallan mobj1 = null;
    protected Common ComObj = null, cmn = null;
    public string distname = "";
    public string dist = "";
    public string sid = "";
    public string ssid = "";
    public string dname = "";
    public string snid = "";
    public string distid = "";
    public string dipotid = "";
    public string cdate = "";
    public string issueid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        issueid = Session["issue_id"].ToString();
        dist = Session["dist_id"].ToString();

        lblSessionDepot.Text = Session["issue_id"].ToString();
        lblsessiondist.Text = Session["dist_id"].ToString();

        if (!IsPostBack)
        {
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
        }
    }

    protected void ddlAccptNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        if (con_WPMS.State == ConnectionState.Closed)
        {
            con_WPMS.Open();
        }

        GetDistt();
    }

    protected void bindRejNote()
    {
        string qry = "Select distinct Acceptance_No from Acceptance_Note_Detail where Distt_ID = '23" + lblsessiondist.Text + "' and IssueCenter_ID = '" + lblSessionDepot.Text + "' and IssueID in (select Receipt_Id from IssueCenterReceipt_Online where Recd_Godown = 'Rejected' and IssueCenter_ID = '" + lblSessionDepot.Text + "') order by Acceptance_No";

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


    void GetDistt()
    {

       // mobj1 = new MoveChallan(ComObj);
        string qrey = "select DistrictId,DepotName  from dbo.tbl_MetaData_DEPOT where DepotID='" + issueid + "'";
       
        SqlCommand cmddis = new SqlCommand(qrey, con);
        SqlDataAdapter dadist = new SqlDataAdapter(cmddis);
        DataSet ds = new DataSet();

        dadist.Fill(ds);

        DataRow dr = ds.Tables[0].Rows[0];
        distid = dr["DistrictId"].ToString();
        dname = dr["DepotName"].ToString();
        dist = distid.Substring(2, 2);

        string qrey2 = "select district_name from pds.districtsmp where district_code='" + dist + "'";

        SqlCommand cmddisn = new SqlCommand(qrey2, con);
        SqlDataAdapter dadistn = new SqlDataAdapter(cmddisn);
        DataSet ds2 = new DataSet();

        dadistn.Fill(ds2);

       
        DataRow dr2 = ds2.Tables[0].Rows[0];
        distname = dr2["district_name"].ToString();


        lbldepot.Text = dname;
        lbldistt.Text = distname;


        lblgno.Text = sid;
        string daten = DateTime.Now.ToString();
        string gdaten = getdate(daten);
        lblgdtae.Text = gdaten;

        if (Session["Commodity_Id"].ToString() == "1")
        {
            try
            {
                if (con_WPMS.State == ConnectionState.Closed)
                {
                    con_WPMS.Open();
                }

                string str = "SELECT IssueCenterReceipt_Online.*,CONVERT(varchar,IssueCenterReceipt_Online.DateOfIssue,106)as DateOfIssue1,Acceptance_Note_Detail.Acceptance_No,Acceptance_Note_Detail.Acceptance_Date,Crop_Master.crop as Commodity_Name,TransportMaster.Transporter_Name,Districts.District_Name,(Society.Society_Name+','+Society.SocPlace)as Society_Name FROM IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId left join TransportMaster on TransportMaster.Transporter_ID=IssueCenterReceipt_Online.TransporterId and TransportMaster.SocietyCode = IssueCenterReceipt_Online.SocietyID left join Acceptance_Note_Detail on Acceptance_Note_Detail.TC_Number = IssueCenterReceipt_Online.TruckChalanNo and Acceptance_Note_Detail.IssueID = IssueCenterReceipt_Online.IssueID left join Districts on Districts.District_Code = IssueCenterReceipt_Online.Sending_District left join Society on Society.Society_Id= IssueCenterReceipt_Online.SocietyID  where IssueCenterReceipt_Online.DistrictId='" + distid + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and Acceptance_Note_Detail.Acceptance_No='" + ddlAccptNumber.SelectedItem.Text + "'";
                SqlDataAdapter daP = new SqlDataAdapter(str, con_WPMS);
                DataSet dsP = new DataSet();
                daP.Fill(dsP);

                lbldepon.Text = dname;

                grd_viewDepot.DataSource = dsP.Tables[0];
                grd_viewDepot.DataBind();

                lblsenddist.Text = dsP.Tables[0].Rows[0]["District_Name"].ToString();
                lblpccenter.Text = dsP.Tables[0].Rows[0]["Society_Name"].ToString();
                cdate = dsP.Tables[0].Rows[0]["DateOfIssue"].ToString();

                string gdate = getdateM(cdate);

                lblcrop.Text = dsP.Tables[0].Rows[0]["CropYear"].ToString();
                lblcomdty.Text = dsP.Tables[0].Rows[0]["Commodity_Name"].ToString();


                lblmoisture.Text = getdateM(dsP.Tables[0].Rows[0]["Acceptance_Date"].ToString());
                Label2.Text = getdateM(dsP.Tables[0].Rows[0]["Acceptance_Date"].ToString());

                commodity.Text = dsP.Tables[0].Rows[0]["Commodity_Name"].ToString();

                lblwcmno.Text = dsP.Tables[0].Rows[0]["Acceptance_No"].ToString(); ;
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
        else if (Session["Commodity_Id"].ToString() == "2" || Session["Commodity_Id"].ToString() == "3")
        {
            try
            {
                if (con_paddy.State == ConnectionState.Closed)
                {
                    con_paddy.Open();
                }
                string str = " SELECT IssueCenterReceipt_Online.*,CONVERT(varchar,IssueCenterReceipt_Online.DateOfIssue,106)as DateOfIssue1,Acceptance_Note_Detail.Acceptance_No,Acceptance_Note_Detail.Acceptance_Date,Crop_Master.crop as Commodity_Name,TransportMaster.Transporter_Name,Districts.District_Name,(Society.Society_Name+','+Society.SocPlace)as Society_Name FROM IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId left join TransportMaster on TransportMaster.Transporter_ID=IssueCenterReceipt_Online.TransporterId and TransportMaster.SocietyCode = IssueCenterReceipt_Online.SocietyID left join Acceptance_Note_Detail on Acceptance_Note_Detail.TC_Number = IssueCenterReceipt_Online.TruckChalanNo left join Districts on Districts.District_Code = IssueCenterReceipt_Online.Sending_District left join Society on Society.Society_Id= IssueCenterReceipt_Online.SocietyID where IssueCenterReceipt_Online.DistrictId='" + distid + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and Acceptance_Note_Detail.Acceptance_No='" + ddlAccptNumber.SelectedItem.Text + "'";
                SqlDataAdapter daP = new SqlDataAdapter(str, con_paddy);
                DataSet dsP = new DataSet();
                daP.Fill(dsP);

                lbldepon.Text = dname;

                grd_viewDepot.DataSource = dsP.Tables[0];
                grd_viewDepot.DataBind();

                lblsenddist.Text = dsP.Tables[0].Rows[0]["District_Name"].ToString();
                lblpccenter.Text = dsP.Tables[0].Rows[0]["Society_Name"].ToString();
                cdate = dsP.Tables[0].Rows[0]["DateOfIssue"].ToString();
                string gdate = getdateM(cdate);

                lblcrop.Text = dsP.Tables[0].Rows[0]["CropYear"].ToString();
                lblcomdty.Text = dsP.Tables[0].Rows[0]["Commodity_Name"].ToString();

                lblmoisture.Text = getdateM(dsP.Tables[0].Rows[0]["Acceptance_Date"].ToString());
                lblwcmno.Text = dsP.Tables[0].Rows[0]["Acceptance_No"].ToString(); ;
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
        else if (Session["Commodity_Id"].ToString() == "4" || Session["Commodity_Id"].ToString() == "5" || Session["Commodity_Id"].ToString() == "6" || Session["Commodity_Id"].ToString() == "7" || Session["Commodity_Id"].ToString() == "8")
        {
            try
            {
                if (con_Maze.State == ConnectionState.Closed)
                {
                    con_Maze.Open();
                }
                string str = " SELECT IssueCenterReceipt_Online.*,CONVERT(varchar,IssueCenterReceipt_Online.DateOfIssue,106)as DateOfIssue1,Acceptance_Note_Detail.Acceptance_No,Acceptance_Note_Detail.Acceptance_Date,Crop_Master.crop as Commodity_Name,TransportMaster.Transporter_Name,Districts.District_Name,(Society.Society_Name+','+Society.SocPlace)as Society_Name FROM IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId left join TransportMaster on TransportMaster.Transporter_ID=IssueCenterReceipt_Online.TransporterId and TransportMaster.SocietyCode = IssueCenterReceipt_Online.SocietyID left join Acceptance_Note_Detail on Acceptance_Note_Detail.TC_Number = IssueCenterReceipt_Online.TruckChalanNo left join Districts on Districts.District_Code = IssueCenterReceipt_Online.Sending_District left join Society on Society.Society_Id= IssueCenterReceipt_Online.SocietyID where IssueCenterReceipt_Online.DistrictId='" + distid + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and Acceptance_Note_Detail.Acceptance_No='" + ddlAccptNumber.SelectedItem.Text + "'";
                SqlDataAdapter daP = new SqlDataAdapter(str, con_Maze);
                DataSet dsP = new DataSet();
                daP.Fill(dsP);

                lbldepon.Text = dname;

                grd_viewDepot.DataSource = dsP.Tables[0];
                grd_viewDepot.DataBind();

                lblsenddist.Text = dsP.Tables[0].Rows[0]["District_Name"].ToString();
                lblpccenter.Text = dsP.Tables[0].Rows[0]["Society_Name"].ToString();
                cdate = dsP.Tables[0].Rows[0]["DateOfIssue"].ToString();
                string gdate = getdateM(cdate);

                lblcrop.Text = dsP.Tables[0].Rows[0]["CropYear"].ToString();
                lblcomdty.Text = dsP.Tables[0].Rows[0]["Commodity_Name"].ToString();


                lblmoisture.Text = getdateM(dsP.Tables[0].Rows[0]["Acceptance_Date"].ToString());
                lblwcmno.Text = dsP.Tables[0].Rows[0]["Acceptance_No"].ToString();
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
        else
        {

            mobj1 = new MoveChallan(ComObj);
            //string qrey4 = "select * from dbo.SCSC_Procurement where IssueCenter_ID='" + issueid + "' and GatePass_id='" + sid + "'";
            string qrey4 = "SELECT SCSC_Procurement.*,(SCSC_Procurement.TC_Number)as TruckChalanNo,(SCSC_Procurement.Truck_Number)as TruckNo ,CONVERT(varchar,SCSC_Procurement.Dispatch_Date,106)as DateOfIssue1,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name,(Acceptance_Note_Detail.Acceptance_No)as Acceptance_No1,Acceptance_Note_Detail.Bags as Acc_Bag,Acceptance_Note_Detail.Acceptance_Date,Acceptance_Note_Detail.Accept_Qty,Acceptance_Note_Detail.Reject_Qty,tbl_MetaData_Purchase_Center.PurchaseCenterName,Transporter_Table.Transporter_Name as Transporter_Name,tbl_MetaData_DEPOT.DepotName as DepotName,districtsmp.district_name as district_name   FROM dbo.SCSC_Procurement left join dbo.tbl_MetaData_STORAGE_COMMODITY on SCSC_Procurement.Commodity_Id=tbl_MetaData_STORAGE_COMMODITY .Commodity_Id left join dbo.Transporter_Table on SCSC_Procurement.Transporter_ID =Transporter_Table.Transporter_ID left join pds.districtsmp on SCSC_Procurement.Sending_District=districtsmp.district_code left join dbo.tbl_MetaData_DEPOT on SCSC_Procurement.Purchase_Center=tbl_MetaData_DEPOT.DepotID left join Acceptance_Note_Detail on Acceptance_Note_Detail.TC_Number = SCSC_Procurement.TC_Number left join tbl_MetaData_Purchase_Center on tbl_MetaData_Purchase_Center.PcId = SCSC_Procurement.Purchase_Center where SCSC_Procurement.Distt_ID='" + dist + "' and  SCSC_Procurement.IssueCenter_ID='" + issueid + "' and Acceptance_Note_Detail.Acceptance_No='" + ddlAccptNumber.SelectedItem.Text + "'";

            DataSet ds4 = mobj1.selectAny(qrey4);
            DataRow dr4 = ds4.Tables[0].Rows[0];

            lbldepon.Text = dname;
            //lblchallanno.Text = dr4["TC_Number"].ToString();
            lblsenddist.Text = dr4["district_name"].ToString();
            lblpccenter.Text = dr4["PurchaseCenterName"].ToString();
            //lblchallandt.Text = dr4["challan_date"].ToString();
            cdate = dr4["Dispatch_Date"].ToString();
            string gdate = getdate(cdate);
            //lblchallandt.Text = gdate;
            lblcomdty.Text = dr4["Commodity_Name"].ToString();

            //lblcrop.Text = ds4.Tables[0].Rows[0]["CropYear"].ToString();

            lblwcmno.Text = dr4["Acceptance_No1"].ToString();
            lblmoisture.Text = getdateM(dr4["Acceptance_Date"].ToString());
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
    protected void ddlcrop_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcrop.SelectedValue == "0" || ddlcrop.SelectedItem.Text == "--Select--")
        {

        }
        else
        {
            Session["Commodity_Id"] = ddlcrop.SelectedValue;

            bindRejNote();

           
        }
        
    }
}
