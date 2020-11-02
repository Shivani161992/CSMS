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
        
        ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
        
        //sid = Request.QueryString["C"].ToString();
        if (Session["gatepass"] != null)
        {
            sid = Session["gatepass"].ToString();
            distid = Session["dist_id"].ToString();
            
            //ssid = sid.Substring(0, 7);
            GetDistt();
        }
        //sid="12365";
        //distid = "26";
        //GetDistt();
    }
    void GetDistt()
    {
        //sid = "12365";
        //distid ="26";
        mobj1 = new MoveChallan(ComObj);
        string qrey4 = "select * from dbo.SCSC_Truck_challan where Dist_ID='" + distid + "' and  TC_number='" + sid + "'";
        DataSet ds4 = mobj1.selectAny(qrey4);
        DataRow dr4 = ds4.Tables[0].Rows[0];
        
        string discode = dr4["Dist_ID"].ToString();
        
        lbldepott .Text= dr4["Dist_ID"].ToString();
        lblrono.Text = dr4["RO_no"].ToString();
        lblroqty.Text = dr4["RO_qty"].ToString();

        string date = dr4["Dispatch_date"].ToString();
        string cdate = getdate(date);

        lblddate.Text = cdate;
        lblpname.Text = dr4["FCI_Depot"].ToString();
        string comdty = dr4["Commodity"].ToString();
        //lblcomdtyn.Text = dr4["Commodity"].ToString();
        int schid = int.Parse(dr4["Scheme"].ToString());
        

        lblbagno.Text = dr4["Bags"].ToString();
        lblchalanno.Text = dr4["TC_number"].ToString();
        lblweight.Text = dr4["Quantity"].ToString();
        lbltruckno.Text = dr4["Truck_no"].ToString();
        lbltransname.Text = dr4["Transporter"].ToString();

        mobj1 = new MoveChallan(ComObj);
        string qryd = "select Dist_name from pds.districtsmp where district_code='" + discode + "'";
        DataSet dsd = mobj1.selectAny(qryd);
        DataRow drd = dsd.Tables[0].Rows[0];
        lbldepott.Text = drd["Dist_name"].ToString();

        string qryc = "select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id='" + comdty + "'";
        DataSet dsc = mobj1.selectAny(qryc);
        DataRow drc = dsc.Tables[0].Rows[0];
        lblcomdtyn.Text = drc["Commodity_Name"].ToString();


        string qrys = "select Scheme_Name from dbo.tbl_MetaData_SCHEME where Scheme_Id =" + schid;
        DataSet dss = mobj1.selectAny(qrys);
        DataRow drs = dss.Tables[0].Rows[0];
        lblsch.Text = drs["Scheme_Name"].ToString();






       



    
    }
    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MM/yyyy");
    }

}
