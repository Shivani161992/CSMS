using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class IssueCenter_RePrint_DO_PDSMO : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da;
    DataSet ds;

    string districtid = "";
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            if (!IsPostBack)
            {
                txtFromDate.Text = txtToDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                Session["DC_MO"] = Session["CreatedDate"] = null;
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ddlTONumber.Items.Clear();
        GetTONumber();
    }

    public void GetTONumber()
    {
        RequiredFieldValidator5.Visible = RequiredFieldValidator6.Visible = false;
        districtid = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string fromdate = Request.Form[txtFromDate.UniqueID];
                txtFromDate.Text = fromdate;
                string todate = Request.Form[txtToDate.UniqueID];
                txtToDate.Text = todate;

                ConvertServerDate ServerDate = new ConvertServerDate();
                string ConvertFromDate = ServerDate.getDate_MDY(fromdate.ToString());
                string ConvertToDate = ServerDate.getDate_MDY(todate.ToString());

                string select = string.Format("SELECT DC_MO,REPLACE(CONVERT(NVARCHAR,CreatedDate, 111), '/', '-') + ' ' + CONVERT(varchar(35),CreatedDate,114) As CreatedDate FROM DeliveryChallan_MO where CreatedDate between '" + ConvertFromDate + " 00:00:00' and '" + ConvertToDate + " 23:59:59' and FrmDist='" + districtid + "' and Issue_Center='" + Session["issue_id"].ToString()+ "' order by DC_MO");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlTONumber.DataSource = ds.Tables[0];
                        ddlTONumber.DataTextField = "DC_MO";
                        ddlTONumber.DataValueField = "CreatedDate";
                        ddlTONumber.DataBind();
                        ddlTONumber.Items.Insert(0, "--Select--");

                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Delivery Challan Is Not Available From These Date'); </script> ");
                        ddlTONumber.DataSource = "";
                        ddlTONumber.DataBind();
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

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (ddlTONumber.SelectedIndex > 0)
        {
            Session["DC_MO"] = ddlTONumber.SelectedItem.ToString();
            Session["CreatedDate"] = ddlTONumber.SelectedValue.ToString();

            string url = "Print_DO_PDSMO.aspx";
            string s = "window.open('" + url + "', 'popup_window');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Delivery Challan Number'); </script> ");
            Session["DC_MO"] = Session["CreatedDate"] = null;
        }
    }

    //protected void ddlTONumber_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    hdfDispatchMode.Value = "";
    //    if (ddlTONumber.SelectedIndex > 0)
    //    {
    //        GetDispatchMode();
    //    }
    //    else
    //    {
    //        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Delivery Challan Number'); </script> ");
    //    }
    //}

    //public void GetDispatchMode()
    //{
    //    districtid = Session["dist_id"].ToString();
    //    using (con = new SqlConnection(strcon))
    //    {
    //        try
    //        {
    //            con.Open();

    //            string select = string.Format("SELECT ModeofDispatch FROM DeliveryChallan_MO where DC_MO='"+ddlTONumber.SelectedItem.ToString() +"' and FrmDist='" + districtid + "'");
    //            da = new SqlDataAdapter(select, con);
    //            ds = new DataSet();
    //            da.Fill(ds);

    //            if (ds != null)
    //            {
    //                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //                {
    //                    hdfDispatchMode.Value = ds.Tables[0].Rows[0]["ModeofDispatch"].ToString();
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
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
}