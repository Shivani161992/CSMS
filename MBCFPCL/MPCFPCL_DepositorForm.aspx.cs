using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.Data.SqlClient;

public partial class MBCFPCL_MPCFPCL_DepositorForm : System.Web.UI.Page
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

    decimal qtyTotal = 0;
    Int32 qtyTotalBags = 0;
    Int32 grQtyTotalBags = 0;
    decimal grQtyTotal = 0;
    Int64 storid = 0;

    decimal qtyTotal_soc = 0;
    Int32 qtyTotalBags_soc = 0;
    Int32 grQtyTotalBags_soc = 0;
    decimal grQtyTotal_soc = 0;
    Int64 storid_soc = 0;


    int rowIndex = 1;

    DataSet ds_grid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["DistID"] != null)
        {
            string samepassword = Session["NotchangePassword"].ToString();
            if (samepassword == "MBC123")
            {
                Response.Redirect("~/MBCFPCL/MBCFPCL_ChangePassword.aspx");
            }
            else if (samepassword != "MBC123")
            {
                if (!IsPostBack)
                {
                    Session["WHR_Request"] = "";
                    Session["Commodity"] = "";

                    //txtissue.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                    //txtissue.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                    //txtissue.Attributes.Add("onchange", "return chksqltxt(this)");

                    txtYear.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                    txtYear.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                    txtYear.Attributes.Add("onchange", "return chksqltxt(this)");



                    //IC_Id = Session["issue_id"].ToString();
                    Dist_Id = Session["DistID"].ToString();

                    //GetICName();
                    GetCommodity();

                    GetAcDate();

                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
            }


        }
        else
        {
            Response.Redirect("~/MBCFPCL/MBCFPCL_Login.aspx");
        }
    }


    //private void GetICName()
    //{
    //    using (con = new SqlConnection(Con_CSMS))
    //    {
    //        try
    //        {
    //            con.Open();
    //            string select = "select DepotName,(CONVERT(VARCHAR,GETDATE(), 105)) As AcptDate from dbo.tbl_MetaData_DEPOT where DepotID='" + IC_Id + "'";
    //            da = new SqlDataAdapter(select, con);

    //            ds = new DataSet();
    //            da.Fill(ds);
    //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //            {
    //                txtissue.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();

    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
    //            return;
    //        }

    //        finally
    //        {
    //            if (con.State != ConnectionState.Closed)
    //            {
    //                con.Close();
    //            }
    //        }
    //    }
    //}

    private void GetCommodity()
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();

                string select = "";

                select = "SELECT Commodity_Name ,Commodity_Id FROM tbl_MetaData_STORAGE_COMMODITY where  Commodity_Id in ('33','63','64') Order By Commodity_Name";

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
            if (ddl_date.SelectedItem.Text == "--Select--")
            {
                ddlcommodtiy.SelectedIndex = 0;
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Date of Acceptance'); </script> ");
                return;
            }
            else
            {
                GridView2.DataSource = null;
                GridView2.DataBind();

                btnRecptSubmit.Enabled = false;

                if (ddlcommodtiy.SelectedValue.ToString() == "33" || ddlcommodtiy.SelectedValue.ToString() == "63" || ddlcommodtiy.SelectedValue.ToString() == "64")
                {
                    bindgrid();
                }

                //GetGodown();
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Commodity'); </script> ");
            return;
        }
    }

    private void GetAcDate()
    {

        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {

               // IC_Id = Session["issue_id"].ToString();
                Dist_Id = Session["DistID"].ToString();

                string select = "";
                select = "SELECT distinct convert(varchar(10),Acceptance_Date,105)Acceptance_Date FROM SCSC_Procurement_CSM where AN_Status = 'Y' and Distt_ID = '" + Dist_Id + "' and  SCSC_Procurement_CSM.TC_Number not in (select TC_Number from DepositerForm_CSM2018 where DepositerForm_CSM2018.IssueCenter_ID = '" + IC_Id + "')";
                con.Open();
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddl_date.DataSource = ds.Tables[0];
                    ddl_date.DataTextField = "Acceptance_Date";
                    ddl_date.DataValueField = "Acceptance_Date";
                    ddl_date.DataBind();
                    ddl_date.Items.Insert(0, "--Select--");
                }
                else
                {
                    ddl_date.Items.Insert(0, "--Select--");

                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Acceptance Note नहीं बनाये गए है अथवा Acceptance Note का Depositer Form जारी किया जा चूका है, अतः दिनाँक उपलब्ध नहीं है|'); </script> ");
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
            if (ddl_date.SelectedItem.Text == "--Select--")
            {
                ddlcommodtiy.SelectedIndex = 0;
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Date of Acceptance'); </script> ");
                return;
            }
            else
            {
                if (ddlcommodtiy.SelectedValue.ToString() == "33" || ddlcommodtiy.SelectedValue.ToString() == "63" || ddlcommodtiy.SelectedValue.ToString() == "64")
                {
                    bindgrid();
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

        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                //IC_Id = Session["issue_id"].ToString();
                string pdate = getDate_MDY(ddl_date.SelectedItem.Text);

                con.Open();
                string qrydata = "";

                if (ddlcommodtiy.SelectedValue.ToString() == "33" || ddlcommodtiy.SelectedValue.ToString() == "63" || ddlcommodtiy.SelectedValue.ToString() == "64")
                {
                    // qrydata = "SELECT Society_MSP.Society_Id , Society_MSP.Society_Name + ' ( '+ Society_MSP.SocPlace + ' )' as Society ,SCSC_Procurement_CSM.TC_Number , SCSC_Procurement_CSM.NetWeight , SCSC_Procurement_CSM.Recd_Bags ,SCSC_Procurement_CSM.Acceptance_No, SCSC_Procurement_CSM.Receipt_Id  FROM SCSC_Procurement_CSM inner join Society_MSP on Society_MSP.Society_Id = SCSC_Procurement_CSM.Purchase_Center  where Recd_Godown = '" + ddlgodown.SelectedValue.ToString() + "' and convert(varchar(10),Acceptance_Date,101) = '" + pdate + "' and SCSC_Procurement_CSM.IssueCenter_ID = '" + IC_Id + "' and SCSC_Procurement_CSM.TC_Number not in (select TC_Number from DepositerForm_CSM2018 where DepositerForm_CSM2018.IssueCenter_ID = '" + IC_Id + "') ";
                    qrydata = "SELECT Society_MSP.Society_Id , Society_MSP.Society_Name + ' ( '+ Society_MSP.SocPlace + ' )' as Society ,SCSC_Procurement_CSM.TC_Number , SCSC_Procurement_CSM.NetWeight , SCSC_Procurement_CSM.Recd_Bags ,SCSC_Procurement_CSM.Acceptance_No, SCSC_Procurement_CSM.Receipt_Id , SCSC_Procurement_CSM.Recd_Godown , tbl_MetaData_GODOWN.Godown_Name  FROM SCSC_Procurement_CSM inner join Society_MSP on Society_MSP.Society_Id = SCSC_Procurement_CSM.Purchase_Center  inner join tbl_MetaData_GODOWN on tbl_MetaData_GODOWn.Godown_ID = SCSC_Procurement_CSM.Recd_Godown  where  convert(varchar(10),Acceptance_Date,101) = '" + pdate + "'  and SCSC_Procurement_CSM.TC_Number not in (select TC_Number from DepositerForm_CSM2018 where DepositerForm_CSM2018.CommodityId = '" + ddlcommodtiy.SelectedValue.ToString() + "') and SCSC_Procurement_CSM.Commodity_Id = '" + ddlcommodtiy.SelectedValue.ToString() + "' and Society_MSP.MBC='Y' ";

                }

                da = new SqlDataAdapter(qrydata, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    btnRecptSubmit.Enabled = true;
                    GridView2.DataSource = ds;
                    GridView2.DataBind();

                    grd_data.DataSource = ds;
                    grd_data.DataBind();
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

    protected void btnRecptSubmit_Click(object sender, EventArgs e)
    {
        if (ddlcommodtiy.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Commodity'); </script> ");
            return;
        }

        else if (ddl_date.SelectedItem.Text == "--Select--")
        {
            ddlcommodtiy.SelectedIndex = 0;
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Date of Acceptance'); </script> ");
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
                        //IC_Id = Session["issue_id"].ToString();
                        Dist_Id = Session["DistID"].ToString();
                        string Accpt_NO = "";

                        SqlCommand cmdacno = new SqlCommand();
                        cmdacno.Parameters.Clear();
                        cmdacno.Parameters.AddWithValue("@District_ID", Dist_Id);
                        //cmdacno.Parameters.AddWithValue("@IssueCenter_ID", IC_Id);
                        cmdacno.Connection = con;

                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        cmdacno.CommandType = CommandType.StoredProcedure;
                        cmdacno.CommandText = "getMAxDepositer_CSM18Depositer";

                        Accpt_NO = Convert.ToString(cmdacno.ExecuteScalar());
                        string whr_ID = Accpt_NO;

                        foreach (GridViewRow gr in grd_data.Rows)
                        {
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }

                            string acceptdate = getDate_MDY(ddl_date.SelectedItem.Text);
                            string commname = ddlcommodtiy.SelectedValue;
                            string godown = gr.Cells[0].Text;
                            string socid = gr.Cells[2].Text;

                            if (godown == "")
                            {

                            }

                            else
                            {
                                Label lbltc = (Label)gr.FindControl("lbltc");
                                string TCnum = lbltc.Text;

                                Label txtrcq = (Label)gr.FindControl("lblacqt");
                                double accepqty = Convert.ToDouble(txtrcq.Text);

                                Label txtrcB = (Label)gr.FindControl("lblacbag");
                                Int32 accepbags = Convert.ToInt32(txtrcB.Text);

                                string AcptNum = gr.Cells[7].Text;

                                string ReceiptId = gr.Cells[8].Text;


                                int month = int.Parse(DateTime.Today.Month.ToString());
                                int year = int.Parse(DateTime.Today.Year.ToString());
                                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                                string opid = Session["OperatorId"].ToString();

                                string commid = ddlcommodtiy.SelectedValue;
                                string GodownId = gr.Cells[0].Text;

                                SqlCommand cmd_con = new SqlCommand();
                                cmd_con.Connection = con;

                                string updatecsms = "Insert into DepositerForm_CSM2018 (Distt_ID,IssueCenter_ID,Purchase_Center,TC_Number,Acceptance_No,Month,Year,Created_Date,IP_Address,OperatorID,Accept_Qty,Acpt_Bags ,CommodityId ,IssueID ,godown ,WHR_Request,CropYear) Values ('" + Dist_Id + "','" + IC_Id + "','" + socid + "','" + TCnum + "' , '" + AcptNum + "' , " + month + " , " + year + " , getdate() , '" + ip + "' , '" + opid + "' , " + accepqty + " , " + accepbags + " , '" + commid + "' , '" + ReceiptId + "' , '" + GodownId + "'," + Accpt_NO + ",'" + txtYear.Text + "') ";
                                cmd_con.CommandText = updatecsms;

                                int y = cmd_con.ExecuteNonQuery();

                                if (y > 0)
                                {

                                    try
                                    {

                                        if (con.State == ConnectionState.Open)
                                        {
                                            con.Close();
                                        }

                                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Proviosal Data Saved Successfully And Your WHR Request Number is " + whr_ID + "'); </script> ");

                                        btnRecptSubmit.Enabled = false;
                                        btnPrint.Enabled = true;

                                        ddlcommodtiy.Enabled = ddlgodown.Enabled = false;

                                        Label2.Visible = true;
                                        Label2.Text = "Data Saved Successfully and Your Proviosal Depositer Form Number is '" + whr_ID + "' ";

                                        Session["WHR_Request"] = whr_ID;
                                        Session["Commodity"] = ddlcommodtiy.SelectedValue;

                                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                                    }

                                    catch (Exception ex)
                                    {
                                        btnRecptSubmit.Enabled = false;


                                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                                        return;
                                    }

                                    finally
                                    {

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


                                    btnRecptSubmit.Enabled = false;


                                }

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
        string url = "../Report_IssueCenter/PrintNewWHRRequest_CSM2018.aspx";
        string s = "window.open('" + url + "', 'popup_window');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }

    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MBCFPCL/MPCFPCL_WelcomeHome.aspx");
    }

    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        getgdntot(sender, e);

        //  getSoctot(sender, e);

    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            aceqty += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NetWeight"));
            accbags += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Recd_Bags"));

            storid = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "Recd_Godown").ToString());
            decimal tmpTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NetWeight").ToString());

            Int32 tmpTotalBags = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Recd_Bags").ToString());
            qtyTotal += tmpTotal;
            grQtyTotal += tmpTotal;

            qtyTotalBags += tmpTotalBags;
            grQtyTotalBags += tmpTotalBags;


            storid_soc = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "Society_Id").ToString());
            decimal tmpTotal_soc = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NetWeight").ToString());

            Int32 tmpTotalBags_soc = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Recd_Bags").ToString());
            qtyTotal_soc += tmpTotal_soc;
            grQtyTotal_soc += tmpTotal_soc;

            qtyTotalBags_soc += tmpTotalBags_soc;
            grQtyTotalBags_soc += tmpTotalBags_soc;

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblqty1 = (Label)e.Row.FindControl("lbl_acqt");
            lblqty1.Text = aceqty.ToString();

            Label lblricebag1 = (Label)e.Row.FindControl("lbl_acbag");
            lblricebag1.Text = accbags.ToString();
        }
    }

    protected void ddl_date_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCommodity();

        GridView2.DataSource = null;
        GridView2.DataBind();
    }

    protected void getgdntot(object sender, GridViewRowEventArgs e)
    {
        bool newRow = false;
        if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "Recd_Godown") != null))
        {
            if (storid != Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "Recd_Godown").ToString()))
                newRow = true;
        }
        if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "Recd_Godown") == null))
        {
            newRow = true;
            rowIndex = 0;
        }
        if (newRow)
        {
            GridView GridView2 = (GridView)sender;
            GridViewRow NewTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
            NewTotalRow.Font.Bold = true;
            NewTotalRow.BackColor = System.Drawing.Color.Gray;
            NewTotalRow.ForeColor = System.Drawing.Color.White;
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Sub Total";
            HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            HeaderCell.ColumnSpan = 5;
            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal.ToString();

            NewTotalRow.Cells.Add(HeaderCell);
            GridView2.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotalBags.ToString();

            NewTotalRow.Cells.Add(HeaderCell);
            GridView2.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow);

            rowIndex++;
            qtyTotal = 0;
            qtyTotalBags = 0;
        }
    }

    protected void getSoctot(object sender, GridViewRowEventArgs e)
    {
        bool newRow = false;
        if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "Society_Id") != null))
        {
            if (storid != Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "Society_Id").ToString()))
                newRow = true;
        }
        if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "Society_Id") == null))
        {
            newRow = true;
            rowIndex = 0;
        }
        if (newRow)
        {
            GridView GridView2 = (GridView)sender;
            GridViewRow NewTotalRow1 = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
            NewTotalRow1.Font.Bold = true;
            NewTotalRow1.BackColor = System.Drawing.Color.Gray;
            NewTotalRow1.ForeColor = System.Drawing.Color.White;
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Sub Total";
            HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            HeaderCell.ColumnSpan = 5;
            NewTotalRow1.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal_soc.ToString();

            NewTotalRow1.Cells.Add(HeaderCell);
            GridView2.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow1);

            NewTotalRow1.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotalBags_soc.ToString();

            NewTotalRow1.Cells.Add(HeaderCell);
            GridView2.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow1);

            rowIndex++;
            qtyTotal_soc = 0;
            qtyTotalBags_soc = 0;
        }
    }

}