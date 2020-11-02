using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.Data.SqlClient;

public partial class MBCFPCL_MPCFPCL_FinalDepositer_CSM2018 : System.Web.UI.Page
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
               // IC_Id = Session["issue_id"].ToString();
                Dist_Id = Session["DistID"].ToString();

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


                    //GetICName();
                    GetCommodity();

                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
            }


        }
        else
        {
            Response.Redirect("~/MBCFPCL/MBCFPCL_Login.aspx");
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
        // ddl_ProDepositer.Items.Clear();
        hdfGodownCode.Value = "";

        GridView2.DataSource = null;
        GridView2.DataBind();

        if (ddlcommodtiy.SelectedIndex > 0)
        {

            GridView2.DataSource = null;
            GridView2.DataBind();

            btnRecptSubmit.Enabled = false;

            if (ddlcommodtiy.SelectedValue.ToString() == "33" || ddlcommodtiy.SelectedValue.ToString() == "63" || ddlcommodtiy.SelectedValue.ToString() == "64")
            {
                GetProvDepositer();
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Commodity'); </script> ");
            return;
        }
    }

    private void GetProvDepositer()
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();
                string select = "select distinct Intergrated_MP_STORAGE.dbo.ChkQuality_ProviosnalDepositorForm.WHR_Request  from Intergrated_MP_STORAGE.dbo.ChkQuality_ProviosnalDepositorForm inner join DepositerForm_CSM2018 on DepositerForm_CSM2018.WHR_Request COLLATE Latin1_General_CI_AS = Intergrated_MP_STORAGE.dbo.ChkQuality_ProviosnalDepositorForm.WHR_Request where Intergrated_MP_STORAGE.dbo.ChkQuality_ProviosnalDepositorForm.WHR_Request COLLATE Latin1_General_CI_AS not in (select WHR_RequestOld from Final_DepositerForm_CSM2018 where Distt_ID = '" + Dist_Id + "' ) and Rejection_Status = 'N' and Intergrated_MP_STORAGE.dbo.ChkQuality_ProviosnalDepositorForm.CommodityID = '" + ddlcommodtiy.SelectedValue + "' and District_ID = '23" + Dist_Id + "'  ";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddl_ProDepositer.DataSource = ds.Tables[0];
                    ddl_ProDepositer.DataTextField = "WHR_Request";
                    ddl_ProDepositer.DataValueField = "WHR_Request";
                    ddl_ProDepositer.DataBind();
                    ddl_ProDepositer.Items.Insert(0, "--Select--");
                }
                else
                {
                    ddl_ProDepositer.DataSource = "";

                    ddl_ProDepositer.DataBind();
                    ddl_ProDepositer.Items.Insert(0, "Data Not Available");
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

        if (ddl_ProDepositer.SelectedIndex > 0)
        {
            if (ddlcommodtiy.SelectedValue.ToString() == "33" || ddlcommodtiy.SelectedValue.ToString() == "63" || ddlcommodtiy.SelectedValue.ToString() == "64")
            {
                bindgrid();
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

                con.Open();
                string qrydata = "";

                if (ddlcommodtiy.SelectedValue.ToString() == "33" || ddlcommodtiy.SelectedValue.ToString() == "63" || ddlcommodtiy.SelectedValue.ToString() == "64")
                {
                    qrydata = "SELECT Society_MSP.Society_Id , Society_MSP.Society_Name + ' ( '+ Society_MSP.SocPlace + ' )' as Society ,SCSC_Procurement_CSM.TC_Number , SCSC_Procurement_CSM.NetWeight , SCSC_Procurement_CSM.Recd_Bags ,SCSC_Procurement_CSM.Acceptance_No, SCSC_Procurement_CSM.Receipt_Id , SCSC_Procurement_CSM.Recd_Godown , tbl_MetaData_GODOWN.Godown_Name  FROM SCSC_Procurement_CSM inner join Society_MSP on Society_MSP.Society_Id = SCSC_Procurement_CSM.Purchase_Center  inner join tbl_MetaData_GODOWN on tbl_MetaData_GODOWn.Godown_ID = SCSC_Procurement_CSM.Recd_Godown  where   SCSC_Procurement_CSM.TC_Number COLLATE Latin1_General_CI_AS in (select   TC_Number  from  Intergrated_MP_STORAGE.dbo.ChkQuality_ProviosnalDepositorForm  where Rejection_Status = 'N' and WHR_Request = '" + ddl_ProDepositer.SelectedItem.Text + "' ) and Society_MSP.MBC='Y' order by SCSC_Procurement_CSM.Recd_Godown ";
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
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Is Not Available'); </script> ");
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
                        cmdacno.CommandText = "getMAxDepositer_FinalCSM18Depositer_Final";

                        Accpt_NO = Convert.ToString(cmdacno.ExecuteScalar());
                        string whr_ReqFinal = Accpt_NO;

                        int addId = 1;

                        foreach (GridViewRow gr in grd_data.Rows)
                        {
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }


                            string commname = ddlcommodtiy.SelectedValue;
                            string godown = gr.Cells[0].Text;
                            string socid = gr.Cells[2].Text;

                            if (godown == "")
                            {

                            }

                            else
                            {
                                string fqry = "select WHR_ReqGdn from Final_DepositerForm_CSM2018 where godown = '" + godown + "' and WHR_RequestOld = '" + ddl_ProDepositer.SelectedItem.Text + "' ";

                                string WHrReqGdn = "";

                                SqlCommand cmdF = new SqlCommand(fqry, con);

                                SqlDataAdapter daF = new SqlDataAdapter(cmdF);

                                DataSet dsF = new DataSet();
                                daF.Fill(dsF);

                                if (dsF.Tables[0].Rows.Count > 0)
                                {
                                    WHrReqGdn = dsF.Tables[0].Rows[0]["WHR_ReqGdn"].ToString();
                                }

                                else
                                {

                                    WHrReqGdn = ddl_ProDepositer.SelectedItem.Text + "A" + addId.ToString();

                                    addId = addId + 1;

                                }

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

                                string updatecsms = "Insert into Final_DepositerForm_CSM2018 (Distt_ID,IssueCenter_ID,Purchase_Center,TC_Number,Acceptance_No,Month,Year,Created_Date,IP_Address,OperatorID,Accept_Qty,Acpt_Bags ,CommodityId ,IssueID ,godown ,WHR_RequestOld,CropYear,WHR_ReqNumber,WHR_ReqGdn,WHRReqNew_Date,BillStatus) Values ('" + Dist_Id + "','" + IC_Id + "','" + socid + "','" + TCnum + "' , '" + AcptNum + "' , " + month + " , " + year + " , getdate() , '" + ip + "' , '" + opid + "' , " + accepqty + " , " + accepbags + " , '" + commid + "' , '" + ReceiptId + "' , '" + GodownId + "'," + ddl_ProDepositer.SelectedItem.Text + ",'" + txtYear.Text + "','" + whr_ReqFinal + "','" + WHrReqGdn + "',getdate(),'N') ";
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

                                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Final Data Saved Successfully And Your WHR Request Number is " + whr_ReqFinal + "'); </script> ");

                                        btnRecptSubmit.Enabled = false;
                                        btnPrint.Enabled = true;

                                        ddlcommodtiy.Enabled = ddl_ProDepositer.Enabled = false;

                                        Label2.Visible = true;
                                        Label2.Text = "Data Saved Successfully and Your FINAL Depositer Form Number is '" + whr_ReqFinal + "' ";

                                        Session["WHR_Request"] = whr_ReqFinal;
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
        string WHr_req = Session["WHR_Request"].ToString();
        string commodity = Session["Commodity"].ToString();

        using (con = new SqlConnection(Con_CSMS))
        {
            string upqry = "Update Final_DepositerForm_CSM2018 set isprint = 'Y' where WHR_ReqNumber = '" + WHr_req + "' and CommodityId = '" + commodity + "' ";
            SqlCommand cmdUP = new SqlCommand(upqry, con);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            int x = cmdUP.ExecuteNonQuery();

            if (x > 0)
            {
                string url = "../Report_IssueCenter/PrintFinalWHRRequest_CSM2018.aspx";

                string s = "window.open('" + url + "', 'popup_window');";
                ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }
        }
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

    protected void ddl_ProDepositer_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindgrid();
    }

}