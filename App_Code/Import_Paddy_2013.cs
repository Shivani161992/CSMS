using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml.Serialization;

/// <summary>
/// Summary description for Import_Paddy_2013
/// </summary>
[WebService(Namespace = "http://microsoft.co.in/", Name = "RunnerServicePaddyProcurement2013", Description = "Import Data (insert data in society mdb /Date: 29082013)")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Import_Paddy_2013 : System.Web.Services.WebService
{
    private SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_PPMS2014"].ToString());
    private SqlCommand command, cmd;
    private SqlDataAdapter dataAdapter;
    private DataSet dataset;
    private SqlTransaction trans = null;
    private SqlCommand commandt = null;

    public Import_Paddy_2013 () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    #region Security

    public securityheadimport securityheadimport;
    [SoapHeader("securityheadimport")]

    [WebMethod(Description = "This Method Is Used For Security Check in")]
    public bool chkSecurityImport(securityheadimport S)
    {
        bool rtev = false;
        if (S != null)
        {
            OpenConnection();
            SqlCommand chkcmd = new SqlCommand();
            DataSet cds = new DataSet();
            chkcmd.Connection = connection;
            chkcmd.CommandType = CommandType.StoredProcedure;
            chkcmd.CommandText = "View_ServiceInformation";
            chkcmd.Parameters.Clear();
            chkcmd.Parameters.AddWithValue("@SID", S.ID);
            SqlDataAdapter da = new SqlDataAdapter(chkcmd);
            da.Fill(cds);
            if (cds != null)
            {
                if (cds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in cds.Tables[0].Rows)
                    {
                        //if (S.username == dr["SName"].ToString() && S.password == dr["SPasswordInClient"].ToString())
                        S.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
                        if (S.IsActive == true)
                        {
                            rtev = true;
                            return rtev;
                        }
                    }
                }
            }

            CloseConnection();
        }
        else
        {
            return rtev;
        }

        return rtev;
    }

    #endregion

    #region Export from Online To Offline.......

    [WebMethod(Description = "This Method Is Used For Update UserPassword Table in offline Database")]
    public DataSet OpUserPassword()
    {
        dataset = new DataSet();
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "select * from UserPassword where Count='1'";
            dataAdapter = new SqlDataAdapter(commandt);
            dataAdapter.Fill(dataset);
            commandt.Dispose();
            trans.Commit();

        }
        catch (Exception)
        {
            trans.Rollback();
            CloseConnection();
        }
        finally
        {
            CloseConnection();

        }
        return dataset;
    }

    [WebMethod(Description = "This Method Is Used For Insert New Village in Offline Database")]
    public DataSet OpVillages(string DistrictId)
    {
        dataset = new DataSet();
        try
        {
            OpenConnection();
            commandt = connection.CreateCommand();
            trans = connection.BeginTransaction();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "select * from VillageMaster where District_Id='" + DistrictId + "' and  VILLAGE_STATUS='N' ";
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

    [WebMethod(Description = "This Method Is Used For Insert New Bank in Offline Database")]
    public DataSet OpBankMaster()
    {
        dataset = new DataSet();
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "select * from Bank where Status='N'";
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

    [WebMethod(Description = "This Method Is Used For Checking Date")]
    public DataSet OpDateControl()
    {
        dataset = new DataSet();
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "select * from DateControl where IsActive='true'";
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
    public DataSet OpSocietyFarmerIDs(string D, string S)
    {

        dataset = new DataSet();
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "select Farmer_id from FarmerRegistration_New  where District_Id='" + D + "' and  Procured_SocietyID='" + S + "'";
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
    public DataSet OpSocietyFamrerRegInfo(string D, string S)
    {
        dataset = new DataSet();
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "select * from FarmerRegistration_New where FarmerRegistration_New.District_Id='" + D + "' and  FarmerRegistration_New.Procured_SocietyID='" + S + "'";
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
    public DataSet OpSocietyFarmerLandInfo(string D, string S)
    {
        dataset = new DataSet();
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "select * from Farmer_LandRecordDescription_New  where NewFarmer_Id in (select NewFarmer_Id from FarmerRegistration_New where FarmerRegistration_New.District_Id='" + D + "' and  FarmerRegistration_New.Procured_SocietyID='" + S + "' )";
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

    #endregion

    #region Commonly Used Functions

    private string getRDate_MDY(string inDate)
    {
        string dat = "";
        if (inDate != "")
        {
            DateTime imd = Convert.ToDateTime(inDate);
            dat = imd.ToShortDateString();
            imd = Convert.ToDateTime(dat);
            dat = imd.ToString("MM/dd/yyyy");
        }
        return dat;
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

    #endregion

    #region Check Connection

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

public class securityheadimport : System.Web.Services.Protocols.SoapHeader
{
    public bool IsActive;
    public String ID;
}

