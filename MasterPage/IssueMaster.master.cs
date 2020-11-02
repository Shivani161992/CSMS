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
public partial class MasterPage2 : System.Web.UI.MasterPage
{
    public string sid = "";
    string version = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            if (Session["Markfed"].ToString() == "Y")
            {
                Image2.ImageUrl = "~/Images/markfed.jpg";

                HyperLink14.Visible = true;
                HyperLink11.Visible = true;
                hypserchid.Visible = true;
                HyperLink16.Visible = true;
                HyperLink19.Visible = true;
                hyprejecnote.Visible = true;
                hypdelproc.Visible = true;
                hyprejprocurement.Visible = true;

                HyperLink1.Visible = false;
                //HyperLink22.Visible = false;
                hypreturnrail.Visible = false;
                hypdelReceipt.Visible = false;

                Hydoorstep.Visible = false;
                HyperLink6.Visible = false;
                HyperLink7.Visible = false;
                //Hpr_miller.Visible = false;
                hypdel_do.Visible = false;
                HyperLink13.Visible = false;
                DH27.Visible = false;

                HyperLink12.Visible = false;
                HyperLink20.Visible = false;
                HyperLink27.Visible = false;
                hypdeletedisp.Visible = false;

                HyperLink2.Visible = true;
                hypReprintAcceptN.Visible = true;
                hypprrejnote.Visible = true;
                
                hypprintDo.Visible = false;
                HyperTO.Visible = false;
                //HyperLink25.Visible = false;
               // HyperLink26.Visible = false;
                HyperLink17.Visible = false;
                HyperLink9.Visible = false;
                HyperLink21.Visible = false;

                HyperLink5.Visible = false;

                lnk_Truckchalan_fps.Visible = false;

                spsn_fps.Visible = false;

                span5.Visible = false;

                span1.Visible = false;

                span7.Visible = false;
                span9.Visible = false;
                span6.Visible = false;

                span2.Visible = false;

             
            }

            else
            {
                Image2.ImageUrl = "~/Images/header.jpg";
                HyperLink14.Visible = true;
                HyperLink11.Visible = true;
                hypserchid.Visible = true;
                HyperLink16.Visible = true;
                HyperLink19.Visible = true;
                hyprejecnote.Visible = true;
                hypdelproc.Visible = true;
                hyprejprocurement.Visible = true;

                HyperLink1.Visible = true;
                //HyperLink22.Visible = true;
                hypreturnrail.Visible = true;
                hypdelReceipt.Visible = true;

                Hydoorstep.Visible = true;
                HyperLink6.Visible = true;
                HyperLink7.Visible = true;
                //Hpr_miller.Visible = true;
                hypdel_do.Visible = true;
                HyperLink13.Visible = true;
                DH27.Visible = true;

                HyperLink12.Visible = true;
                HyperLink20.Visible = true;
                HyperLink27.Visible = true;
                hypdeletedisp.Visible = true;

                HyperLink2.Visible = true;
                hypReprintAcceptN.Visible = true;
                hypprrejnote.Visible = true;


                hypprintDo.Visible = true;
                HyperTO.Visible = true;
               // HyperLink25.Visible = true;
               // HyperLink26.Visible = true;
                HyperLink17.Visible = true;
                HyperLink9.Visible = true;
                HyperLink21.Visible = true;

                HyperLink5.Visible = true;

                lnk_Truckchalan_fps.Visible = true;

                spsn_fps.Visible = true;

                span5.Visible = true;

                span1.Visible = true;

                span7.Visible = true;
                span9.Visible = true;
                span6.Visible = true;

                span2.Visible = true;


            }

            string distid = Session["dist_idH"].ToString().Trim();
            string tx_hashedPasswordAndSalt =CreatePasswordHash(Session["dist_id"].ToString().Trim().ToLower()).ToLower();
            string tx_hashedPasswordAndSalt1 =CreatePasswordHash(Session["salt"].ToString().Trim() + tx_hashedPasswordAndSalt.Trim()).ToLower();
            version = Session["hindi"].ToString();
            if (distid != tx_hashedPasswordAndSalt1)
            {
                Response.Redirect("~/MainLogin.aspx");
            } 

            string dname = Session["issue_name"].ToString();

            //lblissutitle.Visible = true;
            //lblissutitle.Text = "Issue Center";
            Label1.Text = dname;
            Label2.Text = Session["OperatorId"].ToString ();

           
            if (!IsPostBack)
            {
                if (version == "H")
                {
                    spsn_fps.InnerText = Resources.Hindi_New.spsn_fps;
                   
                    lnk_Truckchalan_fps.Text = Resources.Hindi_New.lnk_Truckchalan_fps;
                    HyperLink1.Text = Resources.LocalizedText.HyperLink1;
                    HyperLink2.Text = Resources.LocalizedText.HyperLink2;
                    HyperLink4.Text = Resources.LocalizedText.HyperLink4;
                    //HyperLink3.Text = Resources.LocalizedText.HyperLink3;
                    //HyperLink11.Text = Resources.LocalizedText.HyperLink11;
                    //HyperLink12.Text = Resources.LocalizedText.HyperLink12;
                    HyperLink13.Text = Resources.LocalizedText.HyperLink13;
                   // HyperLink14.Text = Resources.LocalizedText.HyperLink14;
                   // HyperLink16.Text = Resources.LocalizedText.HyperLink16;
                    HyperLink6.Text = Resources.LocalizedText.HyperLink6;
                    HyperLink5.Text = Resources.LocalizedText.HyperLink5;
                    HyperLink9.Text = Resources.LocalizedText.HyperLink9;
                    HyperLink10.Text = Resources.LocalizedText.HyperLink10;
                    HyperLink15.Text = Resources.LocalizedText.HyperLink15;
                  
                    //HyperLink20.Text = Resources.LocalizedText.HyperLink20;
                   // HyperLink8.Text = Resources.LocalizedText.HyperLink8;
                    //HyperLink22.Text = Resources.LocalizedText.HyperLink22;
                    HyperLink21.Text = Resources.LocalizedText.HyperLink21;
                    span1.InnerText = Resources.LocalizedText.span1;
                    span2.InnerText = Resources.LocalizedText.span2;
                    //span3.InnerText = Resources.LocalizedText.span3;
                   
                    span5.InnerText = Resources.LocalizedText.span5;
                    span6.InnerText = Resources.LocalizedText.span6;
                    span7.InnerText = Resources.LocalizedText.span7;
                    span8.InnerText = Resources.LocalizedText.span8;
                    lblwelcome.Text = Resources.LocalizedText.lblwelcome;
                    lblissuecenter.Text = Resources.LocalizedText.lblissuecenter;
                    LinkButton1.Text = Resources.LocalizedText.LinkButton1;

                }

            }
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
