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


public partial class Report_IssueCenter_rpt_Societywisestorage : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_WPMS = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2016"].ToString());

    // By A public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_PPMS2015_16"].ToString());
    // By A public SqlConnection con_Maze = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_MPMS2015_16"].ToString());

    public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.ConnectionStrings["Appconstr_PPMS2015_16"].ToString());
    public SqlConnection con_Maze = new SqlConnection(ConfigurationManager.ConnectionStrings["Appconstr_MPMS2015_16"].ToString());
    
     string dist;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            dist = Session["dist_id"].ToString();


            if (!IsPostBack)
            {
                GetCommodity();
                               
                txtDate.Text = DateTime.Today.Date.ToString("dd-MM-yyyy");

                txtDateTill.Text = DateTime.Today.Date.ToString("dd-MM-yyyy");

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
        if (Session["issue_id"] != null)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
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

            if (ddlsociety.SelectedValue == "0" || ddlsociety.SelectedItem.Text == "--Select--")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Society'); </script> ");
                return;
            }
            

            string fdate = getDate_MDY(txtDate.Text.ToString());

            string tdate = getDate_MDY(txtDateTill.Text.ToString());

            string soc = ddlsociety.SelectedValue;

            string Issuecenter = Session["issue_id"].ToString();

            string Comid = ddlcomdty.SelectedValue;

            
            getData(fdate, tdate, dist, soc, Issuecenter, Comid);
        }
    }

    private void getData(string fdate, string tdate, string dist, string society, string Issuecenter, string Comid)
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

        ReportViewer1.ServerReport.ReportPath = folder + "StoragewiseDepositReport";
        ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
        ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
        System.Collections.Generic.List<ReportParameter> paramList = new System.Collections.Generic.List<ReportParameter>();


        //
        paramList.Add(new ReportParameter("IssueCenter_ID", Issuecenter, false));
        paramList.Add(new ReportParameter("Purchase_Center", society, false));
        paramList.Add(new ReportParameter("District_id", dist, false));
        paramList.Add(new ReportParameter("fromDate", fdate, false));
        paramList.Add(new ReportParameter("toDate", tdate, false));
        paramList.Add(new ReportParameter("Commodity_Id", Comid, false));

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

    private void Getsociety(string distid)
    {
       
        if (ddlcomdty.SelectedValue == "22")  // Wheat
        {
            if (con_WPMS.State == ConnectionState.Closed)
            {
                con_WPMS.Open();
            }
            string soc = "SELECT Society_Id ,Society_Name + ',' + SocPlace as Society_Name FROM Society where DistrictId = '23" + distid + "' and IsWheat = 'Y'";
            SqlCommand cmd = new SqlCommand(soc, con_WPMS);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlsociety.DataSource = ds.Tables[0];
                ddlsociety.DataTextField = "Society_Name";
                ddlsociety.DataValueField = "Society_Id";
                ddlsociety.DataBind();
                ddlsociety.Items.Insert(0, "--Select--");
            }

            if (con_WPMS.State == ConnectionState.Open)
            {
                con_WPMS.Close(); ;
            }
        }

        if (ddlcomdty.SelectedValue == "13" || ddlcomdty.SelectedValue == "14")  // Paddy
        {

            if (con_paddy.State == ConnectionState.Closed)
            {
                con_paddy.Open();
            }

            string soc = "SELECT Society_Id ,Society_Name + ',' + SocPlace as Society_Name FROM Society where DistrictId = '23" + distid + "' and IsPaddy = 'Y'";
            SqlCommand cmd = new SqlCommand(soc, con_paddy);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlsociety.DataSource = ds.Tables[0];
                ddlsociety.DataTextField = "Society_Name";
                ddlsociety.DataValueField = "Society_Id";
                ddlsociety.DataBind();
                ddlsociety.Items.Insert(0, "--Select--");
            }
            if (con_paddy.State == ConnectionState.Open)
            {
                con_paddy.Close(); ;
            }

        }


        if (ddlcomdty.SelectedValue == "8" || ddlcomdty.SelectedValue == "11" || ddlcomdty.SelectedValue == "12" || ddlcomdty.SelectedValue == "40")
        {
            if (con_Maze.State == ConnectionState.Closed)
            {
                con_Maze.Open();
            }


            string soc = "SELECT Society_Id ,Society_Name + ',' + SocPlace as Society_Name FROM Society where DistrictId = '23" + distid + "' and IsMaize = 'Y'";
            SqlCommand cmd = new SqlCommand(soc, con_Maze);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlsociety.DataSource = ds.Tables[0];
                ddlsociety.DataTextField = "Society_Name";
                ddlsociety.DataValueField = "Society_Id";
                ddlsociety.DataBind();
                ddlsociety.Items.Insert(0, "--Select--");
            }

            if (con_Maze.State == ConnectionState.Open)
            {
                con_Maze.Close(); ;
            }
        }

       
    }

    void GetCommodity()
    {

        try
        {
            if (con != null)
            {

                if (con.State == ConnectionState.Closed)
                {

                    con.Open();   
                }

                string qrysel = "SELECT Commodity_Id  ,Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY where Commodity_Id in ('22','13','14','8','12','11','40') order by Commodity_Name desc";

                    SqlDataAdapter da = new SqlDataAdapter(qrysel, con);   
                    /// 
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddlcomdty.DataSource = ds.Tables[0];
                            ddlcomdty.DataTextField = "Commodity_Name";
                            ddlcomdty.DataValueField = "Commodity_Id";
                            ddlcomdty.DataBind();
                            ddlcomdty.Items.Insert(0, "--Select--");

                        }
                    }
            }
            else
            {
            }
        }

        catch (Exception)
        {

            if (con.State == ConnectionState.Open)
            {

                con.Close();   
            }
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {

                con.Close();   
            }
        }
    }

    protected void ddlcomdty_SelectedIndexChanged(object sender, EventArgs e)
    {
        Getsociety(dist);
    }
}