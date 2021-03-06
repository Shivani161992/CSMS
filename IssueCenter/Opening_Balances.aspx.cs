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
public partial class IssueCenter_Opening_Balances : System.Web.UI.Page
{
    public SqlConnection cons = new SqlConnection(ConfigurationManager.ConnectionStrings["connstorage"].ToString());
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    DistributionCenters distobj = null;
  
    Districts DObj = null;
    chksql chk = null;
    Commodity_MP comdtobj = null;
    Scheme_MP schobj = null;
  
    MoveChallan mobj = null;
    string distid = "";
    string issueid = "";
    protected Common ComObj = null, cmn = null;
    public string qry = "";
    string version = "";
    public string recdid = "";
    public string recid = "";
    public Int64 recdnum = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            distid = Session["dist_id"].ToString();
            issueid = Session["issue_id"].ToString();
            version = Session["hindi"].ToString();

            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());


             txtqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
             txtbags.Attributes.Add("onkeypress", "return CheckIsnondecimal(this)");             
             txtqty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
             txtqty.Attributes.Add("onchange", "return chksqltxt(this)");
             txtbags.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
             txtbags.Attributes.Add("onchange", "return chksqltxt(this)");

             effective_from.Attributes.Add("onkeypress", "return CheckCalDate(this)");
             effective_from.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
             effective_from.Attributes.Add("onchange", "return chksqltxt(this)");

            txtqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtspos.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtbags.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();

            chk = new chksql();
            ArrayList ctrllist = new ArrayList();
            ctrllist.Add(txtqty.Text);
            ctrllist.Add(txtbags.Text);
            ctrllist.Add(effective_from);
           if (chk == null)
            {
            }
            else
            {
                bool chkstr = chk.chksql_server(ctrllist);
                if (chkstr == true)
                {
                    Page.Server.Transfer(HttpContext.Current.Request.Path);
                }
            }
            if (!IsPostBack)
            {
                //effective_from.Text = DateTime.Today.ToString("dd/MM/yyyy");
                ddlopeningmonth.SelectedIndex = DateTime.Today.Month - 1;
                GetScheme();
                GetCommodity();
                GetCategory();
                //GetDCName();
                fillGrid();
                //GetPosition();
                GetSource();
                GetGodown();
                lbl_cate.Visible =false;
                ddldamagecataegory.Visible = false;
                if (version == "H")
                {
                    lblSorcePfArrival.Text = Resources.LocalizedText.lblSorcePfArrival;
                    lblCommodity.Text = Resources.LocalizedText.lblCommodity;
                    lblGodown.Text = Resources.LocalizedText.lblGodown;
                    lblScheme.Text = Resources.LocalizedText.lblScheme;
                    lblQuantity.Text = Resources.LocalizedText.lblQuantity;
                    lblBagNumber.Text = Resources.LocalizedText.lblBagNumber;
                    lblopendate.Text = Resources.LocalizedText.lblopendate;
                    lblKgs.Text = Resources.LocalizedText.lblKgs;
                    lblopendetails.Text = Resources.LocalizedText.lblopendetails;
                    btnsubmit.Text = Resources.LocalizedText.btnsubmit;
                    btnclose.Text = Resources.LocalizedText.btnclose;
                }
             
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

    void GetScheme()
    {
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.tbl_MetaData_SCHEME where Scheme_Id='0'";
        DataSet ds = mobj.selectAny(qry);

        ddlscheme.DataSource = ds.Tables[0];
        ddlscheme.DataTextField = "Scheme_Name";
        ddlscheme.DataValueField = "Scheme_Id";
        ddlscheme.DataBind();


    }
    void GetDCName()
    {

        //ddlissue.Items.Clear();
        distobj = new DistributionCenters(ComObj);
        string ord = "Districtid='23" + distid + "' order by DepotName";
        DataSet ds = distobj.select(ord);
        ddlissue.DataSource = ds.Tables[0];
        ddlissue.DataTextField = "DepotName";
        ddlissue.DataValueField = "DepotID";
         ddlissue.DataBind();
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
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT Godown_ID,Godown_Name FROM dbo.tbl_MetaData_GODOWN where DistrictId='23" + distid + "' and DepotId='" + issueid + "' order by Godown_ID";
        //DataSet ds = mobj.selectAny(qry);

        if (cons.State == ConnectionState.Closed)
        {
            cons.Open();
        }

        SqlDataAdapter da = new SqlDataAdapter(qry, cons);
        DataSet ds = new DataSet();
        da.Fill(ds);
         
        if (ds != null)
        {
           if (ds.Tables[0].Rows.Count > 0)
        {
           ddlissue.DataSource = ds.Tables[0];
           ddlissue.DataTextField = "Godown_Name";
           ddlissue.DataValueField = "Godown_ID";
           ddlissue.DataBind();
           ddlissue.Items.Insert(0, "--Select--");
        }
       }
       if (cons.State == ConnectionState.Open)
       {
           cons.Close();
       }
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
        string mcomid = ddlcomdty.SelectedValue;
        string mcatid = ddlcategory.SelectedValue;
        string mscheme = ddlscheme.SelectedValue;
        int year = int.Parse(DateTime.Today.Date.Year.ToString());
        int month = int.Parse(DateTime.Today.Date.Month.ToString());
        string qrychk = "select issue_opening_balance.* ,tbl_MetaData_GODOWN.Godown_Name,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name ,tbl_MetaData_SCHEME.Scheme_Name as Scheme_Name,Source_Arrival_Type.Source_Name as Source_Name  from dbo.issue_opening_balance left join dbo.tbl_MetaData_STORAGE_COMMODITY on issue_opening_balance.Commodity_Id=tbl_MetaData_STORAGE_COMMODITY .Commodity_Id left join dbo.tbl_MetaData_SCHEME on issue_opening_balance.Scheme_Id =tbl_MetaData_SCHEME.Scheme_Id left join dbo.Source_Arrival_Type on issue_opening_balance.Source=Source_Arrival_Type.Source_ID left join dbo.tbl_MetaData_GODOWN on issue_opening_balance.Godown=tbl_MetaData_GODOWN.Godown_ID  where issue_opening_balance.District_Id='" + distid + "'and issue_opening_balance.Depotid='" + issueid + "' and issue_opening_balance.Bags != 0 and issue_opening_balance.Quantity != 0 ";
        mobj = new MoveChallan(ComObj);
        DataSet dschk = mobj.selectAny(qrychk);
        if (dschk==null)
        {
        }
        else
        {
            GridView1.DataSource = dschk.Tables[0];
            GridView1.DataBind();
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
        if (ddlcomdty.SelectedValue == "0")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Commodity......'); </script> ");
            return;
        }
        if (ddlQuality.SelectedValue == "0")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Quality......'); </script> ");
            return;
        }

        if (ddlcategory.SelectedValue == "0")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Categorye......'); </script> ");
            return;
        }

      

        if (ddlsarrival.SelectedValue == "0")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Source......'); </script> ");
            return;
        }
        if (effective_from.Text != "01/04/2014" && effective_from.Text != "01/01/2015")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter correct date of opening balance......'); </script> ");
            return;
        }
        string quality;

        if (ddlQuality.SelectedValue != "Damaged")
        {
        quality = ddlQuality.SelectedValue.ToString();
        }
        else
        {

       quality = ddlQuality.SelectedValue +"-"+ ddldamagecataegory.SelectedItem.Text;
        }




        string opid = Session["OperatorId"].ToString ();
        string mstate = Session["State_Id"].ToString();
        string mcomid = ddlcomdty.SelectedValue;
        string mcatid = ddlcategory.SelectedValue;
        string mscheme = ddlscheme.SelectedValue;
        string msource = ddlsarrival.SelectedValue;

        string cdate = DateTime.Today.Date.ToString();
        double qty;
        bool isOK = double.TryParse(txtqty.Text.Trim(), out qty);
        //float qty = float.Parse (txtqty.Text);
        string udate = "";
        string ddate = "";
        string mcrop = ddlcropyear.SelectedItem.ToString();
        string opnmonth = ddlopeningmonth.SelectedValue;
        string mgodown = ddlissue.SelectedValue;
        string opnyear = DateTime.Today.Year.ToString ();
        int year = int.Parse(opnyear);
        int omonth = int.Parse(opnmonth);
        int mbags = CheckNullInt(txtbags.Text);
        float cbalance = float.Parse(txtqty.Text);
        string pdate = getDate_MDY(effective_from.Text);
        int month = int.Parse(DateTime.Today.Date.Month.ToString());
        string source = ddlsarrival.SelectedValue;
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        float gcapacity = CheckNull(txtcap.Text);
        float gcurrcap = gcapacity - cbalance;
        if (ddlcomdty.SelectedItem.Text == "--Select--" || ddlscheme.SelectedItem.Text == "--Select--" || ddlsarrival.SelectedItem.Text == "--Select--" || ddlissue.SelectedItem.Text == "--Select--" || ddlQuality.SelectedItem.Text== "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please  Select Source of Arrival/Commodity/Scheme/Godown/Quality......'); </script> ");
        }
        else
        {
            string qrychk = "select Quantity  from dbo.issue_opening_balance where Commodity_Id ='" + mcomid + "'and Scheme_Id='" + mscheme + "'and District_Id='" + distid + "'and Depotid='" + issueid + "'and Source='" + source + "'and Godown='" + mgodown + "' and Crop_year = '" + mcrop + "' and Stock_Date='01/01/2015'";
            mobj = new MoveChallan(ComObj);
                DataSet dschk = mobj.selectAny(qrychk);
                if (dschk.Tables[0].Rows.Count == 0)
                {
                    string qrey = "insert into dbo.issue_opening_balance(State_Id,District_Id,Depotid,Commodity_Id,Scheme_Id,Category_Id,Godown,Crop_year,Bags,Quantity,Source,Current_Balance,Current_Bags,Quality,Month,Year,IP_Address,Stock_Date,CreatedDate,UpdatedDate,DeletedDate,OperatorID) values('" + mstate + "','" + distid + "','" + issueid + "','" + mcomid + "','" + mscheme + "','" + mcatid + "','" + mgodown + "','" + mcrop + "'," + mbags + "," + qty + ",'" + msource + "'," + cbalance + "," + mbags + ",'" + quality + "'," + omonth + "," + year + ",'" + ip + "','" + pdate + "',getdate(),'" + udate + "','" + ddate + "','" + opid + "')";
                    
                    cmd.CommandText = qrey;
                    cmd.Connection = con;
                    try
                    {

                        con.Open();
                        int count = cmd.ExecuteNonQuery();
                        con.Close();
                        if (count >= 1)
                        {
                            string qrystock = "select Sum(Quantity) as Qty from dbo.issue_opening_balance where Commodity_Id ='" + mcomid + "' and Scheme_Id='"+mscheme +"' and District_Id='" + distid + "'and Depotid='" + issueid + "'";
                            mobj = new MoveChallan(ComObj);
                            DataSet ds = mobj.selectAny(qrystock);
                             if (ds.Tables[0].Rows.Count==0)
                            {

                            }
                            else
                            {
                                DataRow drop = ds.Tables[0].Rows[0];
                                float mobal = CheckNull(drop["Qty"].ToString());

                                string qryinsopen = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + mcomid + "' and Scheme_Id='" + mscheme + "' and DistrictId ='" + distid + "'and DepotID='" + issueid + "' and Crop_Year= '"+ddlcropyear.SelectedItem.Text+"'";
                                mobj = new MoveChallan(ComObj);
                                DataSet dsopen = mobj.selectAny(qryinsopen);

                                if (dsopen.Tables[0].Rows.Count == 0)
                                {
                                    string qryinsr = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Scheme_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Received_OtherSch,Recieved_Other_Src,Received_RailHead,Received_CMR,Received_Levy,Sale_Do,Sale_otherg,Transfer_OtherSch,Crop_Year,Month,Year,Remarks) Values('" + distid + "','" + issueid + "','" + mcomid + "','" + mscheme + "'," + CheckNull(txtqty.Text) + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + ",'" + mcrop + "'," + month + "," + year + ",'')";
                                    cmd.CommandText = qryinsr;
                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();

                                }
                                else
                                {
                                    string qryinsU = "update dbo.tbl_Stock_Registor set Opening_Balance=" + mobal + " where Commodity_Id ='" + mcomid + "'and Scheme_Id='"+ mscheme +"' and DistrictId='" + distid + "'and DepotID='" + issueid + "'and Year= '"+ddlcropyear.SelectedItem.Text+"'";
                                    cmd.CommandText = qryinsU;
                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                            string select = "Select * from Current_Godown_Position where District_Id='" + distid + "' and Depotid='" + issueid + "' and Godown='" + mgodown+"'";
                            mobj = new MoveChallan(ComObj);
                            DataSet dsgdn = mobj.selectAny(select);
                            if (dsgdn.Tables[0].Rows.Count == 0)
                            {
                                string qreygdn = "insert into dbo.Current_Godown_Position(District_Id,Depotid,Godown,Current_Balance,Current_Bags,Month,Year,IP_Address,CreatedDate,UpdatedDate,DeletedDate,Godown_Capacity,Current_Capacity) values('" + distid + "','" + issueid + "','" + mgodown + "'," + cbalance + "," + mbags + "," + omonth + "," + year + ",'" + ip + "',getdate(),'" + udate + "','" + ddate + "'," + gcapacity + "," + gcurrcap + ")";
                                cmd.CommandText = qreygdn;
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                            else
                            {
                                DataRow drgdn = dsgdn.Tables[0].Rows[0];
                                string capact = drgdn["Current_Capacity"].ToString();
                                string cbags = drgdn["Current_Bags"].ToString();
                                string cqty = drgdn["Current_Balance"].ToString();
                                float ucap = CheckNull(capact) - cbalance;
                                float ucqty = CheckNull(cqty) + cbalance;
                                int ucbags = CheckNullInt(cbags) + mbags;
                                string qreygdnU = "update dbo.Current_Godown_Position set Current_Capacity=" + ucap + ",Current_Bags=" + ucbags + ",Current_Balance="+ucqty +" where District_Id='" + distid + "' and Depotid='" + issueid + "' and Godown='" + mgodown + "'";
                                cmd.CommandText = qreygdnU;
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                            

                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted Successfully......'); </script> ");
                            btnsubmit.Enabled = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        //Label1.Visible = true;
                        //Label1.Text = ex.Message;
                    }
                    finally
                    {
                        ComObj.CloseConnection();
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Opening Already Exist For Selected Commodity/Scheme......'); </script> ");

                }

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
    {
        ComObj.CloseConnection();
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDCName();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillGrid();
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
        string gname = ddlissue.SelectedValue;
        mobj = new MoveChallan(ComObj);
        string qrygdn = "SELECT * FROM dbo.tbl_MetaData_GODOWN where DistrictId='" + distid + "' and DepotId='" + issueid + "' and Godown_ID='"+gname+"'";

        DataSet ds = mobj.selectAny(qrygdn);
        if (ds == null)
        {
        }

        else
        {
            if (ds.Tables[0].Rows.Count == 0)
            {
                lblcap.Visible = false;
                txtcap.Visible = false;
            }
            else
            {
                lblcap.Visible = true;
                txtcap.Visible = true;
                txtcap.ReadOnly = true;
                DataRow dr = ds.Tables[0].Rows[0];
                txtcap.Text = dr["Godown_Capacity"].ToString();
            }
           

        }



    }

    protected void ddlsarrival_DataBound(object sender, EventArgs e)
    {
        DropDownList ddl = sender as DropDownList;
        if (ddl != null)
        {
            string qry = "SELECT * FROM dbo.Source_Arrival_Type order by Source_id";

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int d = 0; d < ds.Tables[0].Rows.Count; d++)
            {
                ddl.Items[d].Attributes.Add("title", ds.Tables[0].Rows[d]["Hindi"].ToString());
            }
        }
    }
    protected void ddlQuality_SelectedIndexChanged(object sender, EventArgs e)
    {
        getsweepageCategory();
        if (ddlQuality.SelectedValue == "Damaged")
        {
            
            ddldamagecataegory.Visible = true;
            lbl_cate.Visible = true;
        }
        else
        {

            ddldamagecataegory.Visible = false;
            lbl_cate.Visible = false;
        }
    }
    void getsweepageCategory()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.tbl_sweepage_CATEGORY ";
        DataSet ds = mobj.selectAny(qry);

        ddldamagecataegory.DataSource = ds.Tables[0];
        ddldamagecataegory.DataTextField = "Category_Name";
        ddldamagecataegory.DataValueField = "Category_Id";
        ddldamagecataegory.DataBind();
        ddldamagecataegory.Items.Insert(0, "--Select--");

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }
}
