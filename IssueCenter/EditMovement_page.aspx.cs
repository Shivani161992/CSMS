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
public partial class EditMovement_page : System.Web.UI.Page
{
    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    public string sid = "";
    public string getdatef = "";
    public string version = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            sid = Session["issue_id"].ToString();
            version = Session["hindi"].ToString();

            //string dbname = "Warehouse";
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
            if (!IsPostBack)
            {
                fillgrid();
                GetSource();
                if (version == "H")
                {
                    lblreceipt.Text = Resources.LocalizedText.lblreceipt;
                    lblSorcePfArrival.Text = Resources.LocalizedText.lblSorcePfArrival;
                   
                }
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");


        }
        
    }
    protected void dgridchallan_EditCommand(object source, DataGridCommandEventArgs e)
    {

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
    void fillgrid()
    {

        int month = int.Parse(DateTime.Today.Month.ToString());
        int cmonth = month - 1;
            
            mobj = new MoveChallan(ComObj);
            string qry = "SELECT tbl_Receipt_Details.challan_no,tbl_Receipt_Details.challan_date,tbl_Receipt_Details.Vehile_no,tbl_Receipt_Details.Receipt_Id,tbl_Receipt_Details.S_of_arrival,Source_Arrival_Type.Source_Name as Source_Name   FROM dbo.tbl_Receipt_Details left join dbo.Source_Arrival_Type on tbl_Receipt_Details.S_of_arrival= Source_Arrival_Type.Source_ID  where tbl_Receipt_Details.Depot_Id='" + sid + "' and tbl_Receipt_Details.S_of_arrival='" + ddlsarrival.SelectedValue + "' and tbl_Receipt_Details.Month>=" + cmonth + " and tbl_Receipt_Details.challan_no not like 'No Challan%' Order by  challan_date desc";
            DataSet ds = mobj.selectAny(qry);

             if (ds==null || ds.Tables[0].Rows.Count == 0)
            {
                lbldisp.Visible = true;
                lbldisp.Text = " Currently No Record Present";

            }
            else
            {
                lbldisp.Visible = false;
                dgridchallan.DataSource = ds.Tables[0];
                dgridchallan.DataBind();
            }

        
    }
    
    protected void dgridchallan_SelectedIndexChanged(object sender, EventArgs e)
    {
        string challan = dgridchallan.SelectedRow.Cells[4].Text;
        string source = dgridchallan.SelectedRow.Cells[5].Text;
        Session["challan"] = challan;
        Session["Receipt_Id"] = dgridchallan.SelectedRow.Cells[4].Text;
        Session["Source"] = source;
        Response.Redirect("../IssueCenter/mpscsc_edit_challan.aspx");
        
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

    protected void ddlsarrival_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsarrival.SelectedItem.ToString() == "Procurement")
        {
            Response.Redirect("Edit_Procurement.aspx");

        }
        else
        {
           
            fillgrid();
            
        }
    }
    protected void dgridchallan_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void dgridchallan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void Prevbutton_Click(object sender, EventArgs e)
    {

    }
}
