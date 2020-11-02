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
public partial class print_Gatepass : System.Web.UI.Page
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

            ssid = sid.Substring(0,5);
            ssid = "23" + ssid;
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
        string qrey = "select DistrictId,DepotName  from dbo.tbl_MetaData_DEPOT where DepotID='" + ssid + "'";
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
        string daten = DateTime.Today.Date.ToString();
        string gdaten = getdate(daten);
        lblgdtae.Text = gdaten;

        mobj1 = new MoveChallan(ComObj);
        string qrey4 = "select tbl_Receipt_Details.* ,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name,tbl_MetaData_SCHEME.Scheme_Name,Transporter_Table.Transporter_Name,tbl_MetaData_DEPOT.DepotName,districtsmp.district_name from dbo.tbl_Receipt_Details left join dbo.tbl_MetaData_STORAGE_COMMODITY on tbl_Receipt_Details.Commodity=tbl_MetaData_STORAGE_COMMODITY .Commodity_ID left join dbo.tbl_MetaData_SCHEME on tbl_Receipt_Details.scheme =tbl_MetaData_SCHEME.Scheme_Id left join dbo.Transporter_Table on tbl_Receipt_Details.Transporter=Transporter_Table.Transporter_ID left join pds.districtsmp  on  tbl_Receipt_Details.A_dist=districtsmp.district_code left join dbo.tbl_MetaData_DEPOT on tbl_Receipt_Details.A_Depo=tbl_MetaData_DEPOT.DepotID where tbl_Receipt_Details.Depot_ID='" + ssid + "' and tbl_Receipt_Details.Receipt_Id='" + sid + "'";
        DataSet ds4 = mobj1.selectAny(qrey4);
        DataRow dr4 = ds4.Tables[0].Rows[0];
        lbldepon.Text = dname;
        lblchallanno.Text = dr4["challan_no"].ToString();
        lbladate.Text = getdate(dr4["arrival_date"].ToString());
        //lblchallandt.Text = dr4["challan_date"].ToString();
        cdate = dr4["challan_date"].ToString();
        string gdate = getdate(cdate);
        lblchallandt.Text = gdate;
        lblcomdty.Text = dr4["Commodity_Name"].ToString();
        lblscheme.Text = dr4["Scheme_Name"].ToString();
       lbldistric .Text = dr4["district_name"].ToString();
       lblissue.Text = dr4["DepotName"].ToString();
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
