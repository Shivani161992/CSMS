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


public partial class SurveyorLogin_Uparjan_SurveyorLogin_QInspection : System.Web.UI.Page
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
        if (Session["user"] != null)
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
            Response.Redirect("~/SurveyorLogin_Uparjan/Surveyor_Login.aspx");
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
        ddlTruckChallan.Items.Clear();
        if (ddlcommodities.SelectedIndex > 0)
        {

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
    protected void ddlTruckChallan_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    //Sarson//
    protected void btnsarsonQualityinspection_Click(object sender, EventArgs e)
    {
        if (float.Parse(lblIFM_IncTaramira.Text) >= float.Parse(txtIFM_IncTaramira.Text) && float.Parse(lblAM_OT_Toria.Text) >= float.Parse(txtAM_OT_Toria.Text) && float.Parse(lblUR_Shvld_Imm.Text) >= float.Parse(txtUR_Shvld_Imm.Text) && float.Parse(lblDamWeevd.Text) >= float.Parse(txtDamWeevd.Text) && float.Parse(lblSmallAtroSeeds.Text) >= float.Parse(txtSmallAtroSeeds.Text) && float.Parse(lblMoisCont.Text) >= float.Parse(txtMoisCont.Text))
        {
            // btnAccept.Enabled = true;
            //  btnReject.Enabled = true;
            btnsarsonaccept.Enabled = true;
            btnsarsonreject.Enabled = false;
            btnsarsonQualityinspection.Enabled = false;
            btnsarsonQualityinspection.Text = "Submitted";






        }
        else
        {
            btnsarsonaccept.Enabled = false;
            btnsarsonreject.Enabled = true;
            //btnReject.Enabled = true;
            // btnAccept.Enabled = false;
            btnsarsonQualityinspection.Enabled = false;
            btnsarsonQualityinspection.Text = "Submitted";



        }
    }
    protected void btnsarsonaccept_Click(object sender, EventArgs e)
    {

    }
    protected void btnsarsonreject_Click(object sender, EventArgs e)
    {

    }

    //Massur (Red Lentils)//
    protected void btnmassurQualityInspection_Click(object sender, EventArgs e)
    {
        if (float.Parse(lblmasur_Foreignmatter.Text) >= float.Parse(txtmasur_Foreignmatter.Text) && float.Parse(lblmasur_admixture.Text) >= float.Parse(txtmasur_admixture.Text) && float.Parse(lblmasur_DamagedPulses.Text) >= float.Parse(txtmasur_DamagedPulses.Text) && float.Parse(lblmasur_sligDamagPulses.Text) >= float.Parse(txtmasur_sligDamagPulses.Text) && float.Parse(lblmasur_ImmaShvldPulses.Text) >= float.Parse(txtmasur_ImmaShvldPulses.Text) && float.Parse(lblmasur_WeevldPulses.Text) >= float.Parse(txtmasur_WeevldPulses.Text) && float.Parse(lblMasur_MoistureContent.Text) >= float.Parse(txtMasur_MoistureContent.Text))
        {

            btnmassurAccpet.Enabled = true;
            btnmassurReject.Enabled = false;
            btnmassurQualityInspection.Enabled = false;
            btnmassurQualityInspection.Text = "Submitted";

            ddlcommodities.Enabled = false;
            ddlTruckChallan.Enabled = false;



        }
        else
        {
            btnmassurAccpet.Enabled = false;
            btnmassurReject.Enabled = true;

            btnmassurQualityInspection.Enabled = false;
            btnmassurQualityInspection.Text = "Submitted";
            ddlcommodities.Enabled = false;
            ddlTruckChallan.Enabled = false;



        }


    }
    protected void btnmassurAccpet_Click(object sender, EventArgs e)
    {

    }
    protected void btnmassurReject_Click(object sender, EventArgs e)
    {

    }
    //Gram(Channa)//
    protected void btnGramQualityInspection_Click(object sender, EventArgs e)
    {
        if (float.Parse(lblGramForeign_Matter.Text) >= float.Parse(txtGramForeign_Matter.Text) && float.Parse(lblGramFoodGrains.Text) >= float.Parse(txtGramFoodGrains.Text) && float.Parse(lblGram_DamagFoodGrains.Text) >= float.Parse(txtGram_DamagFoodGrains.Text) && float.Parse(lblGram_SligDamagTochedGrains.Text) >= float.Parse(txtGram_SligDamagTochedGrains.Text) && float.Parse(lblGram_ImmaShrivBroGrains.Text) >= float.Parse(txtGram_ImmaShrivBroGrains.Text) && float.Parse(lblGram_AdmixOtherVarie.Text) >= float.Parse(txtGram_AdmixOtherVarie.Text) && float.Parse(lblGram_WeevldGrains.Text) >= float.Parse(txtGram_WeevldGrains.Text) && float.Parse(lblGram_MoistureContent.Text) >= float.Parse(txtGram_MoistureContent.Text))
        {
            // btnAccept.Enabled = true;
            //  btnReject.Enabled = true;
            btngramAccept.Enabled = true;
            btnGramReject.Enabled = false;
            btnGramQualityInspection.Enabled = false;
            btnGramQualityInspection.Text = "Submitted";






        }
        else
        {
            btngramAccept.Enabled = false;
            btnGramReject.Enabled = true;
            //btnReject.Enabled = true;
            // btnAccept.Enabled = false;
            btnGramQualityInspection.Enabled = false;
            btnGramQualityInspection.Text = "Submitted";



        }

    }
    protected void btngramAccept_Click(object sender, EventArgs e)
    {

    }
    protected void btnGramReject_Click(object sender, EventArgs e)
    {

    }
}