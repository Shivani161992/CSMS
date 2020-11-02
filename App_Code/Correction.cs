using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for Correction
/// </summary>
/// 
[WebService(Namespace = "http://microsoft.co.in/", Name = "RunnerServiceWheatProcurement2013", Description = "Correction Data (upload data on server)/Date: 30012013")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]


public class Correction : System.Web.Services.WebService
{
    private SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2013"].ToString());
    private SqlCommand command, cmd;
    private SqlDataAdapter dataAdapter;
    private DataSet dataset;
    private SqlTransaction trans = null;
    private SqlCommand commandt = null;


    public Correction()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }


    #region Security

    public SecurityCorrection securityheadcorrection;
    [SoapHeader("securityheadcorrection")]
    [WebMethod]
    public bool chkSecurityCorrection(SecurityCorrection S)
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


    #region Correction

    [WebMethod]
    public bool InGuunyBagsCorrection(DataSet dsGunnyBags)
    {
        bool result = false;
        try
        {

            if (dsGunnyBags != null)
            {
                if (dsGunnyBags.Tables[0].Rows.Count > 0)
                {

                    OpenConnection();
                    trans = connection.BeginTransaction();
                    foreach (DataRow dr in dsGunnyBags.Tables[0].Rows)
                    {
                        string IssueNo_or_GReceiptNo = dr["IssueNo_or_GReceiptNo"].ToString();
                        string District_ID = dr["District_ID"].ToString();
                        string Society_Id = dr["Society_Id"].ToString();
                        string NoOfBags = dr["NoOfBags"].ToString();
                        string IsUpdated = dr["IsUpdated"].ToString();
                        string IsDeleted = dr["IsDeleted"].ToString();
                        commandt = connection.CreateCommand();
                        commandt.Transaction = trans;
                        //commandt.CommandTimeout = 0;
                        commandt.CommandType = CommandType.Text;
                        commandt.CommandText = "select count(*) from GunnyBagsCorrectionLog where District_ID='" + District_ID + "'  and Society_Id= '" + Society_Id + "' and IssueNo_or_GReceiptNo='" + IssueNo_or_GReceiptNo + "' and NoOfBags='" + NoOfBags + "' and IsUpdated='" + IsUpdated + "' and IsDeleted='" + IsDeleted + "'";
                        Int64 res = Convert.ToInt64(commandt.ExecuteScalar());
                        commandt.Dispose();

                        if (res <= 0)
                        {
                            string UserId = dr["UserId"].ToString();
                            string IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();

                            cmd = connection.CreateCommand();
                            cmd.Transaction = trans;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "in_GunnyBagsCorrectionLog";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@IssueNo_or_GReceiptNo", IssueNo_or_GReceiptNo);
                            cmd.Parameters.AddWithValue("@District_ID", District_ID);
                            cmd.Parameters.AddWithValue("@Society_Id", Society_Id);
                            cmd.Parameters.AddWithValue("@NoOfBags", NoOfBags);
                            cmd.Parameters.AddWithValue("@IsUpdated", IsUpdated);
                            cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                            cmd.Parameters.AddWithValue("@UserId", UserId);
                            cmd.Parameters.AddWithValue("@IP", IP);
                            cmd.ExecuteNonQuery();
                        }

                    }
                    trans.Commit();
                    result = true;

                }
            }
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
        return result;
    }

    [WebMethod]
    public bool InPrapteeCorrectionLog(DataSet dsPrapteeCorrection)
    {
        bool result = false;
        try
        {
            if (dsPrapteeCorrection != null)
            {
                if (dsPrapteeCorrection.Tables[0].Rows.Count > 0)
                {
                    OpenConnection();
                    trans = connection.BeginTransaction();

                    foreach (DataRow dr in dsPrapteeCorrection.Tables[0].Rows)
                    {
                        string ReceivedID = dr["ReceivedID"].ToString();
                        string District_Id = dr["District_Id"].ToString();
                        string Society_Id = dr["Society_Id"].ToString();
                        string Old_Bags = dr["Old_Bags"].ToString();
                        string New_Bags = dr["New_Bags"].ToString();
                        string Old_QtyReceived = dr["Old_QtyReceived"].ToString();
                        string New_QtyReceived = dr["New_QtyReceived"].ToString();
                        string IsDelete = dr["IsDelete"].ToString();
                        string IsUpdated = dr["IsUpdated"].ToString();

                        commandt = connection.CreateCommand();
                        commandt.Transaction = trans;
                        commandt.CommandType = CommandType.Text;
                        commandt.CommandText = "select count(*) from PrapteeCorrectionLog where District_Id='" + District_Id + "'  and Society_Id= '" + Society_Id + "' and ReceivedID='" + ReceivedID + "' and Old_Bags='" + Old_Bags + "' and New_Bags='" + New_Bags + "' and Old_QtyReceived='" + Old_QtyReceived + "' and New_QtyReceived='" + New_QtyReceived + "'  and  IsUpdated='" + IsUpdated + "' and IsDelete='" + IsDelete + "'";
                        Int64 res = Convert.ToInt64(commandt.ExecuteScalar());
                        commandt.Dispose();
                        if (res <= 0)
                        {
                            string Old_TotalAmount = dr["Old_TotalAmount"].ToString();
                            string New_TotalAmount = dr["New_TotalAmount"].ToString();
                            string UpdationDate = getRDate_MDY(dr["UpdationDate"].ToString());
                            string UserID = dr["UserID"].ToString();
                            string IP = dr["IP"].ToString();

                            cmd = connection.CreateCommand();
                            cmd.Transaction = trans;
                            // cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "in_PrapteeCorrectionLog";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@ReceivedID", ReceivedID);
                            cmd.Parameters.AddWithValue("@District_Id", District_Id);
                            cmd.Parameters.AddWithValue("@Society_Id", Society_Id);
                            cmd.Parameters.AddWithValue("@Old_Bags", Old_Bags);
                            cmd.Parameters.AddWithValue("@New_Bags", New_Bags);
                            cmd.Parameters.AddWithValue("@Old_QtyReceived", Old_QtyReceived);
                            cmd.Parameters.AddWithValue("@New_QtyReceived", New_QtyReceived);
                            cmd.Parameters.AddWithValue("@IsDelete", IsDelete);
                            cmd.Parameters.AddWithValue("@IsUpdated", IsUpdated);
                            cmd.Parameters.AddWithValue("@Old_TotalAmount", Old_TotalAmount);
                            cmd.Parameters.AddWithValue("@New_TotalAmount", New_TotalAmount);
                            cmd.Parameters.AddWithValue("@UpdationDate", UpdationDate);
                            cmd.Parameters.AddWithValue("@UserID", UserID);
                            cmd.Parameters.AddWithValue("@IP", IP);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    trans.Commit();
                    result = true;
                }
            }
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
        return result;
    }

    [WebMethod]
    public bool InJareeCorrectionLog(DataSet dsJareeCorrectionLog)
    {
        bool result = false;
        try
        {
            if (dsJareeCorrectionLog != null)
            {
                if (dsJareeCorrectionLog.Tables[0].Rows.Count > 0)
                {


                    OpenConnection();
                    trans = connection.BeginTransaction();
                    foreach (DataRow dr in dsJareeCorrectionLog.Tables[0].Rows)
                    {
                        string IssueID = dr["IssueID"].ToString();
                        string DistrictId = dr["DistrictId"].ToString();
                        string SocietyID = dr["SocietyID"].ToString();
                        string DateOfIssue = dr["DateOfIssue"].ToString();
                        string Old_Bags = dr["Old_Bags"].ToString();
                        string New_Bags = dr["New_Bags"].ToString();
                        string Old_QtyTransffer = dr["Old_QtyTransffer"].ToString();
                        string New_QtyTransffer = dr["New_QtyTransffer"].ToString();
                        string UpdatedDate = getRDate_MDY(dr["UpdatedDate"].ToString());
                        string IsDeleted = dr["IsDeleted"].ToString();
                        string IsUpdated = dr["IsUpdated"].ToString();

                        commandt = connection.CreateCommand();
                        commandt.Transaction = trans;
                        commandt.CommandType = CommandType.Text;
                        commandt.CommandText = "select count(*) from JareeCorrectionLog where DistrictId='" + DistrictId + "'  and SocietyID= '" + SocietyID + "' and IssueID='" + IssueID + "' and Old_Bags='" + Old_Bags + "' and New_Bags='" + New_Bags + "' and Old_QtyTransffer='" + Old_QtyTransffer + "' and New_QtyTransffer='" + New_QtyTransffer + "'  and  IsDeleted='" + IsDeleted + "' and IsUpdated='" + IsUpdated + "'";
                        Int64 res = Convert.ToInt64(commandt.ExecuteScalar());
                        commandt.Dispose();
                        if (res <= 0)
                        {

                            string UserId = dr["UserId"].ToString();
                            string Ip = dr["Ip"].ToString();

                            cmd = connection.CreateCommand();
                            cmd.Transaction = trans;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "in_JareeCorrectionLog";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@IssueID", IssueID);
                            cmd.Parameters.AddWithValue("@DistrictId", DistrictId);
                            cmd.Parameters.AddWithValue("@SocietyID", SocietyID);
                            cmd.Parameters.AddWithValue("@DateOfIssue", DateOfIssue);
                            cmd.Parameters.AddWithValue("@Old_Bags", Old_Bags);
                            cmd.Parameters.AddWithValue("@New_Bags", New_Bags);
                            cmd.Parameters.AddWithValue("@Old_QtyTransffer", Old_QtyTransffer);
                            cmd.Parameters.AddWithValue("@New_QtyTransffer", New_QtyTransffer);
                            cmd.Parameters.AddWithValue("@UpdatedDate", UpdatedDate);
                            cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                            cmd.Parameters.AddWithValue("@IsUpdated", IsUpdated);
                            cmd.Parameters.AddWithValue("@UserId", UserId);
                            cmd.Parameters.AddWithValue("@Ip", Ip);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    trans.Commit();
                    result = true;
                }
            }
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
        return result;
    }

    #endregion




    #region Output Correction Log Information

    [WebMethod]
    public DataSet OpGunnyBagsCorrection(string D, string S)
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
            //Here I use user id as unique id whic is a unique id in offline table when coreection module is created this is because of upade and delete both case handling
            commandt.CommandText = "select distinct GunnyBagsCorrectionLog.UserId from GunnyBagsCorrectionLog  where GunnyBagsCorrectionLog.District_ID='" + D + "' and GunnyBagsCorrectionLog.Society_Id='" + S + "'";
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
    public DataSet OpPrapteeCorrection(string D, string S)
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
            //Here I use user id as unique id whic is a unique id in offline table when coreection module is created this is because of upade and delete both case handling
           // commandt.CommandText = "select distinct PrapteeCorrectionLog.UserId from PrapteeCorrectionLog  where PrapteeCorrectionLog.District_ID='" + D + "' and PrapteeCorrectionLog.Society_Id='" + S + "'";
            commandt.CommandText = "select distinct PrapteeCorrectionLog.UserId from PrapteeCorrectionLog  where PrapteeCorrectionLog.District_ID='" + D + "' and PrapteeCorrectionLog.Society_Id='" + S + "' and PrapteeCorrectionLog.UserId=' '";
            
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
    public DataSet OpJareeCorrection(string D, string S)
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
            //Here I use user id as unique id whic is a unique id in offline table when coreection module is created this is because of upade and delete both case handling
           // commandt.CommandText = "select distinct JareeCorrectionLog.UserId from JareeCorrectionLog  where JareeCorrectionLog.DistrictId='" + D + "' and JareeCorrectionLog.SocietyID='" + S + "'";
            commandt.CommandText = "select distinct JareeCorrectionLog.UserId from JareeCorrectionLog  where JareeCorrectionLog.DistrictId='" + D + "' and JareeCorrectionLog.SocietyID='" + S + "' and JareeCorrectionLog.UserId=' '";

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


public class SecurityCorrection : System.Web.Services.Protocols.SoapHeader
{
    public bool IsActive;
    public String ID;
}



