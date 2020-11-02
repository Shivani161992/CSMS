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
using Microsoft.Reporting.WebForms;
using System.Security.Principal;


public partial class District_FCI_cocStorage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                ddlcropyear.Items.Add(DateTime.Now.Year + "-" + (int.Parse(DateTime.Now.Year.ToString()) + 1).ToString());
                ddlcropyear.Items.Add(DateTime.Now.Year - 1 + "-" + DateTime.Now.Year);

                ddl_month.SelectedIndex = DateTime.Today.Month;
                
            }
        }

        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/FCIoption.aspx");  
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string cropyear = ddlcropyear.SelectedItem.Text;
               
        string dist = Session["dist_id"].ToString();

        string month = ddl_month.SelectedValue.ToString();

        get_Data(cropyear, dist, month);
    }

    private void get_Data(string Crop_Year, string Dist , string Month)
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

        ReportViewer1.ServerReport.ReportPath = folder + "FCI_CarryStorage";

        ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
        ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
        System.Collections.Generic.List<ReportParameter> paramList = new System.Collections.Generic.List<ReportParameter>();

        paramList.Add(new ReportParameter("CropYear", Crop_Year, false));
        
        paramList.Add(new ReportParameter("DistrictId", Dist, false));

        paramList.Add(new ReportParameter("AcceptMonth", Month, false));


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
}
