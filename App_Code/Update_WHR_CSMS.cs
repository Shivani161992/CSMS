using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Configuration;	

/// <summary>
/// Summary description for Update_WHR_CSMS
/// </summary>
[WebService(Namespace = "http://microsoft.co.in/", Name = "Update_WHR_CSMS", Description = "Insert WHR Number to CSMS and Uparjan")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

public class Update_WHR_CSMS : System.Web.Services.WebService {

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_PPMS2015_16"].ToString());
    public SqlConnection con_Maze = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_MPMS2015_16"].ToString());

    public SqlConnection con_WPMS = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2016"].ToString());

    private SqlCommand cmd = new SqlCommand();

    private SqlCommand cmd1 = new SqlCommand();

    private SqlDataAdapter dataAdapter;
    private DataSet dataset;
    private SqlTransaction trans = null;
    private SqlCommand commandt = null;
    string Query = "";

    public Update_WHR_CSMS () 
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(Description = "This Method Is Used For insert WHR in CSMS ")]

    public void Insert_WHR_CSMS(string WHR_Request, string DistrictId, string WHR_Number, String WHR_Date, String Commodity)
    {
        
        # region Wheat

        
        if (Commodity == "22")
            {

                string mystring = DistrictId;

                string disttid = mystring.Substring(mystring.Length - 2);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                if (con_WPMS.State == ConnectionState.Closed)
                {
                    con_WPMS.Open();
                }

                SqlTransaction trns;
                cmd.Connection = con;
                trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                cmd.Transaction = trns;

                SqlTransaction trns1;
                cmd1.Connection = con_WPMS;
                trns1 = con_WPMS.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                cmd1.Transaction = trns1;

                try
                {
                    
                    string str = "Update Acceptance_Note_Detail2016 set WhrNumber = '" + WHR_Number + "', WHR_Date = '" + WHR_Date + "' , whrType = 'C' where WHR_Request = '" + WHR_Request + "' and Distt_ID = '" + disttid + "'  and CommodityId = '" + Commodity + "'";
                    cmd.CommandText = str;

                    string str1 = "Update Acceptance_Note_Detail set WhrNumber = '" + WHR_Number + "' where WHR_Request = '" + WHR_Request + "' and Distt_ID = '" + DistrictId + "' and CommodityId = '1'";
                    cmd1.CommandText = str1;

                    int x = cmd.ExecuteNonQuery();

                    int count = cmd1.ExecuteNonQuery();

                    if (count >= 1)
                    {
                        trns1.Commit();

                        if (con_WPMS.State == ConnectionState.Open)
                        {
                            con_WPMS.Close();
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
                 if (con_WPMS.State == ConnectionState.Open)
                 {
                   con_WPMS.Close();
                 }

                 if (con.State == ConnectionState.Open)
                 {
                   con.Close();
                 }
               }

            }

        # endregion

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
                    string str = "Update Acceptance_Note_Detail set WhrNumber = '" + WHR_Number + "', WHR_Date = '" + WHR_Date + "' , whrType = 'C' where WHR_Request = '" + WHR_Request + "' and Distt_ID = '" + disttid + "'  and CommodityId = '" + Commodity + "'";
                    cmd.CommandText = str;

                    string str1 = "Update Acceptance_Note_Detail set WhrNumber = '" + WHR_Number + "', WHR_Date = '" + WHR_Date + "' , whrType = 'C' where WHR_Request = '" + WHR_Request + "' and Distt_ID = '" + DistrictId + "' and CommodityId = '" + Commodity + "'";
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

        if (Commodity == "14")
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
                string str = "Update Acceptance_Note_Detail set WhrNumber = '" + WHR_Number + "', WHR_Date = '" + WHR_Date + "' , whrType = 'C' where WHR_Request = '" + WHR_Request + "' and Distt_ID = '" + disttid + "' and CommodityId = '" + Commodity + "'";
                cmd.CommandText = str;

                string str1 = "Update Acceptance_Note_Detail set WhrNumber = '" + WHR_Number + "', WHR_Date = '" + WHR_Date + "' , whrType = 'C' where WHR_Request = '" + WHR_Request + "' and Distt_ID = '" + DistrictId + "' and CommodityId = '" + Commodity + "'";
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

        if (Commodity == "8")
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (con_Maze.State == ConnectionState.Closed)
            {
                con_Maze.Open();
            }

            string mystring = DistrictId;

            string disttid = mystring.Substring(mystring.Length - 2);


            SqlTransaction trns;
            cmd.Connection = con;
            trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            cmd.Transaction = trns;

            SqlTransaction trns1;
            cmd1.Connection = con_Maze;
            trns1 = con_Maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            cmd1.Transaction = trns1;

            try
            {
                string str = "Update Acceptance_Note_Detail set WhrNumber = '" + WHR_Number + "', WHR_Date = '" + WHR_Date + "' , whrType = 'C' where WHR_Request = '" + WHR_Request + "' and Distt_ID = '" + disttid + "'  and CommodityId = '" + Commodity + "'";
                cmd.CommandText = str;

                string str1 = "Update Acceptance_Note_Detail set WhrNumber = '" + WHR_Number + "', WHR_Date = '" + WHR_Date + "' , whrType = 'C' where WHR_Request = '" + WHR_Request + "' and Distt_ID = '" + DistrictId + "' and CommodityId = '" + Commodity + "'";
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
                if (con_Maze.State == ConnectionState.Open)
                {
                    con_Maze.Close();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        # endregion

        # region Jowar

        if (Commodity == "11")
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (con_Maze.State == ConnectionState.Closed)
            {
                con_Maze.Open();
            }

            string mystring = DistrictId;

            string disttid = mystring.Substring(mystring.Length - 2);


            SqlTransaction trns;
            cmd.Connection = con;
            trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            cmd.Transaction = trns;

            SqlTransaction trns1;
            cmd1.Connection = con_Maze;
            trns1 = con_Maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            cmd1.Transaction = trns1;

            try
            {
                string str = "Update Acceptance_Note_Detail set WhrNumber = '" + WHR_Number + "', WHR_Date = '" + WHR_Date + "' , whrType = 'C' where WHR_Request = '" + WHR_Request + "' and Distt_ID = '" + disttid + "' and CommodityId = '" + Commodity + "'";
                cmd.CommandText = str;

                string str1 = "Update Acceptance_Note_Detail set WhrNumber = '" + WHR_Number + "', WHR_Date = '" + WHR_Date + "' , whrType = 'C' where WHR_Request = '" + WHR_Request + "' and Distt_ID = '" + DistrictId + "' and CommodityId = '" + Commodity + "'";
                cmd1.CommandText = str1;

                int x = cmd.ExecuteNonQuery();

                int count = cmd1.ExecuteNonQuery();

                if (count >= 1)
                {
                    trns1.Commit();

                    if (con_Maze.State == ConnectionState.Open)
                    {
                        con_Maze.Close();
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
                if (con_Maze.State == ConnectionState.Open)
                {
                    con_Maze.Close();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        # endregion

        # region Maize

        if (Commodity == "12")
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (con_Maze.State == ConnectionState.Closed)
            {
                con_Maze.Open();
            }

            string mystring = DistrictId;

            string disttid = mystring.Substring(mystring.Length - 2);


            SqlTransaction trns;
            cmd.Connection = con;
            trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            cmd.Transaction = trns;

            SqlTransaction trns1;
            cmd1.Connection = con_Maze;
            trns1 = con_Maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            cmd1.Transaction = trns1;

            try
            {
                string str = "Update Acceptance_Note_Detail set WhrNumber = '" + WHR_Number + "', WHR_Date = '" + WHR_Date + "' , whrType = 'C' where WHR_Request = '" + WHR_Request + "' and Distt_ID = '" + disttid + "' and CommodityId = '" + Commodity + "'";
                cmd.CommandText = str;

                string str1 = "Update Acceptance_Note_Detail set WhrNumber = '" + WHR_Number + "', WHR_Date = '" + WHR_Date + "' , whrType = 'C' where WHR_Request = '" + WHR_Request + "' and Distt_ID = '" + DistrictId + "' and CommodityId = '" + Commodity + "'";
                cmd1.CommandText = str1;

                int x = cmd.ExecuteNonQuery();

                int count = cmd1.ExecuteNonQuery();

                if (count >= 1)
                {
                    trns1.Commit();

                    if (con_Maze.State == ConnectionState.Open)
                    {
                        con_Maze.Close();
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
                if (con_Maze.State == ConnectionState.Open)
                {
                    con_Maze.Close();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        # endregion

        # region Jau

        if (Commodity == "40")
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (con_Maze.State == ConnectionState.Closed)
            {
                con_Maze.Open();
            }

            string mystring = DistrictId;

            string disttid = mystring.Substring(mystring.Length - 2);


            SqlTransaction trns;
            cmd.Connection = con;
            trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            cmd.Transaction = trns;

            SqlTransaction trns1;
            cmd1.Connection = con_Maze;
            trns1 = con_Maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            cmd1.Transaction = trns1;

            try
            {
                string str = "Update Acceptance_Note_Detail set WhrNumber = '" + WHR_Number + "', WHR_Date = '" + WHR_Date + "' , whrType = 'C' where WHR_Request = '" + WHR_Request + "' and Distt_ID = '" + disttid + "' and CommodityId = '" + Commodity + "'";
                cmd.CommandText = str;

                string str1 = "Update Acceptance_Note_Detail set WhrNumber = '" + WHR_Number + "', WHR_Date = '" + WHR_Date + "' , whrType = 'C' where WHR_Request = '" + WHR_Request + "' and Distt_ID = '" + DistrictId + "' and CommodityId = '" + Commodity + "'";
                cmd1.CommandText = str1;

                int x = cmd.ExecuteNonQuery();

                int count = cmd1.ExecuteNonQuery();

                if (count >= 1)
                {
                    trns1.Commit();

                    if (con_Maze.State == ConnectionState.Open)
                    {
                        con_Maze.Close();
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
                if (con_Maze.State == ConnectionState.Open)
                {
                    con_Maze.Close();
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

