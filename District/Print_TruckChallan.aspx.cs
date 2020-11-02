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
    public string Tono = "";

    protected void Page_Load(object sender, EventArgs e)
    {

       

        //sid = Request.QueryString["C"].ToString();
        if (Session["gatepass"] != null)
        {
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
            sid = Session["gatepass"].ToString();
            distid = Session["dist_id"].ToString();
            Tono = Session["ToNo"].ToString();
            //issueid = Session["issue_id"].ToString();
            //ssid = sid.Substring(0, 7);
            GetDistt();
        }
        
        //  issueid ="2801";
            
        //GetDistt();
        else
        {
        //    sid = "2801";
        //distid = "28";
        //Tono = "2323";
            Response.Redirect("~/MainLogin.aspx");
        }
    }
    void GetDistt()
    {
       // sid = "22222";
       //distid ="26";
        mobj1 = new MoveChallan(ComObj);
        string qrey4 = "select Lift_A_RO.*,Transporter_Table.Transporter_Name as Transporter_Name,tbl_MetaData_DEPOT.DepotName as DepotName,DepoCode.DepoName as DepoName   from dbo.Lift_A_RO left join Transporter_Table on Lift_A_RO.Transporter=Transporter_Table.Transporter_ID left join dbo.tbl_MetaData_DEPOT  on Lift_A_RO.Issue_center=tbl_MetaData_DEPOT.DepotID  left join DepoCode on Lift_A_RO.FCIdepo=DepoCode.DepoCode and Lift_A_RO.FCIdistrict=DepoCode.district_code  where Lift_A_RO.Dist_Id='" + distid + "'and Lift_A_RO.TO_Number='" + Tono + "' and Lift_A_RO.Challan_No='" + sid + "'";
        DataSet ds4 = mobj1.selectAny(qrey4);
        DataRow dr4 = ds4.Tables[0].Rows[0];

        string discode = dr4["Dist_ID"].ToString();

        lbldepott.Text = dr4["Dist_ID"].ToString();

        string date = dr4["Challan_Date"].ToString();
        string cdate = getdate(date);

        lblddate.Text = cdate;
        lblpname.Text = dr4["DepoName"].ToString();
        string comdty = dr4["Commodity"].ToString();
        //string dispatchg = dr4["Dispatch_Godown"].ToString();

        //string toissue = dr4["DepotName"].ToString();

        lblgname.Text = dr4["DepotName"].ToString();

        int schid = int.Parse(dr4["Scheme"].ToString());


        lblbagno.Text = dr4["No_of_Bags"].ToString();
        lbltime.Text  = dr4["Dispatch_Time"].ToString();
        lblchalanno.Text = dr4["Challan_No"].ToString();
        lblweight.Text = dr4["Qty_send"].ToString();
        lbltruckno.Text = dr4["Vehicle_No"].ToString();
        lbltransname.Text = dr4["Transporter_Name"].ToString();

        mobj1 = new MoveChallan(ComObj);
        string qryd = "select Dist_name from pds.districtsmp where district_code='" + discode + "'";
        DataSet dsd = mobj1.selectAny(qryd);
        DataRow drd = dsd.Tables[0].Rows[0];
        lbldepott.Text = drd["Dist_name"].ToString();

        string qryc = "select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY  where Commodity_Id='" + comdty + "'";
        DataSet dsc = mobj1.selectAny(qryc);
        DataRow drc = dsc.Tables[0].Rows[0];
        lblcomdtyn.Text = drc["Commodity_Name"].ToString();


        string qrys = "select Scheme_Name from dbo.tbl_MetaData_SCHEME where Scheme_Id =" + schid;
        DataSet dss = mobj1.selectAny(qrys);
        DataRow drs = dss.Tables[0].Rows[0];
        lblsch.Text = drs["Scheme_Name"].ToString();

        //string qrysg = "select DepotName from dbo.tbl_MetaData_DEPOT where DepotID='" + toissue + "'";
        //DataSet dssg = mobj1.selectAny(qrysg);
        //DataRow drsg = dssg.Tables[0].Rows[0];
        //lblgname.Text = drsg["DepotName"].ToString();

        //string qryssg = "select DepotName from dbo.tbl_MetaData_DEPOT where DepotID='" + issueid + "'";
        //DataSet dsgs = mobj1.selectAny(qryssg);
        //DataRow drsgs = dsgs.Tables[0].Rows[0];
        //lblpname.Text = drsgs["DepotName"].ToString();


    }
    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MM/yyyy");
    }
}
