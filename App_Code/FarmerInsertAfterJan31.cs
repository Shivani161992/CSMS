using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


/// <summary>
/// Summary description for FarmerInsertAfterJan31
/// </summary>
[WebService(Namespace = "http://microsoft.co.in/" ,Description = "Import Farmer Data That is uploaded after 31 january")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class FarmerInsertAfterJan31 : System.Web.Services.WebService
{


    private SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2013"].ToString());
    private SqlCommand command, cmd;
    private SqlDataAdapter dataAdapter;
    private DataSet dataset;
    private SqlTransaction trans = null;
    private SqlCommand commandt = null;

    public FarmerInsertAfterJan31()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public DataSet OpSocietyRemainingFarmerPersonal(string D, string S, string Farmerids)
    {
        dataset = new DataSet();
        try
        {
            if (Farmerids == "")
            {
                Farmerids = "''";
            }
            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "select * from FarmerRegistration  where Farmer_id not in (" + Farmerids + ") and  FarmerRegistration.District_Id='" + D + "' and FarmerRegistration.PC_ID='" + S + "'";
            dataAdapter = new SqlDataAdapter(commandt);
            dataAdapter.Fill(dataset);
            commandt.Dispose();
            trans.Commit();
        }
        catch (Exception)
        {
            trans.Commit();
            CloseConnection();
        }
        finally
        {
            CloseConnection();
        }
        return dataset;


    }


    [WebMethod]
    public DataSet OpSocietyRemainingFarmerLandRecord(string D, string S, string FarmerIds)
    {
        dataset = new DataSet();
        try
        {
            if (FarmerIds == "")
            {
                FarmerIds = "''";
            }
            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "select * from Farmer_LandRecordDescription  where Farmer_LandRecordDescription.Farmer_id  in (" + FarmerIds + ") ";
            dataAdapter = new SqlDataAdapter(commandt);
            dataAdapter.Fill(dataset);
            commandt.Dispose();
            trans.Commit();
        }
        catch (Exception)
        {
            trans.Commit();
            CloseConnection();
        }
        finally
        {
            CloseConnection();
        }
        return dataset;


    }





    #region Common Function


    public void OpenConnection()
    {
        if (connection.State == ConnectionState.Open)
        {
            connection.Close();
            connection.Open();
        }
        else
        {
            connection.Open();
        }

    }

    public void CloseConnection()
    {
        if (connection.State == ConnectionState.Open)
        {
            connection.Close();
        }
    }


    #endregion


}

