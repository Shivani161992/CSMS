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
using Microsoft.Reporting.WebForms;
using System.Text;
using System.Security.Principal;


public partial class IssueCenterLevel_Storage_ReportViewer_Region : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
      //  string url = Session["reporturl"].ToString();

        if (!IsPostBack)
        {
            try
            {

                String Language;
                string folder = "";
       
                    Language = "2";

                //string path = Request.Url.ToString();
                //int index = path.IndexOf(":") + 3;
                //string path2 = path.Substring(index);
                //int index2 = path2.IndexOf("/");
                //int index3 = path2.IndexOf("/", index2 + 1);
                string servername = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();

                ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(servername.ToString()); // Report Server URL

                ReportViewer_Region.Visible = true;


                string uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
                string pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
                string domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();
              


                //for state reports
               // if (url == "rptGodownwiseStackPosition_district")
              //  {

                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder +  "rptGodownPosition_district";
                    //ReportViewer_Region.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;


                    ReportParameter[] reportParameterCollection = new ReportParameter[2];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "DistrictId";
                    reportParameterCollection[0].Values.Add(District.Text);
                   
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "Language";
                    reportParameterCollection[1].Values.Add(Language.ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Region.ServerReport.Refresh();
               // }
             

            }

            catch (Exception ex)
            {
                StringBuilder str = new StringBuilder();
                str.Append("<script>");
                str.Append("alert('" + "Some error has occurred , try again!" + "');</script>");
                this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
            }
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
                //string domain_warehouseName="VALUED-RDPRSG34\\SQL2008";
                string userName = ConfigurationManager.ConnectionStrings["uname"].ProviderName;
                string password = ConfigurationManager.ConnectionStrings["psw"].ProviderName;
                string domain_warehouseName = ConfigurationManager.ConnectionStrings["domain"].ProviderName;
                return new System.Net.NetworkCredential(userName, password, domain_warehouseName);
            }
        }

        #endregion

    }
    
}
