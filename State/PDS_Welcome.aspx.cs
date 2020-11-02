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
public partial class State_PDS_Welcome : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    public string stid = "";
    public string tid = "";
    public string tname = "";
    public string gatepass = "";
    public int getnum;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {
            stid = Session["st_id"].ToString();



            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

            if (Page.IsPostBack == false)
            {
               

            }


        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
       

    }
    void GetConnection()
    {
        string qry = "SELECT DB_NAME(dbid) as DBName,COUNT(dbid) as NumberOfConnections,loginame as LoginName FROM sys.sysprocesses WHERE  dbid > 0 and DB_NAME(dbid)='MPSCSC' GROUP BY dbid, loginame";
        mobj = new MoveChallan(ComObj);
        DataSet ds = mobj.selectAny(qry);
        if (ds.Tables[0].Rows.Count == 0)
        {

        }
        else
        {
            DataRow drp = ds.Tables[0].Rows[0];
            
        }
    }
}
