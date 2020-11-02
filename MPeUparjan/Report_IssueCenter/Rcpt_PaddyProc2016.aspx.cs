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

public partial class Report_IssueCenter_Rcpt_PaddyProc2016 : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    public string user, DistName;

    string Con_CSMS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            if (!IsPostBack)
            {
                txtToDate.Text = DateTime.Now.ToString("dd-MM-yyyy");

                string Month = DateTime.Now.ToString("MM");
                if (Month == "01" || Month == "02" || Month == "04" || Month == "06" || Month == "09" || Month == "11")
                {
                    txtFromDate.Text = DateTime.Now.AddDays(-31).ToString("dd-MM-yyyy");
                }
                else
                {
                    txtFromDate.Text = DateTime.Now.AddDays(-30).ToString("dd-MM-yyyy");
                }

                GetCommodity();
                GetICName();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    private void GetCommodity()
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();
                string select = "";
                select = "SELECT  Commodity_Id ,Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY where Commodity_Id in ('13','14','8','11','12','40')";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlComdty.DataSource = ds.Tables[0];
                    ddlComdty.DataTextField = "Commodity_Name";
                    ddlComdty.DataValueField = "Commodity_Id";
                    ddlComdty.DataBind();
                    ddlComdty.Items.Insert(0, "--Select--");
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                return;
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

    private void GetICName()
    {
        hdfICName.Value = hdfDistName.Value = "";
        string IC_Id = Session["issue_id"].ToString();
        string DistrictId = Session["dist_id"].ToString();

        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();
                string select = "select DepotName,(Select district_name From pds.districtsmp where district_code='" + DistrictId + "' ) As DistName from dbo.tbl_MetaData_DEPOT where DepotID='" + IC_Id + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    hdfICName.Value = ds.Tables[0].Rows[0]["DepotName"].ToString();
                    hdfDistName.Value = ds.Tables[0].Rows[0]["DistName"].ToString();
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                return;
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
        string IssueId = Session["issue_id"].ToString();
        string DistrictId = Session["dist_id"].ToString();

        string fromdate = Request.Form[txtFromDate.UniqueID];
        txtFromDate.Text = fromdate;
        string todate = Request.Form[txtToDate.UniqueID];
        txtToDate.Text = todate;

        ConvertServerDate ServerDate = new ConvertServerDate();
        string fdate = ServerDate.getDate_MDY(fromdate.ToString());
        string ToDate = ServerDate.getDate_MDY(todate.ToString());

        //string ConvertFromDate = ServerDate.getDate_MDY(fromdate.ToString()) + " 00:00:00";
        //string ConvertToDate = ServerDate.getDate_MDY(todate.ToString()) + " 23:59:59";

        if (ddlComdty.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Commodity'); </script> ");
            return;
        }
        else
        {
            getData(ddlComdty.SelectedValue.ToString(), fdate, DistrictId, IssueId, hdfDistName.Value, hdfICName.Value, ddlComdty.SelectedItem.ToString(), ToDate);
        }

    }

    private void getData(string Comdty, string fdate, string DistrictId, string IssueId, string DistName, string ICName, string CmodtyName, string ToDate)
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

        ReportViewer1.ServerReport.ReportPath = folder + "Rcpt_PaddyProc2016";
        ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
        ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
        System.Collections.Generic.List<ReportParameter> paramList = new System.Collections.Generic.List<ReportParameter>();

        paramList.Add(new ReportParameter("comdty", Comdty, false));
        paramList.Add(new ReportParameter("Distt_ID", DistrictId, false));
        paramList.Add(new ReportParameter("IssueCenter_ID", IssueId, false));
        paramList.Add(new ReportParameter("fdate", fdate, false));
        paramList.Add(new ReportParameter("DistName", DistName, false));
        paramList.Add(new ReportParameter("ICName", ICName, false));
        paramList.Add(new ReportParameter("CmodtyName", CmodtyName, false));
        paramList.Add(new ReportParameter("ToDate", ToDate, false));

        ReportViewer1.ServerReport.SetParameters(paramList);
        pInfo = ReportViewer1.ServerReport.GetParameters();
        ReportViewer1.ServerReport.Refresh();
    }

    protected void lnkbtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Report_IssueCenter/PaddyProcHome2016_RptIC.aspx");
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