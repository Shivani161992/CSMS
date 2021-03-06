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


public partial class mpproc_Masters_UpdateFarmerRegistration : System.Web.UI.Page
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

            if (!IsPostBack)
            {

                //GetDist();
                GetDistLRMP();
                GetTehsil();
                getFarInfoonLoad();
            

            }


        }
        else
        {


            Response.Redirect("../frmLogin.aspx");


        }

    }

    private void getFarInfoonLoad()
    {
        try
        {
            GridLandRecord.DataSource = null;
            GridLandRecord.DataBind();


            //SqlObj = new SqlString(ComObj);
            string strsql = "SELECT   FRG.DistNo_LRMP DistID_LRMP, FRG.Tehsile_ID as TehsileID,FRG.Village_Code as Vcode, LRVM.Villagename as village ,FRG.FarmerId as FarmerId ,FRG.FarmerName as FarmerName,FRG.FatherHusName as FarmerFatHUS, FRG.Gram_Panchayat, FRG.PatwariHalkaNo, LRTM.Tehsilname as Tehsil, LRDM .Distname as Distname,FRG.Mobileno Mobileno, FRG.Category Category , FRG.RinPustikaNo as RinPustikaNo, FRG.Farmer_EID_UID_No as EIDUIDNo, FRG.Farmer_BankName as Bankname, FRG.Farmer_BankAccountNo as AccNo, FRG.Procured_SocietyID, FRG.Procured_Dist_ID,  FRG.Procured_Place as PrcPlace , FRG.Col_MaxQty,Category.Category ,CONVERT(VARCHAR(15), FRG.CropDep_Date, 106) as CropDepDate FROM  FarmerRegistration  FRG left join LR_VillageMaster  LRVM on  FRG.DistNo_LRMP=LRVM.Distno and FRG.Tehsile_ID=LRVM.Tehsilno and  FRG.Village_Code=LRVM.Villageno left join LR_TehsilMaster  LRTM  on FRG.Tehsile_ID =LRTM.Distno and FRG.Tehsile_ID=LRTM.Tehsilno left join LR_DistrictMaster LRDM on FRG.DistNo_LRMP =LRDM.Distno left join  Category  on Category.Category_ID=FRG.Category where  FRG.DistNo_LRMP='" + DDL_Dist.SelectedItem.Value.ToString() + "'  order by FarmerId";
            con.Open();
            cmd = new SqlCommand(strsql, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "farInfo");
            
            
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridLandRecord.DataSource = ds.Tables[0];
                    GridLandRecord.DataBind();


                }
            }
        }

        catch (Exception ex)
        {

            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
        }
      
    }

    private void GetDistLRMP()
    {
        try
        {
            SqlObj = new SqlString(ComObj);
            string strSql = "select * from LR_DistrictMaster where Distno in(select  DistNo_LRMP from DistrictMaster where DistrictCode= '" + dist + "')";
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

    }
    protected void DDL_Tah_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDL_Tah.SelectedItem.Text != "--चुनिये--")
        {

            GetVillage();
          
        }
    }

    private void GetVillage()
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
    protected void DDL_Village_SelectedIndexChanged(object sender, EventArgs e)
    {
        getFarmerInfo();
    }



    private void getFarmerInfo()
    {
        try
        {

            GridLandRecord.DataSource = null;

            GridLandRecord.DataBind();


            SqlObj = new SqlString(ComObj);
            string strsql = "SELECT   FRG.DistNo_LRMP DistID_LRMP, FRG.Tehsile_ID as TehsileID,FRG.Village_Code as Vcode, LRVM.Villagename as village ,FRG.FarmerId as FarmerId ,FRG.FarmerName as FarmerName,FRG.FatherHusName as FarmerFatHUS, FRG.Gram_Panchayat, FRG.PatwariHalkaNo, LRTM.Tehsilname as Tehsil, LRDM .Distname as Distname,FRG.Mobileno Mobileno, FRG.Category Category , FRG.RinPustikaNo as RinPustikaNo, FRG.Farmer_EID_UID_No as EIDUIDNo, FRG.Farmer_BankName as Bankname, FRG.Farmer_BankAccountNo as AccNo, FRG.Procured_SocietyID, FRG.Procured_Dist_ID,  FRG.Procured_Place as PrcPlace , FRG.Col_MaxQty,Category.Category ,CONVERT(VARCHAR(15), FRG.CropDep_Date, 106) as CropDepDate FROM  FarmerRegistration  FRG left join LR_VillageMaster  LRVM on  FRG.DistNo_LRMP=LRVM.Distno and FRG.Tehsile_ID=LRVM.Tehsilno and  FRG.Village_Code=LRVM.Villageno left join LR_TehsilMaster  LRTM  on FRG.Tehsile_ID =LRTM.Distno and FRG.Tehsile_ID=LRTM.Tehsilno left join LR_DistrictMaster LRDM on FRG.DistNo_LRMP =LRDM.Distno left join  Category  on Category.Category_ID=FRG.Category where  FRG.DistNo_LRMP='" + DDL_Dist.SelectedValue.ToString() + "' and FRG.Tehsile_ID='" + DDL_Tah.SelectedValue.ToString() + "' and FRG.Village_Code='" + DDL_Village.SelectedValue.ToString() + "'  order by FarmerId";
            DataSet ds = SqlObj.selectAny(strsql);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    GridLandRecord.DataSource = ds.Tables[0];

                    GridLandRecord.DataBind();
                

                }
            }
        }

        catch (Exception ex)
        {

            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
        }
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
    protected void GridLandRecord_SelectedIndexChanged(object sender, EventArgs e)
    {

       
       // Session["upadte"] = "FarmerUpdate";
       //// string fid = GridLandRecord.SelectedRow.Cells[9].Text;
       // String strFID = GridLandRecord.SelectedRow.Cells[10].Text.ToString();
       // String strDID = GridLandRecord.SelectedRow.Cells[11].Text.ToString();
       // String strTID = GridLandRecord.SelectedRow.Cells[12].Text.ToString();
       // String strVID = GridLandRecord.SelectedRow.Cells[13].Text.ToString();
       
       // Session["FarmerFID"]=strFID;
       // Session["FarDID"] = strDID;
       // Session["FarTID"] = strTID;
       // Session["FarVID"] = strVID;
       // Response.Redirect("FarmerRegistration.aspx");

      


    }
    protected void GridLandRecord_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GridLandRecord_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

         try
        {
        int _rowindex = e.RowIndex;
        string FAMID = GridLandRecord.DataKeys[_rowindex].Value.ToString();
        string str = "Delete from  FarmerRegistration where FarmerId='" + FAMID + "'";
        string strdel = "delete from Farmer_LandRecordDescription where Farmer_ID='" + FAMID + "'";
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
             
             cmd.Connection = con;
            if (con != null)
            {
                cmd.CommandText = str;
                cmd.ExecuteNonQuery();

                cmd.CommandText = strdel;
                cmd.ExecuteNonQuery();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }


                string datasave = "Farmer Registration Deleted Successfully......";

                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + datasave + "'); </script> ");
                getFarInfoonLoad();


            }
        }

        catch (Exception ex)
        {

            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");


        }

    }
    protected void GridLandRecord_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                LinkButton lnkDetete = ((LinkButton)e.Row.Cells[0].Controls[0]);
                if (lnkDetete != null)
                    lnkDetete.Attributes["onclick"] = "if(!confirm('Are you sure to delete this row?'))return   false;";
            }
        } 
    }
    protected void GridLandRecord_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
}
