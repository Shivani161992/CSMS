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

public partial class Masters_FarmerDetails : System.Web.UI.Page
{   
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_mpproc"].ToString());
    SqlCommand cmd = new SqlCommand();
    protected Common ComObj = null, cmn = null;
    public string qry = "";
    RationCardType rctObj = null;
    Districts distObj = null;   
    DataReader objDr = null; 
    SqlString SqlObj = null;
     string dist ="";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["pcId"] != null)
        {
            dist = Session["dist_id"].ToString();
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString_mpproc"].ToString());

            txtFarmerName.Attributes.Add("onkeypress", "return CheckIsChar(this)");
            txt_FatherName.Attributes.Add("onkeypress", "return CheckIsChar(this)");
            txt_khasrano.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtHalkaNo.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txt_B1.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txt_khasrano.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

            //
            btn_update.Visible = false;

          
            if (!IsPostBack)
            {
                GetRationCardType();
                GetDist();
                GetTehsil();
                
            }


        }
        else {


            Response.Redirect("../frmLogin.aspx");

           
        
        }

    }

    private void GetDist()
    {
        distObj =new Districts(ComObj);
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

            }
        }

       
    }
     
    private void GetRationCardType()
    {


        rctObj = new RationCardType(ComObj);
        DataSet ds = rctObj.selectAll();
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                DDL_RCardType.DataSource = ds.Tables[0];
                DDL_RCardType.DataTextField = "FES_Name_Eng";
                DDL_RCardType.DataValueField = "FES_Name_Eng";
                DDL_RCardType.DataBind();
            }

        }
   
    }
    protected void DDL_Dist_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (DDL_Dist.SelectedItem.Text != "--Select--")
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
        //        DDL_Tah.Items.Insert(0, "--Select--");
        //    }

        //}

    }

    protected void GetTehsil()
    {
        SqlObj = new SqlString(ComObj);
        string strSql = "select * from LR_TehsilMaster tm,DistrictMaster dm where dm.DistNo_LRMP=tm.Distno and dm.DistrictCode='" + DDL_Dist.SelectedValue + "'";
        DataSet ds = SqlObj.selectAny(strSql);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                DDL_Tah.DataSource = ds.Tables[0];
                DDL_Tah.DataTextField = "Tehsilname";
                DDL_Tah.DataValueField = "Tehsilno";
                DDL_Tah.DataBind();
                DDL_Tah.Items.Insert(0, "--Select--");
            }
        }
    }
    private void fillGridFarDetail()
    {
        if (DDL_Dist.SelectedItem.Text != "--Select--" && DDL_Tah.SelectedItem.Text != "--Select--")
        {
            SqlObj = new SqlString(ComObj);
            string strSql = "select distinct fd.FarmerId,fd.FarmerName,fd.FatherName,dm.DistrictName,tm.Tehsilname,fd.RinPustikaNo,fd.RationCardType,fd.PatwariHalkaNo,vm.Villagename,fd.KhasaraNo,fd.B1_No,fd.RationCardNo,fd.Status from FarmerDetails fd,DistrictMaster dm,LR_VillageMaster vm,LR_TehsilMaster tm where dm.DistrictCode=fd.DistrictId and vm.Villageno=fd.Village_Code and vm.Tehsilno=tm.Tehsilno and vm.Distno=dm.DistNo_LRMP and tm.Distno=dm.DistNo_LRMP and tm.Tehsilno=fd.Taluk_Code and fd.Status='Active' and  fd.DistrictId ='" + DDL_Dist.SelectedValue + "' and fd.Taluk_Code='" + DDL_Tah.SelectedValue + "' and fd.Village_Code='" + DDL_Village.SelectedValue.ToString() + "'";
            DataSet ds = SqlObj.selectAny(strSql);
            if (ds == null)
            { }
            else
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    GridView_Farmer.DataSource = ds.Tables[0];
                    GridView_Farmer.DataBind();

                }
            }
        }
    }
   
    protected void DDL_Tah_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDL_Tah.SelectedItem.Text != "--Select--")
        {
            //SqlObj = new SqlString(ComObj);
            //string strsql = "select * from LR_RIMaster where Distno='" + DDL_Dist.SelectedValue + "' and Tehsilno='" + DDL_Tah.SelectedValue + "'";
            //DataSet ds = SqlObj.selectAny(strsql);
            //if (ds.Tables[0].Rows.Count > 0)
            //{

            //    DDL_RI.DataSource = ds.Tables[0];
            //    DDL_RI.DataTextField = "RIname";
            //    DDL_RI.DataValueField = "RIno";
            //    DDL_RI.DataBind();
            //    DDL_RI.Items.Insert(0, "--Select--");

            //}
            GetVillage();
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
             str=cmd.ExecuteScalar().ToString();
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
      SqlObj = new SqlString(ComObj);
      string strsql = "  select * from LR_VillageMaster vm,DistrictMaster dm,LR_TehsilMaster tm  where dm.DistNo_LRMP=vm.Distno and tm.Tehsilno=vm.Tehsilno and tm.Distno=dm.DistNo_LRMP and dm.DistrictCode='"+DDL_Dist.SelectedValue+"' and vm.Tehsilno='"+DDL_Tah.SelectedValue+"'";
        DataSet ds = SqlObj.selectAny(strsql);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                DDL_Village.DataSource = ds.Tables[0];
                DDL_Village.DataTextField = "Villagename";
                DDL_Village.DataValueField = "Villageno";
                DDL_Village.DataBind();
                DDL_Village.Items.Insert(0, "--Select--");

            }
        }
 }
    protected void DDL_RI_SelectedIndexChanged(object sender, EventArgs e)
    {
      

        //SqlObj = new SqlString(ComObj);
        //string strsql = "select * from LR_VillageMaster where Distno='" + DDL_Dist.SelectedValue + "' and Tehsilno='" + DDL_Tah.SelectedValue + "' and RIno='" + DDL_RI.SelectedValue + "'";
        //DataSet ds = SqlObj.selectAny(strsql);
        //if (ds.Tables[0].Rows.Count > 0)
        //{

        //    DDL_Village.DataSource = ds.Tables[0];
        //    DDL_Village.DataTextField = "Villagename";
        //    DDL_Village.DataValueField = "Villageno";
        //    DDL_Village.DataBind();
        //    DDL_Village.Items.Insert(0, "--Select--");

        //}


     
    }
    protected void DDL_Village_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGridFarDetail();
     
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string gdnid = "";
        int gdnnum = 0;
        string DistNo_LRMP = GetDistNo_LRMP(DDL_Dist.SelectedValue.ToString());
        if (txtFarmerName.Text != "" && txt_FatherName.Text != "" && txtHalkaNo.Text != "" & DDL_Tah.SelectedItem.Text != "--Select--" && DDL_Village.SelectedItem.Text != "--Select--")
        //if (txtFarmerName.Text != "" && txt_FatherName.Text != "" && txt_khasrano.Text != "" && txt_rctno.Text != "" && txt_RinPustikanumber.Text != "" && txtHalkaNo.Text != "" & DDL_Tah.SelectedItem.Text != "--Select--" && DDL_Village.SelectedItem.Text != "--Select--")
            { 
                SqlObj = new SqlString(ComObj);
                string strsql = "select count(*) as count from FarmerDetails where FarmerName='" + txtFarmerName.Text + "' and FatherName='" + txt_FatherName.Text + "' and KhasaraNo='" + txt_khasrano.Text + "' and RationCardNo='" + txt_rctno.Text + "'";
                DataSet ds = SqlObj.selectAny(strsql);
                if (Convert.ToInt32(ds.Tables[0].Rows[0]["count"]) == 0)
                {
                    objDr = new DataReader(ComObj);

                    string qrey = "select Max(FarmerId) as FarmerId  from FarmerDetails ";
                    DataSet dsf = objDr.selectAny(qrey);
                    DataRow dr = dsf.Tables[0].Rows[0];
                    gdnid = dr["FarmerId"].ToString();
                    ComObj.CloseConnection();
                    if (gdnid == "")
                    {
                        gdnid = "1";


                    }
                    else
                    {

                        gdnnum = Int32.Parse(gdnid.ToString());
                        gdnnum = gdnnum + 1;
                        gdnid = gdnnum.ToString();
                        
                    }

                    string strFarmerID = gdnid;
                    try
                    {
                        string strIn = "Insert into FarmerDetails(FarmerId,FarmerName,FatherName,KhasaraNo,B1_No,RationCardNo,RationCardType,DistrictId,  DistNo_LRMP,Taluk_Code,PatwariHalkaNo, Village_Code,FarmerCategoryID,LandOwnerName,LandOwnerFatherName ,  Status , CreatedDate,updatedDate,ip,RinPustikaNo )  values  ( '" + strFarmerID + "','" + txtFarmerName.Text + "','" + txt_FatherName.Text + "','" + txt_khasrano.Text + "','" + txt_B1.Text + "','" + txt_rctno.Text + "','" + DDL_RCardType.SelectedValue.ToString() + "','" + DDL_Dist.SelectedValue.ToString() + "','" + DistNo_LRMP + "','" + DDL_Tah.SelectedValue + "','" + txtHalkaNo.Text + "','" + DDL_Village.SelectedValue.ToString() + "','','','','" + DDL_status.SelectedItem.Text + "',getDate(),'','" + ip + "','" + txt_RinPustikanumber.Text + "')";
                        con.Open();
                        cmd = new SqlCommand(strIn, con);
                        cmd.ExecuteNonQuery();
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record Saves successfully.............'); </script> ");
                        fillGridFarDetail();
                        btn_Save.Enabled = false;
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
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please All the above Details.'); </script> ");
            }
        
        
    }
    protected void GridView_Farmer_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        GridView_Farmer.PageIndex = e.NewPageIndex;
        fillGridFarDetail();
       
    }
    protected void GridView_Farmer_SelectedIndexChanged(object sender, EventArgs e)
    {

        btn_Save.Visible = false;
        btn_update.Visible = true;
        btn_update.Enabled = true;
        txtFarmerName.Text = GridView_Farmer.SelectedRow.Cells[1].Text;
        txt_FatherName.Text = GridView_Farmer.SelectedRow.Cells[3].Text;
        if (GridView_Farmer.SelectedRow.Cells[9].Text == null || GridView_Farmer.SelectedRow.Cells[9].Text == "&nbsp;")
        {
             txt_khasrano.Text  = "";

        }
        else
        {
             txt_khasrano.Text  = GridView_Farmer.SelectedRow.Cells[9].Text;
        }

        if (GridView_Farmer.SelectedRow.Cells[10].Text == "" || GridView_Farmer.SelectedRow.Cells[10].Text=="&nbsp;")
        {
            txt_B1.Text = "";

        }
        else 
        {
            txt_B1.Text = GridView_Farmer.SelectedRow.Cells[10].Text;
        
        }

        if (GridView_Farmer.SelectedRow.Cells[11].Text == "" || GridView_Farmer.SelectedRow.Cells[11].Text == "&nbsp;")
        {
            txt_rctno.Text = "";

        }
        else
        {
            txt_rctno.Text = GridView_Farmer.SelectedRow.Cells[11].Text;

        }
        
        DDL_RCardType.SelectedItem.Text = GridView_Farmer.SelectedRow.Cells[12].Text;


       // DDL_Dist.SelectedItem.Text = GridView_Farmer.SelectedRow.Cells[8].Text;

        DDL_Tah.SelectedItem.Text=GridView_Farmer.SelectedRow.Cells[5].Text;
       // DDL_RI.SelectedItem.Text = GridView_Farmer.SelectedRow.Cells[10].Text;

    
        txtHalkaNo.Text = GridView_Farmer.SelectedRow.Cells[7].Text;
        DDL_Village.SelectedItem.Text = GridView_Farmer.SelectedRow.Cells[8].Text;
        DDL_status.SelectedItem.Text = GridView_Farmer.SelectedRow.Cells[13].Text;
        if (GridView_Farmer.SelectedRow.Cells[6].Text == "" || GridView_Farmer.SelectedRow.Cells[6].Text == "&nbsp;")
        {
            txt_RinPustikanumber.Text = "";
        }
        else
        {
            txt_RinPustikanumber.Text = GridView_Farmer.SelectedRow.Cells[6].Text;
        }
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {
        try
        {
            SqlObj = new SqlString(ComObj);

            if (txtFarmerName.Text != "" && txt_FatherName.Text != "" && txt_khasrano.Text != ""  && txt_rctno.Text != "" && txt_RinPustikanumber.Text != "" && txtHalkaNo.Text != "" & DDL_Tah.SelectedItem.Text != "--Select--" && DDL_Village.SelectedItem.Text != "--Select--")
            {
                string strup = "update FarmerDetails  set FarmerName='" + txtFarmerName.Text + "',FatherName='" + txt_FatherName.Text + "',KhasaraNo='" + txt_khasrano.Text + "',B1_No='" + txt_B1.Text + "',RationCardNo='" + txt_rctno.Text + "',RationCardType='" + DDL_RCardType.SelectedValue.ToString()+ "',PatwariHalkaNo='" + txtHalkaNo.Text + "', Taluk_Code='" + DDL_Tah.SelectedValue + "', Village_Code='" + DDL_Village.SelectedValue + "' ,Status='" + DDL_status.SelectedItem.Text + "' where FarmerId='" + GridView_Farmer.SelectedRow.Cells[2].Text + "'";
                con.Open();
                cmd = new SqlCommand(strup, con);
                cmd.ExecuteNonQuery();
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record Updated successfully.............'); </script> ");
                fillGridFarDetail();
                btn_update.Enabled = false;
                //con.Close();
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please fill all the above values.....'); </script> ");
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
    protected void GridView_Farmer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       

    }
}
