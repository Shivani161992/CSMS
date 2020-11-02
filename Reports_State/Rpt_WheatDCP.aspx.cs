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
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Security.Principal;

public partial class Admin_HOMPSCSC_Rpt_WheatDCP : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            binddivision();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedIndex != 0)
        {
            if (ddlMonth.SelectedValue != "0")
            {
               
                string month = ddlMonth.SelectedItem.Text;
                string div = ddlDivision.SelectedValue.ToString();
                string Year = ddlYear.SelectedItem.Text;

                get_Data(div, month,Year); 
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Month '); </script> ");

               
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Division'); </script> ");
            
        }
    }

    protected void binddivision()
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string query = "SELECT Division_code,Division FROM division";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            ddlDivision.DataSource = ds.Tables[0];
            ddlDivision.DataTextField = "Division";
            ddlDivision.DataValueField = "Division_code";
            ddlDivision.DataBind();
            ddlDivision.Items.Insert(0, "--चुनें--");
        }

        catch
        {

        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }       
    }

    private void get_Data(string div, string Month,string Year)
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

        ReportViewer1.ServerReport.ReportPath = folder + "Rpt_WheatDCP_Divisionwise";
        ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
        ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
        System.Collections.Generic.List<ReportParameter> paramList = new System.Collections.Generic.List<ReportParameter>();

        paramList.Add(new ReportParameter("Year", Year, false));
        paramList.Add(new ReportParameter("Division_code", div, false));
        paramList.Add(new ReportParameter("MONTH", Month, false));

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
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName;

       
            if (this.ReportViewer1.ServerReport.IsDrillthroughReport)
            {
                this.ReportViewer1.PerformBack();
            }
            else
            {
                Response.Redirect("~/State/Ho_FinanceSection.aspx");
            }

    }
}

