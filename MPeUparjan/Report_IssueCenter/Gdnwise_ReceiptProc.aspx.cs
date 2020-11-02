using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Security.Principal;
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;

public partial class Report_IssueCenter_Gdnwise_ReceiptProc : System.Web.UI.Page
{
    public SqlConnection cons = new SqlConnection(ConfigurationManager.ConnectionStrings["connstorage"].ToString());

    string distid;
    string issuecenter;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            issuecenter = Session["issue_id"].ToString();

            distid = Session["dist_id"].ToString();

            if (!IsPostBack)
            {
                Getgon();

                tx_date.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
                tx_todate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");


            }
            tx_date.Attributes.Add("onkeypress", "return CheckCalDate(this)");

            tx_todate.Attributes.Add("onkeypress", "return CheckCalDate(this)");

        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (tx_date.Text == "" || tx_todate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Date'); </script> ");
            return;
        }

        if (ddlgodown.SelectedValue == "0" || ddlgodown.SelectedItem.Text == "--select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Pls Select Godown'); </script> ");
            return;
        }

        string fdate = getDate_MDY(tx_date.Text.ToString());

        string todate = getDate_MDY(tx_todate.Text.ToString());

        string IssueId = Session["issue_id"].ToString();

        string DistrictId = Session["dist_id"].ToString();

        string godown = ddlgodown.SelectedValue;

        getData(fdate, todate, DistrictId, IssueId, godown);
    }

    protected String getDate_MDY(string inDate)
    {
        string dat = "";
        if (inDate != "")
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            dat = (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
        }
        return dat;
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName;

        if (this.ReportViewer1.ServerReport.IsDrillthroughReport)
        {
            this.ReportViewer1.PerformBack();
        }
        else
        {
            Response.Redirect("~/IssueCenter/issue_welcome.aspx");
        }
    }

    private void getData(string fdate, string todate, string DistrictId, string IssueId, string godown)
    {
        while (this.ReportViewer1.ServerReport.IsDrillthroughReport)
        {
            this.ReportViewer1.PerformBack();
        }


        string folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName;

        string reportURL = "";

        reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName;
        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        ReportViewer1.ServerReport.ReportServerUrl = new Uri(reportURL);

        ReportViewer1.ServerReport.ReportPath = folder + "IC_Gdnwise_ReceiptProc";
        ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
        ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
        System.Collections.Generic.List<ReportParameter> paramList = new System.Collections.Generic.List<ReportParameter>();

        paramList.Add(new ReportParameter("dist", DistrictId, false));

        paramList.Add(new ReportParameter("issuecenter", IssueId, false));

        paramList.Add(new ReportParameter("frmdate", fdate, false));

        paramList.Add(new ReportParameter("todate", todate, false));

        paramList.Add(new ReportParameter("godown", godown, false));

        ReportViewer1.ServerReport.SetParameters(paramList);
        pInfo = ReportViewer1.ServerReport.GetParameters();
        ReportViewer1.ServerReport.Refresh();

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

    private void Getgon()
    {
        try
        {
            
                if (cons.State == ConnectionState.Closed)
                {
                    cons.Open();
                }
                string qrysel = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where DistrictId = '23" + distid + "'and Remarks = 'Y' ";
                SqlDataAdapter da = new SqlDataAdapter(qrysel, cons);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlgodown.DataSource = ds.Tables[0];
                        ddlgodown.DataTextField = "Godown_Name";
                        ddlgodown.DataValueField = "Godown_ID";
                        ddlgodown.DataBind();
                        ddlgodown.Items.Insert(0, "--select--");
                    }
                }

            
        }

        catch (Exception)
        {

            cons.Close();
        }
        finally
        {
            cons.Close();
        }

    }
}
