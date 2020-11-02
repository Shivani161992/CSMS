using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class IssueCenter_DelProcRcpt_Pdy2016 : System.Web.UI.Page
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
                DaintyDate3.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                DaintyDate3.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                DaintyDate3.Attributes.Add("onchange", "return chksqltxt(this)");

                IC_Id = Session["issue_id"].ToString();
                Dist_Id = Session["dist_id"].ToString();

                GetCommodity();
                Getdepo();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
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

                select = "SELECT Commodity_Name ,Commodity_Id,(CONVERT(VARCHAR,GETDATE(), 105)) As AcptDate  FROM tbl_MetaData_STORAGE_COMMODITY where  Commodity_Id in ('13','14','8','11','12','40') Order By Commodity_Name";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DaintyDate3.Text = ds.Tables[0].Rows[0]["AcptDate"].ToString();

                    ddlcommodtiy.DataSource = ds.Tables[0];
                    ddlcommodtiy.DataTextField = "Commodity_Name";
                    ddlcommodtiy.DataValueField = "Commodity_Id";
                    ddlcommodtiy.DataBind();

                    ddlcommodtiy.SelectedIndex = 4;
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

    //private void Getdepo()
    //{
    //    using (con = new SqlConnection(Con_WH))
    //    {
    //        try
    //        {
    //            con.Open();
    //            string qrysel = "select BranchId,DepotName from tbl_MetaData_DEPOT where DistrictId='23" + Dist_Id + "'";
    //            da = new SqlDataAdapter(qrysel, con);

    //            ds = new DataSet();
    //            da.Fill(ds);
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                ddlbranchwlc.DataSource = ds.Tables[0];
    //                ddlbranchwlc.DataTextField = "DepotName";
    //                ddlbranchwlc.DataValueField = "BranchId";
    //                ddlbranchwlc.DataBind();
    //                ddlbranchwlc.Items.Insert(0, "--select--");
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

    private void Getdepo()
    {
        using (con = new SqlConnection(Con_WH))
        {
            try
            {
                con.Open();
                string select = string.Format("select tbl_MetaData_DEPOT.DepotName,tbl_MetaData_DEPOT.BranchID from MetaDataBranchWithIssueCenter inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.BranchId=MetaDataBranchWithIssueCenter.BranchID where IssueCenterId='" + IC_Id + "'");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlbranchwlc.DataSource = ds.Tables[0];
                        ddlbranchwlc.DataTextField = "DepotName";
                        ddlbranchwlc.DataValueField = "BranchID";
                        ddlbranchwlc.DataBind();
                        ddlbranchwlc.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Issue Center'); </script> ");
                    }
                }
                else
                {
                    string select1 = string.Format("select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId= '23" + Dist_Id + "' order by DepotName");
                    da = new SqlDataAdapter(select1, con);

                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlbranchwlc.DataSource = ds.Tables[0];
                        ddlbranchwlc.DataTextField = "DepotName";
                        ddlbranchwlc.DataValueField = "BranchId";
                        ddlbranchwlc.DataBind();
                        ddlbranchwlc.Items.Insert(0, "--Select--");
                    }
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

    protected void ddlbranchwlc_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlgodown.Items.Clear();
        ddlIssueID.Items.Clear();

        GridView2.DataSource = "";
        GridView2.DataBind();
        hdfChallan.Value = "";

        if (ddlbranchwlc.SelectedIndex > 0)
        {
            if (DaintyDate3.Text == "")
            {
                ddlbranchwlc.SelectedIndex = 0;
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Receiving Date'); </script> ");
                return;
            }
            else
            {
                GetGodown();
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Branch'); </script> ");
            return;
        }
    }

    private void GetGodown()
    {
        using (con = new SqlConnection(Con_WH))
        {
            try
            {
                Dist_Id = Session["dist_id"].ToString();

                con.Open();
                string qrysel = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where DistrictId= '23" + Dist_Id + "' and BranchId='" + ddlbranchwlc.SelectedValue.ToString() + "' and Remarks = 'Y'";
                da = new SqlDataAdapter(qrysel, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlgodown.DataSource = ds.Tables[0];
                    ddlgodown.DataTextField = "Godown_Name";
                    ddlgodown.DataValueField = "Godown_ID";
                    ddlgodown.DataBind();
                    ddlgodown.Items.Insert(0, "--select--");
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

    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlIssueID.Items.Clear();
        GridView2.DataSource = "";
        GridView2.DataBind();
        hdfChallan.Value = "";

        if (ddlgodown.SelectedIndex > 0)
        {
            if (DaintyDate3.Text == "")
            {
                ddlgodown.SelectedIndex = 0;
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Receiving Date'); </script> ");
                return;
            }
            else
            {
                GetIssueID();
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown'); </script> ");
            return;
        }
    }

    private void GetIssueID()
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                Dist_Id = Session["dist_id"].ToString();
                IC_Id = Session["issue_id"].ToString();
                string redate = getDate_MDY(DaintyDate3.Text);
                con.Open();

                string select = "";
                select = "SELECT distinct Receipt_Id FROM SCSC_Procurement_Kharif2016 where Distt_ID = '" + Dist_Id + "' and IssueCenter_ID = '" + IC_Id + "' and Recd_Godown='" + ddlgodown.SelectedValue.ToString() + "' and Commodity_Id = '" + ddlcommodtiy.SelectedValue.ToString() + "' and Recd_Date = '" + redate + "' and Acceptance_No = '' ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlIssueID.DataSource = ds.Tables[0];
                    ddlIssueID.DataTextField = "Receipt_Id";
                    ddlIssueID.DataValueField = "Receipt_Id";
                    ddlIssueID.DataBind();
                    ddlIssueID.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + DaintyDate3.Text + " की दिनांक में " + ddlgodown.SelectedItem.ToString() + " गोदाम में कोई भी Issue ID उपलब्ध नहीं हैं|'); </script> ");
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

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    protected void ddlIssueID_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnRecptSubmit.Enabled = false;
        hdfChallan.Value = "";
        GridView2.DataSource = "";
        GridView2.DataBind();

        if (ddlIssueID.SelectedIndex > 0)
        {
            if (DaintyDate3.Text == "")
            {
                ddlIssueID.SelectedIndex = 0;
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Receiving Date'); </script> ");
                return;
            }
            else
            {
                GetIssueIDDetails();
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Issue ID'); </script> ");
            return;
        }
    }

    private void GetIssueIDDetails()
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                hdfChallan.Value = "";
                Dist_Id = Session["dist_id"].ToString();
                IC_Id = Session["issue_id"].ToString();
                string redate = getDate_MDY(DaintyDate3.Text);
                con.Open();

                string select = "";
                select = "SELECT TC_Number ,Truck_Number ,Recd_Bags ,Recd_Qty ,convert(nvarchar,Recd_Date,103) As Recd_Date FROM SCSC_Procurement_Kharif2016 where Receipt_Id = '" + ddlIssueID.SelectedItem.ToString() + "' and Receipt_Id not in (select IssueID from Acceptance_Note_Kharif2016 where Distt_ID = '" + Dist_Id + "' and  IssueCenter_ID = '" + IC_Id + "'  and CommodityId = '" + ddlcommodtiy.SelectedValue + "' ) and Distt_ID = '" + Dist_Id + "' and  IssueCenter_ID = '" + IC_Id + "' and Recd_Godown='" + ddlgodown.SelectedValue.ToString() + "' and SCSC_Procurement_Kharif2016.Commodity_Id='" + ddlcommodtiy.SelectedValue + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    GridView2.DataSource = ds.Tables[0];
                    GridView2.DataBind();

                    hdfChallan.Value = ds.Tables[0].Rows[0]["TC_Number"].ToString();
                    btnRecptSubmit.Enabled = true;
                }
                else
                {

                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Acceptance Note Generated, Can Not Delete This Entry'); </script> ");
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
        if (ddlgodown.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Receiving Godown'); </script> ");
            return;
        }
        else if (ddlIssueID.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Issue ID'); </script> ");
            return;
        }
        else if (GridView2.Rows.Count <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Acceptance Note Generated, Can Not Delete This Entry'); </script> ");
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
                            Dist_Id = Session["dist_id"].ToString();
                            IC_Id = Session["issue_id"].ToString();

                            if (con_paddy.State == ConnectionState.Closed)
                            {
                                con_paddy.Open();
                            }
                            try
                            {
                                SqlCommand cmdWdel = new SqlCommand();
                                SqlCommand cmddel = new SqlCommand();

                                SqlTransaction trns1;
                                SqlTransaction trns;

                                string insWlog = "Insert into IssueCenterReceipt_Online_Log(IssueID,DistrictId,IssueCenter_ID,PCID,SocietyID,Sending_District,CropYear,MarketingSeasonId,DateOfIssue,CommodityId,Bags,QtyTransffer,TaulPtrakNo,TransporterId,TruckChalanNo,TruckNo,Recv_Qty,Recd_Godown,Receipt_Id,AN_Status,CreatedDate,UpdatedDate,Recd_Date,Branch_Id,Recd_Bags,RackNumber,RecdQty_Faq,RecdQty_Urs,RecdBags_JuteNew,RecdBags_PP,RecdBags_JuteOld) select IssueID,DistrictId,IssueCenter_ID,PCID,SocietyID,Sending_District,CropYear,MarketingSeasonId,DateOfIssue,CommodityId,Bags,QtyTransffer,TaulPtrakNo,TransporterId,TruckChalanNo,TruckNo,Recv_Qty,Recd_Godown,Receipt_Id,AN_Status,CreatedDate,UpdatedDate,Recd_Date,Branch_Id,Recd_Bags,RackNumber,RecdQty_Faq,RecdQty_Urs,RecdBags_JuteNew,RecdBags_PP,RecdBags_JuteOld from IssueCenterReceipt_Online where IssueID = '" + ddlIssueID.SelectedItem.ToString() + "' and DistrictId = '23" + Dist_Id + "'  and TruckChalanNo = '" + hdfChallan.Value + "' ";
                                SqlCommand cmdWlog = new SqlCommand(insWlog, con_paddy);
                                int x = cmdWlog.ExecuteNonQuery();

                                string delWqry = "delete from IssueCenterReceipt_Online where IssueID = '" + ddlIssueID.SelectedItem.ToString() + "' and DistrictId = '23" + Dist_Id + "'  and TruckChalanNo = '" + hdfChallan.Value + "' ";
                                cmdWdel.CommandText = delWqry;
                                cmdWdel.Connection = con_paddy;

                                trns = con_paddy.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                cmdWdel.Transaction = trns;
                                int y = cmdWdel.ExecuteNonQuery();

                                if (y > 0)
                                {
                                    string opid = Session["OperatorId"].ToString();
                                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }

                                    string inslog = "Insert into SCSC_Procurement_Kharif2016_Log(Distt_ID,IssueCenter_ID,Sending_District,Purchase_Center,Dispatch_Date,TC_Number,Truck_Number,Transporter_ID,Commodity_Id,Crop_Year,No_of_Bags,Quantity,Acceptance_No,Acceptance_Date,Book_No,Recd_Bags,Recd_Qty,Recd_Date,Recd_Godown,Receipt_Id,Month,Year,Status_Deposit,Created_Date,Updates_Date,Deleted_Date,IP_Address,AN_Status,OperatorID,State_Id,NoTransaction,Branch_Id,Stiching_bags,Stencile_bags,RackNumber,RecdQty_Faq,RecdQty_Urs,RecdBags_JuteNew,RecdBags_PP,RecdBags_JuteOld,Moisture,TaulParchi,WHRNumber,category,Transp_Pancard,GodownTypeId,DeletedDate,DeletedIP,Deleted_OPID) select Distt_ID,IssueCenter_ID,Sending_District,Purchase_Center,Dispatch_Date,TC_Number,Truck_Number,Transporter_ID,Commodity_Id,Crop_Year,No_of_Bags,Quantity,Acceptance_No,Acceptance_Date,Book_No,Recd_Bags,Recd_Qty,Recd_Date,Recd_Godown,Receipt_Id,Month,Year,Status_Deposit,Created_Date,Updates_Date,Deleted_Date,IP_Address,AN_Status,OperatorID,State_Id,NoTransaction,Branch_Id,Stiching_bags,Stencile_bags,RackNumber,RecdQty_Faq,RecdQty_Urs,RecdBags_JuteNew,RecdBags_PP,RecdBags_JuteOld,Moisture,TaulParchi,WHRNumber,category,Transp_Pancard,GodownTypeId,GETDATE(),'" + ip + "','" + opid + "' from SCSC_Procurement_Kharif2016 where Receipt_Id = '" + ddlIssueID.SelectedItem.ToString() + "' and Distt_ID = '" + Dist_Id + "' and IssueCenter_ID = '" + IC_Id + "'  and TC_Number = '" + hdfChallan.Value + "' and Commodity_Id='" + ddlcommodtiy.SelectedValue.ToString() + "'  ";
                                    SqlCommand cmdlog = new SqlCommand(inslog, con);
                                    int a = cmdlog.ExecuteNonQuery();

                                    string delqry = "delete from SCSC_Procurement_Kharif2016 where Receipt_Id = '" + ddlIssueID.SelectedItem.ToString() + "' and Distt_ID = '" + Dist_Id + "' and IssueCenter_ID = '" + IC_Id + "'  and TC_Number = '" + hdfChallan.Value + "' and Commodity_Id='" + ddlcommodtiy.SelectedValue.ToString() + "' ";
                                    cmddel.CommandText = delqry;
                                    cmddel.Connection = con;

                                    trns1 = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                    cmddel.Transaction = trns1;
                                    int b = cmddel.ExecuteNonQuery();

                                    if (b > 0)
                                    {
                                        trns.Commit();
                                        trns1.Commit();
                                    }
                                    else
                                    {
                                        trns.Rollback();
                                        trns1.Rollback();
                                    }

                                    btnRecptSubmit.Enabled = false;
                                    ddlcommodtiy.Enabled = ddlgodown.Enabled = ddlbranchwlc.Enabled = ddlIssueID.Enabled = false;
                                    ddlgodown.Items.Clear();

                                    Label2.Visible = true;
                                    Label2.Text = "Data Deleted Sucessfully";

                                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Deleted Sucessfully'); </script> ");
                                }
                            }
                            catch (Exception ex)
                            {
                                btnRecptSubmit.Enabled = false;
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error! To Delete This Entry'); </script> ");
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
            else
            {
                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    using (con = new SqlConnection(Con_CSMS))
                    {
                        try
                        {
                            Dist_Id = Session["dist_id"].ToString();
                            IC_Id = Session["issue_id"].ToString();

                            if (con_maze.State == ConnectionState.Closed)
                            {
                                con_maze.Open();
                            }
                            try
                            {
                                SqlCommand cmdWdel = new SqlCommand();
                                SqlCommand cmddel = new SqlCommand();

                                SqlTransaction trns1;
                                SqlTransaction trns;

                                string insWlog = "Insert into IssueCenterReceipt_Online_Log(IssueID,DistrictId,IssueCenter_ID,PCID,SocietyID,Sending_District,CropYear,MarketingSeasonId,DateOfIssue,CommodityId,Bags,QtyTransffer,TaulPtrakNo,TransporterId,TruckChalanNo,TruckNo,Recv_Qty,Recd_Godown,Receipt_Id,AN_Status,CreatedDate,UpdatedDate,Recd_Date,Branch_Id,Recd_Bags,RackNumber,RecdQty_Faq,RecdQty_Urs,RecdBags_JuteNew,RecdBags_PP,RecdBags_JuteOld) select IssueID,DistrictId,IssueCenter_ID,PCID,SocietyID,Sending_District,CropYear,MarketingSeasonId,DateOfIssue,CommodityId,Bags,QtyTransffer,TaulPtrakNo,TransporterId,TruckChalanNo,TruckNo,Recv_Qty,Recd_Godown,Receipt_Id,AN_Status,CreatedDate,UpdatedDate,Recd_Date,Branch_Id,Recd_Bags,RackNumber,RecdQty_Faq,RecdQty_Urs,RecdBags_JuteNew,RecdBags_PP,RecdBags_JuteOld from IssueCenterReceipt_Online where IssueID = '" + ddlIssueID.SelectedItem.ToString() + "' and DistrictId = '23" + Dist_Id + "' and IssueCenter_ID = '" + IC_Id + "'  and TruckChalanNo = '" + hdfChallan.Value + "' ";
                                SqlCommand cmdWlog = new SqlCommand(insWlog, con_maze);
                                int x = cmdWlog.ExecuteNonQuery();

                                string delWqry = "delete from IssueCenterReceipt_Online where IssueID = '" + ddlIssueID.SelectedItem.ToString() + "' and DistrictId = '23" + Dist_Id + "' and IssueCenter_ID = '" + IC_Id + "'  and TruckChalanNo = '" + hdfChallan.Value + "' ";
                                cmdWdel.CommandText = delWqry;
                                cmdWdel.Connection = con_maze;

                                trns = con_maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                cmdWdel.Transaction = trns;
                                int y = cmdWdel.ExecuteNonQuery();

                                if (y > 0)
                                {
                                    string opid = Session["OperatorId"].ToString();

                                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }

                                    string inslog = "Insert into SCSC_Procurement_Kharif2016_Log(Distt_ID,IssueCenter_ID,Sending_District,Purchase_Center,Dispatch_Date,TC_Number,Truck_Number,Transporter_ID,Commodity_Id,Crop_Year,No_of_Bags,Quantity,Acceptance_No,Acceptance_Date,Book_No,Recd_Bags,Recd_Qty,Recd_Date,Recd_Godown,Receipt_Id,Month,Year,Status_Deposit,Created_Date,Updates_Date,Deleted_Date,IP_Address,AN_Status,OperatorID,State_Id,NoTransaction,Branch_Id,Stiching_bags,Stencile_bags,RackNumber,RecdQty_Faq,RecdQty_Urs,RecdBags_JuteNew,RecdBags_PP,RecdBags_JuteOld,Moisture,TaulParchi,WHRNumber,category,Transp_Pancard,GodownTypeId,DeletedDate,DeletedIP,Deleted_OPID) select Distt_ID,IssueCenter_ID,Sending_District,Purchase_Center,Dispatch_Date,TC_Number,Truck_Number,Transporter_ID,Commodity_Id,Crop_Year,No_of_Bags,Quantity,Acceptance_No,Acceptance_Date,Book_No,Recd_Bags,Recd_Qty,Recd_Date,Recd_Godown,Receipt_Id,Month,Year,Status_Deposit,Created_Date,Updates_Date,Deleted_Date,IP_Address,AN_Status,OperatorID,State_Id,NoTransaction,Branch_Id,Stiching_bags,Stencile_bags,RackNumber,RecdQty_Faq,RecdQty_Urs,RecdBags_JuteNew,RecdBags_PP,RecdBags_JuteOld,Moisture,TaulParchi,WHRNumber,category,Transp_Pancard,GodownTypeId,GETDATE(),'" + ip + "','" + opid + "' from SCSC_Procurement_Kharif2016 where Receipt_Id = '" + ddlIssueID.SelectedItem.ToString() + "' and Distt_ID = '" + Dist_Id + "' and IssueCenter_ID = '" + IC_Id + "'  and TC_Number = '" + hdfChallan.Value + "' and Commodity_Id='" + ddlcommodtiy.SelectedValue.ToString() + "'  ";
                                    SqlCommand cmdlog = new SqlCommand(inslog, con);
                                    int a = cmdlog.ExecuteNonQuery();

                                    string delqry = "delete from SCSC_Procurement_Kharif2016 where Receipt_Id = '" + ddlIssueID.SelectedItem.ToString() + "' and Distt_ID = '" + Dist_Id + "' and IssueCenter_ID = '" + IC_Id + "'  and TC_Number = '" + hdfChallan.Value + "' and Commodity_Id='" + ddlcommodtiy.SelectedValue.ToString() + "' ";
                                    cmddel.CommandText = delqry;
                                    cmddel.Connection = con;

                                    trns1 = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                    cmddel.Transaction = trns1;
                                    int b = cmddel.ExecuteNonQuery();

                                    if (b > 0)
                                    {
                                        trns.Commit();
                                        trns1.Commit();
                                    }
                                    else
                                    {
                                        trns.Rollback();
                                        trns1.Rollback();
                                    }

                                    btnRecptSubmit.Enabled = false;
                                    ddlcommodtiy.Enabled = ddlgodown.Enabled = ddlbranchwlc.Enabled = ddlIssueID.Enabled = false;
                                    ddlgodown.Items.Clear();

                                    Label2.Visible = true;
                                    Label2.Text = "Data Deleted Sucessfully";

                                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Deleted Sucessfully'); </script> ");
                                }
                            }
                            catch (Exception ex)
                            {
                                btnRecptSubmit.Enabled = false;
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error! To Delete This Entry'); </script> ");
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
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
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