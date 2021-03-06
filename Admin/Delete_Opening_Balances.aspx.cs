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
public partial class Admin_Delete_Opening_Balances : System.Web.UI.Page


    {

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    DistributionCenters distobj = null;
  
    Districts DObj = null;
   
    Commodity_MP comdtobj = null;
    Scheme_MP schobj = null;
  
    MoveChallan mobj = null;
    string distid = "";
    string issueid = "";
    protected Common ComObj = null, cmn = null;
    public string qry = "";
    string adminid = "";
    public string recdid = "";
    public string recid = "";
    public Int64 recdnum = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {
            adminid = Session["st_id"].ToString();

            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());


            

            if (!IsPostBack)
            {
                GetDCName();
               
                //GetDCName();
                fillGrid();
             
             
           
             
            }
            

        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");

        }



       

    }

    void GetDCName()
    {

        //ddlissue.Items.Clear();
        distobj = new DistributionCenters(ComObj);
        string ord = "Select * from tbl_MetaData_Depot  order by DepotName";
        DataSet ds = distobj.selectAny(ord);
        ddlissueCenter.DataSource = ds.Tables[0];
        ddlissueCenter.DataTextField = "DepotName";
        ddlissueCenter.DataValueField = "DepotID";
        ddlissueCenter.DataBind();
        // ddDistId.Items.Insert(0, "--चुनिये--");
    }
  
    void GetGodownALL()
    {
        string issueid = ddlissueCenter.SelectedValue;
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.tbl_MetaData_GODOWN where DepotId='" + issueid + "' order by Godown_ID";
        DataSet ds = mobj.selectAny(qry);

        ddlgodown.DataSource = ds.Tables[0];
        ddlgodown.DataTextField = "Godown_Name";
        ddlgodown.DataValueField = "Godown_ID";
        ddlgodown.DataBind();
        ddlgodown.Items.Insert(0, "--Select--");


    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    void fillGrid()
    {
        string depoid = ddlissueCenter.SelectedValue;
        string qrychk = "select issue_opening_balance.* ,tbl_MetaData_GODOWN.Godown_Name,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name ,tbl_MetaData_SCHEME.Scheme_Name as Scheme_Name,Source_Arrival_Type.Source_Name as Source_Name  from dbo.issue_opening_balance left join dbo.tbl_MetaData_STORAGE_COMMODITY on issue_opening_balance.Commodity_Id=tbl_MetaData_STORAGE_COMMODITY .Commodity_Id left join dbo.tbl_MetaData_SCHEME on issue_opening_balance.Scheme_Id =tbl_MetaData_SCHEME.Scheme_Id left join dbo.Source_Arrival_Type on issue_opening_balance.Source=Source_Arrival_Type.Source_ID left join dbo.tbl_MetaData_GODOWN on issue_opening_balance.Godown=tbl_MetaData_GODOWN.Godown_ID  where issue_opening_balance.Depotid='" + depoid + "'";
        mobj = new MoveChallan(ComObj);
        DataSet dschk = mobj.selectAny(qrychk);
        if (dschk==null)
        {
        }
        else
        {
            GridView1.DataSource = dschk.Tables[0];
            GridView1.DataBind();
            //GridView1.Columns[9].Visible = false;
            //GridView1.Columns[10].Visible = false;
            //GridView1.Columns[11].Visible = false;
            //GridView1.Columns[12].Visible = false;
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
        fillGrid();
    }
   
    protected string changeDateDesc(DateTime inDate, int inDays)
    {
        int noofdays = DateTime.DaysInMonth(inDate.Year, inDate.Month);
        int count = 1;
        int xday = inDate.Day;
        int xmonth = inDate.Month;
        int xyear = inDate.Year;
        while (count <= inDays)
        {
            xday = xday - 1;
            if (xday < 1)
            {
                xmonth = xmonth - 1;
                if (xmonth < 1)
                {
                    xyear = xyear - 1;
                    xmonth = 12;
                }
                noofdays = DateTime.DaysInMonth(xyear, xmonth);
                xday = DateTime.DaysInMonth(xyear, xmonth);
               
            }
            
            count = count + 1;
        }
        return (xday + "/" + xmonth + "/" + xyear);
    }

  

    float CheckNull(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        float ValF = float.Parse(ValS);
        return ValF;

    }
    Int32 CheckNullInt(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        int ValF = int.Parse(ValS);
        return ValF;

    }
   
  
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        GridView1.Columns[9].Visible = true;
        GridView1.Columns[10].Visible = true;
        GridView1.Columns[11].Visible = true;
        GridView1.Columns[12].Visible = true;
        string issuid = ddlissueCenter.SelectedValue;
        string mmsource = GridView1.SelectedRow.Cells[9].Text;
        string comdty = GridView1.SelectedRow.Cells[10].Text;
        string scheme = GridView1.SelectedRow.Cells[11].Text;
        string godown = GridView1.SelectedRow.Cells[12].Text;
        string openqty = GridView1.SelectedRow.Cells[5].Text;
        string currqty = GridView1.SelectedRow.Cells[7].Text;
        mobj = new MoveChallan(ComObj);

        string qreyfetch = "Delete from dbo.issue_opening_balance where Depotid='" + issuid + "' and Godown='" + godown + "'and Commodity_ID='" + comdty + "' and Scheme_ID='" + scheme + "' and Source='" + mmsource + "'";
        try
        {
            cmd.Connection = con;
            cmd.CommandText = qreyfetch;
            con.Open();
            cmd.ExecuteNonQuery();

        }
        catch (Exception Ex)
        {
        }
        finally
        {
            con.Close();
        }



    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GridView1_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    //decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalamt"));

        //    string griddate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Acceptance_Date"));

        //    getdatef = getdate(griddate);


        //    Label lbl = (Label)e.Row.FindControl("lblChallan");
        //    lbl.Text = getdatef;

        //    //decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalamt"));
        //    //decimal rowTotalQty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "quantity"));

        //    //grdTotal = grdTotal + rowTotal;
        //    //grdTotalQty = grdTotalQty + rowTotalQty;

        //}
    }
    protected void Lastbutton_Click(object sender, EventArgs e)
    {

    }
    protected void Nextbutton_Click(object sender, EventArgs e)
    {

    }
    protected void Prevbutton_Click(object sender, EventArgs e)
    {

    }
    protected void Firstbutton_Click(object sender, EventArgs e)
    {

    }
    protected void ddlcomdty_SelectedIndexChanged(object sender, EventArgs e)
    {
      
}
protected void  ddlscheme_SelectedIndexChanged(object sender, EventArgs e)
{
    //GetBalance();
}
    protected void ddlissue_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
        //GetGodownALL();
        //string gname = ddlissue.SelectedValue;
        //mobj = new MoveChallan(ComObj);
        //string qrygdn = "SELECT * FROM dbo.tbl_MetaData_GODOWN where DistrictId='" + distid + "' and DepotId='" + issueid + "' and Godown_ID='"+gname+"'";

        //DataSet ds = mobj.selectAny(qrygdn);
        //if (ds == null)
        //{
        //}

        //else
        //{
        //    if (ds.Tables[0].Rows.Count == 0)
        //    {
        //        lblcap.Visible = false;
        //        txtcap.Visible = false;
        //    }
        //    else
        //    {
        //        lblcap.Visible = true;
        //        txtcap.Visible = true;
        //        txtcap.ReadOnly = true;
        //        DataRow dr = ds.Tables[0].Rows[0];
        //        txtcap.Text = dr["Godown_Capacity"].ToString();
        //    }
           

        //}



    }
    protected void ddlissueCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetGodownALL();
       
    }
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/Admin/AdminWelcome.aspx");
    }
}
