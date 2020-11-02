using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for Export_Wheat_Reg_2014
/// </summary>
[WebService(Namespace = "http://microsoft.co.in/", Name = "Export_WheatRegistration_2014", Description = "Export Data (upload data on server)/Date:10/12/2013")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Export_Wheat_Reg_2014 : System.Web.Services.WebService
{
    private SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_WPMS2014"].ToString());
    private SqlCommand command, cmd;
    private SqlDataAdapter dataAdapter;
    private DataSet dataset;
    private SqlTransaction trans = null;
    private SqlCommand commandt = null;

    private int count = 0;
    public string LogID = "";

    public Export_Wheat_Reg_2014()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    #region Security Checking Section

    public securityWheat_Reg_export_2013 securityWheat_Reg_export_2013;
    [SoapHeader("securityWheat_Reg_export_2013")]
    [WebMethod(Description = "This Method Is Used For Security Check in")]
    public bool chkSecurityExport(securityWheat_Reg_export_2013 S)
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

    #region Insertion in Offline To Online Database of Basic Soceity Information...

    [WebMethod(Description = "This Method Is Used For Inserting Runner Registration Information ")]
    public bool InRunner(string SocietyId, string RunnerVer, string DistrictId)
    {
        bool result = false;
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = new SqlCommand();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "Select count(*) from RunnerRegistration where  RunnerID='" + SocietyId + "'  ";
            string res = Convert.ToString(commandt.ExecuteScalar());
            commandt.Dispose();

            string status = "";
            if (Convert.ToInt16(res) > 0)
            {
                status = "Reg";
            }
            else
            {
                status = "UnReg";
            }

            string date = getRDate_MDY(System.DateTime.Now.ToShortDateString());
            string LogID = SocietyId.ToString() + date;
            string LogIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            string LogDate = date;
            cmd = connection.CreateCommand();
            cmd.Transaction = trans;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "in_RunnerLog";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@LogID", LogID);
            cmd.Parameters.AddWithValue("@LogIP", LogIP);
            cmd.Parameters.AddWithValue("@LogDate", LogDate);
            cmd.Parameters.AddWithValue("@Pc_Id", SocietyId);
            cmd.Parameters.AddWithValue("@District_Code", DistrictId);
            cmd.Parameters.AddWithValue("@Society_Id", SocietyId);
            cmd.Parameters.AddWithValue("@RunnerID", SocietyId);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@RunnerVer", RunnerVer);
            cmd.Parameters.AddWithValue("@DayCount", "0");
            int req = cmd.ExecuteNonQuery();
            cmd.Dispose();
            if (req > 0)
            {
                trans.Commit();
                result = true;
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

    [WebMethod(Description = "This Method Is Used For Inserting Initial Soceity Information ")]
    public bool InInitial(DataSet dsInitial)
    {
        bool result = false;
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "select count(*) from Initial where Society_Id='" + dsInitial.Tables[0].Rows[0]["Society_Id"].ToString() + "'";
            Int64 res = Convert.ToInt64(commandt.ExecuteScalar());
            commandt.Dispose();
            if (res <= 0)
            {
                string District_ID = dsInitial.Tables[0].Rows[0]["District_ID"].ToString();
                string District_Name = dsInitial.Tables[0].Rows[0]["District_Name"].ToString();
                string Society_Id = dsInitial.Tables[0].Rows[0]["Society_Id"].ToString();
                string SocietyName = dsInitial.Tables[0].Rows[0]["SocietyName"].ToString();

                string PC_Id = dsInitial.Tables[0].Rows[0]["PC_Id"].ToString();
                string PC_NAME = dsInitial.Tables[0].Rows[0]["PC_NAME"].ToString();
                string AgencyId = dsInitial.Tables[0].Rows[0]["AgencyId"].ToString();
                string AgencyName = dsInitial.Tables[0].Rows[0]["AgencyName"].ToString();

                string MarketingSeasonId = dsInitial.Tables[0].Rows[0]["MarketingSeasonId"].ToString();
                string MarketingSeason = dsInitial.Tables[0].Rows[0]["MarketingSeason"].ToString();
                string CropYear = dsInitial.Tables[0].Rows[0]["CropYear"].ToString();
                string OpeningStockOfGunny = dsInitial.Tables[0].Rows[0]["OpeningStockOfGunny"].ToString();

                string Password1 = dsInitial.Tables[0].Rows[0]["Password1"].ToString();
                string BankName = dsInitial.Tables[0].Rows[0]["BankName"].ToString();
                string AccNO = dsInitial.Tables[0].Rows[0]["AccNO"].ToString();
                string BranchName = dsInitial.Tables[0].Rows[0]["BranchName"].ToString();

                string ManagerName = dsInitial.Tables[0].Rows[0]["ManagerName"].ToString();
                string VersionNo = dsInitial.Tables[0].Rows[0]["VersionNo"].ToString();
                string NoOfToulKanta = dsInitial.Tables[0].Rows[0]["NoOfToulKanta"].ToString();
                string DailySc_Capacity = dsInitial.Tables[0].Rows[0]["DailySc_Capacity"].ToString();

                string Societycreditlimit = dsInitial.Tables[0].Rows[0]["Societycreditlimit"].ToString();
                string OneFarmerLimit = dsInitial.Tables[0].Rows[0]["OneFarmerLimit"].ToString();
                string MgrMobileNo = dsInitial.Tables[0].Rows[0]["MgrMobileNo"].ToString();
                string SocBandaranCapacity = dsInitial.Tables[0].Rows[0]["SocBandaranCapacity"].ToString();

                string DateTimeOfInstall = getRDate_MDY(dsInitial.Tables[0].Rows[0]["DateTimeOfInstall"].ToString());
                string Remarks = dsInitial.Tables[0].Rows[0]["Remarks"].ToString();

                cmd = connection.CreateCommand();
                cmd.Transaction = trans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "in_Initial";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@District_ID", District_ID);
                cmd.Parameters.AddWithValue("@District_Name", District_Name);
                cmd.Parameters.AddWithValue("@Society_Id", Society_Id);
                cmd.Parameters.AddWithValue("@SocietyName", SocietyName);
                cmd.Parameters.AddWithValue("@PC_Id", Society_Id);
                cmd.Parameters.AddWithValue("@PC_NAME", PC_NAME);
                cmd.Parameters.AddWithValue("@AgencyId", AgencyId);
                cmd.Parameters.AddWithValue("@AgencyName", AgencyName);
                cmd.Parameters.AddWithValue("@MarketingSeasonId", MarketingSeasonId);
                cmd.Parameters.AddWithValue("@MarketingSeason", MarketingSeason);
                cmd.Parameters.AddWithValue("@CropYear", CropYear);
                if (OpeningStockOfGunny == "")
                {
                    OpeningStockOfGunny = "0";
                }
                cmd.Parameters.AddWithValue("@OpeningStockOfGunny", OpeningStockOfGunny);
                cmd.Parameters.AddWithValue("@Password1", Password1);
                cmd.Parameters.AddWithValue("@BankName", BankName);
                cmd.Parameters.AddWithValue("@AccNO", AccNO);
                cmd.Parameters.AddWithValue("@BranchName", BranchName);
                cmd.Parameters.AddWithValue("@ManagerName", ManagerName);
                cmd.Parameters.AddWithValue("@VersionNo", VersionNo);
                cmd.Parameters.AddWithValue("@NoOfToulKanta", NoOfToulKanta);
                cmd.Parameters.AddWithValue("@DailySc_Capacity", DailySc_Capacity);

                if (Societycreditlimit == "")
                {
                    Societycreditlimit = "0.0";
                }
                cmd.Parameters.AddWithValue("@Societycreditlimit", Societycreditlimit);
                cmd.Parameters.AddWithValue("@OneFarmerLimit", OneFarmerLimit);
                cmd.Parameters.AddWithValue("@SocBandaranCapacity", SocBandaranCapacity);
                cmd.Parameters.AddWithValue("@MgrMobileNo", MgrMobileNo);
                cmd.Parameters.AddWithValue("@DateTimeOfInstall", DateTimeOfInstall);
                int req = cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            else
            {
                string District_ID = dsInitial.Tables[0].Rows[0]["District_ID"].ToString();
                string Society_Id = dsInitial.Tables[0].Rows[0]["Society_Id"].ToString();
                string PC_Id = dsInitial.Tables[0].Rows[0]["PC_Id"].ToString();
                string NoOfToulKanta = dsInitial.Tables[0].Rows[0]["NoOfToulKanta"].ToString();
                string DailySc_Capacity = dsInitial.Tables[0].Rows[0]["DailySc_Capacity"].ToString();
                string OneFarmerLimit = dsInitial.Tables[0].Rows[0]["OneFarmerLimit"].ToString();
                string MgrMobileNo = dsInitial.Tables[0].Rows[0]["MgrMobileNo"].ToString();
                string SocBandaranCapacity = dsInitial.Tables[0].Rows[0]["SocBandaranCapacity"].ToString();
                string BankName = dsInitial.Tables[0].Rows[0]["BankName"].ToString();
                string AccNO = dsInitial.Tables[0].Rows[0]["AccNO"].ToString();
                string BranchName = dsInitial.Tables[0].Rows[0]["BranchName"].ToString();
                string ManagerName = dsInitial.Tables[0].Rows[0]["ManagerName"].ToString();
                string UpdationDate = getRDate_MDY(dsInitial.Tables[0].Rows[0]["UpdationDate"].ToString());
                string VersionNo = dsInitial.Tables[0].Rows[0]["VersionNo"].ToString();
                string IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                commandt = connection.CreateCommand();
                commandt.Transaction = trans;
                commandt.CommandType = CommandType.Text;
                commandt.CommandText = "select count(*) from Initial where Society_Id='" + Society_Id + "' AND District_ID = '" + District_ID + "' AND  PC_Id = '" + PC_Id + "' AND NoOfToulKanta = '" + NoOfToulKanta + "' AND DailySc_Capacity = '" + DailySc_Capacity + "' AND OneFarmerLimit = '" + OneFarmerLimit + "' AND MgrMobileNo = '" + MgrMobileNo + "' AND SocBandaranCapacity = '" + SocBandaranCapacity + "' AND BankName = N'" + BankName + "' AND AccNO = '" + AccNO + "' AND BranchName = N'" + BranchName + "' AND ManagerName = N'" + ManagerName + "' AND VersionNo = '" + VersionNo + "'";
                Int64 res2 = Convert.ToInt64(commandt.ExecuteScalar());
                commandt.Dispose();
                if (res2 <= 0)
                {
                    cmd = new SqlCommand();
                    cmd = connection.CreateCommand();
                    cmd.Transaction = trans;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "In_Initial_Update_Log";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Society_Id", Society_Id);
                    cmd.Parameters.AddWithValue("@District_ID", District_ID);
                    cmd.Parameters.AddWithValue("@PC_Id", PC_Id);
                    cmd.Parameters.AddWithValue("@IP", IP);
                    int req_log = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    if (req_log > 0)
                    {
                        cmd = new SqlCommand();
                        cmd = connection.CreateCommand();
                        cmd.Transaction = trans;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Update_Initial_Info";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@District_ID", District_ID);
                        cmd.Parameters.AddWithValue("@Society_Id", Society_Id);
                        cmd.Parameters.AddWithValue("@PC_Id", Society_Id);
                        cmd.Parameters.AddWithValue("@BankName", BankName);
                        cmd.Parameters.AddWithValue("@AccNO", AccNO);
                        cmd.Parameters.AddWithValue("@BranchName", BranchName);
                        cmd.Parameters.AddWithValue("@ManagerName", ManagerName);
                        cmd.Parameters.AddWithValue("@VersionNo", VersionNo);
                        cmd.Parameters.AddWithValue("@NoOfToulKanta", NoOfToulKanta);
                        cmd.Parameters.AddWithValue("@DailySc_Capacity", DailySc_Capacity);
                        cmd.Parameters.AddWithValue("@OneFarmerLimit", OneFarmerLimit);
                        cmd.Parameters.AddWithValue("@SocBandaranCapacity", SocBandaranCapacity);
                        cmd.Parameters.AddWithValue("@MgrMobileNo", MgrMobileNo);
                        cmd.Parameters.AddWithValue("@UpdationDate", UpdationDate);
                        int req = cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                }
            }

            trans.Commit();
            result = true;
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

    [WebMethod(Description = "This Method Is Used For Inserting Installation Information ")]
    public bool InInstallationInfo(DataSet dsInstallationInfo)
    {
        bool result = false;
        try
        {
            if (dsInstallationInfo != null)
            {
                if (dsInstallationInfo.Tables[0].Rows.Count > 0)
                {
                    OpenConnection();
                    trans = connection.BeginTransaction();
                    foreach (DataRow dr in dsInstallationInfo.Tables[0].Rows)
                    {
                        try
                        {
                            string SocietyID = dr["SocietyID"].ToString();
                            string District_ID = dr["District_ID"].ToString();

                            commandt = connection.CreateCommand();
                            commandt.Transaction = trans;
                            commandt.CommandType = CommandType.Text;
                            commandt.Parameters.Clear();
                            commandt.CommandText = "select count(*) from InstallationInfo where SocietyID='" + SocietyID + "' and District_ID='" + District_ID + "'";
                            Int64 res = Convert.ToInt64(commandt.ExecuteScalar());
                            commandt.Dispose();
                            if (res <= 0)
                            {
                                string UserID = dr["UserID"].ToString();
                                string InsDate = getRDate_MDY(dr["InsDate"].ToString());
                                cmd = connection.CreateCommand();
                                cmd.Transaction = trans;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "in_InstallationInfo";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@District_ID", District_ID);
                                cmd.Parameters.AddWithValue("@SocietyID", SocietyID);
                                cmd.Parameters.AddWithValue("@UserID", UserID);
                                cmd.Parameters.AddWithValue("@InsDate", InsDate);
                                int req = cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                if (req > 0)
                                {
                                    result = true;
                                }
                            }
                        }
                        catch (Exception)
                        {
                            ///////////////////
                        }
                    }
                    trans.Commit();
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

    [WebMethod(Description = "This Method Is Used For Inserting Operator Information ")]
    public bool InOperator(DataSet dsOperator)
    {
        bool result = false;
        try
        {
            if (dsOperator != null)
            {
                if (dsOperator.Tables[0].Rows.Count > 0)
                {
                    OpenConnection();
                    trans = connection.BeginTransaction();
                    foreach (DataRow dr in dsOperator.Tables[0].Rows)
                    {
                        try
                        {
                            string Op_id = dr["Op_id"].ToString();
                            string PCID = dr["PCID"].ToString();
                            string SocietyID = dr["SocietyID"].ToString();
                            string DistrictId = dr["DistrictId"].ToString();

                            commandt = connection.CreateCommand();
                            commandt.Transaction = trans;
                            commandt.CommandType = CommandType.Text;
                            commandt.CommandText = "select count(*) from OperatorRegistration where Op_id='" + Op_id + "'  and PCID= '" + PCID + "' and SocietyID='" + SocietyID + "'";
                            Int64 res = Convert.ToInt64(commandt.ExecuteScalar());
                            commandt.Dispose();
                            if (res <= 0)
                            {
                                string Proc_AgID = dr["Proc_AgID"].ToString();
                                string Name = dr["Name"].ToString();
                                string MobileNo = dr["MobileNo"].ToString();
                                string Email = dr["Email"].ToString();
                                string Address = dr["Address"].ToString();
                                string Password1 = dr["Password1"].ToString();
                                string MasterPassword = dr["MasterPassword"].ToString();

                                cmd = connection.CreateCommand();
                                cmd.Transaction = trans;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "in_operator";
                                cmd.Parameters.Clear();

                                cmd.Parameters.AddWithValue("@Op_id", Op_id);
                                cmd.Parameters.AddWithValue("@DistrictId", DistrictId);
                                cmd.Parameters.AddWithValue("@Proc_AgID", Proc_AgID);
                                cmd.Parameters.AddWithValue("@PCID", PCID);
                                cmd.Parameters.AddWithValue("@SocietyID", SocietyID);
                                cmd.Parameters.AddWithValue("@Name", Name);
                                cmd.Parameters.AddWithValue("@MobileNo", MobileNo);
                                cmd.Parameters.AddWithValue("@Email", Email);
                                cmd.Parameters.AddWithValue("@Address", Address);
                                cmd.Parameters.AddWithValue("@Password1", Password1);
                                cmd.Parameters.AddWithValue("@MasterPassword", MasterPassword);
                                int req = cmd.ExecuteNonQuery();
                                cmd.Dispose();
                            }
                            else
                            {
                                string Proc_AgID = dr["Proc_AgID"].ToString();
                                string Name = dr["Name"].ToString();
                                string MobileNo = dr["MobileNo"].ToString();
                                string Email = dr["Email"].ToString();
                                string Address = dr["Address"].ToString();
                                string Password1 = dr["Password1"].ToString();
                                string MasterPassword = dr["MasterPassword"].ToString();

                                commandt = connection.CreateCommand();
                                commandt.Transaction = trans;
                                commandt.CommandType = CommandType.Text;
                                commandt.CommandText = "select count(*) from OperatorRegistration where Op_id='" + Op_id + "'  and PCID= '" + PCID + "' and SocietyID='" + SocietyID + "' and DistrictId = '" + DistrictId + "' and Name =N'" + Name + "' and MobileNo ='" + MobileNo + "'and Email ='" + Email + "'and Address =N'" + Address + "'";
                                Int64 res_update = Convert.ToInt64(commandt.ExecuteScalar());
                                commandt.Dispose();
                                if (res_update <= 0)
                                {
                                    cmd = connection.CreateCommand();
                                    cmd.Transaction = trans;
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "In_Operator_Update_Log";
                                    cmd.Parameters.Clear();

                                    cmd.Parameters.AddWithValue("@Op_id", Op_id);
                                    cmd.Parameters.AddWithValue("@DistrictId", DistrictId);

                                    cmd.Parameters.AddWithValue("@PCID", PCID);
                                    cmd.Parameters.AddWithValue("@SocietyID", SocietyID);
                                    int req_log = cmd.ExecuteNonQuery();
                                    cmd.Dispose();
                                    if (req_log > 0)
                                    {
                                        cmd = connection.CreateCommand();
                                        cmd.Transaction = trans;
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.CommandText = "in_Operator_Update_Info";
                                        cmd.Parameters.Clear();

                                        cmd.Parameters.AddWithValue("@Op_id", Op_id);
                                        cmd.Parameters.AddWithValue("@DistrictId", DistrictId);

                                        cmd.Parameters.AddWithValue("@PCID", PCID);
                                        cmd.Parameters.AddWithValue("@SocietyID", SocietyID);
                                        cmd.Parameters.AddWithValue("@Name", Name);
                                        cmd.Parameters.AddWithValue("@MobileNo", MobileNo);
                                        cmd.Parameters.AddWithValue("@Email", Email);
                                        cmd.Parameters.AddWithValue("@Address", Address);

                                        int req = cmd.ExecuteNonQuery();
                                        cmd.Dispose();
                                    }
                                }
                            }
                        }
                        catch
                        {
                            ////////////////
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

    [WebMethod(Description = "This Method Is Used For Finalize Database Information ")]
    public bool InFinalizeData(DataSet dsFinalizeData)
    {
        bool result = false;
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            foreach (DataRow dr in dsFinalizeData.Tables[0].Rows)
            {
                try
                {
                    string District_ID = dr["District_ID"].ToString();
                    string SocietyID = dr["SocietyID"].ToString();
                    commandt = connection.CreateCommand();
                    commandt.Transaction = trans;
                    commandt.CommandType = CommandType.Text;
                    commandt.CommandText = "select count(*) from FinalizeDataBase where District_ID='" + District_ID + "'  and SocietyID= '" + SocietyID + "' ";
                    Int64 res = Convert.ToInt64(commandt.ExecuteScalar());
                    commandt.Dispose();
                    string FinalizeDate = "";
                    if (res <= 0)
                    {
                        string FinalizeStatus = dr["FinalizeStatus"].ToString();
                        string CreatedDate = getRDate_MDY(dr["CreatedDate"].ToString());
                        cmd = connection.CreateCommand();
                        cmd.Transaction = trans;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "in_FinalizeDatabase";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@District_ID", District_ID);
                        cmd.Parameters.AddWithValue("@SocietyID", SocietyID);
                        cmd.Parameters.AddWithValue("@FinalizeStatus", FinalizeStatus);
                        cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                        int rev = cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        if (dr["FinalizeDate"].ToString() != "")
                        {
                            if (dr["FinalizeDate"].ToString() != "")
                            {
                                FinalizeDate = getRDate_MDY(dr["FinalizeDate"].ToString());
                            }
                            else
                            {
                                FinalizeDate = "01/01/1900";
                            }
                            string FinalizeStatus = dr["FinalizeStatus"].ToString();
                            cmd = connection.CreateCommand();
                            cmd.Transaction = trans;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "update FinalizeDataBase set FinalizeStatus='Y',FinalizeDate='" + FinalizeDate + "',IsActive='N' where District_ID='" + District_ID + "' and SocietyID='" + SocietyID + "' ";
                            cmd.ExecuteNonQuery();
                        }
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

    public string mmddyyyy(String dateValue)
    {
        try
        {
            String[] ArryDate;
            String changeDate;
            if (dateValue != "")
            {
                ArryDate = dateValue.Split('/');
                changeDate = ArryDate[1] + "/" + ArryDate[0] + "/" + ArryDate[2];
                return changeDate;
            }
            else
                return "1/1/1900";
        }
        catch (Exception ex)
        {
            throw ex;
        }
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
public class securityWheat_Reg_export_2013 : System.Web.Services.Protocols.SoapHeader
{
    public bool IsActive;
    public String Password;
    public String UserName;
    public String RunnerId;
    public String RunnerPassword;
}

