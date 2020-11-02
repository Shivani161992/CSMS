using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.Configuration;
 //<summary>
//Summary description for FRDataWheat2014
 //</summary>
[WebService(Namespace = "http://tempuri.org/", Name = "FRDataWheat2014", Description = "FRData For WPMS 2014-15,Date:12/02/2014")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class FRDataWheat2014 : System.Web.Services.WebService {
    private  SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2014"].ToString());
    private  SqlCommand command;
    public   SecuredWebServiceHeader spAHeader;
    
    public FRDataWheat2014 () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    [WebMethod]
    public DataSet GetFRData2014_FarmerRegLandRecord(string dcode, string SCode)
    {
        DataSet dataset = new DataSet();
        string sfr = "";
        string slr = "";
        try
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }
                command = new SqlCommand();
                SqlDataAdapter adapter = new SqlDataAdapter();
                command.CommandType = CommandType.Text;
                command.Connection = con;
                command.CommandTimeout = 0;
              
                sfr = "SELECT *  FROM  FarmerRegistration  where District_Id='" + dcode.ToString() + "' and Procured_SocietyID ='" + SCode.ToString() + "' and  FarmerRegistration.Farmer_Id in (select  distinct  Farmer_Id from Farmer_LandRecordDescription)";
                slr = "SELECT Farmer_LandRecordDescription.* from   Farmer_LandRecordDescription  inner join FarmerRegistration on FarmerRegistration.Farmer_Id=Farmer_LandRecordDescription.Farmer_Id  where  District_Id='" + dcode.ToString() + "' and Procured_SocietyID ='" + SCode.ToString() + "'";

                command = new SqlCommand(sfr,con);
                adapter.SelectCommand = command;
                adapter.Fill(dataset, "tbl_FR");

                command = new SqlCommand(slr,con);
                adapter.SelectCommand = command;
                adapter.Fill(dataset, "tbl_LR");
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
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = con;
                command.CommandText = "Get_Society2014";
                command.Parameters.AddWithValue("@DistrictId",Did.ToString());
                command.CommandTimeout = 0;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dataset,"temp");
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
        try
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }

                string User = Username.ToString();
                string Upass = Password.ToString();
                command = new SqlCommand();
                DataSet ds = new DataSet();
                command.CommandType = CommandType.Text;
                command.Connection = con;
                command.CommandText = "select * from  WebServicesInfo where Servicename ='" + SerName.ToString() + "' and Username='" + Username.ToString() + "'";
                command.CommandTimeout = 0;
                SqlDataAdapter ad = new SqlDataAdapter(command);
                ad.Fill(ds);
                ad.Fill(ds, "Cuser");
                command.Dispose();
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
            return chkUser;


        }
    
    
}

