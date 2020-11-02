using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


public partial class PaddyMilling_Reprint_Ins_CMR_Rej : System.Web.UI.Page
{

    SqlConnection con;
    SqlDataAdapter da;
    DataSet ds;

    string districtid = "";
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["dist_id"] != null)
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
                select = "select distinct CIR.Mill_ID, MR.Mill_Name from CMR_Inspection_Rejected as CIR inner join Miller_Registration_2017 as MR on MR.Registration_ID=CIR.Mill_ID and MR.CropYear=MR.CropYear where CMR_RecdDist='" + DistCode + "' order by Mill_Name";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMillName.DataSource = ds.Tables[0];
                    ddlMillName.DataTextField = "Mill_Name";
                    ddlMillName.DataValueField = "Mill_ID";
                    ddlMillName.DataBind();
                    ddlMillName.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके जिले से किसी भी मिलर के लिए CMR Deposit Order नहीं बना है, इसलिए मिल का नाम उपलब्ध नहीं है|'); </script> ");
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
       // ddlAgrmtNo.Items.Clear();
        ddlMvmtNumber.Items.Clear();

        if (ddlMillName.SelectedIndex > 0)
        {
           // GetAgrmtNO();
            GetRejNumber();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Mill Name'); </script> ");
            return;
        }
    }

    public void GetRejNumber()
    {
        string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "select CMR_RejectionNo from CMR_Inspection_Rejected as CIR where Mill_ID='"+ddlMillName.SelectedValue.ToString()+"'";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMvmtNumber.DataSource = ds.Tables[0];
                    ddlMvmtNumber.DataTextField = "CMR_RejectionNo";
                    ddlMvmtNumber.DataValueField = "CMR_RejectionNo";
                    ddlMvmtNumber.DataBind();
                    ddlMvmtNumber.Items.Insert(0, "--Select--");
                    btnPrint.Enabled = true;
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('CMR Rejection Number Is Not Available'); </script> ");
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
            Session["CMRReject_ID"] = ddlMvmtNumber.SelectedItem.ToString();

            string url = "Print/PrintCMRInsp_Rejection.aspx";
            string s = "window.open('" + url + "', 'popup_window');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select CMR Rejection Number'); </script> ");
            Session["CMRReject_ID"] = null;
        }
    }
   
}