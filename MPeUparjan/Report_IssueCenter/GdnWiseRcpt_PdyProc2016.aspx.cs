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

public partial class Report_IssueCenter_GdnWiseRcpt_PdyProc2016 : System.Web.UI.Page
{
    SqlConnection con, con_WH;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    public string user, DistName;

    string Con_CSMS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string Con_WH = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            if (!IsPostBack)
            {
                GetCommodity();
                GetICName();
                GetBranch();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    private void GetCommodity()
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();
                string select = "";
                select = "SELECT  Commodity_Id ,Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY where Commodity_Id in ('13','14','8','11','12','40')";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlComdty.DataSource = ds.Tables[0];
                    ddlComdty.DataTextField = "Commodity_Name";
                    ddlComdty.DataValueField = "Commodity_Id";
                    ddlComdty.DataBind();
                    ddlComdty.Items.Insert(0, "--Select--");
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                return;
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

    private void GetICName()
    {
        hdfICName.Value = hdfDistName.Value = "";
        string IC_Id = Session["issue_id"].ToString();
        string DistrictId = Session["dist_id"].ToString();

        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();
                string select = "select DepotName,(Select district_name From pds.districtsmp where district_code='" + DistrictId + "' ) As DistName from dbo.tbl_MetaData_DEPOT where DepotID='" + IC_Id + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    hdfICName.Value = ds.Tables[0].Rows[0]["DepotName"].ToString();
                    hdfDistName.Value = ds.Tables[0].Rows[0]["DistName"].ToString();
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                return;
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

    private void GetBranch()
    {
        string districtid = Session["dist_id"].ToString();
        string IC_Id = Session["issue_id"].ToString();

        using (con_WH = new SqlConnection(Con_WH))
        {
            try
            {
                con_WH.Open();
                string select = string.Format("select tbl_MetaData_DEPOT.DepotName,tbl_MetaData_DEPOT.BranchID from MetaDataBranchWithIssueCenter inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.BranchId=MetaDataBranchWithIssueCenter.BranchID where IssueCenterId='{0}'", IC_Id);
                da = new SqlDataAdapter(select, con_WH);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlBranch.DataSource = ds.Tables[0];
                        ddlBranch.DataTextField = "DepotName";
                        ddlBranch.DataValueField = "BranchID";
                        ddlBranch.DataBind();
                        ddlBranch.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Issue Center'); </script> ");
                    }
                }
                else
                {
                    string select1 = string.Format("select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId= '23" + districtid + "' order by DepotName");
                    da = new SqlDataAdapter(select1, con_WH);

                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlBranch.DataSource = ds.Tables[0];
                        ddlBranch.DataTextField = "DepotName";
                        ddlBranch.DataValueField = "BranchId";
                        ddlBranch.DataBind();
                        ddlBranch.Items.Insert(0, "--Select--");
                    }
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con_WH.State != ConnectionState.Closed)
                {
                    con_WH.Close();
                }
            }
        }
    }


    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlGodam.Items.Clear();
        hdfCMRGodown.Value = "";

        if (ddlComdty.SelectedIndex > 0)
        {
            if (ddlBranch.SelectedIndex > 0)
            {
                GetOnlyGodown();

                if (hdfCMRGodown.Value != "")
                {
                    GetGodown();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपने जिस ब्रांच का Selection किया है, उस ब्रांच के किसी भी गोदाम में " + ddlComdty.SelectedItem.ToString() + " जमा नहीं हुआ है, इसलिए गोदाम का नाम उपलब्ध नहीं है|'); </script> ");
                    return;
                }
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Branch'); </script> ");
            }
        }
        else
        {
            ddlBranch.SelectedIndex = 0;
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Commodity'); </script> ");
            return;
        }

    }

    public void GetOnlyGodown()
    {
        string districtid = Session["dist_id"].ToString();
        string IC_Id = Session["issue_id"].ToString();
        hdfCMRGodown.Value = "";

        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();

                string select = "Select distinct Recd_Godown From SCSC_Procurement_Kharif2016 where Distt_ID='" + districtid + "' and IssueCenter_ID='" + IC_Id + "' and Commodity_Id='" + ddlComdty.SelectedValue.ToString() + "' and AN_Status='Y' and Book_No!='Rejected' and  Branch_Id='" + ddlBranch.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        hdfCMRGodown.Value += ((hdfCMRGodown.Value == "") ? "" : " , ") + "'" + ds.Tables[0].Rows[i]["Recd_Godown"].ToString() + "'";
                    }
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

    public void GetGodown()
    {
        using (con_WH = new SqlConnection(Con_WH))
        {
            try
            {
                con_WH.Open();
                // string select = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where BranchID='" + ddlBranch.SelectedValue.ToString() + "' order by Godown_Name";
                string select = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where Godown_ID IN (" + hdfCMRGodown.Value + ") order by Godown_Name";
                da = new SqlDataAdapter(select, con_WH);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlGodam.DataSource = ds.Tables[0];
                        ddlGodam.DataTextField = "Godown_Name";
                        ddlGodam.DataValueField = "Godown_ID";
                        ddlGodam.DataBind();
                        ddlGodam.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Branch'); </script> ");
                    }
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con_WH.State != ConnectionState.Closed)
                {
                    con_WH.Close();
                }
            }
        }
    }

    protected void ddlComdty_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdfCMRGodown.Value = "";

        if (ddlComdty.SelectedIndex > 0)
        {
            ddlBranch.SelectedIndex = 0;
            ddlGodam.Items.Clear();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Commodity'); </script> ");
            return;
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        string IssueId = Session["issue_id"].ToString();
        string DistrictId = Session["dist_id"].ToString();

        if (ddlComdty.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Commodity'); </script> ");
            return;
        }
        else
        {
            getData(ddlComdty.SelectedValue.ToString(), DistrictId, IssueId, hdfDistName.Value, hdfICName.Value, ddlComdty.SelectedItem.ToString(),ddlGodam.SelectedValue.ToString(),ddlGodam.SelectedItem.ToString());
        }
    }

    private void getData(string Comdty, string DistrictId, string IssueId, string DistName, string ICName, string CmodtyName, string GodownCode, string GodownName)
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

        ReportViewer1.ServerReport.ReportPath = folder + "GdnWiseRcpt_PdyProc2016";
        ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
        ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
        System.Collections.Generic.List<ReportParameter> paramList = new System.Collections.Generic.List<ReportParameter>();

        paramList.Add(new ReportParameter("comdty", Comdty, false));
        paramList.Add(new ReportParameter("Distt_ID", DistrictId, false));
        paramList.Add(new ReportParameter("IssueCenter_ID", IssueId, false));
        paramList.Add(new ReportParameter("DistName", DistName, false));
        paramList.Add(new ReportParameter("ICName", ICName, false));
        paramList.Add(new ReportParameter("CmodtyName", CmodtyName, false));
        paramList.Add(new ReportParameter("GodownCode", GodownCode, false));
        paramList.Add(new ReportParameter("GodownName", GodownName, false));


        ReportViewer1.ServerReport.SetParameters(paramList);
        pInfo = ReportViewer1.ServerReport.GetParameters();
        ReportViewer1.ServerReport.Refresh();

    }

    protected void lnkbtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Report_IssueCenter/PaddyProcHome2016_RptIC.aspx");
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