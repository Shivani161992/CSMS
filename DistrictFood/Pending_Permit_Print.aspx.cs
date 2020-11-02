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
public partial class DCCB_Pending_Permit_Print : System.Web.UI.Page
{

    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    public string sid = "";
    public string gateno = "";
    public string getdatef = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            sid = Session["dist_id"].ToString();
            //string dbname = "Warehouse";
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
            //mobj = new MoveChallan(ComObj);
            //string qry = "SELECT GatePass_idFROM dbo.tbl_Receipt_Details where Depot_Id='"+ sid +"'";
            //DataSet ds = mobj.selectAny(qry);
            // GridView1.DataSource = ds.Tables[0];
            // GridView1.DataBind();
            if (!IsPostBack)
            {

                fillgrid();

            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");


        }

    }
    void fillgrid()
    {
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT Dccb_Permit.*,m_LeadSoc.LeadSoc_nameU as LeadName  FROM dbo.Dccb_Permit left join dbo.m_LeadSoc on Dccb_Permit.issue_name=m_LeadSoc.LeadSoc_Code  where Dccb_Permit.district_code='" + sid + "' order by Dccb_Permit.Permit_order_no";
        DataSet ds = mobj.selectAny(qry);
         if (ds.Tables[0].Rows.Count==0)
        {
            Label1.Visible = true;
            Label1.Text = "Currently no Record is Present";
        }
        else
        {
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
            //for (int i = 0; i < GridView1.Rows.Count; i++)
            //{
            //    GridView1.Rows[i].Cells[3].Font.Name = "KrutiDev010";
            //    GridView1.Rows[i].Cells[3].Font.Size = 13;


            //}
        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        gateno = GridView1.SelectedRow.Cells[1].Text;
        Session["permit_print"] = gateno;


        GridView1.SelectedRow.Attributes.Add("onclick", "window.open('Print_Permit_Order.aspx',null,'left=200, top=0, height=700, width= 600, status=n o, resizable= no, scrollbars= yes, toolbar= no,location= no, menubar= no');");


        //Response.Redirect("print_Gatepa ss.aspx");
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {


    }
    public void FooterPagerClick(object sender, CommandEventArgs e)
    {
        //Used by external paging
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalamt"));

            string griddate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "permit_date"));

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
    protected void Firstbutton_Click(object sender, EventArgs e)
    {


    }
}
