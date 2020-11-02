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

public partial class Reports_State_State_NewCompiled : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {
            if (!IsPostBack)
            {
                bindscheme();
               bindCommodity();

                txtDateTill.Text = DateTime.Now.ToString("dd-MM-yyyy");
            }
            txtDate.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            txtDateTill.Attributes.Add("onkeypress", "return CheckCalDate(this)");

            
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        if (ddlCommodity.SelectedItem.Text == "--चुनें--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Commodity'); </script> ");
            return;
        }


        if (txtDate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select From Date'); </script> ");
            return;
        }

        if (txtDateTill.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select To Date'); </script> ");
            return;
        }

        if (ddlscheme.SelectedItem.Text == "सभी स्कीम")
        {
            # region withOut_SchemeId

            string CommodityId = ddlCommodity.SelectedValue.ToString();


            string fdate = getDate_MDY(txtDate.Text.ToString());
            string tdate = getDate_MDY(txtDateTill.Text.ToString());
            get_Datacust_without(fdate, tdate, CommodityId);

            # endregion
        }

        else
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('We are unable to deliver Scheme wise report, Kindly select All Scheme'); </script> ");
            return;

        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
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
            string user = Session["st_id"].ToString();

            if (user == "4")
            {
                Response.Redirect("~/State/mdReports_State.aspx");
            }

            else
            {
                Response.Redirect("~/State/frmReports_State.aspx");
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

    protected void bindscheme()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        try
        {
            string query = "SELECT Scheme_Id , Scheme_Name FROM tbl_MetaData_SCHEME where Status = 'Y'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            ddlscheme.DataSource = ds.Tables[0];
            ddlscheme.DataTextField = "Scheme_Name";
            ddlscheme.DataValueField = "Scheme_Id";
            ddlscheme.DataBind();
            ddlscheme.Items.Insert(0, "सभी स्कीम");
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

    protected void bindCommodity()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        try
        {
            string query = "SELECT Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY where Status = 'Y'";
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


    private void get_Datacust_without(string fdate, string tdate, string Commodity_Id)
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

        ReportViewer1.ServerReport.ReportPath = folder + "rpt_compiled_newopening";
        ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
        ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
        System.Collections.Generic.List<ReportParameter> paramList = new System.Collections.Generic.List<ReportParameter>();

        paramList.Add(new ReportParameter("Commodity_Id", Commodity_Id, false));

        paramList.Add(new ReportParameter("frmdate", fdate, false));
        paramList.Add(new ReportParameter("todate", tdate, false));

        ReportViewer1.ServerReport.SetParameters(paramList);
        pInfo = ReportViewer1.ServerReport.GetParameters();
        ReportViewer1.ServerReport.Refresh();

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

}
