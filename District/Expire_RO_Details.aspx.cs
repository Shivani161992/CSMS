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

public partial class District_Expire_RO_Details : System.Web.UI.Page

{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    MoveChallan mobj = null;
    Districts DObj = null;
    protected Common ComObj = null, cmn = null;
    public string distid = "";
    public string tid = "";
    public string adminid = "";
    public string gatepass = "";
    public string getdatef;
    public string validdate;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            distid = Session["dist_id"].ToString();
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

            if (Page.IsPostBack == false)
            {
                fillgrid();
                //GetDist();

            }


        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

    }
    //void GetDist()
    //{
    //    DObj = new Districts(ComObj);
    //    DataSet ds = DObj.selectAll(" order by district_name");
        
    //    ddldistrict.DataSource = ds.Tables[0];
    //    ddldistrict.DataTextField = "district_name";
    //    ddldistrict.DataValueField = "District_Code";

    //    ddldistrict.DataBind();
    //    ddldistrict.Items.Insert(0, "--Select--");
    //}
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
            fillgrid();
        }
    }
    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalamt"));

            string rodate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "RO_date"));
            string validity = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "RO_Validity"));


            getdatef = getdate(rodate);
            validdate = getdate(validity);

            Label lbl = (Label)e.Row.FindControl("lblChallan");
            lbl.Text = getdatef;
            Label lblst = (Label)e.Row.FindControl("lbrovalidity");
            lblst.Text = validdate;
          
        }
    }
    void fillgrid()
    {
        string date = DateTime.Today.Date.ToString("MM/dd/yyyy");
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT RO_of_FCI.*,districtsmp.district_name from dbo.RO_of_FCI left join pds.districtsmp on RO_of_FCI.Distt_Id=districtsmp.district_code where RO_of_FCI.RO_Validity <'" + date + "' and RO_of_FCI.Distt_Id='" + distid + "' and RO_of_FCI.RO_No <> 'NoRO' order by RO_of_FCI.RO_date Desc";
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
            //GridView1.Columns[0].Visible = false;
        }
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    //protected void btnGet_Click(object sender, EventArgs e)
    //{
    //    GridView1.Visible = false;
    //    string distcode = ddldistrict.SelectedValue;
    //    mobj = new MoveChallan(ComObj);
    //    string qryd = "SELECT RO_of_FCI.*,districtsmp.district_name from dbo.RO_of_FCI left join pds.districtsmp on RO_of_FCI.Distt_Id=districtsmp.district_code   where RO_of_FCI.IsExpire='Y' and RO_of_FCI.Distt_Id='"+distcode +"'";
    //    DataSet ds = mobj.selectAny(qryd);
    //    if (ds.Tables[0].Rows.Count == 0)
    //    {
    //        lblmsg.Visible = true;
    //        lblmsg.Text = "No Record Present Currently";

    //    }
    //    else
    //    {
    //        GridView1.Visible = true;
    //        lblmsg.Visible = false;
    //        GridView1.DataSource = ds.Tables[0];
    //        GridView1.DataBind();
           
    //    }

    //}
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/District/Dist_Welcome.aspx");
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
      
        string rono = GridView1.SelectedRow.Cells[0].Text;
        Session["rono"] = rono;
        Response.Redirect("../District/UpdateValidity.aspx");
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void btnGet_Click(object sender, EventArgs e)
    {

    }
}
