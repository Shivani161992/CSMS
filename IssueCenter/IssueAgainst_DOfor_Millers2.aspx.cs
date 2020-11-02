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
using System.Data.Sql;
using System.Globalization;

public partial class IssueCenter_IssueAgainst_OpenSales_DO : System.Web.UI.Page
{
    SqlConnection con_CSMS, con_MPStorage;
    SqlCommand cmd_CSMS;
    SqlDataAdapter da_CSMS, da1_CSMS,da_MPStorage;
    DataSet ds_CSMS, ds1_CSMS,ds_MPStorage;

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_opdms = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_opdms"].ToString());
    public SqlConnection cons = new SqlConnection(ConfigurationManager.ConnectionStrings["connstorage"].ToString());

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd_opdms = new SqlCommand();
    SqlDataReader dr;
    SqlDataReader dr_opdms;
    MoveChallan mobj1 = null;
    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    string distid = "";
    string sid = "";
    public string version = "";
    public string transid = "";
    public float total, Balance_IC;
    Decimal D_Proximate_TotalRice,D_Proximate_CommonRice,D_Proximate_GradeARice;

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
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
        }
        btnsave.Enabled = true;
        distid = Session["dist_id"].ToString();
        sid = Session["issue_id"].ToString();
        version = Session["hindi"].ToString();

        ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
        if (!IsPostBack)
        {
            Label1.Visible = false;
            btnPrint.Enabled = false;
            //GetGodown();
            get_comm();
            get_scheme();
            GetSource();
            Getissuetype();
            DistName();
            IssueCentreName();
            GetCropYearValues();
            
            GetBranch();

            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

            tx_issued_date.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");

            if (version == "H")
            {

                lblCommodity.Text = Resources.LocalizedText.lblCommodity;
                lblScheme.Text = Resources.LocalizedText.lblScheme;
                lblbalcomdty.Text = Resources.LocalizedText.lblbalcomdty;
                lblTruckNumber.Text = Resources.LocalizedText.lblTruckNumber;
                //lblNoofBags.Text = Resources.LocalizedText.lblNoofBags;
                lblBagNumber.Text = Resources.LocalizedText.lblBagNumber;
                lbl_issueqty.Text = Resources.LocalizedText.lbl_issueqty;
                lblbalqty.Text = Resources.LocalizedText.lblbalqty;
                lbldispsource.Text = Resources.LocalizedText.lbldispsource;
                lblGodownNo.Text = Resources.LocalizedText.lblGodownNo;
                lblDispatchQty.Text = Resources.LocalizedText.lblDispatchQty;
                lblissuedate.Text = Resources.LocalizedText.lblissuedate;
                lbltoissue.Text = Resources.LocalizedText.lbltoissue;
                lbldono.Text = Resources.LocalizedText.lbldono;
                lbldodate.Text = Resources.LocalizedText.lbldodate;

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

        tx_qty_to_issueGradeA.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");

        tx_bags.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_bags.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_qty_to_issue.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_qty_to_issue.Attributes.Add("onchange", "return chksqltxt(this)");

        tx_qty_to_issueGradeA.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        tx_qty_to_issueGradeA.Attributes.Add("onchange", "return chksqltxt(this)");


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
        //tx_cur_bags.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
    }

    public void IssueCentreName()
    {
        using (con_CSMS = new SqlConnection(strcon))
        {
            try
            {
                con_CSMS.Open();
                string select = string.Format("SELECT DepotName FROM tbl_MetaData_DEPOT where DepotID = '" + sid + "'");
                da_CSMS = new SqlDataAdapter(select, con_CSMS);

                ds_CSMS = new DataSet();
                da_CSMS.Fill(ds_CSMS, "tbl_MetaData_DEPOT");
                if (ds_CSMS.Tables[0].Rows.Count > 0)
                {
                   hdfICName.Value = ds_CSMS.Tables[0].Rows[0]["DepotName"].ToString();
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con_CSMS.State != ConnectionState.Closed)
                {
                    con_CSMS.Close();
                }
            }
        }
    }

    public void DistName()
    {
        using (con_CSMS = new SqlConnection(strcon))
        {
            try
            {
                con_CSMS.Open();
                string select = string.Format("SELECT district_name FROM pds.districtsmp where district_code = '" + Session["dist_id"].ToString() + "'");
                da_CSMS = new SqlDataAdapter(select, con_CSMS);

                ds_CSMS = new DataSet();
                da_CSMS.Fill(ds_CSMS, "pds.districtsmp");
                if (ds_CSMS.Tables[0].Rows.Count > 0)
                {
                    hdfDistName.Value = ds_CSMS.Tables[0].Rows[0]["district_name"].ToString();
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con_CSMS.State != ConnectionState.Closed)
                {
                    con_CSMS.Close();
                }
            }
        }
    }

    public void GetCropYearValues()
    {
        using (con_CSMS = new SqlConnection(strcon))
        {
            try
            {
                con_CSMS.Open();
                string select = string.Format("SELECT CropYear,ArvaChawal,UshnaChawal FROM PaddyMilling_CropYear order by CropYear desc");
                da_CSMS = new SqlDataAdapter(select, con_CSMS);

                ds_CSMS = new DataSet();
                da_CSMS.Fill(ds_CSMS, "PaddyMilling_CropYear");
                if (ds_CSMS.Tables[0].Rows.Count > 0)
                {
                    txtYear.Text = ds_CSMS.Tables[0].Rows[0]["CropYear"].ToString();
                    hdfArvaChawalRs.Value = ds_CSMS.Tables[0].Rows[0]["ArvaChawal"].ToString();
                    hdfUshnaChawalRs.Value = ds_CSMS.Tables[0].Rows[0]["UshnaChawal"].ToString();
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con_CSMS.State != ConnectionState.Closed)
                {
                    con_CSMS.Close();
                }
            }
        }
    }

    protected void get_do_no()
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        string name = string.Empty;
        string id = string.Empty;

        Label1.Text = tx_qty_to_issue.Text = tx_bags.Text = tx_gatepass.Text = tx_balance_qty.Text = tx_do_qty.Text = tx_issue_balqty.Text = tx_issued_qty.Text = tx_cur_bal.Text = txt_DOGradeADhan.Text = tx_balanceGradeADhan.Text = txt_IssuedGradeADhan.Text = txt_IssuedbalanceGradeADhan.Text = tx_do_date.Text = txtValidDate.Text = tx_cur_bal.Text = tx_qty_to_issueGradeA.Text = "";
        string dist = distid;
        ddl_do_no.Items.Clear();

        using (con_CSMS = new SqlConnection(strcon))
        {
            btnPrint.Enabled = false;
            try
            {
                if (ddlissueto.SelectedIndex > 0)
                {
                    con_CSMS.Open();

                    if (txtYear.Text != "")
                    {
                        string select1 = string.Format("select DO_Number from PaddyMilling_DO where District= '" + dist + "' and Issue_Centre = '" + sid + "' and (Rem_Alloted_CommonDhan!=0 or Rem_Alloted_GradeADhan!=0) and CropYear='" + txtYear.Text + "' order by DO_Number");
                        da1_CSMS = new SqlDataAdapter(select1, con_CSMS);
                        ds1_CSMS = new DataSet();
                        da1_CSMS.Fill(ds1_CSMS);
                        da1_CSMS.Fill(dt1);

                        if (ds1_CSMS != null)
                        {
                            if (dt1.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt1.Rows.Count; i++)
                                {
                                    id = dt1.Rows[i]["DO_Number"].ToString();
                                    name = dt1.Rows[i]["DO_Number"].ToString();
                                    ddl_do_no.Items.Add(new ListItem(name, id));
                                }

                            }
                        }
                    }

                    string select = string.Format("select delivery_order_no from dbo.OpenSale_DO where district_code= '" + dist + "' and issue_type = '" + ddlissueto.SelectedValue + "'  order by delivery_order_no");
                    da_CSMS = new SqlDataAdapter(select, con_CSMS);
                    ds_CSMS = new DataSet();
                    da_CSMS.Fill(ds_CSMS);
                    da_CSMS.Fill(dt);

                    if (ds_CSMS != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                id = dt.Rows[i]["delivery_order_no"].ToString();
                                name = dt.Rows[i]["delivery_order_no"].ToString();
                                ddl_do_no.Items.Add(new ListItem(name, id));
                            }

                        }
                    }

                    ddl_do_no.Items.Insert(0, "--Select--");

                    if (dt1.Rows.Count <= 0 && dt.Rows.Count <= 0)
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('डी० ओ० क्रमांक उपलब्ध नहीं है|'); </script> ");
                        ddl_do_no.Items.Clear();
                    }
                }
                else
                {
                    ddlsarrival.Enabled = ddl_godown.Enabled = ddlBranch.Enabled = false;
                    ddlBranch.SelectedIndex = ddlsarrival.SelectedIndex = 0;

                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया नाम चुनें|'); </script> ");
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con_CSMS.State != ConnectionState.Closed)
                {
                    con_CSMS.Close();
                }
            }
        }
    }

    protected void ddl_do_no_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnPrint.Enabled = false;
        
        try
        {
            if (ddl_do_no.SelectedItem.Text == "--Select--")
            {
                Label1.Text = tx_qty_to_issue.Text = tx_bags.Text = tx_gatepass.Text = tx_balance_qty.Text = tx_do_qty.Text = tx_issue_balqty.Text = tx_issued_qty.Text = tx_cur_bal.Text = tx_do_date.Text = txtValidDate.Text = txt_DOGradeADhan.Text = tx_balanceGradeADhan.Text = txtLotNumber.Text = tx_qty_to_issueGradeA.Text = txt_IssuedGradeADhan.Text = txt_IssuedbalanceGradeADhan.Text = tx_cur_bal.Text = "";
                Label1.ForeColor = System.Drawing.Color.Red;
                ddlpartyname.Items.Clear();
                //ddl_commodity.Items.Clear();
                // ddl_scheme.Items.Clear();

                Label1.Text = "Please Select Delivery Order No. ...";
                ddlsarrival.Enabled = ddl_godown.Enabled = ddlBranch.Enabled = false;
                ddlBranch.SelectedIndex = ddlsarrival.SelectedIndex = 0;
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript > alert('Please Select Delivery Order No.');</script>");
            }
            else
            {
                ddlBranch.SelectedIndex =  ddlsarrival.SelectedIndex = 0;
                tx_cur_bal.Text = "";
                get_do_data();
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
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

    protected void GetPartyname(string PartyID)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        string getname = "SELECT Miller_ID,Miller_Name FROM dbo.Miller_Master where Miller_ID='" + PartyID + "'";
        SqlCommand cmdname = new SqlCommand(getname, con);
        SqlDataAdapter daname = new SqlDataAdapter(cmdname);
        DataSet dsname = new DataSet();

        daname.Fill(dsname);

        if (dsname.Tables[0].Rows.Count > 0)
        {
            ddlpartyname.DataSource = dsname.Tables[0];

            ddlpartyname.DataTextField = "Miller_Name";
            ddlpartyname.DataValueField = "Miller_ID";

            ddlpartyname.DataBind();
        }
    }

    protected void get_do_data()
    {
        btnsave.Enabled = tx_qty_to_issue.Enabled = tx_bags.Enabled = tx_gatepass.Enabled = true;
        Label1.ForeColor = System.Drawing.Color.Blue;

        Label1.Text = tx_qty_to_issue.Text = tx_bags.Text = tx_gatepass.Text = tx_qty_to_issueGradeA.Text = "";
        string issueqty = "", issueto_name = "", do_valid = "", do_qty = "", lead_code = "", Partyname = "";

        string do_no = ddl_do_no.SelectedItem.Text;
        string dist = distid;
        string issue_centre_code = sid;

        using (con_CSMS = new SqlConnection(strcon))
        {
            try
            {
                if (ddl_do_no.SelectedIndex > 0)
                {
                    con_CSMS.Open();
                    string select = string.Format("SELECT OpenSale_DO.commodity_id,OpenSale_DO.scheme_id, round(convert(decimal(18,5), OpenSale_DO.quantity),5) as quantity,convert(nvarchar,OpenSale_DO.do_date,103)do_date, OpenSale_DO.issue_type, ISNULL(round(SUM(convert(decimal(18,5),issue_against_OpenSale_do.qty_issue)),5),0) AS issueqty,OpenSale_DO.Partyname FROM dbo.OpenSale_DO LEFT JOIN dbo.issue_against_OpenSale_do ON OpenSale_DO.delivery_order_no = issue_against_OpenSale_do.delivery_order_no AND OpenSale_DO.district_code = issue_against_OpenSale_do.district_code  GROUP BY OpenSale_DO.Partyname, OpenSale_DO.delivery_order_no, OpenSale_DO.district_code, OpenSale_DO.do_date, OpenSale_DO.issue_type,OpenSale_DO.quantity, OpenSale_DO.commodity_id,OpenSale_DO.scheme_id having OpenSale_DO.delivery_order_no= '" + do_no + "'  and OpenSale_DO.district_code= '" + dist + "' ");
                    da_CSMS = new SqlDataAdapter(select, con_CSMS);
                    ds_CSMS = new DataSet();
                    da_CSMS.Fill(ds_CSMS);

                    if (ds_CSMS != null)
                    {
                        if (ds_CSMS.Tables[0].Rows.Count > 0)
                        {
                            string do_date = ds_CSMS.Tables[0].Rows[0]["do_date"].ToString();

                            issueto_name = ds_CSMS.Tables[0].Rows[0]["issue_type"].ToString();

                            do_qty = ds_CSMS.Tables[0].Rows[0]["quantity"].ToString();

                            issueqty = ds_CSMS.Tables[0].Rows[0]["issueqty"].ToString();

                            Partyname = ds_CSMS.Tables[0].Rows[0]["Partyname"].ToString();

                            GetPartyname(Partyname);

                            tx_do_date.Text = do_date.ToString();

                            tx_do_qty.Text = do_qty;

                            tx_issued_qty.Text = issueqty;

                            double DOQty = Convert.ToDouble(do_qty);

                            double IssQty = Convert.ToDouble(issueqty);

                            double remainQty = DOQty - IssQty;

                            tx_balance_qty.Text = Convert.ToString(remainQty);

                            tx_issue_balqty.Text = Convert.ToString(remainQty);

                            ddl_commodity.SelectedValue = ds_CSMS.Tables[0].Rows[0]["commodity_id"].ToString();
                            ddl_scheme.SelectedValue = ds_CSMS.Tables[0].Rows[0]["scheme_id"].ToString();

                            ViewState["Code"] = "OldCode";
                            txt_DOGradeADhan.Text = tx_balanceGradeADhan.Text = txt_IssuedGradeADhan.Text = txt_IssuedbalanceGradeADhan.Text = "0";
                            ddlsarrival.Enabled = ddl_godown.Enabled = ddlBranch.Enabled = true;
                        }
                    }


                    string select1 = string.Format("Select (case when mm.Miller_Name IS NOT NULL then mm.Miller_Name when mr.Mill_Name IS NOT NULL then mr.Mill_Name else '' end) as  MillName,PMDO.Mill_Name,PMDO.Agreement_ID,PMDO.LotNumber,PMDO.From_Date,PMDO.To_Date,PMDO.Alloted_CommonDhan,PMDO.Alloted_GradeADhan,PMDO.Rem_Alloted_CommonDhan,PMDO.Rem_Alloted_GradeADhan,PMDO.Milling_Type from PaddyMilling_DO PMDO left join Miller_Master mm on mm.Miller_ID=PMDO.Mill_Name left join Miller_Registration mr on mr.Registration_ID=PMDO.Mill_Name where PMDO.DO_Number='" + ddl_do_no.SelectedValue.ToString() + "' and PMDO.CropYear='" + txtYear.Text + "' ");
                    da1_CSMS = new SqlDataAdapter(select1, con_CSMS);
                    ds1_CSMS = new DataSet();
                    da1_CSMS.Fill(ds1_CSMS);

                    if (ds1_CSMS != null)
                    {
                        if (ds1_CSMS.Tables[0].Rows.Count > 0)
                        {
                            txtLotNumber.Text = ds1_CSMS.Tables[0].Rows[0]["LotNumber"].ToString();

                            ddlpartyname.DataSource = ds1_CSMS.Tables[0];
                            ddlpartyname.DataTextField = "MillName";
                            ddlpartyname.DataValueField = "Mill_Name";
                            ddlpartyname.DataBind();

                            DateTime frmDate = DateTime.Parse(ds1_CSMS.Tables[0].Rows[0]["From_Date"].ToString());
                            tx_do_date.Text = frmDate.ToString("dd/MM/yyyy");

                            DateTime todate = DateTime.Parse(ds1_CSMS.Tables[0].Rows[0]["To_Date"].ToString());
                            txtValidDate.Text = todate.ToString("dd/MM/yyyy");

                            tx_do_qty.Text = ds1_CSMS.Tables[0].Rows[0]["Alloted_CommonDhan"].ToString();
                            txt_DOGradeADhan.Text = ds1_CSMS.Tables[0].Rows[0]["Alloted_GradeADhan"].ToString();

                            tx_balance_qty.Text = ds1_CSMS.Tables[0].Rows[0]["Rem_Alloted_CommonDhan"].ToString();
                            tx_balanceGradeADhan.Text = ds1_CSMS.Tables[0].Rows[0]["Rem_Alloted_GradeADhan"].ToString();

                            tx_issue_balqty.Text = ds1_CSMS.Tables[0].Rows[0]["Rem_Alloted_CommonDhan"].ToString();
                            txt_IssuedbalanceGradeADhan.Text = ds1_CSMS.Tables[0].Rows[0]["Rem_Alloted_GradeADhan"].ToString();

                            tx_issued_qty.Text = (float.Parse(tx_do_qty.Text) - float.Parse(tx_balance_qty.Text)).ToString();
                            txt_IssuedGradeADhan.Text = (float.Parse(txt_DOGradeADhan.Text) - float.Parse(tx_balanceGradeADhan.Text)).ToString();

                            hdfMillingType.Value = ds1_CSMS.Tables[0].Rows[0]["Milling_Type"].ToString();
                            hdfAgrmtID.Value= ds1_CSMS.Tables[0].Rows[0]["Agreement_ID"].ToString();

                            ddl_commodity.SelectedItem.Text = "Paddy-Common";
                            ddl_commodity.SelectedValue = "13";
                            ddl_scheme.SelectedItem.Text = "Non Scheme";
                            ddl_scheme.SelectedValue = "0";
                            ViewState["Code"] = "NewCode";
                            ddlsarrival.Enabled = ddl_godown.Enabled = ddlBranch.Enabled = true;
                        }
                    }

                    if (tx_balance_qty.Text == "0" || tx_balance_qty.Text == "")
                    {
                        tx_qty_to_issue.Enabled = false;
                        tx_qty_to_issue.Text = "0";
                    }
                    else
                    {
                        tx_qty_to_issue.Enabled = true;
                    }

                    if (tx_balanceGradeADhan.Text == "0" || tx_balanceGradeADhan.Text == "")
                    {
                        tx_qty_to_issueGradeA.Enabled = false;
                        tx_qty_to_issueGradeA.Text = "0";
                    }
                    else
                    {
                        tx_qty_to_issueGradeA.Enabled = true;
                    }

                    if (tx_balance_qty.Text == "0" && tx_balanceGradeADhan.Text == "0" || tx_balance_qty.Text == "" && tx_balanceGradeADhan.Text == "")
                    {
                        tx_qty_to_issue.Enabled = tx_qty_to_issueGradeA.Enabled = false;
                        tx_qty_to_issue.Text = tx_qty_to_issueGradeA.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con_CSMS.State != ConnectionState.Closed)
                {
                    con_CSMS.Close();
                }
            }
        }
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
        ComObj.CloseConnection();
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }

    protected void GetGodown()
    {
        ddl_godown.Items.Clear();

        // mobj = new MoveChallan(ComObj);
        string qry = "  select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where DistrictId='23" + distid + "' and DepotID='" + sid + "'";

        SqlCommand cmdgdn = new SqlCommand(qry, cons);

        if (cons.State == ConnectionState.Closed)
        {
            cons.Open();
        }

        SqlDataAdapter dagdn = new SqlDataAdapter(cmdgdn);

        DataSet dsgdn = new DataSet();

        dagdn.Fill(dsgdn);

        if (dsgdn.Tables[0].Rows.Count > 0)
        {
            ddl_godown.DataSource = dsgdn.Tables[0];
            ddl_godown.DataTextField = "Godown_Name";
            ddl_godown.DataValueField = "Godown_ID";
            ddl_godown.DataBind();
            ddl_godown.Items.Insert(0, "Select");
        }

        else
        {
            ddl_godown.DataSource = "";
            ddl_godown.DataBind();
        }

        if (cons.State == ConnectionState.Open)
        {
            cons.Close();
        }

    }



    protected void Getissuetype()
    {
        string getname = "SELECT SaleId ,SaleType FROM DO_SaleType where saleId  in (1)";
        SqlCommand cmdname = new SqlCommand(getname, con);
        SqlDataAdapter daname = new SqlDataAdapter(cmdname);
        DataSet dsname = new DataSet();

        daname.Fill(dsname);

        if (dsname.Tables[0].Rows.Count > 0)
        {
            ddlissueto.DataSource = dsname.Tables[0];


            ddlissueto.DataTextField = "SaleType";
            ddlissueto.DataValueField = "SaleId";

            ddlissueto.DataBind();

            ddlissueto.Items.Insert(0, "--Select--");

        }
        else
        {

        }


    }

    void GetBalQty()
    {
        try
        {
            if (ddlissueto.SelectedValue == "1" || ddlissueto.SelectedValue == "3")  // Only for Open sale of Sound Commodity and Paddy to Miller
            {
                string pqry = "stock_depotwise_IncludeDamageqty";
                SqlCommand cmdpqty = new SqlCommand(pqry, con);
                cmdpqty.CommandType = CommandType.StoredProcedure;

                string Commodity_Id = ddl_commodity.SelectedValue;
                Int16 comid = Convert.ToInt16(Commodity_Id);

                cmdpqty.Parameters.Add("@Commodity_Id", SqlDbType.Int).Value = comid;
                cmdpqty.Parameters.Add("@district_code", SqlDbType.NVarChar).Value = distid;

                cmdpqty.Parameters.Add("@Depotid", SqlDbType.Int).Value = sid;

                cmdpqty.Parameters.Add("@GodownId", SqlDbType.NVarChar).Value = ddl_godown.SelectedValue;

                cmdpqty.Parameters.Add("@issueto", SqlDbType.Int).Value = ddlissueto.SelectedValue;

                DataSet ds = new DataSet();
                SqlDataAdapter dr = new SqlDataAdapter(cmdpqty);

                dr.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    double stock = Math.Round(Convert.ToDouble(ds.Tables[0].Rows[0]["Sound"].ToString()), 2);

                    tx_cur_bal.Text = Convert.ToString(stock);
                }
            }

            else
                if (ddlissueto.SelectedValue == "2")  // Only for Damage Commodtity and Miller 
                {
                    string Getqty = "Select isnull(sum(Damage_Quantity),0)as Damage_Quantity from Tbl_Damage_Sweepage where Dist_Id = '" + distid + "' and Depot_ID = '" + sid + "' and Godown = '" + ddl_godown.SelectedValue + "' ";

                    SqlCommand cmdqty = new SqlCommand(Getqty, con);
                    SqlDataAdapter dadam = new SqlDataAdapter(cmdqty);
                    DataSet dsqty = new DataSet();
                    dadam.Fill(dsqty);

                    string Availbleqty = dsqty.Tables[0].Rows[0]["Damage_Quantity"].ToString();

                    string GetIssueqty = "Select isnull(sum(qty_issue),0)as qty_issue from issue_against_OpenSale_do where district_code = '" + distid + "' and issueCentre_code = '" + sid + "' and Godown = '" + ddl_godown.SelectedValue + "' and delivery_order_no = '" + ddl_do_no.SelectedItem.Text + "' ";

                    SqlCommand cmdIssueqty = new SqlCommand(GetIssueqty, con);
                    SqlDataAdapter daIssue = new SqlDataAdapter(cmdIssueqty);
                    DataSet dsissue = new DataSet();
                    daIssue.Fill(dsissue);

                    string Issueqty = dsissue.Tables[0].Rows[0]["qty_issue"].ToString();

                    double DeclareQty = Convert.ToDouble(Availbleqty.ToString());

                    double IssuedQty = Convert.ToDouble(Issueqty.ToString());


                    double Differqty = DeclareQty - IssuedQty;

                    tx_cur_bal.Text = Convert.ToString(Differqty);

                }

                else   // Only for Sweepage Commodtity
                {


                    //string GetIssueqty = "Select isnull(sum(qty_issue),0)as qty_issue from issue_against_OpenSale_do where district_code = '" + distid + "' and issueCentre_code = '" + sid + "' and Godown = '" + ddl_godown.SelectedValue + "' and delivery_order_no = '" + ddl_do_no.SelectedItem.Text + "' ";

                    //SqlCommand cmdIssueqty = new SqlCommand(GetIssueqty, con);
                    //SqlDataAdapter daIssue = new SqlDataAdapter(cmdIssueqty);
                    //DataSet dsissue = new DataSet();
                    //daIssue.Fill(dsissue);

                    //string Issueqty = dsissue.Tables[0].Rows[0]["qty_issue"].ToString();

                    //double DeclareQty = Convert.ToDouble(Availbleqty.ToString());

                    //double IssuedQty = Convert.ToDouble(Issueqty.ToString());

                    //double Differqty = DeclareQty - IssuedQty;

                    //tx_cur_bal.Text = Convert.ToString(Differqty);
                }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
        }

    }

    protected void ddl_godown_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnPrint.Enabled = false;

        if (ddl_do_no.SelectedItem.Text == "--Select--" || ddl_do_no.SelectedItem.Text == "")
        {
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Text = "Please Select Delivery Order No. ...";
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Delivery Order No.');</script>");
            ddl_godown.SelectedIndex = 0;
        }
        else if (ddlsarrival.SelectedItem.Text == "--Select--")
        {
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Text = "Please Select Stock Issued From ...";
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Stock Issued From ...');</script>");
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
            //GetBalQty();
            GetCapGodown();
        }
    }
    void GetCapGodown()
    {
        //DateTime do_date = new DateTime();
        //tx_do_date.Text = frmDate.ToString("dd/MM/yyyy");

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
            //tx_cur_bags.Text = Convert.ToString(bags);

            //txtcurntcap.Text = Convert.ToString(stock);
            //txtavalcap.Text = (System.Math.Round(CheckNull(txtmaxcap.Text) - CheckNull(txtcurntcap.Text), 5)).ToString();

        }

    }
    protected void get_comm()
    {
        try
        {
            cmd.CommandText = "select commodity_id,commodity_name from dbo.tbl_MetaData_STORAGE_COMMODITY where status='Y'";
            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListItem lstitem = new ListItem();
                lstitem.Text = dr["commodity_name"].ToString();
                lstitem.Value = dr["commodity_id"].ToString();
                ddl_commodity.Items.Add(lstitem);
            }
            ListItem lstitem1 = new ListItem();
            lstitem1.Text = " ";
            lstitem1.Value = "N";
            ddl_commodity.Items.Insert(0, lstitem1);

            dr.Close();
            con.Close();
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
        }

    }

    protected void get_scheme()
    {
        cmd.CommandText = "select * from dbo.tbl_MetaData_SCHEME   where status='Y' order by Scheme_Id";
        cmd.Connection = con;
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ListItem lstitem = new ListItem();
            lstitem.Text = dr["scheme_name"].ToString();
            lstitem.Value = dr["scheme_id"].ToString();
            ddl_scheme.Items.Add(lstitem);
        }
        ddl_scheme.Items.Insert(0, " ");


        dr.Close();
        con.Close();
    }

    void GetSource()
    {
        try
        {
            mobj = new MoveChallan(ComObj);
            string qry = "SELECT * FROM dbo.Source_Arrival_Type where Source_ID <> '08' order by Source_ID";
            DataSet ds = mobj.selectAny(qry);

            ddlsarrival.DataSource = ds.Tables[0];
            ddlsarrival.DataTextField = "Source_Name";
            ddlsarrival.DataValueField = "Source_ID";
            ddlsarrival.DataBind();
            ddlsarrival.Items.Insert(0, "--Select--");
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
        }
    }

    protected void ddlsarrival_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btn_new_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (Session["update"].ToString() == ViewState["update"].ToString())
        {
            string opid = Session["OperatorId"].ToString();
            //string state = Session["State_Id"].ToString();
            Label1.Text = "";
            Label1.ForeColor = System.Drawing.Color.Blue;
            if (ddl_do_no.Items.Count == 0 || ddl_do_no.SelectedItem.Text == "--Select--")
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Delivery Order No.');</script>");
                Label1.Text = "Please Select Delivery Order No. ...";
            }
            else if (ddl_godown.Items.Count ==0 || ddl_godown.SelectedItem.Text == "--Select--")
            {
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Godown ...');</script>");
                Label1.Text = "Please Select Godown ...";
            }

            else
            {
                if (tx_issued_date.Text == "")
                {
                    tx_issued_date.Text = DateTime.Today.Date.ToString();
                }
                else
                {

                }

                string ddate = getDate_MDY(tx_do_date.Text);

                DateTime dodate = Convert.ToDateTime(ddate);

                //  DateTime dodate = Convert.ToDateTime(tx_do_date.Text);


                DateTime issuedate = Convert.ToDateTime(DateTime.ParseExact(tx_issued_date.Text.Trim(), "dd/MM/yyyy", null).ToString("MM/dd/yyyy"));


                int result = DateTime.Compare(issuedate, dodate);

                if (result == -1)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Issue Date will not Less than DO Date...'); </script> ");
                    return;
                }

                else
                {
                    string dist = distid;
                    string issue_centre_code = sid;
                    string issue_date = getDate_MDY(tx_issued_date.Text);

                    string do_no = ddl_do_no.SelectedItem.Text;
                    string gate_pass = tx_gatepass.Text;
                    decimal do_qty = CheckNull(tx_balance_qty.Text);
                    decimal issue_qty = CheckNull(tx_qty_to_issue.Text);
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    string notrans = "N";

                    if (ddlpartyname.SelectedValue == "0" || ddlpartyname.SelectedItem.Text == "--Select--" || ddlpartyname.SelectedValue == "")
                    {
                        Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Cannot Submitted because Party Name is Not Valid, pls Contact Dist office for this Deliver Order');</script>");
                        return;
                    }
                    string partyname = ddlpartyname.SelectedValue;

                    if (tx_qty_to_issue.Text != "" && tx_qty_to_issueGradeA.Text != "")
                    {
                        if ((issue_qty > do_qty || issue_qty < 0) || (float.Parse(tx_qty_to_issueGradeA.Text) > float.Parse(tx_balanceGradeADhan.Text)))
                        {
                            Label1.ForeColor = System.Drawing.Color.Red;
                            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Quantity To Issue can not be greater  than DO Quantity');</script>");
                            Label1.Text = "Error: Quantity To Issue can not be greater than DO Quantity";
                            tx_qty_to_issue.Text = "";
                            tx_qty_to_issue.Focus();
                            return;
                        }

                        total = ((float.Parse(tx_qty_to_issueGradeA.Text)) + (float.Parse(tx_qty_to_issue.Text)));
                        Balance_IC = float.Parse(tx_cur_bal.Text);
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Enter Quantity To Issue');</script>");
                        return;
                    }

                    //if (total > Balance_IC )
                    //{
                    //    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('कृपया संग्रहण केंद्र की क्षमता के अनुसार ही धान का आबंटन करें|');</script>");
                    //}
                    //else
                    //{
                        if (ViewState["Code"].ToString() != "NewCode")
                        {
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }
                            int trnscnt = 0;
                            string docount = "";
                            string strqr = "select count(delivery_order_no) as rwcount  from dbo.issue_against_OpenSale_do where delivery_order_no='" + do_no + "' and district_code='" + dist + "'";
                            cmd.CommandText = strqr;
                            cmd.Connection = con;
                            dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                docount = dr["rwcount"].ToString();
                            }
                            dr.Close();
                            strqr = "select trans_id from dbo.issue_against_OpenSale_do where delivery_order_no='" + do_no + "' and district_code='" + dist + "'";
                            cmd.CommandText = strqr;
                            cmd.Connection = con;
                            dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                transid = dr["trans_id"].ToString();
                            }
                            dr.Close();
                            int indx1 = 0;
                            int indx2 = 0;
                            if (transid != "")
                            {
                                indx1 = transid.IndexOf("-");
                                indx2 = transid.LastIndexOf("-");
                            }
                            int indx = indx2 - indx1;
                            if (CheckNullInt(docount) > 0 && indx <= 0)
                            {
                                trnscnt = CheckNullInt(docount) + 1;
                                transid = dist.ToString() + do_no.ToString() + (trnscnt).ToString();
                            }
                            else
                            {
                                strqr = "select count(delivery_order_no) as rwcount  from dbo.issue_against_OpenSale_do where delivery_order_no='" + do_no + "' and district_code='" + dist + "'";
                                cmd.CommandText = strqr;
                                cmd.Connection = con;
                                dr = cmd.ExecuteReader();
                                while (dr.Read())
                                {
                                    docount = dr["rwcount"].ToString();
                                }
                                dr.Close();
                                if (CheckNullInt(docount) < 0)
                                {
                                    docount = "0";
                                }
                                if (docount != "")
                                {
                                    trnscnt = CheckNullInt(docount);
                                }
                                trnscnt++;
                                transid = dist.ToString() + "-" + do_no.ToString() + "-" + (trnscnt).ToString();
                            }
                            decimal lift_qty = 0;
                            string str2 = "select round(convert(decimal(18,5),lift_qty),5) as lift_qty from dbo.sum_trans_do where district_code='" + dist + "' and issueCentre_code='" + issue_centre_code + "' ";
                            cmd.Connection = con;
                            cmd.CommandText = str2;
                            dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                lift_qty = CheckNull(dr["lift_qty"].ToString());
                            }
                            dr.Close();

                            SqlTransaction Trans_csms;
                            Trans_csms = con.BeginTransaction();

                            cmd.Connection = con;

                            cmd.Transaction = Trans_csms;

                            try
                            {
                                string str1 = "INSERT INTO dbo.issue_against_OpenSale_do(trans_id,delivery_order_no,district_code,issueCentre_code,issue_to,qty_issue,bags,Source,Godown,issue_date,gate_pass,created_date,updated_date,swc_status,ip_add,OperatorID,NoTransaction,Partyname,Branch) VALUES('" + transid + "','" + do_no + "','" + dist + "','" + issue_centre_code + "','" + ddlissueto.SelectedValue + "'," + tx_qty_to_issue.Text + "," + tx_bags.Text + ",'" + ddlsarrival.SelectedItem.Value + "','" + ddl_godown.SelectedItem.Value + "','" + issue_date + "','" + gate_pass + "',getdate(),'','N','" + ip + "','" + opid + "','" + notrans + "','" + partyname + "','"+ddlBranch.SelectedValue.ToString()+"')";
                                cmd.CommandText = str1;
                                cmd.ExecuteNonQuery();

                                lift_qty = lift_qty + CheckNull(tx_qty_to_issue.Text);
                                str1 = "update dbo.sum_trans_do set lift_qty=round(" + lift_qty + ",5) where district_code='" + dist + "' and issueCentre_code='" + issue_centre_code + "'";
                                cmd.CommandText = str1;
                                cmd.ExecuteNonQuery();
                                str1 = "update dbo.Current_Godown_Position set Current_Bags=Current_Bags-" + CheckNullInt(tx_bags.Text) + ",Current_Balance=round(convert(decimal(18,5),Current_Balance)-" + CheckNull(tx_qty_to_issue.Text) + ",5),Current_Capacity=round(convert(decimal(18,5),Current_Capacity)+" + CheckNull(tx_qty_to_issue.Text) + ",5) where District_Id='" + dist + "' and Depotid='" + sid + "' and Godown='" + ddl_godown.SelectedItem.Value + "'";
                                cmd.CommandText = str1;
                                cmd.ExecuteNonQuery();

                                {
                                    int rcount = 0;
                                    string dist1 = distid;
                                    string issue_centre_code1 = sid;
                                    string ip1 = Request.ServerVariables["REMOTE_ADDR"].ToString();


                                    string tempos = "NNN";
                                    string strope = "";
                                    strope = "select *  from dbo.tbl_Stock_Registor  where Commodity_Id ='" + ddl_commodity.SelectedItem.Value + "' and Scheme_Id ='" + ddl_scheme.SelectedItem.Value + "' and DistrictId='" + dist1 + "'and DepotID='" + issue_centre_code1 + "'and Month=" + DateTime.Today.Month + "and Year=" + DateTime.Today.Year;
                                    cmd.CommandText = strope;
                                    dr = cmd.ExecuteReader();
                                    while (dr.Read())
                                    {
                                        tempos = "YYY";
                                    }
                                    dr.Close();
                                    if (tempos == "YYY")
                                    {
                                        strope = "update dbo.tbl_Stock_Registor set Sale_Do=round(convert(decimal(18,5),Sale_Do)+" + CheckNull(tx_qty_to_issue.Text) + ",5) where Commodity_Id ='" + ddl_commodity.SelectedItem.Value + "' and Scheme_Id ='" + ddl_scheme.SelectedItem.Value + "' and DistrictId='" + dist1 + "'and DepotID='" + issue_centre_code1 + "'and Month=" + DateTime.Today.Month + "and Year=" + DateTime.Today.Year;
                                        cmd.CommandText = strope;
                                        cmd.ExecuteNonQuery();
                                    }
                                    else
                                    {
                                        decimal opebal = 0;
                                        strope = "select sum(convert(decimal(18,5),Current_Balance)) as opebal  from dbo.issue_opening_balance  where Commodity_Id ='" + ddl_commodity.SelectedItem.Value + "' and Scheme_Id ='" + ddl_scheme.SelectedItem.Value + "' and District_Id='" + dist1 + "'and Depotid='" + issue_centre_code1 + "'";
                                        cmd.CommandText = strope;
                                        dr = cmd.ExecuteReader();
                                        while (dr.Read())
                                        {
                                            opebal = CheckNull(dr["opebal"].ToString());
                                        }
                                        dr.Close();
                                        strope = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Scheme_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Received_OtherSch,Recieved_Other_Src,Received_RailHead,Received_CMR,Received_Levy,Sale_Do,Sale_otherg,Transfer_OtherSch,Month,Year,Remarks) Values('" + distid + "','" + sid + "','" + ddl_commodity.SelectedItem.Value + "','" + ddl_scheme.SelectedItem.Value + "'," + opebal + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + tx_qty_to_issue.Text + "," + 0 + "," + 0 + "," + 0 + "," + DateTime.Today.Month + "," + DateTime.Today.Year + ",'')";
                                        cmd.CommandText = strope;
                                        cmd.ExecuteNonQuery();
                                    }

                                    tempos = "NNN";
                                    strope = "select *  from dbo.issue_opening_balance  where District_Id='" + dist1 + "'and Depotid='" + issue_centre_code1 + "'and Commodity_Id='" + ddl_commodity.SelectedItem.Value + "'and Scheme_Id='" + ddl_scheme.SelectedItem.Value + "' and Godown='" + ddl_godown.SelectedItem.Value + "' and Source='" + ddlsarrival.SelectedItem.Value + "'";
                                    cmd.CommandText = strope;
                                    dr = cmd.ExecuteReader();
                                    while (dr.Read())
                                    {
                                        tempos = "YYY";
                                    }
                                    dr.Close();
                                    if (tempos == "YYY")
                                    {
                                        strope = "Update dbo.issue_opening_balance set Current_Balance=round(convert(decimal(18,5),Current_Balance)-" + CheckNull(tx_qty_to_issue.Text) + ",5),Current_Bags=Current_Bags-" + CheckNullInt(tx_bags.Text) + " where District_Id='" + dist1 + "'and Depotid='" + issue_centre_code1 + "'and Commodity_Id='" + ddl_commodity.SelectedItem.Value + "'and Scheme_Id='" + ddl_scheme.SelectedItem.Value + "' and Godown='" + ddl_godown.SelectedItem.Value + "' and Source='" + ddlsarrival.SelectedItem.Value + "'";
                                        cmd.CommandText = strope;
                                        cmd.ExecuteNonQuery();
                                    }
                                    else
                                    {
                                        strope = "insert into dbo.issue_opening_balance(State_Id,District_Id,Depotid,Commodity_Id,Scheme_Id,Category_Id,Godown,Crop_year,Bags,Quantity,Source,Current_Balance,Current_Bags,Month,Year,IP_Address,Stock_Date,CreatedDate,UpdatedDate,DeletedDate,Branch) values('23','" + distid + "','" + sid + "','" + ddl_commodity.SelectedItem.Value + "','" + ddl_scheme.SelectedItem.Value + "','','" + ddl_godown.SelectedItem.Value + "','',0,0,'" + ddlsarrival.SelectedItem.Value + "'," + -CheckNull(tx_qty_to_issue.Text) + "," + -CheckNullInt(tx_bags.Text) + "," + DateTime.Today.Month + "," + DateTime.Today.Year + ",'" + ip1 + "',getdate(),getdate(),'','','"+ ddlBranch.SelectedValue.ToString() +"')";
                                        cmd.CommandText = strope;
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Data Saved Successfully ...');</script>");
                                Label1.Text = "Data Saved Successfully ...";
                                ddl_do_no.SelectedIndex = ddlBranch.SelectedIndex = ddlsarrival.SelectedIndex = 0;
                                tx_cur_bal.Text = "";
                                btnsave.Enabled = ddlsarrival.Enabled = ddl_godown.Enabled = ddlBranch.Enabled = false;
                                Trans_csms.Commit();
                                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                            }
                            catch (Exception ex)
                            {
                                Trans_csms.Rollback();
                                dr.Close();
                                dr_opdms.Close();
                                

                                Label1.Text = ex.Message;
                            }
                            finally
                            {
                                if (con.State == ConnectionState.Open)
                                {
                                    con.Close();
                                }
                            }
                        }
                        else if (ViewState["Code"].ToString() == "NewCode")
                        {
                            string DOEndDate = getDate_MDY(txtValidDate.Text);

                            DateTime DOValidDate = Convert.ToDateTime(DOEndDate);
                            int result1 = DateTime.Compare(DOValidDate, issuedate);

                            if (result1 == -1)
                            {
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Issue Date Will Not Greater Than DO Validity Date...'); </script> ");
                                return;
                            }
                            else
                            {
                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                ClientIP objClientIP = new ClientIP();
                                string GetIp = (objClientIP.GETIP() + "  " + objClientIP.GETHOST());

                                string browser = Request.Browser.Browser.ToString();
                                string version = Request.Browser.Version.ToString();
                                string useragent = browser + version;

                                int trnscnt = 0;
                                string docount = "";

                                string strqr = "select count(delivery_order_no) as rwcount  from PaddyMilling_IssueAgainst_DO where delivery_order_no='" + do_no + "' and district_code='" + dist + "' and CropYear='" + txtYear.Text + "'";
                                cmd.CommandText = strqr;
                                cmd.Connection = con;
                                dr = cmd.ExecuteReader();
                                while (dr.Read())
                                {
                                    docount = dr["rwcount"].ToString();
                                }
                                dr.Close();
                                strqr = "select trans_id from PaddyMilling_IssueAgainst_DO where delivery_order_no='" + do_no + "' and district_code='" + dist + "' and CropYear='" + txtYear.Text + "'";
                                cmd.CommandText = strqr;
                                cmd.Connection = con;
                                dr = cmd.ExecuteReader();
                                while (dr.Read())
                                {
                                    transid = dr["trans_id"].ToString();
                                }
                                dr.Close();

                                int indx2 = 0;
                                if (transid != "")
                                {
                                    indx2 = transid.LastIndexOf("-");
                                }
                                int indx = indx2;
                                if (CheckNullInt(docount) > 0 && indx <= 0)
                                {
                                    trnscnt = CheckNullInt(docount) + 1;
                                    transid = do_no.ToString() + (trnscnt).ToString();
                                }
                                else
                                {
                                    strqr = "select count(delivery_order_no) as rwcount  from PaddyMilling_IssueAgainst_DO where delivery_order_no='" + do_no + "' and district_code='" + dist + "' and CropYear='" + txtYear.Text + "'";
                                    cmd.CommandText = strqr;
                                    cmd.Connection = con;
                                    dr = cmd.ExecuteReader();
                                    while (dr.Read())
                                    {
                                        docount = dr["rwcount"].ToString();
                                    }
                                    dr.Close();
                                    if (CheckNullInt(docount) < 0)
                                    {
                                        docount = "0";
                                    }
                                    if (docount != "")
                                    {
                                        trnscnt = CheckNullInt(docount);
                                    }
                                    trnscnt++;
                                    transid = do_no.ToString() + "-" + (trnscnt).ToString();
                                }

                                decimal lift_qty = 0;
                                string str2 = "select round(convert(decimal(18,5),lift_qty),5) as lift_qty from dbo.sum_trans_do where district_code='" + dist + "' and issueCentre_code='" + issue_centre_code + "' ";
                                cmd.Connection = con;
                                cmd.CommandText = str2;
                                dr = cmd.ExecuteReader();
                                while (dr.Read())
                                {
                                    lift_qty = CheckNull(dr["lift_qty"].ToString());
                                }
                                dr.Close();

                                SqlTransaction Trans_csms;
                                Trans_csms = con.BeginTransaction();

                                cmd.Connection = con;

                                cmd.Transaction = Trans_csms;

                                try
                                {
                                    D_Proximate_TotalRice = (decimal.Parse(tx_qty_to_issue.Text) + decimal.Parse(tx_qty_to_issueGradeA.Text));
                                    D_Proximate_CommonRice = (decimal.Parse(tx_qty_to_issue.Text));
                                    D_Proximate_GradeARice = (decimal.Parse(tx_qty_to_issueGradeA.Text));

                                    if (hdfMillingType.Value == "अरवा")
                                    {
                                        D_Proximate_CommonRice = (((D_Proximate_CommonRice) * decimal.Parse(hdfArvaChawalRs.Value)) / 100);
                                        D_Proximate_GradeARice = (((D_Proximate_GradeARice) * decimal.Parse(hdfArvaChawalRs.Value)) / 100);
                                        D_Proximate_TotalRice = (((D_Proximate_TotalRice) * decimal.Parse(hdfArvaChawalRs.Value)) / 100);
                                    }
                                    else if (hdfMillingType.Value == "उसना")
                                    {
                                        D_Proximate_CommonRice = (((D_Proximate_CommonRice) * decimal.Parse(hdfUshnaChawalRs.Value)) / 100);
                                        D_Proximate_GradeARice = (((D_Proximate_GradeARice) * decimal.Parse(hdfUshnaChawalRs.Value)) / 100);
                                        D_Proximate_TotalRice = (((D_Proximate_TotalRice) * decimal.Parse(hdfUshnaChawalRs.Value)) / 100);
                                    }

                                    hdfChallanNo.Value = transid;
                                    hdfDONumber.Value = do_no;
                                    hdfGodown.Value = ddl_godown.SelectedItem.ToString();
                                    string str1 = "INSERT INTO PaddyMilling_IssueAgainst_DO(trans_id,delivery_order_no,district_code,issueCentre_code,issue_to,bags,Source,Godown,issue_date,gate_pass,created_date,updated_date,swc_status,ip_add,OperatorID,NoTransaction,Partyname,User_Agent,CropYear,LotNumber,Milling_Type,Issued_CommonDhan,Issued_GradeADhan,qty_issue, Rem_CommonDhan,Rem_GradeADhan,Agreement_ID,Proximate_CommonRice,Proximate_GradeARice,Proximate_TotalRice,Branch) VALUES('" + transid + "','" + do_no + "','" + dist + "','" + issue_centre_code + "','" + ddlissueto.SelectedValue + "'," + tx_bags.Text + ",'" + ddlsarrival.SelectedItem.Value + "','" + ddl_godown.SelectedItem.Value + "','" + issue_date + "','" + gate_pass + "',getdate(),'','N','" + GetIp + "','" + opid + "','" + notrans + "','" + partyname + "','" + useragent + "','" + txtYear.Text + "','" + txtLotNumber.Text + "',N'" + hdfMillingType.Value.ToString() + "','" + tx_qty_to_issue.Text + "','" + tx_qty_to_issueGradeA.Text + "', '" + 0 + "','" + tx_qty_to_issue.Text + "','" + tx_qty_to_issueGradeA.Text + "','" + hdfAgrmtID.Value + "','" + D_Proximate_CommonRice + "','" + D_Proximate_GradeARice + "','" + D_Proximate_TotalRice + "','" + ddlBranch.SelectedValue.ToString() + "')";
                                    cmd.CommandText = str1;
                                    cmd.ExecuteNonQuery();

                                    float RemCDhan = (float.Parse(tx_issue_balqty.Text) - float.Parse(tx_qty_to_issue.Text));
                                    float RemGADhan = (float.Parse(txt_IssuedbalanceGradeADhan.Text) - float.Parse(tx_qty_to_issueGradeA.Text));
                                    str1 = "update PaddyMilling_DO set Rem_Alloted_CommonDhan='" + RemCDhan + "', Rem_Alloted_GradeADhan='" + RemGADhan + "' where DO_Number='" + ddl_do_no.SelectedValue.ToString() + "' and Mill_Name='" + ddlpartyname.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and Agreement_ID='" + hdfAgrmtID.Value + "'";
                                    cmd.CommandText = str1;
                                    cmd.ExecuteNonQuery();

                                    cmd.CommandText = ("Select GETDATE()");
                                    hdfServerDate.Value = cmd.ExecuteScalar().ToString();

                                    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Data Saved Successfully and Your Challan Number Is " + transid + " ');</script>");
                                    Label1.Text = "Data Saved Successfully and Your Challan Number Is :" + transid;
                                    Label1.Visible = true;

                                    ddl_do_no.SelectedIndex = ddlBranch.SelectedIndex = ddlsarrival.SelectedIndex = 0;
                                    tx_cur_bal.Text = "";
                                    btnsave.Enabled = ddlsarrival.Enabled = ddl_godown.Enabled = ddlBranch.Enabled = false;
                                    btnPrint.Enabled = true;
                                    Trans_csms.Commit();
                                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                                }


                                # region Original_Table
                                //if (con.State == ConnectionState.Closed)
                                //{
                                //    con.Open();
                                //}

                                //ClientIP objClientIP = new ClientIP();
                                //string GetIp = (objClientIP.GETIP() + "  " + objClientIP.GETHOST());

                                //string browser = Request.Browser.Browser.ToString();
                                //string version = Request.Browser.Version.ToString();
                                //string useragent = browser + version;

                                //int trnscnt = 0;
                                //string docount = "";
                                //string strqr = "select count(delivery_order_no) as rwcount  from dbo.issue_against_OpenSale_do where delivery_order_no='" + do_no + "' and district_code='" + dist + "' and CropYear='" + txtYear.Text + "'";
                                //cmd.CommandText = strqr;
                                //cmd.Connection = con;
                                //dr = cmd.ExecuteReader();
                                //while (dr.Read())
                                //{
                                //    docount = dr["rwcount"].ToString();
                                //}
                                //dr.Close();
                                //strqr = "select trans_id from dbo.issue_against_OpenSale_do where delivery_order_no='" + do_no + "' and district_code='" + dist + "' and CropYear='" + txtYear.Text + "'";
                                //cmd.CommandText = strqr;
                                //cmd.Connection = con;
                                //dr = cmd.ExecuteReader();
                                //while (dr.Read())
                                //{
                                //    transid = dr["trans_id"].ToString();
                                //}
                                //dr.Close();

                                //int indx2 = 0;
                                //if (transid != "")
                                //{
                                //    indx2 = transid.LastIndexOf("-");
                                //}
                                //int indx = indx2;
                                //if (CheckNullInt(docount) > 0 && indx <= 0)
                                //{
                                //    trnscnt = CheckNullInt(docount) + 1;
                                //    transid = do_no.ToString() + (trnscnt).ToString();
                                //}
                                //else
                                //{
                                //    strqr = "select count(delivery_order_no) as rwcount  from dbo.issue_against_OpenSale_do where delivery_order_no='" + do_no + "' and district_code='" + dist + "' and CropYear='" + txtYear.Text + "'";
                                //    cmd.CommandText = strqr;
                                //    cmd.Connection = con;
                                //    dr = cmd.ExecuteReader();
                                //    while (dr.Read())
                                //    {
                                //        docount = dr["rwcount"].ToString();
                                //    }
                                //    dr.Close();
                                //    if (CheckNullInt(docount) < 0)
                                //    {
                                //        docount = "0";
                                //    }
                                //    if (docount != "")
                                //    {
                                //        trnscnt = CheckNullInt(docount);
                                //    }
                                //    trnscnt++;
                                //    transid = do_no.ToString() + "-" + (trnscnt).ToString();
                                //}

                                //decimal lift_qty = 0;
                                //string str2 = "select round(convert(decimal(18,5),lift_qty),5) as lift_qty from dbo.sum_trans_do where district_code='" + dist + "' and issueCentre_code='" + issue_centre_code + "' ";
                                //cmd.Connection = con;
                                //cmd.CommandText = str2;
                                //dr = cmd.ExecuteReader();
                                //while (dr.Read())
                                //{
                                //    lift_qty = CheckNull(dr["lift_qty"].ToString());
                                //}
                                //dr.Close();

                                //SqlTransaction Trans_csms;
                                //Trans_csms = con.BeginTransaction();

                                //cmd.Connection = con;

                                //cmd.Transaction = Trans_csms;

                                //try
                                //{
                                // hdfChallanNo.Value = transid;
                                //hdfDONumber.Value = do_no;
                                //hdfGodown.Value = ddl_godown.SelectedItem.ToString();
                                //    string str1 = "INSERT INTO dbo.issue_against_OpenSale_do(trans_id,delivery_order_no,district_code,issueCentre_code,issue_to,bags,Source,Godown,issue_date,gate_pass,created_date,updated_date,swc_status,ip_add,OperatorID,NoTransaction,Partyname,User_Agent,CropYear,LotNumber,Milling_Type,Issued_CommonDhan,Issued_GradeADhan,qty_issue, Rem_CommonDhan,Rem_GradeADhan) VALUES('" + transid + "','" + do_no + "','" + dist + "','" + issue_centre_code + "','" + ddlissueto.SelectedValue + "'," + tx_bags.Text + ",'" + ddlsarrival.SelectedItem.Value + "','" + ddl_godown.SelectedItem.Value + "','" + issue_date + "','" + gate_pass + "',getdate(),'','N','" + GetIp + "','" + opid + "','" + notrans + "','" + partyname + "','" + useragent + "','" + txtYear.Text + "','" + txtLotNumber.Text + "',N'" + hdfMillingType.Value.ToString() + "','" + tx_qty_to_issue.Text + "','" + tx_qty_to_issueGradeA.Text + "', '" + 0 + "','" + tx_qty_to_issue.Text + "','" +tx_qty_to_issueGradeA.Text +"')";
                                //    cmd.CommandText = str1;
                                //    cmd.ExecuteNonQuery();

                                //    float RemCDhan = (float.Parse(tx_issue_balqty.Text) - float.Parse(tx_qty_to_issue.Text));
                                //    float RemGADhan = (float.Parse(txt_IssuedbalanceGradeADhan.Text) - float.Parse(tx_qty_to_issueGradeA.Text));
                                //    str1 = "update PaddyMilling_DO set Rem_Alloted_CommonDhan='" + RemCDhan + "', Rem_Alloted_GradeADhan='" + RemGADhan + "' where DO_Number='" + ddl_do_no.SelectedValue.ToString() + "' and Mill_Name='" + ddlpartyname.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "'";
                                //    cmd.CommandText = str1;
                                //    cmd.ExecuteNonQuery();


                                //    decimal TotalQtyIssue = (CheckNull(tx_qty_to_issue.Text) + CheckNull(tx_qty_to_issueGradeA.Text));
                                //    lift_qty = lift_qty + TotalQtyIssue;
                                //    str1 = "update dbo.sum_trans_do set lift_qty=round(" + lift_qty + ",5) where district_code='" + dist + "' and issueCentre_code='" + issue_centre_code + "'";
                                //    cmd.CommandText = str1;
                                //    cmd.ExecuteNonQuery();
                                //    str1 = "update dbo.Current_Godown_Position set Current_Bags=Current_Bags-" + CheckNullInt(tx_bags.Text) + ",Current_Balance=round(convert(decimal(18,5),Current_Balance)-" + TotalQtyIssue + ",5),Current_Capacity=round(convert(decimal(18,5),Current_Capacity)+" + TotalQtyIssue + ",5) where District_Id='" + dist + "' and Depotid='" + sid + "' and Godown='" + ddl_godown.SelectedItem.Value + "'";
                                //    cmd.CommandText = str1;
                                //    cmd.ExecuteNonQuery();

                                //    {
                                //        int rcount = 0;
                                //        string dist1 = distid;
                                //        string issue_centre_code1 = sid;
                                //        string ip1 = Request.ServerVariables["REMOTE_ADDR"].ToString();


                                //        string tempos = "NNN";
                                //        string strope = "";
                                //        strope = "select *  from dbo.tbl_Stock_Registor  where Commodity_Id ='" + ddl_commodity.SelectedItem.Value + "' and Scheme_Id ='" + ddl_scheme.SelectedItem.Value + "' and DistrictId='" + dist1 + "'and DepotID='" + issue_centre_code1 + "'and Month=" + DateTime.Today.Month + "and Year=" + DateTime.Today.Year;
                                //        cmd.CommandText = strope;
                                //        dr = cmd.ExecuteReader();
                                //        while (dr.Read())
                                //        {
                                //            tempos = "YYY";
                                //        }
                                //        dr.Close();
                                //        if (tempos == "YYY")
                                //        {
                                //            strope = "update dbo.tbl_Stock_Registor set Sale_Do=round(convert(decimal(18,5),Sale_Do)+" + TotalQtyIssue + ",5) where Commodity_Id ='" + ddl_commodity.SelectedItem.Value + "' and Scheme_Id ='" + ddl_scheme.SelectedItem.Value + "' and DistrictId='" + dist1 + "'and DepotID='" + issue_centre_code1 + "'and Month=" + DateTime.Today.Month + "and Year=" + DateTime.Today.Year;
                                //            cmd.CommandText = strope;
                                //            cmd.ExecuteNonQuery();
                                //        }
                                //        else
                                //        {
                                //            decimal opebal = 0;
                                //            strope = "select sum(convert(decimal(18,5),Current_Balance)) as opebal  from dbo.issue_opening_balance  where Commodity_Id ='" + ddl_commodity.SelectedItem.Value + "' and Scheme_Id ='" + ddl_scheme.SelectedItem.Value + "' and District_Id='" + dist1 + "'and Depotid='" + issue_centre_code1 + "'";
                                //            cmd.CommandText = strope;
                                //            dr = cmd.ExecuteReader();
                                //            while (dr.Read())
                                //            {
                                //                opebal = CheckNull(dr["opebal"].ToString());
                                //            }
                                //            dr.Close();
                                //            strope = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Scheme_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Received_OtherSch,Recieved_Other_Src,Received_RailHead,Received_CMR,Received_Levy,Sale_Do,Sale_otherg,Transfer_OtherSch,Month,Year,Remarks) Values('" + distid + "','" + sid + "','" + ddl_commodity.SelectedItem.Value + "','" + ddl_scheme.SelectedItem.Value + "'," + opebal + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + TotalQtyIssue + "," + 0 + "," + 0 + "," + 0 + "," + DateTime.Today.Month + "," + DateTime.Today.Year + ",'')";
                                //            cmd.CommandText = strope;
                                //            cmd.ExecuteNonQuery();
                                //        }

                                //        tempos = "NNN";
                                //        strope = "select *  from dbo.issue_opening_balance  where District_Id='" + dist1 + "'and Depotid='" + issue_centre_code1 + "'and Commodity_Id='" + ddl_commodity.SelectedItem.Value + "'and Scheme_Id='" + ddl_scheme.SelectedItem.Value + "' and Godown='" + ddl_godown.SelectedItem.Value + "' and Source='" + ddlsarrival.SelectedItem.Value + "'";
                                //        cmd.CommandText = strope;
                                //        dr = cmd.ExecuteReader();
                                //        while (dr.Read())
                                //        {
                                //            tempos = "YYY";
                                //        }
                                //        dr.Close();
                                //        if (tempos == "YYY")
                                //        {
                                //            strope = "Update dbo.issue_opening_balance set Current_Balance=round(convert(decimal(18,5),Current_Balance)-" + TotalQtyIssue + ",5),Current_Bags=Current_Bags-" + CheckNullInt(tx_bags.Text) + " where District_Id='" + dist1 + "'and Depotid='" + issue_centre_code1 + "'and Commodity_Id='" + ddl_commodity.SelectedItem.Value + "'and Scheme_Id='" + ddl_scheme.SelectedItem.Value + "' and Godown='" + ddl_godown.SelectedItem.Value + "' and Source='" + ddlsarrival.SelectedItem.Value + "'";
                                //            cmd.CommandText = strope;
                                //            cmd.ExecuteNonQuery();
                                //        }
                                //        else
                                //        {
                                //            strope = "insert into dbo.issue_opening_balance(State_Id,District_Id,Depotid,Commodity_Id,Scheme_Id,Category_Id,Godown,Crop_year,Bags,Quantity,Source,Current_Balance,Current_Bags,Month,Year,IP_Address,Stock_Date,CreatedDate,UpdatedDate,DeletedDate) values('23','" + distid + "','" + sid + "','" + ddl_commodity.SelectedItem.Value + "','" + ddl_scheme.SelectedItem.Value + "','','" + ddl_godown.SelectedItem.Value + "','',0,0,'" + ddlsarrival.SelectedItem.Value + "'," + -TotalQtyIssue + "," + -CheckNullInt(tx_bags.Text) + "," + DateTime.Today.Month + "," + DateTime.Today.Year + ",'" + ip1 + "',getdate(),getdate(),'','')";
                                //            cmd.CommandText = strope;
                                //            cmd.ExecuteNonQuery();
                                //        }
                                //    }
                                //    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Data Saved Successfully ...');</script>");
                                //    Label1.Text = "Data Saved Successfully ...";
                                //    

                                 //ddl_do_no.SelectedIndex = ddl_godown.SelectedIndex = ddlsarrival.SelectedIndex = 0;
                                //   tx_cur_bal.Text = "";
                                //   btnsave.Enabled = ddlsarrival.Enabled = ddl_godown.Enabled = false;
                                //  btnPrint.Enabled = true;
                                //    Trans_csms.Commit();
                                //  Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                                //}
                                # endregion

                                catch (Exception ex)
                                {
                                    dr.Close();
                                    dr_opdms.Close();
                                    Trans_csms.Rollback();

                                    Label1.Text = ex.Message;
                                }
                                finally
                                {
                                    if (con.State == ConnectionState.Open)
                                    {
                                        con.Close();
                                    }
                                }
                            }
                        }
                   // }
                }
            }
        }
        else
        {
            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
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

    protected void ddlissueto_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GetPartyname();
        get_do_no();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/PaddyMilling/Print/PDhan_Challan.aspx", true);
    }

    public string DistManager
    {
        get { return hdfDistName.Value.ToString(); }
    }

    public string Year
    {
        get { return txtYear.Text; }
    }

    public string ChallanNo
    {
        get { return hdfChallanNo.Value; }
    }

    public string ChallanDate
    {
        get
        {
            DateTime fromdate = DateTime.ParseExact(tx_issued_date.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            return fromdate.ToString("dd-MMM-yyyy");
        }
    }

    public string DONumber
    {
        get { return hdfDONumber.Value; }
    }

    public string DODate
    {
        get
        {
            DateTime fromdate = DateTime.ParseExact(tx_do_date.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            return fromdate.ToString("dd-MMM-yyyy");
        }
    }

    public string LotNumber
    {
        get { return txtLotNumber.Text; }
    }

    public string MillName
    {
        get { return ddlpartyname.SelectedItem.ToString(); }
    }

    public string DoQuantityCDhan
    {
        get { return tx_do_qty.Text; }
    }

    public string DoQuantityGradeADhan
    {
        get { return txt_DOGradeADhan.Text; }
    }

    public string DoChallanQuantityCDhan
    {
        get { return tx_qty_to_issue.Text; }
    }

    public string DoChallanQuantityGradeADhan
    {
        get { return tx_qty_to_issueGradeA.Text; }
    }

    public string NumberOfBags
    {
        get { return tx_bags.Text; }
    }

    public string ICName
    {
        get { return hdfICName.Value; }
    }

    public string GodownName
    {
        get { return hdfGodown.Value; }
    }

    public string ServerDate
    {
        get { return hdfServerDate.Value; }
    }

    public void GetBranch()
    {
        ddlBranch.Items.Clear();
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select tbl_MetaData_DEPOT.DepotName,tbl_MetaData_DEPOT.BranchID from MetaDataBranchWithIssueCenter inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.BranchId=MetaDataBranchWithIssueCenter.BranchID where IssueCenterId='{0}'", sid);
                da_MPStorage = new SqlDataAdapter(select, con_MPStorage);

                ds_MPStorage = new DataSet();
                da_MPStorage.Fill(ds_MPStorage);
                if (ds_MPStorage != null)
                {
                    if (ds_MPStorage.Tables[0].Rows.Count > 0)
                    {
                        ddlBranch.DataSource = ds_MPStorage.Tables[0];
                        ddlBranch.DataTextField = "DepotName";
                        ddlBranch.DataValueField = "BranchID";
                        ddlBranch.DataBind();
                        ddlBranch.Items.Insert(0, "--Select--");
                    }
                }
                else
                {
                    string select1 = string.Format("select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId= '23" + distid + "' order by DepotName");
                    da_MPStorage = new SqlDataAdapter(select1, con_MPStorage);

                    ds_MPStorage = new DataSet();
                    da_MPStorage.Fill(ds_MPStorage, "tbl_MetaData_DEPOT");
                    if (ds_MPStorage.Tables[0].Rows.Count > 0)
                    {
                        ddlBranch.DataSource = ds_MPStorage.Tables[0];
                        ddlBranch.DataTextField = "DepotName";
                        ddlBranch.DataValueField = "BranchId";
                        ddlBranch.DataBind();
                        ddlBranch.Items.Insert(0, "--Select--");
                    }
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con_MPStorage.State != ConnectionState.Closed)
                {
                    con_MPStorage.Close();
                }
            }
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetGodownMPStorage();
    }

    public void GetGodownMPStorage()
    {
        ddl_godown.Items.Clear();
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where BranchID='{0}'", ddlBranch.SelectedValue.ToString());
                da_MPStorage = new SqlDataAdapter(select, con_MPStorage);

                ds_MPStorage = new DataSet();
                da_MPStorage.Fill(ds_MPStorage);
                if (ds_MPStorage != null)
                {
                    if (ds_MPStorage.Tables[0].Rows.Count > 0)
                    {
                        ddl_godown.DataSource = ds_MPStorage.Tables[0];
                        ddl_godown.DataTextField = "Godown_Name";
                        ddl_godown.DataValueField = "Godown_ID";
                        ddl_godown.DataBind();
                        ddl_godown.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया ब्रांच का नाम चुनें |'); </script> ");
                    }
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con_MPStorage.State != ConnectionState.Closed)
                {
                    con_MPStorage.Close();
                }
            }
        }
    }
}


