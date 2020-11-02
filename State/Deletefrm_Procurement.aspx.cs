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

public partial class District_Deletefrm_Procurement : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_PPMS2014"].ToString());
    public SqlConnection con_Maze = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_MPMS2014"].ToString());
    public SqlConnection con_WPMS = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2015"].ToString());

    public static string distid;

    public static string mscheme = "101";

    public static string mcomdtyu;
    Districts DObj = null;
       protected Common ComObj = null, cmn = null;
       DistributionCenters distobj = null;

    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (Session["st_id"] != null)
        {
           
          //  distid = Session["issue_id"].ToString();
          //string  issueid = Session["issue_id"].ToString();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            if (!IsPostBack)
            {

                Recdate.Attributes.Add("onkeypress", "return CheckCalDate(this)");

                //Recdate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");

                //distid = Session["issue_id"].ToString();

                //GetIssueCenter();
                GetDist();
                GetCrop();        
            }
        }

        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
  }

    protected void GetIssueCenter()
    {
        //distid = Session["dist_id"].ToString();
        //string issueid = Session["issue_id"].ToString();
        string issuecentre = "SELECT *FROM tbl_MetaData_DEPOT where DistrictId= '23" + ddldistrict.SelectedValue.ToString() + "' and DepotID='" + ddlIssueID.SelectedValue.ToString() +"' ";
        SqlCommand cmd = new SqlCommand(issuecentre, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);

        ddlissuecenter.DataSource = ds.Tables[0];

        ddlissuecenter.DataTextField = "DepotName";
        ddlissuecenter.DataValueField = "DepotID";
        ddlissuecenter.DataBind();
        ddlissuecenter.Items.Insert(0, "--Select--"); 

    }

    protected void GetCrop()
    {

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        string cropname = "SELECT Proc_Commodity_Id  ,Commodity_Name FROM Procurement_COMMODITY where Proc_Commodity_Id in('1','2','3','4','5','6') ";
        SqlCommand cmd = new SqlCommand(cropname, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);

        ddlcrop.DataSource = ds.Tables[0];


        ddlcrop.DataTextField = "Commodity_Name";
        ddlcrop.DataValueField = "Proc_Commodity_Id";
        ddlcrop.DataBind();
        ddlcrop.Items.Insert(0, "--Select--");


        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    protected void ddlIssueID_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Recdate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Pls Select Date'); </script> ");
            return;  
        }


        if (ddlcrop.SelectedValue == "0")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Pls Select Crop'); </script> ");
            return;  
        }

   else
            # region Wheat
            if (ddlcrop.SelectedValue.ToString() == "1")
            {
                //string data = "SELECT TruckChalanNo ,TruckNo ,Recd_Bags ,Recv_Qty ,convert(nvarchar,Recd_Date,103)Recd_Date FROM IssueCenterReceipt_Online where Receipt_Id = '" + ddlIssueID.SelectedValue + "' and Receipt_Id not in (select IssueID from Acceptance_Note_Detail where Distt_ID = '23" + ddldistrict.SelectedValue.ToString() + "' and  IssueCenter_ID = '" + ddlissuecenter.SelectedValue + "'  and CommodityId = '" + ddlcrop.SelectedValue + "' )";

                string data = "SELECT TruckChalanNo ,TruckNo ,Recd_Bags ,Recv_Qty ,convert(nvarchar,Recd_Date,103)Recd_Date FROM IssueCenterReceipt_Online where Receipt_Id = '" + ddlIssueID.SelectedValue + "'";

                
                if (con_WPMS.State == ConnectionState.Closed)
                {

                    con_WPMS.Open();
                }

                SqlCommand cmddata = new SqlCommand(data, con_WPMS);

                SqlDataAdapter da = new SqlDataAdapter(cmddata);
                DataSet ds = new DataSet();

                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView2.DataSource = ds.Tables[0];

                    GridView2.DataBind();

                    lblchallan.Text = ds.Tables[0].Rows[0]["TruckChalanNo"].ToString();
                }

                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Acceptance Note Issued, Can not Delete This Entry'); </script> ");
                    return;
                }

                if (con_WPMS.State == ConnectionState.Open)
                {
                    con_WPMS.Close();
                }

            }

            # endregion

   else
            
            # region Paddy

                if (ddlcrop.SelectedValue.ToString() == "2" || ddlcrop.SelectedValue.ToString() == "3")
                {
                    string data = "SELECT TruckChalanNo ,TruckNo ,Recd_Bags ,Recv_Qty ,convert(nvarchar,Recd_Date,103)Recd_Date FROM IssueCenterReceipt_Online where Receipt_Id = '" + ddlIssueID.SelectedValue + "' and Receipt_Id not in (select IssueID from Acceptance_Note_Detail where Distt_ID = '23" + ddldistrict.SelectedValue.ToString() + "' and  IssueCenter_ID = '" + ddlissuecenter.SelectedValue + "'  and CommodityId = '" + ddlcrop.SelectedValue + "' )";

                   
                    if (con_paddy.State == ConnectionState.Closed)
                    {

                        con_paddy.Open();
                    }

                    SqlCommand cmddata = new SqlCommand(data, con_paddy);

                    SqlDataAdapter da = new SqlDataAdapter(cmddata);
                    DataSet ds = new DataSet();

                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GridView2.DataSource = ds.Tables[0];

                        GridView2.DataBind();

                        lblchallan.Text = ds.Tables[0].Rows[0]["TruckChalanNo"].ToString();
                    }

                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Acceptance Note Issued, Can not Delete This Entry'); </script> ");
                        return;
                    }

                    if (con_paddy.State == ConnectionState.Open)
                    {
                        con_paddy.Close();
                    }
                }
                # endregion

   else
            
            # region Maize

                    if (ddlcrop.SelectedValue.ToString() == "4" || ddlcrop.SelectedValue.ToString() == "5" || ddlcrop.SelectedValue.ToString() == "6" || ddlcrop.SelectedValue.ToString() == "7" || ddlcrop.SelectedValue.ToString() == "8")
                    {
                       string data = "SELECT TruckChalanNo ,TruckNo ,Recd_Bags ,Recv_Qty ,convert(nvarchar,Recd_Date,103)Recd_Date FROM IssueCenterReceipt_Online where Receipt_Id = '" + ddlIssueID.SelectedValue + "' and Receipt_Id not in (select IssueID from Acceptance_Note_Detail where Distt_ID = '23" + ddldistrict.SelectedValue.ToString() + "' and  IssueCenter_ID = '" + ddlissuecenter.SelectedValue + "'  and CommodityId = '" + ddlcrop.SelectedValue + "' )";

                        if (con_Maze.State == ConnectionState.Closed)
                        {

                            con_Maze.Open();
                        }

                        SqlCommand cmddata = new SqlCommand(data, con_Maze);

                        SqlDataAdapter da = new SqlDataAdapter(cmddata);
                        DataSet ds = new DataSet();

                        da.Fill(ds);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            GridView2.DataSource = ds.Tables[0];

                            GridView2.DataBind();

                            lblchallan.Text = ds.Tables[0].Rows[0]["TruckChalanNo"].ToString();
                        }

                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Acceptance Note Issued, Can not Delete This Entry'); </script> ");
                            return;
                        }

                        if (con_Maze.State == ConnectionState.Open)
                        {
                            con_Maze.Close();
                        }
                    }
                    # endregion

   }

    protected void ddlcrop_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (ddlcrop.SelectedValue == "0" || ddlcrop.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Plz Select Crop Type'); </script> ");
            return;

        }

        mcomdtyu = ddlcrop.SelectedValue;

        

            # region Wheat

        if (ddlcrop.SelectedValue.ToString() == "1")   // Wheat
        {
            if (con_WPMS.State == ConnectionState.Closed)
            {
                con_WPMS.Open();
            }

            if (Recdate.Text == "")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Plz Select Date'); </script> ");
                return;
            }

            else
            {
                try
                {

                    string redate = getDate_MDY(Recdate.Text);

                    string ReceiptId = "SELECT Receipt_Id +'  ('+ TruckChalanNo + ')' ID, Receipt_Id FROM IssueCenterReceipt_Online where DistrictId = '23" + ddldistrict.SelectedValue.ToString() + "' and IssueCenter_ID = '" + ddlissuecenter.SelectedValue + "' and CommodityId = '" + ddlcrop.SelectedValue + "' and Recd_Date = '" + redate + "' ";
                    SqlCommand cmd = new SqlCommand(ReceiptId, con_WPMS);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    ddlIssueID.DataSource = ds.Tables[0];

                    ddlIssueID.DataTextField = "ID";
                    ddlIssueID.DataValueField = "Receipt_Id";
                    ddlIssueID.DataBind();
                    ddlIssueID.Items.Insert(0, "--Select--");
                }

                catch
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Something Going Wrong'); </script> ");
                }

                if (con_WPMS.State == ConnectionState.Open)
                {
                    con_WPMS.Close();
                }

            }
        }
        # endregion

       else

            # region Paddy

            if (ddlcrop.SelectedValue.ToString() == "2" || ddlcrop.SelectedValue.ToString() == "3")   // Paddy
            {

                if (con_paddy.State == ConnectionState.Closed)
                {
                    con_paddy.Open();
                }

                if (Recdate.Text == "")
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Plz Select Date'); </script> ");
                    return;
                }

                else
                {
                    try
                    {

                        string redate = getDate_MDY(Recdate.Text);

                        mcomdtyu = ddlcrop.SelectedValue;

                        string ReceiptId = "SELECT Receipt_Id +'  ('+ TruckChalanNo + ')' ID, Receipt_Id FROM IssueCenterReceipt_Online where DistrictId = '23" + ddldistrict.SelectedValue.ToString() + "' and IssueCenter_ID = '" + ddlissuecenter.SelectedValue + "' and CommodityId = '" + ddlcrop.SelectedValue + "' and Recd_Date = '" + redate + "'";
                        SqlCommand cmd = new SqlCommand(ReceiptId, con_paddy);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        ddlIssueID.DataSource = ds.Tables[0];

                        ddlIssueID.DataTextField = "ID";
                        ddlIssueID.DataValueField = "Receipt_Id";
                        ddlIssueID.DataBind();
                        ddlIssueID.Items.Insert(0, "--Select--");
                    }

                    catch
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Something Going Wrong'); </script> ");
                    }

                    if (con_paddy.State == ConnectionState.Open)
                    {
                        con_paddy.Close();
                    }

                }
            }

            # endregion

       else

            # region CoarseGrain

                if (ddlcrop.SelectedValue.ToString() == "4" || ddlcrop.SelectedValue.ToString() == "5" || ddlcrop.SelectedValue.ToString() == "6" || ddlcrop.SelectedValue.ToString() == "7" || ddlcrop.SelectedValue.ToString() == "8")  // Maize
                {
                    if (con_Maze.State == ConnectionState.Closed)
                    {
                        con_Maze.Open();
                    }

                    if (Recdate.Text == "")
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Plz Select Date'); </script> ");
                        return;
                    }

                    else
                    {
                        try
                        {

                            string redate = getDate_MDY(Recdate.Text);

                            mcomdtyu = ddlcrop.SelectedValue;

                            string ReceiptId = "SELECT Receipt_Id +'  ('+ TruckChalanNo + ')' ID, Receipt_Id FROM IssueCenterReceipt_Online where DistrictId = '23" + ddldistrict.SelectedValue.ToString() + "' and IssueCenter_ID = '" + ddlissuecenter.SelectedValue + "' and CommodityId = '" + ddlcrop.SelectedValue + "' and Recd_Date = '" + redate + "'  ";
                            SqlCommand cmd = new SqlCommand(ReceiptId, con_Maze);
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            ddlIssueID.DataSource = ds.Tables[0];

                            ddlIssueID.DataTextField = "ID";
                            ddlIssueID.DataValueField = "Receipt_Id";
                            ddlIssueID.DataBind();
                            ddlIssueID.Items.Insert(0, "--Select--");
                        }

                        catch
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Something Going Wrong'); </script> ");
                        }

                        if (con_Maze.State == ConnectionState.Open)
                        {
                            con_Maze.Close();
                        }

                    }
                }
                # endregion

        
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        btnDelete.Enabled = true;
        Response.Redirect("~/State/Deletefrm_Procurement.aspx");
    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/State/State_Welcome.aspx");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

     # region Wheat

        if (ddlcrop.SelectedValue.ToString() == "1")
        {
            if (con_WPMS.State == ConnectionState.Closed)
            {
                con_WPMS.Open();
            }

            try
            {

                if (ddlIssueID.SelectedValue == "0")
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Select Issue ID'); </script> ");
                    return;
                }

                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                string insWlog = "Insert into IssueCenterReceipt_Online_Log select * from IssueCenterReceipt_Online where IssueID = '" + ddlIssueID.SelectedValue + "' and DistrictId = '23" + ddldistrict.SelectedValue + "' and IssueCenter_ID = '" + ddlissuecenter.SelectedValue + "'";

                SqlCommand cmdWlog = new SqlCommand(insWlog, con_WPMS);

                int x = cmdWlog.ExecuteNonQuery();


                string delWqry = "delete from IssueCenterReceipt_Online where IssueID = '" + ddlIssueID.SelectedValue + "' and DistrictId = '23" + ddldistrict.SelectedValue.ToString() + "' and IssueCenter_ID = '" + ddlissuecenter.SelectedValue + "'";

                SqlCommand cmdWdel = new SqlCommand(delWqry, con_WPMS);

                int y = cmdWdel.ExecuteNonQuery();

                if (y == 0)
                {
                    lblwp_oper.Text = "Not Del from WPMS";
                }
                else if (y > 0)
                {

                    string selcheck = "select count(*) from SCSC_Procurement where Receipt_Id = '" + ddlIssueID.SelectedValue + "' and Distt_ID = '" + ddldistrict.SelectedValue + "' and IssueCenter_ID = '" + ddlissuecenter.SelectedValue + "'";
                    SqlCommand cmdcheck = new SqlCommand(selcheck, con);

                    string z = cmdcheck.ExecuteScalar().ToString();

                    int chlog = Convert.ToInt16(z);

                    if (chlog > 0)
                    {
                        string inslog = "Insert into SCSC_Procurement_dellog select * from SCSC_Procurement where Receipt_Id = '" + ddlIssueID.SelectedValue + "' and Distt_ID = '" + ddldistrict.SelectedValue + "' and IssueCenter_ID = '" + ddlissuecenter.SelectedValue + "'";

                        SqlCommand cmdlog = new SqlCommand(inslog, con);

                        int a = cmdlog.ExecuteNonQuery();

                        string delqry = "delete from SCSC_Procurement where Receipt_Id = '" + ddlIssueID.SelectedValue + "' and Distt_ID = '" + ddldistrict.SelectedValue + "' and IssueCenter_ID = '" + ddlissuecenter.SelectedValue + "'";

                        SqlCommand cmddel = new SqlCommand(delqry, con);

                        int b = cmddel.ExecuteNonQuery();

                        if (b == 0)
                        {
                            lbloperation.Text = "Not Del from CSMS";
                        }

                        else
                        {
                            lbloperation.Text = "";

                            string updateqry = "Update SCSC_Procurement_dellog set OperatorID = 'HO' , Deleted_Date = GETDATE(),IP_Address = '" + ip + "'  where Receipt_Id = '" + ddlIssueID.SelectedValue + "' and Distt_ID = '" + ddldistrict.SelectedValue + "' and IssueCenter_ID = '" + ddlissuecenter.SelectedValue + "'";

                            SqlCommand cmdup = new SqlCommand(updateqry, con);

                            int c = cmdup.ExecuteNonQuery();
                        }
                    }

                }

                else
                {
                    lblwp_oper.Text = "";
                }


                //UpdateCBalance();
                //UpdateStock();

                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Deleted Sucessfully'); </script> ");
            }

            catch (Exception ex)
            {
                lblerror.Text = ex.Message;
                lblerror.Visible = true;

                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error! To Delete This Entry'); </script> ");
                return;
            }

            finally
            {
                btnDelete.Enabled = false;

                if (con_WPMS.State == ConnectionState.Open)
                {
                    con_WPMS.Close();
                }

                
            }

        }

