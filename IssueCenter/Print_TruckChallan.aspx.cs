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
public partial class Print_TruckChallan : System.Web.UI.Page
{
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
        if (Session["gatepass"] != null)
        {
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
            sid = Session["gatepass"].ToString();
            distid = Session["dist_id"].ToString();
            issueid = Session["issue_id"].ToString();
            //ssid = sid.Substring(0, 7);
            GetDistt();
        }
        //  sid = "22222";
        //  distid = "26";
        //  issueid ="2326001";
        //GetDistt();
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }
    void GetDistt()
    {
       // sid = "22222";
       //distid ="26";
        mobj1 = new MoveChallan(ComObj);
        string qrey4 = "select SCSC_Truck_challan.*,Transporter_Table.Transporter_Name as Transporter_Name  from dbo.SCSC_Truck_challan left join Transporter_Table on SCSC_Truck_challan.Transporter=Transporter_Table.Transporter_ID  where SCSC_Truck_challan.Dist_ID='" + distid + "'and SCSC_Truck_challan.Depot_Id='" + issueid + "' and SCSC_Truck_challan.Challan_No='" + sid + "'";
        DataSet ds4 = mobj1.selectAny(qrey4);
        DataRow dr4 = ds4.Tables[0].Rows[0];

        string discode = dr4["Dist_ID"].ToString();

        lbldepott.Text = dr4["Dist_ID"].ToString();

        string date = dr4["Challan_Date"].ToString();
        string cdate = getdate(date);

        lblddate.Text = cdate;
      
        string comdty = dr4["Commodity"].ToString();
        string dispatchg = dr4["Dispatch_Godown"].ToString();

        string toissue = dr4["Sendto_IC"].ToString();



        int schid = int.Parse(dr4["Scheme"].ToString());


        lblbagno.Text = dr4["Bags"].ToString();
        lbltime.Text  = dr4["Dispatch_Time"].ToString();
        lblchalanno.Text = dr4["Challan_No"].ToString();
        lblweight.Text = dr4["Qty_send"].ToString();
        lbltruckno.Text = dr4["Truck_no"].ToString();
        lbltransname.Text = dr4["Transporter_Name"].ToString();

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

        string qrysg = "select DepotName from dbo.tbl_MetaData_DEPOT where DepotID='" + toissue + "'";
        DataSet dssg = mobj1.selectAny(qrysg);
        DataRow drsg = dssg.Tables[0].Rows[0];
        lblgname.Text = drsg["DepotName"].ToString();

        string qryssg = "select DepotName from dbo.tbl_MetaData_DEPOT where DepotID='" + issueid + "'";
        DataSet dsgs = mobj1.selectAny(qryssg);
        DataRow drsgs = dsgs.Tables[0].Rows[0];
        lblpname.Text = drsgs["DepotName"].ToString();


    }
    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MM/yyyy");
    }
}
