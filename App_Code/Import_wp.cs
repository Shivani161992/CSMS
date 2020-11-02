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
/// Summary description for Import_wp
/// </summary>
/// 

[WebService(Namespace = "http://microsoft.co.in/", Name = "RunnerServiceWheatProcurement2013", Description = "Import Data (insert data in society mdb /Date: 30012013)")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

public class Import_wp : System.Web.Services.WebService
{
    private SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2013"].ToString());
    private SqlCommand command, cmd;
    private SqlDataAdapter dataAdapter;
    private DataSet dataset;
    private SqlTransaction trans = null;
    private SqlCommand commandt = null;

    public Import_wp()
    {
    }


    #region Security

    public SecurityImport securityheadimport;
    [SoapHeader("securityheadimport")]

    [WebMethod]
    public bool chkSecurityImport(SecurityImport S)
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


    #region OnlineToOffline

    [WebMethod]
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

    [WebMethod]
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

    [WebMethod]
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

    [WebMethod]
    public DataSet OpCommodityRate()
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
            commandt.CommandText = "select * from CommodityRate where IsActive='true' and Status1='true'";
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
    public DataSet OpSocietyLoan(string districtid, string societyid, string farmerids)
    {
        dataset = new DataSet();
        try
        {
            if (farmerids == "")
            {
                farmerids = "''";
            }
            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "select * from SocietyLoanOfFarmer where Farmer_Id not in (" + farmerids + ") and  District_Id='" + districtid + "' and PC_ID='" + societyid + "'";
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
    public DataSet OpDCCBLoan(string districtid, string dccbloanids)
    {
        dataset = new DataSet();
        try
        {
            if (dccbloanids == "")
            {
                dccbloanids = "''";
            }
            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "select * from DCCBLoanOfFarmer where DCCBLoanOfFarmer.Farmer_Id not in (" + dccbloanids + ") and  DCCBLoanOfFarmer.District_Id='" + districtid + "'";
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
    public DataSet OpIrrigationLoan(string districtid, string irriloafarmerids)
    {
        dataset = new DataSet();
        try
        {
            if (irriloafarmerids == "")
            {
                irriloafarmerids = "''";
            }
            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "select * from IrrLoanOfFarmer where IrrLoanOfFarmer.Farmer_Id not in (" + irriloafarmerids + ") and  IrrLoanOfFarmer.District_Id='" + districtid + "'";
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
    public DataSet OpTransportMaster(string districtid, string societyid)
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
            commandt.CommandText = "select * from TransportMaster where District_ID='" + districtid + "' and SocietyCode='" + societyid + "'";
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
    public DataSet Optbl_MPWLC_DEPOT()
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
            commandt.CommandText = "select * from tbl_MPWLC_DEPOT ";
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
    public DataSet OpGodownStorage()
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
            commandt.CommandText = "select * from tbl_MPWLC_Godown_Storage ";
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
    public DataSet OpTehsilYeild()
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
            commandt.CommandText = "select * from TehsilYeild ";
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
    public DataSet OpSocietyLoanMaster(string D, string socloanmids)
    {
        dataset = new DataSet();
        try
        {
            if (socloanmids == "")
            {
                socloanmids = "''";
            }

            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "select * from SocietyLoanMaster where SocietyLoanMaster.Society_Id not in (" + socloanmids + ")  and SocietyLoanMaster.DistrictId='" + D + "' ";
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
    public DataSet OpDistrictFarmerCropLoss(string D, string crplosfarmer)
    {
        dataset = new DataSet();
        try
        {
            if (crplosfarmer == "")
            {
                crplosfarmer = "''";
            }
            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "select * from CropLoss where Farmer_ID not in (" + crplosfarmer + ")  and DistrictID='" + D + "'  ";
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
            //commandt.CommandText = "select * from FarmerRegistration where Farmer_id in (select distinct Farmer_ID from SocietyChangeLog where  District_Id='" + D + "' and SocietyChangeLog.NEW_PC_ID='" + S + "') and FarmerRegistration.District_Id='" + D + "' and FarmerRegistration.PC_ID='" + S + "'";
            //commandt.CommandText = "select * from FarmerRegistration where FarmerRegistration.Farmer_Id in (select SocietyChangeLog.Farmer_ID from SocietyChangeLog where SocietyChangeLog.District_ID='" + D + "' and SocietyChangeLog.NEW_PC_ID='" + S + "') and FarmerRegistration.District_Id='" + D + "'";

            commandt.CommandText = "select * from FarmerRegistration where FarmerRegistration.District_Id='" + D + "' and  FarmerRegistration.PC_ID='" + S + "'";


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
            //commandt.CommandText = "select * from Farmer_LandRecordDescription  where Farmer_id in (select distinct Farmer_ID from SocietyChangeLog where  District_Id='" + D + "' and SocietyChangeLog.NEW_PC_ID='" + S + "') and Farmer_LandRecordDescription.District_Id='" + D + "' ";
            commandt.CommandText = "select * from Farmer_LandRecordDescription  where Farmer_id in (select Farmer_id from FarmerRegistration where FarmerRegistration.District_Id='" + D + "' and  FarmerRegistration.PC_ID='" + S + "' )";
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

    //change on 19032013
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
            commandt.CommandText = "select Farmer_id from FarmerRegistration  where District_Id='" + D + "' and  PC_ID='" + S + "'";
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


    #region Common Function

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


public class SecurityImport : System.Web.Services.Protocols.SoapHeader
{
    public bool IsActive;
    public String ID;
}





