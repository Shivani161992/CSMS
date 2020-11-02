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
using System.Security.Cryptography;
public partial class Sugar_MPSCSC : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {
            if (Session["st_id"].ToString().Trim() == "2")
            {
               
                span2.Visible = false;
            
                span4.Visible = true;
                span5.Visible = false;
         
               
                HyperLink8.Visible = false;
              
                HyperLink13.Visible = false;
                HyperLink14.Visible = false;
            
                HyperLink3.Visible=true;
                HyperLink1.Visible = false;
            }
            

            string stid = Session["st_idH"].ToString().Trim();
            string tx_hashedPasswordAndSalt =CreatePasswordHash(Session["st_id"].ToString().Trim().ToLower()).ToLower();
            string tx_hashedPasswordAndSalt1 =CreatePasswordHash(Session["salt"].ToString().Trim() + tx_hashedPasswordAndSalt.Trim()).ToLower();

            if (stid != tx_hashedPasswordAndSalt1)
            {
                Response.Redirect("~/MainLogin.aspx");
            }
            string dname = Session["st_Name"].ToString();
            Label1.Text = dname;

        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");

        }

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
