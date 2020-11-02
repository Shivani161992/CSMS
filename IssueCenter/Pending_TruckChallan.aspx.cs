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
public partial class IssueCenter_Pending_TruckChallan : System.Web.UI.Page
{
    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    public string sid = "";
    public string getdatef = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            sid = Session["issue_id"].ToString();

            //string dbname = "Warehouse";
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

            fillgrid();
             
            
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");


        }
        
    }
    protected void dgridchallan_EditCommand(object source, DataGridCommandEventArgs e)
    {

    }
    void fillgrid()
    {
        
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT Challan_No,Challan_Date,Truck_no,Dispatch_id FROM dbo.SCSC_Truck_challan where Depot_Id='" + sid + "' and Challan_No not like '%NoChallan%'";
        DataSet ds = mobj.selectAny(qry);
         if (ds==null)
        {
            Label1.Text = "Currently No Record is Present";
        }
        else
        {


            dgridchallan.DataSource = ds.Tables[0];
            dgridchallan.DataBind();
        }

    }
    
    protected void dgridchallan_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        string challan = dgridchallan.SelectedRow.Cells[1].Text;
        Session["gatepass"] = challan;
        dgridchallan.SelectedRow.Attributes.Add("onclick", "window.open('Print_TruckChallan.aspx',null,'left=200, top=10, height=620, width=580, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");
        //Response.Redirect("../IssueCenter/Print_TruckChallan.aspx");
        
    }

    public void FooterPagerClick(object sender, CommandEventArgs e)
    {
        //Used by external paging
        if (dgridchallan.PageCount == 0)
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
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
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

          

        }

     

    }
    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }




    protected void dgridchallan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
}
