using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class IssueCenter_DepositerForm_ProcPaddy2016 : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd, cmd1;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;

    string IC_Id = "", Dist_Id = "";
    decimal aceqty = 0;
    Int32 accbags = 0;

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
                Session["WHR_Request"] = "";
                Session["Commodity"] = "";

                txtissue.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtissue.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtissue.Attributes.Add("onchange", "return chksqltxt(this)");

                txtYear.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtYear.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtYear.Attributes.Add("onchange", "return chksqltxt(this)");

                DaintyDate3.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                DaintyDate3.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                DaintyDate3.Attributes.Add("onchange", "return chksqltxt(this)");

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

        //DaintyDate3.Text = Request.Form[DaintyDate3.UniqueID];
    }

    private void GetICName()
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();
                string select = "select DepotName,(CONVERT(VARCHAR,GETDATE(), 105)) As AcptDate from dbo.tbl_MetaData_DEPOT where DepotID='" + IC_Id + "'";
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

                    //ddlcommodtiy.SelectedIndex = 5;
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
        ddlgodown.Items.Clear();
        hdfGodownCode.Value = "";

        GridView2.DataSource = null;
        GridView2.DataBind();

        if (ddlcommodtiy.SelectedIndex > 0)
        {
            if (DaintyDate3.Text == "")
            {
                ddlcommodtiy.SelectedIndex = 0;
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Date of Acceptance'); </script> ");
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
                string pdate = getDate_MDY(DaintyDate3.Text);
                IC_Id = Session["issue_id"].ToString();
                Dist_Id = Session["dist_id"].ToString();

                string select = "";
                select = "SELECT distinct Recd_Godown FROM SCSC_Procurement_Kharif2016 where Acceptance_Date = '" + pdate + "' and AN_Status = 'Y' and Distt_ID = '" + Dist_Id + "' and IssueCenter_ID = '" + IC_Id + "' and Commodity_Id = '" + ddlcommodtiy.SelectedValue.ToString() + "' and Book_No!='Rejected' and  SCSC_Procurement_Kharif2016.Receipt_Id in (select IssueID from Acceptance_Note_Kharif2016 where WHR_Request is null and Distt_ID = '" + Dist_Id + "' and IssueCenter_ID = '" + IC_Id + "' and Acceptance_Date = '" + pdate + "' and CommodityId = '" + ddlcommodtiy.SelectedValue.ToString() + "')";
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
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Acceptance Note नहीं बनाये गए है अथवा Acceptance Note का Depositer Form जारी किया जा चूका है, अतः गोदाम के नाम उपलब्ध नहीं है|'); </script> ");
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
        GridView2.DataSource = null;
        GridView2.DataBind();

        btnRecptSubmit.Enabled = false;

        if (ddlgodown.SelectedIndex > 0)
        {
            if (DaintyDate3.Text == "")
            {
                ddlgodown.SelectedIndex = 0;
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Date of Acceptance'); </script> ");
                return;
            }
            else
            {
                if (ddlcommodtiy.SelectedValue.ToString() == "13" || ddlcommodtiy.SelectedValue.ToString() == "14")
                {
                    bindgrid();
                }
                else
                {
                    bindgridMotaAnaaj();
                }
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown Name'); </script> ");
            return;
        }
    }

    private void bindgrid()
    {
        GridView2.DataSource = null;
        GridView2.DataBind();

        using (con = new SqlConnection(Con_Paddy))
        {
            try
            {
                IC_Id = Session["issue_id"].ToString();
                string pdate = getDate_MDY(DaintyDate3.Text);

                con.Open();
                string qrydata = "";

                if (ddlcommodtiy.SelectedValue.ToString() == "13")
                {
                    qrydata = "SELECT Society.Society_Id , Society.Society_Name + ' ( '+ Society.SocPlace + ' )' as Society ,Acceptance_Note_Detail.TC_Number , Acceptance_Note_Detail.Accept_Qty , Acceptance_Note_Detail.Bags , Acceptance_Note_Detail.IssueID  FROM Acceptance_Note_Detail inner join Society on Society.Society_Id = Acceptance_Note_Detail.Purchase_Center  where godown = '" + ddlgodown.SelectedValue.ToString() + "' and Acceptance_Date = '" + pdate + "' and Acceptance_Note_Detail.IssueCenter_ID = '" + IC_Id + "'  and Acceptance_Note_Detail.CommodityId='2' and Acceptance_Note_Detail.WHR_Request is null";
                }
                else if (ddlcommodtiy.SelectedValue.ToString() == "14")
                {
                    qrydata = "SELECT Society.Society_Id , Society.Society_Name + ' ( '+ Society.SocPlace + ' )' as Society ,Acceptance_Note_Detail.TC_Number , Acceptance_Note_Detail.Accept_Qty , Acceptance_Note_Detail.Bags , Acceptance_Note_Detail.IssueID  FROM Acceptance_Note_Detail inner join Society on Society.Society_Id = Acceptance_Note_Detail.Purchase_Center  where godown = '" + ddlgodown.SelectedValue.ToString() + "' and Acceptance_Date = '" + pdate + "' and Acceptance_Note_Detail.IssueCenter_ID = '" + IC_Id + "'  and Acceptance_Note_Detail.CommodityId='3' and Acceptance_Note_Detail.WHR_Request is null";
                }
                da = new SqlDataAdapter(qrydata, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    btnRecptSubmit.Enabled = true;
                    GridView2.DataSource = ds;
                    GridView2.DataBind();
                }
                else
                {
                    btnRecptSubmit.Enabled = false;
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Is Not Available On This Date'); </script> ");
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

    private void bindgridMotaAnaaj()
    {
        GridView2.DataSource = null;
        GridView2.DataBind();

        using (con = new SqlConnection(Con_Maze))
        {
            try
            {
                IC_Id = Session["issue_id"].ToString();
                string pdate = getDate_MDY(DaintyDate3.Text);

                con.Open();
                string qrydata = "";

                if (ddlcommodtiy.SelectedValue.ToString() == "11")
                {
                    qrydata = "SELECT Society.Society_Id , Society.Society_Name + ' ( '+ Society.SocPlace + ' )' as Society ,Acceptance_Note_Detail.TC_Number , Acceptance_Note_Detail.Accept_Qty , Acceptance_Note_Detail.Bags , Acceptance_Note_Detail.IssueID  FROM Acceptance_Note_Detail inner join Society on Society.Society_Id = Acceptance_Note_Detail.Purchase_Center  where godown = '" + ddlgodown.SelectedValue.ToString() + "' and Acceptance_Date = '" + pdate + "' and Acceptance_Note_Detail.IssueCenter_ID = '" + IC_Id + "' and Acceptance_Note_Detail.CommodityId='4' and Acceptance_Note_Detail.WHR_Request is null";
                }
                else if (ddlcommodtiy.SelectedValue.ToString() == "12")
                {
                    qrydata = "SELECT Society.Society_Id , Society.Society_Name + ' ( '+ Society.SocPlace + ' )' as Society ,Acceptance_Note_Detail.TC_Number , Acceptance_Note_Detail.Accept_Qty , Acceptance_Note_Detail.Bags , Acceptance_Note_Detail.IssueID  FROM Acceptance_Note_Detail inner join Society on Society.Society_Id = Acceptance_Note_Detail.Purchase_Center  where godown = '" + ddlgodown.SelectedValue.ToString() + "' and Acceptance_Date = '" + pdate + "' and Acceptance_Note_Detail.IssueCenter_ID = '" + IC_Id + "' and Acceptance_Note_Detail.CommodityId='5' and Acceptance_Note_Detail.WHR_Request is null";
                }
                else if (ddlcommodtiy.SelectedValue.ToString() == "8")
                {
                    qrydata = "SELECT Society.Society_Id , Society.Society_Name + ' ( '+ Society.SocPlace + ' )' as Society ,Acceptance_Note_Detail.TC_Number , Acceptance_Note_Detail.Accept_Qty , Acceptance_Note_Detail.Bags , Acceptance_Note_Detail.IssueID  FROM Acceptance_Note_Detail inner join Society on Society.Society_Id = Acceptance_Note_Detail.Purchase_Center  where godown = '" + ddlgodown.SelectedValue.ToString() + "' and Acceptance_Date = '" + pdate + "' and Acceptance_Note_Detail.IssueCenter_ID = '" + IC_Id + "' and Acceptance_Note_Detail.CommodityId='6' and Acceptance_Note_Detail.WHR_Request is null";
                }
                else if (ddlcommodtiy.SelectedValue.ToString() == "40")
                {
                    qrydata = "SELECT Society.Society_Id , Society.Society_Name + ' ( '+ Society.SocPlace + ' )' as Society ,Acceptance_Note_Detail.TC_Number , Acceptance_Note_Detail.Accept_Qty , Acceptance_Note_Detail.Bags , Acceptance_Note_Detail.IssueID  FROM Acceptance_Note_Detail inner join Society on Society.Society_Id = Acceptance_Note_Detail.Purchase_Center  where godown = '" + ddlgodown.SelectedValue.ToString() + "' and Acceptance_Date = '" + pdate + "' and Acceptance_Note_Detail.IssueCenter_ID = '" + IC_Id + "' and Acceptance_Note_Detail.CommodityId='7' and Acceptance_Note_Detail.WHR_Request is null";
                }
                da = new SqlDataAdapter(qrydata, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    btnRecptSubmit.Enabled = true;
                    GridView2.DataSource = ds;
                    GridView2.DataBind();
                }
                else
                {
                    btnRecptSubmit.Enabled = false;
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Is Not Available On This Date'); </script> ");
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

    protected void btnRecptSubmit_Click(object sender, EventArgs e)
    {
        if (ddlcommodtiy.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Commodity'); </script> ");
            return;
        }
        else if (ddlgodown.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown Name'); </script> ");
            return;
        }
        else if (DaintyDate3.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Date of Acceptance'); </script> ");
            return;
        }
        else if (GridView2.Rows.Count <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('There Is No Record Found On This Godown'); </script> ");
            return;
        }
        else
        {
            if (ddlcommodtiy.SelectedValue.ToString() == "13" || ddlcommodtiy.SelectedValue.ToString() == "14")
            {
                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    using (con = new SqlConnection(Con_CSMS))
                    {
                        try
                        {
                            IC_Id = Session["issue_id"].ToString();
                            Dist_Id = Session["dist_id"].ToString();
                            string Accpt_NO = "";

                            SqlCommand cmdacno = new SqlCommand();
                            cmdacno.Parameters.Clear();
                            cmdacno.Parameters.AddWithValue("@District_ID", Dist_Id);
                            cmdacno.Parameters.AddWithValue("@IssueCenter_ID", IC_Id);
                            cmdacno.Connection = con;

                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }

                            cmdacno.CommandType = CommandType.StoredProcedure;
                            cmdacno.CommandText = "prc_getMaxdepositNo_Paddy2016";

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

                                SqlTransaction trns1;
                                SqlTransaction trns;

                                SqlCommand cmd_ppm = new SqlCommand();
                                cmd_ppm.Connection = con_paddy;
                                trns1 = con_paddy.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                cmd_ppm.Transaction = trns1;

                                string updateppms = "Update Acceptance_Note_Detail set WHR_Request = '" + whr_ID + "' , Updates_Date =  getdate() where Distt_ID = '23" + Dist_Id + "' and IssueCenter_ID = '" + IC_Id + "' and Purchase_Center = '" + socid + "' and TC_Number = '" + TCnum + "' and godown = '" + godown + "' and IssueID = '" + ReceiptId + "'";
                                cmd_ppm.CommandText = updateppms;

                                SqlCommand cmd_con = new SqlCommand();
                                cmd_con.Connection = con;
                                trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                cmd_con.Transaction = trns;

                                string updatecsms = "Update Acceptance_Note_Kharif2016 set WHR_Request = '" + whr_ID + "' , Updates_Date =  getdate() where Distt_ID = '" + Dist_Id + "' and IssueCenter_ID = '" + IC_Id + "' and Purchase_Center = '" + socid + "' and TC_Number = '" + TCnum + "' and godown = '" + godown + "' and IssueID = '" + ReceiptId + "' and CommodityId='" + ddlcommodtiy.SelectedValue.ToString() + "'";
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

                                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully And Your WHR Request Number is " + whr_ID + "'); </script> ");

                                            btnRecptSubmit.Enabled = false;
                                            btnPrint.Enabled = true;

                                            ddlcommodtiy.Enabled = ddlgodown.Enabled = false;

                                            Label2.Visible = true;
                                            Label2.Text = "Data Saved Successfully and Your WHR Request Number is '" + whr_ID + "' ";

                                            Session["WHR_Request"] = whr_ID;
                                            Session["Commodity"] = ddlcommodtiy.SelectedValue;

                                            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
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
                                        btnRecptSubmit.Enabled = false;
                                        trns1.Rollback();
                                        trns.Rollback();

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
                                    if (con.State == ConnectionState.Open)
                                    {
                                        con.Close();
                                    }
                                    if (con_paddy.State == ConnectionState.Open)
                                    {
                                        con_paddy.Close();
                                    }

                                    btnRecptSubmit.Enabled = false;
                                    trns1.Rollback();
                                    trns.Rollback();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                            return;
                        }

                        finally
                        {
                            ddlcommodtiy.Items.Clear();
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
            else
            {
                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    using (con = new SqlConnection(Con_CSMS))
                    {
                        try
                        {
                            IC_Id = Session["issue_id"].ToString();
                            Dist_Id = Session["dist_id"].ToString();
                            string Accpt_NO = "";

                            SqlCommand cmdacno = new SqlCommand();
                            cmdacno.Parameters.Clear();
                            cmdacno.Parameters.AddWithValue("@District_ID", Dist_Id);
                            cmdacno.Parameters.AddWithValue("@IssueCenter_ID", IC_Id);
                            cmdacno.Connection = con;

                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }

                            cmdacno.CommandType = CommandType.StoredProcedure;
                            cmdacno.CommandText = "prc_getMaxdepositNo_Paddy2016";

                            Accpt_NO = Convert.ToString(cmdacno.ExecuteScalar());
                            string whr_ID = Accpt_NO;

                            foreach (GridViewRow gr in GridView2.Rows)
                            {
                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                if (con_maze.State == ConnectionState.Closed)
                                {
                                    con_maze.Open();
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

                                SqlTransaction trns1;
                                SqlTransaction trns;

                                SqlCommand cmd_ppm = new SqlCommand();
                                cmd_ppm.Connection = con_maze;
                                trns1 = con_maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                cmd_ppm.Transaction = trns1;

                                string updateppms = "Update Acceptance_Note_Detail set WHR_Request = '" + whr_ID + "' , Updates_Date =  getdate() where Distt_ID = '23" + Dist_Id + "' and IssueCenter_ID = '" + IC_Id + "' and Purchase_Center = '" + socid + "' and TC_Number = '" + TCnum + "' and godown = '" + godown + "' and IssueID = '" + ReceiptId + "'";
                                cmd_ppm.CommandText = updateppms;

                                SqlCommand cmd_con = new SqlCommand();
                                cmd_con.Connection = con;
                                trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                cmd_con.Transaction = trns;

                                string updatecsms = "Update Acceptance_Note_Kharif2016 set WHR_Request = '" + whr_ID + "' , Updates_Date =  getdate() where Distt_ID = '" + Dist_Id + "' and IssueCenter_ID = '" + IC_Id + "' and Purchase_Center = '" + socid + "' and TC_Number = '" + TCnum + "' and godown = '" + godown + "' and IssueID = '" + ReceiptId + "' and CommodityId='" + ddlcommodtiy.SelectedValue.ToString() + "'";
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

                                            if (con_maze.State == ConnectionState.Open)
                                            {
                                                con_maze.Close();
                                            }

                                            trns.Commit();

                                            if (con.State == ConnectionState.Open)
                                            {
                                                con.Close();
                                            }

                                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully And Your WHR Request Number is " + whr_ID + "'); </script> ");

                                            btnRecptSubmit.Enabled = false;
                                            btnPrint.Enabled = true;

                                            ddlcommodtiy.Enabled = ddlgodown.Enabled = false;

                                            Label2.Visible = true;
                                            Label2.Text = "Data Saved Successfully and Your WHR Request Number is '" + whr_ID + "' ";

                                            Session["WHR_Request"] = whr_ID;
                                            Session["Commodity"] = ddlcommodtiy.SelectedValue;

                                            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
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
                                            if (con_maze.State == ConnectionState.Open)
                                            {
                                                con_maze.Close();
                                            }

                                            if (con.State == ConnectionState.Open)
                                            {
                                                con.Close();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        btnRecptSubmit.Enabled = false;
                                        trns1.Rollback();
                                        trns.Rollback();

                                        if (con_maze.State == ConnectionState.Open)
                                        {
                                            con_maze.Close();
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
                                    if (con_maze.State == ConnectionState.Open)
                                    {
                                        con_maze.Close();
                                    }

                                    btnRecptSubmit.Enabled = false;
                                    trns1.Rollback();
                                    trns.Rollback();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                            return;
                        }

                        finally
                        {
                            ddlcommodtiy.Items.Clear();
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
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnPrint_Click1(object sender, EventArgs e)
    {
        string url = "PrintWHRRequest_Paddy2016.aspx";
        string s = "window.open('" + url + "', 'popup_window');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }

    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }

}