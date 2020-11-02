using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class State_Reprint_PDSMovementOrder : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da;
    DataSet ds;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_Name"] != null)
        {
            if (!IsPostBack)
            {
                txtFromDate.Text = txtToDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
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

                string select = string.Format("SELECT distinct MoveOrdernum,(MoveOrdernum + IsAccepted) As Accepted FROM StateMovementOrder where CreatedDate between '" + ConvertFromDate + " 00:00:00' and '" + ConvertToDate + " 23:59:59' and IsCancelled IS NULL order by MoveOrdernum");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlMvmtNumber.DataSource = ds.Tables[0];
                        ddlMvmtNumber.DataTextField = "MoveOrdernum";
                        ddlMvmtNumber.DataValueField = "Accepted";
                        ddlMvmtNumber.DataBind();
                        ddlMvmtNumber.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Movement Order Is Not Available From These Date'); </script> ");
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
            string CheckApproved = "", getlastdigit = "";
            CheckApproved = ddlMvmtNumber.SelectedValue.ToString();
            if (CheckApproved != "")
            {
                getlastdigit = CheckApproved.Substring(CheckApproved.Length - 1, 1);
            }

            if (getlastdigit == "N")
            {
                Session["Acpt/Rjct"] = "Reject".ToString();
            }
            else if (getlastdigit == "" || getlastdigit == "F")
            {
                Session["Acpt/Rjct"] = "Pending".ToString();
            }
            else
            {
                Session["Acpt/Rjct"] = "Approved".ToString();
            }

            if (hdfModeofDispatch.Value != "13")
            {
                Session["MovmtOrderNo"] = ddlMvmtNumber.SelectedItem.ToString();
                string url = "Print_MovementOrder.aspx";
                string s = "window.open('" + url + "', 'popup_window');";
                ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }
            else
            {
                if (hdfSMS_frmDist.Value == 'Y'.ToString())
                {
                    Session["MovmtOrderNo"] = ddlMvmtNumber.SelectedItem.ToString();
                    string url = "Print_SubMovementOrder.aspx";
                    string s = "window.open('" + url + "', 'popup_window');";
                    ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Print Is Not Allow because Your Sub Movement Order Is Not Created'); </script> ");
                    Session["MovmtOrderNo"] = null;
                }
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Movement Order Number'); </script> ");
            Session["MovmtOrderNo"] = null;
        }
    }

    protected void ddlMvmtNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMvmtNumber.SelectedIndex > 0)
        {
            CheckSubMO();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Movement Order Number'); </script> ");
        }
    }

    public void CheckSubMO()
    {
        hdfModeofDispatch.Value = "";
        hdfSMS_frmDist.Value = "";
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = string.Format("Select SMS_frmDist,ModeofDispatch From StateMovementOrder Where MoveOrdernum='" + ddlMvmtNumber.SelectedItem.ToString() + "' and ModeofDispatch='13' and ModeofDist In('Other','Both')");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        hdfModeofDispatch.Value = ds.Tables[0].Rows[0]["ModeofDispatch"].ToString();
                        hdfSMS_frmDist.Value = ds.Tables[0].Rows[0]["SMS_frmDist"].ToString();
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
}