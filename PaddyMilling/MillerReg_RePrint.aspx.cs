using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class PaddyMilling_MillerReg_RePrint : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                rbMPState.Checked = true;
                GetDistName();
                GetCropYearValues();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    public void GetDistName()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("SELECT district_name,district_code FROM pds.districtsmp Order By district_name");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlMPDist.DataSource = ds.Tables[0];
                        ddlMPDist.DataTextField = "district_name";
                        ddlMPDist.DataValueField = "district_code";
                        ddlMPDist.DataBind();
                        ddlMPDist.Items.Insert(0, "--Select--");
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
                    //hdfCropYear.Value = "2017-2018";
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

    public void GetOtherStates()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("SELECT State_Code ,State_Name FROM State_Master where Status = 'Y' and State_Code!=23 order by State_Name");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "State_Master");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlOtherStates.DataSource = ds.Tables[0];
                    ddlOtherStates.DataTextField = "State_Name";
                    ddlOtherStates.DataValueField = "State_Code";
                    ddlOtherStates.DataBind();
                    ddlOtherStates.Items.Insert(0, "--Select--");
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

    protected void rbMPState_CheckedChanged(object sender, EventArgs e)
    {
        ddlOtherStates.Enabled = false;
        GetDistName();
        ddlOtherStates.Items.Clear();
        ddlMvmtNumber.Items.Clear();
    }
    protected void rbOtherState_CheckedChanged(object sender, EventArgs e)
    {
        ddlOtherStates.Enabled = true;
        GetOtherStates();
        ddlMPDist.Items.Clear();
        ddlMvmtNumber.Items.Clear();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if(ddlMvmtNumber.SelectedIndex<=0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Miller Name'); </script> ");
            return;
        }
        if (ddlMvmtNumber.SelectedIndex > 0)
        {
            Session["CropYear"] = hdfCropYear.Value;
            Session["RegistID"] = ddlMvmtNumber.SelectedValue.ToString();

            string url = "/csms/Miller_Registeration/MillerReg_Print.aspx";
            string s = "window.open('" + url + "', 'popup_window');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Mill Name'); </script> ");
            Session["CropYear"] = null;
            Session["RegistID"] = null;
        }
    }

    protected void ddlMPDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMvmtNumber.Items.Clear();

        if (ddlMPDist.SelectedIndex > 0)
        {
            GetMPDistMiller();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select District of M.P State'); </script> ");
            return;
        }
    }

    public void GetMPDistMiller()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select Mill_Name,Registration_ID From Miller_Registration_2017 Where District_Code='" + ddlMPDist.SelectedValue.ToString() + "' and CropYear='" + hdfCropYear.Value + "' and State_Code='23' and State='MP' Order By Mill_Name";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMvmtNumber.DataSource = ds.Tables[0];
                    ddlMvmtNumber.DataTextField = "Mill_Name";
                    ddlMvmtNumber.DataValueField = "Registration_ID";
                    ddlMvmtNumber.DataBind();
                    ddlMvmtNumber.Items.Insert(0, "--Select--");
                    btnPrint.Enabled = true;
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपने जो जिला चुना है, उस जिले से किसी भी मिलर ने रजिस्ट्रेशन नहीं किया है|'); </script> ");
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

    protected void ddlOtherStates_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMvmtNumber.Items.Clear();

        if (ddlOtherStates.SelectedIndex > 0)
        {
            GetOtherStateMiller();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Other State'); </script> ");
            return;
        }
    }

    public void GetOtherStateMiller()
    {
        string districtid = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select Mill_Name,Registration_ID From Miller_Registration_2017 Where CropYear='" + hdfCropYear.Value + "' and State_Code='" + ddlOtherStates.SelectedValue.ToString() + "' and State='Other' Order By Mill_Name";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMvmtNumber.DataSource = ds.Tables[0];
                    ddlMvmtNumber.DataTextField = "Mill_Name";
                    ddlMvmtNumber.DataValueField = "Registration_ID";
                    ddlMvmtNumber.DataBind();
                    ddlMvmtNumber.Items.Insert(0, "--Select--");
                    btnPrint.Enabled = true;
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपने जो राज्य चुना है, उस राज्य से किसी भी मिलर ने रजिस्ट्रेशन नहीं किया है|'); </script> ");
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
}