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
using Data;
using DataAccess;
using System.Data.SqlClient;

public partial class District_DelAcceptance : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

   // public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.ConnectionStrings["Appconstr_PPMS2015_16"].ToString());
    //public SqlConnection con_Maze = new SqlConnection(ConfigurationManager.ConnectionStrings["Appconstr_MPMS2015_16"].ToString());
    //public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_PPMS2015_16"].ToString());
    //public SqlConnection con_Maze = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_MPMS2015_16"].ToString());

    public SqlConnection con_WPMS = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2016"].ToString());
    public SqlConnection cons = new SqlConnection(ConfigurationManager.ConnectionStrings["connstorage"].ToString());

    public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.ConnectionStrings["Appconstr_PPMS2015_16"].ToString());
    public SqlConnection con_Maze = new SqlConnection(ConfigurationManager.ConnectionStrings["Appconstr_MPMS2015_16"].ToString());

    SqlCommand cmdC = new SqlCommand();
    SqlCommand cmdW = new SqlCommand();

    string districtid;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            districtid = Session["dist_id"].ToString();

            if (Page.IsPostBack == false)
            {
                GetDepot();

                GetCommodity();

                txtAccDate.Attributes.Add("onkeypress", "return CheckCalDate(this)");  
            }
        }

        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    public void GetDepot()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        string ord = "Select DepotID , DepotName from tbl_MetaData_DEPOT where DistrictId = '23" + districtid + "' order by DepotName";
        SqlCommand cmd = new SqlCommand(ord, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
        }
        else
        {
            ddlIC.DataSource = ds.Tables[0];
            ddlIC.DataTextField = "DepotName";
            ddlIC.DataValueField = "DepotId";

            ddlIC.DataBind();

            ddlIC.Items.Insert(0, "--Select--");
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    public void GetTC(string date)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        if (ddlcommodtiy.SelectedValue.ToString() == "1")
        {
            string seltc = "Select TC_Number from Acceptance_Note_Detail2016 where convert(varchar(10),Acceptance_Date,105) = convert(varchar(10),'" + date + "',105) and IssueCenter_ID = '" + ddlIC.SelectedValue + "' and CommodityId in('22') order by TC_Number ";

            SqlCommand cmd = new SqlCommand(seltc, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count == 0)
            {

            }
            else
            {
                ddlTCNum.DataSource = ds.Tables[0];
                ddlTCNum.DataTextField = "TC_Number";
                ddlTCNum.DataValueField = "TC_Number";

                ddlTCNum.DataBind();

                ddlTCNum.Items.Insert(0, "--Select--");
            }
        }

        else
            if (ddlcommodtiy.SelectedValue.ToString() == "2" || ddlcommodtiy.SelectedValue.ToString() == "3")
            {
                string seltc = "Select TC_Number from Acceptance_Note_Detail where convert(varchar(10),Acceptance_Date,105) = convert(varchar(10),'" + date + "',105) and IssueCenter_ID = '" + ddlIC.SelectedValue + "' and CommodityId in('13','14') order by TC_Number ";

                SqlCommand cmd = new SqlCommand(seltc, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count == 0)
                {

                }
                else
                {
                    ddlTCNum.DataSource = ds.Tables[0];
                    ddlTCNum.DataTextField = "TC_Number";
                    ddlTCNum.DataValueField = "TC_Number";

                    ddlTCNum.DataBind();

                    ddlTCNum.Items.Insert(0, "--Select--");
                }
            }

            else
                if (ddlcommodtiy.SelectedValue.ToString() == "4" || ddlcommodtiy.SelectedValue.ToString() == "5" || ddlcommodtiy.SelectedValue.ToString() == "6" || ddlcommodtiy.SelectedValue.ToString() == "7" || ddlcommodtiy.SelectedValue.ToString() == "8")
                {
                    string seltc = "Select TC_Number from Acceptance_Note_Detail where convert(varchar(10),Acceptance_Date,105) = convert(varchar(10),'" + date + "',105) and IssueCenter_ID = '" + ddlIC.SelectedValue + "' and CommodityId in('8','11','12','40') order by TC_Number ";

                    SqlCommand cmd = new SqlCommand(seltc, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count == 0)
                    {

                    }
                    else
                    {
                        ddlTCNum.DataSource = ds.Tables[0];
                        ddlTCNum.DataTextField = "TC_Number";
                        ddlTCNum.DataValueField = "TC_Number";

                        ddlTCNum.DataBind();

                        ddlTCNum.Items.Insert(0, "--Select--");
                    }
                }




        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    public void GetAccepNo()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        if (ddlTCNum.SelectedValue == "0" || ddlTCNum.SelectedItem.Text == "--Select--")
        {

        }

        else
        {
            if (ddlcommodtiy.SelectedValue.ToString() == "1")
            {
                string selAcno = "Select Acceptance_No from Acceptance_Note_Detail2016 where TC_Number = '" + ddlTCNum.SelectedValue + "' and IssueCenter_ID = '" + ddlIC.SelectedValue + "' and CommodityId in('22') ";

                SqlCommand cmd = new SqlCommand(selAcno, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count == 0)
                {

                }
                else
                {
                    ddlAcceptanceNumber.DataSource = ds.Tables[0];
                    ddlAcceptanceNumber.DataTextField = "Acceptance_No";
                    ddlAcceptanceNumber.DataValueField = "Acceptance_No";

                    ddlAcceptanceNumber.DataBind();

                    ddlAcceptanceNumber.Items.Insert(0, "--Select--");
                }
            }

            else
                if (ddlcommodtiy.SelectedValue.ToString() == "2" || ddlcommodtiy.SelectedValue.ToString() == "3")
                {
                    string selAcno = "Select Acceptance_No from Acceptance_Note_Detail where TC_Number = '" + ddlTCNum.SelectedValue + "' and IssueCenter_ID = '" + ddlIC.SelectedValue + "' and CommodityId in('13','14') ";

                    SqlCommand cmd = new SqlCommand(selAcno, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count == 0)
                    {

                    }
                    else
                    {
                        ddlAcceptanceNumber.DataSource = ds.Tables[0];
                        ddlAcceptanceNumber.DataTextField = "Acceptance_No";
                        ddlAcceptanceNumber.DataValueField = "Acceptance_No";

                        ddlAcceptanceNumber.DataBind();

                        ddlAcceptanceNumber.Items.Insert(0, "--Select--");
                    }
                }

                else
                    if (ddlcommodtiy.SelectedValue.ToString() == "4" || ddlcommodtiy.SelectedValue.ToString() == "5" || ddlcommodtiy.SelectedValue.ToString() == "6" || ddlcommodtiy.SelectedValue.ToString() == "7" || ddlcommodtiy.SelectedValue.ToString() == "8")
                    {
                        string selAcno = "Select Acceptance_No from Acceptance_Note_Detail where TC_Number = '" + ddlTCNum.SelectedValue + "' and IssueCenter_ID = '" + ddlIC.SelectedValue + "' and CommodityId in('8','11','12','40') ";

                        SqlCommand cmd = new SqlCommand(selAcno, con);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds.Tables[0].Rows.Count == 0)
                        {

                        }
                        else
                        {
                            ddlAcceptanceNumber.DataSource = ds.Tables[0];
                            ddlAcceptanceNumber.DataTextField = "Acceptance_No";
                            ddlAcceptanceNumber.DataValueField = "Acceptance_No";

                            ddlAcceptanceNumber.DataBind();

                            ddlAcceptanceNumber.Items.Insert(0, "--Select--");
                        }
                    }


        }



        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }


    }

    protected void ddlTCNum_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIC.SelectedValue == "0" || ddlIC.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Select Issue Center'); </script> ");
            return;
        }

        if (txtAccDate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Select Date'); </script> ");
            return;
        }

        else
        {
            GetAccepNo();
        }
    }

    protected void ddlAcceptanceNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDetails();
    }

    public void GetDetails()
    {
        if (ddlcommodtiy.SelectedValue.ToString() == "1")
        {
            if (con_WPMS.State == ConnectionState.Closed)
            {
                con_WPMS.Open();
            }

            if (ddlAcceptanceNumber.SelectedValue == "0" || ddlAcceptanceNumber.SelectedItem.Text == "--Select--")
            {
                btnDelete.Enabled = false;
            }

            else
            {
                btnDelete.Enabled = true;

                string getdata = "select IssueID, Truck_No , Society_Name +','+ SocPlace as Society  from Acceptance_Note_Detail inner join Society on Society.Society_Id = Acceptance_Note_Detail.Purchase_Center where Acceptance_Note_Detail.Acceptance_No = '" + ddlAcceptanceNumber.SelectedValue + "' and Acceptance_Note_Detail.Distt_ID = '23" + districtid + "' and IssueCenter_ID = '" + ddlIC.SelectedValue + "' and TC_Number = '" + ddlTCNum.SelectedItem.Text + "'";

                SqlCommand cmd = new SqlCommand(getdata, con_WPMS);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();

                da.Fill(ds);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Not Found'); </script> ");
                    return;
                }
                else
                {
                    lblsociety.Text = ds.Tables[0].Rows[0]["Society"].ToString();
                    lbltruck.Text = ds.Tables[0].Rows[0]["Truck_No"].ToString();
                    lblissueId.Text = ds.Tables[0].Rows[0]["IssueID"].ToString();
                }
            }

            if (con_WPMS.State == ConnectionState.Open)
            {
                con_WPMS.Close();
            }
        }

        else
            if (ddlcommodtiy.SelectedValue.ToString() == "2" || ddlcommodtiy.SelectedValue.ToString() == "3")
            {
                if (con_paddy.State == ConnectionState.Closed)
                {
                    con_paddy.Open();
                }

                if (ddlAcceptanceNumber.SelectedValue == "0" || ddlAcceptanceNumber.SelectedItem.Text == "--Select--")
                {
                    btnDelete.Enabled = false;
                }

                else
                {
                    btnDelete.Enabled = true;

                    string getdata = "select IssueID, Truck_No , Society_Name +','+ SocPlace as Society  from Acceptance_Note_Detail inner join Society on Society.Society_Id = Acceptance_Note_Detail.Purchase_Center where Acceptance_Note_Detail.Acceptance_No = '" + ddlAcceptanceNumber.SelectedValue + "' and Acceptance_Note_Detail.Distt_ID = '23" + districtid + "' and IssueCenter_ID = '" + ddlIC.SelectedValue + "' and TC_Number = '" + ddlTCNum.SelectedItem.Text + "'";

                    SqlCommand cmd = new SqlCommand(getdata, con_paddy);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataSet ds = new DataSet();

                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Not Found'); </script> ");
                        return;
                    }
                    else
                    {
                        lblsociety.Text = ds.Tables[0].Rows[0]["Society"].ToString();
                        lbltruck.Text = ds.Tables[0].Rows[0]["Truck_No"].ToString();
                        lblissueId.Text = ds.Tables[0].Rows[0]["IssueID"].ToString();
                    }
                }

                if (con_paddy.State == ConnectionState.Open)
                {
                    con_paddy.Close();
                }
            }

            else
                if (ddlcommodtiy.SelectedValue.ToString() == "4" || ddlcommodtiy.SelectedValue.ToString() == "5" || ddlcommodtiy.SelectedValue.ToString() == "6" || ddlcommodtiy.SelectedValue.ToString() == "7" || ddlcommodtiy.SelectedValue.ToString() == "8")
                {
                    if (con_Maze.State == ConnectionState.Closed)
                    {
                        con_Maze.Open();
                    }

                    if (ddlAcceptanceNumber.SelectedValue == "0" || ddlAcceptanceNumber.SelectedItem.Text == "--Select--")
                    {
                        btnDelete.Enabled = false;
                    }

                    else
                    {
                        btnDelete.Enabled = true;

                        string getdata = "select IssueID, Truck_No , Society_Name +','+ SocPlace as Society  from Acceptance_Note_Detail inner join Society on Society.Society_Id = Acceptance_Note_Detail.Purchase_Center where Acceptance_Note_Detail.Acceptance_No = '" + ddlAcceptanceNumber.SelectedValue + "' and Acceptance_Note_Detail.Distt_ID = '23" + districtid + "' and IssueCenter_ID = '" + ddlIC.SelectedValue + "' and TC_Number = '" + ddlTCNum.SelectedItem.Text + "'";

                        SqlCommand cmd = new SqlCommand(getdata, con_Maze);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        DataSet ds = new DataSet();

                        da.Fill(ds);

                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Not Found'); </script> ");
                            return;
                        }
                        else
                        {
                            lblsociety.Text = ds.Tables[0].Rows[0]["Society"].ToString();
                            lbltruck.Text = ds.Tables[0].Rows[0]["Truck_No"].ToString();
                            lblissueId.Text = ds.Tables[0].Rows[0]["IssueID"].ToString();
                        }
                    }

                    if (con_Maze.State == ConnectionState.Open)
                    {
                        con_Maze.Close();
                    }
                }
    }

    protected void ddlIC_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtAccDate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Select Date'); </script> ");
            return;
        }

        else
        {
            string accdate = getDate_MDY(txtAccDate.Text);

            GetTC(accdate);
        }
    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("dd-MM-yyyy"));
    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/Dist_Welcome.aspx");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        string distid = districtid;

        string issuecenerid = ddlIC.SelectedValue;

        string challan = ddlTCNum.SelectedValue;

        string acceptnum = ddlAcceptanceNumber.SelectedValue;

        string trucknum = lbltruck.Text;

        if (cons.State == ConnectionState.Closed)
        {
            cons.Open();
        }

        SqlTransaction trns1;

        SqlTransaction trns;

        string mystr = "SELECT * FROM [tbl_Storage_Arrival_Stock] where [DepotId]='" + issuecenerid + "' and AcceptanceNo='" + acceptnum + "' and IssueID='" + lblissueId.Text + "' and Commodity_Id='" + ddlcommodtiy.SelectedValue + "' and Truck_No='" + trucknum + "' and Challan_No='" + challan + "'";

        SqlCommand cmdwhr = new SqlCommand(mystr, cons);
        SqlDataReader sqldr = cmdwhr.ExecuteReader();
        sqldr.Read();

        if (sqldr.HasRows)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('whr created!Entry cannot deleted '); </script> ");

        }

        else
        {
            # region del_wpms
            if (ddlcommodtiy.SelectedValue.ToString() == "1")
            {
                if (con_WPMS.State == ConnectionState.Closed)
                {
                    con_WPMS.Open();
                }


                string inslog = "Insert into Acceptance_Note_Detail2016_Log select * from Acceptance_Note_Detail2016 where Distt_ID = '" + distid + "' and IssueCenter_ID = '" + issuecenerid + "' and TC_Number = '" + challan + "' and Truck_No = '" + trucknum + "' and Acceptance_No = '" + ddlAcceptanceNumber.SelectedValue + "'";

                SqlCommand cmd = new SqlCommand(inslog, con);

                cmd.CommandText = inslog;
                cmd.Connection = con;
                int Clog = cmd.ExecuteNonQuery();

                string inslog1 = "Insert into Acceptance_Note_Detail_DelLog select * from Acceptance_Note_Detail where Distt_ID = '23" + distid + "' and IssueCenter_ID = '" + issuecenerid + "' and TC_Number = '" + challan + "' and Truck_No = '" + trucknum + "' and Acceptance_No = '" + ddlAcceptanceNumber.SelectedValue + "'";

                SqlCommand cmd1 = new SqlCommand(inslog1, con_WPMS);

                cmd1.CommandText = inslog1;
                cmd1.Connection = con_WPMS;
                int y = cmd1.ExecuteNonQuery();

                if (con_WPMS.State == ConnectionState.Closed)
                {
                    con_WPMS.Open();
                }


                cmdW.Connection = con_WPMS;
                trns1 = con_WPMS.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                cmdW.Transaction = trns1;

              

                if (cons.State == ConnectionState.Closed)
                {
                    cons.Open();
                }

                cmdC.Connection = con;
                trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                cmdC.Transaction = trns;


                string delcon = "delete from Acceptance_Note_Detail2016 where Distt_ID = '" + distid + "' and IssueCenter_ID = '" + issuecenerid + "' and TC_Number = '" + challan + "' and Truck_No = '" + trucknum + "' and Acceptance_No = '" + ddlAcceptanceNumber.SelectedValue + "'";

               // SqlCommand cmdcon = new SqlCommand();
                cmdC.CommandText = delcon;
                cmdC.Connection = con;
               
                string delwpms = "delete from Acceptance_Note_Detail where Distt_ID = '23" + distid + "' and IssueCenter_ID = '" + issuecenerid + "' and TC_Number = '" + challan + "' and Truck_No = '" + trucknum + "' and Acceptance_No = '" + ddlAcceptanceNumber.SelectedValue + "'";

               // SqlCommand cmdwpms = new SqlCommand();

                cmdW.CommandText = delwpms;
                cmdW.Connection = con_WPMS;

                try
                {
                   
                    int b = cmdW.ExecuteNonQuery();

                    int a = cmdC.ExecuteNonQuery();

                   if (a >= 1)
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

                       string update = "Update SCSC_Procurement2016 set Acceptance_No = '' , AN_Status = 'N' where Distt_ID = '" + distid + "' and IssueCenter_ID = '" + issuecenerid + "' and TC_Number = '" + challan + "' and Truck_Number = '" + trucknum + "' and Acceptance_No = '" + ddlAcceptanceNumber.SelectedItem.Text + "'";
                       SqlCommand updcmd = new SqlCommand(update, con);

                       if (con.State == ConnectionState.Closed)
                       {
                           con.Open();
                       }

                       updcmd.ExecuteNonQuery();


                       string updateWpms = "Update IssueCenterReceipt_Online set  AN_Status = 'N' where DistrictId = '23" + distid + "' and IssueCenter_ID = '" + issuecenerid + "' and TruckChalanNo = '" + challan + "' and TruckNo = '" + trucknum + "' and IssueID = '" + lblissueId.Text + "'";
                       SqlCommand uptwpms = new SqlCommand(updateWpms, con_WPMS);

                       if (con_WPMS.State == ConnectionState.Closed)
                       {
                           con_WPMS.Open();
                       }

                       uptwpms.ExecuteNonQuery();

                       if (con_WPMS.State == ConnectionState.Open)
                       {
                           con_WPMS.Close();
                       }

                     
                       if (con.State == ConnectionState.Open)
                       {
                           con.Close();

                       }

                   }

                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Deleted Sucessfully'); </script> ");

                    btnDelete.Enabled = false;

                    lblsociety.Text = "";

                    lbltruck.Text = "";

                    lblissueId.Text = "";
                }

                catch
                {
                    trns1.Rollback();

                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error Occured'); </script> ");
                    return;
                }

                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                    if (con_WPMS.State == ConnectionState.Open)
                    {
                        con_WPMS.Close();
                    }

                    if (cons.State == ConnectionState.Open)
                    {
                        cons.Close();
                    }
                }

            }

            # endregion

            # region del_PPMS
            if (ddlcommodtiy.SelectedValue.ToString() == "2" || ddlcommodtiy.SelectedValue.ToString() == "3")
            {
                if (con_paddy.State == ConnectionState.Closed)
                {
                    con_paddy.Open();
                }


                string inslog = "Insert into Acceptance_Note_Detail_DelLog select * from Acceptance_Note_Detail where Distt_ID = '" + distid + "' and IssueCenter_ID = '" + issuecenerid + "' and TC_Number = '" + challan + "' and Truck_No = '" + trucknum + "' and Acceptance_No = '" + ddlAcceptanceNumber.SelectedValue + "'";

                SqlCommand cmd = new SqlCommand(inslog, con);

                cmd.CommandText = inslog;
                cmd.Connection = con;
                int Clog = cmd.ExecuteNonQuery();

                string inslog1 = "Insert into Acceptance_Note_Detail_Log select * from Acceptance_Note_Detail where Distt_ID = '23" + distid + "' and IssueCenter_ID = '" + issuecenerid + "' and TC_Number = '" + challan + "' and Truck_No = '" + trucknum + "' and Acceptance_No = '" + ddlAcceptanceNumber.SelectedValue + "'";

                SqlCommand cmd1 = new SqlCommand(inslog1, con_paddy);

                cmd1.CommandText = inslog1;
                cmd1.Connection = con_paddy;
                int y = cmd1.ExecuteNonQuery();

                if (con_paddy.State == ConnectionState.Closed)
                {
                    con_paddy.Open();
                }


                cmdW.Connection = con_paddy;
                trns1 = con_paddy.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                cmdW.Transaction = trns1;



                if (cons.State == ConnectionState.Closed)
                {
                    cons.Open();
                }

                cmdC.Connection = con;
                trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                cmdC.Transaction = trns;


                string delcon = "delete from Acceptance_Note_Detail where Distt_ID = '" + distid + "' and IssueCenter_ID = '" + issuecenerid + "' and TC_Number = '" + challan + "' and Truck_No = '" + trucknum + "' and Acceptance_No = '" + ddlAcceptanceNumber.SelectedValue + "'";

                // SqlCommand cmdcon = new SqlCommand();
                cmdC.CommandText = delcon;
                cmdC.Connection = con;

                string delwpms = "delete from Acceptance_Note_Detail where Distt_ID = '23" + distid + "' and IssueCenter_ID = '" + issuecenerid + "' and TC_Number = '" + challan + "' and Truck_No = '" + trucknum + "' and Acceptance_No = '" + ddlAcceptanceNumber.SelectedValue + "'";

                // SqlCommand cmdwpms = new SqlCommand();

                cmdW.CommandText = delwpms;
                cmdW.Connection = con_paddy;

                try
                {

                    int b = cmdW.ExecuteNonQuery();

                    int a = cmdC.ExecuteNonQuery();

                    if (a >= 1)
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

                        string update = "Update SCSC_Procurement set Acceptance_No = '' , AN_Status = 'N' where Distt_ID = '" + distid + "' and IssueCenter_ID = '" + issuecenerid + "' and TC_Number = '" + challan + "' and Truck_Number = '" + trucknum + "' and Acceptance_No = '" + ddlAcceptanceNumber.SelectedItem.Text + "'";
                        SqlCommand updcmd = new SqlCommand(update, con);

                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        updcmd.ExecuteNonQuery();


                        string updateWpms = "Update IssueCenterReceipt_Online set  AN_Status = 'N' where DistrictId = '23" + distid + "' and IssueCenter_ID = '" + issuecenerid + "' and TruckChalanNo = '" + challan + "' and TruckNo = '" + trucknum + "' and IssueID = '" + lblissueId.Text + "'";
                        SqlCommand uptwpms = new SqlCommand(updateWpms, con_paddy);

                        if (con_paddy.State == ConnectionState.Closed)
                        {
                            con_paddy.Open();
                        }

                        uptwpms.ExecuteNonQuery();

                        if (con_paddy.State == ConnectionState.Open)
                        {
                            con_paddy.Close();
                        }


                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();

                        }

                    }

                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Deleted Sucessfully'); </script> ");

                    btnDelete.Enabled = false;

                    lblsociety.Text = "";

                    lbltruck.Text = "";

                    lblissueId.Text = "";
                }

                catch
                {
                    trns1.Rollback();

                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error Occured'); </script> ");
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

                    if (cons.State == ConnectionState.Open)
                    {
                        cons.Close();
                    }
                }

            }

            # endregion

            # region del_mpms
            if (ddlcommodtiy.SelectedValue.ToString() == "4" || ddlcommodtiy.SelectedValue.ToString() == "5" || ddlcommodtiy.SelectedValue.ToString() == "6" || ddlcommodtiy.SelectedValue.ToString() == "7" || ddlcommodtiy.SelectedValue.ToString() == "8")
            {
                if (con_Maze.State == ConnectionState.Closed)
                {
                    con_Maze.Open();
                }


                string inslog = "Insert into Acceptance_Note_Detail_DelLog select * from Acceptance_Note_Detail where Distt_ID = '" + distid + "' and IssueCenter_ID = '" + issuecenerid + "' and TC_Number = '" + challan + "' and Truck_No = '" + trucknum + "' and Acceptance_No = '" + ddlAcceptanceNumber.SelectedValue + "'";

                SqlCommand cmd = new SqlCommand(inslog, con);

                cmd.CommandText = inslog;
                cmd.Connection = con;
                int Clog = cmd.ExecuteNonQuery();

                string inslog1 = "Insert into Acceptance_Note_Detail_DelLog select * from Acceptance_Note_Detail where Distt_ID = '23" + distid + "' and IssueCenter_ID = '" + issuecenerid + "' and TC_Number = '" + challan + "' and Truck_No = '" + trucknum + "' and Acceptance_No = '" + ddlAcceptanceNumber.SelectedValue + "'";

                SqlCommand cmd1 = new SqlCommand(inslog1, con_Maze);

                cmd1.CommandText = inslog1;
                cmd1.Connection = con_Maze;
                int y = cmd1.ExecuteNonQuery();

                if (con_Maze.State == ConnectionState.Closed)
                {
                    con_Maze.Open();
                }


                cmdW.Connection = con_Maze;
                trns1 = con_Maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                cmdW.Transaction = trns1;



                if (cons.State == ConnectionState.Closed)
                {
                    cons.Open();
                }

                cmdC.Connection = con;
                trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                cmdC.Transaction = trns;


                string delcon = "delete from Acceptance_Note_Detail where Distt_ID = '" + distid + "' and IssueCenter_ID = '" + issuecenerid + "' and TC_Number = '" + challan + "' and Truck_No = '" + trucknum + "' and Acceptance_No = '" + ddlAcceptanceNumber.SelectedValue + "'";

                // SqlCommand cmdcon = new SqlCommand();
                cmdC.CommandText = delcon;
                cmdC.Connection = con;

                string delwpms = "delete from Acceptance_Note_Detail where Distt_ID = '23" + distid + "' and IssueCenter_ID = '" + issuecenerid + "' and TC_Number = '" + challan + "' and Truck_No = '" + trucknum + "' and Acceptance_No = '" + ddlAcceptanceNumber.SelectedValue + "'";

                // SqlCommand cmdwpms = new SqlCommand();

                cmdW.CommandText = delwpms;
                cmdW.Connection = con_Maze;

                try
                {

                    int b = cmdW.ExecuteNonQuery();

                    int a = cmdC.ExecuteNonQuery();

                    if (a >= 1)
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

                        string update = "Update SCSC_Procurement set Acceptance_No = '' , AN_Status = 'N' where Distt_ID = '" + distid + "' and IssueCenter_ID = '" + issuecenerid + "' and TC_Number = '" + challan + "' and Truck_Number = '" + trucknum + "' and Acceptance_No = '" + ddlAcceptanceNumber.SelectedItem.Text + "'";
                        SqlCommand updcmd = new SqlCommand(update, con);

                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        updcmd.ExecuteNonQuery();


                        string updateWpms = "Update IssueCenterReceipt_Online set  AN_Status = 'N' where DistrictId = '23" + distid + "' and IssueCenter_ID = '" + issuecenerid + "' and TruckChalanNo = '" + challan + "' and TruckNo = '" + trucknum + "' and IssueID = '" + lblissueId.Text + "'";
                        SqlCommand uptwpms = new SqlCommand(updateWpms, con_Maze);

                        if (con_Maze.State == ConnectionState.Closed)
                        {
                            con_Maze.Open();
                        }

                        uptwpms.ExecuteNonQuery();

                        if (con_Maze.State == ConnectionState.Open)
                        {
                            con_Maze.Close();
                        }


                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();

                        }

                    }

                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Deleted Sucessfully'); </script> ");

                    btnDelete.Enabled = false;

                    lblsociety.Text = "";

                    lbltruck.Text = "";

                    lblissueId.Text = "";
                }

                catch
                {
                    trns1.Rollback();

                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error Occured'); </script> ");
                    return;
                }

                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                    if (con_Maze.State == ConnectionState.Open)
                    {
                        con_Maze.Close();
                    }

                    if (cons.State == ConnectionState.Open)
                    {
                        cons.Close();
                    }
                }

            }

            # endregion

        }
        
        if (cons.State == ConnectionState.Open)
        {
            cons.Close();
        }


    }

    void GetCommodity()
    {

        try
        {
            if (con_WPMS != null)
            {

                if (con_WPMS.State == ConnectionState.Closed)
                {

                    con_WPMS.Open();   /// 
                }

                string qrysel = "select crop,crpcode from Crop_Master where crpcode not in ('8')";

                SqlDataAdapter da = new SqlDataAdapter(qrysel, con_WPMS);   ///
                /// 
                DataSet ds = new DataSet();

                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlcommodtiy.DataSource = ds.Tables[0];
                        ddlcommodtiy.DataTextField = "crop";
                        ddlcommodtiy.DataValueField = "crpcode";
                        ddlcommodtiy.DataBind();
                        ddlcommodtiy.Items.Insert(0, "--Select--");

                    }
                }

            }
            else
            {
            }
        }

        catch (Exception)
        {

            if (con_WPMS.State == ConnectionState.Open)
            {

                con_WPMS.Close();   
            }
        }
        finally
        {
            if (con_WPMS.State == ConnectionState.Open)
            {

                con_WPMS.Close();  
            }
        }
    }
}
