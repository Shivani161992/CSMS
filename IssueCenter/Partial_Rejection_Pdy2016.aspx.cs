using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls.WebParts;

public partial class IssueCenter_Partial_Rejection_Pdy2016 : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string IC_Id = "", Dist_Id = "";

    string Con_CSMS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string Con_WH = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    string Con_Paddy = ConfigurationManager.AppSettings["Appconstr_PPMS2016"].ToString(); //PPMS 2016
    public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_PPMS2016"].ToString());

    string Con_Maze = ConfigurationManager.AppSettings["Appconstr_MPMS2016"].ToString(); //MPMS 2016
    public SqlConnection con_maze = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_MPMS2016"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            if (!IsPostBack)
            {
                Session["Godown"] = "";
                Session["ReceiptID"] = "";
                Session["Commodity_Id"] = "";

                txtreason.Attributes.Add("maxlength", txtreason.MaxLength.ToString());

                txtreason.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtreason.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtreason.Attributes.Add("onchange", "return chksqltxt(this)");

                IC_Id = Session["issue_id"].ToString();
                Dist_Id = Session["dist_id"].ToString();

                GetICName();
                GetCommodity();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    private void GetICName()
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();
                string select = "select DepotName,(CONVERT(VARCHAR,GETDATE(), 105)) As AcptDate  from dbo.tbl_MetaData_DEPOT where DepotID='" + IC_Id + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtissue.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                    DaintyDate3.Text = ds.Tables[0].Rows[0]["AcptDate"].ToString();
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                return;
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }

    private void GetCommodity()
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();

                string select = "";

                select = "SELECT Commodity_Name ,Commodity_Id FROM tbl_MetaData_STORAGE_COMMODITY where  Commodity_Id in ('13','14','8','11','12','40') Order By Commodity_Name";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlcommodtiy.DataSource = ds.Tables[0];
                    ddlcommodtiy.DataTextField = "Commodity_Name";
                    ddlcommodtiy.DataValueField = "Commodity_Id";
                    ddlcommodtiy.DataBind();
                    ddlcommodtiy.Items.Insert(0, "--Select--");

                    // ddlcommodtiy.SelectedIndex = 5;
                    //GetGodown();
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                return;
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }

    protected void ddlcommodtiy_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdfOriginalBags.Value = hdfOriginalQty.Value = hdfGodownCode.Value = "";
        ddlgodown.Items.Clear();
        ddlissueId.Items.Clear();
        txtSocName.Text = TxtTruckNumber.Text = txtTcNumber.Text = txtsendbags.Text = txtrecbags.Text = txtsendQty.Text = txtRecdQty.Text = txtdiffBags.Text = txtqtyDiff.Text = txtreason.Text = "";

        if (ddlcommodtiy.SelectedIndex > 0)
        {
            if (DaintyDate3.Text == "")
            {
                ddlcommodtiy.SelectedIndex = 0;
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Date of Deposit'); </script> ");
                return;
            }
            else
            {
                GetGodown();
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Commodity'); </script> ");
            return;
        }
    }

    private void GetGodown()
    {
        hdfGodownCode.Value = "";

        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                IC_Id = Session["issue_id"].ToString();
                Dist_Id = Session["dist_id"].ToString();

                string select = "";
                select = "SELECT distinct Recd_Godown FROM SCSC_Procurement_Kharif2016 where Distt_ID = '" + Dist_Id + "' and IssueCenter_ID = '" + IC_Id + "' and Commodity_Id = '" + ddlcommodtiy.SelectedValue.ToString() + "' and AN_Status = 'Y' and Quantity <> Recd_Qty and Book_No!='Rejected' and Receipt_Id in (select Acceptance_Note_Kharif2016.IssueID from Acceptance_Note_Kharif2016 where Reject_Qty = 0) and Recd_Godown is not null";
                con.Open();
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        hdfGodownCode.Value += ((hdfGodownCode.Value == "") ? "" : " , ") + "'" + ds.Tables[0].Rows[i]["Recd_Godown"].ToString() + "'";
                    }

                    if (hdfGodownCode.Value != "")
                    {
                        GetGodownName();
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('सम्बंधित दिनांक में गोदाम के नाम उपलब्ध नहीं है|'); </script> ");
                    return;
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                return;
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }

    private void GetGodownName()
    {
        using (con = new SqlConnection(Con_WH))
        {
            try
            {
                con.Open();
                string select = "select Godown_Name,Godown_ID from tbl_MetaData_GODOWN where Godown_ID IN (" + hdfGodownCode.Value + ")";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlgodown.DataSource = ds.Tables[0];
                    ddlgodown.DataTextField = "Godown_Name";
                    ddlgodown.DataValueField = "Godown_ID";
                    ddlgodown.DataBind();
                    ddlgodown.Items.Insert(0, "--Select--");
                }

            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }

    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdfOriginalBags.Value = hdfOriginalQty.Value = "";
        ddlissueId.Items.Clear();
        txtSocName.Text = TxtTruckNumber.Text = txtTcNumber.Text = txtsendbags.Text = txtrecbags.Text = txtsendQty.Text = txtRecdQty.Text = txtdiffBags.Text = txtqtyDiff.Text = txtreason.Text = lblSocId.Text = "";

        if (ddlgodown.SelectedIndex > 0)
        {
            if (DaintyDate3.Text == "")
            {
                ddlgodown.SelectedIndex = 0;
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Date of Deposit'); </script> ");
                return;
            }
            else
            {
                GetTCData();
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Receiving Godown'); </script> ");
            return;
        }
    }

    private void GetTCData()
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                IC_Id = Session["issue_id"].ToString();
                Dist_Id = Session["dist_id"].ToString();
                string pdate = getDate_MDY(DaintyDate3.Text);

                con.Open();
                string select = " SELECT SCSC_Procurement_Kharif2016.Receipt_Id + ' (' + SCSC_Procurement_Kharif2016.TC_Number + ')' as Receipt , SCSC_Procurement_Kharif2016.Receipt_Id FROM SCSC_Procurement_Kharif2016  where Commodity_Id='" + ddlcommodtiy.SelectedValue.ToString() + "' and Recd_Godown = '" + ddlgodown.SelectedValue.ToString() + "' and Recd_Date = '" + pdate + "' and SCSC_Procurement_Kharif2016.IssueCenter_ID = '" + IC_Id + "' and AN_Status = 'Y' and Quantity <> Recd_Qty and Distt_ID = '" + Dist_Id + "' and Book_No!='Rejected'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlissueId.DataSource = ds.Tables[0];
                    ddlissueId.DataTextField = "Receipt";
                    ddlissueId.DataValueField = "Receipt_Id";
                    ddlissueId.DataBind();
                    ddlissueId.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('सम्बंधित दिनांक में Issue Id / TC Number उपलब्ध नहीं है|'); </script> ");
                    return;
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    protected void ddlissueId_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdfOriginalBags.Value = hdfOriginalQty.Value = "";
        txtSocName.Text = TxtTruckNumber.Text = txtTcNumber.Text = txtsendbags.Text = txtrecbags.Text = txtsendQty.Text = txtRecdQty.Text = txtdiffBags.Text = txtqtyDiff.Text = txtreason.Text = lblSocId.Text = "";

        if (ddlissueId.SelectedIndex > 0)
        {
            if (DaintyDate3.Text == "")
            {
                ddlissueId.SelectedIndex = 0;
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Date of Deposit'); </script> ");
                return;
            }
            else
            {
                GetAllTCData();

                if (txtsendQty.Text == "" || txtRecdQty.Text == "")
                {
                    btnRecptSubmit.Enabled = false;
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('प्राप्त मात्रा अथवा भेजी गयी मात्रा गलत है  |'); </script> ");
                    return;
                }

                string sendqty = txtsendQty.Text;
                string recqty = txtRecdQty.Text;
                decimal sendquantity = Convert.ToDecimal(sendqty);
                decimal recquantity = Convert.ToDecimal(recqty);
                decimal cal = sendquantity - recquantity;
                string calqty = Convert.ToString(cal);
                string sendbgs = txtsendbags.Text;
                string recbgs = txtrecbags.Text;
                decimal sendbags = Convert.ToDecimal(sendbgs);
                decimal recbags = Convert.ToDecimal(recbgs);
                decimal calbag = sendbags - recbags;
                string calbgss = Convert.ToString(calbag);

                if (calqty == "" || calbgss == "")
                {
                    btnRecptSubmit.Enabled = false;
                    return;
                }
                else
                {
                    btnRecptSubmit.Enabled = true;
                    txtqtyDiff.Text = hdfOriginalQty.Value = calqty;
                    txtdiffBags.Text = hdfOriginalBags.Value = calbgss;
                }
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Issue Id / TC Number'); </script> ");
            return;
        }
    }

    private void GetAllTCData()
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                string pdate = getDate_MDY(DaintyDate3.Text);
                IC_Id = Session["issue_id"].ToString();

                con.Open();
                string select = "SELECT Society.Society_Id , Society.Society_Name + ' ( '+ Society.SocPlace + ' )' as Society ,SCSC_Procurement_Kharif2016.TC_Number ,SCSC_Procurement_Kharif2016.Truck_Number  ,  Quantity , No_of_Bags as sendbags ,(select sum(Recd_Qty) from SCSC_Procurement_Kharif2016 where Commodity_Id='" + ddlcommodtiy.SelectedValue.ToString() + "' and Recd_Date = '" + pdate + "' and SCSC_Procurement_Kharif2016.IssueCenter_ID = '" + IC_Id + "' and Receipt_Id = '" + ddlissueId.SelectedValue.ToString() + "') Recd_Qty  ,( select sum(Recd_Bags) from SCSC_Procurement_Kharif2016 where Commodity_Id='" + ddlcommodtiy.SelectedValue.ToString() + "' and Recd_Date = '" + pdate + "' and SCSC_Procurement_Kharif2016.IssueCenter_ID = '" + IC_Id + "' and Receipt_Id = '" + ddlissueId.SelectedValue + "') Recd_Bags  FROM SCSC_Procurement_Kharif2016 inner join SocietyKharif2016 As Society  on Society.Society_Id = SCSC_Procurement_Kharif2016.Purchase_Center  where Commodity_Id='" + ddlcommodtiy.SelectedValue.ToString() + "' and Recd_Godown = '" + ddlgodown.SelectedValue.ToString() + "' and Recd_Date = '" + pdate + "' and SCSC_Procurement_Kharif2016.IssueCenter_ID = '" + IC_Id + "' and Receipt_Id = '" + ddlissueId.SelectedValue + "' and AN_Status = 'Y'  and Receipt_Id in (select Acceptance_Note_Kharif2016.IssueID from Acceptance_Note_Kharif2016 where CommodityId='" + ddlcommodtiy.SelectedValue.ToString() + "' and IssueID = '" + ddlissueId.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "' and Reject_Qty = 0)";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblSocId.Text = ds.Tables[0].Rows[0]["Society_Id"].ToString();
                    txtSocName.Text = ds.Tables[0].Rows[0]["Society"].ToString();
                    TxtTruckNumber.Text = ds.Tables[0].Rows[0]["Truck_Number"].ToString();
                    txtTcNumber.Text = ds.Tables[0].Rows[0]["TC_Number"].ToString();
                    txtsendQty.Text = ds.Tables[0].Rows[0]["Quantity"].ToString();
                    txtRecdQty.Text = ds.Tables[0].Rows[0]["Recd_Qty"].ToString();
                    txtsendbags.Text = ds.Tables[0].Rows[0]["sendbags"].ToString();
                    txtrecbags.Text = ds.Tables[0].Rows[0]["Recd_Bags"].ToString();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('डाटा उपलब्ध नहीं है अथवा अस्वीकृति पत्रक बन चूका है|'); </script> ");
                    return;
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                return;
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }

    protected void btnRecptSubmit_Click(object sender, EventArgs e)
    {
        double Insertrejqty = Convert.ToDouble(txtqtyDiff.Text);
        Int32 Insertrejbags = Convert.ToInt32(txtdiffBags.Text);

        double Originalhdfrejqty = Convert.ToDouble(hdfOriginalQty.Value);
        Int32 Originalhdfrejbags = Convert.ToInt32(hdfOriginalBags.Value);

        if (ddlcommodtiy.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Commodity'); </script> ");
            return;
        }
        else if (Insertrejqty > Originalhdfrejqty)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Qty की मात्रा कम करें|'); </script> ");
            return;
        }
        else if (Insertrejbags > Originalhdfrejbags)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Bags की मात्रा कम करें|'); </script> ");
            return;
        }
        else if (ddlgodown.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Receiving Godown'); </script> ");
            return;
        }
        else if (DaintyDate3.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Date of Deposit'); </script> ");
            return;
        }
        else if (ddlissueId.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Issue Id / TC Number'); </script> ");
            return;
        }
        else if (chk_brightness.Checked == false && chk_damaged.Checked == false && chk_extra.Checked == false && chk_faq.Checked == false && chk_partially.Checked == false && chk_splited.Checked == false && chk_moist.Checked == false && txtreason.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('अस्विकृति का कोई एक कारण चुने |'); </script> ");
            return;
        }
        else
        {
            if (Session["update"].ToString() == ViewState["update"].ToString())
            {
                using (con = new SqlConnection(Con_CSMS))
                {
                    try
                    {
                        string tcnum = txtTcNumber.Text;
                        string trucknumber = TxtTruckNumber.Text;
                        string recdate = getDate_MDY(DaintyDate3.Text);
                        string issueid = ddlissueId.SelectedValue;
                        string socid = lblSocId.Text;

                        Dist_Id = Session["dist_id"].ToString();
                        IC_Id = Session["issue_id"].ToString();

                        string checkin = "SELECT * FROM Acceptance_Note_Kharif2016 where CommodityId='" + ddlcommodtiy.SelectedValue.ToString() + "' and  IssueID = '" + ddlissueId.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "' and Purchase_Center = '" + lblSocId.Text + "' and IssueCenter_ID = '" + IC_Id + "' and TC_Number = '" + txtTcNumber.Text + "'";
                        SqlCommand cmd = new SqlCommand(checkin, con);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            # region paddy
                            if (ddlcommodtiy.SelectedValue.ToString() == "13" || ddlcommodtiy.SelectedValue.ToString() == "14")
                            {
                                if (con_paddy.State == ConnectionState.Closed)
                                {
                                    con_paddy.Open();
                                }

                                SqlTransaction trns1;
                                SqlCommand cmdwpm = new SqlCommand();
                                cmdwpm.Connection = con_paddy;
                                trns1 = con_paddy.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                cmdwpm.Transaction = trns1;

                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                SqlTransaction trns;
                                SqlCommand cmdcsms = new SqlCommand();
                                cmdcsms.Connection = con;
                                trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                cmdcsms.Transaction = trns;

                                try
                                {
                                    string value_brightness = "0";
                                    string value_damaged = "0";
                                    string value_extra = "0";
                                    string value_faq = "0";
                                    string value_partially = "0";
                                    string value_splited = "0";
                                    string value_moist = "0";

                                    if (chk_brightness.Checked)
                                    {
                                        value_brightness = "1";
                                    }
                                    if (chk_damaged.Checked)
                                    {
                                        value_damaged = "1";
                                    }
                                    if (chk_extra.Checked)
                                    {
                                        value_extra = "1";
                                    }
                                    if (chk_faq.Checked)
                                    {
                                        value_faq = "1";
                                    }
                                    if (chk_partially.Checked)
                                    {
                                        value_partially = "1";
                                    }
                                    if (chk_splited.Checked)
                                    {
                                        value_splited = "1";
                                    }
                                    if (chk_moist.Checked)
                                    {
                                        value_moist = "1";
                                    }

                                    double rejqty = Convert.ToDouble(txtqtyDiff.Text);
                                    Int32 rejbags = Convert.ToInt32(txtdiffBags.Text);

                                    string updtwpms = "Update Acceptance_Note_Detail set Reject_Qty  = " + rejqty + " , reject_Bags = " + rejbags + "  where IssueID = '" + ddlissueId.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "' and Purchase_Center = '" + lblSocId.Text + "' and IssueCenter_ID = '" + IC_Id + "' and TC_Number = '" + txtTcNumber.Text + "'";
                                    cmdwpm.CommandText = updtwpms;

                                    string updtcsms = "Update Acceptance_Note_Kharif2016 set Reject_Qty  = " + rejqty + " , reject_Bags = " + rejbags + "  where CommodityId='" + ddlcommodtiy.SelectedValue.ToString() + "' and IssueID = '" + ddlissueId.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "' and Purchase_Center = '" + lblSocId.Text + "' and IssueCenter_ID = '" + IC_Id + "' and TC_Number = '" + txtTcNumber.Text + "'  and CommodityId='" + ddlcommodtiy.SelectedValue.ToString() + "'";
                                    cmdcsms.CommandText = updtcsms;

                                    int x = cmdwpm.ExecuteNonQuery();
                                    int count = cmdcsms.ExecuteNonQuery();

                                    if (count > 0)
                                    {
                                        trns1.Commit();
                                        trns.Commit();

                                        if (con_paddy.State == ConnectionState.Open)
                                        {
                                            con_paddy.Close();
                                        }

                                        string insrej = "Insert into Rejected_Truck_Details (Distt_Id ,Depot_Id ,IssueId ,FAQ_LowQuality ,ExternalMaterial ,Damaged ,Brightless ,PartiallyAffected ,GrainSplited ,MoisturePercentage ,Others,Faq_Percent,Extra_Percent,Damage_Percent,Bright_Percent,Partial_Percent,Split_Percent,Moisture_percent) values ('" + Dist_Id + "','" + IC_Id + "','" + ddlissueId.SelectedValue + "' ,'" + value_faq + "' , '" + value_extra + "' , '" + value_damaged + "' ,'" + value_brightness + "', '" + value_partially + "', '" + value_splited + "' , '" + value_moist + "' , N'" + txtreason.Text + "' , " + txt_faq_per.Text + " , " + txt_extra_per.Text + " , " + txt_damage_per.Text + " , " + txt_bright_per.Text + " , " + txt_partial_per.Text + " , " + txt_split_per.Text + " , " + txt_moist_per.Text + " )";
                                        SqlCommand cmd_rej = new SqlCommand(insrej, con);
                                        int xx = cmd_rej.ExecuteNonQuery();
                                        if (con.State == ConnectionState.Open)
                                        {
                                            con.Close();
                                        }

                                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Partial Rejection Is Created Successfully....'); </script> ");

                                        btnRecptSubmit.Enabled = false;
                                        btnPrint.Enabled = true;
                                        ddlcommodtiy.Enabled = ddlgodown.Enabled = ddlissueId.Enabled = false;

                                        Label2.Visible = true;
                                        Label2.Text = "Partial Rejection Is Created Successfully";

                                        Session["Godown"] = ddlgodown.SelectedValue;
                                        Session["ReceiptID"] = ddlissueId.SelectedValue;

                                        if (ddlcommodtiy.SelectedValue.ToString() == "13")
                                        {
                                            Session["Commodity_Id"] = "2";
                                        }
                                        else if (ddlcommodtiy.SelectedValue == "14")
                                        {
                                            Session["Commodity_Id"] = "3";
                                        }

                                        ddlcommodtiy.Items.Clear();

                                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                                    }
                                }

                                catch (Exception ex)
                                {
                                    btnRecptSubmit.Enabled = false;
                                    trns1.Rollback();
                                    trns.Rollback();
                                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                                    return;
                                }

                                finally
                                {
                                    if (con.State == ConnectionState.Open)
                                    {
                                        con.Close();
                                    }

                                    if (con_paddy.State == ConnectionState.Open)
                                    {
                                        con_paddy.Close();
                                    }
                                }
                            }
                            # endregion
                            else
                            {
                                if (con_maze.State == ConnectionState.Closed)
                                {
                                    con_maze.Open();
                                }

                                SqlTransaction trns1;
                                SqlCommand cmdwpm = new SqlCommand();
                                cmdwpm.Connection = con_maze;
                                trns1 = con_maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                cmdwpm.Transaction = trns1;

                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                SqlTransaction trns;
                                SqlCommand cmdcsms = new SqlCommand();
                                cmdcsms.Connection = con;
                                trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                cmdcsms.Transaction = trns;

                                try
                                {
                                    string value_brightness = "0";
                                    string value_damaged = "0";
                                    string value_extra = "0";
                                    string value_faq = "0";
                                    string value_partially = "0";
                                    string value_splited = "0";
                                    string value_moist = "0";

                                    if (chk_brightness.Checked)
                                    {
                                        value_brightness = "1";
                                    }
                                    if (chk_damaged.Checked)
                                    {
                                        value_damaged = "1";
                                    }
                                    if (chk_extra.Checked)
                                    {
                                        value_extra = "1";
                                    }
                                    if (chk_faq.Checked)
                                    {
                                        value_faq = "1";
                                    }
                                    if (chk_partially.Checked)
                                    {
                                        value_partially = "1";
                                    }
                                    if (chk_splited.Checked)
                                    {
                                        value_splited = "1";
                                    }
                                    if (chk_moist.Checked)
                                    {
                                        value_moist = "1";
                                    }

                                    double rejqty = Convert.ToDouble(txtqtyDiff.Text);
                                    Int32 rejbags = Convert.ToInt32(txtdiffBags.Text);

                                    string updtwpms = "Update Acceptance_Note_Detail set Reject_Qty  = " + rejqty + " , reject_Bags = " + rejbags + "  where IssueID = '" + ddlissueId.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "' and Purchase_Center = '" + lblSocId.Text + "' and IssueCenter_ID = '" + IC_Id + "' and TC_Number = '" + txtTcNumber.Text + "'";
                                    cmdwpm.CommandText = updtwpms;

                                    string updtcsms = "Update Acceptance_Note_Kharif2016 set Reject_Qty  = " + rejqty + " , reject_Bags = " + rejbags + "  where CommodityId='" + ddlcommodtiy.SelectedValue.ToString() + "' and IssueID = '" + ddlissueId.SelectedValue + "' and godown = '" + ddlgodown.SelectedValue + "' and Purchase_Center = '" + lblSocId.Text + "' and IssueCenter_ID = '" + IC_Id + "' and TC_Number = '" + txtTcNumber.Text + "' and CommodityId='" + ddlcommodtiy.SelectedValue.ToString() + "'";
                                    cmdcsms.CommandText = updtcsms;

                                    int x = cmdwpm.ExecuteNonQuery();
                                    int count = cmdcsms.ExecuteNonQuery();

                                    if (count > 0)
                                    {
                                        trns1.Commit();
                                        trns.Commit();

                                        if (con_maze.State == ConnectionState.Open)
                                        {
                                            con_maze.Close();
                                        }

                                        string insrej = "Insert into Rejected_Truck_Details (Distt_Id ,Depot_Id ,IssueId ,FAQ_LowQuality ,ExternalMaterial ,Damaged ,Brightless ,PartiallyAffected ,GrainSplited ,MoisturePercentage ,Others,Faq_Percent,Extra_Percent,Damage_Percent,Bright_Percent,Partial_Percent,Split_Percent,Moisture_percent) values ('" + Dist_Id + "','" + IC_Id + "','" + ddlissueId.SelectedValue + "' ,'" + value_faq + "' , '" + value_extra + "' , '" + value_damaged + "' ,'" + value_brightness + "', '" + value_partially + "', '" + value_splited + "' , '" + value_moist + "' , N'" + txtreason.Text + "' , " + txt_faq_per.Text + " , " + txt_extra_per.Text + " , " + txt_damage_per.Text + " , " + txt_bright_per.Text + " , " + txt_partial_per.Text + " , " + txt_split_per.Text + " , " + txt_moist_per.Text + " )";
                                        SqlCommand cmd_rej = new SqlCommand(insrej, con);
                                        int xx = cmd_rej.ExecuteNonQuery();
                                        if (con.State == ConnectionState.Open)
                                        {
                                            con.Close();
                                        }

                                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Partial Rejection Is Created Successfully....'); </script> ");

                                        btnRecptSubmit.Enabled = false;
                                        btnPrint.Enabled = true;
                                        ddlcommodtiy.Enabled = ddlgodown.Enabled = ddlissueId.Enabled = false;

                                        Label2.Visible = true;
                                        Label2.Text = "Partial Rejection Is Created Successfully";

                                        Session["Godown"] = ddlgodown.SelectedValue;
                                        Session["ReceiptID"] = ddlissueId.SelectedValue;

                                        if (ddlcommodtiy.SelectedValue.ToString() == "8")
                                        {
                                            Session["Commodity_Id"] = "6";
                                        }
                                        else if (ddlcommodtiy.SelectedValue.ToString() == "11")
                                        {
                                            Session["Commodity_Id"] = "4";
                                        }
                                        else if (ddlcommodtiy.SelectedValue.ToString() == "12")
                                        {
                                            Session["Commodity_Id"] = "5";
                                        }
                                        else if (ddlcommodtiy.SelectedValue.ToString() == "40")
                                        {
                                            Session["Commodity_Id"] = "7";
                                        }

                                        ddlcommodtiy.Items.Clear();

                                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                                    }
                                }

                                catch (Exception ex)
                                {
                                    btnRecptSubmit.Enabled = false;
                                    trns1.Rollback();
                                    trns.Rollback();
                                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                                    return;
                                }

                                finally
                                {
                                    if (con.State == ConnectionState.Open)
                                    {
                                        con.Close();
                                    }

                                    if (con_maze.State == ConnectionState.Open)
                                    {
                                        con_maze.Close();
                                    }
                                }
                            }
                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('स्वीकृत की गयी मात्रा का स्वीकृत पत्रक पहले जारी करें|'); </script> ");
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                        return;
                    }

                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                        {
                            con.Close();
                        }
                    }
                }
            }
            else
            {
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    protected void btnPrint_Click1(object sender, EventArgs e)
    {
        string url = "Print_PartialRej_Pdy2016.aspx";
        string s = "window.open('" + url + "', 'popup_window');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }

    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }
    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

}