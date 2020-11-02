using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;

public partial class PcGdn_Insp_Print_PCInsp : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    string inspdate;
    string croptype;
    string pccode;
    string districtId;
    string usertype;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["District_id"] != null)
        {
            inspdate = Session["InspDate"].ToString();
            croptype  = Session["CropType"].ToString();
            pccode = Session["PCName"].ToString();
            districtId = Session["District_id"].ToString();
            usertype =  Session["UserType"].ToString();

            string qry = "SELECT  pds.districtsmp.district_name ,pds.Tehsilmp.Tehsil_Name ,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name ,Society.Society_Name  ,[Officer_one] ,[Officer_two] ,[Officer_three] ,[Officer_four] ,[Officer_five]  ,[OfficerMobile_One] ,[OfficerMobile_Two] ,[OfficerMobile_Three] ,[OfficerMobile_Four]  ,[OfficerMobile_Five] ,[OfficerDesig_One]  ,[OfficerDesig_Two] ,[OfficerDesig_Three]  ,[OfficerDesig_Four]   ,[OfficerDesig_Five]  ,convert(varchar(10),[Inspection_Date],103)Inspection_Date ,[PC_ManagerName] ,[PC_ManagerMobile]  ,[NodalOfficer_Name] ,[NodalOfficer_Desig] ,[NodalOfficer_Mobile] ,[PC_regsiterFarmer]   , convert(varchar(10),[PC_ProcDate],103)PC_ProcDate  ,[Proc_onInspection] ,[Proc_totalKharidi] ,[Proc_totalTransport] ,[Proc_remainQty] ,[FAQ_Sample] ,[Parkhi] ,[Electronic_Balance] ,[Plastic_Bags] ,[Peetal_Seal]   ,[Moisture_Machine],[Enamel_Plate]  ,[Filter] ,[Thresar_Blower],[Tirpal_Silai]    ,[Double_Silai]  ,[RejectTruck_dueDoubleSilai]  ,[Stencil_Tags] ,[RejectTruck_DuestencilTags],[RejectSample_Entry]  ,[AllRegister_Entry] ,[RejectStock_dueQualityCheck] ,[Totalrecd_Bardana] ,[UsedBardana] ,[RemainBardana_Gathan] ,[LoosBardana] ,[ManakBharti_Weight] ,[Reason_ManakBharti_Weight] ,convert(varchar(10),[Payment_toFarmerDate],103)Payment_toFarmerDate ,[PaymenT_Late3Days] ,[Verify_TaulKanta] ,[ReverseBags_Packing] ,[Totalreject_trucks]  ,[TotalReject_Qty] ,convert(varchar(10),[AcceptanceRecd_toDate],103)AcceptanceRecd_toDate ,[TotalBags_allParameter],[OtherReason,RejectTruck_DoubleSilai]  FROM [PC_Inspect_2016Paddy] inner join pds.districtsmp on pds.districtsmp.district_code = PC_Inspect_2016Paddy.District  inner join pds.Tehsilmp on pds.Tehsilmp.TehsilCode = PC_Inspect_2016Paddy.Tehsil  inner join tbl_MetaData_STORAGE_COMMODITY on tbl_MetaData_STORAGE_COMMODITY.Commodity_Id = PC_Inspect_2016Paddy.PC_Crop  inner join Society on Society.Society_Id = PC_Inspect_2016Paddy.Insp_Pc where [Inspection_Date] = '" + inspdate + "' and district = '" + districtId + "' and Insp_Pc = '" + pccode + "' and PC_Crop = '" + croptype + "' and InspLevel = '" + usertype + "'";

            SqlCommand cmd = new SqlCommand(qry,con);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtName_one.Text = ds.Tables[0].Rows[0]["Officer_one"].ToString();
                txtName_two.Text = ds.Tables[0].Rows[0]["Officer_two"].ToString();
                txtName_three.Text = ds.Tables[0].Rows[0]["Officer_three"].ToString();
                txtName_four.Text = ds.Tables[0].Rows[0]["Officer_four"].ToString();
                txtName_five.Text = ds.Tables[0].Rows[0]["Officer_five"].ToString();

                txtMob_one.Text = ds.Tables[0].Rows[0]["OfficerMobile_One"].ToString();
                txtMob_two.Text = ds.Tables[0].Rows[0]["OfficerMobile_Two"].ToString();
                txtMob_three.Text = ds.Tables[0].Rows[0]["OfficerMobile_Three"].ToString();
                txtMob_four.Text = ds.Tables[0].Rows[0]["OfficerMobile_Four"].ToString();
                txtMob_five.Text = ds.Tables[0].Rows[0]["OfficerMobile_Five"].ToString();

                txtdesig_one.Text = ds.Tables[0].Rows[0]["OfficerDesig_One"].ToString();
                txtdesig_two.Text = ds.Tables[0].Rows[0]["OfficerDesig_Two"].ToString();
                txtdesig_three.Text = ds.Tables[0].Rows[0]["OfficerDesig_Three"].ToString();
                txtdesig_four.Text = ds.Tables[0].Rows[0]["OfficerDesig_Four"].ToString();
                txtdesig_five.Text = ds.Tables[0].Rows[0]["OfficerDesig_Five"].ToString();

                txtInsp_date.Text = ds.Tables[0].Rows[0]["Inspection_Date"].ToString();
                ddlDistrict.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
                ddlTehsil.Text = ds.Tables[0].Rows[0]["Tehsil_Name"].ToString();

                ddlCrop.Text = ds.Tables[0].Rows[0]["Commodity_Name"].ToString();
                ddlPCName.Text = ds.Tables[0].Rows[0]["Society_Name"].ToString();
                txtPC_manager.Text = ds.Tables[0].Rows[0]["PC_ManagerName"].ToString();
                txtPC_Mobile.Text = ds.Tables[0].Rows[0]["PC_ManagerMobile"].ToString();

                txtNodal_officer.Text = ds.Tables[0].Rows[0]["NodalOfficer_Name"].ToString();
                txtNodal_desig.Text = ds.Tables[0].Rows[0]["NodalOfficer_Desig"].ToString();
                txtNodal_Mobile.Text = ds.Tables[0].Rows[0]["NodalOfficer_Mobile"].ToString();

                txtregister_Farmer.Text = ds.Tables[0].Rows[0]["PC_regsiterFarmer"].ToString();
                txtProc_date.Text = ds.Tables[0].Rows[0]["PC_ProcDate"].ToString();

                txtInspDate_Kharidi.Text = ds.Tables[0].Rows[0]["Proc_onInspection"].ToString();

                txt_totalkharidi.Text = ds.Tables[0].Rows[0]["Proc_totalKharidi"].ToString();
                txt_totalTransportation.Text = ds.Tables[0].Rows[0]["Proc_totalTransport"].ToString();
                txt_remainTransport.Text = ds.Tables[0].Rows[0]["Proc_remainQty"].ToString();

                ddl_FaqSample.Text = ds.Tables[0].Rows[0]["FAQ_Sample"].ToString();
                ddl_Parkhi.Text = ds.Tables[0].Rows[0]["Parkhi"].ToString();
                ddl_electronicBalance.Text = ds.Tables[0].Rows[0]["Electronic_Balance"].ToString();
                ddl_plasticbag.Text = ds.Tables[0].Rows[0]["Plastic_Bags"].ToString();
                ddl_seal.Text = ds.Tables[0].Rows[0]["Peetal_Seal"].ToString();
                ddl_moistMachine.Text = ds.Tables[0].Rows[0]["Moisture_Machine"].ToString();
                ddl_enamelplate.Text = ds.Tables[0].Rows[0]["Enamel_Plate"].ToString();
                ddl_filter.Text = ds.Tables[0].Rows[0]["Filter"].ToString();

                ddl_blower.Text = ds.Tables[0].Rows[0]["Thresar_Blower"].ToString();

                ddl_TirplaSilai.Text = ds.Tables[0].Rows[0]["Tirpal_Silai"].ToString();
                ddl_doubleSilai.Text = ds.Tables[0].Rows[0]["Double_Silai"].ToString();
                ddl_reject_doubleSilai.Text = ds.Tables[0].Rows[0]["RejectTruck_dueDoubleSilai"].ToString();
                ddl_stencilTag.Text = ds.Tables[0].Rows[0]["Stencil_Tags"].ToString();      
                txt_truckreject_duestencil.Text = ds.Tables[0].Rows[0]["RejectTruck_DuestencilTags"].ToString();
                ddl_rejectSample.Text = ds.Tables[0].Rows[0]["RejectSample_Entry"].ToString();
                ddl_registerEntry.Text = ds.Tables[0].Rows[0]["AllRegister_Entry"].ToString();   
                txt_rejectQty.Text = ds.Tables[0].Rows[0]["RejectStock_dueQualityCheck"].ToString();
                txt_totalBardana.Text = ds.Tables[0].Rows[0]["Totalrecd_Bardana"].ToString();
                txt_usedBardana.Text = ds.Tables[0].Rows[0]["UsedBardana"].ToString();
                txt_remainBardana_Gathan.Text = ds.Tables[0].Rows[0]["RemainBardana_Gathan"].ToString();
                txt_remain_bardana.Text = ds.Tables[0].Rows[0]["LoosBardana"].ToString();

                ddl_ManikBharti.Text = ds.Tables[0].Rows[0]["ManakBharti_Weight"].ToString();    

                txt_reason_manakbharti.Text = ds.Tables[0].Rows[0]["Reason_ManakBharti_Weight"].ToString();
                txt_paymentDate.Text = ds.Tables[0].Rows[0]["Payment_toFarmerDate"].ToString();
                ddl_last3days.Text = ds.Tables[0].Rows[0]["PaymenT_Late3Days"].ToString();

                ddl_verify_taulkanta.Text = ds.Tables[0].Rows[0]["Verify_TaulKanta"].ToString();
                ddl_paddyBharti_return.Text = ds.Tables[0].Rows[0]["ReverseBags_Packing"].ToString();    //////

                txt_totalreject_truck.Text = ds.Tables[0].Rows[0]["Totalreject_trucks"].ToString();
                txt_totalreject_qty.Text = ds.Tables[0].Rows[0]["TotalReject_Qty"].ToString();

                txt_accept_recddate.Text = ds.Tables[0].Rows[0]["AcceptanceRecd_toDate"].ToString();
                txt_totalbags_allparameter.Text = ds.Tables[0].Rows[0]["TotalBags_allParameter"].ToString();

                txt_otherpoint.Text = ds.Tables[0].Rows[0]["OtherReason"].ToString();

                txt_truckreject_dueDoubleSilai.Text = ds.Tables[0].Rows[0]["RejectTruck_DoubleSilai"].ToString();

                /// Make Yes where Y and NO where N
                if (ddl_FaqSample.Text == "Y")
                {
                    ddl_FaqSample.Text = "Yes";
                }

                else
                {
                    ddl_FaqSample.Text = "No";
                }

                ddl_Parkhi.Text = ds.Tables[0].Rows[0]["Parkhi"].ToString();

                if (ddl_Parkhi.Text == "Y")
                {
                    ddl_Parkhi.Text = "Yes";
                }

                else
                {
                    ddl_Parkhi.Text = "No";
                }

                ddl_electronicBalance.Text = ds.Tables[0].Rows[0]["Electronic_Balance"].ToString();

                if (ddl_electronicBalance.Text == "Y")
                {
                    ddl_electronicBalance.Text = "Yes";
                }

                else
                {
                    ddl_electronicBalance.Text = "No";
                }
              
                if (ddl_plasticbag.Text == "Y")
                {
                    ddl_plasticbag.Text = "Yes";
                }

                else
                {
                    ddl_plasticbag.Text = "No";
                }
               
                if (ddl_seal.Text == "Y")
                {
                    ddl_seal.Text = "Yes";
                }

                else
                {
                    ddl_seal.Text = "No";
                }
              
                if (ddl_moistMachine.Text == "Y")
                {
                    ddl_moistMachine.Text = "Yes";
                }

                else
                {
                    ddl_moistMachine.Text = "No";
                }
               
                if (ddl_enamelplate.Text == "Y")
                {
                    ddl_enamelplate.Text = "Yes";
                }

                else
                {
                    ddl_enamelplate.Text = "No";
                }
              
                if (ddl_filter.Text == "Y")
                {
                    ddl_filter.Text = "Yes";
                }

                else
                {
                    ddl_filter.Text = "No";
                }

               
                if (ddl_blower.Text == "Y")
                {
                    ddl_blower.Text = "Yes";
                }

                else
                {
                    ddl_blower.Text = "No";
                }

                
                if (ddl_TirplaSilai.Text == "Y")
                {
                    ddl_TirplaSilai.Text = "Yes";
                }

                else
                {
                    ddl_TirplaSilai.Text = "No";
                }
                
                if (ddl_doubleSilai.Text == "Y")
                {
                    ddl_doubleSilai.Text = "Yes";
                }

                else
                {
                    ddl_doubleSilai.Text = "No";
                }
                
                if (ddl_reject_doubleSilai.Text == "Y")
                {
                    ddl_reject_doubleSilai.Text = "Yes";
                }

                else
                {
                    ddl_reject_doubleSilai.Text = "No";
                }
               
                if (ddl_stencilTag.Text == "Y")
                {
                    ddl_stencilTag.Text = "Yes";
                }

                else
                {
                    ddl_stencilTag.Text = "No";
                }

                if (ddl_rejectSample.Text == "Y")
                {
                    ddl_rejectSample.Text = "Yes";
                }

                else
                {
                    ddl_rejectSample.Text = "No";
                }
               
                if (ddl_registerEntry.Text == "Y")
                {
                    ddl_registerEntry.Text = "Yes";
                }

                else
                {
                    ddl_registerEntry.Text = "No";
                }
               
                if (ddl_ManikBharti.Text == "Y")
                {
                    ddl_ManikBharti.Text = "Yes";
                }

                else
                {
                    ddl_ManikBharti.Text = "No";
                }
               
                if (ddl_last3days.Text == "Y")
                {
                    ddl_last3days.Text = "Yes";
                }

                else
                {
                    ddl_last3days.Text = "No";
                }

               
                if (ddl_verify_taulkanta.Text == "Y")
                {
                    ddl_verify_taulkanta.Text = "Yes";
                }

                else
                {
                    ddl_verify_taulkanta.Text = "No";
                }
                
                if (ddl_paddyBharti_return.Text == "Y")
                {
                    ddl_paddyBharti_return.Text = "Yes";
                }

                else
                {
                    ddl_paddyBharti_return.Text = "No";
                }
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Unable to Fetch the Record, Pls Try Later |'); </script> ");
                return;
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }


        }
    }
}
