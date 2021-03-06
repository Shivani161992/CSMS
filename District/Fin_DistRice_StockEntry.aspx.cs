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
                txtqty.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
               
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

                                string strSession = HttpContext.Current.Session.SessionID;

                                string IpAddress = Request.ServerVariables["REMOTE_ADDR"].ToString();

                                string date = System.DateTime.Now.ToString();

                                //string tdate = get_MDY(date);

                                # region RandomId

                                Random rnd = new Random();
                                string random_first = Convert.ToString(rnd.Next(9, 99));   // creates a number between 10 and 98
                                string random_second = Convert.ToString(rnd.Next(9, 99)); // creates a number between 11 and 89

                                string random_third = Convert.ToString(rnd.Next(11, 50));  // creates a number between 11 and 89

                                string TransID = district_id + random_first + random_second + random_third;

                                # endregion

                                // Check Duplicate entry Here

                                string chkduplicate = "SELECT count(*) FROM FIN_DistrictStockRegister_RICE where District_id = '" + district_id + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and HeadId = '" + head_Id + "'  and TypeofRice = '" + TYPEofRICE + "'";
                                SqlCommand cmdCheck = new SqlCommand(chkduplicate, con);
                                string value = cmdCheck.ExecuteScalar().ToString();

                                int chk = Convert.ToInt16(value);

                                if (chk > 0)
                                {
                                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('वर्ष " + year + " " + month + " के सम्बंधित head " + TYPEofRICE + " की जानकारी पहले सुरक्षित हो चुकी है');</script>");
                                    
                                }
                                else
                                {
                                    # region insert
                                    string query = "Insert into FIN_DistrictStockRegister_RICE (TransID,District_id ,Year,Month ,HeadId,TypeofRice ,Quantity,Date,Browser ,IPAddress ,SessionId) values ('" + TransID + "','" + district_id + "' ,'" + year + "','" + month + "' ,'" + head_Id + "','" + TYPEofRICE + "' ,'" + quantity + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "' ,'" + strSession + "')";

                                    SqlCommand cmd = new SqlCommand(query, con);

                                    int x = cmd.ExecuteNonQuery();

                                    txtqty.Text = "";
                                    # endregion

                                    # region Totalpurchase

                                    string chktotpur = "SELECT count(*) FROM FIN_DistrictStockRegister_RICE where District_id = '" + district_id + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and HeadId = '1004'  and TypeofRice = 'CMR'";
                                    SqlCommand cmdtot = new SqlCommand(chktotpur, con);
                                    string valuetot = cmdtot.ExecuteScalar().ToString();

                                    int chk1 = Convert.ToInt16(valuetot);

                                    if (chk1 == 0)
                                    {
                                        string selsum = "select isnull(sum(Quantity),0)tot from FIN_DistrictStockRegister_RICE where District_id = '" + district_id + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and HeadId in (1002,1003)  and TypeofRice = 'CMR' ";

                                        SqlCommand cmd1 = new SqlCommand(selsum, con);
                                        double sum1 = Convert.ToDouble(cmd1.ExecuteScalar());
                                        string query1 = "Insert into FIN_DistrictStockRegister_RICE (District_id ,Year,Month ,HeadId,TypeofRice ,Quantity,Date,Browser ,IPAddress ,SessionId) values ('" + district_id + "' ,'" + year + "','" + month + "' ,'1004','CMR' ,'" + sum1 + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "' ,'" + strSession + "')";

                                        SqlCommand cmd11 = new SqlCommand(query1, con);

                                        int x1 = cmd11.ExecuteNonQuery();

                                        // Insert CMR for 1004

                                    }

                                    else
                                    {
                                        string selsum = "select isnull(sum(Quantity),0)tot from FIN_DistrictStockRegister_RICE where District_id = '" + district_id + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and HeadId in (1002,1003)  and TypeofRice = 'CMR' ";

                                        SqlCommand cmd1 = new SqlCommand(selsum, con);
                                        double sum1 = Convert.ToDouble(cmd1.ExecuteScalar());
                                        string query1 = "Update FIN_DistrictStockRegister_RICE set Quantity = '" + sum1 + "' where District_id = '" + district_id + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and HeadId = 1004  and TypeofRice = 'CMR' ";

                                        SqlCommand cmd11 = new SqlCommand(query1, con);

                                        int x1 = cmd11.ExecuteNonQuery();

                                        // Update for 1004 CMR

                                    }

                                    # endregion

                                    # region TotalReceipt

                                    string chktotpur1 = "SELECT count(*) FROM FIN_DistrictStockRegister_RICE where District_id = '" + district_id + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and HeadId = '1007'  and TypeofRice = 'CMR'";
                                    SqlCommand cmdtot1 = new SqlCommand(chktotpur1, con);
                                    string valuetot1 = cmdtot1.ExecuteScalar().ToString();

                                    int chk11 = Convert.ToInt16(valuetot1);

                                    if (chk11 == 0)
                                    {
                                        string selsum1 = "select isnull(sum(Quantity),0)tot from FIN_DistrictStockRegister_RICE where District_id = '" + district_id + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and HeadId in (1005,1006)  and TypeofRice = 'CMR' ";

                                        SqlCommand cmd2 = new SqlCommand(selsum1, con);
                                        double sum2 = Convert.ToDouble(cmd2.ExecuteScalar());
                                        string query2 = "Insert into FIN_DistrictStockRegister_RICE (District_id ,Year,Month ,HeadId,TypeofRice ,Quantity,Date,Browser ,IPAddress ,SessionId) values ('" + district_id + "' ,'" + year + "','" + month + "' ,'1007','CMR' ,'" + sum2 + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "' ,'" + strSession + "')";

                                        SqlCommand cmd12 = new SqlCommand(query2, con);

                                        int x2 = cmd12.ExecuteNonQuery();

                                        // Insert CMR for 1007

                                    }

                                    else
                                    {
                                        string selsum2 = "select isnull(sum(Quantity),0)tot from FIN_DistrictStockRegister_RICE where District_id = '" + district_id + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and HeadId in (1005,1006)  and TypeofRice = 'CMR' ";

                                        SqlCommand cmd2 = new SqlCommand(selsum2, con);
                                        double sum2 = Convert.ToDouble(cmd2.ExecuteScalar());
                                        string query2 = "Update FIN_DistrictStockRegister_RICE set Quantity = '" + sum2 + "' where District_id = '" + district_id + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and HeadId = 1007  and TypeofRice = 'CMR' ";

                                        SqlCommand cmd12 = new SqlCommand(query2, con);

                                        int x1 = cmd12.ExecuteNonQuery();

                                        // Update for 1007 CMR

                                    }

                                    # endregion

                                    # region TotalSale

                                    string chktotpur3 = "SELECT count(*) FROM FIN_DistrictStockRegister_RICE where District_id = '" + district_id + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and HeadId = '1020'  and TypeofRice = 'CMR'";
                                    SqlCommand cmdtot3 = new SqlCommand(chktotpur3, con);
                                    string valuetot3 = cmdtot3.ExecuteScalar().ToString();

                                    int chk3 = Convert.ToInt16(valuetot3);

                                    if (chk3 == 0)
                                    {
                                        string selsum3 = "select isnull(sum(Quantity),0)tot from FIN_DistrictStockRegister_RICE where District_id = '" + district_id + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and HeadId in (1008,1009,1010,1011,1012,1013,1014,1015,1016,1017,1018,1019)  and TypeofRice = 'CMR' ";

                                        SqlCommand cmd3 = new SqlCommand(selsum3, con);
                                        double sum3 = Convert.ToDouble(cmd3.ExecuteScalar());
                                        string query3 = "Insert into FIN_DistrictStockRegister_RICE (District_id ,Year,Month ,HeadId,TypeofRice ,Quantity,Date,Browser ,IPAddress ,SessionId) values ('" + district_id + "' ,'" + year + "','" + month + "' ,'1020','CMR' ,'" + sum3 + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "' ,'" + strSession + "')";

                                        SqlCommand cmd31 = new SqlCommand(query3, con);

                                        int x3 = cmd31.ExecuteNonQuery();

                                        // Insert CMR for 1007

                                    }

                                    else
                                    {
                                        string selsum3 = "select isnull(sum(Quantity),0)tot from FIN_DistrictStockRegister_RICE where District_id = '" + district_id + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and HeadId in (1008,1009,1010,1011,1012,1013,1014,1015,1016,1017,1018,1019)  and TypeofRice = 'CMR' ";

                                        SqlCommand cmd3 = new SqlCommand(selsum3, con);
                                        double sum3 = Convert.ToDouble(cmd3.ExecuteScalar());
                                        string query3 = "Update FIN_DistrictStockRegister_RICE set Quantity = '" + sum3 + "' where District_id = '" + district_id + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and HeadId = 1020  and TypeofRice = 'CMR' ";

                                        SqlCommand cmd31 = new SqlCommand(query3, con);

                                        int x3 = cmd31.ExecuteNonQuery();

                                        // Update for 1007 CMR

                                    }

                                    # endregion

                                    # region TotalIssue

                                    string chkt4 = "SELECT count(*) FROM FIN_DistrictStockRegister_RICE where District_id = '" + district_id + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and HeadId = '1023'  and TypeofRice = 'CMR'";
                                    SqlCommand cmd5 = new SqlCommand(chkt4, con);
                                    string value5 = cmd5.ExecuteScalar().ToString();

                                    int chk5 = Convert.ToInt16(value5);

                                    if (chk5 == 0)
                                    {
                                        string selsum5 = "select isnull(sum(Quantity),0)tot from FIN_DistrictStockRegister_RICE where District_id = '" + district_id + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and HeadId in (1020,1021,1022)  and TypeofRice = 'CMR' ";

                                        SqlCommand cmd6 = new SqlCommand(selsum5, con);
                                        double sum5 = Convert.ToDouble(cmd6.ExecuteScalar());
                                        string query5 = "Insert into FIN_DistrictStockRegister_RICE (District_id ,Year,Month ,HeadId,TypeofRice ,Quantity,Date,Browser ,IPAddress ,SessionId) values ('" + district_id + "' ,'" + year + "','" + month + "' ,'1023','CMR' ,'" + sum5 + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "' ,'" + strSession + "')";

                                        SqlCommand cmd51 = new SqlCommand(query5, con);

                                        int x5 = cmd51.ExecuteNonQuery();

                                        // Insert CMR for 1007

                                    }

                                    else
                                    {
                                        string selsum5 = "select isnull(sum(Quantity),0)tot from FIN_DistrictStockRegister_RICE where District_id = '" + district_id + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and HeadId in (1020,1021,1022)  and TypeofRice = 'CMR' ";

                                        SqlCommand cmd6 = new SqlCommand(selsum5, con);
                                        double sum5 = Convert.ToDouble(cmd6.ExecuteScalar());
                                        string query5 = "Update FIN_DistrictStockRegister_RICE set Quantity = '" + sum5 + "' where District_id = '" + district_id + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and HeadId = 1023  and TypeofRice = 'CMR' ";

                                        SqlCommand cmd51 = new SqlCommand(query5, con);

                                        int x5 = cmd51.ExecuteNonQuery();

                                        // Update for 1007 CMR

                                    }

                                    # endregion

                                    # region closing

                                    string chkt6 = "SELECT count(*) FROM FIN_DistrictStockRegister_RICE where District_id = '" + district_id + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and HeadId = '1028'  and TypeofRice = 'CMR'";
                                    SqlCommand cmd7 = new SqlCommand(chkt6, con);
                                    string value6 = cmd7.ExecuteScalar().ToString();

                                    int chk6 = Convert.ToInt16(value6);

                                    if (chk6 == 0)
                                    {
                                        string selsum6 = "select isnull(sum(Quantity),0)tot from FIN_DistrictStockRegister_RICE where District_id = '" + district_id + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and HeadId in (1007,1024,1025)  and TypeofRice = 'CMR' ";

                                        SqlCommand cmd9 = new SqlCommand(selsum6, con);
                                        double sum6 = Convert.ToDouble(cmd9.ExecuteScalar());
                                        
                                        string selsum7 = "select isnull(sum(Quantity),0)tot from FIN_DistrictStockRegister_RICE where District_id = '" + district_id + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and HeadId in (1020,1026,1027)  and TypeofRice = 'CMR' ";

                                        SqlCommand cmd91 = new SqlCommand(selsum7, con);
                                        double sum7 = Convert.ToDouble(cmd91.ExecuteScalar());

                                        double sum8 = sum6 - sum7;

                                        string query6 = "Insert into FIN_DistrictStockRegister_RICE (District_id ,Year,Month ,HeadId,TypeofRice ,Quantity,Date,Browser ,IPAddress ,SessionId) values ('" + district_id + "' ,'" + year + "','" + month + "' ,'1028','CMR' ,'" + sum8 + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "' ,'" + strSession + "')";

                                        SqlCommand cmd61 = new SqlCommand(query6, con);

                                        int x6 = cmd61.ExecuteNonQuery();

                                        // Insert CMR for 1007

                                    }

                                    else
                                    {
                                        string selsum6 = "select isnull(sum(Quantity),0)tot from FIN_DistrictStockRegister_RICE where District_id = '" + district_id + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and HeadId in (1007,1024,1025)  and TypeofRice = 'CMR' ";

                                        SqlCommand cmd6 = new SqlCommand(selsum6, con);
                                        double sum6 = Convert.ToDouble(cmd6.ExecuteScalar());


                                        string selsum7 = "select isnull(sum(Quantity),0)tot from FIN_DistrictStockRegister_RICE where District_id = '" + district_id + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and HeadId in (1020,1026,1027)  and TypeofRice = 'CMR' ";

                                        SqlCommand cmd71 = new SqlCommand(selsum7, con);
                                        double sum7 = Convert.ToDouble(cmd71.ExecuteScalar());

                                        double sum8 = sum6 - sum7;
                                        string query6 = "Update FIN_DistrictStockRegister_RICE set Quantity = '" + sum8 + "' where District_id = '" + district_id + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and HeadId = 1028  and TypeofRice = 'CMR' ";

                                        SqlCommand cmd61 = new SqlCommand(query6, con);

                                        int x6 = cmd61.ExecuteNonQuery();

                                        // Update for 1007 CMR

                                    }

                                    # endregion


                                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Saved, Thank you |');</script>");
                                    //ddlmonth.SelectedValue = "0";
                                    //checkOpening();
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
        Response.Redirect("~/District/FIN_Print_DistrictRice.aspx");   

    }

   // protected void ddlmonth_SelectedIndexChanged(object sender, EventArgs e)
   // {

   //     checkOpening();

   //         //string previousMonth = DateTime.Now.AddMonths(-1).ToString("MMMM");

   //         //string MonthNo = ddlmonth.SelectedValue;
   //         //int preMonth = Convert.ToInt16(MonthNo.ToString());

   //         //int iMonthNo = preMonth - 1;
   //         //DateTime dtDate = new DateTime(2000, iMonthNo, 1);
   //         //string sMonthName = dtDate.ToString("MMM");
   //         //string sMonthFullName = dtDate.ToString("MMMM");

   //         //string Clobalance = "SELECT count(*) FROM DistrictStockRegister_RICE where HeadId = '1028' and District_id = '" + Session["dist_id"] + "' and YEAR = '" + ddlyear + "' and MONTH = '" + sMonthFullName + "'";
   //         //SqlCommand cmdClose = new SqlCommand(Clobalance, con);

   //         //Int32 count1 = (Int32)cmdClose.ExecuteScalar();
   //}

    public void bindHead()
    {

        string headquery = "SELECT head_id ,head FROM FIN_Rice_Head where head_id not in (1004,1007,1020,1023,1028)";
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


    protected void Button1_Click(object sender, EventArgs e)
    {
        ddlhead.Enabled = true;
    }

    protected void checkOpening()
    {
        string balance = "SELECT count(*) FROM FIN_DistrictStockRegister_RICE where HeadId = '1001' and District_id = '" + Session["dist_id"] + "' and YEAR = '" + ddlyear.SelectedItem.Text + "' and MONTH = '" + ddlmonth.SelectedItem.Text + "' and TypeofRice in ('CMR','LEVY')";
        SqlCommand cmd = new SqlCommand(balance, con);

        Int32 count = (Int32)cmd.ExecuteScalar();

        if (count == 0 || count == 1)  // Opening Balance Not Found
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('पहले CMR and LEVY का Opening Balance भरें |');</script>");

            string headquery = "SELECT head_id ,head FROM FIN_Rice_Head";
            SqlCommand cmd11 = new SqlCommand(headquery, con);
            SqlDataAdapter da11 = new SqlDataAdapter(cmd11);
            DataSet ds11 = new DataSet();
            da11.Fill(ds11);

            ddlhead.DataSource = ds11.Tables[0];

            ddlhead.DataTextField = "head";
            ddlhead.DataValueField = "head_id";
            ddlhead.DataBind();
            ddlhead.Items.Insert(0, "--चुनें--");

            ddlhead.SelectedValue = "1001";
            ddlhead.Enabled = false;
        }
        else   // Found
        {
            string headquery = "SELECT head_id ,head FROM FIN_Rice_Head where head_id not in (1001,1028) ";
            SqlCommand cmd11 = new SqlCommand(headquery, con);
            SqlDataAdapter da11 = new SqlDataAdapter(cmd11);
            DataSet ds11 = new DataSet();
            da11.Fill(ds11);

            ddlhead.DataSource = ds11.Tables[0];

            ddlhead.DataTextField = "head";
            ddlhead.DataValueField = "head_id";
            ddlhead.DataBind();
            ddlhead.Items.Insert(0, "--चुनें--");
            ddlhead.Enabled = true;

        }
    }
    protected void ddlhead_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlTYPE.SelectedIndex = 0;
    }
}
