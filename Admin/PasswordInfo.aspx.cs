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
public partial class Admin_PasswordInfo : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    public string distid = "";
    public string tid = "";
    public string adminid = "";
    public string gatepass = "";
    public string getdatef;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {
            adminid = Session["st_id"].ToString();
            
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

            if (Page.IsPostBack == false)
            {
                GetLogin();
               

            }


        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

    }
    void GetLogin()
    {
        mobj = new MoveChallan(ComObj);
        string qry = "select * from dbo.tbl_MetaData_LoginType";
        DataSet ds = mobj.selectAny(qry);
        ddllogintype.DataSource = ds;
        ddllogintype.DataTextField = "Login_Type";
        ddllogintype.DataValueField = "Login_ID";
        ddllogintype.DataBind();
        ddllogintype.Items.Insert(0, "--Select--");


    }
    protected void btnGet_Click(object sender, EventArgs e)
    {
        if (ddllogintype.SelectedValue == "1")
        {
            string qry = "SELECT Login_MP_Log.*,tbl_MetaData_DEPOT.DepotName as Name FROM Login_MP_Log Left JOIN tbl_MetaData_DEPOT ON Login_MP_Log.Login_ID = tbl_MetaData_DEPOT.DepotID  where Login_MP_Log.Role='IC'";
            fillgrid(qry);
        }
        else if (ddllogintype.SelectedValue == "2")
        {
            string qry = "SELECT Login_MP_Log.*,districtsmp.district_name as Name FROM Login_MP_Log Left JOIN pds.districtsmp ON Login_MP_Log.Login_ID = pds.districtsmp.district_code  where Login_MP_Log.Role='DM'";
            fillgrid(qry);
        }
        else if (ddllogintype.SelectedValue == "3")
        {
            string qry = "SELECT Login_MP_Log.*,districtsmp.district_name as Name FROM Login_MP_Log Left JOIN pds.districtsmp ON Login_MP_Log.Login_ID = pds.districtsmp.district_code  where Login_MP_Log.Role='DF'";
            fillgrid(qry);
        }
        else if (ddllogintype.SelectedValue == "4")
        {
            string qry = "SELECT Login_MP_Log.*,division.division as Name FROM Login_MP_Log Left JOIN dbo.division ON Login_MP_Log.Login_ID = division.division_code  where Login_MP_Log.Role='RM'";
            fillgrid(qry);
        }
        else if (ddllogintype.SelectedValue == "5")
        {
            string qry = "SELECT Login_MP_Log.*,State_Login.User_Name as Name FROM Login_MP_Log Left JOIN State_Login ON Login_MP_Log.Login_ID = State_Login.User_ID  where Login_MP_Log.Role='HO'";
            fillgrid(qry);
        }
        else if (ddllogintype.SelectedValue == "7")
        {
            string qry = "SELECT Login_MP_Log.*,districtsmp.district_name as Name FROM Login_MP_Log Left JOIN pds.districtsmp ON Login_MP_Log.Login_ID = pds.districtsmp.district_code  where Login_MP_Log.Role='DCCB'";
            fillgrid(qry);
        }
       
    }
    void fillgrid( string qry)
    {
       
        string logtype = ddllogintype.SelectedValue;
        mobj = new MoveChallan(ComObj);
        //string qry = "SELECT User_LogTime.user_id,User_LogTime.IP_Address,User_LogTime.Login_Date, User_LogTime.Login_Time, User_LogTime.Logout_Time, User_LogTime.offline,tbl_MetaData_DEPOT.DepotName FROM User_LogTime Left JOIN tbl_MetaData_DEPOT ON User_LogTime.user_id = tbl_MetaData_DEPOT.DepotID  where Login_Date>='" + fromdae + "'and Login_Date <='" + todate + "'and User_LogTime.Login_Type='" + logtype + "' order by User_LogTime.offline";
        DataSet ds = mobj.selectAny(qry);
        if (ds.Tables[0].Rows.Count == 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "No Record Present Currently";
            
        }
        else
        {
            lblmsg.Visible = false;
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }
    }
   
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    public void FooterPagerClick(object sender, CommandEventArgs e)
    {
        //Used by external paging
        if (GridView1.PageCount == 0)
        {
        }
        else
        {
            string arg;
            arg = e.CommandArgument.ToString();

            switch (arg)
            {
                case "next":
                    //The next Button was Clicked
                    if ((GridView1.PageIndex < (GridView1.PageCount - 1)))
                    {
                        GridView1.PageIndex += 1;
                    }

                    break;

                case "prev":
                    //The prev button was clicked
                    if ((GridView1.PageIndex > 0))
                    {
                        GridView1.PageIndex -= 1;
                    }

                    break;

                case "last":
                    //The Last Page button was clicked
                    GridView1.PageIndex = (GridView1.PageCount - 1);
                    break;

                default:
                    //The First Page button was clicked
                    GridView1.PageIndex = Convert.ToInt32(arg);
                    break;
            }
           // fillgrid();
        }
    }
   
    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
  
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalamt"));

            string griddate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Login_Date"));
            string status = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "offline"));


            getdatef = getdate(griddate);


            Label lbl = (Label)e.Row.FindControl("lblChallan");
            lbl.Text = getdatef;
            Label lblst = (Label)e.Row.FindControl("lblstatus");
            if (status == "True")
            {
                lblst.Text = "Offline";
            }
            else
            {
                lblst.BackColor = System.Drawing.Color.Green;
                lblst.ForeColor = System.Drawing.Color.Green;
                lblst.Text = "Online";
            }

            //decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalamt"));
            //decimal rowTotalQty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "quantity"));

            //grdTotal = grdTotal + rowTotal;
            //grdTotalQty = grdTotalQty + rowTotalQty;

        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/Admin/AdminWelcome.aspx");
    }
}
