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

public partial class State_Finance_RiceDCP_Correction : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    static string OldTrans = null;
    static string OldDistrict_id = null;
    static string OldYear = null;
    static string OldMonth = null;
    static string Oldhead_Id = null;

    static string OldTypeofRice = null;
    static string OldQuantity = null;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        if (Session["st_id"] != null)
        {
            if (!IsPostBack)
            {
                try
                {
                     bindDistrict();

                     bindHead();          
                }
                catch
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some Problem Occured,Please Try Again |');</script>");
                }
                txtqty.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    protected void bindHead()
    {
        string headquery = "SELECT head_id ,head FROM FIN_rice_Head";
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

    protected void bindDistrict()
    {
        string Distquery = "SELECT district_code ,district_name FROM pds.districtsmp order by district_code";
        SqlCommand cmd = new SqlCommand(Distquery, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);

        ddlDistrict.DataSource = ds.Tables[0];

        ddlDistrict.DataTextField = "district_name";
        ddlDistrict.DataValueField = "district_code";
        ddlDistrict.DataBind();
        ddlDistrict.Items.Insert(0, "--चुनें--");
    }

    protected void ddlTYPE_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedItem.Text == "--चुनें--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select District |');</script>");
            return;
        }

        string distid = ddlDistrict.SelectedValue.ToString();
        string year = ddlyear.SelectedItem.Text;
        string Month = ddlmonth.SelectedItem.Text;
        string HeadId = ddlhead.SelectedValue.ToString();

        string TypeofRice = ddlTYPE.SelectedItem.Text;

        string getvalue = " SELECT  TransID,District_id ,Year ,Month ,HeadId,TypeofRice,Quantity FROM FIN_DistrictStockRegister_RICE where District_id = '" + distid + "' and YEAR = '" + year + "' and MONTH = '" + Month + "'and HeadId = '" + HeadId + "' and TypeofRice = '" + TypeofRice + "'";
        SqlCommand cmd = new SqlCommand(getvalue, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            txtqty.Text = ds.Tables[0].Rows[0]["Quantity"].ToString();
            
            OldTrans = ds.Tables[0].Rows[0]["TransID"].ToString();
            OldDistrict_id = ds.Tables[0].Rows[0]["District_id"].ToString();
            OldYear = ds.Tables[0].Rows[0]["Year"].ToString();
            OldMonth = ds.Tables[0].Rows[0]["Month"].ToString();
            Oldhead_Id = ds.Tables[0].Rows[0]["HeadId"].ToString();

            OldTypeofRice = ds.Tables[0].Rows[0]["TypeofRice"].ToString();
            OldQuantity = ds.Tables[0].Rows[0]["Quantity"].ToString();
            btnsave.Visible = true;
        }

        else
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Quantity Not Found |');</script>");
            txtqty.Text = "";
            btnsave.Visible = false;
            
        }

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        try
        {

            if (Session["st_id"] != null)
            {
                if (ddlDistrict.SelectedValue != "0")
                {
                    if (ddlmonth.SelectedValue != "0")
                    {
                        if (ddlhead.SelectedValue != "0")
                        {
                            if (ddlTYPE.SelectedIndex != 0)
                            {
                                if (txtqty.Text.Trim() != "")
                                {
                                    # region Insert into Log

                                    string Browser = Request.Browser.Browser;

                                    string computername = System.Environment.MachineName;

                                    string IpAddress = Request.ServerVariables["REMOTE_ADDR"].ToString();

                                    string date = System.DateTime.Now.ToString();

                                    string strSession = HttpContext.Current.Session.SessionID;

                                    string chkLog = "SELECT count(*) FROM FIN_DistrictStockRegister_RICE_Log where TransId = '" + OldTrans + "' and District_id =  '" + ddlDistrict.SelectedValue + "' and HeadId = '" + Oldhead_Id + "' and Month = '" + OldMonth + "' and TypeofRice = '" + OldTypeofRice + "'";
                                    SqlCommand cmdLogCount = new SqlCommand(chkLog, con);
                                    string value = cmdLogCount.ExecuteScalar().ToString();
                                    int chk = Convert.ToInt16(value);

                                    if (chk == 0) // Insert In Log
                                    {
                                        String Logquery = "Insert into FIN_DistrictStockRegister_RICE_Log (TransId ,District_id ,Year ,Month  ,HeadId  ,TypeofRice ,Quantity ,UpdatedDate ,Browser ,IPAddress,SessionId) values ('" + OldTrans + "','" + OldDistrict_id + "','" + OldYear + "','" + OldMonth + "','" + Oldhead_Id + "','" + OldTypeofRice + "','" + OldQuantity + "','" + date + "','" + Browser + "','" + IpAddress + "','" + strSession + "')";
                                        SqlCommand cmdLogIns = new SqlCommand(Logquery, con);
                                        cmdLogIns.ExecuteNonQuery();
                                    }

                                    else  // Update Log
                                    {
                                        string updateLogquery = "Update FIN_DistrictStockRegister_RICE_Log set Quantity = '" + OldQuantity + "',UpdatedDate = '" + date + "' where TransID = '" + OldTrans + "' and District_id = '" + OldDistrict_id + "' and HeadId = '" + Oldhead_Id + "' and Month = '" + OldMonth + "' and TypeofRice = '" + OldTypeofRice + "'";
                                        SqlCommand cmdUpdateqry = new SqlCommand(updateLogquery, con);
                                        int u = cmdUpdateqry.ExecuteNonQuery();
                                    }

                                    # endregion

                                    # region UpdateOperation

                                    string updatequery = "Update FIN_DistrictStockRegister_RICE set Quantity = '" + txtqty.Text.Trim() + "' where TransID = '" + OldTrans + "' and District_id = '" + OldDistrict_id + "' and Year = '" + OldYear + "' and Month = '" + OldMonth + "' and HeadId = '" + Oldhead_Id + "' and TypeofRice = '" + OldTypeofRice + "' ";

                                    SqlCommand cmdUpdate = new SqlCommand(updatequery, con);
                                    cmdUpdate.ExecuteNonQuery();

                                    # endregion

                                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Sucessfully Updated |');</script>");
                                    txtqty.Text = "";
                                }

                                else
                                {
                                    // Qty Bharo
                                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Insert New Quantity |');</script>");
                                }
                            }

                            else
                            {
                                // Type of rice cbuno
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Type of Rice |');</script>");
                            }
                        }

                        else
                        {
                            // Head Chuno
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Head |');</script>");
                        }
                    }

                    else
                    {
                        // MOnth Chuno
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Month |');</script>");
                    }
                }
                else
                {
                    // district selection is 0
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select District |');</script>");
                }

            }
            //alter table table_name add primary key (col_name1, col_name2);
            else
            {
                Response.Redirect("~/MainLogin.aspx");
            }
        }
        catch
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Try Again |');</script>");

        }

        finally
        {

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/State/Ho_FinanceSection.aspx");
    }
}
