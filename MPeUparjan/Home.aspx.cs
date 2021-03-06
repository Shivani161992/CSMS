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

public partial class MPeUparjan_Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void lnkPaddy_OnClick(object o, EventArgs e)
    {
      
        Response.Redirect("~/PPMS2013/MainPage.aspx");
    }
    protected void lnkMaize_OnClick(object o, EventArgs e)
    {
        Session["App"] = "MPMS2013";
        Response.Redirect("~/MPMS2013/MainPage.aspx");
    }
    protected void lnkDhanKhareedee_Click(object sender, EventArgs e)
    {
        Session["App"] = "PPMS2013";
        Response.Redirect("~/PPMS2013/MainPage.aspx");
    }
    protected void lnkMakkaKhareedee_Click(object sender, EventArgs e)
    {
        Session["App"] = "MPMS";
        Response.Redirect("~/MPMS/MainPage.aspx");
    }

    protected void lnkGenhuKhareedee_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WPMS2013/MainPage.aspx");
       
    }
    protected void lnk_paddy_2012_Click(object sender, EventArgs e)
    {
        Session["App"] = "PPMS2012";
        Response.Redirect("~/PPMS/MainPage.aspx");
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WPMS/MainPage.aspx");
    }

    protected void lnkWheat_Click(object sender, EventArgs e)
    {
        Session["App"] = "WPMS2014";
        Response.Redirect("~/WPMS2014/MainPage.aspx");
    }
    protected void Lnk_Wht_2012_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WPMS/MainPage.aspx");
    }

    protected void lnk_Kisan_Panjiyan_Click(object sender, EventArgs e)
    {
        Session["User"] = "Otheruser";
        Session["UserName"] = "PurchaseCenter";
        Session["UserNameHINDI"] = "उपार्जन केन्द्र";
        Response.Redirect("~/PPMS2013/Login1.aspx");
    }
    protected void lnk_Makka_2013_Click(object sender, EventArgs e)
    {
        Session["App"] = "MPMS2013";
        Response.Redirect("~/MPMS2013/MainPage.aspx");
    }
    protected void lnk_Wheat2014_Click(object sender, EventArgs e)
    {
        Session["App"] = "WPMS2014";
        Response.Redirect("~/WPMS2014/MainPage.aspx");
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Session["App"] = "WPMS2015_TEST";
        Response.Redirect("~/WPMS2015_TEST/MainPage.aspx");
    }
    protected void lnkDhanKhareedee1_Click(object sender, EventArgs e)
    {
        Session["App"] = "PPMS2014";
        Response.Redirect("~/PPMS2014/MainPage.aspx");
    }
    protected void lnkDhanKhareedeet_Click(object sender, EventArgs e)
    {
        //Session["App"] = "PPMS2014";
        Response.Redirect("~/test_PPMS/MainPage.aspx");
    }
    protected void lnk_Makka_2014_Click(object sender, EventArgs e)
    {
        Session["App"] = "MPMS2014";
        Response.Redirect("~/MPMS2014/MainPage.aspx");
    }
    protected void lnk_Wheat2015_Click(object sender, EventArgs e)
    {
        Session["App"] = "WPMS2015";
        Response.Redirect("~/WPMS2015/MainPage.aspx");
    }
}
