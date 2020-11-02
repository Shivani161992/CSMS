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
public partial class IssueCenter_Print_Gatepass_Procurement : System.Web.UI.Page
{
    public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_PPMS2014"].ToString());
    public SqlConnection con_Maze = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_MPMS2014"].ToString());
    public SqlConnection con_WPMS = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2015"].ToString());

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

          //sid = Request.QueryString["C"].ToString();
        if (Session["Receipt_ID"] != null)
        {
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
            sid = Session["Receipt_ID"].ToString();
            issueid = Session["issue_id"].ToString();
            dist = Session["dist_id"].ToString();
            ssid = sid.Substring(0, 7);
            GetDistt();
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

    }
    void GetDistt()
    {
       
        mobj1 = new MoveChallan(ComObj);
        string qrey = "select DistrictId,DepotName  from dbo.tbl_MetaData_DEPOT where DepotID='" + issueid + "'";
        DataSet ds = mobj1.selectAny(qrey);
        DataRow dr = ds.Tables[0].Rows[0];
        distid = dr["DistrictId"].ToString();
        dname = dr["DepotName"].ToString();
        dist = distid.Substring(2, 2);

        string qrey2 = "select district_name from pds.districtsmp where district_code='" + dist + "'";
        DataSet ds2 = mobj1.selectAny(qrey2);
        DataRow dr2 = ds2.Tables[0].Rows[0];
        distname = dr2["district_name"].ToString();


        lbldepot.Text = dname;
        lbldistt.Text = distname;
              

        lblgno.Text = sid;
        string daten = DateTime.Now.ToString();
        string gdaten = getdate(daten);
        lblgdtae.Text = gdaten;

        if (Session["Commodity_Id"] != null)
        {
            # region Procrement_Database

            # region Wheat

            if (Session["Commodity_Id"].ToString() == "1")
            {
                try
                {
                    if (con_WPMS.State == ConnectionState.Closed)
                    {
                        con_WPMS.Open();
                    }
                   // string str = " SELECT IssueCenterReceipt_Online.*,Crop_Master.crop as Commodity_Name,TransportMaster.Transporter_Name,Districts.District_Name,(Society.Society_Name+','+Society.SocPlace)as Society_Name FROM IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId left join TransportMaster on TransportMaster.Transporter_ID=IssueCenterReceipt_Online.TransporterId and TransportMaster.SocietyCode = IssueCenterReceipt_Online.SocietyID left join Districts on Districts.District_Code = IssueCenterReceipt_Online.Sending_District left join Society on Society.Society_Id= IssueCenterReceipt_Online.SocietyID where IssueCenterReceipt_Online.DistrictId='" + distid + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and IssueCenterReceipt_Online.Receipt_Id='" + sid + "'";

                    string str = " SELECT IssueCenterReceipt_Online.IssueID,IssueCenterReceipt_Online.CropYear , IssueCenterReceipt_Online.Bags, IssueCenterReceipt_Online.QtyTransffer , IssueCenterReceipt_Online.TaulPtrakNo , IssueCenterReceipt_Online.TruckChalanNo,isnull(IssueCenterReceipt_Online.TruckNo,'')TruckNo , SUM( IssueCenterReceipt_Online.Recv_Qty )Recv_Qty  , sum(IssueCenterReceipt_Online.Recd_Bags )Recd_Bags, IssueCenterReceipt_Online.Recd_Date , IssueCenterReceipt_Online.DateOfIssue , IssueCenterReceipt_Online.TruckChalanNo ,Crop_Master.crop as Commodity_Name,TransportMaster.Transporter_Name,Districts.District_Name,(Society.Society_Name+','+Society.SocPlace)as Society_Name FROM IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId left join TransportMaster on TransportMaster.Transporter_ID=IssueCenterReceipt_Online.TransporterId and TransportMaster.SocietyCode = IssueCenterReceipt_Online.SocietyID left join Districts on Districts.District_Code = IssueCenterReceipt_Online.Sending_District left join Society on Society.Society_Id= IssueCenterReceipt_Online.SocietyID where IssueCenterReceipt_Online.DistrictId='" + distid + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and IssueCenterReceipt_Online.Receipt_Id='" + sid + "'  group by IssueCenterReceipt_Online.IssueID,IssueCenterReceipt_Online.CropYear , IssueCenterReceipt_Online.Bags, IssueCenterReceipt_Online.QtyTransffer , IssueCenterReceipt_Online.TaulPtrakNo , IssueCenterReceipt_Online.TruckChalanNo,IssueCenterReceipt_Online.TruckNo , IssueCenterReceipt_Online.DateOfIssue , IssueCenterReceipt_Online.TruckChalanNo , Crop_Master.crop , IssueCenterReceipt_Online.Recd_Date , TransportMaster.Transporter_Name ,Districts.District_Name , (Society.Society_Name+','+Society.SocPlace)";
                    
                    SqlDataAdapter daP = new SqlDataAdapter(str, con_WPMS);
                    DataSet dsP = new DataSet();
                    daP.Fill(dsP);
                    lbldepon.Text = dname;
                    lblchallanno.Text = dsP.Tables[0].Rows[0]["TruckChalanNo"].ToString();
                    lblsenddist.Text = dsP.Tables[0].Rows[0]["District_Name"].ToString();
                    lblpccenter.Text = dsP.Tables[0].Rows[0]["Society_Name"].ToString();
                    cdate = dsP.Tables[0].Rows[0]["DateOfIssue"].ToString();
                    string gdate = getdateM(cdate);
                    lblchallandt.Text = gdate;
                    lblcrop.Text = dsP.Tables[0].Rows[0]["CropYear"].ToString();
                    lblcomdty.Text = dsP.Tables[0].Rows[0]["Commodity_Name"].ToString();
                    lbltransp.Text = dsP.Tables[0].Rows[0]["Transporter_Name"].ToString();
                    lblvicln.Text = dsP.Tables[0].Rows[0]["TruckNo"].ToString();

                    lblsend_bagsNum.Text = dsP.Tables[0].Rows[0]["Bags"].ToString();
                    lblSend_Qtydisplay.Text = dsP.Tables[0].Rows[0]["QtyTransffer"].ToString();


                    lblbagno.Text = dsP.Tables[0].Rows[0]["Recd_Bags"].ToString();
                    lblweight.Text = dsP.Tables[0].Rows[0]["Recv_Qty"].ToString();
                    lblmoisture.Text = getdateM(dsP.Tables[0].Rows[0]["Recd_Date"].ToString());
                    lblwcmno.Text = "";
                }
                catch (Exception ex)
                {
                    lbleror.Visible = true;
                    lbleror.Text = ex.Message;

                    if (con_WPMS.State == ConnectionState.Open)
                    {
                        con_WPMS.Close();
                    }

                }
                finally
                {
                    if (con_WPMS.State == ConnectionState.Open)
                    {
                        con_WPMS.Close();
                    }
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

                    string str = " SELECT IssueCenterReceipt_Online.*,Crop_Master.crop as Commodity_Name,TransportMaster.Transporter_Name,Districts.District_Name,(Society.Society_Name+','+Society.SocPlace)as Society_Name FROM IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId left join TransportMaster on TransportMaster.Transporter_ID=IssueCenterReceipt_Online.TransporterId and TransportMaster.SocietyCode = IssueCenterReceipt_Online.SocietyID left join Districts on Districts.District_Code = IssueCenterReceipt_Online.Sending_District left join Society on Society.Society_Id= IssueCenterReceipt_Online.SocietyID where IssueCenterReceipt_Online.DistrictId='" + distid + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and IssueCenterReceipt_Online.Receipt_Id='" + sid + "'";
                    

                   // string str = " SELECT IssueCenterReceipt_Online.*,ISNULL(IssueCenterReceipt_Online.Stiching_bags,0)as Stiching_bags1,ISNULL(IssueCenterReceipt_Online.stencile_bags,0)as stencile_bags1,Crop_Master.crop as Commodity_Name,TransportMaster.Transporter_Name,Districts.District_Name,(Society.Society_Name+','+Society.SocPlace)as Society_Name FROM IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId left join TransportMaster on TransportMaster.Transporter_ID=IssueCenterReceipt_Online.TransporterId left join Districts on Districts.District_Code = IssueCenterReceipt_Online.Sending_District left join Society on Society.Society_Id= IssueCenterReceipt_Online.SocietyID where IssueCenterReceipt_Online.DistrictId='" + distid + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and IssueCenterReceipt_Online.Receipt_Id='" + sid + "'";
                    SqlDataAdapter daP = new SqlDataAdapter(str, con_paddy);
                    DataSet dsP = new DataSet();
                    daP.Fill(dsP);
                    lbldepon.Text = dname;
                    lblchallanno.Text = dsP.Tables[0].Rows[0]["TruckChalanNo"].ToString();
                    lblsenddist.Text = dsP.Tables[0].Rows[0]["District_Name"].ToString();
                    lblpccenter.Text = dsP.Tables[0].Rows[0]["Society_Name"].ToString();
                    cdate = dsP.Tables[0].Rows[0]["DateOfIssue"].ToString();
                    string gdate = getdateM(cdate);
                    lblchallandt.Text = gdate;
                    lblcrop.Text = dsP.Tables[0].Rows[0]["CropYear"].ToString();
                    lblcomdty.Text = dsP.Tables[0].Rows[0]["Commodity_Name"].ToString();
                    lbltransp.Text = dsP.Tables[0].Rows[0]["Transporter_Name"].ToString();
                    lblvicln.Text = dsP.Tables[0].Rows[0]["TruckNo"].ToString();
                    lblbagno.Text = dsP.Tables[0].Rows[0]["Recd_Bags"].ToString();
                    lblweight.Text = dsP.Tables[0].Rows[0]["Recv_Qty"].ToString();
                    lblmoisture.Text = getdateM(dsP.Tables[0].Rows[0]["Recd_Date"].ToString());
                    lblwcmno.Text = "";
                }
                catch (Exception ex)
                {
                    lbleror.Visible = true;
                    lbleror.Text = ex.Message;

                    if (con_paddy.State == ConnectionState.Open)
                    {
                        con_paddy.Close();
                    }

                }
                finally
                {
                    if (con_paddy.State == ConnectionState.Open)
                    {
                        con_paddy.Close();
                    }
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

                    string str = " SELECT IssueCenterReceipt_Online.*,Crop_Master.crop as Commodity_Name,TransportMaster.Transporter_Name,Districts.District_Name,(Society.Society_Name+','+Society.SocPlace)as Society_Name FROM IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId left join TransportMaster on TransportMaster.Transporter_ID=IssueCenterReceipt_Online.TransporterId and TransportMaster.SocietyCode = IssueCenterReceipt_Online.SocietyID left join Districts on Districts.District_Code = IssueCenterReceipt_Online.Sending_District left join Society on Society.Society_Id= IssueCenterReceipt_Online.SocietyID where IssueCenterReceipt_Online.DistrictId='" + distid + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and IssueCenterReceipt_Online.Receipt_Id='" + sid + "'";
                    

                    //string str = " SELECT IssueCenterReceipt_Online.*,Crop_Master.crop as Commodity_Name,TransportMaster.Transporter_Name,Districts.District_Name,(Society.Society_Name+','+Society.SocPlace)as Society_Name FROM IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId left join TransportMaster on TransportMaster.Transporter_ID=IssueCenterReceipt_Online.TransporterId left join Districts on Districts.District_Code = IssueCenterReceipt_Online.Sending_District left join Society on Society.Society_Id= IssueCenterReceipt_Online.SocietyID where IssueCenterReceipt_Online.DistrictId='" + distid + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and IssueCenterReceipt_Online.Receipt_Id='" + sid + "'";
                    
                    SqlDataAdapter daP = new SqlDataAdapter(str, con_Maze);
                    DataSet dsP = new DataSet();
                    daP.Fill(dsP);
                    lbldepon.Text = dname;
                    lblchallanno.Text = dsP.Tables[0].Rows[0]["TruckChalanNo"].ToString();
                    lblsenddist.Text = dsP.Tables[0].Rows[0]["District_Name"].ToString();
                    lblpccenter.Text = dsP.Tables[0].Rows[0]["Society_Name"].ToString();
                    cdate = dsP.Tables[0].Rows[0]["DateOfIssue"].ToString();
                    string gdate = getdateM(cdate);
                    lblchallandt.Text = gdate;
                    lblcrop.Text = dsP.Tables[0].Rows[0]["CropYear"].ToString();
                    lblcomdty.Text = dsP.Tables[0].Rows[0]["Commodity_Name"].ToString();
                    lbltransp.Text = dsP.Tables[0].Rows[0]["Transporter_Name"].ToString();
                    lblvicln.Text = dsP.Tables[0].Rows[0]["TruckNo"].ToString();
                    lblbagno.Text = dsP.Tables[0].Rows[0]["Recd_Bags"].ToString();
                    lblweight.Text = dsP.Tables[0].Rows[0]["Recv_Qty"].ToString();
                    lblmoisture.Text = getdateM(dsP.Tables[0].Rows[0]["Recd_Date"].ToString());
                    lblwcmno.Text = "";
                }
                catch (Exception ex)
                {
                    lbleror.Visible = true;
                    lbleror.Text = ex.Message;

                    if (con_Maze.State == ConnectionState.Open)
                    {
                        con_Maze.Close();
                    }

                }
                finally
                {
                    if (con_Maze.State == ConnectionState.Open)
                    {
                        con_Maze.Close();
                    }
                }
            }

            # endregion

            # endregion
        }
        else
        {

            mobj1 = new MoveChallan(ComObj);
            //string qrey4 = "select * from dbo.SCSC_Procurement where IssueCenter_ID='" + issueid + "' and GatePass_id='" + sid + "'";
            string qrey4 = "SELECT SCSC_Procurement.*,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name,tbl_MetaData_Purchase_Center.PurchaseCenterName,Transporter_Table.Transporter_Name as Transporter_Name,tbl_MetaData_DEPOT.DepotName as DepotName,districtsmp.district_name as district_name   FROM dbo.SCSC_Procurement left join dbo.tbl_MetaData_STORAGE_COMMODITY on SCSC_Procurement.Commodity_Id=tbl_MetaData_STORAGE_COMMODITY .Commodity_Id left join dbo.Transporter_Table on SCSC_Procurement.Transporter_ID =Transporter_Table.Transporter_ID and Transporter_Table.PcId= SCSC_Procurement.Purchase_Center left join pds.districtsmp on SCSC_Procurement.Sending_District=districtsmp.district_code left join dbo.tbl_MetaData_DEPOT on SCSC_Procurement.Purchase_Center=tbl_MetaData_DEPOT.DepotID  left join tbl_MetaData_Purchase_Center on tbl_MetaData_Purchase_Center.PcId = SCSC_Procurement.Purchase_Center where SCSC_Procurement.Distt_ID='" + dist + "' and  SCSC_Procurement.IssueCenter_ID='" + issueid + "' and SCSC_Procurement.Receipt_Id='" + sid + "'";

            DataSet ds4 = mobj1.selectAny(qrey4);
            DataRow dr4 = ds4.Tables[0].Rows[0];
            lbldepon.Text = dname;
            lblchallanno.Text = dr4["TC_Number"].ToString();
            lblsenddist.Text = dr4["district_name"].ToString();
            lblpccenter.Text = dr4["PurchaseCenterName"].ToString();
            //lblchallandt.Text = dr4["challan_date"].ToString();
            cdate = dr4["Dispatch_Date"].ToString();
            string gdate = getdateM(cdate);
            lblchallandt.Text = gdate;
            lblcomdty.Text = dr4["Commodity_Name"].ToString();

            lbltransp.Text = dr4["Transporter_Name"].ToString();
            lblvicln.Text = dr4["Truck_Number"].ToString();

            lblbagno.Text = dr4["No_of_Bags"].ToString();
            lblweight.Text = dr4["Quantity"].ToString();
            lblwcmno.Text = dr4["Acceptance_No"].ToString();
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

}
