using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class IssueCenter_Reprint_CMR_Acpt_Reject : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da;
    DataSet ds;

    string districtid = "", IC_ID = "";
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            if (!IsPostBack)
            {
                districtid = Session["dist_id"].ToString();
                IC_ID = Session["issue_id"].ToString();

                GetCropYearValues();
                //GetMillName();
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
                   // hdfCropYear.Value = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    ddlCropyear.DataSource = ds.Tables[0];
                    ddlCropyear.DataTextField = "CropYear";
                    ddlCropyear.DataValueField = "CropYear";
                    ddlCropyear.DataBind();
                    ddlCropyear.Items.Insert(0, "--Select--");
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
    protected void ddlCropyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlAgrmtNo.Items.Clear();
        ddlMvmtNumber.Items.Clear();


        if (ddlCropyear.SelectedIndex > 0)
        {
            GetMillName();
        }
        else
        {
            return;

        }
    }

    public void GetMillName()
    {
        ddlMillName.Items.Clear();
        districtid = Session["dist_id"].ToString();
        string DistCode = Session["dist_id"].ToString();
        IC_ID = Session["issue_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";
                select = "Select distinct CMR.Mill_Name As MillCode,MR.Mill_Name From CMR_QualityInspection As CMR Left Join Miller_Registration_2017 As MR ON(CMR.Mill_Name=MR.Registration_ID and CMR.CropYear=MR.CropYear) Where CMR.District='" + districtid + "' and issueCentre_code='" + IC_ID + "' and CMR.CropYear='" + ddlCropyear.SelectedValue.ToString() + "' ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMillName.DataSource = ds.Tables[0];
                    ddlMillName.DataTextField = "Mill_Name";
                    ddlMillName.DataValueField = "MillCode";
                    ddlMillName.DataBind();
                    ddlMillName.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके प्रदाय केंद्र में किसी भी मिलर ने CMR जमा नहीं किया हैं, इसलिए मिल का नाम उपलब्ध नहीं है|'); </script> ");
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
        IC_ID = Session["issue_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "Select distinct CMR.Agreement_ID From CMR_QualityInspection As CMR Where CMR.District='" + DistCode + "' and issueCentre_code='" + IC_ID + "' and CMR.CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and CMR.Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' ";

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
                select = "Select CMR.Book_Number From CMR_QualityInspection As CMR Where CMR.District='" + DistCode + "' and issueCentre_code='" + IssueCentreCode + "' and CMR.CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and CMR.Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and CMR.Agreement_ID='" + ddlAgrmtNo.SelectedItem.ToString() + "' ";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMvmtNumber.DataSource = ds.Tables[0];
                    ddlMvmtNumber.DataTextField = "Book_Number";
                    ddlMvmtNumber.DataValueField = "Book_Number";
                    ddlMvmtNumber.DataBind();
                    ddlMvmtNumber.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('CMR Acceptance/Rejection No Is Not Available'); </script> ");
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
            Session["Book_Number"] = ddlMvmtNumber.SelectedItem.ToString();

            string url = "ReprintCMRAccept.aspx";
            string s = "window.open('" + url + "', 'popup_window');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select CMR Acceptance/Rejection Number'); </script> ");
            Session["Book_Number"] = null;
        }
    }

   
}