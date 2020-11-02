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

public partial class District_Change_Password_Dist : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
     MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    public string distid = "";
    public string tid = "";
    public string tname = "";
    public string gatepass = "";
    public int getnum;
    protected void Page_Load(object sender, EventArgs e)
    {
        txtoldpwd.Attributes.Add("onkeypress", "return CheckPassword(event,this),taLimit(this)");
        txtoldpwd.Attributes.Add("onKeyUp", "return taCount(this,'myCounter')");

        txtnewpwd.Attributes.Add("onkeypress", "return CheckPassword(event,this),taLimit(this)");
        txtnewpwd.Attributes.Add("onKeyUp", "return taCount(this,'myCounter')");

        txtconfpwd.Attributes.Add("onkeypress", "return CheckPassword(event,this),taLimit(this)");
        txtconfpwd.Attributes.Add("onKeyUp", "return taCount(this,'myCounter')");
        if (Session["dist_id"] != null)
        {
            distid = Session["dist_id"].ToString();
          
                   

            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

            if (Page.IsPostBack == false)
            {
              
            }


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

        string qry = "SELECT Password FROM dbo.DM_Mpscsc_Login where District_ID='" + distid + "'";
        mobj = new MoveChallan(ComObj);
        DataSet ds = mobj.selectAny(qry);
         if (ds.Tables[0].Rows.Count==0)
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

                string qryupdate = "Update  dbo.DM_Mpscsc_Login set Password='" + mnewpwd + "' where District_ID='" + distid + "'";
                string qryins = "Insert into  dbo.Login_MP_Log(Login_ID,Password,updated_By,updated_date,HostName,DynamicIP,UserAgent,Role)values('" + distid + "','" + mnewpwd + "','" + ip + "',getdate(),'" + HostName  + "','" + addr[0].ToString() + "','" + useragent + "','DF')";
                cmd.Connection = con;
                cmd.CommandText = qryupdate;
                try
                {
                    con.Open();
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
    protected void btnclose_Click(object sender, EventArgs e)
    {
        
        ComObj.CloseConnection();


        Response.Redirect("~/DistrictFood/DO_Welcome.aspx");
    }
}
