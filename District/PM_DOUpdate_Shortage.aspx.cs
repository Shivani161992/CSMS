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

public partial class PaddyMilling_PM_DOUpdate_Shortage : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                Session["ICGBQ"] = null;
                GridView1.DataSource = "";
                GridView1.DataBind();

                GetCropYearValues();
                GetDistName();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

        txtDODate.Text = Request.Form[txtDODate.UniqueID];
        txtDOLastDate.Text = Request.Form[txtDOLastDate.UniqueID];
    }

    // void Page_PreInit(object sender, EventArgs e)
    //{
    //    if (Session["dist_id"] != null)
     //   {
           // if (Session["dist_id"].ToString() == "10")
           // {
           //     this.MasterPageFile = "~/MasterPage/Markfed_PDY.master";
           // }
      //  }
      //  else
      //  {
      //      Response.Redirect("~/MainLogin.aspx");
//}
   // }

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
                   // select = "SELECT district_name,district_code FROM pds.districtsmp where paddy_markfed='Y' Order By district_name";
              //  }
               // else
               // {
                    select = "SELECT district_name,district_code FROM pds.districtsmp Order By district_name";
                //}
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
        ClientIP objClientIP = new ClientIP();
        string GetIp = (objClientIP.GETIP());

        ConvertServerDate ServerDate = new ConvertServerDate();
        string ConvertFromDate = ServerDate.getDate_MDY(txtDODate.Text);
        string ConvertToDate = ServerDate.getDate_MDY(txtDOLastDate.Text);

        if (txtYear.Text != "")
        {
            if (Session["update"].ToString() == ViewState["update"].ToString())
            {
                using (con = new SqlConnection(strcon))
                {
                    try
                    {
                        con.Open();

                        string instr = "";

                        if (chkDate.Checked == true)
                        {
                            instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION ";
                            instr += "Insert Into PaddyMilling_DO_Log_2017 select *,GETDATE(),'" + GetIp + "','U' From PaddyMilling_DO_2017 where Check_DO='" + ddlDONo.SelectedItem.ToString() + "' ; ";
                            instr += "Update PaddyMilling_DO_2017 set From_Date='" + ConvertFromDate + "',To_Date='" + ConvertToDate + "' where Check_DO='" + ddlDONo.SelectedItem.ToString() + "' ;";
                            instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";

                            ddlMillName.Enabled = ddlAgtmtNumber.Enabled = ddlFrmDist.Enabled = ddlDONo.Enabled = false;
                        }
                        else if (chkGodam.Checked == true)
                        {
                            instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION ";

                            instr += "Insert Into PaddyMilling_DO_Log_2017 select *,GETDATE(),'" + GetIp + "','U' From PaddyMilling_DO_2017 where Check_DO='" + ddlDONo.SelectedItem.ToString() + "' ; ";

                            for (int i = 0; i < GridView2.Rows.Count; i++)
                            {
                                string RemAllotedPaddy = GridView2.Rows[i].Cells[4].Text;
                                string MappingNo = GridView2.Rows[i].Cells[7].Text;
                                instr += "Update PaddyMilling_DO_2017 Set Alloted_CommonDhan='" + RemAllotedPaddy + "' where DO_Number='" + MappingNo + "' ;";
                            }

                            instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                        }
                        else
                        {
                            if (GridView1.Rows.Count < 0)
                            {
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आप इसे Update नहीं कर सकते, आपको कम से कम किसी एक गोदाम में Qty Add करनी होगी|'); </script> ");
                                return;
                            }
                            else
                            {
                                string ChechMax = "";
                                decimal MaxMappingNo = 0;
                                ChechMax = "Select Max(DO_Number) As MaxMap From PaddyMilling_DO_2017 Where Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and Mill_Code='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and District='" + ddlFrmDist.SelectedValue.ToString() + "' and Check_DO='" + ddlDONo.SelectedItem.ToString() + "'";
                                da = new SqlDataAdapter(ChechMax, con);
                                ds = new DataSet();
                                da.Fill(ds);

                                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                {
                                    MaxMappingNo = decimal.Parse(ds.Tables[0].Rows[0]["MaxMap"].ToString());
                                }

                                if (MaxMappingNo != 0)
                                {
                                    int LotNo = 0;

                                    instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION ";

                                    instr += "Insert Into PaddyMilling_DO_Log_2017 ([DO_Number] ,[Mill_Code] ,[Agreement_ID]  ,[District]   ,[CropYear]  ,[From_Date]  ,[To_Date]  ,[Milling_Type]  ,[Alloted_CommonDhan] ,[Alloted_GradeADhan] ,[R_Arva]  ,[R_Ushna],[Issue_Centre] ,[IP_Address] ,[Current_DateTime] ,[User_Agent]  ,[Check_DO],[DispatchDhan_IC]  ,[Return_CommonRice] ,[Return_GradeARice]  ,[Return_TotalRice]  ,[LotOnlyNumber] ,[Rejected]  ,[DhanLot] ,[Branch_Code],[Godown_Code]  ,[LotNumber] ,[Rem_Alloted_CommonDhan],[Rem_Alloted_GradeADhan] ,[Rem_DhanLot]  ,[LotNo] ,[DeletedDate]  ,[DeletedIP]   ,[Operation]  ,[IsSociety] ,[SocietyDist]  ,[SocietyID] ,[IsGodown]) select [DO_Number] ,[Mill_Code] ,[Agreement_ID]  ,[District]   ,[CropYear]  ,[From_Date]  ,[To_Date]  ,[Milling_Type]  ,[Alloted_CommonDhan] ,[Alloted_GradeADhan] ,[R_Arva]  ,[R_Ushna],[Issue_Centre] ,[IP_Address] ,[Current_DateTime] ,[User_Agent]  ,[Check_DO],[DispatchDhan_IC]  ,[Return_CommonRice] ,[Return_GradeARice]  ,[Return_TotalRice]  ,[LotOnlyNumber] ,[Rejected]  ,[DhanLot] ,[Branch_Code],[Godown_Code]  ,[LotNumber] ,[Rem_Alloted_CommonDhan],[Rem_Alloted_GradeADhan] ,[Rem_DhanLot]  ,[LotNo] ,GETDATE(),'" + GetIp + "'   ,'U' ,[IsSociety] ,[SocietyDist]  ,[SocietyID] ,[IsGodown] From PaddyMilling_DO_2017 where Check_DO='" + ddlDONo.SelectedItem.ToString() + "' ; ";

                                    for (int i = 0; i < GridView2.Rows.Count; i++)
                                    {
                                        LotNo = LotNo + 1;
                                        string Alloted_CommonDhan = GridView2.Rows[i].Cells[4].Text;
                                        string MappingNo = GridView2.Rows[i].Cells[7].Text;
                                        instr += "Update PaddyMilling_DO_2017 Set Alloted_CommonDhan='" + Alloted_CommonDhan + "',From_Date='" + ConvertFromDate + "',To_Date='" + ConvertToDate + "' where DO_Number='" + MappingNo + "' ;";
                                    }

                                    DataTable dt = adddetails();
                                    if (dt != null)
                                    {
                                        for (int i = 0; i < dt.Rows.Count; i++)
                                        {
                                            MaxMappingNo = MaxMappingNo + 1;
                                            LotNo = LotNo + 1;
                                            string FinalLotNO = "Lot" + hdfDhanLot.Value + "/" + (LotNo).ToString();

                                            instr += "Insert into PaddyMilling_DO_2017(DO_Number,Mill_Code,Agreement_ID,District,CropYear,From_Date,To_Date,Milling_Type,Alloted_CommonDhan,Alloted_GradeADhan,R_Arva,R_Ushna,Issue_Centre,IP_Address,Current_DateTime,User_Agent,Check_DO,DispatchDhan_IC,DhanLot,Branch_Code,Godown_Code,LotNumber,Rem_Alloted_CommonDhan,Rem_Alloted_GradeADhan,LotNo) values('" + MaxMappingNo + "','" + ddlMillName.SelectedValue.ToString() + "','" + ddlAgtmtNumber.SelectedItem.ToString() + "','" + ddlFrmDist.SelectedValue.ToString() + "','" + txtYear.Text + "','" + ConvertFromDate + "','" + ConvertToDate + "',N'" + hdfMillingType.Value + "','" + dt.Rows[i]["QuantityQtls"] + "','0','0','0','" + dt.Rows[i]["ICVal"] + "','" + GetIp + "',GETDATE(),'HO','" + ddlDONo.SelectedValue.ToString() + "','N','" + hdfDhanLot.Value + "','0','" + dt.Rows[i]["GodownVal"] + "','" + MaxMappingNo + "','" + hdfRem_Alloted_CommonDhan.Value + "','0','" + FinalLotNO + "') ;";
                                        }
                                    }

                                    instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                                }
                                else
                                {
                                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('!!!!Failed'); </script> ");
                                    return;
                                }
                            }
                        }

                        cmd = new SqlCommand(instr, con);
                        int count = cmd.ExecuteNonQuery();

                        if (count > 0)
                        {
                            btnRecptSubmit.Enabled = btnAdd.Enabled = false;
                            Session["ICGBQ"] = null;
                            ddlIssueCentre.Items.Clear();
                            ddlBranch.Items.Clear();
                            ddlGodown.Items.Clear();
                            chkDate.Visible = false;

                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Updated Successfully'); </script> ");
                            txtYear.Text = "";
                            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Sorry, Updation Not Allow'); </script> ");
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

    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            //if (Session["st_id"].ToString() == "10")
            //{
            //    Response.Redirect("~/State/PaddyMillingHome_MFD.aspx");
            //}
            //else
           // {
                Response.Redirect("~/State/PaddyMillingHome.aspx");
           // }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    protected void ddlFrmDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMillName.Items.Clear();
        ddlAgtmtNumber.Items.Clear();
        ddlIssueCentre.Items.Clear();
        ddlDONo.Items.Clear();
        ddlBranch.Items.Clear();
        ddlGodown.Items.Clear();
        txtGodownRemQty.Text = txtDORemQty.Text = txtQty.Text = txtDODate.Text = txtDOLastDate.Text = "";
        hdfSelectedGodam.Value = "";
        chkDate.Visible = lblmsg.Visible = false;
        chkDate.Checked = false;

        chkChange.Checked = false;
        chkChange.Visible = false;
        trMsg.Visible = false;
        chkGodam.Checked = false;
        chkGodam.Visible = false;

        GridView2.DataSource = "";
        GridView2.DataBind();

        if (ddlFrmDist.SelectedIndex > 0)
        {
            GetMillName();
            GetOnlyDistName();
            //GetMPIssueCentre();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select District'); </script> ");
            return;
        }
    }

    public void GetOnlyDistName()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "SELECT district_name,district_code FROM pds.districtsmp where district_code!='" + ddlFrmDist.SelectedValue.ToString() + "' Order By district_name";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlFrmDist1.DataSource = ds.Tables[0];
                        ddlFrmDist1.DataTextField = "district_name";
                        ddlFrmDist1.DataValueField = "district_code";
                        ddlFrmDist1.DataBind();
                        ddlFrmDist1.Items.Insert(0, "--Select--");
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

    public void GetMillName()
    {
        ddlMillName.Items.Clear();

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
        //ddlIssueCentre.SelectedIndex = 0;
        ddlIssueCentre.Items.Clear();
        ddlBranch.Items.Clear();
        ddlGodown.Items.Clear();
        ddlDONo.Items.Clear();
        chkDate.Visible = lblmsg.Visible = false;
        chkDate.Checked = false;
        chkChange.Checked = false;
        chkChange.Visible = false;
        trMsg.Visible = false;
        chkGodam.Checked = false;
        chkGodam.Visible = false;
        txtGodownRemQty.Text = txtDORemQty.Text = txtQty.Text = txtDODate.Text = txtDOLastDate.Text = "";
        hdfSelectedGodam.Value = "";
        GridView2.DataSource = "";
        GridView2.DataBind();

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
        //ddlIssueCentre.SelectedIndex = 0;
        ddlIssueCentre.Items.Clear();
        ddlBranch.Items.Clear();
        ddlGodown.Items.Clear();
        ddlDONo.Items.Clear();
        txtGodownRemQty.Text = txtDORemQty.Text = txtQty.Text = txtDODate.Text = txtDOLastDate.Text = "";
        hdfSelectedGodam.Value = "";
        chkDate.Visible = lblmsg.Visible = false;
        chkDate.Checked = false;
        chkChange.Checked = false;
        chkChange.Visible = false;
        trMsg.Visible = false;
        chkGodam.Checked = false;
        chkGodam.Visible = false;

        GridView2.DataSource = "";
        GridView2.DataBind();

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

                string select = "Select distinct Check_DO From PaddyMilling_DO_2017 where Mill_Code='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedValue.ToString() + "' and District='" + ddlFrmDist.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlDONo.DataSource = ds.Tables[0];
                    ddlDONo.DataTextField = "Check_DO";
                    ddlDONo.DataValueField = "Check_DO";
                    ddlDONo.DataBind();
                    ddlDONo.Items.Insert(0, "--Select--");
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


    protected void ddlDONo_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlIssueCentre.SelectedIndex = 0;
        ddlIssueCentre.Items.Clear();
        ddlBranch.Items.Clear();
        ddlGodown.Items.Clear();
        chkDate.Visible = lblmsg.Visible = false;
        chkDate.Checked = false;
        chkChange.Checked = false;
        chkChange.Visible = false;
        trMsg.Visible = false;
        chkGodam.Checked = false;
        chkGodam.Visible = false;

        txtGodownRemQty.Text = txtDORemQty.Text = txtQty.Text = txtDODate.Text = txtDOLastDate.Text = "";
        hdfSelectedGodam.Value = "";

        GridView2.DataSource = "";
        GridView2.DataBind();

        if (ddlDONo.SelectedIndex > 0)
        {
            GetMPIssueCentre();
            GetDOData();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select DO Number'); </script> ");
        }
    }

    public void GetDOData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select Rem_Alloted_CommonDhan,DhanLot,DO_Number,Milling_Type,Alloted_CommonDhan,Issue_Centre,Godown_Code,ROUND((Alloted_CommonDhan-(ISNULL(Return_CommonRice,0))),2) As RemDOQty,LotNo,From_Date,To_Date From PaddyMilling_DO_2017 where Mill_Code='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedValue.ToString() + "' and District='" + ddlFrmDist.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and Check_DO='" + ddlDONo.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Decimal DOQty = 0, DORemQty = 0;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //DOQty += decimal.Parse(ds.Tables[0].Rows[i]["Alloted_CommonDhan"].ToString());
                        DORemQty += decimal.Parse(ds.Tables[0].Rows[i]["RemDOQty"].ToString());
                    }

                    if (DORemQty >= 1)
                    {
                        chkDate.Visible = lblmsg.Visible = chkGodam.Visible = true;
                    }
                    else
                    {
                        chkDate.Visible = lblmsg.Visible = chkGodam.Visible = false;
                    }

                    //if (DOQty != DORemQty)
                    //{
                    //    chkDate.Visible = false;
                    //}
                    //else
                    //{
                    //    chkDate.Visible = true;
                    //    chkDate.Checked = false;
                    //}

                    chkChange.Visible = true;
                    DateTime DODate = DateTime.Parse(ds.Tables[0].Rows[0]["From_Date"].ToString());
                    txtDODate.Text = DODate.ToString("dd-MM-yyyy");

                    DateTime DOLastDate = DateTime.Parse(ds.Tables[0].Rows[0]["To_Date"].ToString());
                    txtDOLastDate.Text = DOLastDate.ToString("dd-MM-yyyy");

                    hdfMillingType.Value = ds.Tables[0].Rows[0]["Milling_Type"].ToString();
                    hdfDhanLot.Value = ds.Tables[0].Rows[0]["DhanLot"].ToString();
                    hdfRem_Alloted_CommonDhan.Value = ds.Tables[0].Rows[0]["Rem_Alloted_CommonDhan"].ToString();

                    GridView2.DataSource = ds.Tables[0];
                    GridView2.DataBind();
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

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string GodownName = "", ICName = "";
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();

            GodownName = e.Row.Cells[3].Text;
            ICName = e.Row.Cells[2].Text;

            using (con_MPStorage = new SqlConnection(strcon_MPStorage))
            {
                try
                {
                    con_MPStorage.Open();
                    string select = string.Format("select (select DepotName from tbl_MetaData_DEPOT where DepotId='" + ICName + "') As ICName,(select Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + GodownName + "') As Godown_Name");
                    da = new SqlDataAdapter(select, con_MPStorage);

                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            e.Row.Cells[3].Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
                            e.Row.Cells[2].Text = ds.Tables[0].Rows[0]["ICName"].ToString();
                        }
                    }
                    else
                    {

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

    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdfIndex.Value = hdfSelectedGodam.Value = "";
        txtDORemQty.Text = "";
        txtDORemQty.Text = decimal.Parse(GridView2.Rows[GridView2.SelectedIndex].Cells[5].Text).ToString();
        hdfIndex.Value = int.Parse(GridView2.Rows[GridView2.SelectedIndex].Cells[0].Text).ToString();
        hdfSelectedGodam.Value = decimal.Parse(GridView2.Rows[GridView2.SelectedIndex].Cells[6].Text).ToString();

        if (ddlIssueCentre.Items.Count > 0)
        {
            ddlIssueCentre.SelectedIndex = 0;
        }

        ddlBranch.Items.Clear();
        ddlGodown.Items.Clear();
        txtGodownRemQty.Text = "";

        foreach (GridViewRow row in GridView2.Rows)
        {
            if (row.RowIndex == GridView2.SelectedIndex)
            {
                row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
            }
            else
            {
                row.BackColor = ColorTranslator.FromHtml("#FFFBD6");
            }
        }
    }

    public void GetMPIssueCentre()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "select DepotName,DepotID from tbl_MetaData_DEPOT where DistrictId= '23" + ddlFrmDist.SelectedValue.ToString() + "' order by DepotName";
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

    protected void ddlIssueCentre_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Items.Clear();
        ddlGodown.Items.Clear();
        txtGodownRemQty.Text = txtQty.Text = "";
        hdfGodown.Value = "";

        if (ddlIssueCentre.SelectedIndex > 0)
        {
            GetBranch();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Issue Center'); </script> ");
        }
    }

    public void GetBranch()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select tbl_MetaData_DEPOT.DepotName,tbl_MetaData_DEPOT.BranchID from MetaDataBranchWithIssueCenter inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.BranchId=MetaDataBranchWithIssueCenter.BranchID where IssueCenterId='" + ddlIssueCentre.SelectedValue.ToString() + "'");
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlBranch.DataSource = ds.Tables[0];
                        ddlBranch.DataTextField = "DepotName";
                        ddlBranch.DataValueField = "BranchID";
                        ddlBranch.DataBind();
                        ddlBranch.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Issue Center'); </script> ");
                    }
                }
                else
                {
                    string select1 = string.Format("select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId= '23" + ddlFrmDist.SelectedValue.ToString() + "' order by DepotName");
                    da = new SqlDataAdapter(select1, con_MPStorage);

                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlBranch.DataSource = ds.Tables[0];
                        ddlBranch.DataTextField = "DepotName";
                        ddlBranch.DataValueField = "BranchId";
                        ddlBranch.DataBind();
                        ddlBranch.Items.Insert(0, "--Select--");
                    }
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

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlGodown.Items.Clear();
        txtGodownRemQty.Text = txtQty.Text = "";
        hdfGodown.Value = "";

        if (ddlBranch.SelectedIndex > 0)
        {
            if (chkChange.Checked)
            {
                GETMappingGodown(); //For Other Dist Paddy Godown
                if (hdfGodown.Value != "")
                {
                    OnlyMappedGodown();
                }
            }
            else
            {
                GetGodown();
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Branch'); </script> ");
        }
    }

    public void GETMappingGodown()
    {
        hdfGodown.Value = "";

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "Select Godown_id From PM_DistToGodown Where FrmDistrict='" + ddlFrmDist.SelectedValue.ToString() + "' and ToDistrict='" + ddlFrmDist1.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' ";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        hdfGodown.Value += ((hdfGodown.Value == "") ? "" : " , ") + "'" + ds.Tables[0].Rows[i]["Godown_id"].ToString() + "'";
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया मुख्यालय से गोदाम की मैपिंग कीजिये|'); </script> ");
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

    public void OnlyMappedGodown()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where Remarks='Y' and BranchID='" + ddlBranch.SelectedValue.ToString() + "' and Godown_ID IN(" + hdfGodown.Value + ") order by Godown_Name");
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlGodown.DataSource = ds.Tables[0];
                        ddlGodown.DataTextField = "Godown_Name";
                        ddlGodown.DataValueField = "Godown_ID";
                        ddlGodown.DataBind();
                        ddlGodown.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया मुख्यालय से गोदाम की मैपिंग कीजिये|'); </script> ");
                    }
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

    public void GetGodown()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where Remarks='Y' and BranchID='" + ddlBranch.SelectedValue.ToString() + "' order by Godown_Name");
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlGodown.DataSource = ds.Tables[0];
                        ddlGodown.DataTextField = "Godown_Name";
                        ddlGodown.DataValueField = "Godown_ID";
                        ddlGodown.DataBind();
                        ddlGodown.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Branch Name'); </script> ");
                    }
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

    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtGodownRemQty.Text = txtQty.Text = "";
        txtQty.Enabled = false;

        if (ddlGodown.SelectedIndex > 0)
        {
            GetGodownBalance();
            txtQty.Text = txtDORemQty.Text;
            txtQty.Enabled = true;
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown'); </script> ");
        }
    }

    public void GetGodownBalance()
    {
       // string pqry = "space_forcommodity_ingodown";
        string pqry = "space_forcommodity_ingodown_2017";
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                SqlCommand cmdpqty = new SqlCommand(pqry, con);
                cmdpqty.CommandType = CommandType.StoredProcedure;

                cmdpqty.Parameters.Add("@district_code", SqlDbType.NVarChar).Value = ddlFrmDist.SelectedValue.ToString();
                cmdpqty.Parameters.Add("@Depotid", SqlDbType.NVarChar).Value = ddlIssueCentre.SelectedValue.ToString();
                cmdpqty.Parameters.Add("@GodownId", SqlDbType.NVarChar).Value = ddlGodown.SelectedValue.ToString();
                cmdpqty.Parameters.Add("@commodity", SqlDbType.NVarChar).Value = "13";  //Paddy-Common
                cmdpqty.Parameters.Add("@Openingdate", SqlDbType.NVarChar).Value = "01/01/2015";

                DataSet ds = new DataSet();
                SqlDataAdapter dr = new SqlDataAdapter(cmdpqty);
                dr.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    double stock = Math.Round(Convert.ToDouble(ds.Tables[0].Rows[0]["Total"].ToString()), 5);

                    txtGodownRemQty.Text = Convert.ToString(stock);
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtDORemQty.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Mapping की गयी मात्रा में से किसी एक मात्रा का चुनाव करे|'); </script> ");
            return;
        }

        double DORemQty = double.Parse(txtDORemQty.Text);
        double AllotedQty = double.Parse(txtQty.Text);

        if (ddlAgtmtNumber.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('कृपया अनुबंध नंबर चुने|'); </script> ");
            return;
        }
        if (ddlGodown.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('कृपया गोदाम चुने|'); </script> ");
            return;
        }
        else if (AllotedQty == 0.0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Do Not Allow 0'); </script> ");
            return;
        }
        else if (txtQty.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('कृपया Quantity इंटर करे|'); </script> ");
            return;
        }
        else if (DORemQty < AllotedQty)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('DO के आधार पर बची हुई मात्रा के अनुसार ही धान का आबंटन करे|'); </script> ");
            return;
        }
        else
        {
            chkDate.Checked = false;
            chkDate.Visible = false;

            chkGodam.Enabled = chkChange.Enabled = false;

            if (chkGodam.Checked == true)
            {
                int checkDupGodown = 0;

                if (hdfSelectedGodam.Value == ddlGodown.SelectedValue.ToString())
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('आप उस गोदाम का चुनाव नहीं कर सकते, जिस गोदाम को आपने Select किया हैं|'); </script> ");
                    return;
                }

                for (int i = 0; i < GridView2.Rows.Count; i++)
                {
                    string GodownCode = GridView2.Rows[i].Cells[6].Text;
                    if (GodownCode == ddlGodown.SelectedValue.ToString())
                    {
                        checkDupGodown = 1;
                        break;
                    }
                }

                if (checkDupGodown != 0)
                {
                    ddlMillName.Enabled = ddlAgtmtNumber.Enabled = ddlFrmDist.Enabled = ddlDONo.Enabled = false;

                    int index = (int.Parse(hdfIndex.Value)) - 1;
                    txtDORemQty.Text = txtQty.Text = GridView2.Rows[index].Cells[5].Text = (DORemQty - AllotedQty).ToString();

                    double MappedQty = 0;
                    MappedQty = double.Parse(GridView2.Rows[index].Cells[4].Text);
                    GridView2.Rows[index].Cells[4].Text = (MappedQty - AllotedQty).ToString();

                    for (int i = 0; i < GridView2.Rows.Count; i++)
                    {
                        string GodownCode1 = GridView2.Rows[i].Cells[6].Text;
                        if (GodownCode1 == ddlGodown.SelectedValue.ToString())
                        {
                            double LastQty = 0, LastMappedQty = 0;
                            LastQty = double.Parse(GridView2.Rows[i].Cells[5].Text);
                            LastMappedQty = double.Parse(GridView2.Rows[i].Cells[4].Text);

                            GridView2.Rows[i].Cells[5].Text = (LastQty + AllotedQty).ToString();
                            GridView2.Rows[i].Cells[4].Text = (LastMappedQty + AllotedQty).ToString();
                            break;
                        }
                    }

                    btnRecptSubmit.Enabled = true;
                    txtQty.Focus();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('आप केवल उसी गोदाम का चुनाव कर सकते, जिस गोदाम की आपने पहले से Mapping की हैं|'); </script> ");
                    return;
                }
            }
            else
            {
                DataTable dt = adddetails();
                if (dt == null)
                {
                    dt = new DataTable("aadqty");
                    dt.Columns.Add("ICName");
                    dt.Columns.Add("ICVal");
                    dt.Columns.Add("GodownName");
                    dt.Columns.Add("GodownVal");
                    dt.Columns.Add("Quantity");
                    dt.Columns.Add("QuantityQtls");
                }

                int count = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (ddlGodown.SelectedValue.ToString() == dt.Rows[i]["GodownVal"].ToString())
                    {
                        count = 1;
                        break;
                    }
                }

                if (count == 0)
                {
                    int checkDupGodown = 0;

                    for (int i = 0; i < GridView2.Rows.Count; i++)
                    {
                        string GodownCode = GridView2.Rows[i].Cells[6].Text;
                        if (GodownCode == ddlGodown.SelectedValue.ToString())
                        {
                            checkDupGodown = 1;
                            break;
                        }
                    }

                    if (checkDupGodown == 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr["ICName"] = ddlIssueCentre.SelectedItem.Text;
                        dr["ICVal"] = ddlIssueCentre.SelectedValue;
                        dr["GodownName"] = ddlGodown.SelectedItem.Text;
                        dr["GodownVal"] = ddlGodown.SelectedValue;
                        dr["Quantity"] = dr["QuantityQtls"] = txtQty.Text;

                        dr["quantity"] = (AllotedQty).ToString("0.00");
                        dt.Rows.Add(dr);
                        Session["ICGBQ"] = dt;
                        ddlMillName.Enabled = ddlAgtmtNumber.Enabled = ddlFrmDist.Enabled = ddlDONo.Enabled = false;
                        fillgrid();

                        int index = (int.Parse(hdfIndex.Value)) - 1;
                        txtDORemQty.Text = txtQty.Text = GridView2.Rows[index].Cells[5].Text = (DORemQty - AllotedQty).ToString();
                        GridView2.Rows[index].Cells[4].Text = ((double.Parse(GridView2.Rows[index].Cells[4].Text)) - AllotedQty).ToString();

                        btnRecptSubmit.Enabled = true;
                        txtQty.Focus();
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('आप उस गोदाम का चुनाव नहीं कर सकते, जिस गोदाम की आपने पहले से Mapping की हैं|'); </script> ");
                        return;
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Duplicate Godown Is Not Allowed'); </script> ");
                    return;
                }
            }
        }
    }

    public DataTable adddetails()
    {
        DataTable dt = (DataTable)Session["ICGBQ"];
        return dt;
    }

    public void fillgrid()
    {
        DataTable dt = adddetails();
        if (dt == null)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
        }
    }

    protected void chkDate_CheckedChanged(object sender, EventArgs e)
    {
        chkGodam.Checked = chkChange.Checked = false;

        if (chkDate.Checked == true)
        {
            btnRecptSubmit.Enabled = true;
            ddlIssueCentre.SelectedIndex = 0;
            ddlGodown.Items.Clear();
            txtGodownRemQty.Text = txtDORemQty.Text = txtQty.Text = "0";
            btnAdd.Visible = false;
            GridView2.Visible = false;
            chkChange.Visible = chkGodam.Visible = false;
            ddlIssueCentre.Enabled = ddlGodown.Enabled = txtQty.Enabled = false;
        }
        else
        {
            btnRecptSubmit.Enabled = false;
            ddlIssueCentre.SelectedIndex = 0;
            ddlGodown.Items.Clear();
            txtGodownRemQty.Text = txtDORemQty.Text = txtQty.Text = "";
            btnAdd.Visible = true;
            GridView2.Visible = true;
            chkChange.Visible = chkGodam.Visible = true;
            ddlIssueCentre.Enabled = ddlGodown.Enabled = txtQty.Enabled = true;
        }
    }

    protected void chkChange_CheckedChanged(object sender, EventArgs e)
    {
        ddlFrmDist1.SelectedIndex = 0;
        chkDate.Checked = chkGodam.Checked = false;

        if (chkChange.Checked)
        {
            chkDate.Visible = chkGodam.Visible = false;
            if (ddlDONo.SelectedIndex <= 0)
            {
                trMsg.Visible = false;
                chkChange.Checked = false;
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select DO Number'); </script> ");
                return;
            }
            else
            {
                ddlIssueCentre.Items.Clear();
                ddlBranch.Items.Clear();
                ddlGodown.Items.Clear();
                trMsg.Visible = true;
            }
        }
        else
        {
            chkDate.Visible = chkGodam.Visible = true;
            ddlIssueCentre.Items.Clear();
            ddlBranch.Items.Clear();
            ddlGodown.Items.Clear();
            GetMPIssueCentre();
            trMsg.Visible = false;
        }
    }

    protected void ddlFrmDist1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlIssueCentre.Items.Clear();
        ddlBranch.Items.Clear();
        ddlGodown.Items.Clear();

        if (ddlFrmDist1.SelectedIndex > 0)
        {
            GetMPIssueCentre1();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select District'); </script> ");
            return;
        }
    }

    public void GetMPIssueCentre1()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "select DepotName,DepotID from tbl_MetaData_DEPOT where DistrictId= '23" + ddlFrmDist1.SelectedValue.ToString() + "' order by DepotName";
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

    protected void chkGodam_CheckedChanged(object sender, EventArgs e)
    {
        chkDate.Checked = chkChange.Checked = false;

        if (chkGodam.Checked == true)
        {
            btnAdd.Text = "Update";
            chkDate.Visible = chkChange.Visible = false;
        }
        else
        {
            btnAdd.Text = "Add";
            chkDate.Visible = chkChange.Visible = true;
        }
    }
}