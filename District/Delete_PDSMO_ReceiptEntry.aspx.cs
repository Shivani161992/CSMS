using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class District_Delete_PDSMO_ReceiptEntry : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    SqlCommand cmd;

    string districtid = "", ActualFrmGodown = "";
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                txtFromDate.Text = txtToDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                rdbRoad.Checked = true;
                GetMPIssueCentre();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    public void GetMPIssueCentre()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = string.Format("select DepotName,DepotID from tbl_MetaData_DEPOT where DistrictId= '23" + Session["dist_id"].ToString() + "' order by DepotName");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlIssueCentre.DataSource = ds.Tables[0];
                    ddlIssueCentre.DataTextField = "DepotName";
                    ddlIssueCentre.DataValueField = "DepotID";
                    ddlIssueCentre.DataBind();
                    ddlIssueCentre.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके जिले में कोई भी प्रदाय केंद्र उपलब्ध नहीं है|'); </script> ");
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ddlDC.Items.Clear();
        btnDelete.Enabled = false;
        txtMONo.Text = txtCommodity.Text = txtTranspNO.Text = txtFrmDist.Text = txtToDist.Text = txtTCNo.Text = txtIssuedQty.Text = txtRecdGodown.Text = txtBags.Text = "";
        hdfDispMode.Value = hdfSMO.Value = hdfRMO.Value = "";
        ddlDC.Enabled = true;
        hdfWHRValue.Value = "0";

        if (ddlIssueCentre.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Issue Centre'); </script> ");
            return;
        }
        else if (txtFromDate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select From Date'); </script> ");
            return;
        }
        else if (txtToDate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select To Date'); </script> ");
            return;
        }
        else
        {
            ddlDC.Items.Clear();
            GetDCNumber();
        }
    }

    public void GetDCNumber()
    {
        RequiredFieldValidator5.Visible = RequiredFieldValidator6.Visible = false;
        districtid = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string fromdate = Request.Form[txtFromDate.UniqueID];
                txtFromDate.Text = fromdate;
                string todate = Request.Form[txtToDate.UniqueID];
                txtToDate.Text = todate;

                ConvertServerDate ServerDate = new ConvertServerDate();
                string ConvertFromDate = ServerDate.getDate_MDY(fromdate.ToString());
                string ConvertToDate = ServerDate.getDate_MDY(todate.ToString());

                string select = "";

                if (rdbRoad.Checked == true)
                {
                    select = "Select DC_MO,ReceiptID From ReceiptEntry_MO where CreatedDate between '" + ConvertFromDate + " 00:00:00' and '" + ConvertToDate + " 23:59:59' and ToDist='" + districtid + "' and ModeofDispatch='12' and RecIssue_Center='" + ddlIssueCentre.SelectedValue.ToString() + "' order by ReceiptID Desc";
                    da = new SqlDataAdapter(select, con);
                    ds = new DataSet();
                    da.Fill(ds);

                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            ddlDC.DataSource = ds.Tables[0];
                            ddlDC.DataTextField = "ReceiptID";
                            ddlDC.DataValueField = "DC_MO";
                            ddlDC.DataBind();
                            ddlDC.Items.Insert(0, "--Select--");

                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस दिनांक में By Road के लिए कोई भी Receipt Number उपलब्ध नहीं है|'); </script> ");
                            ddlDC.DataSource = "";
                            ddlDC.DataBind();
                        }
                    }
                }
                else
                {
                    select = "Select DC_MO,ReceiptID From ReceiptEntry_MO where CreatedDate between '" + ConvertFromDate + " 00:00:00' and '" + ConvertToDate + " 23:59:59' and ToDist='" + districtid + "' and ModeofDispatch='13' and RecIssue_Center='" + ddlIssueCentre.SelectedValue.ToString() + "' order by ReceiptID Desc";
                    da = new SqlDataAdapter(select, con);
                    ds = new DataSet();
                    da.Fill(ds);

                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            ddlDC.DataSource = ds.Tables[0];
                            ddlDC.DataTextField = "ReceiptID";
                            ddlDC.DataValueField = "DC_MO";
                            ddlDC.DataBind();
                            ddlDC.Items.Insert(0, "--Select--");
                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस दिनांक में By Rack के लिए कोई भी Receipt Number उपलब्ध नहीं है|'); </script> ");
                            ddlDC.DataSource = "";
                            ddlDC.DataBind();
                        }
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

    protected void ddlDC_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtMONo.Text = txtCommodity.Text = txtTranspNO.Text = txtFrmDist.Text = txtToDist.Text = txtTCNo.Text = txtIssuedQty.Text = txtRecdGodown.Text = txtBags.Text = "";
        hdfDispMode.Value = hdfSMO.Value = hdfRMO.Value = "";
        hdfWHRValue.Value = "0";

        if (ddlDC.SelectedIndex > 0)
        {
            CheckWHRData();
            if (hdfWHRValue.Value == "0")
            {
                GetAllDCData();
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपने जो नंबर Select किया है, उसके लिए WHR जारी किया जा चूका है, इसलिए आप इसे Delete नहीं कर सकते|'); </script> ");
                return;
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया डिलेवरी चालान क्रमांक चुने |'); </script> ");
            btnDelete.Enabled = false;
        }
    }

    public void CheckWHRData()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();

                string select = "Select * From tbl_Storage_Arrival_Stock where State_Id='23' and Challan_No='" + ddlDC.SelectedValue.ToString() + "' ";
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

    public void GetAllDCData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                String select = "";
                select = "Select  mvmtplan.RMO,RR.SMO,RR.ModeofDispatch,RR.MoveOrdernum,com.Commodity_Name,TO_No,MP.district_name,Depot.DepotName,(Case When RecChange_Godown='00' Then RecDefault_Godown Else RecChange_Godown End) As Godown,RecQty,RecdBags,RecTruck_No From ReceiptEntry_MO As RR Left Join tbl_MetaData_STORAGE_COMMODITY  As Com ON(Com.Commodity_Id=RR.Commodity) Left Join pds.districtsmp As MP ON(MP.district_code=RR.FrmDist) Left Join tbl_MetaData_DEPOT As Depot ON (Depot.DepotID=RR.SendingIssue_Center and Depot.DistrictId='23'+RR.FrmDist) Left Join RecAgainst_StateMovementOrder As MvmtPlan ON (RR.SMO=MvmtPlan.SMO and MvmtPlan.Issue_Center = RR.RecIssue_Center and MvmtPlan.Godown=(Case When RecChange_Godown='00' Then RecDefault_Godown Else RecChange_Godown End)) Where RR.ReceiptID='" + ddlDC.SelectedItem.ToString() + "' ";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtMONo.Text = ds.Tables[0].Rows[0]["MoveOrdernum"].ToString();
                    txtCommodity.Text = ds.Tables[0].Rows[0]["Commodity_Name"].ToString();
                    txtTranspNO.Text = ds.Tables[0].Rows[0]["TO_No"].ToString();
                    txtFrmDist.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
                    txtToDist.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                    txtTCNo.Text = ds.Tables[0].Rows[0]["RecTruck_No"].ToString();
                    txtIssuedQty.Text = ds.Tables[0].Rows[0]["RecQty"].ToString();
                    txtBags.Text = ds.Tables[0].Rows[0]["RecdBags"].ToString();
                    txtRecdGodown.Text = ds.Tables[0].Rows[0]["Godown"].ToString();
                    hdfDispMode.Value = ds.Tables[0].Rows[0]["ModeofDispatch"].ToString();
                    hdfSMO.Value = ds.Tables[0].Rows[0]["SMO"].ToString();
                    hdfRMO.Value = ds.Tables[0].Rows[0]["RMO"].ToString();

                    GetBG();
                    btnDelete.Enabled = true;
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('अन्य जानकारी उपलब्ध नहीं हैं|'); </script> ");
                    btnDelete.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                btnDelete.Enabled = false;
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

    public void GetBG()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                string select = "";
                con_MPStorage.Open();

                select = string.Format("select (Select Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + txtRecdGodown.Text + "') As RecddGodown_Name");
                da1 = new SqlDataAdapter(select, con_MPStorage);

                ds1 = new DataSet();
                da1.Fill(ds1);
                if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    txtRecdGodown.Text = ds1.Tables[0].Rows[0]["RecddGodown_Name"].ToString();
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (ddlDC.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Receipt Number'); </script> ");
            return;
        }
        else if (hdfWHRValue.Value != "0")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपने जो नंबर Select किया है, उसके लिए WHR जारी किया जा चूका है, इसलिए आप इसे Delete नहीं कर सकते|'); </script> ");
            return;
        }
        else if (txtIssuedQty.Text == "" || txtTCNo.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Recd. Qty Or Truck Number Is Not Allow Blank'); </script> ");
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

                        double RecdQty = 0;
                        RecdQty = double.Parse(txtIssuedQty.Text);

                        con.Open();

                        string instr = "";

                        instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION ";

                        if (hdfDispMode.Value == "12") //Received By Road
                        {
                            instr += "Insert Into StateMovementOrder_Trans_Log(MoveOrdernum,FrmDist,ToDist,Commodity,Quantity,CropYear,ReachDate,ModeofDispatch,SMS_todist,SMS_frmDist,IsIssued,CreatedDate,IP,SMO,RecAgainstHO,DispatchAgainstMO,ReceivedAgainstMO,RemQty,SubmitedQty,ModeofDist,DeletedDate,DeletedIP) select MoveOrdernum,FrmDist,ToDist,Commodity,Quantity,CropYear,ReachDate,ModeofDispatch,SMS_todist,SMS_frmDist,IsIssued,CreatedDate,IP,SMO,RecAgainstHO,DispatchAgainstMO,ReceivedAgainstMO,RemQty,SubmitedQty,ModeofDist,GETDATE(),'" + GetIp + "' From StateMovementOrder where SMO='" + hdfSMO.Value + "' ;";
                            instr += "Update StateMovementOrder set SubmitedQty = (" + RecdQty + " - (ISNULL(SubmitedQty,0))) where SMO='" + hdfSMO.Value + "' ; ";
                            instr += "Update RecAgainst_StateMovementOrder set SubmitedQty = (" + RecdQty + " - (ISNULL(SubmitedQty,0))), RemQty=RemQty+" + RecdQty + " where RMO='" + hdfRMO.Value + "' ; ";
                            instr += "Update DeliveryChallan_MO Set IsReceived='N',Recd_Qty='0',Recd_Bags='0',Recd_Date=''  Where SMO='" + hdfSMO.Value + "' and TO_No='" + txtTranspNO.Text + "' and DC_MO='" + ddlDC.SelectedValue.ToString() + "' and ModeofDispatch='12' ; ";
                            instr += "Insert Into SCSC_Truck_challan_Log(Dist_ID,Depot_Id,TO_Number,Challan_Date,Dispatch_Godown,Sendto_District,Sendto_IC,Commodity,Scheme,Bags,Qty_send,Challan_No,Truck_no,Transporter,Dispatch_Time,Remarks,Dispatch_id,IsDeposit,Month,Year,Created_date,Updated_Date,IP_Address,Source,State_Id,OperatorID,NoTransaction,DispatchType,DispatchID,Branchid,Cropyear) Select Dist_ID,Depot_Id,TO_Number,Challan_Date,Dispatch_Godown,Sendto_District,Sendto_IC,Commodity,Scheme,Bags,Qty_send,Challan_No,Truck_no,Transporter,Dispatch_Time,Remarks,Dispatch_id,IsDeposit,Month,Year,Created_date,Updated_Date,IP_Address,Source,State_Id,OperatorID,NoTransaction,DispatchType,DispatchID,Branchid,Cropyear From SCSC_Truck_challan where Challan_No='" + ddlDC.SelectedValue.ToString() + "' ; ";
                            instr += "Update SCSC_Truck_challan Set IsDeposit='N' where Challan_No='" + ddlDC.SelectedValue.ToString() + "';";
                            instr += "Insert Into ReceiptEntry_MO_Trans_Log(MoveOrdernum,SMO,TO_No,STO_No,DC_No,Transporter_ID,FrmDist,ToDist,Commodity,CropYear,TOEndDate,ModeofDispatch,DC_MO,ReceiptID,ToulReceiptNo,SendingIssue_Center,SendingBranch,SendingGodown,SendingQty,SendingBags,SendingDate,SendingBags_Type,SendingTruck_No,DC_RecIC,DC_RecBranch,DC_RecGodown,RecIssue_Center,RecDefault_Branch,RecDefault_Godown,RecChange_Branch,RecChange_Godown,RecQty,RecdBags,Variation_qty,Variation_Bags,RecDate,RecTruck_No,RecSArrival,CreatedDate,IP,OperatorID,DeletedDate,DeletedIP,Operation)  select MoveOrdernum,SMO,TO_No,STO_No,DC_No,Transporter_ID,FrmDist,ToDist,Commodity,CropYear,TOEndDate,ModeofDispatch,DC_MO,ReceiptID,ToulReceiptNo,SendingIssue_Center,SendingBranch,SendingGodown,SendingQty,SendingBags,SendingDate,SendingBags_Type,SendingTruck_No,DC_RecIC,DC_RecBranch,DC_RecGodown,RecIssue_Center,RecDefault_Branch,RecDefault_Godown,RecChange_Branch,RecChange_Godown,RecQty,RecdBags,Variation_qty,Variation_Bags,RecDate,RecTruck_No,RecSArrival,CreatedDate,IP,OperatorID,GETDATE(),'" + GetIp + "' ,'D' From ReceiptEntry_MO Where DC_MO='" + ddlDC.SelectedValue.ToString() + "'; ";
                            instr += "Delete ReceiptEntry_MO Where DC_MO='" + ddlDC.SelectedValue.ToString() + "'; ";
                            instr += "Insert Into tbl_Receipt_Details_log(Dist_Id,Depot_ID,Receipt_id,S_of_arrival,S_name,A_dist,A_Depo,RO_No,TO_Number,Dispatch_Date,arrival_date,challan_no,challan_date,Qty,Commodity,Scheme,Crop_year,Category,Transporter,Vehile_no,Arrival_time,Gunny_type,No_of_Bags,Recd_Qty,Recieved_Bags,Moisture,WCM_no,Variation_qty,Month,Year,IsDeposit,IP_Address,Created_date,Updated_date,Challan_Status,Godown,OperatorID,State_Id,NoTransaction,Orderno,Branch) Select Dist_Id,Depot_ID,Receipt_id,S_of_arrival,S_name,A_dist,A_Depo,RO_No,TO_Number,Dispatch_Date,arrival_date,challan_no,challan_date,Qty,Commodity,Scheme,Crop_year,Category,Transporter,Vehile_no,Arrival_time,Gunny_type,No_of_Bags,Recd_Qty,Recieved_Bags,Moisture,WCM_no,Variation_qty,Month,Year,IsDeposit,IP_Address,Created_date,Updated_date,Challan_Status,Godown,OperatorID,State_Id,NoTransaction,Orderno,Branch From tbl_Receipt_Details Where challan_no='" + ddlDC.SelectedValue.ToString() + "' ; ";
                            instr += "Delete tbl_Receipt_Details Where challan_no='" + ddlDC.SelectedValue.ToString() + "' ; ";
                            instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                        }
                        else if (hdfDispMode.Value == "13") //Received By Rack
                        {
                            instr += "Update RecAgainst_StateMovementOrder set SubmitedQty = (" + RecdQty + " - (ISNULL(SubmitedQty,0))), RemQty=RemQty+" + RecdQty + " where RMO='" + hdfRMO.Value + "' ; ";
                            instr += "Update DeliveryChallan_MO Set IsReceived='N',Recd_Qty='0',Recd_Bags='0',Recd_Date=''  Where SMO='" + hdfSMO.Value + "' and TO_No='" + txtTranspNO.Text + "' and DC_MO='" + ddlDC.SelectedValue.ToString() + "' and ModeofDispatch='13' ; ";
                            instr += "Insert Into ReceiptEntry_MO_Trans_Log(MoveOrdernum,SMO,TO_No,STO_No,DC_No,Transporter_ID,FrmDist,ToDist,Commodity,CropYear,TOEndDate,ModeofDispatch,DC_MO,ReceiptID,ToulReceiptNo,SendingIssue_Center,SendingBranch,SendingGodown,SendingQty,SendingBags,SendingDate,SendingBags_Type,SendingTruck_No,DC_RecIC,DC_RecBranch,DC_RecGodown,RecIssue_Center,RecDefault_Branch,RecDefault_Godown,RecChange_Branch,RecChange_Godown,RecQty,RecdBags,Variation_qty,Variation_Bags,RecDate,RecTruck_No,RecSArrival,CreatedDate,IP,OperatorID,DeletedDate,DeletedIP,Operation)  select MoveOrdernum,SMO,TO_No,STO_No,DC_No,Transporter_ID,FrmDist,ToDist,Commodity,CropYear,TOEndDate,ModeofDispatch,DC_MO,ReceiptID,ToulReceiptNo,SendingIssue_Center,SendingBranch,SendingGodown,SendingQty,SendingBags,SendingDate,SendingBags_Type,SendingTruck_No,DC_RecIC,DC_RecBranch,DC_RecGodown,RecIssue_Center,RecDefault_Branch,RecDefault_Godown,RecChange_Branch,RecChange_Godown,RecQty,RecdBags,Variation_qty,Variation_Bags,RecDate,RecTruck_No,RecSArrival,CreatedDate,IP,OperatorID,GETDATE(),'" + GetIp + "' ,'D' From ReceiptEntry_MO Where DC_MO='" + ddlDC.SelectedValue.ToString() + "'; ";
                            instr += "Delete ReceiptEntry_MO Where DC_MO='" + ddlDC.SelectedValue.ToString() + "'; ";
                            instr += "Insert Into RR_receipt_Depot_log(district_code,DepotID,Rack_No,S_RailHead,TC_Number,TC_date,Transporter_ID,Truck_No,Trans_ID,Disp_Bags,Disp_Qty,Recd_Bags,Recd_Qty,Month,Year,Created_date,Updated_date,Ip_Address,Challan_st,Godown,Commodity,Scheme,State_Id,OperatorID,NoTransaction,Cropyear)  select district_code,DepotID,Rack_No,S_RailHead,TC_Number,TC_date,Transporter_ID,Truck_No,Trans_ID,Disp_Bags,Disp_Qty,Recd_Bags,Recd_Qty,Month,Year,Created_date,Updated_date,Ip_Address,Challan_st,Godown,Commodity,Scheme,State_Id,OperatorID,NoTransaction,Cropyear From RR_receipt_Depot where TC_Number='" + ddlDC.SelectedValue.ToString() + "' ;";
                            instr += "Delete RR_receipt_Depot where TC_Number='" + ddlDC.SelectedValue.ToString() + "' ;";
                            instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                        }

                        cmd = new SqlCommand(instr, con);
                        int count = cmd.ExecuteNonQuery();

                        if (count > 0)
                        {
                            btnDelete.Enabled = false;
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Deleted Successfully'); </script> ");
                            txtMONo.Text = txtCommodity.Text = txtTranspNO.Text = "";
                            ddlDC.Enabled = false;
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
        Response.Redirect("~/District/MovementOrderHome.aspx");
    }
    protected void rdbRoad_CheckedChanged(object sender, EventArgs e)
    {
        btnDelete.Enabled = false;
        ddlIssueCentre.SelectedIndex = 0;
        ddlDC.Items.Clear();
        txtMONo.Text = txtCommodity.Text = txtTranspNO.Text = txtFrmDist.Text = txtToDist.Text = txtTCNo.Text = txtIssuedQty.Text = txtRecdGodown.Text = txtBags.Text = "";
    }
    protected void rdbRack_CheckedChanged(object sender, EventArgs e)
    {
        btnDelete.Enabled = false;
        ddlIssueCentre.SelectedIndex = 0;
        ddlDC.Items.Clear();
        txtMONo.Text = txtCommodity.Text = txtTranspNO.Text = txtFrmDist.Text = txtToDist.Text = txtTCNo.Text = txtIssuedQty.Text = txtRecdGodown.Text = txtBags.Text = "";
    }
    protected void ddlIssueCentre_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnDelete.Enabled = false;
        ddlDC.Items.Clear();
        txtMONo.Text = txtCommodity.Text = txtTranspNO.Text = txtFrmDist.Text = txtToDist.Text = txtTCNo.Text = txtIssuedQty.Text = txtRecdGodown.Text = txtBags.Text = "";
    }
}