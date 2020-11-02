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
using System.Net;

public partial class District_ChangePassword_Coll_DIO : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    MoveChallan mobj = null;
    chksql chk = null;
    protected Common ComObj = null, cmn = null;
    public string distid_Coll = "";
    public string distid_DIO = "";
    public string tid = "";
    public string tname = "";
    public string gatepass = "";
    public int getnum;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Master.SetPanel1.GetType();
            //Master.SetPanel2.GetType();

            if (Session["Collector/DIO"] == "Collector")
            {
                Master.BodyTitle = "Collector";
            }

            else if (Session["Collector/DIO"] == "DIO")
            {
                Master.BodyTitle = "DIO";
            }

            Master.Name = Session["dist_name"].ToString();
        }

         if (Session["dist_id"] != null)
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

            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
         }

        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
            string browser = Request.Browser.Browser.ToString();
            string version = Request.Browser.Version.ToString();
            string useragent = browser + version;
            string host = Request.ServerVariables["REMOTE_HOST"].ToString();

            string[] computer_name = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName.Split(new Char[] { '.' });
            String ecn = System.Environment.MachineName;
            string HostName = computer_name[0].ToString();


            // string useragent = Request.ServerVariables["HTTP_USER_AGENT"].;
            string strHostName = "";
            strHostName = Dns.GetHostName();

            IPHostEntry ipEntry = Dns.GetHostByName(HostName);
            IPAddress[] addr = ipEntry.AddressList;


            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

            if (Session["Collector/DIO"] == "Collector")
            {
                distid_Coll = Session["dist_id"].ToString();

                string qry = "SELECT Password FROM dbo.CollectorLogin where District_ID='" + distid_Coll + "'";

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

                        string qryupdate = "Update dbo.CollectorLogin set Password='" + mnewpwd + "' where District_ID='" + distid_Coll + "'";

                        string qryins = "Insert into dbo.Login_MP_Log(Login_ID,Password,updated_By,updated_date,HostName,DynamicIP,UserAgent,Role)values('" + distid_Coll + "','" + mnewpwd + "','" + ip + "',getdate(),'" + HostName + "','" + addr[0].ToString() + "','" + useragent + "','Collector')";

                        cmd.Connection = con;
                        cmd.CommandText = qryupdate;
                        con.Open();

                        try
                        {

                            cmd.ExecuteNonQuery();
                            cmd.CommandText = qryins;
                            cmd.ExecuteNonQuery();
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Password Changed Successfully.....'); </script> ");
                            btnadd.Enabled = false;

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
            }

            else if (Session["Collector/DIO"] == "DIO")
            {
                distid_DIO = Session["dist_id"].ToString();

                string qry = "SELECT Password FROM dbo.DIOLogin where District_ID='" + distid_DIO + "'";

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

                        string qryupdate = "Update dbo.DIOLogin set Password='" + mnewpwd + "' where District_ID='" + distid_DIO + "'";

                        string qryins = "Insert into dbo.Login_MP_Log(Login_ID,Password,updated_By,updated_date,HostName,DynamicIP,UserAgent,Role)values('" + distid_DIO + "','" + mnewpwd + "','" + ip + "',getdate(),'" + HostName + "','" + addr[0].ToString() + "','" + useragent + "','DIO')";

                        cmd.Connection = con;
                        cmd.CommandText = qryupdate;
                        con.Open();

                        try
                        {

                            cmd.ExecuteNonQuery();
                            cmd.CommandText = qryins;
                            cmd.ExecuteNonQuery();
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Password Changed Successfully.....'); </script> ");
                            btnadd.Enabled = false;

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
            }
        }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/District/Collector_DIO.aspx");
    }
}