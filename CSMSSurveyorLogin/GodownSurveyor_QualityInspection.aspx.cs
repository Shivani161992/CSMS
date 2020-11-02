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

public partial class CSMSSurveyorLogin_GodownSurveyor_QualityInspection : System.Web.UI.Page
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
                    HdfSurveyorName.Value = Session["SurveyorName"].ToString();
                    hdfSurveyorID.Value = Session["ID"].ToString();

                    Session["ICGBQ"] = null;

                    GetGodown();

                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
            }
        }
        else
        {
            Response.Redirect("~/CSMSSurveyorLogin/SurveyorLogin_Godown.aspx");
        }
        txtDateOfInsp.Text = Request.Form[txtDateOfInsp.UniqueID];
        txtdate.Text = Request.Form[txtdate.UniqueID];

    }

    public void GetGodown()
    {
        UserName = Session["userGodown"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "select distinct SM.Godown, G.Godown_Name from SMSCom_SurveyorMaster as SM inner join tbl_MetaData_GODOWN as G on G.Godown_ID=SM.Godown where MobNum='" + UserName + "' and GETDATE()>=Fromdate and GETDATE()<=Todate";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlGodown.DataSource = ds.Tables[0];
                    ddlGodown.DataTextField = "Godown_Name";
                    ddlGodown.DataValueField = "Godown";
                    ddlGodown.DataBind();
                    ddlGodown.Items.Insert(0, "--Select--");
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
    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlcommodities.Items.Clear();
        ddlTruckChallan.Items.Clear();
        txtDateOfInsp.Text = "";
        txtSendDist.Text = "";
        txtSociety.Text = "";
        txtCropYear.Text = "";

        txtdate.Text = "";
        txttransporter.Text = "";
        txttrucknumber.Text = "";

        txtqty.Text = "";
        txtbags.Text = "";
        txtStatus.Text = "";
        txtCommodities.Text = "";
        txtbagstype.Text = "";

        UserName = Session["userGodown"].ToString();
        txtMobNum.Text = UserName;

        //channa
        txtGramForeign_Matter.Text = txtGramFoodGrains.Text = txtGram_DamagFoodGrains.Text = txtGram_SligDamagTochedGrains.Text = txtGram_ImmaShrivBroGrains.Text = txtGram_AdmixOtherVarie.Text = txtGram_WeevldGrains.Text = txtGram_MoistureContent.Text = "";
        lbluparGramForeign_Matter.Text = "";
        lbluparGramFoodGrains.Text = "";

        lbluparGram_DamagFoodGrains.Text = "";
        lbluparGram_SligDamagTochedGrains.Text = "";

        lbluparGram_ImmaShrivBroGrains.Text = "";
        lbluparGram_AdmixOtherVarie.Text = "";

        lbluparGram_WeevldGrains.Text = "";
        lbluparGram_MoistureContent.Text = "";
        //massor
        txtmasur_Foreignmatter.Text = txtmasur_admixture.Text = txtmasur_DamagedPulses.Text = txtmasur_sligDamagPulses.Text = txtmasur_ImmaShvldPulses.Text = txtmasur_WeevldPulses.Text = txtMasur_MoistureContent.Text = "";
        lbluparmasur_Foreignmatter.Text = "";
        lbluparmasur_admixture.Text = "";

        lbluparmasur_DamagedPulses.Text = "";
        lbluparmasur_sligDamagPulses.Text = "";

        lbluparmasur_ImmaShvldPulses.Text = "";
        lbluparmasur_WeevldPulses.Text = "";

        lbluparMasur_MoistureContent.Text = "";
        //sarson
        txtIFM_IncTaramira.Text = txtAM_OT_Toria.Text = txtUR_Shvld_Imm.Text = txtDamWeevd.Text = txtSmallAtroSeeds.Text = txtMoisCont.Text = "";
        lbluparFM_IncTaramira.Text = "";
        lbluparAM_OT_Toria.Text = "";

        lbluparUR_Shvld_Imm.Text = "";
        lbluparDamWeevd.Text = "";

        lbluparSmallAtroSeeds.Text = "";
        lbluparMoisCont.Text = "";
        if (ddlGodown.SelectedIndex > 0)
        {
            GetTruckChallan();
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

                select = "select TruckChalanNo from IssueToSangrahanaKendra_CSM2018 where GodownNumber='" + ddlGodown.SelectedValue.ToString() + "' and TruckChalanNo not in (select TruckChallan from CMS_GodownSurveyor_Inspection where ReceivingGodown='" + ddlGodown.SelectedValue.ToString() + "')  ";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlTruckChallan.DataSource = ds.Tables[0];
                    ddlTruckChallan.DataTextField = "TruckChalanNo";
                    ddlTruckChallan.DataValueField = "TruckChalanNo";
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
        txtDateOfInsp.Text = "";
        txtSendDist.Text = "";
        txtSociety.Text = "";
        txtCropYear.Text = "";

        txtdate.Text = "";
        txttransporter.Text = "";
        txttrucknumber.Text = "";

        txtqty.Text = "";
        txtbags.Text = "";
        txtStatus.Text = "";
        txtCommodities.Text = "";
        txtbagstype.Text = "";


        //channa
        txtGramForeign_Matter.Text = txtGramFoodGrains.Text = txtGram_DamagFoodGrains.Text = txtGram_SligDamagTochedGrains.Text = txtGram_ImmaShrivBroGrains.Text = txtGram_AdmixOtherVarie.Text = txtGram_WeevldGrains.Text = txtGram_MoistureContent.Text = "";
        lbluparGramForeign_Matter.Text = "";
        lbluparGramFoodGrains.Text = "";

        lbluparGram_DamagFoodGrains.Text = "";
        lbluparGram_SligDamagTochedGrains.Text = "";

        lbluparGram_ImmaShrivBroGrains.Text = "";
        lbluparGram_AdmixOtherVarie.Text = "";

        lbluparGram_WeevldGrains.Text = "";
        lbluparGram_MoistureContent.Text = "";
        //massor
        txtmasur_Foreignmatter.Text = txtmasur_admixture.Text = txtmasur_DamagedPulses.Text = txtmasur_sligDamagPulses.Text = txtmasur_ImmaShvldPulses.Text = txtmasur_WeevldPulses.Text = txtMasur_MoistureContent.Text = "";
        lbluparmasur_Foreignmatter.Text = "";
        lbluparmasur_admixture.Text = "";

        lbluparmasur_DamagedPulses.Text = "";
        lbluparmasur_sligDamagPulses.Text = "";

        lbluparmasur_ImmaShvldPulses.Text = "";
        lbluparmasur_WeevldPulses.Text = "";

        lbluparMasur_MoistureContent.Text = "";
        //sarson
        txtIFM_IncTaramira.Text = txtAM_OT_Toria.Text = txtUR_Shvld_Imm.Text = txtDamWeevd.Text = txtSmallAtroSeeds.Text = txtMoisCont.Text = "";
        lbluparFM_IncTaramira.Text = "";
        lbluparAM_OT_Toria.Text = "";

        lbluparUR_Shvld_Imm.Text = "";
        lbluparDamWeevd.Text = "";

        lbluparSmallAtroSeeds.Text = "";
        lbluparMoisCont.Text = "";


        txtStatus.ForeColor = System.Drawing.Color.Black;
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

                select = "select top 1  CHK_FAQ, GS_Status, BranchID, CommodityId, CSM.CropYear, SD.district_name, CSM.DistrictId , S.Society_Name, CSM.SocietyID, T.Transporter_Name, CSM.TransporterId, convert (varchar(10), DateOfIssue, 103) as DateOfIssue , TruckNo, QtyTransffer, Bags, JutBag, Jut_OldBag, HDPEBag, HDPE_OldBag from IssueToSangrahanaKendra_CSM2018 as CSM   inner join Society_MSP as S on S.Society_Id=CSM.SocietyID and S.DistrictId=CSM.DistrictId left join Transporter_Table as T on T.Transporter_ID=CSM.TransporterId and IsActive='Y' inner join pds.districtsmp as SD On SD.distcd4=CSM.DistrictId where TruckChalanNo='" + ddlTruckChallan.SelectedValue.ToString() + "' and  GodownNumber='" + ddlGodown.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    hdfStatus.Value = ds.Tables[0].Rows[0]["GS_Status"].ToString();

                    if (hdfStatus.Value == "A")
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस ट्रक चालान की जांच हो चुकी है और ये Accept कर दिया गया है|'); </script> ");
                        return;
                    }
                    else if (hdfStatus.Value == "R")
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस ट्रक चालान की जांच हो चुकी है और ये Reject कर दिया गया है|'); </script> ");
                        return;
                    }
                    else
                    {


                        hdfsocietyDist.Value = ds.Tables[0].Rows[0]["DistrictId"].ToString();
                        hdfsociety.Value = ds.Tables[0].Rows[0]["SocietyID"].ToString();
                        hdftransporterid.Value = ds.Tables[0].Rows[0]["TransporterId"].ToString();
                        hdfbranch.Value = ds.Tables[0].Rows[0]["BranchID"].ToString();

                        DateTime today = DateTime.Today;
                        txtDateOfInsp.Text = Convert.ToString(today.ToString("dd/MM/yyyy"));
                        txtSendDist.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
                        txtSociety.Text = ds.Tables[0].Rows[0]["Society_Name"].ToString();
                        txtCropYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();

                        txtdate.Text = ds.Tables[0].Rows[0]["DateOfIssue"].ToString();
                        txttransporter.Text = ds.Tables[0].Rows[0]["Transporter_Name"].ToString();
                        txttrucknumber.Text = ds.Tables[0].Rows[0]["TruckNo"].ToString();

                        txtqty.Text = ds.Tables[0].Rows[0]["QtyTransffer"].ToString();
                        txtbags.Text = ds.Tables[0].Rows[0]["Bags"].ToString();
                        txtStatus.Text = ds.Tables[0].Rows[0]["CHK_FAQ"].ToString();
                        btnQualityInspecction.Enabled = true;


                        string JuteBag, JuteOldBags, PPBags, PPOldBags = "";

                        JuteBag = ds.Tables[0].Rows[0]["JutBag"].ToString();
                        JuteOldBags = ds.Tables[0].Rows[0]["Jut_OldBag"].ToString();
                        PPBags = ds.Tables[0].Rows[0]["HDPEBag"].ToString();
                        PPOldBags = ds.Tables[0].Rows[0]["HDPE_OldBag"].ToString();



                        if (JuteBag != "0")
                        {
                            txtbagstype.Text = "Jute";
                        }
                        else if (JuteOldBags != "0")
                        {

                            txtbagstype.Text = "Old Jute";
                        }

                        else if (PPBags != "0")
                        {

                            txtbagstype.Text = "HDPE/PP";
                        }
                        else if (PPOldBags != "0")
                        {

                            txtbagstype.Text = "Old HDPE/PP";
                        }


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

                        if (txtStatus.Text == "N")
                        {
                            txtStatus.Text = "उपार्जन केंद्र सुर्वेयर द्वारा अप्रमाणित";

                            // txtStatus.BackColor = System.Drawing.Color.Red;
                            txtStatus.ForeColor = System.Drawing.Color.Red;
                            btnQualityInspecction.Enabled = false;

                            //channa
                            txtGramForeign_Matter.Enabled = txtGramFoodGrains.Enabled = txtGram_DamagFoodGrains.Enabled = txtGram_SligDamagTochedGrains.Enabled = txtGram_ImmaShrivBroGrains.Enabled = txtGram_AdmixOtherVarie.Enabled = txtGram_WeevldGrains.Enabled = txtGram_MoistureContent.Enabled = false;

                            //massor
                            txtmasur_Foreignmatter.Enabled = txtmasur_admixture.Enabled = txtmasur_DamagedPulses.Enabled = txtmasur_sligDamagPulses.Enabled = txtmasur_ImmaShvldPulses.Enabled = txtmasur_WeevldPulses.Enabled = txtMasur_MoistureContent.Enabled = false;

                            //sarson
                            txtIFM_IncTaramira.Enabled = txtAM_OT_Toria.Enabled = txtUR_Shvld_Imm.Enabled = txtDamWeevd.Enabled = txtSmallAtroSeeds.Enabled = txtMoisCont.Enabled = false;




                        }
                        else if (txtStatus.Text == "A")
                        {
                            txtStatus.Text = "उपार्जन केंद्र सुर्वेयर द्वारा प्रमाणित";
                            txtStatus.ForeColor = System.Drawing.Color.Green;
                            btnQualityInspecction.Enabled = true;

                            if (hdfCommoditiesCSMS.Value == "63")
                            {
                                GetChallanParametersGram();
                            }
                            else if (hdfCommoditiesCSMS.Value == "64")
                            {
                                GetChallanParametersLentils();
                            }
                            else if (hdfCommoditiesCSMS.Value == "33")
                            {
                                GetChallanParametersMustard();
                            }

                            //channa
                            txtGramForeign_Matter.Enabled = txtGramFoodGrains.Enabled = txtGram_DamagFoodGrains.Enabled = txtGram_SligDamagTochedGrains.Enabled = txtGram_ImmaShrivBroGrains.Enabled = txtGram_AdmixOtherVarie.Enabled = txtGram_WeevldGrains.Enabled = txtGram_MoistureContent.Enabled = true;

                            //massor
                            txtmasur_Foreignmatter.Enabled = txtmasur_admixture.Enabled = txtmasur_DamagedPulses.Enabled = txtmasur_sligDamagPulses.Enabled = txtmasur_ImmaShvldPulses.Enabled = txtmasur_WeevldPulses.Enabled = txtMasur_MoistureContent.Enabled = true;

                            //sarson
                            txtIFM_IncTaramira.Enabled = txtAM_OT_Toria.Enabled = txtUR_Shvld_Imm.Enabled = txtDamWeevd.Enabled = txtSmallAtroSeeds.Enabled = txtMoisCont.Enabled = true;
                        }
                        else if (txtStatus.Text == "R")
                        {
                            txtStatus.Text = "उपार्जन केंद्र सुर्वेयर द्वारा अस्वीकृत";

                            txtStatus.ForeColor = System.Drawing.Color.Red;
                            btnQualityInspecction.Enabled = false;
                            if (hdfCommoditiesCSMS.Value == "63")
                            {
                                GetChallanParametersGram();
                            }
                            else if (hdfCommoditiesCSMS.Value == "64")
                            {
                                GetChallanParametersLentils();
                            }
                            else if (hdfCommoditiesCSMS.Value == "33")
                            {
                                GetChallanParametersMustard();
                            }

                            //channa
                            txtGramForeign_Matter.Enabled = txtGramFoodGrains.Enabled = txtGram_DamagFoodGrains.Enabled = txtGram_SligDamagTochedGrains.Enabled = txtGram_ImmaShrivBroGrains.Enabled = txtGram_AdmixOtherVarie.Enabled = txtGram_WeevldGrains.Enabled = txtGram_MoistureContent.Enabled = false;

                            //massor
                            txtmasur_Foreignmatter.Enabled = txtmasur_admixture.Enabled = txtmasur_DamagedPulses.Enabled = txtmasur_sligDamagPulses.Enabled = txtmasur_ImmaShvldPulses.Enabled = txtmasur_WeevldPulses.Enabled = txtMasur_MoistureContent.Enabled = false;

                            //sarson
                            txtIFM_IncTaramira.Enabled = txtAM_OT_Toria.Enabled = txtUR_Shvld_Imm.Enabled = txtDamWeevd.Enabled = txtSmallAtroSeeds.Enabled = txtMoisCont.Enabled = false;
                        }









                        GetCommodities();
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
                    txtCommodities.Text = ds.Tables[0].Rows[0]["Commodity_Name"].ToString();


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

                select = "select ForeignMatter, OtherFoodGrains, DamagedGrains, SligDamagedTouchGrains, ImmaShrivAndBroGrains, AdmixOfOtherVarieties, WeevilleGrains, MositureContent  from IssueFAQ_Final_Chana where TeruckChallan='" + ddlTruckChallan.SelectedValue.ToString() + "'";
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
    public void GetChallanParametersLentils()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "select Foreignmatter, Admixture, Damagedpuls, Slightlydamaged, ImmatureShrivelled, Weevilled, MoistureContent from IssueFAQ_Final_Masoor where TeruckChallan='" + ddlTruckChallan.SelectedValue.ToString() + "'";
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

                select = "select ImpuriForemttrincldTarmir, AdmxtrothrtypincldTor, UnripShrvlldimmatre, DamgdWvilled, Smllatrophidsid, MositureContent from IssueFAQ_Final_Sarson where TeruckChallan='" + ddlTruckChallan.SelectedValue.ToString() + "'";
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


    protected void btnQualityInspecction_Click(object sender, EventArgs e)
    {
       

        UserName = Session["userGodown"].ToString();
        txtMobNum.Text = UserName;
        //btnsendOTP.Enabled = true;
        GetOTPCount();

        if(hdfOTPCount.Value=="0")
        {
            GetQualityInspection();
            trotp.Visible = true;
            trAcceptReject.Visible = false;
            btnROTP.Enabled = false;
            btnROTP.Visible = false ;
            btnsendOTP.Enabled = true;
            btnsendOTP.Visible = true;

        }
        else if (hdfOTPCount.Value == "1")
        {
           
            GetOTPFirstTime();
            if (hdfOTPFirstOTPTime.Value == "Y")
            {
                trotp.Visible = true;
                btnROTP.Enabled = true;
                btnROTP.Visible = true;
                btnsendOTP.Enabled = false;
                btnsendOTP.Visible = false;
                GetQualityInspection();
            }
            else {
                GetQualityInspection();
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('पहला OTP भेजे गये अभी 5 mintues नहीं हुए हैं |'); </script> ");
                return;
            
            }
        
        }
        else if (hdfOTPCount.Value == "2")
        {
            GetOTPSecondTime();
            if (hdfOTPFirstOTPTime.Value == "Y")
            {

                trotp.Visible = false;
                GetQualityInspection();
                trAcceptReject.Visible = true;
            }
            else
            {
                GetQualityInspection();
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('पहला OTP भेजे गये अभी 10 mintues नहीं हुए हैं |'); </script> ");
                return;

            }
        }
        

       

    }
    public void GetOTPCount()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "select CountOTP from CMS_GodownSurveyor_InspectionOTP  where TruckChallan='"+ddlTruckChallan.SelectedValue.ToString()+"' and DistrictID='"+hdfsocietyDist.Value+"'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    hdfOTPCount.Value = ds.Tables[0].Rows[0]["CountOTP"].ToString();


                }
                else 
                {
                    hdfOTPCount.Value = "0";
                
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

    public void GetOTPFirstTime()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "Select CAse When GETDATE() < DateAdd(Mi,5,Max(First_OTPTime)) THen 'N' Else 'Y' end as chkapr from CMS_GodownSurveyor_InspectionOTP Where TruckChallan='"+ddlTruckChallan.SelectedValue.ToString()+"'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    hdfOTPFirstOTPTime.Value = ds.Tables[0].Rows[0]["chkapr"].ToString();


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
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }

    public void GetOTPSecondTime()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "Select CAse When GETDATE() < DateAdd(Mi,10,Max(Resend_OTPTime)) THen 'N' Else 'Y' end as chkapr from CMS_GodownSurveyor_InspectionOTP Where TruckChallan='" + ddlTruckChallan.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    hdfOTPFirstOTPTime.Value = ds.Tables[0].Rows[0]["chkapr"].ToString();


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
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }
    public void GetQualityInspection()
    {
        //trotp.Visible = true;

        UserName = Session["userGodown"].ToString();
        txtMobNum.Text = UserName;
        btnsendOTP.Enabled = true;

        //sarsonMustard
        if (hdfCommoditiesCSMS.Value == "33")
        {
            if (float.Parse(lblIFM_IncTaramira.Text) >= float.Parse(txtIFM_IncTaramira.Text) && float.Parse(lblAM_OT_Toria.Text) >= float.Parse(txtAM_OT_Toria.Text) && float.Parse(lblUR_Shvld_Imm.Text) >= float.Parse(txtUR_Shvld_Imm.Text) && float.Parse(lblDamWeevd.Text) >= float.Parse(txtDamWeevd.Text) && float.Parse(lblSmallAtroSeeds.Text) >= float.Parse(txtSmallAtroSeeds.Text) && float.Parse(lblMoisCont.Text) >= float.Parse(txtMoisCont.Text))
            {

                btnAccept.Enabled = true;
                btnReject.Enabled = false;
                btnQualityInspecction.Enabled = false;
                btnQualityInspecction.Text = "Submitted";
                ddlTruckChallan.Enabled = false;
                ddlGodown.Enabled = false;


                //channa
                txtGramForeign_Matter.Enabled = txtGramFoodGrains.Enabled = txtGram_DamagFoodGrains.Enabled = txtGram_SligDamagTochedGrains.Enabled = txtGram_ImmaShrivBroGrains.Enabled = txtGram_AdmixOtherVarie.Enabled = txtGram_WeevldGrains.Enabled = txtGram_MoistureContent.Enabled = false;

                //massor
                txtmasur_Foreignmatter.Enabled = txtmasur_admixture.Enabled = txtmasur_DamagedPulses.Enabled = txtmasur_sligDamagPulses.Enabled = txtmasur_ImmaShvldPulses.Enabled = txtmasur_WeevldPulses.Enabled = txtMasur_MoistureContent.Enabled = false;

                //sarson
                txtIFM_IncTaramira.Enabled = txtAM_OT_Toria.Enabled = txtUR_Shvld_Imm.Enabled = txtDamWeevd.Enabled = txtSmallAtroSeeds.Enabled = txtMoisCont.Enabled = false;





            }
            else
            {
                btnAccept.Enabled = false;
                btnReject.Enabled = true;

                btnQualityInspecction.Enabled = false;
                btnQualityInspecction.Text = "Submitted";
                ddlTruckChallan.Enabled = false;
                ddlGodown.Enabled = false;


                //channa
                txtGramForeign_Matter.Enabled = txtGramFoodGrains.Enabled = txtGram_DamagFoodGrains.Enabled = txtGram_SligDamagTochedGrains.Enabled = txtGram_ImmaShrivBroGrains.Enabled = txtGram_AdmixOtherVarie.Enabled = txtGram_WeevldGrains.Enabled = txtGram_MoistureContent.Enabled = false;

                //massor
                txtmasur_Foreignmatter.Enabled = txtmasur_admixture.Enabled = txtmasur_DamagedPulses.Enabled = txtmasur_sligDamagPulses.Enabled = txtmasur_ImmaShvldPulses.Enabled = txtmasur_WeevldPulses.Enabled = txtMasur_MoistureContent.Enabled = false;

                //sarson
                txtIFM_IncTaramira.Enabled = txtAM_OT_Toria.Enabled = txtUR_Shvld_Imm.Enabled = txtDamWeevd.Enabled = txtSmallAtroSeeds.Enabled = txtMoisCont.Enabled = false;


            }


        }
        //Channa Gram
        else if (hdfCommoditiesCSMS.Value == "63")
        {
            trotp.Visible = true;
            btnsendOTP.Enabled = true;
            if (float.Parse(lblGramForeign_Matter.Text) >= float.Parse(txtGramForeign_Matter.Text) && float.Parse(lblGramFoodGrains.Text) >= float.Parse(txtGramFoodGrains.Text) && float.Parse(lblGram_DamagFoodGrains.Text) >= float.Parse(txtGram_DamagFoodGrains.Text) && float.Parse(lblGram_SligDamagTochedGrains.Text) >= float.Parse(txtGram_SligDamagTochedGrains.Text) && float.Parse(lblGram_ImmaShrivBroGrains.Text) >= float.Parse(txtGram_ImmaShrivBroGrains.Text) && float.Parse(lblGram_AdmixOtherVarie.Text) >= float.Parse(txtGram_AdmixOtherVarie.Text) && float.Parse(lblGram_WeevldGrains.Text) >= float.Parse(txtGram_WeevldGrains.Text) && float.Parse(lblGram_MoistureContent.Text) >= float.Parse(txtGram_MoistureContent.Text))
            {
                btnAccept.Enabled = true;
                btnReject.Enabled = false;
                btnQualityInspecction.Enabled = false;
                btnQualityInspecction.Text = "Submitted";

                ddlTruckChallan.Enabled = false;
                ddlGodown.Enabled = false;


                //channa
                txtGramForeign_Matter.Enabled = txtGramFoodGrains.Enabled = txtGram_DamagFoodGrains.Enabled = txtGram_SligDamagTochedGrains.Enabled = txtGram_ImmaShrivBroGrains.Enabled = txtGram_AdmixOtherVarie.Enabled = txtGram_WeevldGrains.Enabled = txtGram_MoistureContent.Enabled = false;

                //massor
                txtmasur_Foreignmatter.Enabled = txtmasur_admixture.Enabled = txtmasur_DamagedPulses.Enabled = txtmasur_sligDamagPulses.Enabled = txtmasur_ImmaShvldPulses.Enabled = txtmasur_WeevldPulses.Enabled = txtMasur_MoistureContent.Enabled = false;

                //sarson
                txtIFM_IncTaramira.Enabled = txtAM_OT_Toria.Enabled = txtUR_Shvld_Imm.Enabled = txtDamWeevd.Enabled = txtSmallAtroSeeds.Enabled = txtMoisCont.Enabled = false;








            }
            else
            {
                btnAccept.Enabled = false;
                btnReject.Enabled = true;

                btnQualityInspecction.Enabled = false;
                btnQualityInspecction.Text = "Submitted";

                ddlTruckChallan.Enabled = false;
                ddlGodown.Enabled = false;


                //channa
                txtGramForeign_Matter.Enabled = txtGramFoodGrains.Enabled = txtGram_DamagFoodGrains.Enabled = txtGram_SligDamagTochedGrains.Enabled = txtGram_ImmaShrivBroGrains.Enabled = txtGram_AdmixOtherVarie.Enabled = txtGram_WeevldGrains.Enabled = txtGram_MoistureContent.Enabled = false;

                //massor
                txtmasur_Foreignmatter.Enabled = txtmasur_admixture.Enabled = txtmasur_DamagedPulses.Enabled = txtmasur_sligDamagPulses.Enabled = txtmasur_ImmaShvldPulses.Enabled = txtmasur_WeevldPulses.Enabled = txtMasur_MoistureContent.Enabled = false;

                //sarson
                txtIFM_IncTaramira.Enabled = txtAM_OT_Toria.Enabled = txtUR_Shvld_Imm.Enabled = txtDamWeevd.Enabled = txtSmallAtroSeeds.Enabled = txtMoisCont.Enabled = false;



            }
        }
        //massor lentils
        else if (hdfCommoditiesCSMS.Value == "64")
        {
            if (float.Parse(lblmasur_Foreignmatter.Text) >= float.Parse(txtmasur_Foreignmatter.Text) && float.Parse(lblmasur_admixture.Text) >= float.Parse(txtmasur_admixture.Text) && float.Parse(lblmasur_DamagedPulses.Text) >= float.Parse(txtmasur_DamagedPulses.Text) && float.Parse(lblmasur_sligDamagPulses.Text) >= float.Parse(txtmasur_sligDamagPulses.Text) && float.Parse(lblmasur_ImmaShvldPulses.Text) >= float.Parse(txtmasur_ImmaShvldPulses.Text) && float.Parse(lblmasur_WeevldPulses.Text) >= float.Parse(txtmasur_WeevldPulses.Text) && float.Parse(lblMasur_MoistureContent.Text) >= float.Parse(txtMasur_MoistureContent.Text))
            {

                btnAccept.Enabled = true;
                btnReject.Enabled = false;
                btnQualityInspecction.Enabled = false;
                btnQualityInspecction.Text = "Submitted";

                ddlTruckChallan.Enabled = false;
                ddlGodown.Enabled = false;


                //channa
                txtGramForeign_Matter.Enabled = txtGramFoodGrains.Enabled = txtGram_DamagFoodGrains.Enabled = txtGram_SligDamagTochedGrains.Enabled = txtGram_ImmaShrivBroGrains.Enabled = txtGram_AdmixOtherVarie.Enabled = txtGram_WeevldGrains.Enabled = txtGram_MoistureContent.Enabled = false;

                //massor
                txtmasur_Foreignmatter.Enabled = txtmasur_admixture.Enabled = txtmasur_DamagedPulses.Enabled = txtmasur_sligDamagPulses.Enabled = txtmasur_ImmaShvldPulses.Enabled = txtmasur_WeevldPulses.Enabled = txtMasur_MoistureContent.Enabled = false;

                //sarson
                txtIFM_IncTaramira.Enabled = txtAM_OT_Toria.Enabled = txtUR_Shvld_Imm.Enabled = txtDamWeevd.Enabled = txtSmallAtroSeeds.Enabled = txtMoisCont.Enabled = false;






            }
            else
            {
                btnAccept.Enabled = false;
                btnReject.Enabled = true;

                btnQualityInspecction.Enabled = false;
                btnQualityInspecction.Text = "Submitted";

                ddlTruckChallan.Enabled = false;
                ddlGodown.Enabled = false;


                //channa
                txtGramForeign_Matter.Enabled = txtGramFoodGrains.Enabled = txtGram_DamagFoodGrains.Enabled = txtGram_SligDamagTochedGrains.Enabled = txtGram_ImmaShrivBroGrains.Enabled = txtGram_AdmixOtherVarie.Enabled = txtGram_WeevldGrains.Enabled = txtGram_MoistureContent.Enabled = false;

                //massor
                txtmasur_Foreignmatter.Enabled = txtmasur_admixture.Enabled = txtmasur_DamagedPulses.Enabled = txtmasur_sligDamagPulses.Enabled = txtmasur_ImmaShvldPulses.Enabled = txtmasur_WeevldPulses.Enabled = txtMasur_MoistureContent.Enabled = false;

                //sarson
                txtIFM_IncTaramira.Enabled = txtAM_OT_Toria.Enabled = txtUR_Shvld_Imm.Enabled = txtDamWeevd.Enabled = txtSmallAtroSeeds.Enabled = txtMoisCont.Enabled = false;



            }

        }

    }
       


    protected void btnsendOTP_Click(object sender, EventArgs e)
    {

        if (txtMobNum.Text != "")
        {
            // Call Jquery Function TimerFunc()
            txtCheckOTP.Text = "";


            GenerateUniqueOTP();

            btnsendOTP.Enabled = false;
            btnsendOTP.Visible = true;
            btnROTP.Enabled = false;
            btnROTP.Visible = false;

            txtCheckOTP.Enabled = true;
            btncheckOTP.Enabled = true;
            txtCheckOTP.Focus();

             using (con = new SqlConnection(strcon))
            try
            {
              con.Open();
              string strselect = "";
              string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
              strselect = "insert into CMS_GodownSurveyor_InspectionOTP (TruckChallan, DistrictID, OTP, First_OTPTime, CreatedDate, IP, CountOTP) values ('" + ddlTruckChallan.SelectedValue.ToString() + "', '" + hdfsocietyDist.Value + "', '" + GenerateOTP + "', GetDate(), GetDate(), '" + ip + "', '1' ) ";
              cmd = new SqlCommand(strselect, con);
              string check = (string)cmd.ExecuteScalar();
            }
            catch
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please check and Enter valid data...'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
            ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:TimerFunc(); ", true);
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Mobile Number Not Available'); </script> ");
            return;
        }
    }

    protected void GenerateUniqueOTP()
    {
        string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
        string numbers = "1234567890";

        string characters = numbers;

        characters += alphabets + small_alphabets + numbers;

        string MONumber = ddlTruckChallan.SelectedItem.ToString();
        int lastdigit = int.Parse(MONumber.Substring(6));
        int length = 0;
        if (lastdigit >= 6)
        {
            length = 8;
        }
        else if (lastdigit >= 3 && lastdigit <= 5)
        {
            length = 6;
        }
        else
        {
            length = 5;
        }

        //int length = int.Parse(ddlMvmtNo.SelectedItem.Value);
        string otp = string.Empty;
        for (int i = 0; i < length; i++)
        {
            string character = string.Empty;
            do
            {
                int index = new Random().Next(0, characters.Length);
                character = characters.ToCharArray()[index].ToString();
            } while (otp.IndexOf(character) != -1);
            otp += character;
        }

        GenerateOTP = otp;
        OTPSMS = "'" + txtCommodities.Text + "' Truck Challan Number " + ddlTruckChallan.SelectedValue.ToString() + " OTP Is '" + otp + "'";
        hdfOTP.Value = "";
        hdfOTP.Value = otp;

        SMS Message = new SMS();

        string MobileNo = "";

        MobileNo = txtMobNum.Text;
        //Message.SendSMS(MobileNo, OTPSMS);
    }


    protected void btncheckOTP_Click(object sender, EventArgs e)
    {
        txtCheckOTP.Enabled = false;

        if (hdfOTP.Value == txtCheckOTP.Text)
        {
            lblmsg.Text = "Success";
            txtCheckOTP.Enabled = false;
            trAcceptReject.Visible = true;
            trotp.Visible = false;

        }
        else if (hdfOTP.Value != txtCheckOTP.Text)
        {
            lblmsg.Text = "Wrong OTP";
            trAcceptReject.Visible = false;
            trotp.Visible = true;

            txtCheckOTP.Enabled = true;
            txtCheckOTP.Text = "";
            txtCheckOTP.Focus();
        }
    }


    protected void btnAccept_Click(object sender, EventArgs e)
    {
        using (con = new SqlConnection(strcon))
            try
            {



                con.Open();
                int IsAvailable = 0;
                String CheckData = "";
                CheckData = "Select * From CMS_GodownSurveyor_Inspection where TruckChallan='" + ddlTruckChallan.SelectedValue.ToString() + "' and ReceivingGodown='"+ddlGodown.SelectedValue.ToString()+"' ";
                da = new SqlDataAdapter(CheckData, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    IsAvailable = 1;
                }

                if (IsAvailable == 0)
                {


                    string qrey = "select max(Insp_ID) as Insp_ID  from CMS_GodownSurveyor_Inspection where  LEN(Insp_ID)<15 ";
                    da = new SqlDataAdapter(qrey, con);

                    ds = new DataSet();
                    da.Fill(ds);

                    DataRow dr = ds.Tables[0].Rows[0];

                    gatepass = ds.Tables[0].Rows[0]["Insp_ID"].ToString();

                    if (gatepass == "")
                    {
                        gatepass = "18" + "01";
                    }
                    else
                    {
                        getnum = Convert.ToInt32(gatepass);

                        getnum = getnum + 1;
                        gatepass = getnum.ToString();
                    }

                    if (hdfCommoditiesCSMS.Value == "64")
                    {
                        InspectionID = "64" + gatepass;
                    }
                    else if (hdfCommoditiesCSMS.Value == "63")
                    {
                        InspectionID = "63" + gatepass;
                    }
                    else if (hdfCommoditiesCSMS.Value == "33")
                    {
                        InspectionID = "33" + gatepass;
                    }

                    string strselect = "";
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                    ConvertServerDate ServerDate = new ConvertServerDate();
                    string ConvertDateofdispatch = ServerDate.getDate_MDY(txtdate.Text);
                    string ConvertDateofinsp = ServerDate.getDate_MDY(txtDateOfInsp.Text);
                    string BookNumber = "A" + InspectionID + ddlTruckChallan.SelectedValue.ToString();

                    //massor red lentil
                    if (hdfCommoditiesCSMS.Value == "64")
                    {
                        strselect = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                            "update IssueToSangrahanaKendra_CSM2018 set GS_Status='A', G_Surveyor='" + hdfSurveyorID.Value + "' where TruckChalanNo='" + ddlTruckChallan.SelectedValue.ToString() + "' and GodownNumber='" + ddlGodown.SelectedValue.ToString() + "'";

                        strselect += "insert into CMS_GodownSurveyor_Inspection([GodownSurveyorInsp_ID], [Commodity] ,[TruckChallan], [SocietyDist] , [Society], [CropYear], [DateofDispatch], [TransporterID]      ,[TruckNumber]      ,[Quantity]      ,[Bags]      ,[BagsType]      ,[Status]      ,[BookNumber]      ,[AcceptanceNumber]      ,[RejectionNumber]      ,[ReceivingGodown]      ,[ReceivingBranch]      ,[Sarson]      ,[Sarson_ImpForeignmatt_IncTaramira]      ,[Sarson_Admix_WOT_IncToria]      ,[Sarson_Unripe_ShirvellImmature]      ,[Sarson_DamagedAndWeevilled]      ,[Sarson_SmallAtrophiedSeeds]      ,[Sarson_MoistureContent]      ,[Massor]      ,[Massur_ForeignMatter]      ,[Massur_Admixture]      ,[Massur_DamagedPulses]      ,[Massur_SligDamagedPulses]      ,[Massur_ImmaShrivellPulses]      ,[Massur_weevilledPulses]      ,[Massur_MoistureContent]      ,[Channa]      ,[Gram_ForeignMatter]      ,[Gram_OtherFoodGrains]      ,[Gram_DamagedGrains]      ,[Gram_SligDamagedTouchGrains]       ,[Gram_ImmaShrivAndBroGrains] ,[Gram_AdmixOfOtherVarieties] ,[Gram_WeevilleGrains] ,[Gram_MositureContent]  ,[CreatedDate] ,[IP], Insp_ID, [SurveyorID] ,[SurveyorName], [SurveyorMob], DateOfInsp) values ('" + InspectionID + "','" + hdfCommoditiesCSMS.Value + "','" + ddlTruckChallan.SelectedValue.ToString() + "','" + hdfsocietyDist.Value + "','" + hdfsociety.Value + "','" + txtCropYear.Text + "','" + ConvertDateofdispatch + "','" + hdftransporterid.Value + "','" + txttrucknumber.Text + "','" + txtqty.Text + "','" + txtbags.Text + "','" + txtbagstype.Text + "','Accept','" + BookNumber + "','" + BookNumber + "','0', '" + ddlGodown.SelectedValue.ToString() + "','" + hdfbranch.Value + "','0','0','0','0','0','0','0','Y','" + txtmasur_Foreignmatter.Text + "','" + txtmasur_admixture.Text + "','" + txtmasur_DamagedPulses.Text + "','" + txtmasur_sligDamagPulses.Text + "','" + txtmasur_ImmaShvldPulses.Text + "','" + txtmasur_WeevldPulses.Text + "','" + txtMasur_MoistureContent.Text + "','0','0','0','0','0','0','0','0','0', GetDate(), '" + ip + "','" + gatepass + "', '" + hdfSurveyorID.Value + "','" + HdfSurveyorName.Value + "','" + txtMobNum.Text + "','" + ConvertDateofinsp + "')";
                        strselect += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";

                    }
                    //Channa gram
                    else if (hdfCommoditiesCSMS.Value == "63")
                    {
                        strselect = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                           "update IssueToSangrahanaKendra_CSM2018 set GS_Status='A', G_Surveyor='" + hdfSurveyorID.Value + "' where TruckChalanNo='" + ddlTruckChallan.SelectedValue.ToString() + "' and GodownNumber='" + ddlGodown.SelectedValue.ToString() + "'";

                        strselect += "insert into CMS_GodownSurveyor_Inspection([GodownSurveyorInsp_ID], [Commodity] ,[TruckChallan], [SocietyDist] , [Society], [CropYear], [DateofDispatch], [TransporterID]      ,[TruckNumber]      ,[Quantity]      ,[Bags]      ,[BagsType]      ,[Status]      ,[BookNumber]      ,[AcceptanceNumber]      ,[RejectionNumber]      ,[ReceivingGodown]      ,[ReceivingBranch]      ,[Sarson]      ,[Sarson_ImpForeignmatt_IncTaramira]      ,[Sarson_Admix_WOT_IncToria]      ,[Sarson_Unripe_ShirvellImmature]      ,[Sarson_DamagedAndWeevilled]      ,[Sarson_SmallAtrophiedSeeds]      ,[Sarson_MoistureContent]      ,[Massor]      ,[Massur_ForeignMatter]      ,[Massur_Admixture]      ,[Massur_DamagedPulses]      ,[Massur_SligDamagedPulses]      ,[Massur_ImmaShrivellPulses]      ,[Massur_weevilledPulses]      ,[Massur_MoistureContent]      ,[Channa]      ,[Gram_ForeignMatter]      ,[Gram_OtherFoodGrains]      ,[Gram_DamagedGrains]      ,[Gram_SligDamagedTouchGrains]       ,[Gram_ImmaShrivAndBroGrains] ,[Gram_AdmixOfOtherVarieties] ,[Gram_WeevilleGrains] ,[Gram_MositureContent]  ,[CreatedDate] ,[IP], Insp_ID, [SurveyorID] ,[SurveyorName], [SurveyorMob], DateOfInsp) values ('" + InspectionID + "','" + hdfCommoditiesCSMS.Value + "','" + ddlTruckChallan.SelectedValue.ToString() + "','" + hdfsocietyDist.Value + "','" + hdfsociety.Value + "','" + txtCropYear.Text + "','" + ConvertDateofdispatch + "','" + hdftransporterid.Value + "','" + txttrucknumber.Text + "','" + txtqty.Text + "','" + txtbags.Text + "','" + txtbagstype.Text + "','Accept','" + BookNumber + "','" + BookNumber + "','0', '" + ddlGodown.SelectedValue.ToString() + "','" + hdfbranch.Value + "','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','Y','" + txtGramForeign_Matter.Text + "','" + txtGramFoodGrains.Text + "','" + txtGram_DamagFoodGrains.Text + "','" + txtGram_SligDamagTochedGrains.Text + "','" + txtGram_ImmaShrivBroGrains.Text + "','" + txtGram_AdmixOtherVarie.Text + "','" + txtGram_WeevldGrains.Text + "','" + txtGram_MoistureContent.Text + "', GetDate(), '" + ip + "','" + gatepass + "', '" + hdfSurveyorID.Value + "','" + HdfSurveyorName.Value + "','" + txtMobNum.Text + "','" + ConvertDateofinsp + "')";
                        strselect += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";

                    }
                    //sarson mustard
                    else if (hdfCommoditiesCSMS.Value == "33")
                    {
                        strselect = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                           "update IssueToSangrahanaKendra_CSM2018 set GS_Status='A', G_Surveyor='" + hdfSurveyorID.Value + "' where TruckChalanNo='" + ddlTruckChallan.SelectedValue.ToString() + "' and GodownNumber='" + ddlGodown.SelectedValue.ToString() + "'";


                        strselect += "insert into CMS_GodownSurveyor_Inspection([GodownSurveyorInsp_ID], [Commodity] ,[TruckChallan], [SocietyDist] , [Society], [CropYear], [DateofDispatch], [TransporterID]      ,[TruckNumber]      ,[Quantity]      ,[Bags]      ,[BagsType]      ,[Status]      ,[BookNumber]      ,[AcceptanceNumber]      ,[RejectionNumber]      ,[ReceivingGodown]      ,[ReceivingBranch]      ,[Sarson]      ,[Sarson_ImpForeignmatt_IncTaramira]      ,[Sarson_Admix_WOT_IncToria]      ,[Sarson_Unripe_ShirvellImmature]      ,[Sarson_DamagedAndWeevilled]      ,[Sarson_SmallAtrophiedSeeds]      ,[Sarson_MoistureContent]      ,[Massor]      ,[Massur_ForeignMatter]      ,[Massur_Admixture]      ,[Massur_DamagedPulses]      ,[Massur_SligDamagedPulses]      ,[Massur_ImmaShrivellPulses]      ,[Massur_weevilledPulses]      ,[Massur_MoistureContent]      ,[Channa]      ,[Gram_ForeignMatter]      ,[Gram_OtherFoodGrains]      ,[Gram_DamagedGrains]      ,[Gram_SligDamagedTouchGrains]       ,[Gram_ImmaShrivAndBroGrains] ,[Gram_AdmixOfOtherVarieties] ,[Gram_WeevilleGrains] ,[Gram_MositureContent]  ,[CreatedDate] ,[IP], Insp_ID, [SurveyorID] ,[SurveyorName], [SurveyorMob], DateOfInsp) values ('" + InspectionID + "','" + hdfCommoditiesCSMS.Value + "','" + ddlTruckChallan.SelectedValue.ToString() + "','" + hdfsocietyDist.Value + "','" + hdfsociety.Value + "','" + txtCropYear.Text + "','" + ConvertDateofdispatch + "','" + hdftransporterid.Value + "','" + txttrucknumber.Text + "','" + txtqty.Text + "','" + txtbags.Text + "','" + txtbagstype.Text + "','Accept','" + BookNumber + "','" + BookNumber + "','0', '" + ddlGodown.SelectedValue.ToString() + "','" + hdfbranch.Value + "','Y','" + txtIFM_IncTaramira.Text + "','" + txtAM_OT_Toria.Text + "','" + txtUR_Shvld_Imm.Text + "','" + txtDamWeevd.Text + "','" + txtSmallAtroSeeds.Text + "','" + txtMoisCont.Text + "','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0', GetDate(), '" + ip + "','" + gatepass + "', '" + hdfSurveyorID.Value + "','" + HdfSurveyorName.Value + "','" + txtMobNum.Text + "','" + ConvertDateofinsp + "')";
                        strselect += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";

                    }
                    cmd = new SqlCommand(strselect, con);
                    string check = (string)cmd.ExecuteScalar();

                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");
                    btnAccept.Enabled = false;
                    trNumber.Visible = true;
                    Label2.Visible = true;
                    Label2.Text = "Your Acceptance Number is :- " + BookNumber;
                }
                else
                {
                    //Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपने इस ट्रक चालान का Quality Inspection कर चुकें हैं इसलिए आप इसको दुबारा नहीं कर सकते|'); </script> ");
                    //btnAccept.Enabled = btnReject.Enabled = false;
                    //return;
                }
            }
            catch
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please check and Enter valid data...'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }

    }
    protected void btnReject_Click(object sender, EventArgs e)
    {

        using (con = new SqlConnection(strcon))
            try
            {



                con.Open();
                 con.Open();
                int IsAvailable = 0;
                String CheckData = "";
                CheckData = "Select * From CMS_GodownSurveyor_Inspection where TruckChallan='" + ddlTruckChallan.SelectedValue.ToString() + "' and ReceivingGodown='" + ddlGodown.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(CheckData, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    IsAvailable = 1;
                }

                if (IsAvailable == 0)
                {
                string qrey = "select max(Insp_ID) as Insp_ID  from CMS_GodownSurveyor_Inspection where  LEN(Insp_ID)<15 ";
                da = new SqlDataAdapter(qrey, con);

                ds = new DataSet();
                da.Fill(ds);

                DataRow dr = ds.Tables[0].Rows[0];

                gatepass = ds.Tables[0].Rows[0]["Insp_ID"].ToString();

                if (gatepass == "")
                {
                    gatepass = "18" + "01";
                }
                else
                {
                    getnum = Convert.ToInt32(gatepass);

                    getnum = getnum + 1;
                    gatepass = getnum.ToString();
                }

                if (hdfCommoditiesCSMS.Value == "64")
                {
                    InspectionID = "64" + gatepass;
                }
                else if (hdfCommoditiesCSMS.Value == "63")
                {
                    InspectionID = "63" + gatepass;
                }
                else if (hdfCommoditiesCSMS.Value == "33")
                {
                    InspectionID = "33" + gatepass;
                }

                string strselect = "";
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                ConvertServerDate ServerDate = new ConvertServerDate();
                string ConvertDateofdispatch = ServerDate.getDate_MDY(txtdate.Text);
                string ConvertDateofinsp = ServerDate.getDate_MDY(txtDateOfInsp.Text);
                string BookNumber = "R" + InspectionID + ddlTruckChallan.SelectedValue.ToString();

                //massor red lentil
                if (hdfCommoditiesCSMS.Value == "64")
                {
                    strselect = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                       "update IssueToSangrahanaKendra_CSM2018 set GS_Status='R', G_Surveyor='" + hdfSurveyorID.Value + "' where TruckChalanNo='" + ddlTruckChallan.SelectedValue.ToString() + "' and GodownNumber='" + ddlGodown.SelectedValue.ToString() + "'";


                    strselect += "insert into CMS_GodownSurveyor_Inspection([GodownSurveyorInsp_ID], [Commodity] ,[TruckChallan], [SocietyDist] , [Society], [CropYear], [DateofDispatch], [TransporterID]      ,[TruckNumber]      ,[Quantity]      ,[Bags]      ,[BagsType]      ,[Status]      ,[BookNumber]      ,[AcceptanceNumber]      ,[RejectionNumber]      ,[ReceivingGodown]      ,[ReceivingBranch]      ,[Sarson]      ,[Sarson_ImpForeignmatt_IncTaramira]      ,[Sarson_Admix_WOT_IncToria]      ,[Sarson_Unripe_ShirvellImmature]      ,[Sarson_DamagedAndWeevilled]      ,[Sarson_SmallAtrophiedSeeds]      ,[Sarson_MoistureContent]      ,[Massor]      ,[Massur_ForeignMatter]      ,[Massur_Admixture]      ,[Massur_DamagedPulses]      ,[Massur_SligDamagedPulses]      ,[Massur_ImmaShrivellPulses]      ,[Massur_weevilledPulses]      ,[Massur_MoistureContent]      ,[Channa]      ,[Gram_ForeignMatter]      ,[Gram_OtherFoodGrains]      ,[Gram_DamagedGrains]      ,[Gram_SligDamagedTouchGrains]       ,[Gram_ImmaShrivAndBroGrains] ,[Gram_AdmixOfOtherVarieties] ,[Gram_WeevilleGrains] ,[Gram_MositureContent]  ,[CreatedDate] ,[IP], Insp_ID, [SurveyorID] ,[SurveyorName], [SurveyorMob], DateOfInsp) values ('" + InspectionID + "','" + hdfCommoditiesCSMS.Value + "','" + ddlTruckChallan.SelectedValue.ToString() + "','" + hdfsocietyDist.Value + "','" + hdfsociety.Value + "','" + txtCropYear.Text + "','" + ConvertDateofdispatch + "','" + hdftransporterid.Value + "','" + txttrucknumber.Text + "','" + txtqty.Text + "','" + txtbags.Text + "','" + txtbagstype.Text + "','Reject','" + BookNumber + "','0','" + BookNumber + "', '" + ddlGodown.SelectedValue.ToString() + "','" + hdfbranch.Value + "','0','0','0','0','0','0','0','Y','" + txtmasur_Foreignmatter.Text + "','" + txtmasur_admixture.Text + "','" + txtmasur_DamagedPulses.Text + "','" + txtmasur_sligDamagPulses.Text + "','" + txtmasur_ImmaShvldPulses.Text + "','" + txtmasur_WeevldPulses.Text + "','" + txtMasur_MoistureContent.Text + "','0','0','0','0','0','0','0','0','0', GetDate(), '" + ip + "','" + gatepass + "', '" + hdfSurveyorID.Value + "','" + HdfSurveyorName.Value + "','" + txtMobNum.Text + "' ,'" + ConvertDateofinsp + "')";
                    strselect += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";

                }
                //Channa gram
                else if (hdfCommoditiesCSMS.Value == "63")
                {
                    strselect = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                      "update IssueToSangrahanaKendra_CSM2018 set GS_Status='R', G_Surveyor='" + hdfSurveyorID.Value + "' where TruckChalanNo='" + ddlTruckChallan.SelectedValue.ToString() + "' and GodownNumber='" + ddlGodown.SelectedValue.ToString() + "'";


                    strselect += "insert into CMS_GodownSurveyor_Inspection([GodownSurveyorInsp_ID], [Commodity] ,[TruckChallan], [SocietyDist] , [Society], [CropYear], [DateofDispatch], [TransporterID]      ,[TruckNumber]      ,[Quantity]      ,[Bags]      ,[BagsType]      ,[Status]      ,[BookNumber]      ,[AcceptanceNumber]      ,[RejectionNumber]      ,[ReceivingGodown]      ,[ReceivingBranch]      ,[Sarson]      ,[Sarson_ImpForeignmatt_IncTaramira]      ,[Sarson_Admix_WOT_IncToria]      ,[Sarson_Unripe_ShirvellImmature]      ,[Sarson_DamagedAndWeevilled]      ,[Sarson_SmallAtrophiedSeeds]      ,[Sarson_MoistureContent]      ,[Massor]      ,[Massur_ForeignMatter]      ,[Massur_Admixture]      ,[Massur_DamagedPulses]      ,[Massur_SligDamagedPulses]      ,[Massur_ImmaShrivellPulses]      ,[Massur_weevilledPulses]      ,[Massur_MoistureContent]      ,[Channa]      ,[Gram_ForeignMatter]      ,[Gram_OtherFoodGrains]      ,[Gram_DamagedGrains]      ,[Gram_SligDamagedTouchGrains]       ,[Gram_ImmaShrivAndBroGrains] ,[Gram_AdmixOfOtherVarieties] ,[Gram_WeevilleGrains] ,[Gram_MositureContent]  ,[CreatedDate] ,[IP], Insp_ID, [SurveyorID] ,[SurveyorName], [SurveyorMob], DateOfInsp) values ('" + InspectionID + "','" + hdfCommoditiesCSMS.Value + "','" + ddlTruckChallan.SelectedValue.ToString() + "','" + hdfsocietyDist.Value + "','" + hdfsociety.Value + "','" + txtCropYear.Text + "','" + ConvertDateofdispatch + "','" + hdftransporterid.Value + "','" + txttrucknumber.Text + "','" + txtqty.Text + "','" + txtbags.Text + "','" + txtbagstype.Text + "','Reject','" + BookNumber + "','0','" + BookNumber + "', '" + ddlGodown.SelectedValue.ToString() + "','" + hdfbranch.Value + "','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','Y','" + txtGramForeign_Matter.Text + "','" + txtGramFoodGrains.Text + "','" + txtGram_DamagFoodGrains.Text + "','" + txtGram_SligDamagTochedGrains.Text + "','" + txtGram_ImmaShrivBroGrains.Text + "','" + txtGram_AdmixOtherVarie.Text + "','" + txtGram_WeevldGrains.Text + "','" + txtGram_MoistureContent.Text + "', GetDate(), '" + ip + "','" + gatepass + "', '" + hdfSurveyorID.Value + "','" + HdfSurveyorName.Value + "','" + txtMobNum.Text + "' ,'" + ConvertDateofinsp + "')";
                    strselect += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";

                }
                //sarson mustard
                else if (hdfCommoditiesCSMS.Value == "33")
                {
                    strselect = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                      "update IssueToSangrahanaKendra_CSM2018 set GS_Status='R', G_Surveyor='" + hdfSurveyorID.Value + "' where TruckChalanNo='" + ddlTruckChallan.SelectedValue.ToString() + "' and GodownNumber='" + ddlGodown.SelectedValue.ToString() + "'";

                    strselect += "insert into CMS_GodownSurveyor_Inspection([GodownSurveyorInsp_ID], [Commodity] ,[TruckChallan], [SocietyDist] , [Society], [CropYear], [DateofDispatch], [TransporterID]      ,[TruckNumber]      ,[Quantity]      ,[Bags]      ,[BagsType]      ,[Status]      ,[BookNumber]      ,[AcceptanceNumber]      ,[RejectionNumber]      ,[ReceivingGodown]      ,[ReceivingBranch]      ,[Sarson]      ,[Sarson_ImpForeignmatt_IncTaramira]      ,[Sarson_Admix_WOT_IncToria]      ,[Sarson_Unripe_ShirvellImmature]      ,[Sarson_DamagedAndWeevilled]      ,[Sarson_SmallAtrophiedSeeds]      ,[Sarson_MoistureContent]      ,[Massor]      ,[Massur_ForeignMatter]      ,[Massur_Admixture]      ,[Massur_DamagedPulses]      ,[Massur_SligDamagedPulses]      ,[Massur_ImmaShrivellPulses]      ,[Massur_weevilledPulses]      ,[Massur_MoistureContent]      ,[Channa]      ,[Gram_ForeignMatter]      ,[Gram_OtherFoodGrains]      ,[Gram_DamagedGrains]      ,[Gram_SligDamagedTouchGrains]       ,[Gram_ImmaShrivAndBroGrains] ,[Gram_AdmixOfOtherVarieties] ,[Gram_WeevilleGrains] ,[Gram_MositureContent]  ,[CreatedDate] ,[IP], Insp_ID, [SurveyorID] ,[SurveyorName], [SurveyorMob], DateOfInsp) values ('" + InspectionID + "','" + hdfCommoditiesCSMS.Value + "','" + ddlTruckChallan.SelectedValue.ToString() + "','" + hdfsocietyDist.Value + "','" + hdfsociety.Value + "','" + txtCropYear.Text + "','" + ConvertDateofdispatch + "','" + hdftransporterid.Value + "','" + txttrucknumber.Text + "','" + txtqty.Text + "','" + txtbags.Text + "','" + txtbagstype.Text + "','Reject','" + BookNumber + "','0','" + BookNumber + "', '" + ddlGodown.SelectedValue.ToString() + "','" + hdfbranch.Value + "','Y','" + txtIFM_IncTaramira.Text + "','" + txtAM_OT_Toria.Text + "','" + txtUR_Shvld_Imm.Text + "','" + txtDamWeevd.Text + "','" + txtSmallAtroSeeds.Text + "','" + txtMoisCont.Text + "','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0', GetDate(), '" + ip + "','" + gatepass + "', '" + hdfSurveyorID.Value + "','" + HdfSurveyorName.Value + "','" + txtMobNum.Text + "' ,'" + ConvertDateofinsp + "')";
                    strselect += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";

                }
                cmd = new SqlCommand(strselect, con);
                string check = (string)cmd.ExecuteScalar();

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");
                btnReject.Enabled = false;
                trNumber.Visible = true;
                Label2.Visible = true;
                Label2.Text = "Your Rejection Number is :- " + BookNumber;
                }
                else
                {
                    //Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपने इस ट्रक चालान का Quality Inspection कर चुकें हैं इसलिए आप इसको दुबारा नहीं कर सकते|'); </script> ");
                    //btnAccept.Enabled = btnReject.Enabled = false;
                    //return;
                }

            }
            catch
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please check and Enter valid data...'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }

    }





    protected void btnROTP_Click(object sender, EventArgs e)
    {
        if (txtMobNum.Text != "")
        {
            // Call Jquery Function TimerFunc()
            txtCheckOTP.Text = "";


            GenerateUniqueOTP();

            btnsendOTP.Enabled = false;
            btnsendOTP.Visible = false;
            btnROTP.Enabled = false;
            btnROTP.Visible = true;

            txtCheckOTP.Enabled = true;
            btncheckOTP.Enabled = true;
            txtCheckOTP.Focus();

            using (con = new SqlConnection(strcon))
                try
                {
                    con.Open();
                    string strselect = "";
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    strselect = "Update CMS_GodownSurveyor_InspectionOTP set ResendOTP='"+GenerateOTP+"', Resend_OTPTime=GetDate(), Created_Date=GetDate(), CountOTP='2' where TruckChallan='"+ddlTruckChallan.SelectedValue.ToString()+"'";
                    cmd = new SqlCommand(strselect, con);
                    string check = (string)cmd.ExecuteScalar();
                }
                catch
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please check and Enter valid data...'); </script> ");
                }

                finally
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                    }
                }
            ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:TimerFuncResend(); ", true);
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Mobile Number Not Available'); </script> ");
            return;
        }
    }
}