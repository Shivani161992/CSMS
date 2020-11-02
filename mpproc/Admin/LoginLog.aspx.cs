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

public partial class mpproc_Admin_LoginLog : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_mpproc"].ToString());
    SqlCommand cmd = new SqlCommand();
    protected Common ComObj = null, cmn = null;
    public string qry = "";
    SqlString SqlObj = null;
    DataSet ds = null;
    public string getdatef;
    public string getTimef;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["admin"] != null)
        {

            ComObj = new Common(ConfigurationSettings.AppSettings["ConnectionString_mpproc"].ToString());


        }
        else {


            Response.Redirect("../../mpproc/frmLogin.aspx");
        
        }

    }
    protected void btn_Get_Click(object sender, EventArgs e)
    {

        fillgridLogDetails();

    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    private void fillgridLogDetails()
    {

       ds = new DataSet();

        try
        {
            if (Session["admin"] != null && (txt_from.Text != "" || txt_Todate.Text != ""))
            {

                if (Convert.ToDateTime(txt_Todate.Text) >= Convert.ToDateTime(txt_from.Text))
                {
                    string fromdate=getDate_MDY(txt_from.Text);
                    string todate = getDate_MDY(txt_Todate.Text);

                    string str = "SELECT  distinct  login.User_Name as User_Name,IP_Address,Login_Date,UserAgent,Logout_Time FROM User_LogTime,login where User_LogTime.Loginid=Login.Login_ID and User_LogTime.Login_Date BETWEEN  '" + fromdate + "' and '" + todate + "' order by Login_Date desc";
                  
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                  
                    SqlDataAdapter da = new SqlDataAdapter(str, con);
                    da.Fill(ds, "tab1");
                    if (ds == null)
                    {

                    }
                    else
                    {
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {

                            GridView1.DataSource = ds.Tables[0];
                            GridView1.DataBind();



                        }
                        else {

                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record Not found..'); </script> ");
                        
                        }
                    }
                }
                else {

                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('from Date is not less then Todate'); </script> ");
                
                }
            }
        }
        catch (Exception ex)
        {
            DivMsg.InnerText = ex.Message;

        }
        finally
        {

            ds = null;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }


        }


    }

    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }

    public string getTime(string DTime)
    {
        return Convert.ToDateTime(DTime).ToString("hh:mm:ss tt");
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            string griddate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Login_Date"));
            getdatef = getdate(griddate);

            string gridTime = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Login_Date"));
            getTimef = getTime(gridTime);

            Label lblDate = (Label)e.Row.FindControl("lbl_LogDate");
            lblDate.Text = getdatef;

            Label lblTime = (Label)e.Row.FindControl("lbl_LogTime");
            lblTime.Text = getTimef;

        

        }
        
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgridLogDetails();
    }
}
