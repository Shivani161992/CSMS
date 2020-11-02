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

public partial class District_Reprint_Acceptance : System.Web.UI.Page
{
    public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_PPMS2014"].ToString());
    public SqlConnection con_Maze = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_MPMS2014"].ToString());
    public SqlConnection con_WPMS = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2015"].ToString());

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    MoveChallan mobj1 = null;
    protected Common ComObj = null, cmn = null;
    public static string distname = "";
    public static string dist = "";
    public string sid = "";
    public string ssid = "";
    public static string dname = "";
    public string snid = "";
    public static string distid = "";
   
    public string cdate = "";
    public static string issueid = "";

    //int totalSB;

    //double totalSQ;

    //int totalRB;

    //double totalRQ;

    //int totalRjB;

    //double totalRjQ;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        if (con_WPMS.State == ConnectionState.Closed)
        {
            con_WPMS.Open();
        }

        if (Session["dist_id"] != null)
        {

        if (!IsPostBack)
          {
                   
             dist = Session["dist_id"].ToString();

             lblSessionDist.Text = Session["dist_id"].ToString();         
                    
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
            
            string qrey2 = "select district_name from pds.districtsmp where district_code='" + lblSessionDist.Text + "'";

            SqlCommand cmddist = new SqlCommand(qrey2, con);

            SqlDataAdapter dadist = new SqlDataAdapter(cmddist);

            DataSet dsdist = new DataSet();

            dadist.Fill(dsdist);
            
            DataRow dr2 = dsdist.Tables[0].Rows[0];
            distname = dr2["district_name"].ToString();

            lbldistt.Text = distname;

            }
        }

        else
             {

               Response.Redirect("~/MainLogin.aspx");

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

            string qry = "Select distinct Acceptance_No from Acceptance_Note_Detail where Distt_ID = '23" + lblSessionDist.Text + "' and Acceptance_Note_Detail.IssueCenter_ID = 'Rack' order by Acceptance_No";

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

    void GetDistt()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        if (con_WPMS.State == ConnectionState.Closed)
        {
            con_WPMS.Open();
        }

       
        string qrey2 = "select district_name from pds.districtsmp where district_code='" + lblSessionDist.Text + "'";
        SqlCommand cmd2 = new SqlCommand(qrey2, con);

        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);

        DataSet ds2 = new DataSet();

        da2.Fill(ds2);

        DataRow dr2 = ds2.Tables[0].Rows[0];
        distname = dr2["district_name"].ToString();

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
                
                string str = "SELECT IssueCenterReceipt_Online.*,CONVERT(varchar,IssueCenterReceipt_Online.DateOfIssue,106)as DateOfIssue1,Acceptance_Note_Detail.Bags as Acc_Bag,Acceptance_Note_Detail.Accept_Qty,Acceptance_Note_Detail.Reject_Qty,Acceptance_Note_Detail.Acceptance_No,Acceptance_Note_Detail.Acceptance_Date,Crop_Master.crop as Commodity_Name,TransportMaster.Transporter_Name,Districts.District_Name,(Society.Society_Name+','+Society.SocPlace)as Society_Name , isnull(Acceptance_Note_Detail.Stiching_bags_Good,0)Stiching_bags_Good ,isnull(Acceptance_Note_Detail.Stiching_bags_Bad,0)Stiching_bags_Bad , isnull(Acceptance_Note_Detail.Stencile_bags_Good,0)Stencile_bags_Good ,ISNULL(Acceptance_Note_Detail.Stencile_bags_Bad,0)Stencile_bags_Bad  FROM IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId left join TransportMaster on TransportMaster.Transporter_ID=IssueCenterReceipt_Online.TransporterId and TransportMaster.SocietyCode = IssueCenterReceipt_Online.SocietyID left join Acceptance_Note_Detail on Acceptance_Note_Detail.TC_Number = IssueCenterReceipt_Online.TruckChalanNo and Acceptance_Note_Detail.IssueID = IssueCenterReceipt_Online.IssueID left join Districts on Districts.District_Code = IssueCenterReceipt_Online.Sending_District left join Society on Society.Society_Id= IssueCenterReceipt_Online.SocietyID  where IssueCenterReceipt_Online.DistrictId='23" + lblSessionDist.Text + "' and IssueCenterReceipt_Online.IssueCenter_ID='ByRack' and Acceptance_Note_Detail.Acceptance_No='" + sid + "'";

