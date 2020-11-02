using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class State_PDSMO_SDN : System.Web.UI.Page
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
                txtToDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                txtFromDate.Text = DateTime.Now.AddDays(-30).ToString("dd-MM-yyyy");
                rdbSendDist.Checked = true;

                Session["MovmtOrderNo"] = null;
                Session["RecdDist"] = null;
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

                string select = string.Format("SELECT distinct MoveOrdernum FROM StateMovementOrder where CreatedDate between '" + ConvertFromDate + " 00:00:00' and '" + ConvertToDate + " 23:59:59' and IsAccepted='Y' and ModeofDispatch='12' order by MoveOrdernum");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlMvmtNumber.DataSource = ds.Tables[0];
                        ddlMvmtNumber.DataTextField = "MoveOrdernum";
                        ddlMvmtNumber.DataValueField = "MoveOrdernum";
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
            if (rdbSendDist.Checked)
            {
                Session["MovmtOrderNo"] = ddlMvmtNumber.SelectedItem.ToString();
                string url = "PrintPDSMO_SendSDN.aspx";
                string s = "window.open('" + url + "', 'popup_window');";
                ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }
            else if (rdbRecdDist.Checked)
            {
                if (ddlRecdDist.SelectedIndex > 0)
                {
                    Session["MovmtOrderNo"] = ddlMvmtNumber.SelectedItem.ToString();
                    Session["RecdDist"] = ddlRecdDist.SelectedValue.ToString();
                    string url = "PrintPDSMO_RecdSDN.aspx";
                    string s = "window.open('" + url + "', 'popup_window');";
                    ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Received Dist'); </script> ");
                    Session["MovmtOrderNo"] = null;
                    Session["RecdDist"] = null;
                }
            }

        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select MO Number'); </script> ");
            Session["MovmtOrderNo"] = null;
            Session["RecdDist"] = null;
        }
    }


    protected void rdbSendDist_CheckedChanged(object sender, EventArgs e)
    {
        ddlMvmtNumber.Items.Clear();
        ddlRecdDist.Items.Clear();
    }
    protected void rdbRecdDist_CheckedChanged(object sender, EventArgs e)
    {
        ddlMvmtNumber.Items.Clear();
        ddlRecdDist.Items.Clear();
    }

    protected void ddlMvmtNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlRecdDist.Items.Clear();
        if (rdbRecdDist.Checked)
        {
            using (con = new SqlConnection(strcon))
            {
                try
                {
                    con.Open();

                    string select = string.Format("SELECT MO.ToDist,MP.district_name FROM StateMovementOrder MO left Join pds.districtsmp MP ON(MO.ToDist=MP.district_code) where MO.MoveOrdernum='" + ddlMvmtNumber.SelectedItem.ToString() + "'  and MO.ModeofDispatch='12' order by district_name ");
                    da = new SqlDataAdapter(select, con);
                    ds = new DataSet();
                    da.Fill(ds);

                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            ddlRecdDist.DataSource = ds.Tables[0];
                            ddlRecdDist.DataTextField = "district_name";
                            ddlRecdDist.DataValueField = "ToDist";
                            ddlRecdDist.DataBind();
                            ddlRecdDist.Items.Insert(0, "--Select--");
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
}