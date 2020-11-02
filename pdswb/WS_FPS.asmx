<%@ WebService Language="C#" Class="WS_FPS" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;


[WebService(Namespace = "http://microsoft.co.in/", Name = "PDS_Service_For_Android", Description = "Inserting , Hosted Date :: ")]

[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WS_FPS  : System.Web.Services.WebService {
    SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["constr_andro"].ToString());
    string Result = "";
    private SqlTransaction trans = null;
   
    #region FirstTimeLogin
    [WebMethod]
    public bool FirstLogin(string Userid, string Pwd,string localip)
    {
        try
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            string str = "SELECT * FROM [PDS_ANDRO].[dbo].[tbl_FPSLogin]  where [UserId]='" + Userid + "' and [Pwd]='" + Pwd + "'";
            SqlDataAdapter da = new SqlDataAdapter(str, connection);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string Uptbl = "UPDATE [PDS_ANDRO].[dbo].[tbl_FPSLogin] SET [LoginDT] =GETDATE(),[IpAddress] ='" + localip + "' WHERE [UserId]='" + Userid + "' and [Pwd]='" + Pwd + "'";
                SqlCommand cmd = new SqlCommand(Uptbl, connection);
                cmd.ExecuteNonQuery();
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            connection.Close();
        }

    }
    #endregion

    #region Fatching Data all detail family 
    
    [WebMethod]
    public XmlElement GetData(string Userid, string pwd, string SamgraId)
    {
        XmlElement xmlElement =null;
        try
        {
            if (Userid != "" && pwd != "")
            {
                bool a;
                a = login_fps(Userid.ToString(), pwd.ToString());
                if (a == true)
                {                   
                    Result = "True";
                    DataSet ds=fetchdata_logindetails(Userid, pwd);
                    string fps_code=ds.Tables[0].Rows[0].ItemArray[1].ToString();
                    xmlElement = GetUserDetails(SamgraId,fps_code);                   
                }
                else
                {
                   
                    return null;
                }
            }
            else
            {

            }
            return xmlElement;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    [WebMethod]
    public XmlElement GetUserDetails(string SamgraId,string fpscode)
    {
        try
        {
            XmlElement xmlElement = null;
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["constr_andro"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand(" SELECT *  FROM [PDS_ANDRO].[dbo].[tbl_FPSData] where [SamagraFamilyID]=@userName and [FPS_Code]=@fpscode", con);
            cmd.Parameters.AddWithValue("@userName", SamgraId);
            cmd.Parameters.AddWithValue("@fpscode", fpscode);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            // Create an instance of DataSet.
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            string strm = string.Empty;
            if (ds.Tables[0].Rows.Count > 0)
            {
                // Return the DataSet as an XmlElement.
                XmlDataDocument xmldata = new XmlDataDocument(ds);
                xmlElement = xmldata.DocumentElement;
            }
            else
            {
               
            }
            return xmlElement;
        }
        catch(Exception)
        {
            throw new SoapException("false", SoapException.ClientFaultCode);
        }

    }
    [WebMethod]
    public bool login_fps(string Userid, string Pwd)
    {
        try
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            string str = "SELECT [Fps_Code],[FpsName] FROM [PDS_ANDRO].[dbo].[tbl_FPSLogin]  where [UserId]='" + Userid + "' and [Pwd]='" + Pwd + "'";
            SqlDataAdapter da = new SqlDataAdapter(str, connection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            connection.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    
    [WebMethod]
    public DataSet fetchdata_logindetails(object userid, object password)
    {
        try
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            string str = "SELECT [DistrictId],[Fps_Code],[FpsName] FROM [PDS_ANDRO].[dbo].[tbl_FPSLogin]  where [UserId]='" + userid + "' and [Pwd]='" + password + "'";
            SqlDataAdapter da = new SqlDataAdapter(str, connection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            connection.Close();
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    
    [WebMethod]
    public bool InsertFPSData(string fpscode, string sfid, string cardid, string month, string year, string wheatA, string RiceA, string MaizeA, string SaltA, string SugarA, string KeroseneA, string totlEtm, string localip, string DistrictCode, string totallifting, string lflag, string remlift)
    {
        SqlCommand cmd=null;
        bool result = false;
        try
        {
                connection.Open();
               
                cmd = new SqlCommand();
                cmd.Connection = connection;

                string str = "SELECT * FROM [PDS_ANDRO].[dbo].[tbl_FPSMonthWiseAllotment] where [l_Month]='" + month + "' and [l_Year]='" + year + "' and FPS_Code='" + fpscode + "' and Samagra_Family_ID='" + sfid + "' AND Finl_flag='"+lflag+"' AND remaining_lifting='"+remlift+"'";
            SqlDataAdapter da = new SqlDataAdapter(str, connection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {              
                if (lflag == "N" || remlift != "0")
                {
                    try
                    {
                        trans = connection.BeginTransaction();
                        cmd.Transaction = trans;
                        string StrIns = "INSERT INTO [PDS_ANDRO].[dbo].[tbl_FPSMonthWiseAllotment]([FPS_Code],[Samagra_Family_ID],[card_Id],[l_Month],[l_Year],[Wheat_L],[Rice_L],[Maize_L],[Salt_L],[Sugar_L],[Kerosene_L],[Total_Entitalment],[DTime_allotment],[Ip_Add],[DistrictCode],total_lifting,Finl_flag,remaining_lifting) VALUES ('" + fpscode + "','" + sfid + "','" + cardid + "','" + month + "','" + year + "','" + wheatA + "','" + RiceA + "','" + MaizeA + "','" + SaltA + "','" + SugarA + "','" + KeroseneA + "','" + totlEtm + "',GETDATE(),'" + localip + "','" + DistrictCode + "','"+totallifting+"','"+lflag+"','"+remlift+"')";
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = StrIns;
                        int x = cmd.ExecuteNonQuery();
                        trans.Commit();
                        if (x == 1)
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        connection.Close();
                    }
                }
                else
                {
                    result = false;
                }
            }                
            else
            {

                try
                {
                    trans = connection.BeginTransaction();
                    cmd.Transaction = trans;
                    string StrIns = "INSERT INTO [PDS_ANDRO].[dbo].[tbl_FPSMonthWiseAllotment]([FPS_Code],[Samagra_Family_ID],[card_Id],[l_Month],[l_Year],[Wheat_L],[Rice_L],[Maize_L],[Salt_L],[Sugar_L],[Kerosene_L],[Total_Entitalment],[DTime_allotment],[Ip_Add],[DistrictCode],total_lifting,Finl_flag,remaining_lifting) VALUES ('" + fpscode + "','" + sfid + "','" + cardid + "','" + month + "','" + year + "','" + wheatA + "','" + RiceA + "','" + MaizeA + "','" + SaltA + "','" + SugarA + "','" + KeroseneA + "','" + totlEtm + "',GETDATE(),'" + localip + "','" + DistrictCode + "','" + totallifting + "','" + lflag + "','" + remlift + "')";
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = StrIns;
                    int x = cmd.ExecuteNonQuery();
                    trans.Commit();
                    if (x == 1)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                catch (Exception)
                {
                    trans.Rollback();
                    connection.Close();
                }
            }
            }
        catch (Exception)
        {
            
        }
        finally
        {
            connection.Close();
        }
        return result;
    }

    [WebMethod]
    public XmlElement GetFPSData(string fpscode)
    {
        try
        {
            XmlElement xmlElement = null;           
            connection.Open();
            SqlCommand cmd = new SqlCommand("SELECT *  FROM [PDS_ANDRO].[dbo].[tbl_FPSData] where [FPS_Code]=@fpscode", connection);
            cmd.Parameters.AddWithValue("@fpscode", fpscode);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            // Create an instance of DataSet.
            DataSet ds = new DataSet();
            da.Fill(ds);
            connection.Close();
            string strm = string.Empty;
            if (ds.Tables[0].Rows.Count > 0)
            {
                // Return the DataSet as an XmlElement.
                XmlDataDocument xmldata = new XmlDataDocument(ds);
                xmlElement = xmldata.DocumentElement;
            }
            else
            {

            }
            return xmlElement;
        }
        catch (Exception)
        {
            throw new SoapException("false", SoapException.ClientFaultCode);
        }

    }
    [WebMethod]
    public DataSet FetchDataSqlite()
    {
        SqlCommand commandt = null;
        DataSet ds = null;

        try
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            string str = "SELECT * FROM [PDS_ANDRO].[dbo].[tbl_FPSMonthWiseAllotment]";
            commandt = new SqlCommand(str, connection, trans);
            SqlDataAdapter da = new SqlDataAdapter(commandt);
            ds = new DataSet();
            da.Fill(ds);
            commandt.Dispose();
            trans.Commit();
        }
        catch (Exception ex)
        {
            connection.Close();
        }
        finally
        {
            connection.Close();
        }
        return ds;
    }
    public bool CheckFetchData(DataSet CFD)
    {
        bool result = false;
        SqlCommand commandt = null;
        try
        {
            if (CFD != null)
            {
                if (CFD.Tables[0].Rows.Count > 0)
                {
                    trans = connection.BeginTransaction();
                    foreach (DataRow dr in CFD.Tables[0].Rows)
                    {
                        try
                        {
                            string fpscode = dr["fpscode"].ToString();
                            string month = dr["month"].ToString();
                            string year = dr["year"].ToString();
                            string sfid = dr["Samagra_Family_ID"].ToString();

                            commandt = connection.CreateCommand();
                            commandt.Transaction = trans;
                            commandt.CommandType = CommandType.Text;
                            commandt.CommandText = "SELECT * FROM [PDS_ANDRO].[dbo].[tbl_FPSMonthWiseAllotment] WHERE FPS_Code='" + fpscode + "' AND [l_Month]='" + month + "' AND [l_Year]='" + year + "' AND Samagra_Family_ID='" + sfid + "' ";
                            Int64 res = Convert.ToInt64(commandt.ExecuteScalar());
                            commandt.Dispose();
                            if (res <= 0)
                            {
                                string UserId = dr["UserId"].ToString();
                                string IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();

                                SqlCommand cmd = connection.CreateCommand();
                                cmd.Transaction = trans;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "";
                                cmd.Parameters.Clear();

                            }
                        }
                        catch
                        {
                            //////////////
                        }
                    }
                    trans.Commit();
                    result = true;
                }
                else
                {
                }
            }
        }
        catch (Exception)
        {
            trans.Rollback();
            connection.Close();

        }
        finally
        {
            connection.Close();
        }
        return result;
    }
    
  [WebMethod]
    public XmlElement GetPreviousData(string Userid, string pwd, string SamgraFId,string lmonth,string lyear)
    {
        XmlElement xmlElement =null;
        try
        {
            if (Userid != "" && pwd != "")
            {
                bool a;
                a = login_fps(Userid.ToString(), pwd.ToString());
                if (a == true)
                {                   
                    Result = "True";
                    DataSet ds=fetchdata_logindetails(Userid, pwd);
                    string fps_code=ds.Tables[0].Rows[0].ItemArray[1].ToString();
                    xmlElement = GetPreDetails(SamgraFId, fps_code,lmonth,lyear);                   
                }
                else
                {                  
                    return null;
                }
            }
            else
            {
                return null;
            }
            return xmlElement;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
  [WebMethod]
  public XmlElement GetPreDetails(string SamgraFId, string fpscode,string lmonth,string lyear)
  {
      try
      {
          XmlElement xmlElement = null;
          SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["constr_andro"].ToString());
          con.Open();
          SqlCommand cmd = new SqlCommand("SELECT distinct FPS_Code,Samagra_Family_ID,card_Id,l_Month,l_year,SUM(Wheat_L) AS Wheat_L ,SUM(Rice_L) AS Rice_L,SUM(Maize_L) AS Maize_L,SUM(Salt_L) AS Salt_L,SUM(Sugar_L) AS Sugar_L,SUM(Kerosene_L) AS Kerosene_L,SUM(total_lifting) AS total_lifting,Total_Entitalment,DistrictCode FROM [PDS_ANDRO].[dbo].[tbl_FPSMonthWiseAllotment] WHERE l_Month=@lmonth and l_Year=@lyear and FPS_Code=@fpscode and Samagra_Family_ID=@SamgraFId  GROUP BY Samagra_Family_ID,FPS_Code,l_Month,l_Year,card_Id,Total_Entitalment,DistrictCode", con);
          cmd.Parameters.AddWithValue("@SamgraFId", SamgraFId);
          cmd.Parameters.AddWithValue("@fpscode", fpscode);
          cmd.Parameters.AddWithValue("@lmonth", lmonth);
          cmd.Parameters.AddWithValue("@lyear",lyear);
          cmd.ExecuteNonQuery();
          SqlDataAdapter da = new SqlDataAdapter(cmd);
          // Create an instance of DataSet.
          DataSet ds = new DataSet();
          da.Fill(ds);
          con.Close();
          string strm = string.Empty;
          if (ds.Tables[0].Rows.Count > 0)
          {
              // Return the DataSet as an XmlElement.
              XmlDataDocument xmldata = new XmlDataDocument(ds);
              xmlElement = xmldata.DocumentElement;
          }
          else
          {
              return null;
          }
          return xmlElement;
      }
      catch (Exception)
      {
          throw new SoapException("false", SoapException.ClientFaultCode);
      }

  }
  
    
}











           


