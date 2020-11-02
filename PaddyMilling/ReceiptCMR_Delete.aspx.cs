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

public partial class PaddyMilling_ReceiptCMR_Delete : System.Web.UI.Page
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

               // select = "SELECT district_name,district_code FROM pds.districtsmp Order By district_name";

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

    protected void ddlFrmDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMillName.Items.Clear();
        ddlAgtmtNumber.Items.Clear();
        ddlAcptNo.Items.Clear();

        btnRecptSubmit.Enabled = false;
        txtLotNo.Text = txtStatus.Text = txtQty.Text = txtBags.Text = txtTruckNo.Text = txtRecdDate.Text = "";
        hdfStatus.Value = hdfLotNo.Value = hdfDO_NO.Value = "";
        hdfWHRValue.Value = "0";

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

                select = "Select distinct QI.Mill_Name As MillCode,MR.Mill_Name As MillName From CMR_QualityInspection As QI Left Join Miller_Registration_2017 MR ON(QI.Mill_Name=MR.Registration_ID and QI.CropYear=MR.CropYear) Where Qi.District='" + ddlFrmDist.SelectedValue.ToString() + "' and Qi.CropYear='" + ddlCropyear.SelectedValue.ToString() + "' Order By MR.Mill_Name Asc";

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
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके जिले में किसी भी मिलर ने CMR जमा नहीं किया हैं, इसलिए मिल का नाम उपलब्ध नहीं है|'); </script> ");
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
        ddlAcptNo.Items.Clear();

        btnRecptSubmit.Enabled = false;
        txtLotNo.Text = txtStatus.Text = txtQty.Text = txtBags.Text = txtTruckNo.Text = txtRecdDate.Text = "";
        hdfStatus.Value = hdfLotNo.Value = hdfDO_NO.Value =  "";
        hdfWHRValue.Value = "0";

        using (con = new SqlConnection(strcon))
        {
            try
            {
                if (ddlMillName.SelectedIndex > 0)
                {
                    con.Open();

                    string select = "";
                    select = "Select distinct QI.Agreement_ID From CMR_QualityInspection As QI Where QI.District='" + ddlFrmDist.SelectedValue.ToString() + "' and QI.CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and QI.Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' Order By QI.Agreement_ID Asc";

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
        ddlAcptNo.Items.Clear();
        btnRecptSubmit.Enabled = false;
        txtLotNo.Text = txtStatus.Text = txtQty.Text = txtBags.Text = txtTruckNo.Text = txtRecdDate.Text = "";
        hdfStatus.Value = hdfLotNo.Value = hdfDO_NO.Value = "";
        hdfWHRValue.Value = "0";

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

                string select = "Select QI.Book_Number From CMR_QualityInspection As QI Where QI.District='" + ddlFrmDist.SelectedValue.ToString() + "' and QI.CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and QI.Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and QI.Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' order by QI.Current_DateTime desc";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlAcptNo.DataSource = ds.Tables[0];
                    ddlAcptNo.DataTextField = "Book_Number";
                    ddlAcptNo.DataValueField = "Book_Number";
                    ddlAcptNo.DataBind();
                    ddlAcptNo.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('CMR Acceptance / Rejection Number Is Not Available'); </script> ");
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

    protected void ddlAcptNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnRecptSubmit.Enabled = false;
        txtLotNo.Text = txtStatus.Text = txtQty.Text = txtBags.Text = txtTruckNo.Text = txtRecdDate.Text = "";
        hdfStatus.Value = hdfLotNo.Value = hdfDO_NO.Value =  "";
        hdfWHRValue.Value = "0";

        if (ddlAcptNo.SelectedIndex > 0)
        {
            CheckWHRData();

            if (hdfWHRValue.Value == "0")
            {
                GetAcptData();
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपने जो नंबर Select किया है, उसके लिए WHR जारी किया जा चूका है, इसलिए आप इसे Delete नहीं कर सकते|'); </script> ");
                return;
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select CMR Acceptance / Rejection Number'); </script> ");
        }
    }

    public void CheckWHRData()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();

                string select = "Select * From tbl_Storage_Arrival_Stock where State_Id='23' and District_Id='23" + ddlFrmDist.SelectedValue.ToString() + "' and Commodity_Id='3' and Source_of_Arrival='05' and Challan_No='" + ddlAcptNo.SelectedItem.ToString() + "' ";
                da = new SqlDataAdapter(select, con_MPStorage);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    hdfWHRValue.Value = "1";
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

    public void GetAcptData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "Select QI.Date,QI.DO_Number,QI.LotNumber,QI.Truck_No,QI.Submited,Accept_CommonRice,QI.Reject_CommonRice,QI.Bags ,CMRDO.DO_No As CMRDO_NO From CMR_QualityInspection As QI Left Join CMR_DepositOrder As CMRDO ON(CMRDO.CMR_DO=QI.DO_Number) Where QI.Book_Number='" + ddlAcptNo.SelectedItem.ToString() + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DateTime CMRDate = DateTime.Parse(ds.Tables[0].Rows[0]["Date"].ToString());
                    txtRecdDate.Text = CMRDate.ToString("dd/MMM/yyyy");

                    hdfCMRDoNo.Value = ds.Tables[0].Rows[0]["DO_Number"].ToString();
                    hdfLotNo.Value = ds.Tables[0].Rows[0]["LotNumber"].ToString();
                    hdfDO_NO.Value = ds.Tables[0].Rows[0]["CMRDO_NO"].ToString();
                    txtLotNo.Text = "Lot" + ds.Tables[0].Rows[0]["LotNumber"].ToString();
                    txtTruckNo.Text = ds.Tables[0].Rows[0]["Truck_No"].ToString();

                    hdfStatus.Value = ds.Tables[0].Rows[0]["Submited"].ToString();
                    if (hdfStatus.Value == "Y")
                    {
                        txtStatus.Text = "Accepted";
                    }
                    else
                    {
                        txtStatus.Text = "Rejected";
                    }

                    string Qty = ds.Tables[0].Rows[0]["Reject_CommonRice"].ToString();
                    if (Qty == "0")
                    {
                        txtQty.Text = ds.Tables[0].Rows[0]["Accept_CommonRice"].ToString();
                    }
                    else
                    {
                        txtQty.Text = ds.Tables[0].Rows[0]["Reject_CommonRice"].ToString();
                    }

                    txtBags.Text = ds.Tables[0].Rows[0]["Bags"].ToString();

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

    protected void btnRecptSubmit_Click1(object sender, EventArgs e)
    {
        if (ddlAcptNo.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select CMR Acceptance / Rejection Number'); </script> ");
            return;
        }
        else if (txtQty.Text == "" || txtBags.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select All Fields'); </script> ");
            return;
        }
        else if (hdfWHRValue.Value != "0")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपने जो नंबर Select किया है, उसके लिए WHR जारी किया जा चूका है, इसलिए आप इसे Delete नहीं कर सकते|'); </script> ");
            return;
        }
        else
        {
            if (Session["update"].ToString() == ViewState["update"].ToString())
            {
                using (con = new SqlConnection(strcon))
                {
                    try
                    {
                        ClientIP objClientIP = new ClientIP();
                        string GetIp = (objClientIP.GETIP());

                        con.Open();

                        string instr = "";

                        {
                            instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                                "Insert Into CMR_QualityInspection_Log(District,issueCentre_code,CropYear,Book_Number,Date,Mill_Name,Milling_Type,DO_Number,Agreement_ID,LotNumber,Acceptance_No,Rejection_No,Truck_No,LD_No,TotaGA,TotaS,TotaRemark,ChoteToteGA,ChoteToteS,ChoteToteRemark,VijatiyeGA,VijatiyeS,VijatiyeRemark,DamageDaaneGA,DamageDaaneS,DamageDaaneRemark,BadrangDaaneGA,BadrangDaaneS,BadrangDaaneRemark,ChaakiDaaneGA,ChaakiDaaneS,ChaakiDaaneRemark,LaalDaaneGA,LaalDaaneS,LaalDaaneRemark,OtherGA,OtherS,OtherRemark,ChokarDaaneGA,ChokarDaaneS,ChokarDaaneRemark,NamiGA,NamiS,NamiRemark,BookOnlyNumber,IP_Address,Current_DateTime,User_Agent,Submited,Rejected,Daane,Accept_CommonRice,Accept_GradeARice,Reject_CommonRice,Reject_GradeARice,Branch_Code,Godown_Code,Bags,BagType,Tags,TagNo,TruckNo1,DeletedDate,DeletedIP,Operation,ToulReceiptNo, Inspector_ID, Agreement_Dist, StackNumber) select District,issueCentre_code,CropYear,Book_Number,Date,Mill_Name,Milling_Type,DO_Number,Agreement_ID,LotNumber,Acceptance_No,Rejection_No,Truck_No,LD_No,TotaGA,TotaS,TotaRemark,ChoteToteGA,ChoteToteS,ChoteToteRemark,VijatiyeGA,VijatiyeS,VijatiyeRemark,DamageDaaneGA,DamageDaaneS,DamageDaaneRemark,BadrangDaaneGA,BadrangDaaneS,BadrangDaaneRemark,ChaakiDaaneGA,ChaakiDaaneS,ChaakiDaaneRemark,LaalDaaneGA,LaalDaaneS,LaalDaaneRemark,OtherGA,OtherS,OtherRemark,ChokarDaaneGA,ChokarDaaneS,ChokarDaaneRemark,NamiGA,NamiS,NamiRemark,BookOnlyNumber,IP_Address,Current_DateTime,User_Agent,Submited,Rejected,Daane,Accept_CommonRice,Accept_GradeARice,Reject_CommonRice,Reject_GradeARice,Branch_Code,Godown_Code,Bags,BagType,Tags,TagNo,TruckNo1,GETDATE(),'" + GetIp + "','D',ToulReceiptNo, Inspector_ID, Agreement_Dist, StackNumber From CMR_QualityInspection where Book_Number='" + ddlAcptNo.SelectedItem.ToString() + "' ; ";

                            instr += "Insert Into PaddyMilling_Agreement_Log select District,Dist_Manager_Name,Mill_Addr_District,Mill_Name,Mill_Operator_Name,Mill_Operator,CropYear,From_Date,To_Date,Common_Dhan,GradeA_Dhan,Total_Dhan,Agreement_ID,IP_Address,Current_DateTime,User_Agent,State,State_Code,DepositMoney,DhanLot,DhanAmount,DhanAmountType,DhanAmountDetails,Agrmt_Date,R_Arva,R_Ushna,Rem_Common_Dhan,Rem_GradeA_Dhan,Rem_Total_Dhan,Rem_DhanLot,Milling_Type,Return_CommonDhan,Return_GradeADhan,Return_TotalDhan,Return_CommonRice,Return_GradeARice,Return_TotalRice,Rejected,IsAccepted,MobileNO,AcceptedIP,AcceptedDate,GETDATE(),'" + GetIp + "','U' From PaddyMilling_Agreement_2017 where Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' ; ";

                            instr += "Delete From CMR_QualityInspection where Book_Number='" + ddlAcptNo.SelectedItem.ToString() + "';";


                            if (hdfStatus.Value == "Y")
                            {
                                decimal RecdCMR = decimal.Parse(txtQty.Text);
                                instr += "Insert Into dbo.tbl_Receipt_Details_log select * From dbo.tbl_Receipt_Details where challan_no='" + ddlAcptNo.SelectedItem.ToString() + "' and S_of_arrival='05' ; ";
                                instr += "Delete From tbl_Receipt_Details where challan_no='" + ddlAcptNo.SelectedItem.ToString() + "' and S_of_arrival='05' ;";
                                instr += "Update PaddyMilling_Agreement_2017 set Return_CommonRice= (ISNULL(Return_CommonRice,0)-" + RecdCMR + "), Return_TotalRice= (ISNULL(Return_TotalRice,0)-" + RecdCMR + "),Rem_DhanLot=(Rem_DhanLot-1) where Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "'; ";
                                instr += "Update CMR_DepositOrder set IsAccepted='N',IsRejected='N' where CMR_DO='" + hdfCMRDoNo.Value + "';";
                                instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                            }
                            else
                            {
                                instr += "Update PaddyMilling_DO_2017 Set DispatchDhan_IC='Y' where Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and DhanLot='" + hdfLotNo.Value + "' and Check_DO='" + hdfDO_NO.Value + "' ;";
                                instr += "Update PaddyMilling_Agreement_2017 set Rejected= (ISNULL(Rejected,0)-1) where Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "'; ";
                                instr += "Update CMR_DepositOrder set IsAccepted='N',IsRejected='N' where CMR_DO='" + hdfCMRDoNo.Value + "';";
                                instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                            }
                        }

                        cmd = new SqlCommand(instr, con);
                        int count = cmd.ExecuteNonQuery();

                        if (count > 0)
                        {
                            btnRecptSubmit.Enabled = false;
                            //txtYear.Text = "";
                            ddlCropyear.Enabled = false;
                            ddlMillName.Enabled = false;
                            ddlAgtmtNumber.Enabled = false;
                            ddlAgtmtNumber.Enabled = false;

                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Your Record Is Deleted Successfully'); </script> ");

                            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('!!!!Failed'); </script> ");
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

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/State/PaddyMillingHome.aspx");
        Response.Redirect("~/District/PaddyMillingHome.aspx");
    }
    
}