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

public partial class Procurement_agency_welcom : System.Web.UI.Page

{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_mpproc"].ToString());
    SqlCommand cmd = new SqlCommand();
    protected Common ComObj = null, cmn = null;
    public string qry = "";
  
    protected void Page_Load(object sender, EventArgs e)
    {

        if ( Session["pc_name"] != null && Session["pcId"] != null)
        {

            ComObj = new Common(ConfigurationSettings.AppSettings["ConnectionString_mpproc"].ToString());
            if (!IsPostBack)
            {

                if (Session["IS_Login"] != null )
                {
                
                    DoLoginLog();
                    Session.Remove("IS_Login");
                }
                else
                {


                }
            }
        }
        else
        {

            Response.Redirect("../sessionexpired.aspx");


        }
    }

    private void DoLoginLog()
    {
        string of = "False";
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string userAgent = Request.Browser.Type.ToString();
        string logtype = "1";
        string agid = Session["Ag_id"].ToString();
        string logid = Session["Log_id"].ToString();
        string pcid = Session["pcId"].ToString();
        string qry = "insert into dbo.User_LogTime(Login_Type,Loginid,PCID,PCType_ID,IP_Address,UserAgent,Login_Date,offline)values('" + logtype + "','" + logid + "','" + pcid + "','" + agid + "','" + ip + "','" + userAgent + "',getdate(),'" + of + "')";
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
