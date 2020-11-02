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
public partial class IssueCenter_Truck_Movement_Details : System.Web.UI.Page
{
    Transporter tobj = null;
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string distid = "";
    string sid = "";
    DataTable dt = new DataTable();
    MoveChallan mobj1 = null;
    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    public string challan = "";
    public string truckno = "";
    public string gatepass = "";
    public int getnum;
    public string getdatef = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            sid = Session["issue_id"].ToString();
            distid = Session["dist_id"].ToString();

            //string dbname = "Warehouse";
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

            fillgrid();
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");


        }



    }
    protected void dgridchallan_SelectedIndexChanged(object sender, EventArgs e)
    {
        string challan = dgridchallan.SelectedRow.Cells[1].Text;
        Session["challan"] = challan;
        dgridchallan.SelectedRow.Attributes.Add("onclick", "window.open('Truck_Movement_Print.aspx',null,'left=200, top=10, height=600, width= 520, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");
        //Response.Redirect("../District/Truck_Movement_Print.aspx");
    }
    public void FooterPagerClick(object sender, CommandEventArgs e)
    {
        //Used by external paging
        if (dgridchallan.PageCount==0)
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
                if ((dgridchallan.PageIndex < (dgridchallan.PageCount - 1)))
                {
                    dgridchallan.PageIndex += 1;
                }

                break;

            case "prev":
                //The prev button was clicked
                if ((dgridchallan.PageIndex > 0))
                {
                    dgridchallan.PageIndex -= 1;
                }

                break;

            case "last":
                //The Last Page button was clicked
                dgridchallan.PageIndex = (dgridchallan.PageCount - 1);
                break;

            default:
                //The First Page button was clicked
                dgridchallan.PageIndex = Convert.ToInt32(arg);
                break;
        }
        fillgrid();
        }
    }
    protected void dgridchallan_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalamt"));

            string griddate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "challan_date"));

            getdatef = getdate(griddate);


            Label lbl = (Label)e.Row.FindControl("lblChallan");
            lbl.Text = getdatef;

            //decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalamt"));
            //decimal rowTotalQty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "quantity"));

            //grdTotal = grdTotal + rowTotal;
            //grdTotalQty = grdTotalQty + rowTotalQty;

        }



    }

    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }
    void fillgrid()
    {
      
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.SCSC_Truck_challan where Dist_ID='" + distid + "' and Depot_Id='" + sid + "' and Challan_No not like '%NoChallan%' ";
        DataSet ds = mobj.selectAny(qry);
         if (ds.Tables[0].Rows.Count==0)
        {
            lbldisp.Visible = true;
            lbldisp.Text = "No Record Present";

        }
        else
        {
            dgridchallan.DataSource = ds.Tables[0];
            dgridchallan.DataBind();
        }


    }
}
