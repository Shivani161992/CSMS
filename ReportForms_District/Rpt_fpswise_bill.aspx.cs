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

public partial class Reports_State_Rpt_csms_distwise_compliedreport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                bindbill();
            }
            //txtDate.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            //txtDateTill.Attributes.Add("onkeypress", "return CheckCalDate(this)");
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (ddlcropyear.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Year'); </script> ");
            return;
        }
        if (ddl_billno.SelectedIndex == 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select bill no.'); </script> ");
            return;
        }
        string monthId = ddl_allot_month.SelectedValue.ToString();
        string Cropyear = ddlcropyear.SelectedItem.Text;
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
 
      
        
            string distid = Session["dist_id"].ToString();
            string billno = ddl_billno.SelectedValue.ToString();
            //string tdate = getDate_MDY(txtDateTill.Text.ToString());
            get_billdata(distid, billno,monthId,Cropyear);

         

     
      
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

    private void get_billdata(string district, string billno, string monthId, string Cropyear)
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

        ReportViewer1.ServerReport.ReportPath = folder + "rpt_FPSwisepayment_detail";
        ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
        ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
        System.Collections.Generic.List<ReportParameter> paramList = new System.Collections.Generic.List<ReportParameter>();
        paramList.Add(new ReportParameter("district", district, false));
        paramList.Add(new ReportParameter("billno", billno, false));
        paramList.Add(new ReportParameter("Allotmentmonth", monthId, false));

        paramList.Add(new ReportParameter("Allotmentyear", Cropyear, false));
     

        ReportViewer1.ServerReport.SetParameters(paramList);
        pInfo = ReportViewer1.ServerReport.GetParameters();
        ReportViewer1.ServerReport.Refresh();

    }
    protected void bindbill()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        try
        {
            string query = "select distinct  cast(Issued_Doorstep_do_fps.allotment_month as nvarchar)+cast(Issued_Doorstep_do_fps.fps_code as nvarchar) as billno,cast(Issued_Doorstep_do_fps.allotment_month as nvarchar)+cast(Issued_Doorstep_do_fps.fps_code as nvarchar)+'('+tbl_rootchart_master.fps_name+')' as billnoname from  Issued_Doorstep_do_fps join tbl_rootchart_master on tbl_rootchart_master.fps_code=Issued_Doorstep_do_fps.fps_code  where Issued_Doorstep_do_fps.fps_code not in(select Link_CooperativeSociety.FPSCode from Link_CooperativeSociety) and district_code='" + Session["dist_id"].ToString() + "' and allotment_month='" + ddl_allot_month.SelectedValue.ToString() + "' and allotment_year='" + ddlcropyear.SelectedValue.ToString() + "' and Transport_Order is not null ";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            ddl_billno.DataSource = ds.Tables[0];
            ddl_billno.DataTextField = "billnoname";
            ddl_billno.DataValueField = "billno";
            ddl_billno.DataBind();
            ddl_billno.Items.Insert(0, "--select--");
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








    protected void ddlcropyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindbill();
    }


}
