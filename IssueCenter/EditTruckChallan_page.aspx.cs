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
    public string did = "";
    public string getdatef = "";
    public string version = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            sid = Session["issue_id"].ToString();
            did = Session["dist_id"].ToString();
            version = Session["hindi"].ToString();
            //string dbname = "Warehouse";
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
            if (!IsPostBack)
            {
                fillgrid();
                if (version == "H")
                {
                    lblchallandetails.Text = Resources.LocalizedText.lblchallandetails;

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
   
    void fillgrid()
    {
       
            
            mobj = new MoveChallan(ComObj);
            string qry = "SELECT SCSC_Truck_challan.Depot_Id, tbl_MetaData_DEPOT.DepotName, SCSC_Truck_challan.Commodity,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name, SCSC_Truck_challan.Challan_Date, SCSC_Truck_challan.Dispatch_Godown,SCSC_Truck_challan.Sendto_District, SCSC_Truck_challan.Sendto_IC, SCSC_Truck_challan.Qty_send,SCSC_Truck_challan.Challan_No, SCSC_Truck_challan.Truck_no, SCSC_Truck_challan.Transporter,SCSC_Truck_challan.Dispatch_id FROM SCSC_Truck_challan Left JOIN tbl_MetaData_DEPOT ON SCSC_Truck_challan.Sendto_IC = tbl_MetaData_DEPOT.DepotID Left JOIN tbl_MetaData_STORAGE_COMMODITY ON SCSC_Truck_challan.Commodity = tbl_MetaData_STORAGE_COMMODITY .Commodity_Id where SCSC_Truck_challan.Dist_ID='" + did + "'and SCSC_Truck_challan.Depot_Id='" + sid + "' and SCSC_Truck_challan.Challan_No not like 'NoChallan%' ";
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
        string dispid = dgridchallan.SelectedRow.Cells[4].Text;
        Session["Dispatch"] = dispid;
       

        string tag = "Y";
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT IsDeposit FROM dbo.SCSC_Truck_challan where Dispatch_id='" + dispid  + "'";
        DataSet ds = mobj.selectAny(qry);
        DataRow dr = ds.Tables[0].Rows[0];
        string st = dr["IsDeposit"].ToString().Trim();
        if (st == tag)
        {

            lbldisp.Visible = true;
            lbldisp.Text = "Sorry You Can't Edit This Truck Challan ,It has been Diposited..";
            Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Sorry You Can't Edit This RO ,It has been lifted'); </script> ");
        }
        else
        {


            Response.Redirect("../IssueCenter/TruckChallan_Edit.aspx");
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
