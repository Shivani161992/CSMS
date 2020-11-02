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

public partial class PaddyMilling_PM_DO_Delete : System.Web.UI.Page
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
        string DistCode = Session["dist_id"].ToString();

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
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select DO Number'); </script> ");
            return;
        }

        decimal DOQty = 0, DORemQty = 0;
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            DOQty += decimal.Parse(GridView2.Rows[i].Cells[4].Text);
            DORemQty += decimal.Parse(GridView2.Rows[i].Cells[5].Text);
        }

        if (DOQty != DORemQty)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस डिलीवरी आर्डर के विरुद्ध धान जारी किया जा चूका है, इसलिए आप इसे Delete नहीं कर सकते|'); </script> ");
            return;
        }

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

                        instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION ";

                        instr += "Insert Into PaddyMilling_Agreement_Log(District,Dist_Manager_Name,Mill_Addr_District,Mill_Name,Mill_Operator_Name,Mill_Operator,CropYear,From_Date,To_Date,Common_Dhan,GradeA_Dhan,Total_Dhan,Agreement_ID,IP_Address,Current_DateTime,User_Agent,State,State_Code,DepositMoney,DhanLot,DhanAmount,DhanAmountType,DhanAmountDetails,Agrmt_Date,R_Arva,R_Ushna,Rem_Common_Dhan,Rem_GradeA_Dhan,Rem_Total_Dhan,Rem_DhanLot,Milling_Type,Return_CommonDhan,Return_GradeADhan,Return_TotalDhan,Return_CommonRice,Return_GradeARice,Return_TotalRice,Rejected,IsAccepted,MobileNO,AcceptedIP,AcceptedDate,DeletedDate,DeletedIP,Operation) select District,Dist_Manager_Name,Mill_Addr_District,Mill_Name,Mill_Operator_Name,Mill_Operator,CropYear,From_Date,To_Date,Common_Dhan,GradeA_Dhan,Total_Dhan,Agreement_ID,IP_Address,Current_DateTime,User_Agent,State,State_Code,DepositMoney,DhanLot,DhanAmount,DhanAmountType,DhanAmountDetails,Agrmt_Date,R_Arva,R_Ushna,Rem_Common_Dhan,Rem_GradeA_Dhan,Rem_Total_Dhan,Rem_DhanLot,Milling_Type,Return_CommonDhan,Return_GradeADhan,Return_TotalDhan,Return_CommonRice,Return_GradeARice,Return_TotalRice,Rejected,IsAccepted,MobileNO,AcceptedIP,AcceptedDate,GETDATE(),'" + GetIp + "','U' From PaddyMilling_Agreement_2017 where Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' ; ";
                        instr += "Insert Into PaddyMilling_DO_Log_2017(DO_Number,Mill_Code,Agreement_ID,District,CropYear,From_Date,To_Date,Milling_Type,Alloted_CommonDhan,Alloted_GradeADhan,R_Arva,R_Ushna,Issue_Centre,IP_Address,Current_DateTime,User_Agent,Check_DO,DispatchDhan_IC,Return_CommonRice,Return_GradeARice,Return_TotalRice,LotOnlyNumber,Rejected,DhanLot,Branch_Code,Godown_Code,LotNumber,Rem_Alloted_CommonDhan,Rem_Alloted_GradeADhan,Rem_DhanLot,LotNo,DeletedDate,DeletedIP,Operation) select DO_Number,Mill_Code,Agreement_ID,District,CropYear,From_Date,To_Date,Milling_Type,Alloted_CommonDhan,Alloted_GradeADhan,R_Arva,R_Ushna,Issue_Centre,IP_Address,Current_DateTime,User_Agent,Check_DO,DispatchDhan_IC,Return_CommonRice,Return_GradeARice,Return_TotalRice,LotOnlyNumber,Rejected,DhanLot,Branch_Code,Godown_Code,LotNumber,Rem_Alloted_CommonDhan,Rem_Alloted_GradeADhan,Rem_DhanLot,LotNo,GETDATE(),'" + GetIp + "','U' From PaddyMilling_DO_2017 where Check_DO='" + ddlDONo.SelectedItem.ToString() + "' ; ";

                        instr += "Update PaddyMilling_Agreement_2017 Set Rem_Common_Dhan=(Rem_Common_Dhan+'" + DORemQty + "') , Rem_Total_Dhan=(Rem_Total_Dhan+'" + DORemQty + "') , Rem_DhanLot=(Rem_DhanLot+1) where Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' ;";

                        instr += "Delete PaddyMilling_DO_2017 where Check_DO='" + ddlDONo.SelectedItem.ToString() + "' ; ";

                        for (int i = 0; i < GridView2.Rows.Count; i++)
                        {
                            decimal UPDORemQty = 0;
                            string IC = "", Godown = "";
                            UPDORemQty = decimal.Parse(GridView2.Rows[i].Cells[5].Text);
                            IC = GridView2.Rows[i].Cells[6].Text;
                            Godown = GridView2.Rows[i].Cells[7].Text;

                            instr += "Update PaddyMapping_Godown set Rem_CommonPaddy=(Rem_CommonPaddy+" + UPDORemQty + ") where CropYear='" + txtYear.Text + "' and Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedValue.ToString() + "' and Godown_id='" + Godown + "'; ";
                        }

                        instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";

                        cmd = new SqlCommand(instr, con);
                        int count = cmd.ExecuteNonQuery();

                        if (count > 0)
                        {
                            btnDelete.Enabled = false;

                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Deleted Successfully'); </script> ");
                            txtYear.Text = "";
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

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        //if (Session["st_id"].ToString() == "10")
        //{
        //    Response.Redirect("~/State/PaddyMillingHome_MFD.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/State/PaddyMillingHome.aspx");
        //}
        Response.Redirect("~/District/PaddyMillingHome.aspx");
    }

    protected void ddlFrmDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMillName.Items.Clear();
        ddlAgtmtNumber.Items.Clear();
        ddlDONo.Items.Clear();
        btnDelete.Enabled = false;
        txtDODate.Text = txtDOLastDate.Text = "";

        GridView2.DataSource = "";
        GridView2.DataBind();

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
        ddlDONo.Items.Clear();
        btnDelete.Enabled = false;
        txtDODate.Text = txtDOLastDate.Text = "";

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
        ddlDONo.Items.Clear();
        txtDODate.Text = txtDOLastDate.Text = "";
        btnDelete.Enabled = false;
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
        txtDODate.Text = txtDOLastDate.Text = "";
        btnDelete.Enabled = false;
        GridView2.DataSource = "";
        GridView2.DataBind();

        if (ddlDONo.SelectedIndex > 0)
        {
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

                string select = "Select Rem_Alloted_CommonDhan,DhanLot,DO_Number,Milling_Type,Alloted_CommonDhan,Issue_Centre,Godown_Code,(Alloted_CommonDhan-(ISNULL(Return_CommonRice,0))) As RemDOQty,LotNo,From_Date,To_Date From PaddyMilling_DO_2017 where Mill_Code='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedValue.ToString() + "' and District='" + ddlFrmDist.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and Check_DO='" + ddlDONo.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Decimal DOQty = 0, DORemQty = 0;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DOQty += decimal.Parse(ds.Tables[0].Rows[i]["Alloted_CommonDhan"].ToString());
                        DORemQty += decimal.Parse(ds.Tables[0].Rows[i]["RemDOQty"].ToString());
                    }

                    if (DOQty != DORemQty)
                    {
                        btnDelete.Enabled = false;
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस डिलीवरी आर्डर के विरुद्ध धान जारी किया जा चूका है, इसलिए आप इसे Delete नहीं कर सकते|'); </script> ");
                    }
                    else
                    {
                        btnDelete.Enabled = true;
                    }

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

}