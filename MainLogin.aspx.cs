using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Data;
using DataAccess;
using System.Security.Cryptography;
using System.Data.SqlClient;
//using chksql_NS_App_Cod;

public partial class MainLogin : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    DistributionCenters distobj = null;
    DCLogin loginobj = null;
    Districts DObj = null;
    chksql chk = null;
    Division DivObj = null;
    DataReader objDr = null;
    protected Common ComObj = null, cmn = null;
    public string qry = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        ComObj = new Common(ConfigurationSettings.AppSettings["ConnectionString"].ToString());

        txtpwd.Attributes.Add("onkeypress", "return checksqlkey_psw(event,this)");
        txtpwd.Attributes.Add("onKeyUp", "return taCount(this,'myCounter')");
        txtpwd.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        txtpwd.Attributes.Add("onchange", "return chksqltxt_psw(this),MD5(this);");

        ArrayList ctrllist = new ArrayList();
        ctrllist.Add(txtpwd.Text);       
        if (chk == null)
        {
        }
        else
        {
            bool chkstr =chk.chksql_server(ctrllist) ;
            if (chkstr == true)
            {
                Page.Server.Transfer(HttpContext.Current.Request.Path);
            }
        }


        //txtpwd.Attributes.Add("onkeypress", "return Search(this)");

        if (!IsPostBack)
        {

            int saltSize = 5;
            string salt = "";
            salt = CreateSalt(saltSize);
            Session["salt"] = salt.ToString();

            GetDist();
            GetDistOff();
            GetDivision();
            GetDistDM();
            GetDistDCCB();
            GetHO();
            GetDirFood();
            GetAdmin();
            GetAgency();
            GetDist_CollectorandDIO();
            //Label1.BackColor = System.Drawing.Color.FromName("#336699");
        }
        if (rdbissue.Checked == true)
        {
            //ddldistrict.Visible = true;
            //ddlissue.Visible = true;

            pnl_issue.Visible = true;
            panelCollector.Visible = false;
            ddldistoff.Visible = false;

            ddlregion.Visible = false;
            ddlho.Visible = false;
            //ddldistrictdm.Visible = false;
            pnl_dm.Visible = false;
            dccbdistrict.Visible = false;
            ddladmin.Visible = false;
            ddlAgency.Visible = false;
            drpdirfood.Visible = false;
            if (ddl_ICrole.SelectedValue == "BM")
            {
                qry = "SELECT Password , Masterpassword FROM dbo.DCLogin_MP where District_ID='23" + ddldistrict.SelectedValue.ToString() + "'and DC_ID='" + ddlissue.SelectedValue + "'";
            }

            else
                if (ddl_ICrole.SelectedValue == "DMO")
                {
                    qry = "SELECT markfed_login , Masterpassword FROM dbo.DCLogin_MP where District_ID='23" + ddldistrict.SelectedValue.ToString() + "'and DC_ID='" + ddlissue.SelectedValue + "'";
                }

            else
            {
                qry = Label6.Text;
            }
        }
        else if (rdbdistt.Checked == true)
        {
            
            //ddldistrict.Visible = false;
            //ddlissue.Visible = false;

            pnl_issue.Visible = false;
            panelCollector.Visible = false;
            ddlregion.Visible = false;
            ddlho.Visible = false;
            //ddldistoff.Visible = true;
            dccbdistrict.Visible = false;
            ddldistrictdm.Visible = false;
            pnl_dm.Visible = true;
            ddladmin.Visible = false;
            drpdirfood.Visible = false;
            ddlAgency.Visible = false;

        if (ddl_DMrole.SelectedValue == "DM")
          {
            qry = "SELECT Password , Masterpassword FROM dbo.Distt_login where District_ID='" + ddldistoff.SelectedValue.ToString() + "'";
          }
        else if (ddl_DMrole.SelectedValue == "DMO")
            {
                qry = "SELECT markfed_login , Masterpassword FROM dbo.Distt_login where District_ID='" + ddldistoff.SelectedValue.ToString() + "'";
            }

        else
        {
            qry = Label6.Text;
        }


        }
        else if (rdbregion.Checked == true)
        {
            //ddldistrict.Visible = false;
            //ddlissue.Visible = false;
            pnl_issue.Visible = false ;
            //ddldistoff.Visible = false;
            ddlregion.Visible = true;
            ddlho.Visible = false;
            dccbdistrict.Visible = false;
            ddldistrictdm.Visible = false;
            pnl_dm.Visible = false;
            ddladmin.Visible = false;
            ddlAgency.Visible = false;
            drpdirfood.Visible = false;
            qry = "SELECT Password , MasterPassword  FROM dbo.RegionLogin where Division_code='" + ddlregion.SelectedValue.ToString().Trim() + "'";
        }
        else if (rdbho.Checked == true)
        {
            //ddldistrict.Visible = false;
            //ddlissue.Visible = false;

            pnl_issue.Visible = false;

            //ddldistoff.Visible = false;
            ddlregion.Visible = false;
            ddlho.Visible = true;
            ddldistrictdm.Visible = false;
            pnl_dm.Visible = false;
            dccbdistrict.Visible = false;
            ddladmin.Visible = false;
            ddlAgency.Visible = false;
            drpdirfood.Visible = false;
            qry = "SELECT Password , MasterPassword  FROM dbo.State_Login where User_ID ='" + ddlho.SelectedValue + "'";
        }
        else if (rbdirfood.Checked == true)
        {
            //ddldistrict.Visible = false;
            //ddlissue.Visible = false;

            pnl_issue.Visible = false;

            //ddldistoff.Visible = false;
            ddlregion.Visible = false;
            ddlho.Visible = false;
            drpdirfood.Visible = true;
            ddldistrictdm.Visible = false;
            pnl_dm.Visible = false;
            dccbdistrict.Visible = false;
            ddladmin.Visible = false;
            ddlAgency.Visible = false;
            qry = "SELECT Password FROM dbo.State_Login where User_ID ='" + drpdirfood.SelectedValue + "'";
        }
        else if (rdbdm.Checked == true)
        {
            ddldistrictdm.Visible = true;
            pnl_dm.Visible = false ;     
            //ddldistrict.Visible = false;
            //ddlissue.Visible = false;

            pnl_issue.Visible = false;
            drpdirfood.Visible = false;
            //ddldistoff.Visible = false;
            ddlregion.Visible = false;
            ddlho.Visible = false;
            dccbdistrict.Visible = false;
            ddladmin.Visible = false;
            ddlAgency.Visible = false;
            qry = "SELECT DSOPassword, MasterPassword  FROM [dbo].[DIOLogin] where District_ID='" + ddldistrictdm.SelectedValue.ToString() + "'";


        }
        else if (rdbdccb.Checked == true)
        {
           ddldistrictdm.Visible = false;

            pnl_dm.Visible = false;
            //ddldistrict.Visible = false;
            //ddlissue.Visible = false;
            drpdirfood.Visible = false;
            pnl_issue.Visible = false;
            //ddldistoff.Visible = false;
            ddlregion.Visible = false;
            ddlho.Visible = false;
            dccbdistrict.Visible = true;
            ddladmin.Visible = false;
            ddlAgency.Visible = false;
            qry = "SELECT Password FROM dbo.DCCB_Login where District_ID='" + dccbdistrict.SelectedValue + "'";


        }
        else if (rdbadmin.Checked == true)
        {
          ddldistrictdm.Visible = false;
            pnl_dm.Visible = false;
            //ddldistrict.Visible = false;
            //ddlissue.Visible = false;
            drpdirfood.Visible = false;
            pnl_issue.Visible = false;
            //ddldistoff.Visible = false;
            ddlregion.Visible = false;
            ddlho.Visible = false;
            dccbdistrict.Visible = false;
            ddladmin.Visible = true;
            ddlAgency.Visible = false;
            qry = "SELECT Password,MasterPassword FROM dbo.Admin_Login where UID='" + ddladmin.SelectedValue + "'";


        }

        else if (rdbagency.Checked == true)
        {
            ddldistrictdm.Visible = false;
            pnl_dm.Visible = false;
            //ddldistrict.Visible = false;
            //ddlissue.Visible = false;
            drpdirfood.Visible = false;
            pnl_issue.Visible = false;
            //ddldistoff.Visible = false;
            ddlregion.Visible = false;
            ddlho.Visible = false;
            dccbdistrict.Visible = false;
            ddladmin.Visible = false;
            ddlAgency.Visible = true;
            qry = "SELECT Password,MasterPassword FROM dbo.Admin_Login where UID='" + ddlAgency.SelectedValue + "'";


        }
        else if (rbCollector.Checked == true)
        {

            //ddldistrict.Visible = false;
            //ddlissue.Visible = false;

            pnl_issue.Visible = false;
            pnl_dm.Visible = false;
            panelCollector.Visible = true;
            ddlregion.Visible = false;
            ddlho.Visible = false;
            //ddldistoff.Visible = true;
            dccbdistrict.Visible = false;
            ddldistrictdm.Visible = false;           
            ddladmin.Visible = false;
            drpdirfood.Visible = false;
            ddlAgency.Visible = false;
            //if (ddl_Collector.SelectedValue == "Collector")
            //{
            //    qry = "SELECT Password, Masterpassword FROM dbo.CollectorLogin where District_ID='" + ddl_dist.SelectedValue.ToString() + "'";
            //}

            //else if (ddl_Collector.SelectedValue == "DIO")
            //{
            //    qry = "SELECT Password, Masterpassword FROM dbo.DIOLogin where District_ID='" + ddl_dist.SelectedValue.ToString() + "'";
            //}

        }
    }
    void GetDist()
    {
        if (ddl_ICrole.SelectedItem.Value == "DMO")
        {
            string qrybruit = "SELECT district_code ,district_name FROM [pds].[districtsmp] where paddy_markfed = 'Y' order by district_name";
            SqlCommand cmd = new SqlCommand(qrybruit, con);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                // for Center Login Bind
                ddldistrict.DataSource = ds.Tables[0];

                ddldistrict.DataTextField = "district_name";
                ddldistrict.DataValueField = "district_code";
                ddldistrict.DataBind();
                ddldistrict.Items.Insert(0, "-Select-");

            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        else
        {
            DObj = new Districts(ComObj);
            DataSet ds = DObj.selectAll(" order by district_name");
            if (ds == null)
            {
            }
            else
            {
                ddldistrict.DataSource = ds.Tables[0];
                ddldistrict.DataTextField = "district_name";
                ddldistrict.DataValueField = "District_Code";

                ddldistrict.DataBind();
                ddldistrict.Items.Insert(0, "--Select--");
            }
        }
        
    }
    void GetDistDCCB()
    {

        DObj = new Districts(ComObj);
        DataSet ds = DObj.selectAll(" order by district_name");
        if (ds == null)
        {
        }
        else
        {
            dccbdistrict.DataSource = ds.Tables[0];
            dccbdistrict.DataTextField = "district_name";
            dccbdistrict.DataValueField = "District_Code";

            dccbdistrict.DataBind();
            dccbdistrict.Items.Insert(0, "--Select--");
        }
    }
    void GetAdmin()
    {

        DObj = new Districts(ComObj);
        string qryadmin = "Select  UID,UserName from dbo.Admin_Login where UID='1001' order by UID ";
        DataSet ds = DObj.selectAny(qryadmin);
        if (ds == null)
        {
        }
        else
        {

            ddladmin.DataSource = ds.Tables[0];
            ddladmin.DataTextField = "UserName";
            ddladmin.DataValueField = "UID";
            ddladmin.DataBind();
            ddladmin.Items.Insert(0, "--Select--");
        }
    }

    void GetAgency()
    {

        DObj = new Districts(ComObj);
        string qryadmin = "Select  UID,UserName from dbo.Admin_Login where UID='1002' order by UID ";
        DataSet ds = DObj.selectAny(qryadmin);
        if (ds == null)
        {
        }
        else
        {

            ddlAgency.DataSource = ds.Tables[0];
            ddlAgency.DataTextField = "UserName";
            ddlAgency.DataValueField = "UID";
            ddlAgency.DataBind();
            ddlAgency.Items.Insert(0, "--Select--");
        }
    }

    void GetDistOff()
    {
        if (ddl_DMrole.SelectedItem.Value == "DMO")
        {
            string qrybruit = "SELECT district_code ,district_name FROM [pds].[districtsmp] where paddy_markfed = 'Y' order by district_name";
            SqlCommand cmd = new SqlCommand(qrybruit, con);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {           
                // for dist Login Bind
                ddldistoff.DataSource = ds.Tables[0];

                ddldistoff.DataTextField = "district_name";
                ddldistoff.DataValueField = "district_code";
                ddldistoff.DataBind();
                ddldistoff.Items.Insert(0, "-Select-");
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        else
        {
            DObj = new Districts(ComObj);
            DataSet ds = DObj.selectAll(" order by district_name");
            if (ds == null)
            {
            }
            else
            {
                ddldistoff.DataSource = ds.Tables[0];
                ddldistoff.DataTextField = "district_name";
                ddldistoff.DataValueField = "District_Code";

                ddldistoff.DataBind();
                ddldistoff.Items.Insert(0, "--Select--");
            }
        }
        
    }
    void GetDistDM()
    {
        DObj = new Districts(ComObj);
        DataSet ds = DObj.selectAll(" order by district_name");
        if (ds == null)
        {
        }
        else
        {
            ddldistrictdm.DataSource = ds.Tables[0];
            ddldistrictdm.DataTextField = "district_name";
            ddldistrictdm.DataValueField = "District_Code";

            ddldistrictdm.DataBind();
            ddldistrictdm.Items.Insert(0, "--Select--");
        }
    }
    void GetHO()
    {
        DObj = new Districts(ComObj);
        string qryho = "Select  User_ID,User_Name from dbo.State_Login where User_ID in ('1','4','9','3','10') order by  User_ID ";
        DataSet ds = DObj.selectAny(qryho);
        if (ds == null)
        {
        }
        else
        {
            ddlho.DataSource = ds.Tables[0];
            ddlho.DataTextField = "User_Name";
            ddlho.DataValueField = "User_ID";

            ddlho.DataBind();
            ddlho.Items.Insert(0, "--Select--");
        }
    }
    void GetDirFood()
    {
        DObj = new Districts(ComObj);
        string qryho = "Select  User_ID,User_Name from dbo.State_Login where User_ID='2' order by  User_ID ";
        DataSet ds = DObj.selectAny(qryho);
        if (ds == null)
        {
        }
        else
        {
            drpdirfood.DataSource = ds.Tables[0];
            drpdirfood.DataTextField = "User_Name";
            drpdirfood.DataValueField = "User_ID";

            drpdirfood.DataBind();
            drpdirfood.Items.Insert(0, "--Select--");
        }
    }
    void GetDCName()
    {

        ddlissue.Items.Clear();
        distobj = new DistributionCenters(ComObj);
        string ord = "Districtid='23" + ddldistrict.SelectedValue.ToString() + "' and Status_Flag = 'Y' order by DepotName";
        DataSet ds = distobj.select(ord);
        if (ds == null)
        {
        }
        else
        {
            ddlissue.DataSource = ds.Tables[0];
            ddlissue.DataTextField = "DepotName";
            ddlissue.DataValueField = "DepotId";

            ddlissue.DataBind();
        }
        // ddDistId.Items.Insert(0, "--चुनिये--");
       
    }
    void GetDivision()
    {
        DivObj = new Division(ComObj);
        string divselect = "select Left(Division,Len(Division)-9) as Division_name,Division_code from dbo.division order by Division_code";
        DataSet ds = DivObj.selectAny(divselect);
        if (ds == null)
        {
        }
        else
        {
            ddlregion.DataSource = ds.Tables[0];
            ddlregion.DataTextField = "Division_name";
            ddlregion.DataValueField = "Division_code";
            ddlregion.DataBind();
            ddlregion.Items.Insert(0, "--Select--");
        }
    }
    void GetDist_CollectorandDIO()
    {
        DObj = new Districts(ComObj);
        DataSet ds = DObj.selectAll(" order by district_name");
        if (ds == null)
        {
        }
        else
        {
            ddl_dist.DataSource = ds.Tables[0];
            ddl_dist.DataTextField = "district_name";
            ddl_dist.DataValueField = "District_Code";

            ddl_dist.DataBind();
            ddl_dist.Items.Insert(0, "--Select--");
        }
    }
    void DSOLogin()
    {
        if (Session["salt"] == null)
        {
            Response.Redirect("~/MainLogin.aspx");
        }

        string strdsopd = "";
        string strMpd = "";

        loginobj = new DCLogin(ComObj);
        //string qry = "SELECT Password FROM dbo.DCLogin_MP where District_ID='23" + ddldistrict.SelectedValue.ToString() + "'";
        DataSet ds = loginobj.selectAny(qry);
        if (ds == null)
        {

        }
        else
        {

            DataRow dr = ds.Tables[0].Rows[0];

            string dsopwd = dr["DSOPassword"].ToString();
            string Mpwd = dr["Masterpassword"].ToString();
            
            strdsopd = dsopwd;
            strMpd = Mpwd;
            
            string distid = ddldistrict.SelectedValue;
            Session.Add("_Sdcid", distid);
            string password = txtpwd.Text;
            txtpwd.Text = "";
            loginobj.DC_ID = "23" + distid;
            loginobj.Tname = "dbo.DCLogin_MP";
            loginobj.select();
            string hpwd = CreatePasswordHash(strdsopd.ToLower());

            string hmpwd = CreatePasswordHash(strMpd.ToLower());

            Session["State_Id"] = "23";
            //if (password == loginobj.Password || password == hpwd.ToLower() || password == hmpwd.ToLower())
            {
                if (rdbdm.Checked == true)
                {

                    string tx_hashedPasswordAndSalt = CreatePasswordHash(ddldistrictdm.SelectedValue.Trim().ToLower()).ToLower();
                    string tx_hashedPasswordAndSalt1 = CreatePasswordHash(Session["salt"].ToString().Trim() + tx_hashedPasswordAndSalt.Trim()).ToLower();
                    Session["dist_id"] = ddldistrictdm.SelectedValue;
                    Session["dist_name"] = ddldistrictdm.SelectedItem.ToString();
                    Session["dist_idH"] = tx_hashedPasswordAndSalt1;


                    if (chkhindi.Checked == true)
                    {
                        Session.Add("hindi", "H");
                    }
                    else
                    {
                        Session.Add("hindi", "I");
                    }
                    Response.Redirect("~/DistrictFood/DO_Welcome.aspx");
                }
            }

        }
    }
    void DoLogin()
    {

        if (Session["salt"] == null)
        {
            Response.Redirect("~/MainLogin.aspx");
        }
        string strpd = "";

        string strMpd = "";

        loginobj = new DCLogin(ComObj);
        //string qry = "SELECT Password FROM dbo.DCLogin_MP where District_ID='23" + ddldistrict.SelectedValue.ToString() + "'";
        DataSet ds = loginobj.selectAny(qry);
        if (ds == null)
        {

        }
        else
        {
            string pwd = "";
            string Mpwd = "";

            if (ddl_ICrole.SelectedValue == "DMO")
            {
                DataRow dr = ds.Tables[0].Rows[0];
                pwd = dr["markfed_login"].ToString();

                 Mpwd = dr["Masterpassword"].ToString();

                 Session["Markfed"] = "Y";
            }

            else if (ddl_DMrole.SelectedValue == "DMO")
            {
                DataRow dr = ds.Tables[0].Rows[0];
                pwd = dr["markfed_login"].ToString();

                Mpwd = dr["Masterpassword"].ToString();

               Session["Markfed"] = "Y";
            }

            else
            {
                DataRow dr = ds.Tables[0].Rows[0];
                 pwd = dr["Password"].ToString();

                 Mpwd = dr["Masterpassword"].ToString();
                                 
                 if (pwd == "def@2k16")    /// Check Default pasword , if yes then force to change password
                 {
                     Session["Def_password"] = "Y";
                 }
                 // 
                 else
                 {
                     Session["Def_password"] = "N";
                 }

                 Session["Markfed"] = "N";
            }

            strpd = pwd;

            strMpd = Mpwd;

            string distid = ddldistrict.SelectedValue;
            Session.Add("_Sdcid", distid);
            string password = txtpwd.Text;
            txtpwd.Text = "";
            loginobj.DC_ID = "23" + distid;
            loginobj.Tname = "dbo.DCLogin_MP";
            loginobj.select();
            string hpwd = CreatePasswordHash(strpd.ToLower());

            string hmpwd = CreatePasswordHash(strMpd.ToLower());

            Session["State_Id"] = "23";

            if (password == loginobj.Password || password == hpwd.ToLower() || password == hmpwd.ToLower())
            {

                if (rdbissue.Checked == true)
                {
                    if (ddl_ICrole.SelectedValue == "BM" || ddl_ICrole.SelectedValue == "DMO")
                    {

                        string tx_hashedPasswordAndSalt = CreatePasswordHash(ddldistrict.SelectedValue.Trim().ToLower()).ToLower();
                        string tx_hashedPasswordAndSalt1 = CreatePasswordHash(Session["salt"].ToString().Trim() + tx_hashedPasswordAndSalt.Trim()).ToLower();
                        Session["issue_id"] = ddlissue.SelectedValue;
                        Session["dist_id"] = ddldistrict.SelectedValue;
                        Session["dist_idH"] = tx_hashedPasswordAndSalt1;
                        Session.Add("issue_name", ddlissue.SelectedItem);
                        Session["BranchManager"] = "BM";

                        if (ddl_ICrole.SelectedValue == "DMO")
                        {
                            Session["OperatorId"] = "DMO";
                        }

                        else
                        {
                            Session["OperatorId"] = "BM";
                        }
                        
                        
                        if (chkhindi.Checked == true)
                        {
                            Session.Add("hindi", "H");
                        }
                        else
                        {
                            Session.Add("hindi", "I");
                        }

                        if (Session["Def_password"] == "Y")
                        {
                            Response.Redirect("~/IssueCenter/Change_defPassword.aspx");
                        }

                        else
                        {
                            Response.Redirect("~/IssueCenter/issue_welcome.aspx");
                        }

                    }

                    else
                    {
                        if (DDLOperetorId.SelectedValue != "--Select--")
                        {
                            string tx_hashedPasswordAndSalt = CreatePasswordHash(ddldistrict.SelectedValue.Trim().ToLower()).ToLower();
                            string tx_hashedPasswordAndSalt1 = CreatePasswordHash(Session["salt"].ToString().Trim() + tx_hashedPasswordAndSalt.Trim()).ToLower();
                            Session["issue_id"] = ddlissue.SelectedValue;
                            Session["dist_id"] = ddldistrict.SelectedValue;
                            Session["dist_idH"] = tx_hashedPasswordAndSalt1;
                            Session.Add("issue_name", ddlissue.SelectedItem);

                            //Session.Add("issue_name", DDLOperetorId.SelectedValue.ToString());

                            Session["OperatorId"] = DDLOperetorId.SelectedValue.ToString();
                            if (chkhindi.Checked == true)
                            {
                                Session.Add("hindi", "H");
                            }
                            else
                            {
                                Session.Add("hindi", "I");
                            }
                            InsertOperatorLogin(DDLOperetorId.SelectedValue.ToString());
                            Response.Redirect("~/IssueCenter/issue_welcome.aspx");

                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Please Select OperatorId First'); </script> ");
                        }
                    }


                }
                else if (rdbdistt.Checked == true)
                {
                    if (ddl_DMrole.SelectedValue == "DM" || ddl_DMrole.SelectedValue == "DMO")
                    {
                        string tx_hashedPasswordAndSalt = CreatePasswordHash(ddldistoff.SelectedValue.Trim().ToLower()).ToLower();
                        string tx_hashedPasswordAndSalt1 = CreatePasswordHash(Session["salt"].ToString().Trim() + tx_hashedPasswordAndSalt.Trim()).ToLower();
                        Session["dist_id"] = ddldistoff.SelectedValue;
                        Session["dist_name"] = ddldistoff.SelectedItem.ToString();
                        Session["dist_idH"] = tx_hashedPasswordAndSalt1;
                        Session["DistrictManager"] = "DM";
                        Session["OperatorIDDM"] = "DM";
                        Session["Collector/DIO"] = "DMMPSCSC";

                        if (ddl_DMrole.SelectedValue == "DMO")
                        {
                            Session["DistrictManager"] = "DDMO";
                            Session["OperatorIDDM"] = "DDMO";
                        }
                        else
                        {
                            Session["DistrictManager"] = "DM";
                            Session["OperatorIDDM"] = "DM";
                            Session["Collector/DIO"] = "DMMPSCSC";
                        }

                        if (chkhindi.Checked == true)
                        {
                            Session.Add("hindi", "H");
                        }
                        else
                        {
                            Session.Add("hindi", "I");
                        }

                        Response.Redirect("~/District/Dist_Welcome.aspx");
                    }
                    else 
                    {
                        if (ddl_DMrole.SelectedValue != "--Select--")
                        {
                            string tx_hashedPasswordAndSalt = CreatePasswordHash(ddldistoff.SelectedValue.Trim().ToLower()).ToLower();
                            string tx_hashedPasswordAndSalt1 = CreatePasswordHash(Session["salt"].ToString().Trim() + tx_hashedPasswordAndSalt.Trim()).ToLower();
                            Session["dist_id"] = ddldistoff.SelectedValue;
                            Session["dist_name"] = ddldistoff.SelectedItem.ToString();
                            Session["dist_idH"] = tx_hashedPasswordAndSalt1;
                            Session["DistrictManager"] = "OP";
                            Session["OperatorIDDM"] = ddl_DMoperatorID.SelectedValue.ToString();
                            if (chkhindi.Checked == true)
                            {
                                Session.Add("hindi", "H");
                            }
                            else
                            {
                                Session.Add("hindi", "I");
                            }
                            InsertOperatorLogin(ddl_DMoperatorID.SelectedValue.ToString());
                            Response.Redirect("~/District/Dist_Welcome.aspx");
                        }
                    }
                }
                else if (rdbregion.Checked == true)
                {
                    string tx_hashedPasswordAndSalt = CreatePasswordHash(ddlregion.SelectedValue.Trim().ToLower()).ToLower();
                    string tx_hashedPasswordAndSalt1 = CreatePasswordHash(Session["salt"].ToString().Trim() + tx_hashedPasswordAndSalt.Trim()).ToLower();
                    Session["Region_id"] = ddlregion.SelectedValue;
                    Session["Region_name"] = ddlregion.SelectedItem.ToString();
                    Session["dist_idH"] = tx_hashedPasswordAndSalt1;


                    if (chkhindi.Checked == true)
                    {
                        Session.Add("hindi", "H");
                    }
                    else
                    {
                        Session.Add("hindi", "I");
                    }

                    Response.Redirect("~/Regional_Office/Welcome_RO.aspx");
                }
                //else if (rdbdm.Checked == true)
                //{

                //    string tx_hashedPasswordAndSalt = CreatePasswordHash(ddldistrictdm.SelectedValue.Trim().ToLower()).ToLower();
                //    string tx_hashedPasswordAndSalt1 = CreatePasswordHash(Session["salt"].ToString().Trim() + tx_hashedPasswordAndSalt.Trim()).ToLower();
                //    Session["dist_id"] = ddldistrictdm.SelectedValue;
                //    Session["dist_name"] = ddldistrictdm.SelectedItem.ToString();
                //    Session["dist_idH"] = tx_hashedPasswordAndSalt1;


                //    if (chkhindi.Checked == true)
                //    {
                //        Session.Add("hindi", "H");
                //    }
                //    else
                //    {
                //        Session.Add("hindi", "I");
                //    }
                //    Response.Redirect("~/DistrictFood/DO_Welcome.aspx");
                //}
                else if (rdbdccb.Checked == true)
                {


                    string tx_hashedPasswordAndSalt = CreatePasswordHash(dccbdistrict.SelectedValue.Trim().ToLower()).ToLower();
                    string tx_hashedPasswordAndSalt1 = CreatePasswordHash(Session["salt"].ToString().Trim() + tx_hashedPasswordAndSalt.Trim()).ToLower();
                    Session["dist_id"] = dccbdistrict.SelectedValue;
                    Session["dist_name"] = dccbdistrict.SelectedItem.ToString();
                    Session["dist_idH"] = tx_hashedPasswordAndSalt1;

                    if (chkhindi.Checked == true)
                    {
                        Session.Add("hindi", "H");
                    }
                    else
                    {
                        Session.Add("hindi", "I");
                    }
                    Response.Redirect("~/DCCB/DCCB_Welcome.aspx");

                }

                else if (rdbho.Checked == true)
                {

                    string tx_hashedPasswordAndSalt = CreatePasswordHash(ddlho.SelectedValue.Trim().ToLower()).ToLower();
                    string tx_hashedPasswordAndSalt1 = CreatePasswordHash(Session["salt"].ToString().Trim() + tx_hashedPasswordAndSalt.Trim()).ToLower();
                    Session["st_id"] = ddlho.SelectedValue;
                    Session["st_Name"] = ddlho.SelectedItem.Text;
                    Session["st_idH"] = tx_hashedPasswordAndSalt1;

                    if (chkhindi.Checked == true)
                    {
                        Session.Add("hindi", "H");
                    }
                    else
                    {
                        Session.Add("hindi", "I");
                    }

                    if (ddlho.SelectedValue == "1")  // Ho Main Login full display
                    {
                        Response.Redirect("~/State/State_Welcome.aspx");
                    }
                    if (ddlho.SelectedValue == "4")  // finance login for MD Link Dispaly
                    {
                        Response.Redirect("~/State/MD_Welcome.aspx");
                    }
                    if (ddlho.SelectedValue == "3")  // finance login for HO Link Dispaly
                    {
                        Response.Redirect("~/HOFinance/HoFinance_Welcome.aspx");
                    }
                    if (ddlho.SelectedValue == "5")  // finance login for PDS Link Dispaly
                    {
                        Response.Redirect("~/State/PDS_Welcome.aspx");
                    }

                    if (ddlho.SelectedValue == "6")  // finance login for Transport Link Dispaly
                    {
                        Response.Redirect("~/State/Transport_Welcome.aspx");
                    }


                    if (ddlho.SelectedValue == "7")  // finance login for Sugar Link Dispaly
                    {
                        Response.Redirect("~/State/Sugar_Welcome.aspx");
                    }

                    if (ddlho.SelectedValue == "8")  // finance login for Procuremnt Link Dispaly
                    {
                        Response.Redirect("~/State/Procurement_Welcome.aspx");
                    }

                    if (ddlho.SelectedValue == "9") // Paddy/Rice Section For Paddy Milling 
                    {
                        Response.Redirect("~/State/Paddy_Welcome.aspx");
                    }

                    if (ddlho.SelectedValue == "10") // Markfed For Paddy Milling 
                    {
                        Response.Redirect("~/State/Markfed_Welcome.aspx");
                    }

                }
                else if (rbdirfood.Checked == true)
                {

                    string tx_hashedPasswordAndSalt = CreatePasswordHash(drpdirfood.SelectedValue.Trim().ToLower()).ToLower();
                    string tx_hashedPasswordAndSalt1 = CreatePasswordHash(Session["salt"].ToString().Trim() + tx_hashedPasswordAndSalt.Trim()).ToLower();
                    Session["st_id"] = drpdirfood.SelectedValue;
                    Session["st_Name"] = drpdirfood.SelectedItem.Text;
                    Session["st_idH"] = tx_hashedPasswordAndSalt1;

                    if (chkhindi.Checked == true)
                    {
                        Session.Add("hindi", "H");
                    }
                    else
                    {
                        Session.Add("hindi", "I");
                    }
                    Response.Redirect("~/State/State_Welcome.aspx");
                }
                else if (rdbadmin.Checked == true)
                {

                    string tx_hashedPasswordAndSalt = CreatePasswordHash(ddladmin.SelectedValue.Trim().ToLower()).ToLower();
                    string tx_hashedPasswordAndSalt1 = CreatePasswordHash(Session["salt"].ToString().Trim() + tx_hashedPasswordAndSalt.Trim()).ToLower();
                    Session["st_id"] = ddladmin.SelectedValue;
                    Session["st_Name"] = ddladmin.SelectedItem.Text;
                    Session["st_idH"] = tx_hashedPasswordAndSalt1;


                    if (chkhindi.Checked == true)
                    {
                        Session.Add("hindi", "H");
                    }
                    else
                    {
                        Session.Add("hindi", "I");
                    }
                    Response.Redirect("~/Admin/AdminWelcome.aspx");
                }

                else if (rdbagency.Checked == true)
                {

                    string tx_hashedPasswordAndSalt = CreatePasswordHash(ddlAgency.SelectedValue.Trim().ToLower()).ToLower();
                    string tx_hashedPasswordAndSalt1 = CreatePasswordHash(Session["salt"].ToString().Trim() + tx_hashedPasswordAndSalt.Trim()).ToLower();
                    Session["ag_id"] = ddlAgency.SelectedValue;
                    Session["ag_Name"] = ddlAgency.SelectedItem.Text;
                    Session["ag_idH"] = tx_hashedPasswordAndSalt1;


                    if (chkhindi.Checked == true)
                    {
                        Session.Add("hindi", "H");
                    }
                    else
                    {
                        Session.Add("hindi", "I");
                    }
                    Response.Redirect("~/Agency/agencywelcome.aspx");
                }

                else if (rbCollector.Checked == true)
                {
                    if (ddl_Collector.SelectedValue == "Collector")
                    {
                        string tx_hashedPasswordAndSalt = CreatePasswordHash(ddl_dist.SelectedValue.Trim().ToLower()).ToLower();
                        string tx_hashedPasswordAndSalt1 = CreatePasswordHash(Session["salt"].ToString().Trim() + tx_hashedPasswordAndSalt.Trim()).ToLower();
                        Session["dist_id"] = ddl_dist.SelectedValue;
                        Session["dist_name"] = ddl_dist.SelectedItem.ToString();
                        Session["dist_idH"] = tx_hashedPasswordAndSalt1;
                        if (ddl_Collector.SelectedValue == "Collector")
                        {
                            Session["Collector/DIO"] = "Collector";
                        }
                        
                        if (chkhindi.Checked == true)
                        {
                            Session.Add("hindi", "H");
                        }
                        else
                        {
                            Session.Add("hindi", "I");
                        }

                        Response.Redirect("~/District/Collector_DIO.aspx");

                    }
                    else if (ddl_Collector.SelectedValue == "DIO")
                    {
                        string tx_hashedPasswordAndSalt = CreatePasswordHash(ddl_dist.SelectedValue.Trim().ToLower()).ToLower();
                        string tx_hashedPasswordAndSalt1 = CreatePasswordHash(Session["salt"].ToString().Trim() + tx_hashedPasswordAndSalt.Trim()).ToLower();
                        Session["dist_id"] = ddl_dist.SelectedValue;
                        Session["dist_name"] = ddl_dist.SelectedItem.ToString();
                        Session["dist_idH"] = tx_hashedPasswordAndSalt1;
                        if (ddl_Collector.SelectedValue == "DIO")
                        {
                            Session["Collector/DIO"] = "DIO";
                        }

                        if (chkhindi.Checked == true)
                        {
                            Session.Add("hindi", "H");
                        }
                        else
                        {
                            Session.Add("hindi", "I");
                        }

                        Response.Redirect("~/District/Collector_DIO.aspx");
                    }
                        //Response.Redirect("~/District/Dist_Welcome.aspx");
                }

            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Your Password is wrong.'); </script> ");
                Label1.Visible = true;
                Label1.Text = "Your Login attempt was not successful.Please try again";
                Label1.ForeColor = System.Drawing.Color.Maroon;
                Label1.BackColor = System.Drawing.Color.LightGray;
                //ClientScript.RegisterClientScriptBlock(typeof(string), "mymsg", "<script language=javascript> alert('Your Password is wrong.'); </script> ");
                txtpwd.Text = "";
            }
            //}
        }
    }
    protected int checkOpt()
    {
        string chk = txtpwd.Text;
        string chk1 = chk.ToLower();
        if (chk1.Contains("drop"))
        {
            //Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Drop not allowed...'); </script> ");
            return (1);

        }
        else if (chk1.Contains("delete"))
        {
            return (1);

        }
        else if (chk1.Contains("alter"))
        {
            return (1);
        }
        else if (chk1.Contains("modify"))
        {
            return (1);

        }
        else if (chk1.Contains("truncate"))
        {
            return (1);

        }
        else
        {
            return (0);

        }
    }  
    protected void btnlogin_Click(object sender, ImageClickEventArgs e)
    {
        //int n =checkOpt();
        //if (n == 1)
        //{
        //    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('DML Operation(Drop/Delete/Modify etc.) not allowed...'); </script> ");
        //}
        //else
        //{
            if (rdbissue.Checked == true)
            {
                if (ddl_ICrole.SelectedValue == "BM")
                {
                    if (ddldistrict.SelectedItem.Text == "--Select--" || ddlissue.Items.Count == 0 )
                    {
                       Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Please Select District and IssueCenter...'); </script> ");
                    }
                    else
                    {
                        DoLogin();
                    }

                }
                else if (ddl_ICrole.SelectedValue == "DMO")
                {
                    if (ddldistrict.SelectedItem.Text == "--Select--" || ddlissue.Items.Count == 0)
                    {
                        Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Please Select District and IssueCenter...'); </script> ");
                    }
                    else
                    {
                        DoLogin();
                    }

                }

                else
                {
                    if (DDLOperetorId.Items.Count == 0 || DDLOperetorId.SelectedItem.Text == "--Select--" )
                    {

                        string pwd = "nicop";
                        string hpwd1 = CreatePasswordHash(pwd.ToLower());
                        
                        if (txtpwd.Text == hpwd1.ToLower())
                        {
                            //Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert(' Welcome Operator'); </script> ");
                            Session["issue_id"] = ddlissue.SelectedValue;
                            Session["dist_id"] = ddldistrict.SelectedValue;
                            Session.Add("issue_name", ddlissue.SelectedItem);
                            Session["BranchManager"] = "BM";
                            Session["OperatorId"] = "BM";
                            Session["_Sdcid"] = ddldistrict.SelectedValue;
                            Response.Redirect("~/IssueCenter/OperatorRegistration.aspx");
                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert(' Please Select Operator'); </script> ");

                        }

                       // Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Please Select Operator...'); </script> ");
                    }
                    else
                    {
                        DoLogin();
                    }
                }
            }
            else if (rdbdistt.Checked == true)
            {
                if (ddl_DMrole.SelectedValue == "DM")
                {
                    if (ddldistoff.SelectedItem.Text == "--Select--")
                    {
                        Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Please Select District Office...'); </script> ");

                    }
                    else
                    {
                        DoLogin();
                    }

                }
                else if (ddl_DMrole.SelectedValue == "DMO")
                {
                    if (ddldistoff.SelectedItem.Text == "--Select--")
                    {
                        Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Please Select District Office...'); </script> ");

                    }
                    else
                    {
                        DoLogin();
                    }

                }
                else
                {
                    if (ddl_DMoperatorID.Items.Count == 0 || ddl_DMoperatorID.SelectedItem.Text == "--Select--" )
                    {
                        string pwd = "nicop";
                        string hpwd1 = CreatePasswordHash(pwd.ToLower());


                        if (txtpwd.Text == hpwd1.ToLower ())
                        {
                            //Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert(' Welcome Operator'); </script> ");
                            Session["dist_id"] = ddldistoff.SelectedValue.ToString();
                            Session["DistrictManager"] = "DM";
                          Response.Redirect("~/District/OperatorRegistration.aspx");
  
                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert(' Please Select Operator'); </script> ");

                        }
                    }
                    else
                    {
                        DoLogin();
                    }

                }

            }

            else if (rdbregion.Checked == true)
            {
                if (ddlregion.SelectedItem.Text == "--Select--")
                {
                    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Please Select Regional Office...'); </script> ");
                }
                else
                {
                    DoLogin();
                }
            }
            else if (rdbdm.Checked == true)
            {
                if (ddldistrictdm.SelectedItem.Text == "--Select--")
                {
                    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Please Select Login District...'); </script> ");

                }
                else
                {
                    DSOLogin();
                }
            }
            else if (rdbdccb.Checked == true)
            {

                if (dccbdistrict.SelectedItem.Text == "--Select--")
                {
                    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Please Select Login District...'); </script> ");
                }
                else
                {
                    DoLogin();
                }
            }
            else if (rdbho.Checked == true)
            {
                if (ddlho.SelectedItem.Text == "--Select--")
                {
                    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Please Select Login Type...'); </script> ");

                }
                else
                {
                    DoLogin();
                }
            }
            else if (rbdirfood.Checked == true)
            {
                if (drpdirfood.SelectedItem.Text == "--Select--")
                {
                    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Please Select Login Type...'); </script> ");
                }
                else
                {
                    DoLogin();
                }
            }
            else if (rdbadmin.Checked == true)
            {
                if (ddladmin.SelectedItem.Text == "--Select--")
                {
                    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Please Select Admin Name...'); </script> ");
                }
                else
                {
                    DoLogin();
                }
            }

            else if (rdbagency.Checked == true)
            {
                if (  ddlAgency.SelectedItem.Text == "--Select--")
                {
                    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Please Select Agency  Name...'); </script> ");

                }
                else
                {
                    DoLogin();
                }
            }
            else if (rbCollector.Checked == true)
            {
                if (ddl_dist.SelectedItem.Text == "--Select--" || ddl_Collector.SelectedItem.Text=="--Select--")
                {
                    Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Please Select Role or District ...'); </script> ");

                }
                else
                {
                    if (ddl_Collector.SelectedValue == "Collector")
                    {
                        qry = "SELECT Password, Masterpassword FROM dbo.CollectorLogin where District_ID='" + ddl_dist.SelectedValue.ToString() + "'";
                    }

                    else if (ddl_Collector.SelectedValue == "DIO")
                    {
                        qry = "SELECT Password, Masterpassword FROM dbo.DIOLogin where District_ID='" + ddl_dist.SelectedValue.ToString() + "'";
                    }
                    DoLogin();
                }
            }

        //}
    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_DMoperatorID.Items.Clear();
        GetDCName();
        GetOPid();

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

    protected void txtpwd_TextChanged(object sender, EventArgs e)
    {
       
    }
    private string base64Encode(string sData)
    { try 
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
        System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding(); 
        System.Text.Decoder utf8Decode = encoder.GetDecoder();
        byte[] todecode_byte = Convert.FromBase64String(sData);
        int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length); 
        char[] decoded_char = new char[charCount];
        utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
        string result = new String(decoded_char); return result; 
    }

    protected void ddl_ICrole_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_ICrole.SelectedValue == "BM")
        {
            lbldist.Visible = true;
            lblissue.Visible = true;
            lbloper.Visible = false;
            DDLOperetorId.Visible = false;
            ddldistrict.Visible = true;
            ddlissue.Visible = true;

        }

        else if (ddl_ICrole.SelectedValue == "OP")
        {
            lbldist.Visible = true;
            lblissue.Visible = true;
            lbloper.Visible = true;
            DDLOperetorId.Visible = true;
            ddldistrict.Visible = true;
            ddlissue.Visible = true;
         
        }
        else if (ddl_ICrole.SelectedValue == "DMO")
        {
            GetDist();

            lbldist.Visible = true;
            lblissue.Visible = true;
            lbloper.Visible = false;
            DDLOperetorId.Visible = false;
            ddldistrict.Visible = true;
            ddlissue.Visible = true;
        }
        else
        {
            lbldist.Visible = false;
            lblissue.Visible = false;
            lbloper.Visible = false;
            DDLOperetorId.Visible = false;
            ddldistrict.Visible = false;
            ddlissue.Visible = false;
        }

    }

    protected void ddl_DMrole_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_DMrole.SelectedValue == "DM")
        {
            lbldmdist.Visible = true;
            ddldistoff.Visible = true;

            lbldmop.Visible = false;
            ddl_DMoperatorID.Visible = false;

        }
        else if (ddl_DMrole.SelectedValue == "OP")
        {
            lbldmdist.Visible = true;
            ddldistoff.Visible = true;

            lbldmop.Visible = true;
            ddl_DMoperatorID.Visible = true;

        }
        else if (ddl_DMrole.SelectedValue == "DMO")
        {

            GetDistOff();
            lbldmdist.Visible = true;
            ddldistoff.Visible = true;

            lbldmop.Visible = false;
            ddl_DMoperatorID.Visible = false;

        }

        else
        {
            lbldmdist.Visible = false;
            ddldistoff.Visible = false;

            lbldmop.Visible = false;
            ddl_DMoperatorID.Visible = false;

        }
    }

    protected void ddlissue_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetOPid();
    }
    private void GetOPid()
    {
        DDLOperetorId.Items.Clear();
        string strsql = "Select OperatorID,OperatorName from Operator_Registration where District_ID='" + ddldistrict.SelectedValue.ToString() + "' and DepotID='" + ddlissue.SelectedValue.ToString() + "' and Status = 'Y'";
        objDr = new DataReader(ComObj);
        DataSet ds = objDr.selectAny(strsql);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DDLOperetorId.DataSource = ds;
            DDLOperetorId.DataTextField = "OperatorID";
            DDLOperetorId.DataValueField = "OperatorID";
            DDLOperetorId.DataBind();
            DDLOperetorId.Items.Insert(0, "--Select--");
        }

        else
        {
            
        }

    }

    private void GetOPidDist()
    {
        ddl_DMoperatorID.Items.Clear();
        string strsql = "Select OperatorID,OperatorName from Operator_Registration where District_ID='" + ddldistoff.SelectedValue.ToString() + "' and OperatorType='DM'";
        objDr = new DataReader(ComObj);
        DataSet ds = objDr.selectAny(strsql);
        if (ds.Tables[0].Rows.Count > 0)
        {


            ddl_DMoperatorID.DataSource = ds;
            ddl_DMoperatorID.DataTextField = "OperatorID";
            ddl_DMoperatorID.DataValueField = "OperatorID";
            ddl_DMoperatorID.DataBind();
            ddl_DMoperatorID.Items.Insert(0, "--Select--");
        }

        else
        {

        }

    }

    protected void ddl_DMoperatorID_SelectedIndexChanged(object sender, EventArgs e)
    {
        qry = "SELECT Password , Masterpassword FROM dbo.Operator_Registration where District_ID='" + ddldistoff.SelectedValue.ToString() + "'and OperatorID='" + ddl_DMoperatorID.SelectedItem.Text.ToString() + "'";

        Label6.Text = qry;
    }

    protected void DDLOperetorId_SelectedIndexChanged(object sender, EventArgs e)
    {
        qry = "SELECT Password , Masterpassword FROM dbo.Operator_Registration where District_ID='" + ddldistrict.SelectedValue.ToString() + "'and DepotID='" + ddlissue.SelectedValue + "' and OperatorID='" + DDLOperetorId.SelectedItem.Text.ToString() + "'";

        Label6.Text = qry;
    }

    protected void ddldistoff_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetOPidDist();
    }

    protected void InsertOperatorLogin(string opid )
    {
        string browser = Request.Browser.Browser.ToString();
        string version = Request.Browser.Version.ToString();
        string useragent = browser + version;

        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

       // string OPID = DDLOperetorId.SelectedItem.Text.ToString();

        string strSql = "INSERT INTO  Operator_Log (OperatorID ,Login_Date ,Ip_Address ,UserAgent) Values('" + opid + "',getDate(),'" + ip + "','" + useragent + "')";

        try
        {
            con.Open();
            cmd = new SqlCommand(strSql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");

        }
        finally
        {

        }
        
    }

    protected void ddl_Collector_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void ddl_dist_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
}
