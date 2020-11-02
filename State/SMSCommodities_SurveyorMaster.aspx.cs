using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Data;
using DataAccess;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;


public partial class State_SMSCommodities_SurveyorMaster : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS

    public string gatepass = "";
    public int getnum;
    SqlDataReader dr;
    protected Common ComObj = null;
    //MoveChallan mobj1 = null;

    public string sid = "";

    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;


    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {
            if (!IsPostBack)
            {
                Session["ICGBQ"] = null;
                btAdd.Enabled = true;
                FillGrid();
                GetDist();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
        txtFrmDate.Text = Request.Form[txtFrmDate.UniqueID];
        txtToDate.Text = Request.Form[txtToDate.UniqueID];
    }
    protected void btAdd_Click(object sender, EventArgs e)
    {
        using (con = new SqlConnection(strcon))
            try
            {
                con.Open();
                string qrey = "select max(SurveyorID) as SurveyorID  from SMSCom_SurveyorMaster where  LEN(SurveyorID)<15 ";
                da = new SqlDataAdapter(qrey, con);

                ds = new DataSet();
                da.Fill(ds);

                DataRow dr = ds.Tables[0].Rows[0];

                gatepass = ds.Tables[0].Rows[0]["SurveyorID"].ToString();

                if (gatepass == "")
                {
                    gatepass = "18" + "01";
                }
                else
                {
                    getnum = Convert.ToInt32(gatepass);

                    getnum = getnum + 1;
                    gatepass = getnum.ToString();
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

        using (con = new SqlConnection(strcon))
            try
            {
                ConvertServerDate ServerDate = new ConvertServerDate();
                string ConvertFromDate = ServerDate.getDate_MDY(txtFrmDate.Text);
                string ConvertToDate = ServerDate.getDate_MDY(txtToDate.Text);

                string time = DateTime.Now.ToString("hh:mm:ss");
                string hour = time.Substring(0, 2);
                string Minu = time.Substring(3, 2);
                // string Secs = time.Substring(7, 2);


                string Password;
                Password = "SMSSUR";
                string username="Login";
                string MasterPassword = "LoginMas";



                con.Open();
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                string strselect = "insert into SMSCom_SurveyorMaster (SurveyorID, SurveyorName, Designation, MobNum, AadharNum, Fromdate, Todate, CreatedDate, IP, Password, District, IssueCenter, Godown, UserName, MasterPassword ) values ('" + gatepass + "','" + txtsurname.Text + "','" + txtDesignation.Text + "','" + txtMobileNum.Text + "','" + txtaadhar.Text + "','" + ConvertFromDate + "','" + ConvertToDate + "', Getdate(), '" + ip + "','" + Password + "','" + ddlDist.SelectedValue.ToString() + "','" + ddlIssueCenter.SelectedValue.ToString() + "', '" + ddlGodown.SelectedValue.ToString() + "','" + username + "', '" + MasterPassword + "')";
                cmd = new SqlCommand(strselect, con);
                string check = (string)cmd.ExecuteScalar();

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");
                txtsurname.Enabled = false;
                txtDesignation.Enabled = false;
                txtMobileNum.Enabled = false;
                txtaadhar.Enabled = false;
                txtFrmDate.Enabled = false;
                txtToDate.Enabled = false;
                ddlDist.Enabled = false;
                ddlGodown.Enabled = false;
                ddlIssueCenter.Enabled = false;
                FillGrid();


            }
            catch
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please check and Enter valid data...'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }




    }

    void FillGrid()
    {

        DataSet ds = new DataSet();
        using (SqlConnection con = new SqlConnection(strcon))
        {
            con.Open();

            string select = "  select SurveyorID, SurveyorName, Designation, MobNum, AadharNum, CONVERT(varchar(10), Fromdate, 103) Fromdate,  CONVERT(varchar(10), Todate, 103) Todate, D.district_name, IC.DepotName, G.Godown_Name  from  SMSCom_SurveyorMaster as SM inner join pds.districtsmp as D on d.district_code=SM.District inner join tbl_MetaData_DEPOT as IC on IC.DepotID=SM.IssueCenter inner join tbl_MetaData_GODOWN as G on G.Godown_ID=SM.Godown order by SurveyorName, Designation  ";
            SqlCommand cmd = new SqlCommand(select, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
    }



    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        trdist.Visible = false;
        hdfID.Value = GridView1.SelectedRow.Cells[1].Text;
        txtsurname.Text = GridView1.SelectedRow.Cells[2].Text;
        txtDesignation.Text = GridView1.SelectedRow.Cells[3].Text;
        txtMobileNum.Text = GridView1.SelectedRow.Cells[4].Text;
        
        //ddlDist.SelectedItem.Text = GridView1.SelectedRow.Cells[5].Text;
        //ddlDist.Enabled = false;
        //GetMPIssueCentre();
        //ddlIssueCenter.SelectedItem.Text = GridView1.SelectedRow.Cells[6].Text;

        //GetGodown();
        //ddlGodown.SelectedItem.Text = GridView1.SelectedRow.Cells[7].Text;
        //ddlGodown.Enabled = false;
        txtFrmDate.Text = GridView1.SelectedRow.Cells[8].Text;
        txtToDate.Text = GridView1.SelectedRow.Cells[9].Text;

        btAdd.Visible = false;
        btnUpdate.Visible = true;
        btnUpdate.Enabled = true;
        txtsurname.ReadOnly = true;
        txtDesignation.ReadOnly = true;
        txtMobileNum.ReadOnly = true;
        txtaadhar.ReadOnly = true;


    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        using (con = new SqlConnection(strcon))
            try
            {
                ConvertServerDate ServerDate = new ConvertServerDate();
                string ConvertFromDate = ServerDate.getDate_MDY(txtFrmDate.Text);
                string ConvertToDate = ServerDate.getDate_MDY(txtToDate.Text);

                con.Open();
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string strselect = "Update SMSCom_SurveyorMaster set Fromdate='" + ConvertFromDate + "', Todate='" + ConvertToDate + "' where SurveyorID='" + hdfID.Value + "'";
                cmd = new SqlCommand(strselect, con);
                string check = (string)cmd.ExecuteScalar();

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is updated successfully'); </script> ");
                btnUpdate.Enabled = false;

                txtsurname.Enabled = false;
                txtDesignation.Enabled = false;
                txtMobileNum.Enabled = false;
                txtaadhar.Enabled = false;
                txtFrmDate.Enabled = false;
                txtToDate.Enabled = false;
                ddlDist.Enabled = false;
                ddlGodown.Enabled = false;
                ddlIssueCenter.Enabled = false;
                FillGrid();

            }
            catch
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please check and Enter valid data...'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }

    }
    public void GetDist()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "";
                select = "SELECT district_name,district_code FROM pds.districtsmp Order By district_name";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlDist.DataSource = ds.Tables[0];
                        ddlDist.DataTextField = "district_name";
                        ddlDist.DataValueField = "district_code";
                        ddlDist.DataBind();
                        ddlDist.Items.Insert(0, "--Select--");

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
    protected void ddlDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlIssueCenter.Items.Clear();
        ddlGodown.Items.Clear();

        if (ddlDist.SelectedIndex > 0)
        {
            GetMPIssueCentre();
        }
        else
        {

        }
    }
    private void GetMPIssueCentre()
    {

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = string.Format("select DepotName,DepotID from tbl_MetaData_DEPOT where DistrictId= '23" + ddlDist.SelectedValue.ToString() + "' order by DepotName");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlIssueCenter.DataSource = ds.Tables[0];
                    ddlIssueCenter.DataTextField = "DepotName";
                    ddlIssueCenter.DataValueField = "DepotID";
                    ddlIssueCenter.DataBind();
                    ddlIssueCenter.Items.Insert(0, "--Select--");

                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके जिले में कोई भी प्रदाय केंद्र उपलब्ध नहीं है|'); </script> ");
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

    protected void ddlIssueCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlGodown.Items.Clear();

        if (ddlIssueCenter.SelectedIndex > 0)
        {
            GetGodown();
        }
        else
        {

        }
    }

    public void GetGodown()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where DepotId='" + ddlIssueCenter.SelectedValue.ToString() + "' and  DistrictId='23" + ddlDist.SelectedValue.ToString() + "'order by Godown_Name";
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlGodown.DataSource = ds.Tables[0];
                        ddlGodown.DataTextField = "Godown_Name";
                        ddlGodown.DataValueField = "Godown_ID";
                        ddlGodown.DataBind();
                        ddlGodown.Items.Insert(0, "--Select--");

                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Issue Center'); </script> ");
                    }
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con_MPStorage.State != ConnectionState.Closed)
                {
                    con_MPStorage.Close();
                }
            }
        }
    }

}