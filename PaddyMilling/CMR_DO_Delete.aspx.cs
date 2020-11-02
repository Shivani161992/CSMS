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

public partial class PaddyMilling_CMR_DO_Delete : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["st_id"] != null)
        if (Session["dist_id"] != null)
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
                    //txtYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
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
        if (ddlCropyear.SelectedIndex > 0)
        {
            GetMillName();
        }
        else
        {
            return;

        }

    }

    public void GetDistName()
    {
        string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "";

                //select = "SELECT district_name,district_code FROM pds.districtsmp Order By district_name";

                select = "SELECT district_name,district_code FROM pds.districtsmp where district_code='" + DistCode + "'";

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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (ddlDONo.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select CMR DO Number'); </script> ");
            return;
        }
        else if (hdfAcpt.Value == "Y")
        {
            btnDelete.Enabled = false;
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस DEPOSIT ORDER के विरुद्ध चावल जमा किया जा चूका है, इसलिए आप इसे Delete नहीं कर सकते|'); </script> ");
            return;
        }
        else if (hdfReject.Value == "Y")
        {
            btnDelete.Enabled = false;
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस DEPOSIT ORDER के विरुद्ध चावल को रिजेक्ट किया जा चूका है, इसलिए आप इसे Delete नहीं कर सकते|'); </script> ");
            return;
        }
        else
        {
            if (ddlCropyear.SelectedValue.ToString() != "")
            {
                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    using (con = new SqlConnection(strcon))
                    {
                        try
                        {
                            ClientIP objClientIP = new ClientIP();
                            string GetIp = (objClientIP.GETIP());

                            ConvertServerDate ServerDate = new ConvertServerDate();
                            string ConvertFromDate = ServerDate.getDate_MDY(txtDODate.Text);
                            string ConvertToDate = ServerDate.getDate_MDY(txtDOLastDate.Text);

                            con.Open();

                            string instr = "";

                            instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION ";

                            instr += "Insert Into PaddyMilling_DO_Log_2017(DO_Number,Mill_Code,Agreement_ID,District,CropYear,From_Date,To_Date,Milling_Type,Alloted_CommonDhan,Alloted_GradeADhan,R_Arva,R_Ushna,Issue_Centre,IP_Address,Current_DateTime,User_Agent,Check_DO,DispatchDhan_IC,Return_CommonRice,Return_GradeARice,Return_TotalRice,LotOnlyNumber,Rejected,DhanLot,Branch_Code,Godown_Code,LotNumber,Rem_Alloted_CommonDhan,Rem_Alloted_GradeADhan,Rem_DhanLot,LotNo,DeletedDate,DeletedIP,Operation) select DO_Number,Mill_Code,Agreement_ID,District,CropYear,From_Date,To_Date,Milling_Type,Alloted_CommonDhan,Alloted_GradeADhan,R_Arva,R_Ushna,Issue_Centre,IP_Address,Current_DateTime,User_Agent,Check_DO,DispatchDhan_IC,Return_CommonRice,Return_GradeARice,Return_TotalRice,LotOnlyNumber,Rejected,DhanLot,Branch_Code,Godown_Code,LotNumber,Rem_Alloted_CommonDhan,Rem_Alloted_GradeADhan,Rem_DhanLot,LotNo,GETDATE(),'" + GetIp + "','U' From PaddyMilling_DO_2017 where Mill_Code='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and DhanLot='" + hdfLotNo.Value + "' and Check_DO='" + hdfDONo.Value + "'; ";
                            instr += "Update PaddyMilling_DO_2017 Set DispatchDhan_IC='N' where Mill_Code='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and DhanLot='" + hdfLotNo.Value + "' and Check_DO='" + hdfDONo.Value + "'; ";

                            instr += "Insert Into CMR_DepositOrder_Log(CMR_DO,CMR_RecdDist,Paddy_AgrmtDist,CropYear,Mill_ID,Agreement_ID,Lot_No,DO_No,IssueCenter,Branch,Godown_id,CMR_DODate,CreatedDate,IP_Address,IsAccepted,IsRejected,DeletedDate,DeletedIP,Operation) Select CMR_DO,CMR_RecdDist,Paddy_AgrmtDist,CropYear,Mill_ID,Agreement_ID,Lot_No,DO_No,IssueCenter,Branch,Godown_id,CMR_DODate,CreatedDate,IP_Address,IsAccepted,IsRejected,GETDATE(),'" + GetIp + "','D' From CMR_DepositOrder where CMR_DO='" + ddlDONo.SelectedItem.ToString() + "' ; ";
                            instr += "Delete From CMR_DepositOrder where CMR_DO='" + ddlDONo.SelectedItem.ToString() + "' ; ";

                            instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";

                            cmd = new SqlCommand(instr, con);
                            int count = cmd.ExecuteNonQuery();

                            if (count > 0)
                            {
                                btnDelete.Enabled = false;

                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('CMR Deposit Order Deleted Successfully'); </script> ");
                                //txtYear.Text = "";
                                ddlCropyear.Enabled = false;
                                ddlFrmDist.Enabled = ddlMillName.Enabled = ddlAgtmtNumber.Enabled = ddlDONo.Enabled = false;
                                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                            }
                            else
                            {
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Sorry, Deletion Not Allow'); </script> ");
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
        //Response.Redirect("~/State/PaddyMillingHome.aspx");
        Response.Redirect("~/District/PaddyMillingHome.aspx");
    }

    protected void ddlFrmDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMillName.Items.Clear();
        ddlAgtmtNumber.Items.Clear();
        ddlDONo.Items.Clear();
        btnDelete.Enabled = false;
        txtDODate.Text = txtDOLastDate.Text = txtIC.Text = txtGodown.Text = "";
        hdfAcpt.Value = hdfReject.Value = hdfLotNo.Value = hdfDONo.Value = "";

        if (ddlFrmDist.SelectedIndex > 0)
        {
            //GetMillName();
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

        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";

                //Only For Agrmt Dist & Miller Dist.
                //select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement As PM Left Join Miller_Registration MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where PM.District='" + ddlFrmDist.SelectedValue.ToString() + "' and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' order by MillName Asc";

                //Agrmt Dist & Miller Dist & CMR Map. Dist
                select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join CMRDO_OtherDistMap As CDO ON(CDO.CropYear=PM.CropYear and CDO.District=PM.District and  CDO.Agreement_ID=PM.Agreement_ID) Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where (PM.District='" + ddlFrmDist.SelectedValue.ToString() + "' or PM.Mill_Addr_District='" + ddlFrmDist.SelectedValue.ToString() + "' OR CDO.CMRDistrict='" + ddlFrmDist.SelectedValue.ToString() + "') and PM.CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and PM.IsAccepted='Y' order by MillName Asc";

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
        ddlDONo.Items.Clear();
        btnDelete.Enabled = false;
        txtDODate.Text = txtDOLastDate.Text = txtIC.Text = txtGodown.Text = "";
        hdfAcpt.Value = hdfReject.Value = hdfLotNo.Value = hdfDONo.Value = "";

        using (con = new SqlConnection(strcon))
        {
            try
            {
                if (ddlMillName.SelectedIndex > 0)
                {
                    con.Open();

                    string select = "";
                    //Only For Agrmt Dist & Miller Dist.
                    //select = "Select Agreement_ID From PaddyMilling_Agreement where (District='" + ddlFrmDist.SelectedValue.ToString() + "' OR Mill_Addr_District='" + ddlFrmDist.SelectedValue.ToString() + "') and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and IsAccepted='Y' order by Agreement_ID";

                    //Agrmt Dist & Miller Dist & CMR Map. Dist
                    select = "Select distinct PM.Agreement_ID From PaddyMilling_Agreement_2017 AS PM Left Join CMRDO_OtherDistMap As CDO ON(CDO.CropYear=PM.CropYear and CDO.District=PM.District and  CDO.Agreement_ID=PM.Agreement_ID) where (PM.Mill_Addr_District='" + ddlFrmDist.SelectedValue.ToString() + "' Or PM.District='" + ddlFrmDist.SelectedValue.ToString() + "' OR CDO.CMRDistrict='" + ddlFrmDist.SelectedValue.ToString() + "') and PM.Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and PM.CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and PM.IsAccepted='Y' order by PM.Agreement_ID";

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
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Mill Name'); </script> ");
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
        ddlDONo.Items.Clear();
        txtDODate.Text = txtDOLastDate.Text = txtIC.Text = txtGodown.Text = "";
        hdfAcpt.Value = hdfReject.Value = hdfLotNo.Value = hdfDONo.Value = "";
        btnDelete.Enabled = false;

        if (ddlAgtmtNumber.SelectedIndex > 0)
        {
            GetAgrmtData();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Agreement Number'); </script> ");
        }
    }

    public void GetAgrmtData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select CMR_DO From CMR_DepositOrder  where Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedValue.ToString() + "' and (Paddy_AgrmtDist='" + ddlFrmDist.SelectedValue.ToString() + "' OR CMR_RecdDist='" + ddlFrmDist.SelectedValue.ToString() + "' ) and CropYear='" + ddlCropyear.SelectedValue.ToString() + "' ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlDONo.DataSource = ds.Tables[0];
                    ddlDONo.DataTextField = "CMR_DO";
                    ddlDONo.DataValueField = "CMR_DO";
                    ddlDONo.DataBind();
                    ddlDONo.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('CMR Deposit Order Number Is Not Available'); </script> ");
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

    protected void ddlDONo_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDODate.Text = txtDOLastDate.Text = txtIC.Text = txtGodown.Text = "";
        hdfAcpt.Value = hdfReject.Value = hdfLotNo.Value = hdfDONo.Value = "";
        btnDelete.Enabled = false;

        if (ddlDONo.SelectedIndex > 0)
        {
            GetDOData();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select CMR DO Number'); </script> ");
        }
    }

    public void GetDOData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select DO_No,Lot_No,IsAccepted,IsRejected,IssueCenter,Godown_id,CreatedDate,CMR_DODate From CMR_DepositOrder Where CMR_DO='" + ddlDONo.SelectedItem.ToString() + "' ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    hdfAcpt.Value = ds.Tables[0].Rows[0]["IsAccepted"].ToString();
                    hdfReject.Value = ds.Tables[0].Rows[0]["IsRejected"].ToString();
                    hdfLotNo.Value = ds.Tables[0].Rows[0]["Lot_No"].ToString();
                    hdfDONo.Value = ds.Tables[0].Rows[0]["DO_No"].ToString();

                    if (hdfAcpt.Value == "Y")
                    {
                        btnDelete.Enabled = false;
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस DEPOSIT ORDER के विरुद्ध चावल जमा किया जा चूका है, इसलिए आप इसे Delete नहीं कर सकते|'); </script> ");
                    }
                    else if (hdfReject.Value == "Y")
                    {
                        btnDelete.Enabled = false;
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस DEPOSIT ORDER के विरुद्ध चावल को रिजेक्ट किया जा चूका है, इसलिए आप इसे Delete नहीं कर सकते|'); </script> ");
                    }
                    else
                    {
                        btnDelete.Enabled = true;
                    }

                    DateTime DODate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                    txtDODate.Text = DODate.ToString("dd-MM-yyyy");

                    DateTime DOLastDate = DateTime.Parse(ds.Tables[0].Rows[0]["CMR_DODate"].ToString());
                    txtDOLastDate.Text = DOLastDate.ToString("dd-MM-yyyy");

                    txtIC.Text = ds.Tables[0].Rows[0]["IssueCenter"].ToString();
                    txtGodown.Text = ds.Tables[0].Rows[0]["Godown_id"].ToString();

                    GetGodownName();
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

    public void GetGodownName()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                string GodownName = "", ICName = "";

                GodownName = txtGodown.Text;
                ICName = txtIC.Text;

                con_MPStorage.Open();
                string select = string.Format("select (select DepotName from tbl_MetaData_DEPOT where DepotId='" + ICName + "') As ICName,(select Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + GodownName + "') As Godown_Name");
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        txtGodown.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
                        txtIC.Text = ds.Tables[0].Rows[0]["ICName"].ToString();
                    }
                }
                else
                {
                    txtGodown.Text = "";
                    txtIC.Text = "";
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con_MPStorage.State != ConnectionState.Closed)
                {
                    con_MPStorage.Close();
                }
            }
        }
    }
   
}