using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class IssueCenter_WHREntry_Paddy2016 : System.Web.UI.Page
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
                txtWHRNo.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtWHRNo.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtWHRNo.Attributes.Add("onchange", "return chksqltxt(this)");

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
                string select = "select DepotName from dbo.tbl_MetaData_DEPOT where DepotID='" + IC_Id + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtissue.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
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
        using (con = new SqlConnection(Con_Paddy))
        {
            try
            {
                con.Open();

                string select = "";

                select = "select crop,crpcode from Crop_Master where crpcode IN ('2','3','4','5','6','7')";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlcommodtiy.DataSource = ds.Tables[0];
                    ddlcommodtiy.DataTextField = "crop";
                    ddlcommodtiy.DataValueField = "crpcode";
                    ddlcommodtiy.DataBind();
                    ddlcommodtiy.Items.Insert(0, "--Select--");

                    ddlcommodtiy.SelectedIndex = 1;
                    GetDepositerNo();
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
        ddldepositer.Items.Clear();
        txttruck.Text = txtBags.Text = txtQty.Text = txtWHRNo.Text = "";

        if (ddlcommodtiy.SelectedIndex > 0)
        {
            GetDepositerNo();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Commodity'); </script> ");
            return;
        }
    }

    private void GetDepositerNo()
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                IC_Id = Session["issue_id"].ToString();
                Dist_Id = Session["dist_id"].ToString();

                con.Open();
                string select = "";

                if (ddlcommodtiy.SelectedValue.ToString() == "2")
                {
                    select = "Select distinct WHR_Request from Acceptance_Note_Kharif2016 where Distt_ID = '" + Dist_Id + "'  and IssueCenter_ID = '" + IC_Id + "'  and WhrNumber is null and WHR_Request != '' and CommodityId IN ('13')";
                }
                else if (ddlcommodtiy.SelectedValue.ToString() == "3")
                {
                    select = "Select distinct WHR_Request from Acceptance_Note_Kharif2016 where Distt_ID = '" + Dist_Id + "'  and IssueCenter_ID = '" + IC_Id + "'  and WhrNumber is null and WHR_Request != '' and CommodityId IN ('14')";
                }
                else if (ddlcommodtiy.SelectedValue.ToString() == "4")
                {
                    select = "Select distinct WHR_Request from Acceptance_Note_Kharif2016 where Distt_ID = '" + Dist_Id + "'  and IssueCenter_ID = '" + IC_Id + "'  and WhrNumber is null and WHR_Request != '' and CommodityId IN ('11')";
                }
                else if (ddlcommodtiy.SelectedValue.ToString() == "5")
                {
                    select = "Select distinct WHR_Request from Acceptance_Note_Kharif2016 where Distt_ID = '" + Dist_Id + "'  and IssueCenter_ID = '" + IC_Id + "'  and WhrNumber is null and WHR_Request != '' and CommodityId IN ('12')";
                }
                else if (ddlcommodtiy.SelectedValue.ToString() == "6")
                {
                    select = "Select distinct WHR_Request from Acceptance_Note_Kharif2016 where Distt_ID = '" + Dist_Id + "'  and IssueCenter_ID = '" + IC_Id + "'  and WhrNumber is null and WHR_Request != '' and CommodityId IN ('8')";
                }
                else if (ddlcommodtiy.SelectedValue.ToString() == "7")
                {
                    select = "Select distinct WHR_Request from Acceptance_Note_Kharif2016 where Distt_ID = '" + Dist_Id + "'  and IssueCenter_ID = '" + IC_Id + "'  and WhrNumber is null and WHR_Request != '' and CommodityId IN ('40')";
                }
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddldepositer.DataSource = ds.Tables[0];
                    ddldepositer.DataTextField = "WHR_Request";
                    ddldepositer.DataValueField = "WHR_Request";
                    ddldepositer.DataBind();
                    ddldepositer.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Depositer Form Number Is Not Available'); </script> ");
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


    protected void ddldepositer_SelectedIndexChanged(object sender, EventArgs e)
    {
        txttruck.Text = txtBags.Text = txtQty.Text = txtWHRNo.Text = "";

        if (ddldepositer.SelectedIndex > 0)
        {
            GetDepositDetails();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Depositer Form No.'); </script> ");
            return;
        }
    }

    private void GetDepositDetails()
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                IC_Id = Session["issue_id"].ToString();
                Dist_Id = Session["dist_id"].ToString();

                con.Open();

                string select ="";

                if (ddlcommodtiy.SelectedValue.ToString() == "2")
                {
                    select = "SELECT COUNT(truck_no) Truck , isnull(SUM(accept_qty),0)Qty , isnull(SUM(bags),0) bags  FROM Acceptance_Note_Kharif2016 where WHR_Request = '" + ddldepositer.SelectedValue.ToString() + "' and CommodityId in('13') and IssueCenter_ID = '" + IC_Id + "' and Distt_ID = '" + Dist_Id + "'";
                }
                else if (ddlcommodtiy.SelectedValue.ToString() == "3")
                {
                    select = "SELECT COUNT(truck_no) Truck , isnull(SUM(accept_qty),0)Qty , isnull(SUM(bags),0) bags  FROM Acceptance_Note_Kharif2016 where WHR_Request = '" + ddldepositer.SelectedValue.ToString() + "' and CommodityId in('14') and IssueCenter_ID = '" + IC_Id + "' and Distt_ID = '" + Dist_Id + "'";
                }
                else if (ddlcommodtiy.SelectedValue.ToString() == "4")
                {
                    select = "SELECT COUNT(truck_no) Truck , isnull(SUM(accept_qty),0)Qty , isnull(SUM(bags),0) bags  FROM Acceptance_Note_Kharif2016 where WHR_Request = '" + ddldepositer.SelectedValue.ToString() + "' and CommodityId in('11') and IssueCenter_ID = '" + IC_Id + "' and Distt_ID = '" + Dist_Id + "'";
                }
                else if (ddlcommodtiy.SelectedValue.ToString() == "5")
                {
                    select = "SELECT COUNT(truck_no) Truck , isnull(SUM(accept_qty),0)Qty , isnull(SUM(bags),0) bags  FROM Acceptance_Note_Kharif2016 where WHR_Request = '" + ddldepositer.SelectedValue.ToString() + "' and CommodityId in('12') and IssueCenter_ID = '" + IC_Id + "' and Distt_ID = '" + Dist_Id + "'";
                }
                else if (ddlcommodtiy.SelectedValue.ToString() == "6")
                {
                    select = "SELECT COUNT(truck_no) Truck , isnull(SUM(accept_qty),0)Qty , isnull(SUM(bags),0) bags  FROM Acceptance_Note_Kharif2016 where WHR_Request = '" + ddldepositer.SelectedValue.ToString() + "' and CommodityId in('8') and IssueCenter_ID = '" + IC_Id + "' and Distt_ID = '" + Dist_Id + "'";
                }
                else if (ddlcommodtiy.SelectedValue.ToString() == "7")
                {
                    select = "SELECT COUNT(truck_no) Truck , isnull(SUM(accept_qty),0)Qty , isnull(SUM(bags),0) bags  FROM Acceptance_Note_Kharif2016 where WHR_Request = '" + ddldepositer.SelectedValue.ToString() + "' and CommodityId in('40') and IssueCenter_ID = '" + IC_Id + "' and Distt_ID = '" + Dist_Id + "'";
                }

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txttruck.Text = ds.Tables[0].Rows[0]["Truck"].ToString();
                    txtBags.Text = ds.Tables[0].Rows[0]["bags"].ToString();
                    txtQty.Text = ds.Tables[0].Rows[0]["Qty"].ToString();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Details Is Not Available'); </script> ");
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
        else if (ddldepositer.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Depositer Form No.'); </script> ");
            return;
        }
        else if (txtWHRNo.Text == "" || txtWHRNo.Text.Trim() == "0")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Correct WHR Number'); </script> ");
            return;
        }
        else if (txttruck.Text == "" || txtQty.Text == "" || txtBags.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('ट्रक/बोरे/वजन की जानकारी नहीं है |'); </script> ");
            return;
        }
        else
        {
            if (ddlcommodtiy.SelectedValue.ToString() == "2" || ddlcommodtiy.SelectedValue.ToString() == "3")
            {
                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    using (con = new SqlConnection(Con_CSMS))
                    {
                        try
                        {
                            IC_Id = Session["issue_id"].ToString();
                            Dist_Id = Session["dist_id"].ToString();

                            string select = "";
                            select = "select * from Acceptance_Note_Kharif2016 where WhrNumber = '" + txtWHRNo.Text.Trim() + "' and IssueCenter_ID = '" + IC_Id + "'";
                            da = new SqlDataAdapter(select, con);
                            ds = new DataSet();
                            da.Fill(ds);
                            if (ds.Tables[0].Rows.Count == 0)
                            {
                                SqlCommand cmd1 = new SqlCommand();
                                SqlCommand cmd = new SqlCommand();

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

                                string whrnum1 = txtWHRNo.Text.Trim();
                                string whrtype1 = "M";

                                try
                                {
                                    string qryinsert = "Update Acceptance_Note_Kharif2016 set WhrNumber = '" + whrnum1 + "' , whrType = '" + whrtype1 + "' where WHR_Request = '" + ddldepositer.SelectedValue.ToString() + "' and Distt_ID = '" + Dist_Id + "' and IssueCenter_ID = '" + IC_Id + "' ";
                                    cmd.CommandText = qryinsert;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    cmd.Connection = con;

                                    string inserttotest = "Update Acceptance_Note_Detail set WhrNumber = '" + whrnum1 + "' where WHR_Request = '" + ddldepositer.SelectedValue.ToString() + "' and Distt_ID = '23" + Dist_Id + "' and IssueCenter_ID = '" + IC_Id + "' ";
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
                                        trns12.Commit();

                                        if (con_paddy.State == ConnectionState.Open)
                                        {
                                            con_paddy.Close();
                                        }
                                        if (con.State == ConnectionState.Open)
                                        {
                                            con.Close();
                                        }

                                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted Successfully....'); </script> ");
                                        btnRecptSubmit.Enabled = false;
                                        ddlcommodtiy.Items.Clear();

                                        ddlcommodtiy.Enabled = ddldepositer.Enabled = txtWHRNo.Enabled = false;
                                        Label2.Visible = true;
                                        Label2.Text = "Data Inserted Successfully";

                                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trns11.Rollback();
                                    trns12.Rollback();

                                    btnRecptSubmit.Enabled = false;
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
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('WHR नंबर पहले से उपलब्ध हैं|'); </script> ");
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

                            string select = "";
                            select = "select * from Acceptance_Note_Kharif2016 where WhrNumber = '" + txtWHRNo.Text.Trim() + "' and IssueCenter_ID = '" + IC_Id + "'";
                            da = new SqlDataAdapter(select, con);
                            ds = new DataSet();
                            da.Fill(ds);
                            if (ds.Tables[0].Rows.Count == 0)
                            {
                                SqlCommand cmd1 = new SqlCommand();
                                SqlCommand cmd = new SqlCommand();

                                if (con_maze.State == ConnectionState.Closed)
                                {
                                    con_maze.Open();
                                }
                                SqlTransaction trns11;
                                cmd1.Connection = con_maze;
                                trns11 = con_maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                cmd1.Transaction = trns11;

                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }
                                SqlTransaction trns12;
                                cmd.Connection = con;
                                trns12 = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                cmd.Transaction = trns12;

                                string whrnum1 = txtWHRNo.Text.Trim();
                                string whrtype1 = "M";

                                try
                                {
                                    string qryinsert = "Update Acceptance_Note_Kharif2016 set WhrNumber = '" + whrnum1 + "' , whrType = '" + whrtype1 + "' where WHR_Request = '" + ddldepositer.SelectedValue.ToString() + "' and Distt_ID = '" + Dist_Id + "' and IssueCenter_ID = '" + IC_Id + "' ";
                                    cmd.CommandText = qryinsert;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    cmd.Connection = con;

                                    string inserttotest = "Update Acceptance_Note_Detail set WhrNumber = '" + whrnum1 + "' where WHR_Request = '" + ddldepositer.SelectedValue.ToString() + "' and Distt_ID = '23" + Dist_Id + "' and IssueCenter_ID = '" + IC_Id + "' ";
                                    cmd1.CommandText = inserttotest;
                                    if (con_maze.State == ConnectionState.Closed)
                                    {
                                        con_maze.Open();
                                    }
                                    cmd1.Connection = con_maze;

                                    int x = cmd1.ExecuteNonQuery();
                                    int count = cmd.ExecuteNonQuery();

                                    if (count >= 1)
                                    {
                                        trns11.Commit();
                                        trns12.Commit();

                                        if (con_maze.State == ConnectionState.Open)
                                        {
                                            con_maze.Close();
                                        }
                                        if (con.State == ConnectionState.Open)
                                        {
                                            con.Close();
                                        }

                                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted Successfully....'); </script> ");
                                        btnRecptSubmit.Enabled = false;
                                        ddlcommodtiy.Items.Clear();

                                        ddlcommodtiy.Enabled = ddldepositer.Enabled = txtWHRNo.Enabled = false;
                                        Label2.Visible = true;
                                        Label2.Text = "Data Inserted Successfully";

                                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trns11.Rollback();
                                    trns12.Rollback();

                                    btnRecptSubmit.Enabled = false;
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
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('WHR नंबर पहले से उपलब्ध हैं|'); </script> ");
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
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }
}