using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.Configuration;

/// <summary>
///Summary description for FRDataMaize2013
///</summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

public class FRDataPaddy2013 : System.Web.Services.WebService
{
    private SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_PPMS2013"].ToString());
    private SqlCommand command;
    public SecuredWebServiceHeader spAHeader;
    public FRDataPaddy2013()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public DataSet GetFRData_FarmerRegistration(string dcode, string SCode)
    {
        DataSet dataset = new DataSet();

        try
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = con;
                command.CommandText = "GetFRData_FarmerRegistration_Paddy";
                command.Parameters.AddWithValue("@District_Id", dcode.ToString());
                command.Parameters.AddWithValue("@Procured_SocietyID", SCode.ToString());
                command.CommandTimeout = 0;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dataset);
                command.Dispose();

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }


        return dataset;

    }
   

    [WebMethod]

    public DataSet GetFRData_LandRecord(string dcode, string SCode)
    {
        DataSet dataset = new DataSet();

        try
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = con;
                command.CommandText = "GetFRData_Farmer_LandRecordDescription_Paddy";
                command.Parameters.AddWithValue("@District_Id", dcode.ToString());
                command.Parameters.AddWithValue("@Procured_SocietyID", SCode.ToString());

                command.CommandTimeout = 0;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dataset);
                command.Dispose();

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }


        return dataset;

    }
    [WebMethod]

    public DataSet Get_Society(string Did)
    {
        DataSet dataset = new DataSet();

        try
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }
                command = new SqlCommand();
                String qrySelect = "Select * from Society  where DistrictId='" + Did + "'";
                command.CommandText = qrySelect;
                command.Connection = con;
                command.CommandTimeout = 0;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dataset);
                command.Dispose();

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }


        return dataset;

    }

    public class SecuredWebServiceHeader : System.Web.Services.Protocols.SoapHeader
    {
        public string Sername;
        public string Username;
        public string Password;

    }
    [WebMethod]
    public bool Check_User(string SerName, string Username, string Password)
    {
        if (Check_UserAuthentication(SerName, Username, Password))
            return true;
        else
            return false;
    }
    public bool Check_UserAuthentication(string SerName, string Username, string Password)
    {
        bool chkUser = false;
        if (con != null)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();

            }

            string User = Username.ToString();
            string Upass = Password.ToString();

            SqlDataAdapter ad = new SqlDataAdapter();
            DataSet ds = new DataSet();
            string sqlstr = "select * from  WebServicesInfo where Servicename ='" + SerName + "' and Username='" + Username + "'";
            SqlCommand cmd = new SqlCommand(sqlstr, con);
            ad.SelectCommand = cmd;
            ad.Fill(ds, "Cuser");
            cmd.Dispose();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    DataRow dr = ds.Tables[0].Rows[0];

                    string username = dr["Username"].ToString();
                    string pwd = dr["Password"].ToString();

                    if (User == Username && Upass == pwd)
                    {

                        chkUser = true;

                    }

                    else
                    {

                        chkUser = false;


                    }


                }


            }
            else
            {

                return chkUser;


            }

        }

        return chkUser;


    }

}

