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

public partial class State_CMS_TruckChallan_Status : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    public string gatepass, InspectionID = "";
    public int getnum;
    SqlDataReader dr;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {
            if (!IsPostBack)
            {
                Session["ICGBQ"] = null;
                //GetTruckChallan();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

    }

    //public void GetTruckChallan()
    //{

    //    using (con = new SqlConnection(strcon))
    //    {
    //        try
    //        {
    //            con.Open();

    //            string select = "";

    //            select = "select TruckChalanNo from IssueToSangrahanaKendra_CSM2018 ";
    //            da = new SqlDataAdapter(select, con);

    //            ds = new DataSet();
    //            da.Fill(ds);
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                ddltruckChallan.DataSource = ds.Tables[0];
    //                ddltruckChallan.DataTextField = "TruckChalanNo";
    //                ddltruckChallan.DataValueField = "TruckChalanNo";
    //                ddltruckChallan.DataBind();
    //                ddltruckChallan.Items.Insert(0, "--Select--");
    //            }
    //            else
    //            {
    //                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Truck Number Not Available|'); </script> ");
    //                return;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
    //        }

    //        finally
    //        {
    //            if (con.State != ConnectionState.Closed)
    //            {
    //                con.Close();
    //            }
    //        }
    //    }
    //}
    //protected void ddltruckChallan_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddltruckChallan.SelectedIndex > 0)
    //    {
    //        GetChallanData();


    //    }


    //}

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txttruckChallan.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Truck challan Number not available|'); </script> ");
            return;
        }
        else if (txttruckChallan.Text != "")
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

                select = "select SK.CropYear, D.district_name as DistrictName, SK.DistrictId, SMSP.Society_Name as SocietyName, SocietyID, convert(varchar(10), DateOfIssue, 103) DateOfIssue , IssueID, CommodityId, case when CHK_FAQ='A' then 'Accepted' when CHK_FAQ='N' then 'Pending' when CHK_FAQ='R' then 'Rejected' end as UStatus,  QtyTransffer, Bags, TruckNo, TaulPtrakNo ,  BiltiNo, G1.Godown_Name as OldGodown, Srvyr_Nm,  convert(varchar(10), UpdateFAQ, 103) as UInspDate, TruckChalanNo,G.Godown_Name as NewGodown from IssueToSangrahanaKendra_CSM2018 as SK inner join pds.districtsmp as D on D.distcd4=SK.DistrictId left join Society_MSP as SMSP on SMSP.Society_Id=SK.SocietyID left join IntegratedMPStorage.dbo.tbl_MetaData_GODOWN as G on G.Godown_ID=SK.GodownNumber left join IntegratedMPStorage.dbo.tbl_MetaData_GODOWN as G1 on G1.Godown_ID=SK.OldGodown left join Srvyr_Reg_Initial as SR on SR.Srvyr_Id=SK.Srvyr_ID where TruckChalanNo='" + txttruckChallan.Text + "' ";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblTC.Text = ds.Tables[0].Rows[0]["TruckChalanNo"].ToString();
                    lblCropYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    lblDist.Text = ds.Tables[0].Rows[0]["DistrictName"].ToString();
                    lblPurchaseCenter.Text = ds.Tables[0].Rows[0]["SocietyName"].ToString();
                    //
                    lblMandi.Text = ds.Tables[0].Rows[0]["TruckChalanNo"].ToString();
                    lblDateOfIssue.Text = ds.Tables[0].Rows[0]["DateOfIssue"].ToString();
                    lblIssueID.Text = ds.Tables[0].Rows[0]["IssueID"].ToString();

                    //
                    lblcomm.Text = ds.Tables[0].Rows[0]["CommodityId"].ToString();
                    // //channa
                    if (lblcomm.Text == "6")
                    {
                        lblcomm.Text = "Channa (Gram)";
                            hdfCommoditiesCSMS.Value = "63";
                    }
                     //sarson
                    else if (lblcomm.Text == "12")
                    {
                        lblcomm.Text = "Sarson (Mustard)";
                            
                            hdfCommoditiesCSMS.Value = "33";
                    }
                     //massor
                    else if (lblcomm.Text == "63")
                    {
                        lblcomm.Text = "Massor (Red Lentils)";
                            
                            hdfCommoditiesCSMS.Value = "64";
                    }


                    lblUFNF.Text = ds.Tables[0].Rows[0]["UStatus"].ToString();
                    lblDQty.Text = ds.Tables[0].Rows[0]["QtyTransffer"].ToString();

                    //

                    lblDBags.Text = ds.Tables[0].Rows[0]["Bags"].ToString();
                    lblTN.Text = ds.Tables[0].Rows[0]["TruckNo"].ToString();
                    lblToulPat.Text = ds.Tables[0].Rows[0]["TaulPtrakNo"].ToString();

                    //

                    lblBilNum.Text = ds.Tables[0].Rows[0]["BiltiNo"].ToString();
                    lblGod.Text = ds.Tables[0].Rows[0]["OldGodown"].ToString();
                    if (lblGod.Text=="")
                    {
                        Label32.Text=lblGod.Text = ds.Tables[0].Rows[0]["NewGodown"].ToString();
                    }
                    else if (lblGod.Text != "")
                    {
                        Label32.Text = ds.Tables[0].Rows[0]["NewGodown"].ToString();
                    }

                    lblUsur.Text = ds.Tables[0].Rows[0]["Srvyr_Nm"].ToString();
                    //
                    lblDUS.Text = ds.Tables[0].Rows[0]["UInspDate"].ToString();
                    //sarson
                    if (hdfCommoditiesCSMS.Value == "33")
                    {
                        trchana.Visible = false;
                        trmassor.Visible = false;
                        trsarson.Visible = true;

                        trGChanna.Visible = false;
                        trGmassor.Visible = false;
                        trGSarson.Visible = true;
                        GetUFAQNFAQSarson();
                    }
                        //channa
                    else if (hdfCommoditiesCSMS.Value == "63")
                    {
                        trchana.Visible = true;
                        trmassor.Visible = false;
                        trsarson.Visible = false;

                        trGChanna.Visible = true;
                        trGmassor.Visible = false;
                        trGSarson.Visible = false;
                        GetUFAQNFAQGram();
                    }
                        //massor
                    else if (hdfCommoditiesCSMS.Value == "64")
                    {
                        trchana.Visible = false;
                        trmassor.Visible = true;
                        trsarson.Visible = false;

                        trGChanna.Visible = false;
                        trGmassor.Visible = true;
                        trGSarson.Visible = false;
                        GetUFAQNFAQMassor();
                    }






                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Challan Data Not Available|'); </script> ");
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

    public void GetUFAQNFAQSarson()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "select ImpuriForemttrincldTarmir, AdmxtrothrtypincldTor, UnripShrvlldimmatre, DamgdWvilled, Smllatrophidsid, MositureContent from IssueFAQ_Final_Sarson where TeruckChallan='" + txttruckChallan.Text + "'";
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

    public void GetUFAQNFAQGram()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "select ForeignMatter, OtherFoodGrains, DamagedGrains, SligDamagedTouchGrains, ImmaShrivAndBroGrains, AdmixOfOtherVarieties, WeevilleGrains, MositureContent  from IssueFAQ_Final_Chana where TeruckChallan='" + txttruckChallan.Text + "'";
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

    public void GetUFAQNFAQMassor()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "select Foreignmatter, Admixture, Damagedpuls, Slightlydamaged, ImmatureShrivelled, Weevilled, MoistureContent from IssueFAQ_Final_Masoor where TeruckChallan='" + txttruckChallan.Text + "'";
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

    public void GetGSurveyorData()
    {
         //sarson
        if (hdfCommoditiesCSMS.Value == "33")
        {
            using (con = new SqlConnection(strcon))
            {
                try
                {
                    con.Open();

                    string select = "";

                    select = "select Foreignmatter, Admixture, Damagedpuls, Slightlydamaged, ImmatureShrivelled, Weevilled, MoistureContent from IssueFAQ_Final_Masoor where TeruckChallan='" + txttruckChallan.Text + "'";
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
            //channa
        else if (hdfCommoditiesCSMS.Value == "63")
        {
            using (con = new SqlConnection(strcon))
            {
                try
                {
                    con.Open();

                    string select = "";

                    select = "select Foreignmatter, Admixture, Damagedpuls, Slightlydamaged, ImmatureShrivelled, Weevilled, MoistureContent from IssueFAQ_Final_Masoor where TeruckChallan='" + txttruckChallan.Text + "'";
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
            //massor
        else if (hdfCommoditiesCSMS.Value == "64")
        {
            using (con = new SqlConnection(strcon))
            {
                try
                {
                    con.Open();

                    string select = "";

                    select = "select Foreignmatter, Admixture, Damagedpuls, Slightlydamaged, ImmatureShrivelled, Weevilled, MoistureContent from IssueFAQ_Final_Masoor where TeruckChallan='" + txttruckChallan.Text + "'";
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
    
    }


   
}