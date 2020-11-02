using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Data;
using DataAccess;
using System.Data.SqlClient;
public partial class District_OperatorRegistration : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    MoveChallan mobj = null;
    DataReader objDr = null; 
    DataSet ds = null;
    public  string strDistrict_ID = "", strDepotID = "";
    protected Common ComObj = null, cmn = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        strDistrict_ID = Session["dist_id"].ToString();
        if (strDistrict_ID != "")
        {

            strDistrict_ID = Session["dist_id"].ToString();
            //strDepotID = Session["issue_id"].ToString();

            //LinkbtnnewOp.Click += new EventHandler(LinkbtnnewOp_Click);
            //btnCancal.Click += new EventHandler(btnCancal_Click);
            //btnLogin.Click += new EventHandler(btnLogin_Click);
           
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
         
           
            //btnSubmit.Click += new EventHandler(btnSubmit_Click);

            txtOfficNo.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtOfficNo.Attributes.Add("onchange", "return CheckIsNumericMobile(this)");

            txtOp_Mobile.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtOp_Mobile.Attributes.Add("onchange", "return CheckIsNumericMobile(this)");

            txtBr_MobileNO.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtBr_MobileNO.Attributes.Add("onchange", "return CheckIsNumericMobile(this)");

            txtOp_Name.Attributes.Add("onkeypress", "return CheckIsChar(this)");
            txtpwd.Attributes.Add("onkeypress", "return Checkpasswaordlength(this)");
            txtConPwd.Attributes.Add("onkeypress", "return Checkpasswaordlength(this)");

            if (!IsPostBack)
            {
                
                if (Session["DistrictManager"] != null)
                {
                    GetOPidForGird();
                    if (gdview_OperatorDist.Rows.Count > 0)
                    {
                        PanelOP_Reg.Visible = true;
                        PanelMessage.Visible = false;
                        PanelNoOper.Visible = false;
                        PanelOP_Login.Visible = false;
                    }
                    else
                    {
                        PanelNoOper.Visible = true;
                        PanelOP_Reg.Visible = true;
                    }
                }
                else
                {
                    DDLOperetorId.SelectedValue = Session["OperatorIDDistrict"].ToString();
                    GetOPid();
                    if (DDLOperetorId.Items.Count > 0)
                    {
                        PanelOP_Reg.Visible = false;
                        PanelMessage.Visible = false;
                        PanelNoOper.Visible = false;
                        PanelOP_Login.Visible = true;
                    }
                    else
                    {
                        PanelNoOper.Visible = true;
                    }
                }
                
            }

        }

    }

    private void GetOPidForGird()
    {
        string strsql = "Select OperatorID,OperatorName from Operator_Registration where District_ID='" + strDistrict_ID + "' and  OperatorType='DM'";
        objDr = new DataReader(ComObj);
        DataSet ds = objDr.selectAny(strsql);
        if (ds == null)
        {
        }
        else
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                gdview_OperatorDist.DataSource = ds.Tables[0];
                gdview_OperatorDist.DataBind();
            }
        }
    }

    void btnLogin_Click(object sender, EventArgs e)
    {
        if (DDLOperetorId.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Invalid Operaot Login ID :'); </script> ");
        }

        else
        {

            string browser = Request.Browser.Browser.ToString();
            string version = Request.Browser.Version.ToString();
            string useragent = browser + version;

            
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string OPID = DDLOperetorId.SelectedItem.Text.ToString();

            
            string strsqllog = "Select * from Operator_Registration where OperatorID='" + DDLOperetorId.SelectedItem.Text.ToString() + "' and Password='" + txtPassword.Text + "'";
            objDr = new DataReader(ComObj);


            DataSet ds = objDr.selectAny(strsqllog);
            if (ds.Tables[0].Rows.Count > 0)
            {

                Session["OPLogIn"] = ds.Tables[0].Rows[0]["OperatorID"].ToString();
                Session["OPName"] = ds.Tables[0].Rows[0]["OperatorName"].ToString();
                


                string strSql = "INSERT INTO  Operator_Log (OperatorID ,Login_Date ,Ip_Address ,UserAgent) Values('" + OPID  + "',getDate(),'" + ip  + "','" + useragent + "')";

                try
                {
                    con.Open();
                    cmd = new SqlCommand(strSql, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                catch (Exception ex)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('"+ex.Message +"'); </script> ");

                }
                finally
                {

                }


                

                Response.Redirect("~/District/Dist_Welcome.aspx");

            }

            else
            {

                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Invalid Login:'); </script> ");


            }
        }

    }

    protected void btnCancal_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/MainLogin.aspx");


    }

    void LinkbtnnewOp_Click(object sender, EventArgs e)
    {


        PanelOP_Reg.Visible = true;
        PanelMessage.Visible = false;


    }

    private void GetOPid()
    {

        string strsql = "Select OperatorID,OperatorName from Operator_Registration where District_ID='" + strDistrict_ID + "' and  OperatorType='DM'";
        objDr=new DataReader(ComObj);
        DataSet ds = objDr.selectAny(strsql);
        if (ds.Tables[0].Rows.Count > 0)
        {
            PanelOP_Login.Visible =true;
            PanelOP_Reg.Visible = false;
            PanelMessage.Visible = false;

            DDLOperetorId.DataSource = ds;
            DDLOperetorId.DataTextField = "OperatorID";
            DDLOperetorId.DataValueField = "OperatorID";
            DDLOperetorId.DataBind();
            DDLOperetorId.Items.Insert(0, "--Select--");
        }

        else 
        {

            PanelOP_Login.Visible = false;
            PanelOP_Reg.Visible = false;
            PanelMessage.Visible = true;
        
        }

    } 

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        //try
        //{
            string gdnid = "";
            int gdnnum = 0;
          
            string strName = "", strOpMobNo = "", stremail = "", strAddress = "", strBrhMgr = "", strBr_MobNo = "", strOfficNo = "", stractive = "";

            mobj = new MoveChallan(ComObj);

            string qrey = "select max(Ref_No) as Ref_No from dbo.Operator_Registration where District_ID='" + strDistrict_ID + "' and OperatorType='DM'";
            DataSet ds = mobj.selectAny(qrey);
            DataRow dr = ds.Tables[0].Rows[0];
            gdnid = dr["Ref_No"].ToString();
            ComObj.CloseConnection();
            if (gdnid == "")
            {
                gdnid = strDistrict_ID  + "01";


            }
            else
            {

                gdnnum = Int32.Parse(gdnid.ToString());
                gdnnum = gdnnum + 1;
                gdnid = gdnnum.ToString();


            }

            string strRefid = gdnid;

            string strpass = "";
            strName = txtOp_Name.Text;
            strOpMobNo = txtOp_Mobile.Text;
            stremail = txtEmail.Text;
            strAddress = txtAddress.Text;
            strBrhMgr = txtBranchMgr.Text;
            strBr_MobNo = txtBr_MobileNO.Text;
            strOfficNo = txtOfficNo.Text;
            stractive = "Y"; 
            strpass = txtpwd.Text;
            string strOPid = "D" +  gdnid;
            string IPAddr = Request.ServerVariables["REMOTE_ADDR"].ToString();


            if (txtOp_Name.Text == "")
            {

                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Name Should Not Be Blank:'); </script> ");

            }
            else if (txtBr_MobileNO.Text == "")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter 10 Digits Mobile No:'); </script> ");


            }

            else if (txtBranchMgr.Text == "")
            {

                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Branch Manager Name:'); </script> ");

            }
            else if (txtpwd.Text == "")
            {

                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Password:'); </script> ");

            }

            else
            {

                string strSql = "INSERT INTO Operator_Registration (District_ID ,DepotID ,OperatorID ,OperatorName ,Mobile ,email ,Adress ,BranchManager ,BM_Contact,OfficeNo,CreatedDate,Ip_Address,Status,Ref_No,Password,OperatorType) Values('" + strDistrict_ID + "','" + strDistrict_ID + "','" + strOPid + "','" + strName + "'," + strOpMobNo + ", '" + stremail + "','" + strAddress + "', '" + strBrhMgr + "'," + strBr_MobNo + ", " + strOfficNo + ",getDate(),'" + IPAddr + "','" + stractive + "','" + strRefid + "','" + strpass + "','DM')";

              
                con.Open();
                cmd = new SqlCommand(strSql, con);
                cmd.ExecuteNonQuery();

                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record Saves successfully.............'); </script> ");

                lblmsg.Text = "Your Id is: " + strOPid;
                btnSubmit.Enabled = false;



               // Response.Redirect("../MainLogin.aspx");
                con.Close();
           

            }

        //}
        //catch (Exception ex)
        //{

        //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('"+ex.Message+"'); </script> ");
        
        
        //}


    }





    protected void linkNewOprator_Click(object sender, EventArgs e)
    {

        PanelOP_Reg.Visible = true;
        PanelMessage.Visible = false;
        PanelOP_Login.Visible = false;


    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MainLogin.aspx");

    }



  
}
