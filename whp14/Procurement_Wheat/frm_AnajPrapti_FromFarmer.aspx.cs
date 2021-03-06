using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Globalization;
using System.Drawing;
using whtmpData;
using System.Security.Cryptography;
using DataAccess;
using System.Web.Security;
using System.Text.RegularExpressions;
public partial class WHP14_Procurement_Wheat_frm_AnajPrapti_FromFarmer : System.Web.UI.Page
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_WPMS2014_Test"].ToString());
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_WPMS2014"].ToString());
   
    clsConnection_WPMS2014 clsobj = new clsConnection_WPMS2014();
    SqlCommand cmd = null;
    DataSet ds = null;
    DataTable dtGrid = new DataTable();
    DataTable dt = new DataTable();
    private Common ComObj = null;

    private loginWhtMP lobj = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["App"].ToString() != null && Session["App"].ToString() == "WPMS2014")
            {
                if (Session["UserName"] != null && Session["Society_Name"] != null)
                {
                    if (Session["UserName"].ToString() == "PurchaseCenter")
                    {
                        if (Session["District_Code"] != null && Session["Society_Id"] != null)
                        {
                            string script_btn_FRList = "$(document).ready(function () { $('[id*=btnSearchFarmer]').click(); });";
                            ClientScript.RegisterStartupScript(this.GetType(), "load", script_btn_FRList, true);
                            string script_btnSave = "$(document).ready(function () { $('[id*=btnSave]').click(); });";
                            ClientScript.RegisterStartupScript(this.GetType(), "load", script_btnSave, true);

                            txtpwd.Focus();
                            lbl_ERMSG.Text = "";
                            //ComObj = new Common(ConfigurationManager.AppSettings["Appconstr_WPMS2014_Test"].ToString());
                            ComObj = new Common(ConfigurationManager.AppSettings["Appconstr_WPMS2014"].ToString());
                            lblDistName.Text = Session["DistrictName"].ToString();
                            //txtpwd.Attributes.Add("onkeypress", "return checksqlkey_psw(event,this)");
                            //txtpwd.Attributes.Add("onKeyUp", "return taCount(this,'myCounter')");
                            //txtpwd.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                            //txtpwd.Attributes.Add("onchange", "return chksqltxt_psw(this),MD5(this);");
                            //txtpwd.Attributes.Add("onKeyUp", "return Do_login();");
                            //txtSubmit.Attributes.Add("onkeypress", "return LoginOnEnter(this);");
                            GetTDate();
                            if (!Page.IsPostBack)
                            {
                                txtSubmit.Focus();
                                Panel_Prapti_afterpass.Visible = false;
                                pnl_CheckPass_Kharidi.Visible = true;
                                pnlConfirm_save.Visible = false;
                                chb_Confirm.Visible = false;

                                FillBlankDll();
                                FillTehsil(ddl_Tehsil);
                                FillCrop();
                                Session["dt"] = null;
                                CreateColoumnIn_dt();
                                pnlConfirm_save.Enabled = false;
                                Panel_Prapti_afterpass.Enabled = false;

                                chb_Confirm.Checked = false;

                                //try
                                //{
                                //    txtPraptiDate.Text = Convert.ToString(DateTime.Now.ToString("dd/MM/yyyy"));
                                //}
                                //catch (Exception)
                                //{ }
                                GetDateFuction();
                                Panel_Prapti_afterpass.Visible = true;
                                pnlConfirm_save.Visible = true;
                                pnl_CheckPass_Kharidi.Visible = false;
                                Panel_Prapti_afterpass.Enabled = true;
                                pnlConfirm_save.Enabled = false;
                                chb_Confirm.Checked = false;
                                chb_Confirm.Visible = true;
                                chb_Confirm.Enabled = true;

                            }
                        }

                        else
                        {
                            Response.Redirect("../Login1.aspx");
                        }
                    }
                    else
                    {
                        Response.Redirect("../Login1.aspx");
                    }

                }
                else
                {
                    Response.Redirect("../Login1.aspx");
                }
            }
            else
            {
                Response.Redirect("../Login1.aspx");
            }
        }

        catch (Exception ex)
        {
            Response.Redirect("../Login1.aspx");
        }

    }
    protected void GetTDate()
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();

            }
            //txtPraptiDate.Text = "";
            DataSet ds_tdate = new DataSet();
            SqlDataAdapter da_tdate = new SqlDataAdapter("Select Convert(varchar(50),Getdate(),103) as Tdate", con);
            da_tdate.Fill(ds_tdate);
            if (ds_tdate.Tables[0].Rows.Count > 0)
            {
                if (txtPraptiDate.Text.Trim() == String.Empty)
                {
                    txtPraptiDate.Text = Convert.ToString(ds_tdate.Tables[0].Rows[0]["Tdate"]);
                }
            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

        }
        catch (Exception ex)
        { }
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    public void FillCrop()
    {

        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            DataSet dt_soc = new DataSet();
            //SqlDataAdapter da_GetFarmer = new SqlDataAdapter("SELECT [crpcode] ,[crop] FROM [dbo].[Crop_Master] Where crpcode in ('4','5','6','7','9')", con);
            SqlDataAdapter da_GetFarmer = new SqlDataAdapter("SELECT [CommodityId] ,[CommodityName] FROM [dbo].[CommodityRate] Where CommodityId in ('1')", con);
            da_GetFarmer.Fill(dt_soc);
            if (dt_soc.Tables[0].Rows.Count > 0)
            {
                ddl_Comodity.DataSource = dt_soc;
                ddl_Comodity.DataTextField = "CommodityName";
                ddl_Comodity.DataValueField = "CommodityId";
                ddl_Comodity.DataBind();
                ddl_Comodity.Items.Insert(0, "--चयन करें--");
                ddl_Comodity.SelectedIndex = 1;


            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void FillBlankDll()
    {
        ddl_Farmer.Items.Clear();
        ddl_Farmer.Items.Insert(0, "--चयन करें--");
        ddl_Village.Items.Clear();
        ddl_Village.Items.Insert(0, "--चयन करें--");
        ddl_Tehsil.Items.Clear();
        ddl_Tehsil.Items.Insert(0, "--चयन करें--");
    }
    public void FillTehsil(DropDownList ddlname)
    {

        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            DataSet dt_soc = new DataSet();
            SqlDataAdapter da_GetFarmer = new SqlDataAdapter("select TehsilCode,Tehsil_Name from Tehsils where  District_Code='" + Session["District_Code"].ToString() + "'  order by TehsilCode asc", con);
            da_GetFarmer.Fill(dt_soc);
            if (dt_soc.Tables[0].Rows.Count > 0)
            {
                ddl_Tehsil.DataSource = dt_soc;
                ddl_Tehsil.DataTextField = "Tehsil_Name";
                ddl_Tehsil.DataValueField = "TehsilCode";
                ddl_Tehsil.DataBind();
                ddl_Tehsil.Items.Insert(0, "--चयन करें--");


            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        catch (Exception ex)
        {

        }
    }

    public void Fill_ddl_Village(string TehsilId)
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            DataSet dt_soc = new DataSet();
            SqlDataAdapter da_GetFarmer = new SqlDataAdapter("select Distinct Village_Id,VillageName  from FarmerRegistration where District_Id='" + Session["District_Code"].ToString() + "' and Tehsil_Id='" + TehsilId.Trim() + "' and Procured_SocietyID='" + Session["Society_Id"].ToString() + "' order by VillageName asc", con);
            da_GetFarmer.Fill(dt_soc);
            if (dt_soc.Tables[0].Rows.Count > 0)
            {
                ddl_Village.DataSource = dt_soc;
                ddl_Village.DataTextField = "VillageName";
                ddl_Village.DataValueField = "Village_Id";
                ddl_Village.DataBind();
                ddl_Village.Items.Insert(0, "--चुने--");


            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    public void FIll_ddl_Farmer(string vcode, string tcode)
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            DataSet dt_Farmer = new DataSet();
            //SqlDataAdapter da_GetFarmer = new SqlDataAdapter("SELECT  Farmer_Id, (Farmer_Id +'   (  '+ FarmerName +' / '+ FatherHusName +' )')  as FarmerInfo from  FarmerRegistration WHERE District_Id=RTRIM(LTRIM('" + Session["District_Code"].ToString() + "'))	and   Tehsil_Id=RTRIM(LTRIM('" + tcode.Trim() + "'))		and 	  Village_Id=RTRIM(LTRIM('" + vcode.Trim() + "'))   and  MarketingSeasonId=RTRIM(LTRIM('R')) and Procured_SocietyID='" + Session["Society_Id"].ToString().Trim() + "'  order by Farmer_Id asc  ", con);
            SqlDataAdapter da_GetFarmer = new SqlDataAdapter("SELECT  Farmer_Id, FarmerName ,FatherHusName from  FarmerRegistration WHERE District_Id=RTRIM(LTRIM('" + Session["District_Code"].ToString() + "'))	and   Tehsil_Id=RTRIM(LTRIM('" + tcode.Trim() + "'))		and 	  VillageName=RTRIM(LTRIM(N'" + vcode.Trim() + "'))   and  MarketingSeasonId=RTRIM(LTRIM('R')) and Procured_SocietyID='" + Session["Society_Id"].ToString().Trim() + "'  order by Farmer_Id asc  ", con);
            da_GetFarmer.Fill(dt_Farmer);
            dt_Farmer.Tables[0].Columns.Add("DisplayName", typeof(string), "Farmer_Id + '  (  ' +FarmerName +'  /  '+ FatherHusName + '  )'");
            dt_Farmer.AcceptChanges();
            if (dt_Farmer.Tables[0].Rows.Count > 0)
            {
                ddl_Farmer.DataSource = dt_Farmer;
                ddl_Farmer.DataTextField = "DisplayName";
                ddl_Farmer.DataValueField = "Farmer_Id";
                ddl_Farmer.DataBind();
                ddl_Farmer.Items.Insert(0, "--चुने--");


            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void ddl_Tehsil_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            btnPawati.Visible = false;
            GridView2.DataSource = null;
            GridView2.DataBind();

            GridView_CropSinchit_Asinchit.DataSource = null;
            GridView_CropSinchit_Asinchit.DataBind();
            GridView_AbhiTak_Becha.DataSource = null;
            GridView_AbhiTak_Becha.DataBind();
            txtKisaanKulRakba.Text = "";
            txtSikamiRakba.Text = "";
            txtOwnRakba.Text = "";

            //ddl_Comodity.SelectedIndex = 0;
            txtBoriSankhya.Text = "";
            txtMatra.Text = "";
            //txtSamarthan_muly.Text = "";
            //txt_Rajya_Bonus.Text = "";

            txtTolPatrak.Text = "";
            txtAnajKulMatra.Text = "";
            txtShudhBhugtan_YogyaRashi.Text = "";

            txtSocLoan.Text = "";
            txtDCCBLoan.Text = "";
            txtIrrigationLoan.Text = "";
            txt_Amount_Against_SocLoan.Text = "";
            txt_Amt_AgainstDCCBLoan.Text = "";
            txt_Amount_AgainstIrrigationLoan.Text = "";
            txtShudhBhugtan.Text = "";
            Session["dt"] = null;
            CreateColoumnIn_dt();
            ddl_Farmer.Items.Clear();
            ddl_Farmer.Items.Insert(0, "--चयन करें--");
            ddl_Village.Items.Clear();
            ddl_Village.Items.Insert(0, "--चयन करें--");
            if (ddl_Tehsil.SelectedIndex != 0)
            {

                Fill_ddl_Village(ddl_Tehsil.SelectedValue.ToString());
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void ddl_Village_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            btnPawati.Visible = false;
            GridView2.DataSource = null;
            GridView2.DataBind();

            GridView_CropSinchit_Asinchit.DataSource = null;
            GridView_CropSinchit_Asinchit.DataBind();
            GridView_AbhiTak_Becha.DataSource = null;
            GridView_AbhiTak_Becha.DataBind();
            txtKisaanKulRakba.Text = "";
            txtSikamiRakba.Text = "";
            txtOwnRakba.Text = "";

            //ddl_Comodity.SelectedIndex = 0;
            txtBoriSankhya.Text = "";
            txtMatra.Text = "";
            //txtSamarthan_muly.Text = "";
            //txt_Rajya_Bonus.Text = "";

            txtTolPatrak.Text = "";
            txtAnajKulMatra.Text = "";
            txtShudhBhugtan_YogyaRashi.Text = "";

            txtSocLoan.Text = "";
            txtDCCBLoan.Text = "";
            txtIrrigationLoan.Text = "";
            txt_Amount_Against_SocLoan.Text = "";
            txt_Amt_AgainstDCCBLoan.Text = "";
            txt_Amount_AgainstIrrigationLoan.Text = "";
            txtShudhBhugtan.Text = "";
            Session["dt"] = null;
            CreateColoumnIn_dt();
            ddl_Farmer.Items.Clear();
            if (ddl_Tehsil.SelectedIndex != 0 || ddl_Village.SelectedIndex != 0)
            {

                FIll_ddl_Farmer(ddl_Village.SelectedItem.Text.Trim(), ddl_Tehsil.SelectedItem.Value.Trim());
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('तहसील एवं ग्राम का चयन करें'); </script> ");

            }
        }
        catch (Exception)
        { }
    }
    protected void GetSikamiRakba(string frid, string ltype, string call)
    {
        try
        {
            if (call.Trim() == "Button")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                DataSet dsr = new DataSet();
                SqlDataAdapter daR = new SqlDataAdapter("Select (isnull(SUM(Rakba_crop_asinchit),0)+ isnull(SUM(Rakba_crop_sinchit),0)) as SikamiRakba From Farmer_LandRecordDescription  Where Farmer_Id='" + frid.Trim() + "' and LandType='" + ltype.Trim() + "'", con);
                daR.Fill(dsr);
                if (dsr.Tables[0].Rows.Count > 0)
                {
                    txtSikamiRakba.Text = Convert.ToString(dsr.Tables[0].Rows[0]["SikamiRakba"]);

                }
                else
                {
                    txtSikamiRakba.Text = "0";
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            if (call.Trim() == "Drop")
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                DataSet dsr = new DataSet();
                SqlDataAdapter daR = new SqlDataAdapter("Select (isnull(SUM(Rakba_crop_asinchit),0)+ isnull(SUM(Rakba_crop_sinchit),0)) as SikamiRakba From Farmer_LandRecordDescription  Where Farmer_Id='" + frid.Trim() + "' and LandType='" + ltype.Trim() + "'", con);
                daR.Fill(dsr);
                if (dsr.Tables[0].Rows.Count > 0)
                {
                    txtSikamiRakba.Text = Convert.ToString(dsr.Tables[0].Rows[0]["SikamiRakba"]);

                }
                else
                {
                    txtSikamiRakba.Text = "0";
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        catch (Exception)
        {
        }
    }
    protected void GetKulRakba(string frid, string call)
    {
        try
        {

            if (call.Trim() == "Button")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                DataSet dsr = new DataSet();
                SqlDataAdapter daR = new SqlDataAdapter("Select isnull(SUM(Rakba),0) as Rakba From Farmer_LandRecordDescription  Where Farmer_Id='" + frid.Trim() + "'", con);
                daR.Fill(dsr);
                if (dsr.Tables[0].Rows.Count > 0)
                {
                    txtKisaanKulRakba.Text = Convert.ToString(dsr.Tables[0].Rows[0]["Rakba"]);

                }
                else
                {
                    txtKisaanKulRakba.Text = "0";
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            if (call.Trim() == "Drop")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                DataSet dsr = new DataSet();
                SqlDataAdapter daR = new SqlDataAdapter("Select isnull(SUM(Rakba),0) as Rakba From Farmer_LandRecordDescription  Where Farmer_Id='" + frid.Trim() + "'", con);
                daR.Fill(dsr);
                if (dsr.Tables[0].Rows.Count > 0)
                {
                    txtKisaanKulRakba.Text = Convert.ToString(dsr.Tables[0].Rows[0]["Rakba"]);

                }
                else
                {
                    txtKisaanKulRakba.Text = "0";
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        catch (Exception)
        {
        }
    }
    protected void GetOwnRakba(string frid, string ltype, string call)
    {
        try
        {
            if (call.Trim() == "Button")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                DataSet dsr = new DataSet();
                SqlDataAdapter daR = new SqlDataAdapter("Select (isnull(SUM(Rakba_crop_asinchit),0)+ isnull(SUM(Rakba_crop_sinchit),0)) as OwnRakba From Farmer_LandRecordDescription  Where Farmer_Id='" + frid.Trim() + "' and LandType='" + ltype.Trim() + "'", con);
                daR.Fill(dsr);
                if (dsr.Tables[0].Rows.Count > 0)
                {
                    txtOwnRakba.Text = Convert.ToString(dsr.Tables[0].Rows[0]["OwnRakba"]);

                }
                else
                {
                    txtOwnRakba.Text = "0";
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            if (call.Trim() == "Drop")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                DataSet dsr = new DataSet();
                SqlDataAdapter daR = new SqlDataAdapter("Select (isnull(SUM(Rakba_crop_asinchit),0)+ isnull(SUM(Rakba_crop_sinchit),0)) as OwnRakba From Farmer_LandRecordDescription  Where Farmer_Id='" + frid.Trim() + "' and LandType='" + ltype.Trim() + "'", con);
                daR.Fill(dsr);
                if (dsr.Tables[0].Rows.Count > 0)
                {
                    txtOwnRakba.Text = Convert.ToString(dsr.Tables[0].Rows[0]["OwnRakba"]);

                }
                else
                {
                    txtOwnRakba.Text = "0";
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }
        catch (Exception)
        {
        }
    }
    protected void GetCropSinchit_Asinchit(string frid, string call)
    {
        try
        {
            if (call.Trim() == "Drop")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                DataSet dsg = new DataSet();
                SqlDataAdapter da_grid = new SqlDataAdapter("Select lr.crpcode,cm.CommodityName,isnull(SUM(lr.Rakba_crop_sinchit),0) as Rakba_crop_sinchit,isnull(SUM(lr.Rakba_crop_asinchit),0) as Rakba_crop_asinchit from Farmer_LandRecordDescription as lr left join CommodityRate as cm on lr.crpcode=cm.CommodityId  Where lr.Farmer_Id='" + frid.Trim() + "' and lr.crpcode  in ('1') Group By lr.crpcode,cm.CommodityName", con);
                //Select lr.crpcode,cm.crop,isnull(SUM(lr.Rakba_crop_sinchit),0) as Rakba_crop_sinchit,isnull(SUM(lr.Rakba_crop_asinchit),0) as Rakba_crop_asinchit from Farmer_LandRecordDescription as lr left join Crop_Master as cm on lr.crpcode=cm.crpcode  Where lr.Farmer_Id='" + frid.Trim() + "' and lr.crpcode  in ('2','3') Group By lr.crpcode,cm.crop
                //SqlDataAdapter da_grid = new SqlDataAdapter("Select crpcode,crop , (Select isnull(SUM(Rakba_crop_sinchit),0) as Rakba_crop_sinchit From Farmer_LandRecordDescription Where crpcode=Crop_Master.crpcode and Farmer_Id='" + frid.Trim() + "') as Rakba_crop_sinchit, (Select isnull(SUM(Rakba_crop_asinchit),0) as Rakba_crop_asinchit From Farmer_LandRecordDescription Where crpcode=Crop_Master.crpcode and Farmer_Id='" + frid.Trim() + "') as Rakba_crop_asinchit from Crop_Master Where crpcode in ('2','3')", con);
                da_grid.Fill(dsg);
                if (dsg.Tables[0].Rows.Count > 0)
                {
                    GridView_CropSinchit_Asinchit.DataSource = dsg;
                    GridView_CropSinchit_Asinchit.DataBind();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            if (call.Trim() == "Button")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                DataSet dsg = new DataSet();
                SqlDataAdapter da_grid = new SqlDataAdapter("Select lr.crpcode,cm.CommodityName,isnull(SUM(lr.Rakba_crop_sinchit),0) as Rakba_crop_sinchit,isnull(SUM(lr.Rakba_crop_asinchit),0) as Rakba_crop_asinchit from Farmer_LandRecordDescription as lr left join CommodityRate as cm on lr.crpcode=cm.CommodityId  Where lr.Farmer_Id='" + frid.Trim() + "' and lr.crpcode  in ('1') Group By lr.crpcode,cm.CommodityName", con);
                //Select lr.crpcode,cm.crop,isnull(SUM(lr.Rakba_crop_sinchit),0) as Rakba_crop_sinchit,isnull(SUM(lr.Rakba_crop_asinchit),0) as Rakba_crop_asinchit from Farmer_LandRecordDescription as lr left join Crop_Master as cm on lr.crpcode=cm.crpcode  Where lr.Farmer_Id='" + frid.Trim() + "' and lr.crpcode  in ('2','3') Group By lr.crpcode,cm.crop
                //SqlDataAdapter da_grid = new SqlDataAdapter("Select crpcode,crop , (Select isnull(SUM(Rakba_crop_sinchit),0) as Rakba_crop_sinchit From Farmer_LandRecordDescription Where crpcode=Crop_Master.crpcode and Farmer_Id='" + frid.Trim() + "') as Rakba_crop_sinchit, (Select isnull(SUM(Rakba_crop_asinchit),0) as Rakba_crop_asinchit From Farmer_LandRecordDescription Where crpcode=Crop_Master.crpcode and Farmer_Id='" + frid.Trim() + "') as Rakba_crop_asinchit from Crop_Master Where crpcode in ('2','3')", con);
                da_grid.Fill(dsg);
                if (dsg.Tables[0].Rows.Count > 0)
                {
                    GridView_CropSinchit_Asinchit.DataSource = dsg;
                    GridView_CropSinchit_Asinchit.DataBind();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }
        catch (Exception)
        { }
    }

    protected void GetBechaAnaj_AajTak(string fid, string did, string call)
    {
        try
        {
            if (call.Trim() == "Drop")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                DataSet dsk = new DataSet();
                //SqlDataAdapter da = new SqlDataAdapter("Select CT.CommodityId,CM.crop,isnull(Sum(CT.QtyReceived),0) as Matra From CommodityReceived_Transaction as CT left join Crop_Master as CM on CT.CommodityId=CM.crpcode Where Farmer_Id='" + fid.Trim() + "' and District_Id='" + did.Trim() + "' group by ct.CommodityId,cm.crop", con);
                SqlDataAdapter da = new SqlDataAdapter("Select CT.CommodityId,CM.CommodityName,isnull(Sum(CT.QtyReceived),0) as Matra From CommodityReceivedFromFarmer as CT left join CommodityRate as CM on CT.CommodityId=CM.CommodityId Where Farmer_Id='" + fid.Trim() + "' and District_Id='" + did.Trim() + "' group by ct.CommodityId,cm.CommodityName", con);
                da.Fill(dsk);
                if (dsk.Tables[0].Rows.Count > 0)
                {
                    GridView_AbhiTak_Becha.DataSource = dsk;
                    GridView_AbhiTak_Becha.DataBind();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            if (call.Trim() == "Button")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                DataSet dsk = new DataSet();
                //SqlDataAdapter da = new SqlDataAdapter("Select CT.CommodityId,CM.crop,isnull(Sum(CT.QtyReceived),0) as Matra From CommodityReceived_Transaction as CT left join Crop_Master as CM on CT.CommodityId=CM.crpcode Where Farmer_Id='" + fid.Trim() + "' and District_Id='" + did.Trim() + "' group by ct.CommodityId,cm.crop", con);
                SqlDataAdapter da = new SqlDataAdapter("Select CT.CommodityId,CM.CommodityName,isnull(Sum(CT.QtyReceived),0) as Matra From CommodityReceivedFromFarmer as CT left join CommodityRate as CM on CT.CommodityId=CM.CommodityId Where Farmer_Id='" + fid.Trim() + "' and District_Id='" + did.Trim() + "' group by ct.CommodityId,cm.CommodityName", con);
                da.Fill(dsk);
                if (dsk.Tables[0].Rows.Count > 0)
                {
                    GridView_AbhiTak_Becha.DataSource = dsk;
                    GridView_AbhiTak_Becha.DataBind();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }


        }
        catch (Exception)
        { }
    }
    protected void ddl_Farmer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            btnPawati.Visible = false;
            GridView2.DataSource = null;
            GridView2.DataBind();

            GridView_CropSinchit_Asinchit.DataSource = null;
            GridView_CropSinchit_Asinchit.DataBind();
            GridView_AbhiTak_Becha.DataSource = null;
            GridView_AbhiTak_Becha.DataBind();
            txtKisaanKulRakba.Text = "";
            txtSikamiRakba.Text = "";
            txtOwnRakba.Text = "";

            //ddl_Comodity.SelectedIndex = 0;
            txtBoriSankhya.Text = "";
            txtMatra.Text = "";
            //txtSamarthan_muly.Text = "";
            //txt_Rajya_Bonus.Text = "";

            txtTolPatrak.Text = "";
            txtAnajKulMatra.Text = "";
            txtShudhBhugtan_YogyaRashi.Text = "";

            txtSocLoan.Text = "";
            txtDCCBLoan.Text = "";
            txtIrrigationLoan.Text = "";
            txt_Amount_Against_SocLoan.Text = "";
            txt_Amt_AgainstDCCBLoan.Text = "";
            txt_Amount_AgainstIrrigationLoan.Text = "";
            txtShudhBhugtan.Text = "";
            Session["dt"] = null;
            CreateColoumnIn_dt();

            if (ddl_Farmer.SelectedIndex == 0)
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('किसान का चयन करे'); </script> ");
                return;
            }
            if (ddl_Tehsil.SelectedIndex == 0)
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('तहसील का चयन करे'); </script> ");
                return;
            }
            if (ddl_Village.SelectedIndex == 0)
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('गाँव का चयन करे'); </script> ");
                return;
            }
            CommIndex();
            GetFarmer_Land_Record(lblfarmerid.Text, "Sikami", "Own", Session["District_Code"].ToString(), Session["Society_Id"].ToString());
            //GetSikamiRakba(ddl_Farmer.SelectedItem.Value, "Sikami","Drop");
            //GetKulRakba(ddl_Farmer.SelectedItem.Value,"Drop");
            //GetOwnRakba(ddl_Farmer.SelectedItem.Value, "Own", "Drop");
            GetCropSinchit_Asinchit(lblfarmerid.Text, "Drop");
            GetBechaAnaj_AajTak(lblfarmerid.Text, Session["District_Code"].ToString(), "Drop");
            ////GetMax_TehsilYeild(ddl_Farmer.SelectedItem.Value);
            ////GetBankLoan_LoanAmt_Info(ddl_Farmer.SelectedItem.Value, Session["District_Code"].ToString());
            //GetSocLoan(ddl_Farmer.SelectedItem.Value, Session["District_Code"].ToString(), Session["Society_Id"].ToString(),"Drop");
            //GetDCCBLoan(ddl_Farmer.SelectedItem.Value, Session["District_Code"].ToString(), Session["Society_Id"].ToString(),"Drop");
            //GetIRRLoan(ddl_Farmer.SelectedItem.Value, Session["District_Code"].ToString(), Session["Society_Id"].ToString(),"Drop");
        }
        catch (Exception)
        {

        }
    }
    public void GetFarmer_Land_Record(object Farmer_Id, object SLandType, object OLandType, object District_Id, object Society_Id)
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("dbo.GetFarmer_LandRecordinfo_for_anaj_Parapti", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Farmer_Id", SqlDbType.VarChar).Value = Farmer_Id;
            cmd.Parameters.Add("@SLandType", SqlDbType.VarChar).Value = SLandType;
            cmd.Parameters.Add("@OLandType", SqlDbType.VarChar).Value = OLandType;
            cmd.Parameters.Add("@District_Id", SqlDbType.VarChar).Value = District_Id;
            cmd.Parameters.Add("@Society_Id", SqlDbType.VarChar).Value = Society_Id;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SikamiRakba"].ToString() != "")
                {
                    txtSikamiRakba.Text = Convert.ToString(ds.Tables[0].Rows[0]["SikamiRakba"]);
                }
                else
                {
                    txtSikamiRakba.Text = "0";
                }
                if (ds.Tables[0].Rows[0]["OwnRakba"].ToString() != "")
                {
                    txtKisaanKulRakba.Text = Convert.ToString(ds.Tables[0].Rows[0]["OwnRakba"]);
                }
                else
                {
                    txtKisaanKulRakba.Text = "0";
                }
                if (ds.Tables[0].Rows[0]["OwnRakba"].ToString() != "")
                {
                    txtOwnRakba.Text = Convert.ToString(ds.Tables[0].Rows[0]["OwnRakba"]);
                }
                else
                {
                    txtOwnRakba.Text = "0";
                }
                //Society Loan
                string socloan_fr = "";
                string socloan_CommodityReceivedFromFarmer = "";
                if (ds.Tables[0].Rows[0]["SocietyLoanAmount"].ToString() != "")
                {
                    socloan_fr = Convert.ToString(ds.Tables[0].Rows[0]["SocietyLoanAmount"]);
                }
                else
                {
                    socloan_fr = "0";
                }
                if (ds.Tables[0].Rows[0]["FarmerLoanFromSc"].ToString() != "")
                {
                    socloan_CommodityReceivedFromFarmer = Convert.ToString(ds.Tables[0].Rows[0]["FarmerLoanFromSc"]);
                }
                else
                {
                    socloan_CommodityReceivedFromFarmer = "0";
                }
                decimal Farmerlnamt = 0;
                Farmerlnamt = Convert.ToDecimal(socloan_fr) - Convert.ToDecimal(socloan_CommodityReceivedFromFarmer);
                if (Farmerlnamt <= 0)
                {
                    txtSocLoan.Text = "0";
                }
                else
                {
                    txtSocLoan.Text = Convert.ToString(Farmerlnamt);
                }
                //DCCB Loan
                decimal lnamt = 0;
                lnamt = Convert.ToDecimal(ds.Tables[0].Rows[0]["DCCLoanAmount"].ToString()) - Convert.ToDecimal(ds.Tables[0].Rows[0]["BankLoan"].ToString());
                if (lnamt <= 0)
                {
                    txtDCCBLoan.Text = "0";
                }
                else
                {
                    txtDCCBLoan.Text = lnamt.ToString();
                }
                //irrigation loan
                decimal IRRlnamt = 0;
                IRRlnamt = Convert.ToDecimal(ds.Tables[0].Rows[0]["IrrLoanAmount"].ToString()) - Convert.ToDecimal(ds.Tables[0].Rows[0]["IrrigationLoan"].ToString());
                if (IRRlnamt <= 0)
                {
                    txtIrrigationLoan.Text = "0";
                }
                else
                {
                    txtIrrigationLoan.Text = Convert.ToString(IRRlnamt);
                }
            }
            else
            {
                txtSikamiRakba.Text = "0";
                txtKisaanKulRakba.Text = "0";
                txtOwnRakba.Text = "0";
                txtSocLoan.Text = "0";
                txtDCCBLoan.Text = "0";
                txtIrrigationLoan.Text = "0";
            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        catch
        {

        }
    }
    protected void GetDCCBLoan(string fid, string did, string sid, string call)
    {
        try
        {
            if (call.Trim() == "Button")
            {
                string DCCBloan_fr = "";
                string DCCBloan_CommodityReceivedFromFarmer = "";
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                DataSet ds_sloan = new DataSet();
                SqlDataAdapter da_socloan = new SqlDataAdapter("Select ISNULL(LoanAmount,0) as LoanAmount From DCCBLoanOfFarmer Where Farmer_Id='" + fid.Trim() + "' And District_Id='" + did.Trim() + "'", con);
                da_socloan.Fill(ds_sloan);
                if (ds_sloan.Tables[0].Rows.Count > 0)
                {
                    DCCBloan_fr = Convert.ToString(ds_sloan.Tables[0].Rows[0]["LoanAmount"]);
                }
                else
                {
                    DCCBloan_fr = "0";
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                DataSet ds_soc1 = new DataSet();
                SqlDataAdapter da_soc1 = new SqlDataAdapter("Select ISNULL(SUM(AmtAgainstBankCredit),0) as BankLoan From CommodityReceivedFromFarmer Where Farmer_Id='" + fid.Trim() + "' and District_Id='" + did.Trim() + "' and Society_Id='" + sid.Trim() + "' ", con);
                da_soc1.Fill(ds_soc1);
                if (ds_soc1.Tables[0].Rows.Count > 0)
                {
                    DCCBloan_CommodityReceivedFromFarmer = Convert.ToString(ds_soc1.Tables[0].Rows[0]["BankLoan"]);
                }
                else
                {
                    DCCBloan_CommodityReceivedFromFarmer = "0";
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                decimal lnamt = 0;
                lnamt = Convert.ToDecimal(DCCBloan_fr) - Convert.ToDecimal(DCCBloan_CommodityReceivedFromFarmer);
                if (lnamt <= 0)
                {
                    txtDCCBLoan.Text = "0";

                }
                else
                {

                    txtDCCBLoan.Text = Convert.ToString(lnamt);
                }
            }

            if (call.Trim() == "Drop")
            {
                string DCCBloan_fr = "";
                string DCCBloan_CommodityReceivedFromFarmer = "";
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                DataSet ds_sloan = new DataSet();
                SqlDataAdapter da_socloan = new SqlDataAdapter("Select ISNULL(LoanAmount,0) as LoanAmount From DCCBLoanOfFarmer Where Farmer_Id='" + fid.Trim() + "' And District_Id='" + did.Trim() + "'", con);
                da_socloan.Fill(ds_sloan);
                if (ds_sloan.Tables[0].Rows.Count > 0)
                {
                    DCCBloan_fr = Convert.ToString(ds_sloan.Tables[0].Rows[0]["LoanAmount"]);
                }
                else
                {
                    DCCBloan_fr = "0";
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                DataSet ds_soc1 = new DataSet();
                SqlDataAdapter da_soc1 = new SqlDataAdapter("Select ISNULL(SUM(AmtAgainstBankCredit),0) as BankLoan From CommodityReceivedFromFarmer Where Farmer_Id='" + fid.Trim() + "' and District_Id='" + did.Trim() + "' and Society_Id='" + sid.Trim() + "' ", con);
                da_soc1.Fill(ds_soc1);
                if (ds_soc1.Tables[0].Rows.Count > 0)
                {
                    DCCBloan_CommodityReceivedFromFarmer = Convert.ToString(ds_soc1.Tables[0].Rows[0]["BankLoan"]);
                }
                else
                {
                    DCCBloan_CommodityReceivedFromFarmer = "0";
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                decimal lnamt = 0;
                lnamt = Convert.ToDecimal(DCCBloan_fr) - Convert.ToDecimal(DCCBloan_CommodityReceivedFromFarmer);
                if (lnamt <= 0)
                {
                    txtDCCBLoan.Text = "0";

                }
                else
                {

                    txtDCCBLoan.Text = Convert.ToString(lnamt);
                }
            }

        }
        catch (Exception)
        { }
    }
    protected void GetSocLoan(string fid, string did, string sid, string call)
    {
        try
        {
            if (call.Trim() == "Button")
            {
                string socloan_fr = "";
                string socloan_CommodityReceivedFromFarmer = "";
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                DataSet ds_sloan = new DataSet();
                SqlDataAdapter da_socloan = new SqlDataAdapter("Select ISNULL(LoanAmount,0) as LoanAmount From SocietyLoanOfFarmer Where Farmer_Id='" + fid.Trim() + "' And District_Id='" + did.Trim() + "' And PC_ID='" + sid.Trim() + "'", con);
                da_socloan.Fill(ds_sloan);
                if (ds_sloan.Tables[0].Rows.Count > 0)
                {
                    socloan_fr = Convert.ToString(ds_sloan.Tables[0].Rows[0]["LoanAmount"]);
                }
                else
                {
                    socloan_fr = "0";
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                DataSet ds_soc1 = new DataSet();
                SqlDataAdapter da_soc1 = new SqlDataAdapter("Select ISNULL(SUM(AmtAgainstSCCredit),0) as FarmerLoanFromSc From CommodityReceivedFromFarmer Where Farmer_Id='" + fid.Trim() + "' and District_Id='" + did.Trim() + "' and Society_Id='" + sid.Trim() + "' ", con);
                da_soc1.Fill(ds_soc1);
                if (ds_soc1.Tables[0].Rows.Count > 0)
                {
                    socloan_CommodityReceivedFromFarmer = Convert.ToString(ds_soc1.Tables[0].Rows[0]["FarmerLoanFromSc"]);
                }
                else
                {
                    socloan_CommodityReceivedFromFarmer = "0";
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                decimal lnamt = 0;
                lnamt = Convert.ToDecimal(socloan_fr) - Convert.ToDecimal(socloan_CommodityReceivedFromFarmer);
                if (lnamt <= 0)
                {
                    txtSocLoan.Text = "0";
                }
                else
                {
                    txtSocLoan.Text = Convert.ToString(lnamt);
                }
            }
            if (call.Trim() == "Drop")
            {
                string socloan_fr = "";
                string socloan_CommodityReceivedFromFarmer = "";
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                DataSet ds_sloan = new DataSet();
                SqlDataAdapter da_socloan = new SqlDataAdapter("Select ISNULL(LoanAmount,0) as LoanAmount From SocietyLoanOfFarmer Where Farmer_Id='" + fid.Trim() + "' And District_Id='" + did.Trim() + "' And PC_ID='" + sid.Trim() + "'", con);
                da_socloan.Fill(ds_sloan);
                if (ds_sloan.Tables[0].Rows.Count > 0)
                {
                    socloan_fr = Convert.ToString(ds_sloan.Tables[0].Rows[0]["LoanAmount"]);
                }
                else
                {
                    socloan_fr = "0";
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                DataSet ds_soc1 = new DataSet();
                SqlDataAdapter da_soc1 = new SqlDataAdapter("Select ISNULL(SUM(AmtAgainstSCCredit),0) as FarmerLoanFromSc From CommodityReceivedFromFarmer Where Farmer_Id='" + fid.Trim() + "' and District_Id='" + did.Trim() + "' and Society_Id='" + sid.Trim() + "' ", con);
                da_soc1.Fill(ds_soc1);
                if (ds_soc1.Tables[0].Rows.Count > 0)
                {
                    socloan_CommodityReceivedFromFarmer = Convert.ToString(ds_soc1.Tables[0].Rows[0]["FarmerLoanFromSc"]);
                }
                else
                {
                    socloan_CommodityReceivedFromFarmer = "0";
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                decimal lnamt = 0;
                lnamt = Convert.ToDecimal(socloan_fr) - Convert.ToDecimal(socloan_CommodityReceivedFromFarmer);
                if (lnamt <= 0)
                {
                    txtSocLoan.Text = "0";
                }
                else
                {
                    txtSocLoan.Text = Convert.ToString(lnamt);
                }
            }

        }
        catch (Exception)
        { }
    }
    protected void GetIRRLoan(string fid, string did, string sid, string call)
    {
        try
        {
            if (call.Trim() == "Button")
            {
                string IRRloan_fr = "";
                string IRRloan_CommodityReceivedFromFarmer = "";
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                DataSet ds_sloan = new DataSet();
                SqlDataAdapter da_socloan = new SqlDataAdapter("Select ISNULL(LoanAmount,0) as LoanAmount From IrrLoanOfFarmer Where Farmer_Id='" + fid.Trim() + "' And District_Id='" + did.Trim() + "'", con);
                da_socloan.Fill(ds_sloan);
                if (ds_sloan.Tables[0].Rows.Count > 0)
                {
                    IRRloan_fr = Convert.ToString(ds_sloan.Tables[0].Rows[0]["LoanAmount"]);
                }
                else
                {
                    IRRloan_fr = "0";
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                DataSet ds_soc1 = new DataSet();
                SqlDataAdapter da_soc1 = new SqlDataAdapter("Select ISNULL(SUM(AmtAgIrg_Loan),0) as IrrigationLoan From CommodityReceivedFromFarmer Where Farmer_Id='" + fid.Trim() + "' and District_Id='" + did.Trim() + "' and Society_Id='" + sid.Trim() + "' ", con);
                da_soc1.Fill(ds_soc1);
                if (ds_soc1.Tables[0].Rows.Count > 0)
                {
                    IRRloan_CommodityReceivedFromFarmer = Convert.ToString(ds_soc1.Tables[0].Rows[0]["IrrigationLoan"]);
                }
                else
                {
                    IRRloan_CommodityReceivedFromFarmer = "0";
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                decimal lnamt = 0;
                lnamt = Convert.ToDecimal(IRRloan_fr) - Convert.ToDecimal(IRRloan_CommodityReceivedFromFarmer);
                if (lnamt <= 0)
                {
                    txtIrrigationLoan.Text = "0";
                }
                else
                {
                    txtIrrigationLoan.Text = Convert.ToString(lnamt);
                }
            }
            if (call.Trim() == "Drop")
            {
                string IRRloan_fr = "";
                string IRRloan_CommodityReceivedFromFarmer = "";
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                DataSet ds_sloan = new DataSet();
                SqlDataAdapter da_socloan = new SqlDataAdapter("  Select ISNULL(LoanAmount,0) as LoanAmount From IrrLoanOfFarmer Where Farmer_Id='" + fid.Trim() + "' And District_Id='" + did.Trim() + "'", con);
                da_socloan.Fill(ds_sloan);
                if (ds_sloan.Tables[0].Rows.Count > 0)
                {
                    IRRloan_fr = Convert.ToString(ds_sloan.Tables[0].Rows[0]["LoanAmount"]);
                }
                else
                {
                    IRRloan_fr = "0";
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                DataSet ds_soc1 = new DataSet();
                SqlDataAdapter da_soc1 = new SqlDataAdapter("Select ISNULL(SUM(AmtAgIrg_Loan),0) as IrrigationLoan From CommodityReceivedFromFarmer Where Farmer_Id='" + fid.Trim() + "' and District_Id='" + did.Trim() + "' and Society_Id='" + sid.Trim() + "' ", con);
                da_soc1.Fill(ds_soc1);
                if (ds_soc1.Tables[0].Rows.Count > 0)
                {
                    IRRloan_CommodityReceivedFromFarmer = Convert.ToString(ds_soc1.Tables[0].Rows[0]["IrrigationLoan"]);
                }
                else
                {
                    IRRloan_CommodityReceivedFromFarmer = "0";
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                decimal lnamt = 0;
                lnamt = Convert.ToDecimal(IRRloan_fr) - Convert.ToDecimal(IRRloan_CommodityReceivedFromFarmer);
                if (lnamt <= 0)
                {
                    txtIrrigationLoan.Text = "0";
                }
                else
                {
                    txtIrrigationLoan.Text = Convert.ToString(lnamt);
                }
            }

        }
        catch (Exception)
        { }
    }
    protected void GetBankLoan_LoanAmt_Info(string fid, string did)
    {

    }

    protected void GetMax_TehsilYeild(string qry, string fid)
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            lblTehsilYeild.Text = "0";
            decimal SinchitRakba = 0;
            decimal ASinchitRakba = 0;
            decimal CL_Sinchit = 0;
            decimal CL_ASinchit = 0;
            decimal CL_Sinchit_Percent = 0;
            decimal CL_ASinchit_Percent = 0;
            decimal Sinchit_NonAffected = 0;
            decimal ASinchit_NonAffected = 0;
            DataSet ds_crop = new DataSet();
            SqlDataAdapter da_crop = new SqlDataAdapter("Select Isnull((Select Isnull(Sum(Rakba_crop_sinchit),0)  From Farmer_LandRecordDescription Where Farmer_Id='" + fid.Trim() + "'),0) as RakbaSinchit ,Isnull((Select Isnull(Sum(Rakba_crop_asinchit),0)   From Farmer_LandRecordDescription Where Farmer_Id='" + fid.Trim() + "'),0) as AsinchitRakba,Isnull((Select ISNULL(Sum(CL_InAsinchitRakba),0) From CropLoss Where Farmer_Id='" + fid.Trim() + "'),0) as CL_Asinchit,Isnull((Select ISNULL(SUM(CL_InSinchitRakba),0)  From CropLoss Where Farmer_Id='" + fid.Trim() + "'),0) as CL_Sinchit,Isnull((Select ISNULL(CL_InSinchitRakbaPer ,0) From CropLoss Where Farmer_Id='" + fid.Trim() + "'),0) as Affeted_Sinchit_Per,Isnull((Select ISNULL(CL_InAsinchitRakbaPer,0) From CropLoss Where Farmer_Id='" + fid.Trim() + "'),0) as Affected_Asinchit_Per", con);
            da_crop.Fill(ds_crop);
            if (ds_crop.Tables[0].Rows.Count > 0)
            {
                SinchitRakba = Convert.ToDecimal(Convert.ToString(ds_crop.Tables[0].Rows[0]["RakbaSinchit"]));
                CL_Sinchit = Convert.ToDecimal(Convert.ToString(ds_crop.Tables[0].Rows[0]["CL_Sinchit"]));
                ASinchitRakba = Convert.ToDecimal(Convert.ToString(ds_crop.Tables[0].Rows[0]["AsinchitRakba"]));
                CL_ASinchit = Convert.ToDecimal(Convert.ToString(ds_crop.Tables[0].Rows[0]["CL_Asinchit"]));
                CL_Sinchit_Percent = Convert.ToDecimal(Convert.ToString(ds_crop.Tables[0].Rows[0]["Affeted_Sinchit_Per"]));
                CL_ASinchit_Percent = Convert.ToDecimal(Convert.ToString(ds_crop.Tables[0].Rows[0]["Affected_Asinchit_Per"]));
            }
            if (CL_Sinchit == 0 && CL_ASinchit == 0)
            {
                #region If_Rakba_Loss_Is_0
                string query = "Select (isnull(SUM(lr.Rakba_crop_sinchit),0)*isnull(ty.MaxYieldIrrigated,0)+isnull(SUM(lr.Rakba_crop_asinchit),0)*isnull(ty.MaxYieldUnIrrigated,0))  as Upaj From Farmer_LandRecordDescription as lr left join [TehsilYield] as ty on lr.Tehsil_ID=ty.TehsilCode Where lr.Farmer_Id='" + lblfarmerid.Text.Trim() + "' and lr.crpcode ='1' Group By ty.MaxYieldIrrigated,ty.MaxYieldUnIrrigated";
                DataSet dst1 = new DataSet();



                SqlDataAdapter dat1 = new SqlDataAdapter(query, con);
                dat1.Fill(dst1);
                if (dst1.Tables[0].Rows.Count > 0)
                {
                    decimal tehsilyeild = 0;
                    for (int i = 0; i <= dst1.Tables[0].Rows.Count - 1; i++)
                    {
                        tehsilyeild = tehsilyeild + Math.Round(Convert.ToDecimal(dst1.Tables[0].Rows[i]["Upaj"]), 2);
                    }
                    lblTehsilYeild.Text = Convert.ToString(tehsilyeild);
                }
                else
                {
                    lblTehsilYeild.Text = "0";
                }
                #endregion
            }

            else
            {
                #region Else




                DataSet dst = new DataSet();



                SqlDataAdapter dat = new SqlDataAdapter(qry, con);
                dat.Fill(dst);
                if (dst.Tables[0].Rows.Count > 0)
                {
                    decimal tehsilyeild_Sinchit = 0;
                    decimal tehsilyeild_ASinchit = 0;
                    for (int i = 0; i <= dst.Tables[0].Rows.Count - 1; i++)
                    {
                        tehsilyeild_Sinchit = tehsilyeild_Sinchit + Math.Round(Convert.ToDecimal(dst.Tables[0].Rows[i]["TehsilYield_Sinchit"]), 2);
                        tehsilyeild_ASinchit = tehsilyeild_ASinchit + Math.Round(Convert.ToDecimal(dst.Tables[0].Rows[i]["TehsilYield_Asinchit"]), 2);
                    }

                    Sinchit_NonAffected = SinchitRakba - CL_Sinchit;
                    ASinchit_NonAffected = ASinchitRakba - CL_ASinchit;


                    decimal NonAffetected_Productivity_Sinchit = 0;
                    decimal NonAffected_Productivity_Asichit = 0;


                    NonAffetected_Productivity_Sinchit = Sinchit_NonAffected * tehsilyeild_Sinchit;
                    NonAffected_Productivity_Asichit = ASinchit_NonAffected * tehsilyeild_ASinchit;

                    decimal Affected_Productivity_Sinchit = 0;
                    decimal Affected_Productivity_ASinchit = 0;

                    Affected_Productivity_Sinchit = CL_Sinchit * tehsilyeild_Sinchit;
                    Affected_Productivity_ASinchit = CL_ASinchit * tehsilyeild_ASinchit;

                    decimal Affected_Productivity_Sinchit_Per = 0;
                    decimal Affected_Productivity_Asichit_Per = 0;

                    //Check Percentage Range 
                    //Sinchit
                    if (CL_Sinchit_Percent >= 0 && CL_Sinchit_Percent <= 25)
                    {
                        CL_Sinchit_Percent = 0;
                    }
                    if (CL_Sinchit_Percent > 25 && CL_Sinchit_Percent <= 50)
                    {
                        CL_Sinchit_Percent = 25;
                    }
                    if (CL_Sinchit_Percent > 50 && CL_Sinchit_Percent <= 100)
                    {
                        CL_Sinchit_Percent = 50;
                    }
                    //
                    //Asinchit
                    if (CL_ASinchit_Percent >= 0 && CL_ASinchit_Percent <= 25)
                    {
                        CL_ASinchit_Percent = 0;
                    }
                    if (CL_ASinchit_Percent > 25 && CL_ASinchit_Percent <= 50)
                    {
                        CL_ASinchit_Percent = 25;
                    }
                    if (CL_ASinchit_Percent > 50 && CL_ASinchit_Percent <= 100)
                    {
                        CL_ASinchit_Percent = 50;
                    }
                    //


                    Affected_Productivity_Sinchit_Per = Affected_Productivity_Sinchit - Affected_Productivity_Sinchit * (CL_Sinchit_Percent / 100);

                    Affected_Productivity_Asichit_Per = Affected_Productivity_ASinchit - Affected_Productivity_ASinchit * (CL_ASinchit_Percent / 100);

                    decimal Final_Productivity_Sinchit = 0;
                    decimal Final_Productivity_Asinchit = 0;
                    decimal FInal_Productivity = 0;
                    Final_Productivity_Sinchit = NonAffetected_Productivity_Sinchit + Affected_Productivity_Sinchit_Per;

                    Final_Productivity_Asinchit = NonAffected_Productivity_Asichit + Affected_Productivity_Asichit_Per;

                    FInal_Productivity = Math.Abs(Final_Productivity_Sinchit) + Math.Abs(Final_Productivity_Asinchit);

                    ////


                    lblTehsilYeild.Text = Convert.ToString(Math.Round(Convert.ToDecimal(FInal_Productivity), 2));

                }
                else
                {
                    lblTehsilYeild.Text = "0";
                }
                #endregion
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            //Math.Round(Convert.ToDouble(lblTehsilYeild.Text.Trim()), 2);
            decimal NewTehsilYeild = ((Math.Round(Convert.ToDecimal(lblTehsilYeild.Text.Trim()), 2) * 2) / 100 + Convert.ToDecimal(lblTehsilYeild.Text.Trim()));


            lblTehsilYeild.Text = Convert.ToString(Math.Round(NewTehsilYeild, 2));
        }
        catch (Exception)
        {

        }

    }
    void CreateColoumnIn_dt()
    {
        try
        {
            dt.Columns.Add("AnajPrakar_Code");
            dt.Columns.Add("AnajPrakar_Name");
            dt.Columns.Add("BoriSankhya");
            dt.Columns.Add("Matra");
            dt.Columns.Add("BhugtanYogy_Rashi");
            Session["dt"] = dt;
        }
        catch (Exception ex)
        {


        }
    }
    protected string Get_Temp_TehsilYeild(string qry, string fid)
    {
        string res = "";
        string TehsilYeild = "0";
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            decimal SinchitRakba = 0;
            decimal ASinchitRakba = 0;
            decimal CL_Sinchit = 0;
            decimal CL_ASinchit = 0;
            decimal CL_Sinchit_Percent = 0;
            decimal CL_ASinchit_Percent = 0;
            decimal Sinchit_NonAffected = 0;
            decimal ASinchit_NonAffected = 0;
            DataSet ds_crop = new DataSet();
            //SqlDataAdapter da_crop = new SqlDataAdapter("Select (Select Isnull(Sum(Rakba_crop_sinchit),0)+Isnull(Sum(Rakba_crop_asinchit),0)   From Farmer_LandRecordDescription Where Farmer_Id='" + fid.Trim() + "') as fcrop,(Select ISNULL(Sum(CL_InAsinchitRakbaPer),0)+ISNULL(SUM(CL_InSinchitRakbaPer),0)  From CropLoss Where Farmer_Id='" + fid.Trim() + "') as CLPer", con);
            SqlDataAdapter da_crop = new SqlDataAdapter("Select (Select Isnull(Sum(Rakba_crop_sinchit),0)From Farmer_LandRecordDescription Where Farmer_Id='" + fid.Trim() + "') as RakbaSinchit ,(Select Isnull(Sum(Rakba_crop_asinchit),0)   From Farmer_LandRecordDescription Where Farmer_Id='" + fid.Trim() + "') as AsinchitRakba,(Select ISNULL(Sum(CL_InAsinchitRakba),0) From CropLoss Where Farmer_Id='" + fid.Trim() + "') as CL_Asinchit,(Select ISNULL(SUM(CL_InSinchitRakba),0)  From CropLoss Where Farmer_Id='" + fid.Trim() + "') as CL_Sinchit", con);
            da_crop.Fill(ds_crop);
            if (ds_crop.Tables[0].Rows.Count > 0)
            {
                SinchitRakba = Convert.ToDecimal(Convert.ToString(ds_crop.Tables[0].Rows[0]["RakbaSinchit"]));
                CL_Sinchit = Convert.ToDecimal(Convert.ToString(ds_crop.Tables[0].Rows[0]["CL_Sinchit"]));
                ASinchitRakba = Convert.ToDecimal(Convert.ToString(ds_crop.Tables[0].Rows[0]["AsinchitRakba"]));
                CL_ASinchit = Convert.ToDecimal(Convert.ToString(ds_crop.Tables[0].Rows[0]["CL_Asinchit"]));
            }
            if (CL_Sinchit == 0 && CL_ASinchit == 0)
            {
                #region If_Rakba_Loss_Is_0
                string query = "Select (isnull(SUM(lr.Rakba_crop_sinchit),0)*isnull(ty.MaxYieldIrrigated,0)+isnull(SUM(lr.Rakba_crop_asinchit),0)*isnull(ty.MaxYieldUnIrrigated,0))  as Upaj From Farmer_LandRecordDescription as lr left join [TehsilYield] as ty on lr.Tehsil_ID=ty.TehsilCode Where lr.Farmer_Id='" + lblfarmerid.Text.Trim() + "' and lr.crpcode ='1' Group By ty.MaxYieldIrrigated,ty.MaxYieldUnIrrigated";
                DataSet dst1 = new DataSet();



                SqlDataAdapter dat1 = new SqlDataAdapter(query, con);
                dat1.Fill(dst1);
                if (dst1.Tables[0].Rows.Count > 0)
                {
                    decimal tehsilyeild = 0;
                    for (int i = 0; i <= dst1.Tables[0].Rows.Count - 1; i++)
                    {
                        tehsilyeild = tehsilyeild + Math.Round(Convert.ToDecimal(dst1.Tables[0].Rows[i]["Upaj"]), 2);
                    }
                    TehsilYeild = Convert.ToString(tehsilyeild);
                }
                else
                {
                    TehsilYeild = "0";
                }
                #endregion
            }
            else
            {
                #region elsepart
                DataSet dst = new DataSet();



                SqlDataAdapter dat = new SqlDataAdapter(qry, con);
                dat.Fill(dst);
                if (dst.Tables[0].Rows.Count > 0)
                {
                    decimal tehsilyeild_Sinchit = 0;
                    decimal tehsilyeild_ASinchit = 0;
                    for (int i = 0; i <= dst.Tables[0].Rows.Count - 1; i++)
                    {
                        tehsilyeild_Sinchit = tehsilyeild_Sinchit + Math.Round(Convert.ToDecimal(dst.Tables[0].Rows[i]["TehsilYield_Sinchit"]), 2);
                        tehsilyeild_ASinchit = tehsilyeild_ASinchit + Math.Round(Convert.ToDecimal(dst.Tables[0].Rows[i]["TehsilYield_Asinchit"]), 2);
                    }


                    Sinchit_NonAffected = SinchitRakba - CL_Sinchit;
                    ASinchit_NonAffected = ASinchitRakba - CL_ASinchit;


                    decimal NonAffetected_Productivity_Sinchit = 0;
                    decimal NonAffected_Productivity_Asichit = 0;


                    NonAffetected_Productivity_Sinchit = Sinchit_NonAffected * tehsilyeild_Sinchit;
                    NonAffected_Productivity_Asichit = Sinchit_NonAffected * tehsilyeild_ASinchit;

                    decimal Affected_Productivity_Sinchit = 0;
                    decimal Affected_Productivity_ASinchit = 0;

                    Affected_Productivity_Sinchit = CL_Sinchit * tehsilyeild_Sinchit;
                    Affected_Productivity_ASinchit = CL_ASinchit * tehsilyeild_ASinchit;

                    decimal Affected_Productivity_Sinchit_Per = 0;
                    decimal Affected_Productivity_Asichit_Per = 0;


                    //Check Percentage Range 
                    //Sinchit
                    if (CL_Sinchit_Percent >= 0 && CL_Sinchit_Percent <= 25)
                    {
                        CL_Sinchit_Percent = 0;
                    }
                    if (CL_Sinchit_Percent > 25 && CL_Sinchit_Percent <= 50)
                    {
                        CL_Sinchit_Percent = 25;
                    }
                    if (CL_Sinchit_Percent > 50 && CL_Sinchit_Percent <= 100)
                    {
                        CL_Sinchit_Percent = 50;
                    }
                    //
                    //Asinchit
                    if (CL_ASinchit_Percent >= 0 && CL_ASinchit_Percent <= 25)
                    {
                        CL_ASinchit_Percent = 0;
                    }
                    if (CL_ASinchit_Percent > 25 && CL_ASinchit_Percent <= 50)
                    {
                        CL_ASinchit_Percent = 25;
                    }
                    if (CL_ASinchit_Percent > 50 && CL_ASinchit_Percent <= 100)
                    {
                        CL_ASinchit_Percent = 50;
                    }
                    //


                    Affected_Productivity_Sinchit_Per = Affected_Productivity_Sinchit - Affected_Productivity_Sinchit * (CL_Sinchit_Percent / 100);

                    Affected_Productivity_Asichit_Per = Affected_Productivity_ASinchit - Affected_Productivity_ASinchit * (CL_ASinchit_Percent / 100);

                    decimal Final_Productivity_Sinchit = 0;
                    decimal Final_Productivity_Asinchit = 0;
                    decimal FInal_Productivity = 0;
                    Final_Productivity_Sinchit = NonAffetected_Productivity_Sinchit + Affected_Productivity_Sinchit_Per;

                    Final_Productivity_Asinchit = NonAffected_Productivity_Asichit + Affected_Productivity_Asichit_Per;

                    FInal_Productivity = Final_Productivity_Sinchit + Final_Productivity_Asinchit;

                    TehsilYeild = Convert.ToString(Math.Round(Convert.ToDecimal(FInal_Productivity), 2));



                }
                else
                {
                    TehsilYeild = "0";
                }
                #endregion
            }


            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }


            decimal NewTehsilYeild = ((Math.Round(Convert.ToDecimal(TehsilYeild.Trim()), 2) * 2) / 100 + Convert.ToDecimal(TehsilYeild.Trim()));
            res = Convert.ToString(Math.Round(NewTehsilYeild, 2));
            return res;
        }
        catch (Exception)
        {

        }
        return res;
    }

    protected bool Check_Valid_Entry_From_DB(string fid, string did)
    {
        bool res = false;
        try
        {
            //string db_abhitakBecha = "";
            //string db_commdity_code = "";
            //string db_commodity_name = "";
            decimal GridSum = 0;
            decimal AbhiTakBecha = 0;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            DataSet ds_cab = new DataSet();
            //SqlDataAdapter da = new SqlDataAdapter("Select CT.CommodityId,CM.crop,isnull(Sum(CT.QtyReceived),0) as Matra From CommodityReceived_Transaction as CT left join Crop_Master as CM on CT.CommodityId=CM.crpcode Where Farmer_Id='" + fid.Trim() + "' and District_Id='" + did.Trim() + "' group by ct.CommodityId,cm.crop", con);
            SqlDataAdapter da_cab = new SqlDataAdapter("Select CT.CommodityId,CM.CommodityName,isnull(Sum(CT.QtyReceived),0) as Matra From CommodityReceivedFromFarmer as CT left join CommodityRate as CM on CT.CommodityId=CM.CommodityId Where Farmer_Id='" + fid.Trim() + "' and District_Id='" + did.Trim() + "' group by ct.CommodityId,cm.CommodityName", con);
            da_cab.Fill(ds_cab);
            if (ds_cab.Tables[0].Rows.Count > 0)
            {
                //for (int ab = 0; ab <= ds_cab.Tables[0].Rows.Count - 1; ab++)
                //{
                //    db_abhitakBecha = Convert.ToString(ds_cab.Tables[0].Rows[ab]["Matra"]);
                //    db_commdity_code = Convert.ToString(ds_cab.Tables[0].Rows[ab]["CommodityId"]);
                //    db_commodity_name = Convert.ToString(ds_cab.Tables[0].Rows[ab]["CommodityName"]);

                //}
                string[] check_FT = new string[50];
                int count = 0;
                for (int d1 = 0; d1 <= ds_cab.Tables[0].Rows.Count - 1; d1++)
                {

                    for (int g1 = 0; g1 <= GridView2.Rows.Count - 1; g1++)
                    {

                        if (Convert.ToString(ds_cab.Tables[0].Rows[d1]["CommodityId"]) == Convert.ToString(GridView2.Rows[g1].Cells[5].Text.Trim()))
                        {

                            AbhiTakBecha = Convert.ToDecimal(Convert.ToString(ds_cab.Tables[0].Rows[d1]["Matra"]).Trim());
                            GridSum = Convert.ToDecimal(GridView2.Rows[g1].Cells[3].Text.Trim());
                            decimal CTotal = GridSum;
                            decimal QTY = 0;
                            string Temp_TehsilYeild = "";
                            string query = "";
                            if (Convert.ToString(ds_cab.Tables[0].Rows[d1]["CommodityId"]) == "1")
                            {

                                //query = "Select (isnull(SUM(lr.Rakba_crop_sinchit),0)*isnull(ty.MaxYieldIrrigated,0)+isnull(SUM(lr.Rakba_crop_asinchit),0)*isnull(ty.MaxYieldUnIrrigated,0))  as Upaj From Farmer_LandRecordDescription as lr left join [TehsilYield] as ty on lr.Tehsil_ID=ty.TehsilCode Where lr.Farmer_Id='" + ddl_Farmer.SelectedItem.Value.Trim() + "' and lr.crpcode ='1' Group By ty.MaxYieldIrrigated,ty.MaxYieldUnIrrigated";
                                //query = "Select Isnull((Select (isnull(SUM(lr.Rakba_crop_sinchit),0)*isnull(ty.MaxYieldIrrigated,0)) From Farmer_LandRecordDescription as lr left join [TehsilYield] as ty on lr.Tehsil_ID=ty.TehsilCode Where lr.Farmer_Id='" + ddl_Farmer.SelectedItem.Value.Trim() + "' and lr.crpcode ='1' Group By ty.MaxYieldIrrigated),0) as TehsilYield_Sinchit,Isnull((Select (isnull(SUM(lr.Rakba_crop_asinchit),0)*isnull(ty.MaxYieldUnIrrigated,0))   From Farmer_LandRecordDescription as lr left join [TehsilYield] as ty on lr.Tehsil_ID=ty.TehsilCode Where lr.Farmer_Id='" + ddl_Farmer.SelectedItem.Value.Trim() + "' and lr.crpcode ='1' Group By ty.MaxYieldUnIrrigated),0) as TehsilYield_Asinchit ";
                                query = "Select (Select isnull(ty.MaxYieldIrrigated,0)From Farmer_LandRecordDescription as lr left join [TehsilYield] as ty on lr.Tehsil_ID=ty.TehsilCode Where lr.Farmer_Id='" + lblfarmerid.Text.Trim() + "' and lr.crpcode ='1' Group By ty.MaxYieldIrrigated) as TehsilYield_Sinchit,(Select isnull(ty.MaxYieldUnIrrigated,0)  From Farmer_LandRecordDescription as lr left join [TehsilYield] as ty on lr.Tehsil_ID=ty.TehsilCode Where lr.Farmer_Id='" + lblfarmerid.Text.Trim() + "' and lr.crpcode ='1' Group By ty.MaxYieldUnIrrigated) as TehsilYield_Asinchit ";
                                Temp_TehsilYeild = Get_Temp_TehsilYeild(query.Trim(), fid.Trim());
                            }



                            QTY = Math.Round((Convert.ToDecimal(Temp_TehsilYeild.Trim()) - AbhiTakBecha), 2);


                            if (CTotal > QTY)
                            {
                                //drRC["Result"] = "false";
                                check_FT[count] = "F";
                                //return false;
                            }
                            if (CTotal <= QTY)
                            {
                                check_FT[count] = "T";
                                //return true;
                                //drRC["Result"] = "true";
                            }
                            count++;

                        }
                    }
                }
                foreach (string s in check_FT)
                {
                    if (s == "F")
                    {
                        return false;
                    }
                }
                return true;

            }
            else
            {
                return true;
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }


        }
        catch (Exception)
        { }
        return res;
    }
    protected bool Check_ValidEntry()
    {
        bool res = false;
        try
        {
            //double GridSum = 0;
            //double QTY = 0;

            //for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
            //{
            //    GridSum = GridSum + Convert.ToDouble(GridView2.Rows[i].Cells[3].Text.Trim());
            //}
            //double CTotal = Convert.ToDouble(txtMatra.Text.Trim()) + GridSum;

            //QTY=Convert.ToDouble(lblTehsilYeild.Text.Trim()) - Convert.ToDouble(txtBechaAnaj_AbhiTak.Text.Trim())  ;


            //if (CTotal > QTY)
            //{
            //    return false;
            //}
            //if (CTotal <= QTY)
            //{
            //    return true;
            //}

            decimal GridSum = 0;
            decimal QTY = 0;
            decimal AbhiTakBecha = 0;


            for (int c = 0; c <= GridView_AbhiTak_Becha.Rows.Count - 1; c++)
            {
                if (Convert.ToString(GridView_AbhiTak_Becha.Rows[c].Cells[2].Text.Trim()) == ddl_Comodity.SelectedItem.Value.Trim())
                {
                    AbhiTakBecha = AbhiTakBecha + Convert.ToDecimal(GridView_AbhiTak_Becha.Rows[c].Cells[1].Text.Trim());
                }
            }




            for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
            {
                if (Convert.ToString(GridView2.Rows[i].Cells[5].Text.Trim()) == ddl_Comodity.SelectedItem.Value.Trim())
                {

                    GridSum = GridSum + Convert.ToDecimal(GridView2.Rows[i].Cells[3].Text.Trim());
                }
            }



            decimal CTotal = Convert.ToDecimal(txtMatra.Text.Trim()) + GridSum;

            QTY = Math.Round((Convert.ToDecimal(lblTehsilYeild.Text.Trim()) - AbhiTakBecha), 2);


            if (CTotal > QTY)
            {
                return false;
            }
            if (CTotal <= QTY)
            {
                return true;
            }
        }
        catch (Exception)
        {

        }
        return res;

    }
    protected bool Check_IfFarmerCrop(string fid, string cropcode)
    {
        bool res = false;
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("Select  COUNT(*) From Farmer_LandRecordDescription Where Farmer_Id='" + fid.Trim() + "' and crpcode='" + cropcode.Trim() + "'", con);
            int count = (int)cmd.ExecuteScalar();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            if (count == 0)
            {
                return false;
            }
            if (count > 0)
            {
                return true;
            }
        }
        catch (Exception)
        {

        }
        return res;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtBoriSankhya.Text.Trim() == String.Empty)
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('बोरी स.की प्रविष्टि करे'); </script> ");
                return;
            }
            if (txtMatra.Text.Trim() == String.Empty)
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('मात्रा की प्रविष्टि करे'); </script> ");
                return;
            }
            try
            {
                decimal.Parse(txtMatra.Text.Trim());
            }
            catch
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('सही मात्रा की प्रविष्टि करे'); </script> ");
                return;
            }
            try
            {
                int.Parse(txtBoriSankhya.Text.Trim());
            }
            catch
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('सही बोरी संख्या प्रविष्टि करे'); </script> ");
                return;
            }
            if (txt_Amount_Against_SocLoan.Text.Trim() == String.Empty)
            {
                txt_Amount_Against_SocLoan.Text = "0";
            }
            if (txt_Amt_AgainstDCCBLoan.Text.Trim() == String.Empty)
            {
                txt_Amt_AgainstDCCBLoan.Text = "0";
            }
            if (txt_Amount_AgainstIrrigationLoan.Text.Trim() == String.Empty)
            {
                txt_Amount_AgainstIrrigationLoan.Text = "0";
            }
            decimal BhugtanYogyaRashi_Sum = 0;
            //if (ddl_Farmer.SelectedIndex == 0)
            //{
            //    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('किसान  का चयन करे'); </script> ");
            //    return;
            //}
            // GetCropSinchit_Asinchit(ddl_Farmer.SelectedItem.Value);
            if (ddl_Comodity.SelectedIndex == 0)
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('अनाज के प्रकार का चयन करे'); </script> ");
                return;
            }

            if (Convert.ToDouble(txtMatra.Text.Trim()) <= 0)
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('सही मात्रा प्रविष्टि करे'); </script> ");
                return;
            }
            if (Convert.ToDouble(txtBoriSankhya.Text.Trim()) <= 0)
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('सही बोरी स.प्रविष्टि करे'); </script> ");
                return;
            }
            string fcr = "";
            if (ddl_Comodity.SelectedItem.Value.Trim() == "1")
            {
                fcr = "1";
            }



            if (Check_IfFarmerCrop(lblfarmerid.Text, fcr.Trim()) == false)
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('किसान का चयनित फसल के लिए पंजीयन नहीं हुआ है'); </script> ");
                return;
            }



            for (int j = 0; j <= GridView2.Rows.Count - 1; j++)
            {
                if (Convert.ToString(GridView2.Rows[j].Cells[5].Text) == Convert.ToString(ddl_Comodity.SelectedItem.Value.Trim()))
                {
                    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('चयनित अनाज की प्रविष्टि पहले हो चुकी है'); </script> ");
                    return;
                }
            }


            if (Check_ValidEntry() == false)
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('किसान अधिकतम उपज से ज्यादा नहीं बेच सकता'); </script> ");

                return;
            }

            for (int ch = 0; ch <= GridView_AbhiTak_Becha.Rows.Count - 1; ch++)
            {
                if (ddl_Comodity.SelectedItem.Value.Trim() == Convert.ToString(GridView_AbhiTak_Becha.Rows[ch].Cells[2].Text.Trim()))
                {
                    if (Math.Round(Convert.ToDecimal(txtMatra.Text.Trim()), 2) + Math.Round(Convert.ToDecimal(GridView_AbhiTak_Becha.Rows[ch].Cells[1].Text.Trim()), 2) > Math.Round(Convert.ToDecimal(lblTehsilYeild.Text.Trim()), 2))
                    {
                        Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('किसान अधिकतम उपज से ज्यादा नहीं बेच सकता'); </script> ");

                        return;
                    }
                }
            }

            if (Math.Round(Convert.ToDecimal(txtMatra.Text.Trim()), 2) <= 0)
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('सही मात्रा प्रविष्ट करे'); </script> ");

                return;


            }

            BhugtanYogyaRashi_Sum = Convert.ToDecimal(txtMatra.Text.Trim()) * (Convert.ToDecimal(txt_kendraBonus.Text.Trim()) + Convert.ToDecimal(txt_Rajya_Bonus.Text.Trim()) + Convert.ToDecimal(txtSamarthan_muly.Text.Trim()));

            BhugtanYogyaRashi_Sum = Math.Round(BhugtanYogyaRashi_Sum, 3);
            if (Session["dt"] == null)
            {
                CreateColoumnIn_dt();
            }
            else
            {
                dt = (DataTable)Session["dt"];
                DataRow drRC = dt.NewRow();

                drRC["AnajPrakar_Code"] = ddl_Comodity.SelectedItem.Value.ToString().Trim();
                drRC["AnajPrakar_Name"] = ddl_Comodity.SelectedItem.Text.ToString().Trim();
                drRC["BoriSankhya"] = Convert.ToString(Convert.ToInt16(txtBoriSankhya.Text.Trim()));
                drRC["Matra"] = Convert.ToString(Math.Round(Convert.ToDecimal(txtMatra.Text.Trim()), 3));
                drRC["BhugtanYogy_Rashi"] = Convert.ToString(BhugtanYogyaRashi_Sum);
                dt.Rows.Add(drRC);
                GridView2.DataSource = dt;
                GridView2.DataBind();
                Session["dt"] = dt;
                //ddl_Comodity.SelectedIndex = 0;
                txtMatra.Text = "";
                txtBoriSankhya.Text = "";

                //txt_Rajya_Bonus.Text = "";
                //txtSamarthan_muly.Text = "";
                decimal TotalQTY = 0;
                decimal BhugtanYogyaRashi = 0;
                for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
                {
                    TotalQTY = TotalQTY + Convert.ToDecimal(GridView2.Rows[i].Cells[3].Text.Trim());
                    BhugtanYogyaRashi = BhugtanYogyaRashi + Convert.ToDecimal(GridView2.Rows[i].Cells[4].Text.Trim());
                }
                if (TotalQTY > 0)
                {
                    txtAnajKulMatra.Text = Convert.ToString(TotalQTY);
                }
                if (TotalQTY <= 0)
                {
                    txtAnajKulMatra.Text = "0";
                }
                if (BhugtanYogyaRashi > 0)
                {
                    txtShudhBhugtan_YogyaRashi.Text = Convert.ToString(BhugtanYogyaRashi);


                    txtShudhBhugtan.Text = txtShudhBhugtan_YogyaRashi.Text.Trim();
                }
                if (BhugtanYogyaRashi <= 0)
                {
                    txtShudhBhugtan_YogyaRashi.Text = "0";
                    txtShudhBhugtan.Text = txtShudhBhugtan_YogyaRashi.Text.Trim();
                }

            }
        }
        catch (Exception)
        {

        }
    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            decimal TotalQTY = 0;
            decimal BhugtanYogyaRashi = 0;
            decimal F_TotalQTY = 0;
            decimal F_BhugtanYogyaRashi = 0;
            int idx = GridView2.SelectedIndex;

            TotalQTY = Convert.ToDecimal(GridView2.Rows[idx].Cells[3].Text.Trim());
            BhugtanYogyaRashi = Convert.ToDecimal(GridView2.Rows[idx].Cells[4].Text.Trim());
            if (txtAnajKulMatra.Text.Trim() == String.Empty)
            {
                txtAnajKulMatra.Text = "0";
            }
            if (txtShudhBhugtan_YogyaRashi.Text.Trim() == String.Empty)
            {
                txtShudhBhugtan_YogyaRashi.Text = "0";
            }
            F_TotalQTY = Convert.ToDecimal(txtAnajKulMatra.Text) - TotalQTY;
            txtAnajKulMatra.Text = Convert.ToString(System.Math.Abs(F_TotalQTY));
            F_BhugtanYogyaRashi = Convert.ToDecimal(txtShudhBhugtan_YogyaRashi.Text) - BhugtanYogyaRashi;
            txtShudhBhugtan_YogyaRashi.Text = Convert.ToString(System.Math.Abs(F_BhugtanYogyaRashi));

            txtShudhBhugtan.Text = txtShudhBhugtan_YogyaRashi.Text.Trim();

            dt = (DataTable)Session["dt"];
            dt.Rows[idx].Delete();
            GridView2.DataSource = dt;
            GridView2.DataBind();
            Session["dt"] = dt;

        }
        catch (Exception ex)
        {

        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        try
        {
            #region Coding and checks of insertion

            if (lblfrmsocityid.Text.Trim() == Session["Society_Id"].ToString())
            {
                if (txtPraptiDate.Text.Trim() == String.Empty)
                {
                    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('प्राप्ति दिनांक प्रविष्ट करे |'); </script> ");
                    return;
                }

                DateTime startdate = DateTime.ParseExact("01/01/2013", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime enddate = DateTime.ParseExact("01/01/2013", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime datevalue = DateTime.ParseExact(txtPraptiDate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string phase1 = "";

                DataSet ds_chd1 = new DataSet();
                SqlDataAdapter da_chd1 = new SqlDataAdapter("Select Id,Phase,Division,CONVERT(varchar(50),GETDATE(),103) as TodayDate From tbl_Division_Reg_Date_PhaseWise Where Division in (Select DivisionCode From Districts Where District_Code='" + Session["District_Code"].ToString() + "')", con);
                da_chd1.Fill(ds_chd1);

                if (ds_chd1.Tables[0].Rows.Count > 0)
                {
                    phase1 = Convert.ToString(ds_chd1.Tables[0].Rows[0]["Phase"]);
                    //DateTime   TodayDate = Convert.ToDateTime(Convert.ToString(ds_chd1.Tables[0].Rows[0]["TodayDate"].ToString().Trim()));
                    DateTime TodayDate = DateTime.ParseExact(Convert.ToString(ds_chd1.Tables[0].Rows[0]["TodayDate"].ToString().Trim()), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (phase1.Trim() == "A")
                    {
                        startdate = DateTime.ParseExact("25/03/2014", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        enddate = DateTime.ParseExact("26/05/2014", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    if (phase1.Trim() == "B")
                    {
                        startdate = DateTime.ParseExact("01/04/2014", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        enddate = DateTime.ParseExact("26/05/2014", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }

                    if (phase1.Trim() == "A")
                    {
                        if (Convert.ToDateTime(TodayDate) < startdate)
                        {
                            Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('अनाज खरीदी की अवधि 25/03/2014 से 26/05/2014 तक  है |'); </script> ");
                            return;
                        }
                        if (Convert.ToDateTime(TodayDate) > enddate)
                        {
                            Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('अनाज खरीदी की अवधि 25/03/2014 से 26/05/2014 तक  है |'); </script> ");
                            return;
                        }
                        if (datevalue < startdate)
                        {
                            Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('प्राप्ति दिंनाक 25/03/2014 से 26/05/2014 के बीच की चुने |'); </script> ");
                            return;
                        }
                        if (datevalue > enddate)
                        {
                            Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('प्राप्ति दिंनाक 25/03/2014 से 26/05/2014 के बीच की चुने |'); </script> ");
                            return;
                        }
                    }
                    if (phase1.Trim() == "B")
                    {
                        if (Convert.ToDateTime(TodayDate) < startdate)
                        {
                            Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('अनाज खरीदी की अवधि 01/04/2014 से 26/05/2014 तक  है |'); </script> ");
                            return;
                        }
                        if (Convert.ToDateTime(TodayDate) > enddate)
                        {
                            Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('अनाज खरीदी की अवधि 01/04/2014 से 26/05/2014 तक  है |'); </script> ");
                            return;
                        }
                        if (datevalue < startdate)
                        {
                            Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('प्राप्ति दिंनाक 01/04/2014 से 26/05/2014 के बीच की चुने |'); </script> ");
                            return;
                        }
                        if (datevalue > enddate)
                        {
                            Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('प्राप्ति दिंनाक 01/04/2014 से 26/05/2014 के बीच की चुने |'); </script> ");
                            return;
                        }
                    }



                }
                //DateTime mindate = DateTime.ParseExact("25/03/2014", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //DateTime maxdate = DateTime.ParseExact("25/05/2014", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //DateTime datevalue = DateTime.ParseExact(txtPraptiDate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //if (datevalue < mindate)
                //{
                //    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('प्राप्ति दिनांक 25/03/2014 से 25/05/2014 के बीच की चुने |'); </script> ");
                //    return;
                //}
                //if (datevalue > maxdate)
                //{
                //    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('प्राप्ति दिनांक 25/03/2014 से 25/05/2014 के बीच की चुने |'); </script> ");
                //    return;
                //}
            }
            else
            {
                MsgScriptwith("किसान सोसायटी और उपार्जन केंद्र एक नहीं मिली पुनः प्रयास करें..", "../Login1.aspx");
                return;
            }
        }
        catch (Exception)
        {
            Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('प्राप्ती दिनांक dd/MM/yyyy फॉर्मेट मे ही प्रविष्ट करे |'); </script> ");
            return;
        }


        if (GridView2.Rows.Count <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('किसान द्वारा लायी गयी अनाज की मात्रा जोड़े'); </script> ");
            return;
        }

        if (txt_Amount_Against_SocLoan.Text.Trim() == String.Empty)
        {
            txt_Amount_Against_SocLoan.Text = "0";
        }
        if (txt_Amt_AgainstDCCBLoan.Text.Trim() == String.Empty)
        {
            txt_Amt_AgainstDCCBLoan.Text = "0";
        }
        if (txt_Amount_AgainstIrrigationLoan.Text == String.Empty)
        {
            txt_Amount_AgainstIrrigationLoan.Text = "0";
        }
        if (txtShudhBhugtan.Text == String.Empty)
        {
            txtShudhBhugtan.Text = "0";
        }

        #region Check_Dup_Col
        string[] arr1 = new string[GridView2.Rows.Count];



        int c1 = 1;


        for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
        {
            if (Convert.ToString(GridView2.Rows[i].Cells[4].Text).Trim() == "1")
            {
                arr1[i] = Convert.ToString(c1);
                c1++;
            }
        }





        foreach (string s in arr1)
        {
            if (Convert.ToInt16(s) > 1)
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('एक फसल को दो बार नहीं जोड़ सकते'); </script> ");

                return;
            }
        }


        #endregion



        if (Check_Valid_Entry_From_DB(lblfarmerid.Text.Trim(), Session["District_Code"].ToString()) == false)
        {
            Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('किसान अधिकतम उपज से ज्यादा नहीं बेच सकता'); </script> ");

            return;
        }




        string agency = "";
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        DataSet ds_ag = new DataSet();
        SqlDataAdapter da_getAgency = new SqlDataAdapter("Select AgencyId From Initial Where District_ID='" + Session["District_Code"].ToString() + "' and Society_Id='" + Session["Society_Id"].ToString() + "'", con);
        da_getAgency.Fill(ds_ag);
        if (ds_ag.Tables[0].Rows.Count > 0)
        {
            agency = Convert.ToString(ds_ag.Tables[0].Rows[0]["AgencyId"]);
        }
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
        txtPraptiDate.Text = ddldate.SelectedValue;
        string RcvDate = "";
        RcvDate = getDate_MDY(txtPraptiDate.Text.Trim().ToString());
        //RcvDate = txtPraptiDate.Text.Trim();

        if (txtAnajKulMatra.Text.Trim() == String.Empty)
        {
            txtAnajKulMatra.Text = "0";
        }
        string[] computer_name = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName.Split(new Char[] { '.' });
        String ecn = System.Environment.MachineName;
        string HostName = computer_name[0].ToString();
        #region InsertCode_in_CommodityReceivedFromFarmer
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        string ReceivedID = "";
        SqlCommand cmd_sp = new SqlCommand();
        cmd_sp.Connection = con;
        SqlTransaction trans = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
        cmd_sp.Transaction = trans;
        cmd_sp.CommandText = "stp_insert_in_CommodityReceived_on";
        cmd_sp.CommandType = CommandType.StoredProcedure;
        try
        {
            // cmd_sp.Parameters.AddWithValue("@District_Id", Session["District_Code"].ToString());
            //
            cmd_sp.Parameters.Add("@District_Id", SqlDbType.VarChar, 4);
            cmd_sp.Parameters["@District_Id"].Value = Session["District_Code"].ToString();

            // cmd_sp.Parameters.AddWithValue("@Farmer_Id", ddl_Farmer.SelectedItem.Value.ToString());
            //
            cmd_sp.Parameters.Add("@Farmer_Id", SqlDbType.VarChar, 18);
            cmd_sp.Parameters["@Farmer_Id"].Value = lblfarmerid.Text.ToString();

            //  cmd_sp.Parameters.AddWithValue("@Proc_AgID", "");
            //
            cmd_sp.Parameters.Add("@Proc_AgID", SqlDbType.VarChar, 2);
            cmd_sp.Parameters["@Proc_AgID"].Value = agency;

            //   cmd_sp.Parameters.AddWithValue("@Society_Id", Session["Society_Id"].ToString());
            //
            cmd_sp.Parameters.Add("@Society_Id", SqlDbType.VarChar, 7);
            cmd_sp.Parameters["@Society_Id"].Value = Session["Society_Id"].ToString();

            //   cmd_sp.Parameters.AddWithValue("@CropYear", "2013-14");
            //
            cmd_sp.Parameters.Add("@CropYear", SqlDbType.VarChar, 10);
            cmd_sp.Parameters["@CropYear"].Value = "2014-15";


            cmd_sp.Parameters.Add("@CommodityId", SqlDbType.VarChar, 4);
            cmd_sp.Parameters["@CommodityId"].Value = Convert.ToString(GridView2.Rows[0].Cells[5].Text.Trim());

            cmd_sp.Parameters.Add("@QtyReceived", SqlDbType.Float);
            cmd_sp.Parameters["@QtyReceived"].Value = Convert.ToString(GridView2.Rows[0].Cells[3].Text.Trim());


            cmd_sp.Parameters.Add("@Bags", SqlDbType.Float);
            cmd_sp.Parameters["@Bags"].Value = Convert.ToString(GridView2.Rows[0].Cells[2].Text.Trim());

            cmd_sp.Parameters.Add("@MarketingSeasonId", SqlDbType.VarChar, 2);
            cmd_sp.Parameters["@MarketingSeasonId"].Value = "R";

            cmd_sp.Parameters.Add("@TotaAmountPayableToFarmer", SqlDbType.Float);
            cmd_sp.Parameters["@TotaAmountPayableToFarmer"].Value = txtShudhBhugtan_YogyaRashi.Text.Trim();


            cmd_sp.Parameters.Add("@TaulPatrakNo", SqlDbType.VarChar, 20);
            cmd_sp.Parameters["@TaulPatrakNo"].Value = txtTolPatrak.Text.Trim();


            cmd_sp.Parameters.Add("@FarmerLoanFromSc", SqlDbType.Float);
            cmd_sp.Parameters["@FarmerLoanFromSc"].Value = txtSocLoan.Text.Trim();

            cmd_sp.Parameters.Add("@FarmerLoanFromBank", SqlDbType.Float);
            cmd_sp.Parameters["@FarmerLoanFromBank"].Value = txtDCCBLoan.Text.Trim();

            cmd_sp.Parameters.Add("@AmtAgainstSCCredit", SqlDbType.Float);
            cmd_sp.Parameters["@AmtAgainstSCCredit"].Value = txt_Amount_Against_SocLoan.Text.Trim();

            cmd_sp.Parameters.Add("@AmtAgainstBankCredit", SqlDbType.Float);
            cmd_sp.Parameters["@AmtAgainstBankCredit"].Value = txt_Amt_AgainstDCCBLoan.Text.Trim();

            cmd_sp.Parameters.Add("@Irrigation_Loan", SqlDbType.Float);
            cmd_sp.Parameters["@Irrigation_Loan"].Value = txtIrrigationLoan.Text.Trim();

            cmd_sp.Parameters.Add("@AmtAgIrg_Loan", SqlDbType.Float);
            cmd_sp.Parameters["@AmtAgIrg_Loan"].Value = txt_Amount_AgainstIrrigationLoan.Text.Trim();


            cmd_sp.Parameters.Add("@NetAmountPayableToFarmer", SqlDbType.Float);
            cmd_sp.Parameters["@NetAmountPayableToFarmer"].Value = txtShudhBhugtan.Text.Trim();

            cmd_sp.Parameters.Add("@Date_Of_Receipt", SqlDbType.DateTime);
            cmd_sp.Parameters["@Date_Of_Receipt"].Value = Convert.ToDateTime(RcvDate);

            cmd_sp.Parameters.Add("@Status", SqlDbType.VarChar, 10);
            cmd_sp.Parameters["@Status"].Value = "ON";
            // cmd_sp.Parameters.AddWithValue("@UserId", Request.ServerVariables["REMOTE_ADDR"].ToString());
            //
            cmd_sp.Parameters.Add("@UserId", SqlDbType.VarChar, 20);
            cmd_sp.Parameters["@UserId"].Value = Request.ServerVariables["REMOTE_ADDR"].ToString();

            cmd_sp.Parameters.Add("@outputId", SqlDbType.VarChar, 50);
            cmd_sp.Parameters["@outputId"].Direction = ParameterDirection.Output;

            cmd_sp.ExecuteNonQuery();
            ReceivedID = Convert.ToString(cmd_sp.Parameters["@outputId"].Value);
            cmd_sp.Parameters.Clear();
            if (ReceivedID != "" && ReceivedID != "N")
            {
                Session["ReceivedID"] = ReceivedID;
                lblb.Text = "";
                lblb.Text = ReceivedID.ToString();
            }
            trans.Commit();
            btnSave.Enabled = false;
            btnPawati.Visible = true;
            chb_Confirm.Enabled = false;
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('किसान से अनाज की प्रविष्टि सुरक्षित कर ली गयी है'); </script> ");
            GridView_CropSinchit_Asinchit.DataSource = null;
            GridView_CropSinchit_Asinchit.DataBind();
            txtKisaanKulRakba.Text = "";
            txtSikamiRakba.Text = "";
            txtOwnRakba.Text = "";

            //ddl_Comodity.SelectedIndex = 0;
            txtBoriSankhya.Text = "";
            txtMatra.Text = "";
            //txtSamarthan_muly.Text = "";
            //txt_Rajya_Bonus.Text = "";
            GridView_AbhiTak_Becha.DataSource = null;
            GridView_AbhiTak_Becha.DataBind();
            txtTolPatrak.Text = "";
            txtAnajKulMatra.Text = "";
            txtShudhBhugtan_YogyaRashi.Text = "";
            GridView2.DataSource = null;
            GridView2.DataBind();
            txtSocLoan.Text = "0";
            txtDCCBLoan.Text = "0";
            txtIrrigationLoan.Text = "0";
            txt_Amount_Against_SocLoan.Text = "0";
            txt_Amt_AgainstDCCBLoan.Text = "0";
            txt_Amount_AgainstIrrigationLoan.Text = "0";
            txtShudhBhugtan.Text = "";
            Session["dt"] = null;
            CreateColoumnIn_dt();
        }
        catch (Exception ex)
        {
            trans.Rollback();
            lbl_ERMSG.Text = ex.Message;
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('पुनः प्रयास करे'); </script> ");

        }
        #endregion
        #endregion

    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Redirect("~/WHP14/Procurement_Wheat/frm_AnajPrapti_FromFarmer.aspx");
        }
        catch (Exception)
        { }
    }
    protected void CommIndex()
    {
        try
        {
            //if (ddl_Farmer.Items.Count <= 0)
            //{
            //    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('किसान सुची उपलब्ध नहीं है'); </script> ");
            //    //if (ddl_Comodity.Items.Count > 0)
            //    //{
            //    //    ddl_Comodity.SelectedIndex = 0;
            //    //}
            //    return;
            //}
            //if (ddl_Farmer.SelectedIndex == 0)
            //{
            //    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('किसान का चयन करे'); </script> ");
            //    //if (ddl_Comodity.Items.Count > 0)
            //    //{
            //    //    ddl_Comodity.SelectedIndex = 0;
            //    //}
            //    return;
            //}

            //GetCropSinchit_Asinchit(ddl_Farmer.SelectedItem.Value);
            if (ddl_Comodity.Items.Count <= 0)
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('अनाज/फसल की सुची उपलब्ध नहीं है'); </script> ");
                return;
            }
            if (ddl_Comodity.SelectedIndex == 0)
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('अनाज/फसल का चयन करे'); </script> ");
                return;
            }
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            DataSet dsC = new DataSet();
            SqlDataAdapter daC = new SqlDataAdapter("Select isnull(MSPRate,0) as Samarthan,ISNULL(CentralBonus,0) as Kendriya,ISNULL(StateBonus,0) as Rajya From  [dbo].[CommodityRate] Where CommodityId='" + ddl_Comodity.SelectedItem.Value.Trim() + "'", con);
            daC.Fill(dsC);
            if (dsC.Tables[0].Rows.Count > 0)
            {
                txtSamarthan_muly.Text = Convert.ToString(dsC.Tables[0].Rows[0]["Samarthan"]);
                txt_Rajya_Bonus.Text = Convert.ToString(dsC.Tables[0].Rows[0]["Rajya"]);
                txt_kendraBonus.Text = Convert.ToString(dsC.Tables[0].Rows[0]["Kendriya"]);
            }
            else
            {
                txtSamarthan_muly.Text = "0";
                txt_Rajya_Bonus.Text = "0";
                txt_kendraBonus.Text = "0";


            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            string query = "";


            if (ddl_Comodity.SelectedItem.Value.Trim() == "1")
            {

                //query = "Select (isnull(SUM(lr.Rakba_crop_sinchit),0)*isnull(ty.MaxYieldIrrigated,0)+isnull(SUM(lr.Rakba_crop_asinchit),0)*isnull(ty.MaxYieldUnIrrigated,0))  as Upaj From Farmer_LandRecordDescription as lr left join [TehsilYield] as ty on lr.Tehsil_ID=ty.TehsilCode Where lr.Farmer_Id='" + ddl_Farmer.SelectedItem.Value.Trim() + "' and lr.crpcode ='1' Group By ty.MaxYieldIrrigated,ty.MaxYieldUnIrrigated";
                query = "Select (Select isnull(ty.MaxYieldIrrigated,0)From Farmer_LandRecordDescription as lr left join [TehsilYield] as ty on lr.Tehsil_ID=ty.TehsilCode Where lr.Farmer_Id='" + lblfarmerid.Text.Trim() + "' and lr.crpcode ='1' Group By ty.MaxYieldIrrigated) as TehsilYield_Sinchit,(Select isnull(ty.MaxYieldUnIrrigated,0)  From Farmer_LandRecordDescription as lr left join [TehsilYield] as ty on lr.Tehsil_ID=ty.TehsilCode Where lr.Farmer_Id='" + lblfarmerid.Text.Trim() + "' and lr.crpcode ='1' Group By ty.MaxYieldUnIrrigated) as TehsilYield_Asinchit ";
                GetMax_TehsilYeild(query, lblfarmerid.Text.Trim());
            }



        }
        catch (Exception)
        {

        }
    }

    protected void ddl_Comodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    if (ddl_Farmer.Items.Count <= 0)
        //    {
        //        Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('किसान सुची उपलब्ध नहीं है'); </script> ");
        //        if (ddl_Comodity.Items.Count > 0)
        //        {
        //            ddl_Comodity.SelectedIndex = 0;
        //        }
        //        return;
        //    }
        //    if (ddl_Farmer.SelectedIndex == 0)
        //    {
        //        Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('किसान का चयन करे'); </script> ");
        //        if (ddl_Comodity.Items.Count > 0)
        //        {
        //            ddl_Comodity.SelectedIndex = 0;
        //        }
        //        return;
        //    }

        //    //GetCropSinchit_Asinchit(ddl_Farmer.SelectedItem.Value);
        //    if (ddl_Comodity.Items.Count <= 0)
        //    {
        //        Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('अनाज/फसल की सुची उपलब्ध नहीं है'); </script> ");
        //        return;
        //    }
        //    if (ddl_Comodity.SelectedIndex == 0)
        //    {
        //        Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('अनाज/फसल का चयन करे'); </script> ");
        //        return;
        //    }
        //    if (con.State == ConnectionState.Closed)
        //    {
        //        con.Open();
        //    }
        //    DataSet dsC = new DataSet();
        //    SqlDataAdapter daC = new SqlDataAdapter("Select isnull(MSPRate,0) as Samarthan,ISNULL(CentralBonus,0) as Kendriya,ISNULL(StateBonus,0) as Rajya From  [dbo].[CommodityRate] Where CommodityId='" + ddl_Comodity.SelectedItem.Value.Trim() + "'", con);
        //    daC.Fill(dsC);
        //    if (dsC.Tables[0].Rows.Count > 0)
        //    {
        //        txtSamarthan_muly.Text = Convert.ToString(dsC.Tables[0].Rows[0]["Samarthan"]);
        //        txt_Rajya_Bonus.Text = Convert.ToString(dsC.Tables[0].Rows[0]["Rajya"]);
        //        txt_kendraBonus.Text = Convert.ToString(dsC.Tables[0].Rows[0]["Kendriya"]);
        //    }
        //    else
        //    {
        //        txtSamarthan_muly.Text = "0";
        //        txt_Rajya_Bonus.Text = "0";
        //        txt_kendraBonus.Text = "0";


        //    }
        //    if (con.State == ConnectionState.Open)
        //    {
        //        con.Close();
        //    }
        //    string query = "";
        //    //query = "Select (isnull(lr.Rakba_crop_sinchit,0)*isnull(ty.MaxYeildIrrigated_Paddy,0)+isnull(lr.Rakba_crop_asinchit,0)*isnull(ty.MaxYeildUnIrrigated_Paddy,0))  as Upaj From Farmer_LandRecordDescription as lr left join [TehsilYield] as ty on lr.Tehsil_ID=ty.TehsilCode Where lr.Farmer_Id='" + ddl_Farmer.SelectedItem.Value.Trim() + "' and lr.crpcode ='2'";

        //    //GetMax_TehsilYeild(query);

        //    if (ddl_Comodity.SelectedItem.Value.Trim() == "1")
        //    {
        //        //query = "Select (isnull(lr.Rakba_crop_sinchit,0)*isnull(ty.MaxYeildIrrigated_Paddy,0)+isnull(lr.Rakba_crop_asinchit,0)*isnull(ty.MaxYeildUnIrrigated_Paddy,0))  as Upaj From Farmer_LandRecordDescription as lr left join [TehsilYield] as ty on lr.Tehsil_ID=ty.TehsilCode Where lr.Farmer_Id='" + ddl_Farmer.SelectedItem.Value.Trim() + "' and lr.crpcode ='2'";
        //        query = "Select (isnull(SUM(lr.Rakba_crop_sinchit),0)*isnull(ty.MaxYieldIrrigated,0)+isnull(SUM(lr.Rakba_crop_asinchit),0)*isnull(ty.MaxYieldUnIrrigated,0))  as Upaj From Farmer_LandRecordDescription as lr left join [TehsilYield] as ty on lr.Tehsil_ID=ty.TehsilCode Where lr.Farmer_Id='" + ddl_Farmer.SelectedItem.Value.Trim() + "' and lr.crpcode ='1' Group By ty.MaxYieldIrrigated,ty.MaxYieldUnIrrigated";
        //        GetMax_TehsilYeild(query,ddl_Farmer.SelectedItem.Value.Trim());
        //    }



        //}
        //catch (Exception)
        //{

        //}
    }
    protected void txt_Amount_Against_SocLoan_TextChanged(object sender, EventArgs e)
    {

    }
    protected void chb_Confirm_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chb_Confirm.Checked == true)
            {
                #region CheckValue

                //if (ddl_Tehsil.Items.Count <= 0)
                //{
                //    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('तहसील की सुची उपलब्ध नहीं है'); </script> ");
                //    pnlConfirm_save.Enabled = false;
                //    Panel_Prapti_afterpass.Enabled = true;
                //    chb_Confirm.Checked = false;
                //    return;
                //}
                //if (ddl_Tehsil.SelectedIndex == 0)
                //{
                //    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('तहसील का चयन करे'); </script> ");
                //    pnlConfirm_save.Enabled = false;
                //    Panel_Prapti_afterpass.Enabled = true;
                //    chb_Confirm.Checked = false;
                //    return;
                //}
                //if (ddl_Village.Items.Count <= 0)
                //{
                //    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('गाँव  की सुची उपलब्ध नहीं है'); </script> ");
                //    pnlConfirm_save.Enabled = false;
                //    Panel_Prapti_afterpass.Enabled = true;
                //    chb_Confirm.Checked = false;
                //    return;
                //}
                //if (ddl_Village.SelectedIndex == 0)
                //{
                //    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('गाँव का चयन करे'); </script> ");
                //    pnlConfirm_save.Enabled = false;
                //    Panel_Prapti_afterpass.Enabled = true;
                //    chb_Confirm.Checked = false;
                //    return;
                //}
                if (lblfarmerid.Text.Trim() == "")
                {
                    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('किसान की सुची उपलब्ध नहीं है'); </script> ");
                    pnlConfirm_save.Enabled = false;
                    Panel_Prapti_afterpass.Enabled = true;
                    chb_Confirm.Checked = false;
                    return;
                }
                if (lblfarmerid.Text.Trim() == "")
                {
                    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('किसान का चयन करे'); </script> ");
                    pnlConfirm_save.Enabled = false;
                    Panel_Prapti_afterpass.Enabled = true;
                    chb_Confirm.Checked = false;
                    return;
                }
                GetCropSinchit_Asinchit(lblfarmerid.Text.Trim(), "Drop");
                if (txtTolPatrak.Text.Trim() == String.Empty)
                {
                    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('तोल पत्रक क्र. प्रविष्ट करे'); </script> ");
                    pnlConfirm_save.Enabled = false;
                    Panel_Prapti_afterpass.Enabled = true;
                    chb_Confirm.Checked = false;
                    return;
                }
                if (GridView2.Rows.Count <= 0)
                {
                    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('किसान द्वारा लायी गयी अनाज की मात्रा जोड़े'); </script> ");
                    pnlConfirm_save.Enabled = false;
                    Panel_Prapti_afterpass.Enabled = true;
                    chb_Confirm.Checked = false;
                    return;

                }
                if (txt_Amount_Against_SocLoan.Text.Trim() == String.Empty)
                {
                    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('सोसाईटी ऋण के विरूद्ध वसूली राशि  खाली ना छोड़े यदि ऋण नहीं है तो 0 प्रविष्ट करे'); </script> ");
                    pnlConfirm_save.Enabled = false;
                    Panel_Prapti_afterpass.Enabled = true;
                    chb_Confirm.Checked = false;

                    return;
                }
                if (txt_Amt_AgainstDCCBLoan.Text.Trim() == String.Empty)
                {
                    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('बैंक ऋण के विरूद्ध वसूली राशि  खाली ना छोड़े यदि ऋण नहीं है तो 0 प्रविष्ट करे'); </script> ");
                    pnlConfirm_save.Enabled = false;
                    Panel_Prapti_afterpass.Enabled = true;
                    chb_Confirm.Checked = false;
                    return;
                }
                if (txt_Amount_AgainstIrrigationLoan.Text.Trim() == String.Empty)
                {
                    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('सिंचाई विभाग ऋण के विरूद्ध वसूली राशि  खाली ना छोड़े यदि ऋण नहीं है तो 0 प्रविष्ट करे'); </script> ");
                    pnlConfirm_save.Enabled = false;
                    Panel_Prapti_afterpass.Enabled = true;
                    chb_Confirm.Checked = false;
                    return;
                }

                try
                {
                    int.Parse(txtTolPatrak.Text.Trim());
                }
                catch
                {
                    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('तोलपत्रक क्रमांक सही प्रविष्ट करे'); </script> ");
                    pnlConfirm_save.Enabled = false;
                    Panel_Prapti_afterpass.Enabled = true;
                    chb_Confirm.Checked = false;
                    return;
                }
                try
                {
                    decimal.Parse(txt_Amount_Against_SocLoan.Text.Trim());
                }
                catch
                {
                    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('समिति ऋण की राशि सही प्रविष्ट करे'); </script> ");
                    pnlConfirm_save.Enabled = false;
                    Panel_Prapti_afterpass.Enabled = true;
                    chb_Confirm.Checked = false;
                    return;
                }
                try
                {
                    decimal.Parse(txt_Amt_AgainstDCCBLoan.Text.Trim());
                }
                catch
                {
                    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('डी.सी.सी.बी. ऋण की राशि सही प्रविष्ट करे'); </script> ");
                    pnlConfirm_save.Enabled = false;
                    Panel_Prapti_afterpass.Enabled = true;
                    chb_Confirm.Checked = false;
                    return;
                }
                try
                {
                    decimal.Parse(txt_Amount_AgainstIrrigationLoan.Text.Trim());
                }
                catch
                {
                    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('सिचाई विभाग के बकाया  ऋण की राशि सही प्रविष्ट करे'); </script> ");
                    pnlConfirm_save.Enabled = false;
                    Panel_Prapti_afterpass.Enabled = true;
                    chb_Confirm.Checked = false;
                    return;
                }




                if (Convert.ToDecimal(txt_Amount_Against_SocLoan.Text.Trim()) > Convert.ToDecimal(txtSocLoan.Text.Trim()))
                {
                    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('सोसाइटी ऋण के विरूद्ध वसूली गयी राशि , बकाया ऋण से ज्यादा नहीं हो सकती'); </script> ");
                    pnlConfirm_save.Enabled = false;
                    Panel_Prapti_afterpass.Enabled = true;
                    chb_Confirm.Checked = false;
                    return;
                }
                if (Convert.ToDecimal(txt_Amt_AgainstDCCBLoan.Text.Trim()) > Convert.ToDecimal(txtDCCBLoan.Text.Trim()))
                {
                    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('जिला केन्द्रीय सहकारी बैंक ऋण के विरूद्ध वसूली गयी राशि , बकाया ऋण से ज्यादा नहीं हो सकती'); </script> ");
                    pnlConfirm_save.Enabled = false;
                    Panel_Prapti_afterpass.Enabled = true;
                    chb_Confirm.Checked = false;
                    return;
                }
                if (Convert.ToDecimal(txt_Amount_AgainstIrrigationLoan.Text.Trim()) > Convert.ToDecimal(txtIrrigationLoan.Text.Trim()))
                {
                    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('सिंचाई विभाग ऋण के विरूद्ध वसूली गयी राशि , बकाया ऋण से ज्यादा नहीं हो सकती'); </script> ");
                    pnlConfirm_save.Enabled = false;
                    Panel_Prapti_afterpass.Enabled = true;
                    chb_Confirm.Checked = false;
                    return;
                }
                if (txtPraptiDate.Text.Trim() == String.Empty)
                {
                    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('प्राप्ति दिंनाक का चयन करे'); </script> ");
                    pnlConfirm_save.Enabled = false;
                    Panel_Prapti_afterpass.Enabled = true;
                    chb_Confirm.Checked = false;
                    return;
                }
                string[] arr1 = new string[GridView2.Rows.Count];

                int c1 = 1;

                for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
                {
                    if (Convert.ToString(GridView2.Rows[i].Cells[5].Text).Trim() == "1")
                    {
                        arr1[i] = Convert.ToString(c1);
                        c1++;
                    }
                }


                foreach (string s in arr1)
                {
                    if (Convert.ToInt16(s) > 1)
                    {
                        Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('एक फसल को दो बार नहीं जोड़ सकते'); </script> ");
                        pnlConfirm_save.Enabled = false;
                        Panel_Prapti_afterpass.Enabled = true;
                        chb_Confirm.Checked = false;
                        return;
                    }
                }


                #endregion


                //Check Amount Against SocLOan
                decimal Final_loanAmount = 0;
                decimal Amt_soc_Loan = 0;
                decimal Amt_DCCBloan = 0;
                decimal Amt_IrrLoan = 0;

                Amt_soc_Loan = Convert.ToDecimal(txt_Amount_Against_SocLoan.Text.Trim());
                Amt_DCCBloan = Convert.ToDecimal(txt_Amt_AgainstDCCBLoan.Text.Trim());
                Amt_IrrLoan = Convert.ToDecimal(txt_Amount_AgainstIrrigationLoan.Text.Trim());
                Final_loanAmount = Amt_soc_Loan + Amt_DCCBloan + Amt_IrrLoan;

                if (Convert.ToDecimal(txtShudhBhugtan_YogyaRashi.Text.Trim()) >= Final_loanAmount)
                {
                    txtShudhBhugtan.Text = Convert.ToString(Convert.ToDecimal(txtShudhBhugtan_YogyaRashi.Text.Trim()) - Final_loanAmount);

                }
                if (Final_loanAmount > Convert.ToDecimal(txtShudhBhugtan_YogyaRashi.Text.Trim()))
                {
                    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('ऋण द्वारा वसूली गयी राशि शुद्ध  भुगतान योग्य राशि से ज्यादा नहीं हो सकती'); </script> ");
                    pnlConfirm_save.Enabled = false;
                    Panel_Prapti_afterpass.Enabled = true;

                    chb_Confirm.Checked = false;
                    txt_Amount_Against_SocLoan.Text = "0";
                    txt_Amt_AgainstDCCBLoan.Text = "0";
                    txt_Amount_AgainstIrrigationLoan.Text = "0";

                    return;
                }

                Panel_Prapti_afterpass.Enabled = false;

                pnlConfirm_save.Enabled = true;

            }
            if (chb_Confirm.Checked == false)
            {
                if (lblfarmerid.Text.Trim() != "")
                {
                    if (lblfarmerid.Text.Trim() != "")
                    {
                        GetCropSinchit_Asinchit(lblfarmerid.Text.Trim(), "Drop");
                        GetBechaAnaj_AajTak(lblfarmerid.Text.Trim(), Session["District_Code"].ToString(), "Drop");
                    }
                }
                txtShudhBhugtan.Text = txtShudhBhugtan_YogyaRashi.Text.Trim();
                pnlConfirm_save.Enabled = false;
                Panel_Prapti_afterpass.Enabled = true;
            }
        }
        catch (Exception)
        { }
    }

    private string base64Encode(string sData)
    {
        try
        {
            byte[] encData_byte = new byte[sData.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(sData);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }
        catch (Exception ex)
        {
            throw new Exception("Error in base64Encode" + ex.Message);
        }
    }

    public string base64Decode(string sData)
    {
        try
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(sData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char); return result;
        }
        catch (Exception ex)
        {
            throw new Exception("Error in base64Encode" + ex.Message);
        }
    }

    
    protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Redirect("~/WHP14/Login1.aspx");
        }
        catch (Exception)
        { }
    }
    private bool DoLogin(string qry, string user)
    {
        bool res = false;
        if (Session["salt"] == null)
        {
            Response.Redirect("~/WHP14/Login1.aspx");
        }
        string strpd = "";
        lobj = new loginWhtMP(ComObj);
        DataSet ds = lobj.selectAny(qry);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                string pwd = dr["Password"].ToString();
                strpd = pwd;
                string password = txtpwd.Text;
                txtpwd.Text = "";
                string hpwd = CreatePasswordHash(strpd.ToLower());
                if (password == lobj.Password || password == hpwd.ToLower())
                {
                    string tx_hashedPasswordAndSalt = CreatePasswordHash(user.Trim().ToLower()).ToLower();
                    string tx_hashedPasswordAndSalt1 = CreatePasswordHash(Session["salt"].ToString().Trim() + tx_hashedPasswordAndSalt.Trim()).ToLower();



                    res = true;



                }
            }
        }
        return res;
    }
    private static string CreateSalt(int size)
    {
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        byte[] buff = new byte[size + 1];
        rng.GetBytes(buff);
        return Convert.ToBase64String(buff);
    }

    public string CreatePasswordHash(string dist)
    {
        string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(dist.ToString().Trim(), "MD5");
        return hashedPwd;
    }
    protected void txtSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtpwd.Text.Trim() == String.Empty)
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('पासवर्ड प्रविष्ट करे'); </script> ");
                return;
            }
            if (Session["User"].ToString() == "Otheruser")
            {
                if (Session["UserName"].ToString() == "PurchaseCenter")
                {

                    string username = "";
                    string qry = "";


                    username = Session["UserName"].ToString();

                    qry = "Select UserName,Password From UserPassword_control Where PassWordStatus='Yes' and UserName='frm_Wheat14_Prapti'";
                    bool res = DoLogin(qry, username);
                    if (res)
                    {

                        //pnl_CheckPass.Visible = false;
                        //pnl_pass.Visible = true;
                        Panel_Prapti_afterpass.Visible = true;
                        pnlConfirm_save.Visible = true;
                        pnl_CheckPass_Kharidi.Visible = false;
                        Panel_Prapti_afterpass.Enabled = true;
                        pnlConfirm_save.Enabled = false;
                        chb_Confirm.Checked = false;
                        chb_Confirm.Visible = true;
                        chb_Confirm.Enabled = true;
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('आपका पासवर्ड गलत है'); </script> ");
                        Panel_Prapti_afterpass.Visible = false;
                        pnlConfirm_save.Visible = false;
                        pnl_CheckPass_Kharidi.Visible = true;

                        return;
                    }



                }
            }
        }
        catch (Exception)
        {
            Panel_Prapti_afterpass.Visible = false;
            pnlConfirm_save.Visible = false;
            pnl_CheckPass_Kharidi.Visible = true;
        }
    }
    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            DateTime d;
            //args.IsValid =
            if (!DateTime.TryParseExact(args.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('dd/MM/yyyy फोर्मेट मे ही दिनांक चुने |'); </script> ");
                txtPraptiDate.Text = "";
                return;
            }

        }
        catch (Exception)
        { }
    }



    protected void btnSearchFarmer_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtFarmerCode.Text.Trim() == String.Empty)
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('किसान कोड प्रविष्ट करे |'); </script> ");
                return;

            }
            btnPawati.Visible = false;
            GridView2.DataSource = null;
            GridView2.DataBind();

            GridView_CropSinchit_Asinchit.DataSource = null;
            GridView_CropSinchit_Asinchit.DataBind();
            GridView_AbhiTak_Becha.DataSource = null;
            GridView_AbhiTak_Becha.DataBind();
            txtKisaanKulRakba.Text = "";
            txtSikamiRakba.Text = "";
            txtOwnRakba.Text = "";

            //ddl_Comodity.SelectedIndex = 0;
            txtBoriSankhya.Text = "";
            txtMatra.Text = "";
            //txtSamarthan_muly.Text = "";
            //txt_Rajya_Bonus.Text = "";

            txtTolPatrak.Text = "";
            txtAnajKulMatra.Text = "";
            txtShudhBhugtan_YogyaRashi.Text = "";

            txtSocLoan.Text = "";
            txtDCCBLoan.Text = "";
            txtIrrigationLoan.Text = "";
            txt_Amount_Against_SocLoan.Text = "";
            txt_Amt_AgainstDCCBLoan.Text = "";
            txt_Amount_AgainstIrrigationLoan.Text = "";
            txtShudhBhugtan.Text = "";
            Session["dt"] = null;
            CreateColoumnIn_dt();

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd_chkFR = new SqlCommand("Select Count(*) From FarmerRegistration Where District_Id='" + Session["District_Code"].ToString() + "' and  Procured_SocietyID='" + Session["Society_Id"].ToString() + "' and Farmer_Id='" + txtFarmerCode.Text.Trim() + "' ", con);
            int cnt = (int)cmd_chkFR.ExecuteScalar();
            if (cnt <= 0)
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('किसान की जानकारी उपलब्ध नहीं है |'); </script> ");

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return;
            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            GetFR_BasicDetail(txtFarmerCode.Text.Trim(), Session["District_Code"].ToString(), Session["Society_Id"].ToString());
            CommIndex();
            GetFarmer_Land_Record(lblfarmerid.Text, "Sikami", "Own", Session["District_Code"].ToString(), Session["Society_Id"].ToString());
            //GetSikamiRakba(txtFarmerCode.Text.Trim(), "Sikami", "Button");
            //GetKulRakba(txtFarmerCode.Text.Trim(), "Button");
            //GetOwnRakba(txtFarmerCode.Text.Trim(), "Own", "Button");
            GetCropSinchit_Asinchit(lblfarmerid.Text.Trim(), "Button");
            GetBechaAnaj_AajTak(lblfarmerid.Text.Trim(), Session["District_Code"].ToString(), "Button");
            ////GetMax_TehsilYeild(ddl_Farmer.SelectedItem.Value);
            ////GetBankLoan_LoanAmt_Info(ddl_Farmer.SelectedItem.Value, Session["District_Code"].ToString());
            //GetSocLoan(txtFarmerCode.Text.Trim(), Session["District_Code"].ToString(), Session["Society_Id"].ToString(), "Button");
            //GetDCCBLoan(txtFarmerCode.Text.Trim(), Session["District_Code"].ToString(), Session["Society_Id"].ToString(), "Button");
            //GetIRRLoan(txtFarmerCode.Text.Trim(), Session["District_Code"].ToString(), Session["Society_Id"].ToString(), "Button");
        }
        catch (Exception ex)
        { }
    }
    protected void GetFR_BasicDetail(string frcode, string dcode, string soccode)
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlDataAdapter da_fr = new SqlDataAdapter("Select Tehsil_Id,Village_Id,VillageName,Farmer_Id,FarmerName,FatherHusName,Procured_SocietyID From FarmerRegistration Where District_Id='" + dcode.Trim() + "' and Procured_SocietyID='" + soccode.Trim() + "' and Farmer_Id='" + frcode.Trim() + "'", con);
            DataSet ds_fr = new DataSet();
            da_fr.Fill(ds_fr);
            if (ds_fr.Tables[0].Rows.Count > 0)
            {
                //string tehsilcode = "";
                //tehsilcode = Convert.ToString(ds_fr.Tables[0].Rows[0]["Tehsil_Id"]);
                //if (tehsilcode.Trim() != String.Empty)
                //{
                //    ddl_Tehsil.SelectedValue = tehsilcode;
                //}
                //string villcode = "";
                //string Villname = "";
                //Fill_ddl_Village(tehsilcode);
                //villcode = Convert.ToString(ds_fr.Tables[0].Rows[0]["Village_Id"]);
                //Villname = Convert.ToString(ds_fr.Tables[0].Rows[0]["VillageName"]);
                //if (villcode.Trim() != String.Empty)
                //{
                //    ddl_Village.SelectedValue = villcode;
                //}
                //FIll_ddl_Farmer(Villname, tehsilcode);
                lblfrmsocityid.Text = "";
                string Procured_SocietyID = "";
                lblfarmerid.Text = Convert.ToString(ds_fr.Tables[0].Rows[0]["Farmer_Id"]);
                Procured_SocietyID = Convert.ToString(ds_fr.Tables[0].Rows[0]["Procured_SocietyID"]);
                if (Procured_SocietyID.Trim() != String.Empty)
                {
                    lblfrmsocityid.Text = Procured_SocietyID;
                }
            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        catch (Exception ex)
        { }
    }
    ///display message fuction//
    public void MsgScriptwith(string msgsc, string url)
    {
        Page page = HttpContext.Current.Handler as Page;
        string strScript = "<script>";
        strScript += "alert('" + msgsc + "');";
        strScript += "window.location='" + url + "';";
        strScript += "</script>";
        page.ClientScript.RegisterStartupScript(this.GetType(), "Startup", strScript);
    }
    public void GetDateFuction()
    {
        try
        {
            ddldate.Items.Clear();
            DateTime dttoday = System.DateTime.Now;
            DateTime dtfrom = System.DateTime.Now.AddDays(-4);
            for (DateTime x = dtfrom; x <= dttoday; x = x.AddDays(1))          
            {
                ddldate.Items.Add(x.Date.ToString("dd/MM/yyyy"));
               
            }
            ddldate.SelectedValue = System.DateTime.Now.Date.ToString("dd/MM/yyyy");
            
        }
        catch (Exception ex)
        { }
    }



    protected void btnPawati_Click(object sender, EventArgs e)
    {
        try
        {
            string fid = base64Encode(lblfarmerid.Text.Trim());
            string RCVID = base64Encode(lblb.Text.Trim());
            Page.ClientScript.RegisterStartupScript(
    this.GetType(), "OpenWindow", "window.open('Procurement_Report/frm_PrintAnajPawati_htmlPrint.aspx?RCVID=" + RCVID + "&fid=" + fid + "','_newtab');", true);
        }
        catch (Exception ex)
        { }
    }
}
