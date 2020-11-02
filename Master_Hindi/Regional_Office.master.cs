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
using System.Security.Cryptography;
public partial class MasterPage_Regional_Office : System.Web.UI.MasterPage
{
    public string sid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Region_id"] != null)
        {
            string distid = Session["dist_idH"].ToString().Trim();
            string tx_hashedPasswordAndSalt = CreatePasswordHash(Session["Region_id"].ToString().Trim().ToLower()).ToLower();
            string tx_hashedPasswordAndSalt1 = CreatePasswordHash(Session["salt"].ToString().Trim() + tx_hashedPasswordAndSalt.Trim()).ToLower();

            if (distid != tx_hashedPasswordAndSalt1)
            {
                Response.Redirect("~/MainLogin.aspx");
            }

            string dname = Session["Region_name"].ToString();
            Label1.Text = dname;

        }
        else
        {
            Response.Redirect("Session_Expire_Dist.aspx");

        }
    }
    protected void logout_Click(object sender, EventArgs e)
    {


    }
    private static string CreateSalt(int size)
    {
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        byte[] buff = new byte[size + 1];
        rng.GetBytes(buff);
        return Convert.ToBase64String(buff);
    }
    public string CreatePasswordHash(string dist)
    {
        string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(dist.ToString().Trim(), "MD5");
        return hashedPwd;
    } 
}
