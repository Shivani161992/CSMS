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
using System.Data.SqlClient;

public partial class District_Print_Receipt_Procurement : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_PPMS2013"].ToString());
    public SqlConnection con_Maze = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_MPMS2013"].ToString());
    public SqlConnection con_WPMS = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2014"].ToString());


    public string distname = "";
    public string dist = "";
    public string sid = "";
    public string ssid = "";
    public string dname = "";
    public string snid = "";
    public string distid = "";
    public string dipotid = "";
    public string cdate = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Receipt_ID"] != null)
        {
            
            sid = Session["Receipt_ID"].ToString();
         
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
        string qrey4 = "SELECT SCSC_Procurement.*,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as Commodity_Name,Society.Society_Name,Transporter_Table.Transporter_Name as Transporter_Name,districtsmp.district_name as district_name   FROM dbo.SCSC_Procurement left join dbo.tbl_MetaData_STORAGE_COMMODITY on SCSC_Procurement.Commodity_Id=tbl_MetaData_STORAGE_COMMODITY .Commodity_Id left join dbo.Transporter_Table on SCSC_Procurement.Transporter_ID =Transporter_Table.Transporter_ID and Transporter_Table.PcId= SCSC_Procurement.Purchase_Center left join pds.districtsmp on SCSC_Procurement.Sending_District=districtsmp.district_code left join Society on Society.Society_Id = SCSC_Procurement.Purchase_Center where SCSC_Procurement.Distt_ID='" + dist + "' and SCSC_Procurement.Receipt_Id='" + sid + "'";

        SqlCommand cmd4 = new SqlCommand(qrey4, con);

        SqlDataAdapter da = new SqlDataAdapter(cmd4);

        DataSet ds4 = new DataSet();

        da.Fill(ds4);
        DataRow dr4 = ds4.Tables[0].Rows[0];

        lblchallanno.Text = dr4["TC_Number"].ToString();
        lblsenddist.Text = dr4["district_name"].ToString();
        lblpccenter.Text = dr4["Society_Name"].ToString();
        //lblchallandt.Text = dr4["challan_date"].ToString();
        cdate = dr4["Dispatch_Date"].ToString();
        string gdate = getdateM(cdate);
        lblchallandt.Text = gdate;
        lblcomdty.Text = dr4["Commodity_Name"].ToString();

        lbltransp.Text = dr4["Transporter_Name"].ToString();
        lblvicln.Text = dr4["Truck_Number"].ToString();
        lblcrop.Text = dr4["Crop_Year"].ToString();

        lblsend_bagsNum.Text = dr4["No_of_Bags"].ToString();

        lblSend_Qtydisplay.Text = dr4["Quantity"].ToString();

        string daten = DateTime.Now.ToString();
        string gdaten = getdate(daten);
        lblgdtae.Text = gdaten;

        lblbagno.Text = dr4["Recd_Bags"].ToString();
        lblweight.Text = dr4["Recd_Qty"].ToString();
        lblwcmno.Text = dr4["Acceptance_No"].ToString();

        lblmoisture.Text = getdateM(dr4["Acceptance_Date"].ToString());
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
