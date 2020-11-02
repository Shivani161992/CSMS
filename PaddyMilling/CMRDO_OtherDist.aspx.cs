using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;

public partial class PaddyMilling_CMRDO_OtherDist : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {
            if (!IsPostBack)
            {
                GetCropYearValues();
                GetDistName();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    //protected void Page_PreInit(object sender, EventArgs e)
    //{
    //    if (Session["st_id"] != null)
    //    {
    //        if (Session["st_id"].ToString() == "10")
    //        {
    //            this.MasterPageFile = "~/MasterPage/Markfed_PDY.master";
    //        }
    //    }
    //    else
    //    {
    //        Response.Redirect("~/MainLogin.aspx");
    //    }
    //}

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
                    txtYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
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

    public void GetDistName()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                //if (Session["st_id"].ToString() == "10")
                //{
                //    select = "SELECT district_name,district_code FROM pds.districtsmp where paddy_markfed='Y' Order By district_name";
                //}
                //else
                //{
                //    select = "SELECT district_name,district_code FROM pds.districtsmp where paddy_markfed IS NULL Order By district_name";
                //}

                select = "SELECT district_name,district_code FROM pds.districtsmp Order By district_name";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlFrmDist.DataSource = ds.Tables[0];
                        ddlFrmDist.DataTextField = "district_name";
                        ddlFrmDist.DataValueField = "district_code";
                        ddlFrmDist.DataBind();
                        ddlFrmDist.Items.Insert(0, "--Select--");
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

    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnRecptSubmit_Click1(object sender, EventArgs e)
    {
        if (ddlCMRDist.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select CMR District'); </script> ");
            return;
        }
        else if (ddlAgtmtNumber.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Agreement Number'); </script> ");
            return;
        }
        else if (hdfMappingData.Value == "1")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस Agreement No. के विरुद्ध " + ddlCMRDist.SelectedItem.ToString() + " जिले के लिए आपने Already Mapping कर चुकी हैं|'); </script> ");
            return;
        }
        else
        {
            if (txtYear.Text != "")
            {
                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    using (con = new SqlConnection(strcon))
                    {
                        try
                        {
                            con.Open();

                            ClientIP objClientIP = new ClientIP();
                            string GetIp = (objClientIP.GETIP());

                            string instr = "";

                            string ChechMax = "";
                            decimal MaxMappingNo = 0;
                            ChechMax = "Select Max(Mapping_No) As MaxMap From CMRDO_OtherDistMap Where District='" + ddlFrmDist.SelectedValue.ToString() + "' and CMRDistrict='" + ddlCMRDist.SelectedValue.ToString() + "' ";
                            da = new SqlDataAdapter(ChechMax, con);
                            ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                string Data = "";
                                Data = ds.Tables[0].Rows[0]["MaxMap"].ToString();
                                if (Data == "")
                                {
                                    MaxMappingNo = decimal.Parse(ddlFrmDist.SelectedValue.ToString() + ddlCMRDist.SelectedValue.ToString() + "100000");
                                }
                                else
                                {
                                    string wid = Data.Substring(Data.Length - 6);
                                    Int64 wid_ID_new = Convert.ToInt64(wid);
                                    wid_ID_new = wid_ID_new + 1;
                                    string combine = wid_ID_new.ToString();
                                    MaxMappingNo = decimal.Parse(ddlFrmDist.SelectedValue.ToString() + ddlCMRDist.SelectedValue.ToString() + combine);
                                }
                            }

                            if (MaxMappingNo != 0)
                            {
                                instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION ";
                                instr += "Insert Into CMRDO_OtherDistMap(District,CMRDistrict,CropYear,Mill_ID,Agreement_ID,Mapping_No,CreatedDate,IP_Address) Values('" + ddlFrmDist.SelectedValue.ToString() + "','" + ddlCMRDist.SelectedValue.ToString() + "','" + txtYear.Text + "','" + ddlMillName.SelectedValue.ToString() + "','" + ddlAgtmtNumber.SelectedItem.ToString() + "','" + MaxMappingNo + "',GETDATE(),'" + GetIp + "');";
                                instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";

                                cmd = new SqlCommand(instr, con);
                                int count = cmd.ExecuteNonQuery();

                                if (count > 0)
                                {
                                    btnRecptSubmit.Enabled = false;
                                    ddlFrmDist.Enabled = ddlMillName.Enabled = ddlAgtmtNumber.Enabled = ddlCMRDist.Enabled = false;
                                    txtYear.Text = "";
                                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully'); </script> ");
                                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                                }
                                else
                                {
                                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('!!!!Failed'); </script> ");
                                }
                            }
                            else
                            {
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Sorry, Changes Not Allow'); </script> ");
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
                else
                {
                    Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        if (Session["st_id"].ToString() == "10")
        {
            Response.Redirect("~/State/PaddyMillingHome_MFD.aspx");
        }
        else
        {
            Response.Redirect("~/State/PaddyMillingHome.aspx");
        }
    }

    protected void ddlFrmDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMillName.Items.Clear();
        ddlAgtmtNumber.Items.Clear();
        ddlCMRDist.Items.Clear();
        txtAgrmtQty.Text = "";
        hdfMappingData.Value = "";
        btnRecptSubmit.Enabled = false;

        if (ddlFrmDist.SelectedIndex > 0)
        {
            GetMillName();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select District'); </script> ");
            return;
        }
    }

    public void GetMillName()
    {
        ddlMillName.Items.Clear();
        ddlCMRDist.Items.Clear();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";

                select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where PM.District='" + ddlFrmDist.SelectedValue.ToString() + "' and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' order by MillName Asc";

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
        ddlAgtmtNumber.Items.Clear();
        ddlCMRDist.Items.Clear();
        txtAgrmtQty.Text = "";
        hdfMappingData.Value = "";
        btnRecptSubmit.Enabled = false;

        using (con = new SqlConnection(strcon))
        {
            try
            {
                if (ddlMillName.SelectedIndex > 0)
                {
                    con.Open();

                    string select = "";

                    select = "Select Agreement_ID From PaddyMilling_Agreement_2017 where District='" + ddlFrmDist.SelectedValue.ToString() + "' and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and IsAccepted='Y' order by Agreement_ID";

                    da = new SqlDataAdapter(select, con);
                    ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlAgtmtNumber.DataSource = ds.Tables[0];
                        ddlAgtmtNumber.DataTextField = "Agreement_ID";
                        ddlAgtmtNumber.DataValueField = "Agreement_ID";
                        ddlAgtmtNumber.DataBind();
                        ddlAgtmtNumber.Items.Insert(0, "--Select--");
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया मिल का नाम चुनें|'); </script> ");
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

    protected void ddlAgtmtNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtAgrmtQty.Text = "";
        hdfMappingData.Value = "";
        ddlCMRDist.Items.Clear();
        btnRecptSubmit.Enabled = false;

        if (ddlAgtmtNumber.SelectedIndex > 0)
        {
            GetAgrmtData();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया अनुबंध नंबर चुनें|'); </script> ");
        }
    }

    public void GetAgrmtData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select Common_Dhan From PaddyMilling_Agreement_2017 Where District='" + ddlFrmDist.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedValue.ToString() + "' and IsAccepted='Y'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtAgrmtQty.Text = ds.Tables[0].Rows[0]["Common_Dhan"].ToString();
                    GetCMRDist();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('No Data Found'); </script> ");
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

    public void GetCMRDist()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "SELECT district_name,district_code FROM pds.districtsmp Order By district_name";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlCMRDist.DataSource = ds.Tables[0];
                        ddlCMRDist.DataTextField = "district_name";
                        ddlCMRDist.DataValueField = "district_code";
                        ddlCMRDist.DataBind();
                        ddlCMRDist.Items.Insert(0, "--Select--");
                        ddlCMRDist.Items.FindByValue(ddlFrmDist.SelectedValue.ToString()).Enabled = false;
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

    protected void ddlCMRDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnRecptSubmit.Enabled = false;
        hdfMappingData.Value = "";
        if (ddlCMRDist.SelectedIndex > 0)
        {
            GetMappingData();

            if (hdfMappingData.Value == "1")
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस Agreement No. के विरुद्ध " + ddlCMRDist.SelectedItem.ToString() + " जिले के लिए आपने Already Mapping कर चुकी हैं|'); </script> ");
                return;
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select CMR District'); </script> ");
            return;
        }
    }

    public void GetMappingData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select * From CMRDO_OtherDistMap Where District='" + ddlFrmDist.SelectedValue.ToString() + "' and CMRDistrict='" + ddlCMRDist.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    hdfMappingData.Value = "1";
                }
                else
                {
                    btnRecptSubmit.Enabled = true;
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