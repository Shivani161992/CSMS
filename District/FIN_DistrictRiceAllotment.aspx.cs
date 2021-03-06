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

public partial class District_FIN_DistrictRiceAllotment : System.Web.UI.Page
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

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
                catch
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('पुनः प्रयास करें |');</script>");
                }
                
                txtMonthAllot.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
                txtsalesofFCI.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
                txtBPLAllot.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
                txtActualBPLallot.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
                txtSeniourallot.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
                txtActualSeniourAllot.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");

            }
        }

        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

    }

    public void bindHead()
    {

        string headquery = "SELECT  head_id ,head FROM FIN_Rice_Head where head_id not in (1001,1002,1003,1004,1005,1006,1007,1020,1021,1022,1023,1024,1025,1026,1027,1028)";
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

    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/Dist_Welcome.aspx");
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            string district_id = Session["dist_id"].ToString();

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            # region BPL_APL

            if ( Label1.Text == "2")
            {
                if (ddlmonth.SelectedValue == "0")
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Month |');</script>");

                    ddlmonth.Focus();
                    return;
                }

                try
                {
                    string year = ddlyear.SelectedItem.Text;

                    string month = ddlmonth.SelectedItem.Text;
                      
                        if (txtBPLAllot.Text == "")
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('BPL आवंटन नहीं भरा है, जानकारी भरे अथवा शुन्य भरें |');</script>");
                            txtBPLAllot.Focus();
                            return;
                        }
                        if (txtActualBPLallot.Text == "")
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Actual BPL आवंटन नहीं भरा है, जानकारी भरे अथवा शुन्य भरें|');</script>");
                            txtActualBPLallot.Focus();
                            return;
                        }
                        if (txtSeniourallot.Text == "")
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Seniour आवंटन नहीं भरा है, जानकारी भरे अथवा शुन्य भरें |');</script>");
                            txtSeniourallot.Focus();
                            return;
                        }

                        if (txtActualSeniourAllot.Text == "")
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Actual Seniour आवंटन नहीं भरा है, जानकारी भरे अथवा शुन्य भरें |');</script>");
                            txtActualSeniourAllot.Focus();
                            return;
                        }

                        
                        string BPLAllot = txtBPLAllot.Text.Trim();

                        string ActualBPLallot = txtActualBPLallot.Text.Trim();

                        string Seniourallot = txtSeniourallot.Text.Trim();

                        string ActualSeniourAllot = txtActualSeniourAllot.Text.Trim();

                        string date = System.DateTime.Now.ToString();

                        //string tdate = get_MDY(date);

                        string chkduplicate1 = "SELECT count(BPLallotment_outof_APL) FROM FIN_DistrictAllotment_RICE where District_id = '" + district_id + "' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                        SqlCommand cmdCheck1 = new SqlCommand(chkduplicate1, con);
                        string value1 = cmdCheck1.ExecuteScalar().ToString();

                        int chk1 = Convert.ToInt16(value1);

                        if (chk1 > 0)
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('वर्ष " + year + " " + month + " APL,BPL की जानकारी पहले सुरक्षित हो चुकी है');</script>");
                            return;
                        }

                        else
                        {
                            string query = "Insert into FIN_DistrictAllotment_RICE (District_id ,Year,Month ,BPLallotment_outof_APL,ActualDistribution_inBPL_againstAPL,AllotmentforSeniourCitizen_outof_BPLAllotment,ActualDistribution_SeniourCitizen ,Date) values ('" + district_id + "' ,'" + year + "','" + month + "' ,'" + BPLAllot + "','" + ActualBPLallot + "','" + Seniourallot + "','" + ActualSeniourAllot + "','" + date + "')";

                            SqlCommand cmd = new SqlCommand(query, con);

                            int x = cmd.ExecuteNonQuery();

                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Saved, Thank you |');</script>");
                            //ddlmonth.SelectedValue = "0";

                            txtBPLAllot.Text = "";

                            txtActualBPLallot.Text = "";

                            txtSeniourallot.Text = "";

                            txtActualSeniourAllot.Text = "";
                        }
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

            # endregion

            # region Month_Allotment
            else
            {
                if (ddlmonth.SelectedValue == "0")
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Month |');</script>");

                    ddlmonth.Focus();
                    return;
                }

                if (ddlhead.SelectedIndex == 0)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Head |');</script>");

                    ddlhead.Focus();

                    return;
                }


                try
                {
                    string year = ddlyear.SelectedItem.Text;

                    string month = ddlmonth.SelectedItem.Text;

                    string head_Id = ddlhead.SelectedValue.ToString();

                    string chkduplicate = "SELECT count(*) FROM FIN_DistrictAllotment_RICE where District_id = '" + district_id + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and HeadId = '" + head_Id + "'";
                    SqlCommand cmdCheck = new SqlCommand(chkduplicate, con);
                    string value = cmdCheck.ExecuteScalar().ToString();

                    int chk = Convert.ToInt16(value);

                    if (chk > 0)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('वर्ष " + year + " " + month + " के सम्बंधित Scheme  की जानकारी पहले सुरक्षित हो चुकी है');</script>");
                        return;
                    }

                    else
                    {

                        if (txtMonthAllot.Text == "")
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('माह का आवंटन नहीं भरा है, जानकारी भरे अथवा शुन्य भरें |');</script>");
                            txtMonthAllot.Focus();
                            return;
                        }
                        if (txtsalesofFCI.Text == "")
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('FCI Sale नहीं भरा है, जानकारी भरे अथवा शुन्य भरें |');</script>");
                            txtsalesofFCI.Focus();
                            return;
                        }
                        

                        
                        string MonthAllot = txtMonthAllot.Text.Trim();

                        string salesofFCI = txtsalesofFCI.Text.Trim();

                       
                        string date = System.DateTime.Now.ToString();

                        //string tdate = get_MDY(date);

                        string query = "Insert into FIN_DistrictAllotment_RICE (District_id ,Year,Month ,HeadId,MonthlyAllotment,Sales_outof_FCI ,Date) values ('" + district_id + "' ,'" + year + "','" + month + "' ,'" + head_Id + "' ,'" + MonthAllot + "','" + salesofFCI + "','" + date + "')";

                        SqlCommand cmd = new SqlCommand(query, con);

                        int x = cmd.ExecuteNonQuery();


                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Saved, Thank you |');</script>");
                        //ddlmonth.SelectedValue = "0";

                        txtMonthAllot.Text = "";

                        txtsalesofFCI.Text = "";

                    }

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


            # endregion
  
            
        }

        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }
    

    private String get_MDY(string inDate)
    {
        string dat = "";
        if (inDate != "")
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            dat = (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));

        }
        return dat;
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Label1.Text = "1";
        Label8.Visible = true;
        ddlhead.Visible = true;
        lbl1.Visible = true;
        txtMonthAllot.Visible = true;
        lbl2.Visible = true;
        txtsalesofFCI.Visible = true;

        lbl3.Visible = false;
        txtBPLAllot.Visible = false;
        lbl4.Visible = false;
        txtActualBPLallot.Visible = false;
        lbl5.Visible = false;
        txtSeniourallot.Visible = false;
        lbl6.Visible = false;
        txtActualSeniourAllot.Visible = false;

        btnsave.Visible = true;
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Label1.Text = "2";

        lbl3.Visible = true;
        txtBPLAllot.Visible = true;
        lbl4.Visible = true;
        txtActualBPLallot.Visible = true;
        lbl5.Visible = true;
        txtSeniourallot.Visible = true;
        lbl6.Visible = true;
        txtActualSeniourAllot.Visible = true;

        Label8.Visible = false;
        ddlhead.Visible = false;
        lbl1.Visible = false;
        txtMonthAllot.Visible = false;
        lbl2.Visible = false;
        txtsalesofFCI.Visible = false;

        btnsave.Visible = true;

        bindHead();

    }
}
