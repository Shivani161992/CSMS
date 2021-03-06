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
using DataAccess;
using Data;
using whtmpData;

public partial class WHP14_Login1 : System.Web.UI.Page
{
    private Common ComObj = null;
    private Collector coll = null;
    private Irrigation irri = null;
    private DCCB dccb = null;
    private StateLogin stst = null;
    private AgencyLogin agen = null;
    private DistrictsWhtMP distr = null;
    private SocietyLogin soc = null;
    private loginWhtMP lobj = null;
    private DataReader dobj = null;
    private PurchaseCenterWhtMp PC = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            txtpwd.Focus();
            //ComObj = new Common(ConfigurationManager.AppSettings["Appconstr_WPMS2014_Test"].ToString());
            ComObj = new Common(ConfigurationManager.AppSettings["Appconstr_WPMS2014"].ToString());

            txtpwd.Attributes.Add("onkeypress", "return checksqlkey_psw(event,this)");
            txtpwd.Attributes.Add("onKeyUp", "return taCount(this,'myCounter')");
            txtpwd.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtpwd.Attributes.Add("onchange", "return chksqltxt_psw(this),MD5(this);");
            txtpwd.Attributes.Add("onKeyUp", "return Do_login();");
            btnlogin.Attributes.Add("onkeypress", "return LoginOnEnter(this);");

            if (!IsPostBack)
            {
                Session["App"] = "WPMS2014";
                Session["User"] = "Otheruser";
                Session["UserName"] = "PurchaseCenter";
                Session["UserNameHINDI"] = "उपार्जन केन्द्र";
                btnlogin.Focus();
                int saltSize = 5;
                string salt = "";
                salt = CreateSalt(saltSize);
                Session["salt"] = salt.ToString();
            }
            else
            {
                //Response.Redirect("../Login1.aspx");
            }
        }
        catch (Exception ex)
        { }
    }

    private bool DoLogin(string qry, string user)
    {
        bool res = false;
        if (Session["salt"] == null)
        {
            Response.Redirect("../Login1.aspx");
        }
        string strpd = "";
        lobj = new loginWhtMP(ComObj);
        DataSet ds = lobj.selectAny(qry);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                string pwd = dr["Password"].ToString();
                strpd = pwd;
                string password = txtpwd.Text;
                txtpwd.Text = "";
                string hpwd = CreatePasswordHash(strpd.ToLower());
                if (password == lobj.Password || password == hpwd.ToLower())
                {
                    string tx_hashedPasswordAndSalt = CreatePasswordHash(user.Trim().ToLower()).ToLower();
                    string tx_hashedPasswordAndSalt1 = CreatePasswordHash(Session["salt"].ToString().Trim() + tx_hashedPasswordAndSalt.Trim()).ToLower();

                    if (Session["User"].ToString() == "Otheruser")
                    {
                        if (Session["UserName"].ToString() == "PurchaseCenter")
                        {
                            Session["PurchaseCenterName"] = ds.Tables[0].Rows[0]["PurchaseCenterName"].ToString();
                            Session["District_Code"] = ds.Tables[0].Rows[0]["District_Code"].ToString();
                            Session["DistrictName"] = dr["District_Name"].ToString();
                            Session["Society_Id"] = ds.Tables[0].Rows[0]["Society_Code"].ToString();
                            Session["Society_Name"] = ddl_Society.Text.ToString();
                        }
                        else if (Session["UserName"].ToString() == "CallCenter")
                        {

                        }

                    }



                    res = true;

                    #region Lock

                    #endregion

                }
            }
        }
        return res;
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
    protected void btnlogin_Click(object sender, EventArgs e)
    {
        string user = "";
        string District_Code = "";
        string qry = "";
        string username = "";
        if (Session["User"] == "Otheruser")
        {
            if (Session["UserName"].ToString() == "PurchaseCenter")
            {
                username = Session["UserName"].ToString();
                string Society_Code = ddl_Society.Text.ToString();
                qry = "SELECT a.PC_Id,a.PurchaseCenterName,a.Society_Code,a.District_Code,b.District_Name,a.IsPaddy,a.IsMaize,a.IsWheat,a.Password,a.IP,a.UpdatedDate FROM PurchaseCenter_login a,Districts b where Society_Code='" + Society_Code + "' and a.District_Code=b.District_Code";
                bool res = DoLogin(qry, username);
                if (res)
                    Response.Redirect("~/WHP14/Procurement_Wheat/frm_AnajPrapti_FromFarmer.aspx");
                else
                    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('आपका पासवर्ड गलत है'); </script> ");

            }
        }
    }
}
