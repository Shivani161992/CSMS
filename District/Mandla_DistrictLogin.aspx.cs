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

public partial class District_Mandla_DistrictLogin : System.Web.UI.Page
{

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    chksql chk = null;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["dist_id"] != null || Session["Operations"] != null)  
        {
        //txtpassword.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        //txtpassword.Attributes.Add("onchange", "return chksqltxt(this)");
        //txtpassword.Attributes.Add("onchange", "return chksqltxt_psw(this),MD5(this);");

        //ArrayList ctrllist = new ArrayList();
        //ctrllist.Add(txtpassword.Text);

        //if (chk == null)
        //{
        //}
        //else
        //{
        //    bool chkstr = chk.chksql_server(ctrllist);
        //    if (chkstr == true)
        //    {
        //        Page.Server.Transfer(HttpContext.Current.Request.Path);
        //    }
        //}

        if (!IsPostBack)
        {
            //int saltSize = 5;
            //string salt = "";
            //salt = CreateSalt(saltSize);
            //Session["salt"] = salt.ToString();

        }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    //public string CreatePasswordHash(string dist)
    //{
    //    string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(dist.ToString().Trim(), "MD5");
    //    return hashedPwd;
    //}
    //protected int checkOpt()
    //{
    //    string chk = txtpassword.Text;
    //    string chk1 = chk.ToLower();
    //    if (chk1.Contains("drop"))
    //    {
    //        //Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Drop not allowed...'); </script> ");
    //        return (1);

    //    }
    //    else if (chk1.Contains("delete"))
    //    {
    //        return (1);

    //    }
    //    else if (chk1.Contains("alter"))
    //    {
    //        return (1);
    //    }
    //    else if (chk1.Contains("modify"))
    //    {
    //        return (1);

    //    }
    //    else if (chk1.Contains("truncate"))
    //    {
    //        return (1);

    //    }
    //    else
    //    {
    //        return (0);

    //    }
    //}

    //private static string CreateSalt(int size)
    //{
    //    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
    //    byte[] buff = new byte[size + 1];
    //    rng.GetBytes(buff);
    //    return Convert.ToBase64String(buff);
    //}

    //private string base64Encode(string sData)
    //{
    //    try
    //    {
    //        byte[] encData_byte = new byte[sData.Length];
    //        encData_byte = System.Text.Encoding.UTF8.GetBytes(sData);
    //        string encodedData = Convert.ToBase64String(encData_byte);
    //        return encodedData;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception("Error in base64Encode" + ex.Message);
    //    }
    //}

    //public string base64Decode(string sData)
    //{
    //    System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
    //    System.Text.Decoder utf8Decode = encoder.GetDecoder();
    //    byte[] todecode_byte = Convert.FromBase64String(sData);
    //    int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
    //    char[] decoded_char = new char[charCount];
    //    utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
    //    string result = new String(decoded_char); return result;
    //}
    protected void bttsub_Click(object sender, EventArgs e)
    {
        //if (Session["salt"] == null)
        //{
        //    Response.Redirect("http://mpscsc.mp.gov.in/");
        //}

        

       // else
       
            string strpd = "";
            string strpdmasterpassword = "";
            string password = txtpassword.Text;
           

           // string qry = "SELECT SurveyorID, MobNum, MasterPassword, Password, SurveyorName FROM SMSCom_SurveyorMaster where MobNum='" + txtuser.Text + "' and GETDATE()>=Fromdate and GETDATE()<=Todate ";

            //if (con.State == ConnectionState.Closed)
            //{
            //    con.Open();
            //}
            //SqlCommand cmd = new SqlCommand(qry, con);

            //SqlDataAdapter da = new SqlDataAdapter(cmd);

            //DataSet ds = new DataSet();

            //da.Fill(ds);

            //if (ds.Tables[0].Rows.Count > 0)
            //{
                //strpd = ds.Tables[0].Rows[0]["Password"].ToString();

            strpd = "Mand123la";
                //ID = ds.Tables[0].Rows[0]["SurveyorID"].ToString();

                //string hpwd1 = CreatePasswordHash(strpd.ToLower());
                //string hpwd1masterPassword = CreatePasswordHash(strpdmasterpassword.ToLower());

            if (password != strpd)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Wrong Password.. |'); </script> ");
                    return;
                }

                else
                {
                    //string hpwd = CreatePasswordHash(strpd.ToLower());
                    //string hpwdmasterPassword = CreatePasswordHash(strpdmasterpassword.ToLower());

                    if (password == strpd)
                    {

                      
                        Session["user"] = strpd;



                        Response.Redirect("~/District/PaddyMillingHome.aspx");
                    }
                }

            //}

            //else
            //{
            //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('इस Surveyor की वैधता समाप्त हो गयी है|'); </script> ");
            //    return;
            //}
        }


   
}