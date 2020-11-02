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

public partial class SurveyorLogin_Uparjan_FarmerQualityInspection : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    public string gatepass = "";
    public int getnum;
    SqlDataReader dr;
    string IC_Id = "", Dist_Id = "";

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] != null)
        {
            if (!IsPostBack)
            {
                Session["ICGBQ"] = null;
                GetCommodities();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/SurveyorLogin_Uparjan/Surveyor_Login.aspx");
        }
    }


    public void GetCommodities()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "Select Commodity_Id,Commodity_Name From tbl_MetaData_STORAGE_COMMODITY Where Commodity_Id IN('33','64', '63') order by Commodity_Name";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlcommodity.DataSource = ds.Tables[0];
                    ddlcommodity.DataTextField = "Commodity_Name";
                    ddlcommodity.DataValueField = "Commodity_Id";
                    ddlcommodity.DataBind();
                    ddlcommodity.Items.Insert(0, "--Select--");
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

    protected void ddlfarmercode_SelectedIndexChanged(object sender, EventArgs e)
    {




    }


    protected void ddlqualityinspection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcommodity.SelectedIndex > 0)
        {
            string status = ddlqualityinspection.SelectedItem.Text;
            string stat = ddlqualityinspection.SelectedValue.ToString();

            if (ddlqualityinspection.SelectedValue.ToString() == "NonFAQ")
            {

                if (ddlcommodity.SelectedValue.ToString() == "33")
                {
                    trmasur.Visible = false;
                    trgram.Visible = false;
                    trsarson.Visible = true;
                }
                else if (ddlcommodity.SelectedValue.ToString() == "63")
                {
                    trgram.Visible = true;
                    trmasur.Visible = false;
                    trsarson.Visible = false;

                }
                else if (ddlcommodity.SelectedValue.ToString() == "64")
                {
                    trsarson.Visible = false;
                    trgram.Visible = false;
                    trmasur.Visible = true;
                }
                btnsubmit.Enabled = true;
            }
            else if (ddlqualityinspection.SelectedValue.ToString() == "FAQ")
            {
                if (ddlcommodity.SelectedValue.ToString() == "33")
                {
                    trmasur.Visible = false;
                    trgram.Visible = false;
                    trsarson.Visible = false;
                }
                else if (ddlcommodity.SelectedValue.ToString() == "63")
                {
                    trgram.Visible = false;
                    trmasur.Visible = false;
                    trsarson.Visible = false;

                }
                else if (ddlcommodity.SelectedValue.ToString() == "64")
                {
                    trsarson.Visible = false;
                    trgram.Visible = false;
                    trmasur.Visible = false;
                }
                btnsubmit.Enabled = true;
            }

        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select commodity'); </script> ");
            ddlqualityinspection.ClearSelection();

        }
    }

    protected void ddlcommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcommodity.SelectedIndex > 0)
        {
            ddlqualityinspection.ClearSelection();

            if (ddlqualityinspection.SelectedIndex == 0)
            {
                btnsubmit.Enabled = false;
                trmasur.Visible = false;
                trgram.Visible = false;
                trsarson.Visible = false;

            }

        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select commodity'); </script> ");
            ddlqualityinspection.ClearSelection();

        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {

    }
}