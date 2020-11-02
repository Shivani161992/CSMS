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
public partial class IssueCenter_OperatorRegistration : System.Web.UI.Page
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

        if (Session["issue_id"] != null)
        {

            strDistrict_ID = Session["_Sdcid"].ToString();
            strDepotID = Session["issue_id"].ToString();

          
           
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());         
           
           

            txtOfficNo.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtOfficNo.Attributes.Add("onchange", "return CheckIsNumericMobile(this)");

            txtOp_Mobile.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtOp_Mobile.Attributes.Add("onchange", "return CheckIsNumericMobile(this)");

            txtDob.Attributes.Add("onkeypress", "return Checkkeypress(this);");
            txtBr_MobileNO.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtBr_MobileNO.Attributes.Add("onchange", "return CheckIsNumericMobile(this)");

            txtOp_Name.Attributes.Add("onkeypress", "return CheckIsChar(this)");

            txtpwd.Attributes.Add("onkeypress", "return Checkpasswaordlength(this)");
            txtConPwd.Attributes.Add("onkeypress", "return Checkpasswaordlength(this)");

            if (!IsPostBack)
            {
                //btnSubmit.Click += new EventHandler(btnSubmit_Click);
                //LinkbtnnewOp.Click += new EventHandler(LinkbtnnewOp_Click);
                //btnCancal.Click += new EventHandler(btnCancal_Click);
                //btnLogin.Click += new EventHandler(btnLogin_Click);

                if (Session["BranchManager"] != null)
                {
                    GetOPidForGird();
                    if (gdview_Operator.Rows.Count > 0)
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
                    DDLOperetorId.SelectedValue = Session["OperatorId"].ToString();
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
        string strsql = "Select OperatorID,OperatorName from Operator_Registration where District_ID='" + strDistrict_ID + "' and DepotID='" + strDepotID + "'";
        objDr = new DataReader(ComObj);
        DataSet ds = objDr.selectAny(strsql);
        if (ds == null)
        {
        }
        else
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                gdview_Operator.DataSource = ds.Tables[0];
                gdview_Operator.DataBind();
            }
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (DDLOperetorId.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Invalid Operaot Login ID :'); </script> ");
        }

        else
        {
            
            string operatorid = DDLOperetorId.SelectedValue.ToString();

            if (Session["OperatorId"].ToString() == operatorid)
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




                    string strSql = "INSERT INTO  Operator_Log (OperatorID ,Login_Date ,Ip_Address ,UserAgent) Values('" + OPID + "',getDate(),'" + ip + "','" + useragent + "')";

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
                    Response.Redirect("issue_welcome.aspx");
                }

                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Invalid Login:'); </script> ");
                }
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('You have Selected wrong ID to LogIn'); </script> ");
            }

        }

    }

    protected void btnCancal_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/IssueCenter/issue_welcome.aspx");


    }

    protected void LinkbtnnewOp_Click(object sender, EventArgs e)
    {


        PanelOP_Reg.Visible = true;
        PanelMessage.Visible = false;


    }

    private void GetOPid()
    {

        string strsql = "Select OperatorID,OperatorName from Operator_Registration where District_ID='" + strDistrict_ID + "' and DepotID='" + strDepotID + "'";
        objDr=new DataReader(ComObj);
        DataSet ds = objDr.selectAny(strsql);
        if (ds == null)
        { }
        else
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                //PanelOP_Login.Visible =true;
                //PanelOP_Reg.Visible = false;
                //PanelMessage.Visible = false;

                DDLOperetorId.DataSource = ds;
                DDLOperetorId.DataTextField = "OperatorID";
                DDLOperetorId.DataValueField = "OperatorID";
                DDLOperetorId.DataBind();
                DDLOperetorId.Items.Insert(0, "--Select--");
            }
        }
        //else 
        //{

        //    PanelOP_Login.Visible = false;
        //    PanelOP_Reg.Visible = false;
        //    PanelMessage.Visible = true;
        
        //}

    } 

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        //try
        //{
            string gdnid = "";
            int gdnnum = 0;
          
            string strName = "", strOpMobNo = "", stremail = "", strAddress = "", strBrhMgr = "", strBr_MobNo = "", strOfficNo = "", stractive = "";
            if (txtOfficNo.Text == "")
            {

                txtOfficNo.Text = "0";

            }
            mobj = new MoveChallan(ComObj);

            string qrey = "select max(Ref_No) as Ref_No from dbo.Operator_Registration where District_ID='" + strDistrict_ID + "'and DepotID='" + strDepotID + "' and OperatorType='IC' ";
            DataSet ds = mobj.selectAny(qrey);
            DataRow dr = ds.Tables[0].Rows[0];
            gdnid = dr["Ref_No"].ToString();
            ComObj.CloseConnection();
            if (gdnid == "")
            {
                gdnid = strDepotID + "01";

            }
            else
            {

                gdnnum = Int32.Parse(gdnid.ToString());
                gdnnum = gdnnum + 1;
                gdnid = gdnnum.ToString();


            }

            string strRefid = gdnid;

            string strpass = "";
            strName = txtOp_Name.Text.Trim();
            strOpMobNo = txtOp_Mobile.Text.Trim().ToLower();
            stremail = txtEmail.Text.Trim();
            strAddress = txtAddress.Text.Trim();
            strBrhMgr = txtBranchMgr.Text.Trim();
            strBr_MobNo = txtBr_MobileNO.Text.Trim();
            strOfficNo = txtOfficNo.Text.Trim();
            stractive = "Y";
            strpass = txtpwd.Text.Trim();
            string operatordob = txtDob.Text.Trim();
            string strOPid = "I" + gdnid;
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
            else if (txtDob.Text == "")
            {

                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Operator's Date of Birth'); </script> ");

            }


            else
            {
                try
                {
                    if (con != null)
                    {
                        con.Open();
                        string chkopr = "Select count(*) from Operator_Registration where District_ID='" + strDistrict_ID + "' and DepotID = '" + strDepotID + "' and OperatorDob = '" + getDate_MDY(operatordob) + "' and OperatorName='" + strName + "'";
                        cmd = new SqlCommand(chkopr, con);
                        int res = int.Parse(cmd.ExecuteScalar().ToString());
                        if (res <= 0)
                        {

                            string strSql = "INSERT INTO Operator_Registration (District_ID ,DepotID ,OperatorID ,OperatorName ,Mobile ,email ,Adress,OperatorDob ,BranchManager ,BM_Contact,OfficeNo,CreatedDate,Ip_Address,Status,Ref_No,Password,OperatorType) Values('" + strDistrict_ID + "','" + strDepotID + "','" + strOPid + "','" + strName + "'," + strOpMobNo + ", '" + stremail + "','" + strAddress + "','" + getDate_MDY(operatordob) + "', '" + strBrhMgr + "'," + strBr_MobNo + ", " + strOfficNo + ",getDate(),'" + IPAddr + "','" + stractive + "','" + strRefid + "','" + strpass + "','IC')";
                            cmd = new SqlCommand(strSql, con);
                            cmd.ExecuteNonQuery();
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Operator Registered Successfully.............'); </script> ");
                            lblmsg.Text = "Your Id is: " + strOPid;
                            btnSubmit.Enabled = false;
                            GetOPidForGird();                           
                            
                            // Response.Redirect("../MainLogin.aspx");
                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Operator Already Registered.............'); </script> ");
                        }
                        txtOp_Name.Text = "";
                        txtAddress.Text = "";
                        txtBr_MobileNO.Text = "";
                        txtBranchMgr.Text = "";
                        txtDob.Text = "";
                        txtEmail.Text = "";
                        txtOfficNo.Text = "";
                        txtOp_Mobile.Text = "";
                    }

                }
                catch (Exception ex)
                {
                    lblmsg.Text = "Some error occured Please Try Again later";
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
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




    protected void LinkButton1_Click(object sender, EventArgs e)
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
