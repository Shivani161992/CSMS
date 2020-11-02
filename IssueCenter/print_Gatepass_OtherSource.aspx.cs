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
public partial class print_Gatepass_OtherSource : System.Web.UI.Page
{

   
    MoveChallan mobj1 = null;
    protected Common ComObj = null, cmn = null;
    public string distname="";
    public string dist = "";
    public string sid = "";
    public string ssid = "";
    public string dname= "";
    public string snid = "";
    public string distid = "";
    public string dipotid = "";
    public string cdate = "";
    protected void Page_Load(object sender, EventArgs e)
    {      
             
        
        if (Session["Receipt_ID"] != null)
        {
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
            sid = Session["Receipt_ID"].ToString();

            //ssid = sid.Substring(0,5);
            //ssid = "23" + ssid;
            //GetDistt();
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

    }
    void GetDistt()
    {
             
       
        lblgno.Text = sid;
        string daten = DateTime.Today.Date.ToString();
        string gdaten = getdate(daten);
        lblgdtae.Text = gdaten;

        mobj1 = new MoveChallan(ComObj);
        string qrey4 = "SELECT Transporter_Table.Transporter_Name,tbl_Receipt_Details.Dist_Id, pds.districtsmp.district_name, tbl_Receipt_Details.Depot_ID, tbl_MetaData_DEPOT.DepotName,tbl_Receipt_Details.A_dist, tbl_Receipt_Details.A_Depo, DepoCode.DepoName, DepoCode.District,tbl_Receipt_Details.Commodity, tbl_MetaData_STORAGE_COMMODITY .Commodity_Name, tbl_Receipt_Details.Scheme,tbl_MetaData_SCHEME.Scheme_Name, tbl_Receipt_Details.RO_No, tbl_Receipt_Details.TO_Number,tbl_Receipt_Details.Dispatch_Date, tbl_Receipt_Details.arrival_date, tbl_Receipt_Details.challan_no,tbl_Receipt_Details.challan_date, tbl_Receipt_Details.Qty, tbl_Receipt_Details.Crop_year, tbl_Receipt_Details.Category,tbl_Receipt_Details.Transporter, tbl_Receipt_Details.Vehile_no, tbl_Receipt_Details.Arrival_time,tbl_Receipt_Details.Gunny_type, tbl_Receipt_Details.No_of_Bags, tbl_Receipt_Details.Recd_Qty,tbl_Receipt_Details.Recieved_Bags, tbl_Receipt_Details.Moisture, tbl_Receipt_Details.WCM_no,tbl_Receipt_Details.Variation_qty FROM tbl_Receipt_Details LEFT JOIN  pds.districtsmp ON tbl_Receipt_Details.Dist_Id = pds.districtsmp.district_code LEFT JOIN  tbl_MetaData_DEPOT ON tbl_Receipt_Details.Depot_ID = tbl_MetaData_DEPOT.DepotID LEFT JOIN  DepoCode ON tbl_Receipt_Details.A_dist = DepoCode.District_Code AND tbl_Receipt_Details.A_Depo = DepoCode.DepoCode LEFT JOIN tbl_MetaData_STORAGE_COMMODITY ON tbl_Receipt_Details.Commodity = tbl_MetaData_STORAGE_COMMODITY .Commodity_Id LEFT JOIN tbl_MetaData_SCHEME ON tbl_Receipt_Details.Scheme = tbl_MetaData_SCHEME.Scheme_Id left join dbo.Transporter_Table on tbl_Receipt_Details.Transporter=Transporter_Table.Transporter_ID where tbl_Receipt_Details.S_of_arrival='03' and tbl_Receipt_Details.Receipt_id='" + sid + "'";
        DataSet ds4 = mobj1.selectAny(qrey4);
        DataRow dr4 = ds4.Tables[0].Rows[0];

        lbldepon.Text = dr4["DepotName"].ToString();
        lbldepott.Text = dr4["district_name"].ToString();
        //lblfcidist.Text = dr4["District"].ToString();
        //lblfcidepot.Text = dr4["DepoName"].ToString();
        //lblrono.Text = dr4["RO_No"].ToString();
        //lbltono.Text = dr4["TO_Number"].ToString();
        
        lblchallanno.Text = dr4["challan_no"].ToString();
        lbladate.Text = getdate(dr4["arrival_date"].ToString());
        //lblchallandt.Text = dr4["challan_date"].ToString();
        cdate = dr4["challan_date"].ToString();
        string gdate = getdate(cdate);
        lblchallandt.Text = gdate;
        lblcomdty.Text = dr4["Commodity_Name"].ToString();
        lblscheme.Text = dr4["Scheme_Name"].ToString();
        lbltransp.Text = dr4["Transporter_Name"].ToString();
        lblvicln.Text = dr4["Vehile_no"].ToString();
        lblatime.Text  = dr4["Arrival_time"].ToString();
        lblbagno.Text = dr4["No_of_Bags"].ToString();
        lblweight.Text  = dr4["Recd_Qty"].ToString();
        lblwcmno.Text  = dr4["WCM_no"].ToString();
        lblmoisture.Text  = dr4["Moisture"].ToString();

       




    
    }
    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MM/yyyy");
    }

}
