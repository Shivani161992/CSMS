using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Millers_Login_Miller_Home : System.Web.UI.Page
{

    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da, da1, daI, daCMR, daSMO;
    DataSet ds, ds1, dsI, dsCMR, dsSMO;
    string SMO;
    string millerDist;

    //string Gdistance = "";
    //string Mdistance = "";


    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS

    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] != null)
        {
            if (!IsPostBack)
            {
                lblMillerName.Text = Session["MillerName"].ToString();

            }
        }
        else
        {
            Response.Redirect("~/Millers_Login/Miller_Login.aspx");
        }

       

    }
}