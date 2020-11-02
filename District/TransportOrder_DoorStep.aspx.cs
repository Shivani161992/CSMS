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

public partial class District_TransportOrder_DoorStep : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    public string distid = "";

    public  string IssuecenterID;

    DataTable Dt1 = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["dist_id"] != null)
            {
                distid = Session["dist_id"].ToString();


                if (!IsPostBack)
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    Session["issubmited"] = "No";

                    txtAllotQty.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");

                    txtTranpDate.Attributes.Add("onkeypress", "return CheckCalDate(event,this);");

                    txtvalid.Attributes.Add("onkeypress", "return CheckCalDate(event,this);");

                   
                    get_IssueCenter();

                    get_Transporter();

                    get_Commodity();

                   
                    ddlyear.Items.Add(DateTime.Today.Year.ToString());
                    ddlyear.Items.Add((int.Parse(DateTime.Today.Year.ToString()) + 1).ToString());

                    ddlyear.Items.Insert(0, "--Select--");


                    hlinkpdo.Attributes.Add("onclick", "window.open('Print_DPY_TransportOrder.aspx',null,'left=800, top=800, height=900, width= 800, status=n o, resizable= yes, scrollbars= yes, toolbar= no,location= no, menubar= no');");

                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            else
            {
                Response.Redirect("~/MainLogin.aspx");
            }
        }

        catch (Exception ex)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            Response.Write(ex.Message);
        }
    }

    protected void get_IssueCenter()
    {

        string qry = "select DepotID , DepotName from tbl_MetaData_DEPOT where DistrictId = 23"+distid+"";

        SqlCommand cmd = new SqlCommand(qry, con);

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();

        da.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlissueCenter.DataSource = ds.Tables[0];
            ddlissueCenter.DataTextField = "DepotName";
            ddlissueCenter.DataValueField = "DepotID";
            ddlissueCenter.DataBind();
            ddlissueCenter.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlissueCenter.DataSource = "";

            ddlissueCenter.DataBind();

            ddlissueCenter.Items.Insert(0, "--Select--");
            
        }
    }

    protected void get_Transporter()
    {
        string qry = "select Transporter_Name , Transporter_ID from Transporter_Table where Distt_ID = '" + distid + "' and Transport_ID = 7";

        SqlCommand cmd = new SqlCommand(qry, con);

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();

        da.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddltransporter.DataSource = ds.Tables[0];
            ddltransporter.DataTextField = "Transporter_Name";
            ddltransporter.DataValueField = "Transporter_ID";
            ddltransporter.DataBind();
            ddltransporter.Items.Insert(0, "--Select--");
        }
        else
        {
            ddltransporter.DataSource = "";

            ddltransporter.DataBind();

            ddltransporter.Items.Insert(0, "--Select--");

        }

    }

    protected void get_Commodity()
    {
        string qry = "select Commodity_Id , Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id in ('22','19','23','3','4') and Status = 'Y'";

        SqlCommand cmd = new SqlCommand(qry, con);

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
        else
        {
            ddlcommodtiy.DataSource = "";

            ddlcommodtiy.DataBind();

            ddlcommodtiy.Items.Insert(0, "--Select--");

        }
    }

    protected void get_RootChart()
    {
        if (ddltransporter.SelectedIndex == 0 || ddltransporter.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Select Transporter');</script>");
            return;
        }

        else
        {
            string qry = "Select distinct root_no from tbl_rootchart_master where Transporter_id = '" + ddltransporter.SelectedValue + "'";

            SqlCommand cmd = new SqlCommand(qry, con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlrootnumber.DataSource = ds.Tables[0];
                ddlrootnumber.DataTextField = "root_no";
                ddlrootnumber.DataValueField = "root_no";
                ddlrootnumber.DataBind();
                ddlrootnumber.Items.Insert(0, "--Select--");
            }
            else
            {
                ddlrootnumber.DataSource = "";

                ddlrootnumber.DataBind();

                ddlrootnumber.Items.Insert(0, "--Select--");

            }
        }

        

    }

    protected void get_FeedNumber()
    {
        string qry = "Select distinct feed_no from tbl_rootchart_master where root_no = '" + ddlrootnumber.SelectedValue + "' and DistrictId = '" + distid + "'";

        SqlCommand cmd = new SqlCommand(qry, con);

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();

        da.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlfeednumber.DataSource = ds.Tables[0];
            ddlfeednumber.DataTextField = "feed_no";
            ddlfeednumber.DataValueField = "feed_no";
            ddlfeednumber.DataBind();
            ddlfeednumber.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlfeednumber.DataSource = "";

            ddlfeednumber.DataBind();

            ddlfeednumber.Items.Insert(0, "--Select--");

        }
    }

    protected void ddltransporter_SelectedIndexChanged(object sender, EventArgs e)
    {
        get_RootChart();
    }

    protected void ddlrootnumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlrootnumber.SelectedIndex == 0 || ddlrootnumber.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('');</script>");
            return;
        }
        else
        {
            get_FeedNumber();

        }
        
    }

    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlyear.SelectedItem.Text == "--Select--")
        {

        }

        else
        {
            // get allotment quantity
        }
    }

    protected void btnCLose_Click(object sender, EventArgs e)
    {
        Session["dt1"] = null;

        Session["issubmited"] = "No";

        Response.Redirect("~/District/Dist_Welcome.aspx");
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Session["issubmited"] = "No";

        Session["dt1"] = null;


        Response.Redirect("~/District/TransportOrder_DoorStep.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlyear.SelectedIndex == 0 || ddlyear.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Allotment Year');</script>");
            return;
        }

        if (ddltransporter.SelectedIndex == 0 || ddltransporter.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Transporter Name');</script>");
            return;
        }

        if (ddlrootnumber.SelectedIndex == 0 || ddlrootnumber.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Root Number');</script>");
            return;
        }

        if (ddlfeednumber.SelectedIndex == 0 || ddlfeednumber.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Feed Number');</script>");
            return;
        }

        if (ddlissueCenter.SelectedIndex == 0 || ddlissueCenter.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Issue Center Name');</script>");
            return;
        }

        if (ddlcommodtiy.SelectedIndex == 0 || ddlcommodtiy.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Transporter Name');</script>");
            return;
        }

        if (txtAllotQty.Text == "0" || txtAllotQty.Text == "")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Insert Allotment Quantity');</script>");
            return;
        }

        if (txtTranpDate.Text == "")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Transport order Date from Calender');</script>");
            return;
        }


        if (txtvalid.Text == "")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Transportation Validity Date from Calender');</script>");
            return;
        }

        else
        {
            if (Session["issubmited"] == "Yes")
            {

            }

            else
            {
                try
                {
                    btnSave.Enabled = false;

                    string issueCode = ddlissueCenter.SelectedValue;

                    string AllotMonth = ddlmonth.SelectedItem.Value;


                    string AllotYear = ddlyear.SelectedItem.Value;

                    string Transporter = ddltransporter.SelectedValue;

                    string TOrder_Date = getDate_MDY(txtTranpDate.Text);

                    string Valid_Date = getDate_MDY(txtvalid.Text);

                    string RootNum = ddlrootnumber.SelectedValue;

                    string FeedNum = ddlfeednumber.SelectedValue;

                    // TO Number Auto Generate   == Isscode + Allot Month + Allot Year  + Increment Number 4 digit

                    string selectmax = "select max(cast(TransportOrder as bigint)) as ToNum from DPY_TranportOrder where DistrictId='" + distid + "' and IssueCenter = '" + issueCode + "' ";

                    SqlCommand cmdmax = new SqlCommand(selectmax, con);
                    SqlDataAdapter damax = new SqlDataAdapter(cmdmax);

                    DataSet dsmax = new DataSet();

                    damax.Fill(dsmax);


                    string TO_Num = dsmax.Tables[0].Rows[0]["ToNum"].ToString();

                    if (TO_Num == "")
                    {
                        TO_Num = issueCode + AllotMonth + AllotYear + "1000";
                    }
                    else
                    {
                        string forto = TO_Num.Substring(TO_Num.Length - 4);

                        Int64 TO_Num_new = Convert.ToInt64(forto);

                        TO_Num_new = TO_Num_new + 1;

                        string combine = TO_Num_new.ToString();

                        TO_Num = issueCode + AllotMonth + AllotYear + combine;
                    }

                    string IP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                    string user = Session["OperatorIDDM"].ToString();

                    int y = 0;

                    for (int i = 0; GridView2.Rows.Count > i; i++)
                    {
                        string Commodity = GridView2.DataKeys[i].Values[0].ToString();

                        string FPS_Code = GridView2.DataKeys[i].Values[1].ToString();

                        string AQty = GridView2.Rows[i].Cells[2].Text.ToString();

                        double AllotQty = Convert.ToDouble(AQty);

                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }


                        string chck = "Select count(TransportOrder) from DPY_TranportOrder where TransportOrder = '" + TO_Num + "' and DistrictId = '" + distid + "' and IssueCenter = '" + issueCode + "' and FPSCode = '" + FPS_Code + "'";

                        SqlCommand cmdcheck = new SqlCommand(chck, con);

                        string str1 = cmdcheck.ExecuteScalar().ToString();

                        if (Convert.ToInt16(str1) == 0)   // not Found, Insert start
                        {
                            string insqry = "Insert into DPY_TranportOrder (DistrictId ,IssueCenter ,TransportOrder ,TransporterId ,AllotMonth ,AllotYear ,RouteNumber ,FeedNumber,FPSCode,TransportDate ,ValidityDate ,CreatedDate ,IP ,OPId,SMS,DO_Challan) Values ('" + distid + "','" + issueCode + "','" + TO_Num + "','" + Transporter + "','" + AllotMonth + "','" + AllotYear + "','" + RootNum + "','" + FeedNum + "','" + FPS_Code + "' , '" + TOrder_Date + "','" + Valid_Date + "',getdate(),'" + IP + "','" + user + "','N','N')";

                            SqlCommand cmdinsert = new SqlCommand(insqry, con);

                            int x = cmdinsert.ExecuteNonQuery();

                            if (x > 0)
                            {

                                y = y + 1;
                            }
                        }



                        if (Commodity == "22")
                        {
                            // Update wheat

                            string upt = "Update DPY_TranportOrder set Comm_W = " + Commodity + " , WheatAllot = " + AllotQty + " where TransportOrder = '" + TO_Num + "' and DistrictId = '" + distid + "' and IssueCenter = '" + issueCode + "' and AllotMonth = '" + AllotMonth + "' and FPSCode = '" + FPS_Code + "' and RouteNumber = '" + RootNum + "'";
                            SqlCommand cmdup = new SqlCommand(upt, con);

                            cmdup.ExecuteNonQuery();
                        }

                        if (Commodity == "3" || Commodity == "4")
                        {
                            // Update Rice

                            string upt = "Update DPY_TranportOrder set Comm_R = " + Commodity + " , RiceAllot = " + AllotQty + " where TransportOrder = '" + TO_Num + "' and DistrictId = '" + distid + "' and IssueCenter = '" + issueCode + "' and AllotMonth = '" + AllotMonth + "' and FPSCode = '" + FPS_Code + "' and RouteNumber = '" + RootNum + "'";
                            SqlCommand cmdup = new SqlCommand(upt, con);

                            cmdup.ExecuteNonQuery();
                        }


                        if (Commodity == "23")
                        {
                            // Update Sugar

                            string upt = "Update DPY_TranportOrder set Comm_Sug = " + Commodity + " , SugarAllot = " + AllotQty + " where TransportOrder = '" + TO_Num + "' and DistrictId = '" + distid + "' and IssueCenter = '" + issueCode + "' and AllotMonth = '" + AllotMonth + "' and FPSCode = '" + FPS_Code + "' and RouteNumber = '" + RootNum + "'";
                            SqlCommand cmdup = new SqlCommand(upt, con);

                            cmdup.ExecuteNonQuery();
                        }


                        if (Commodity == "19")
                        {
                            // Update Salt

                            string upt = "Update DPY_TranportOrder set Comm_Salt = " + Commodity + " , SaltAllot = " + AllotQty + " where TransportOrder = '" + TO_Num + "' and DistrictId = '" + distid + "' and IssueCenter = '" + issueCode + "' and AllotMonth = '" + AllotMonth + "' and FPSCode = '" + FPS_Code + "' and RouteNumber = '" + RootNum + "'";
                            SqlCommand cmdup = new SqlCommand(upt, con);

                            cmdup.ExecuteNonQuery();
                        }

                    }

                    if (y > 0)
                    {
                       // btnsms.Visible = true;

                        hlinkpdo.Visible = true;

                        Session["TO"] = TO_Num;

                        Session["sid"] = issueCode;

                        Session["issubmited"] = "Yes";

                        Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Data Inserted');</script>");

                    }

                }

                catch
                {
                    btnSave.Enabled = true;

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
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
    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    //protected void btnsms_Click(object sender, EventArgs e)
    //{

    //}
    protected void btnadd_Click(object sender, EventArgs e)
    {
        Panel2.Visible = true;
        bool checkstatus = false;
        try
        {
            if (txtAllotQty.Text == "")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Allotment Quantity to be added is required!'); </script> ");
                return;
            }
            
            else if (ddlFps.SelectedIndex == 0)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select FPS Name!'); </script> ");
                return;
            }
            else if (ddlcommodtiy.SelectedIndex == 0)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Commodity Name!'); </script> ");
                return;
            }
            else
            {

                if (ddlFps.Items.Count > 0)
                {

                    btnSave.Enabled = true;

                    if (Session["dt1"] == null)
                    {
                        Dt1 = CreateTable();
                        Session["dt1"] = Dt1;
                    }

                    // adding rows to the datatable
                    DataRow dr = ((DataTable)Session["dt1"]).NewRow();
                    ((DataTable)Session["dt1"]).AcceptChanges();
                    dr["FPSName"] = ddlFps.SelectedItem;

                    dr["qty_issue"] = txtAllotQty.Text.Trim();
                   
                    dr["commodity"] = ddlcommodtiy.SelectedItem;

                    dr["commodityid"] = ddlcommodtiy.SelectedValue;
                    dr["FPSCode"] = ddlFps.SelectedValue;

                    if (GridView2.Rows.Count > 0)
                    {
                        int i;

                        // checking whether or not the godown is already added to the grid view
                        for (i = 0; i <= GridView2.Rows.Count - 1; i++)
                        {
                            string commodityid = GridView2.DataKeys[i].Values[0].ToString();
                            string selectcomm = ddlcommodtiy.SelectedValue.ToString();

                            string FPSCode = GridView2.DataKeys[i].Values[1].ToString();
                            string selectfps = ddlFps.SelectedValue.ToString();

                            if (FPSCode == selectfps && commodityid == selectcomm)
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
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Entry for this FPS is already done'); </script> ");
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
                   // Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('No Godown under the selected Commodity  Number'); </script> ");
                }

            }
        }
        catch (Exception ex)
        {
            
        }

    }

    private DataTable CreateTable()
    {
        DataTable dt = new DataTable();//DataTable is created
        DataColumn FPSName = new DataColumn("FPSName", Type.GetType("System.String"));
        DataColumn qty_issue = new DataColumn("qty_issue", Type.GetType("System.Decimal"));
        
        DataColumn commodity = new DataColumn("commodity", Type.GetType("System.String"));
        DataColumn commodityid = new DataColumn("commodityid", Type.GetType("System.Int32"));

        DataColumn FPSCode = new DataColumn("FPSCode", Type.GetType("System.Int64"));

        dt.Columns.Add(FPSName);//Column is added to the DataTable
        dt.Columns.Add(qty_issue);//Column is added to the DataTable
        


        dt.Columns.Add(commodity);//Column is added to the DataTable
        dt.Columns.Add(commodityid);//Column is added to the DataTable
        dt.Columns.Add(FPSCode);


        dt.AcceptChanges();
        return dt;
    }

    protected void ddlfeednumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        get_FPS();
    }


    protected void get_FPS()
    {
        string qry = "Select fps_code,fps_name from tbl_rootchart_master where root_no = '" + ddlrootnumber.SelectedValue + "' and feed_no = '" + ddlfeednumber.SelectedValue + "' and DistrictId = '" + distid + "'";

        SqlCommand cmd = new SqlCommand(qry, con);

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();

        da.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlFps.DataSource = ds.Tables[0];
            ddlFps.DataTextField = "fps_name";
            ddlFps.DataValueField = "fps_code";
            ddlFps.DataBind();
            ddlFps.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlFps.DataSource = "";

            ddlFps.DataBind();

            ddlFps.Items.Insert(0, "--Select--");

        }
    }

}
