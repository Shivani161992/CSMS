using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


/// <summary>
/// Summary description for Export_Paddy_Proc_2013
/// </summary>
[WebService(Namespace = "http://microsoft.co.in/", Name = "ExportPaddyProcurement2013", Description = "Export Data (upload data on server)/Date: 21/10/2013")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Export_Paddy_Proc_2013 : System.Web.Services.WebService 
{
    private SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_PPMS2014"].ToString());
    private SqlCommand command, cmd;
    private SqlDataAdapter dataAdapter;
    private DataSet dataset;
    private SqlTransaction trans = null;
    private SqlCommand commandt = null;

    private int count = 0;
    public string LogID = "";

    public Export_Paddy_Proc_2013 () 
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    #region Security Checking Section

    public securityPaddyexport_Proc_2013 securityPaddyexport_Proc_2013;
    [SoapHeader("securityPaddyexport_Proc_2013")]
    [WebMethod(Description = "This Method Is Used For Security Check in")]
    public bool chkSecurityExport(securityPaddyexport_Proc_2013 S)
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
            string status = "UnReg";
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
                string UpdationDate = getDate_MDY(dsInitial.Tables[0].Rows[0]["UpdationDate"].ToString());
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

    #region Kharidi Paddy Procurement 2013-14 Insertion in Offline To Online Database

    [WebMethod(Description = "This Method Is Used For Farmer SMS Information ")]
    public bool InSMS(DataSet dsInSMS)
    {
        bool result = false;
        try
        {
            if (dsInSMS != null)
            {
                if (dsInSMS.Tables[0].Rows.Count > 0)
                {
                    OpenConnection();
                    trans = connection.BeginTransaction();
                    foreach (DataRow dr in dsInSMS.Tables[0].Rows)
                    {
                        try
                        {
                            string SMS_ID = dr["SMS_ID"].ToString();
                            string District_Code = dr["District_Code"].ToString();
                            string PC_ID = dr["Society_ID"].ToString();
                            string Farmer_Id = dr["Farmer_Id"].ToString();
                            commandt = connection.CreateCommand();
                            commandt.Transaction = trans;
                            commandt.CommandType = CommandType.Text;
                            commandt.CommandText = "select count(*) from FarmerSMS where  District_Code= '" + District_Code + "' and PC_ID='" + PC_ID + "' and  Farmer_Id='" + Farmer_Id + "' and  SMS_ID='" + SMS_ID + "' ";
                            Int64 res = Convert.ToInt64(commandt.ExecuteScalar());
                            commandt.Dispose();
                            if (res <= 0)
                            {
                                string Society_ID = dr["Society_ID"].ToString();
                                string crpcode = dr["crpcode"].ToString();
                                string DateOfCollingFarmer = getRDate_MDY(dr["DateOfCollingFarmer"].ToString());
                                string MobileNo = dr["MobileNo"].ToString();
                                string DateOfCreation = getRDate_MDY(dr["DateOfCreation"].ToString());
                                string CropExpectedDate = getRDate_MDY(dr["CropExpectedDate"].ToString());
                                
                                double OneFarmerLimit1 = Convert.ToDouble(dr["OneFarmerLimit"].ToString());
                                string OneFarmerLimit = Convert.ToString(Math.Round(OneFarmerLimit1, 2));
                                string NextCropExpectedDate = getRDate_MDY(dr["NextCropExpectedDate"].ToString());
                                
                                double RemainingCrop1 = Convert.ToDouble(dr["RemainingCrop"].ToString());
                                string RemainingCrop = Convert.ToString(Math.Round(RemainingCrop1, 2));
                                string SocietyLimit = dr["SocietyLimit"].ToString();
                                string Userid = System.Environment.MachineName;
                                string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();

                                cmd = connection.CreateCommand();
                                cmd.Transaction = trans;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "in_FarmerSMS";
                                cmd.Parameters.Clear();

                                cmd.Parameters.AddWithValue("@SMS_ID", SMS_ID);
                                cmd.Parameters.AddWithValue("@District_Code", District_Code);
                                cmd.Parameters.AddWithValue("@PC_ID", Society_ID);
                                cmd.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                                cmd.Parameters.AddWithValue("@Society_ID", Society_ID);
                                cmd.Parameters.AddWithValue("@crpcode", crpcode);

                                cmd.Parameters.AddWithValue("@DateOfCollingFarmer", DateOfCollingFarmer);
                                cmd.Parameters.AddWithValue("@MobileNo", MobileNo);
                                cmd.Parameters.AddWithValue("@DateOfCreation", DateOfCreation);
                                cmd.Parameters.AddWithValue("@CropExpectedDate", CropExpectedDate);
                                cmd.Parameters.AddWithValue("@OneFarmerLimit", OneFarmerLimit);
                                cmd.Parameters.AddWithValue("@NextCropExpectedDate", NextCropExpectedDate);
                                cmd.Parameters.AddWithValue("@RemainingCrop", RemainingCrop);
                                cmd.Parameters.AddWithValue("@SocietyLimit", SocietyLimit);
                                cmd.Parameters.AddWithValue("@Userid", Userid);
                                cmd.Parameters.AddWithValue("@ip", ip);
                                int rev = cmd.ExecuteNonQuery();
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

    [WebMethod(Description = "This Method Is Used For Commodity Receipt Information ")]
    public bool InCommodityReceipt(DataSet dsCommodityReceipt)
    {
        bool result = false;
        try
        {
            if (dsCommodityReceipt != null)
            {
                if (dsCommodityReceipt.Tables[0].Rows.Count > 0)
                {
                    OpenConnection();
                    trans = connection.BeginTransaction();

                    foreach (DataRow dr in dsCommodityReceipt.Tables[0].Rows)
                    {
                        try
                        {
                            string ReceivedID = dr["ReceivedID"].ToString();
                            string District_Id = dr["District_Id"].ToString();
                            string Farmer_Id = dr["Farmer_Id"].ToString();
                            string Society_Id = dr["Society_Id"].ToString();

                            if (District_Id == "2338")
                            {
                                commandt = connection.CreateCommand();
                                commandt.Transaction = trans;
                                commandt.CommandType = CommandType.Text;
                                commandt.CommandText = "select count(*) from CommodityReceivedFromFarmer where District_Id='" + District_Id + "'  and  Society_Id='" + Society_Id + "' and Farmer_Id= '" + Farmer_Id + "' and ReceivedID='" + ReceivedID + "' ";
                                Int64 res = Convert.ToInt64(commandt.ExecuteScalar());
                                commandt.Dispose();
                                if (res <= 0)
                                {
                                    string Proc_AgID = dr["Proc_AgID"].ToString();
                                    string CropYear = dr["CropYear"].ToString();
                                    string MarketingSeasonId = dr["MarketingSeasonId"].ToString();
                                    string TotaAmountPayableToFarmer = dr["TotaAmountPayableToFarmer"].ToString();
                                    string TaulPatrakNo = dr["TaulPatrakNo"].ToString();
                                    string FarmerLoanFromSc = dr["FarmerLoanFromSc"].ToString();
                                    string FarmerLoanFromBank = dr["FarmerLoanFromBank"].ToString();
                                    string Irrigation_Loan = dr["Irrigation_Loan"].ToString();
                                    string AmtAgainstSCCredit = dr["AmtAgainstSCCredit"].ToString();
                                    string AmtAgainstBankCredit = dr["AmtAgainstBankCredit"].ToString();
                                    string AmtAgIrg_Loan = dr["AmtAgIrg_Loan"].ToString();
                                    string NetAmountPayableToFarmer = dr["NetAmountPayableToFarmer"].ToString();
                                    string Date_Of_Receipt = getRDate_MDY(dr["Date_Of_Receipt"].ToString());
                                    DateTime Date_Of_Creation = Convert.ToDateTime(dr["Date_Of_Creation"].ToString());
                                    string Status = dr["Status"].ToString();
                                    string UserId = dr["UserId"].ToString();
                                  
                                    cmd = connection.CreateCommand();
                                    cmd.Transaction = trans;
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "Proc_InsertCommodityReceivedFromFarmer";
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@ReceivedID", ReceivedID);
                                    cmd.Parameters.AddWithValue("@District_Id", District_Id);
                                    cmd.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                                    cmd.Parameters.AddWithValue("@Society_Id", Society_Id);

                                    cmd.Parameters.AddWithValue("@Proc_AgID", Proc_AgID);
                                    cmd.Parameters.AddWithValue("@CropYear", CropYear);
                                    cmd.Parameters.AddWithValue("@MarketingSeasonId", MarketingSeasonId);
                                    cmd.Parameters.AddWithValue("@TotaAmountPayableToFarmer", TotaAmountPayableToFarmer);
                                    cmd.Parameters.AddWithValue("@TaulPatrakNo", TaulPatrakNo);

                                    cmd.Parameters.AddWithValue("@FarmerLoanFromSc", FarmerLoanFromSc);
                                    cmd.Parameters.AddWithValue("@FarmerLoanFromBank", FarmerLoanFromBank);
                                    cmd.Parameters.AddWithValue("@Irrigation_Loan", Irrigation_Loan);
                                    cmd.Parameters.AddWithValue("@AmtAgainstSCCredit", AmtAgainstSCCredit);

                                    cmd.Parameters.AddWithValue("@AmtAgainstBankCredit", AmtAgainstBankCredit);
                                    cmd.Parameters.AddWithValue("@AmtAgIrg_Loan", AmtAgIrg_Loan);
                                    cmd.Parameters.AddWithValue("@NetAmountPayableToFarmer", NetAmountPayableToFarmer);
                                    cmd.Parameters.AddWithValue("@Date_Of_Receipt", Date_Of_Receipt);

                                    cmd.Parameters.AddWithValue("@Date_Of_Creation", Date_Of_Creation);
                                    cmd.Parameters.AddWithValue("@Status", Status);
                                    cmd.Parameters.AddWithValue("@UserId", UserId);
                                   
                                    int x = cmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                commandt = connection.CreateCommand();
                                commandt.Transaction = trans;
                                commandt.CommandType = CommandType.Text;
                                commandt.CommandText = "select count(*) from CommodityReceivedFromFarmer_After28jan where District_Id='" + District_Id + "'  and  Society_Id='" + Society_Id + "' and Farmer_Id= '" + Farmer_Id + "' and ReceivedID='" + ReceivedID + "' ";
                                Int64 res = Convert.ToInt64(commandt.ExecuteScalar());
                                commandt.Dispose();
                                if (res <= 0)
                                {
                                    string Proc_AgID = dr["Proc_AgID"].ToString();
                                    string CropYear = dr["CropYear"].ToString();
                                    string MarketingSeasonId = dr["MarketingSeasonId"].ToString();
                                    string TotaAmountPayableToFarmer = dr["TotaAmountPayableToFarmer"].ToString();
                                    string TaulPatrakNo = dr["TaulPatrakNo"].ToString();
                                    string FarmerLoanFromSc = dr["FarmerLoanFromSc"].ToString();
                                    string FarmerLoanFromBank = dr["FarmerLoanFromBank"].ToString();
                                    string Irrigation_Loan = dr["Irrigation_Loan"].ToString();
                                    string AmtAgainstSCCredit = dr["AmtAgainstSCCredit"].ToString();
                                    string AmtAgainstBankCredit = dr["AmtAgainstBankCredit"].ToString();
                                    string AmtAgIrg_Loan = dr["AmtAgIrg_Loan"].ToString();
                                    string NetAmountPayableToFarmer = dr["NetAmountPayableToFarmer"].ToString();
                                    string Date_Of_Receipt = getRDate_MDY(dr["Date_Of_Receipt"].ToString());
                                    DateTime Date_Of_Creation = Convert.ToDateTime(dr["Date_Of_Creation"].ToString());
                                    string Status = dr["Status"].ToString();
                                    string UserId = dr["UserId"].ToString();
                                    string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                                   
                                    cmd = connection.CreateCommand();
                                    cmd.Transaction = trans;
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "Proc_InsertCommodityReceivedFromFarmer_After28jan";
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@ReceivedID", ReceivedID);
                                    cmd.Parameters.AddWithValue("@District_Id", District_Id);
                                    cmd.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                                    cmd.Parameters.AddWithValue("@Society_Id", Society_Id);

                                    cmd.Parameters.AddWithValue("@Proc_AgID", Proc_AgID);
                                    cmd.Parameters.AddWithValue("@CropYear", CropYear);
                                    cmd.Parameters.AddWithValue("@MarketingSeasonId", MarketingSeasonId);
                                    cmd.Parameters.AddWithValue("@TotaAmountPayableToFarmer", TotaAmountPayableToFarmer);
                                    cmd.Parameters.AddWithValue("@TaulPatrakNo", TaulPatrakNo);

                                    cmd.Parameters.AddWithValue("@FarmerLoanFromSc", FarmerLoanFromSc);
                                    cmd.Parameters.AddWithValue("@FarmerLoanFromBank", FarmerLoanFromBank);
                                    cmd.Parameters.AddWithValue("@Irrigation_Loan", Irrigation_Loan);
                                    cmd.Parameters.AddWithValue("@AmtAgainstSCCredit", AmtAgainstSCCredit);

                                    cmd.Parameters.AddWithValue("@AmtAgainstBankCredit", AmtAgainstBankCredit);
                                    cmd.Parameters.AddWithValue("@AmtAgIrg_Loan", AmtAgIrg_Loan);
                                    cmd.Parameters.AddWithValue("@NetAmountPayableToFarmer", NetAmountPayableToFarmer);
                                    cmd.Parameters.AddWithValue("@Date_Of_Receipt", Date_Of_Receipt);

                                    cmd.Parameters.AddWithValue("@Date_Of_Creation", Date_Of_Creation);
                                    cmd.Parameters.AddWithValue("@Status", Status);
                                    cmd.Parameters.AddWithValue("@UserId", UserId);
                                    cmd.Parameters.AddWithValue("@ip", ip);
                                    int x = cmd.ExecuteNonQuery();
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

    [WebMethod(Description = "This Method Is Used For Commodity Transaction Information ")]
    public bool InCommityTrans(DataSet dsCommodityTrans)
    {
        bool result = false;
        try
        {
            if (dsCommodityTrans != null)
            {
                if (dsCommodityTrans.Tables[0].Rows.Count > 0)
                {
                    OpenConnection();
                    trans = connection.BeginTransaction();
                    foreach (DataRow dr in dsCommodityTrans.Tables[0].Rows)
                    {
                        try
                        {
                            string ComTransID = dr["ComTransID"].ToString();
                            string ReceivedID = dr["ReceivedID"].ToString();
                            string Farmer_Id = dr["Farmer_Id"].ToString();
                            string District_Id = dr["District_Id"].ToString();
                            string Society_Id = dr["Society_Id"].ToString();

                            if (District_Id == "2338")
                            {
                                commandt = connection.CreateCommand();
                                commandt.Transaction = trans;
                                commandt.CommandType = CommandType.Text;
                                commandt.CommandText = "select count(*) from CommodityReceived_Transaction where District_Id='" + District_Id + "'  and Society_Id= '" + Society_Id + "' and Farmer_Id='" + Farmer_Id + "' and ReceivedID='" + ReceivedID + "' and ComTransID='" + ComTransID + "'";
                                Int64 res = Convert.ToInt64(commandt.ExecuteScalar());
                                commandt.Dispose();
                                if (res <= 0)
                                {
                                    string Proc_AgID = dr["Proc_AgID"].ToString();
                                    string CropYear = dr["CropYear"].ToString();
                                    string MarketingSeasonId = dr["MarketingSeasonId"].ToString();
                                    string CommodityId = dr["CommodityId"].ToString();
                                    string QtyReceived = dr["QtyReceived"].ToString();
                                    string Bags = dr["Bags"].ToString();
                                    string TotalAmount = dr["TotalAmount"].ToString();
                                    string CreatedDate = getRDate_MDY(dr["CreatedDate"].ToString());
                                    string Date_Of_Receipt = getRDate_MDY(dr["Date_Of_Receipt"].ToString());

                                    cmd = connection.CreateCommand();
                                    cmd.Transaction = trans;
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "in_CommodtiyTrans";
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@ComTransID", ComTransID);
                                    cmd.Parameters.AddWithValue("@ReceivedID", ReceivedID);
                                    cmd.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                                    cmd.Parameters.AddWithValue("@District_Id", District_Id);
                                    cmd.Parameters.AddWithValue("@Society_Id", Society_Id);
                                    cmd.Parameters.AddWithValue("@Proc_AgID", Proc_AgID);
                                    cmd.Parameters.AddWithValue("@CropYear", CropYear);
                                    cmd.Parameters.AddWithValue("@MarketingSeasonId", MarketingSeasonId);
                                    cmd.Parameters.AddWithValue("@CommodityId", CommodityId);
                                    cmd.Parameters.AddWithValue("@QtyReceived", QtyReceived);
                                    cmd.Parameters.AddWithValue("@Bags", Bags);
                                    cmd.Parameters.AddWithValue("@TotalAmount", TotalAmount);
                                    cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                                    cmd.Parameters.AddWithValue("@Date_Of_Receipt", Date_Of_Receipt);
                                    int rev = cmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                commandt = connection.CreateCommand();
                                commandt.Transaction = trans;
                                commandt.CommandType = CommandType.Text;
                                commandt.CommandText = "select count(*) from CommodityReceived_Transaction_After28jan where District_Id='" + District_Id + "'  and Society_Id= '" + Society_Id + "' and Farmer_Id='" + Farmer_Id + "' and ReceivedID='" + ReceivedID + "' and ComTransID='" + ComTransID + "'";
                                Int64 res = Convert.ToInt64(commandt.ExecuteScalar());
                                commandt.Dispose();
                                if (res <= 0)
                                {
                                    string Proc_AgID = dr["Proc_AgID"].ToString();
                                    string CropYear = dr["CropYear"].ToString();
                                    string MarketingSeasonId = dr["MarketingSeasonId"].ToString();
                                    string CommodityId = dr["CommodityId"].ToString();
                                    string QtyReceived = dr["QtyReceived"].ToString();
                                    string Bags = dr["Bags"].ToString();
                                    string TotalAmount = dr["TotalAmount"].ToString();
                                    string CreatedDate = getRDate_MDY(dr["CreatedDate"].ToString());
                                    string Date_Of_Receipt = getRDate_MDY(dr["Date_Of_Receipt"].ToString());

                                    cmd = connection.CreateCommand();
                                    cmd.Transaction = trans;
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "in_CommodtiyTrans_After28jan";
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@ComTransID", ComTransID);
                                    cmd.Parameters.AddWithValue("@ReceivedID", ReceivedID);
                                    cmd.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                                    cmd.Parameters.AddWithValue("@District_Id", District_Id);
                                    cmd.Parameters.AddWithValue("@Society_Id", Society_Id);
                                    cmd.Parameters.AddWithValue("@Proc_AgID", Proc_AgID);
                                    cmd.Parameters.AddWithValue("@CropYear", CropYear);
                                    cmd.Parameters.AddWithValue("@MarketingSeasonId", MarketingSeasonId);
                                    cmd.Parameters.AddWithValue("@CommodityId", CommodityId);
                                    cmd.Parameters.AddWithValue("@QtyReceived", QtyReceived);
                                    cmd.Parameters.AddWithValue("@Bags", Bags);
                                    cmd.Parameters.AddWithValue("@TotalAmount", TotalAmount);
                                    cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                                    cmd.Parameters.AddWithValue("@Date_Of_Receipt", Date_Of_Receipt);
                                    int rev = cmd.ExecuteNonQuery();
                                }
                            }
                 
                        }
                        catch
                        {
                            //////////////////
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

    [WebMethod(Description = "This Method Is Used For Transport Information ")]
    public bool InTransport(DataSet dsTransport)
    {
        bool result = false;
        try
        {
            if (dsTransport != null)
            {
                if (dsTransport.Tables[0].Rows.Count > 0)
                {
                    OpenConnection();
                    trans = connection.BeginTransaction();
                    foreach (DataRow dr in dsTransport.Tables[0].Rows)
                    {
                        try
                        {
                            string Transporter_ID = dr["Transporter_ID"].ToString();
                            string District_ID = dr["District_ID"].ToString();
                            string SocietyCode = dr["SocietyCode"].ToString();
                            commandt = connection.CreateCommand();
                            commandt.Transaction = trans;
                            commandt.CommandType = CommandType.Text;
                            commandt.CommandText = "select count(*) from TransportMaster where District_ID='" + District_ID + "'  and SocietyCode= '" + SocietyCode + "' and Transporter_ID='" + Transporter_ID + "'";
                            Int64 res = Convert.ToInt64(commandt.ExecuteScalar());
                            commandt.Dispose();
                            if (res <= 0)
                            {
                                string Transporter_Name = dr["Transporter_Name"].ToString();
                                string NoOfTrucs = dr["NoOfTrucs"].ToString();
                                string Address = dr["Address"].ToString();
                                string MobileNo = dr["MobileNo"].ToString();
                                string User_ID = dr["User_ID"].ToString();
                                string DateTimeStamp = getRDate_MDY(dr["DateTimeStamp"].ToString());
                                string Opration = dr["Opration"].ToString();
                                string Locked = dr["Locked"].ToString();
                                string IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();

                                cmd = connection.CreateCommand();
                                cmd.Transaction = trans;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "Proc_InsertTransportMaster";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@Transporter_ID", Transporter_ID);
                                cmd.Parameters.AddWithValue("@District_ID", District_ID);
                                cmd.Parameters.AddWithValue("@SocietyCode", SocietyCode);
                                cmd.Parameters.AddWithValue("@Transporter_Name", Transporter_Name);
                                cmd.Parameters.AddWithValue("@NoOfTrucs", NoOfTrucs);
                                cmd.Parameters.AddWithValue("@Address", Address);
                                cmd.Parameters.AddWithValue("@MobileNo", MobileNo);
                                cmd.Parameters.AddWithValue("@User_ID", User_ID);
                                cmd.Parameters.AddWithValue("@DateTimeStamp", DateTimeStamp);
                                cmd.Parameters.AddWithValue("@Opration", Opration);
                                cmd.Parameters.AddWithValue("@Locked", Locked);
                                cmd.Parameters.AddWithValue("@IP", IP);
                                int rev = cmd.ExecuteNonQuery();
                                cmd.Dispose();
                            }
                            else
                            {
                                string Transporter_Name = dr["Transporter_Name"].ToString();
                                string NoOfTrucs = dr["NoOfTrucs"].ToString();
                                string Address = dr["Address"].ToString();
                                string MobileNo = dr["MobileNo"].ToString();
                                commandt = connection.CreateCommand();
                                commandt.Transaction = trans;
                                commandt.CommandType = CommandType.Text;
                                commandt.CommandText = "select count(*) from TransportMaster where District_ID='" + District_ID + "'  and SocietyCode= '" + SocietyCode + "' and Transporter_ID='" + Transporter_ID + "' and Transporter_Name= N'" + Transporter_Name + "' and NoOfTrucs='" + NoOfTrucs + "' and Address= N'" + Address + "' and MobileNo='" + MobileNo + "'";
                                Int64 res2 = Convert.ToInt64(commandt.ExecuteScalar());
                                commandt.Dispose();
                                if (res2 <= 0)
                                {
                                    cmd = new SqlCommand();
                                    cmd = connection.CreateCommand();
                                    cmd.Transaction = trans;
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "In_Transporter_Update_Info";
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@Transporter_ID", Transporter_ID);
                                    cmd.Parameters.AddWithValue("@District_ID", District_ID);
                                    cmd.Parameters.AddWithValue("@SocietyCode", SocietyCode);
                                    cmd.Parameters.AddWithValue("@Transporter_Name", Transporter_Name);
                                    cmd.Parameters.AddWithValue("@NoOfTrucs", NoOfTrucs);
                                    cmd.Parameters.AddWithValue("@Address", Address);
                                    cmd.Parameters.AddWithValue("@MobileNo", MobileNo);
                                    int rev = cmd.ExecuteNonQuery();
                                    cmd.Dispose();
                                }
                            }
                        }
                        catch
                        {
                            ////////////
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

    [WebMethod(Description = "This Method Is Used For Issue to Sangrah Information ")]
    public bool InIssueToSangrhan(DataSet dsIssueToSangrhan)
    {
        bool result = false;
        try
        {
            if (dsIssueToSangrhan != null)
            {
                if (dsIssueToSangrhan.Tables[0].Rows.Count > 0)
                {
                    OpenConnection();
                    trans = connection.BeginTransaction();
                    foreach (DataRow dr in dsIssueToSangrhan.Tables[0].Rows)
                    {
                        string TransID = dr["TransID"].ToString();
                        string IssueID = dr["IssueID"].ToString();
                        string DistrictId = dr["DistrictId"].ToString();
                        string SocietyID = dr["SocietyID"].ToString();
                        commandt = connection.CreateCommand();
                        commandt.Transaction = trans;
                        commandt.CommandType = CommandType.Text;
                        commandt.CommandText = "select count(*) from IssueToSangrahanaKendra where DistrictId='" + DistrictId + "'  and SocietyID= '" + SocietyID + "' and IssueID='" + IssueID + "' and TransID='" + TransID + "'";
                        Int64 res = Convert.ToInt64(commandt.ExecuteScalar());
                        commandt.Dispose();
                        if (res <= 0)
                        {
                            string Proc_AgID = dr["Proc_AgID"].ToString();
                            string PCID = dr["SocietyID"].ToString();
                            string CropYear = dr["CropYear"].ToString();
                            string MarketingSeasonId = dr["MarketingSeasonId"].ToString();
                            string DateOfIssue = getRDate_MDY(dr["DateOfIssue"].ToString());
                            string IssueTo = dr["IssueTo"].ToString();
                            string SendingDistId = dr["SendingDistId"].ToString();
                            string GodownTypeId = dr["GodownTypeId"].ToString();
                            string GodownCenterId = dr["GodownCenterId"].ToString();
                            string GodownName = dr["GodownName"].ToString();
                            string GodownNumber = dr["GodownNumber"].ToString();
                            string Place = dr["Place"].ToString();
                            string RailRackOf_ID = dr["RailRackOf_ID"].ToString();
                            string RailRack_SendingPlace = dr["RailRack_SendingPlace"].ToString();
                            string RailRack_RecievingPlace = dr["RailRack_RecievingPlace"].ToString();
                            string Miller_ID = dr["Miller_ID"].ToString();
                            string MillerRepresentative = dr["MillerRepresentative"].ToString();
                            string CommodityId = dr["CommodityId"].ToString();
                            string Bags = dr["Bags"].ToString();
                            string QtyTransffer = dr["QtyTransffer"].ToString();
                            string TaulPtrakNo = dr["TaulPtrakNo"].ToString();
                            string TransporterId = dr["TransporterId"].ToString();
                            string TruckChalanNo = dr["TruckChalanNo"].ToString();
                            string TruckNo = dr["TruckNo"].ToString();
                            string DriverName = dr["DriverName"].ToString();
                            string CreatedDate = getRDate_MDY(dr["CreatedDate"].ToString());
                            string CreatedBy = dr["CreatedBy"].ToString();
                            string UpdatedBy = dr["UpdatedBy"].ToString();
                            string UpdatedDate = getRDate_MDY(dr["UpdatedDate"].ToString());
                            string UserId = dr["UserId"].ToString();
                            string Ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();

                            cmd = connection.CreateCommand();
                            cmd.Transaction = trans;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "Proc_IssueToSangrahanaKendra";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@TransID", TransID);
                            cmd.Parameters.AddWithValue("@IssueID", IssueID);
                            cmd.Parameters.AddWithValue("@DistrictId", DistrictId);
                            cmd.Parameters.AddWithValue("@Proc_AgID", Proc_AgID);
                            cmd.Parameters.AddWithValue("@SocietyID", SocietyID);
                            cmd.Parameters.AddWithValue("@PCID", SocietyID);

                            cmd.Parameters.AddWithValue("@CropYear", CropYear);
                            cmd.Parameters.AddWithValue("@MarketingSeasonId", MarketingSeasonId);
                            cmd.Parameters.AddWithValue("@DateOfIssue", DateOfIssue);
                            cmd.Parameters.AddWithValue("@IssueTo", IssueTo);
                            cmd.Parameters.AddWithValue("@SendingDistId", SendingDistId);

                            cmd.Parameters.AddWithValue("@GodownTypeId", GodownTypeId);
                            cmd.Parameters.AddWithValue("@GodownCenterId", GodownCenterId);
                            cmd.Parameters.AddWithValue("@GodownName", GodownName);
                            cmd.Parameters.AddWithValue("@GodownNumber", GodownNumber);

                            cmd.Parameters.AddWithValue("@Place", Place);
                            cmd.Parameters.AddWithValue("@RailRackOf_ID", RailRackOf_ID);
                            cmd.Parameters.AddWithValue("@RailRack_SendingPlace", RailRack_SendingPlace);
                            cmd.Parameters.AddWithValue("@RailRack_RecievingPlace", RailRack_RecievingPlace);
                            cmd.Parameters.AddWithValue("@Miller_ID", Miller_ID);
                            cmd.Parameters.AddWithValue("@MillerRepresentative", MillerRepresentative);

                            cmd.Parameters.AddWithValue("@CommodityId", CommodityId);
                            cmd.Parameters.AddWithValue("@Bags", Bags);
                            cmd.Parameters.AddWithValue("@QtyTransffer", QtyTransffer);
                            cmd.Parameters.AddWithValue("@TaulPtrakNo", TaulPtrakNo);
                            cmd.Parameters.AddWithValue("@TransporterId", TransporterId);

                            cmd.Parameters.AddWithValue("@TruckChalanNo", TruckChalanNo);
                            cmd.Parameters.AddWithValue("@TruckNo", TruckNo);
                            cmd.Parameters.AddWithValue("@DriverName", DriverName);

                            cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);

                            cmd.Parameters.AddWithValue("@UpdatedDate", UpdatedDate);
                            cmd.Parameters.AddWithValue("@UpdatedBy", UpdatedBy);
                            cmd.Parameters.AddWithValue("@UserId", UserId);
                            cmd.Parameters.AddWithValue("@Ip", Ip);
                            try
                            {
                                int rev = cmd.ExecuteNonQuery();
                            }
                            catch
                            {
                                /////////////////////////////
                            }
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

    [WebMethod(Description = "This Method Is Used For Gunny Bag receipt Information ")]
    public bool InGuunyBagsReceipt(DataSet dsGunnyBagsReceipt)
    {
        bool result = false;
        try
        {
            if (dsGunnyBagsReceipt != null)
            {
                if (dsGunnyBagsReceipt.Tables[0].Rows.Count > 0)
                {
                    OpenConnection();
                    trans = connection.BeginTransaction();
                    foreach (DataRow dr in dsGunnyBagsReceipt.Tables[0].Rows)
                    {
                        try
                        {
                            string GReceiptNo = dr["GReceiptNo"].ToString();
                            string District_ID = dr["District_ID"].ToString();
                            string SocietyCode = dr["SocietyCode"].ToString();
                            string PC_Id = dr["PC_Id"].ToString();
                            commandt = connection.CreateCommand();
                            commandt.Transaction = trans;
                            commandt.CommandType = CommandType.Text;
                            commandt.CommandText = "select count(*) from GunnyBagsReceipt where District_ID='" + District_ID + "'  and SocietyCode= '" + SocietyCode + "' and GReceiptNo='" + GReceiptNo + "' ";
                            Int64 res = Convert.ToInt64(commandt.ExecuteScalar());
                            commandt.Dispose();
                            if (res <= 0)
                            {
                                string DateOfRecv = getRDate_MDY(dr["DateOfRecv"].ToString());
                                string GunnyType = dr["GunnyType"].ToString();
                                string NoOfBags = dr["NoOfBags"].ToString();
                                string TruckChallanNo = dr["TruckChallanNo"].ToString();
                                string TruckNo = dr["TruckNo"].ToString();
                                string ReceivedFrom = dr["ReceivedFrom"].ToString();
                                string TruckChallanDate = getRDate_MDY(dr["TruckChallanDate"].ToString());
                                string userid = dr["userid"].ToString();
                                string datetimestamp = getRDate_MDY(dr["datetimestamp"].ToString());
                                string opration = dr["opration"].ToString();
                                string Locked = dr["Locked"].ToString();
                                string Ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();

                                cmd = connection.CreateCommand();
                                cmd.Transaction = trans;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "Prco_InsertGunnyBagsReceipt";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@GReceiptNo", GReceiptNo);
                                cmd.Parameters.AddWithValue("@District_ID", District_ID);
                                cmd.Parameters.AddWithValue("@SocietyCode", SocietyCode);
                                cmd.Parameters.AddWithValue("@PC_Id", PC_Id);
                                cmd.Parameters.AddWithValue("@DateOfRecv", DateOfRecv);
                                cmd.Parameters.AddWithValue("@GunnyType", GunnyType);
                                cmd.Parameters.AddWithValue("@NoOfBags", NoOfBags);
                                cmd.Parameters.AddWithValue("@TruckChallanNo", TruckChallanNo);
                                cmd.Parameters.AddWithValue("@TruckNo", TruckNo);
                                cmd.Parameters.AddWithValue("@ReceivedFrom", ReceivedFrom);
                                cmd.Parameters.AddWithValue("@TruckChallanDate", TruckChallanDate);
                                cmd.Parameters.AddWithValue("@userid", userid);
                                cmd.Parameters.AddWithValue("@datetimestamp", datetimestamp);
                                cmd.Parameters.AddWithValue("@opration", opration);
                                cmd.Parameters.AddWithValue("@Locked", Locked);
                                cmd.Parameters.AddWithValue("@Ip", Ip);
                                int x = cmd.ExecuteNonQuery();
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

    [WebMethod(Description = "This Method Is Used For Gunny Bag Issue Information ")]
    public bool InGunnyBagsIssue(DataSet dsGunnyBagsIssue)
    {
        bool result = false;
        try
        {
            if (dsGunnyBagsIssue != null)
            {
                if (dsGunnyBagsIssue.Tables[0].Rows.Count > 0)
                {
                    OpenConnection();
                    trans = connection.BeginTransaction();
                    foreach (DataRow dr in dsGunnyBagsIssue.Tables[0].Rows)
                    {
                        try
                        {
                            string IssueNo = dr["IssueNo"].ToString();
                            string District_ID = dr["District_ID"].ToString();
                            string SocietyCode = dr["SocietyCode"].ToString();
                            string PC_Id = dr["PC_Id"].ToString();

                            commandt = connection.CreateCommand();
                            commandt.Transaction = trans;
                            commandt.CommandType = CommandType.Text;
                            commandt.CommandText = "select count(*) from GunnyBagsIssueTable where District_ID='" + District_ID + "'  and SocietyCode= '" + SocietyCode + "' and IssueNo='" + IssueNo + "' ";
                            Int64 res = Convert.ToInt64(commandt.ExecuteScalar());
                            commandt.Dispose();
                            if (res <= 0)
                            {
                                string DateOfIssue = getRDate_MDY(dr["DateOfIssue"].ToString());
                                string TypeofBags = dr["TypeofBags"].ToString();
                                string GunnyType = dr["GunnyType"].ToString();
                                string NoOfBags = dr["NoOfBags"].ToString();
                                string TruckChallanNo = dr["TruckChallanNo"].ToString();
                                string TruckNo = dr["TruckNo"].ToString();
                                string IssuedFrom = dr["IssuedFrom"].ToString();
                                string TruckChallanDate = getRDate_MDY(dr["TruckChallanDate"].ToString());
                                string userid = dr["userid"].ToString();
                                string datetimestamp = getRDate_MDY(dr["datetimestamp"].ToString());
                                string opration = dr["opration"].ToString();
                                string Locked = dr["Locked"].ToString();
                                string Ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();

                                cmd = connection.CreateCommand();
                                cmd.Transaction = trans;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "Proc_InsertGunnyBagsIssueTable";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@IssueNo", IssueNo);
                                cmd.Parameters.AddWithValue("@District_ID", District_ID);
                                cmd.Parameters.AddWithValue("@SocietyCode", SocietyCode);
                                cmd.Parameters.AddWithValue("@PC_Id", PC_Id);
                                cmd.Parameters.AddWithValue("@DateOfIssue", DateOfIssue);
                                cmd.Parameters.AddWithValue("@TypeofBags", TypeofBags);
                                cmd.Parameters.AddWithValue("@GunnyType", GunnyType);
                                cmd.Parameters.AddWithValue("@NoOfBags", NoOfBags);
                                cmd.Parameters.AddWithValue("@TruckChallanNo", TruckChallanNo);
                                cmd.Parameters.AddWithValue("@TruckNo", TruckNo);
                                cmd.Parameters.AddWithValue("@IssuedFrom", IssuedFrom);
                                cmd.Parameters.AddWithValue("@TruckChallanDate", TruckChallanDate);
                                cmd.Parameters.AddWithValue("@userid", userid);
                                cmd.Parameters.AddWithValue("@datetimestamp", datetimestamp);
                                cmd.Parameters.AddWithValue("@opration", opration);
                                cmd.Parameters.AddWithValue("@Locked", Locked);
                                cmd.Parameters.AddWithValue("@Ip", Ip);
                                int x = cmd.ExecuteNonQuery();
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

    [WebMethod(Description = "This Method Is Used For Paid to Farmer Information ")]
    public bool InPaiToFarmer(DataSet dsPaidTofarmer)
    {
        bool result = false;
        try
        {
            if (dsPaidTofarmer != null)
            {
                if (dsPaidTofarmer.Tables[0].Rows.Count > 0)
                {
                    OpenConnection();
                    trans = connection.BeginTransaction();
                    foreach (DataRow dr in dsPaidTofarmer.Tables[0].Rows)
                    {
                        try
                        {
                            string ReceivedID = dr["ReceivedID"].ToString();
                            string District_Id = dr["District_Id"].ToString();
                            string Society_Id = dr["Society_Id"].ToString();
                            string FarmerId = dr["FarmerId"].ToString();
                            commandt = connection.CreateCommand();
                            commandt.Transaction = trans;
                            commandt.CommandType = CommandType.Text;
                            commandt.CommandText = "select count(*) from PaidToFarmer where ReceivedID='" + ReceivedID + "' and District_Id='" + District_Id + "' and  Society_Id='" + Society_Id + "' and FarmerId='" + FarmerId + "'";
                            Int32 ret = Convert.ToInt32(commandt.ExecuteScalar());
                            if (ret <= 0)
                            {

                                string Id = dr["Id"].ToString();
                                string PaidToFarmer = dr["PaidToFarmer"].ToString();
                                string PaidDate = getRDate_MDY(dr["PaidDate"].ToString());
                                string Date_Of_Creation = getRDate_MDY(dr["Date_Of_Creation"].ToString());
                                string Date_Of_Updation = getRDate_MDY(dr["Date_Of_Updation"].ToString());
                                string UserId = dr["UserId"].ToString();
                                string CheckNumber = dr["CheckNumber"].ToString();
                                cmd = connection.CreateCommand();
                                cmd.Transaction = trans;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "In_PaidToFarmer";
                                cmd.Parameters.Clear();

                                cmd.Parameters.AddWithValue("@Id", Id);
                                cmd.Parameters.AddWithValue("@ReceivedID", ReceivedID);
                                cmd.Parameters.AddWithValue("@District_Id", District_Id);
                                cmd.Parameters.AddWithValue("@Society_Id", Society_Id);
                                cmd.Parameters.AddWithValue("@FarmerId", FarmerId);

                                cmd.Parameters.AddWithValue("@PaidToFarmer", PaidToFarmer);
                                cmd.Parameters.AddWithValue("@PaidDate", PaidDate);
                                cmd.Parameters.AddWithValue("@Date_Of_Creation", Date_Of_Creation);
                                cmd.Parameters.AddWithValue("@Date_Of_Updation", Date_Of_Updation);
                                cmd.Parameters.AddWithValue("@UserId", UserId);
                                cmd.Parameters.AddWithValue("@CheckNumber", CheckNumber);
                                int x = cmd.ExecuteNonQuery();
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

    [WebMethod(Description = "This Method Is Used For Transport Information ")]
    public bool InMillerMaster(DataSet dsMiller)
    {
        bool result = false;
        try
        {
            if (dsMiller != null)
            {
                if (dsMiller.Tables[0].Rows.Count > 0)
                {
                    OpenConnection();
                    trans = connection.BeginTransaction();
                    foreach (DataRow dr in dsMiller.Tables[0].Rows)
                    {
                        try
                        {
                            string Mill_ID = dr["Mill_ID"].ToString();
                            string District_ID = dr["District_ID"].ToString();
                            string SocietyCode = dr["SocietyID"].ToString();
                            commandt = connection.CreateCommand();
                            commandt.Transaction = trans;
                            commandt.CommandType = CommandType.Text;
                            commandt.CommandText = "select count(*) from MillMaster where District_ID='" + District_ID + "'  and SocietyID= '" + SocietyCode + "' and Mill_ID='" + Mill_ID + "'";
                            Int64 res = Convert.ToInt64(commandt.ExecuteScalar());
                            commandt.Dispose();
                            if (res <= 0)
                            {
                                string Mill_Name = dr["Mill_Name"].ToString();
                                string Address = dr["Address"].ToString();
                                string MobileNo = dr["MobileNo"].ToString();

                                string CreatedDate = getRDate_MDY(dr["CreatedDate"].ToString());
                                string Updateddate = getRDate_MDY(dr["Updateddate"].ToString());
                                string CreatedBy = dr["CreatedBy"].ToString();
                                string Status = dr["Status"].ToString();
                                cmd = new SqlCommand();
                                cmd = connection.CreateCommand();
                                cmd.Transaction = trans;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "Proc_Insert_Miller_Master";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@Mill_ID", Mill_ID);
                                cmd.Parameters.AddWithValue("@District_ID", District_ID);
                                cmd.Parameters.AddWithValue("@SocietyID", SocietyCode);
                                cmd.Parameters.AddWithValue("@Mill_Name", Mill_Name);
                                cmd.Parameters.AddWithValue("@Address", Address);
                                cmd.Parameters.AddWithValue("@MobileNo", MobileNo);
                                cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                                cmd.Parameters.AddWithValue("@Updateddate", Updateddate);
                                cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                                cmd.Parameters.AddWithValue("@Status", Status);
                                int rev = cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                string Mill_Name = dr["Mill_Name"].ToString();
                                string Address = dr["Address"].ToString();
                                string MobileNo = dr["MobileNo"].ToString();
                                commandt = connection.CreateCommand();
                                commandt.Transaction = trans;
                                commandt.CommandType = CommandType.Text;
                                commandt.CommandText = "select count(*) from MillMaster where District_ID='" + District_ID + "'  and SocietyID= '" + SocietyCode + "' and Mill_ID='" + Mill_ID + "' and Mill_Name=N'" + Mill_Name + "'  and Address= N'" + Mill_Name + "' and MobileNo='" + MobileNo + "'";
                                Int64 Res_N = Convert.ToInt64(commandt.ExecuteScalar());
                                commandt.Dispose();
                                if (Res_N <= 0)
                                {
                                    string Updateddate = getRDate_MDY(dr["Updateddate"].ToString());
                                    cmd = new SqlCommand();
                                    cmd = connection.CreateCommand();
                                    cmd.Transaction = trans;
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "In_Miller_Update_Info";
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@Mill_ID", Mill_ID);
                                    cmd.Parameters.AddWithValue("@District_ID", District_ID);
                                    cmd.Parameters.AddWithValue("@SocietyID", SocietyCode);
                                    cmd.Parameters.AddWithValue("@Mill_Name", Mill_Name);
                                    cmd.Parameters.AddWithValue("@Address", Address);
                                    cmd.Parameters.AddWithValue("@MobileNo", MobileNo);
                                    cmd.Parameters.AddWithValue("@Updateddate", Updateddate);
                                    int rev = cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        catch
                        {
                            ////////////
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

    #region Output for Offline Database.....

    [WebMethod(Description = "This Method Is Used For Taking Output for Farmer SMS Information ")]
    public DataSet OpFarmerSMS(string D, string S)
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
            commandt.CommandText = "select  distinct FarmerSMS.SMS_ID  from  FarmerSMS where FarmerSMS.District_Code='" + D + "' and FarmerSMS.Society_ID='" + S + "'";
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

    [WebMethod(Description = "This Method Is Used For Taking Output for Commodity Received Information ")]
    public DataSet OpCommodityReceived(string D, string S)
    {
        dataset = new DataSet();
        try
        {
            if (D == "2338")
            {
                OpenConnection();
                trans = connection.BeginTransaction();
                commandt = connection.CreateCommand();
                commandt.Transaction = trans;
                commandt.Parameters.Clear();
                commandt.CommandType = CommandType.Text;
                commandt.CommandText = "select  distinct CommodityReceivedFromFarmer.ReceivedID  from  CommodityReceivedFromFarmer where CommodityReceivedFromFarmer.District_Id='" + D + "' and CommodityReceivedFromFarmer.Society_Id='" + S + "'";
                dataAdapter = new SqlDataAdapter(commandt);
                dataAdapter.Fill(dataset);
                commandt.Dispose();
                trans.Commit();
            }
            else
            {
                OpenConnection();
                trans = connection.BeginTransaction();
                commandt = connection.CreateCommand();
                commandt.Transaction = trans;
                commandt.Parameters.Clear();
                commandt.CommandType = CommandType.Text;
                commandt.CommandText = "select  distinct CommodityReceivedFromFarmer_After28jan.ReceivedID  from  CommodityReceivedFromFarmer_After28jan where CommodityReceivedFromFarmer_After28jan.District_Id='" + D + "' and CommodityReceivedFromFarmer_After28jan.Society_Id='" + S + "'";
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

    [WebMethod(Description = "This Method Is Used For Taking Output for Commodity Transaction Information ")]
    public DataSet OpCommodityTrans(string D, string S)
    {
        dataset = new DataSet();
        try
        {
            if (D == "2338")
            {
                OpenConnection();
                trans = connection.BeginTransaction();
                commandt = connection.CreateCommand();
                commandt.Transaction = trans;
                commandt.Parameters.Clear();
                commandt.CommandType = CommandType.Text;
                commandt.CommandText = "select  distinct CommodityReceived_Transaction.ComTransID  from  CommodityReceived_Transaction where CommodityReceived_Transaction.District_Id='" + D + "' and CommodityReceived_Transaction.Society_Id='" + S + "'";
                dataAdapter = new SqlDataAdapter(commandt);
                dataAdapter.Fill(dataset);
                commandt.Dispose();
                trans.Commit();
            }
            else
            {
                OpenConnection();
                trans = connection.BeginTransaction();
                commandt = connection.CreateCommand();
                commandt.Transaction = trans;
                commandt.Parameters.Clear();
                commandt.CommandType = CommandType.Text;
                commandt.CommandText = "select  distinct CommodityReceived_Transaction_After28jan.ComTransID  from  CommodityReceived_Transaction_After28jan where CommodityReceived_Transaction_After28jan.District_Id='" + D + "' and CommodityReceived_Transaction_After28jan.Society_Id='" + S + "'";
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

    [WebMethod(Description = "This Method Is Used For Taking Output for Transport Information ")]
    public DataSet OpTransportrInfo(string D, string S)
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
            commandt.CommandText = "select  distinct TransportMaster.Transporter_ID  from  TransportMaster where TransportMaster.District_ID='" + D + "' and TransportMaster.SocietyCode='" + S + "'";
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

    [WebMethod(Description = "This Method Is Used For Taking Output for Transport Information ")]
    public DataSet OpMillerMasterInfo(string D, string S)
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
            commandt.CommandText = "select  distinct MillMaster.Mill_ID  from  MillMaster where MillMaster.District_ID='" + D + "' and MillMaster.SocietyID='" + S + "'";
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

    [WebMethod(Description = "This Method Is Used For Taking Output for Issue to Sangrah Kendra Information ")]
    public DataSet OpIssueToSangrhanKendra(string D, string S)
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
            commandt.CommandText = "select  distinct IssueToSangrahanaKendra.IssueID  from  IssueToSangrahanaKendra where IssueToSangrahanaKendra.DistrictId='" + D + "' and IssueToSangrahanaKendra.SocietyID='" + S + "'";
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

    [WebMethod(Description = "This Method Is Used For Taking Output for Gunny Bags Receipt Information ")]
    public DataSet OpGuunyBagsReceipt(string D, string S)
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
            commandt.CommandText = "select  distinct GunnyBagsReceipt.GReceiptNo  from  GunnyBagsReceipt where GunnyBagsReceipt.District_ID='" + D + "' and GunnyBagsReceipt.SocietyCode='" + S + "'";
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

    [WebMethod(Description = "This Method Is Used For Taking Output for Gunny Bags Issue Information ")]
    public DataSet OpGunnyBagsIssue(string D, string S)
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
            commandt.CommandText = "select  distinct GunnyBagsIssueTable.IssueNo  from  GunnyBagsIssueTable where GunnyBagsIssueTable.District_ID='" + D + "' and GunnyBagsIssueTable.SocietyCode='" + S + "'";
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

    [WebMethod(Description = "This Method Is Used For Taking Output for Paid to Farmer Information ")]
    public DataSet OpPaidToFarmer(string D, string S)
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
            commandt.CommandText = "select  distinct PaidToFarmer.ReceivedID  from  PaidToFarmer where PaidToFarmer.District_Id='" + D + "' and PaidToFarmer.Society_Id='" + S + "'";
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
public class securityPaddyexport_Proc_2013 : System.Web.Services.Protocols.SoapHeader
{
    public bool IsActive;
    public String Password;
    public String UserName;
    public String RunnerId;
    public String RunnerPassword;
}

