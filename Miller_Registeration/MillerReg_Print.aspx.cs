using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


public partial class Miller_Registeration_MillerReg_Print : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString; //CSMS

    string CropYear = "", RegistID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CropYear = Session["CropYear"].ToString();
            RegistID = Session["RegistID"].ToString();

            GetDataTO();
        }
    }

    public void GetDataTO()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select GETDATE() As CurrentDte, OldGunnybags, Did_millinglastyear, Created_date,CropYear,Registration_ID,Mill_Name, upcoming_SixMonths, Total_AgreeQty, Total_BRLQty_Insp, Upgraded_BRLQty, Remain_qty, Aadhar_number,(Select State_Name From State_Master where State_Code=MR.State_Code) As State,(Case When MR.State_Code='23' Then (Select district_name From pds.districtsmp As MP where MP.district_code=MR.District_Code) Else (Select district_name FROM OtherState_DistrictCode As OS where OS.district_code=MR.District_Code and OS.State_Id=MR.State_Code) End) As Dist,"
                                    + " (Case When MR.State_Code='23' Then (Select Tehsil_Name From pds.Tehsilmp As TH where TH.TehsilCode=MR.tehsil_code) Else (Select subdistrict_name FROM Sub_Division As SD where SD.subdistrict_code=MR.tehsil_code and SD.state_code=MR.State_Code) End) As Tehsil,mill_Address As Gram,mill_pincode,mill_phone As LadLine, Miller_MobileNo,(Case When mill_ownership='' Then N'प्रोपाइटरशिप'Else N'फर्म' End )  As Prop,"
                                        + " mill_proto_name As PropName,mill_proto_address As PropAdd,mill_proto_city As PrpCity,firm_type,firmliasnce,operator_name As SanchName,operator_father As SanchFname, Operator_permanent_add As SanchAdd,partner1,partner2,pratner3,partner4,Operator_telephone1,Operator_telephone2,Operator_telephone3,Operator_telephone4,mill_running_status,(Case When mill_ownership_status='firm' Then N'लीज़ पर है' Else N'स्वयं की है' End) As SelfLiz,"
                                            + " leez_mill_ownername,leez_mill_owneraddress,milling_capacity_arwa,milling_capacity_usna,CropYear_Shift,salstax_no,Pan_No,alloted_servicetaxno,current_servicetax,mandipra_lisance,mandivyapar_lisance,last_yearmillng_qunt,reprenstaive_name,id_card,AprUnit,AprEmp,AprShift,MayUnit,MayEmp,MayShift,JuneUnit,JuneEmp,JuneShift,JulyUnit,JulyEmp,JulyShift,AugUnit,AugEmp,AugShift,SepUnit,SepEmp,SepShift,"
                                            + " (Case when leez_mill_enddate<='2016-01-01' Then Cast(' ' AS NVARCHAR) Else Cast((REPLACE(CONVERT(NVARCHAR,leez_mill_enddate, 111), '/', '-')) AS Varchar)End ) As leez_mill_enddate"
                                                + " From Miller_Registration_2017 As MR where Registration_ID='" + RegistID + "' and CropYear='" + CropYear + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblCropYear.Text = Label2.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    lblCurrentDateTime.Text = lblCurrentDateTime2.Text = ds.Tables[0].Rows[0]["CurrentDte"].ToString();
                    DateTime RegDate = DateTime.Parse(ds.Tables[0].Rows[0]["Created_date"].ToString());
                    lblRegDate.Text = RegDate.ToString("dd/MMM/yyyy");

                    lblRegNo.Text = lblRegisID2.Text = lblRegisID1.Text = ds.Tables[0].Rows[0]["Registration_ID"].ToString();
                    lblMillName.Text = ds.Tables[0].Rows[0]["Mill_Name"].ToString();
                    lblState.Text = ds.Tables[0].Rows[0]["State"].ToString();
                    lblDist.Text = ds.Tables[0].Rows[0]["Dist"].ToString();
                    lblTehsil.Text = ds.Tables[0].Rows[0]["Tehsil"].ToString();
                    lblGram.Text = ds.Tables[0].Rows[0]["Gram"].ToString();
                    lblPin.Text = ds.Tables[0].Rows[0]["mill_pincode"].ToString();
                    lblMob.Text = ds.Tables[0].Rows[0]["Miller_MobileNo"].ToString();
                    LblPhone.Text = ds.Tables[0].Rows[0]["LadLine"].ToString();
                    lblOwnership.Text = ds.Tables[0].Rows[0]["Prop"].ToString();
                    lblPropName.Text = ds.Tables[0].Rows[0]["PropName"].ToString();
                    lblPropAdd.Text = ds.Tables[0].Rows[0]["PropAdd"].ToString();
                    lblPropCity.Text = ds.Tables[0].Rows[0]["PrpCity"].ToString();
                    lblFirm.Text = ds.Tables[0].Rows[0]["firm_type"].ToString();
                    lblFirmPanjiyan.Text = ds.Tables[0].Rows[0]["firmliasnce"].ToString();
                    lblSanchName.Text = ds.Tables[0].Rows[0]["SanchName"].ToString();
                    lblSnachFName.Text = ds.Tables[0].Rows[0]["SanchFname"].ToString();
                    lblSanchAdd.Text = ds.Tables[0].Rows[0]["SanchAdd"].ToString();
                    lblPart1.Text = ds.Tables[0].Rows[0]["partner1"].ToString();
                    lblPart2.Text = ds.Tables[0].Rows[0]["partner2"].ToString();
                    lblPart3.Text = ds.Tables[0].Rows[0]["pratner3"].ToString();
                    lblPart4.Text = ds.Tables[0].Rows[0]["partner4"].ToString();
                    lblPartPhone1.Text = ds.Tables[0].Rows[0]["Operator_telephone1"].ToString();
                    lblPartPhone2.Text = ds.Tables[0].Rows[0]["Operator_telephone2"].ToString();
                    lblPartPhone3.Text = ds.Tables[0].Rows[0]["Operator_telephone3"].ToString();
                    lblPartPhone4.Text = ds.Tables[0].Rows[0]["Operator_telephone4"].ToString();
                    lblRunningStatus.Text = ds.Tables[0].Rows[0]["mill_running_status"].ToString();
                    lblLiz.Text = ds.Tables[0].Rows[0]["SelfLiz"].ToString();
                    lblLizName.Text = ds.Tables[0].Rows[0]["leez_mill_ownername"].ToString();
                    lblLizAdd.Text = ds.Tables[0].Rows[0]["leez_mill_owneraddress"].ToString();
                    lblArva.Text = ds.Tables[0].Rows[0]["milling_capacity_arwa"].ToString();
                    lblUsna.Text = ds.Tables[0].Rows[0]["milling_capacity_usna"].ToString();
                    lblShift.Text = ds.Tables[0].Rows[0]["CropYear_Shift"].ToString();
                    lblTaxNo.Text = ds.Tables[0].Rows[0]["salstax_no"].ToString();
                    lblPANNo.Text = ds.Tables[0].Rows[0]["Pan_No"].ToString();
                    lblServiceTax.Text = ds.Tables[0].Rows[0]["alloted_servicetaxno"].ToString();
                    lblReadingNo.Text = ds.Tables[0].Rows[0]["current_servicetax"].ToString();
                    lblMandiNo.Text = ds.Tables[0].Rows[0]["mandipra_lisance"].ToString();
                    lbllicenceNo.Text = ds.Tables[0].Rows[0]["mandivyapar_lisance"].ToString();
                    lblCustomMilling.Text = ds.Tables[0].Rows[0]["last_yearmillng_qunt"].ToString();
                    lblAdhikratName.Text = ds.Tables[0].Rows[0]["reprenstaive_name"].ToString();
                    lblAdhikratAdd.Text = ds.Tables[0].Rows[0]["id_card"].ToString();
                    AprUnit.Text = ds.Tables[0].Rows[0]["AprUnit"].ToString();
                    lblAprEmp.Text = ds.Tables[0].Rows[0]["AprEmp"].ToString();
                    lblAprShift.Text = ds.Tables[0].Rows[0]["AprShift"].ToString();
                    MayUnit.Text = ds.Tables[0].Rows[0]["MayUnit"].ToString();
                    lblMayEmp.Text = ds.Tables[0].Rows[0]["MayEmp"].ToString();
                    lblMayShift.Text = ds.Tables[0].Rows[0]["MayShift"].ToString();
                    JuneUnit.Text = ds.Tables[0].Rows[0]["JuneUnit"].ToString();
                    lblJuneEmp.Text = ds.Tables[0].Rows[0]["JuneEmp"].ToString();
                    lblJuneShift.Text = ds.Tables[0].Rows[0]["JuneShift"].ToString();
                    JulyUnit.Text = ds.Tables[0].Rows[0]["JulyUnit"].ToString();
                    lblJulyEmp.Text = ds.Tables[0].Rows[0]["JulyEmp"].ToString();
                    lblJulyShift.Text = ds.Tables[0].Rows[0]["JulyShift"].ToString();
                    AugUnit.Text = ds.Tables[0].Rows[0]["AugUnit"].ToString();
                    lblAugEmp.Text = ds.Tables[0].Rows[0]["AugEmp"].ToString();
                    lblAugShift.Text = ds.Tables[0].Rows[0]["AugShift"].ToString();
                    SepUnit.Text = ds.Tables[0].Rows[0]["SepUnit"].ToString();
                    lblSepEmp.Text = ds.Tables[0].Rows[0]["SepEmp"].ToString();
                    lblSepShift.Text = ds.Tables[0].Rows[0]["SepShift"].ToString();
                    lblupcomingsixmonths.Text = ds.Tables[0].Rows[0]["upcoming_SixMonths"].ToString();


                    lbl_aadharno.Text = ds.Tables[0].Rows[0]["Aadhar_number"].ToString();
                    string lastYearMillingstatus=ds.Tables[0].Rows[0]["Did_millinglastyear"].ToString();

                    if (lastYearMillingstatus == "Yes")
                    {

                        trone.Visible = true;
                        trtwo.Visible = true;
                        trthree.Visible = true;
                        lbl_remQty.Text = ds.Tables[0].Rows[0]["Remain_qty"].ToString();
                        lbl_Changed_BRLQty.Text = ds.Tables[0].Rows[0]["Upgraded_BRLQty"].ToString();
                        lbl_InsBRLQty.Text = ds.Tables[0].Rows[0]["Total_BRLQty_Insp"].ToString();
                        lbl_total_agreeQty.Text = ds.Tables[0].Rows[0]["Total_AgreeQty"].ToString();
                        lblGunny_bags.Text = ds.Tables[0].Rows[0]["OldGunnybags"].ToString();
                    }
                    else
                    {
                        trone.Visible = false;

                        trtwo.Visible = false;
                        trthree.Visible = false;

                    }

                    string leez_mill_enddate = ds.Tables[0].Rows[0]["leez_mill_enddate"].ToString();
                    if (leez_mill_enddate != " ")
                    {
                        DateTime LezDate = DateTime.Parse(ds.Tables[0].Rows[0]["leez_mill_enddate"].ToString());
                        lblLizDate.Text = LezDate.ToString("dd/MMM/yyyy");
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
}