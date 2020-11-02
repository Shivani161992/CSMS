using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class PaddyMilling_Reprint_CMRDO : System.Web.UI.Page
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

        string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";
                select = "Select distinct MR.Mill_Name,CMRDO.Mill_ID As MillID From CMR_DepositOrder As CMRDO Left Join Miller_Registration_2017 MR ON(CMRDO.Mill_ID=MR.Registration_ID and CMRDO.CropYear=MR.CropYear) where CMRDO.CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and (CMRDO.CMR_RecdDist='" + DistCode + "' OR CMRDO.Paddy_AgrmtDist='" + DistCode + "') order by MR.Mill_Name Asc";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMillName.DataSource = ds.Tables[0];
                    ddlMillName.DataTextField = "Mill_Name";
                    ddlMillName.DataValueField = "MillID";
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
              //  select = "Select distinct Agreement_ID From CMR_DepositOrder where (Paddy_AgrmtDist='" + DistCode + "' OR CMR_RecdDist='" + DistCode + "') and CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and Mill_ID='" + ddlMillName.SelectedValue.ToString() + "'";
                select = "Select distinct PM.Agreement_ID From PaddyMilling_Agreement_2017 AS PM Left Join CMRDO_OtherDistMap As CDO ON(CDO.CropYear=PM.CropYear and CDO.District=PM.District and  CDO.Agreement_ID=PM.Agreement_ID) where (PM.Mill_Addr_District='" + DistCode + "' Or PM.District='" + DistCode + "' OR CDO.CMRDistrict='" + DistCode + "') and PM.Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and PM.CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and PM.IsAccepted='Y' order by PM.Agreement_ID";
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
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('अनुबंध का नंबर उपलब्ध नहीं है|'); </script> ");
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
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('अनुबंध का नंबर उपलब्ध नहीं है|'); </script> ");
            return;
        }
    }

    public void GetDONumber()
    {
        string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                //select = "Select CMR_DO From CMR_DepositOrder where (Paddy_AgrmtDist='" + DistCode + "' OR CMR_RecdDist='" + DistCode + "')  and CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgrmtNo.SelectedItem.ToString() + "'";
                select = "Select CMR_DO From CMR_DepositOrder  where Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgrmtNo.SelectedValue.ToString() + "' and (Paddy_AgrmtDist='" + DistCode + "' OR CMR_RecdDist='" + DistCode + "' ) and CropYear='" + ddlCropyear.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMvmtNumber.DataSource = ds.Tables[0];
                    ddlMvmtNumber.DataTextField = "CMR_DO";
                    ddlMvmtNumber.DataValueField = "CMR_DO";
                    ddlMvmtNumber.DataBind();
                    ddlMvmtNumber.Items.Insert(0, "--Select--");
                    btnPrint.Enabled = true;
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('CMR Deposit Order Number Is Not Available'); </script> ");
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
            Session["CMRDO_ID"] = ddlMvmtNumber.SelectedItem.ToString();

            string url = "Print/PrintCMR_DepositOrder.aspx";
            string s = "window.open('" + url + "', 'popup_window');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select CMR Deposit Order Number'); </script> ");
            Session["CMRDO_ID"] = null;
        }
    }
}