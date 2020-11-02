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
/// Summary description for Import_Wheat_Reg_2014
/// </summary>
[WebService(Namespace = "http://microsoft.co.in/", Name = "ImportWheatRegistration_2014", Description = "Import Data (upload data on server)/Date: 10/12/2013")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Import_Wheat_Reg_2014 : System.Web.Services.WebService 
{
    private SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_WPMS2014"].ToString());
    private SqlCommand command, cmd;
    private SqlDataAdapter dataAdapter;
    private DataSet dataset;
    private SqlTransaction trans = null;
    private SqlCommand commandt = null;

    public Import_Wheat_Reg_2014 () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    #region Security

    public security_Wheat_import_Reg_2014 security_Wheat_import_Reg_2014;
    [SoapHeader("security_Wheat_import_Reg_2014")]

    [WebMethod(Description = "This Method Is Used For Security Check in")]
    public bool chkSecurityImport(security_Wheat_import_Reg_2014 S)
    {
        bool rtev = false;
        try
        {
            if (S != null)
            {
                OpenConnection();
                SqlCommand chkcmd = new SqlCommand();
                DataSet cds = new DataSet();
                chkcmd.Connection = connection;
                chkcmd.CommandType = CommandType.StoredProcedure;
                chkcmd.CommandText = "View_ServiceInformation";
                chkcmd.Parameters.Clear();
                chkcmd.Parameters.AddWithValue("@UserName", S.UserName);
                chkcmd.Parameters.AddWithValue("@SPasswordInClient", S.Password);

                SqlDataAdapter da = new SqlDataAdapter(chkcmd);
                da.Fill(cds);
                if (cds != null)
                {
                    if (cds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in cds.Tables[0].Rows)
                        {
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
        }
        catch
        {
            /////////
        }
        return rtev;
    }

    #endregion

    #region Export Data from Online To Offline.......

    [WebMethod(Description = "This Method Is Used For Insert New Village in Offline Database")]
    public DataSet OpVillages(string DistrictId, string Villagecodes)
    {
        dataset = new DataSet();
        try
        {
            if (Villagecodes == "")
            {
                Villagecodes = "''";
            }
            OpenConnection();
            commandt = connection.CreateCommand();
            trans = connection.BeginTransaction();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "select * from VillageMaster where District_Id='" + DistrictId + "' and  VILLAGE_STATUS='N' and VILLAGE_CODE not in (" + Villagecodes + ") ";
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
            commandt.CommandText = "select * from BankMaster where Status='N'";
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

    [WebMethod(Description = "This Method Is Used For Registration Date output for offline database ")]
    public DataSet OpRegDateControl()
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
            commandt.CommandText = "select * from RegistrationDateControl where status='Change'";
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

public class security_Wheat_import_Reg_2014 : System.Web.Services.Protocols.SoapHeader
{
    public bool IsActive;
    public String Password;
    public String UserName;
    public String RunnerId;
    public String RunnerPassword;
}

