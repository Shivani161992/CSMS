using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Data;
using DataAccess;
using System.Security.Cryptography;
using System.Data.SqlClient;
public partial class District_District_Login : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    chksql chk = null;
    string Distcode;
    protected void Page_Load(object sender, EventArgs e)
    {
        txtpassword.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        txtpassword.Attributes.Add("onchange", "return chksqltxt(this)");
        txtpassword.Attributes.Add("onchange", "return chksqltxt_psw(this),MD5(this);");

        ArrayList ctrllist = new ArrayList();
        ctrllist.Add(txtpassword.Text);

        if (chk == null)
        {
        }
        else
        {
            bool chkstr = chk.chksql_server(ctrllist);
            if (chkstr == true)
            {
                Page.Server.Transfer(HttpContext.Current.Request.Path);
            }
        }
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                int saltSize = 5;
                string salt = "";
                salt = CreateSalt(saltSize);
                Session["salt"] = salt.ToString();

                txtuser.Text = Session["dist_name"].ToString();
                Distcode = Session["dist_id"].ToString();

                

               

               
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

       
    }
    public string CreatePasswordHash(string dist)
    {
        string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(dist.ToString().Trim(), "MD5");
        return hashedPwd;
    }
    protected int checkOpt()
    {
        string chk = txtpassword.Text;
        string chk1 = chk.ToLower();
        if (chk1.Contains("drop"))
        {
            //Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Drop not allowed...'); </script> ");
            return (1);

        }
        else if (chk1.Contains("delete"))
        {
            return (1);

        }
        else if (chk1.Contains("alter"))
        {
            return (1);
        }
        else if (chk1.Contains("modify"))
        {
            return (1);

        }
        else if (chk1.Contains("truncate"))
        {
            return (1);

        }
        else
        {
            return (0);

        }
    }

    private static string CreateSalt(int size)
    {
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        byte[] buff = new byte[size + 1];
        rng.GetBytes(buff);
        return Convert.ToBase64String(buff);
    }

    private string base64Encode(string sData)
    {
        try
        {
            byte[] encData_byte = new byte[sData.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(sData);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }
        catch (Exception ex)
        {
            throw new Exception("Error in base64Encode" + ex.Message);
        }
    }

    public string base64Decode(string sData)
    {
        System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
        System.Text.Decoder utf8Decode = encoder.GetDecoder();
        byte[] todecode_byte = Convert.FromBase64String(sData);
        int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
        char[] decoded_char = new char[charCount];
        utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
        string result = new String(decoded_char); return result;
    }



    protected void bttsub_Click(object sender, EventArgs e)
    {
         Distcode = Session["dist_id"].ToString();

        if (Session["salt"] == null)
        {
            Response.Redirect("http://mpscsc.mp.gov.in/");
        }

        if (txtuser.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Login |'); </script> ");
        }

        else
        {
            string strpd = "";
            string Masterpwd = "";
            string password = txtpassword.Text;
            string MillerName = "";

            string qry = "SELECT Password , Masterpassword FROM dbo.Distt_login where District_ID='"+Distcode+"' ";

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand(qry, con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                strpd = ds.Tables[0].Rows[0]["Password"].ToString();
               // MillerName = ds.Tables[0].Rows[0]["Mill_Name"].ToString();
                Masterpwd = ds.Tables[0].Rows[0]["Masterpassword"].ToString();

                string hpwd1 = CreatePasswordHash(strpd.ToLower());
                string hpwdmspwd = CreatePasswordHash(Masterpwd.ToLower());
                if (password != hpwd1.ToLower() || password != hpwdmspwd.ToLower())
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Wrong Password.. |'); </script> ");
                    return;
                }

                else
                {
                    string hpwd = CreatePasswordHash(strpd.ToLower());
                    string hpwdmpwd = CreatePasswordHash(Masterpwd.ToLower());

                    if (password == hpwd.ToLower() || password == hpwdmpwd.ToLower())
                    {

                        Session["password"] = "";

                        Session["DistName"] = txtuser.Text;
                        Session["DistCode"] = Distcode;
                        Session["Operations"] = "Operations";


                        Response.Redirect("~/District/PaddyMillingHome.aspx");
                    }
                }

            }
        }




    }
}