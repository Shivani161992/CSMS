using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Globalization;

public partial class IssueCenter_DepositerForm_procurement2015 : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_WPMS = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2015"].ToString());

    //By A public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_PPMS2015_16"].ToString());
    //By A public SqlConnection con_Maze = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_MPMS2015_16"].ToString());

    public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.ConnectionStrings["Appconstr_PPMS2015_16"].ToString());
    public SqlConnection con_Maze = new SqlConnection(ConfigurationManager.ConnectionStrings["Appconstr_MPMS2015_16"].ToString());

    SqlCommand cmd_wpm = new SqlCommand();
    SqlCommand cmd_ppm = new SqlCommand();
    SqlCommand cmd_mpm = new SqlCommand();

    SqlCommand cmd_con = new SqlCommand();

    string gdn = "";
    string distid = "";
    string issueid = "";
    string Accpt_NO = "";

    decimal aceqty = 0;
    Int32 accbags = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            distid = Session["dist_id"].ToString();

            issueid = Session["issue_id"].ToString();

            DaintyDate3.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            DaintyDate3.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            DaintyDate3.Attributes.Add("onchange", "return chksqltxt(this)");


            if (!IsPostBack)
            {
                if (Session["Markfed"].ToString() == "Y")
                {
                    string getcom = "SELECT Commodity_Name ,Commodity_Id FROM tbl_MetaData_STORAGE_COMMODITY where  Commodity_Id in ('13','14','22') order by Commodity_Name desc";
                    SqlCommand cmd = new SqlCommand(getcom, con);

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandTimeout = 0;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataSet ds = new DataSet();

                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlcommodtiy.DataSource = ds.Tables[0];

                        ddlcommodtiy.DataTextField = "Commodity_Name";
                        ddlcommodtiy.DataValueField = "Commodity_Id";

                        ddlcommodtiy.DataBind();

                        ddlcommodtiy.Items.Insert(0, "--Select--");
                    }


                }


                else
                {
                    string getcom = "SELECT Commodity_Name ,Commodity_Id FROM tbl_MetaData_STORAGE_COMMODITY where  Commodity_Id in (8,11,12,13,14,22,40) order by Commodity_Name desc";
                    SqlCommand cmd = new SqlCommand(getcom, con);

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandTimeout = 0;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataSet ds = new DataSet();

                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlcommodtiy.DataSource = ds.Tables[0];

                        ddlcommodtiy.DataTextField = "Commodity_Name";
                        ddlcommodtiy.DataValueField = "Commodity_Id";

                        ddlcommodtiy.DataBind();

                        ddlcommodtiy.Items.Insert(0, "--Select--");
                    }

                }

                HyperLink1.Attributes.Add("onclick", "window.open('Print_WHRRequest2015.aspx',null,'left=50, top=10, height=570, width= 690, status=no, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");

                //  GetGodown();

                if (con.State == ConnectionState.Open)
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

    void GetGodown()
    {
        if (DaintyDate3.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('स्वीकृति पत्रक दिनांक चुने'); </script> ");
        }
        else
        {

            string pdate = getDate_MDY(DaintyDate3.Text);

            if (ddlcommodtiy.SelectedValue == "22")
            {
                string qry = "SELECT distinct Recd_Godown ,GodownName FROM SCSC_Procurement where Acceptance_Date = '" + pdate + "' and AN_Status = 'Y' and Distt_ID = '" + distid + "' and IssueCenter_ID = '" + issueid + "' and Commodity_Id = '" + ddlcommodtiy.SelectedValue + "' and SCSC_Procurement.Receipt_Id in (select IssueID from Acceptance_Note_Detail where WHR_Request is null and Distt_ID = '" + distid + "' and IssueCenter_ID = '" + issueid + "' and Acceptance_Date = '" + pdate + "' and CommodityId = '" + ddlcommodtiy.SelectedValue + "')and GodownName is not null";

                SqlCommand cmd = new SqlCommand(qry, con);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.CommandTimeout = 0;

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();

                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlgodown.DataSource = ds.Tables[0];

                    ddlgodown.DataTextField = "GodownName";
                    ddlgodown.DataValueField = "Recd_Godown";

                    ddlgodown.DataBind();

                    ddlgodown.Items.Insert(0, "--Select--");

                    lblmsg.Text = "";
                }

                else
                {
                    lblmsg.Text = "स्वीकृति पत्रक नहीं बनाये गए है अथवा स्वीकृति पत्रक का डीपोसिट फ्रॉम जारी किया जा चूका है , अतः गोदाम के नाम उपलब्ध नहीं है";

                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('स्वीकृति पत्रक नहीं बनाये गए है अथवा स्वीकृति पत्रक का डीपोसिट फ्रॉम जारी किया जा चूका है , अतः गोदाम के नाम उपलब्ध नहीं है'); </script> ");
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            else
            {
                string qry = "SELECT distinct Recd_Godown ,GodownName FROM SCSC_Procurement where Acceptance_Date = '" + pdate + "' and AN_Status = 'Y' and Distt_ID = '" + distid + "' and IssueCenter_ID = '" + issueid + "' and Commodity_Id = '" + ddlcommodtiy.SelectedValue + "' and SCSC_Procurement.Receipt_Id in (select IssueID from Acceptance_Note_Detail where WHR_Request is null and Distt_ID = '" + distid + "' and IssueCenter_ID = '" + issueid + "' and Acceptance_Date = '" + pdate + "' and CommodityId = '" + ddlcommodtiy.SelectedValue + "')and GodownName is not null";

                SqlCommand cmd = new SqlCommand(qry, con);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.CommandTimeout = 0;

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();

                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlgodown.DataSource = ds.Tables[0];

                    ddlgodown.DataTextField = "GodownName";
                    ddlgodown.DataValueField = "Recd_Godown";

                    ddlgodown.DataBind();

                    ddlgodown.Items.Insert(0, "--Select--");

                    lblmsg.Text = "";
                }

                else
                {
                    lblmsg.Text = "स्वीकृति पत्रक नहीं बनाये गए है अथवा स्वीकृति पत्रक का डीपोसिट फ्रॉम जारी किया जा चूका है , अतः गोदाम के नाम उपलब्ध नहीं है";

                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('स्वीकृति पत्रक नहीं बनाये गए है अथवा स्वीकृति पत्रक का डीपोसिट फ्रॉम जारी किया जा चूका है , अतः गोदाम के नाम उपलब्ध नहीं है'); </script> ");
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        btnsave.Enabled = false;

        if (ddlgodown.SelectedValue == "0")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('गोदाम चुने'); </script> ");
            return;
        }

        if (DaintyDate3.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('दिनांक चुने'); </script> ");
            return;
        }

        gdn = ddlgodown.SelectedValue;

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        SqlCommand cmdacno = new SqlCommand();

        cmdacno.Parameters.Clear();
        cmdacno.Parameters.AddWithValue("@District_ID", distid);
        cmdacno.Parameters.AddWithValue("@IssueCenter_ID", issueid);
        cmdacno.Connection = con;


        if (ddlcommodtiy.SelectedValue == "22")
        {
            # region WPMS

            cmdacno.CommandTimeout = 0;

            cmdacno.CommandType = CommandType.StoredProcedure;
            cmdacno.CommandText = "prc_getMaxdepositNo";

            Accpt_NO = Convert.ToString(cmdacno.ExecuteScalar());

            string whr_ID = Accpt_NO;

            foreach (GridViewRow gr in GridView2.Rows)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                if (con_WPMS.State == ConnectionState.Closed)
                {
                    con_WPMS.Open();
                }

                string acceptdate = getDate_MDY(DaintyDate3.Text);

                string commname = ddlcommodtiy.SelectedValue;

                string godown = ddlgodown.SelectedValue;

                string socid = gr.Cells[0].Text;


                Label lbltc = (Label)gr.FindControl("lbltc");

                string TCnum = lbltc.Text;

                Label txtrcq = (Label)gr.FindControl("lblacqt");

                double accepqty = Convert.ToDouble(txtrcq.Text);

                Label txtrcB = (Label)gr.FindControl("lblacbag");

                Int32 accepbags = Convert.ToInt32(txtrcB.Text);

                string ReceiptId = gr.Cells[5].Text;


                //genwhr

                // update acceptance tbl of csms and wpms with transaction in case failed

                SqlTransaction trns1;

                SqlTransaction trns;


                cmd_wpm.Connection = con_WPMS;
                trns1 = con_WPMS.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                cmd_wpm.Transaction = trns1;

                string updatewpms = "Update Acceptance_Note_Detail set WHR_Request = '" + whr_ID + "' , Updates_Date =  getdate() where Distt_ID = '23" + distid + "' and IssueCenter_ID = '" + issueid + "' and Purchase_Center = '" + socid + "' and TC_Number = '" + TCnum + "' and godown = '" + godown + "' and IssueID = '" + ReceiptId + "'";
                cmd_wpm.CommandText = updatewpms;

                cmd_con.Connection = con;
                trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                cmd_con.Transaction = trns;

                string updatecsms = "Update Acceptance_Note_Detail set WHR_Request = '" + whr_ID + "' , Updates_Date =  getdate() where Distt_ID = '" + distid + "' and IssueCenter_ID = '" + issueid + "' and Purchase_Center = '" + socid + "' and TC_Number = '" + TCnum + "' and godown = '" + godown + "' and IssueID = '" + ReceiptId + "'";
                cmd_con.CommandText = updatecsms;


                int y = cmd_wpm.ExecuteNonQuery();

                if (y > 0)
                {
                    int x = cmd_con.ExecuteNonQuery();

                    if (x > 0)
                    {
                        try
                        {
                            trns1.Commit();

                            if (con_WPMS.State == ConnectionState.Open)
                            {
                                con_WPMS.Close();
                            }

                            trns.Commit();

                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();
                            }

                            btnsave.Enabled = false;

                            whrreq.Text = "WHR Request Number is '" + whr_ID + "' ";

                            Session["WHR_Request"] = whr_ID;

                            Session["Commodity"] = ddlcommodtiy.SelectedValue;

                            HyperLink1.Visible = true;

                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved WHR Request Number is , " + whr_ID + "'); </script> ");

                        }

                        catch (Exception ex)
                        {
                            btnsave.Enabled = true;

                            trns1.Rollback();

                            trns.Rollback();

                            Label6.Visible = true;
                            Label6.Text = "error:6" + ex.Message;
                        }

                        finally
                        {
                            if (con_WPMS.State == ConnectionState.Open)
                            {
                                con_WPMS.Close();
                            }


                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();

                            }
                        }

                    }

                    else
                    {
                        btnsave.Enabled = true;

                        trns1.Rollback();

                        trns.Rollback();

                        if (con_WPMS.State == ConnectionState.Open)
                        {
                            con_WPMS.Close();
                        }



                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();

                        }


                    }
                }

                else
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                    if (con_WPMS.State == ConnectionState.Open)
                    {
                        con_WPMS.Close();
                    }

                    btnsave.Enabled = true;

                    trns1.Rollback();

                    trns.Rollback();
                }


            }


            # endregion
        }

        else if (ddlcommodtiy.SelectedValue == "13" || ddlcommodtiy.SelectedValue == "14")
        {
            # region Paddy

            cmdacno.CommandTimeout = 0;

            cmdacno.CommandType = CommandType.StoredProcedure;
            cmdacno.CommandText = "prc_getMaxdepositNo";

            Accpt_NO = Convert.ToString(cmdacno.ExecuteScalar());

            string whr_ID = Accpt_NO;

            foreach (GridViewRow gr in GridView2.Rows)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                if (con_paddy.State == ConnectionState.Closed)
                {
                    con_paddy.Open();
                }

                string acceptdate = getDate_MDY(DaintyDate3.Text);

                string commname = ddlcommodtiy.SelectedValue;

                string godown = ddlgodown.SelectedValue;

                string socid = gr.Cells[0].Text;


                Label lbltc = (Label)gr.FindControl("lbltc");

                string TCnum = lbltc.Text;

                Label txtrcq = (Label)gr.FindControl("lblacqt");

                double accepqty = Convert.ToDouble(txtrcq.Text);

                Label txtrcB = (Label)gr.FindControl("lblacbag");

                Int32 accepbags = Convert.ToInt32(txtrcB.Text);

                string ReceiptId = gr.Cells[5].Text;


                //genwhr

                // update acceptance tbl of csms and wpms with transaction in case failed

                SqlTransaction trns1;

                SqlTransaction trns;


                cmd_ppm.Connection = con_paddy;
                trns1 = con_paddy.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                cmd_ppm.Transaction = trns1;

                string updateppms = "Update Acceptance_Note_Detail set WHR_Request = '" + whr_ID + "' , Updates_Date =  getdate() where Distt_ID = '23" + distid + "' and IssueCenter_ID = '" + issueid + "' and Purchase_Center = '" + socid + "' and TC_Number = '" + TCnum + "' and godown = '" + godown + "' and IssueID = '" + ReceiptId + "'";
                cmd_ppm.CommandText = updateppms;

                cmd_con.Connection = con;
                trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                cmd_con.Transaction = trns;

                string updatecsms = "Update Acceptance_Note_Detail set WHR_Request = '" + whr_ID + "' , Updates_Date =  getdate() where Distt_ID = '" + distid + "' and IssueCenter_ID = '" + issueid + "' and Purchase_Center = '" + socid + "' and TC_Number = '" + TCnum + "' and godown = '" + godown + "' and IssueID = '" + ReceiptId + "'";
                cmd_con.CommandText = updatecsms;


                int y = cmd_ppm.ExecuteNonQuery();

                if (y > 0)
                {
                    int x = cmd_con.ExecuteNonQuery();

                    if (x > 0)
                    {
                        try
                        {
                            trns1.Commit();

                            if (con_paddy.State == ConnectionState.Open)
                            {
                                con_paddy.Close();
                            }

                            trns.Commit();

                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();
                            }

                            btnsave.Enabled = false;

                            whrreq.Text = "WHR Request Number is '" + whr_ID + "' ";

                            Session["WHR_Request"] = whr_ID;

                            Session["Commodity"] = ddlcommodtiy.SelectedValue;

                            HyperLink1.Visible = true;

                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved WHR Request Number is , " + whr_ID + "'); </script> ");

                        }

                        catch (Exception ex)
                        {
                            btnsave.Enabled = true;

                            trns1.Rollback();

                            trns.Rollback();

                            Label6.Visible = true;
                            Label6.Text = "error:6" + ex.Message;
                        }

                        finally
                        {
                            if (con_paddy.State == ConnectionState.Open)
                            {
                                con_paddy.Close();
                            }


                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();

                            }
                        }

                    }

                    else
                    {
                        btnsave.Enabled = true;

                        trns1.Rollback();

                        trns.Rollback();

                        if (con_paddy.State == ConnectionState.Open)
                        {
                            con_paddy.Close();
                        }

                        trns.Rollback();

                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();

                        }


                    }
                }

                else
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                    if (con_paddy.State == ConnectionState.Open)
                    {
                        con_paddy.Close();
                    }

                    btnsave.Enabled = true;

                    trns1.Rollback();

                    trns.Rollback();
                }


            }

            # endregion
        }

        else if (ddlcommodtiy.SelectedValue == "8")
        {
            # region Maize_8

            cmdacno.CommandTimeout = 0;

            cmdacno.CommandType = CommandType.StoredProcedure;
            cmdacno.CommandText = "prc_getMaxdepositNo";

            Accpt_NO = Convert.ToString(cmdacno.ExecuteScalar());

            string whr_ID = Accpt_NO;

            foreach (GridViewRow gr in GridView2.Rows)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                if (con_Maze.State == ConnectionState.Closed)
                {
                    con_Maze.Open();
                }

                string acceptdate = getDate_MDY(DaintyDate3.Text);

                string commname = ddlcommodtiy.SelectedValue;

                string godown = ddlgodown.SelectedValue;

                string socid = gr.Cells[0].Text;


                Label lbltc = (Label)gr.FindControl("lbltc");

                string TCnum = lbltc.Text;

                Label txtrcq = (Label)gr.FindControl("lblacqt");

                double accepqty = Convert.ToDouble(txtrcq.Text);

                Label txtrcB = (Label)gr.FindControl("lblacbag");

                Int32 accepbags = Convert.ToInt32(txtrcB.Text);

                string ReceiptId = gr.Cells[5].Text;


                //genwhr

                // update acceptance tbl of csms and wpms with transaction in case failed

                SqlTransaction trns1;

                SqlTransaction trns;


                cmd_mpm.Connection = con_Maze;
                trns1 = con_Maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                cmd_mpm.Transaction = trns1;

                string updatempms = "Update Acceptance_Note_Detail set WHR_Request = '" + whr_ID + "' , Updates_Date =  getdate() where Distt_ID = '23" + distid + "' and IssueCenter_ID = '" + issueid + "' and Purchase_Center = '" + socid + "' and TC_Number = '" + TCnum + "' and godown = '" + godown + "' and IssueID = '" + ReceiptId + "'";
                cmd_mpm.CommandText = updatempms;

                cmd_con.Connection = con;
                trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                cmd_con.Transaction = trns;

                string updatecsms = "Update Acceptance_Note_Detail set WHR_Request = '" + whr_ID + "' , Updates_Date =  getdate() where Distt_ID = '" + distid + "' and IssueCenter_ID = '" + issueid + "' and Purchase_Center = '" + socid + "' and TC_Number = '" + TCnum + "' and godown = '" + godown + "' and IssueID = '" + ReceiptId + "'";
                cmd_con.CommandText = updatecsms;


                int y = cmd_mpm.ExecuteNonQuery();

                if (y > 0)
                {
                    int x = cmd_con.ExecuteNonQuery();

                    if (x > 0)
                    {
                        try
                        {
                            trns1.Commit();

                            if (con_Maze.State == ConnectionState.Open)
                            {
                                con_Maze.Close();
                            }

                            trns.Commit();

                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();
                            }

                            btnsave.Enabled = false;

                            whrreq.Text = "WHR Request Number is '" + whr_ID + "' ";

                            Session["WHR_Request"] = whr_ID;

                            Session["Commodity"] = ddlcommodtiy.SelectedValue;

                            HyperLink1.Visible = true;

                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved WHR Request Number is , " + whr_ID + "'); </script> ");

                        }

                        catch (Exception ex)
                        {
                            btnsave.Enabled = true;

                            trns1.Rollback();

                            trns.Rollback();

                            Label6.Visible = true;
                            Label6.Text = "error:6" + ex.Message;
                        }

                        finally
                        {
                            if (con_Maze.State == ConnectionState.Open)
                            {
                                con_Maze.Close();
                            }


                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();

                            }
                        }

                    }

                    else
                    {
                        btnsave.Enabled = true;

                        trns1.Rollback();

                        trns.Rollback();

                        if (con_Maze.State == ConnectionState.Open)
                        {
                            con_Maze.Close();
                        }

                        trns.Rollback();

                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();

                        }


                    }
                }

                else
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                    if (con_Maze.State == ConnectionState.Open)
                    {
                        con_Maze.Close();
                    }

                    btnsave.Enabled = true;

                    trns1.Rollback();

                    trns.Rollback();
                }


            }

            # endregion
        }

        else if (ddlcommodtiy.SelectedValue == "11")
        {
            # region Maize_11

            cmdacno.CommandTimeout = 0;

            cmdacno.CommandType = CommandType.StoredProcedure;
            cmdacno.CommandText = "prc_getMaxdepositNo";

            Accpt_NO = Convert.ToString(cmdacno.ExecuteScalar());

            string whr_ID = Accpt_NO;

            foreach (GridViewRow gr in GridView2.Rows)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                if (con_Maze.State == ConnectionState.Closed)
                {
                    con_Maze.Open();
                }

                string acceptdate = getDate_MDY(DaintyDate3.Text);

                string commname = ddlcommodtiy.SelectedValue;

                string godown = ddlgodown.SelectedValue;

                string socid = gr.Cells[0].Text;


                Label lbltc = (Label)gr.FindControl("lbltc");

                string TCnum = lbltc.Text;

                Label txtrcq = (Label)gr.FindControl("lblacqt");

                double accepqty = Convert.ToDouble(txtrcq.Text);

                Label txtrcB = (Label)gr.FindControl("lblacbag");

                Int32 accepbags = Convert.ToInt32(txtrcB.Text);

                string ReceiptId = gr.Cells[5].Text;


                //genwhr

                // update acceptance tbl of csms and wpms with transaction in case failed

                SqlTransaction trns1;

                SqlTransaction trns;


                cmd_mpm.Connection = con_Maze;
                trns1 = con_Maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                cmd_mpm.Transaction = trns1;

                string updatempms = "Update Acceptance_Note_Detail set WHR_Request = '" + whr_ID + "' , Updates_Date =  getdate() where Distt_ID = '23" + distid + "' and IssueCenter_ID = '" + issueid + "' and Purchase_Center = '" + socid + "' and TC_Number = '" + TCnum + "' and godown = '" + godown + "' and IssueID = '" + ReceiptId + "'";
                cmd_mpm.CommandText = updatempms;

                cmd_con.Connection = con;
                trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                cmd_con.Transaction = trns;

                string updatecsms = "Update Acceptance_Note_Detail set WHR_Request = '" + whr_ID + "' , Updates_Date =  getdate() where Distt_ID = '" + distid + "' and IssueCenter_ID = '" + issueid + "' and Purchase_Center = '" + socid + "' and TC_Number = '" + TCnum + "' and godown = '" + godown + "' and IssueID = '" + ReceiptId + "'";
                cmd_con.CommandText = updatecsms;


                int y = cmd_mpm.ExecuteNonQuery();

                if (y > 0)
                {
                    int x = cmd_con.ExecuteNonQuery();

                    if (x > 0)
                    {
                        try
                        {
                            trns1.Commit();

                            if (con_Maze.State == ConnectionState.Open)
                            {
                                con_Maze.Close();
                            }

                            trns.Commit();

                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();
                            }

                            btnsave.Enabled = false;

                            whrreq.Text = "WHR Request Number is '" + whr_ID + "' ";

                            Session["WHR_Request"] = whr_ID;

                            Session["Commodity"] = ddlcommodtiy.SelectedValue;

                            HyperLink1.Visible = true;

                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved WHR Request Number is , " + whr_ID + "'); </script> ");

                        }

                        catch (Exception ex)
                        {
                            btnsave.Enabled = true;

                            trns1.Rollback();

                            trns.Rollback();

                            Label6.Visible = true;
                            Label6.Text = "error:6" + ex.Message;
                        }

                        finally
                        {
                            if (con_Maze.State == ConnectionState.Open)
                            {
                                con_Maze.Close();
                            }


                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();

                            }
                        }

                    }

                    else
                    {
                        btnsave.Enabled = true;

                        trns1.Rollback();

                        trns.Rollback();

                        if (con_Maze.State == ConnectionState.Open)
                        {
                            con_Maze.Close();
                        }

                        trns.Rollback();

                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();

                        }


                    }
                }

                else
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                    if (con_Maze.State == ConnectionState.Open)
                    {
                        con_Maze.Close();
                    }

                    btnsave.Enabled = true;

                    trns1.Rollback();

                    trns.Rollback();
                }


            }

            # endregion
        }

        else if (ddlcommodtiy.SelectedValue == "12")
        {
            # region Maize_12

            cmdacno.CommandTimeout = 0;

            cmdacno.CommandType = CommandType.StoredProcedure;
            cmdacno.CommandText = "prc_getMaxdepositNo";

            Accpt_NO = Convert.ToString(cmdacno.ExecuteScalar());

            string whr_ID = Accpt_NO;

            foreach (GridViewRow gr in GridView2.Rows)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                if (con_Maze.State == ConnectionState.Closed)
                {
                    con_Maze.Open();
                }

                string acceptdate = getDate_MDY(DaintyDate3.Text);

                string commname = ddlcommodtiy.SelectedValue;

                string godown = ddlgodown.SelectedValue;

                string socid = gr.Cells[0].Text;


                Label lbltc = (Label)gr.FindControl("lbltc");

                string TCnum = lbltc.Text;

                Label txtrcq = (Label)gr.FindControl("lblacqt");

                double accepqty = Convert.ToDouble(txtrcq.Text);

                Label txtrcB = (Label)gr.FindControl("lblacbag");

                Int32 accepbags = Convert.ToInt32(txtrcB.Text);

                string ReceiptId = gr.Cells[5].Text;


                //genwhr

                // update acceptance tbl of csms and wpms with transaction in case failed

                SqlTransaction trns1;

                SqlTransaction trns;


                cmd_mpm.Connection = con_Maze;
                trns1 = con_Maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                cmd_mpm.Transaction = trns1;

                string updatempms = "Update Acceptance_Note_Detail set WHR_Request = '" + whr_ID + "' , Updates_Date =  getdate() where Distt_ID = '23" + distid + "' and IssueCenter_ID = '" + issueid + "' and Purchase_Center = '" + socid + "' and TC_Number = '" + TCnum + "' and godown = '" + godown + "' and IssueID = '" + ReceiptId + "'";
                cmd_mpm.CommandText = updatempms;

                cmd_con.Connection = con;
                trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                cmd_con.Transaction = trns;

                string updatecsms = "Update Acceptance_Note_Detail set WHR_Request = '" + whr_ID + "' , Updates_Date =  getdate() where Distt_ID = '" + distid + "' and IssueCenter_ID = '" + issueid + "' and Purchase_Center = '" + socid + "' and TC_Number = '" + TCnum + "' and godown = '" + godown + "' and IssueID = '" + ReceiptId + "'";
                cmd_con.CommandText = updatecsms;


                int y = cmd_mpm.ExecuteNonQuery();

                if (y > 0)
                {
                    int x = cmd_con.ExecuteNonQuery();

                    if (x > 0)
                    {
                        try
                        {
                            trns1.Commit();

                            if (con_Maze.State == ConnectionState.Open)
                            {
                                con_Maze.Close();
                            }

                            trns.Commit();

                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();
                            }

                            btnsave.Enabled = false;

                            whrreq.Text = "WHR Request Number is '" + whr_ID + "' ";

                            Session["WHR_Request"] = whr_ID;

                            Session["Commodity"] = ddlcommodtiy.SelectedValue;

                            HyperLink1.Visible = true;

                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved WHR Request Number is , " + whr_ID + "'); </script> ");

                        }

                        catch (Exception ex)
                        {
                            btnsave.Enabled = true;

                            trns1.Rollback();

                            trns.Rollback();

                            Label6.Visible = true;
                            Label6.Text = "error:6" + ex.Message;
                        }

                        finally
                        {
                            if (con_Maze.State == ConnectionState.Open)
                            {
                                con_Maze.Close();
                            }


                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();

                            }
                        }

                    }

                    else
                    {
                        btnsave.Enabled = true;

                        trns1.Rollback();

                        trns.Rollback();

                        if (con_Maze.State == ConnectionState.Open)
                        {
                            con_Maze.Close();
                        }

                        trns.Rollback();

                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();

                        }


                    }
                }

                else
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                    if (con_Maze.State == ConnectionState.Open)
                    {
                        con_Maze.Close();
                    }

                    btnsave.Enabled = true;

                    trns1.Rollback();

                    trns.Rollback();
                }


            }

            # endregion
        }


        else if (ddlcommodtiy.SelectedValue == "40")
        {
            # region Maize_40

            cmdacno.CommandTimeout = 0;

            cmdacno.CommandType = CommandType.StoredProcedure;
            cmdacno.CommandText = "prc_getMaxdepositNo";

            Accpt_NO = Convert.ToString(cmdacno.ExecuteScalar());

            string whr_ID = Accpt_NO;

            foreach (GridViewRow gr in GridView2.Rows)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                if (con_Maze.State == ConnectionState.Closed)
                {
                    con_Maze.Open();
                }

                string acceptdate = getDate_MDY(DaintyDate3.Text);

                string commname = ddlcommodtiy.SelectedValue;

                string godown = ddlgodown.SelectedValue;

                string socid = gr.Cells[0].Text;


                Label lbltc = (Label)gr.FindControl("lbltc");

                string TCnum = lbltc.Text;

                Label txtrcq = (Label)gr.FindControl("lblacqt");

                double accepqty = Convert.ToDouble(txtrcq.Text);

                Label txtrcB = (Label)gr.FindControl("lblacbag");

                Int32 accepbags = Convert.ToInt32(txtrcB.Text);

                string ReceiptId = gr.Cells[5].Text;


                //genwhr

                // update acceptance tbl of csms and wpms with transaction in case failed

                SqlTransaction trns1;

                SqlTransaction trns;


                cmd_mpm.Connection = con_Maze;
                trns1 = con_Maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                cmd_mpm.Transaction = trns1;

                string updatempms = "Update Acceptance_Note_Detail set WHR_Request = '" + whr_ID + "' , Updates_Date =  getdate() where Distt_ID = '23" + distid + "' and IssueCenter_ID = '" + issueid + "' and Purchase_Center = '" + socid + "' and TC_Number = '" + TCnum + "' and godown = '" + godown + "' and IssueID = '" + ReceiptId + "'";
                cmd_mpm.CommandText = updatempms;

                cmd_con.Connection = con;
                trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                cmd_con.Transaction = trns;

                string updatecsms = "Update Acceptance_Note_Detail set WHR_Request = '" + whr_ID + "' , Updates_Date =  getdate() where Distt_ID = '" + distid + "' and IssueCenter_ID = '" + issueid + "' and Purchase_Center = '" + socid + "' and TC_Number = '" + TCnum + "' and godown = '" + godown + "' and IssueID = '" + ReceiptId + "'";
                cmd_con.CommandText = updatecsms;


                int y = cmd_mpm.ExecuteNonQuery();

                if (y > 0)
                {
                    int x = cmd_con.ExecuteNonQuery();

                    if (x > 0)
                    {
                        try
                        {
                            trns1.Commit();

                            if (con_Maze.State == ConnectionState.Open)
                            {
                                con_Maze.Close();
                            }

                            trns.Commit();

                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();
                            }

                            btnsave.Enabled = false;

                            whrreq.Text = "WHR Request Number is '" + whr_ID + "' ";

                            Session["WHR_Request"] = whr_ID;

                            Session["Commodity"] = ddlcommodtiy.SelectedValue;

                            HyperLink1.Visible = true;

                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved WHR Request Number is , " + whr_ID + "'); </script> ");

                        }

                        catch (Exception ex)
                        {
                            btnsave.Enabled = true;

                            trns1.Rollback();

                            trns.Rollback();

                            Label6.Visible = true;
                            Label6.Text = "error:6" + ex.Message;
                        }

                        finally
                        {
                            if (con_Maze.State == ConnectionState.Open)
                            {
                                con_Maze.Close();
                            }


                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();

                            }
                        }

                    }

                    else
                    {
                        btnsave.Enabled = true;

                        trns1.Rollback();

                        trns.Rollback();

                        if (con_Maze.State == ConnectionState.Open)
                        {
                            con_Maze.Close();
                        }

                        trns.Rollback();

                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();

                        }


                    }
                }

                else
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                    if (con_Maze.State == ConnectionState.Open)
                    {
                        con_Maze.Close();
                    }

                    btnsave.Enabled = true;

                    trns1.Rollback();

                    trns.Rollback();
                }


            }

            # endregion
        }

    }

    void bindgrid()
    {

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        string pdate = getDate_MDY(DaintyDate3.Text);

        gdn = ddlgodown.SelectedValue;


        if (ddlcommodtiy.SelectedValue == "22")
        {
            string getdata = "SELECT Society.Society_Id , Society.Society_Name + ' ( '+ Society.SocPlace + ' )' as Society ,Acceptance_Note_Detail.TC_Number , Acceptance_Note_Detail.Accept_Qty , Acceptance_Note_Detail.Bags , Acceptance_Note_Detail.IssueID  FROM Acceptance_Note_Detail inner join Society on Society.Society_Id = Acceptance_Note_Detail.Purchase_Center  where godown = '" + gdn + "' and Acceptance_Date = '" + pdate + "' and Acceptance_Note_Detail.IssueCenter_ID = '" + issueid + "'  and Acceptance_Note_Detail.WHR_Request is null";

            if (con_WPMS.State == ConnectionState.Closed)
            {
                con_WPMS.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(getdata, con_WPMS);
            DataSet dschdt = new DataSet();
            da.Fill(dschdt);

            if (dschdt.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = dschdt.Tables[0];

                GridView2.DataBind();
            }

            else
            {
                GridView2.DataSource = "";

                GridView2.DataBind();
            }

            if (con_WPMS.State == ConnectionState.Open)
            {
                con_WPMS.Close();
            }

        }

        else if (ddlcommodtiy.SelectedValue == "13" || ddlcommodtiy.SelectedValue == "14")
        {
            string getdata = "SELECT Society.Society_Id , Society.Society_Name + ' ( '+ Society.SocPlace + ' )' as Society ,Acceptance_Note_Detail.TC_Number , Acceptance_Note_Detail.Accept_Qty , Acceptance_Note_Detail.Bags , Acceptance_Note_Detail.IssueID  FROM Acceptance_Note_Detail inner join Society on Society.Society_Id = Acceptance_Note_Detail.Purchase_Center  where godown = '" + gdn + "' and Acceptance_Date = '" + pdate + "' and Acceptance_Note_Detail.IssueCenter_ID = '" + issueid + "'  and Acceptance_Note_Detail.WHR_Request is null";

            if (con_paddy.State == ConnectionState.Closed)
            {
                con_paddy.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(getdata, con_paddy);
            DataSet dschdt = new DataSet();
            da.Fill(dschdt);

            if (dschdt.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = dschdt.Tables[0];

                GridView2.DataBind();
            }

            else
            {
                GridView2.DataSource = "";

                GridView2.DataBind();
            }

            if (con_paddy.State == ConnectionState.Open)
            {
                con_paddy.Close();
            }

        }

        else if (ddlcommodtiy.SelectedValue == "8")
        {
            string getdata = "SELECT Society.Society_Id , Society.Society_Name + ' ( '+ Society.SocPlace + ' )' as Society ,Acceptance_Note_Detail.TC_Number , Acceptance_Note_Detail.Accept_Qty , Acceptance_Note_Detail.Bags , Acceptance_Note_Detail.IssueID  FROM Acceptance_Note_Detail inner join Society on Society.Society_Id = Acceptance_Note_Detail.Purchase_Center  where godown = '" + gdn + "' and Acceptance_Date = '" + pdate + "' and Acceptance_Note_Detail.IssueCenter_ID = '" + issueid + "'  and Acceptance_Note_Detail.WHR_Request is null";

            if (con_Maze.State == ConnectionState.Closed)
            {
                con_Maze.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(getdata, con_Maze);
            DataSet dschdt = new DataSet();
            da.Fill(dschdt);

            if (dschdt.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = dschdt.Tables[0];

                GridView2.DataBind();
            }

            else
            {
                GridView2.DataSource = "";

                GridView2.DataBind();
            }

            if (con_Maze.State == ConnectionState.Open)
            {
                con_Maze.Close();
            }

        }

        else if (ddlcommodtiy.SelectedValue == "11")
        {
            string getdata = "SELECT Society.Society_Id , Society.Society_Name + ' ( '+ Society.SocPlace + ' )' as Society ,Acceptance_Note_Detail.TC_Number , Acceptance_Note_Detail.Accept_Qty , Acceptance_Note_Detail.Bags , Acceptance_Note_Detail.IssueID  FROM Acceptance_Note_Detail inner join Society on Society.Society_Id = Acceptance_Note_Detail.Purchase_Center  where godown = '" + gdn + "' and Acceptance_Date = '" + pdate + "' and Acceptance_Note_Detail.IssueCenter_ID = '" + issueid + "'  and Acceptance_Note_Detail.WHR_Request is null";

            if (con_Maze.State == ConnectionState.Closed)
            {
                con_Maze.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(getdata, con_Maze);
            DataSet dschdt = new DataSet();
            da.Fill(dschdt);

            if (dschdt.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = dschdt.Tables[0];

                GridView2.DataBind();
            }

            else
            {
                GridView2.DataSource = "";

                GridView2.DataBind();
            }

            if (con_Maze.State == ConnectionState.Open)
            {
                con_Maze.Close();
            }
        }

        else if (ddlcommodtiy.SelectedValue == "12")
        {
            string getdata = "SELECT Society.Society_Id , Society.Society_Name + ' ( '+ Society.SocPlace + ' )' as Society ,Acceptance_Note_Detail.TC_Number , Acceptance_Note_Detail.Accept_Qty , Acceptance_Note_Detail.Bags , Acceptance_Note_Detail.IssueID  FROM Acceptance_Note_Detail inner join Society on Society.Society_Id = Acceptance_Note_Detail.Purchase_Center  where godown = '" + gdn + "' and Acceptance_Date = '" + pdate + "' and Acceptance_Note_Detail.IssueCenter_ID = '" + issueid + "'  and Acceptance_Note_Detail.WHR_Request is null";

            if (con_Maze.State == ConnectionState.Closed)
            {
                con_Maze.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(getdata, con_Maze);
            DataSet dschdt = new DataSet();
            da.Fill(dschdt);

            if (dschdt.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = dschdt.Tables[0];

                GridView2.DataBind();
            }

            else
            {
                GridView2.DataSource = "";

                GridView2.DataBind();
            }

            if (con_Maze.State == ConnectionState.Open)
            {
                con_Maze.Close();
            }
        }

        else if (ddlcommodtiy.SelectedValue == "40")
        {
            string getdata = "SELECT Society.Society_Id , Society.Society_Name + ' ( '+ Society.SocPlace + ' )' as Society ,Acceptance_Note_Detail.TC_Number , Acceptance_Note_Detail.Accept_Qty , Acceptance_Note_Detail.Bags , Acceptance_Note_Detail.IssueID  FROM Acceptance_Note_Detail inner join Society on Society.Society_Id = Acceptance_Note_Detail.Purchase_Center  where godown = '" + gdn + "' and Acceptance_Date = '" + pdate + "' and Acceptance_Note_Detail.IssueCenter_ID = '" + issueid + "'  and Acceptance_Note_Detail.WHR_Request is null";

            if (con_Maze.State == ConnectionState.Closed)
            {
                con_Maze.Open();
            }


            SqlDataAdapter da = new SqlDataAdapter(getdata, con_Maze);
            DataSet dschdt = new DataSet();
            da.Fill(dschdt);

            if (dschdt.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = dschdt.Tables[0];

                GridView2.DataBind();
            }

            else
            {
                GridView2.DataSource = "";

                GridView2.DataBind();
            }

            if (con_Maze.State == ConnectionState.Open)
            {
                con_Maze.Close();
            }
        }


        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

        if (con_WPMS.State == ConnectionState.Open)
        {
            con_WPMS.Close();
        }

        if (con_paddy.State == ConnectionState.Open)
        {
            con_paddy.Close();
        }

        if (con_Maze.State == ConnectionState.Open)
        {
            con_Maze.Close();
        }

        Response.Redirect("~/IssueCenter/DepositerForm_Procurement2015.aspx");

        //btnsave.Enabled = true;

        //bindgrid();
    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

        if (con_WPMS.State == ConnectionState.Open)
        {
            con_WPMS.Close();
        }

        if (con_paddy.State == ConnectionState.Open)
        {
            con_paddy.Close();
        }

        if (con_Maze.State == ConnectionState.Open)
        {
            con_Maze.Close();
        }

        Response.Redirect("~/IssueCenter/issue_welcome.aspx");


    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            aceqty += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Accept_Qty"));

            accbags += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Bags"));

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblqty1 = (Label)e.Row.FindControl("lbl_acqt");

            lblqty1.Text = aceqty.ToString();

            Label lblricebag1 = (Label)e.Row.FindControl("lbl_acbag");

            lblricebag1.Text = accbags.ToString();


        }
    }

    protected void btngodown_Click(object sender, EventArgs e)
    {
        if (DaintyDate3.Text == "")
        {

            ddlgodown.DataSource = "";

            ddlgodown.DataBind();

            ddlgodown.Items.Insert(0, "--Select--");

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('स्वीकृति पत्रक दिनांक चुने'); </script> ");

        }
        else
        {
            GetGodown();
        }
    }

    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DaintyDate3.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('स्वीकृति दिनांक चुनिए |'); </script> ");
            return;
        }

        if (ddlgodown.SelectedValue == "0")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('गोदाम का नाम चुने |'); </script> ");
            return;
        }

        bindgrid();
    }
}
