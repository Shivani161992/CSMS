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
public partial class DCCB_Welcome : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    public string distid = "";
    public string tid = "";
    public string issueid = "";
    public string gatepass = "";
    public int getnum;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            distid = Session["dist_id"].ToString();

            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

            if (!IsPostBack)
            {

                string httpf = Application["sPath"].ToString();
                string shtt = httpf + "/MainLogin.aspx";
                string st = Request.UrlReferrer.ToString(); ;
                if (shtt == st)
                {
                    insertLog();

                }
                else
                {
                  

                }
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

    }
    public string GetCurrentPageName()
    {
        string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
        System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
        string sRet = oInfo.Name;
        return sRet;
    }

    void insertLog()
    {
        string of = "False";
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string hh = DateTime.Now.Hour.ToString();
        string mm = DateTime.Now.Minute.ToString();
        string ss = DateTime.Now.Second.ToString();
        string time = hh + ":" + mm + ":" + ss;
        string logtype = "7";

        string qry = "insert into dbo.User_LogTime(Login_Type,user_id,IP_Address,Login_Date,Login_Time,offline)values('" + logtype + "','" + distid + "','" + ip + "',getdate(),'" + time + "','" + of + "')";
        cmd.Connection = con;
        cmd.CommandText = qry;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();


        }
        catch (Exception ex)
        {

        }
        finally
        {
            con.Close();
            ComObj.CloseConnection();
        }


    }
}
