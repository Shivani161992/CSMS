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

public partial class MasterPage_SCSC_master : System.Web.UI.MasterPage
{
    public string version = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            version = Session["hindi"].ToString();
            string distid = Session["dist_idH"].ToString().Trim();

            string tx_hashedPasswordAndSalt = CreatePasswordHash(Session["dist_id"].ToString().Trim().ToLower()).ToLower();
            string tx_hashedPasswordAndSalt1 = CreatePasswordHash(Session["salt"].ToString().Trim() + tx_hashedPasswordAndSalt.Trim()).ToLower();

            if (distid != tx_hashedPasswordAndSalt1)
            {
                Response.Redirect("~/MainLogin.aspx");
            }

            string dname = Session["dist_name"].ToString();
            Label1.Text = dname;

            if (Session["Markfed"].ToString() == "Y")
            {
                Image2.ImageUrl = "~/Images/markfed.jpg";
            }
            else
            {
                Image2.ImageUrl = "~/Images/csmsheader.jpg";
            }

            if (Session["Collector/DIO"] != null)
            {
                Label2.Text = Session["Collector/DIO"].ToString();

                if (Session["Collector/DIO"].ToString() == "Collector" || Session["Collector/DIO"].ToString() == "DIO")
                {
                    spandeliveryorder.Visible = false;
                    //span2.Visible = false; hide lifting on dated :-22/11/13
                    //span3.Visible = false; hide on dated :-22/11/13
                    //span4.Visible = false; hide on dated :-22/11/13
                    span5.Visible = false;
                    span8.Visible = false;
                    span9.Visible = false;
                    //DH1.Visible = false; // Release Order(FCI),EditRO and Update RO Validity hide.
                    //DH2.Visible = false;
                    //DH20.Visible = false;
                    //DH26.Visible = false;
                    //DH27.Visible = false;
                    //DH15.Visible = false; // Transport order and edit transport order hide
                    //DH19.Visible = false;
                    //DH4.Visible = false;
                    //DH25.Visible = false;
                    //DH7.Visible = false;
                    //DH18.Visible = false; scheme transfer hide on dated :-22/11/13
                    //DH11.Visible = false; RR Details hide on dated :-22/11/13
                    //DH17.Visible = false;
                    //DH21.Visible = false;
                    //DH16.Visible = false;
                    DH14.Visible = false;
                    DH23.Visible = false;
                    DH24.Visible = false;
                    DH12.Visible = false;
                    DH5.Visible = false;
                    DH14.Visible = false;
                    DH10.Visible = false;
                    DH13.Visible = false;
                    DH3.Visible = false;
                    DH22.Visible = false;
                    //DH8.Visible = false;
                    DH6.Visible = false;

                }

            }
            if (!IsPostBack)
            {
                if (version == "H")
                {
                    //DH1.Text = Resources.LocalizedText.DH1;
                    //DH2.Text = Resources.LocalizedText.DH2;
                    DH3.Text = Resources.LocalizedText.DH3;
                    //DH4.Text = Resources.LocalizedText.DH4;
                    DH5.Text = Resources.LocalizedText.DH5;
                    DH6.Text = Resources.LocalizedText.DH6;
                    span9.InnerText = Resources.LocalizedText.span9;
                    //DH7.Text = Resources.LocalizedText.DH7;
                    span8.InnerText = Resources.LocalizedText.span8;
                    DH10.Text = Resources.LocalizedText.DH10;
                    //DH11.Text = Resources.LocalizedText.DH11;
                    DH12.Text = Resources.LocalizedText.DH12;
                    DH13.Text = Resources.LocalizedText.DH13;
                    span5.InnerText = Resources.LocalizedText.span5;
                    span7.InnerText = Resources.LocalizedText.span7;
                    //hypdelopningbalance.Text = Resources.LocalizedText.hypdelopningbalance;
                    spandeliveryorder.InnerText = Resources.LocalizedText.spandeliveryorder;
                    //DH15.Text = Resources.LocalizedText.DH15;    // Transport order and edit transport order hide
                    //DH16.Text = Resources.LocalizedText.DH16;    // RR Details on dated :-22/11/13
                    //DH17.Text = Resources.LocalizedText.DH17;    // RR Details on dated :-22/11/13
                    //DH18.Text = Resources.LocalizedText.DH18;
                    //DH19.Text = Resources.LocalizedText.DH19;      // Transport order and edit transport order hide
                    //DH20.Text = Resources.LocalizedText.DH20;
                    //DH21.Text = Resources.LocalizedText.DH21;       // RR Details on dated :-22/11/13
                    DH22.Text = Resources.LocalizedText.DH22;
                    DH23.Text = Resources.LocalizedText.DH23;
                    DH24.Text = Resources.LocalizedText.DH24;
                    //DH25.Text = Resources.LocalizedText.DH25;       on dated :-22/11/13
                    //DH26.Text = Resources.LocalizedText.DH26;
                    //DH27.Text = Resources.LocalizedText.DH27;
                    DH14.Text = Resources.LocalizedText.DH14;
                }

            }
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
