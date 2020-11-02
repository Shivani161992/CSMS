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

public partial class State_Search_receiptEntry : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_WPMS = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2014"].ToString());

    SqlCommand cmd = new SqlCommand();
    DistributionCenters distobj = null;
    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    public string time;
    public string sid = "";
    public string did = "";
    public string snid = "";
    public string getdatef = "";
    public static string distid;
    protected void Page_Load(object sender, EventArgs e)
    {
       

            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
            distobj = new DistributionCenters(ComObj);

            if (!IsPostBack)
            {

                //fillgrid();

                GetDist();
            }

       

    }
    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }
    void fillgrid()
    {
        con.Open();
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT SCSC_Procurement.*,tbl_MetaData_GODOWN.Godown_Name,tbl_MetaData_DEPOT.DepotName, tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name,Society.Society_Name +','+ SocPlace +',' + Society_Id as PurchaseCenterName FROM dbo.SCSC_Procurement left join dbo.tbl_MetaData_STORAGE_COMMODITY on SCSC_Procurement.Commodity_Id=tbl_MetaData_STORAGE_COMMODITY .Commodity_Id left join dbo.Society on SCSC_Procurement.Purchase_Center= Society.Society_Id inner join tbl_MetaData_DEPOT on SCSC_Procurement.IssueCenter_ID=tbl_MetaData_DEPOT.DepotID left join tbl_MetaData_GODOWN on tbl_MetaData_GODOWN.Godown_ID=SCSC_Procurement.Recd_Godown where TC_Number='" + txt_tcno.Text.Trim() + "' and Purchase_Center ='" + ddl_purchasecentre.SelectedValue.ToString() + "' order by SCSC_Procurement.Recd_date desc";
        DataSet ds = mobj.selectAny(qry);
        if (ds.Tables[0].Rows.Count == 0)
        {
            Label1.Visible = true;
            Label1.Text = "Currently no Record is present";
            dgridchallan.DataBind();
        }
        else
        {
            Label1.Visible = false;
            dgridchallan.DataSource = ds.Tables[0];
            dgridchallan.DataBind();
        }
        con.Close();


    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    void fillgridDate()
    {
        con.Open();
        string receiveddate = getDate_MDY(txtDate.Text);
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT SCSC_Procurement.*,tbl_MetaData_GODOWN.Godown_Name,tbl_MetaData_DEPOT.DepotName, tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name,Society.Society_Name +','+ SocPlace +',' + Society_Id as PurchaseCenterName FROM dbo.SCSC_Procurement left join dbo.tbl_MetaData_STORAGE_COMMODITY on SCSC_Procurement.Commodity_Id=tbl_MetaData_STORAGE_COMMODITY .Commodity_Id left join dbo.Society on SCSC_Procurement.Purchase_Center= Society.Society_Id inner join tbl_MetaData_DEPOT on SCSC_Procurement.IssueCenter_ID=tbl_MetaData_DEPOT.DepotID left join tbl_MetaData_GODOWN on tbl_MetaData_GODOWN.Godown_ID=SCSC_Procurement.Recd_Godown where Distt_ID='" + ddl_district.SelectedValue.ToString() + "' and TC_Number='" + txt_tcno.Text.Trim() + "' and Purchase_Center ='" + ddl_purchasecentre.SelectedValue.ToString() + "' and Recd_Date='" + receiveddate + "'  order by SCSC_Procurement.Recd_date desc";
        DataSet ds = mobj.selectAny(qry);
        if (ds.Tables[0].Rows.Count == 0)
        {
            Label1.Visible = true;
            Label1.Text = "Currently no Record is present";
            dgridchallan.DataBind();
          
        }
        else
        {
            Label1.Visible = false;
            dgridchallan.DataSource = ds.Tables[0];
            dgridchallan.DataBind();

        }
        con.Close();


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
 
    protected void dgridchallan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void dgridchallan_PageIndexChanged(object sender, EventArgs e)
    {

    }


    protected void btn_search_Click1(object sender, EventArgs e)
    {

    }
    public void GetDist()
    {
        try
        {
            string qry = "SELECT district_code ,district_name FROM pds.districtsmp  order by district_name";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_district.DataSource = ds.Tables[0];
                ddl_district.DataTextField = "district_name";
                ddl_district.DataValueField = "district_code";
                ddl_district.DataBind();
                ddl_district.Items.Insert(0, "--Select--");

            }
        }
        catch (Exception)
        {
            //////
        }
    }
    void getWhtUparjncntr()
    {
        try
        {
            if (con_WPMS != null)
            {
                if (con_WPMS.State == ConnectionState.Closed)
                {
                    con_WPMS.Open();
                }
                // string qrysel = "select (Society_Name+','+SocPlace)as Society_Name,Society_Id from Society where DistrictId='" + ddldistpdy.SelectedValue.ToString() + "' and IsWheat='Y' order by Society_Name";
                string qrysel = "select distinct ic.SocietyID as Society_Id,(Society.Society_Name+','+Society.SocPlace+'('+ ic.SocietyID +')')as Society_Name from IssueToSangrahanaKendra ic inner join Society on Society.Society_Id = ic.SocietyID where ic.DistrictId='23'+'" + ddl_district.SelectedValue.ToString() + "' order by SocietyID";
                SqlDataAdapter da = new SqlDataAdapter(qrysel, con_WPMS);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddl_purchasecentre.DataSource = ds.Tables[0];
                        ddl_purchasecentre.DataTextField = "Society_Name";
                        ddl_purchasecentre.DataValueField = "Society_Id";
                        ddl_purchasecentre.DataBind();
                        ddl_purchasecentre.Items.Insert(0, "--Select--");
                    }
                }

            }
            else
            {
            }
        }

        catch (Exception)
        {

            con_WPMS.Close();
        }
        finally
        {
            con_WPMS.Close();
        }


    }
    protected void ddl_district_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_district.SelectedValue == "0" || ddl_district.SelectedItem.Text == "--Select--")
        {

        }

        else
        {
            distid = ddl_district.SelectedValue;


            try
            {
                getWhtUparjncntr();
        
            }
            catch (Exception)
            {
                //////
            }
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        if (txtDate.Text == "")
        {
            fillgrid();
        }
        else
        {

            fillgridDate();
        
        }
    }

 
}