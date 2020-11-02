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

public partial class CSMSSurveyorLogin_Delete_GodownSurveyor_QualityInspection : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    public string gatepass, InspectionID = "";
    public int getnum;
    SqlDataReader dr;
    string UserName = "";
    public string GenerateOTP = "", OTPSMS = "";

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userGodown"] != null)
        {
            if (!IsPostBack)
            {
                UserName = Session["userGodown"].ToString();
                string samepassword = Session["NotchangePassword"].ToString();
                if (samepassword == "SMSSUR")
                {
                    Response.Redirect("~/CSMSSurveyorLogin/GodownSurveyor_ChangePassword.aspx");
                }
                else if (samepassword != "SMSSUR")
                {


                    Session["ICGBQ"] = null;

                    GetTruckChallan();

                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
            }
        }
        else
        {
            Response.Redirect("~/CSMSSurveyorLogin/SurveyorLogin_Godown.aspx");
        }
    }

    public void GetTruckChallan()
    {
        UserName = Session["userGodown"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "select distinct TruckChallan from CMS_GodownSurveyor_Inspection where TruckChallan not in  (select distinct TC_Number from SCSC_Procurement_CSM  ) and SurveyorMob='" + UserName + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlTruckChallan.DataSource = ds.Tables[0];
                    ddlTruckChallan.DataTextField = "TruckChallan";
                    ddlTruckChallan.DataValueField = "TruckChallan";
                    ddlTruckChallan.DataBind();
                    ddlTruckChallan.Items.Insert(0, "--Select--");
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
    protected void ddlTruckChallan_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtCommodities.Text = "";
        txtAcceptReject.Text = "";
        txtStatus.Text = "";

        if (ddlTruckChallan.SelectedIndex > 0)
        {
            GetChallanData();


        }

    }
    public void GetChallanData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "select BookNumber, C.Commodity_Name, GI.Status, CONVERT (varchar(10), DateOfInsp, 103) DateOfInsp, Quantity  from CMS_GodownSurveyor_Inspection as GI inner join tbl_MetaData_STORAGE_COMMODITY as C on c.Commodity_Id=GI.Commodity where TruckChallan='" + ddlTruckChallan.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtCommodities.Text = ds.Tables[0].Rows[0]["Commodity_Name"].ToString();
                    txtAcceptReject.Text = ds.Tables[0].Rows[0]["BookNumber"].ToString();
                    txtStatus.Text = ds.Tables[0].Rows[0]["Status"].ToString();
                    txtDateOfInspect.Text = ds.Tables[0].Rows[0]["DateOfInsp"].ToString();
                    txtQty.Text = ds.Tables[0].Rows[0]["Quantity"].ToString();
                    btnDelete.Enabled = true;


                }

                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Not Available'); </script> ");
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
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (ddlTruckChallan.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Truck Challan No.'); </script> ");
            return;
        }
        
        else
        {
            
                ClientIP objClientIP = new ClientIP();
                string GetIp = (objClientIP.GETIP());

              
                    using (con = new SqlConnection(strcon))
                    {
                        try
                        {
                            con.Open();

                           

                            string instr = "";

                            instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION ";

                            instr += "Insert Into CMS_GodownSurveyor_Inspection_Log([GodownSurveyorInsp_ID], [Commodity] ,[TruckChallan], [SocietyDist] , [Society], [CropYear], [DateofDispatch], [TransporterID]      ,[TruckNumber]      ,[Quantity]      ,[Bags]      ,[BagsType]      ,[Status]      ,[BookNumber]      ,[AcceptanceNumber]      ,[RejectionNumber]      ,[ReceivingGodown]      ,[ReceivingBranch]      ,[Sarson]      ,[Sarson_ImpForeignmatt_IncTaramira]      ,[Sarson_Admix_WOT_IncToria]      ,[Sarson_Unripe_ShirvellImmature]      ,[Sarson_DamagedAndWeevilled]      ,[Sarson_SmallAtrophiedSeeds]      ,[Sarson_MoistureContent]      ,[Massor]      ,[Massur_ForeignMatter]      ,[Massur_Admixture]      ,[Massur_DamagedPulses]      ,[Massur_SligDamagedPulses]      ,[Massur_ImmaShrivellPulses]      ,[Massur_weevilledPulses]      ,[Massur_MoistureContent]      ,[Channa]      ,[Gram_ForeignMatter]      ,[Gram_OtherFoodGrains]      ,[Gram_DamagedGrains]      ,[Gram_SligDamagedTouchGrains]       ,[Gram_ImmaShrivAndBroGrains] ,[Gram_AdmixOfOtherVarieties] ,[Gram_WeevilleGrains] ,[Gram_MositureContent]  ,[CreatedDate] ,[IP], Insp_ID, [SurveyorID] ,[SurveyorName], [SurveyorMob], DateOfInsp, DeletedDate, DeletedIP) select [GodownSurveyorInsp_ID], [Commodity] ,[TruckChallan], [SocietyDist] , [Society], [CropYear], [DateofDispatch], [TransporterID]      ,[TruckNumber]      ,[Quantity]      ,[Bags]      ,[BagsType]      ,[Status]      ,[BookNumber]      ,[AcceptanceNumber]      ,[RejectionNumber]      ,[ReceivingGodown]      ,[ReceivingBranch]      ,[Sarson]      ,[Sarson_ImpForeignmatt_IncTaramira]      ,[Sarson_Admix_WOT_IncToria]      ,[Sarson_Unripe_ShirvellImmature]      ,[Sarson_DamagedAndWeevilled]      ,[Sarson_SmallAtrophiedSeeds]      ,[Sarson_MoistureContent]      ,[Massor]      ,[Massur_ForeignMatter]      ,[Massur_Admixture]      ,[Massur_DamagedPulses]      ,[Massur_SligDamagedPulses]      ,[Massur_ImmaShrivellPulses]      ,[Massur_weevilledPulses]      ,[Massur_MoistureContent]      ,[Channa]      ,[Gram_ForeignMatter]      ,[Gram_OtherFoodGrains]      ,[Gram_DamagedGrains]      ,[Gram_SligDamagedTouchGrains]       ,[Gram_ImmaShrivAndBroGrains] ,[Gram_AdmixOfOtherVarieties] ,[Gram_WeevilleGrains] ,[Gram_MositureContent]  ,[CreatedDate] ,[IP], Insp_ID, [SurveyorID] ,[SurveyorName], [SurveyorMob], DateOfInsp, GetDate(),'"+GetIp+"' From CMS_GodownSurveyor_Inspection where TruckChallan='" +ddlTruckChallan.SelectedValue.ToString() + "' ; ";
                            instr += "update IssueToSangrahanaKendra_CSM2018 set GS_Status='0', G_Surveyor='0' where TruckChalanNo='" + ddlTruckChallan.SelectedValue.ToString() + "' ;";
                            instr += "delete from CMS_GodownSurveyor_Inspection  where TruckChallan='"+ddlTruckChallan.SelectedValue.ToString()+"';";

                          
                            instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";

                            cmd = new SqlCommand(instr, con);
                            int count = cmd.ExecuteNonQuery();

                            if (count > 0)
                            {
                                btnDelete.Enabled = false;
                                ddlTruckChallan.Enabled = false;
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Deleted Successfully'); </script> ");
                               
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
    }

}