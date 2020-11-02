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

public partial class State_CMS_LabourChargesMaster : System.Web.UI.Page
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

        }


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
                string qrey = "select max(RateID) as RateID  from CMS_LabourCharges_Master where  LEN(RateID)<15  ";
                da = new SqlDataAdapter(qrey, con);

                ds = new DataSet();
                da.Fill(ds);

                DataRow dr = ds.Tables[0].Rows[0];

                gatepass = ds.Tables[0].Rows[0]["RateID"].ToString();

                if (gatepass == "")
                {
                    gatepass = "1817" + "01";
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
                decimal NackedCost = Convert.ToDecimal(txtRate.Text);

                decimal NackedCostRound = Math.Round(NackedCost, 2);
                txtRate.Text = Convert.ToString(NackedCostRound);
                con.Open();
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string strselect = "insert into CMS_LabourCharges_Master( RateID, Commodity, Rate_Rs, FromDate, ToDate, Createddate, IP) values ('" + gatepass + "','" + ddlcommodities.SelectedValue.ToString() + "','" + txtRate.Text + "','" + ConvertFromDate + "','" + ConvertToDate + "',getdate(),'" + ip + "')";
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
        Fillgrid();
    }


    void Fillgrid()
    {


        DataSet ds = new DataSet();
        using (SqlConnection con = new SqlConnection(strcon))
        {
            con.Open();

            string select = "select C.Commodity_Name, Rate_Rs, CONVERT(varchar(10), FromDate , 103) as FromDate, CONVERT(varchar(10), ToDate , 103) as ToDate   From CMS_LabourCharges_Master as NC inner join tbl_MetaData_STORAGE_COMMODITY as C on C.Commodity_Id=NC.Commodity order by Commodity_Name, NC.Createddate ";
            SqlCommand cmd = new SqlCommand(select, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
    }
}