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

public partial class mpproc_Admin_AdminWelcome : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_mpproc"].ToString());
    SqlCommand cmd = new SqlCommand();
    protected Common ComObj = null, cmn = null;
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["admin"] != null)
        {

            ComObj = new Common(ConfigurationSettings.AppSettings["ConnectionString_mpproc"].ToString());
            if (!IsPostBack)
            {


                if (Session["IS_Admin"] !=null)
                {

                    DoLoginLog();
                    Session.Remove("IS_Admin");
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
        string logtype = "4";
        string logid = Session["LogAid"].ToString();
        string qry = "insert into dbo.User_LogTime(Login_Type,Loginid,IP_Address,UserAgent,Login_Date,offline)values('" + logtype + "','" + logid + "','" + ip + "','" + userAgent + "',getdate(),'" + of + "')";
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
            DivMsg.InnerText = ex.Message;
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

