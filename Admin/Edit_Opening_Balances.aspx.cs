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
public partial class Admin_Edit_Opening_Balances : System.Web.UI.Page
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


             txtqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
             txtbags.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

            txtqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();

            txtspos.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtbags.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();


            if (!IsPostBack)
            {
                GetDCName();
                ddlopeningmonth.SelectedIndex = DateTime.Today.Month - 1;
                GetScheme();
                GetCommodity();
                GetCategory();
                //GetDCName();
                fillGrid();
                //GetPosition();
                GetSource();
             
           
             
            }
            

        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");

        }



       

    }
  
    void GetCommodity()
    {
        comdtobj = new Commodity_MP(ComObj);
        DataSet ds = comdtobj.selectAll(" order by Commodity_Name  desc");
        ddlcomdty.DataSource = ds.Tables[0];
        ddlcomdty.DataTextField = "Commodity_Name";
        ddlcomdty.DataValueField = "Commodity_Id";
        ddlcomdty.DataBind();
        ddlcomdty.Items.Insert(0, "--Select--");


    }

    void GetScheme()
    {

        schobj = new Scheme_MP(ComObj);
        DataSet ds = schobj.selectAll("  order by Scheme_Id");
        ddlscheme.DataSource = ds.Tables[0];
        ddlscheme.DataTextField = "Scheme_Name";
        ddlscheme.DataValueField = "Scheme_Id";
        ddlscheme.DataBind();
        ddlscheme.Items.Insert(0, "--Select--");

    }
    void GetCategory()
    {
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.tbl_MetaData_STORAGE_CATEGORY";
        DataSet ds = mobj.selectAny(qry);

        ddlcategory.DataSource = ds.Tables[0];
        ddlcategory.DataTextField = "Category_Name";
        ddlcategory.DataValueField = "Category_Id";
        ddlcategory.DataBind();
       

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
    void GetGodown()
    {
        string depo = ddlissueCenter.SelectedValue;
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.tbl_MetaData_GODOWN where  DepotId='" + depo + "' order by Godown_ID";
        DataSet ds = mobj.selectAny(qry);

        ddlissue.DataSource = ds.Tables[0];
        ddlissue.DataTextField = "Godown_Name";
        ddlissue.DataValueField = "Godown_ID";
        ddlissue.DataBind();
        ddlissue.Items.Insert(0, "--Select--");


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
    void GetPosition()
    {
        string qrypos = "select Round( Sum(Current_Balance),2) as Current_Balance  from dbo.issue_opening_balance where District_Id='" + distid + "'and Depotid='" + issueid + "'";
        mobj = new MoveChallan(ComObj);
        
        DataSet dspos= mobj.selectAny(qrypos);
         if (dspos.Tables[0].Rows.Count==0)
        {
            txtspos.Text ="0";
            txtspos.ReadOnly = true;
            txtspos.BackColor = System.Drawing.Color.Wheat;
        }
        else
        {
            DataRow drpos = dspos.Tables[0].Rows[0];
            float pos = CheckNull(drpos["Current_Balance"].ToString());
            if (pos == 0)
            {
                txtspos.Text = "0";
                txtspos.ReadOnly = true;
                txtspos.BackColor = System.Drawing.Color.Wheat;

            }
            else
            {
                txtspos.Text = drpos["Current_Balance"].ToString();
                txtspos.ReadOnly = true;
                txtspos.BackColor = System.Drawing.Color.Wheat;
            }

        }


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

    void GetBalance()
    {


        DateTime acdare = new DateTime();
        acdare = Convert.ToDateTime(getDate_MDY(effective_from.Text));
        //string ddd = changeDateDesc(acdare,1);
        //int day = int.Parse(acdare.Day.ToString());
        //string month = acdare.Month.ToString();
        //string year = acdare.Year.ToString();
        //int fday = day - 1;
        //string stdate = month + "/" + fday.ToString() + "/" + year;
        string mcom = ddlcomdty.SelectedValue;
        string msch = ddlscheme.SelectedValue;
        string qrypos = "select Round( Sum(Current_Balance),2) as Current_Balance  from dbo.issue_opening_balance where District_Id='" + distid + "'and Depotid='" + issueid + "'and Commodity_Id='" + mcom + "'and Scheme_Id='" + msch + "'";//and Stock_Date='" + ddd +"'";
        mobj = new MoveChallan(ComObj);

        DataSet dspos = mobj.selectAny(qrypos);
        if (dspos == null)
        {
        }
        else
        {
            if (dspos.Tables[0].Rows.Count == 0)
            {
                txtspos.Text = "0";
                txtspos.ReadOnly = true;
                txtspos.BackColor = System.Drawing.Color.Wheat;
            }
            else
            {
                DataRow drpos = dspos.Tables[0].Rows[0];
                float pos = CheckNull(drpos["Current_Balance"].ToString());
                if (pos == 0)
                {
                    txtspos.Text = "0";
                    txtspos.ReadOnly = true;
                    txtspos.BackColor = System.Drawing.Color.Wheat;

                }
                else
                {
                    txtspos.Text = drpos["Current_Balance"].ToString();
                    txtspos.ReadOnly = true;
                    txtspos.BackColor = System.Drawing.Color.Wheat;
                }

            }


        }

    }
   
    protected void btnsubmit_Click(object sender, EventArgs e)
    {

       // mobj = new MoveChallan(ComObj);

       // string qreyrec = "select max(Receipt_ID) as Receipt_ID from dbo.issue_opening_balance where District_Id='" + distid  + "'and Depotid='" + issueid  + "'";
       // DataSet dsrec = mobj.selectAny(qreyrec);
       // DataRow drrec = dsrec.Tables[0].Rows[0];
       // recdid = drrec["Receipt_ID"].ToString();
       //if (recdid == "")
       // {
       //    recdid = issueid + "100";


       // }
       // else
       // {
       //     recdnum = Int64.Parse(recdid.ToString());
       //     recdnum = recdnum + 1;
       //     recdid = recdnum.ToString();


       // }

        GridView1.Columns[9].Visible = true ;
        GridView1.Columns[10].Visible = true ;
        GridView1.Columns[11].Visible = true ;
        GridView1.Columns[12].Visible = true ;

        string mmsource = GridView1.SelectedRow.Cells[9].Text;
        string comdty = GridView1.SelectedRow.Cells[10].Text;
        string scheme = GridView1.SelectedRow.Cells[11].Text;
        string godown = GridView1.SelectedRow.Cells[12].Text;

       // string mrecdid = recdid;
        string recid=GridView1.SelectedRow.Cells[9].Text ;
        float recdqty = CheckNull(GridView1.SelectedRow.Cells[5].Text);
        int recdbag = CheckNullInt(GridView1.SelectedRow.Cells[6].Text);
        int recdcurrbag = CheckNullInt(GridView1.SelectedRow.Cells[8].Text);
        float recdcurrqty = CheckNull(GridView1.SelectedRow.Cells[7].Text);
        
        string mstate = "23";
        string mcomid = ddlcomdty.SelectedValue;
        string mcatid = ddlcategory.SelectedValue;
        string mscheme = ddlscheme.SelectedValue;
        string msourceid = ddlsarrival.SelectedValue;

        string cdate = DateTime.Today.Date.ToString();
        float qty = float .Parse (txtqty.Text);

        string udate = "";
        string ddate = "";
        string mcrop = ddlcropyear.SelectedItem.ToString();
        string opnmonth = ddlopeningmonth.SelectedValue;
        string mgodown = ddlissue.SelectedValue;
        string opnyear = DateTime.Today.Year.ToString ();
        int year = int.Parse(opnyear);
        int omonth = int.Parse(opnmonth);
        int mbags = CheckNullInt(txtbags.Text);
        int ubags = recdbag - mbags;
        float cbalance = float.Parse(txtqty.Text);
        float uqty = recdqty - cbalance;
        string pdate = getDate_MDY(effective_from.Text);
        int month = int.Parse(DateTime.Today.Date.Month.ToString());
        string msource = ddlsarrival.SelectedValue;
         string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        float gcapacity = CheckNull(txtcap.Text);
        float gcurrcap = gcapacity - cbalance;
        float mcurbalance = CheckNull(txtcurbalance.Text);
        int mcurbags = CheckNullInt(txtcurbags.Text);
        string issueid = ddlissueCenter.SelectedValue;
        if (ddlcomdty.SelectedItem.Text == "--Select--" || ddlscheme.SelectedItem.Text == "--Select--" || ddlsarrival.SelectedItem.Text == "--Select--" || ddlissue.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please  Select Source of Arrival/Commodity/Scheme/Godown......'); </script> ");
        }
        else
        {


            //string qrychk = "select Quantity  from dbo.issue_opening_balance where Commodity_Id ='" + mcomid + "'and Scheme_Id='" + mscheme + "'and District_Id='" + distid + "'and Depotid='" + issueid + "'and Source='" + source + "'and Godown='"+mgodown +"'";
            //    mobj = new MoveChallan(ComObj);
            //    DataSet dschk = mobj.selectAny(qrychk);
            //    if (dschk.Tables[0].Rows.Count == 0)
            //    {
                  string qrey = "Update dbo.issue_opening_balance Set Commodity_Id='" + mcomid + "',Scheme_Id='" + mscheme + "',Godown='" + mgodown + "',Bags="+mbags+",Quantity="+cbalance+",Source='" + msource + "',Current_Balance="+mcurbalance +",Current_Bags="+mcurbags +" where Depotid='" + issueid + "' and Godown='" + godown + "'and Commodity_ID='" + comdty + "' and Scheme_ID='" + scheme + "' and Source='" + mmsource+ "'";
                    
                    cmd.CommandText = qrey;
                    cmd.Connection = con;
                    try
                    {

                        con.Open();
                        int count = cmd.ExecuteNonQuery();
                        con.Close();
                        
                            

                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Updated Successfully......'); </script> ");
                            btnsubmit.Enabled = false;
                        }
                    
                    catch (Exception ex)
                    {
                        Label1.Visible = true;
                        Label1.Text = ex.Message;
                    }
                    finally
                    {
                        ComObj.CloseConnection();
                    }







                //}
                //else
                //{
                //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Opening Already Exist For Selected Commodity/Scheme......'); </script> ");

                //}


                   

                fillGrid();
    }
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
    //protected string getDate_MDY(string inDate)
    //{
    //    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
    //    DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
    //    return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    //}
    protected void btnclose_Click(object sender, EventArgs e)
    {   ComObj.CloseConnection();
        Response.Redirect("~/Admin/AdminWelcome.aspx");
    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDCName();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel1.Visible = true;
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

        string qreyfetch = "select * from dbo.issue_opening_balance where Depotid='" + issuid + "' and Godown='" + godown + "'and Commodity_ID='" + comdty + "' and Scheme_ID='" + scheme + "' and Source='" + mmsource + "'";
        DataSet dsrec = mobj.selectAny(qreyfetch);
        if (dsrec == null)
        {

        }
        else
        {
            if (dsrec.Tables[0].Rows.Count == 0)
            {
            }
            else
            {
                DataRow drrec = dsrec.Tables[0].Rows[0];
                ddlsarrival.SelectedValue = drrec["Source"].ToString();
                ddlcomdty.SelectedValue = drrec["Commodity_Id"].ToString();
                ddlscheme.SelectedValue = drrec["Scheme_Id"].ToString();
                ddlissue.SelectedValue = drrec["Godown"].ToString();
                txtqty.Text = drrec["Quantity"].ToString();
                txtbags.Text = drrec["Bags"].ToString();
                txtcurbags.Text = drrec["Current_Bags"].ToString();
                txtcurbalance.Text = drrec["Current_Balance"].ToString();
                //if (CheckNull(openqty) == CheckNull(currqty))
                //{
                //    ddlsarrival.Enabled = true;
                //    ddlcomdty.Enabled = true;
                //    ddlscheme.Enabled = true;
                //    ddlissue.Enabled = true;
                //}
                //else
                //{
                //    ddlsarrival.Enabled = false;
                //    ddlcomdty.Enabled = false;
                //    ddlscheme.Enabled = false;
                //    ddlissue.Enabled = false;

                //}

            }

        }


        GridView1.Columns[9].Visible = false;
        GridView1.Columns[10].Visible = false;
        GridView1.Columns[11].Visible = false;
        GridView1.Columns[12].Visible = false;



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
        GetGodown();
    }
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
}
