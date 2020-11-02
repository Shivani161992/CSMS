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
public partial class CSMSSurveyorLogin_Print_GodownSurveyor_QualityInspection : System.Web.UI.Page
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
                    lblSurName.Text = lblSurveyor.Text = Session["SurveyorName"].ToString();
                    lblrejectionNum.Text = hdfRejectionNumber.Value = Session["RejectionNumber"].ToString();
                    lbltruckChallan.Text = hdfTruckChallan.Value = Session["TruckChallan"].ToString();
                    lblGodown.Text = Session["GodownName"].ToString();
                    hdfGodown.Value = Session["Godown"].ToString();
                    lblMobNum.Text = UserName;

                    Session["ICGBQ"] = null;

                    GetData();

                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
            }
        }
    }


    public void GetData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "select top 1  CHK_FAQ, GS_Status, BranchID, CommodityId, CSM.CropYear, SD.district_name, CSM.DistrictId , S.Society_Name, CSM.SocietyID, T.Transporter_Name, CSM.TransporterId, convert (varchar(10), DateOfIssue, 103) as DateOfIssue , TruckNo, QtyTransffer, Bags, JutBag, Jut_OldBag, HDPEBag, HDPE_OldBag from IssueToSangrahanaKendra_CSM2018 as CSM   inner join Society_MSP as S on S.Society_Id=CSM.SocietyID and S.DistrictId=CSM.DistrictId inner join Transporter_Table as T on T.Transporter_ID=CSM.TransporterId inner join pds.districtsmp as SD On SD.distcd4=CSM.DistrictId where TruckChalanNo='" + hdfTruckChallan.Value +"' and  GodownNumber='" + hdfGodown.Value + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblCropYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();





                    lblSocietyDist.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
                    lblsociety.Text = ds.Tables[0].Rows[0]["Society_Name"].ToString();


                    lblDateOfDispatch.Text = ds.Tables[0].Rows[0]["DateOfIssue"].ToString();
                    lbltransporter.Text = ds.Tables[0].Rows[0]["Transporter_Name"].ToString();
                    lblTruckNum.Text = ds.Tables[0].Rows[0]["TruckNo"].ToString();

                    lblQuantity.Text = ds.Tables[0].Rows[0]["QtyTransffer"].ToString();
                    lblBags.Text = ds.Tables[0].Rows[0]["Bags"].ToString();
                    lblBagsType.Text = ds.Tables[0].Rows[0]["CHK_FAQ"].ToString();


                    hdfCommoditiesUparjan.Value = ds.Tables[0].Rows[0]["CommodityId"].ToString();
                    //channa
                    if (hdfCommoditiesUparjan.Value == "6")
                    {
                        hdfCommoditiesCSMS.Value = "63";

                        trgram.Visible = true;
                        trmasur.Visible = false;
                        trsarson.Visible = false;
                    }
                    //sarson
                    else if (hdfCommoditiesUparjan.Value == "12")
                    {
                        hdfCommoditiesCSMS.Value = "33";

                        trmasur.Visible = false;
                        trgram.Visible = false;
                        trsarson.Visible = true;
                    }
                    //massor
                    else if (hdfCommoditiesUparjan.Value == "63")
                    {
                        hdfCommoditiesCSMS.Value = "64";

                        trsarson.Visible = false;
                        trgram.Visible = false;
                        trmasur.Visible = true;
                    }



                   



                    string JuteBag, JuteOldBags, PPBags, PPOldBags = "";

                    JuteBag = ds.Tables[0].Rows[0]["JutBag"].ToString();
                    JuteOldBags = ds.Tables[0].Rows[0]["Jut_OldBag"].ToString();
                    PPBags = ds.Tables[0].Rows[0]["HDPEBag"].ToString();
                    PPOldBags = ds.Tables[0].Rows[0]["HDPE_OldBag"].ToString();



                    if (JuteBag != "0")
                    {
                        lblBagsType.Text = "Jute";
                    }
                    else if (JuteOldBags != "0")
                    {

                        lblBagsType.Text = "Old Jute";
                    }

                    else if (PPBags != "0")
                    {

                        lblBagsType.Text = "HDPE/PP";
                    }
                    else if (PPOldBags != "0")
                    {

                        lblBagsType.Text = "Old HDPE/PP";
                    }




                    GetCommodities();
                    if (hdfCommoditiesCSMS.Value == "63")
                    {
                        GetChallanParametersGram();
                        GetChallanParametersGramGodown();
                    }
                    else if (hdfCommoditiesCSMS.Value == "64")
                    {
                        GetChallanParametersLentils();
                        GetChallanParametersGramGodown();
                    }
                    else if (hdfCommoditiesCSMS.Value == "33")
                    {
                         GetChallanParametersMustard();
                        GetChallanParametersGramGodown();
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
    public void GetCommodities()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "Select Commodity_Id,Commodity_Name From tbl_MetaData_STORAGE_COMMODITY Where Commodity_Id='" + hdfCommoditiesCSMS.Value + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblCommodity.Text = ds.Tables[0].Rows[0]["Commodity_Name"].ToString();


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

    public void GetChallanParametersGram()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "select ForeignMatter, OtherFoodGrains, DamagedGrains, SligDamagedTouchGrains, ImmaShrivAndBroGrains, AdmixOfOtherVarieties, WeevilleGrains, MositureContent  from IssueFAQ_Final_Chana where TeruckChallan='" +hdfTruckChallan.Value+ "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //channa Gram
                    if (hdfCommoditiesCSMS.Value == "63")
                    {
                        lbluparGramForeign_Matter.Text = ds.Tables[0].Rows[0]["ForeignMatter"].ToString();
                        lbluparGramFoodGrains.Text = ds.Tables[0].Rows[0]["OtherFoodGrains"].ToString();

                        lbluparGram_DamagFoodGrains.Text = ds.Tables[0].Rows[0]["DamagedGrains"].ToString();
                        lbluparGram_SligDamagTochedGrains.Text = ds.Tables[0].Rows[0]["SligDamagedTouchGrains"].ToString();

                        lbluparGram_ImmaShrivBroGrains.Text = ds.Tables[0].Rows[0]["ImmaShrivAndBroGrains"].ToString();
                        lbluparGram_AdmixOtherVarie.Text = ds.Tables[0].Rows[0]["AdmixOfOtherVarieties"].ToString();

                        lbluparGram_WeevldGrains.Text = ds.Tables[0].Rows[0]["WeevilleGrains"].ToString();
                        lbluparGram_MoistureContent.Text = ds.Tables[0].Rows[0]["MositureContent"].ToString();

                    }


                }

                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Inspection of Truck Challan is yet to be done'); </script> ");
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
    public void GetChallanParametersGramGodown()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "select CONVERT(varchar(10), DateOfInsp, 103) DateOfInsp, [Gram_ForeignMatter]      ,[Gram_OtherFoodGrains]      ,[Gram_DamagedGrains]      ,[Gram_SligDamagedTouchGrains] ,[Gram_ImmaShrivAndBroGrains]  ,[Gram_AdmixOfOtherVarieties]  ,[Gram_WeevilleGrains] ,[Gram_MositureContent]    ,[Massur_ForeignMatter], [Massur_Admixture], [Massur_DamagedPulses], [Massur_SligDamagedPulses], [Massur_ImmaShrivellPulses], [Massur_weevilledPulses], [Massur_MoistureContent], [Sarson_ImpForeignmatt_IncTaramira], [Sarson_Admix_WOT_IncToria], [Sarson_Unripe_ShirvellImmature], [Sarson_DamagedAndWeevilled], [Sarson_SmallAtrophiedSeeds], [Sarson_MoistureContent] from CMS_GodownSurveyor_Inspection where TruckChallan='" + hdfTruckChallan.Value + "' and RejectionNumber='" + lblrejectionNum.Text + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //channa Gram
                    if (hdfCommoditiesCSMS.Value == "63")
                    {
                        lblInspectionDate.Text = ds.Tables[0].Rows[0]["DateOfInsp"].ToString();

                        lblGDNGramForeign_Matter.Text = ds.Tables[0].Rows[0]["Gram_ForeignMatter"].ToString();
                        lblGDNGramFoodGrains.Text = ds.Tables[0].Rows[0]["Gram_OtherFoodGrains"].ToString();

                        lblGDNGram_DamagFoodGrains.Text = ds.Tables[0].Rows[0]["Gram_DamagedGrains"].ToString();
                        lblGDNGram_SligDamagTochedGrains.Text = ds.Tables[0].Rows[0]["Gram_SligDamagedTouchGrains"].ToString();

                        lblGDNGram_ImmaShrivBroGrains.Text = ds.Tables[0].Rows[0]["Gram_ImmaShrivAndBroGrains"].ToString();
                        lblGDNGram_AdmixOtherVarie.Text = ds.Tables[0].Rows[0]["Gram_AdmixOfOtherVarieties"].ToString();

                        lblGDNGram_WeevldGrains.Text = ds.Tables[0].Rows[0]["Gram_WeevilleGrains"].ToString();
                        lblGDNGram_MoistureContent.Text = ds.Tables[0].Rows[0]["Gram_MositureContent"].ToString();

                    }
                        //sarson
                    else if (hdfCommoditiesCSMS.Value == "33")
                    {
                        lblInspectionDate.Text = ds.Tables[0].Rows[0]["DateOfInsp"].ToString();
                        lblGdnFM_IncTaramira.Text = ds.Tables[0].Rows[0]["Sarson_ImpForeignmatt_IncTaramira"].ToString();
                        lblGDNAM_OT_Toria.Text = ds.Tables[0].Rows[0]["Sarson_Admix_WOT_IncToria"].ToString();
                        lblGDNUR_Shvld_Imm.Text = ds.Tables[0].Rows[0]["Sarson_Unripe_ShirvellImmature"].ToString();
                        lblGDNDamWeevd.Text = ds.Tables[0].Rows[0]["Sarson_DamagedAndWeevilled"].ToString();
                        lblGDNSmallAtroSeeds.Text = ds.Tables[0].Rows[0]["Sarson_SmallAtrophiedSeeds"].ToString();
                        lblGDNMoisCont.Text = ds.Tables[0].Rows[0]["Sarson_MoistureContent"].ToString();



                    }
                        //massor
                    else if (hdfCommoditiesCSMS.Value == "64")
                    {
                        lblInspectionDate.Text = ds.Tables[0].Rows[0]["DateOfInsp"].ToString();
                        GDNmasur_Foreignmatter.Text = ds.Tables[0].Rows[0]["Massur_ForeignMatter"].ToString();
                        lblGDNmasur_admixture.Text = ds.Tables[0].Rows[0]["Massur_Admixture"].ToString();
                        lblGDNmasur_DamagedPulses.Text = ds.Tables[0].Rows[0]["Massur_DamagedPulses"].ToString();

                        lblGDNmasur_sligDamagPulses.Text = ds.Tables[0].Rows[0]["Massur_SligDamagedPulses"].ToString();
                        GDNmasur_ImmaShvldPulses.Text = ds.Tables[0].Rows[0]["Massur_ImmaShrivellPulses"].ToString();
                        GDNrmasur_WeevldPulses.Text = ds.Tables[0].Rows[0]["Massur_weevilledPulses"].ToString();

                        GDNMasur_MoistureContent.Text = ds.Tables[0].Rows[0]["Massur_MoistureContent"].ToString();

                    }


                }

                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Inspection of Truck Challan is yet to be done'); </script> ");
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


    public void GetChallanParametersLentils()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "select Foreignmatter, Admixture, Damagedpuls, Slightlydamaged, ImmatureShrivelled, Weevilled, MoistureContent from IssueFAQ_Final_Masoor where TeruckChallan='" + hdfTruckChallan.Value + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (hdfCommoditiesCSMS.Value == "64")
                    {
                        lbluparmasur_Foreignmatter.Text = ds.Tables[0].Rows[0]["Foreignmatter"].ToString();
                        lbluparmasur_admixture.Text = ds.Tables[0].Rows[0]["Admixture"].ToString();

                        lbluparmasur_DamagedPulses.Text = ds.Tables[0].Rows[0]["Damagedpuls"].ToString();
                        lbluparmasur_sligDamagPulses.Text = ds.Tables[0].Rows[0]["Slightlydamaged"].ToString();

                        lbluparmasur_ImmaShvldPulses.Text = ds.Tables[0].Rows[0]["ImmatureShrivelled"].ToString();
                        lbluparmasur_WeevldPulses.Text = ds.Tables[0].Rows[0]["Weevilled"].ToString();

                        lbluparMasur_MoistureContent.Text = ds.Tables[0].Rows[0]["MoistureContent"].ToString();
                    }


                }

                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Inspection of Truck Challan is yet to be done'); </script> ");
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

    public void GetChallanParametersMustard()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "select ImpuriForemttrincldTarmir, AdmxtrothrtypincldTor, UnripShrvlldimmatre, DamgdWvilled, Smllatrophidsid, MositureContent from IssueFAQ_Final_Sarson where TeruckChallan='" + hdfTruckChallan.Value + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (hdfCommoditiesCSMS.Value == "33")
                    {
                        lbluparFM_IncTaramira.Text = ds.Tables[0].Rows[0]["ImpuriForemttrincldTarmir"].ToString();
                        lbluparAM_OT_Toria.Text = ds.Tables[0].Rows[0]["AdmxtrothrtypincldTor"].ToString();

                        lbluparUR_Shvld_Imm.Text = ds.Tables[0].Rows[0]["UnripShrvlldimmatre"].ToString();
                        lbluparDamWeevd.Text = ds.Tables[0].Rows[0]["DamgdWvilled"].ToString();

                        lbluparSmallAtroSeeds.Text = ds.Tables[0].Rows[0]["Smllatrophidsid"].ToString();
                        lbluparMoisCont.Text = ds.Tables[0].Rows[0]["MositureContent"].ToString();
                    }

                }

                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Inspection of Truck Challan is yet to be done'); </script> ");
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
}