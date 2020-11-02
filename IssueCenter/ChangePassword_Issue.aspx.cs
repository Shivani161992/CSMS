using System;
using System.Net;

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

public partial class IssueCenter_ChangePassword_Issue : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    public string distid = "";
    public string tid = "";
    public string issueid = "";
    public string gatepass = "";
    public int getnum;
    chksql chk = null;
    public string version = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        txtoldpwd.Attributes.Add("onkeypress", "return checksqlkey_psw(event,this)");
        txtoldpwd.Attributes.Add("onKeyUp", "return taCount(this,'myCounter')");
        txtoldpwd.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        txtoldpwd.Attributes.Add("onchange", "return chksqltxt_psw(this);");

        txtnewpwd.Attributes.Add("onkeypress", "return checksqlkey_psw(event,this)");
        txtnewpwd.Attributes.Add("onKeyUp", "return taCount(this,'myCounter')");
        txtnewpwd.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        txtnewpwd.Attributes.Add("onchange", "return chksqltxt_psw(this);");

        txtconfpwd.Attributes.Add("onkeypress", "return checksqlkey_psw(event,this)");
        txtconfpwd.Attributes.Add("onKeyUp", "return taCount(this,'myCounter')");
        txtconfpwd.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        txtconfpwd.Attributes.Add("onchange", "return chksqltxt_psw(this);");


        ArrayList ctrllist = new ArrayList();
        ctrllist.Add(txtoldpwd.Text);
        ctrllist.Add(txtnewpwd.Text);
        ctrllist.Add(txtconfpwd.Text);
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




        //txtpwd.Attributes.Add("onkeypress", "return checksqlkey_psw(event,this)");
       
        //txtpwd.Attributes.Add("onKeyUp", "return taCount(this,'myCounter')");
        //txtpwd.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        //txtpwd.Attributes.Add("onchange", "return chksqltxt_psw(this),MD5(this);");
      

        if (Session["issue_id"] != null)
        {    
            distid = Session["dist_id"].ToString();
            issueid = Session["issue_id"].ToString();
            version = Session["hindi"].ToString();
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
            if (Page.IsPostBack == false)
            {
                if (version == "H")
                {
                    lblChangePassword.Text = Resources.LocalizedText.lblChangePassword;
                    lblOldPassword.Text = Resources.LocalizedText.lblOldPassword;
                    lblNewPassword.Text = Resources.LocalizedText.lblNewPassword;
                    lblConfirmPassword.Text  = Resources.LocalizedText.lblConfirmPassword;
                    btnsave.Text = Resources.LocalizedText.btnsave;
                    btnclose.Text = Resources.LocalizedText.btnclose;

                }

            }

        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }


    }
   
    protected void btnclose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        string opid = Session["OperatorId"].ToString();

        if (opid == "BM")
        {

            # region BMUpdate
            // BM Update

            string browser= Request.Browser.Browser.ToString ();
       string version = Request.Browser.Version.ToString();
       string useragent = browser + version;
       string host = Request.ServerVariables["REMOTE_HOST"].ToString();

       string[] computer_name = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName.Split(new Char[] { '.' });
       String ecn = System.Environment.MachineName;
       string HostName = computer_name[0].ToString();
      
        string  strHostName = "";
        strHostName = Dns.GetHostName();

        IPHostEntry ipEntry = Dns.GetHostByName(HostName);
        IPAddress[] addr = ipEntry.AddressList;

        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string qry = "SELECT Password FROM dbo.DCLogin_MP where District_ID='23" + distid + "' and DC_ID='" + issueid + "'";
        mobj = new MoveChallan(ComObj);
        DataSet ds = mobj.selectAny(qry);

        if (ds.Tables[0].Rows.Count == 0)
        {

        }
        else
        {
            DataRow drp = ds.Tables[0].Rows[0];
            string mpwd = drp["Password"].ToString();
            string moldpwd = txtoldpwd.Text;
            string mnewpwd = txtnewpwd.Text;

            if (mpwd == moldpwd)
            {

                string qryupdate = "Update  dbo.DCLogin_MP set Password='" + mnewpwd + "' where District_ID='23" + distid + "' and DC_ID='" + issueid + "'";

                string qryins = "Insert into  dbo.Login_MP_Log(Login_ID,Password,updated_By,updated_date,HostName,DynamicIP,UserAgent,Role)values('" + issueid + "','" + mnewpwd + "','" + ip + "',getdate(),'" + HostName + "','" + addr[0].ToString() + "','" + useragent + "','IC')";
                cmd.Connection = con;
                cmd.CommandText = qryupdate;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = qryins;
                    cmd.ExecuteNonQuery();

                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Password Changed Successfully.....'); </script> ");
                    btnsave.Enabled = false;

                }
                catch (Exception ex)
                {
                    Label1.Text = ex.Message;
                }
                finally
                {
                    con.Close();
                    ComObj.CloseConnection();
                }
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Your Old Password is wrong'); </script> ");
                txtoldpwd.Focus();

            }

        }
            # endregion

        }

        else
        {
            # region OP Update
            // Op Update

            string browser = Request.Browser.Browser.ToString();
            string version = Request.Browser.Version.ToString();
            string useragent = browser + version;
            string host = Request.ServerVariables["REMOTE_HOST"].ToString();

            string[] computer_name = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName.Split(new Char[] { '.' });
            String ecn = System.Environment.MachineName;
            string HostName = computer_name[0].ToString();

            string strHostName = "";
            strHostName = Dns.GetHostName();

            IPHostEntry ipEntry = Dns.GetHostByName(HostName);
            IPAddress[] addr = ipEntry.AddressList;

            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string qry = "SELECT password FROM dbo.Operator_Registration where District_ID='" + distid + "' and DepotID='" + issueid + "' and OperatorID = '" + opid + "'";
            mobj = new MoveChallan(ComObj);
            DataSet ds = mobj.selectAny(qry);

            if (ds.Tables[0].Rows.Count == 0)
            {

            }
            else
            {
                DataRow drp = ds.Tables[0].Rows[0];
                string mpwd = drp["password"].ToString();
                string moldpwd = txtoldpwd.Text;
                string mnewpwd = txtnewpwd.Text;

                if (mpwd == moldpwd)
                {

                    string qryupdate = "Update  dbo.Operator_Registration set password='" + mnewpwd + "' where District_ID='" + distid + "' and DepotID='" + issueid + "' and OperatorID = '" + opid + "'";

                    string qryins = "Insert into  dbo.Login_MP_Log(Login_ID,Password,updated_By,updated_date,HostName,DynamicIP,UserAgent,Role)values('" + opid + "','" + mnewpwd + "','" + ip + "',getdate(),'" + HostName + "','" + addr[0].ToString() + "','" + useragent + "','IC')";
                    cmd.Connection = con;
                    cmd.CommandText = qryupdate;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = qryins;
                        cmd.ExecuteNonQuery();

                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Password Changed Successfully.....'); </script> ");
                        btnsave.Enabled = false;

                    }
                    catch (Exception ex)
                    {
                        Label1.Text = ex.Message;
                    }
                    finally
                    {
                        con.Close();
                        ComObj.CloseConnection();
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Your Old Password is wrong'); </script> ");
                    txtoldpwd.Focus();

                }

            }


            # endregion

        }

       

    }
}
