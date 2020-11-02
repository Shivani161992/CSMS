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


public partial class State_SMSCommodities_QIParametersMaster : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    public string gatepass = "";
    public int getnum;
    SqlDataReader dr;
    string IC_Id = "", Dist_Id = "";

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {
            if (!IsPostBack)
            {
                Session["ICGBQ"] = null;
                GetCommodities();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
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

                select = "Select Commodity_Id,Commodity_Name From tbl_MetaData_STORAGE_COMMODITY Where Commodity_Id IN('33','64', '63') order by Commodity_Name";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlcommodities.DataSource = ds.Tables[0];
                    ddlcommodities.DataTextField = "Commodity_Name";
                    ddlcommodities.DataValueField = "Commodity_Id";
                    ddlcommodities.DataBind();
                    ddlcommodities.Items.Insert(0, "--Select--");
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

    protected void ddlcommodities_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCropYear.Items.Clear();
        if (ddlcommodities.SelectedIndex > 0)
        {
            GetCropYear();
            if (ddlcommodities.SelectedValue.ToString() == "33")
            {
                trmasur.Visible = false;
                trgram.Visible = false;
                trsarson.Visible = true;
            }
            else if (ddlcommodities.SelectedValue.ToString() == "63")
            {
                trgram.Visible = true;
                trmasur.Visible = false;
                trsarson.Visible = false;

            }
            else if (ddlcommodities.SelectedValue.ToString() == "64")
            {
                trsarson.Visible = false;
                trgram.Visible = false;
                trmasur.Visible = true;
            }


        }
    }
    public void GetCropYear()
    {
        ddlCropYear.Items.Insert(0, "--Select--");
        ddlCropYear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1));
        ddlCropYear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year);
       
        ddlCropYear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1));
        
    }
    protected void ddlCropYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCropYear.SelectedIndex > 0)
        {
            GetDetails();

        }
    }
    public void GetDetails()
    { 
        if(ddlcommodities.SelectedValue.ToString()=="33")
        {
            GetSarsonData();
        }
        else if (ddlcommodities.SelectedValue.ToString()=="63")
        {
            GetGramData();
        }
        else if (ddlcommodities.SelectedValue.ToString() == "64")
        {
            GetMasurData();
        }
    
    }
    public void GetSarsonData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "select Sarson_ImpForeignmatt_IncTaramira, Admix_WOT_IncToria, Unripe_ShirvellImmature, DamagedAndWeevilled, SmallAtrophiedSeeds, MoistureContent  from  CMSCommo_SarsonQI_Parameters where Commodity='"+ddlcommodities.SelectedValue.ToString()+"' and CropYear='"+ddlCropYear.SelectedValue.ToString()+"'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtIFM_IncTaramira.Text = ds.Tables[0].Rows[0]["Sarson_ImpForeignmatt_IncTaramira"].ToString();
                    txtAM_OT_Toria.Text = ds.Tables[0].Rows[0]["Admix_WOT_IncToria"].ToString();

                    txtUR_Shvld_Imm.Text = ds.Tables[0].Rows[0]["Unripe_ShirvellImmature"].ToString();
                    txtDamWeevd.Text = ds.Tables[0].Rows[0]["DamagedAndWeevilled"].ToString();

                    txtSmallAtroSeeds.Text = ds.Tables[0].Rows[0]["SmallAtrophiedSeeds"].ToString();
                    txtMoisCont.Text = ds.Tables[0].Rows[0]["MoistureContent"].ToString();
                    btAddMustardSeeds.Visible = false;
                    btAddMustardSeeds.Enabled = false;
                    btnUpdateMustardSeeds.Visible = true;
                    btnUpdateMustardSeeds.Enabled = true;
                 

                }

                else
                {
                    txtIFM_IncTaramira.Text ="";
                    txtIFM_IncTaramira.Focus();
                    txtAM_OT_Toria.Text ="";

                    txtUR_Shvld_Imm.Text = "";
                    txtDamWeevd.Text = "";

                    txtSmallAtroSeeds.Text = "";
                    txtMoisCont.Text = "";
                    btAddMustardSeeds.Visible = true;
                    btAddMustardSeeds.Enabled = true;
                    btnUpdateMustardSeeds.Visible = false;
                    btnUpdateMustardSeeds.Enabled = false;
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
    public void GetGramData()
    {

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "select Gram_ForeignMatter, Gram_OtherFoodGrains, Gram_DamagedGrains, Gram_SligDamagedTouchGrains, Gram_ImmaShrivAndBroGrains, Gram_AdmixOfOtherVarieties, Gram_WeevilleGrains,  Gram_MositureContent from CMSCommo_GramChannaQI_Parameters where Commodity='"+ddlcommodities.SelectedValue.ToString()+"' and CropYear='"+ddlCropYear.SelectedValue.ToString()+"'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtGramForeign_Matter.Text = ds.Tables[0].Rows[0]["Gram_ForeignMatter"].ToString();
                    txtGramFoodGrains.Text = ds.Tables[0].Rows[0]["Gram_OtherFoodGrains"].ToString();

                    txtGram_DamagFoodGrains.Text = ds.Tables[0].Rows[0]["Gram_DamagedGrains"].ToString();
                    txtGram_SligDamagTochedGrains.Text = ds.Tables[0].Rows[0]["Gram_SligDamagedTouchGrains"].ToString();

                    txtGram_ImmaShrivBroGrains.Text = ds.Tables[0].Rows[0]["Gram_ImmaShrivAndBroGrains"].ToString();
                    txtGram_AdmixOtherVarie.Text = ds.Tables[0].Rows[0]["Gram_AdmixOfOtherVarieties"].ToString();

                    txtGram_WeevldGrains.Text = ds.Tables[0].Rows[0]["Gram_WeevilleGrains"].ToString();
                    txtGram_MoistureContent.Text = ds.Tables[0].Rows[0]["Gram_MositureContent"].ToString();

                    bttnAddGram.Visible = false;
                    bttnAddGram.Enabled = false;
                    bttnUpdateGram.Enabled = true;
                    bttnUpdateGram.Visible = true;
                }

                else
                {
                    txtGramForeign_Matter.Text = "";
                    txtGramForeign_Matter.Focus();
                    txtGramFoodGrains.Text = "";

                    txtGram_DamagFoodGrains.Text = "";
                    txtGram_SligDamagTochedGrains.Text = "";

                    txtGram_ImmaShrivBroGrains.Text = "";
                    txtGram_AdmixOtherVarie.Text = "";

                    txtGram_WeevldGrains.Text = "";
                    txtGram_MoistureContent.Text = "";

                    bttnAddGram.Visible = true;
                    bttnAddGram.Enabled = true;
                    bttnUpdateGram.Enabled = false;
                    bttnUpdateGram.Visible = false;
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
    public void GetMasurData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "select Massur_ForeignMatter, Massur_Admixture, Massur_DamagedPulses, Massur_SligDamagedPulses, Massur_ImmaShrivellPulses, Massur_weevilledPulses, Massur_MoistureContent from CMSCommo_MassurLentilsQI_Parameters where Commodity='"+ddlcommodities.SelectedValue.ToString()+"' and CropYear='"+ddlCropYear.SelectedValue.ToString()+"'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtmasur_Foreignmatter.Text = ds.Tables[0].Rows[0]["Massur_ForeignMatter"].ToString();
                    txtmasur_admixture.Text = ds.Tables[0].Rows[0]["Massur_Admixture"].ToString();

                    txtmasur_DamagedPulses.Text = ds.Tables[0].Rows[0]["Massur_DamagedPulses"].ToString();
                    txtmasur_sligDamagPulses.Text = ds.Tables[0].Rows[0]["Massur_SligDamagedPulses"].ToString();

                    txtmasur_ImmaShvldPulses.Text = ds.Tables[0].Rows[0]["Massur_ImmaShrivellPulses"].ToString();
                    txtmasur_WeevldPulses.Text = ds.Tables[0].Rows[0]["Massur_weevilledPulses"].ToString();

                    txtMasur_MoistureContent.Text = ds.Tables[0].Rows[0]["Massur_MoistureContent"].ToString();

                    bttnmasursubmit.Visible = false;
                    bttnmasursubmit.Enabled = false;
                    bttnmasurupdate.Enabled = true;
                    bttnmasurupdate.Visible = true;
                }

                else
                {
                    txtmasur_Foreignmatter.Text = "";
                    txtmasur_admixture.Text = "";

                    txtmasur_DamagedPulses.Text = "";
                    txtmasur_sligDamagPulses.Text = "";

                    txtmasur_ImmaShvldPulses.Text = "";
                    txtmasur_WeevldPulses.Text = "";

                    txtMasur_MoistureContent.Text = "";


                    bttnmasursubmit.Visible = true;
                    bttnmasursubmit.Enabled = true;
                    bttnmasurupdate.Enabled = false ;
                    bttnmasurupdate.Visible = false ;
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


    protected void btAddMustardSeeds_Click(object sender, EventArgs e)
    {
        using (con = new SqlConnection(strcon))
            try
            {
                con.Open();
                string qrey = "select max(SarsonID) as SarsonID  from CMSCommo_SarsonQI_Parameters where  LEN(SarsonID)<15 ";
                da = new SqlDataAdapter(qrey, con);

                ds = new DataSet();
                da.Fill(ds);

                DataRow dr = ds.Tables[0].Rows[0];

                gatepass = ds.Tables[0].Rows[0]["SarsonID"].ToString();

                if (gatepass == "")
                {
                    gatepass = "33" + "01";
                }
                else
                {
                    getnum = Convert.ToInt32(gatepass);

                    getnum = getnum + 1;
                    gatepass = getnum.ToString();
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

        using (con = new SqlConnection(strcon))
            try
            {
               
                con.Open();
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                string strselect = "insert into CMSCommo_SarsonQI_Parameters (SarsonID, Commodity, CropYear,  Sarson_ImpForeignmatt_IncTaramira, Admix_WOT_IncToria, Unripe_ShirvellImmature, DamagedAndWeevilled, SmallAtrophiedSeeds, MoistureContent, CreatedDate, IP  ) values ('" + gatepass + "','" + ddlcommodities.SelectedValue.ToString() + "','" + ddlCropYear.SelectedValue.ToString() + "','" + txtIFM_IncTaramira.Text + "','" + txtAM_OT_Toria.Text + "','" + txtUR_Shvld_Imm.Text + "','" + txtDamWeevd.Text + "','" + txtSmallAtroSeeds.Text + "','" + txtMoisCont .Text+ "', Getdate(), '" + ip + "')";
                cmd = new SqlCommand(strselect, con);
                string check = (string)cmd.ExecuteScalar();

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");
               
                ddlCropYear.Enabled = false;
                ddlcommodities.Enabled = false;

                txtIFM_IncTaramira.Enabled = false;
                txtIFM_IncTaramira.Enabled = false;
                txtAM_OT_Toria.Enabled = false;

                txtUR_Shvld_Imm.Enabled = false;
                txtDamWeevd.Enabled = false;

                txtSmallAtroSeeds.Enabled = false;
                txtMoisCont.Enabled = false;
                btnUpdateMustardSeeds.Visible = false;
                btnUpdateMustardSeeds.Enabled = false;
                btAddMustardSeeds.Enabled = false;

               


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

    protected void btnUpdateMustardSeeds_Click(object sender, EventArgs e)
    {
        using (con = new SqlConnection(strcon))
            try
            {
               

                con.Open();

                string strselect = "Update CMSCommo_SarsonQI_Parameters set Sarson_ImpForeignmatt_IncTaramira='" + txtIFM_IncTaramira.Text + "', Admix_WOT_IncToria='" + txtAM_OT_Toria.Text + "', Unripe_ShirvellImmature='" + txtUR_Shvld_Imm.Text + "', DamagedAndWeevilled='" + txtDamWeevd.Text + "', SmallAtrophiedSeeds='" + txtSmallAtroSeeds.Text + "', MoistureContent='" + txtMoisCont.Text + "' where  Commodity='"+ddlcommodities.SelectedValue.ToString()+"'and CropYear='"+ddlCropYear.SelectedValue.ToString()+"'";
                cmd = new SqlCommand(strselect, con);
                string check = (string)cmd.ExecuteScalar();

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is updated successfully'); </script> ");
                ddlCropYear.Enabled = false;
                ddlcommodities.Enabled = false;

                txtIFM_IncTaramira.Enabled = false;
                txtIFM_IncTaramira.Enabled = false;
                txtAM_OT_Toria.Enabled = false;

                txtUR_Shvld_Imm.Enabled = false;
                txtDamWeevd.Enabled = false;

                txtSmallAtroSeeds.Enabled = false;
                txtMoisCont.Enabled = false;
                btAddMustardSeeds.Visible = false;
                btnUpdateMustardSeeds.Enabled = false;
                btAddMustardSeeds.Enabled = false;


                

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


    protected void bttnmasursubmit_Click(object sender, EventArgs e)
    {
        using (con = new SqlConnection(strcon))
            try
            {
                con.Open();
                string qrey = "select max(MassurID) as MassurID  from CMSCommo_MassurLentilsQI_Parameters where  LEN(MassurID)<15 ";
                da = new SqlDataAdapter(qrey, con);

                ds = new DataSet();
                da.Fill(ds);

                DataRow dr = ds.Tables[0].Rows[0];

                gatepass = ds.Tables[0].Rows[0]["MassurID"].ToString();

                if (gatepass == "")
                {
                    gatepass = "64" + "01";
                }
                else
                {
                    getnum = Convert.ToInt32(gatepass);

                    getnum = getnum + 1;
                    gatepass = getnum.ToString();
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

        using (con = new SqlConnection(strcon))
            try
            {

                con.Open();
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                string strselect = "insert into CMSCommo_MassurLentilsQI_Parameters (MassurID, Commodity, CropYear,  Massur_ForeignMatter, Massur_Admixture, Massur_DamagedPulses, Massur_SligDamagedPulses, Massur_ImmaShrivellPulses, Massur_weevilledPulses, Massur_MoistureContent, Createddate, IP ) values ('" + gatepass + "','" + ddlcommodities.SelectedValue.ToString() + "','" + ddlCropYear.SelectedValue.ToString() + "','" + txtmasur_Foreignmatter.Text + "','" + txtmasur_admixture.Text + "','" + txtmasur_DamagedPulses.Text + "','" + txtmasur_sligDamagPulses.Text + "','" + txtmasur_ImmaShvldPulses.Text + "','" + txtmasur_WeevldPulses.Text + "','" + txtMasur_MoistureContent.Text+ "', Getdate(), '" + ip + "')";
                cmd = new SqlCommand(strselect, con);
                string check = (string)cmd.ExecuteScalar();

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");

                bttnmasurupdate.Visible = false;
                bttnmasursubmit.Enabled = false;
                bttnmasurupdate.Enabled = false;
                ddlCropYear.Enabled = false;
                ddlcommodities.Enabled = false;

                txtmasur_Foreignmatter.Enabled = false;
                txtmasur_admixture.Enabled = false;

                txtmasur_DamagedPulses.Enabled = false;
                txtmasur_sligDamagPulses.Enabled = false;

                txtmasur_ImmaShvldPulses.Enabled = false;
                txtmasur_WeevldPulses.Enabled = false;

                txtMasur_MoistureContent.Enabled = false;






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
    protected void bttnmasurupdate_Click(object sender, EventArgs e)
    {
        using (con = new SqlConnection(strcon))
            try
            {


                con.Open();

                string strselect = "Update CMSCommo_MassurLentilsQI_Parameters set   Massur_ForeignMatter='" + txtmasur_Foreignmatter.Text + "', Massur_Admixture='" + txtmasur_admixture.Text + "', Massur_DamagedPulses='" + txtmasur_DamagedPulses.Text + "', Massur_SligDamagedPulses='" + txtmasur_sligDamagPulses.Text + "', Massur_ImmaShrivellPulses='" + txtmasur_ImmaShvldPulses.Text + "', Massur_weevilledPulses='" + txtmasur_WeevldPulses.Text + "', Massur_MoistureContent='" + txtMasur_MoistureContent .Text+ "' where  Commodity='" + ddlcommodities.SelectedValue.ToString() + "'and CropYear='" + ddlCropYear.SelectedValue.ToString() + "'";
                cmd = new SqlCommand(strselect, con);
                string check = (string)cmd.ExecuteScalar();

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is updated successfully'); </script> ");
              
                ddlCropYear.Enabled = false;
                ddlcommodities.Enabled = false;

                txtmasur_Foreignmatter.Enabled = false;
                txtmasur_admixture.Enabled = false;

                txtmasur_DamagedPulses.Enabled = false;
                txtmasur_sligDamagPulses.Enabled = false;

                txtmasur_ImmaShvldPulses.Enabled = false;
                txtmasur_WeevldPulses.Enabled = false;

                txtMasur_MoistureContent.Enabled = false;
                bttnmasurupdate.Enabled = false;
                bttnmasursubmit.Enabled = false;
                bttnmasursubmit.Visible = false;




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


    protected void bttnAddGram_Click(object sender, EventArgs e)
    {
        using (con = new SqlConnection(strcon))
            try
            {
                con.Open();
                string qrey = "select max(ChannaID) as ChannaID  from CMSCommo_GramChannaQI_Parameters where  LEN(ChannaID)<15 ";
                da = new SqlDataAdapter(qrey, con);

                ds = new DataSet();
                da.Fill(ds);

                DataRow dr = ds.Tables[0].Rows[0];

                gatepass = ds.Tables[0].Rows[0]["ChannaID"].ToString();

                if (gatepass == "")
                {
                    gatepass = "63" + "01";
                }
                else
                {
                    getnum = Convert.ToInt32(gatepass);

                    getnum = getnum + 1;
                    gatepass = getnum.ToString();
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

        using (con = new SqlConnection(strcon))
            try
            {

                con.Open();
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                string strselect = "insert into CMSCommo_GramChannaQI_Parameters (ChannaID, Commodity, CropYear, Gram_ForeignMatter, Gram_OtherFoodGrains, Gram_DamagedGrains, Gram_SligDamagedTouchGrains, Gram_ImmaShrivAndBroGrains, Gram_AdmixOfOtherVarieties, Gram_WeevilleGrains,  Gram_MositureContent, CreatedDate, IP) values ('" + gatepass + "','" + ddlcommodities.SelectedValue.ToString() + "','" + ddlCropYear.SelectedValue.ToString() + "','" + txtGramForeign_Matter.Text + "','" + txtGramFoodGrains.Text + "','" + txtGram_DamagFoodGrains.Text + "','" + txtGram_SligDamagTochedGrains.Text + "','" + txtGram_ImmaShrivBroGrains.Text + "','" + txtGram_AdmixOtherVarie.Text + "','" + txtGram_WeevldGrains.Text + "','" + txtGram_MoistureContent.Text + "', Getdate(), '" + ip + "')";
                cmd = new SqlCommand(strselect, con);
                string check = (string)cmd.ExecuteScalar();

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");
               
                ddlCropYear.Enabled = false;
                ddlcommodities.Enabled = false;

                txtGramForeign_Matter.Enabled = false;
                txtGramFoodGrains.Enabled = false;

                txtGram_DamagFoodGrains.Enabled = false;
                txtGram_SligDamagTochedGrains.Enabled = false;

                txtGram_ImmaShrivBroGrains.Enabled = false;
                txtGram_AdmixOtherVarie.Enabled = false;

                txtGram_WeevldGrains.Enabled = false;
                txtGram_MoistureContent.Enabled = false;

               
                bttnAddGram.Enabled = false;
                bttnUpdateGram.Enabled = false;
                bttnUpdateGram.Visible = false;



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
    protected void bttnUpdateGram_Click(object sender, EventArgs e)
    {
        using (con = new SqlConnection(strcon))
            try
            {


                con.Open();

                string strselect = "Update CMSCommo_GramChannaQI_Parameters set Gram_ForeignMatter='" + txtGramForeign_Matter.Text + "', Gram_OtherFoodGrains='" + txtGramFoodGrains.Text + "', Gram_DamagedGrains='" + txtGram_DamagFoodGrains.Text + "', Gram_SligDamagedTouchGrains='" + txtGram_SligDamagTochedGrains.Text + "', Gram_ImmaShrivAndBroGrains='" + txtGram_ImmaShrivBroGrains.Text + "', Gram_AdmixOfOtherVarieties='" + txtGram_AdmixOtherVarie.Text + "', Gram_WeevilleGrains='" + txtGram_WeevldGrains.Text + "',  Gram_MositureContent='" + txtGram_MoistureContent .Text+ "' where  Commodity='" + ddlcommodities.SelectedValue.ToString() + "'and CropYear='" + ddlCropYear.SelectedValue.ToString() + "'";
                cmd = new SqlCommand(strselect, con);
                string check = (string)cmd.ExecuteScalar();

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is updated successfully'); </script> ");
                ddlCropYear.Enabled = false;
                ddlcommodities.Enabled = false;

                txtGramForeign_Matter.Enabled = false;
                txtGramFoodGrains.Enabled = false;

                txtGram_DamagFoodGrains.Enabled = false;
                txtGram_SligDamagTochedGrains.Enabled = false;

                txtGram_ImmaShrivBroGrains.Enabled = false;
                txtGram_AdmixOtherVarie.Enabled = false;

                txtGram_WeevldGrains.Enabled = false;
                txtGram_MoistureContent.Enabled = false;

                bttnAddGram.Visible = false;
                bttnAddGram.Enabled = false;
                bttnUpdateGram.Enabled = false;
            



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
}