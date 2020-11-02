using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
public partial class PaddyMilling_Reprint_PMAgrmt : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da;
    DataSet ds;

    string districtid = "";
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {

                txtFromDate.Text = txtToDate.Text = DateTime.Now.ToString("dd-MM-yyyy"); ;
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }

    public void Search()
    {
        RequiredFieldValidator5.Visible = RequiredFieldValidator6.Visible = false;
        districtid = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string fromdate = Request.Form[txtFromDate.UniqueID];
                txtFromDate.Text = fromdate;
                string todate = Request.Form[txtToDate.UniqueID];
                txtToDate.Text = todate;

                ConvertServerDate ServerDate = new ConvertServerDate();
                string ConvertFromDate = ServerDate.getDate_MDY(fromdate.ToString());
                string ConvertToDate = ServerDate.getDate_MDY(todate.ToString());

                string select = string.Format("SELECT distinct Agreement_ID FROM PaddyMilling_Agreement where Current_DateTime between '" + ConvertFromDate + " 00:00:00' and '" + ConvertToDate + " 23:59:59' and District='" + districtid + "' and IsAccepted='Y'");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlMvmtNumber.DataSource = ds.Tables[0];
                        ddlMvmtNumber.DataTextField = "Agreement_ID";
                        ddlMvmtNumber.DataValueField = "Agreement_ID";
                        ddlMvmtNumber.DataBind();
                        ddlMvmtNumber.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Agreement Number Is Not Available From These Date'); </script> ");
                        ddlMvmtNumber.DataSource = "";
                        ddlMvmtNumber.DataBind();
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

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (ddlMvmtNumber.SelectedIndex > 0)
        {
            Session["AgrmtNo"] = ddlMvmtNumber.SelectedItem.ToString();

            string url = "Print/PMillingAgreement.aspx";
            string s = "window.open('" + url + "', 'popup_window');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Agreement Number'); </script> ");
            Session["AgrmtNo"] = null;
        }
    }

}