                SqlDataAdapter daP = new SqlDataAdapter(str, con_WPMS);
                DataSet dsP = new DataSet();
                daP.Fill(dsP);
                grd_viewDepot.DataSource = dsP.Tables[0];
                grd_viewDepot.DataBind();
                
                lblsenddist.Text = dsP.Tables[0].Rows[0]["District_Name"].ToString();
                lblpccenter.Text = dsP.Tables[0].Rows[0]["Society_Name"].ToString();
                cdate = dsP.Tables[0].Rows[0]["DateOfIssue"].ToString();
                string gdate = getdateM(cdate);

                lblcrop.Text = dsP.Tables[0].Rows[0]["CropYear"].ToString();
                lblcomdty.Text = dsP.Tables[0].Rows[0]["Commodity_Name"].ToString();
                
                LblStechingGood.Text = dsP.Tables[0].Rows[0]["Stiching_bags_Good"].ToString();

                LblStechingBad.Text = dsP.Tables[0].Rows[0]["Stiching_bags_Bad"].ToString();

                LblStencileGood.Text = dsP.Tables[0].Rows[0]["Stencile_bags_Good"].ToString();

                LblStencileBad.Text = dsP.Tables[0].Rows[0]["Stencile_bags_Bad"].ToString();

                lblmoisture.Text = getdateM(dsP.Tables[0].Rows[0]["Acceptance_Date"].ToString());

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

