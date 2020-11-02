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

public partial class IssueCenter_WHREntry : System.Web.UI.Page
{
    public SqlConnection con_WPMS = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2016"].ToString());
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    //By A public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_PPMS2015_16"].ToString());
    //By A public SqlConnection con_Maze = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_MPMS2015_16"].ToString());

    public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.ConnectionStrings["Appconstr_PPMS2015_16"].ToString());
    public SqlConnection con_Maze = new SqlConnection(ConfigurationManager.ConnectionStrings["Appconstr_MPMS2015_16"].ToString());

    string isscen = "";
    string distid = "";

    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            isscen = Session["issue_id"].ToString();
            distid = Session["dist_id"].ToString();

            txtwhr.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

            if (!IsPostBack)
            {
                GetCommodity();
            }
        }
    }

    protected void ddlcommodtiy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcommodtiy.SelectedValue != "0")
        {
            if (ddlcommodtiy.SelectedValue == "1")
            {
                # region wheat
                string getdepnum = "Select distinct WHR_Request from Acceptance_Note_Detail2016 where Distt_ID = '" + distid + "'  and IssueCenter_ID = '" + isscen + "'  and WhrNumber is null and WHR_Request <> '' and CommodityId = 22";

                SqlCommand cmd = new SqlCommand(getdepnum, con);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();

                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddldepositer.DataSource = ds.Tables[0];

                    ddldepositer.DataTextField = "WHR_Request";

                    ddldepositer.DataValueField = "WHR_Request";

                    ddldepositer.DataBind();

                    ddldepositer.Items.Insert(0, "--select--");
                }

                else
                {
                    ddldepositer.Items.Insert(0, "--select--");
                }
                # endregion
            }

            if (ddlcommodtiy.SelectedValue == "2" || ddlcommodtiy.SelectedValue == "3")
            {
                # region Paddy
                string getdepnum1 = "Select distinct WHR_Request from Acceptance_Note_Detail where Distt_ID = '" + distid + "'  and IssueCenter_ID = '" + isscen + "'  and WhrNumber is null and WHR_Request <> '' and CommodityId in (13,14)";

                SqlCommand cmd1 = new SqlCommand(getdepnum1, con);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);

                DataSet ds1 = new DataSet();

                da1.Fill(ds1);

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    ddldepositer.DataSource = ds1.Tables[0];

                    ddldepositer.DataTextField = "WHR_Request";

                    ddldepositer.DataValueField = "WHR_Request";

                    ddldepositer.DataBind();

                    ddldepositer.Items.Insert(0, "--select--");
                }

                else
                {
                    ddldepositer.Items.Insert(0, "--select--");
                }
                # endregion
            }

            if (ddlcommodtiy.SelectedValue == "4" || ddlcommodtiy.SelectedValue == "5" || ddlcommodtiy.SelectedValue == "6" || ddlcommodtiy.SelectedValue == "7")
            {
                # region Maize

                string getdepnum2 = "Select distinct WHR_Request from Acceptance_Note_Detail where Distt_ID = '" + distid + "'  and IssueCenter_ID = '" + isscen + "'  and WhrNumber is null and WHR_Request <> '' and CommodityId in (8,11,12,40)";

                SqlCommand cmd2 = new SqlCommand(getdepnum2, con);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);

                DataSet ds2 = new DataSet();

                da2.Fill(ds2);

                if (ds2.Tables[0].Rows.Count > 0)
                {
                    ddldepositer.DataSource = ds2.Tables[0];

                    ddldepositer.DataTextField = "WHR_Request";

                    ddldepositer.DataValueField = "WHR_Request";

                    ddldepositer.DataBind();

                    ddldepositer.Items.Insert(0, "--select--");
                }

                else
                {
                    ddldepositer.Items.Insert(0, "--select--");
                }

                # endregion

            }

        }

        else
        {
            ddldepositer.DataSource = "";

            ddldepositer.DataBind();

            ddldepositer.Items.Insert(0, "--select--");
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

                    con_WPMS.Open();
                }

                if (Session["Markfed"].ToString() == "Y")
                {
                    string qrysel = "select crop,crpcode from Crop_Master where crpcode not in ('1','4','5','6','7','8')";

                    SqlDataAdapter da = new SqlDataAdapter(qrysel, con_WPMS);

                    if (con_WPMS.State == ConnectionState.Closed)
                    {
                        con_WPMS.Open();
                    }

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

                        else
                        {
                            ddlcommodtiy.Items.Insert(0, "--Select--");
                        }
                    }

                }


                else
                {
                    string qrysel = "select crop,crpcode from Crop_Master where crpcode not in ('8')";

                    SqlDataAdapter da = new SqlDataAdapter(qrysel, con_WPMS);

                    if (con_WPMS.State == ConnectionState.Closed)
                    {
                        con_WPMS.Open();
                    }

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

                        else
                        {
                            ddlcommodtiy.Items.Insert(0, "--Select--");
                        }
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

    protected void ddldepositer_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcommodtiy.SelectedValue == "1")  // Wheat
        {
            string qry = "SELECT COUNT(truck_no) Truck , isnull(SUM(accept_qty),0)Qty , isnull(SUM(bags),0) bags  FROM Acceptance_Note_Detail2016 where WHR_Request = '" + ddldepositer.SelectedValue + "' and CommodityId = '22' and IssueCenter_ID = '" + isscen + "' ";

            SqlCommand cmd = new SqlCommand(qry, con);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lbltruck.Text = ds.Tables[0].Rows[0]["Truck"].ToString();

                lblbags.Text = ds.Tables[0].Rows[0]["bags"].ToString();

                lblqty.Text = ds.Tables[0].Rows[0]["Qty"].ToString();
            }
        }

        else if (ddlcommodtiy.SelectedValue == "2" || ddlcommodtiy.SelectedValue == "3")
        {
            string qry = "SELECT COUNT(truck_no) Truck , isnull(SUM(accept_qty),0)Qty , isnull(SUM(bags),0) bags  FROM Acceptance_Note_Detail where WHR_Request = '" + ddldepositer.SelectedValue + "' and CommodityId in('13','14') and IssueCenter_ID = '" + isscen + "' ";

            SqlCommand cmd = new SqlCommand(qry, con);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lbltruck.Text = ds.Tables[0].Rows[0]["Truck"].ToString();

                lblbags.Text = ds.Tables[0].Rows[0]["bags"].ToString();

                lblqty.Text = ds.Tables[0].Rows[0]["Qty"].ToString();
            }
        }

        else if (ddlcommodtiy.SelectedValue == "4")  // Jowar
        {
            string qry = "SELECT COUNT(truck_no) Truck , isnull(SUM(accept_qty),0)Qty , isnull(SUM(bags),0) bags  FROM Acceptance_Note_Detail where WHR_Request = '" + ddldepositer.SelectedValue + "' and CommodityId in (11) and IssueCenter_ID = '" + isscen + "' ";

            SqlCommand cmd = new SqlCommand(qry, con);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lbltruck.Text = ds.Tables[0].Rows[0]["Truck"].ToString();

                lblbags.Text = ds.Tables[0].Rows[0]["bags"].ToString();

                lblqty.Text = ds.Tables[0].Rows[0]["Qty"].ToString();
            }
        }

        else if (ddlcommodtiy.SelectedValue == "5")  // Maize
        {
            string qry = "SELECT COUNT(truck_no) Truck , isnull(SUM(accept_qty),0)Qty , isnull(SUM(bags),0) bags  FROM Acceptance_Note_Detail where WHR_Request = '" + ddldepositer.SelectedValue + "' and CommodityId in (12) and IssueCenter_ID = '" + isscen + "' ";

            SqlCommand cmd = new SqlCommand(qry, con);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lbltruck.Text = ds.Tables[0].Rows[0]["Truck"].ToString();

                lblbags.Text = ds.Tables[0].Rows[0]["bags"].ToString();

                lblqty.Text = ds.Tables[0].Rows[0]["Qty"].ToString();
            }
        }

        else if (ddlcommodtiy.SelectedValue == "6")  // Bajra
        {
            string qry = "SELECT COUNT(truck_no) Truck , isnull(SUM(accept_qty),0)Qty , isnull(SUM(bags),0) bags  FROM Acceptance_Note_Detail where WHR_Request = '" + ddldepositer.SelectedValue + "' and CommodityId in (8) and IssueCenter_ID = '" + isscen + "' ";

            SqlCommand cmd = new SqlCommand(qry, con);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lbltruck.Text = ds.Tables[0].Rows[0]["Truck"].ToString();

                lblbags.Text = ds.Tables[0].Rows[0]["bags"].ToString();

                lblqty.Text = ds.Tables[0].Rows[0]["Qty"].ToString();
            }
        }


        else if (ddlcommodtiy.SelectedValue == "7")  // JO
        {
            string qry = "SELECT COUNT(truck_no) Truck , isnull(SUM(accept_qty),0)Qty , isnull(SUM(bags),0) bags  FROM Acceptance_Note_Detail where WHR_Request = '" + ddldepositer.SelectedValue + "' and CommodityId in (40) and IssueCenter_ID = '" + isscen + "' ";

            SqlCommand cmd = new SqlCommand(qry, con);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lbltruck.Text = ds.Tables[0].Rows[0]["Truck"].ToString();

                lblbags.Text = ds.Tables[0].Rows[0]["bags"].ToString();

                lblqty.Text = ds.Tables[0].Rows[0]["Qty"].ToString();
            }
        }

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {

        if (ddldepositer.Items.Count == null || ddldepositer.Items.Count == 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Depositer Missing'); </script> ");
            return;
        }

        if (ddlcommodtiy.SelectedItem.Text == "--select--" || ddldepositer.SelectedItem.Text == "--select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Select Commodity or Depositer Num.'); </script> ");
            return;
        }


        else if (rdcomp.Checked == false && rdmanul.Checked == false)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Select WHR Type'); </script> ");
            return;

        }

        else if (txtwhr.Text == "" || txtwhr.Text == "0")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Enter Correct WHR Number'); </script> ");
            return;
        }
        else if (lblqty.Text == "" || lblbags.Text == "" || lbltruck.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('ट्रक/बोरे/वजन की जानकारी नहीं है |'); </script> ");
            return;
        }
        else
        {

            string select = "select * from Acceptance_Note_Detail2016 where WhrNumber = '" + txtwhr.Text.Trim() + "' and IssueCenter_ID = '" + isscen + "'";
            SqlCommand cmdchck = new SqlCommand(select, con);

            cmdchck.CommandText = select;

            cmdchck.Connection = con;

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter daP = new SqlDataAdapter(cmdchck);

            DataSet dsP = new DataSet();

            daP.Fill(dsP);

            if (dsP.Tables[0].Rows.Count == 0)
            {

                # region Wheat

                if (con_WPMS.State == ConnectionState.Closed)
                {
                    con_WPMS.Open();
                }

                SqlTransaction trns1;

                cmd1.Connection = con_WPMS;
                trns1 = con_WPMS.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                cmd1.Transaction = trns1;

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlTransaction trns;
                cmd.Connection = con;
                trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                cmd.Transaction = trns;


                string whrnum = txtwhr.Text.Trim();

                string whrtype = "";

                if (rdcomp.Checked == true)
                {
                    whrtype = "C";
                }

                else if (rdmanul.Checked == true)
                {
                    whrtype = "M";
                }

                try
                {
                    string qryinsert = "Update Acceptance_Note_Detail2016 set WhrNumber = '" + whrnum + "' , whrType = '" + whrtype + "' where WHR_Request = '" + ddldepositer.SelectedValue + "' and Distt_ID = '" + distid + "' and IssueCenter_ID = '" + isscen + "' ";
                    cmd.CommandText = qryinsert;

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();

                    }
                    cmd.Connection = con;


                    string inserttotest = "Update Acceptance_Note_Detail set WhrNumber = '" + whrnum + "' where WHR_Request = '" + ddldepositer.SelectedValue + "' and Distt_ID = '23" + distid + "' and IssueCenter_ID = '" + isscen + "' ";
                    cmd1.CommandText = inserttotest;

                    if (con_WPMS.State == ConnectionState.Closed)
                    {
                        con_WPMS.Open();
                    }
                    cmd1.Connection = con_WPMS;


                    int x = cmd1.ExecuteNonQuery();


                    int count = cmd.ExecuteNonQuery();

                    if (count >= 1)
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

                    }

                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted Successfully....'); </script> ");
                    btnsave.Enabled = false;

                }


                catch (Exception ex)
                {

                    trns1.Rollback();
                    Label10.Visible = true;
                    Label10.Text = "error:6" + ex.Message;

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




                # endregion

                # region Paddy

                if (con_paddy.State == ConnectionState.Closed)
                {
                    con_paddy.Open();
                }
                SqlTransaction trns11;
                cmd1.Connection = con_paddy;
                trns11 = con_paddy.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                cmd1.Transaction = trns11;

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlTransaction trns12;
                cmd.Connection = con;
                trns12 = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                cmd.Transaction = trns12;


                string whrnum1 = txtwhr.Text.Trim();

                string whrtype1 = "";

                if (rdcomp.Checked == true)
                {
                    whrtype1 = "C";
                }

                else if (rdmanul.Checked == true)
                {
                    whrtype1 = "M";
                }

                try
                {
                    string qryinsert = "Update Acceptance_Note_Detail set WhrNumber = '" + whrnum1 + "' , whrType = '" + whrtype1 + "' where WHR_Request = '" + ddldepositer.SelectedValue + "' and Distt_ID = '" + distid + "' and IssueCenter_ID = '" + isscen + "' ";
                    cmd.CommandText = qryinsert;

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();

                    }
                    cmd.Connection = con;


                    string inserttotest = "Update Acceptance_Note_Detail set WhrNumber = '" + whrnum1 + "' where WHR_Request = '" + ddldepositer.SelectedValue + "' and Distt_ID = '23" + distid + "' and IssueCenter_ID = '" + isscen + "' ";
                    cmd1.CommandText = inserttotest;

                    if (con_paddy.State == ConnectionState.Closed)
                    {
                        con_paddy.Open();
                    }
                    cmd1.Connection = con_paddy;


                    int x = cmd1.ExecuteNonQuery();


                    int count = cmd.ExecuteNonQuery();

                    if (count >= 1)
                    {
                        trns11.Commit();


                        if (con_WPMS.State == ConnectionState.Open)
                        {
                            con_WPMS.Close();
                        }

                        trns12.Commit();

                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();

                        }

                    }

                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted Successfully....'); </script> ");
                    btnsave.Enabled = false;

                }


                catch (Exception ex)
                {

                    trns11.Rollback();
                    Label10.Visible = true;
                    Label10.Text = "error:6" + ex.Message;

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




                # endregion

                # region Maize

                if (con_Maze.State == ConnectionState.Closed)
                {
                    con_Maze.Open();
                }
                SqlTransaction trns13;
                cmd1.Connection = con_Maze;
                trns13 = con_Maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                cmd1.Transaction = trns13;

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlTransaction trns31;
                cmd.Connection = con;
                trns31 = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                cmd.Transaction = trns31;


                string whrnum3 = txtwhr.Text.Trim();

                string whrtype3 = "";

                if (rdcomp.Checked == true)
                {
                    whrtype3 = "C";
                }

                else if (rdmanul.Checked == true)
                {
                    whrtype3 = "M";
                }

                try
                {
                    string qryinsert = "Update Acceptance_Note_Detail set WhrNumber = '" + whrnum3 + "' , whrType = '" + whrtype3 + "' where WHR_Request = '" + ddldepositer.SelectedValue + "' and Distt_ID = '" + distid + "' and IssueCenter_ID = '" + isscen + "' ";
                    cmd.CommandText = qryinsert;

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();

                    }
                    cmd.Connection = con;


                    string inserttotest = "Update Acceptance_Note_Detail set WhrNumber = '" + whrnum3 + "' where WHR_Request = '" + ddldepositer.SelectedValue + "' and Distt_ID = '23" + distid + "' and IssueCenter_ID = '" + isscen + "' ";
                    cmd1.CommandText = inserttotest;

                    if (con_Maze.State == ConnectionState.Closed)
                    {
                        con_Maze.Open();
                    }
                    cmd1.Connection = con_Maze;


                    int x = cmd1.ExecuteNonQuery();


                    int count = cmd.ExecuteNonQuery();

                    if (count >= 1)
                    {
                        trns13.Commit();


                        if (con_Maze.State == ConnectionState.Open)
                        {
                            con_Maze.Close();
                        }

                        trns31.Commit();

                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();

                        }

                    }

                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted Successfully....'); </script> ");
                    btnsave.Enabled = false;

                }


                catch (Exception ex)
                {

                    trns13.Rollback();
                    Label10.Visible = true;
                    Label10.Text = "error:6" + ex.Message;

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




                # endregion

            }

            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('WHR नंबर पहले से उपलब्ध हैl |'); </script> ");
            }

        }
    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
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


        if (con.State == ConnectionState.Open)
        {
            con.Close();

        }

        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
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


        if (con.State == ConnectionState.Open)
        {
            con.Close();

        }

        Response.Redirect("~/IssueCenter/WHREntry.aspx");
    }
}
