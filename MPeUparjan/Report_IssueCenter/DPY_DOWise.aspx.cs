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
using System.Security.Principal;
using System.Data.SqlClient;

public partial class Report_IssueCenter_DPY_DOWise : System.Web.UI.Page
{
    public string IssueCenterID = "";
    public string DistrictId = "";

    public string DONumber = "";

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {

            IssueCenterID = Session["issue_id"].ToString();

            DistrictId = Session["dist_id"].ToString();

            if (!IsPostBack)
            {
                GetDO();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

   protected void GetDO()
    {
        string qry = "select DoorStep_DO.delivery_order_no from DoorStep_DO where DoorStep_DO.district_code = '"+DistrictId+"' and DoorStep_DO.issueCentre_code = '"+IssueCenterID+"'";
       
       SqlCommand cmd = new SqlCommand(qry,con);

       SqlDataAdapter da = new SqlDataAdapter(cmd);
       DataSet ds = new DataSet();

       da.Fill(ds);

       if (ds.Tables[0].Rows.Count > 0)
       {
           ddlDelivery.DataSource = ds.Tables[0];

           ddlDelivery.DataTextField = "delivery_order_no";
           ddlDelivery.DataValueField = "delivery_order_no";
           ddlDelivery.DataBind();
           ddlDelivery.Items.Insert(0, "--Select--");
       }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        if (ddlDelivery.SelectedValue == "0" || ddlDelivery.SelectedItem.Text == "--Select--")
        {

        }

        else
        {
            DONumber = ddlDelivery.SelectedValue;

            get_Data(IssueCenterID, DistrictId, DONumber);
        }
        
    }


    private void get_Data(string IssueID, string DistrictId , string DoNum)
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

        ReportViewer1.ServerReport.ReportPath = folder + "DPY_Issue_DOWise";
        ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
        ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
        System.Collections.Generic.List<ReportParameter> paramList = new System.Collections.Generic.List<ReportParameter>();

        paramList.Add(new ReportParameter("issueCentre_code", IssueID, false));

        paramList.Add(new ReportParameter("district_code", DistrictId, false));

        paramList.Add(new ReportParameter("delivery_order_no", DoNum, false));

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
            Response.Redirect("~/IssueCenter/issue_welcome.aspx");
        }
    }
}

