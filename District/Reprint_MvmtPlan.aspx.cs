using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class District_Reprint_MvmtPlan : System.Web.UI.Page
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

                    string select = string.Format("SELECT distinct MoveOrdernum,SMO FROM RecAgainst_StateMovementOrder where CreatedDate between '" + ConvertFromDate + " 00:00:00' and '" + ConvertToDate + " 23:59:59' and ToDist='" + districtid + "' order by MoveOrdernum");
                    da = new SqlDataAdapter(select, con);
                    ds = new DataSet();
                    da.Fill(ds);

                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            ddlMvmtNumber.DataSource = ds.Tables[0];
                            ddlMvmtNumber.DataTextField = "MoveOrdernum";
                            ddlMvmtNumber.DataValueField = "SMO";
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
            Session["MovmtOrderNo"] = ddlMvmtNumber.SelectedItem.ToString();
            Session["SubMovmtOrderNo"] = ddlMvmtNumber.SelectedValue.ToString();

            string url = "Print_MvmtPlanAgainst_PDSMO.aspx";
            string s = "window.open('" + url + "', 'popup_window');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Movement Order Number'); </script> ");
            Session["MovmtOrderNo"] = Session["SubMovmtOrderNo"] = null;
        }
    }
}