                string str = " SELECT IssueCenterReceipt_Online.*,CONVERT(varchar,IssueCenterReceipt_Online.DateOfIssue,106)as DateOfIssue1,Acceptance_Note_Detail.Bags as Acc_Bag,Acceptance_Note_Detail.Accept_Qty,Acceptance_Note_Detail.Reject_Qty,Acceptance_Note_Detail.Acceptance_No,Acceptance_Note_Detail.Acceptance_Date,Crop_Master.crop as Commodity_Name,TransportMaster.Transporter_Name,Districts.District_Name,(Society.Society_Name+','+Society.SocPlace)as Society_Name FROM IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId left join TransportMaster on TransportMaster.Transporter_ID=IssueCenterReceipt_Online.TransporterId and TransportMaster.SocietyCode = IssueCenterReceipt_Online.SocietyID left join Acceptance_Note_Detail on Acceptance_Note_Detail.TC_Number = IssueCenterReceipt_Online.TruckChalanNo left join Districts on Districts.District_Code = IssueCenterReceipt_Online.Sending_District left join Society on Society.Society_Id= IssueCenterReceipt_Online.SocietyID where IssueCenterReceipt_Online.DistrictId='23" + lblSessionDist.Text + "' and IssueCenterReceipt_Online.IssueCenter_ID='ByRack' and Acceptance_Note_Detail.Acceptance_No='" + sid + "'";
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
                string str = " SELECT IssueCenterReceipt_Online.*,CONVERT(varchar,IssueCenterReceipt_Online.DateOfIssue,106)as DateOfIssue1,Acceptance_Note_Detail.Bags as Acc_Bag,Acceptance_Note_Detail.Accept_Qty,Acceptance_Note_Detail.Reject_Qty,Acceptance_Note_Detail.Acceptance_No,Acceptance_Note_Detail.Acceptance_Date,Crop_Master.crop as Commodity_Name,TransportMaster.Transporter_Name,Districts.District_Name,(Society.Society_Name+','+Society.SocPlace)as Society_Name FROM IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId left join TransportMaster on TransportMaster.Transporter_ID=IssueCenterReceipt_Online.TransporterId and TransportMaster.SocietyCode = IssueCenterReceipt_Online.SocietyID left join Acceptance_Note_Detail on Acceptance_Note_Detail.TC_Number = IssueCenterReceipt_Online.TruckChalanNo left join Districts on Districts.District_Code = IssueCenterReceipt_Online.Sending_District left join Society on Society.Society_Id= IssueCenterReceipt_Online.SocietyID where IssueCenterReceipt_Online.DistrictId='23" + lblSessionDist.Text + "' and IssueCenterReceipt_Online.IssueCenter_ID='ByRack' and Acceptance_Note_Detail.Acceptance_No='" + sid + "'";
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
            string qrey4 = "SELECT SCSC_Procurement.*,(SCSC_Procurement.No_of_Bags)as Bags,(SCSC_Procurement.Quantity)as Quantity,(SCSC_Procurement.TC_Number)as TruckChalanNo,(SCSC_Procurement.Truck_Number)as TruckNo ,CONVERT(varchar,SCSC_Procurement.Dispatch_Date,106)as DateOfIssue1,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name,(Acceptance_Note_Detail.Acceptance_No)as Acceptance_No1,Acceptance_Note_Detail.Bags as Acc_Bag,Acceptance_Note_Detail.Acceptance_Date,Acceptance_Note_Detail.Accept_Qty,Acceptance_Note_Detail.Reject_Qty,tbl_MetaData_Purchase_Center.PurchaseCenterName,Transporter_Table.Transporter_Name as Transporter_Name,tbl_MetaData_DEPOT.DepotName as DepotName,districtsmp.district_name as district_name   FROM dbo.SCSC_Procurement left join dbo.tbl_MetaData_STORAGE_COMMODITY on SCSC_Procurement.Commodity_Id=tbl_MetaData_STORAGE_COMMODITY .Commodity_Id left join dbo.Transporter_Table on SCSC_Procurement.Transporter_ID =Transporter_Table.Transporter_ID left join pds.districtsmp on SCSC_Procurement.Sending_District=districtsmp.district_code left join dbo.tbl_MetaData_DEPOT on SCSC_Procurement.Purchase_Center=tbl_MetaData_DEPOT.DepotID left join Acceptance_Note_Detail on Acceptance_Note_Detail.TC_Number = SCSC_Procurement.TC_Number left join tbl_MetaData_Purchase_Center on tbl_MetaData_Purchase_Center.PcId = SCSC_Procurement.Purchase_Center where SCSC_Procurement.Distt_ID='" + dist + "' and  SCSC_Procurement.IssueCenter_ID='ByRack' and Acceptance_Note_Detail.Acceptance_No='" + sid + "'";

            DataSet ds4 = mobj1.selectAny(qrey4);
            DataRow dr4 = ds4.Tables[0].Rows[0];
            grd_viewDepot.DataSource = ds4.Tables[0];
            grd_viewDepot.DataBind();
            //lbldepon.Text = dname;
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

        if (con_WPMS.State == ConnectionState.Open)
        {
            con_WPMS.Close();
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
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
    
    float Acce = 0;

    float Qtytrans = 0;

    protected void grd_viewDepot_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Acce += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Accept_Qty"));

            Qtytrans += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "QtyTransffer"));

            float diff = Qtytrans - Acce;

            if (diff < 0)
            {
                diff = 0;
            }

            float NetAmt = diff;

            Label TxtQtyAmt = (Label)e.Row.FindControl("TxtNetQty");

            TxtQtyAmt.Text = NetAmt.ToString();
        }

    }
    
    protected void ddlAccptNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAccptNumber.SelectedItem.Text == "--Select--")
        {

        }

        else
        {
            sid = ddlAccptNumber.SelectedItem.Text;

            GetDistt();
        }
    }
    protected void lnkback_Click(object sender, EventArgs e)
    {
        if (con_WPMS.State == ConnectionState.Open)
        {
            con_WPMS.Close();
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }


        Response.Redirect("~/District/Dist_Welcome.aspx");
    }
}
