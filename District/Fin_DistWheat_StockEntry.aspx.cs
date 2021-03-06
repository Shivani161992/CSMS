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

                                        string IpAddress = Request.ServerVariables["REMOTE_ADDR"].ToString();

                                        string date = System.DateTime.Now.ToString();

                                        string strSession = HttpContext.Current.Session.SessionID;

                                        # region RandomId

                                        Random rnd = new Random();
                                        string random_first = Convert.ToString(rnd.Next(9, 99));   // creates a number between 10 and 98
                                        string random_second = Convert.ToString(rnd.Next(9, 99)); // creates a number between 11 and 89

                                        string random_third = Convert.ToString(rnd.Next(11, 50));  // creates a number between 11 and 89

                                        string TransID = DistrictId + random_first + random_second + random_third;

                                        # endregion

                                        // Duplicate Check before Entry

                                        string chkduplicate = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '" + head_Id + "'  and Storage = '" + storage + "' and Packing = '" + packing + "'";
                                        SqlCommand cmdCheck = new SqlCommand(chkduplicate,con);
                                        string value = cmdCheck.ExecuteScalar().ToString();

                                        int chk = Convert.ToInt16(value);

                                        if (chk > 0)
                                        {
                                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('year " + year + " " + month + " के सम्बंधित Scheme के " + storage + "  " + packing + " की जानकारी पहले सुरक्षित हो चुकी है');</script>");
                                        }
                                        else
                                        {
                                  
                                            # region insert
                                            string query = "Insert into FIN_DistrictStockRegister_Wheat (TransID,District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + TransID + "','" + DistrictId + "' ,'" + year + "','" + month + "' ,'" + head_Id + "','" + storage + "' ,'" + packing + "' ,'" + quantity + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                            SqlCommand cmd = new SqlCommand(query, con);

                                            int x = cmd.ExecuteNonQuery();
                                                                               
                                            if (x > 0)
                                            {
                                                txtqty.Text = "";

                                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Saved, Thank you |');</script>");
                                                //bindPacking();
                                            }

                                            # endregion

                                            # region Total Availablity
                                            // cap Start

                                            # region TotalAvailibilty_Cap-Jutenew
                                            string totavail = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1006' and Storage = 'Cap' and Packing = 'Jute New'";
                                            SqlCommand cmdcapjute = new SqlCommand(totavail, con);
                                            string valueavail = cmdcapjute.ExecuteScalar().ToString();
                                            int val = Convert.ToInt16(valueavail);

                                            if (val == 0)
                                            {
                                                string selcapjute = "select sum(Quantity)TOT from FIN_DistrictStockRegister_Wheat where head_Id in (1001,1002,1003,1004,1005) and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'Jute New'";
                                                SqlCommand cmd1 = new SqlCommand(selcapjute, con);
                                                double sum = Convert.ToDouble(cmd1.ExecuteScalar());


                                                string querycaljute = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1006','Cap' ,'Jute New' ,'" + sum + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmd2 = new SqlCommand(querycaljute, con);
                                                int xyz = cmd2.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selcapjute = "select sum(Quantity)TOT from FIN_DistrictStockRegister_Wheat where head_Id in (1001,1002,1003,1004,1005) and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'Jute New'";
                                                SqlCommand cmd1 = new SqlCommand(selcapjute, con);
                                                double sum = Convert.ToDouble(cmd1.ExecuteScalar());


                                                string querycaljute = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sum + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1006' and Storage = 'Cap' and Packing = 'Jute New'";
                                                SqlCommand cmd2 = new SqlCommand(querycaljute, con);
                                                cmd2.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region TotalAvailibilty_Cap-HDPE
                                            string totCH = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1006' and Storage = 'Cap' and Packing = 'HDPE'";
                                            SqlCommand cmdcapH = new SqlCommand(totCH, con);
                                            string valueCH = cmdcapH.ExecuteScalar().ToString();
                                            int valCH = Convert.ToInt16(valueCH);

                                            if (valCH == 0)
                                            {
                                                string selcapH = "select sum(Quantity)TOT from FIN_DistrictStockRegister_Wheat where head_Id in (1001,1002,1003,1004,1005) and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'HDPE' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCH = new SqlCommand(selcapH, con);
                                                double sumCH = Convert.ToDouble(cmdCH.ExecuteScalar());


                                                string querycalH = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1006','Cap' ,'HDPE' ,'" + sumCH + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCH2 = new SqlCommand(querycalH, con);
                                                int CH = cmdCH2.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selcapH = "select sum(Quantity)TOT from FIN_DistrictStockRegister_Wheat where head_Id in (1001,1002,1003,1004,1005) and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'HDPE' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCH = new SqlCommand(selcapH, con);
                                                double sumH = Convert.ToDouble(cmdCH.ExecuteScalar());


                                                string querycalH = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumH + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1006' and Storage = 'Cap' and Packing = 'HDPE'";
                                                SqlCommand cmdCH2 = new SqlCommand(querycalH, con);
                                                cmdCH2.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region TotalAvailibilty_Cap-Jute Once Used
                                            string totCO = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1006' and Storage = 'Cap' and Packing = 'Jute Once Used'";
                                            SqlCommand cmdcapO = new SqlCommand(totCO, con);
                                            string valueCO = cmdcapO.ExecuteScalar().ToString();
                                            int valCO = Convert.ToInt16(valueCO);

                                            if (valCO == 0)
                                            {
                                                string selcapO = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in (1001,1002,1003,1004,1005) and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'Jute Once Used' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCO = new SqlCommand(selcapO, con);
                                                string allsum = cmdCO.ExecuteScalar().ToString();
                                                double sumCO = Convert.ToDouble(allsum);


                                                string querycalO = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1006','Cap' ,'Jute Once Used' ,'" + sumCO + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCO2 = new SqlCommand(querycalO, con);
                                                int CO = cmdCO2.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selcapO = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in (1001,1002,1003,1004,1005) and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'Jute Once Used' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCO = new SqlCommand(selcapO, con);
                                                string allsum = cmdCO.ExecuteScalar().ToString();
                                                double sumCO = Convert.ToDouble(allsum);
                                               // double sumO = Convert.ToDouble(cmdCO.ExecuteScalar());


                                                string querycalO = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumCO + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1006' and Storage = 'Cap' and Packing = 'Jute Once Used'";
                                                SqlCommand cmdCO2 = new SqlCommand(querycalO, con);
                                                cmdCO2.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            // Covered Start

                                            # region TotalAvailibilty_Covered-Jutenew
                                            string totavail1 = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1006' and Storage = 'Covered' and Packing = 'Jute New'";
                                            SqlCommand cmdCoveredjute = new SqlCommand(totavail1, con);
                                            string valueavail1 = cmdCoveredjute.ExecuteScalar().ToString();
                                            int val1 = Convert.ToInt16(valueavail1);

                                            if (val1 == 0)
                                            {
                                                string selCoveredjute = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in (1001,1002,1003,1004,1005) and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'Jute New'";
                                                SqlCommand cmd1 = new SqlCommand(selCoveredjute, con);
                                                double sum1 = Convert.ToDouble(cmd1.ExecuteScalar());


                                                string queryCoveredjute = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1006','Covered' ,'Jute New' ,'" + sum1 + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmd21 = new SqlCommand(queryCoveredjute, con);
                                                int xyz1 = cmd21.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selCoveredjute = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in (1001,1002,1003,1004,1005) and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'Jute New'";
                                                SqlCommand cmd11 = new SqlCommand(selCoveredjute, con);
                                                double sum1 = Convert.ToDouble(cmd11.ExecuteScalar());


                                                string queryCoveredjute = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sum1 + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1006' and Storage = 'Covered' and Packing = 'Jute New'";
                                                SqlCommand cmd2 = new SqlCommand(queryCoveredjute, con);
                                                cmd2.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region TotalAvailibilty_Covered-HDPE
                                            string totCoH = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1006' and Storage = 'Covered' and Packing = 'HDPE'";
                                            SqlCommand cmdcoH = new SqlCommand(totCoH, con);
                                            string valueCoH = cmdcoH.ExecuteScalar().ToString();
                                            int valCoH = Convert.ToInt16(valueCoH);

                                            if (valCoH == 0)
                                            {
                                                string selcoH = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in (1001,1002,1003,1004,1005) and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'HDPE' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCoH = new SqlCommand(selcoH, con);
                                                double sumCoH = Convert.ToDouble(cmdCoH.ExecuteScalar());


                                                string querycoH = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1006','Covered' ,'HDPE' ,'" + sumCoH + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCoH2 = new SqlCommand(querycoH, con);
                                                int CH = cmdCoH2.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selcoH = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in (1001,1002,1003,1004,1005) and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'HDPE' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCoH = new SqlCommand(selcoH, con);
                                                double sumcoH = Convert.ToDouble(cmdCoH.ExecuteScalar());


                                                string querycoH = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumcoH + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1006' and Storage = 'Covered' and Packing = 'HDPE'";
                                                SqlCommand cmdCoH2 = new SqlCommand(querycoH, con);
                                                cmdCoH2.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region TotalAvailibilty_Covered-Jute Once Used
                                            string totCOj = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1006' and Storage = 'Covered' and Packing = 'Jute Once Used'";
                                            SqlCommand cmdcapOj = new SqlCommand(totCOj, con);
                                            string valueCOj = cmdcapOj.ExecuteScalar().ToString();
                                            int valCj = Convert.ToInt16(valueCOj);

                                            if (valCj == 0)
                                            {
                                                string selcjO = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in (1001,1002,1003,1004,1005) and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'Jute Once Used' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCjO = new SqlCommand(selcjO, con);
                                                double sumCOj = Convert.ToDouble(cmdCjO.ExecuteScalar());


                                                string querycjO = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1006','Covered' ,'Jute Once Used' ,'" + sumCOj + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCOj2 = new SqlCommand(querycjO, con);
                                                int CO = cmdCOj2.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selcjO = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in (1001,1002,1003,1004,1005) and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'Jute Once Used' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCjO = new SqlCommand(selcjO, con);
                                                double sumjO = Convert.ToDouble(cmdCjO.ExecuteScalar());


                                                string querycjO = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumjO + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1006' and Storage = 'Covered' and Packing = 'Jute Once Used'";
                                                SqlCommand cmdCjO2 = new SqlCommand(querycjO, con);
                                                cmdCjO2.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            // SYLO Start

                                            # region TotalAvailibilty_Sylo-Jutenew
                                            string totavail11 = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1006' and Storage = 'SILO' and Packing = 'Jute New'";
                                            SqlCommand cmdSylojute1 = new SqlCommand(totavail11, con);
                                            string valueavail11 = cmdSylojute1.ExecuteScalar().ToString();
                                            int val11 = Convert.ToInt16(valueavail11);

                                            if (val11 == 0)
                                            {
                                                string selSylojute1 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in (1001,1002,1003,1004,1005) and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Jute New'";
                                                SqlCommand cmd11 = new SqlCommand(selSylojute1, con);
                                                double sum11 = Convert.ToDouble(cmd11.ExecuteScalar());


                                                string querySylojute1 = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1006','SILO' ,'Jute New' ,'" + sum11 + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmd211 = new SqlCommand(querySylojute1, con);
                                                int xyz11 = cmd211.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selSILOjute1 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in (1001,1002,1003,1004,1005) and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Jute New'";
                                                SqlCommand cmd111 = new SqlCommand(selSILOjute1, con);
                                                double sum11 = Convert.ToDouble(cmd111.ExecuteScalar());


                                                string querySILOjute1 = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sum11 + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1006' and Storage = 'SILO' and Packing = 'Jute New'";
                                                SqlCommand cmd21 = new SqlCommand(querySILOjute1, con);
                                                cmd21.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region TotalAvailibilty_Sylo-HDPE
                                            string totCoH1 = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1006' and Storage = 'SILO' and Packing = 'HDPE'";
                                            SqlCommand cmdcoH1 = new SqlCommand(totCoH1, con);
                                            string valueCoH1 = cmdcoH1.ExecuteScalar().ToString();
                                            int valCoH1 = Convert.ToInt16(valueCoH1);

                                            if (valCoH1 == 0)
                                            {
                                                string selcoH1 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in (1001,1002,1003,1004,1005) and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'HDPE' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCoH1 = new SqlCommand(selcoH1, con);
                                                double sumCoH1 = Convert.ToDouble(cmdCoH1.ExecuteScalar());


                                                string querycoH1 = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1006','SILO' ,'HDPE' ,'" + sumCoH1 + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCoH21 = new SqlCommand(querycoH1, con);
                                                int CH = cmdCoH21.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selcoH1 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in (1001,1002,1003,1004,1005) and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'HDPE' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCoH1 = new SqlCommand(selcoH1, con);
                                                double sumcoH1 = Convert.ToDouble(cmdCoH1.ExecuteScalar());


                                                string querycoH1 = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumcoH1 + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1006' and Storage = 'SILO' and Packing = 'HDPE'";
                                                SqlCommand cmdCoH21 = new SqlCommand(querycoH1, con);
                                                cmdCoH21.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region TotalAvailibilty_SYLO-Jute Once Used
                                            string totCOj1 = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1006' and Storage = 'SILO' and Packing = 'Jute Once Used'";
                                            SqlCommand cmdcapOj1 = new SqlCommand(totCOj1, con);
                                            string valueCOj1 = cmdcapOj1.ExecuteScalar().ToString();
                                            int valCj1 = Convert.ToInt16(valueCOj1);

                                            if (valCj1 == 0)
                                            {
                                                string selcjO1 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in (1001,1002,1003,1004,1005) and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Jute Once Used' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCjO1 = new SqlCommand(selcjO1, con);
                                                double sumCOj1 = Convert.ToDouble(cmdCjO1.ExecuteScalar());


                                                string querycjO1 = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1006','SILO' ,'Jute Once Used' ,'" + sumCOj1 + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCOj21 = new SqlCommand(querycjO1, con);
                                                int CO1 = cmdCOj21.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selcjO1 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in (1001,1002,1003,1004,1005) and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Jute Once Used' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCj1O = new SqlCommand(selcjO1, con);
                                                double sumjO1 = Convert.ToDouble(cmdCj1O.ExecuteScalar());


                                                string querycjO1 = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumjO1 + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1006' and Storage = 'SILO' and Packing = 'Jute Once Used'";
                                                SqlCommand cmdCjO21 = new SqlCommand(querycjO1, con);
                                                cmdCjO21.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region TotalAvailibilty_SYLO-Loose
                                            string totCOj11 = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1006' and Storage = 'SILO' and Packing = 'Loose'";
                                            SqlCommand cmdcapOj11 = new SqlCommand(totCOj11, con);
                                            string valueCOj11 = cmdcapOj11.ExecuteScalar().ToString();
                                            int valCj11 = Convert.ToInt16(valueCOj11);

                                            if (valCj11 == 0)
                                            {
                                                string selcjO11 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in (1001,1002,1003,1004,1005) and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Loose' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCjO11 = new SqlCommand(selcjO11, con);
                                                double sumCOj11 = Convert.ToDouble(cmdCjO11.ExecuteScalar());


                                                string querycjO11 = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1006','SILO' ,'Loose' ,'" + sumCOj11 + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCOj211 = new SqlCommand(querycjO11, con);
                                                int CO11 = cmdCOj211.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selcjO11 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in (1001,1002,1003,1004,1005) and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Loose' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCj1O1 = new SqlCommand(selcjO11, con);
                                                double sumjO11 = Convert.ToDouble(cmdCj1O1.ExecuteScalar());


                                                string querycjO11 = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumjO11 + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1006' and Storage = 'SILO' and Packing = 'Loose'";
                                                SqlCommand cmdCjO211 = new SqlCommand(querycjO11, con);
                                                cmdCjO211.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # endregion

                                            # region Total Sales

                                            //CAP

                                            # region Totalsale_Cap-Jutenew
                                            string totavail17 = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1019' and Storage = 'Cap' and Packing = 'Jute New'";
                                            SqlCommand cmdcapjute17 = new SqlCommand(totavail17, con);
                                            string valueavail17 = cmdcapjute17.ExecuteScalar().ToString();
                                            int val17 = Convert.ToInt16(valueavail17);

                                            if (val17 == 0)
                                            {
                                                string selcapjute1 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1007','1008','1009','1010','1011','1012','1013','1014','1015','1016','1017','1018') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'Jute New'";
                                                SqlCommand cmd11 = new SqlCommand(selcapjute1, con);
                                                double sum1 = Convert.ToDouble(cmd11.ExecuteScalar());


                                                string querycaljute1 = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1019','Cap' ,'Jute New' ,'" + sum1 + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmd21 = new SqlCommand(querycaljute1, con);
                                                int xyz1 = cmd21.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selcapjute1 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1007','1008','1009','1010','1011','1012','1013','1014','1015','1016','1017','1018') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'Jute New'";
                                                SqlCommand cmd11 = new SqlCommand(selcapjute1, con);
                                                double sum1 = Convert.ToDouble(cmd11.ExecuteScalar());


                                                string querycaljute1 = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sum1 + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1019' and Storage = 'Cap' and Packing = 'Jute New'";
                                                SqlCommand cmd21 = new SqlCommand(querycaljute1, con);
                                                cmd21.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region Totalsale_Cap-HDPE
                                            string totCH1 = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1019' and Storage = 'Cap' and Packing = 'HDPE'";
                                            SqlCommand cmdcapH1 = new SqlCommand(totCH1, con);
                                            string valueCH1 = cmdcapH1.ExecuteScalar().ToString();
                                            int valCH1 = Convert.ToInt16(valueCH1);

                                            if (valCH1 == 0)
                                            {
                                                string selcapH1 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1007','1008','1009','1010','1011','1012','1013','1014','1015','1016','1017','1018') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'HDPE' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCH1 = new SqlCommand(selcapH1, con);
                                                double sumCH1 = Convert.ToDouble(cmdCH1.ExecuteScalar());


                                                string querycalH1 = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1019','Cap' ,'HDPE' ,'" + sumCH1 + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCH21 = new SqlCommand(querycalH1, con);
                                                int CH1 = cmdCH21.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selcapH1 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1007','1008','1009','1010','1011','1012','1013','1014','1015','1016','1017','1018') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'HDPE' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCH1 = new SqlCommand(selcapH1, con);
                                                double sumH1 = Convert.ToDouble(cmdCH1.ExecuteScalar());


                                                string querycalH1 = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumH1 + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1019' and Storage = 'Cap' and Packing = 'HDPE'";
                                                SqlCommand cmdCH21 = new SqlCommand(querycalH1, con);
                                                cmdCH21.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region TotalSale_Cap-Jute Once Used
                                            string totCO1 = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1019' and Storage = 'Cap' and Packing = 'Jute Once Used'";
                                            SqlCommand cmdcapO1 = new SqlCommand(totCO1, con);
                                            string valueCO1 = cmdcapO1.ExecuteScalar().ToString();
                                            int valCO1 = Convert.ToInt16(valueCO1);

                                            if (valCO1 == 0)
                                            {
                                                string selcapO1 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1007','1008','1009','1010','1011','1012','1013','1014','1015','1016','1017','1018') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'Jute Once Used' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCO1 = new SqlCommand(selcapO1, con);
                                                double sumCO1 = Convert.ToDouble(cmdCO1.ExecuteScalar());


                                                string querycalO1 = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1019','Cap' ,'Jute Once Used' ,'" + sumCO1 + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCO21 = new SqlCommand(querycalO1, con);
                                                int CO1 = cmdCO21.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selcapO1 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1007','1008','1009','1010','1011','1012','1013','1014','1015','1016','1017','1018') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'Jute Once Used' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCO1 = new SqlCommand(selcapO1, con);
                                                double sumO1 = Convert.ToDouble(cmdCO1.ExecuteScalar());


                                                string querycalO1 = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumO1 + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1019' and Storage = 'Cap' and Packing = 'Jute Once Used'";
                                                SqlCommand cmdCO21 = new SqlCommand(querycalO1, con);
                                                cmdCO21.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            // Covered Start

                                            # region TotalSale_Covered-Jutenew
                                            string totavail112 = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1019' and Storage = 'Covered' and Packing = 'Jute New'";
                                            SqlCommand cmdCoveredjute12 = new SqlCommand(totavail112, con);
                                            string valueavail112 = cmdCoveredjute12.ExecuteScalar().ToString();
                                            int val112 = Convert.ToInt16(valueavail112);

                                            if (val112 == 0)
                                            {
                                                string selCoveredjute1 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1007','1008','1009','1010','1011','1012','1013','1014','1015','1016','1017','1018') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'Jute New'";
                                                SqlCommand cmd11 = new SqlCommand(selCoveredjute1, con);
                                                double sum11 = Convert.ToDouble(cmd11.ExecuteScalar());


                                                string queryCoveredjute1 = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1019','Covered' ,'Jute New' ,'" + sum11 + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmd211 = new SqlCommand(queryCoveredjute1, con);
                                                int xyz11 = cmd211.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selCoveredjute1 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1007','1008','1009','1010','1011','1012','1013','1014','1015','1016','1017','1018') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'Jute New'";
                                                SqlCommand cmd111 = new SqlCommand(selCoveredjute1, con);
                                                double sum11 = Convert.ToDouble(cmd111.ExecuteScalar());


                                                string queryCoveredjute1 = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sum11 + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1019' and Storage = 'Covered' and Packing = 'Jute New'";
                                                SqlCommand cmd21 = new SqlCommand(queryCoveredjute1, con);
                                                cmd21.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region Totalsale_Covered-HDPE
                                            string totCoH11 = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1019' and Storage = 'Covered' and Packing = 'HDPE'";
                                            SqlCommand cmdcoH11 = new SqlCommand(totCoH11, con);
                                            string valueCoH11 = cmdcoH11.ExecuteScalar().ToString();
                                            int valCoH11 = Convert.ToInt16(valueCoH11);

                                            if (valCoH11 == 0)
                                            {
                                                string selcoH1 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1007','1008','1009','1010','1011','1012','1013','1014','1015','1016','1017','1018') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'HDPE' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCoH1 = new SqlCommand(selcoH1, con);
                                                double sumCoH1 = Convert.ToDouble(cmdCoH1.ExecuteScalar());


                                                string querycoH1 = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1019','Covered' ,'HDPE' ,'" + sumCoH1 + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCoH21 = new SqlCommand(querycoH1, con);
                                                int CH1 = cmdCoH21.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selcoH1 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1007','1008','1009','1010','1011','1012','1013','1014','1015','1016','1017','1018') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'HDPE' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCoH1 = new SqlCommand(selcoH1, con);
                                                double sumcoH1 = Convert.ToDouble(cmdCoH1.ExecuteScalar());


                                                string querycoH1 = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumcoH1 + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1019' and Storage = 'Covered' and Packing = 'HDPE'";
                                                SqlCommand cmdCoH21 = new SqlCommand(querycoH1, con);
                                                cmdCoH21.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region TotalSale_Covered-Jute Once Used
                                            string totCOj3 = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1019' and Storage = 'Covered' and Packing = 'Jute Once Used'";
                                            SqlCommand cmdcapOj3 = new SqlCommand(totCOj3, con);
                                            string valueCOj3 = cmdcapOj3.ExecuteScalar().ToString();
                                            int valCj3 = Convert.ToInt16(valueCOj3);

                                            if (valCj3 == 0)
                                            {
                                                string selcjO = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1007','1008','1009','1010','1011','1012','1013','1014','1015','1016','1017','1018') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'Jute Once Used' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCjO = new SqlCommand(selcjO, con);
                                                double sumCOj = Convert.ToDouble(cmdCjO.ExecuteScalar());


                                                string querycjO = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1019','Covered' ,'Jute Once Used' ,'" + sumCOj + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCOj2 = new SqlCommand(querycjO, con);
                                                int CO = cmdCOj2.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selcjO = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1007','1008','1009','1010','1011','1012','1013','1014','1015','1016','1017','1018') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'Jute Once Used' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCjO = new SqlCommand(selcjO, con);
                                                double sumjO = Convert.ToDouble(cmdCjO.ExecuteScalar());


                                                string querycjO = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumjO + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1019' and Storage = 'Covered' and Packing = 'Jute Once Used'";
                                                SqlCommand cmdCjO2 = new SqlCommand(querycjO, con);
                                                cmdCjO2.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            // SYLO Start

                                            # region TotalSale_Sylo-Jutenew
                                            string totavail114 = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1019' and Storage = 'SILO' and Packing = 'Jute New'";
                                            SqlCommand cmdSylojute14 = new SqlCommand(totavail114, con);
                                            string valueavail114 = cmdSylojute14.ExecuteScalar().ToString();
                                            int val114 = Convert.ToInt16(valueavail114);

                                            if (val114 == 0)
                                            {
                                                string selSylojute14 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1007','1008','1009','1010','1011','1012','1013','1014','1015','1016','1017','1018') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Jute New'";
                                                SqlCommand cmd114 = new SqlCommand(selSylojute14, con);
                                                double sum114 = Convert.ToDouble(cmd114.ExecuteScalar());


                                                string querySylojute14 = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1019','SILO' ,'Jute New' ,'" + sum114 + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmd2114 = new SqlCommand(querySylojute14, con);
                                                int xyz114 = cmd2114.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selSILOjute14 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1007','1008','1009','1010','1011','1012','1013','1014','1015','1016','1017','1018') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Jute New'";
                                                SqlCommand cmd1114 = new SqlCommand(selSILOjute14, con);
                                                double sum114 = Convert.ToDouble(cmd1114.ExecuteScalar());


                                                string querySILOjute14 = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sum114 + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1019' and Storage = 'SILO' and Packing = 'Jute New'";
                                                SqlCommand cmd214 = new SqlCommand(querySILOjute14, con);
                                                cmd214.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region TotalSale_Sylo-HDPE
                                            string totCoH15 = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1019' and Storage = 'SILO' and Packing = 'HDPE'";
                                            SqlCommand cmdcoH15 = new SqlCommand(totCoH15, con);
                                            string valueCoH15 = cmdcoH15.ExecuteScalar().ToString();
                                            int valCoH15 = Convert.ToInt16(valueCoH15);

                                            if (valCoH15 == 0)
                                            {
                                                string selcoH15 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1007','1008','1009','1010','1011','1012','1013','1014','1015','1016','1017','1018') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'HDPE' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCoH15 = new SqlCommand(selcoH15, con);
                                                double sumCoH15 = Convert.ToDouble(cmdCoH15.ExecuteScalar());


                                                string querycoH1 = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1019','SILO' ,'HDPE' ,'" + sumCoH15 + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCoH21 = new SqlCommand(querycoH1, con);
                                                int CH5 = cmdCoH21.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selcoH15 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1007','1008','1009','1010','1011','1012','1013','1014','1015','1016','1017','1018') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'HDPE' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCoH15 = new SqlCommand(selcoH15, con);
                                                double sumcoH15 = Convert.ToDouble(cmdCoH15.ExecuteScalar());


                                                string querycoH15 = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumcoH15 + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1019' and Storage = 'SILO' and Packing = 'HDPE'";
                                                SqlCommand cmdCoH215 = new SqlCommand(querycoH15, con);
                                                cmdCoH215.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region TotalSale_SYLO-Jute Once Used
                                            string totCOj16 = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1019' and Storage = 'SILO' and Packing = 'Jute Once Used'";
                                            SqlCommand cmdcapOj16 = new SqlCommand(totCOj16, con);
                                            string valueCOj16 = cmdcapOj16.ExecuteScalar().ToString();
                                            int valCj16 = Convert.ToInt16(valueCOj16);

                                            if (valCj16 == 0)
                                            {
                                                string selcjO16 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1007','1008','1009','1010','1011','1012','1013','1014','1015','1016','1017','1018') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Jute Once Used' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCjO16 = new SqlCommand(selcjO16, con);
                                                double sumCOj16 = Convert.ToDouble(cmdCjO16.ExecuteScalar());


                                                string querycjO16 = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1019','SILO' ,'Jute Once Used' ,'" + sumCOj16 + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCOj216 = new SqlCommand(querycjO16, con);
                                                int CO16 = cmdCOj216.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selcjO16 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1007','1008','1009','1010','1011','1012','1013','1014','1015','1016','1017','1018') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Jute Once Used' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCj1O6 = new SqlCommand(selcjO16, con);
                                                double sumjO16 = Convert.ToDouble(cmdCj1O6.ExecuteScalar());


                                                string querycjO16 = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumjO16 + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1019' and Storage = 'SILO' and Packing = 'Jute Once Used'";
                                                SqlCommand cmdCjO216 = new SqlCommand(querycjO16, con);
                                                cmdCjO216.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region TotalSale_SYLO-Loose
                                            string totCOj116 = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1019' and Storage = 'SILO' and Packing = 'Loose'";
                                            SqlCommand cmdcapOj116 = new SqlCommand(totCOj116, con);
                                            string valueCOj116 = cmdcapOj116.ExecuteScalar().ToString();
                                            int valCj116 = Convert.ToInt16(valueCOj116);

                                            if (valCj116 == 0)
                                            {
                                                string selcjO116 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1007','1008','1009','1010','1011','1012','1013','1014','1015','1016','1017','1018') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Loose' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCjO116 = new SqlCommand(selcjO116, con);
                                                double sumCOj116 = Convert.ToDouble(cmdCjO116.ExecuteScalar());


                                                string querycjO116 = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1019','SILO' ,'Loose' ,'" + sumCOj116 + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCOj2116 = new SqlCommand(querycjO116, con);
                                                int CO116 = cmdCOj2116.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selcjO116 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1007','1008','1009','1010','1011','1012','1013','1014','1015','1016','1017','1018') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Loose' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCj1O16 = new SqlCommand(selcjO116, con);
                                                double sumjO116 = Convert.ToDouble(cmdCj1O16.ExecuteScalar());


                                                string querycjO116 = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumjO116 + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1019' and Storage = 'SILO' and Packing = 'Loose'";
                                                SqlCommand cmdCjO2116 = new SqlCommand(querycjO116, con);
                                                cmdCjO2116.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # endregion

                                            # region Total Issue

                                            //CAP

                                            # region TotalIssue_Cap-Jutenew
                                            string totiss = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1023' and Storage = 'Cap' and Packing = 'Jute New'";
                                            SqlCommand cmdcapjute_iss = new SqlCommand(totiss, con);
                                            string valueavaiiss = cmdcapjute_iss.ExecuteScalar().ToString();
                                            int valIss = Convert.ToInt16(valueavaiiss);

                                            if (valIss == 0)
                                            {
                                                string seliss = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1019','1020','1021','1022') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'Jute New'";
                                                SqlCommand cmdIss = new SqlCommand(seliss, con);
                                                double sumIss = Convert.ToDouble(cmdIss.ExecuteScalar());


                                                string queryiss = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1023','Cap' ,'Jute New' ,'" + sumIss + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdiss1 = new SqlCommand(queryiss, con);
                                                int iss = cmdiss1.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string seliss = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1019','1020','1021','1022') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'Jute New'";
                                                SqlCommand cmdiss = new SqlCommand(seliss, con);
                                                double sumIss = Convert.ToDouble(cmdiss.ExecuteScalar());


                                                string queryiss = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumIss + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1023' and Storage = 'Cap' and Packing = 'Jute New'";
                                                SqlCommand cmdiss1 = new SqlCommand(queryiss, con);
                                                cmdiss1.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region TotalIssue_Cap-HDPE
                                            string totCHiss = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1023' and Storage = 'Cap' and Packing = 'HDPE'";
                                            SqlCommand cmdcapHiss = new SqlCommand(totCHiss, con);
                                            string valueCHiss = cmdcapHiss.ExecuteScalar().ToString();
                                            int valCHiss = Convert.ToInt16(valueCHiss);

                                            if (valCHiss == 0)
                                            {
                                                string selcapHiss = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1019','1020','1021','1022') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'HDPE' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCHiss = new SqlCommand(selcapHiss, con);
                                                double sumCHiss = Convert.ToDouble(cmdCHiss.ExecuteScalar());


                                                string querycalHiss = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1023','Cap' ,'HDPE' ,'" + sumCHiss + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCHiss1 = new SqlCommand(querycalHiss, con);
                                                int CHiss = cmdCHiss1.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string querycalHiss = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1019','1020','1021','1022') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'HDPE' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCHiss = new SqlCommand(querycalHiss, con);
                                                double sumHiss = Convert.ToDouble(cmdCHiss.ExecuteScalar());


                                                string querycalHiss1 = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumHiss + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1023' and Storage = 'Cap' and Packing = 'HDPE'";
                                                SqlCommand cmdCHiss1 = new SqlCommand(querycalHiss1, con);
                                                cmdCHiss1.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region TotalIssue_Cap-Jute Once Used
                                            string totCOis = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1023' and Storage = 'Cap' and Packing = 'Jute Once Used'";
                                            SqlCommand cmdcapis = new SqlCommand(totCOis, con);
                                            string valueCOis = cmdcapis.ExecuteScalar().ToString();
                                            int valCOis = Convert.ToInt16(valueCOis);

                                            if (valCOis == 0)
                                            {
                                                string selcapiss = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1019','1020','1021','1022') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'Jute Once Used' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCiss = new SqlCommand(selcapiss, con);
                                                double sumCiss = Convert.ToDouble(cmdCiss.ExecuteScalar());


                                                string querycalis = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1023','Cap' ,'Jute Once Used' ,'" + sumCiss + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCOis = new SqlCommand(querycalis, con);
                                                int COis = cmdCOis.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selcapis = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1019','1020','1021','1022') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'Jute Once Used' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCOis = new SqlCommand(selcapis, con);
                                                double sumOis = Convert.ToDouble(cmdCOis.ExecuteScalar());


                                                string querycalOis = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumOis + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1023' and Storage = 'Cap' and Packing = 'Jute Once Used'";
                                                SqlCommand cmdCOis1 = new SqlCommand(querycalOis, con);
                                                cmdCOis1.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            // Covered Start

                                            # region TotalIssue_Covered-Jutenew
                                            string totavail1is = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1023' and Storage = 'Covered' and Packing = 'Jute New'";
                                            SqlCommand cmdCoveredjute1is = new SqlCommand(totavail1is, con);
                                            string valueavail11is = cmdCoveredjute1is.ExecuteScalar().ToString();
                                            int val11is = Convert.ToInt16(valueavail11is);

                                            if (val11is == 0)
                                            {
                                                string selCoveredjuteis = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1019','1020','1021','1022') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'Jute New'";
                                                SqlCommand cmd1is = new SqlCommand(selCoveredjuteis, con);
                                                double sum1is = Convert.ToDouble(cmd1is.ExecuteScalar());


                                                string queryCoveredjuteIS1 = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1023','Covered' ,'Jute New' ,'" + sum1is + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdIss2 = new SqlCommand(queryCoveredjuteIS1, con);
                                                int Iss_cov = cmdIss2.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selCoveredjuteIs = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1019','1020','1021','1022') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'Jute New'";
                                                SqlCommand cmdis2 = new SqlCommand(selCoveredjuteIs, con);
                                                double sumis2 = Convert.ToDouble(cmdis2.ExecuteScalar());


                                                string queryCoveredjuteiss2 = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumis2 + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1023' and Storage = 'Covered' and Packing = 'Jute New'";
                                                SqlCommand cmdiss21 = new SqlCommand(queryCoveredjuteiss2, con);
                                                cmdiss21.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region TotalIssue_Covered-HDPE
                                            string isstot = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1023' and Storage = 'Covered' and Packing = 'HDPE'";
                                            SqlCommand isscmd = new SqlCommand(isstot, con);
                                            string isscmd1 = isscmd.ExecuteScalar().ToString();
                                            int issval = Convert.ToInt16(isscmd1);

                                            if (issval == 0)
                                            {
                                                string isssel = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1019','1020','1021','1022') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'HDPE' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCoHISS1 = new SqlCommand(isssel, con);
                                                double Isssum = Convert.ToDouble(cmdCoHISS1.ExecuteScalar());


                                                string querycoHIss = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1023','Covered' ,'HDPE' ,'" + Isssum + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCoHiss = new SqlCommand(querycoHIss, con);
                                                int CHiss1 = cmdCoHiss.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selcoHiss = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1019','1020','1021','1022') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'HDPE' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCoHiss1 = new SqlCommand(selcoHiss, con);
                                                double sumcoHiss1 = Convert.ToDouble(cmdCoHiss1.ExecuteScalar());


                                                string querycoHiss1 = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumcoHiss1 + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1023' and Storage = 'Covered' and Packing = 'HDPE'";
                                                SqlCommand cmdCoHiss3 = new SqlCommand(querycoHiss1, con);
                                                cmdCoHiss3.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region TotalIssue_Covered-Jute Once Used
                                            string totCOjiss = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1023' and Storage = 'Covered' and Packing = 'Jute Once Used'";
                                            SqlCommand cmdcapOjis = new SqlCommand(totCOjiss, con);
                                            string valueCOjis = cmdcapOjis.ExecuteScalar().ToString();
                                            int valCjis = Convert.ToInt16(valueCOjis);

                                            if (valCjis == 0)
                                            {
                                                string selcjOis = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1019','1020','1021','1022') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'Jute Once Used' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCjis = new SqlCommand(selcjOis, con);
                                                double sumCOjis = Convert.ToDouble(cmdCjis.ExecuteScalar());


                                                string querycjO = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1023','Covered' ,'Jute Once Used' ,'" + sumCOjis + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCOj2 = new SqlCommand(querycjO, con);
                                                int CO = cmdCOj2.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selcjOis = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1019','1020','1021','1022') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'Jute Once Used' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCjOis = new SqlCommand(selcjOis, con);
                                                double sumjOis = Convert.ToDouble(cmdCjOis.ExecuteScalar());


                                                string querycjOis = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumjOis + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1023' and Storage = 'Covered' and Packing = 'Jute Once Used'";
                                                SqlCommand cmdCjO2is = new SqlCommand(querycjOis, con);
                                                cmdCjO2is.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            // SYLO Start

                                            # region TotalIssue_Sylo-Jutenew
                                            string totavail114is = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1023' and Storage = 'SILO' and Packing = 'Jute New'";
                                            SqlCommand cmdSylojute14is = new SqlCommand(totavail114is, con);
                                            string valueavail114is = cmdSylojute14is.ExecuteScalar().ToString();
                                            int val114is = Convert.ToInt16(valueavail114is);

                                            if (val114is == 0)
                                            {
                                                string selSylojute14is = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1019','1020','1021','1022') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Jute New'";
                                                SqlCommand cmd114is = new SqlCommand(selSylojute14is, con);
                                                double sum114is = Convert.ToDouble(cmd114is.ExecuteScalar());


                                                string querySylojute14is = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1023','SILO' ,'Jute New' ,'" + sum114is + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmd2114is = new SqlCommand(querySylojute14is, con);
                                                int xyz114is = cmd2114is.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selSILOjute14is = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1019','1020','1021','1022') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Jute New'";
                                                SqlCommand cmd1114is = new SqlCommand(selSILOjute14is, con);
                                                double sum114is = Convert.ToDouble(cmd1114is.ExecuteScalar());


                                                string querySILOjute14is = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sum114is + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1023' and Storage = 'SILO' and Packing = 'Jute New'";
                                                SqlCommand cmd214is = new SqlCommand(querySILOjute14is, con);
                                                cmd214is.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region TotalIssue_Sylo-HDPE
                                            string TIS = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1023' and Storage = 'SILO' and Packing = 'HDPE'";
                                            SqlCommand cmdIS = new SqlCommand(TIS, con);
                                            string VIS = cmdIS.ExecuteScalar().ToString();
                                            int VaIS = Convert.ToInt16(VIS);

                                            if (VaIS == 0)
                                            {
                                                string SelIS = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1019','1020','1021','1022') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'HDPE' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmIS = new SqlCommand(SelIS, con);
                                                double sumIS = Convert.ToDouble(cmIS.ExecuteScalar());


                                                string QIS = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1023','SILO' ,'HDPE' ,'" + sumIS + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand CIS = new SqlCommand(QIS, con);
                                                int C5 = CIS.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string SeIS = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1019','1020','1021','1022') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'HDPE' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdIS1 = new SqlCommand(SeIS, con);
                                                double sumIS = Convert.ToDouble(cmdIS1.ExecuteScalar());


                                                string QIS = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumIS + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1023' and Storage = 'SILO' and Packing = 'HDPE'";
                                                SqlCommand CIS = new SqlCommand(QIS, con);
                                                CIS.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region TotalIssue_SYLO-Jute Once Used
                                            string totCOjIS = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1023' and Storage = 'SILO' and Packing = 'Jute Once Used'";
                                            SqlCommand cmdcapOjIS = new SqlCommand(totCOjIS, con);
                                            string valueCOjIS = cmdcapOjIS.ExecuteScalar().ToString();
                                            int valCjIS = Convert.ToInt16(valueCOjIS);

                                            if (valCjIS == 0)
                                            {
                                                string selcjOIS = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1019','1020','1021','1022') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Jute Once Used' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCjOIS = new SqlCommand(selcjOIS, con);
                                                double sumCOjIS = Convert.ToDouble(cmdCjOIS.ExecuteScalar());


                                                string querycjOIS = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1023','SILO' ,'Jute Once Used' ,'" + sumCOjIS + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCOj2IS = new SqlCommand(querycjOIS, con);
                                                int COIS = cmdCOj2IS.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selcjOIS = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1019','1020','1021','1022') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Jute Once Used' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCjIS = new SqlCommand(selcjOIS, con);
                                                double sumjOIS = Convert.ToDouble(cmdCjIS.ExecuteScalar());


                                                string querycjOIS = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumjOIS + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1023' and Storage = 'SILO' and Packing = 'Jute Once Used'";
                                                SqlCommand cmdCjO2IS = new SqlCommand(querycjOIS, con);
                                                cmdCjO2IS.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region TotalIssue_SYLO-Loose
                                            string TISSLO = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1023' and Storage = 'SILO' and Packing = 'Loose'";
                                            SqlCommand cmdTISSLO = new SqlCommand(TISSLO, con);
                                            string VTISSLO = cmdTISSLO.ExecuteScalar().ToString();
                                            int VaIS1 = Convert.ToInt16(VTISSLO);

                                            if (VaIS1 == 0)
                                            {
                                                string selcjOIS = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1019','1020','1021','1022') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Loose' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCjIS = new SqlCommand(selcjOIS, con);
                                                double sumCOjIS = Convert.ToDouble(cmdCjIS.ExecuteScalar());


                                                string querycjIS = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1023','SILO' ,'Loose' ,'" + sumCOjIS + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCOjIS = new SqlCommand(querycjIS, con);
                                                int CO1IS = cmdCOjIS.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selcjIS = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1007','1008','1009','1010','1011','1012','1013','1014','1015','1016','1017','1018') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Loose' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCjIS = new SqlCommand(selcjIS, con);
                                                double sumjIS = Convert.ToDouble(cmdCjIS.ExecuteScalar());


                                                string querycjIS = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumjIS + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1023' and Storage = 'SILO' and Packing = 'Loose'";
                                                SqlCommand cmdCjIS1 = new SqlCommand(querycjIS, con);
                                                cmdCjIS1.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # endregion

                                            # region Total Gain

                                            //CAP

                                            # region TotalGain_Cap-Jutenew
                                            string totG = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1026' and Storage = 'Cap' and Packing = 'Jute New'";
                                            SqlCommand cmdcapjute_G = new SqlCommand(totG, con);
                                            string valueavaiG = cmdcapjute_G.ExecuteScalar().ToString();
                                            int valG = Convert.ToInt16(valueavaiG);

                                            if (valG == 0)
                                            {
                                                string selG = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1024','1025') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'Jute New'";
                                                SqlCommand cmdG = new SqlCommand(selG, con);
                                                double sumG = Convert.ToDouble(cmdG.ExecuteScalar());


                                                string queryG = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1026','Cap' ,'Jute New' ,'" + sumG + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdG1 = new SqlCommand(queryG, con);
                                                int G = cmdG1.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selG = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1024','1025') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'Jute New'";
                                                SqlCommand cmdG = new SqlCommand(selG, con);
                                                double sumG = Convert.ToDouble(cmdG.ExecuteScalar());


                                                string queryG = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumG + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1026' and Storage = 'Cap' and Packing = 'Jute New'";
                                                SqlCommand cmdG1 = new SqlCommand(queryG, con);
                                                cmdG1.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region TotalGain_Cap-HDPE
                                            string totCHG = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1026' and Storage = 'Cap' and Packing = 'HDPE'";
                                            SqlCommand cmdcapHG = new SqlCommand(totCHG, con);
                                            string valueCHG = cmdcapHG.ExecuteScalar().ToString();
                                            int valCHG = Convert.ToInt16(valueCHG);

                                            if (valCHG == 0)
                                            {
                                                string selcapHG = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1024','1025') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'HDPE' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCHG = new SqlCommand(selcapHG, con);
                                                double sumCHG = Convert.ToDouble(cmdCHG.ExecuteScalar());


                                                string querycalHG = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1026','Cap' ,'HDPE' ,'" + sumCHG + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCHG1 = new SqlCommand(querycalHG, con);
                                                int CHG = cmdCHG1.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string querycalHG = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1024','1025') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'HDPE' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCHG = new SqlCommand(querycalHG, con);
                                                double sumHG = Convert.ToDouble(cmdCHG.ExecuteScalar());


                                                string querycalHG1 = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumHG + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1026' and Storage = 'Cap' and Packing = 'HDPE'";
                                                SqlCommand cmdCHG1 = new SqlCommand(querycalHG1, con);
                                                cmdCHG1.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region TotalGain_Cap-Jute Once Used
                                            string totCOG = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1026' and Storage = 'Cap' and Packing = 'Jute Once Used'";
                                            SqlCommand cmdcapG = new SqlCommand(totCOG, con);
                                            string valueCOG = cmdcapG.ExecuteScalar().ToString();
                                            int valCOG = Convert.ToInt16(valueCOG);

                                            if (valCOG == 0)
                                            {
                                                string selcapG = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1024','1025') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'Jute Once Used' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCG = new SqlCommand(selcapG, con);
                                                double sumCG = Convert.ToDouble(cmdCG.ExecuteScalar());


                                                string querycalG = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1026','Cap' ,'Jute Once Used' ,'" + sumCG + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCOG = new SqlCommand(querycalG, con);
                                                int COG = cmdCOG.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selcapG = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1024','1025') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'Jute Once Used' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCOG = new SqlCommand(selcapG, con);
                                                double sumOG = Convert.ToDouble(cmdCOG.ExecuteScalar());


                                                string querycalOG = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumOG + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1026' and Storage = 'Cap' and Packing = 'Jute Once Used'";
                                                SqlCommand cmdCOG1 = new SqlCommand(querycalOG, con);
                                                cmdCOG1.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            // Covered Start

                                            # region TotalGain_Covered-Jutenew
                                            string totavail1G = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1026' and Storage = 'Covered' and Packing = 'Jute New'";
                                            SqlCommand cmdCoveredjute1G = new SqlCommand(totavail1G, con);
                                            string valueavail11G = cmdCoveredjute1G.ExecuteScalar().ToString();
                                            int val11G = Convert.ToInt16(valueavail11G);

                                            if (val11G == 0)
                                            {
                                                string selCoveredjuteG = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1024','1025') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'Jute New'";
                                                SqlCommand cmd1G = new SqlCommand(selCoveredjuteG, con);
                                                double sum1G = Convert.ToDouble(cmd1G.ExecuteScalar());


                                                string queryCoveredjuteG1 = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1026','Covered' ,'Jute New' ,'" + sum1G + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdG2 = new SqlCommand(queryCoveredjuteG1, con);
                                                int IG_cov = cmdG2.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selCoveredjuteG = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1024','1025') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'Jute New'";
                                                SqlCommand cmdG2 = new SqlCommand(selCoveredjuteG, con);
                                                double sumG2 = Convert.ToDouble(cmdG2.ExecuteScalar());


                                                string queryCoveredjuteG2 = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumG2 + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1026' and Storage = 'Covered' and Packing = 'Jute New'";
                                                SqlCommand cmdG21 = new SqlCommand(queryCoveredjuteG2, con);
                                                cmdG21.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region TotalGain_Covered-HDPE
                                            string Gtot = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1026' and Storage = 'Covered' and Packing = 'HDPE'";
                                            SqlCommand Gcmd = new SqlCommand(Gtot, con);
                                            string Gcmd1 = Gcmd.ExecuteScalar().ToString();
                                            int Gval = Convert.ToInt16(Gcmd1);

                                            if (Gval == 0)
                                            {
                                                string Gsel = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1024','1025') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'HDPE' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCoHG1 = new SqlCommand(Gsel, con);
                                                double Gsum = Convert.ToDouble(cmdCoHG1.ExecuteScalar());


                                                string querycoHG = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1026','Covered' ,'HDPE' ,'" + Gsum + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCoHG = new SqlCommand(querycoHG, con);
                                                int CHG1 = cmdCoHG.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selcoHG = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1024','1025') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'HDPE' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCoHG1 = new SqlCommand(selcoHG, con);
                                                double sumcoHG1 = Convert.ToDouble(cmdCoHG1.ExecuteScalar());


                                                string querycoHG1 = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumcoHG1 + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1026' and Storage = 'Covered' and Packing = 'HDPE'";
                                                SqlCommand cmdCoHG3 = new SqlCommand(querycoHG1, con);
                                                cmdCoHG3.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region TotalGain_Covered-Jute Once Used
                                            string totCOjG = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1026' and Storage = 'Covered' and Packing = 'Jute Once Used'";
                                            SqlCommand cmdcapOjG = new SqlCommand(totCOjG, con);
                                            string valueCOjG = cmdcapOjG.ExecuteScalar().ToString();
                                            int valCjG = Convert.ToInt16(valueCOjG);

                                            if (valCjG == 0)
                                            {
                                                string selcjOG = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1024','1025') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'Jute Once Used' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCjG = new SqlCommand(selcjOG, con);
                                                double sumCOjG = Convert.ToDouble(cmdCjG.ExecuteScalar());


                                                string querycjG = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1026','Covered' ,'Jute Once Used' ,'" + sumCOjG + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCOjG2 = new SqlCommand(querycjG, con);
                                                int COG = cmdCOjG2.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selcjOG = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1024','1025') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'Jute Once Used' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCjOG = new SqlCommand(selcjOG, con);
                                                double sumjOG = Convert.ToDouble(cmdCjOG.ExecuteScalar());


                                                string querycjOG = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumjOG + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1026' and Storage = 'Covered' and Packing = 'Jute Once Used'";
                                                SqlCommand cmdCjO2G = new SqlCommand(querycjOG, con);
                                                cmdCjO2G.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            // SYLO Start

                                            # region TotalGain_Sylo-Jutenew
                                            string totavail114G = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1026' and Storage = 'SILO' and Packing = 'Jute New'";
                                            SqlCommand cmdSylojute14G = new SqlCommand(totavail114G, con);
                                            string valueavail114G = cmdSylojute14G.ExecuteScalar().ToString();
                                            int val114G = Convert.ToInt16(valueavail114G);

                                            if (val114G == 0)
                                            {
                                                string selSylojute14G = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1024','1025') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Jute New'";
                                                SqlCommand cmd114G = new SqlCommand(selSylojute14G, con);
                                                double sum114G = Convert.ToDouble(cmd114G.ExecuteScalar());


                                                string querySylojute14G = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1026','SILO' ,'Jute New' ,'" + sum114G + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmd2114G = new SqlCommand(querySylojute14G, con);
                                                int xyz114G = cmd2114G.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selSILOjute14G = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1024','1025') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Jute New'";
                                                SqlCommand cmd1114G = new SqlCommand(selSILOjute14G, con);
                                                double sum114G = Convert.ToDouble(cmd1114G.ExecuteScalar());


                                                string querySILOjute14G = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sum114G + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1026' and Storage = 'SILO' and Packing = 'Jute New'";
                                                SqlCommand cmd214G = new SqlCommand(querySILOjute14G, con);
                                                cmd214G.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region TotalGain_Sylo-HDPE
                                            string TG = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1026' and Storage = 'SILO' and Packing = 'HDPE'";
                                            SqlCommand cmdG11 = new SqlCommand(TG, con);
                                            string VG = cmdG11.ExecuteScalar().ToString();
                                            int VaG = Convert.ToInt16(VG);

                                            if (VaG == 0)
                                            {
                                                string SelG = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1024','1025') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'HDPE' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmG = new SqlCommand(SelG, con);
                                                double sumG = Convert.ToDouble(cmG.ExecuteScalar());


                                                string QG = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1026','SILO' ,'HDPE' ,'" + sumG + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand CG = new SqlCommand(QG, con);
                                                int CG1 = CG.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string SeG = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1024','1025') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'HDPE' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdG12 = new SqlCommand(SeG, con);
                                                double sumG = Convert.ToDouble(cmdG12.ExecuteScalar());


                                                string QG = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumG + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1026' and Storage = 'SILO' and Packing = 'HDPE'";
                                                SqlCommand CG = new SqlCommand(QG, con);
                                                CG.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region TotalGain_SYLO-Jute Once Used
                                            string totCOjG1 = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1026' and Storage = 'SILO' and Packing = 'Jute Once Used'";
                                            SqlCommand cmdcapOjG1 = new SqlCommand(totCOjG1, con);
                                            string valueCOjG1 = cmdcapOjG1.ExecuteScalar().ToString();
                                            int valCjG1 = Convert.ToInt16(valueCOjG1);

                                            if (valCjG1 == 0)
                                            {
                                                string selcjOG = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1024','1025') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Jute Once Used' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCjOG = new SqlCommand(selcjOG, con);
                                                double sumCOjG = Convert.ToDouble(cmdCjOG.ExecuteScalar());


                                                string querycjOG = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1026','SILO' ,'Jute Once Used' ,'" + sumCOjG + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCOj2G = new SqlCommand(querycjOG, con);
                                                int COG = cmdCOj2G.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selcjOG = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1024','1025') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Jute Once Used' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCjG = new SqlCommand(selcjOG, con);
                                                double sumjOG = Convert.ToDouble(cmdCjG.ExecuteScalar());


                                                string querycjOG = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumjOG + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1026' and Storage = 'SILO' and Packing = 'Jute Once Used'";
                                                SqlCommand cmdCjO2G = new SqlCommand(querycjOG, con);
                                                cmdCjO2G.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region TotalGain_SYLO-Loose
                                            string TGLO = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1026' and Storage = 'SILO' and Packing = 'Loose'";
                                            SqlCommand cmdTGLO = new SqlCommand(TGLO, con);
                                            string VTGLO = cmdTGLO.ExecuteScalar().ToString();
                                            int VaG1 = Convert.ToInt16(VTGLO);

                                            if (VaG1 == 0)
                                            {
                                                string selcjOG = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1024','1025') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Loose' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCjG = new SqlCommand(selcjOG, con);
                                                double sumCOjG = Convert.ToDouble(cmdCjG.ExecuteScalar());


                                                string querycjG = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1026','SILO' ,'Loose' ,'" + sumCOjG + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCOjG = new SqlCommand(querycjG, con);
                                                int CO1G = cmdCOjG.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selcjG = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1024','1025') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Loose' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCjG = new SqlCommand(selcjG, con);
                                                double sumjG = Convert.ToDouble(cmdCjG.ExecuteScalar());


                                                string querycjG = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumjG + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1026' and Storage = 'SILO' and Packing = 'Loose'";
                                                SqlCommand cmdCjG1 = new SqlCommand(querycjG, con);
                                                cmdCjG1.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # endregion

                                            # region Total Shortage

                                            //CAP

                                            # region TotalShortage_Cap-Jutenew
                                            string totS = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1029' and Storage = 'Cap' and Packing = 'Jute New'";
                                            SqlCommand cmdcapjute_S = new SqlCommand(totS, con);
                                            string valueavaiS = cmdcapjute_S.ExecuteScalar().ToString();
                                            int valS = Convert.ToInt16(valueavaiS);

                                            if (valS == 0)
                                            {
                                                string selG = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1027','1028') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'Jute New'";
                                                SqlCommand cmdG = new SqlCommand(selG, con);
                                                double sumG = Convert.ToDouble(cmdG.ExecuteScalar());


                                                string queryS = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1029','Cap' ,'Jute New' ,'" + sumG + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdS1 = new SqlCommand(queryS, con);
                                                int S = cmdS1.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selS = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1027','1028') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'Jute New'";
                                                SqlCommand cmdS = new SqlCommand(selS, con);
                                                double sumS = Convert.ToDouble(cmdS.ExecuteScalar());


                                                string queryS = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumS + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1029' and Storage = 'Cap' and Packing = 'Jute New'";
                                                SqlCommand cmdS1 = new SqlCommand(queryS, con);
                                                cmdS1.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region TotalShortage_Cap-HDPE
                                            string totCHS = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1029' and Storage = 'Cap' and Packing = 'HDPE'";
                                            SqlCommand cmdcapHS = new SqlCommand(totCHS, con);
                                            string valueCHS = cmdcapHS.ExecuteScalar().ToString();
                                            int valCHS = Convert.ToInt16(valueCHS);

                                            if (valCHS == 0)
                                            {
                                                string selcapHS = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1027','1028') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'HDPE' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCHS = new SqlCommand(selcapHS, con);
                                                double sumCHS = Convert.ToDouble(cmdCHS.ExecuteScalar());


                                                string querycalHS = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1029','Cap' ,'HDPE' ,'" + sumCHS + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCHS1 = new SqlCommand(querycalHS, con);
                                                int CHS = cmdCHS1.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string querycalHS = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1027','1028') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'HDPE' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCHS = new SqlCommand(querycalHS, con);
                                                double sumHS = Convert.ToDouble(cmdCHS.ExecuteScalar());


                                                string querycalHS1 = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumHS + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1029' and Storage = 'Cap' and Packing = 'HDPE'";
                                                SqlCommand cmdCHS1 = new SqlCommand(querycalHS1, con);
                                                cmdCHS1.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region TotalShortage_Cap-Jute Once Used
                                            string totCOS = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1029' and Storage = 'Cap' and Packing = 'Jute Once Used'";
                                            SqlCommand cmdcapS = new SqlCommand(totCOS, con);
                                            string valueCOS = cmdcapS.ExecuteScalar().ToString();
                                            int valCOS = Convert.ToInt16(valueCOS);

                                            if (valCOS == 0)
                                            {
                                                string selcapS = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1027','1028') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'Jute Once Used' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCS = new SqlCommand(selcapS, con);
                                                double sumCS = Convert.ToDouble(cmdCS.ExecuteScalar());


                                                string querycalS = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1029','Cap' ,'Jute Once Used' ,'" + sumCS + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCOS = new SqlCommand(querycalS, con);
                                                int COS = cmdCOS.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selcapS = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1027','1028') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'Jute Once Used' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCOS = new SqlCommand(selcapS, con);
                                                double sumOS = Convert.ToDouble(cmdCOS.ExecuteScalar());


                                                string querycalOS = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumOS + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1029' and Storage = 'Cap' and Packing = 'Jute Once Used'";
                                                SqlCommand cmdCOS1 = new SqlCommand(querycalOS, con);
                                                cmdCOS1.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            // Covered Start

                                            # region TotalShortage_Covered-Jutenew
                                            string totavail1S = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1029' and Storage = 'Covered' and Packing = 'Jute New'";
                                            SqlCommand cmdCoveredjute1S = new SqlCommand(totavail1S, con);
                                            string valueavail11S = cmdCoveredjute1S.ExecuteScalar().ToString();
                                            int val11S = Convert.ToInt16(valueavail11S);

                                            if (val11S == 0)
                                            {
                                                string selCoveredjuteS = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1027','1028') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'Jute New'";
                                                SqlCommand cmd1S = new SqlCommand(selCoveredjuteS, con);
                                                double sum1S = Convert.ToDouble(cmd1S.ExecuteScalar());


                                                string queryCoveredjuteS1 = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1029','Covered' ,'Jute New' ,'" + sum1S + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdS2 = new SqlCommand(queryCoveredjuteS1, con);
                                                int IS_cov = cmdS2.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selCoveredjuteS = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1027','1028') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'Jute New'";
                                                SqlCommand cmdS2 = new SqlCommand(selCoveredjuteS, con);
                                                double sumS2 = Convert.ToDouble(cmdS2.ExecuteScalar());


                                                string queryCoveredjuteS2 = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumS2 + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1029' and Storage = 'Covered' and Packing = 'Jute New'";
                                                SqlCommand cmdS21 = new SqlCommand(queryCoveredjuteS2, con);
                                                cmdS21.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region TotalShortage_Covered-HDPE
                                            string Stot = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1029' and Storage = 'Covered' and Packing = 'HDPE'";
                                            SqlCommand Scmd = new SqlCommand(Stot, con);
                                            string Scmd1 = Scmd.ExecuteScalar().ToString();
                                            int Sval = Convert.ToInt16(Scmd1);

                                            if (Sval == 0)
                                            {
                                                string Ssel = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1027','1028') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'HDPE' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCoHS1 = new SqlCommand(Ssel, con);
                                                double Ssum = Convert.ToDouble(cmdCoHS1.ExecuteScalar());


                                                string querycoHS = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1029','Covered' ,'HDPE' ,'" + Ssum + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCoHS = new SqlCommand(querycoHS, con);
                                                int CHS1 = cmdCoHS.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selcoHS = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1027','1028') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'HDPE' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCoHS1 = new SqlCommand(selcoHS, con);
                                                double sumcoHS1 = Convert.ToDouble(cmdCoHS1.ExecuteScalar());


                                                string querycoHS1 = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumcoHS1 + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1029' and Storage = 'Covered' and Packing = 'HDPE'";
                                                SqlCommand cmdCoHS3 = new SqlCommand(querycoHS1, con);
                                                cmdCoHS3.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region TotalShoratge_Covered-Jute Once Used
                                            string totCOjS = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1029' and Storage = 'Covered' and Packing = 'Jute Once Used'";
                                            SqlCommand cmdcapOjS = new SqlCommand(totCOjS, con);
                                            string valueCOjS = cmdcapOjS.ExecuteScalar().ToString();
                                            int valCjS = Convert.ToInt16(valueCOjS);

                                            if (valCjS == 0)
                                            {
                                                string selcjOS = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1027','1028') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'Jute Once Used' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCjS = new SqlCommand(selcjOS, con);
                                                double sumCOjS = Convert.ToDouble(cmdCjS.ExecuteScalar());


                                                string querycjS = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1029','Covered' ,'Jute Once Used' ,'" + sumCOjS + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCOjS2 = new SqlCommand(querycjS, con);
                                                int COS = cmdCOjS2.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selcjOS = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1027','1028') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'Jute Once Used' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCjOS = new SqlCommand(selcjOS, con);
                                                double sumjOS = Convert.ToDouble(cmdCjOS.ExecuteScalar());


                                                string querycjOS = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumjOS + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1029' and Storage = 'Covered' and Packing = 'Jute Once Used'";
                                                SqlCommand cmdCjO2S = new SqlCommand(querycjOS, con);
                                                cmdCjO2S.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            // SYLO Start

                                            # region TotalShortage_Sylo-Jutenew
                                            string totavail114S = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1029' and Storage = 'SILO' and Packing = 'Jute New'";
                                            SqlCommand cmdSylojute14S = new SqlCommand(totavail114S, con);
                                            string valueavail114S = cmdSylojute14S.ExecuteScalar().ToString();
                                            int val114S = Convert.ToInt16(valueavail114S);

                                            if (val114S == 0)
                                            {
                                                string selSylojute14S = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1027','1028') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Jute New'";
                                                SqlCommand cmd114S = new SqlCommand(selSylojute14S, con);
                                                double sum114S = Convert.ToDouble(cmd114S.ExecuteScalar());


                                                string querySylojute14S = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1029','SILO' ,'Jute New' ,'" + sum114S + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmd2114S = new SqlCommand(querySylojute14S, con);
                                                int xyz114S = cmd2114S.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selSILOjute14S = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1027','1028') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Jute New'";
                                                SqlCommand cmd1114S = new SqlCommand(selSILOjute14S, con);
                                                double sum114S = Convert.ToDouble(cmd1114S.ExecuteScalar());


                                                string querySILOjute14S = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sum114S + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1029' and Storage = 'SILO' and Packing = 'Jute New'";
                                                SqlCommand cmd214S = new SqlCommand(querySILOjute14S, con);
                                                cmd214S.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region TotalShortage_Sylo-HDPE
                                            string TS = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1029' and Storage = 'SILO' and Packing = 'HDPE'";
                                            SqlCommand cmdS11 = new SqlCommand(TS, con);
                                            string VS = cmdS11.ExecuteScalar().ToString();
                                            int VaS = Convert.ToInt16(VS);

                                            if (VaS == 0)
                                            {
                                                string SelS = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1027','1028') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'HDPE' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmS = new SqlCommand(SelS, con);
                                                double sumS = Convert.ToDouble(cmS.ExecuteScalar());


                                                string QS = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1029','SILO' ,'HDPE' ,'" + sumS + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand CS = new SqlCommand(QS, con);
                                                int CS1 = CS.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string SeS = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1027','1028') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'HDPE' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdS12 = new SqlCommand(SeS, con);
                                                double sumS = Convert.ToDouble(cmdS12.ExecuteScalar());


                                                string QS = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumS + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1029' and Storage = 'SILO' and Packing = 'HDPE'";
                                                SqlCommand CS = new SqlCommand(QS, con);
                                                CS.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region TotalShortage_SYLO-Jute Once Used
                                            string totCOjS1 = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1029' and Storage = 'SILO' and Packing = 'Jute Once Used'";
                                            SqlCommand cmdcapOjS1 = new SqlCommand(totCOjS1, con);
                                            string valueCOjS1 = cmdcapOjS1.ExecuteScalar().ToString();
                                            int valCjS1 = Convert.ToInt16(valueCOjS1);

                                            if (valCjS1 == 0)
                                            {
                                                string selcjOS = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1027','1028') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Jute Once Used' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCjOS = new SqlCommand(selcjOS, con);
                                                double sumCOjS = Convert.ToDouble(cmdCjOS.ExecuteScalar());


                                                string querycjOS = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1029','SILO' ,'Jute Once Used' ,'" + sumCOjS + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCOj2S = new SqlCommand(querycjOS, con);
                                                int COS = cmdCOj2S.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selcjOS = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1027','1028') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Jute Once Used' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCjS = new SqlCommand(selcjOS, con);
                                                double sumjOS = Convert.ToDouble(cmdCjS.ExecuteScalar());


                                                string querycjOS = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumjOS + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1029' and Storage = 'SILO' and Packing = 'Jute Once Used'";
                                                SqlCommand cmdCjO2S = new SqlCommand(querycjOS, con);
                                                cmdCjO2S.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region TotalShortage_SYLO-Loose
                                            string TSLO = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1029' and Storage = 'SILO' and Packing = 'Loose'";
                                            SqlCommand cmdTSLO = new SqlCommand(TSLO, con);
                                            string VTSLO = cmdTSLO.ExecuteScalar().ToString();
                                            int VaS1 = Convert.ToInt16(VTSLO);

                                            if (VaS1 == 0)
                                            {
                                                string selcjOS = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1027','1028') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Loose' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCjS = new SqlCommand(selcjOS, con);
                                                double sumCOjS = Convert.ToDouble(cmdCjS.ExecuteScalar());


                                                string querycjS = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1029','SILO' ,'Loose' ,'" + sumCOjS + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCOjS = new SqlCommand(querycjS, con);
                                                int CO1S = cmdCOjS.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selcjG = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in ('1027','1028') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Loose' and YEAR = '" + year + "' and MONTH = '" + month + "'";
                                                SqlCommand cmdCjG = new SqlCommand(selcjG, con);
                                                double sumjG = Convert.ToDouble(cmdCjG.ExecuteScalar());


                                                string querycjG = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + sumjG + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1029' and Storage = 'SILO' and Packing = 'Loose'";
                                                SqlCommand cmdCjG1 = new SqlCommand(querycjG, con);
                                                cmdCjG1.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # endregion

                                            # region Closing

                                            //CAP

                                            # region Closing_Cap-Jutenew
                                            string totC = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1030' and Storage = 'Cap' and Packing = 'Jute New'";
                                            SqlCommand cmdcapjute_C = new SqlCommand(totC, con);
                                            string valueavaiC = cmdcapjute_C.ExecuteScalar().ToString();
                                            int valC = Convert.ToInt16(valueavaiC);

                                            if (valC == 0)
                                            {
                                                string selG = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1023','1026') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'Jute New'";
                                                SqlCommand cmdG = new SqlCommand(selG, con);
                                                double sumGain = Convert.ToDouble(cmdG.ExecuteScalar());

                                                string selLoss = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1006','1029') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'Jute New'";
                                                SqlCommand cmdL = new SqlCommand(selLoss, con);
                                                double sumL = Convert.ToDouble(cmdL.ExecuteScalar());

                                                double cal = sumGain - sumL;


                                                string queryS = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1030','Cap' ,'Jute New' ,'" + cal + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdS1 = new SqlCommand(queryS, con);
                                                int S = cmdS1.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selG = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1023','1026') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'Jute New'";
                                                SqlCommand cmdG = new SqlCommand(selG, con);
                                                double sumGain = Convert.ToDouble(cmdG.ExecuteScalar());

                                                string selLoss = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1006','1029') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'Jute New'";
                                                SqlCommand cmdL = new SqlCommand(selLoss, con);
                                                double sumL = Convert.ToDouble(cmdL.ExecuteScalar());

                                                double cal = sumGain - sumL;


                                                string queryS = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + cal + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1030' and Storage = 'Cap' and Packing = 'Jute New'";
                                                SqlCommand cmdS1 = new SqlCommand(queryS, con);
                                                cmdS1.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region Closing_Cap-HDPE
                                            string totCHC = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1030' and Storage = 'Cap' and Packing = 'HDPE'";
                                            SqlCommand cmdcapHC = new SqlCommand(totCHC, con);
                                            string valueCHC = cmdcapHC.ExecuteScalar().ToString();
                                            int valCHC = Convert.ToInt16(valueCHC);

                                            if (valCHC == 0)
                                            {
                                                string selG1 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1023','1026') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'HDPE'";
                                                SqlCommand cmdG1 = new SqlCommand(selG1, con);
                                                double sumGain1 = Convert.ToDouble(cmdG1.ExecuteScalar());

                                                string selLoss1 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1006','1029') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'HDPE'";
                                                SqlCommand cmdL1 = new SqlCommand(selLoss1, con);
                                                double sumL1 = Convert.ToDouble(cmdL1.ExecuteScalar());

                                                double ca1l = sumGain1 - sumL1;



                                                string querycalHC = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1030','Cap' ,'HDPE' ,'" + ca1l + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCHC1 = new SqlCommand(querycalHC, con);
                                                int CHC = cmdCHC1.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selG1 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1023','1026') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'HDPE'";
                                                SqlCommand cmdG1 = new SqlCommand(selG1, con);
                                                double sumGain1 = Convert.ToDouble(cmdG1.ExecuteScalar());

                                                string selLoss1 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1006','1029') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'HDPE'";
                                                SqlCommand cmdL1 = new SqlCommand(selLoss1, con);
                                                double sumL1 = Convert.ToDouble(cmdL1.ExecuteScalar());

                                                double ca1l = sumGain1 - sumL1;


                                                string queryC1 = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + ca1l + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1030' and Storage = 'Cap' and Packing = 'HDPE'";
                                                SqlCommand cmdC1 = new SqlCommand(queryC1, con);
                                                cmdC1.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region Closing-Jute Once Used
                                            string Clo = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1030' and Storage = 'Cap' and Packing = 'Jute Once Used'";
                                            SqlCommand cmdclo = new SqlCommand(Clo, con);
                                            string valueclo = cmdclo.ExecuteScalar().ToString();
                                            int valclo = Convert.ToInt16(valueclo);

                                            if (valclo == 0)
                                            {
                                                string selG2 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1023','1026') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'Jute Once Used'";
                                                SqlCommand cmdG2 = new SqlCommand(selG2, con);
                                                double sumGain2 = Convert.ToDouble(cmdG2.ExecuteScalar());

                                                string selLoss2 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1006','1029') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'Jute Once Used'";
                                                SqlCommand cmdL2 = new SqlCommand(selLoss2, con);
                                                double sumL2 = Convert.ToDouble(cmdL2.ExecuteScalar());

                                                double cal2 = sumGain2 - sumL2;



                                                string queryclo = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1030','Cap' ,'Jute Once Used' ,'" + cal2 + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdClo = new SqlCommand(queryclo, con);
                                                int COl = cmdClo.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selG2 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1023','1026') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'Jute Once Used'";
                                                SqlCommand cmdG2 = new SqlCommand(selG2, con);
                                                double sumGain2 = Convert.ToDouble(cmdG2.ExecuteScalar());

                                                string selLoss2 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1006','1029') and District_id = '" + DistrictId + "' and Storage = 'Cap' and Packing = 'Jute Once Used'";
                                                SqlCommand cmdL2 = new SqlCommand(selLoss2, con);
                                                double sumL2 = Convert.ToDouble(cmdL2.ExecuteScalar());

                                                double cal2 = sumGain2 - sumL2;


                                                string q = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + cal2 + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1030' and Storage = 'Cap' and Packing = 'Jute Once Used'";
                                                SqlCommand c = new SqlCommand(q, con);
                                                c.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            // Covered Start

                                            # region Closing_Covered-Jutenew
                                            string to = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1030' and Storage = 'Covered' and Packing = 'Jute New'";
                                            SqlCommand cm = new SqlCommand(to, con);
                                            string val4 = cm.ExecuteScalar().ToString();
                                            int va = Convert.ToInt16(val4);

                                            if (va == 0)
                                            {
                                                string selG4 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1023','1026') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'Jute New'";
                                                SqlCommand cmdG4 = new SqlCommand(selG4, con);
                                                double sumGain4 = Convert.ToDouble(cmdG4.ExecuteScalar());

                                                string selLoss4 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1006','1029') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'Jute New'";
                                                SqlCommand cmdL4 = new SqlCommand(selLoss4, con);
                                                double sumL4 = Convert.ToDouble(cmdL4.ExecuteScalar());

                                                double cal4 = sumGain4 - sumL4;



                                                string qrcl = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1030','Covered' ,'Jute New' ,'" + cal4 + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdcl = new SqlCommand(qrcl, con);
                                                int IS_cl = cmdcl.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            { //start
                                                string selG4 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1023','1026') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'Jute New'";
                                                SqlCommand cmdG4 = new SqlCommand(selG4, con);
                                                double sumGain4 = Convert.ToDouble(cmdG4.ExecuteScalar());

                                                string selLoss4 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1006','1029') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'Jute New'";
                                                SqlCommand cmdL4 = new SqlCommand(selLoss4, con);
                                                double sumL4 = Convert.ToDouble(cmdL4.ExecuteScalar());

                                                double cal4 = sumGain4 - sumL4;


                                                string queryCocl = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + cal4 + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1030' and Storage = 'Covered' and Packing = 'Jute New'";
                                                SqlCommand cmdScl = new SqlCommand(queryCocl, con);
                                                cmdScl.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region closing_Covered-HDPE
                                            string Stot1 = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1030' and Storage = 'Covered' and Packing = 'HDPE'";
                                            SqlCommand clocmd1 = new SqlCommand(Stot1, con);
                                            string Scmd11 = clocmd1.ExecuteScalar().ToString();
                                            int Sval1 = Convert.ToInt16(Scmd11);

                                            if (Sval1 == 0)
                                            {
                                                string selG5 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1023','1026') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'HDPE'";
                                                SqlCommand cmdG5 = new SqlCommand(selG5, con);
                                                double sumGain5 = Convert.ToDouble(cmdG5.ExecuteScalar());

                                                string selLoss5 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1006','1029') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'HDPE'";
                                                SqlCommand cmdL5 = new SqlCommand(selLoss5, con);
                                                double sumL5 = Convert.ToDouble(cmdL5.ExecuteScalar());

                                                double cal5 = sumGain5 - sumL5;



                                                string qucl = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1030','Covered' ,'HDPE' ,'" + cal5 + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCcl = new SqlCommand(qucl, con);
                                                int CHcl = cmdCcl.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selG5 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1023','1026') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'HDPE'";
                                                SqlCommand cmdG5 = new SqlCommand(selG5, con);
                                                double sumGain5 = Convert.ToDouble(cmdG5.ExecuteScalar());

                                                string selLoss5 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1006','1029') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'HDPE'";
                                                SqlCommand cmdL5 = new SqlCommand(selLoss5, con);
                                                double sumL5 = Convert.ToDouble(cmdL5.ExecuteScalar());

                                                double cal5 = sumGain5 - sumL5;


                                                string quercl = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + cal5 + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1030' and Storage = 'Covered' and Packing = 'HDPE'";
                                                SqlCommand cmdCocl = new SqlCommand(quercl, con);
                                                cmdCocl.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region Closing_Covered-Jute Once Used
                                            string t1 = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1030' and Storage = 'Covered' and Packing = 'Jute Once Used'";
                                            SqlCommand c1 = new SqlCommand(t1, con);
                                            string v1 = c1.ExecuteScalar().ToString();
                                            int v11 = Convert.ToInt16(v1);

                                            if (v11 == 0)
                                            {
                                                string selG6 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1023','1026') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'Jute Once Used'";
                                                SqlCommand cmdG6 = new SqlCommand(selG6, con);
                                                double sumGain6 = Convert.ToDouble(cmdG6.ExecuteScalar());

                                                string selLoss6 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1006','1029') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'Jute Once Used'";
                                                SqlCommand cmdL6 = new SqlCommand(selLoss6, con);
                                                double sumL6 = Convert.ToDouble(cmdL6.ExecuteScalar());

                                                double cal6 = sumGain6 - sumL6;



                                                string que1 = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1030','Covered' ,'Jute Once Used' ,'" + cal6 + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmd11 = new SqlCommand(que1, con);
                                                int COS4 = cmd11.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selG6 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1023','1026') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'Jute Once Used'";
                                                SqlCommand cmdG6 = new SqlCommand(selG6, con);
                                                double sumGain6 = Convert.ToDouble(cmdG6.ExecuteScalar());

                                                string selLoss6 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1006','1029') and District_id = '" + DistrictId + "' and Storage = 'Covered' and Packing = 'Jute Once Used'";
                                                SqlCommand cmdL6 = new SqlCommand(selLoss6, con);
                                                double sumL6 = Convert.ToDouble(cmdL6.ExecuteScalar());

                                                double cal6 = sumGain6 - sumL6;


                                                string quec = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + cal6 + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1030' and Storage = 'Covered' and Packing = 'Jute Once Used'";
                                                SqlCommand cmdCc = new SqlCommand(quec, con);
                                                cmdCc.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            // SYLO Start

                                            # region Closing_Sylo-Jutenew
                                            string totclo = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1030' and Storage = 'SILO' and Packing = 'Jute New'";
                                            SqlCommand cmdSyloclo = new SqlCommand(totclo, con);
                                            string valueavclo = cmdSyloclo.ExecuteScalar().ToString();
                                            int val11clo = Convert.ToInt16(valueavclo);

                                            if (val11clo == 0)
                                            {
                                                string selG7 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1023','1026') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Jute New'";
                                                SqlCommand cmdG7 = new SqlCommand(selG7, con);
                                                double sumGain7 = Convert.ToDouble(cmdG7.ExecuteScalar());

                                                string selLoss7 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1006','1029') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Jute New'";
                                                SqlCommand cmdL7 = new SqlCommand(selLoss7, con);
                                                double sumL7 = Convert.ToDouble(cmdL7.ExecuteScalar());

                                                double cal7 = sumGain7 - sumL7;

                                                string querycl = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1030','SILO' ,'Jute New' ,'" + cal7 + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmd2cl = new SqlCommand(querycl, con);
                                                int xcl = cmd2cl.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selG7 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1023','1026') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Jute New'";
                                                SqlCommand cmdG7 = new SqlCommand(selG7, con);
                                                double sumGain7 = Convert.ToDouble(cmdG7.ExecuteScalar());

                                                string selLoss7 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1006','1029') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Jute New'";
                                                SqlCommand cmdL7 = new SqlCommand(selLoss7, con);
                                                double sumL7 = Convert.ToDouble(cmdL7.ExecuteScalar());

                                                double cal7 = sumGain7 - sumL7;


                                                string querycl1 = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + cal7 + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1030' and Storage = 'SILO' and Packing = 'Jute New'";
                                                SqlCommand cmd2cl1 = new SqlCommand(querycl1, con);
                                                cmd2cl1.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region Closing_Sylo-HDPE
                                            string TScl = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1030' and Storage = 'SILO' and Packing = 'HDPE'";
                                            SqlCommand cmdSclo = new SqlCommand(TScl, con);
                                            string VSclo = cmdSclo.ExecuteScalar().ToString();
                                            int VaSclo = Convert.ToInt16(VSclo);

                                            if (VaSclo == 0)
                                            {
                                                string selG8 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1023','1026') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'HDPE'";
                                                SqlCommand cmdG8 = new SqlCommand(selG8, con);
                                                double sumGain8 = Convert.ToDouble(cmdG8.ExecuteScalar());

                                                string selLoss8 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1006','1029') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'HDPE'";
                                                SqlCommand cmdL8 = new SqlCommand(selLoss8, con);
                                                double sumL8 = Convert.ToDouble(cmdL8.ExecuteScalar());

                                                double cal8 = sumGain8 - sumL8;


                                                string QS = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1030','SILO' ,'HDPE' ,'" + cal8 + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand CS = new SqlCommand(QS, con);
                                                int CS1 = CS.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selG8 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1023','1026') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'HDPE'";
                                                SqlCommand cmdG8 = new SqlCommand(selG8, con);
                                                double sumGain8 = Convert.ToDouble(cmdG8.ExecuteScalar());

                                                string selLoss8 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1006','1029') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'HDPE'";
                                                SqlCommand cmdL8 = new SqlCommand(selLoss8, con);
                                                double sumL8 = Convert.ToDouble(cmdL8.ExecuteScalar());

                                                double cal8 = sumGain8 - sumL8;


                                                string QS1 = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + cal8 + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1030' and Storage = 'SILO' and Packing = 'HDPE'";
                                                SqlCommand CS1 = new SqlCommand(QS1, con);
                                                CS1.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region Closing_SYLO-Jute Once Used
                                            string totclos = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1030' and Storage = 'SILO' and Packing = 'Jute Once Used'";
                                            SqlCommand cmdclos = new SqlCommand(totclos, con);
                                            string valueclos = cmdclos.ExecuteScalar().ToString();
                                            int valclos = Convert.ToInt16(valueclos);

                                            if (valclos == 0)
                                            {
                                                string selG9 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1023','1026') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Jute Once Used'";
                                                SqlCommand cmdG9 = new SqlCommand(selG9, con);
                                                double sumGain9 = Convert.ToDouble(cmdG9.ExecuteScalar());

                                                string selLoss9 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1006','1029') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Jute Once Used'";
                                                SqlCommand cmdL9 = new SqlCommand(selLoss9, con);
                                                double sumL9 = Convert.ToDouble(cmdL9.ExecuteScalar());

                                                double cal9 = sumGain9 - sumL9;


                                                string queryclo = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1030','SILO' ,'Jute Once Used' ,'" + cal9 + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdClo = new SqlCommand(queryclo, con);
                                                int COclo1 = cmdClo.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selG9 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1023','1026') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Jute Once Used'";
                                                SqlCommand cmdG9 = new SqlCommand(selG9, con);
                                                double sumGain9 = Convert.ToDouble(cmdG9.ExecuteScalar());

                                                string selLoss9 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1006','1029') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Jute Once Used'";
                                                SqlCommand cmdL9 = new SqlCommand(selLoss9, con);
                                                double sumL9 = Convert.ToDouble(cmdL9.ExecuteScalar());

                                                double cal9 = sumGain9 - sumL9;


                                                string queryclo = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + cal9 + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1030' and Storage = 'SILO' and Packing = 'Jute Once Used'";
                                                SqlCommand cmdClo = new SqlCommand(queryclo, con);
                                                cmdClo.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # region Closing_SYLO-Loose
                                            string TSLO1 = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where District_id = '" + DistrictId + "' and YEAR = '" + year + "' and MONTH = '" + month + "' and head_Id = '1030' and Storage = 'SILO' and Packing = 'Loose'";
                                            SqlCommand cmdTSLO1 = new SqlCommand(TSLO1, con);
                                            string VTSLO1 = cmdTSLO1.ExecuteScalar().ToString();
                                            int VaS11 = Convert.ToInt16(VTSLO1);

                                            if (VaS11 == 0)
                                            {
                                                string selG10 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1023','1026') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Loose'";
                                                SqlCommand cmdG10 = new SqlCommand(selG10, con);
                                                double sumGain10 = Convert.ToDouble(cmdG10.ExecuteScalar());

                                                string selLoss10 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1006','1029') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Loose'";
                                                SqlCommand cmdL10 = new SqlCommand(selLoss10, con);
                                                double sumL10 = Convert.ToDouble(cmdL10.ExecuteScalar());

                                                double cal10 = sumGain10 - sumL10;


                                                string queryccl = "Insert into FIN_DistrictStockRegister_Wheat (District_id ,Year,Month ,head_Id,Storage ,Packing ,Quantity ,Date,Browser ,IPAddress,SessionId) values ('" + DistrictId + "' ,'" + year + "','" + month + "' ,'1030','SILO' ,'Loose' ,'" + cal10 + "' ,'" + date + "','" + Browser + "' ,'" + IpAddress + "','" + strSession + "')";
                                                SqlCommand cmdCcl = new SqlCommand(queryccl, con);
                                                int COcl4 = cmdCcl.ExecuteNonQuery();

                                                // do insert fro total availablity
                                            }

                                            else
                                            {
                                                string selG10 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1023','1026') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Loose'";
                                                SqlCommand cmdG10 = new SqlCommand(selG10, con);
                                                double sumGain10 = Convert.ToDouble(cmdG10.ExecuteScalar());

                                                string selLoss10 = "select isnull(sum(Quantity),0)TOT from FIN_DistrictStockRegister_Wheat where head_Id in('1006','1029') and District_id = '" + DistrictId + "' and Storage = 'SILO' and Packing = 'Loose'";
                                                SqlCommand cmdL10 = new SqlCommand(selLoss10, con);
                                                double sumL10 = Convert.ToDouble(cmdL10.ExecuteScalar());

                                                double cal10 = sumGain10 - sumL10;


                                                string queryclo1 = "Update FIN_DistrictStockRegister_Wheat set Quantity = '" + cal10 + "' where District_id = '" + DistrictId + "' and Year = '" + year + "'and Month = '" + month + "'  and head_Id = '1030' and Storage = 'SILO' and Packing = 'Loose'";
                                                SqlCommand cmdCclo = new SqlCommand(queryclo1, con);
                                                cmdCclo.ExecuteNonQuery();
                                                // doupdate
                                            }

                                            # endregion

                                            # endregion




                                        }  
                                    }

                                    catch
                                    {
                                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('भरी हुई जानकारी जांच कर पुनः प्रयास करें |');</script>");
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
        string headquery = "SELECT head_id ,head FROM FIN_Wheat_Head where head_id not in (1006,1019,1023,1026,1029,1030) ";
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
        string storagequery = "SELECT StorageId ,Storage FROM FIN_TypeofStorage";
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
        string packingquery = "SELECT PackingId ,Packing FROM FIN_TypeofPacking";
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
        Response.Redirect("~/District/FIN_Print_WheatDCP.aspx");
    }
    //protected void ddlmonth_SelectedIndexChanged(object sender, EventArgs e)
    //{
 
    //    string balance = "SELECT count(*) FROM FIN_DistrictStockRegister_Wheat where head_Id = '1001' and District_id = '" + Session["dist_id"] + "' and YEAR = '" + ddlyear.SelectedItem.Text + "' and MONTH = '" + ddlmonth.SelectedItem.Text + "'";
    //    SqlCommand cmd = new SqlCommand(balance, con);

    //    Int32 count = (Int32)cmd.ExecuteScalar();

    //    if (count == 0 || count == 1)  // Opening Balance Not Found
    //    {
    //        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('पहले  Opening Balance भरें |');</script>");

    //        string headquery = "SELECT head_id ,head FROM FIN_Wheat_Head";
    //        SqlCommand cmd11 = new SqlCommand(headquery, con);
    //        SqlDataAdapter da11 = new SqlDataAdapter(cmd11);
    //        DataSet ds11 = new DataSet();
    //        da11.Fill(ds11);

    //        ddlhead.DataSource = ds11.Tables[0];

    //        ddlhead.DataTextField = "head";
    //        ddlhead.DataValueField = "head_id";
    //        ddlhead.DataBind();
    //        ddlhead.Items.Insert(0, "--चुनें--");

    //        ddlhead.SelectedValue = "1001";
    //        ddlhead.Enabled = false;   

    //    }
    //    else   // Found
    //    {
    //        bindHead();
    //        ddlhead.Enabled = true;
    //    }
    //}

   
}
