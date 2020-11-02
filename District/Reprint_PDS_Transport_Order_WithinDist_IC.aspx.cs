using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using DataAccess;
using Data;

public partial class District_Reprint_PDS_Transport_Order_WithinDist_IC : System.Web.UI.Page
{

    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    public string gatepass = "";
    public int getnum;
    SqlDataReader dr;
    // protected Common ComObj = null;
    //MoveChallan mobj1 = null;

    public string sid = "";
    public string DistId, ICID;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // ICID = Session["issue_id"].ToString();
            DistId = Session["dist_id"].ToString();
            // ddlCropYear.Items.Insert(0, "--Select--");
            //ddlCropYear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1));
            // ddlCropYear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year);
            //ddlCropYear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1));

            // GetDist();
            // GetCropYearValues();
            //GetMillerDistrict();
            GetTransportOrder();
            GetDist();
            //GetGodown();
        }

    }
    public void GetDist()
    {
        string DistCode = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "SELECT district_name,district_code FROM pds.districtsmp where district_code='" + DistCode + "' Order By district_name  ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtDistrict.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
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
    public void GetTransportOrder()
    {

        //IC_Id = Session["issue_id"].ToString();
        //ICID = Session["issue_id"].ToString();
        string DistCode = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("select distinct DistTransportOrdernum from PDS_Dist_TransportOrder_Intra_IC where District='"+DistCode+"' order by DistTransportOrdernum");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlTransPortOrder.DataSource = ds.Tables[0];
                        ddlTransPortOrder.DataTextField = "DistTransportOrdernum";
                        ddlTransPortOrder.DataValueField = "DistTransportOrdernum";
                        ddlTransPortOrder.DataBind();
                        ddlTransPortOrder.Items.Insert(0, "--Select--");
                        // txtdesig.Text = ds.Tables[0].Rows[0]["Inspector_desig"].ToString();
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Inspector Name is Not available'); </script> ");
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
    protected void bttprint_Click(object sender, EventArgs e)
    {
        if(ddlTransPortOrder.SelectedIndex >= 0)
        {
            Session["DistTransportOrdernum"] = ddlTransPortOrder.SelectedValue.ToString();
        string url = "Print_DistTransportOrder.aspx";
        string s = "window.open('" + url + "', 'popup_window');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
    }
}