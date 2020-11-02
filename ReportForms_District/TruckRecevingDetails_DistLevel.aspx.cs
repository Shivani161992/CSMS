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

public partial class ReportForms_District_TruckRecevingDetails_DistLevel : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                get_commodity_name();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

    }

    protected void get_commodity_name()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        string query = "SELECT [Commodity_Id],[Commodity_Name],[Status] FROM [dbo].[tbl_MetaData_STORAGE_COMMODITY] where Status='Y' and Commodity_Id in ('13','14','8','11','12','40','22')";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
        ddlcrop.DataSource = ds.Tables[0];
        ddlcrop.DataTextField = "Commodity_Name";
        ddlcrop.DataValueField = "Commodity_Id";
        ddlcrop.DataBind();
        ddlcrop.Items.Insert(0, "--Select--");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (ddlcrop.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Commodity'); </script> ");
            return;
        }
        else
        {
            string districtid = Session["dist_id"].ToString();

            if (ddlcrop.SelectedValue.ToString() == "22")
            {
                get_Data(districtid);
            }
            else
            {
                get_DataKharif2016(districtid);
            }
        }
    }

    private void get_Data(string dist)
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

        ReportViewer1.ServerReport.ReportPath = folder + "TruckReceived_DistDrilldown";
        ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
        ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
        System.Collections.Generic.List<ReportParameter> paramList = new System.Collections.Generic.List<ReportParameter>();

        paramList.Add(new ReportParameter("district_code", dist, false));

        ReportViewer1.ServerReport.SetParameters(paramList);
        pInfo = ReportViewer1.ServerReport.GetParameters();
        ReportViewer1.ServerReport.Refresh();
    }

    private void get_DataKharif2016(string dist)
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

        ReportViewer1.ServerReport.ReportPath = folder + "TruckReceived_DistKharif2016";
        ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
        ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
        System.Collections.Generic.List<ReportParameter> paramList = new System.Collections.Generic.List<ReportParameter>();

        paramList.Add(new ReportParameter("district_code", dist, false));

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
                string userName = ConfigurationManager.ConnectionStrings["uname"].ProviderName;
                string password = ConfigurationManager.ConnectionStrings["psw"].ProviderName;
                string domainName = ConfigurationManager.ConnectionStrings["domain"].ProviderName;
                return new System.Net.NetworkCredential(userName, password, domainName);
            }
        }

        #endregion

    }

    protected void LinkButton1_Click1(object sender, EventArgs e)
    {
        string folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName;


        if (this.ReportViewer1.ServerReport.IsDrillthroughReport)
        {
            this.ReportViewer1.PerformBack();
        }
        else
        {
            if (Session["Collector/DIO"] == "Collector" || Session["Collector/DIO"] == "DIO")
            {
                Response.Redirect("~/District/frmReport_Coll_DIO.aspx");
            }

            else if (Session["DistrictManager"] == "DM" || Session["OperatorIDDM"] == "DM" || Session["Collector/DIO"] == "DMMPSCSC" || Session["DistrictManager"] == "OP")
            {
                Response.Redirect("~/District/Frm_Procurement_Reports.aspx");
            }
        }
    }

}
