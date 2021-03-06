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

public partial class District_DistWheatDCP_RMS_entry : System.Web.UI.Page
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
                    bindHead();

                    bindStorage();

                    bindPacking();


                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                }
                catch
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some Problem Occured,Please Try Again |');</script>");
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
            if (ddlyear.SelectedValue != "0")
            {
                if (ddlmonth.SelectedValue != "0")
                {
                    if (ddlhead.SelectedIndex != 0)
                    {
                        if (ddlstorage.SelectedValue != "0")
                        {
                            if (ddlpacking.SelectedValue != "0")
                            {
                                if (txtqty.Text.Trim() != "")
                                {
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }

                                    string DistrictId = Session["dist_id"].ToString();

                                    try
                                    {
                                        string year = ddlyear.SelectedItem.Text;

                                        string month = ddlmonth.SelectedItem.Text;

                                        string head_Id = ddlhead.SelectedValue.ToString();

                                        string storage = ddlstorage.SelectedItem.Text;

                                        string packing = ddlpacking.SelectedItem.Text;

                                        string quantity = txtqty.Text.Trim();

                                        string Browser = Request.Browser.Browser;

                                        string computername = System.Environment.MachineName;

                                        string IpAddress = Request.ServerVariables["REMOTE_ADDR"].ToString();

                                        string date = System.DateTime.Now.ToString();

                                        string query = "Insert into DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,ComputerName ,IPAddress) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'" + head_Id + "','" + storage + "' ,'" + packing + "' ,'" + quantity + "' ,'" + date + "','" + Browser + "' ,'" + computername + "','" + IpAddress + "')";
                                        SqlCommand cmd = new SqlCommand(query, con);

                                        int x = cmd.ExecuteNonQuery();

                                        if (x > 0)
                                        {
                                            txtqty.Text = "";

                                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Saved, Thank you |');</script>");
                                            bindPacking();
                                        }
                                    }

                                    catch
                                    {
                                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('भरी अथवा चुनी हुई जानकारी जांच कर पुनः प्रयास करें |');</script>");
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
                                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Fill Quantity |');</script>");
                                    txtqty.Focus();
                                }
                            }
                            else
                            {
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Packing Type|');</script>");

                                ddlpacking.Focus();
                            }
                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Storage Type |');</script>");

                            ddlstorage.Focus();
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
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Year |');</script>");

                ddlyear.Focus();
            }
        }

        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    protected void bindHead()
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

    }

    protected void bindStorage()
    {
        string storagequery = "SELECT StorageId ,Storage FROM TypeofStorage";
        SqlCommand cmd = new SqlCommand(storagequery, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);

        ddlstorage.DataSource = ds.Tables[0];

        ddlstorage.DataTextField = "Storage";
        ddlstorage.DataValueField = "StorageId";
        ddlstorage.DataBind();
        ddlstorage.Items.Insert(0, "--चुनें--");

    }

    protected void bindPacking()
    {
        string packingquery = "SELECT PackingId ,Packing FROM TypeofPacking";
        SqlCommand cmd = new SqlCommand(packingquery, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);

        ddlpacking.DataSource = ds.Tables[0];

        ddlpacking.DataTextField = "Packing";
        ddlpacking.DataValueField = "PackingId";
        ddlpacking.DataBind();
        ddlpacking.Items.Insert(0, "--चुनें--");

    }
    
    protected void ddlstorage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlstorage.SelectedValue == "3")
        {
            ddlpacking.Items.FindByValue("4").Enabled = true;
        }
        else
        {
            ddlpacking.Items.FindByValue("4").Enabled = false;
        }

        //bindPacking();
    }

   
    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/Dist_Welcome.aspx");
    }
    protected void btnprint_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/Rpt_Print_WheatDCP.aspx");
    }
}
