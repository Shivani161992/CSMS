using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Data;
using DataAccess;
using System.Security.Cryptography;
using System.Data.SqlClient;

public partial class mpproc_Masters_FarmerRegistration : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_mpproc"].ToString());
    SqlCommand cmd = new SqlCommand();
    protected Common ComObj = null, cmn = null;
    public string qry = "";
    RationCardType rctObj = null;
    Districts distObj = null;
    DataReader objDr = null;
    SqlString SqlObj = null;
    string dist = "";

    DataTable Dt1 = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["pcId"] != null)
        {
            dist = Session["dist_id"].ToString();
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString_mpproc"].ToString());

            txtFarmerName.Attributes.Add("onkeypress", "return CheckIsChar(this)");
            txt_FatherName.Attributes.Add("onkeypress", "return CheckIsChar(this)");

        
            txtHalkaNo.Attributes.Add("onkeypress", "return CheckIsNum(event,this)");
            txt_mobileno.Attributes.Add("onkeypress", "return CheckIsNum(event,this)");
            txt_UIDAadhaar.Attributes.Add("onkeypress", "return CheckIsNum(event,this)");
            txt_BnkAccno.Attributes.Add("onkeypress", "return CheckIsNum(event,this)");




            txt_Grid_PrcQty.Attributes.Add("onblur", "extractNumber(this,2,false)");
            txt_Grid_PrcQty.Attributes.Add("onkeyup", "extractNumber(this,2,false)");
            txt_Grid_PrcQty.Attributes.Add("onkeypress", "return blockNonNumbers(this, event, true, false)");
            txt_Grid_PrcQty.Style["text-align"] = "right";


            txt_Grid_AsinchitQty.Attributes.Add("onblur", "extractNumber(this,2,false)");
            txt_Grid_AsinchitQty.Attributes.Add("onkeyup", "extractNumber(this,2,false)");
            txt_Grid_AsinchitQty.Attributes.Add("onkeypress", "return blockNonNumbers(this, event, true, false)");
            txt_Grid_AsinchitQty.Style["text-align"] = "right";

            txt_Grid_AchinchitQty.Attributes.Add("onblur", "extractNumber(this,2,false)");
            txt_Grid_AchinchitQty.Attributes.Add("onkeyup", "extractNumber(this,2,false)");
            txt_Grid_AchinchitQty.Attributes.Add("onkeypress", "return blockNonNumbers(this, event, true, false)");
            txt_Grid_AchinchitQty.Style["text-align"] = "right";


            txt_ColMaxQty.Attributes.Add("onblur", "extractNumber(this,2,false)");
            txt_ColMaxQty.Attributes.Add("onkeyup", "extractNumber(this,2,false)");
            txt_ColMaxQty.Attributes.Add("onkeypress", "return blockNonNumbers(this, event, true, false)");
            txt_ColMaxQty.Style["text-align"] = "right";


            txt_Grid_RKBSinchit.Attributes.Add("onblur", "extractNumber(this,2,false)");
            txt_Grid_RKBSinchit.Attributes.Add("onkeyup", "extractNumber(this,2,false)");
            txt_Grid_RKBSinchit.Attributes.Add("onkeypress", "return blockNonNumbers(this, event, true, false)");
            txt_Grid_RKBSinchit.Style["text-align"] = "right";

            txt_Grid_RKBASinchit.Attributes.Add("onblur", "extractNumber(this,2,false)");
            txt_Grid_RKBASinchit.Attributes.Add("onkeyup", "extractNumber(this,2,false)");
            txt_Grid_RKBASinchit.Attributes.Add("onkeypress", "return blockNonNumbers(this, event, true, false)");
            txt_Grid_RKBASinchit.Style["text-align"] = "right";

            





            
            if (!IsPostBack)
            {
               
                //GetDist();
                GetDistLRMP();
                GetTehsil();
                GetCategory();
                GetLandType();
                getSamity();
                Session["dt1"] = null;

                btn_LandReocord.Enabled = false;

            }

            //if (Session["upadte"] == "FarmerUpdate")
            //{

            //    getFarmerInoForUpdate();
            
            
            
            //}




        }
        else
        {


            Response.Redirect("../frmLogin.aspx");

      
        }

    }

    private void getFarmerInoForUpdate()
    {
        btn_update.Visible = true;

        string sid = Session["FarmerFID"].ToString();
       
        //DDL_Tah.Items.FindByValue(Session["FarTID"].ToString()).Selected = true;

        //SqlObj = new SqlString(ComObj);
        //string strFarmer = "SELECT  FRG.DistNo_LRMP DistID_LRMP, FRG.Tehsile_ID as TehsileID,LRTM.Tehsilname as Tehsilname,LRVM.Villagename as Villagename, FRG.Village_Code as Vcode, LRVM.Villagename as village ,FRG.FarmerId as FarmerId ,FRG.FarmerName as FarmerName,FRG.FatherHusName as FarmerFatHUS, FRG.Gram_Panchayat as gp, FRG.PatwariHalkaNo, LRTM.Tehsilname as Tehsil, LRDM .Distname as Distname,FRG.Mobileno Mobileno, FRG.Category Category , FRG.RinPustikaNo as RinPustikaNo, FRG.Farmer_EID_UID_No as EIDUIDNo, FRG.Farmer_BankName as Bankname, FRG.Farmer_BankAccountNo as AccNo, FRG.Procured_SocietyID, FRG.Procured_Dist_ID,  FRG.Procured_Place as PrcPlace , FRG.Col_MaxQty,Category.Category ,CONVERT(VARCHAR(15), FRG.CropDep_Date, 106) as CropDepDate FROM  FarmerRegistration  FRG left join LR_VillageMaster  LRVM on  FRG.DistNo_LRMP=LRVM.Distno and FRG.Tehsile_ID=LRVM.Tehsilno and  FRG.Village_Code=LRVM.Villageno left join LR_TehsilMaster  LRTM  on FRG.Tehsile_ID =LRTM.Distno and FRG.Tehsile_ID=LRTM.Tehsilno left join LR_DistrictMaster LRDM on FRG.DistNo_LRMP =LRDM.Distno left join  Category  on Category.Category_ID=FRG.Category where  FRG.FarmerId= '"+sid+"'";
        //DataSet ds = SqlObj.selectAny(strFarmer);

        //if (ds != null)
        //{
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {

        //        DDL_Tah.SelectedItem.Text = ds.Tables[0].Rows[0]["Tehsilname"].ToString();
        //        DDL_Village.SelectedItem.Text = ds.Tables[0].Rows[0]["Villagename"].ToString();
        //        txt_GramPanchayat.Text = ds.Tables[0].Rows[0]["gp"].ToString();

        //    }
        //}

        string strsql = "SELECT  FRG.DistNo_LRMP DistID_LRMP, FRG.Tehsile_ID as TehsileID,LRTM.Tehsilname as Tehsilname,LRVM.Villagename as Villagename, FRG.Village_Code as Vcode, LRVM.Villagename as village ,FRG.FarmerId as FarmerId ,FRG.FarmerName as FarmerName,FRG.FatherHusName as FarmerFatHUS, FRG.Gram_Panchayat as gp, FRG.PatwariHalkaNo as PhlNo, LRTM.Tehsilname as Tehsil, LRDM .Distname as Distname,FRG.Mobileno Mobileno, FRG.Category Category , FRG.RinPustikaNo as RinPustikaNo, FRG.Farmer_EID_UID_No as EIDUIDNo, FRG.Farmer_BankName as Bankname, FRG.Farmer_BankAccountNo as AccNo, FRG.Procured_SocietyID, FRG.Procured_Dist_ID,  FRG.Procured_Place as PrcPlace , FRG.Col_MaxQty,Category.Category ,CONVERT(VARCHAR(15), FRG.CropDep_Date, 106) as CropDepDate FROM  FarmerRegistration  FRG left join LR_VillageMaster  LRVM on  FRG.DistNo_LRMP=LRVM.Distno and FRG.Tehsile_ID=LRVM.Tehsilno and  FRG.Village_Code=LRVM.Villageno left join LR_TehsilMaster  LRTM  on FRG.Tehsile_ID =LRTM.Distno and FRG.Tehsile_ID=LRTM.Tehsilno left join LR_DistrictMaster LRDM on FRG.DistNo_LRMP =LRDM.Distno left join  Category  on Category.Category_ID=FRG.Category where  FRG.FarmerId= '" + sid + "'";
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        cmd = new SqlCommand(strsql, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds, "farInfo");


        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDL_Tah.SelectedValue = ds.Tables[0].Rows[0]["TehsileID"].ToString();
                GetVillage();
                GetVillageForGrid();
                getCrop();
                DDL_Village.SelectedValue = ds.Tables[0].Rows[0]["Vcode"].ToString();
                txt_GramPanchayat.Text = ds.Tables[0].Rows[0]["gp"].ToString();
                txtHalkaNo.Text = ds.Tables[0].Rows[0]["PhlNo"].ToString();
                txtFarmerName.Text = ds.Tables[0].Rows[0]["FarmerName"].ToString();
                txt_FatherName.Text = ds.Tables[0].Rows[0]["FarmerFatHUS"].ToString();
                txt_mobileno.Text = ds.Tables[0].Rows[0]["Mobileno"].ToString();
                ddl_Category.SelectedValue = ds.Tables[0].Rows[0]["Category"].ToString();
                txt_RinPustikanumber.Text = ds.Tables[0].Rows[0]["RinPustikaNo"].ToString();
                txt_UIDAadhaar.Text = ds.Tables[0].Rows[0]["EIDUIDNo"].ToString();
                txt_bankname.Text = ds.Tables[0].Rows[0]["Bankname"].ToString();
                txt_BnkAccno.Text = ds.Tables[0].Rows[0]["AccNo"].ToString();

                string sqlType = "SELECT  *  from Farmer_LandRecordDescription where  Farmer_ID '" + sid + "'";

                cmd = new SqlCommand(sqlType, con);
                SqlDataAdapter daLR = new SqlDataAdapter(cmd);
                DataSet dsLR = new DataSet();
                daLR.Fill(dsLR, "farLand");

                if (dsLR != null)
                {
                    if (dsLR.Tables[0].Rows.Count > 0)
                    {

                        ddl_LandType.SelectedValue = dsLR.Tables[0].Rows[0]["LandType"].ToString();
                       



                    }
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }




                //DDL_Tah.SelectedItem.Value = Session["FarTID"].ToString();


            }
        }
    }

    private void GetDistLRMP()
    {
        try
        {
            SqlObj = new SqlString(ComObj);
            string strSql = "select * from LR_DistrictMaster where Distno in(select  DistNo_LRMP from DistrictMaster where DistrictCode= '"+dist+"')";
            DataSet ds = SqlObj.selectAny(strSql);
           if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    DDL_Dist.DataSource = ds.Tables[0];
                    DDL_Dist.DataTextField = "Distname";
                    DDL_Dist.DataValueField = "Distno";
                    DDL_Dist.DataBind();
                  
                   
                    DDL_Dist.Enabled = false;

                    //for ddl_DistPrc
                    ddl_DistPrc.DataSource = ds.Tables[0];
                    ddl_DistPrc.DataTextField = "Distname";
                    ddl_DistPrc.DataValueField = "Distno";
                    ddl_DistPrc.DataBind();

                    ddl_DistPrc.Enabled = false;

                }
        }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");

        }
       
    }

    private void getSamity()
    {

        try
        {
            SqlObj = new SqlString(ComObj);
            string str = "SELECT * FROM PurchaseCenterMaster,MarketingSeasonMaster where PurchaseCenterMaster.DistrictId = '" + dist  + "'  and  PurchaseCenterMaster.MarkSeasId = '" + Session["Markseas_id"].ToString() + "' and cropyear ='" + Session["cropyear"].ToString() + "' and MarketingSeasonMaster.MarkSeasId = PurchaseCenterMaster.MarkSeasId order by PurchaseCenterName ";
            DataSet ds = SqlObj.selectAny(str);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl_samity.DataSource = ds.Tables[0];
                    ddl_samity.DataTextField = "PurchaseCenterName";
                    ddl_samity.DataValueField = "PcId";
                    ddl_samity.DataBind();

                }
            }
        }
        catch (Exception ex)
        {

            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");

        }


    }

    private void GetLandType()
    {
        try
        {
            SqlObj = new SqlString(ComObj);
            string strSql = "select * from LandType";
            DataSet ds = SqlObj.selectAny(strSql);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    ddl_LandType.DataSource = ds.Tables[0];
                    ddl_LandType.DataTextField = "LandtypeHindi";
                    ddl_LandType.DataValueField = "LandType";
                    ddl_LandType.DataBind();
                    ddl_LandType.Items.Insert(0, "--चुनिये--");
                }
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");

        }
    }

    private void GetCategory()
    {
        try
        {
            SqlObj = new SqlString(ComObj);
            string strSql = "select * from Category";
            DataSet ds = SqlObj.selectAny(strSql);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    ddl_Category.DataSource = ds.Tables[0];
                    ddl_Category.DataTextField = "Category";
                    ddl_Category.DataValueField = "Category_ID";
                    ddl_Category.DataBind();
                    ddl_Category.Items.Insert(0, "--चुनिये--");
                }
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");

        }
    }
    private void GetDist()
    {
        try
        {

            distObj = new Districts(ComObj);
            DataSet ds = distObj.selectmp(dist);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    DDL_Dist.DataSource = ds.Tables[0];
                    DDL_Dist.DataTextField = "DistrictName";
                    DDL_Dist.DataValueField = "DistrictCode";
                    DDL_Dist.DataBind();

                    DDL_Dist.Enabled = false;

                    //for ddl_DistPrc
                    ddl_DistPrc.DataSource = ds.Tables[0];
                    ddl_DistPrc.DataTextField = "DistrictName";
                    ddl_DistPrc.DataValueField = "DistrictCode";
                    ddl_DistPrc.DataBind();

                    ddl_DistPrc.Enabled = false;

                }
            }

        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");

        }
    }

  
    protected void DDL_Dist_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (DDL_Dist.SelectedItem.Text != "--चुनिये--")
        //{
        //    SqlObj = new SqlString(ComObj);
        //    string strSql = "select * from LR_TehsilMaster tm,DistrictMaster dm where dm.DistNo_LRMP=tm.Distno and tm.Distno='" + DDL_Dist.SelectedValue + "'";
        //    DataSet ds = SqlObj.selectAny(strSql);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {

        //        DDL_Tah.DataSource = ds.Tables[0];
        //        DDL_Tah.DataTextField = "Tehsilname";
        //        DDL_Tah.DataValueField = "Tehsilno";
        //        DDL_Tah.DataBind();
        //        DDL_Tah.Items.Insert(0, "--चुनिये--");
        //    }

        //}

    }

    protected void GetTehsil()
    {
        try
        {
            SqlObj = new SqlString(ComObj);
            string strSql = "select * from LR_TehsilMaster tm,DistrictMaster dm where dm.DistNo_LRMP=tm.Distno and dm.DistNo_LRMP='" + DDL_Dist.SelectedValue.ToString() + "'";
            DataSet ds = SqlObj.selectAny(strSql);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    DDL_Tah.DataSource = ds.Tables[0];
                    DDL_Tah.DataTextField = "Tehsilname";
                    DDL_Tah.DataValueField = "Tehsilno";
                    DDL_Tah.DataBind();
                    DDL_Tah.Items.Insert(0, "--चुनिये--");
                }
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");

        }
    }
   

    protected void DDL_Tah_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDL_Tah.SelectedItem.Text != "--चुनिये--")
        {

            GetVillage();
            GetVillageForGrid();
            getCrop();
        }
    }

    protected void getCrop()
    {
        try
        {
            SqlObj = new SqlString(ComObj);
            string strSql = "Select * from CropMaster";
            DataSet ds = SqlObj.selectAny(strSql);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    ddl_GridCrop.DataSource = ds.Tables[0];
                    ddl_GridCrop.DataTextField = "cropName";
                    ddl_GridCrop.DataValueField = "cropId";
                    ddl_GridCrop.DataBind();
                    ddl_GridCrop.Items.Insert(0, "--चुनिये--");
                }
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");

        }
    }

    private string GetDistNo_LRMP(string distcode)
    {
        string str = "";
        try
        {
            SqlObj = new SqlString(ComObj);
            string qry = "Select DistNo_LRMP from DistrictMaster where DistrictCode='" + distcode + "'";
            con.Open();
            cmd = new SqlCommand(qry, con);
            str = cmd.ExecuteScalar().ToString();
        }
        catch (Exception ex)
        {
            Label1.Visible = true;
            Label1.Text = ex.Message;
        }
        finally
        {
            con.Close();
        }
        return str;
    }

    private void GetVillage()
    {
        try
        {

            DDL_Village.DataSource = null;
            DDL_Village.DataBind();


            SqlObj = new SqlString(ComObj);
            string strsql = "  select * from LR_VillageMaster vm,DistrictMaster dm,LR_TehsilMaster tm  where dm.DistNo_LRMP=vm.Distno and tm.Tehsilno=vm.Tehsilno and tm.Distno=dm.DistNo_LRMP and dm.DistNo_LRMP='" + DDL_Dist.SelectedValue + "' and vm.Tehsilno='" + DDL_Tah.SelectedValue + "'  order by vm.Villagename ";
            DataSet ds = SqlObj.selectAny(strsql);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    DDL_Village.DataSource = ds.Tables[0];
                    DDL_Village.DataTextField = "Villagename";
                    DDL_Village.DataValueField = "Villageno";
                    DDL_Village.DataBind();
                    DDL_Village.Items.Insert(0, "--चुनिये--");

                }
            }
        }

        catch (Exception ex)
        {

            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
        }
    }
    private void GetVillageForGrid()
    {
        try
        {

            SqlObj = new SqlString(ComObj);
            string strsql = "select * from LR_VillageMaster vm,DistrictMaster dm,LR_TehsilMaster tm  where dm.DistNo_LRMP=vm.Distno and tm.Tehsilno=vm.Tehsilno and tm.Distno=dm.DistNo_LRMP and dm.DistNo_LRMP='" + DDL_Dist.SelectedValue + "' and vm.Tehsilno='" + DDL_Tah.SelectedValue + "'";
            DataSet ds = SqlObj.selectAny(strsql);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    ddl_GridVillage.DataSource = ds.Tables[0];
                    ddl_GridVillage.DataTextField = "Villagename";
                    ddl_GridVillage.DataValueField = "Villageno";
                    ddl_GridVillage.DataBind();
                    ddl_GridVillage.Items.Insert(0, "--चुनिये--");

                }
            }
        }

        catch (Exception ex)
        {

            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
        }
    }

    protected void DDL_Village_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fillGridFarDetail();

    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        try
        {

            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string gdnid = "";
            int gdnnum = 0;

            string DistNo_LRMP = DDL_Dist.SelectedValue.ToString();
            //string DistNo_LRMP = GetDistNo_LRMP(DDL_Dist.SelectedValue.ToString());
            if (txtFarmerName.Text != "" && txt_FatherName.Text != "" && txtHalkaNo.Text != "" & DDL_Tah.SelectedItem.Text != "--चुनिये--" && DDL_Village.SelectedItem.Text != "--चुनिये--")
            //if (txtFarmerName.Text != "" && txt_FatherName.Text != "" && txt_khasrano.Text != "" && txt_rctno.Text != "" && txt_RinPustikanumber.Text != "" && txtHalkaNo.Text != "" & DDL_Tah.SelectedItem.Text != "--चुनिये--" && DDL_Village.SelectedItem.Text != "--चुनिये--")
            {
               
               SqlObj = new SqlString(ComObj);
               string strsql = "select count(*) as count from FarmerRegistration where FarmerName='" + txtFarmerName.Text + "' and FatherHusName='" + txt_FatherName.Text + "' and DistNo_LRMP='" + DDL_Dist.SelectedValue.ToString() + "' and  Tehsile_ID ='" + DDL_Tah.SelectedValue.ToString() + "' and village_Code='" + DDL_Village.SelectedValue.ToString() + "'";

                DataSet ds = SqlObj.selectAny(strsql);
                if (Convert.ToInt32(ds.Tables[0].Rows[0]["count"]) == 0)
                {
                    objDr = new DataReader(ComObj);

                    //string qrey = "select Max(FarmerId) as FarmerId  from FarmerRegistration ";
                    string qrey = "select max(RIGHT('0000' + CAST(FarmerId as varchar), 4 ))  as FarmerId  from FarmerRegistration  where DistrictId='" + dist + "' and  Tehsile_ID='" + DDL_Tah.SelectedValue + "' and Village_Code='"+DDL_Village.SelectedValue+"'";
                    DataSet dsf = objDr.selectAny(qrey);
                    DataRow dr = dsf.Tables[0].Rows[0];
                    gdnid = dr["FarmerId"].ToString();
                    ComObj.CloseConnection();
                    if (gdnid == "")
                    {
                        gdnid = "0001";


                    }
                    else
                    {

                        gdnnum = Int32.Parse(gdnid.ToString());
                        gdnnum = gdnnum + 1;
                        gdnid = gdnnum.ToString();

                    }



                    string strFarmerID = Session["dist_id"].ToString() + DDL_Tah.SelectedValue.ToString() + DDL_Village.SelectedValue.ToString() + gdnid;
                  
                    try
                    {

                        if (GridLandRecord.Rows.Count > 0)
                        {


                            string strIn = "Insert into FarmerRegistration(DistrictId,DistNo_LRMP,Village_Code ,Tehsile_ID ,FarmerId ,FarmerName ,FatherHusName ,Gram_Panchayat ,PatwariHalkaNo, Mobileno,Category,RinPustikaNo,Farmer_EID_UID_No,Farmer_BankName  ,Farmer_BankAccountNo ,Procured_SocietyID,Procured_Dist_ID ,Procured_Place ,Col_MaxQty,CropDep_Date,UserID,Transferred ,Status ,CreatedDate ,updatedDate ,ip)   values  ( '" + dist + "','" + DistNo_LRMP + "','" + DDL_Village.SelectedValue.ToString() + "','" + DDL_Tah.SelectedValue + "','" + strFarmerID + "',N'" + txtFarmerName.Text + "',N'" + txt_FatherName.Text + "',N'" + txt_GramPanchayat.Text + "',N'" + txtHalkaNo.Text + "','" + txt_mobileno.Text + "','" + ddl_Category.SelectedValue + "','" + txt_RinPustikanumber.Text + "','" + txt_UIDAadhaar.Text + "',N'" + txt_bankname.Text + "','" + txt_BnkAccno.Text + "','" + ddl_samity.SelectedValue + "','" + ddl_DistPrc.SelectedValue + "',N'" + txt_place.Text + "','" + Convert.ToDecimal(txt_ColMaxQty.Text.ToString()) + "' ,'" + getDate_MDY(txt_datetithi.Text.ToString()) + "','','','',getDate(),'','" + ip + "')";
                            con.Open();
                            cmd = new SqlCommand(strIn, con);
                            cmd.ExecuteNonQuery();


                            int l;
                            for (l = 0; l < GridLandRecord.Rows.Count; l++)
                            {
                                string LandType = GridLandRecord.Rows[l].Cells[14].Text.ToString();


                                string villageID = GridLandRecord.Rows[l].Cells[12].Text.ToString();
                                string cid = GridLandRecord.Rows[l].Cells[13].Text.ToString();


                                string LandOwnerName = "";
                                string LandOwnerRinPustikaNo = "";
                                if (LandType == "Own")
                                {
                                    LandOwnerName = "";
                                    LandOwnerRinPustikaNo = "";

                                }
                                else if (LandType == "Sikami")
                                {

                                    LandOwnerName = GridLandRecord.Rows[l].Cells[3].Text.ToString();
                                    LandOwnerRinPustikaNo = GridLandRecord.Rows[l].Cells[4].Text.ToString();

                                }


                                string Ksrno = GridLandRecord.Rows[l].Cells[5].Text.ToString();
                                string rkb = GridLandRecord.Rows[l].Cells[6].Text.ToString();

                                decimal Rakbacropsinchit = Convert.ToDecimal(GridLandRecord.Rows[l].Cells[7].Text.ToString());
                                decimal Rakbacropasinchit = Convert.ToDecimal(GridLandRecord.Rows[l].Cells[8].Text.ToString());

                                decimal Rakbacropsinchitqty = Convert.ToDecimal(GridLandRecord.Rows[l].Cells[9].Text.ToString());
                                decimal Rakbacropasinchitqty = Convert.ToDecimal(GridLandRecord.Rows[l].Cells[10].Text.ToString());
                                decimal Procuredqty = Convert.ToDecimal(GridLandRecord.Rows[l].Cells[11].Text.ToString());

                                String strFarRec = "insert into  Farmer_LandRecordDescription ( Farmer_ID,  Village_ID,  Crop_ID,LandOwner_Name,LandOwner_RinPustikaNo,LandType,KhasaraNo,  Rakba,  Rakba_crop_sinchit,  Rakba_crop_asinchit,  Rakba_crop_sinchit_qty,  Rakba_crop_asinchit_qty,  Procured_qty) values ('" + strFarmerID + "','" + villageID + "','" + cid + "',N'" + LandOwnerName + "','" + LandOwnerRinPustikaNo + "','" + LandType + "','" + Ksrno + "','" + rkb + "','" + Rakbacropsinchit + "','" + Rakbacropasinchit + "','" + Rakbacropsinchitqty + "','" + Rakbacropasinchitqty + "','" + Procuredqty + "')";
                                cmd = new SqlCommand(strFarRec, con);
                                cmd.ExecuteNonQuery();
                            }


                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('किसान की प्रविष्टि सुरक्षित हो गई है । किसान का पंजीयन क्रं. :-   '+'" + strFarmerID.ToString() + "'+'  है|'); </script> ");

                            btn_Save.Enabled = false;
                        }
                        else
                        {

                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('भूमि से संबन्धित  विवरण भरीये .... '); </script> ");
                        
                        
                        }
                    }
                    catch (Exception ex)
                    {
                        Label1.Visible = true;
                        Label1.Text = ex.Message;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                else {

                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('This Farmer with this information allready exits '); </script> ");
                
                }
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Fill All the Farmer Details.'); </script> ");
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");

        }

    }

   
    //protected void btn_update_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        SqlObj = new SqlString(ComObj);

    //        if (txtFarmerName.Text != "" && txt_FatherName.Text != "" && txt_khasrano.Text != "" && txt_rctno.Text != "" && txt_RinPustikanumber.Text != "" && txtHalkaNo.Text != "" & DDL_Tah.SelectedItem.Text != "--चुनिये--" && DDL_Village.SelectedItem.Text != "--चुनिये--")
    //        {
    //            string strup = "update FarmerDetails  set FarmerName='" + txtFarmerName.Text + "',FatherName='" + txt_FatherName.Text + "',KhasaraNo='" + txt_khasrano.Text + "',B1_No='" + txt_B1.Text + "',RationCardNo='" + txt_rctno.Text + "',RationCardType='" + DDL_RCardType.SelectedValue.ToString() + "',PatwariHalkaNo='" + txtHalkaNo.Text + "', Taluk_Code='" + DDL_Tah.SelectedValue + "', Village_Code='" + DDL_Village.SelectedValue + "' ,Status='" + DDL_status.SelectedItem.Text + "' where FarmerId='" + GridView_Farmer.SelectedRow.Cells[2].Text + "'";
    //            con.Open();
    //            cmd = new SqlCommand(strup, con);
    //            cmd.ExecuteNonQuery();
    //            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record Updated successfully.............'); </script> ");
    //            fillGridFarDetail();
    //            btn_update.Enabled = false;
    //            //con.Close();
    //        }
    //        else
    //        {
    //            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please fill all the above values.....'); </script> ");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Label1.Visible = true;
    //        Label1.Text = ex.Message;
    //    }
    //    finally
    //    {
    //        con.Close();
    //    }
    //}
   
    protected void btn_Reset_Click(object sender, EventArgs e)
    {
        Response.Redirect("FarmerDetails.aspx");
    }
    protected void btn_LandReocord_Click(object sender, EventArgs e)
    {

        if (ddl_GridVillage.SelectedIndex > 0 && ddl_GridCrop.SelectedIndex > 0 && txt_Grid_KhasaraNo.Text != "" && txt_Grid_rakaba.Text != "" && txt_Grid_RKBSinchit.Text != "" && txt_Grid_RKBASinchit.Text != "" && txt_Grid_AchinchitQty.Text != "" && txt_Grid_AsinchitQty.Text != "" && txt_Grid_PrcQty.Text != "")
        {
            if (ddl_LandType.SelectedValue.ToString() == "Sikami")
            {
                if (txt_MoolBhoSwamiName.Text == "" || txt_Grid_MoolRinPustikaNo.Text == "")
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('मूल भूमि स्वामी से संबन्धित  विवरण भरीये .... '); </script> ");
                    return;
                }

            }



                if (Session["dt1"] == null)
                {
                    Dt1 = CreateTable();
                    Session["dt1"] = Dt1;
                }

                DataRow dr = ((DataTable)Session["dt1"]).NewRow();
                ((DataTable)Session["dt1"]).AcceptChanges();
                if (ddl_GridVillage.SelectedIndex != 0)
                {
                    dr["VillageName"] = ddl_GridVillage.SelectedItem.Text;
                    dr["Village_ID"] = ddl_GridVillage.SelectedValue;
                }
                else
                {
                    dr["VillageName"] = "";
                    dr["Village_ID"] = "";
                }

                if (ddl_GridCrop.SelectedIndex != 0)
                {
                    dr["crop_Name"] = ddl_GridCrop.SelectedItem.Text;
                    dr["Crop_Id"] = ddl_GridCrop.SelectedValue;
                }
                else
                {
                    dr["crop_Name"] = "";
                    dr["Crop_Id"] = "";
                }

                dr["MoolBhoSwami"] = txt_MoolBhoSwamiName.Text.Trim();
                dr["BhoSwamiRinPustikaNo"] = txt_Grid_MoolRinPustikaNo.Text.Trim();

                dr["khasarano"] = txt_Grid_KhasaraNo.Text.Trim();
                dr["rakba"] = txt_Grid_rakaba.Text.Trim();
                dr["Rakba_crop_sinchit"] = txt_Grid_RKBSinchit.Text.Trim();

                dr["Rakba_crop_asinchit"] = txt_Grid_RKBASinchit.Text.Trim();
                dr["Rakba_crop_sinchit_qty"] = txt_Grid_AchinchitQty.Text.Trim();
                dr["Rakba_crop_asinchit_qty"] = txt_Grid_AsinchitQty.Text.Trim();
                dr["Procured_qty"] = txt_Grid_PrcQty.Text.Trim();
                dr["LandType"] = ddl_LandType.SelectedValue;
                

                ((DataTable)Session["dt1"]).Rows.Add(dr);
                ((DataTable)Session["dt1"]).AcceptChanges();
                GridLandRecord.DataSource = (DataTable)Session["dt1"];
                GridLandRecord.DataBind();

                txt_MoolBhoSwamiName.Text = "";
                txt_Grid_MoolRinPustikaNo.Text = "";

                txt_Grid_KhasaraNo.Text = "";
                txt_Grid_rakaba.Text = "";
                txt_Grid_RKBSinchit.Text = "";

                txt_Grid_RKBASinchit.Text = "";
                txt_Grid_AchinchitQty.Text = "";
                txt_Grid_AsinchitQty.Text = "";
                txt_Grid_PrcQty.Text = "";


            }
        
        else
        {

            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('भूमि से संबन्धित सभी विवरण भरीये .... '); </script> ");

        }
      
    }

    private DataTable CreateTable()
    {
        DataTable Dt_LandRecord = new DataTable();

        Dt_LandRecord.Columns.Add("VillageName");
        Dt_LandRecord.Columns.Add("crop_Name");
        Dt_LandRecord.Columns.Add("MoolBhoSwami");
        Dt_LandRecord.Columns.Add("BhoSwamiRinPustikaNo");
        Dt_LandRecord.Columns.Add("khasarano");
        Dt_LandRecord.Columns.Add("rakba");
        Dt_LandRecord.Columns.Add("Rakba_crop_sinchit");
        Dt_LandRecord.Columns.Add("Rakba_crop_asinchit");
        Dt_LandRecord.Columns.Add("Rakba_crop_sinchit_qty");
        Dt_LandRecord.Columns.Add("Rakba_crop_asinchit_qty");
        Dt_LandRecord.Columns.Add("Procured_qty");
        Dt_LandRecord.Columns.Add("Village_ID");
        Dt_LandRecord.Columns.Add("Crop_Id");
        Dt_LandRecord.Columns.Add("LandType");

        
        Dt_LandRecord.AcceptChanges();
       
        return Dt_LandRecord;
    }
    protected void ddl_LandType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_LandType.SelectedValue.ToString() == "Own")
        {

            btn_LandReocord.Enabled = true;
            txt_MoolBhoSwamiName.Enabled = false;
            txt_Grid_MoolRinPustikaNo.Enabled = false;
            txt_MoolBhoSwamiName.Text = "";
            txt_Grid_MoolRinPustikaNo.Text = "";


            //lbl_molbhoname.Visible = false;
            //txt_MoolBhoSwamiName.Visible = false;

            //lbl_moolbhorinp.Visible = false;
            //txt_Grid_MoolRinPustikaNo.Visible = false;


            txt_MoolBhoSwamiName.BackColor = System.Drawing.Color.Khaki;
            txt_Grid_MoolRinPustikaNo.BackColor = System.Drawing.Color.Khaki;
        }
        else if (ddl_LandType.SelectedValue.ToString() == "Sikami")
        {
            txt_MoolBhoSwamiName.Enabled = true;
            txt_Grid_MoolRinPustikaNo.Enabled = true;
            btn_LandReocord.Enabled = true;
            txt_MoolBhoSwamiName.BackColor = System.Drawing.Color.White;
            txt_Grid_MoolRinPustikaNo.BackColor = System.Drawing.Color.White;
        }
        else
        {

            btn_LandReocord.Enabled = false;

        }
    }
   
    protected void GridLandRecord_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridLandRecord_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int i = e.RowIndex;
            if (GridLandRecord.Rows.Count < 1)
            {
                ViewState["ckstat"] = "Delete";
            }
            ((DataTable)Session["dt1"]).Rows[i].Delete();
            ((DataTable)Session["dt1"]).AcceptChanges();

            GridLandRecord.DataSource = (DataTable)Session["dt1"];
            GridLandRecord.DataBind();
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
        }
    }
  
    protected void btn_can_Click(object sender, EventArgs e)
    {
        Response.Redirect("FarmerRegistration.aspx");
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {

    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
}
