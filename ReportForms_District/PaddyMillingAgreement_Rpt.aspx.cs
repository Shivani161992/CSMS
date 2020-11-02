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

public partial class ReportForms_District_PaddyMillingAgreement_Rpt : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    public string user, DistName;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null || Session["st_id"] != null)
        {
            if (!IsPostBack)
            {
                ddlCropYear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1));
                ddlCropYear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year);

                ddlCropYear.SelectedIndex = 1;

                if (Session["dist_id"] != null)
                {
                    lblDistName.Visible = false;
                    ddlDistName.Visible = false;
                    GetMPDistrict();
                }
                else if (Session["st_id"] != null) //For MD & HO
                {
                    lblDistName.Visible = true;
                    ddlDistName.Visible = true;
                    GetAllMPDistrict();
                }
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    public void GetMPDistrict()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("SELECT district_name FROM pds.districtsmp where district_code='" + Session["dist_id"].ToString() + "' ");
                cmd = new SqlCommand(select, con);
                ViewState["DistName"] = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }

    public void GetAllMPDistrict()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string qry = "SELECT district_code ,district_name FROM pds.districtsmp order by district_name ";
                da = new SqlDataAdapter(qry, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDistName.DataSource = ds.Tables[0];
                    ddlDistName.DataTextField = "district_name";
                    ddlDistName.DataValueField = "district_code";
                    ddlDistName.DataBind();
                    ddlDistName.Items.Insert(0, "--Select--");
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }


    protected void btnReport_Click(object sender, EventArgs e)
    {
        string strCropYear = ddlCropYear.SelectedValue.ToString();

        if (Session["dist_id"] != null)
        {
            DistName = ViewState["DistName"].ToString();
            getData(strCropYear, DistName);
        }

        else if (Session["st_id"] != null) //For MD  & HO
        {
            if (ddlDistName.SelectedIndex > 0)
            {
                ReportViewer1.Visible = true;
                DistName = ddlDistName.SelectedItem.ToString();
                getData(strCropYear, DistName);
            }
            else
            {
                ReportViewer1.Visible = false;
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Dist Name'); </script> ");
            }
        }
    }

    private void getData(string strCropYear, string DistID)
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

        ReportViewer1.ServerReport.ReportPath = folder + "PaddyMillingAgreement_rpt";
        ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
        ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
        System.Collections.Generic.List<ReportParameter> paramList = new System.Collections.Generic.List<ReportParameter>();


        paramList.Add(new ReportParameter("District", DistID, false));
        paramList.Add(new ReportParameter("CropYear", strCropYear, false));

        ReportViewer1.ServerReport.SetParameters(paramList);
        pInfo = ReportViewer1.ServerReport.GetParameters();
        ReportViewer1.ServerReport.Refresh();
    }

    protected void lnkbtnBack_Click(object sender, EventArgs e)
    {
        string folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName;

        if (Session["st_id"] != null)
        {
            user = Session["st_id"].ToString();
        }

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
                Response.Redirect("~/District/frmReports.aspx");
            }
            else if (user == "4")  //For MD
            {
                Response.Redirect("~/State/mdReports_State.aspx");
            }
            else if (user == "1")  //For HO
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


}