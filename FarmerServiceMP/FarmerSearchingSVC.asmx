<%@ WebService Language="C#" Class="FarmerSearchingSVC" %>

using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

[WebService(Namespace = "http://microsoft.co.in/", Name = "MPFarmerService", Description = "Get MadhyaPradesh Farmer Details :: Hosted Date 01/07/2013")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class FarmerSearchingSVC : System.Web.Services.WebService
{
    private SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionStringFarmerDB"].ToString());
    private SqlCommand command, cmd;
    private SqlDataAdapter dataAdapter;
    private DataSet dataset;
    private SqlTransaction trans = null;
    private SqlCommand commandt = null;

    public bool chkSecurityFarmer(string username, string password)
    {
        bool rtev = false;
        bool chkTemp = false;

        if (username != null && password != null)
        {
            OpenConnection();
            SqlCommand chkcmd = new SqlCommand();
            DataSet cds = new DataSet();
            chkcmd.Connection = connection;
            chkcmd.CommandType = CommandType.StoredProcedure;
            chkcmd.CommandText = "View_ServiceInformation";
            chkcmd.Parameters.Clear();
            chkcmd.Parameters.AddWithValue("@SPasswordInClient", password.Trim());
            chkcmd.Parameters.AddWithValue("@UserName", username.Trim());
            SqlDataAdapter da = new SqlDataAdapter(chkcmd);
            da.Fill(cds);
            if (cds != null)
            {
                if (cds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in cds.Tables[0].Rows)
                    {
                        chkTemp = Convert.ToBoolean(dr["IsActive"].ToString());
                        if (chkTemp == true)
                        {
                            rtev = true;
                            return rtev;
                        }
                        else
                        {
                            rtev = false;
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

    [WebMethod(Description = "Send Farmer Name in hindi(unicode) or english, username and password ")]
    public DataSet getFarmerByName(string FarmerName, string username, string password)
    {
        dataset = new DataSet();
        try
        {
            bool res = chkSecurityFarmer(username.Trim(), password.Trim());
            OpenConnection();
            if (res)
            {
                trans = connection.BeginTransaction();
                commandt = connection.CreateCommand();
                commandt.Transaction = trans;
                commandt.CommandType = CommandType.StoredProcedure;
                commandt.CommandText = "View_FarmerByName";
                commandt.Parameters.Clear();
                commandt.Parameters.Add("@FarmerName", FarmerName);
                dataAdapter = new SqlDataAdapter(commandt);
                dataAdapter.Fill(dataset);
                commandt.Dispose();
                trans.Commit();
            }
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

    [WebMethod(Description = "Send crop name in hindi(unicode) and District name in hindi(unicode),english, username and password ")]
    public DataSet getFarmerByCrop(string CropName, string DistrictName, string username, string password)
    {
        dataset = new DataSet();
        try
        {
            bool res = chkSecurityFarmer(username.Trim(), password.Trim());
            OpenConnection();
            if (res)
            {
                trans = connection.BeginTransaction();
                commandt = connection.CreateCommand();
                commandt.Transaction = trans;
                commandt.CommandType = CommandType.StoredProcedure;
                commandt.CommandText = "proc_view_FarmerByCropName";
                commandt.Parameters.Clear();
                commandt.Parameters.Add("@cropname", CropName.Trim());
                commandt.Parameters.Add("@District_Name", DistrictName.Trim());
                dataAdapter = new SqlDataAdapter(commandt);
                dataAdapter.Fill(dataset);
                commandt.Dispose();
                trans.Commit();
            }
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


    [WebMethod(Description = "Send FarmerId , username ,password ")]
    public DataSet getFarmerByFarmerID(string FarmerId, string username, string password)
    {
        dataset = new DataSet();
        try
        {
            bool res = chkSecurityFarmer(username.Trim(), password.Trim());
            OpenConnection();
            if (res)
            {
                trans = connection.BeginTransaction();
                commandt = connection.CreateCommand();
                commandt.Transaction = trans;
                commandt.CommandType = CommandType.StoredProcedure;
                commandt.CommandText = "View_FarmerByFarmerId";
                commandt.Parameters.Clear();
                commandt.Parameters.Add("@Farmer_Id", FarmerId.Trim());
                dataAdapter = new SqlDataAdapter(commandt);
                dataAdapter.Fill(dataset);
                commandt.Dispose();
                trans.Commit();
            }
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

    [WebMethod(Description = "Send District Name  in hindi(unicode) or english ,username ,password")]
    public DataSet getFarmerByDistrictName(string DistrictName, string username, string password)
    {
        dataset = new DataSet();
        try
        {
            bool res = chkSecurityFarmer(username.Trim(), password.Trim());
            OpenConnection();
            if (res)
            {
                trans = connection.BeginTransaction();
                commandt = connection.CreateCommand();
                commandt.Transaction = trans;
                commandt.CommandType = CommandType.StoredProcedure;
                commandt.CommandText = "proc_vew_FarmerByDistrictName";
                commandt.Parameters.Clear();
                commandt.Parameters.Add("@District_Name", DistrictName.Trim());
                dataAdapter = new SqlDataAdapter(commandt);
                dataAdapter.Fill(dataset);
                commandt.Dispose();
                trans.Commit();
            }
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

    [WebMethod(Description = "Send DistrictId  ,username ,password")]
    public DataSet getFarmerByDistrictID(string DistrictID, string username, string password)
    {
        dataset = new DataSet();
        try
        {
            bool res = chkSecurityFarmer(username.Trim(), password.Trim());
            OpenConnection();
            if (res)
            {
                trans = connection.BeginTransaction();
                commandt = connection.CreateCommand();
                commandt.Transaction = trans;
                commandt.CommandType = CommandType.StoredProcedure;
                commandt.CommandText = "proc_vew_FarmerByDistrictID";
                commandt.Parameters.Clear();
                commandt.Parameters.Add("@District_Id", DistrictID.Trim());
                dataAdapter = new SqlDataAdapter(commandt);
                dataAdapter.Fill(dataset);
                commandt.Dispose();
                trans.Commit();
            }
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

    [WebMethod(Description = "Send Tehsil Name  in hindi(unicode) ,username ,password ")]
    public DataSet getFarmerByTehislName(string TehsilName, string username, string password)
    {
        dataset = new DataSet();
        try
        {
            bool res = chkSecurityFarmer(username.Trim(), password.Trim());
            OpenConnection();
            if (res)
            {
                trans = connection.BeginTransaction();
                commandt = connection.CreateCommand();
                commandt.Transaction = trans;
                commandt.CommandType = CommandType.StoredProcedure;
                commandt.CommandText = "proc_vew_FarmerByTehsilName";
                commandt.Parameters.Clear();
                commandt.Parameters.Add("@Tehsil_Name", TehsilName.Trim());
                dataAdapter = new SqlDataAdapter(commandt);
                dataAdapter.Fill(dataset);
                commandt.Dispose();
                trans.Commit();
            }
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

    [WebMethod(Description = "Send TehsilID ,username ,password ")]
    public DataSet getFarmerByTehislID(string TehsilID, string username, string password)
    {
        dataset = new DataSet();
        try
        {
            bool res = chkSecurityFarmer(username.Trim(), password.Trim());
            OpenConnection();
            if (res)
            {
                trans = connection.BeginTransaction();
                commandt = connection.CreateCommand();
                commandt.Transaction = trans;
                commandt.CommandType = CommandType.StoredProcedure;
                commandt.CommandText = "proc_vew_FarmerByTehsilID";
                commandt.Parameters.Clear();
                commandt.Parameters.Add("@Tehsil_Id", TehsilID.Trim());
                dataAdapter = new SqlDataAdapter(commandt);
                dataAdapter.Fill(dataset);
                commandt.Dispose();
                trans.Commit();
            }
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

}

