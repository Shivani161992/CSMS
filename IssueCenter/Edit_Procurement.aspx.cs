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
public partial class IssueCenter_Edit_Procurement : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    DistributionCenters distobj = null;
    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    public string time;
    public string sid = "";
    public string did = "";
    public string snid = "";
    public string getdatef = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            sid = Session["issue_id"].ToString();
            did = Session["dist_id"].ToString();


            
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
            distobj = new DistributionCenters(ComObj);

            if (!IsPostBack)
            {

                 fillgrid();
                 GetSource();

            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

    }
    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }

    void fillgrid()
    {
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT SCSC_Procurement.*,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name,Society.Society_Name +','+ SocPlace +',' + Society_Id as PurchaseCenterName FROM dbo.SCSC_Procurement left join dbo.tbl_MetaData_STORAGE_COMMODITY on SCSC_Procurement.Commodity_Id=tbl_MetaData_STORAGE_COMMODITY .Commodity_Id left join dbo.Society on SCSC_Procurement.Purchase_Center= Society.Society_Id where IssueCenter_ID='" + sid + "' order by SCSC_Procurement.Recd_date desc";
        DataSet ds = mobj.selectAny(qry);
         if (ds.Tables[0].Rows.Count==0)
        {
            Label1.Visible = true;
            Label1.Text = "Currently no Record is present";
        }
        else
        {

            dgridchallan.DataSource = ds.Tables[0];
            dgridchallan.DataBind();
        }


    }
    void GetSource()
    {
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.Source_Arrival_Type order by Source_ID";
        DataSet ds = mobj.selectAny(qry);
       
        ddlsarrival.DataSource = ds.Tables[0];
        ddlsarrival.DataTextField = "Source_Name";
        ddlsarrival.DataValueField = "Source_ID";
        ddlsarrival.DataBind();
        ddlsarrival.Items.Insert(0, "--Select--");
    }
    protected void dgridchallan_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalamt"));

            string griddate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Recd_Date"));

            getdatef = getdate(griddate);


            Label lbl = (Label)e.Row.FindControl("lblChallan");
            lbl.Text = getdatef;

            //decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalamt"));
            //decimal rowTotalQty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "quantity"));

            //grdTotal = grdTotal + rowTotal;
            //grdTotalQty = grdTotalQty + rowTotalQty;

        }
    }
    protected void ddlsarrival_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        if (ddlsarrival.SelectedItem.ToString() == "Procurement")
        {
            Response.Redirect("Edit_Procurement.aspx");

        }
        else
        {
            Response.Redirect("EditMovement_page.aspx");

        }
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
    protected void dgridchallan_SelectedIndexChanged(object sender, EventArgs e)
    {
             
        
        
        
        string challan = dgridchallan.SelectedRow.Cells[1].Text;
        Session["challan"] = challan;
        Session["Recd_ID"] = dgridchallan.SelectedRow.Cells[9].Text;
        Response.Redirect("../IssueCenter/Edit_Procurement_SCSC.aspx");
    }
    protected void dgridchallan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void dgridchallan_PageIndexChanged(object sender, EventArgs e)
    {

    }
}
