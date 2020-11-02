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

public partial class FrmLogOut : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    public string adminid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ag_id"] != null)
        {
            adminid = Session["ag_id"].ToString();
            Session.Abandon();
            Session.RemoveAll();
            //Update();
            SqlConnection.ClearAllPools();
            Response.Redirect("~/MainLogin.aspx");

        }

    }
    void Update()
    {
        string of = "True";
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string hh = DateTime.Now.Hour.ToString ();
        string mm = DateTime.Now.Minute.ToString();
        string ss = DateTime.Now.Second.ToString();
        string time = hh +":"+ mm + ":"+ ss;
        string qryUpdate = "Update dbo.User_LogTime Set Logout_Time ='" + time + "',offline='" + of + "' where user_id='" + adminid + "' and IP_Address='" + ip + "'";
        cmd.Connection = con;
        cmd.CommandText = qryUpdate;
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
            
        }
    }
}
