using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Configuration;	

/// <summary>
/// Summary description for Update_WHR_CSMSPdy2016
/// </summary>
[WebService(Namespace = "http://microsoft.co.in/", Name = "Update_WHR_CSMSPdy2016", Description = "Insert Paddy WHR Number to CSMS and Uparjan")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

public class Update_WHR_CSMSPdy2016 : System.Web.Services.WebService {

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_PPMS2016"].ToString());
    public SqlConnection con_maze = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_MPMS2016"].ToString());

    private SqlCommand cmd = new SqlCommand();
    private SqlCommand cmd1 = new SqlCommand();

    private SqlDataAdapter dataAdapter;
    private DataSet dataset;
    private SqlTransaction trans = null;
    private SqlCommand commandt = null;
    string Query = "";

    public Update_WHR_CSMSPdy2016 () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(Description = "This Method Is Used For insert WHR in CSMS ")]

    public void Insert_WHR_CSMS(string WHR_Request, string DistrictId, string WHR_Number, String WHR_Date, String Commodity)
    {
        # region Paddy_Common

        if (Commodity == "13")
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (con_paddy.State == ConnectionState.Closed)
            {
                con_paddy.Open();
            }

            string mystring = DistrictId;
            string disttid = mystring.Substring(mystring.Length - 2);

            SqlTransaction trns;
            cmd.Connection = con;
            trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            cmd.Transaction = trns;

            SqlTransaction trns1;
            cmd1.Connection = con_paddy;
            trns1 = con_paddy.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            cmd1.Transaction = trns1;

            try
            {
                string str = "Update Acceptance_Note_Kharif2016 set WhrNumber = '" + WHR_Number + "', WHR_Date = '" + WHR_Date + "' , whrType = 'C' where WHR_Request = '" + WHR_Request + "' and Distt_ID = '" + disttid + "'  and CommodityId = '" + Commodity + "'";
                cmd.CommandText = str;

                string str1 = "Update Acceptance_Note_Detail set WhrNumber = '" + WHR_Number + "' where WHR_Request = '" + WHR_Request + "' and Distt_ID = '" + DistrictId + "' and CommodityId = '2'";
                cmd1.CommandText = str1;

                int x = cmd.ExecuteNonQuery();

                int count = cmd1.ExecuteNonQuery();

                if (count >= 1)
                {
                    trns1.Commit();

                    if (con_paddy.State == ConnectionState.Open)
                    {
                        con_paddy.Close();
                    }

                    trns.Commit();

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();

                    }

                }
            }

            catch (Exception ex)
            {
                trns1.Rollback();

                string msg = "Exception : Update CSMS Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }

            finally
            {
                if (con_paddy.State == ConnectionState.Open)
                {
                    con_paddy.Close();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        # endregion

        # region Paddy_A

        else if (Commodity == "14")
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (con_paddy.State == ConnectionState.Closed)
            {
                con_paddy.Open();
            }

            string mystring = DistrictId;
            string disttid = mystring.Substring(mystring.Length - 2);

            SqlTransaction trns;
            cmd.Connection = con;
            trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            cmd.Transaction = trns;

            SqlTransaction trns1;
            cmd1.Connection = con_paddy;
            trns1 = con_paddy.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            cmd1.Transaction = trns1;

            try
            {
                string str = "Update Acceptance_Note_Kharif2016 set WhrNumber = '" + WHR_Number + "', WHR_Date = '" + WHR_Date + "' , whrType = 'C' where WHR_Request = '" + WHR_Request + "' and Distt_ID = '" + disttid + "'  and CommodityId = '" + Commodity + "'";
                cmd.CommandText = str;

                string str1 = "Update Acceptance_Note_Detail set WhrNumber = '" + WHR_Number + "' where WHR_Request = '" + WHR_Request + "' and Distt_ID = '" + DistrictId + "' and CommodityId = '3'";
                cmd1.CommandText = str1;

                int x = cmd.ExecuteNonQuery();

                int count = cmd1.ExecuteNonQuery();

                if (count >= 1)
                {
                    trns1.Commit();

                    if (con_paddy.State == ConnectionState.Open)
                    {
                        con_paddy.Close();
                    }

                    trns.Commit();

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();

                    }

                }
            }

            catch (Exception ex)
            {
                trns1.Rollback();

                string msg = "Exception : Update CSMS Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }

            finally
            {
                if (con_paddy.State == ConnectionState.Open)
                {
                    con_paddy.Close();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        # endregion

        # region Bajra

        else if (Commodity == "8")
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            if (con_maze.State == ConnectionState.Closed)
            {
                con_maze.Open();
            }

            string mystring = DistrictId;
            string disttid = mystring.Substring(mystring.Length - 2);

            SqlTransaction trns;
            cmd.Connection = con;
            trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            cmd.Transaction = trns;

            SqlTransaction trns1;
            cmd1.Connection = con_maze;
            trns1 = con_maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            cmd1.Transaction = trns1;

            try
            {
                string str = "Update Acceptance_Note_Kharif2016 set WhrNumber = '" + WHR_Number + "', WHR_Date = '" + WHR_Date + "' , whrType = 'C' where WHR_Request = '" + WHR_Request + "' and Distt_ID = '" + disttid + "'  and CommodityId = '" + Commodity + "'";
                cmd.CommandText = str;

                string str1 = "Update Acceptance_Note_Detail set WhrNumber = '" + WHR_Number + "' where WHR_Request = '" + WHR_Request + "' and Distt_ID = '" + DistrictId + "' and CommodityId = '6'";
                cmd1.CommandText = str1;

                int x = cmd.ExecuteNonQuery();

                int count = cmd1.ExecuteNonQuery();

                if (count >= 1)
                {
                    trns1.Commit();

                    if (con_maze.State == ConnectionState.Open)
                    {
                        con_maze.Close();
                    }

                    trns.Commit();

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();

                    }

                }
            }

            catch (Exception ex)
            {
                trns1.Rollback();

                string msg = "Exception : Update CSMS Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }

            finally
            {
                if (con_maze.State == ConnectionState.Open)
                {
                    con_maze.Close();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        # endregion

        # region Jowar

        else if (Commodity == "11")
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (con_maze.State == ConnectionState.Closed)
            {
                con_maze.Open();
            }

            string mystring = DistrictId;

            string disttid = mystring.Substring(mystring.Length - 2);


            SqlTransaction trns;
            cmd.Connection = con;
            trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            cmd.Transaction = trns;

            SqlTransaction trns1;
            cmd1.Connection = con_maze;
            trns1 = con_maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            cmd1.Transaction = trns1;

            try
            {
                string str = "Update Acceptance_Note_Kharif2016 set WhrNumber = '" + WHR_Number + "', WHR_Date = '" + WHR_Date + "' , whrType = 'C' where WHR_Request = '" + WHR_Request + "' and Distt_ID = '" + disttid + "'  and CommodityId = '" + Commodity + "'";
                cmd.CommandText = str;

                string str1 = "Update Acceptance_Note_Detail set WhrNumber = '" + WHR_Number + "' where WHR_Request = '" + WHR_Request + "' and Distt_ID = '" + DistrictId + "' and CommodityId = '4'";
                cmd1.CommandText = str1;

                int x = cmd.ExecuteNonQuery();
                int count = cmd1.ExecuteNonQuery();

                if (count >= 1)
                {
                    trns1.Commit();

                    if (con_maze.State == ConnectionState.Open)
                    {
                        con_maze.Close();
                    }

                    trns.Commit();

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();

                    }

                }
            }

            catch (Exception ex)
            {
                trns1.Rollback();

                string msg = "Exception : Update CSMS Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }

            finally
            {
                if (con_maze.State == ConnectionState.Open)
                {
                    con_maze.Close();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        # endregion

        # region Maize

        else if (Commodity == "12")
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (con_maze.State == ConnectionState.Closed)
            {
                con_maze.Open();
            }

            string mystring = DistrictId;

            string disttid = mystring.Substring(mystring.Length - 2);


            SqlTransaction trns;
            cmd.Connection = con;
            trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            cmd.Transaction = trns;

            SqlTransaction trns1;
            cmd1.Connection = con_maze;
            trns1 = con_maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            cmd1.Transaction = trns1;

            try
            {
                string str = "Update Acceptance_Note_Kharif2016 set WhrNumber = '" + WHR_Number + "', WHR_Date = '" + WHR_Date + "' , whrType = 'C' where WHR_Request = '" + WHR_Request + "' and Distt_ID = '" + disttid + "'  and CommodityId = '" + Commodity + "'";
                cmd.CommandText = str;

                string str1 = "Update Acceptance_Note_Detail set WhrNumber = '" + WHR_Number + "' where WHR_Request = '" + WHR_Request + "' and Distt_ID = '" + DistrictId + "' and CommodityId = '5'";
                cmd1.CommandText = str1;

                int x = cmd.ExecuteNonQuery();
                int count = cmd1.ExecuteNonQuery();

                if (count >= 1)
                {
                    trns1.Commit();

                    if (con_maze.State == ConnectionState.Open)
                    {
                        con_maze.Close();
                    }

                    trns.Commit();

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();

                    }

                }
            }

            catch (Exception ex)
            {
                trns1.Rollback();

                string msg = "Exception : Update CSMS Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }

            finally
            {
                if (con_maze.State == ConnectionState.Open)
                {
                    con_maze.Close();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        # endregion

        # region Jau

        else if (Commodity == "40")
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (con_maze.State == ConnectionState.Closed)
            {
                con_maze.Open();
            }

            string mystring = DistrictId;
            string disttid = mystring.Substring(mystring.Length - 2);

            SqlTransaction trns;
            cmd.Connection = con;
            trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            cmd.Transaction = trns;

            SqlTransaction trns1;
            cmd1.Connection = con_maze;
            trns1 = con_maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            cmd1.Transaction = trns1;

            try
            {
                string str = "Update Acceptance_Note_Kharif2016 set WhrNumber = '" + WHR_Number + "', WHR_Date = '" + WHR_Date + "' , whrType = 'C' where WHR_Request = '" + WHR_Request + "' and Distt_ID = '" + disttid + "'  and CommodityId = '" + Commodity + "'";
                cmd.CommandText = str;

                string str1 = "Update Acceptance_Note_Detail set WhrNumber = '" + WHR_Number + "' where WHR_Request = '" + WHR_Request + "' and Distt_ID = '" + DistrictId + "' and CommodityId = '7'";
                cmd1.CommandText = str1;

                int x = cmd.ExecuteNonQuery();
                int count = cmd1.ExecuteNonQuery();

                if (count >= 1)
                {
                    trns1.Commit();

                    if (con_maze.State == ConnectionState.Open)
                    {
                        con_maze.Close();
                    }

                    trns.Commit();

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();

                    }

                }
            }

            catch (Exception ex)
            {
                trns1.Rollback();

                string msg = "Exception : Update CSMS Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }

            finally
            {
                if (con_maze.State == ConnectionState.Open)
                {
                    con_maze.Close();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        # endregion
    }


    private string getDate_MDY(string inDate)
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
    
}
