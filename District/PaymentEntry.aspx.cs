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

public partial class District_PaymentEntry : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_WPMS = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2015"].ToString());

    public string dist = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            dist = Session["dist_id"].ToString();

            if (!IsPostBack)
            {
                txtdeduction.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

                txtnetamtpaid.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

                txtPaydate.Attributes.Add("onkeypress", "return CheckCalDate(this)");

                lbldist.Text = Session["dist_name"].ToString();

                txtdeduction.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
                txtdeductstencile.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
                txtdeductstich.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
                txtdeductgunny.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
                txtdeductothers.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            }

        }
    }

    protected void btnclose_Click(object sender, EventArgs e)
    {

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
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

                string qrysel = "select crop,crpcode from Crop_Master where crpcode not in ('8')";

                SqlDataAdapter da = new SqlDataAdapter(qrysel, con_WPMS);   
                 
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlcrop.DataSource = ds.Tables[0];
                        ddlcrop.DataTextField = "crop";
                        ddlcrop.DataValueField = "crpcode";
                        ddlcrop.DataBind();
                        ddlcrop.Items.Insert(0, "--Select--");

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

    protected void ddlcrop_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcrop.SelectedValue == "1")
        {
            getWhtUparjncntr();
        }

        else if (ddlcrop.SelectedValue == "2" || ddlcrop.SelectedValue == "3")
        {

        }

        else if (ddlcrop.SelectedValue == "4" || ddlcrop.SelectedValue == "5" || ddlcrop.SelectedValue == "6")
        {

        }
        else
        {

        }
        
    }

    void getWhtUparjncntr()
    {
        try
        {
            if (con_WPMS != null)
            {
                if (con_WPMS.State == ConnectionState.Closed)
                {
                    con_WPMS.Open();
                }
                
                string qrysel = "select society.Society_Id , society.Society_Name + ',' + society.Society_Id as Society_Name from society where IsWheat = 'Y' and DistrictId = 23" + dist + "";
                SqlDataAdapter da = new SqlDataAdapter(qrysel, con_WPMS);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlsociety.DataSource = ds.Tables[0];
                        ddlsociety.DataTextField = "Society_Name";
                        ddlsociety.DataValueField = "Society_Id";
                        ddlsociety.DataBind();
                        ddlsociety.Items.Insert(0, "--Select--");
                    }
                }

            }
            else
            {
            }
        }

        catch (Exception)
        {

            con_WPMS.Close();
        }
        finally
        {
            con_WPMS.Close();
        }


    }
}
