﻿ using System;
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
using System.Net;

public partial class CSMSSurveyorLogin_GodownSurveyor_ChangePassword : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    MoveChallan mobj = null;
    chksql chk = null;
    protected Common ComObj = null, cmn = null;
    public string Username = "";
    public string tid = "";
    public string tname = "";
    public string gatepass = "";
    public int getnum;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userGodown"] != null)
        {
            if (!IsPostBack)
            {
                Username = Session["userGodown"].ToString();
                txtoldpassword.Attributes.Add("onkeypress", "return checksqlkey_psw(event,this)");
                txtoldpassword.Attributes.Add("onKeyUp", "return taCount(this,'myCounter')");
                txtoldpassword.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtoldpassword.Attributes.Add("onchange", "return chksqltxt_psw(this);");

                txtnewpassword.Attributes.Add("onkeypress", "return checksqlkey_psw(event,this)");
                txtnewpassword.Attributes.Add("onKeyUp", "return taCount(this,'myCounter')");
                txtnewpassword.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtnewpassword.Attributes.Add("onchange", "return chksqltxt_psw(this);");

                txtconpassword.Attributes.Add("onkeypress", "return checksqlkey_psw(event,this)");
                txtconpassword.Attributes.Add("onKeyUp", "return taCount(this,'myCounter')");
                txtconpassword.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtconpassword.Attributes.Add("onchange", "return chksqltxt_psw(this);");


                ArrayList ctrllist = new ArrayList();
                ctrllist.Add(txtoldpassword.Text);
                ctrllist.Add(txtnewpassword.Text);
                ctrllist.Add(txtconpassword.Text);
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


                ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

                if (Page.IsPostBack == false)
                {

                }
            }
        }
        else
        {
            Response.Redirect("~/CSMSSurveyorLogin/SurveyorLogin_Godown.aspx");
        }
    }
    protected void bttsub_Click(object sender, EventArgs e)
    {
        //string browser = Request.Browser.Browser.ToString();
        //string version = Request.Browser.Version.ToString();
        //string useragent = browser + version;
        //string host = Request.ServerVariables["REMOTE_HOST"].ToString();

        //string[] computer_name = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName.Split(new Char[] { '.' });
        //String ecn = System.Environment.MachineName;
        //string HostName = computer_name[0].ToString();


        //// string useragent = Request.ServerVariables["HTTP_USER_AGENT"].;
        //string strHostName = "";
        //strHostName = Dns.GetHostName();

        //IPHostEntry ipEntry = Dns.GetHostByName(HostName);
        //IPAddress[] addr = ipEntry.AddressList;


        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        Username = Session["userGodown"].ToString();
        string qry = "SELECT Password FROM SMSCom_SurveyorMaster where MobNum='" + Username + "' and GETDATE()>=Fromdate and GETDATE()<=Todate";

        SqlCommand cmd = new SqlCommand(qry, con);

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();

        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {

        }
        else
        {
            DataRow drp = ds.Tables[0].Rows[0];
            string mpwd = drp["Password"].ToString();
            string moldpwd = txtoldpassword.Text;
            string mnewpwd = txtnewpassword.Text;
            if (mpwd == moldpwd)
            {

                string qryupdate = "Update  SMSCom_SurveyorMaster set Password='" + mnewpwd + "' where MobNum='" + Username + "'";

               // string qryins = "Insert into dbo.Login_MP_Log(Login_ID,Password,updated_By,updated_date,HostName,DynamicIP,UserAgent,Role)values('" + Username + "','" + mnewpwd + "','" + ip + "',getdate(),'" + HostName + "','" + addr[0].ToString() + "','" + useragent + "','DM')";

                cmd.Connection = con;
                cmd.CommandText = qryupdate;
                con.Open();


                //SqlTransaction trns;
                //trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                //cmd.Transaction = trns;
                try
                {

                    cmd.ExecuteNonQuery();
                    //cmd.CommandText = qryins;
                    //cmd.ExecuteNonQuery();
                    //trns.Commit();
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Password Changed Successfully.....'); </script> ");
                    Session.Abandon();
                    Session.RemoveAll();
                    Response.Redirect("~/CSMSSurveyorLogin/SurveyorLogin_Godown.aspx");
                    bttsub.Enabled = false;

                }
                catch (Exception ex)
                {
                    //trns.Rollback();
                    // Label1.Text = ex.Message;
                }
                finally
                {
                    con.Close();

                }



            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Your Old Password is wrong'); </script> ");
                txtoldpassword.Focus();

            }

        }
    }
}