# endregion

        else

     # region paddy

            if (ddlcrop.SelectedValue.ToString() == "2" || ddlcrop.SelectedValue.ToString() == "3")
            {
                if (con_paddy.State == ConnectionState.Closed)
                {
                    con_paddy.Open();
                }

                try
                {

                    if (ddlIssueID.SelectedValue == "0")
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Select Issue ID'); </script> ");
                        return;
                    }

                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                    string insWlog = "Insert into IssueCenterReceipt_Online_Log select * from IssueCenterReceipt_Online where IssueID = '" + ddlIssueID.SelectedValue + "' and DistrictId = '23" + ddldistrict.SelectedValue.ToString() + "' and IssueCenter_ID = '" + ddlissuecenter.SelectedValue + "'";

                    SqlCommand cmdWlog = new SqlCommand(insWlog, con_paddy);

                    int x = cmdWlog.ExecuteNonQuery();


                    string delWqry = "delete from IssueCenterReceipt_Online where IssueID = '" + ddlIssueID.SelectedValue + "' and DistrictId = '23" + ddldistrict.SelectedValue.ToString() + "' and IssueCenter_ID = '" + ddlissuecenter.SelectedValue + "'";

                    SqlCommand cmdWdel = new SqlCommand(delWqry, con_paddy);

                    int y = cmdWdel.ExecuteNonQuery();

                    if (y == 0)
                    {
                        lblwp_oper.Text = "Not Del from WPMS";
                    }
                    else if (y > 0)
                    {

                        string selcheck = "select count(*) from SCSC_Procurement where Receipt_Id = '" + ddlIssueID.SelectedValue + "' and Distt_ID = '" + ddldistrict.SelectedValue.ToString() + "' and IssueCenter_ID = '" + ddlissuecenter.SelectedValue + "'";
                        SqlCommand cmdcheck = new SqlCommand(selcheck, con);

                        string z = cmdcheck.ExecuteScalar().ToString();

                        int chlog = Convert.ToInt16(z);

                        if (chlog > 0)
                        {
                            string inslog = "Insert into SCSC_Procurement_dellog select * from SCSC_Procurement where Receipt_Id = '" + ddlIssueID.SelectedValue + "' and Distt_ID = '" + ddldistrict.SelectedValue.ToString() + "' and IssueCenter_ID = '" + ddlissuecenter.SelectedValue + "'";

                            SqlCommand cmdlog = new SqlCommand(inslog, con);

                            int a = cmdlog.ExecuteNonQuery();

                            string delqry = "delete from SCSC_Procurement where Receipt_Id = '" + ddlIssueID.SelectedValue + "' and Distt_ID = '" + ddldistrict.SelectedValue.ToString() + "' and IssueCenter_ID = '" + ddlissuecenter.SelectedValue + "'";

                            SqlCommand cmddel = new SqlCommand(delqry, con);

                            int b = cmddel.ExecuteNonQuery();

                            if (b == 0)
                            {
                                lbloperation.Text = "Not Del from CSMS";
                            }

                            else
                            {
                                lbloperation.Text = "";

                                string updateqry = "Update SCSC_Procurement_dellog set OperatorID = 'Del by HO' , Deleted_Date = GETDATE(),IP_Address = '" + ip + "'  where Receipt_Id = '" + ddlIssueID.SelectedValue + "' and Distt_ID = '" + ddldistrict.SelectedValue.ToString() + "' and IssueCenter_ID = '" + ddlissuecenter.SelectedValue + "'";

                                SqlCommand cmdup = new SqlCommand(updateqry, con);

                                int c = cmdup.ExecuteNonQuery();
                            }
                        }

                    }

                    else
                    {
                        lblwp_oper.Text = "";
                    }


                    //UpdateCBalance();
                    //UpdateStock();

                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Deleted Sucessfully'); </script> ");
                }

                catch (Exception ex)
                {
                    lblerror.Text = ex.Message;
                    lblerror.Visible = true;

                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error! To Delete This Entry'); </script> ");
                    return;
                }

                finally
                {
                    btnDelete.Enabled = false;

                    if (con_paddy.State == ConnectionState.Open)
                    {
                        con_paddy.Close();
                    }


                }
            }
            # endregion

            else

      # region Maize

                if (ddlcrop.SelectedValue.ToString() == "4" || ddlcrop.SelectedValue.ToString() == "5" || ddlcrop.SelectedValue.ToString() == "6" || ddlcrop.SelectedValue.ToString() == "7" || ddlcrop.SelectedValue.ToString() == "8")
                {
                    if (con_Maze.State == ConnectionState.Closed)
                    {
                        con_Maze.Open();
                    }

                    try
                    {

                        if (ddlIssueID.SelectedValue == "0")
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Select Issue ID'); </script> ");
                            return;
                        }

                        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                        string insWlog = "Insert into IssueCenterReceipt_Online_Log select * from IssueCenterReceipt_Online where IssueID = '" + ddlIssueID.SelectedValue + "' and DistrictId = '23" + ddldistrict.SelectedValue.ToString() + "' and IssueCenter_ID = '" + ddlissuecenter.SelectedValue + "'";

                        SqlCommand cmdWlog = new SqlCommand(insWlog, con_Maze);

                        int x = cmdWlog.ExecuteNonQuery();


                        string delWqry = "delete from IssueCenterReceipt_Online where IssueID = '" + ddlIssueID.SelectedValue + "' and DistrictId = '23" + ddldistrict.SelectedValue.ToString() + "' and IssueCenter_ID = '" + ddlissuecenter.SelectedValue + "'";

                        SqlCommand cmdWdel = new SqlCommand(delWqry, con_Maze);

                        int y = cmdWdel.ExecuteNonQuery();

                        if (y == 0)
                        {
                            lblwp_oper.Text = "Not Del from WPMS";
                        }
                        else if (y > 0)
                        {

                            string selcheck = "select count(*) from SCSC_Procurement where Receipt_Id = '" + ddlIssueID.SelectedValue+ "' and Distt_ID = '" + ddldistrict.SelectedValue.ToString() + "' and IssueCenter_ID = '" + ddlissuecenter.SelectedValue + "'";
                            SqlCommand cmdcheck = new SqlCommand(selcheck, con);

                            string z = cmdcheck.ExecuteScalar().ToString();

                            int chlog = Convert.ToInt16(z);

                            if (chlog > 0)
                            {
                                string inslog = "Insert into SCSC_Procurement_dellog select * from SCSC_Procurement where Receipt_Id = '" + ddlIssueID.SelectedValue + "' and Distt_ID = '" + ddldistrict.SelectedValue.ToString() + "' and IssueCenter_ID = '" + ddlissuecenter.SelectedValue + "'";

                                SqlCommand cmdlog = new SqlCommand(inslog, con);

                                int a = cmdlog.ExecuteNonQuery();

                                string delqry = "delete from SCSC_Procurement where Receipt_Id = '" + ddlIssueID.SelectedValue + "' and Distt_ID = '" + ddldistrict.SelectedValue.ToString() + "' and IssueCenter_ID = '" + ddlissuecenter.SelectedValue + "'";

                                SqlCommand cmddel = new SqlCommand(delqry, con);

                                int b = cmddel.ExecuteNonQuery();

                                if (b == 0)
                                {
                                    lbloperation.Text = "Not Del from CSMS";
                                }

                                else
                                {
                                    lbloperation.Text = "";

                                    string updateqry = "Update SCSC_Procurement_dellog set OperatorID = 'Del by HO' , Deleted_Date = GETDATE(),IP_Address = '" + ip + "'  where Receipt_Id = '" + ddlIssueID.SelectedValue + "' and Distt_ID = '" + ddldistrict.SelectedValue.ToString() + "' and IssueCenter_ID = '" + ddlissuecenter.SelectedValue + "'";

                                    SqlCommand cmdup = new SqlCommand(updateqry, con);

                                    int c = cmdup.ExecuteNonQuery();
                                }
                            }

                        }

                        else
                        {
                            lblwp_oper.Text = "";
                        }


                        //UpdateCBalance();
                        //UpdateStock();

                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Deleted Sucessfully'); </script> ");
                    }

                    catch (Exception ex)
                    {
                        lblerror.Text = ex.Message;
                        lblerror.Visible = true;

                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error! To Delete This Entry'); </script> ");
                        return;
                    }

                    finally
                    {
                        btnDelete.Enabled = false;

                        if (con_Maze.State == ConnectionState.Open)
                        {
                            con_Maze.Close();
                        }


                    }
                }

                # endregion


        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    //void UpdateStock()
    //{

    //    if (con.State == ConnectionState.Closed)
    //    {
    //        con.Open();
    //    }

    //    string mfyear = DateTime.Today.Year.ToString();
   
    //    int monthu = int.Parse(DateTime.Today.Month.ToString());
    //    int yearu = int.Parse(DateTime.Today.Year.ToString());
    //    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

       
    //    decimal mrfci = CheckNull(lblRecQty.Text);

       
    //    string qryinsU = "update dbo.tbl_Stock_Registor set Recieved_Procure= convert(decimal(18,5), Recieved_Procure)-" + mrfci + " where Commodity_Id ='" + mcomdtyu + "' and Scheme_ID='" + mscheme + "' and DistrictId='" + ddldistrict.SelectedValue.ToString() + "'and DepotID='" + ddlissuecenter.SelectedValue + "'and Month=" + monthu + "and Year=" + yearu;

    //    SqlCommand cmd = new SqlCommand(qryinsU, con);
    //            try
    //            {            
    //                cmd.ExecuteNonQuery();                
    //            }
    //            catch (Exception ex)
    //            {
    //                Label9.Visible = true;
    //                Label9.Text = "error:5" + ex.Message;

    //            }
    //            finally
    //            {
                    

    //            }

    //            if (con.State == ConnectionState.Open)
    //            {
    //                con.Close();
    //            }
            
    //}

    //void UpdateCBalance()
    //{

    //    string query = "Update dbo.issue_opening_balance set Current_Balance = convert(decimal(18,5), Current_Balance)-" + CheckNull(lblRecQty.Text) + ",Current_Bags=Current_Bags-" + lblRecBags.Text + " where District_Id='" + ddldistrict.SelectedValue.ToString() + "'and Depotid='" + ddlissuecenter.SelectedValue + "'and Commodity_Id='" + mcomdtyu + "'and Godown='" + lblgdnId.Text + "' and Scheme_Id='" + mscheme + "' and Source='01' ";
    //    SqlCommand cmd = new SqlCommand();       
    //    cmd.CommandText = query;
    //   cmd.Connection = con;

    //            try
    //            {
    //                if (con.State == ConnectionState.Closed)
    //                {
    //                    con.Open();
    //                }
    //                cmd.ExecuteNonQuery();

    //            }
    //            catch (Exception ex)
    //            {
    //                Label9.Visible = true;
    //                Label9.Text = "error:3" + ex.Message;

    //            }
    //            finally
    //            {
    //                if (con.State == ConnectionState.Open)
    //                {
    //                    con.Close();
    //                }                 

    //            }        
     
    //}
      

    decimal CheckNull(string Val)
    {

        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        decimal ValF = decimal.Parse(ValS);
        return ValF;


    }

    Int32 CheckNullInt(string Val)
    {

        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        int ValF = int.Parse(ValS);
        return ValF;

    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }


    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldistrict.SelectedValue == "0" || ddldistrict.SelectedItem.Text == "--Select--")
        {

        }

        else
        {
            distid = ddldistrict.SelectedValue;
                 

        try
        {
            string qry = "Select DepotName,DepotID from dbo.tbl_MetaData_DEPOT where DistrictId='23" + ddldistrict.SelectedValue.ToString() +"'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlissuecenter.DataSource = ds.Tables[0];
                ddlissuecenter.DataTextField = "DepotName";
                ddlissuecenter.DataValueField = "DepotID";
                ddlissuecenter.DataBind();
                ddlissuecenter.SelectedIndex = 0;
            }
        }
        catch (Exception)
        {
            //////
        }
    }
        
    
    }

    void GetDCName()
    {

        ddlissuecenter.Items.Clear();
        distobj = new DistributionCenters(ComObj);
        string ord = "Districtid='23" + ddldistrict.SelectedValue.ToString() + "' order by DepotName";
        DataSet ds = distobj.select(ord);
        if (ds == null)
        {
        }
        else
        {
            ddlissuecenter.DataSource = ds.Tables[0];
            ddlissuecenter.DataTextField = "DepotName";
            ddlissuecenter.DataValueField = "DepotId";

            ddlissuecenter.DataBind();
        }
        // ddDistId.Items.Insert(0, "--चुनिये--");

    }

  public void GetDist()
    {
        try
        {
            string qry = "SELECT district_code ,district_name FROM pds.districtsmp order by district_name";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddldistrict.DataSource = ds.Tables[0];
                ddldistrict.DataTextField = "district_name";
                ddldistrict.DataValueField = "district_code";
                ddldistrict.DataBind();
                ddldistrict.Items.Insert(0, "--Select--");
              
            }
        }
        catch (Exception)
        {
            //////
        }
    }

    protected void ddlissuecenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCrop();

        Recdate.Text = "";

    }
}
