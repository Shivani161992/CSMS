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

public partial class District_RiceDCP_RMS_entry : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                try
                {
                    string headquery = "SELECT head_id ,head FROM rice_Head";
                    SqlCommand cmd = new SqlCommand(headquery, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    ddlhead.DataSource = ds.Tables[0];

                    ddlhead.DataTextField = "head";
                    ddlhead.DataValueField = "head_id";
                    ddlhead.DataBind();
                    ddlhead.Items.Insert(0, "--चुनें--");

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                }
                catch
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('भरी अथवा चुनी हुई जानकारी जांच कर पुनः प्रयास करें |');</script>");
                }
                txtqty.Attributes.Add("onkeypress", "return IsNumericMobile(event,this)");
            }
        }

        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            string district_id = Session["dist_id"].ToString();

            if (ddlmonth.SelectedValue != "0")
            {
                if (ddlhead.SelectedIndex != 0)
                {
                    if (ddlTYPE.SelectedValue != "0")
                    {
                        if (txtqty.Text.Trim() != "")
                        {
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }

                            try
                            {
                                string year = ddlyear.SelectedItem.Text;

                                string month = ddlmonth.SelectedItem.Text;

                                string head_Id = ddlhead.SelectedValue.ToString();

                                string TYPEofRICE = ddlTYPE.SelectedItem.Text;

                                string quantity = txtqty.Text.Trim();

                                string Browser = Request.Browser.Browser;

                                string computername = System.Environment.MachineName;

                                string IpAddress = Request.ServerVariables["REMOTE_ADDR"].ToString();

                                string date = System.DateTime.Now.ToString();

                                string query = "Insert into DistrictStockRegister_RICE (District_id ,Year,Month ,HeadId,TypeofRice ,Quantity ,Date,Browser ,ComputerName ,IPAddress) values ('" + district_id + "' ,'" + year + "','" + month + "' ,'" + head_Id + "','" + TYPEofRICE + "' ,'" + quantity + "' ,'" + date + "','" + Browser + "' ,'" + computername + "','" + IpAddress + "')";

                                SqlCommand cmd = new SqlCommand(query, con);

                                int x = cmd.ExecuteNonQuery();

                                txtqty.Text = "";

                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Saved, Thank you |');</script>");
                            }

                            catch
                            {
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Something going Wrong, Please Try again |');</script>");
                            }

                            finally
                            {
                                if (con.State == ConnectionState.Open)
                                {
                                    con.Close();
                                }
                            }
                        }

                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('please insert Quantity |');</script>");

                            txtqty.Focus();
                            // Matra Bharen
                        }
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Rice Type |');</script>");
                        // TYPE Chune

                        ddlTYPE.Focus();
                    }
                }
                else // Head
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Head |');</script>");

                    ddlhead.Focus();
                }
            }
            else // Month
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Month |');</script>");

                ddlmonth.Focus();
            }

        }

        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/Dist_Welcome.aspx");
    }
    protected void btnprint_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/Rpt_Print_DistrictRice.aspx");   

    }
}
