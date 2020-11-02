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
public partial class frmLogin : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_mpproc"].ToString());
    SqlCommand cmd = new SqlCommand();
    protected Common ComObj = null, cmn = null;
    public string qry = "";
    chksql chk = null;
    SqlString sqlObj = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        ComObj = new Common(ConfigurationSettings.AppSettings["ConnectionString_mpproc"].ToString());

        ArrayList ctrllist = new ArrayList();
        ctrllist.Add(txt_password.Text);
        if (chk == null)
        {
        }
        else
        {
            bool chkstr = chk.chksql_server(ctrllist);
            if (chkstr == true)
            {
                Page.Server.Transfer(HttpContext.Current.Request.Path);
            }
        }

        if (!IsPostBack)
        {
            int saltSize = 5;
            string salt = "";
            salt = CreateSalt(saltSize);

            ViewState["salt"] = salt.ToString();
            //Session["salt"] = salt.ToString();
            GetAgencyName();
            GetDist();
            GetMakSeas();
            GetCropYear();
            GetAdmin();
            RDButonAgency.Checked = true;
        }

        //radio button click//
        if (RDButonAgency.Checked == true)
        {
            //Invisible items
            lbl_Depot.Visible = false;
            lbl_Admin.Visible = false;
            lbl_PC.Visible = false;
            DDL_PC.Visible = false;
            DDL_Admin.Visible = false;
            DDL_Depot.Visible = false;
           
           //***

            //When Chk is true then visible
            lbl_Agency.Visible = true;
            lbl_CropYear.Visible = true;
            lbl_MarkSeas.Visible = true;
            lbl_Dist.Visible = true;
            lbl_PC.Visible = true;
            DDL_Agency.Visible = true;
            DDL_CropYear.Visible = true;
            DDL_Dist.Visible = true;
            DDL_MarkSeas.Visible = true;
            DDL_PC.Visible = true;
            //****
            

        }

    

        if (RDButonState.Checked == true)
        {

            //Invisible
            lbl_Admin.Visible = false;
            lbl_Dist.Visible = false;
            lbl_PC.Visible = false;
            DDL_PC.Visible = false;
            DDL_Admin.Visible = false;
            lbl_Dist.Visible = false;
            DDL_Dist.Visible = false;
            //****

            //When Chk is true then visible
            lbl_Agency.Visible = true;
            lbl_CropYear.Visible = true;
            lbl_MarkSeas.Visible = true;

            DDL_Agency.Visible = true;
            DDL_CropYear.Visible = true;
            DDL_MarkSeas.Visible = true;
            
            //****
        
        }

         if (RDButonAdmin.Checked == true)
        {
            lbl_Agency.Visible = false;
            lbl_CropYear.Visible = false;
            lbl_MarkSeas.Visible = false;
            lbl_Dist.Visible = false;
            lbl_Depot.Visible = false;
            lbl_PC.Visible = false;
            DDL_PC.Visible = false;
            DDL_Agency.Visible = false;
            DDL_CropYear.Visible = false;
            lbl_Dist.Visible = false;
            DDL_MarkSeas.Visible = false;
            DDL_Depot.Visible = false;
            DDL_Dist.Visible = false;
            lbl_Admin.Visible = true;
            DDL_Admin.Visible = true;
            
        }


    }

    private void GetAdmin()
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            DataTable dtAdmin = new DataTable();
            string qrySelect = "SELECT * FROM Login l where l.User_Name='admin'";
            SqlDataAdapter oda = new SqlDataAdapter();
            oda.SelectCommand = new SqlCommand(qrySelect, con);
            oda.Fill(dtAdmin);
            if (dtAdmin == null)
            {

            }
            else
            {
              

                DDL_Admin.DataSource = dtAdmin;
                DDL_Admin.DataValueField = "Login_ID";
                DDL_Admin.DataTextField = "User_Name";
                DDL_Admin.DataBind();
                DDL_Admin.Items.Insert(0, "--Select--");
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
        
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

        }
    }

    private void GetCropYear()
    {
        try
        {
            sqlObj = new SqlString(ComObj);
            string str = "SELECT CropId,CropYear FROM CropYearMaster  where CropId in('8') ";
            DataSet ds = sqlObj.selectAny(str);
            if (ds == null)
            {
            }
            else
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DDL_CropYear.DataSource = ds.Tables[0];
                    DDL_CropYear.DataTextField = "CropYear";
                    DDL_CropYear.DataValueField = "CropId";
                    DDL_CropYear.DataBind();
                    //DDL_CropYear.Items.Insert(0, "--Select--");
                    DDL_CropYear.SelectedValue = "8";
                }
            }
        }
        catch (Exception ex)
        {

            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('"+ex.Message+"'); </script> ");
        
        }

    }

    private void GetAgencyName()
    {
        try
        {
            sqlObj = new SqlString(ComObj);
            string qry = "select * from PurchaseAgencyMaster  where PurchaseAgencyId in('232','234')   order by PurchaseAgencyName";
            DataSet ds = sqlObj.selectAny(qry);

            if (ds == null)
            {
            }
            else
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DDL_Agency.DataSource = ds.Tables[0];
                    DDL_Agency.DataTextField = "PurchaseAgencyName";
                    DDL_Agency.DataValueField = "PCType_ID";
                    DDL_Agency.DataBind();
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
            sqlObj = new SqlString(ComObj);

            string qrySelect = "SELECT * FROM DistrictMaster order by DistrictName";

            DataSet ds = sqlObj.selectAny(qrySelect);
            if (ds == null)
            {

            }
            else
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DDL_Dist.DataSource = ds.Tables[0];
                    DDL_Dist.DataValueField = "DistrictCode";
                    DDL_Dist.DataTextField = "DistrictName";
                    DDL_Dist.DataBind();
                    DDL_Dist.Items.Insert(0, "--Select--");
                }
            }
        }

        catch (Exception ex)
        {

            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
        
        
        }
    }
    private void GetMakSeas()
    {
        try
        {
            sqlObj=new SqlString(ComObj);
            string qrySelect = "SELECT MarkSeasId,MarkSeaon FROM MarketingSeasonMaster where MarkSeasId in('r') ";
            DataSet ds = sqlObj.selectAny(qrySelect);
            if (ds == null)
            {
            }
            else
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DDL_MarkSeas.DataSource = ds.Tables[0];
                    DDL_MarkSeas.DataValueField = "MarkSeasId";
                    DDL_MarkSeas.DataTextField = "MarkSeaon";
                    DDL_MarkSeas.DataBind();
                    DDL_CropYear.SelectedValue = "r";
                }

            }
        }
        catch (Exception ex)

        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
        
        
        }
    }

    protected void btn_login_Click(object sender, EventArgs e)
    {
        if (RDButonAgency.Checked == true)
        {
            if(DDL_Agency.SelectedItem.Text == "--Select--" || DDL_Agency.Items.Count == 0)
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Please Select Agency Name'); </script> ");
            }
            else if (DDL_Dist.SelectedItem.Text == "--Select--" || DDL_Dist.Items.Count == 0)
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Please Select District Name'); </script> ");
            }
            else if (DDL_CropYear.SelectedItem.Text == "--Select--" || DDL_CropYear.Items.Count == 0)
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Please Select DDL_CropYear Name'); </script> ");
            }
            else if (DDL_MarkSeas.SelectedItem.Text == "--Select--" || DDL_MarkSeas.Items.Count == 0)
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Please Select Marketing Season '); </script> ");
            }
            else if (DDL_PC.SelectedItem.Text == "--Select--" )
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Please Select Purchase Center'); </script> ");
            }
           else
            {             
                DoLogin();
               
            }      
        }
        else if (RDButonAdmin.Checked == true)
        {
            string username = "";
            if (txt_password.Text != "")
            {
                if (DDL_Admin.SelectedItem.Text == "admin")
                {
                    username = DDL_Admin.SelectedItem.Text.ToString();
                    DoLoginAdmin(username);
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Please Select the User'); </script> ");
                }
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Please Enter Password'); </script> ");
            }
        }
         else if (RDButonState.Checked == true) 
          {
              if (txt_password.Text != "")
              {
                  DoStateLogin();
              
              }
              else
              {
                  Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Please Enter Password'); </script> ");
              }
        
          }

    }

    private void DoStateLogin()
    {
        try
        {
            string Uname = DDL_Agency.SelectedItem.Text;
            string strsqllog = "Select * from Login  where Login.User_Name='" + Uname + "'";
            string strpd = "";
            sqlObj = new SqlString(ComObj);
            DataSet ds = sqlObj.selectAny(strsqllog);
            if (ds == null)
            {

            }
            else
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    string pwd = dr["Password"].ToString();
                    string login_ID = dr["Login_ID"].ToString();
                    strpd = txt_password.Text;

                    string hpwd = CreatePasswordHash(strpd.ToLower());
                    string hpwd1 = CreatePasswordHash(pwd.ToLower());
                    if (hpwd.ToLower() == hpwd1.ToLower())
                    {
                        Session["StateLog_Agency"] = DDL_Agency.SelectedItem.Text;
                        Session["StateLog_AgId"] = DDL_Agency.SelectedValue;
                        Session["StateLog_MarkSeas"] = DDL_MarkSeas.SelectedItem.Text;
                        Session["StateLog_MarkID"] = DDL_MarkSeas.SelectedValue;
                        Session["StateLog_CropY"] = DDL_CropYear.SelectedItem.Text;
                        Session["Loginid"] = login_ID;
                        Session["IS_StateLogin"] = "YES";
                        string tx_hashedPasswordAndSalt = CreatePasswordHash(ds.Tables[0].Rows[0]["Login_ID"].ToString().Trim().ToLower()).ToLower();
                        string tx_hashedPasswordAndSalt1 = CreatePasswordHash(ViewState["salt"].ToString().Trim() + tx_hashedPasswordAndSalt.Trim()).ToLower();
                        Response.Redirect("~/mpproc/State/StateWelcome.aspx");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter the correct Password'); </script> ");

                    }
                }
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");

        }
    }

    private void DoLogin()
    {

        //try
        //{
            string strPcID = DDL_Agency.SelectedValue;
            string strDid = DDL_Dist.SelectedValue;
            string struname = DDL_PC.SelectedItem.Text;
            string strsqllog = "Select * from login where PCType_ID='" + strPcID + "' and DistrictId='" + strDid + "' and FName='" + struname + "'";
            string strpd = "";
            sqlObj = new SqlString(ComObj);
            //string qry = "SELECT Password FROM dbo.DCLogin_MP where District_ID='23" + ddldistrict.SelectedValue.ToString() + "'";
            DataSet ds = sqlObj.selectAny(strsqllog);
            if (ds == null)
            {

            }
            else
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    string loginID = dr["Login_ID"].ToString();
                    string pwd = dr["Password"].ToString();
                    strpd = txt_password.Text;
                    string distid = DDL_Dist.SelectedValue.ToString();
                    Session.Add("_Sdcid", distid);
                    string password = txt_password.Text;
                    txt_password.Text = "";
                    //loginobj.DC_ID = "23" + distid;
                    //loginobj.Tname = "dbo.DCLogin_MP";
                    //loginobj.select();
                    string hpwd = CreatePasswordHash(strpd.ToLower());
                    string hpwd1 = CreatePasswordHash(pwd.ToLower());

                    if (hpwd.ToLower() == hpwd1.ToLower())
                    {

                        string tx_hashedPasswordAndSalt = CreatePasswordHash(DDL_PC.SelectedValue.Trim().ToLower()).ToLower();
                        string tx_hashedPasswordAndSalt1 = CreatePasswordHash(ViewState["salt"].ToString().Trim() + tx_hashedPasswordAndSalt.Trim()).ToLower();

                        Session["Ag_Name"] = DDL_Agency.SelectedItem.Text;
                        Session["Ag_id"] = DDL_Agency.SelectedValue.ToString();
                        Session["Mark_Seas"] = DDL_MarkSeas.SelectedItem.Text;
                        Session["Markseas_id"] = DDL_MarkSeas.SelectedValue.ToString();
                        Session["dist_id"] = DDL_Dist.SelectedValue.ToString();
                        Session["dist_name"] = DDL_Dist.SelectedItem.Text;
                        Session["cropyear"] = DDL_CropYear.SelectedItem.Text;
                        Session["pc_name"] = DDL_PC.SelectedItem.Text;
                        Session["pcId"] = DDL_PC.SelectedValue.ToString();
                        Session["Log_id"] = loginID;
                        Session["IS_Login"] = "Yes"; 
                        Response.Redirect("~/mpproc/Procurement/agency_welcom.aspx");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter the correct Password'); </script> ");

                    }

                }
            }
        //}

        //catch (Exception ex)
        //{
        //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
        
        //}
    }

    private void DoLoginAdmin(string username)
    {
        try
        {
            string strsqllog = "Select * from Login l where l.User_Name='" + username + "'";
            string strpd = "";
            sqlObj = new SqlString(ComObj);
            DataSet ds = sqlObj.selectAny(strsqllog);
            if (ds == null)
            {

            }
            else
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    string pwd = dr["Password"].ToString();
                    string loginAdID = dr["Login_ID"].ToString();
                    strpd = txt_password.Text;

                    string hpwd = CreatePasswordHash(strpd.ToLower());
                    string hpwd1 = CreatePasswordHash(pwd.ToLower());
                    if (hpwd.ToLower() == hpwd1.ToLower())
                    {
                        Session["admin"] = DDL_Admin.SelectedItem.Text;
                        Session["LogAid"] = loginAdID;
                        Session["IS_Admin"] = "yes";
                        string tx_hashedPasswordAndSalt = CreatePasswordHash(ds.Tables[0].Rows[0]["Login_ID"].ToString().Trim().ToLower()).ToLower();
                        string tx_hashedPasswordAndSalt1 = CreatePasswordHash(ViewState["salt"].ToString().Trim() + tx_hashedPasswordAndSalt.Trim()).ToLower();
                        Response.Redirect("~/mpproc/Admin/AdminWelcome.aspx");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter the correct Password'); </script> ");

                    }
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
        try
        {
            sqlObj = new SqlString(ComObj);
            string str = "SELECT * FROM PurchaseCenterMaster,MarketingSeasonMaster where PurchaseCenterMaster.DistrictId = '" + DDL_Dist.SelectedValue + "'  and  PurchaseCenterMaster.MarkSeasId = '" + DDL_MarkSeas.SelectedValue + "' and cropyear ='" + DDL_CropYear.SelectedItem.Text + "' and MarketingSeasonMaster.MarkSeasId = PurchaseCenterMaster.MarkSeasId order by PurchaseCenterName ";
            //string str = "SELECT * FROM PurchaseCenterMaster,MarketingSeasonMaster where PurchaseCenterMaster.DistrictId = '" + DDL_Dist.SelectedValue + "'  and  PurchaseCenterMaster.MarkSeasId = '" + DDL_MarkSeas.SelectedValue + "' and cropyear ='" + DDL_CropYear.Text + "' and MarketingSeasonMaster.MarkSeasId = PurchaseCenterMaster.MarkSeasId order by PurchaseCenterName ";
            DataSet ds = sqlObj.selectAny(str);
            if (ds!= null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DDL_PC.DataSource = ds.Tables[0];
                    DDL_PC.DataTextField = "PurchaseCenterName";
                    DDL_PC.DataValueField = "PcId";
                    DDL_PC.DataBind();

                }
                else
                {
                    DDL_PC.Items.Clear();

                }

            }
           
            DDL_PC.Items.Insert(0, "--Select--");
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
        
        }
    }
    protected void DDL_CropYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            sqlObj = new SqlString(ComObj);
            string str = "SELECT * FROM PurchaseCenterMaster,MarketingSeasonMaster where PurchaseCenterMaster.DistrictId = '" + DDL_Dist.SelectedValue + "'  and  PurchaseCenterMaster.MarkSeasId = '" + DDL_MarkSeas.SelectedValue + "' and cropyear ='" + DDL_CropYear.SelectedItem.Text + "' and MarketingSeasonMaster.MarkSeasId = PurchaseCenterMaster.MarkSeasId order by PurchaseCenterName ";
            DataSet ds = sqlObj.selectAny(str);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DDL_PC.DataSource = ds.Tables[0];
                    DDL_PC.DataTextField = "PurchaseCenterName";
                    DDL_PC.DataValueField = "PcId";
                    DDL_PC.DataBind();

                }
            }
        }
        catch (Exception ex)
        {

            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
        
        }
    }
    protected void DDL_MarkSeas_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            sqlObj = new SqlString(ComObj);
            string str = "SELECT * FROM PurchaseCenterMaster,MarketingSeasonMaster where PurchaseCenterMaster.DistrictId = '" + DDL_Dist.SelectedValue + "' and cropyear ='" + DDL_CropYear.SelectedItem.Text + "' and  PurchaseCenterMaster.MarkSeasId = '" + DDL_MarkSeas.SelectedValue + "'  and MarketingSeasonMaster.MarkSeasId = PurchaseCenterMaster.MarkSeasId order by PurchaseCenterName ";
            DataSet ds = sqlObj.selectAny(str);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DDL_PC.DataSource = ds.Tables[0];
                    DDL_PC.DataTextField = "PurchaseCenterName";
                    DDL_PC.DataValueField = "PcId";
                    DDL_PC.DataBind();

                }
            }
        }
        catch (Exception ex)
        {

            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");

        }
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


   
}
