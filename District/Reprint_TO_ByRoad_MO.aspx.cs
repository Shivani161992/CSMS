using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class District_Reprint_TO_ByRoad_MO : System.Web.UI.Page
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
                txtFromDate.Text = txtToDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                Session["MovmtOrderNo"] = Session["SubMovmtOrderNo"] = Session["ToDostCode"] = Session["TransportNumber"] = null;
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ddlTONumber.Items.Clear();
        GetTONumber();
    }

    public void GetTONumber()
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

                string select = string.Format("SELECT distinct TO_No,ToDist FROM TO_AgainstHO_MO where CreatedDate between '" + ConvertFromDate + " 00:00:00' and '" + ConvertToDate + " 23:59:59' and FrmDist='" + districtid + "' and ModeofDispatch='12' order by TO_No");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlTONumber.DataSource = ds.Tables[0];
                        ddlTONumber.DataTextField = "TO_No";
                        ddlTONumber.DataValueField = "ToDist";
                        ddlTONumber.DataBind();
                        ddlTONumber.Items.Insert(0, "--Select--");

                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Transport Order Is Not Available From These Date'); </script> ");
                        ddlTONumber.DataSource = "";
                        ddlTONumber.DataBind();
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
        Session["MovmtOrderNo"] = Session["SubMovmtOrderNo"] = Session["ToDostCode"] = Session["TransportNumber"] = null;

        if (ddlTONumber.SelectedIndex > 0)
        {
            Session["MovmtOrderNo"] = "";
            Session["SubMovmtOrderNo"] = "";
            Session["ToDostCode"] = ddlTONumber.SelectedValue.ToString();
            Session["TransportNumber"] = ddlTONumber.SelectedItem.ToString();

            string url = "Print_TOAgainst_PDSMovmtOrder.aspx";
            string s = "window.open('" + url + "', 'popup_window');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select TO Number'); </script> ");
            Session["MovmtOrderNo"] = Session["SubMovmtOrderNo"] = null;
        }
    }


}