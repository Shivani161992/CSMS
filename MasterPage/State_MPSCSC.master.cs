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

using System.Collections.Generic;
using System.Diagnostics;
using Data;
using DataAccess;
using System.Data.SqlClient;
public partial class State_MPSCSC : System.Web.UI.MasterPage
{
    chksql chk = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {
            if (Session["st_id"].ToString().Trim() == "2")
            {
                span1.Visible = false;
                span2.Visible = false;
                span3.Visible = false;
                span4.Visible = true;
                // span5.Visible = false;
                span6.Visible = false;
                HyperLink2.Visible = false;
                HyperLink5.Visible = false;
                HyperLink4.Visible = false;
                HyperLink6.Visible = false;
                HyperLink8.Visible = false;
                HyperLink9.Visible = false;
                HyperLink10.Visible = false;
                HyperLink13.Visible = false;
                HyperLink14.Visible = false;
                HyperLink7.Visible = false;
                HyperLink3.Visible = true;
                // HyperLink1.Visible = false;
            }


            string stid = Session["st_idH"].ToString().Trim();
            string tx_hashedPasswordAndSalt = CreatePasswordHash(Session["st_id"].ToString().Trim().ToLower()).ToLower();
            string tx_hashedPasswordAndSalt1 = CreatePasswordHash(Session["salt"].ToString().Trim() + tx_hashedPasswordAndSalt.Trim()).ToLower();

            if (stid != tx_hashedPasswordAndSalt1)
            {
                Response.Redirect("~/MainLogin.aspx");
            }
            string dname = Session["st_Name"].ToString();
            Label1.Text = "Head Office";
            DateTime today = DateTime.Today;
            lbldate.Text = Convert.ToString(today.ToString("dd/MM/yyyy"));

        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");

        }

    }

    public void DisableCriticalJavaScriptFiles()
    {
        critical_js_files.Visible = false;
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

  
    //protected void bttnLogin_Click(object sender, EventArgs e)
    //{
    //    if ()
    //    {
    //        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Enter UserName |'); </script> ");
    //    }

    //    else
    //    {
    //        string strpd = "";
    //        string strpdmasterpassword = "";
    //        string password = txtpassword.Text;
    //        string UserName = "";
    //        string Name, ID = "";
    //        string Masterpassword = txtpassword.Text;

    //        string qry = "SELECT SurveyorID, MobNum, MasterPassword, Password, SurveyorName FROM SMSCom_SurveyorMaster where MobNum='" + txtuser.Text + "' and GETDATE()>=Fromdate and GETDATE()<=Todate ";

    //        if (con.State == ConnectionState.Closed)
    //        {
    //            con.Open();
    //        }
    //        SqlCommand cmd = new SqlCommand(qry, con);

    //        SqlDataAdapter da = new SqlDataAdapter(cmd);

    //        DataSet ds = new DataSet();

    //        da.Fill(ds);

    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            strpd = ds.Tables[0].Rows[0]["Password"].ToString();
    //            UserName = ds.Tables[0].Rows[0]["MobNum"].ToString();
    //            Name = ds.Tables[0].Rows[0]["SurveyorName"].ToString();
    //            strpdmasterpassword = ds.Tables[0].Rows[0]["MasterPassword"].ToString();
    //            ID = ds.Tables[0].Rows[0]["SurveyorID"].ToString();

    //            string hpwd1 = CreatePasswordHash(strpd.ToLower());
    //            string hpwd1masterPassword = CreatePasswordHash(strpdmasterpassword.ToLower());

    //            if (password != hpwd1.ToLower() && password != hpwd1masterPassword.ToLower())
    //            {
    //                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Wrong Password.. |'); </script> ");
    //                return;
    //            }

    //            else
    //            {
    //                string hpwd = CreatePasswordHash(strpd.ToLower());
    //                string hpwdmasterPassword = CreatePasswordHash(strpdmasterpassword.ToLower());

    //                if (password == hpwd.ToLower() || password == hpwdmasterPassword.ToLower())
    //                {

    //                    Session["password"] = "";

    //                    Session["userGodown"] = txtuser.Text;

                       

    //                    if (strpd == "SMSSUR")
    //                    {
    //                        Response.Redirect("~/CSMSSurveyorLogin/GodownSurveyor_ChangePassword.aspx");

    //                    }
    //                    else if (strpd != "SMSSUR")

    //                        Response.Redirect("~/CSMSSurveyorLogin/CSMS_SurveyorLogin_Welcome.aspx");
    //                }
    //            }

    //        }

    //        else
    //        {
    //            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('?? Surveyor ?? ????? ?????? ?? ??? ??|'); </script> ");
    //            return;
    //        }
    //    }


    //}
}
