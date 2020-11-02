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
public partial class Edit_Rack_Receipt_page : System.Web.UI.Page
{

    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    public string distid = "";
    public string getdatef = "";
    public string version = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            distid = Session["dist_id"].ToString();
           

            //string dbname = "Warehouse";
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
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
    protected void dgridchallan_EditCommand(object source, DataGridCommandEventArgs e)
    {

    }
    
    void fillgrid()
    {
       
            
            mobj = new MoveChallan(ComObj);
            string qry = "SELECT Rack_Receipt_Details.Recd_RailHead, tbl_Rail_Head.RailHead_Name, Rack_Receipt_Details.IssueCenter,tbl_MetaData_DEPOT.DepotName, Rack_Receipt_Details.Rack_No, Rack_Receipt_Details.Challan_No,Rack_Receipt_Details.Challan_date, Rack_Receipt_Details.Truck_No, Rack_Receipt_Details.Disp_Bags,Rack_Receipt_Details.Disp_Qty, Rack_Receipt_Details.IsReceived, Rack_Receipt_Details.district_code FROM Rack_Receipt_Details Left JOIN tbl_Rail_Head ON Rack_Receipt_Details.Recd_RailHead = tbl_Rail_Head.RailHead_Code Left JOIN tbl_MetaData_DEPOT ON Rack_Receipt_Details.IssueCenter = tbl_MetaData_DEPOT.DepotID where Rack_Receipt_Details.district_code='" + distid + "' and Rack_Receipt_Details.Challan_No not like 'NoChallan%' Order by  challan_date desc";
            DataSet ds = mobj.selectAny(qry);

             if (ds==null)
            {
                lbldisp.Visible = true;
                lbldisp.Text = " Currently No Record Present";

            }
            else
            {
                dgridchallan.DataSource = ds.Tables[0];
                dgridchallan.DataBind();
            }

        
    }
    
    protected void dgridchallan_SelectedIndexChanged(object sender, EventArgs e)
    {
        string status = dgridchallan.SelectedRow.Cells[7].Text;
        string tag = "Y";

        if (status == tag)
        {
           lbldisp.Visible=true;
           lbldisp.Text = "Sorry You Can't Edit This Details ,It has been Deposited";
        }
        else
        {
            lbldisp.Visible = false;
            string challan = dgridchallan.SelectedRow.Cells[2].Text;
            string rackno = dgridchallan.SelectedRow.Cells[1].Text;
            Session["challan"] = challan;
            Session["rackno"] = rackno;
            Response.Redirect("../District/Edit_Rack_Receipt_Dtl.aspx");
        }



       
        
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

    
    protected void dgridchallan_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void dgridchallan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
}
