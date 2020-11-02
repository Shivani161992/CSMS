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
using System.Security.Principal;
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;

public partial class Reports_State_MovmtOrderStatus_Rpt : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    public string user, DistName;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Region_id"] != null)
        {
            if (!IsPostBack)
            {
                txtToDate.Text = DateTime.Now.ToString("dd-MM-yyyy");

                GetRMDist();

                string Month = DateTime.Now.ToString("MM");
                if (Month == "01" || Month == "02" || Month == "04" || Month == "06" || Month == "09" || Month == "11")
                {
                    txtFromDate.Text = DateTime.Now.AddDays(-31).ToString("dd-MM-yyyy");
                }
                else
                {
                    txtFromDate.Text = DateTime.Now.AddDays(-30).ToString("dd-MM-yyyy");
                }
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    public void GetRMDist()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("SELECT district_name,district_code FROM pds.districtsmp where Division_code='" + Session["Region_id"].ToString() + "' Order By district_name");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlDist.DataSource = ds.Tables[0];
                        ddlDist.DataTextField = "district_name";
                        ddlDist.DataValueField = "district_code";
                        ddlDist.DataBind();
                        ddlDist.Items.Insert(0, "--Select--");
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

    protected void btnReport_Click(object sender, EventArgs e)
    {
        string fromdate = Request.Form[txtFromDate.UniqueID];
        txtFromDate.Text = fromdate;
        string todate = Request.Form[txtToDate.UniqueID];
        txtToDate.Text = todate;

        ConvertServerDate ServerDate = new ConvertServerDate();
        string ConvertFromDate = ServerDate.getDate_MDY(fromdate.ToString()) + " 00:00:00";
        string ConvertToDate = ServerDate.getDate_MDY(todate.ToString()) + " 23:59:59";

        if (ddlComdty.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Commodity'); </script> ");
            return;
        }
        else if (ddlDist.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select District'); </script> ");
            return;
        }
        else
        {
            getData(ddlComdty.SelectedValue.ToString(), ConvertFromDate, ConvertToDate, ddlDist.SelectedValue.ToString());
        }
    }

    private void getData(string Comdty, string FrmDate, string ToDate, string DistCode)
    {
        while (this.ReportViewer1.ServerReport.IsDrillthroughReport)
        {
            this.ReportViewer1.PerformBack();
        }

        pnlreport.Visible = true;
        string folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName;

        string reportURL = "";

        reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName;
        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        ReportViewer1.ServerReport.ReportServerUrl = new Uri(reportURL);

        ReportViewer1.ServerReport.ReportPath = folder + "MovmtOrderStatusRM";
        ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
        ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
        System.Collections.Generic.List<ReportParameter> paramList = new System.Collections.Generic.List<ReportParameter>();

        paramList.Add(new ReportParameter("comdty", Comdty, false));
        paramList.Add(new ReportParameter("FrmDate", FrmDate, false));
        paramList.Add(new ReportParameter("ToDate", ToDate, false));
        paramList.Add(new ReportParameter("DistCode", DistCode, false));

        ReportViewer1.ServerReport.SetParameters(paramList);
        pInfo = ReportViewer1.ServerReport.GetParameters();
        ReportViewer1.ServerReport.Refresh();
    }

    protected void lnkbtnBack_Click(object sender, EventArgs e)
    {
        string folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName;

        if (this.ReportViewer1.ServerReport.IsDrillthroughReport)
        {
            this.ReportViewer1.PerformBack();
        }
        else
        {
            Response.Redirect("~/Regional_Office/frmReports.aspx");
        }
    }

    public sealed class ReportServerNetworkCredentials : IReportServerCredentials
    {
        #region IReportServerCredentials Members
        public bool GetFormsCredentials(out System.Net.Cookie authCookie, out string userName,
        out string password, out string authority)
        {
            authCookie = null;
            userName = null;
            password = null;
            authority = null;
            return false;
        }

        // Specifies the user to impersonate when connecting to a report server. 
        //A WindowsIdentity object representing the user to impersonate.
        public WindowsIdentity ImpersonationUser
        {
            get
            {
                return null;
            }
        }

        // Returns network credentials to be used for authentication with the report server. 
        //A NetworkCredentials object.
        public System.Net.ICredentials NetworkCredentials
        {
            get
            {
                //you can place below settings in configuration xml file
                //string userName = "Administrator";
                //string password = "nic123";
                //string domainName="VALUED-RDPRSG34\\SQL2008";
                string userName = ConfigurationManager.ConnectionStrings["uname"].ProviderName;
                string password = ConfigurationManager.ConnectionStrings["psw"].ProviderName;
                string domainName = ConfigurationManager.ConnectionStrings["domain"].ProviderName;
                return new System.Net.NetworkCredential(userName, password, domainName);
            }
        }

        #endregion
    }


}