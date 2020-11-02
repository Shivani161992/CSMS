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
public partial class MasterPage_AgencyMaster : System.Web.UI.MasterPage

{
    public string sid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ag_id"] != null)
        {
            string distid = Session["ag_idH"].ToString().Trim();
            string tx_hashedPasswordAndSalt = CreatePasswordHash(Session["ag_id"].ToString().Trim().ToLower()).ToLower();
            string tx_hashedPasswordAndSalt1 = CreatePasswordHash(Session["salt"].ToString().Trim() + tx_hashedPasswordAndSalt.Trim()).ToLower();

            if (distid != tx_hashedPasswordAndSalt1)
            {
                Response.Redirect("~/MainLogin.aspx");
            }

            string dname = Session["ag_name"].ToString();
            Label1.Text = dname;

        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
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
