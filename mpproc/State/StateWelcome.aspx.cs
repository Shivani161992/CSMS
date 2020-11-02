using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Data;
using DataAccess;
using System.Security.Cryptography;
using System.Data.SqlClient;

public partial class mpproc_State_StateWelcome : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_mpproc"].ToString());
    SqlCommand cmd = new SqlCommand();
    protected Common ComObj = null, cmn = null;
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["StateLog_Agency"] != null && Session["StateLog_AgId"] != null)
        {

            ComObj = new Common(ConfigurationSettings.AppSettings["ConnectionString_mpproc"].ToString());
            if (!IsPostBack)
            {

               
                if (Session["IS_StateLogin"] != null)
                {

                    DoLoginLog();
                    Session.Remove("IS_StateLogin");
                }
                else
                {


                }
            }
        }
        else
        {


            Response.Redirect("../mpproc/frmLogin.aspx");


        }
    }
    private void DoLoginLog()
    {
        string of = "False";
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string userAgent = Request.Browser.Type.ToString();
        string logtype = "3";
        string agid = Session["StateLog_AgId"].ToString();
        string logid = Session["Loginid"].ToString();
        string qry = "insert into dbo.User_LogTime(Login_Type,Loginid,PCType_ID,IP_Address,UserAgent,Login_Date,offline)values('" + logtype + "','" + logid + "','" + agid + "','" + ip + "','" + userAgent + "',getdate(),'" + of + "')";
        cmd.Connection = con;
        cmd.CommandText = qry;
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();


        }
        catch (Exception ex)
        {

        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            ComObj.CloseConnection();
        }
    }
}
