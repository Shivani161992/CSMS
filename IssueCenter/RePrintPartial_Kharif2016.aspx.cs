using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class IssueCenter_RePrintPartial_Kharif2016 : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da;
    DataSet ds;

    string districtid = "";
    string Con_CSMS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string Con_WH = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            if (!IsPostBack)
            {
                txtFromDate.Text = txtToDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                Session["ReceiptID"] = Session["Commodity_Id"] = null;
                GetCommodity();
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

                select = "SELECT Commodity_Name ,Commodity_Id  FROM tbl_MetaData_STORAGE_COMMODITY where  Commodity_Id in ('13','14','8','11','12','40') Order By Commodity_Name";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlCommodity.DataSource = ds.Tables[0];
                    ddlCommodity.DataTextField = "Commodity_Name";
                    ddlCommodity.DataValueField = "Commodity_Id";
                    ddlCommodity.DataBind();
                    ddlCommodity.Items.Insert(0, "--Select--");
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


    protected void ddlCommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtFromDate.Text = Request.Form[txtFromDate.UniqueID];
        txtToDate.Text = Request.Form[txtToDate.UniqueID];

        ddlBranch.Items.Clear();
        ddlGodown.Items.Clear();
        ddlReceiptID.Items.Clear();

        if (ddlCommodity.SelectedIndex > 0)
        {
            GetBranch();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Commodity'); </script> ");
            return;
        }
    }



    private void GetBranch()
    {
        string Dist_Id, ICID = "";
        Dist_Id = Session["dist_id"].ToString();
        ICID = Session["issue_id"].ToString();

        using (con = new SqlConnection(Con_WH))
        {
            try
            {
                con.Open();
                string select = string.Format("select tbl_MetaData_DEPOT.DepotName,tbl_MetaData_DEPOT.BranchID from MetaDataBranchWithIssueCenter inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.BranchId=MetaDataBranchWithIssueCenter.BranchID where IssueCenterId='" + ICID + "'");
                da = new SqlDataAdapter(select, con);
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
                    string select1 = string.Format("select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId= '23" + Dist_Id + "' order by DepotName");
                    da = new SqlDataAdapter(select1, con);

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
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtFromDate.Text = Request.Form[txtFromDate.UniqueID];
        txtToDate.Text = Request.Form[txtToDate.UniqueID];

        ddlGodown.Items.Clear();
        ddlReceiptID.Items.Clear();

        if (ddlBranch.SelectedIndex > 0)
        {
            GetGodown();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Branch'); </script> ");
            return;
        }
    }

    private void GetGodown()
    {
        using (con = new SqlConnection(Con_WH))
        {
            try
            {
                string Dist_Id = Session["dist_id"].ToString();

                con.Open();
                string qrysel = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where DistrictId= '23" + Dist_Id + "' and BranchId='" + ddlBranch.SelectedValue.ToString() + "' and Remarks = 'Y'";
                da = new SqlDataAdapter(qrysel, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlGodown.DataSource = ds.Tables[0];
                    ddlGodown.DataTextField = "Godown_Name";
                    ddlGodown.DataValueField = "Godown_ID";
                    ddlGodown.DataBind();
                    ddlGodown.Items.Insert(0, "--select--");
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

    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtFromDate.Text = Request.Form[txtFromDate.UniqueID];
        txtToDate.Text = Request.Form[txtToDate.UniqueID];

        ddlReceiptID.Items.Clear();

        if (ddlGodown.SelectedIndex > 0)
        {
            GetReceiptID();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown'); </script> ");
            return;
        }
    }

    private void GetReceiptID()
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();

                string Dist_Id = Session["dist_id"].ToString();
                string ICID = Session["issue_id"].ToString();

                string fromdate = Request.Form[txtFromDate.UniqueID];
                txtFromDate.Text = fromdate;
                string todate = Request.Form[txtToDate.UniqueID];
                txtToDate.Text = todate;

                ConvertServerDate ServerDate = new ConvertServerDate();
                string ConvertFromDate = ServerDate.getDate_MDY(fromdate.ToString());
                string ConvertToDate = ServerDate.getDate_MDY(todate.ToString());

                string select = "";

                select = "Select  IssueID + ' (' + TC_Number + ')' as Receipt,IssueID From Acceptance_Note_Kharif2016 Where Distt_ID='" + Dist_Id + "' and IssueCenter_ID='" + ICID + "' and CommodityId='" + ddlCommodity.SelectedValue.ToString() + "' and godown='" + ddlGodown.SelectedValue.ToString() + "' and Acceptance_Date between '" + ConvertFromDate + " 00:00:00' and '" + ConvertToDate + " 23:59:59' and Reject_Qty!='0'";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlReceiptID.DataSource = ds.Tables[0];
                    ddlReceiptID.DataTextField = "Receipt";
                    ddlReceiptID.DataValueField = "IssueID";
                    ddlReceiptID.DataBind();
                    ddlReceiptID.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Issue Id / TC Number Is Not Available On Selected Date'); </script> ");
                    return;
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

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (ddlReceiptID.SelectedIndex > 0)
        {
            Session["ReceiptID"] = ddlReceiptID.SelectedValue.ToString();

            if (ddlCommodity.SelectedValue.ToString() == "13")
            {
                Session["Commodity_Id"] = "2";
            }
            else
            {
                Session["Commodity_Id"] = "0";
            }

            string url = "Print_PartialRej_Pdy2016.aspx";
            string s = "window.open('" + url + "', 'popup_window');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Issue Id / TC Number'); </script> ");
            Session["DC_MO"] = Session["CreatedDate"] = null;
        }
    }
}