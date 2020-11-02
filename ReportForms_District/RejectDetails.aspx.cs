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

public partial class ReportForms_District_RejectDetails : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                bindCommodity();
            }

        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    protected void bindCommodity()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        try
        {
            string query = "SELECT Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY where Commodity_Id in ('22','13','14') order by Commodity_Id desc";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            ddlCommodity.DataSource = ds.Tables[0];
            ddlCommodity.DataTextField = "Commodity_Name";
            ddlCommodity.DataValueField = "Commodity_Id";
            ddlCommodity.DataBind();
            ddlCommodity.Items.Insert(0, "--चुनें--");
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

    protected void Button1_Click(object sender, EventArgs e)
    {


        if (ddlCommodity.SelectedItem.Text == "--चुनें--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Commodity'); </script> ");
            return;
        }

        string CommodityId = ddlCommodity.SelectedValue.ToString();

        string DistID = Session["dist_id"].ToString();

        get_Datacust_without(CommodityId, DistID);

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
            if (Session["Collector/DIO"] == "Collector" || Session["Collector/DIO"] == "DIO")
            {
                Response.Redirect("~/District/frmReport_Coll_DIO.aspx");
            }

            else if (Session["DistrictManager"] == "DM" || Session["OperatorIDDM"] == "DM" || Session["Collector/DIO"] == "DMMPSCSC" || Session["DistrictManager"] == "OP")
            {
                Response.Redirect("~/District/Dist_Welcome.aspx");
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
                //string domainName="VALUED-RDPRSG34\\SQL2008";
                string userName = ConfigurationManager.ConnectionStrings["uname"].ProviderName;
                string password = ConfigurationManager.ConnectionStrings["psw"].ProviderName;
                string domainName = ConfigurationManager.ConnectionStrings["domain"].ProviderName;
                return new System.Net.NetworkCredential(userName, password, domainName);
            }
        }

        #endregion
    }

    private void get_Datacust_without(string Commodity_Id, string DistID)
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

        if (ddlrejtype.SelectedItem.Text == "पूर्ण")
        {
            ReportViewer1.ServerReport.ReportPath = folder + "Dist_FullReject";
            ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
            ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
            System.Collections.Generic.List<ReportParameter> paramList = new System.Collections.Generic.List<ReportParameter>();

            paramList.Add(new ReportParameter("CommodityId", Commodity_Id, false));

            paramList.Add(new ReportParameter("DistID", DistID, false));

            ReportViewer1.ServerReport.SetParameters(paramList);
            pInfo = ReportViewer1.ServerReport.GetParameters();
            ReportViewer1.ServerReport.Refresh();
        }

        else
        {
            ReportViewer1.ServerReport.ReportPath = folder + "Dist_Partialreject";
            ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
            ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
            System.Collections.Generic.List<ReportParameter> paramList = new System.Collections.Generic.List<ReportParameter>();

            paramList.Add(new ReportParameter("CommodityId", Commodity_Id, false));
            
            paramList.Add(new ReportParameter("DistID", DistID, false));

            ReportViewer1.ServerReport.SetParameters(paramList);
            pInfo = ReportViewer1.ServerReport.GetParameters();
            ReportViewer1.ServerReport.Refresh();
        }
    }
}
