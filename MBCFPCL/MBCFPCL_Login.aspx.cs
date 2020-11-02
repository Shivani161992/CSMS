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

public partial class MBCFPCL_MBCFPCL_Login : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    chksql chk = null;
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS

    public string gatepass = "";
    public int getnum;
    SqlDataReader dr;
    protected Common ComObj = null;
    //MoveChallan mobj1 = null;

    public string sid = "";

    SqlConnection con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
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

        if (!IsPostBack)
        {
            int saltSize = 5;
            string salt = "";
            salt = CreateSalt(saltSize);
            Session["salt"] = salt.ToString();
            GetDist();

        }
  
    }

    public void GetDist()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "";
                select = "SELECT district_name,district_code FROM pds.districtsmp where CSM_MBC='Y' Order By district_name";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtuser.DataSource = ds.Tables[0];
                        txtuser.DataTextField = "district_name";
                        txtuser.DataValueField = "district_code";
                        txtuser.DataBind();
                        txtuser.Items.Insert(0, "Select");

                    }
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
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
        if (txtuser.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please select District |'); </script> ");
            return;

        }
        else if (txtpassword.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Enter Password |'); </script> ");
            return;

        }
        else
        {
            string strpd = "";
            string strpdmasterpassword = "";
            string password = txtpassword.Text;
            string DistID = "";
            string DistrictName = "";


            string qry = "select Password, Master_PWD, D.district_name, MB.District from MBCFPCL_Login as MB  inner join pds.districtsmp as D on D.district_code=MB.District where District='" + txtuser.SelectedValue.ToString() + "'";

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

                strpdmasterpassword = ds.Tables[0].Rows[0]["Master_PWD"].ToString();
                DistID = ds.Tables[0].Rows[0]["District"].ToString();
                DistrictName = ds.Tables[0].Rows[0]["district_name"].ToString();


                string hpwd1 = CreatePasswordHash(strpd.ToLower());
                string hpwd1masterPassword = CreatePasswordHash(strpdmasterpassword.ToLower());

                if (password != hpwd1.ToLower() && password != hpwd1masterPassword.ToLower())
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Wrong Password.. |'); </script> ");
                    return;
                }

                else
                {
                    string hpwd = CreatePasswordHash(strpd.ToLower());
                    string hpwdmasterPassword = CreatePasswordHash(strpdmasterpassword.ToLower());

                    if (password == hpwd.ToLower() || password == hpwdmasterPassword.ToLower())
                    {
                   
                        Session["DistID"] = DistID;
                        Session["DistName"] = DistrictName;
                        Session["NotchangePassword"] = strpd;

                        if (strpd == "MBC123")
                        {
                            Response.Redirect("~/MBCFPCL/MBCFPCL_ChangePassword.aspx");

                        }
                        else if (strpd != "MBC123")
                        {

                            Response.Redirect("~/MBCFPCL/MPCFPCL_WelcomeHome.aspx");
                        }
                    }
                }

            }

            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('ये लोगिन वैध नहीं है|'); </script> ");
                return;
            }
        }

    }
  
}