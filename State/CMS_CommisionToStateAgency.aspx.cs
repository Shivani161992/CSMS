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
public partial class State_CMS_CommisionToStateAgency : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    public string gatepass = "";
    public int getnum;
    SqlDataReader dr;
    string IC_Id = "", Dist_Id = "";

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {
            if (!IsPostBack)
            {
                Session["ICGBQ"] = null;
                GetCommodities();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                Fillgrid();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
        txtFrmDate.Text = Request.Form[txtFrmDate.UniqueID];
        txtToDate.Text = Request.Form[txtToDate.UniqueID];

        txtNackedFromdate.Text = Request.Form[txtNackedFromdate.UniqueID];
        txtNackedTodate.Text = Request.Form[txtNackedTodate.UniqueID];
    }

    public void GetCommodities()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "Select Commodity_Id,Commodity_Name From tbl_MetaData_STORAGE_COMMODITY Where Commodity_Id IN('33','64', '63') order by Commodity_Name";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlcommodities.DataSource = ds.Tables[0];
                    ddlcommodities.DataTextField = "Commodity_Name";
                    ddlcommodities.DataValueField = "Commodity_Id";
                    ddlcommodities.DataBind();
                    ddlcommodities.Items.Insert(0, "--Select--");
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
    protected void ddlcommodities_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtRate.Text = "";
        txtFrmDate.Text = "";
        txtToDate.Text = "";
        if (ddlcommodities.SelectedIndex > 0)
        {
            GetNackeddata();

        }


    }

    public void GetNackeddata()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = " select Rate_Rs as  Rate_Rs, CONVERT(varchar(10),FromDate, 103) as FromDate, CONVERT(varchar(10),ToDate, 103) as ToDate from CMS_NackedCostMaster where Commodity='" + ddlcommodities.SelectedValue.ToString() + "' and RateID in (select MAX(RateID) as  RateID  from CMS_NackedCostMaster where Commodity='" + ddlcommodities.SelectedValue.ToString() + "' and GETDATE()>= FromDate and GETDATE()<= ToDate )";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtNackedCost.Text = ds.Tables[0].Rows[0]["Rate_Rs"].ToString();
                    txtNackedFromdate.Text = ds.Tables[0].Rows[0]["FromDate"].ToString();
                    txtNackedTodate.Text = ds.Tables[0].Rows[0]["ToDate"].ToString();
                    //GetCommissionToSocietyRate();

                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Nacked Cost is not available'); </script> ");
                    return;
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
    public void GetCommissionToSocietyRate() 
    {

        double NRate = Convert.ToDouble(txtNackedCost.Text);
        double percentcal = (Convert.ToDouble(txtPercantage.Text) / 100);

        double percent = (percentcal);

        double onePercent = (NRate * percent);
        txtRate.Text = Convert.ToString(onePercent);
        decimal NackedCost = Convert.ToDecimal(txtRate.Text);

        decimal NackedCostRound = Math.Round(NackedCost, 2);
        txtRate.Text = Convert.ToString(NackedCostRound);

    }
    protected void btAdd_Click(object sender, EventArgs e)
    {
        if (ddlcommodities.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Commodity'); </script> ");
            return;
        }
        using (con = new SqlConnection(strcon))
            try
            {
                con.Open();
                string qrey = "select max(RateID) as RateID  from CMS_CommisssionToStateAgency where  LEN(RateID)<15  ";
                da = new SqlDataAdapter(qrey, con);

                ds = new DataSet();
                da.Fill(ds);

                DataRow dr = ds.Tables[0].Rows[0];

                gatepass = ds.Tables[0].Rows[0]["RateID"].ToString();

                if (gatepass == "")
                {
                    gatepass = "118" + "01";
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

                string ConvertNackedFromdate = ServerDate.getDate_MDY(txtNackedFromdate.Text);
                string ConvertNackedTodate = ServerDate.getDate_MDY(txtNackedTodate.Text);

                con.Open();
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string strselect = "insert into CMS_CommisssionToStateAgency( RateID, Commodity, Rate_Rs, FromDate, ToDate, Createddate, IP, NackedRate, Nacked_Fromdate, Nacked_ToDate, Percentage) values ('" + gatepass + "','" + ddlcommodities.SelectedValue.ToString() + "','" + txtRate.Text + "','" + ConvertFromDate + "','" + ConvertToDate + "',getdate(),'" + ip + "', '" + txtNackedCost.Text + "','" + ConvertNackedFromdate + "','" + ConvertNackedTodate + "','"+txtPercantage.Text+"')";
                cmd = new SqlCommand(strselect, con);
                string check = (string)cmd.ExecuteScalar();

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");

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



        btAdd.Enabled = false;
        ddlcommodities.Enabled = false;
        txtRate.Enabled = false;
        txtFrmDate.Enabled = false;
        txtToDate.Enabled = false;
        txtNackedFromdate.Enabled = false;
        txtNackedTodate.Enabled = false;
        txtNackedCost.Enabled = false;
        txtPercantage.Enabled = false;
        Fillgrid();
    }

    void Fillgrid()
    {


        DataSet ds = new DataSet();
        using (SqlConnection con = new SqlConnection(strcon))
        {
            con.Open();

            string select = "select C.Commodity_Name, Rate_Rs, CONVERT(varchar(10), FromDate , 103) as FromDate, CONVERT(varchar(10), ToDate , 103) as ToDate, Percentage   From CMS_CommisssionToStateAgency as NC inner join tbl_MetaData_STORAGE_COMMODITY as C on C.Commodity_Id=NC.Commodity order by Commodity_Name, NC.Createddate ";
            SqlCommand cmd = new SqlCommand(select, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
    }

    protected void txtPercantage_TextChanged(object sender, EventArgs e)
    {
        if (txtPercantage.Text != "")
        {
            GetCommissionToSocietyRate();
        }
        else if (txtPercantage.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter percentage'); </script> ");
            txtPercantage.Text = "";
            txtPercantage.Focus();
            return;
        }
    }

}