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

public partial class PaddyMilling_Del_RcptCMR_FCI : System.Web.UI.Page
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

    private void GetCropYearValues()
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

    private void GetDistName()
    {
        //string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "";

                //select = "SELECT district_name,district_code FROM pds.districtsmp where district_code='" + DistCode + "'";

                if (Session["st_id"].ToString() == "10")
                {
                    select = "SELECT district_name,district_code FROM pds.districtsmp where paddy_markfed='Y' Order By district_name";
                }
                else
                {
                    select = "SELECT district_name,district_code FROM pds.districtsmp Order By district_name";
                }

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
        txtLotNo.Text = txtDONo.Text = txtQty.Text = txtBags.Text = txtTruckNo.Text = txtRecdDist.Text = txtAcptNo.Text = txtAcptDate.Text = txtMemoNo.Text = txtMemoDate.Text = "";

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

        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";
                select = "Select distinct FCI.Mill_ID As MillCode,MR.Mill_Name As MillName From Receipt_CMR_FCI As FCI Left Join Miller_Registration MR ON(FCI.Mill_ID=MR.Registration_ID and FCI.CropYear=MR.CropYear) Where FCI.Paddy_AgrmtDist='" + ddlFrmDist.SelectedValue.ToString() + "' and FCI.CropYear='" + txtYear.Text + "' Order By MR.Mill_Name Asc";
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
        txtLotNo.Text = txtDONo.Text = txtQty.Text = txtBags.Text = txtTruckNo.Text = txtRecdDist.Text = txtAcptNo.Text = txtAcptDate.Text = txtMemoNo.Text = txtMemoDate.Text = "";

        using (con = new SqlConnection(strcon))
        {
            try
            {
                if (ddlMillName.SelectedIndex > 0)
                {
                    con.Open();

                    string select = "";
                    select = "Select distinct Agreement_ID From Receipt_CMR_FCI Where Paddy_AgrmtDist='" + ddlFrmDist.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' Order By Agreement_ID Asc";
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
        txtLotNo.Text = txtDONo.Text = txtQty.Text = txtBags.Text = txtTruckNo.Text = txtRecdDist.Text = txtAcptNo.Text = txtAcptDate.Text = txtMemoNo.Text = txtMemoDate.Text = "";

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

                string select = "Select Receipt_ID From Receipt_CMR_FCI Where Paddy_AgrmtDist='" + ddlFrmDist.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and IsAccepted='Y' Order By CAST(Lot_No AS Int)";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlAcptNo.DataSource = ds.Tables[0];
                    ddlAcptNo.DataTextField = "Receipt_ID";
                    ddlAcptNo.DataValueField = "Receipt_ID";
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
        txtLotNo.Text = txtDONo.Text = txtQty.Text = txtBags.Text = txtTruckNo.Text = txtRecdDist.Text = txtAcptNo.Text = txtAcptDate.Text = txtMemoNo.Text = txtMemoDate.Text = "";

        if (ddlAcptNo.SelectedIndex > 0)
        {
            GetAcptData();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select CMR Acceptance / Rejection Number'); </script> ");
        }
    }

    public void GetAcptData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "Select OS.district_name,CMR_RecdDist,Receipt_Date,CommonRice,Bags,Truck_No,Lot_No,DO_No,Acceptance_No,WeightCheck_No,WeightCheck_Date From Receipt_CMR_FCI Left Join OtherState_DistrictCode As OS ON(CMR_RecdState=OS.State_Id and CMR_RecdDist=OS.district_code) where Receipt_ID='" + ddlAcptNo.SelectedItem.ToString() + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DateTime CMRDate = DateTime.Parse(ds.Tables[0].Rows[0]["Receipt_Date"].ToString());
                    txtAcptDate.Text = CMRDate.ToString("dd/MMM/yyyy");

                    string CheckDate = ds.Tables[0].Rows[0]["WeightCheck_Date"].ToString();

                    if (CheckDate != "")
                    {
                        DateTime MemoDate = DateTime.Parse(ds.Tables[0].Rows[0]["WeightCheck_Date"].ToString());
                        txtMemoDate.Text = MemoDate.ToString("dd/MMM/yyyy");
                    }

                    txtRecdDist.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
                    txtQty.Text = ds.Tables[0].Rows[0]["CommonRice"].ToString();
                    txtBags.Text = ds.Tables[0].Rows[0]["Bags"].ToString();
                    txtTruckNo.Text = ds.Tables[0].Rows[0]["Truck_No"].ToString();
                    txtLotNo.Text = ds.Tables[0].Rows[0]["Lot_No"].ToString();
                    txtDONo.Text = ds.Tables[0].Rows[0]["DO_No"].ToString();
                    txtAcptNo.Text = ds.Tables[0].Rows[0]["Acceptance_No"].ToString();
                    txtMemoNo.Text = ds.Tables[0].Rows[0]["WeightCheck_No"].ToString();

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

                        decimal RecdCMR = decimal.Parse(txtQty.Text);

                        con.Open();

                        string instr = "";

                        {
                            instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                                "Insert Into ReceiptCMR_FCI_Log(Paddy_AgrmtDist,CMR_RecdDist,CMR_RecdState,Receipt_ID,Receipt_Date,CommonRice,GradeARice,Bags,Truck_No,Truck_No1,CropYear,Mill_ID,Milling_Type,Agreement_ID,Lot_No,DO_No,CreatedDate,IP_Address,IsAccepted,Acceptance_No,WeightCheck_No,WeightCheck_Date,BagType,WHR_No,DeletedDate,DeletedIP,Operation) Select Paddy_AgrmtDist,CMR_RecdDist,CMR_RecdState,Receipt_ID,Receipt_Date,CommonRice,GradeARice,Bags,Truck_No,Truck_No1,CropYear,Mill_ID,Milling_Type,Agreement_ID,Lot_No,DO_No,CreatedDate,IP_Address,IsAccepted,Acceptance_No,WeightCheck_No,WeightCheck_Date,BagType,WHR_No,GETDATE(),'" + GetIp + "','D' From  Receipt_CMR_FCI where Receipt_ID='" + ddlAcptNo.SelectedItem.ToString() + "' ;";
                            
                            instr += "Delete From Receipt_CMR_FCI Where Receipt_ID='" + ddlAcptNo.SelectedItem.ToString() + "' ;";

                            instr += "Update PaddyMilling_DO Set DispatchDhan_IC='N' where Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and CropYear='" + txtYear.Text + "' and DhanLot='" + txtLotNo.Text + "' and Check_DO='" + txtDONo.Text + "'; ";
                            
                            instr += "Insert Into PaddyMilling_Agreement_Log select District,Dist_Manager_Name,Mill_Addr_District,Mill_Name,Mill_Operator_Name,Mill_Operator,CropYear,From_Date,To_Date,Common_Dhan,GradeA_Dhan,Total_Dhan,Agreement_ID,IP_Address,Current_DateTime,User_Agent,State,State_Code,DepositMoney,DhanLot,DhanAmount,DhanAmountType,DhanAmountDetails,Agrmt_Date,R_Arva,R_Ushna,Rem_Common_Dhan,Rem_GradeA_Dhan,Rem_Total_Dhan,Rem_DhanLot,Milling_Type,Return_CommonDhan,Return_GradeADhan,Return_TotalDhan,Return_CommonRice,Return_GradeARice,Return_TotalRice,Rejected,IsAccepted,MobileNO,AcceptedIP,AcceptedDate,GETDATE(),'" + GetIp + "','U' From PaddyMilling_Agreement where Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' ; ";

                            instr += "Update PaddyMilling_Agreement set Return_CommonRice= (ISNULL(Return_CommonRice,0)-" + RecdCMR + "), Return_TotalRice= (ISNULL(Return_TotalRice,0)-" + RecdCMR + ") where Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' ; ";
                            
                            instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                        }

                        cmd = new SqlCommand(instr, con);
                        int count = cmd.ExecuteNonQuery();

                        if (count > 0)
                        {
                            btnRecptSubmit.Enabled = false;
                            txtYear.Text = "";
                            ddlMillName.Enabled = ddlAgtmtNumber.Enabled = ddlAcptNo.Enabled = ddlFrmDist.Enabled = false;

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
        if (Session["st_id"] != null)
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
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }
}