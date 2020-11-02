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
using System.Data.SqlClient;
using Data;
using DataAccess;
using System.Security.Principal;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
public partial class IssueCenter_IssueAgainst_TO : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_opdms = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_opdms"].ToString());
    public SqlConnection cons = new SqlConnection(ConfigurationManager.ConnectionStrings["connstorage"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd_opdms = new SqlCommand();
    SqlDataReader dr;
    SqlDataReader dr_opdms;
    MoveChallan mobj1 = null;
    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    string distid = "";
    string sid = "";
    DateTime dtt;

    DataTable Dt1 = new DataTable();
    public string version = "";
    string qunt = "0";
    decimal totalissuequnt;
    decimal do_qunat;
    decimal totalissuequnt_salt;
    decimal totalissuequnt_sugar;
    decimal totalissuequnt_maize;
    decimal totalissuequnt_Wheat;
    decimal totalissuequnt_rice;
    decimal do_qunat_wheat;
    decimal do_qunat_sugar;
    decimal do_qunat_salt;
    decimal do_qunat_maize;
    decimal do_qunat_rice;
    string connectedfps="";
    string connectedfpscode = "";
    string suspendendfpscode = "";

    string wheattotalsus = "";
    string ricetotalsus = "";
    string salttotalsus = "";
    string maizetotalsus = "";
    string sugartotalsus = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (Session["dist_id"] == null)
            {
                Response.Redirect("~/Session_Expire_Dist.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
       
        distid = Session["dist_id"].ToString();
        sid = Session["issue_id"].ToString();
        version = Session["hindi"].ToString();
        ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
        if (Page.IsPostBack == false)
        {
            Session["issubmited"] = "No"; 
            btnsave.Enabled = true;
            ddlyear.Items.Add(DateTime.Today.Year.ToString());

            ddlyear.Items.Add((int.Parse(DateTime.Today.Year.ToString()) - 1).ToString());

            ddlyear.Items.Add((int.Parse(DateTime.Today.Year.ToString()) + 1).ToString());

            ddlyear.Items.Insert(0, "--Select--");

            ddlyear.SelectedIndex = 1;
            ddd_allot_year.Items.Add((CheckNullInt(DateTime.Today.Year.ToString()) - 1).ToString());
            ddd_allot_year.Items.Add(DateTime.Today.Year.ToString());
            ddd_allot_year.Items.Add((CheckNullInt(DateTime.Today.Year.ToString()) + 1).ToString());
            ddd_allot_year.SelectedIndex = 1;
            ddl_allot_month.SelectedIndex = DateTime.Today.Month - 1;
            ddlmonth.SelectedValue = (Convert.ToInt64(DateTime.Today.Month.ToString()) + 1).ToString();
          
              ddlyear_SelectedIndexChanged( sender,  e);
             

            //get_to_no();
            Getbranch();
            //GetGodown();

            ////get_scheme();
            //GetSource();


            get_Transporter();
            hd_check_salt.Value = "";
            hd_check_sugar.Value = "";
            hd_check_wheat.Value = "";
            hd_check_rice.Value = "";

            tx_issued_date.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
            if (version == "H")
            {
              
                lblallotyear.Text = Resources.LocalizedText.lblallotyear;
                lblallotmonth.Text = Resources.LocalizedText.lblallotmonth;
                lblCommodity.Text = Resources.LocalizedText.lblCommodity;
                //lblScheme.Text = Resources.LocalizedText.lblScheme;
                lblbalcomdty.Text = Resources.LocalizedText.lblbalcomdty;
                lblTruckNumber.Text = Resources.LocalizedText.lblTruckNumber;
                lblNoofBags.Text = Resources.LocalizedText.lblNoofBags;
                lblBagNumber.Text = Resources.LocalizedText.lblBagNumber;
                lbl_issueqty.Text = Resources.LocalizedText.lbl_issueqty;
                lblbalqty.Text = Resources.LocalizedText.lblbalqty;
                lbldispsource.Text = Resources.LocalizedText.lbldispsource;
                lblGodownNo.Text = Resources.LocalizedText.lblGodownNo;
                lblDispatchQty.Text = Resources.LocalizedText.lblDispatchQty;
                lblissuedate.Text = Resources.LocalizedText.lblissuedate;
                //lbltoissue.Text = Resources.LocalizedText.lbltoissue;
                lbldono.Text = Resources.LocalizedText.lbldono;
                lbldodate.Text = Resources.LocalizedText.lbldodate;
                lbldovalidity.Text = Resources.LocalizedText.lbldovalidity;
                lbl_issueqty.Text = Resources.LocalizedText.lbl_issueqty;
                lbl_balqty.Text = Resources.LocalizedText.lbl_balqty;
                lblQuantity.Text = Resources.LocalizedText.lblQuantity;
                btn_new.Text = Resources.LocalizedText.btn_new;
                btnsave.Text = Resources.LocalizedText.btnsave;
                btnclose.Text = Resources.LocalizedText.btnclose;

            }



        }

        tx_bags.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");
        tx_qty_to_issue.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");

        tx_bags.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_bags.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_qty_to_issue.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_qty_to_issue.Attributes.Add("onchange", "return chksqltxt(this)");

        hlinkpdo.Attributes.Add("onclick", "window.open('Print_DeliveryChallan.aspx',null,'left=400, top=100, height=900, width= 700, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");
        tx_gatepass.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
        tx_gatepass.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_gatepass.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_issued_date.Attributes.Add("onkeypress", "return CheckCalDate(this)");
        tx_issued_date.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_issued_date.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_bags.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_balance_qty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_do_qty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_gatepass.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_issue_balqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_issued_qty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_qty_to_issue.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_qty_to_issue.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_cur_bal.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        tx_cur_bags.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
    }

    protected void get_to_no()
    {
        try
        {
            Label1.Text = "";
            tx_qty_to_issue.Text = "";
            tx_bags.Text = "";
            tx_gatepass.Text = "";
            tx_balance_qty.Text = "";
            tx_do_qty.Text = "";
            tx_do_validity.Text = "";
            tx_issue_balqty.Text = "";
            tx_issued_qty.Text = "";
            //tx_issueto.Text = "";
         
            string dist = distid;
            ddl_TO_no.Items.Clear();
            //string issue_centre_code = sid;
            cmd.CommandText = "select  distinct TransportOrder from dbo.DPY_TranportOrder where DPY_TranportOrder.DistrictId='" + distid + "' and  AllotMonth='" + ddlmonth.SelectedValue.ToString() + "' and AllotYear='" + ddlyear.SelectedValue.ToString() + "'   order by  TransportOrder desc";
            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddl_TO_no.Items.Add(dr["TransportOrder"].ToString());
           
              
            }
            ddl_TO_no.Items.Insert(0, "Select");
            dr.Close();
            con.Close();
        }
        catch (Exception)
        { }
    }
    protected void ddl_do_no_SelectedIndexChanged(object sender, EventArgs e)
    {

        Session["dt1"] = null;
        Panel2.Visible = false;
        lblCommodity.Visible = true;
        ddl_commodity.Visible = true;
        Gridissue.Visible = false;
        lbl_dcno.Visible = false;
        try
        {
            if (ddl_TO_no.SelectedItem.Text == "Select")
            {
                Label1.Text = "";
                tx_qty_to_issue.Text = "";
                tx_bags.Text = "";
                tx_gatepass.Text = "";
                tx_balance_qty.Text = "";
                tx_do_qty.Text = "";
                tx_do_validity.Text = "";
                tx_issue_balqty.Text = "";
                tx_issued_qty.Text = "";
                //tx_issueto.Text = "";
                //tx_lead.Text = "";
                tx_cur_bal.Text = "";
                tx_cur_bags.Text = "";
                tx_do_date.Text = "";
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = "Please Select Transport Order No. ...";
                Gridissue.DataSource = null;
                Gridissue.DataBind();
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Transport Order No.');</script>");
            }
            else
            {
                string dist = distid;

                string checkdo = "Select TransporterId  from DPY_TranportOrder where DistrictId='" + dist + "' and  TransportOrder = '" + ddl_TO_no.SelectedItem.Text + "' ";

                SqlCommand cmd_dotype = new SqlCommand(checkdo, con);
                SqlDataAdapter da_dotype = new SqlDataAdapter(cmd_dotype);
                DataSet ds_dotype = new DataSet();

                da_dotype.Fill(ds_dotype);


               
               
                string transprter = ds_dotype.Tables[0].Rows[0]["TransporterId"].ToString();
                hd_transpoeter.Value = transprter;
           


                    ddltransporter.SelectedValue = transprter;
                    ddltransporter.Enabled = false;
                    //ddl_godown.SelectedIndex = 0;
                    //ddlsarrival.SelectedIndex = 0;
                    get_To_data();
                    get_feed();


                    get_route();
                    //get_comm();

            }
        }
        catch (Exception)
        {

        }

    }

    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MM/yyyy");
    }
    protected String getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    public string get_days(DateTime fromDate, DateTime toDate)
    {
        int y1 = 0, m1 = 0, d1 = 0, y2 = 0, m2 = 0, d2 = 0;
        y1 = fromDate.Year;
        m1 = fromDate.Month;
        d1 = fromDate.Day;
        y2 = toDate.Year;
        m2 = toDate.Month;
        d2 = toDate.Day;
        int y = (y2 - y1) * 12;
        int m = (y + m2) - m1;
        int noofdays = DateTime.DaysInMonth(fromDate.Year, fromDate.Month);
        int d = (m * noofdays) + d2;
        int day = d - d1;
        return day.ToString();
    }
    protected void get_To_data()
    {
        try
        {
            btnsave.Enabled = true;
            Label1.ForeColor = System.Drawing.Color.Blue;
            tx_qty_to_issue.Enabled = true;
            tx_bags.Enabled = true;
            Label1.Text = "";
            tx_qty_to_issue.Text = "";
            tx_bags.Text = "";
            tx_gatepass.Text = "";
            //tx_issued_date.Enabled = true ;
            tx_gatepass.Enabled = true;
            string do_no = ddl_TO_no.SelectedItem.Text;
            string dist = distid;
            string issuedqunattity = "0";
            string issue_centre_code = sid;
            string blancequantity = "0";
            //string bal_qty = "";
            string issueqty = "0";
            string issueto_name = "";
            string do_valid = "";
            DateTime do_date = new DateTime();
            string do_qty = "0";
            string lead_code = "";
            cmd.CommandText = "SELECT  DPY_TranportOrder.AllotMonth,DPY_TranportOrder.AllotYear,sum(round(convert(decimal(18,5),isnull((DPY_TranportOrder.WheatAllot),0)+isnull(DPY_TranportOrder.SugarAllot,0)+isnull(DPY_TranportOrder.RiceAllot,0)+isnull(DPY_TranportOrder.SaltAllot,0)+isnull(DPY_TranportOrder.MaizeAllot,0)),5)) as quantity,DPY_TranportOrder.ValidityDate,DPY_TranportOrder.CreatedDate as createda FROM dbo.DPY_TranportOrder where DPY_TranportOrder.TransportOrder='" + do_no + "' and DPY_TranportOrder.DistrictId='" + dist + "'  group by AllotYear ,AllotMonth,ValidityDate ,ValidityDate ,DPY_TranportOrder.CreatedDate";
            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                do_date = DateTime.Parse(dr["createda"].ToString());
                do_valid = dr["ValidityDate"].ToString();

                do_qty = dr["quantity"].ToString();
                //issueqty = dr["qty_issue"].ToString();
                try
                {
                    ddd_allot_year.SelectedValue = dr["AllotYear"].ToString();
                }
                catch (Exception)
                { }

                ddl_allot_month.SelectedValue = dr["AllotMonth"].ToString();

                //ddl_commodity.SelectedValue = dr["commodity"].ToString();
                //ddl_scheme.SelectedValue = "116";
            }

            dr.Close();
            con.Close();
            cmd.CommandText = "select sum(round((convert(decimal(18,5),issue_against_Doorstep_do.qty_issue)),5)) as issueqty from dbo.issue_against_Doorstep_do  where issue_against_Doorstep_do.Transport_order='" + do_no + "' and issue_against_Doorstep_do.district_code='" + dist + "' and issue_against_Doorstep_do.issueCentre_code='" + issue_centre_code + "'";
            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr["issueqty"].ToString() != "")
                {
                    issuedqunattity = dr["issueqty"].ToString();
                    blancequantity = (Convert.ToDecimal(do_qty) - Convert.ToDecimal(dr["issueqty"].ToString())).ToString();
                }
                tx_balance_qty.Text = blancequantity;
                tx_issue_balqty.Text = blancequantity;
            }
            dr.Close();
            con.Close();
            //Session["issueto"] = lead_code;
            if (issueqty == "")
            {
                issueqty = "0";
            }

            //string ddate = ;
            tx_do_date.Text = getdate(do_date.ToString());
            tx_do_validity.Text = getdate(do_valid.ToString());
            tx_do_qty.Text = do_qty;
            Session["do_valid"] = do_valid;

            decimal qty = CheckNull(do_qty) - CheckNull(issueqty);
            //tx_balance_qty.Text = System.Math.Round(qty, 5).ToString();
            //tx_issue_balqty.Text = System.Math.Round(qty, 5).ToString();
            //}

            tx_do_qty.Text = do_qty;
            tx_issued_qty.Text = issuedqunattity;
        }
        catch (Exception)
        { }
    }


    protected void ddl_allot_month_SelectedIndexChanged(object sender, EventArgs e)
    {
        //get_do_no();
    }
    protected string changeDate(DateTime inDate, int inDays)
    {
        int noofdays = DateTime.DaysInMonth(inDate.Year, inDate.Month);
        int count = 1;
        int xday = inDate.Day;
        int xmonth = inDate.Month;
        int xyear = inDate.Year;
        while (count <= inDays)
        {
            xday = xday + 1;
            if (xday > noofdays)
            {
                xday = 1;
                xmonth = xmonth + 1;
                if (xmonth > 12)
                {
                    xyear = xyear + 1;
                    xmonth = 1;
                }
                noofdays = DateTime.DaysInMonth(xyear, xmonth);
            }
            count = count + 1;
        }
        return (xday + "/" + xmonth + "/" + xyear);
    }
    protected decimal CheckNull(string Val)
    {
        decimal rval = 0;
        if (Val.Trim() == "" || Val.ToLower().Contains("n") || Val == null)
        {
            rval = 0;
        }
        else
        {
            rval = Convert.ToDecimal(Val);
        }
        return rval;
    }
    protected int CheckNullInt(string Val)
    {
        int rval = 0;
        if (Val.Trim() == "" || Val.ToLower().Contains("n") || Val == null)
        {
            rval = 0;
        }
        else
        {
            rval = int.Parse(Val);
        }
        return rval;
    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        Session["issubmited"] = "No";
        ComObj.CloseConnection();
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }
    protected void GetGodown()
    {
        cons.Open();

        string qrysel = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where BranchID='" + ddl_branch.SelectedValue.ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(qrysel, cons);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_godown.DataSource = ds.Tables[0];
                ddl_godown.DataTextField = "Godown_Name";
                ddl_godown.DataValueField = "Godown_ID";
                ddl_godown.DataBind();
                ddl_godown.Items.Insert(0, "--select--");
            }
        }
        cons.Close();
    }

    protected void Getbranch()
    {
        cons.Open();

        string qrysel = "select IssueCenterName, tbl_MetaData_DEPOT.DepotName,tbl_MetaData_DEPOT.BranchID from MetaDataBranchWithIssueCenter inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.BranchId=MetaDataBranchWithIssueCenter.BranchID where IssueCenterId='" + sid + "'";
        SqlDataAdapter da = new SqlDataAdapter(qrysel, cons);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_branch.DataSource = ds.Tables[0];
                ddl_branch.DataTextField = "DepotName";
                ddl_branch.DataValueField = "BranchID";
                ddl_branch.DataBind();
                ddl_branch.Items.Insert(0, "--select--");
            }
        }
        else
        {
            string qrysel2 = " select BranchId,DepotName from  tbl_MetaData_DEPOT where DistrictId='23"+ distid +"'";
            ddl_branch.DataSource = ds.Tables[0];
            ddl_branch.DataTextField = "DepotName";
            ddl_branch.DataValueField = "BranchId";
            ddl_branch.DataBind();
            ddl_branch.Items.Insert(0, "--select--");

        }
        cons.Close();
    }


    protected void get_comm()
    {
        try
        {
            mobj = new MoveChallan(ComObj);
            string qry = "select  Commodity_Id,Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Status='Y' and Commodity_Id in('3','12','19','23','22','35','4')";
            DataSet ds = mobj.selectAny(qry);

            ddl_commodity.DataSource = ds.Tables[0];
            ddl_commodity.DataTextField = "Commodity_Name";
            ddl_commodity.DataValueField = "Commodity_Id";
            ddl_commodity.DataBind();
            ddl_commodity.Items.Insert(0, "--Select--");
        }
        catch (Exception)
        { }

    }
    protected void get_route()
    {
        try
        {
            mobj = new MoveChallan(ComObj);
            string qry = "Select distinct RouteNumber  from DPY_TranportOrder where DistrictId='" + distid + "'  and TransportOrder = '" + ddl_TO_no.SelectedItem.Text + "' and DPY_TranportOrder.AllotYear=" + ddd_allot_year.SelectedValue.ToString() + " and DPY_TranportOrder.AllotMonth=" + ddl_allot_month.SelectedValue.ToString() + "";
            DataSet ds = mobj.selectAny(qry);

            ddl_route.DataSource = ds.Tables[0];
            ddl_route.DataTextField = "RouteNumber";
            ddl_route.DataValueField = "RouteNumber";
            ddl_route.DataBind();
            ddl_route.Items.Insert(0, "--Select--");
        }
        catch (Exception)
        { }

    }
    protected void get_feed()
    {
        try
        {
            mobj = new MoveChallan(ComObj);
            string qry = "Select distinct FeedNumber  from DPY_TranportOrder where DistrictId='" + distid + "'  and TransportOrder = '" + ddl_TO_no.SelectedItem.Text + "' and DPY_TranportOrder.AllotYear=" +  ddd_allot_year.SelectedValue.ToString() + " and DPY_TranportOrder.AllotMonth=" + ddl_allot_month.SelectedValue.ToString() + "";
            DataSet ds = mobj.selectAny(qry);

            ddl_feed.DataSource = ds.Tables[0];
            ddl_feed.DataTextField = "FeedNumber";
            ddl_feed.DataValueField = "FeedNumber";
            ddl_feed.DataBind();
            ddl_feed.Items.Insert(0, "--Select--");
        }
        catch (Exception)
        { }

    }
  
    void GetSource()
    {
        string qry = "";
        try
        {
            mobj = new MoveChallan(ComObj);
            if (ddl_commodity.SelectedValue == "22" || ddl_commodity.SelectedValue == "35")
            {
                qry = "SELECT * FROM dbo.Source_Arrival_Type where Source_ID <> '08' and Source_ID in('02','01') order by Source_ID ";

            }
            else if (ddl_commodity.SelectedValue == "3" || ddl_commodity.SelectedValue == "4")
            {
                qry = "SELECT * FROM dbo.Source_Arrival_Type where Source_ID <> '08' and Source_ID in('02','05','11') order by Source_ID ";

            }
            else if (ddl_commodity.SelectedValue == "19" || ddl_commodity.SelectedValue == "23")
            {
                qry = "SELECT * FROM dbo.Source_Arrival_Type where Source_ID <> '08' and Source_ID in('09','10') order by Source_ID ";

            }
          
            else if (ddl_commodity.SelectedValue == "12")
            {
                qry = "SELECT * FROM dbo.Source_Arrival_Type where Source_ID <> '08' and Source_ID in('01','02') order by Source_ID ";

            }
            DataSet ds = mobj.selectAny(qry);

            ddlsarrival.DataSource = ds.Tables[0];

            ddlsarrival.DataTextField = "Source_Name";
            ddlsarrival.DataValueField = "Source_ID";
            ddlsarrival.DataBind();
            ddlsarrival.Items.Insert(0, "--Select--");
        }
        catch (Exception)
        { }
    }


    protected void btn_new_Click(object sender, EventArgs e)
    {
        Session["issubmited"] = "No";
        Response.Redirect("~/IssueCenter/IssueAgainst_TO.aspx");
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {

        if (Session["issubmited"].ToString() == "Yes")
        {

        }
        else
        {
            DateTime dispdate = Convert.ToDateTime(DateTime.ParseExact(tx_issued_date.Text.Trim(), "dd/MM/yyyy", null).ToString("MM/dd/yyyy"));

            string today_date = System.DateTime.Now.ToString("MM/dd/yyyy");

            dtt = Convert.ToDateTime(today_date);


            DateTime date1 = new DateTime(dtt.Year, dtt.Month, dtt.Day);

            DateTime date2 = new DateTime(dispdate.Year, dispdate.Month, dispdate.Day);

            int result1 = DateTime.Compare(date1, date2);


            string relationship = string.Empty;

            if (result1 >= 0)
            {
                decimal issuedqty = 0;

                for (int i = 0; GridView2.Rows.Count > i; i++)
                {

                    //issuedqty = issuedqty + decimal.Parse(GridView2.Rows[i].Cells[2].Text.ToString());
                    //totalissuequnt = issuedqty;


                    foreach (GridViewRow row in GridView2.Rows)
                    {
                        if (GridView2.DataKeys[i].Values[0].ToString() == "35" || GridView2.DataKeys[i].Values[0].ToString() == "22")
                        {
                            decimal issuedqty_wheat = 0;
                            issuedqty_wheat = issuedqty_wheat + decimal.Parse(GridView2.Rows[i].Cells[2].Text.ToString());
                            totalissuequnt_Wheat = issuedqty_wheat;

                        }
                        if (GridView2.DataKeys[i].Values[0].ToString() == "3" || GridView2.DataKeys[i].Values[0].ToString() == "4")
                        {
                            decimal issuedqty_rice = 0;
                            issuedqty_rice = issuedqty_rice + decimal.Parse(GridView2.Rows[i].Cells[2].Text.ToString());
                            totalissuequnt_rice = issuedqty_rice;

                        }
                        if (GridView2.DataKeys[i].Values[0].ToString() == "19")
                        {
                            decimal issuedqty_salt = 0;
                            issuedqty_salt = issuedqty_salt + decimal.Parse(GridView2.Rows[i].Cells[2].Text.ToString());
                            totalissuequnt_salt = issuedqty_salt;

                        }
                        if (GridView2.DataKeys[i].Values[0].ToString() == "23")
                        {
                            decimal issuedqty_sugar = 0;
                            issuedqty_sugar = issuedqty_sugar + decimal.Parse(GridView2.Rows[i].Cells[2].Text.ToString());
                            totalissuequnt_sugar = issuedqty_sugar;

                        }
                        if (GridView2.DataKeys[i].Values[0].ToString() == "12")
                        {
                            decimal issuedqty_maize = 0;
                            issuedqty_maize = issuedqty_maize + decimal.Parse(GridView2.Rows[i].Cells[2].Text.ToString());
                            totalissuequnt_maize = issuedqty_maize;

                        }

                    }
                }


                decimal tot = 0;
                for (int R = 0; R < Gridissue.Rows.Count; R++)
                {

                    if (((CheckBox)Gridissue.Rows[R].FindControl("chkBoxId")).Checked == true)
                    {
                        if (Gridissue.HeaderRow.Cells[3].Text == "WheatAlloc")
                        {
                            decimal tot_wheat = 0;
                            tot_wheat = tot_wheat + decimal.Parse(Gridissue.Rows[R].Cells[3].Text.ToString());
                            do_qunat_wheat = tot_wheat;
                            if (do_qunat_wheat < totalissuequnt_Wheat)
                            {
                                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('issue Quantity should not be greater than TO Quantity');</script>");

                                return;
                            }

                        }
                        if (Gridissue.Columns[4].HeaderText == "RiceAlloc")
                        {
                            decimal tot_rice = 0;
                            tot_rice = tot_rice + decimal.Parse(Gridissue.Rows[R].Cells[4].Text.ToString());
                            do_qunat_rice = tot_rice;
                            if (do_qunat_rice < totalissuequnt_rice)
                            {
                                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('issue Quantity should not be greater than TO Quantity');</script>");

                                return;
                            }
                        }
                        if (Gridissue.Columns[5].HeaderText == "SugarAlloc")
                        {
                            decimal tot_sugar = 0;
                            tot_sugar = tot_sugar + decimal.Parse(Gridissue.Rows[R].Cells[5].Text.ToString());
                            do_qunat_sugar = tot_sugar;
                            if (do_qunat_sugar < totalissuequnt_sugar)
                            {
                                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('issue Quantity should not be greater than TO Quantity');</script>");

                                return;
                            }

                        }
                        if (Gridissue.Columns[6].HeaderText == "SaltAlloc")
                        {
                            decimal tot_salt = 0;
                            tot_salt = tot_salt + decimal.Parse(Gridissue.Rows[R].Cells[6].Text.ToString());
                            do_qunat_salt = tot_salt;
                            if (do_qunat_salt < totalissuequnt_salt)
                            {
                                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('issue Quantity should not be greater than TO Quantity');</script>");
                                return;
                            }

                        }
                        if (Gridissue.Columns[7].HeaderText == "MaizeAlloc")
                        {
                            decimal tot_maize = 0;
                            tot_maize = tot_maize + decimal.Parse(Gridissue.Rows[R].Cells[7].Text.ToString());
                            do_qunat_maize = tot_maize;
                            if (do_qunat_maize < totalissuequnt_maize)
                            {
                                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('issue Quantity should not be greater than TO Quantity');</script>");
                                return;
                            }

                        }

                    }
                    totalissuequnt = do_qunat_salt + do_qunat_sugar + do_qunat_wheat + do_qunat_rice + do_qunat_maize;
                    if (totalissuequnt == 0)
                    {
                        Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Transport Order No. detail');</script>");

                        return;

                    }

                    if (do_qunat_salt >= totalissuequnt_salt || do_qunat_sugar >= totalissuequnt_sugar || do_qunat_rice >= totalissuequnt_rice || do_qunat_wheat >= totalissuequnt_Wheat || do_qunat_maize >= totalissuequnt_maize)
                    {

                        string opid = Session["OperatorId"].ToString();
                        string state = Session["State_Id"].ToString();
                        Label1.Text = "";
                        Label1.ForeColor = System.Drawing.Color.Blue;
                        if (ddl_TO_no.SelectedItem.Text == "Select")
                        {
                            Label1.ForeColor = System.Drawing.Color.Red;
                            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Transport Order No.');</script>");
                            Label1.Text = "Please Select Transport Order No. ...";
                        }
                        else if (ddl_godown.SelectedItem.Text == "Select")
                        {
                            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Godown ...');</script>");
                            Label1.Text = "Please Select Godown ...";
                        }

                        else
                        {
                            string do_valid_days = Session["do_valid"].ToString();
                            string issueddate = "";
                            if (tx_issued_date.Text == "")
                            {
                                issueddate = DateTime.Today.Date.ToString();
                            }
                            else
                            {
                                issueddate = tx_issued_date.Text;

                            }
                            DateTime frmDate = DateTime.Parse(getDate_MDY(tx_do_date.Text.Trim()));
                            DateTime toDate = DateTime.Parse(getDate_MDY(issueddate));
                            string chk_valid_days = get_days(frmDate, toDate);

                            //if (CheckNullInt(chk_valid_days) > CheckNullInt(do_valid_days))
                            //{
                            //    Label1.ForeColor = System.Drawing.Color.Red;
                            //    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Validity  has Expired please check Issued Date ...');</script>");
                            //    Label1.Text = "Validity  has Expired please check Issued Date ...";
                            //}
                            if (CheckNullInt(chk_valid_days) < 0)
                            {
                                Label1.ForeColor = System.Drawing.Color.Red;
                                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Issued Date can not be less than DO Date please check Issued Date ...');</script>");
                                Label1.Text = "Issued Date can not be less than DO Date please check Issued Date ...";
                                return;
                            }
                            else
                            {
                                string month = ddl_allot_month.SelectedItem.Value;
                                int permit_valid = 0;
                                //Double qty = Convert.ToDouble(tx_qty.Text);

                                string fpsname = ddl_fpsname.SelectedValue.ToString();


                                //string cetId = issue_centre_code.Substring(issue_centre_code.Length - 5);    // Left 5 digit from center for Do generation

                                string year_do = System.DateTime.Now.Date.ToString("yy");    // For DO generation year wise (29/03/14)

                                string selectmax = "select isnull(max(cast(delivery_order_no as bigint)),0) as Do_Challan from Issued_Doorstep_do_fps where district_code='" + distid + "'  and fps_code='" + fpsname + "'  and allotment_month='"+ ddl_allot_month.SelectedValue.ToString() +"' and allotment_year='"+ ddd_allot_year.SelectedValue.ToString() +"' and trans_date >'3-12-2015' and Transport_order is not null ";

                                SqlCommand cmdmax = new SqlCommand(selectmax, con);
                                SqlDataAdapter damax = new SqlDataAdapter(cmdmax);

                                DataSet dsmax = new DataSet();

                                damax.Fill(dsmax);
                                string DO_ID = dsmax.Tables[0].Rows[0]["Do_Challan"].ToString();
                                string monthcode = "";
                                if (ddl_allot_month.SelectedValue.Length == 1)
                                {
                                    monthcode = "0" + ddl_allot_month.SelectedValue.ToString();
                                }
                                else
                                {
                                    monthcode = ddl_allot_month.SelectedValue.ToString();
                                }

                                if (DO_ID == "0")
                                {
                                    DO_ID = year_do + monthcode + fpsname + "100";
                                }
                                else
                                {
                                    string fordo = DO_ID.Substring(DO_ID.Length - 3);

                                    Int64 DO_ID_new = Convert.ToInt64(fordo);

                                    DO_ID_new = DO_ID_new + 1;

                                    string combine = DO_ID_new.ToString();

                                    DO_ID = year_do + monthcode + fpsname + combine;

                                }


                                string Do_Challan = DO_ID;
                                try
                                {

                                    for (int i = 0; GridView2.Rows.Count > i; i++)
                                    {

                                        string dist = distid;
                                        string issue_centre_code = sid;
                                        string issue_date = getDate_MDY(tx_issued_date.Text);
                                        string do_no = ddl_TO_no.SelectedItem.Text;
                                        string gate_pass = tx_gatepass.Text;
                                        decimal do_qty = CheckNull(tx_balance_qty.Text);
                                        decimal issue_qty = CheckNull(tx_qty_to_issue.Text);
                                        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                                        string notrans = "N";
                                        if (qunt == "1")
                                        {

                                        }
                                        else
                                        {
                                            if (con.State == ConnectionState.Closed)
                                            {
                                                con.Open();
                                            }
                                            int trnscnt = 0;
                                            string docount = "";
                                            string transid = "";
                                            string strqr = "select count(delivery_order_no) as rwcount  from dbo.issue_against_Doorstep_do where delivery_order_no='" + Do_Challan + "' and district_code='" + dist + "' and issue_against_Doorstep_do.Transport_order is not  null";
                                            cmd.CommandText = strqr;
                                            cmd.Connection = con;
                                            dr = cmd.ExecuteReader();
                                            while (dr.Read())
                                            {
                                                docount = dr["rwcount"].ToString();
                                            }
                                            dr.Close();
                                    
                                          
                                                trnscnt = CheckNullInt(docount) + 1;
                                                transid =  Do_Challan.ToString() + "-"+docount;
                                            
                                           
                                            decimal lift_qty = 0;
                                            string str2 = "select round(convert(decimal(18,5),lift_qty),5) as lift_qty from dbo.sum_trans_do where district_code='" + dist + "' and issueCentre_code='" + issue_centre_code + "' and trans_month=" + ddl_allot_month.SelectedItem.Value + " and trans_year=" + ddd_allot_year.SelectedItem.Text + "";
                                            cmd.Connection = con;
                                            cmd.CommandText = str2;
                                            dr = cmd.ExecuteReader();
                                            while (dr.Read())
                                            {
                                                lift_qty = CheckNull(dr["lift_qty"].ToString());
                                            }
                                            dr.Close();
                                            if (con_opdms.State == ConnectionState.Closed)
                                            {
                                                con_opdms.Open();
                                            }
                                            SqlTransaction Trans_csms;
                                            Trans_csms = con.BeginTransaction();
                                            SqlTransaction Trans_opdms;
                                            Trans_opdms = con_opdms.BeginTransaction();
                                            cmd.Connection = con;
                                            cmd_opdms.Connection = con_opdms;
                                            cmd.Transaction = Trans_csms;
                                            cmd_opdms.Transaction = Trans_opdms;

                                            try
                                            {
                                                if (con.State == ConnectionState.Closed)
                                                {
                                                    con.Open();
                                                }


                                                string mystr = "select* from issue_against_Doorstep_do Where delivery_order_no='" + Do_Challan + "' AND district_code='" + dist + "' AND issueCentre_code='" + issue_centre_code + "' AND allotment_month='" + ddl_allot_month.SelectedItem.Value + "' AND allotment_year='" + ddd_allot_year.SelectedValue.ToString() + "'  AND cmd='" +  GridView2.DataKeys[i].Values[0].ToString() + "' AND qty_issue='" + GridView2.Rows[i].Cells[2].Text.ToString() + "' AND bags='" + GridView2.Rows[i].Cells[3].Text.ToString() + "' AND Source='" + GridView2.DataKeys[i].Values[2].ToString() + "' AND Godown='" + GridView2.DataKeys[i].Values[1].ToString() + "' AND gate_pass='" + GridView2.Rows[i].Cells[4].Text.ToString() + "' and issue_date='" + issue_date + "'";
                                                cmd.Connection = con;
                                                cmd.CommandText = mystr;
                                                dr = cmd.ExecuteReader();


                                                if (dr.HasRows)
                                                {

                                                    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Quantity Already Issued .....');</script>");

                                                    Label1.Text = "Quantity Already Issued";
                                                    return;


                                                }
                                                dr.Close();

                                                string IssuetoName = "FPS";
                                                string commodityid = GridView2.DataKeys[i].Values[0].ToString();
                                                string godownid = GridView2.DataKeys[i].Values[1].ToString();
                                                string source = GridView2.DataKeys[i].Values[2].ToString();
                                                string truckno = GridView2.Rows[i].Cells[4].Text.ToString();
                                                string cropyear = GridView2.Rows[i].Cells[5].Text.ToString();
                                                string issuedquant = GridView2.Rows[i].Cells[2].Text.ToString();
                                                string issuedbags = GridView2.Rows[i].Cells[3].Text.ToString();
                                                string branchid = GridView2.DataKeys[i].Values[3].ToString();

                                                string str1 = "INSERT INTO dbo.issue_against_Doorstep_do(State_Id,trans_id,delivery_order_no,Transport_order,district_code,issueCentre_code,allotment_month,allotment_year,cmd,issue_to,qty_issue,bags,Source,Godown,issue_date,gate_pass,created_date,updated_date,swc_status,ip_add,OperatorID,NoTransaction,Issue_to_LS_name,Transporter_id,truck_no,crop_year,Branch_id) VALUES('" + state + "','" + transid + "','" + Do_Challan + "','" + ddl_TO_no.SelectedValue.ToString() + "','" + dist + "','" + issue_centre_code + "'," + ddl_allot_month.SelectedItem.Value + "," + ddd_allot_year.SelectedItem.Text + ",'" + commodityid + "','FPS'," + issuedquant + "," + issuedbags + ",'" + source + "','" + godownid + "','" + issue_date + "','" + truckno + "',getdate(),'','N','" + ip + "','" + opid + "','" + notrans + "','FPS','" + ddltransporter.SelectedValue.ToString() + "','" + truckno + "','" + cropyear + "','" + branchid + "')";
                                                cmd.CommandText = str1;
                                                cmd.ExecuteNonQuery();

                                                string strIssue = "INSERT INTO dbo.issue_against_do(State_Id,trans_id,delivery_order_no,Transport_order,district_code,issueCentre_code,allotment_month,allotment_year,issue_to,qty_issue,bags,Source,Godown,issue_date,gate_pass,created_date,updated_date,swc_status,ip_add,OperatorID,NoTransaction,Issue_to_LS_name,commodity,DOType,Transporter_id,truck_no,crop_year) VALUES('" + state + "','" + transid + "','" + Do_Challan + "','" + ddl_TO_no.SelectedValue.ToString() + "','" + dist + "','" + issue_centre_code + "'," + ddl_allot_month.SelectedItem.Value + "," + ddd_allot_year.SelectedItem.Text + ",'FPS'," + issuedquant + "," + issuedbags + ",'" + source + "','" + godownid + "','" + issue_date + "','" + truckno + "',getdate(),'','N','" + ip + "','" + opid + "','" + notrans + "',N'" + IssuetoName + "','" + commodityid + "','TO','" + ddltransporter.SelectedValue.ToString() + "','" + truckno + "','" + cropyear + "')";
                                                cmd.CommandText = strIssue;
                                                cmd.ExecuteNonQuery();


                                                //GetSelected();
                                                //get_do_data();
                                                lift_qty = lift_qty + Convert.ToDecimal(issuedquant);
                                                str1 = "update dbo.sum_trans_do set lift_qty=round(" + lift_qty + ",5) where district_code='" + dist + "' and issueCentre_code='" + issue_centre_code + "' and trans_month=" + ddl_allot_month.SelectedItem.Value + " and trans_year=" + ddd_allot_year.SelectedItem.Text + "";
                                                cmd.CommandText = str1;
                                                cmd.ExecuteNonQuery();


                                                {
                                                    int rcount = 0;
                                                    string dist1 = distid;
                                                    string issue_centre_code1 = sid;
                                                    string ip1 = Request.ServerVariables["REMOTE_ADDR"].ToString();


                                                    //if (totalissuequnt >= CheckNull(tx_balance_qty.Text))
                                                    //{
                                                    //    str1 = "update dbo.DPY_TranportOrder set DO_Challan='Y' where  TransportOrder='" + ddl_TO_no.SelectedValue.ToString() + "' and DistrictId='" + distid + "'  and IssueCenter='" + sid + "' and AllotMonth=" + ddl_allot_month.SelectedItem.Value + " and AllotYear=" + ddd_allot_year.SelectedItem.Text + "";
                                                    //    cmd.CommandText = str1;
                                                    //    cmd.ExecuteNonQuery();
                                                    //}
                                                    string tempos = "NNN";
                                                    string strope = "";
                                                    tempos = "NNN";
                                                    strope = "select *  from dbo.issue_opening_balance  where District_Id='" + dist1 + "'and Depotid='" + issue_centre_code1 + "'and Commodity_Id='" + commodityid + "'and Scheme_Id='116' and Godown='" + godownid + "' and Source='" + source + "'";
                                                    cmd.CommandText = strope;
                                                    dr = cmd.ExecuteReader();
                                                    while (dr.Read())
                                                    {
                                                        tempos = "YYY";
                                                    }
                                                    dr.Close();
                                                    if (tempos == "YYY")
                                                    {
                                                        strope = "Update dbo.issue_opening_balance set Current_Balance=round(convert(decimal(18,5),Current_Balance)-" + issuedquant + ",5),Current_Bags=Current_Bags-" + issuedbags + " where District_Id='" + dist1 + "'and Depotid='" + issue_centre_code1 + "'and Commodity_Id='" + commodityid + "'and Scheme_Id='116' and Godown='" + godownid + "' and Source='" + source + "'";
                                                        cmd.CommandText = strope;
                                                        cmd.ExecuteNonQuery();
                                                    }
                                                    else
                                                    {
                                                        strope = "insert into dbo.issue_opening_balance(State_Id,District_Id,Depotid,Commodity_Id,Scheme_Id,Category_Id,Godown,Crop_year,Bags,Quantity,Source,Current_Balance,Current_Bags,Month,Year,IP_Address,Stock_Date,CreatedDate,UpdatedDate,DeletedDate) values('23','" + distid + "','" + sid + "','" + commodityid + "','116','','" + godownid + "','',0,0,'" + source + "'," + -Convert.ToDecimal(issuedquant) + "," + -CheckNullInt(issuedbags) + "," + DateTime.Today.Month + "," + DateTime.Today.Year + ",'" + ip1 + "',getdate(),getdate(),'','')";
                                                        cmd.CommandText = strope;
                                                        cmd.ExecuteNonQuery();
                                                    }

                                                }
                                                for (int j = 0; j < Gridissue.Rows.Count; j++)
                                                {
                                                    int rcount = 0;
                                                    string dist1 = distid;
                                                    string issue_centre_code1 = sid;
                                                    string ip1 = Request.ServerVariables["REMOTE_ADDR"].ToString();

                                                    if (((CheckBox)Gridissue.Rows[j].FindControl("chkBoxId")).Checked == true)
                                                    {
                                                        int godown = 0;

                                                        rcount = rcount + 1;
                                                        string fpscode = Gridissue.Rows[j].Cells[1].Text.ToString();
                                                        string do_no1 = ddl_TO_no.SelectedItem.Text;
                                                        string comm_name = "";
                                                        decimal do_qty1 = 0;
                                                        string comm = "";
                                                        string comm_str = "";


                                                        if (totalissuequnt_salt != 0 && commodityid == "19")
                                                        {

                                                            comm = "19";
                                                            do_qty1 = totalissuequnt_salt;
                                                            comm_name = "salt";
                                                            hd_check_salt.Value = "salt";

                                                        }
                                                        else if (totalissuequnt_sugar != 0 && commodityid == "23")
                                                        {
                                                            comm = "23";
                                                            do_qty1 = totalissuequnt_sugar;
                                                            comm_name = "sugar";
                                                            hd_check_sugar.Value = "sugar";
                                                        }
                                                        else if (totalissuequnt_rice != 0 && commodityid == "3")
                                                        {
                                                            comm = "3";
                                                            do_qty1 = totalissuequnt_rice;
                                                            comm_name = "rice";
                                                            hd_check_rice.Value = "rice";
                                                        }
                                                        else if (totalissuequnt_rice != 0 && commodityid == "4")
                                                        {
                                                            comm = "4";
                                                            do_qty1 = totalissuequnt_rice;
                                                            comm_name = "rice";
                                                            hd_check_rice.Value = "rice";
                                                        }
                                                        else if (totalissuequnt_Wheat != 0 && commodityid == "22")
                                                        {
                                                            comm = "22";
                                                            do_qty1 = totalissuequnt_Wheat;
                                                            comm_name = "wheat";
                                                            hd_check_wheat.Value = "wheat";
                                                        }
                                                        else if (totalissuequnt_Wheat != 0 && commodityid == "35")
                                                        {
                                                            comm = "35";
                                                            do_qty1 = totalissuequnt_Wheat;
                                                            comm_name = "wheat";
                                                            hd_check_wheat.Value = "wheat";
                                                        }
                                                        else if (totalissuequnt_maize != 0 && commodityid == "12")
                                                        {
                                                            comm = "12";
                                                            do_qty1 = totalissuequnt_maize;
                                                            comm_name = "Maize";
                                                            hd_check_maize.Value = "Maize";
                                                        }


                                                        //string comm = Gridissue.Rows[j].Cells[8].Text.ToString();
                                                        string scheme = "116";
                                                        if (comm_name.ToLower().Contains("wheat"))
                                                        {
                                                            comm_str = "wheat_phh_lift";
                                                        }
                                                        if (comm_name.ToLower().Contains("rice"))
                                                        {
                                                            comm_str = "rice_phh_lift";
                                                        }
                                                        if (comm_name.ToLower().Contains("sugar"))
                                                        {
                                                            comm_str = "sugar_lift";
                                                        }
                                                        if (comm_name.ToLower().Contains("salt"))
                                                        {
                                                            comm_str = "salt_lift";
                                                        }
                                                        if (comm_name.ToLower().Contains("maize"))
                                                        {
                                                            comm_str = "Maize_PHH_lift";
                                                        }

                                                        //string scheme_name = Gridissue.Rows[j].Cells[4].Text.ToString(); ;
                                                        string ipadd = "";
                                                        decimal qty_lift_old = 0;
                                                        decimal qty_lift_new = 0;
                                                        string docount1 = "";
                                                        int trnscnt1 = 0;
                                                        if (comm_str != "")
                                                        {
                                                            str1 = "select " + comm_str + " ,lft_insert  from pds.fps_allot  where district_code='" + distid + "'  and fps_code='" + fpscode + "' and month=" + ddl_allot_month.SelectedItem.Value + " and Year=" + ddd_allot_year.SelectedItem.Text + "";
                                                            cmd_opdms.CommandText = str1;
                                                            dr_opdms = cmd_opdms.ExecuteReader();
                                                            while (dr_opdms.Read())
                                                            {
                                                                qty_lift_old = CheckNull(dr_opdms[comm_str].ToString());
                                                                ipadd = dr_opdms["lft_insert"].ToString();
                                                            }
                                                            dr_opdms.Close();
                                                            if (qty_lift_old == 0)
                                                            {
                                                                qty_lift_new = qty_lift_old + 100 * do_qty1;
                                                                str1 = "update pds.fps_allot set " + comm_str + "=round(" + qty_lift_new + ",5)  where district_code='" + distid + "'  and fps_code='" + fpscode + "' and month=" + ddl_allot_month.SelectedItem.Value + " and Year=" + ddd_allot_year.SelectedItem.Text + "";
                                                                cmd_opdms.CommandText = str1;
                                                                cmd_opdms.ExecuteNonQuery();
                                                            }

                                                            string strr = "";
                                                            strr = "update pds.fps_allot_mpsc set " + comm_str + "=isnull(convert(decimal(18,5)," + comm_str + "),0)+round(" + 100 * do_qty1 + ",5),lft_updated_on=getdate()  where district_code='" + distid + "'  and  depot_code='" + sid + "' and  fps_code='" + fpscode + "' and  month=" + ddl_allot_month.SelectedItem.Value + " and  Year=" + ddd_allot_year.SelectedItem.Text + "";
                                                            cmd.CommandText = strr;
                                                            cmd.ExecuteNonQuery();
                                                        }
                                                        trnscnt1 = 0;
                                                        docount1 = "";
                                          

                                                        str1 = "INSERT INTO dbo.Issued_DoorStep_do_fps(State_Id,trans_id,delivery_order_no,Transport_order,district_code,issueCentre_code,allotment_month,allotment_year,fps_code,commodity,scheme_id,issue_qty,lift_qty,trans_date,ip_add,issue_date,DeliverQuantity,DeliverDate,UpdatedDate)VALUES('" + state + "','" + transid + "','" + Do_Challan + "','" + do_no1 + "','" + dist1 + "','" + issue_centre_code1 + "'," + ddl_allot_month.SelectedItem.Value + "," + ddd_allot_year.SelectedItem.Text + ",'" + fpscode + "','" + comm + "','" + scheme + "',round(" + issuedquant + ",5),round(" + issuedquant + ",5),getdate(),'" + ip1 + "','" + issue_date + "','','','')";
                                                        cmd.CommandText = str1;
                                                        cmd.ExecuteNonQuery();
                                                        str1 = "INSERT INTO dbo.Issued_do_fps(State_Id,trans_id,delivery_order_no,Transport_order,district_code,issueCentre_code,allotment_month,allotment_year,fps_code,commodity,scheme_id,issue_qty,lift_qty,trans_date,ip_add,DOType,issue_date)VALUES('" + state + "','" + transid + "','" + Do_Challan + "','" + do_no1 + "','" + dist1 + "','" + issue_centre_code1 + "'," + ddl_allot_month.SelectedItem.Value + "," + ddd_allot_year.SelectedItem.Text + ",'" + fpscode + "','" + comm + "','" + scheme + "',round(" + issuedquant + ",5),round(" + issuedquant + ",5),getdate(),'" + ip1 + "','TO','" + issue_date + "')";
                                                        cmd.CommandText = str1;
                                                        cmd.ExecuteNonQuery();

                                                    }
                                                }

                                                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Data Saved Successfully!your Delivery Challan No. is- " + DO_ID + "');</script>");
                                                StringBuilder sbScript = new StringBuilder();

                                                sbScript.Append("<script language='JavaScript' type='text/javascript'>\n");
                                                sbScript.Append("<!--\n");
                                                sbScript.Append(this.GetPostBackEventReference(this, "PBArg") + ";\n");
                                                sbScript.Append("// -->\n");
                                                sbScript.Append("</script>\n");

                                                this.RegisterStartupScript("AutoPostBackScript", sbScript.ToString());
                                                Label1.Text = "Data Saved Successfully ...";
                                                //ddl_do_no.SelectedIndex = 0;
                                                //panel_do.Enabled = false;
                                                btnsave.Enabled = false;
                                                Trans_csms.Commit();
                                                Trans_opdms.Commit();
                                                hlinkpdo.Visible = true;
                                                hlinkpdo.Enabled = true;
                                                ddl_fpsname.Enabled = false;
                                                lbl_dcno.Visible = true;
                                                lbl_dcno.Text = DO_ID;
                                                Session["fpscode"] = ddl_fpsname.SelectedValue.ToString();
                                                Session["TO"] = ddl_TO_no.SelectedValue.ToString();
                                                Session["DeliveryChallanTO"] = DO_ID;
                                                Session["TOdate"] = tx_do_date.Text;
                           

                                            }
                                            catch (Exception ex)
                                            {
                                                dr.Close();
                                                dr_opdms.Close();
                                                Trans_csms.Rollback();
                                                Trans_opdms.Rollback();
                                                Label1.Text = ex.Message;
                                            }
                                            finally
                                            {
                                                if (con.State == ConnectionState.Open)
                                                {
                                                    con.Close();
                                                }
                                                if (con_opdms.State == ConnectionState.Open)
                                                {
                                                    con_opdms.Close();
                                                }
                                            }
                                        }
                                        //tx_qty_to_issue.Text = "";
                                        //tx_bags.Text = "";
                                        //tx_gatepass.Text = "";
                                        //ddl_do_no.SelectedIndex = 0;

                                    }
                                }
                                catch (Exception ex)
                                {
                                    lbl_error.Text = ex.ToString();

                                }

                            }

                        }
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('TO Quantity for selected fps commodities should not be greater or lesser than TO commodity Quantity ');</script>");


                    }
                }
            }
            else
            {
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select correct date of issue');</script>");


            }
            Session["issubmited"] = "Yes";
        }
    }

    protected void get_Transporter()
    {
        try
        {
            string dist = distid;
            ddltransporter.Items.Clear();
            cmd.CommandText = "SELECT distinct Transporter_ID,(Transporter_Name+',('+Transporter_ID+')') as Transporter_Name from Transporter_Table where transport_ID='7' and Distt_ID ='" + distid + "' order by Transporter_Name";
            cmd.Connection = con;

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }


            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ListItem lstitem = new ListItem();
                lstitem.Text = dr["Transporter_Name"].ToString();
                lstitem.Value = dr["Transporter_ID"].ToString();
                ddltransporter.Items.Add(lstitem);
            }
            ListItem lstitem1 = new ListItem();
            lstitem1.Text = "Select";
            lstitem1.Value = "N";
            ddltransporter.Items.Insert(0, lstitem1);
            dr.Close();

            if (con.State == ConnectionState.Open)
            {

                con.Close();
            }
        }
        catch (Exception)
        {
        }
    }
    protected void get_fpsname()
    {
      

        try
        {
            mobj = new MoveChallan(ComObj);
            string qry = "SELECT DPY_TranportOrder.FPSCode, (fps_master.fps_Uname +'('+DPY_TranportOrder.FPSCode+')') as fps_Uname  from DPY_TranportOrder LEFT join opdms.pds.fps_master on fps_master.fps_code =  DPY_TranportOrder.FPSCode where DPY_TranportOrder.AllotMonth = '" + ddl_allot_month.SelectedItem.Value + "' and DPY_TranportOrder.AllotYear = '" + ddd_allot_year.SelectedItem.Text + "'   and DPY_TranportOrder.RouteNumber='" + ddl_route.SelectedValue.ToString() + "' and DPY_TranportOrder.FeedNumber='" + ddl_feed.SelectedValue.ToString() + "' and DPY_TranportOrder.DO_Challan='N' and DPY_TranportOrder.DistrictId='" + distid + "'  and DPY_TranportOrder.TransportOrder='" + ddl_TO_no.SelectedValue.ToString() + "' order by FPSCode";
            DataSet ds = mobj.selectAny(qry);

            ddl_fpsname.DataSource = ds.Tables[0];
            ddl_fpsname.DataTextField = "fps_Uname";
            ddl_fpsname.DataValueField = "FPSCode";
            ddl_fpsname.DataBind();
            ddl_fpsname.Items.Insert(0, "--Select--");
        }
        catch (Exception)
        {
        }
    }
    void GetCapGodown()
    {
        try
        {
            //DateTime do_date = new DateTime();

            DateTime dodate = Convert.ToDateTime(DateTime.ParseExact(tx_do_date.Text.Trim(), "dd/MM/yyyy", null).ToString("MM/dd/yyyy"));
            string Godown = ddl_godown.SelectedItem.Value;
            string openingdate;
            Int64 comid = Convert.ToInt64(Godown);
            if (dodate >= Convert.ToDateTime(DateTime.ParseExact("01/01/2015", "dd/MM/yyyy", null).ToString("MM/dd/yyyy")))
            {
                openingdate = "01/01/2015";
            }
            else
            {
                openingdate = "01/04/2014";
            }
            string pqry = "space_forcommodity_ingodown";

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmdpqty = new SqlCommand(pqry, con);
            cmdpqty.CommandType = CommandType.StoredProcedure;


            cmdpqty.Parameters.Add("@district_code", SqlDbType.NVarChar).Value = distid;
            cmdpqty.Parameters.Add("@Depotid", SqlDbType.NVarChar).Value = sid;
            cmdpqty.Parameters.Add("@GodownId", SqlDbType.NVarChar).Value = Godown;
            cmdpqty.Parameters.Add("@commodity", SqlDbType.NVarChar).Value = ddl_commodity.SelectedValue.ToString();
            //cmdpqty.Parameters.Add("@source", SqlDbType.NVarChar).Value = ddlsarrival.SelectedValue.ToString();
            cmdpqty.Parameters.Add("@Openingdate", SqlDbType.NVarChar).Value = openingdate;
            DataSet ds = new DataSet();
            SqlDataAdapter dr = new SqlDataAdapter(cmdpqty);

            dr.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                double stock = Math.Round(Convert.ToDouble(ds.Tables[0].Rows[0]["Total"].ToString()), 5);

                double bags = Convert.ToDouble(ds.Tables[0].Rows[0]["Totalbags"].ToString());

                tx_cur_bal.Text = Convert.ToString(stock);
                tx_cur_bags.Text = Convert.ToString(bags);

                //txtcurntcap.Text = Convert.ToString(stock);
                //txtavalcap.Text = (System.Math.Round(CheckNull(txtmaxcap.Text) - CheckNull(txtcurntcap.Text), 5)).ToString();

            }
        }
        catch
        {
        }

    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
     
        Panel2.Visible = true;
        bool checkstatus = false;
        try
        {
            if (tx_qty_to_issue.Text == "")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Quantity is required!'); </script> ");
                return;
            }
            else if (tx_bags.Text == "")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Bags to be added in Godown is required!'); </script> ");
                return;
            }
            else if (tx_gatepass.Text == "")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Truck no.  is required!'); </script> ");
                return;
            }
            else if (ddl_godown.SelectedIndex == 0)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Godown!'); </script> ");
                return;
            }
            else if (ddlsarrival.SelectedIndex == 0)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Source of arrival!'); </script> ");
                return;
            }
            else if ((tx_cur_bal.Text != "") && (Convert.ToDecimal(tx_cur_bal.Text) < Convert.ToDecimal(tx_qty_to_issue.Text)))
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Insuffient Capacity in the Selected Godown!'); </script> ");
                return;
            }
                
            else
            {

                if (ddl_godown.Items.Count > 0)
                {

                    btnsave.Enabled = true;
                    if (Session["dt1"] == null)
                    {
                        Dt1 = CreateTable();
                        Session["dt1"] = Dt1;
                    }
                    // adding rows to the datatable
                    DataRow dr = ((DataTable)Session["dt1"]).NewRow();
                    ((DataTable)Session["dt1"]).AcceptChanges();

                   
                    dr["Godown"] = ddl_godown.SelectedItem;
                    dr["qty_issue"] = tx_qty_to_issue.Text.Trim();
                    dr["bags"] = tx_bags.Text.Trim();
                    dr["gate_pass"] = tx_gatepass.Text.Trim();
                    dr["commodity"] = ddl_commodity.SelectedItem;

                    dr["commodityid"] = ddl_commodity.SelectedValue;
                    dr["Godownid"] = ddl_godown.SelectedValue;
                    dr["crop_year"] = ddlcropyear.SelectedValue;
                    dr["Source_ID"] = ddlsarrival.SelectedValue;

                    dr["Branch_id"] = ddl_branch.SelectedValue;

                    if (GridView2.Rows.Count > 0)
                    {
                        int i;

                        // checking whether or not the godown is already added to the grid view
                        for (i = 0; i <= GridView2.Rows.Count - 1; i++)
                        {
                            string commodityid = GridView2.DataKeys[i].Values[0].ToString();
                            string selectcomm = ddl_commodity.SelectedValue.ToString();

                            string godownid = GridView2.DataKeys[i].Values[1].ToString();
                            string selectgodown = ddl_godown.SelectedValue.ToString();
                            if (godownid == selectgodown && commodityid == selectcomm)
                            {
                                checkstatus = true;
                            }
                        }
                        if (checkstatus == false)
                        {
                            ((DataTable)Session["dt1"]).Rows.Add(dr);
                            ((DataTable)Session["dt1"]).AcceptChanges();
                            GridView2.DataSource = (DataTable)Session["dt1"];
                            GridView2.DataBind();


                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Entry for this Godown is already done'); </script> ");
                        }
                    }
                    else
                    {
                        ((DataTable)Session["dt1"]).Rows.Add(dr);
                        ((DataTable)Session["dt1"]).AcceptChanges();
                        GridView2.DataSource = (DataTable)Session["dt1"];
                        GridView2.DataBind();



                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('No Godown under the selected Commodity  Number'); </script> ");
                }

            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please enter valid quantity, try again'); </script> ");
        }
        tx_qty_to_issue.Text = "";
        tx_cur_bal.Text = "";
        tx_cur_bags.Text = "";
       
        get_comm();
        GetGodown();
    }

    private DataTable CreateTable()
    {
        DataTable dt = new DataTable();//DataTable is created
        DataColumn Godown = new DataColumn("Godown", Type.GetType("System.String"));
        DataColumn qty_issue = new DataColumn("qty_issue", Type.GetType("System.Decimal"));
        DataColumn bags = new DataColumn("bags", Type.GetType("System.Int32"));
        DataColumn truck = new DataColumn("gate_pass", Type.GetType("System.String"));
        DataColumn commodity = new DataColumn("commodity", Type.GetType("System.String"));
        DataColumn commodityid = new DataColumn("commodityid", Type.GetType("System.Int32"));

        DataColumn Godownid = new DataColumn("Godownid", Type.GetType("System.Int64"));

        DataColumn crop_year = new DataColumn("crop_year", Type.GetType("System.String"));

        DataColumn Source_ID = new DataColumn("Source_ID", Type.GetType("System.String"));

        DataColumn branchid = new DataColumn("Branch_id", Type.GetType("System.String"));
     
        dt.Columns.Add(Godown);//Column is added to the DataTable
        dt.Columns.Add(qty_issue);//Column is added to the DataTable
        dt.Columns.Add(bags);//Column is added to the DataTable
        dt.Columns.Add(truck);//Column is added to the DataTable


        dt.Columns.Add(commodity);//Column is added to the DataTable
        dt.Columns.Add(commodityid);//Column is added to the DataTable
        dt.Columns.Add(Godownid);
        dt.Columns.Add(crop_year);
        dt.Columns.Add(Source_ID);

        dt.Columns.Add(branchid);
        dt.AcceptChanges();
        return dt;
    }







    
    protected void ddl_commodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSource();
     
    
    }
    protected void ddl_godown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_TO_no.SelectedItem.Text == "Select")
        {
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Text = "Please Select Transport Order No. ...";
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Transport Order No.');</script>");
            ddl_godown.SelectedIndex = 0;
        }
        else if (ddl_commodity.SelectedItem.Text == "--Select--")
        {
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Text = "Please Select commodity ...";
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select commodity ...');</script>");
            ddl_godown.SelectedIndex = 0;
        }
        else if (ddl_godown.SelectedItem.Text == "Select")
        {
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Text = "Please Select Godown. ...";
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Godown ...');</script>");
        }
        else
        {

            GetCapGodown();
        }

    }
    void filltotalfps()
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string querysuspension = "SELECT SuspendFPS,ConnectFPS ,tbl_rootchart_master.fps_name from SuspendedFPS inner join tbl_rootchart_master on tbl_rootchart_master.fps_code=ConnectFPS  where SuspendedFPS.DistrictId='" + distid + "' and Month = '" + ddl_allot_month.SelectedItem.Value + "' and Year = '" + ddd_allot_year.SelectedItem.Text + "' and  SuspendFPS='" + ddl_fpsname.SelectedValue.ToString() + "' or ConnectFPS='" + ddl_fpsname.SelectedValue.ToString()  + "' ";
            SqlCommand cmd_dotype = new SqlCommand(querysuspension, con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd_dotype);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            if (ds1.Tables[0].Rows.Count != 0)
            {
                string fps_codecon = "";
                connectedfpscode = ds1.Tables[0].Rows[0]["ConnectFPS"].ToString();
                connectedfps = ds1.Tables[0].Rows[0]["fps_name"].ToString();
                 suspendendfpscode = ds1.Tables[0].Rows[0]["SuspendFPS"].ToString();
                if (ds1.Tables[0].Rows[0]["ConnectFPS"].ToString() == ddl_fpsname.SelectedValue.ToString())
                {

                    fps_codecon = suspendendfpscode;
                  
                }
                else
                {
                    fps_codecon = connectedfpscode;
                   
                }
                cmd.CommandText = "SELECT DPY_TranportOrder.FPSCode, fps_master.fps_Uname , RiceAllot ,MaizeAllot, WheatAllot , SugarAllot , SaltAllot from DPY_TranportOrder LEFT join opdms.pds.fps_master on fps_master.fps_code =  DPY_TranportOrder.FPSCode  where DPY_TranportOrder.AllotMonth = '" + ddl_allot_month.SelectedItem.Value + "' and DPY_TranportOrder.AllotYear = '" + ddd_allot_year.SelectedItem.Text + "'   and DPY_TranportOrder.DO_Challan='N' and DPY_TranportOrder.DistrictId='" + distid + "'  and DPY_TranportOrder.FPSCode='" + fps_codecon + "' and TransportOrder ='" + ddl_TO_no.SelectedValue.ToString() + "' order by fps_Uname";
                cmd.Connection = con;

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    hd_check_wheat.Value = dr["WheatAllot"].ToString();


                    hd_check_rice.Value = dr["RiceAllot"].ToString();

                    hd_check_salt.Value = dr["SaltAllot"].ToString();

                    hd_check_sugar.Value = dr["SugarAllot"].ToString();
                    hd_check_maize.Value = dr["MaizeAllot"].ToString();


                }
                dr.Close();
                if (connectedfpscode == fps_codecon)
                {
                    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('This is a suspended fps.Connected FPS Name:=" + connectedfps + "');</script>");
                }

            }
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string query = "SELECT DPY_TranportOrder.FPSCode, fps_master.fps_Uname , RiceAllot ,MaizeAllot, WheatAllot , SugarAllot , SaltAllot from DPY_TranportOrder LEFT join opdms.pds.fps_master on fps_master.fps_code =  DPY_TranportOrder.FPSCode  where DPY_TranportOrder.AllotMonth = '" + ddl_allot_month.SelectedItem.Value + "' and DPY_TranportOrder.AllotYear = '" + ddd_allot_year.SelectedItem.Text + "'   and DPY_TranportOrder.RouteNumber='" + ddl_route.SelectedValue.ToString() + "' and DPY_TranportOrder.FeedNumber='" + ddl_feed.SelectedValue.ToString() + "' and DPY_TranportOrder.DO_Challan='N' and DPY_TranportOrder.DistrictId='" + distid + "' and DPY_TranportOrder.FPSCode='" + ddl_fpsname.SelectedValue.ToString() + "' and TransportOrder ='" + ddl_TO_no.SelectedValue.ToString() + "' order by fps_Uname";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            Grdtotal_fps.DataSource = ds.Tables[0];
            Grdtotal_fps.DataBind();
            if (ds == null)
            {
            }
            else
            {
                if (connectedfpscode != "")
                {
                    dr = cmd.ExecuteReader();



                    string wheat = Grdtotal_fps.Rows[0].Cells[1].Text;
                    string rice=Grdtotal_fps.Rows[0].Cells[2].Text;
                    string salt=Grdtotal_fps.Rows[0].Cells[4].Text  ;
                    string maize = Grdtotal_fps.Rows[0].Cells[5].Text;
                    string sugar=Grdtotal_fps.Rows[0].Cells[3].Text;
                    Grdtotal_fps.Rows[0].Cells[1].Text = (Convert.ToDecimal(wheat) + Convert.ToDecimal(hd_check_wheat.Value)).ToString();
                    Grdtotal_fps.Rows[0].Cells[2].Text = (Convert.ToDecimal(rice) + Convert.ToDecimal(hd_check_rice.Value)).ToString();
                    Grdtotal_fps.Rows[0].Cells[3].Text = (Convert.ToDecimal(sugar) + Convert.ToDecimal(hd_check_sugar.Value)).ToString();
                    Grdtotal_fps.Rows[0].Cells[4].Text = (Convert.ToDecimal(salt) + Convert.ToDecimal(hd_check_salt.Value)).ToString();
                    Grdtotal_fps.Rows[0].Cells[5].Text = (Convert.ToDecimal(maize) + Convert.ToDecimal(hd_check_maize.Value)).ToString();

                     wheattotalsus = Grdtotal_fps.Rows[0].Cells[1].Text;
                    ricetotalsus = Grdtotal_fps.Rows[0].Cells[2].Text;
                   salttotalsus = Grdtotal_fps.Rows[0].Cells[4].Text;
                     maizetotalsus = Grdtotal_fps.Rows[0].Cells[5].Text;
                     sugartotalsus = Grdtotal_fps.Rows[0].Cells[3].Text;
                  
                }
                else
                {

                  
                }
                dr.Close();
            }
        }
        catch
        {

        }
       
        
}
    void fillgrid()
    {
        try
        {


            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string query = "SELECT DPY_TranportOrder.FPSCode,fps_master.fps_Uname , RiceAllot ,MaizeAllot, WheatAllot , SugarAllot , SaltAllot from DPY_TranportOrder LEFT join opdms.pds.fps_master on fps_master.fps_code =  DPY_TranportOrder.FPSCode  where DPY_TranportOrder.AllotMonth = '" + ddl_allot_month.SelectedItem.Value + "' and DPY_TranportOrder.AllotYear = '" + ddd_allot_year.SelectedItem.Text + "'   and DPY_TranportOrder.RouteNumber='" + ddl_route.SelectedValue.ToString() + "' and DPY_TranportOrder.FeedNumber='" + ddl_feed.SelectedValue.ToString() + "' and DPY_TranportOrder.DO_Challan='N' and DPY_TranportOrder.DistrictId='" + distid + "'  and DPY_TranportOrder.FPSCode='" + ddl_fpsname.SelectedValue.ToString() + "' and TransportOrder ='" + ddl_TO_no.SelectedValue.ToString() + "' order by fps_Uname";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
         
            da.Fill(ds);
            if (ds == null)
            {
            }
            else
            {

                Gridissue.DataSource = ds.Tables[0];

                Gridissue.DataBind();
            }
            lbl_fpsname.Text = ddl_fpsname.SelectedItem.Text;
            if (suspendendfpscode == ddl_fpsname.SelectedValue.ToString())
            {
                Gridissue.Rows[0].Cells[1].Text = connectedfpscode;
                lbl_name.ForeColor = System.Drawing.Color.Red;
                lbl_fpsname.ForeColor = System.Drawing.Color.Red;
                lbl_name.Text = "Suspendende FPS";
                Panel3.Visible = true;
                lbl_connectedfps.Text = connectedfps + "(" + connectedfpscode + ")";

            }
            else
            {
                Panel3.Visible = false;
                lbl_name.Text = "FPS Name";
                lbl_name.ForeColor = System.Drawing.Color.Blue;
                lbl_fpsname.ForeColor = System.Drawing.Color.Blue;
            }

            for (int R = 0; R < Gridissue.Rows.Count; R++)
            {
                if (Gridissue.HeaderRow.Cells[3].Text == "WheatAlloc")
                {
                    cmd.CommandText = "SELECT sum(Issued_Doorstep_do_fps.issue_qty)as wheat_issue FROM dbo.Issued_Doorstep_do_fps where Transport_order='" + ddl_TO_no.SelectedValue.ToString() + "' and Issued_Doorstep_do_fps.district_code='" + distid + "'   and Issued_Doorstep_do_fps.allotment_month = '" + ddl_allot_month.SelectedValue.ToString() + "' and Issued_Doorstep_do_fps.allotment_year = '" + ddd_allot_year.SelectedValue.ToString() + "' and  Issued_Doorstep_do_fps.commodity in ('22','35')  and Issued_Doorstep_do_fps.fps_code='" + Gridissue.Rows[0].Cells[1].Text + "'";
                    cmd.Connection = con;

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        string issuedqunattity_wheat = Gridissue.Rows[R].Cells[3].Text;


                        if (dr["wheat_issue"].ToString() != "")
                        {
                            string blancequantity = (Convert.ToDecimal(issuedqunattity_wheat) - Convert.ToDecimal(dr["wheat_issue"].ToString())).ToString();
                            Gridissue.Rows[R].Cells[3].Text = blancequantity;
                            if(connectedfps!="")
                            {
                                Gridissue.Rows[R].Cells[3].Text = (Convert.ToDecimal(wheattotalsus) - Convert.ToDecimal(dr["wheat_issue"].ToString())).ToString();
                            }
                        }

                        else
                            if (issuedqunattity_wheat == "&nbsp;" || Convert.ToDecimal(issuedqunattity_wheat) < 0)
                            {
                                Gridissue.Rows[R].Cells[3].Text = "0";
                            }


                    }
                    dr.Close();

                }
                if (Gridissue.Columns[4].HeaderText == "RiceAlloc")
                {
                    cmd.CommandText = "SELECT  sum(Issued_Doorstep_do_fps.issue_qty)as rice_issue FROM dbo.Issued_Doorstep_do_fps where Transport_order='" + ddl_TO_no.SelectedValue.ToString() + "' and Issued_Doorstep_do_fps.district_code='" + distid + "'  and Issued_Doorstep_do_fps.allotment_month = '" + ddl_allot_month.SelectedValue.ToString() + "' and Issued_Doorstep_do_fps.allotment_year = '" + ddd_allot_year.SelectedValue.ToString() + "' and  Issued_Doorstep_do_fps.commodity in ('3','4')  and Issued_Doorstep_do_fps.fps_code='" + Gridissue.Rows[0].Cells[1].Text + "'";
                    cmd.Connection = con;

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        string issuedqunattity_rice = Gridissue.Rows[R].Cells[4].Text;
                        if (dr["rice_issue"].ToString() != "")
                        {
                            string blancequantity = (Convert.ToDecimal(issuedqunattity_rice) - Convert.ToDecimal(dr["rice_issue"].ToString())).ToString();
                            Gridissue.Rows[R].Cells[4].Text = blancequantity;
                            if (connectedfps != "")
                            {
                                Gridissue.Rows[R].Cells[4].Text = (Convert.ToDecimal(ricetotalsus) - Convert.ToDecimal(dr["rice_issue"].ToString())).ToString();
                            }
                        }

                        else
                            if (issuedqunattity_rice == "&nbsp;" || Convert.ToDecimal(issuedqunattity_rice) < 0)
                            {
                                Gridissue.Rows[R].Cells[4].Text = "0";
                            }

                    }
                    dr.Close();

                }
                if (Gridissue.Columns[5].HeaderText == "SugarAlloc")
                {
                    cmd.CommandText = "SELECT  sum(Issued_Doorstep_do_fps.issue_qty)as sugar_issue FROM dbo.Issued_Doorstep_do_fps where Transport_order='" + ddl_TO_no.SelectedValue.ToString() + "' and Issued_Doorstep_do_fps.district_code='" + distid + "'  and Issued_Doorstep_do_fps.allotment_month = '" + ddl_allot_month.SelectedValue.ToString() + "' and Issued_Doorstep_do_fps.allotment_year = '" + ddd_allot_year.SelectedValue.ToString() + "' and  Issued_Doorstep_do_fps.commodity in ('23')  and Issued_Doorstep_do_fps.fps_code='" + Gridissue.Rows[0].Cells[1].Text + "'";
                    cmd.Connection = con;

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        string issuedqunattity_sugar = Gridissue.Rows[R].Cells[5].Text;

                        if (dr["sugar_issue"].ToString() != "")
                        {
                            string blancequantity = (Convert.ToDecimal(issuedqunattity_sugar) - Convert.ToDecimal(dr["sugar_issue"].ToString())).ToString();
                            Gridissue.Rows[R].Cells[5].Text = blancequantity;
                            if (connectedfps != "")
                            {
                                Gridissue.Rows[R].Cells[5].Text = (Convert.ToDecimal(sugartotalsus) - Convert.ToDecimal(dr["sugar_issue"].ToString())).ToString();
                            }
                        }

                        else
                            if (issuedqunattity_sugar == "&nbsp;" || Convert.ToDecimal(issuedqunattity_sugar) < 0)
                            {
                                Gridissue.Rows[R].Cells[5].Text = "0";
                            }


                    }
                    dr.Close();


                }
                if (Gridissue.Columns[6].HeaderText == "SaltAlloc")
                {
                    cmd.CommandText = "SELECT  sum(Issued_Doorstep_do_fps.issue_qty)as salt_issue FROM dbo.Issued_Doorstep_do_fps where Transport_order='" + ddl_TO_no.SelectedValue.ToString() + "' and Issued_Doorstep_do_fps.district_code='" + distid + "'  and Issued_Doorstep_do_fps.allotment_month = '" + ddl_allot_month.SelectedValue.ToString() + "' and Issued_Doorstep_do_fps.allotment_year = '" + ddd_allot_year.SelectedValue.ToString() + "' and  Issued_Doorstep_do_fps.commodity in ('19')  and Issued_Doorstep_do_fps.fps_code='" + Gridissue.Rows[0].Cells[1].Text + "'";
                    cmd.Connection = con;

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        string issuedqunattity_salt = Gridissue.Rows[R].Cells[6].Text;
                        if (dr["salt_issue"].ToString() != "")
                        {
                            string blancequantity = (Convert.ToDecimal(issuedqunattity_salt) - Convert.ToDecimal(dr["salt_issue"].ToString())).ToString();
                            Gridissue.Rows[R].Cells[6].Text = blancequantity;
                            if (connectedfps != "")
                            {
                                Gridissue.Rows[R].Cells[6].Text = (Convert.ToDecimal(salttotalsus) - Convert.ToDecimal(dr["salt_issue"].ToString())).ToString();
                            }
                        }

                        else
                            if (issuedqunattity_salt == "&nbsp;" || Convert.ToDecimal(issuedqunattity_salt) < 0)
                            {
                                Gridissue.Rows[R].Cells[6].Text = "0";
                            }


                    }
                    dr.Close();

                }
                if (Gridissue.Columns[7].HeaderText == "MaizeAlloc")
                {
                    cmd.CommandText = "SELECT sum(Issued_Doorstep_do_fps.issue_qty)as Maize_issue FROM dbo.Issued_Doorstep_do_fps where Transport_order='" + ddl_TO_no.SelectedValue.ToString() + "' and Issued_Doorstep_do_fps.district_code='" + distid + "'  and Issued_Doorstep_do_fps.allotment_month = '" + ddl_allot_month.SelectedValue.ToString() + "' and Issued_Doorstep_do_fps.allotment_year = '" + ddd_allot_year.SelectedValue.ToString() + "' and  Issued_Doorstep_do_fps.commodity in ('12')  and Issued_Doorstep_do_fps.fps_code='" + Gridissue.Rows[0].Cells[1].Text + "'";
                    cmd.Connection = con;

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        string issuedqunattity_maize = Gridissue.Rows[R].Cells[7].Text;
                        if (dr["Maize_issue"].ToString() != "")
                        {
                            string blancequantity = (Convert.ToDecimal(issuedqunattity_maize) - Convert.ToDecimal(dr["maize_issue"].ToString())).ToString();
                            Gridissue.Rows[R].Cells[7].Text = blancequantity;
                            if (connectedfps != "")
                            {
                                Gridissue.Rows[R].Cells[7].Text = (Convert.ToDecimal(maizetotalsus) - Convert.ToDecimal(dr["maize_issue"].ToString())).ToString();
                            }
                        }


                        else
                            if (issuedqunattity_maize == "&nbsp;" || Convert.ToDecimal(issuedqunattity_maize) < 0)
                            {
                                Gridissue.Rows[R].Cells[7].Text = "0";
                            }


                    }
                    dr.Close();

                }
            }
        }
        catch
        {
        }

            
    }
    protected void ddl_feed_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_fpsname.Enabled = true;
        hlinkpdo.Visible = false;
        get_fpsname();

  
    }
    protected void ddl_route_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddl_feed_SelectedIndexChanged(sender, e);
    }
    protected void chkBoxId_CheckedChanged(object sender, EventArgs e)
    {
        lbl_cash.Visible = false;
        dr.Close();
        foreach (GridViewRow row in Gridissue.Rows)
        {
            if (((CheckBox)row.FindControl("chkBoxId")).Checked == true)
            {
                cmd.CommandText = "SELECT *from tbl_rootchart_master where Payment_mode='Cash' and DistrictId='" + distid + "'  and fps_code='" + row.Cells[1].Text +"'";
                cmd.Connection = con;

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Close();
                    cmd.CommandText = "SELECT * FROM DPY_FPS_Payment where fpscode='" + row.Cells[1].Text + "' and DistrictId='" + distid + "'  and allotMonth='" + ddl_allot_month.SelectedValue.ToString() + "' and Allotyear='" + ddd_allot_year.SelectedValue.ToString() + "'";
                    cmd.Connection = con;

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    dr = cmd.ExecuteReader();
                    if(dr.HasRows)
                    {

                    }
                    else
                    {
                        ((CheckBox)row.FindControl("chkBoxId")).Checked = false;
                        ((CheckBox)row.FindControl("chkBoxId")).Enabled = false;
                        lbl_cash.Visible = true;
                        Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please complete payment detail for this fps ...');</script>");
                        ddl_commodity.Enabled = false;
                        ddl_godown.Enabled = false;
                        

                    }

                }
            }


            else
            {
                //lbl_cash.Visible = false;
            }

        }

    }
    protected void ddl_fpsname_SelectedIndexChanged(object sender, EventArgs e)
    {
     
      
        get_comm();
        filltotalfps();
        tx_bags.Text = "0";
        fillgrid();
      
        chkBoxId_CheckedChanged(sender, e);
        Gridissue.Visible = true;
        lbl_dcno.Visible = false;
        ddl_TO_no.Enabled = false;
        Session["dt1"] = null;
        chkBoxId_CheckedChanged(sender, e);
    }
    //protected void ddlsarrival_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //GetGodown();
    //}


    protected void ddltransporter_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetGodown();
    }
    protected void ddlmonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        get_to_no();

    }
   
    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        get_to_no();
    }
}