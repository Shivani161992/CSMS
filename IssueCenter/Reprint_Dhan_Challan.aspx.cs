using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class IssueCenter_Reprint_Dhan_Challan : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da;
    DataSet ds;

    string districtid = "";
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            if (!IsPostBack)
            {
                GetCropYearValues();
                GetMillName();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    public void GetCropYearValues()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("SELECT CropYear FROM PaddyMilling_CropYear order by CropYear desc");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "PaddyMilling_CropYear");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    hdfCropYear.Value = ds.Tables[0].Rows[0]["CropYear"].ToString();
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

    public void GetMillName()
    {
        ddlMillName.Items.Clear();

        string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";

                if (Session["Markfed"].ToString() == "Y")
                {
                    select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where PM.District='" + DistCode + "' and PM.CropYear='" + hdfCropYear.Value + "' and PM.IsAccepted='Y' and PM.User_Agent='DDMO' order by MillName Asc";
                }
                else
                {
                    select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where PM.District='" + DistCode + "' and PM.CropYear='" + hdfCropYear.Value + "' and PM.IsAccepted='Y' and PM.User_Agent='DDMO'  order by MillName Asc";
                }
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMillName.DataSource = ds.Tables[0];
                    ddlMillName.DataTextField = "MillName";
                    ddlMillName.DataValueField = "MillCode";
                    ddlMillName.DataBind();
                    ddlMillName.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके जिले से किसी भी मिलर ने अनुबंध नहीं किया हैं, इसलिए मिल का नाम उपलब्ध नहीं है|'); </script> ");
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

    protected void ddlMillName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlAgrmtNo.Items.Clear();
        ddlMvmtNumber.Items.Clear();

        if (ddlMillName.SelectedIndex > 0)
        {
            GetAgrmtNO();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Mill Name'); </script> ");
            return;
        }
    }

    public void GetAgrmtNO()
    {
        string DistCode = Session["dist_id"].ToString();


        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                if (Session["Markfed"].ToString() == "Y")
                {
                    select = "Select Agreement_ID From PaddyMilling_Agreement_2017 where District='" + DistCode + "' and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + hdfCropYear.Value + "' and IsAccepted='Y' and User_Agent='DDMO' order by Agreement_ID";
                }
                else
                {
                    select = "Select Agreement_ID From PaddyMilling_Agreement_2017 where District='" + DistCode + "' and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + hdfCropYear.Value + "' and IsAccepted='Y' and User_Agent='DDMO' order by Agreement_ID";
                }

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlAgrmtNo.DataSource = ds.Tables[0];
                    ddlAgrmtNo.DataTextField = "Agreement_ID";
                    ddlAgrmtNo.DataValueField = "Agreement_ID";
                    ddlAgrmtNo.DataBind();
                    ddlAgrmtNo.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Agreement Number Is Not Available'); </script> ");
                    return;
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

    protected void ddlAgrmtNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMvmtNumber.Items.Clear();

        if (ddlAgrmtNo.SelectedIndex > 0)
        {
            GetDONumber();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Agreement Number'); </script> ");
            return;
        }
    }

    public void GetDONumber()
    {
        string DistCode = Session["dist_id"].ToString();
        string IssueCentreCode = Session["issue_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                if (Session["Markfed"].ToString() == "Y")
                {
                    select = "Select trans_id From PaddyMilling_IssueAgainst_DO where district_code='" + DistCode + "' and Partyname='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + hdfCropYear.Value + "' and Agreement_ID='" + ddlAgrmtNo.SelectedItem.ToString() + "' ";
                }
                else
                {
                    select = "Select trans_id From PaddyMilling_IssueAgainst_DO where district_code='" + DistCode + "' and Partyname='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + hdfCropYear.Value + "' and Agreement_ID='" + ddlAgrmtNo.SelectedItem.ToString() + "' ";
                }

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMvmtNumber.DataSource = ds.Tables[0];
                    ddlMvmtNumber.DataTextField = "trans_id";
                    ddlMvmtNumber.DataValueField = "trans_id";
                    ddlMvmtNumber.DataBind();
                    ddlMvmtNumber.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Challan Number Is Not Available'); </script> ");
                    return;
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

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (ddlMvmtNumber.SelectedIndex > 0)
        {
            Session["Agreement_ID"] = ddlAgrmtNo.SelectedItem.ToString();
            Session["MillCode"] = ddlMillName.SelectedValue.ToString();
            Session["Paddy_Challan"] = ddlMvmtNumber.SelectedItem.ToString();

            string url = "/csms/PaddyMilling/Print/PDhan_Challan.aspx";
            string s = "window.open('" + url + "', 'popup_window');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Challan Number'); </script> ");
            Session["Agreement_ID"] = null;
            Session["Paddy_Challan"] = null;
            Session["MillCode"] = null;
        }
    }
}