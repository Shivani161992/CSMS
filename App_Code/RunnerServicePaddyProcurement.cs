
using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


/// <summary>
/// Summary description for RunnerServicePaddyProcurement
/// </summary>
/// 


[WebService(Namespace = "http://microsoft.co.in/")]

[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

public class RunnerServicePaddyProcurement : System.Web.Services.WebService
{
    private SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con_proc_online_PPMS"].ToString());
    private SqlCommand commandtrans, cmd;
    private SqlDataAdapter dataAdapter;
    private DataSet dataset;
    private SqlTransaction trans;
    private static int statu_cntr = 0;
    string res = "";
    int reint = 0;


    public RunnerServicePaddyProcurement()
    {
        //Uncomment the following line if using designed components 
        // InitializeComponent(); 
    }

    public ChkPaddyUser MyHeader;
    [WebMethod(Description = "This method is used for checking username and password")]
    [SoapHeader("MyHeader")]

    public bool chkInformation(ChkPaddyUser userpwd)
    {
        bool rtev = false;
        if (userpwd != null)
        {
            if (userpwd.username == "pp" && userpwd.password == "pp")
            {
                rtev = true;
                return rtev;
            }
        }
        else
        {
            return rtev;
        }

        return rtev;
    }


    #region Offline TO Online Data Insert



    [WebMethod(Description = "23012013")]
    public void InsertFarmerBhugtan(DataSet dsBhigtanInfo, string societyID)
    {
        string LogID = "";
        string date = "";
        //string farmerid = "";
        try
        {
            if (connection != null)
            {
                connection.Open();
                commandtrans = connection.CreateCommand();
                trans = connection.BeginTransaction();
                commandtrans.Connection = connection;
                commandtrans.Transaction = trans;
                date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                LogID = societyID.ToString() + date;

                if (dsBhigtanInfo != null)
                {
                    foreach (DataRow dr in dsBhigtanInfo.Tables[0].Rows)
                    {
                        string ReceivedID = dr["ReceivedID"].ToString();
                        string District_Id = dr["District_Id"].ToString();
                        string Society_Id = dr["Society_Id"].ToString();
                        string FarmerId = dr["FarmerId"].ToString();
                        commandtrans.CommandType = CommandType.Text;
                        commandtrans.CommandText = "select count(*) from PaidToFarmer where ReceivedID='" + ReceivedID + "' and District_Id='" + District_Id + "' and  Society_Id='" + Society_Id + "' and FarmerId='" + FarmerId + "'";
                        Int32 ret = Convert.ToInt32(commandtrans.ExecuteScalar());

                        if (ret <= 0)
                        {

                            string Id = dr["Id"].ToString();
                            string PaidToFarmer = dr["PaidToFarmer"].ToString();
                            string PaidDate = chkDate(dr["PaidDate"].ToString());
                            string Date_Of_Creation = chkDate(dr["Date_Of_Creation"].ToString());
                            string UserId = dr["UserId"].ToString();
                            string CheckNumber = dr["CheckNumber"].ToString();

                            commandtrans.CommandType = CommandType.StoredProcedure;
                            commandtrans.CommandText = "In_PaidToFarmer";
                            commandtrans.Parameters.Clear();

                            commandtrans.Parameters.AddWithValue("@ReceivedID", ReceivedID);
                            commandtrans.Parameters.AddWithValue("@District_Id", District_Id);
                            commandtrans.Parameters.AddWithValue("@Society_Id", Society_Id);
                            commandtrans.Parameters.AddWithValue("@FarmerId", FarmerId);
                            commandtrans.Parameters.AddWithValue("@Id", Id);
                            commandtrans.Parameters.AddWithValue("@PaidToFarmer", PaidToFarmer);
                            commandtrans.Parameters.AddWithValue("@PaidDate", PaidDate);
                            commandtrans.Parameters.AddWithValue("@Date_Of_Creation", Date_Of_Creation);
                            commandtrans.Parameters.AddWithValue("@UserId", UserId);
                            commandtrans.Parameters.AddWithValue("@CheckNumber", CheckNumber);
                            int x = commandtrans.ExecuteNonQuery();


                        }

                    }
                    statu_cntr = 11;
                    trans.Commit();
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
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();
        }
    }

    //19102012
    [WebMethod(Description = "1. Runner Log -Off2Onn")]
    public void InsertUpdRunnerLog(string societyID, ChkPaddyUser userpwd)
    {
        string LogID = "";
        try
        {

            if (connection != null && userpwd.username == "pp" && userpwd.password == "pp")
            {
                string date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                LogID = societyID.ToString() + date;

                connection.Open();
                commandtrans = connection.CreateCommand();
                trans = connection.BeginTransaction();
                commandtrans.Connection = connection;
                commandtrans.Transaction = trans;
                commandtrans.CommandType = CommandType.Text;
                commandtrans.CommandText = "select count(LogID) from RunnerLog where LogID='" + LogID + "'";

                string res = commandtrans.ExecuteScalar().ToString();
                if (Convert.ToInt16(res) >= 0)
                {
                    string logcount = "";
                    if (Convert.ToInt16(res) == 0)
                    {
                        logcount = Convert.ToString(1);
                    }
                    else
                    {
                        logcount = Convert.ToString((Convert.ToInt16(res) + 1));
                    }
                    string LogIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    string LogDate = "";
                    string Pc_Id = societyID.ToString();
                    string District_Code = societyID.Substring(0, 4);
                    string Society_Id = societyID.ToString();
                    string RunnerID = societyID.ToString();
                    string Status = "YES";
                    string RunnerVer = "3.2.3";
                    commandtrans.CommandType = CommandType.StoredProcedure;
                    commandtrans.CommandText = "Proc_InsertRunnerLog";
                    commandtrans.Parameters.Clear();
                    commandtrans.Parameters.AddWithValue("@LogID", LogID);
                    commandtrans.Parameters.AddWithValue("@LogIP", LogIP);
                    commandtrans.Parameters.AddWithValue("@Pc_Id", Pc_Id);
                    commandtrans.Parameters.AddWithValue("@District_Code", District_Code);
                    commandtrans.Parameters.AddWithValue("@Society_Id", Society_Id);
                    commandtrans.Parameters.AddWithValue("@RunnerID", RunnerID);
                    commandtrans.Parameters.AddWithValue("@Status", Status);
                    commandtrans.Parameters.AddWithValue("@RunnerVer", RunnerVer);
                    commandtrans.Parameters.AddWithValue("@DayCount", logcount);
                    int x = commandtrans.ExecuteNonQuery();
                    statu_cntr = 5;
                    trans.Commit();
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
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();
        }

    }

    //19102012
    [WebMethod(Description = "2.Initial ddfasdf-Off2Onn")]
    public void InsertUpdateINITAIAL(DataSet dsInitial, bool ChkPaddyUser, string societyID, string DateInstall)
    {
        string LogID = "";
        string date = "";
        try
        {
            if (connection != null)
            {
                connection.Open();
                date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                LogID = societyID.ToString() + date;
                foreach (DataRow dr in dsInitial.Tables[0].Rows)
                {
                    string Society_Id = dr["Society_Id"].ToString();
                    commandtrans = connection.CreateCommand();
                    trans = connection.BeginTransaction();
                    commandtrans.Connection = connection;
                    commandtrans.Transaction = trans;
                    commandtrans.CommandType = CommandType.Text;
                    commandtrans.CommandText = "Select count(*) from Initial where Society_Id='" + Society_Id + "'";
                    string res = commandtrans.ExecuteScalar().ToString();
                    if (Convert.ToInt16(res) <= 0)
                    {

                        string OpeningStockOfGunny = dr["OpeningStockOfGunny"].ToString();
                        string District_ID = dr["District_ID"].ToString();
                        string District_Name = dr["District_Name"].ToString();
                        // string Society_Id = dr["Society_Id"].ToString();
                        string SocietyName = dr["SocietyName"].ToString();
                        string PC_NAME = dr["PC_NAME"].ToString();
                        string AgencyId = dr["AgencyId"].ToString();
                        string AgencyName = dr["AgencyName"].ToString();
                        string MarketingSeasonId = dr["MarketingSeasonId"].ToString();
                        string MarketingSeason = dr["MarketingSeason"].ToString();
                        string CropYear = dr["CropYear"].ToString();
                        string OneFarmerLimit = dr["OneFarmerLimit"].ToString();
                        string Password1 = dr["Password1"].ToString();
                        string BankName = dr["BankName"].ToString();
                        string AccNO = dr["AccNO"].ToString();
                        string BranchName = dr["BranchName"].ToString();
                        string ManagerName = dr["ManagerName"].ToString();
                        string VersionNo = dr["VersionNo"].ToString();
                        Int32 NoOfToulKanta = CheckNullInt(dr["NoOfToulKanta"].ToString());
                        string DailySc_Capacity = Convert.ToString(CheckNullFloat(dr["DailySc_Capacity"].ToString()));
                        string UpdationDate = "";
                        if (UpdationDate != "")
                        {
                            UpdationDate = chkDate(dr["UpdationDate"].ToString());
                        }
                        else
                        {
                            UpdationDate = System.DateTime.Now.ToString("01/01/1900");
                        }
                        string Societycreditlimit = Convert.ToString(CheckNullFloat(dr["Societycreditlimit"].ToString()));
                        string MgrMobileNo = dr["MgrMobileNo"].ToString();
                        Int64 SocBandaranCapacity = CheckNullInt(dr["SocBandaranCapacity"].ToString());
                        string DateTimeOfInstall = DateInstall;
                        DateTime indate = new DateTime();
                        if (DateTimeOfInstall != "")
                        {
                            DateTimeOfInstall = (DateInstall);
                            DateTimeOfInstall = dr["DateTimeOfInstall"].ToString();
                            indate = Convert.ToDateTime(DateTimeOfInstall);
                        }
                        string qryInsert = "Insert Into Initial(OpeningStockOfGunny,District_ID,District_Name,Society_Id,SocietyName,PC_NAME,AgencyId,AgencyName,MarketingSeasonId,MarketingSeason,CropYear,Password1,BankName,";
                        qryInsert += "AccNO,BranchName,ManagerName,VersionNo,NoOfToulKanta,DailySc_Capacity,Societycreditlimit,MgrMobileNo, SocBandaranCapacity,DateTimeOfInstall,OneFarmerLimit)";
                        qryInsert += " values('" + OpeningStockOfGunny + "','" + District_ID + "',N'" + District_Name + "','" + Society_Id + "',N'" + SocietyName + "',N'" + PC_NAME + "','" + AgencyId + "','" + AgencyName + "',N'" + MarketingSeasonId + "',N'" + MarketingSeason + "','" + CropYear + "','" + Password1 + "','" + BankName + "','" + AccNO + "',N'" + BranchName + "' ,";
                        qryInsert += " N'" + ManagerName + "','" + VersionNo + "'," + NoOfToulKanta + "," + DailySc_Capacity + "," + Societycreditlimit + ",'" + MgrMobileNo + "'," + SocBandaranCapacity + ",'" + indate + "','" + OneFarmerLimit + "' )";
                        commandtrans.CommandType = CommandType.Text;
                        commandtrans.CommandText = qryInsert;
                        int xins = commandtrans.ExecuteNonQuery();

                    }
                    else
                    {

                        string District_ID = dr["District_ID"].ToString();
                        string District_Name = dr["District_Name"].ToString();
                        // string Society_Id = dr["Society_Id"].ToString();
                        string SocietyName = dr["SocietyName"].ToString();
                        string PC_NAME = dr["PC_NAME"].ToString();
                        string AgencyId = dr["AgencyId"].ToString();
                        string AgencyName = dr["AgencyName"].ToString();
                        string MarketingSeasonId = dr["MarketingSeasonId"].ToString();
                        string MarketingSeason = dr["MarketingSeason"].ToString();
                        string CropYear = dr["CropYear"].ToString();
                        string Password1 = dr["Password1"].ToString();
                        string BankName = dr["BankName"].ToString();
                        string OneFarmerLimit = dr["OneFarmerLimit"].ToString();
                        string AccNO = dr["AccNO"].ToString();
                        string BranchName = dr["BranchName"].ToString();
                        string ManagerName = dr["ManagerName"].ToString();
                        string VersionNo = dr["VersionNo"].ToString();
                        Int32 NoOfToulKanta = CheckNullInt(dr["NoOfToulKanta"].ToString());
                        float DailySc_Capacity = CheckNullFloat(dr["DailySc_Capacity"].ToString());
                        string UpdationDate = dr["UpdationDate"].ToString();
                        if (UpdationDate != "")
                        {
                            UpdationDate = chkDate(dr["UpdationDate"].ToString());
                        }
                        else
                        {
                            UpdationDate = System.DateTime.Now.ToString("01/01/1900");
                        }
                        float Societycreditlimit = CheckNullFloat(dr["Societycreditlimit"].ToString());
                        string MgrMobileNo = dr["MgrMobileNo"].ToString();
                        Int64 SocBandaranCapacity = CheckNullInt(dr["SocBandaranCapacity"].ToString());
                        string DateTimeOfInstall = DateInstall;
                        DateTime imdate = new DateTime();
                        if (DateTimeOfInstall != "")
                        {
                            DateTimeOfInstall = (DateInstall);
                            DateTimeOfInstall = dr["DateTimeOfInstall"].ToString();
                            imdate = Convert.ToDateTime(DateTimeOfInstall);
                        }
                        else
                        {
                            DateTimeOfInstall = System.DateTime.Now.ToString("01/01/1900");
                        }

                        string qryUpd = "Update Initial set UpdationDate='" + UpdationDate + "',Password1='" + Password1 + "',BankName=N'" + BankName + "', AccNO='" + AccNO + "',BranchName=N'" + BranchName + "',ManagerName=N'" + ManagerName + "',VersionNo='" + VersionNo + "',NoOfToulKanta=" + NoOfToulKanta + ",DailySc_Capacity=" + DailySc_Capacity + " ,MarketingSeasonId=N'" + MarketingSeasonId + "',MarketingSeason=N'" + MarketingSeason + "',Societycreditlimit='" + Societycreditlimit + "',MgrMobileNo='" + MgrMobileNo + "',SocBandaranCapacity=" + SocBandaranCapacity + ", DateTimeOfInstall='" + imdate + "',OneFarmerLimit='" + OneFarmerLimit + "' where District_ID='" + District_ID + "' and Society_Id='" + Society_Id + "'";
                        commandtrans.CommandType = CommandType.Text;
                        commandtrans.CommandText = qryUpd;
                        int xupd = commandtrans.ExecuteNonQuery();

                    }
                }
                statu_cntr = 10;
                trans.Commit();

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
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();
        }

    }



    //19102012
    #region No Requirement Operator Regsitration


    //[WebMethod(Description = "2. Operator Registration -Off2Onn")]
    //public void InsertUpdOperatorRegistration(DataSet ds, bool ChkPaddyUser, string societyID)
    //{
    //    string LogID = "";
    //    string date = "";
    //    try
    //    {
    //        if (ds != null)
    //        {
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                if (connection != null)
    //                {
    //                    connection.Open();
    //                    date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
    //                    LogID = societyID.ToString() + date;

    //                    foreach (DataRow dr in ds.Tables[0].Rows)
    //                    {

    //                        commandtrans = connection.CreateCommand();
    //                        trans = connection.BeginTransaction();
    //                        commandtrans.Connection = connection;
    //                        commandtrans.Transaction = trans;
    //                        commandtrans.CommandType = CommandType.Text;
    //                        commandtrans.CommandText = "select count(*) from OperatorRegistration where Op_id='" + dr["Op_id"].ToString() + "' ";
    //                        string res = commandtrans.ExecuteScalar().ToString();
    //                        int ret = Convert.ToInt16(commandtrans.ExecuteScalar());
    //                        if (ret <= 0)
    //                        {
    //                            string Op_id = dr["Op_id"].ToString();
    //                            string DistrictId = dr["DistrictId"].ToString();
    //                            string Proc_AgID = dr["Proc_AgID"].ToString();
    //                            string PCID = dr["PCID"].ToString();
    //                            string SocietyID = dr["SocietyID"].ToString();
    //                            string Name = dr["Name"].ToString();
    //                            string MobileNo = dr["MobileNo"].ToString();
    //                            string Email = dr["Email"].ToString();
    //                            string Address = dr["Address"].ToString();
    //                            string Password1 = dr["Password1"].ToString();
    //                            string MasterPassword = dr["MasterPassword"].ToString();


    //                            commandtrans.CommandType = CommandType.StoredProcedure;
    //                            commandtrans.CommandText = "Proc_InsertOperatorRegistration";
    //                            commandtrans.Parameters.Clear();
    //                            commandtrans.Parameters.AddWithValue("@Op_id", Op_id);
    //                            commandtrans.Parameters.AddWithValue("@DistrictId", DistrictId);
    //                            commandtrans.Parameters.AddWithValue("@Proc_AgID", Proc_AgID);
    //                            commandtrans.Parameters.AddWithValue("@PCID", PCID);
    //                            commandtrans.Parameters.AddWithValue("@SocietyID", SocietyID);
    //                            commandtrans.Parameters.AddWithValue("@Name", Name);
    //                            commandtrans.Parameters.AddWithValue("@MobileNo", MobileNo);
    //                            commandtrans.Parameters.AddWithValue("@Email", Email);
    //                            commandtrans.Parameters.AddWithValue("@Address", Address);
    //                            commandtrans.Parameters.AddWithValue("@Password1", Password1);
    //                            commandtrans.Parameters.AddWithValue("@MasterPassword", MasterPassword);
    //                            int retv = commandtrans.ExecuteNonQuery();


    //                        }
    //                        else
    //                        {
    //                            string Op_id = dr["Op_id"].ToString();
    //                            string DistrictId = dr["DistrictId"].ToString();
    //                            string Proc_AgID = dr["Proc_AgID"].ToString();
    //                            string PCID = dr["PCID"].ToString();
    //                            string SocietyID = dr["SocietyID"].ToString();
    //                            string Name = dr["Name"].ToString();
    //                            string MobileNo = dr["MobileNo"].ToString();
    //                            string Email = dr["Email"].ToString();
    //                            string Address = dr["Address"].ToString();
    //                            string Password1 = dr["Password1"].ToString();
    //                            string MasterPassword = dr["MasterPassword"].ToString();


    //                            commandtrans.CommandType = CommandType.StoredProcedure;
    //                            commandtrans.CommandText = "Proc_UpdateOperatorRegistration";//-----Make Procedure On Server 18092012  
    //                            commandtrans.Parameters.Clear();
    //                            commandtrans.Parameters.AddWithValue("@Op_id", Op_id);
    //                            commandtrans.Parameters.AddWithValue("@DistrictId", DistrictId);
    //                            commandtrans.Parameters.AddWithValue("@Proc_AgID", Proc_AgID);
    //                            commandtrans.Parameters.AddWithValue("@PCID", PCID);
    //                            commandtrans.Parameters.AddWithValue("@SocietyID", SocietyID);
    //                            commandtrans.Parameters.AddWithValue("@Name", Name);
    //                            commandtrans.Parameters.AddWithValue("@MobileNo", MobileNo);
    //                            commandtrans.Parameters.AddWithValue("@Email", Email);
    //                            commandtrans.Parameters.AddWithValue("@Address", Address);
    //                            int retv = commandtrans.ExecuteNonQuery();


    //                        }
    //                    }

    //                    statu_cntr = 12;
    //                    trans.Commit();

    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        trans.Rollback();
    //        connection.Close();
    //    }
    //    finally
    //    {
    //        connection.Close();
    //        UploadDataStatus(statu_cntr, LogID);
    //        trans.Dispose();
    //        commandtrans.Dispose();
    //    }
    //}

    #endregion


    //19102012
    [WebMethod(Description = "3. FarmerSMS In Offline- Off2Onn")]
    public void InsertFarmerSMS(DataSet dsFarmer, string societyID, string districtyID, bool ChkPaddyUser)
    {

        string LogID = "";
        string date = "";
        string farmerid = "";
        try
        {
            if (connection != null && ChkPaddyUser == true)
            {
                connection.Open();
                commandtrans = connection.CreateCommand();
                trans = connection.BeginTransaction();
                commandtrans.Connection = connection;
                commandtrans.Transaction = trans;
                date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                LogID = societyID.ToString() + date;

                if (dsFarmer != null)
                {
                    foreach (DataRow dr in dsFarmer.Tables[0].Rows)
                    {
                        string SMS_ID = dr["SMS_ID"].ToString();
                        string District_Code = dr["District_Code"].ToString();
                        string PC_ID = dr["PC_ID"].ToString();
                        string MobileNo = dr["MobileNo"].ToString();

                        commandtrans.CommandType = CommandType.Text;
                        commandtrans.CommandText = "select count(*) from FarmerSMS where SMS_ID='" + SMS_ID + "' and District_Code='" + District_Code + "' and  PC_ID='" + PC_ID + "' and MobileNo='" + MobileNo + "'";
                        int ret = Convert.ToInt16(commandtrans.ExecuteScalar());


                        if (ret <= 0)
                        {
                            string Farmer_Id = dr["Farmer_Id"].ToString();
                            string Society_ID = dr["Society_ID"].ToString();
                            string crpcode = dr["crpcode"].ToString();
                            string CommodityName = dr["CommodityName"].ToString();

                            string DateOfCollingFarmer = chkDate(dr["DateOfCollingFarmer"].ToString());
                            string DateOfCreation = chkDate(dr["DateOfCreation"].ToString());
                            string CropExpectedDate = chkDate(dr["CropExpectedDate"].ToString());
                            string OneFarmerLimit = dr["OneFarmerLimit"].ToString();
                            string NextCropExpectedDate = chkDate(dr["NextCropExpectedDate"].ToString());
                            string RemainingCrop = dr["RemainingCrop"].ToString();
                            string SocietyLimit = dr["SocietyLimit"].ToString();
                            commandtrans.CommandType = CommandType.StoredProcedure;
                            commandtrans.CommandText = "Proc_InsertFarmerSMS";
                            commandtrans.Parameters.Clear();
                            commandtrans.Parameters.AddWithValue("@SMS_ID", SMS_ID);
                            commandtrans.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                            commandtrans.Parameters.AddWithValue("@District_Code", District_Code);
                            commandtrans.Parameters.AddWithValue("@Society_ID", Society_ID);
                            commandtrans.Parameters.AddWithValue("@PC_ID", PC_ID);
                            commandtrans.Parameters.AddWithValue("@crpcode", crpcode);
                            commandtrans.Parameters.AddWithValue("@CommodityName", CommodityName);
                            commandtrans.Parameters.AddWithValue("@DateOfCollingFarmer", DateOfCollingFarmer);
                            commandtrans.Parameters.AddWithValue("@MobileNo", MobileNo);
                            commandtrans.Parameters.AddWithValue("@DateOfCreation", DateOfCreation);
                            commandtrans.Parameters.AddWithValue("@CropExpectedDate", CropExpectedDate);
                            commandtrans.Parameters.AddWithValue("@OneFarmerLimit", OneFarmerLimit);
                            commandtrans.Parameters.AddWithValue("@NextCropExpectedDate", NextCropExpectedDate);
                            commandtrans.Parameters.AddWithValue("@RemainingCrop", RemainingCrop);
                            commandtrans.Parameters.AddWithValue("@SocietyLimit", SocietyLimit);
                            int x = commandtrans.ExecuteNonQuery();
                        }

                    }

                    statu_cntr = 15;
                    trans.Commit();

                }
            }
        }
        catch (Exception ex)
        {
            trans.Rollback();
            connection.Close();
        }
        finally
        {
            connection.Close();
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();
        }
    }




    //19102012
    [WebMethod(Description = "6. New Farmer Registration(Personal Info) -Off2Onn")]
    public void InsertFarmerRegistration(DataSet dsFarmerInfo, string societyID, bool ChkPaddyUser)
    {
        string LogID = "";
        string date = "";
        string farmerid = "";
        try
        {
            if (connection != null && ChkPaddyUser == true)
            {
                connection.Open();
                commandtrans = connection.CreateCommand();
                trans = connection.BeginTransaction();
                commandtrans.Connection = connection;
                commandtrans.Transaction = trans;

                date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                LogID = societyID.ToString() + date;
                if (dsFarmerInfo != null)
                {

                    foreach (DataRow dr in dsFarmerInfo.Tables[0].Rows)
                    {
                        farmerid = dr["Farmer_Id"].ToString();
                        string District_Id = dr["District_Id"].ToString();

                        commandtrans.CommandType = CommandType.Text;
                        commandtrans.CommandText = "select count(*) from FarmerRegistration where [Farmer_Id]='" + dr["Farmer_Id"].ToString() + "' and [District_Id]='" + dr["District_Id"].ToString() + "' and PC_ID='" + dr["PC_ID"].ToString() + "' ";
                        string res = commandtrans.ExecuteScalar().ToString();
                        int ret = Convert.ToInt16(commandtrans.ExecuteScalar());
                        if (ret <= 0)
                        {

                            string Village_Id = dr["Village_Id"].ToString();
                            string VillageName = dr["VillageName"].ToString();
                            string Tehsil_Id = dr["Tehsil_Id"].ToString();
                            string FarmerName = dr["FarmerName"].ToString();
                            string FatherHusName = dr["FatherHusName"].ToString();
                            string Gram_Panchayat = dr["Gram_Panchayat"].ToString();
                            string PatwariHalkaNo = dr["PatwariHalkaNo"].ToString();
                            string Mobileno = dr["Mobileno"].ToString();
                            string Category = dr["Category"].ToString();
                            string RinPustikaNo = dr["RinPustikaNo"].ToString();
                            string Farmer_EID_UID_No = dr["Farmer_EID_UID_No"].ToString();
                            string Farmer_BankName_New = dr["Farmer_BankName_New"].ToString();
                            string Farmer_BankBranchName = dr["Farmer_BankBranchName"].ToString();
                            string Farmer_BankAccountNo = dr["Farmer_BankAccountNo"].ToString();
                            string PC_ID = dr["PC_ID"].ToString();
                            string Procured_SocietyID = dr["Procured_SocietyID"].ToString();
                            string Procured_Dist_ID = dr["Procured_Dist_ID"].ToString();
                            string Procured_Place = dr["Procured_Place"].ToString();
                            string CropExpected_Date = chkDate(dr["CropExpected_Date"].ToString());
                            string UserID = dr["UserID"].ToString();
                            string CreatedDate = getDate_MDY(dr["CreatedDate"].ToString());
                            string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                            string RegistrationDate = getDate_MDY(dr["RegistrationDate"].ToString());
                            string IsApproved = dr["IsApproved"].ToString();
                            string Status = dr["Status"].ToString();


                            commandtrans.CommandType = CommandType.StoredProcedure;
                            commandtrans.CommandText = "Proc_InsertFarmerNew";
                            commandtrans.Parameters.Clear();
                            commandtrans.Parameters.AddWithValue("@District_Id", District_Id);
                            commandtrans.Parameters.AddWithValue("@Village_Id", Village_Id);
                            commandtrans.Parameters.AddWithValue("@VillageName", VillageName);
                            commandtrans.Parameters.AddWithValue("@Tehsil_Id", Tehsil_Id);
                            commandtrans.Parameters.AddWithValue("@Farmer_Id", farmerid);
                            commandtrans.Parameters.AddWithValue("@FarmerName", FarmerName);
                            commandtrans.Parameters.AddWithValue("@FatherHusName", FatherHusName);
                            commandtrans.Parameters.AddWithValue("@Gram_Panchayat", Gram_Panchayat);
                            commandtrans.Parameters.AddWithValue("@PatwariHalkaNo", PatwariHalkaNo);
                            commandtrans.Parameters.AddWithValue("@Mobileno", Mobileno);
                            commandtrans.Parameters.AddWithValue("@Category", Category);
                            commandtrans.Parameters.AddWithValue("@RinPustikaNo", RinPustikaNo);
                            commandtrans.Parameters.AddWithValue("@Farmer_EID_UID_No", Farmer_EID_UID_No);
                            commandtrans.Parameters.AddWithValue("@Farmer_BankName_New", Farmer_BankName_New);
                            commandtrans.Parameters.AddWithValue("@Farmer_BankBranchName", Farmer_BankBranchName);
                            commandtrans.Parameters.AddWithValue("@Farmer_BankAccountNo", Farmer_BankAccountNo);
                            commandtrans.Parameters.AddWithValue("@PC_ID", PC_ID);
                            commandtrans.Parameters.AddWithValue("@Procured_SocietyID", Procured_SocietyID);
                            commandtrans.Parameters.AddWithValue("@Procured_Dist_ID", Procured_Dist_ID);
                            commandtrans.Parameters.AddWithValue("@Procured_Place", Procured_Place);
                            commandtrans.Parameters.AddWithValue("@CropExpected_Date", CropExpected_Date);
                            commandtrans.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                            commandtrans.Parameters.AddWithValue("@ip", ip);
                            commandtrans.Parameters.AddWithValue("@RegistrationDate", RegistrationDate);
                            commandtrans.Parameters.AddWithValue("@IsApproved", IsApproved);
                            commandtrans.Parameters.AddWithValue("@UserID", UserID);
                            commandtrans.Parameters.AddWithValue("@Status", Status);
                            int x = commandtrans.ExecuteNonQuery();

                        }

                    }
                    statu_cntr = 20;
                    trans.Commit();
                }
            }
        }
        catch (Exception ex)
        {
            trans.Rollback();
            connection.Close();
        }
        finally
        {
            connection.Close();
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();
        }

    }


    //19102012
    [WebMethod(Description = "7. New Farmers Land-Record Description -Off2Onn")]
    public void InsertFarmer_LandRecordDescription(DataSet dsFarmerInfo, string societyID, bool ChkPaddyUser)
    {
        string LogID = "";
        string date = "";
        string farmerid = "";
        try
        {
            if (connection != null && ChkPaddyUser == true)
            {
                connection.Open();
                commandtrans = connection.CreateCommand();
                trans = connection.BeginTransaction();
                commandtrans.Connection = connection;
                commandtrans.Transaction = trans;
                if (dsFarmerInfo != null)
                {
                    date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                    LogID = societyID.ToString() + date;


                    foreach (DataRow dr in dsFarmerInfo.Tables[0].Rows)
                    {
                        farmerid = dr["Farmer_Id"].ToString();


                        commandtrans.CommandType = CommandType.Text;
                        commandtrans.CommandText = "select count(Farmer_Id) from FarmerRegistration where Farmer_Id='" + dr["Farmer_Id"].ToString() + "' ";
                        int ret = Convert.ToInt16(commandtrans.ExecuteScalar());
                        if (Convert.ToInt16(ret) > 0)
                        {
                            commandtrans.CommandType = CommandType.Text;
                            commandtrans.CommandText = "select count(*) from Farmer_LandRecordDescription where TransID='" + dr["TransID"].ToString() + "' and Farmer_Id='" + dr["Farmer_Id"].ToString() + "' ";
                            commandtrans.Connection = connection;
                            int retv = Convert.ToInt16(commandtrans.ExecuteScalar());

                            if (retv <= 0)
                            {
                                string TransID = dr["TransID"].ToString();
                                string Farmer_Id = dr["Farmer_Id"].ToString();
                                string Village_ID = dr["Village_ID"].ToString();
                                string VillageName = dr["VillageName"].ToString();
                                string Crop_ID = dr["Crop_ID"].ToString();
                                string LandOwner_Name = dr["LandOwner_Name"].ToString();
                                string LandOwner_RinPustikaNo = dr["LandOwner_RinPustikaNo"].ToString();
                                string LandType = dr["LandType"].ToString();
                                string KhasaraNo = dr["KhasaraNo"].ToString();
                                string Rakba = Convert.ToString(CheckNullFloat(dr["Rakba"].ToString()));
                                string Rakba_crop_sinchit = Convert.ToString(CheckNullFloat(dr["Rakba_crop_sinchit"].ToString()));
                                string Rakba_crop_asinchit = Convert.ToString(CheckNullFloat(dr["Rakba_crop_asinchit"].ToString()));
                                string Rakba_crop_sinchit_qty = Convert.ToString(CheckNullFloat(dr["Rakba_crop_sinchit_qty"].ToString()));
                                string Rakba_crop_asinchit_qty = Convert.ToString(CheckNullFloat(dr["Rakba_crop_asinchit_qty"].ToString()));
                                string Procured_qty = Convert.ToString(CheckNullFloat(dr["Procured_qty"].ToString()));
                                string crpcode = dr["crpcode"].ToString();


                                commandtrans.CommandType = CommandType.StoredProcedure;
                                commandtrans.CommandText = "Proc_InsertLandRecord";
                                commandtrans.Parameters.Clear();
                                commandtrans.Parameters.AddWithValue("@TransID", TransID);
                                commandtrans.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                                commandtrans.Parameters.AddWithValue("@Village_ID", Village_ID);
                                commandtrans.Parameters.AddWithValue("@VillageName", VillageName);
                                commandtrans.Parameters.AddWithValue("@Crop_ID", Crop_ID);
                                commandtrans.Parameters.AddWithValue("@LandOwner_Name", LandOwner_Name);
                                commandtrans.Parameters.AddWithValue("@LandOwner_RinPustikaNo", LandOwner_RinPustikaNo);
                                commandtrans.Parameters.AddWithValue("@LandType", LandType);
                                commandtrans.Parameters.AddWithValue("@KhasaraNo", KhasaraNo);
                                commandtrans.Parameters.AddWithValue("@Rakba", Rakba);
                                commandtrans.Parameters.AddWithValue("@Rakba_crop_sinchit", Rakba_crop_sinchit);
                                commandtrans.Parameters.AddWithValue("@Rakba_crop_asinchit", Rakba_crop_asinchit);
                                commandtrans.Parameters.AddWithValue("@Rakba_crop_sinchit_qty", Rakba_crop_sinchit_qty);
                                commandtrans.Parameters.AddWithValue("@Rakba_crop_asinchit_qty", Rakba_crop_asinchit_qty);
                                commandtrans.Parameters.AddWithValue("@Procured_qty", Procured_qty);
                                commandtrans.Parameters.AddWithValue("@crpcode", crpcode);
                                int rl = commandtrans.ExecuteNonQuery();

                            }

                        }

                    }
                    statu_cntr = 22;
                    trans.Commit();


                }
            }
        }

        catch (Exception ex)
        {
            trans.Rollback();
            connection.Close();
        }
        finally
        {
            connection.Close();
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();
        }
    }


    //19102012---New Create Table 

    [WebMethod(Description = " 11.0 FarmerUpdationLog   -Off2Onn")]
    public void InsertFarmerUpdationLog(DataSet dsFarmerUpdationLog, string society_ID, string District_ID, bool ChkPaddyUser)
    {
        string LogID = "";
        string date = "";
        //string Farmer_Id = "";
        try
        {
            if (connection != null && ChkPaddyUser == true)
            {
                connection.Open();
                commandtrans = connection.CreateCommand();
                trans = connection.BeginTransaction();
                commandtrans.Connection = connection;
                commandtrans.Transaction = trans;

                date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                LogID = society_ID.ToString() + date;

                if (dsFarmerUpdationLog != null)
                {


                    foreach (DataRow dr in dsFarmerUpdationLog.Tables[0].Rows)
                    {

                        string FarmerId = dr["FarmerId"].ToString();
                        string DisrictID = District_ID;
                        string SocietyID = society_ID;
                        string UpdationDate_Off = chkDate(dr["UpdationDate"].ToString());
                        commandtrans.CommandType = CommandType.Text;
                        commandtrans.CommandText = "select count(*) from FarmerUpdationLog where FarmerId='" + FarmerId + "' and DisrictID='" + DisrictID + "' and SocietyID='" + SocietyID + "'";
                        string res = commandtrans.ExecuteScalar().ToString();
                        if (Convert.ToInt32(res) <= 0)
                        {
                            string UpdationDate_Onn = System.DateTime.Now.ToString("MM/dd/yyyy");

                            commandtrans.CommandType = CommandType.Text;
                            commandtrans.CommandText = "Insert Into FarmerUpdationLog(FarmerId,DisrictID,SocietyID,UpdationDate_Off,UpdationDate_Onn)values('" + FarmerId + "','" + DisrictID + "','" + SocietyID + "','" + UpdationDate_Off + "','" + UpdationDate_Onn + "')";
                            commandtrans.Parameters.Clear();
                            int r = commandtrans.ExecuteNonQuery();

                        }

                    }
                    statu_cntr = 22;
                    trans.Commit();
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
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();
        }

    }

    //19102012
    [WebMethod(Description = "11. Updating FarmerRegistration(Personal Info) (with oldlog) In online -Off2Onn")]
    public void updateFarmerRegistration(DataSet dsFarmerInofrmation, string societyID, bool ChkPaddyUser)
    {
        string LogID = "";
        string date = "";
        //string Farmer_Id = "";
        try
        {
            if (connection != null && ChkPaddyUser == true)
            {
                connection.Open();
                commandtrans = connection.CreateCommand();
                trans = connection.BeginTransaction();
                commandtrans.Connection = connection;
                commandtrans.Transaction = trans;

                date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                LogID = societyID.ToString() + date;
                if (dsFarmerInofrmation != null)
                {
                    foreach (DataRow dr in dsFarmerInofrmation.Tables[0].Rows)
                    {
                        string Farmer_Id = dr["Farmer_Id"].ToString();
                        string District_Id = dr["District_Id"].ToString();
                        string societyid = dr["PC_ID"].ToString();
                        string Village_Id = dr["Village_Id"].ToString();
                        commandtrans.CommandType = CommandType.Text;
                        commandtrans.CommandText = "Select count(*) from FarmerRegistration_Log where Farmer_Id='" + Farmer_Id + "' and  District_Id='" + District_Id + "' ";
                        string res = commandtrans.ExecuteScalar().ToString();
                        //if (Convert.ToInt32(12321321321321321) <= 0)
                        if (Convert.ToInt64(res) <= 0)
                        {
                            string VillageName = dr["VillageName"].ToString();
                            string Tehsil_Id = dr["Tehsil_Id"].ToString();
                            string FarmerName = dr["FarmerName"].ToString();
                            string FatherHusName = dr["FatherHusName"].ToString();
                            string Gram_Panchayat = dr["Gram_Panchayat"].ToString();
                            string PatwariHalkaNo = dr["PatwariHalkaNo"].ToString();
                            string Mobileno = dr["Mobileno"].ToString();
                            string Category = dr["Category"].ToString();
                            string RinPustikaNo = dr["RinPustikaNo"].ToString();
                            string Farmer_EID_UID_No = dr["Farmer_EID_UID_No"].ToString();
                            string Farmer_BankName_New = dr["Farmer_BankName_New"].ToString();
                            string Farmer_BankBranchName = dr["Farmer_BankBranchName"].ToString();
                            string Farmer_BankAccountNo = dr["Farmer_BankAccountNo"].ToString();
                            string PC_ID = dr["PC_ID"].ToString();
                            string Procured_SocietyID = dr["Procured_SocietyID"].ToString();
                            string Procured_Dist_ID = dr["Procured_Dist_ID"].ToString();
                            string Procured_Place = dr["Procured_Place"].ToString();
                            string CropExpected_Date = chkDate(dr["CropExpected_Date"].ToString());
                            string UserID = dr["UserID"].ToString();
                            string UpdatedDate = chkDate(dr["updatedDate"].ToString());
                            string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                            string IsApproved = dr["IsApproved"].ToString();
                            string Status = dr["Status"].ToString();


                            commandtrans.CommandType = CommandType.Text;
                            commandtrans.CommandText = "Select * from FarmerRegistration where Farmer_Id='" + Farmer_Id + "' and  District_Id='" + District_Id + "' and PC_ID='" + societyID + "' ";
                            SqlDataReader dread = commandtrans.ExecuteReader();
                            if (dread != null)
                            {
                                if (dread.HasRows)
                                {
                                    dread.Read();

                                    string _Village_Id = dread["Village_Id"].ToString();
                                    string _VillageName = dread["VillageName"].ToString();
                                    string _Tehsil_Id = dread["Tehsil_Id"].ToString();
                                    string _FarmerName = dread["FarmerName"].ToString();
                                    string _FatherHusName = dread["FatherHusName"].ToString();
                                    string _Gram_Panchayat = dread["Gram_Panchayat"].ToString();
                                    string _PatwariHalkaNo = dread["PatwariHalkaNo"].ToString();
                                    string _Mobileno = dread["Mobileno"].ToString();
                                    string _Category = dread["Category"].ToString();
                                    string _RinPustikaNo = dread["RinPustikaNo"].ToString();
                                    string _Farmer_EID_UID_No = dread["Farmer_EID_UID_No"].ToString();
                                    string _Farmer_BankName_New = dread["Farmer_BankName_New"].ToString();
                                    string _Farmer_BankBranchName = dread["Farmer_BankBranchName"].ToString();
                                    string _Farmer_BankAccountNo = dread["Farmer_BankAccountNo"].ToString();
                                    string _PC_ID = dread["PC_ID"].ToString();
                                    string _Procured_SocietyID = dread["Procured_SocietyID"].ToString();
                                    string _Procured_Dist_ID = dread["Procured_Dist_ID"].ToString();
                                    string _Procured_Place = dread["Procured_Place"].ToString();
                                    string _CropExpected_Date = chkDate(dread["CropExpected_Date"].ToString());
                                    string _UserID = dread["UserID"].ToString();
                                    string _CreatedDate = chkDate(dread["CreatedDate"].ToString());
                                    string _ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                                    string _RegistrationDate = chkDate(dread["RegistrationDate"].ToString());
                                    string _IsApproved = dread["IsApproved"].ToString();
                                    string _Status = dread["Status"].ToString();

                                    dread.Close();
                                    dread.Dispose();

                                    commandtrans.CommandType = CommandType.StoredProcedure;
                                    commandtrans.CommandText = "Proc_InsertFarmerRegistration_Log";
                                    commandtrans.Parameters.Clear();
                                    commandtrans.Parameters.AddWithValue("@District_Id", District_Id);
                                    commandtrans.Parameters.AddWithValue("@Village_Id", _Village_Id);
                                    commandtrans.Parameters.AddWithValue("@VillageName", _VillageName);
                                    commandtrans.Parameters.AddWithValue("@Tehsil_Id", _Tehsil_Id);
                                    commandtrans.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                                    commandtrans.Parameters.AddWithValue("@FarmerName", _FarmerName);
                                    commandtrans.Parameters.AddWithValue("@FatherHusName", _FatherHusName);
                                    commandtrans.Parameters.AddWithValue("@Gram_Panchayat", _Gram_Panchayat);
                                    commandtrans.Parameters.AddWithValue("@PatwariHalkaNo", _PatwariHalkaNo);
                                    commandtrans.Parameters.AddWithValue("@Mobileno", _Mobileno);
                                    commandtrans.Parameters.AddWithValue("@Category", _Category);
                                    commandtrans.Parameters.AddWithValue("@RinPustikaNo", _RinPustikaNo);
                                    commandtrans.Parameters.AddWithValue("@Farmer_EID_UID_No", _Farmer_EID_UID_No);
                                    commandtrans.Parameters.AddWithValue("@Farmer_BankName_New", _Farmer_BankName_New);
                                    commandtrans.Parameters.AddWithValue("@Farmer_BankBranchName", _Farmer_BankBranchName);
                                    commandtrans.Parameters.AddWithValue("@Farmer_BankAccountNo", _Farmer_BankAccountNo);
                                    commandtrans.Parameters.AddWithValue("@PC_ID", _PC_ID);
                                    commandtrans.Parameters.AddWithValue("@Procured_SocietyID", _Procured_SocietyID);
                                    commandtrans.Parameters.AddWithValue("@Procured_Dist_ID", _Procured_Dist_ID);
                                    commandtrans.Parameters.AddWithValue("@Procured_Place", _Procured_Place);
                                    commandtrans.Parameters.AddWithValue("@CropExpected_Date", _CropExpected_Date);
                                    commandtrans.Parameters.AddWithValue("@CreatedDate", _CreatedDate);
                                    commandtrans.Parameters.AddWithValue("@ip", _ip);
                                    commandtrans.Parameters.AddWithValue("@RegistrationDate", _RegistrationDate);
                                    commandtrans.Parameters.AddWithValue("@IsApproved", _IsApproved);
                                    commandtrans.Parameters.AddWithValue("@UserID", _UserID);
                                    commandtrans.Parameters.AddWithValue("@Status", _Status);
                                    int xe = commandtrans.ExecuteNonQuery();

                                }

                                dread.Close();
                                dread.Dispose();


                            }

                            commandtrans.CommandType = CommandType.StoredProcedure;
                            commandtrans.CommandText = "proc_updFarmerInfo";
                            commandtrans.Parameters.Clear();
                            commandtrans.Parameters.AddWithValue("@District_Id", District_Id);
                            //commandtrans.Parameters.AddWithValue("@Village_Id", Village_Id);
                            commandtrans.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                            //commandtrans.Parameters.AddWithValue("@FarmerName", FarmerName);
                            //commandtrans.Parameters.AddWithValue("@FatherHusName", FatherHusName);
                            commandtrans.Parameters.AddWithValue("@Gram_Panchayat", Gram_Panchayat);
                            commandtrans.Parameters.AddWithValue("@PatwariHalkaNo", PatwariHalkaNo);
                            commandtrans.Parameters.AddWithValue("@Mobileno", Mobileno);
                            commandtrans.Parameters.AddWithValue("@Category", Category);
                            commandtrans.Parameters.AddWithValue("@RinPustikaNo", RinPustikaNo);
                            commandtrans.Parameters.AddWithValue("@Farmer_EID_UID_No", Farmer_EID_UID_No);
                            commandtrans.Parameters.AddWithValue("@Farmer_BankName_New", Farmer_BankName_New);
                            commandtrans.Parameters.AddWithValue("@Farmer_BankBranchName", Farmer_BankBranchName);
                            commandtrans.Parameters.AddWithValue("@Farmer_BankAccountNo", Farmer_BankAccountNo);
                            commandtrans.Parameters.AddWithValue("@PC_ID", PC_ID);
                            commandtrans.Parameters.AddWithValue("@Procured_SocietyID", Procured_SocietyID);
                            commandtrans.Parameters.AddWithValue("@Procured_Dist_ID", Procured_Dist_ID);
                            commandtrans.Parameters.AddWithValue("@Procured_Place", Procured_Place);
                            commandtrans.Parameters.AddWithValue("@CropExpected_Date", CropExpected_Date);
                            commandtrans.Parameters.AddWithValue("@updatedDate", UpdatedDate);
                            commandtrans.Parameters.AddWithValue("@ip", ip);
                            commandtrans.Parameters.AddWithValue("@IsApproved", IsApproved);
                            commandtrans.Parameters.AddWithValue("@UserID", UserID);
                            commandtrans.Parameters.AddWithValue("@Status", Status);
                            int xs = commandtrans.ExecuteNonQuery();

                        }
                        else
                        {

                        }

                    }
                    statu_cntr = 25;
                    trans.Commit();
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
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();
        }


    }



    //19102012
    [WebMethod(Description = "12. Updating FarmerRegistration(Land Record) In online -Off2Onn")]
    public void updateFarmerLandRecord_Description(DataSet dsLI, string societyID, bool ChkPaddyUser)
    {
        string LogID = "";
        string date = "";
        string farmerid = "";
        try
        {
            if (connection != null && ChkPaddyUser == true)
            {
                connection.Open();
                commandtrans = connection.CreateCommand();
                trans = connection.BeginTransaction();
                commandtrans.Connection = connection;
                commandtrans.Transaction = trans;
                date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                LogID = societyID.ToString() + date;
                if (dsLI != null)
                {
                    foreach (DataRow dr in dsLI.Tables[0].Rows)
                    {
                        string TransID = dr["TransID"].ToString();
                        string Farmer_Id = dr["Farmer_Id"].ToString();
                        commandtrans.Transaction = trans;
                        commandtrans.CommandType = CommandType.Text;
                        commandtrans.CommandText = "Select count(*) from Farmer_LandRecordDescription_Log where TransID='" + TransID + "' and Farmer_Id='" + Farmer_Id + "'";
                        string res = commandtrans.ExecuteScalar().ToString();
                        if (Convert.ToInt32(res) <= 0)
                        {
                            commandtrans.CommandType = CommandType.Text;
                            commandtrans.CommandText = "select * from Farmer_LandRecordDescription where TransID='" + TransID + "' and Farmer_Id='" + Farmer_Id + "'";
                            SqlDataReader dred = commandtrans.ExecuteReader();
                            if (dred != null)
                            {
                                if (dred.HasRows)
                                {
                                    dred.Read();
                                    string _TransID = dred["TransID"].ToString();
                                    string _Farmer_Id = dred["Farmer_Id"].ToString();
                                    string _Village_ID = dred["Village_ID"].ToString();
                                    string _VillageName = dred["VillageName"].ToString();
                                    string _Crop_ID = dred["Crop_ID"].ToString();
                                    string _LandOwner_Name = dred["LandOwner_Name"].ToString();
                                    string _LandOwner_RinPustikaNo = dred["LandOwner_RinPustikaNo"].ToString();
                                    string _LandType = dred["LandType"].ToString();
                                    string _KhasaraNo = dred["KhasaraNo"].ToString();
                                    string _Rakba = Convert.ToString(CheckNullFloat(dred["Rakba"].ToString()));
                                    string _Rakba_crop_sinchit = Convert.ToString(CheckNullFloat(dred["Rakba_crop_sinchit"].ToString()));
                                    string _Rakba_crop_asinchit = Convert.ToString(CheckNullFloat(dred["Rakba_crop_asinchit"].ToString()));
                                    string _Rakba_crop_sinchit_qty = Convert.ToString(CheckNullFloat(dred["Rakba_crop_sinchit_qty"].ToString()));
                                    string _Rakba_crop_asinchit_qty = Convert.ToString(CheckNullFloat(dred["Rakba_crop_asinchit_qty"].ToString()));
                                    string _Procured_qty = Convert.ToString(CheckNullFloat(dred["Procured_qty"].ToString()));
                                    string _crpcode = dred["crpcode"].ToString();
                                    dred.Close();
                                    dred.Dispose();

                                    commandtrans.CommandType = CommandType.StoredProcedure;
                                    commandtrans.CommandText = "Proc_InsertFarmerLandRecordDescription_Log";
                                    commandtrans.Parameters.Clear();
                                    commandtrans.Parameters.AddWithValue("@TransID", _TransID);
                                    commandtrans.Parameters.AddWithValue("@Farmer_Id", _Farmer_Id);
                                    commandtrans.Parameters.AddWithValue("@Village_ID", _Village_ID);
                                    commandtrans.Parameters.AddWithValue("@VillageName", _VillageName);
                                    commandtrans.Parameters.AddWithValue("@Crop_ID", _Crop_ID);
                                    commandtrans.Parameters.AddWithValue("@LandOwner_Name", _LandOwner_Name);
                                    commandtrans.Parameters.AddWithValue("@LandOwner_RinPustikaNo", _LandOwner_RinPustikaNo);
                                    commandtrans.Parameters.AddWithValue("@LandType", _LandType);
                                    commandtrans.Parameters.AddWithValue("@KhasaraNo", _KhasaraNo);
                                    commandtrans.Parameters.AddWithValue("@Rakba", _Rakba);
                                    commandtrans.Parameters.AddWithValue("@Rakba_crop_sinchit", _Rakba_crop_sinchit);
                                    commandtrans.Parameters.AddWithValue("@Rakba_crop_asinchit", _Rakba_crop_asinchit);
                                    commandtrans.Parameters.AddWithValue("@Rakba_crop_sinchit_qty", _Rakba_crop_sinchit_qty);
                                    commandtrans.Parameters.AddWithValue("@Rakba_crop_asinchit_qty", _Rakba_crop_asinchit_qty);
                                    commandtrans.Parameters.AddWithValue("@Procured_qty", _Procured_qty);
                                    commandtrans.Parameters.AddWithValue("@crpcode", _crpcode);
                                    int rl = commandtrans.ExecuteNonQuery();

                                }
                                dred.Close();
                                dred.Dispose();

                            }
                            string Village_ID = dr["Village_ID"].ToString();
                            string VillageName = dr["VillageName"].ToString();
                            string Crop_ID = dr["Crop_ID"].ToString();
                            string LandOwner_Name = dr["LandOwner_Name"].ToString();
                            string LandOwner_RinPustikaNo = dr["LandOwner_RinPustikaNo"].ToString();
                            string LandType = dr["LandType"].ToString();
                            string KhasaraNo = dr["KhasaraNo"].ToString();
                            string Rakba = Convert.ToString(CheckNullFloat(dr["Rakba"].ToString()));
                            string Rakba_crop_sinchit = Convert.ToString(CheckNullFloat(dr["Rakba_crop_sinchit"].ToString()));
                            string Rakba_crop_asinchit = Convert.ToString(CheckNullFloat(dr["Rakba_crop_asinchit"].ToString()));
                            string Rakba_crop_sinchit_qty = Convert.ToString(CheckNullFloat(dr["Rakba_crop_sinchit_qty"].ToString()));
                            string Rakba_crop_asinchit_qty = Convert.ToString(CheckNullFloat(dr["Rakba_crop_asinchit_qty"].ToString()));
                            string Procured_qty = Convert.ToString(CheckNullFloat(dr["Procured_qty"].ToString()));
                            string crpcode = dr["crpcode"].ToString();
                            commandtrans.Connection = connection;
                            commandtrans.CommandType = CommandType.StoredProcedure;
                            commandtrans.Parameters.Clear();
                            commandtrans.CommandText = "Proc_updFarmerLandrecord";
                            commandtrans.Parameters.AddWithValue("@TransID", TransID);
                            commandtrans.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                            commandtrans.Parameters.AddWithValue("@Village_ID", Village_ID);
                            commandtrans.Parameters.AddWithValue("@VillageName", VillageName);
                            commandtrans.Parameters.AddWithValue("@Crop_ID", Crop_ID);
                            commandtrans.Parameters.AddWithValue("@LandOwner_Name", LandOwner_Name);
                            commandtrans.Parameters.AddWithValue("@LandOwner_RinPustikaNo", LandOwner_RinPustikaNo);
                            commandtrans.Parameters.AddWithValue("@LandType", LandType);
                            commandtrans.Parameters.AddWithValue("@KhasaraNo", KhasaraNo);
                            commandtrans.Parameters.AddWithValue("@Rakba", Rakba);
                            commandtrans.Parameters.AddWithValue("@Rakba_crop_sinchit", Rakba_crop_sinchit);
                            commandtrans.Parameters.AddWithValue("@Rakba_crop_asinchit", Rakba_crop_asinchit);
                            commandtrans.Parameters.AddWithValue("@Rakba_crop_sinchit_qty", Rakba_crop_sinchit_qty);
                            commandtrans.Parameters.AddWithValue("@Rakba_crop_asinchit_qty", Rakba_crop_asinchit_qty);
                            commandtrans.Parameters.AddWithValue("@Procured_qty", Procured_qty);
                            commandtrans.Parameters.AddWithValue("@crpcode", crpcode);
                            int rls = commandtrans.ExecuteNonQuery();

                        }
                    }
                    statu_cntr = 28;
                    trans.Commit();

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
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();
        }
    }


    //19102012
    [WebMethod(Description = "9. Society Change Offline insert(Farmer Registration) SocietyFarmerchangeLog -Off2Onn")]
    public void InsertFarmerRegistrationSocietyChange_Log(DataSet dsFarmerListSoc, string societyID, string DistrictID, bool ChkPaddyUser)
    {
        string LogID = "";
        string date = "";
        //  string Farmer_Id = "";
        try
        {

            if (connection != null && ChkPaddyUser == true)
            {
                connection.Open();
                commandtrans = connection.CreateCommand();
                trans = connection.BeginTransaction();
                commandtrans.Connection = connection;
                commandtrans.Transaction = trans;

                date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                LogID = societyID.ToString() + date;
                if (dsFarmerListSoc != null)
                {
                    foreach (DataRow dr in dsFarmerListSoc.Tables[0].Rows)
                    {
                        string District_ID = dr["District_Id"].ToString();
                        string Farmer_ID = dr["Farmer_Id"].ToString();
                        string OLD_PC_ID = societyID;
                        //change because of continu inserting record 
                        //string NEW_PC_ID = dr["PC_ID"].ToString(); Procured_SocietyID
                        string NEW_PC_ID = dr["Procured_SocietyID"].ToString();
                        commandtrans.CommandType = CommandType.Text;
                        commandtrans.CommandText = "Select count(*) From SocietyChangeLog where Farmer_ID='" + Farmer_ID + "' and OLD_PC_ID='" + OLD_PC_ID + "' and NEW_PC_ID='" + NEW_PC_ID + "' and  District_ID='" + District_ID + "'";
                        int res = Convert.ToInt16(commandtrans.ExecuteScalar());
                        if (res <= 0)
                        {

                            string District_Id = dr["District_Id"].ToString();
                            string Village_Id = dr["Village_Id"].ToString();
                            string VillageName = dr["VillageName"].ToString();
                            string Tehsil_Id = dr["Tehsil_Id"].ToString();
                            string FarmerName = dr["FarmerName"].ToString();
                            string FatherHusName = dr["FatherHusName"].ToString();
                            string Gram_Panchayat = dr["Gram_Panchayat"].ToString();
                            string PatwariHalkaNo = dr["PatwariHalkaNo"].ToString();
                            string Mobileno = dr["Mobileno"].ToString();
                            string Category = dr["Category"].ToString();
                            string RinPustikaNo = dr["RinPustikaNo"].ToString();
                            string Farmer_EID_UID_No = dr["Farmer_EID_UID_No"].ToString();
                            string Farmer_BankName_New = dr["Farmer_BankName_New"].ToString();
                            string Farmer_BankBranchName = dr["Farmer_BankBranchName"].ToString();
                            string Farmer_BankAccountNo = dr["Farmer_BankAccountNo"].ToString();
                            string PC_ID = dr["PC_ID"].ToString();
                            string Procured_SocietyID = dr["Procured_SocietyID"].ToString();
                            string Procured_Dist_ID = dr["Procured_Dist_ID"].ToString();
                            string Procured_Place = dr["Procured_Place"].ToString();
                            string CropExpected_Date = chkDate(dr["CropExpected_Date"].ToString());
                            string UserID = dr["UserID"].ToString();
                            string CreatedDate = chkDate(dr["CreatedDate"].ToString());
                            string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                            string RegistrationDate = chkDate(dr["RegistrationDate"].ToString());
                            string IsApproved = dr["IsApproved"].ToString();
                            string Status = dr["Status"].ToString();


                            commandtrans.CommandType = CommandType.StoredProcedure;
                            commandtrans.CommandText = "Proc_InsertFarmerRegistrationSocietyChange_Log";
                            commandtrans.Parameters.Clear();
                            commandtrans.Parameters.AddWithValue("@District_Id", District_Id);
                            commandtrans.Parameters.AddWithValue("@Village_Id", Village_Id);
                            commandtrans.Parameters.AddWithValue("@VillageName", VillageName);
                            commandtrans.Parameters.AddWithValue("@Tehsil_Id", Tehsil_Id);
                            commandtrans.Parameters.AddWithValue("@Farmer_Id", Farmer_ID);
                            commandtrans.Parameters.AddWithValue("@FarmerName", FarmerName);
                            commandtrans.Parameters.AddWithValue("@FatherHusName", FatherHusName);
                            commandtrans.Parameters.AddWithValue("@Gram_Panchayat", Gram_Panchayat);
                            commandtrans.Parameters.AddWithValue("@PatwariHalkaNo", PatwariHalkaNo);
                            commandtrans.Parameters.AddWithValue("@Mobileno", Mobileno);
                            commandtrans.Parameters.AddWithValue("@Category", Category);
                            commandtrans.Parameters.AddWithValue("@RinPustikaNo", RinPustikaNo);
                            commandtrans.Parameters.AddWithValue("@Farmer_EID_UID_No", Farmer_EID_UID_No);
                            commandtrans.Parameters.AddWithValue("@Farmer_BankName_New", Farmer_BankName_New);
                            commandtrans.Parameters.AddWithValue("@Farmer_BankBranchName", Farmer_BankBranchName);
                            commandtrans.Parameters.AddWithValue("@Farmer_BankAccountNo", Farmer_BankAccountNo);
                            // commandtrans.Parameters.AddWithValue("@PC_ID", PC_ID);
                            //change because of offline 20102012
                            commandtrans.Parameters.AddWithValue("@PC_ID", Procured_SocietyID);
                            commandtrans.Parameters.AddWithValue("@Procured_SocietyID", Procured_SocietyID);
                            commandtrans.Parameters.AddWithValue("@Procured_Dist_ID", Procured_Dist_ID);
                            commandtrans.Parameters.AddWithValue("@Procured_Place", Procured_Place);
                            commandtrans.Parameters.AddWithValue("@CropExpected_Date", CropExpected_Date);
                            commandtrans.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                            commandtrans.Parameters.AddWithValue("@ip", ip);
                            commandtrans.Parameters.AddWithValue("@RegistrationDate", RegistrationDate);
                            commandtrans.Parameters.AddWithValue("@IsApproved", IsApproved);
                            commandtrans.Parameters.AddWithValue("@UserID", UserID);
                            commandtrans.Parameters.AddWithValue("@Status", Status);
                            int x = commandtrans.ExecuteNonQuery();



                            string _Farmer_Id = dr["Farmer_ID"].ToString();
                            string _District_Id = dr["District_Id"].ToString();
                            string _VillageName = dr["VillageName"].ToString();
                            string _Tehsil_Id = dr["Tehsil_Id"].ToString();
                            string _FarmerName = dr["FarmerName"].ToString();
                            string _FatherHusName = dr["FatherHusName"].ToString();
                            string _Gram_Panchayat = dr["Gram_Panchayat"].ToString();
                            string _PatwariHalkaNo = dr["PatwariHalkaNo"].ToString();
                            string _Mobileno = dr["Mobileno"].ToString();
                            string _Category = dr["Category"].ToString();
                            string _RinPustikaNo = dr["RinPustikaNo"].ToString();
                            string _Farmer_EID_UID_No = dr["Farmer_EID_UID_No"].ToString();
                            string _Farmer_BankName_New = dr["Farmer_BankName_New"].ToString();
                            string _Farmer_BankBranchName = dr["Farmer_BankBranchName"].ToString();
                            string _Farmer_BankAccountNo = dr["Farmer_BankAccountNo"].ToString();
                            string _PC_ID = dr["PC_ID"].ToString();
                            string _Procured_SocietyID = dr["Procured_SocietyID"].ToString();
                            string _Procured_Dist_ID = dr["Procured_Dist_ID"].ToString();
                            string _Procured_Place = dr["Procured_Place"].ToString();
                            string _CropExpected_Date = chkDate(dr["CropExpected_Date"].ToString());
                            string _UserID = dr["UserID"].ToString();
                            string _UpdatedDate = chkDate(dr["updatedDate"].ToString());
                            string _ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                            string _IsApproved = dr["IsApproved"].ToString();
                            string _Status = dr["Status"].ToString();

                            commandtrans.CommandType = CommandType.StoredProcedure;
                            commandtrans.CommandText = "proc_updFarmerInfo";
                            commandtrans.Parameters.Clear();
                            commandtrans.Parameters.AddWithValue("@District_Id", _District_Id);
                            //commandtrans.Parameters.AddWithValue("@Village_Id", Village_Id);
                            commandtrans.Parameters.AddWithValue("@Farmer_Id", _Farmer_Id);
                            //commandtrans.Parameters.AddWithValue("@FarmerName", FarmerName);
                            //commandtrans.Parameters.AddWithValue("@FatherHusName", FatherHusName);
                            commandtrans.Parameters.AddWithValue("@Gram_Panchayat", _Gram_Panchayat);
                            commandtrans.Parameters.AddWithValue("@PatwariHalkaNo", _PatwariHalkaNo);
                            commandtrans.Parameters.AddWithValue("@Mobileno", _Mobileno);
                            commandtrans.Parameters.AddWithValue("@Category", _Category);
                            commandtrans.Parameters.AddWithValue("@RinPustikaNo", _RinPustikaNo);
                            commandtrans.Parameters.AddWithValue("@Farmer_EID_UID_No", _Farmer_EID_UID_No);
                            commandtrans.Parameters.AddWithValue("@Farmer_BankName_New", _Farmer_BankName_New);
                            commandtrans.Parameters.AddWithValue("@Farmer_BankBranchName", _Farmer_BankBranchName);
                            commandtrans.Parameters.AddWithValue("@Farmer_BankAccountNo", _Farmer_BankAccountNo);

                            //commandtrans.Parameters.AddWithValue("@PC_ID", PC_ID);
                            //change becaue of in offline  
                            commandtrans.Parameters.AddWithValue("@PC_ID", Procured_SocietyID);
                            commandtrans.Parameters.AddWithValue("@Procured_SocietyID", Procured_SocietyID);
                            commandtrans.Parameters.AddWithValue("@Procured_Dist_ID", Procured_Dist_ID);
                            commandtrans.Parameters.AddWithValue("@Procured_Place", Procured_Place);

                            commandtrans.Parameters.AddWithValue("@CropExpected_Date", _CropExpected_Date);
                            commandtrans.Parameters.AddWithValue("@updatedDate", _UpdatedDate);
                            commandtrans.Parameters.AddWithValue("@ip", _ip);
                            commandtrans.Parameters.AddWithValue("@IsApproved", _IsApproved);
                            commandtrans.Parameters.AddWithValue("@UserID", _UserID);
                            commandtrans.Parameters.AddWithValue("@Status", _Status);
                            int xs = commandtrans.ExecuteNonQuery();
                        }
                        else
                        {
                            //Update 

                        }

                    }
                    statu_cntr = 30;
                    trans.Commit();
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
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();
        }


    }


    //19102012
    [WebMethod(Description = "10. Society Change Offline insert(Farmer LandRecord) SocietyFarmerchangeLog -Off2Onn")]
    public void InsertFarmer_LandRecordDescriptionSocietyChange_Log(DataSet dsFarmerLandInfo, string societyID, string DistrictID, bool ChkPaddyUser)
    {
        string LogID = "";
        string date = "";
        string Farmer_ID = "";
        try
        {
            if (connection != null && ChkPaddyUser == true)
            {
                connection.Open();
                commandtrans = connection.CreateCommand();
                trans = connection.BeginTransaction();
                commandtrans.Connection = connection;
                commandtrans.Transaction = trans;

                date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                LogID = societyID.ToString() + date;

                if (dsFarmerLandInfo != null)
                {
                    foreach (DataRow dr in dsFarmerLandInfo.Tables[0].Rows)
                    {
                        Farmer_ID = dr["Farmer_ID"].ToString();
                        //string mid = Farmer_ID.Substring(1, 5);
                        //if (Farmer_ID.Substring(1, 5) != "36001")
                        //{
                        string TransID = dr["TransID"].ToString();
                        commandtrans.CommandType = CommandType.Text;
                        commandtrans.CommandText = "Select count(*) From Farmer_LandRecordDescriptionSocietyChange_Log where Farmer_ID='" + Farmer_ID + "' and TransID='" + TransID + "'  ";
                        int res = Convert.ToInt16(commandtrans.ExecuteScalar());
                        if (res <= 0)
                        {

                            string Village_ID = dr["Village_ID"].ToString();
                            string VillageName = dr["VillageName"].ToString();
                            string Crop_ID = dr["Crop_ID"].ToString();
                            string LandOwner_Name = dr["LandOwner_Name"].ToString();
                            string LandOwner_RinPustikaNo = dr["LandOwner_RinPustikaNo"].ToString();
                            string LandType = dr["LandType"].ToString();
                            string KhasaraNo = dr["KhasaraNo"].ToString();
                            string Rakba = Convert.ToString(CheckNullFloat(dr["Rakba"].ToString()));
                            string Rakba_crop_sinchit = Convert.ToString(CheckNullFloat(dr["Rakba_crop_sinchit"].ToString()));
                            string Rakba_crop_asinchit = Convert.ToString(CheckNullFloat(dr["Rakba_crop_asinchit"].ToString()));
                            string Rakba_crop_sinchit_qty = Convert.ToString(CheckNullFloat(dr["Rakba_crop_sinchit_qty"].ToString()));
                            string Rakba_crop_asinchit_qty = Convert.ToString(CheckNullFloat(dr["Rakba_crop_asinchit_qty"].ToString()));
                            string Procured_qty = Convert.ToString(CheckNullFloat(dr["Procured_qty"].ToString()));
                            string crpcode = dr["crpcode"].ToString();


                            commandtrans.CommandType = CommandType.StoredProcedure;
                            commandtrans.CommandText = "Proc_InsertFarmer_LandRecordDescriptionSocietyChange_Log";
                            commandtrans.Parameters.Clear();
                            commandtrans.Parameters.AddWithValue("@TransID", TransID);
                            commandtrans.Parameters.AddWithValue("@Farmer_Id", Farmer_ID);
                            commandtrans.Parameters.AddWithValue("@Village_ID", Village_ID);
                            commandtrans.Parameters.AddWithValue("@VillageName", VillageName);
                            commandtrans.Parameters.AddWithValue("@Crop_ID", Crop_ID);
                            commandtrans.Parameters.AddWithValue("@LandOwner_Name", LandOwner_Name);
                            commandtrans.Parameters.AddWithValue("@LandOwner_RinPustikaNo", LandOwner_RinPustikaNo);
                            commandtrans.Parameters.AddWithValue("@LandType", LandType);
                            commandtrans.Parameters.AddWithValue("@KhasaraNo", KhasaraNo);
                            commandtrans.Parameters.AddWithValue("@Rakba", Rakba);
                            commandtrans.Parameters.AddWithValue("@Rakba_crop_sinchit", Rakba_crop_sinchit);
                            commandtrans.Parameters.AddWithValue("@Rakba_crop_asinchit", Rakba_crop_asinchit);
                            commandtrans.Parameters.AddWithValue("@Rakba_crop_sinchit_qty", Rakba_crop_sinchit_qty);
                            commandtrans.Parameters.AddWithValue("@Rakba_crop_asinchit_qty", Rakba_crop_asinchit_qty);
                            commandtrans.Parameters.AddWithValue("@Procured_qty", Procured_qty);
                            commandtrans.Parameters.AddWithValue("@crpcode", crpcode);
                            int rl = commandtrans.ExecuteNonQuery();


                            //string TransID = dr["TransID"].ToString();
                            string _Village_ID = dr["Village_ID"].ToString();
                            string _VillageName = dr["VillageName"].ToString();
                            string _Crop_ID = dr["Crop_ID"].ToString();
                            string _LandOwner_Name = dr["LandOwner_Name"].ToString();
                            string _LandOwner_RinPustikaNo = dr["LandOwner_RinPustikaNo"].ToString();
                            string _LandType = dr["LandType"].ToString();
                            string _KhasaraNo = dr["KhasaraNo"].ToString();
                            string _Rakba = Convert.ToString(CheckNullFloat(dr["Rakba"].ToString()));
                            string _Rakba_crop_sinchit = Convert.ToString(CheckNullFloat(dr["Rakba_crop_sinchit"].ToString()));
                            string _Rakba_crop_asinchit = Convert.ToString(CheckNullFloat(dr["Rakba_crop_asinchit"].ToString()));
                            string _Rakba_crop_sinchit_qty = Convert.ToString(CheckNullFloat(dr["Rakba_crop_sinchit_qty"].ToString()));
                            string _Rakba_crop_asinchit_qty = Convert.ToString(CheckNullFloat(dr["Rakba_crop_asinchit_qty"].ToString()));
                            string _Procured_qty = Convert.ToString(CheckNullFloat(dr["Procured_qty"].ToString()));
                            string _crpcode = dr["crpcode"].ToString();
                            commandtrans.Connection = connection;
                            commandtrans.CommandType = CommandType.StoredProcedure;
                            commandtrans.Parameters.Clear();
                            commandtrans.CommandText = "Proc_updFarmerLandrecord";
                            commandtrans.Parameters.AddWithValue("@TransID", TransID);
                            commandtrans.Parameters.AddWithValue("@Farmer_Id", Farmer_ID);
                            commandtrans.Parameters.AddWithValue("@Village_ID", _Village_ID);
                            commandtrans.Parameters.AddWithValue("@VillageName", _VillageName);
                            commandtrans.Parameters.AddWithValue("@Crop_ID", _Crop_ID);
                            commandtrans.Parameters.AddWithValue("@LandOwner_Name", _LandOwner_Name);
                            commandtrans.Parameters.AddWithValue("@LandOwner_RinPustikaNo", _LandOwner_RinPustikaNo);
                            commandtrans.Parameters.AddWithValue("@LandType", _LandType);
                            commandtrans.Parameters.AddWithValue("@KhasaraNo", _KhasaraNo);
                            commandtrans.Parameters.AddWithValue("@Rakba", _Rakba);
                            commandtrans.Parameters.AddWithValue("@Rakba_crop_sinchit", _Rakba_crop_sinchit);
                            commandtrans.Parameters.AddWithValue("@Rakba_crop_asinchit", _Rakba_crop_asinchit);
                            commandtrans.Parameters.AddWithValue("@Rakba_crop_sinchit_qty", _Rakba_crop_sinchit_qty);
                            commandtrans.Parameters.AddWithValue("@Rakba_crop_asinchit_qty", _Rakba_crop_asinchit_qty);
                            commandtrans.Parameters.AddWithValue("@Procured_qty", _Procured_qty);
                            commandtrans.Parameters.AddWithValue("@crpcode", _crpcode);
                            int rls = commandtrans.ExecuteNonQuery();

                        }
                        // }
                    }
                    statu_cntr = 32;
                    trans.Commit();
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
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();
        }

    }



    //19102012
    [WebMethod(Description = "8. Society Change Offline -Off2Onn")]
    public void InsertSocietyChangeLog(DataSet dsFarmerListSoc, string societyID, string DistrictID, bool ChkPaddyUser)
    {
        string LogID = "";
        string date = "";

        try
        {
            if (dsFarmerListSoc != null)
            {
                if (connection != null)
                {
                    connection.Open();
                    commandtrans = connection.CreateCommand();
                    trans = connection.BeginTransaction();
                    commandtrans.Connection = connection;
                    commandtrans.Transaction = trans;
                    date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                    LogID = societyID.ToString() + date;
                    foreach (DataRow dr in dsFarmerListSoc.Tables[0].Rows)
                    {
                        string District_ID = dr["District_Id"].ToString();
                        string Farmer_ID = dr["Farmer_Id"].ToString();
                        string OLD_PC_ID = societyID;


                        //string NEW_PC_ID = dr["PC_ID"].ToString();

                        //change on 19102012 at 7/46
                        string NEW_PC_ID = dr["Procured_SocietyID"].ToString();

                        commandtrans.CommandType = CommandType.Text;
                        commandtrans.CommandText = "Select count(*) From SocietyChangeLog where Farmer_ID='" + Farmer_ID + "' and OLD_PC_ID='" + OLD_PC_ID + "' and NEW_PC_ID='" + NEW_PC_ID + "' and  District_ID='" + District_ID + "'";
                        int res = Convert.ToInt16(commandtrans.ExecuteScalar());
                        if (res <= 0)
                        {
                            commandtrans.CommandType = CommandType.StoredProcedure;
                            commandtrans.CommandText = "Proc_InsertSocietyChangeLog";
                            commandtrans.Parameters.Clear();
                            commandtrans.Parameters.AddWithValue("@District_ID", District_ID);
                            commandtrans.Parameters.AddWithValue("@Farmer_ID", Farmer_ID);
                            commandtrans.Parameters.AddWithValue("@OLD_PC_ID", OLD_PC_ID);
                            commandtrans.Parameters.AddWithValue("@NEW_PC_ID", NEW_PC_ID);
                            int retv = commandtrans.ExecuteNonQuery();
                        }

                    }
                    statu_cntr = 35;
                    trans.Commit();
                }
            }
        }
        catch (Exception ex)
        {
            trans.Rollback();
            connection.Close();
        }
        finally
        {
            connection.Close();
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();
        }

    }



    //19102012
    [WebMethod(Description = "4. CommodityReceived From Farmer-  HostDate---22102012  Off2Onn")]
    public void InsertCommodityReceivedFromFarmer(DataSet ds, string DistrictId, string societyID, bool ChkPaddyUser)
    {
        res = "";
        reint = 0;
        string LogID = "";
        string date = "";
        string Farmer_Id = "";
        try
        {
            if (connection != null && ChkPaddyUser == true)
            {
                connection.Open();
                if (ds != null)
                {

                    commandtrans = connection.CreateCommand();
                    trans = connection.BeginTransaction();
                    commandtrans.Connection = connection;
                    commandtrans.Transaction = trans;
                    date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                    LogID = societyID.ToString() + date;

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {


                        Farmer_Id = dr["Farmer_Id"].ToString();
                        string District_Id = dr["District_Id"].ToString();
                        string ReceivedID = dr["ReceivedID"].ToString();
                        string SocietyID = dr["Society_Id"].ToString();

                        commandtrans.CommandType = CommandType.Text;
                        commandtrans.CommandText = "select count(*) from CommodityReceivedFromFarmer where ReceivedID='" + dr["ReceivedID"].ToString() + "' and Farmer_Id='" + dr["Farmer_Id"].ToString() + "' and District_Id ='" + dr["District_Id"].ToString() + "' and Society_Id ='" + dr["Society_Id"].ToString() + "' ";
                        res = commandtrans.ExecuteScalar().ToString();
                        reint = Convert.ToInt16(commandtrans.ExecuteScalar());
                        if (reint <= 0)
                        {
                            //Insert In Online 
                            string Proc_AgID = dr["Proc_AgID"].ToString();
                            string Society_Id = dr["Society_Id"].ToString();
                            string CropYear = dr["CropYear"].ToString();

                            string MarketingSeasonId = dr["MarketingSeasonId"].ToString();
                            string TotaAmountPayableToFarmer = dr["TotaAmountPayableToFarmer"].ToString();
                            string TaulPatrakNo = dr["TaulPatrakNo"].ToString();
                            string FarmerLoanFromSc = dr["FarmerLoanFromSc"].ToString();
                            string FarmerLoanFromBank = dr["FarmerLoanFromBank"].ToString();
                            string AmtAgainstSCCredit = dr["AmtAgainstSCCredit"].ToString();
                            string AmtAgainstBankCredit = dr["AmtAgainstBankCredit"].ToString();
                            string Irrigation_Loan = dr["Irrigation_Loan"].ToString();
                            string AmtAgIrg_Loan = dr["AmtAgIrg_Loan"].ToString();
                            string NetAmountPayableToFarmer = dr["NetAmountPayableToFarmer"].ToString();

                            string Date_Of_Receipt = chkDate(dr["Date_Of_Receipt"].ToString());


                            string Date_Of_Creation = chkDate(dr["Date_Of_Creation"].ToString());


                            // string Date_Of_Updation = getDate_MDY(dr["Date_Of_Updation"].ToString());
                            string Status = dr["Status"].ToString();
                            string UserId = dr["UserId"].ToString();

                            commandtrans.CommandType = CommandType.StoredProcedure;
                            commandtrans.CommandText = "Proc_InsertCommodityReceivedFromFarmer";
                            commandtrans.Parameters.Clear();
                            commandtrans.Parameters.AddWithValue("@ReceivedID", ReceivedID);
                            commandtrans.Parameters.AddWithValue("@District_Id", District_Id);
                            commandtrans.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                            commandtrans.Parameters.AddWithValue("@Proc_AgID", Proc_AgID);
                            commandtrans.Parameters.AddWithValue("@Society_Id", Society_Id);
                            // commandtrans.Parameters.AddWithValue("@PCID", PCID);
                            commandtrans.Parameters.AddWithValue("@CropYear", CropYear);
                            commandtrans.Parameters.AddWithValue("@MarketingSeasonId", MarketingSeasonId);
                            commandtrans.Parameters.AddWithValue("@TotaAmountPayableToFarmer", TotaAmountPayableToFarmer);
                            commandtrans.Parameters.AddWithValue("@TaulPatrakNo", TaulPatrakNo);
                            commandtrans.Parameters.AddWithValue("@FarmerLoanFromSc", FarmerLoanFromSc);
                            commandtrans.Parameters.AddWithValue("@FarmerLoanFromBank", FarmerLoanFromBank);
                            commandtrans.Parameters.AddWithValue("@AmtAgainstSCCredit", AmtAgainstSCCredit);
                            commandtrans.Parameters.AddWithValue("@AmtAgainstBankCredit", AmtAgainstBankCredit);
                            commandtrans.Parameters.AddWithValue("@Irrigation_Loan", Irrigation_Loan);
                            commandtrans.Parameters.AddWithValue("@AmtAgIrg_Loan", AmtAgIrg_Loan);
                            commandtrans.Parameters.AddWithValue("@NetAmountPayableToFarmer", NetAmountPayableToFarmer);
                            commandtrans.Parameters.AddWithValue("@Date_Of_Receipt", Date_Of_Receipt);
                            commandtrans.Parameters.AddWithValue("@Date_Of_Creation", Date_Of_Creation);
                            commandtrans.Parameters.AddWithValue("@Status", Status);
                            commandtrans.Parameters.AddWithValue("@UserId", UserId);
                            int retv = commandtrans.ExecuteNonQuery();
                        }




                    }
                    statu_cntr = 40;
                    trans.Commit();

                }
            }
        }
        catch (Exception ex)
        {
            trans.Rollback();
            connection.Close();
        }
        finally
        {
            connection.Close();
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();
        }
    }

    //19102012
    [WebMethod(Description = "5. Commodtiy Received Transaction From Farmer -Off2Onn")]
    public void InsertCommodityReceived_Transaction(DataSet ds, string DistrictId, string societyID, bool ChkPaddyUser)
    {
        res = "";
        reint = 0;

        string LogID = "";
        string date = "";
        string Farmer_Id = "";
        try
        {
            if (connection != null && ChkPaddyUser == true)
            {
                connection.Open();
                if (ds != null)
                {
                    commandtrans = connection.CreateCommand();
                    trans = connection.BeginTransaction();
                    commandtrans.Connection = connection;
                    commandtrans.Transaction = trans;
                    date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                    LogID = societyID.ToString() + date;

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {

                        Farmer_Id = dr["Farmer_Id"].ToString();
                        string ComTransID = dr["ComTransID"].ToString();
                        string District_Id = dr["District_Id"].ToString();
                        string ReceivedID = dr["ReceivedID"].ToString();
                        string Society_Id = dr["Society_Id"].ToString();
                        commandtrans.CommandType = CommandType.Text;
                        commandtrans.CommandText = "select count(*) from CommodityReceivedFromFarmer where ReceivedID='" + dr["ReceivedID"].ToString() + "' and District_Id='" + District_Id + "' and Farmer_Id='" + Farmer_Id + "' and Society_Id='" + Society_Id + "'";
                        res = commandtrans.ExecuteScalar().ToString();
                        reint = Convert.ToInt16(commandtrans.ExecuteScalar());
                        if (reint > 0)
                        {
                            commandtrans.CommandType = CommandType.Text;
                            commandtrans.CommandText = "select count(*) from CommodityReceived_Transaction where Farmer_Id='" + Farmer_Id + "' and District_Id='" + District_Id + "' and Society_Id='" + Society_Id + "'  and ComTransID='" + ComTransID + "' ";
                            res = commandtrans.ExecuteScalar().ToString();
                            reint = Convert.ToInt16(commandtrans.ExecuteScalar());
                            if (reint <= 0)
                            {
                                string Proc_AgID = dr["Proc_AgID"].ToString();
                                string CropYear = dr["CropYear"].ToString();
                                string MarketingSeasonId = dr["MarketingSeasonId"].ToString();
                                string CommodityId = dr["CommodityId"].ToString();
                                string CommodityName = dr["CommodityName"].ToString();
                                string QtyReceived = dr["QtyReceived"].ToString();
                                string Bags = dr["Bags"].ToString();
                                string TotalAmount = dr["TotalAmount"].ToString();
                                string Date_Of_Receipt = chkDate(dr["Date_Of_Receipt"].ToString());
                                string CreatedDate = chkDate(dr["CreatedDate"].ToString());


                                //commandtrans.CommandType = CommandType.StoredProcedure;
                                //commandtrans.CommandText = "Proc_InsertCommodityReceived_Transaction";
                                //commandtrans.Parameters.Clear();
                                //commandtrans.Parameters.AddWithValue("@MarketingSeasonId", MarketingSeasonId);
                                //commandtrans.Parameters.AddWithValue("@ComTransID", ComTransID);
                                //commandtrans.Parameters.AddWithValue("@ReceivedID", ReceivedID);
                                //commandtrans.Parameters.AddWithValue("@District_Id", District_Id);
                                //commandtrans.Parameters.AddWithValue("@Proc_AgID", Proc_AgID);
                                //commandtrans.Parameters.AddWithValue("@Society_Id", Society_Id);
                                //commandtrans.Parameters.AddWithValue("@CropYear", CropYear);
                                //commandtrans.Parameters.AddWithValue("@Farmer_Id ", Farmer_Id);
                                //commandtrans.Parameters.AddWithValue("@CommodityId", CommodityId);
                                //commandtrans.Parameters.AddWithValue("@CommodityName ", CommodityName);
                                //commandtrans.Parameters.AddWithValue("@QtyReceived", QtyReceived);
                                //commandtrans.Parameters.AddWithValue("@Bags", Bags);
                                //commandtrans.Parameters.AddWithValue("@TotalAmount", TotalAmount);
                                //commandtrans.Parameters.AddWithValue("@CreatedDate ", CreatedDate);
                                //commandtrans.Parameters.AddWithValue("@Date_Of_Receipt", Date_Of_Receipt);

                                commandtrans.CommandType = CommandType.Text;
                                string qry = "Insert Into CommodityReceived_Transaction(ComTransID,ReceivedID,District_Id,Proc_AgID,Society_Id,CropYear ,MarketingSeasonId,Farmer_Id ,CommodityId ,CommodityName,QtyReceived ,Bags,TotalAmount ,CreatedDate,Date_Of_Receipt)";
                                qry += "values('" + ComTransID + "','" + ReceivedID + "','" + District_Id + "','" + Proc_AgID + "','" + Society_Id + "','" + CropYear + "','" + MarketingSeasonId + "','" + Farmer_Id + "','" + CommodityId + "',N'" + CommodityName + "','" + QtyReceived + "','" + Bags + "','" + TotalAmount + "','" + CreatedDate + "','" + Date_Of_Receipt + "')";
                                commandtrans.CommandText = qry;
                                int rex = commandtrans.ExecuteNonQuery();
                            }

                        }

                    }
                    statu_cntr = 42;
                    trans.Commit();

                }
            }
        }
        catch (Exception ex)
        {
            trans.Rollback();
            connection.Close();
        }
        finally
        {
            connection.Close();
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();
        }

    }


    //19102012
    [WebMethod(Description = "14. Insert IssueToSangrahanaKendra  -Off2Onn")]
    public void InsertIssueToSangrahanaKendra(DataSet dsIssue, string societyID, bool ChkPaddyUser)
    {
        string LogID = "";
        string date = "";
        try
        {
            if (connection != null)
            {
                connection.Open();
                commandtrans = connection.CreateCommand();
                trans = connection.BeginTransaction();
                commandtrans.Connection = connection;
                commandtrans.Transaction = trans;
                date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                LogID = societyID.ToString() + date;
                if (dsIssue != null)
                {
                    if (dsIssue.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsIssue.Tables[0].Rows)
                        {
                            string IssueID = dr["IssueID"].ToString();
                            string TransID = dr["TransID"].ToString();
                            commandtrans.CommandType = CommandType.Text;
                            string selectqry = "select count(*)  from IssueToSangrahanaKendra where  IssueID='" + IssueID + "' and TransID='" + TransID + "'";
                            commandtrans.CommandText = selectqry;
                            int res = Convert.ToInt16(commandtrans.ExecuteScalar());
                            if (res <= 0)
                            {
                                string DistrictId = dr["DistrictId"].ToString();
                                string Proc_AgID = dr["Proc_AgID"].ToString();
                                string SocietyID = dr["SocietyID"].ToString();
                                string PCID = dr["PCID"].ToString();
                                string CropYear = dr["CropYear"].ToString();
                                string MarketingSeasonId = dr["MarketingSeasonId"].ToString();
                                string DateOfIssue = chkDate(dr["DateOfIssue"].ToString());
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
                                string CreatedDate = chkDate(dr["CreatedDate"].ToString());
                                string CreatedBy = dr["CreatedBy"].ToString();
                                //string UpdatedDate = dr["UpdatedDate"].ToString();
                                //string UpdatedBy = dr["UpdatedBy"].ToString();
                                string UserId = dr["UserId"].ToString();

                                commandtrans.Connection = connection;
                                commandtrans.CommandType = CommandType.StoredProcedure;
                                commandtrans.Parameters.Clear();
                                commandtrans.CommandText = "Proc_IssueToSangrahanaKendra";
                                commandtrans.Parameters.AddWithValue("@TransID", TransID);
                                commandtrans.Parameters.AddWithValue("@IssueID", IssueID);
                                commandtrans.Parameters.AddWithValue("@DistrictId", DistrictId);
                                commandtrans.Parameters.AddWithValue("@Proc_AgID", Proc_AgID);
                                commandtrans.Parameters.AddWithValue("@SocietyID", SocietyID);
                                commandtrans.Parameters.AddWithValue("@PCID", PCID);
                                commandtrans.Parameters.AddWithValue("@CropYear", CropYear);
                                commandtrans.Parameters.AddWithValue("@MarketingSeasonId", MarketingSeasonId);
                                commandtrans.Parameters.AddWithValue("@DateOfIssue", DateOfIssue);
                                commandtrans.Parameters.AddWithValue("@IssueTo", IssueTo);
                                commandtrans.Parameters.AddWithValue("@SendingDistId", SendingDistId);
                                commandtrans.Parameters.AddWithValue("@GodownTypeId", GodownTypeId);
                                commandtrans.Parameters.AddWithValue("@GodownCenterId", GodownCenterId);
                                commandtrans.Parameters.AddWithValue("@GodownName", GodownName);
                                commandtrans.Parameters.AddWithValue("@GodownNumber", GodownNumber);
                                commandtrans.Parameters.AddWithValue("@Place", Place);
                                commandtrans.Parameters.AddWithValue("@RailRackOf_ID", RailRackOf_ID);
                                commandtrans.Parameters.AddWithValue("@RailRack_SendingPlace", RailRack_SendingPlace);
                                commandtrans.Parameters.AddWithValue("@RailRack_RecievingPlace", RailRack_RecievingPlace);
                                commandtrans.Parameters.AddWithValue("@Miller_ID", Miller_ID);
                                commandtrans.Parameters.AddWithValue("@MillerRepresentative", MillerRepresentative);
                                commandtrans.Parameters.AddWithValue("@CommodityId", CommodityId);
                                commandtrans.Parameters.AddWithValue("@Bags", Bags);
                                commandtrans.Parameters.AddWithValue("@QtyTransffer", QtyTransffer);
                                commandtrans.Parameters.AddWithValue("@TaulPtrakNo", TaulPtrakNo);
                                commandtrans.Parameters.AddWithValue("@TransporterId", TransporterId);
                                commandtrans.Parameters.AddWithValue("@TruckChalanNo", TruckChalanNo);
                                commandtrans.Parameters.AddWithValue("@TruckNo", TruckNo);
                                commandtrans.Parameters.AddWithValue("@DriverName", DriverName);
                                commandtrans.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                                commandtrans.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                                // commandtrans.Parameters.AddWithValue("@UpdatedDate", UpdatedDate);
                                //commandtrans.Parameters.AddWithValue("@UpdatedBy", UpdatedBy);
                                commandtrans.Parameters.AddWithValue("@UserId", UserId);
                                int x = commandtrans.ExecuteNonQuery();

                            }
                        }

                        statu_cntr = 45;
                        trans.Commit();
                    }
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
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();

        }
    }

    //19102012
    [WebMethod(Description = "13. Insert UploadData Status -Off2Onn")]
    public void UploadDataStatus(int cntr, string LogID)
    {
        try
        {
            if (connection != null)
            {
                connection.Open();
                commandtrans = connection.CreateCommand();
                trans = connection.BeginTransaction();
                commandtrans.Connection = connection;
                commandtrans.Transaction = trans;
                commandtrans.CommandType = CommandType.Text;
                string selectqry = "select count(LogID) as TotalCount from RunnerLog where  LogID='" + LogID + "'";
                commandtrans.CommandText = selectqry;
                int res = Convert.ToInt16(commandtrans.ExecuteScalar());
                string logcount = "";
                if (res == 0)
                { logcount = Convert.ToString(1); }
                else
                { logcount = res.ToString(); }

                commandtrans.CommandText = "Proc_UpdRunnerLog";
                commandtrans.CommandType = CommandType.StoredProcedure;
                commandtrans.Parameters.Clear();
                commandtrans.Parameters.AddWithValue("@LogID", LogID);
                commandtrans.Parameters.AddWithValue("@DayCount", logcount);
                commandtrans.Parameters.AddWithValue("@Status", cntr);
                int x = commandtrans.ExecuteNonQuery();
                trans.Commit();
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
            trans.Dispose();
            commandtrans.Dispose();

        }
    }


    //22102012
    [WebMethod(Description = "15. Delete FarmerDeleteRequest  -Off2Onn")]
    public void InsertFarmerDeleteRequest(DataSet dsdeleteFarmer, string societyID, bool ChkPaddyUser)
    {
        string LogID = "";
        string date = "";
        try
        {
            if (connection != null)
            {
                connection.Open();
                commandtrans = connection.CreateCommand();
                trans = connection.BeginTransaction();
                commandtrans.Connection = connection;
                commandtrans.Transaction = trans;
                date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                LogID = societyID.ToString() + date;

                foreach (DataRow dr in dsdeleteFarmer.Tables[0].Rows)
                {
                    string Farmer_Id = dr["Farmer_Id"].ToString();
                    commandtrans.CommandType = CommandType.Text;
                    string selectqry = "select count(Farmer_Id)  from FarmerDeleteRequest where  Farmer_Id='" + Farmer_Id + "'";
                    commandtrans.CommandText = selectqry;
                    int res = Convert.ToInt16(commandtrans.ExecuteScalar());
                    if (res <= 0)
                    {

                        string District_Id = dr["District_Id"].ToString();
                        string SocietyID = dr["SocietyID"].ToString();
                        string Reason = dr["Reason"].ToString();

                        string DeletedDate = chkDate(dr["DeletedDate"].ToString());
                        string DeletedBy = dr["DeletedBy"].ToString();
                        string IsDeleteRequest = dr["IsDeleteRequest"].ToString();


                        commandtrans.Connection = connection;
                        commandtrans.CommandType = CommandType.StoredProcedure;
                        commandtrans.Parameters.Clear();
                        commandtrans.CommandText = "Proc_InsertFarmerDeleteRequest";
                        commandtrans.Parameters.AddWithValue("@District_Id", District_Id);
                        commandtrans.Parameters.AddWithValue("@SocietyID", SocietyID);
                        commandtrans.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                        commandtrans.Parameters.AddWithValue("@Reason", Reason);
                        commandtrans.Parameters.AddWithValue("@DeletedDate", DeletedDate);
                        commandtrans.Parameters.AddWithValue("@DeletedBy", DeletedBy);
                        commandtrans.Parameters.AddWithValue("@IsDeleteRequest", IsDeleteRequest);
                        int x = commandtrans.ExecuteNonQuery();



                    }

                }

                statu_cntr = 50;
                trans.Commit();
            }


        }


        catch (Exception)
        {
            // throw ex;
            trans.Rollback();
            connection.Close();
        }
        finally
        {
            connection.Close();
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();

        }


    }



    //22102012
    [WebMethod(Description = "15.1 Insert Del_Farmer_LandRecorDescription_Log   -Off2Onn")]
    public void InsertDelFarmerLandRecord(DataSet dsFarmerTransID, string societyID, bool ChkPaddyUser)
    {

        string LogID = "";
        string date = "";
        try
        {
            if (connection != null)
            {
                connection.Open();
                commandtrans = connection.CreateCommand();
                trans = connection.BeginTransaction();
                commandtrans.Connection = connection;
                commandtrans.Transaction = trans;
                date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                LogID = societyID.ToString() + date;


                foreach (DataRow dr in dsFarmerTransID.Tables[0].Rows)
                {
                    string Farmer_Id = dr["Farmer_Id"].ToString();
                    string TransID = dr["TransID"].ToString();
                    string crpcode = dr["crpcode"].ToString();
                    commandtrans.CommandType = CommandType.Text;
                    string selectqry = "select count(Farmer_Id)  from FarmerDeleteRequest where  Farmer_Id='" + Farmer_Id + "'";
                    commandtrans.CommandText = selectqry;
                    int res = Convert.ToInt16(commandtrans.ExecuteScalar());
                    if (res > 0)
                    {
                        commandtrans.CommandType = CommandType.Text;
                        string selectq = "select count(TransID)  from Del_Farmer_LandRecordDescription_Log where TransID='" + TransID + "' and  Farmer_Id='" + Farmer_Id + "' and crpcode in ('2','3')";
                        commandtrans.CommandText = selectq;
                        res = Convert.ToInt16(commandtrans.ExecuteScalar());
                        if (res <= 0)
                        {


                            // string TransID = dr["TransID"].ToString();
                            //string Farmer_Id = dr["Farmer_Id"].ToString();

                            string Village_ID = dr["Village_ID"].ToString();
                            string _VillageName = dr["VillageName"].ToString();
                            string Crop_ID = dr["Crop_ID"].ToString();
                            string LandOwner_Name = dr["LandOwner_Name"].ToString();
                            string LandOwner_RinPustikaNo = dr["LandOwner_RinPustikaNo"].ToString();
                            string LandType = dr["LandType"].ToString();
                            string KhasaraNo = dr["KhasaraNo"].ToString();
                            string Rakba = Convert.ToString(CheckNullFloat(dr["Rakba"].ToString()));
                            string Rakba_crop_sinchit = Convert.ToString(CheckNullFloat(dr["Rakba_crop_sinchit"].ToString()));
                            string Rakba_crop_asinchit = Convert.ToString(CheckNullFloat(dr["Rakba_crop_asinchit"].ToString()));
                            string Rakba_crop_sinchit_qty = Convert.ToString(CheckNullFloat(dr["Rakba_crop_sinchit_qty"].ToString()));
                            string Rakba_crop_asinchit_qty = Convert.ToString(CheckNullFloat(dr["Rakba_crop_asinchit_qty"].ToString()));
                            string Procured_qty = Convert.ToString(CheckNullFloat(dr["Procured_qty"].ToString()));


                            commandtrans.CommandType = CommandType.StoredProcedure;
                            commandtrans.CommandText = "Proc_Insert_DeleteLandRecord_Log";
                            commandtrans.Parameters.Clear();
                            commandtrans.Parameters.AddWithValue("@TransID", TransID);
                            commandtrans.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                            commandtrans.Parameters.AddWithValue("@Village_ID", Village_ID);
                            commandtrans.Parameters.AddWithValue("@VillageName", _VillageName);
                            commandtrans.Parameters.AddWithValue("@Crop_ID", Crop_ID);
                            commandtrans.Parameters.AddWithValue("@LandOwner_Name", LandOwner_Name);
                            commandtrans.Parameters.AddWithValue("@LandOwner_RinPustikaNo", LandOwner_RinPustikaNo);
                            commandtrans.Parameters.AddWithValue("@LandType", LandType);
                            commandtrans.Parameters.AddWithValue("@KhasaraNo", KhasaraNo);
                            commandtrans.Parameters.AddWithValue("@Rakba", Rakba);
                            commandtrans.Parameters.AddWithValue("@Rakba_crop_sinchit", Rakba_crop_sinchit);
                            commandtrans.Parameters.AddWithValue("@Rakba_crop_asinchit", Rakba_crop_asinchit);
                            commandtrans.Parameters.AddWithValue("@Rakba_crop_sinchit_qty", Rakba_crop_sinchit_qty);
                            commandtrans.Parameters.AddWithValue("@Rakba_crop_asinchit_qty", Rakba_crop_asinchit_qty);
                            commandtrans.Parameters.AddWithValue("@Procured_qty", Procured_qty);
                            commandtrans.Parameters.AddWithValue("@crpcode", crpcode);
                            int resv = commandtrans.ExecuteNonQuery();
                            if (resv > 0)
                            {
                                commandtrans.Parameters.Clear();
                                commandtrans.CommandType = CommandType.Text;
                                commandtrans.CommandText = "Delete from Farmer_LandRecordDescription where Farmer_Id='" + Farmer_Id + "' and TransID='" + TransID + "' and crpcode in('2','3') ";
                                int deret = commandtrans.ExecuteNonQuery();
                            }
                        }

                    }

                }
                statu_cntr = 51;
                trans.Commit();
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
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();

        }
    }



    //22102012
    [WebMethod(Description = "15.2 Insert Del_FarmerRegistration_Log   -Off2Onn")]
    public void InsertDelFarmerRegistration(DataSet dsFarmerInfoID, string societyID, bool ChkPaddyUser)
    {
        string LogID = "";
        string date = "";
        try
        {
            if (connection != null)
            {
                connection.Open();
                commandtrans = connection.CreateCommand();
                trans = connection.BeginTransaction();
                commandtrans.Connection = connection;
                commandtrans.Transaction = trans;
                date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                LogID = societyID.ToString() + date;


                foreach (DataRow dr in dsFarmerInfoID.Tables[0].Rows)
                {
                    string Farmer_Id = dr["Farmer_Id"].ToString();
                    string District_Id = dr["District_Id"].ToString();
                    string SocietyID = societyID;
                    commandtrans.CommandType = CommandType.Text;
                    string selectqry = "select count(Farmer_Id)  from FarmerDeleteRequest where  Farmer_Id='" + Farmer_Id + "'";
                    commandtrans.CommandText = selectqry;
                    int res = Convert.ToInt16(commandtrans.ExecuteScalar());
                    if (res > 0)
                    {
                        commandtrans.CommandType = CommandType.Text;
                        string qryReg = "select * from FarmerRegistration where  Farmer_Id='" + Farmer_Id + "' and District_Id='" + District_Id + "' and PC_ID='" + SocietyID + "'";
                        commandtrans.CommandText = qryReg;
                        SqlDataReader dard = commandtrans.ExecuteReader();
                        if (dard != null)
                        {
                            if (dard.HasRows)
                            {
                                dard.Read();

                                string Village_Id = dard["Village_Id"].ToString();
                                string VillageName = dard["VillageName"].ToString();
                                string Tehsil_Id = dard["Tehsil_Id"].ToString();
                                string FarmerName = dard["FarmerName"].ToString();
                                string FatherHusName = dard["FatherHusName"].ToString();
                                string Gram_Panchayat = dard["Gram_Panchayat"].ToString();
                                string PatwariHalkaNo = dard["PatwariHalkaNo"].ToString();
                                string Mobileno = dard["Mobileno"].ToString();
                                string Category = dard["Category"].ToString();
                                string RinPustikaNo = dard["RinPustikaNo"].ToString();
                                string Farmer_EID_UID_No = dard["Farmer_EID_UID_No"].ToString();
                                string Farmer_BankName_New = dard["Farmer_BankName_New"].ToString();
                                string Farmer_BankBranchName = dard["Farmer_BankBranchName"].ToString();
                                string Farmer_BankAccountNo = dard["Farmer_BankAccountNo"].ToString();
                                string PC_ID = dard["PC_ID"].ToString();
                                string Procured_SocietyID = dard["Procured_SocietyID"].ToString();
                                string Procured_Dist_ID = dard["Procured_Dist_ID"].ToString();
                                string Procured_Place = dard["Procured_Place"].ToString();
                                string CropExpected_Date = chkDate(dard["CropExpected_Date"].ToString());
                                string UserID = dard["UserID"].ToString();
                                string CreatedDate = chkDate(dard["CreatedDate"].ToString());
                                string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                                string RegistrationDate = chkDate(dard["RegistrationDate"].ToString());


                                string IsApproved = dard["IsApproved"].ToString();
                                string Status = dard["Status"].ToString();
                                dard.Close(); dard.Dispose();

                                commandtrans.CommandType = CommandType.StoredProcedure;
                                commandtrans.CommandText = "Proc_Insert_DeleteFarmerReg_Log";
                                commandtrans.Parameters.Clear();
                                commandtrans.Parameters.AddWithValue("@District_Id", District_Id);
                                commandtrans.Parameters.AddWithValue("@Village_Id", Village_Id);
                                commandtrans.Parameters.AddWithValue("@VillageName", VillageName);
                                commandtrans.Parameters.AddWithValue("@Tehsil_Id", Tehsil_Id);
                                commandtrans.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                                commandtrans.Parameters.AddWithValue("@FarmerName", FarmerName);
                                commandtrans.Parameters.AddWithValue("@FatherHusName", FatherHusName);
                                commandtrans.Parameters.AddWithValue("@Gram_Panchayat", Gram_Panchayat);
                                commandtrans.Parameters.AddWithValue("@PatwariHalkaNo", PatwariHalkaNo);
                                commandtrans.Parameters.AddWithValue("@Mobileno", Mobileno);
                                commandtrans.Parameters.AddWithValue("@Category", Category);
                                commandtrans.Parameters.AddWithValue("@RinPustikaNo", RinPustikaNo);
                                commandtrans.Parameters.AddWithValue("@Farmer_EID_UID_No", Farmer_EID_UID_No);
                                commandtrans.Parameters.AddWithValue("@Farmer_BankName_New", Farmer_BankName_New);
                                commandtrans.Parameters.AddWithValue("@Farmer_BankBranchName", Farmer_BankBranchName);
                                commandtrans.Parameters.AddWithValue("@Farmer_BankAccountNo", Farmer_BankAccountNo);
                                commandtrans.Parameters.AddWithValue("@PC_ID", PC_ID);
                                commandtrans.Parameters.AddWithValue("@Procured_SocietyID", Procured_SocietyID);
                                commandtrans.Parameters.AddWithValue("@Procured_Dist_ID", Procured_Dist_ID);
                                commandtrans.Parameters.AddWithValue("@Procured_Place", Procured_Place);
                                commandtrans.Parameters.AddWithValue("@CropExpected_Date", CropExpected_Date);
                                commandtrans.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                                commandtrans.Parameters.AddWithValue("@ip", ip);
                                commandtrans.Parameters.AddWithValue("@RegistrationDate", RegistrationDate);
                                commandtrans.Parameters.AddWithValue("@IsApproved", IsApproved);
                                commandtrans.Parameters.AddWithValue("@UserID", UserID);
                                commandtrans.Parameters.AddWithValue("@Status", Status);
                                res = commandtrans.ExecuteNonQuery();

                                if (res > 0)
                                {
                                    //count landrecord
                                    commandtrans.CommandType = CommandType.Text;
                                    qryReg = "select count(Farmer_Id) from Farmer_LandRecordDescription where  Farmer_Id='" + Farmer_Id + "' and crpcode not in ('2','3') ";
                                    commandtrans.CommandText = qryReg;
                                    string rev = Convert.ToString(commandtrans.ExecuteScalar());

                                    if (Convert.ToInt16(rev) > 0)
                                    {


                                    }
                                    else
                                    {
                                        commandtrans.Parameters.Clear();
                                        commandtrans.CommandType = CommandType.Text;
                                        commandtrans.CommandText = "Delete from FarmerRegistration where Farmer_Id='" + Farmer_Id + "' ";
                                        int ret = commandtrans.ExecuteNonQuery();
                                    }

                                }

                            }
                            dard.Close();
                            dard.Dispose();
                        }
                    }
                }
                statu_cntr = 52;
                trans.Commit();
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
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();

        }
    }



    //19102012
    [WebMethod(Description = "16. Insert Miller Master  -Off2Onn")]
    public void InsertMillerMaster(DataSet dsMillerMaster, string SocietyId, bool ChkPaddyUser)
    {
        string LogID = "";
        string date = "";
        try
        {
            if (connection != null)
            {
                connection.Open();
                commandtrans = connection.CreateCommand();
                trans = connection.BeginTransaction();
                commandtrans.Connection = connection;
                commandtrans.Transaction = trans;
                date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                LogID = SocietyId.ToString() + date;


                foreach (DataRow dr in dsMillerMaster.Tables[0].Rows)
                {
                    string Mill_ID = dr["Mill_ID"].ToString();
                    commandtrans.CommandType = CommandType.Text;
                    string selectqry = "select count(Mill_ID)  from MillMaster where  Mill_ID='" + Mill_ID + "' and SocietyID='" + SocietyId + "'";
                    commandtrans.CommandText = selectqry;
                    int res = Convert.ToInt16(commandtrans.ExecuteScalar());
                    if (res <= 0)
                    {
                        string Mill_Name = dr["Mill_Name"].ToString();
                        string Address = dr["Address"].ToString();
                        string MobileNo = dr["MobileNo"].ToString();
                        string CreatedDate = chkDate(dr["CreatedDate"].ToString());
                        string Updateddate = "";
                        if (dr["Updateddate"].ToString() == "")
                        {
                            Updateddate = System.DateTime.Now.ToString("01/01/1900");
                        }
                        else
                        {
                            Updateddate = chkDate(dr["Updateddate"].ToString());
                        }
                        string CreatedBy = dr["CreatedBy"].ToString();
                        string Status = dr["Status"].ToString();
                        string SocietyID = dr["SocietyID"].ToString();
                        string District_ID = dr["District_ID"].ToString();



                        commandtrans.Connection = connection;
                        commandtrans.CommandType = CommandType.StoredProcedure;
                        commandtrans.Parameters.Clear();
                        commandtrans.CommandText = "Prco_InsertMillerMaster";
                        commandtrans.Parameters.AddWithValue("@Mill_ID", Mill_ID);
                        commandtrans.Parameters.AddWithValue("@Mill_Name", Mill_Name);
                        commandtrans.Parameters.AddWithValue("@Address", Address);
                        commandtrans.Parameters.AddWithValue("@MobileNo", MobileNo);
                        commandtrans.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                        commandtrans.Parameters.AddWithValue("@Updateddate", Updateddate);
                        commandtrans.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                        commandtrans.Parameters.AddWithValue("@Status", Status);
                        commandtrans.Parameters.AddWithValue("@SocietyID", SocietyID);
                        commandtrans.Parameters.AddWithValue("@District_ID", District_ID);

                        int x = commandtrans.ExecuteNonQuery();


                    }
                }

                statu_cntr = 54;
                trans.Commit();
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
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();

        }





    }


    //19102012
    [WebMethod(Description = "17. Insert GunnyBagsReceipt  -Off2Onn")]
    public void InsertGunnyBagsReceipt(DataSet dsGunnyBags, string SocietyID, bool ChkPaddyUser)
    {

        string LogID = "";
        string date = "";
        try
        {
            if (connection != null)
            {
                connection.Open();
                commandtrans = connection.CreateCommand();
                trans = connection.BeginTransaction();
                commandtrans.Connection = connection;
                commandtrans.Transaction = trans;
                date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                LogID = SocietyID.ToString() + date;

                foreach (DataRow dr in dsGunnyBags.Tables[0].Rows)
                {
                    string GReceiptNo = dr["GReceiptNo"].ToString();
                    string District_ID = dr["District_ID"].ToString();
                    string SocietyCode = dr["SocietyCode"].ToString();
                    commandtrans.CommandType = CommandType.Text;
                    string selectqry = "select count(*)  from GunnyBagsReceipt where  GReceiptNo='" + GReceiptNo + "' and District_ID='" + District_ID + "' and SocietyCode='" + SocietyCode + "'";
                    commandtrans.CommandText = selectqry;
                    int res = Convert.ToInt16(commandtrans.ExecuteScalar());
                    if (res <= 0)
                    {
                        //Insert

                        string PC_Id = dr["PC_Id"].ToString();
                        string DateOfRecv = chkDate(dr["DateOfRecv"].ToString());
                        string NoOfBags = dr["NoOfBags"].ToString();
                        string TruckChallanNo = dr["TruckChallanNo"].ToString();
                        string TruckNo = dr["TruckNo"].ToString();
                        string ReceivedFrom = dr["ReceivedFrom"].ToString();
                        string TruckChallanDate = chkDate(dr["TruckChallanDate"].ToString());
                        string userid = dr["userid"].ToString();
                        string datetimestamp = chkDate(dr["datetimestamp"].ToString());
                        string opration = dr["opration"].ToString();
                        string Locked = dr["Locked"].ToString();
                        string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                        string GunnyType = dr["GunnyType"].ToString();



                        commandtrans.Connection = connection;
                        commandtrans.CommandType = CommandType.StoredProcedure;
                        commandtrans.Parameters.Clear();
                        commandtrans.CommandText = "Prco_InsertGunnyBagsReceipt";
                        commandtrans.Parameters.AddWithValue("@GReceiptNo", GReceiptNo);
                        commandtrans.Parameters.AddWithValue("@District_ID", District_ID);
                        commandtrans.Parameters.AddWithValue("@SocietyCode", SocietyCode);
                        commandtrans.Parameters.AddWithValue("@PC_Id", PC_Id);
                        commandtrans.Parameters.AddWithValue("@DateOfRecv", DateOfRecv);
                        commandtrans.Parameters.AddWithValue("@NoOfBags", NoOfBags);
                        commandtrans.Parameters.AddWithValue("@TruckChallanNo", TruckChallanNo);
                        commandtrans.Parameters.AddWithValue("@TruckNo", TruckNo);
                        commandtrans.Parameters.AddWithValue("@ReceivedFrom", ReceivedFrom);
                        commandtrans.Parameters.AddWithValue("@TruckChallanDate", TruckChallanDate);
                        commandtrans.Parameters.AddWithValue("@userid", userid);
                        commandtrans.Parameters.AddWithValue("@datetimestamp", datetimestamp);
                        commandtrans.Parameters.AddWithValue("@opration", opration);
                        commandtrans.Parameters.AddWithValue("@Locked", Locked);
                        commandtrans.Parameters.AddWithValue("@ip", ip);
                        commandtrans.Parameters.AddWithValue("@GunnyType", GunnyType);
                        int x = commandtrans.ExecuteNonQuery();
                    }
                }
                statu_cntr = 52;
                trans.Commit();
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
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();

        }
    }


    //19102012
    [WebMethod(Description = "18. Insert GunnyBagsIssueTable   -Off2Onn")]
    public void InsertGunnyBagsIssue(DataSet dsGunnyBagsIssue, string SocietyID, bool ChkPaddyUser)
    {
        string LogID = "";
        string date = "";

        try
        {
            if (connection != null)
            {
                connection.Open();
                commandtrans = connection.CreateCommand();
                trans = connection.BeginTransaction();
                commandtrans.Connection = connection;
                commandtrans.Transaction = trans;
                date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                LogID = SocietyID.ToString() + date;
                foreach (DataRow dr in dsGunnyBagsIssue.Tables[0].Rows)
                {

                    string IssueNo = dr["IssueNo"].ToString();
                    string District_ID = dr["District_ID"].ToString();
                    string SocietyCode = dr["SocietyCode"].ToString();
                    commandtrans.CommandType = CommandType.Text;
                    string selectqry = "select count(*)  from GunnyBagsIssueTable where  IssueNo='" + IssueNo + "' and District_ID='" + District_ID + "' and SocietyCode='" + SocietyCode + "'";
                    commandtrans.CommandText = selectqry;
                    int res = Convert.ToInt16(commandtrans.ExecuteScalar());
                    if (res <= 0)
                    {
                        //InsertPC_Id
                        string PC_Id = dr["PC_Id"].ToString();
                        string DateOfIssue = chkDate(dr["DateOfIssue"].ToString());
                        string TypeofBags = dr["TypeofBags"].ToString();
                        string GunnyType = dr["GunnyType"].ToString();
                        string NoOfBags = dr["NoOfBags"].ToString();
                        string TruckChallanNo = dr["TruckChallanNo"].ToString();
                        string TruckNo = dr["TruckNo"].ToString();
                        string IssuedFrom = dr["IssuedFrom"].ToString();
                        string TruckChallanDate = chkDate(dr["TruckChallanDate"].ToString());
                        string userid = dr["userid"].ToString();
                        string datetimestamp = chkDate(dr["datetimestamp"].ToString());
                        string opration = dr["opration"].ToString();
                        string Locked = dr["Locked"].ToString();
                        string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();


                        commandtrans.Connection = connection;
                        commandtrans.CommandType = CommandType.StoredProcedure;
                        commandtrans.Parameters.Clear();
                        commandtrans.CommandText = "Proc_InsertGunnyBagsIssueTable";
                        commandtrans.Parameters.AddWithValue("@IssueNo", IssueNo);
                        commandtrans.Parameters.AddWithValue("@District_ID", District_ID);
                        commandtrans.Parameters.AddWithValue("@SocietyCode", SocietyCode);
                        commandtrans.Parameters.AddWithValue("@PC_Id", PC_Id);
                        commandtrans.Parameters.AddWithValue("@DateOfIssue", DateOfIssue);
                        commandtrans.Parameters.AddWithValue("@TypeofBags", TypeofBags);
                        commandtrans.Parameters.AddWithValue("@GunnyType", GunnyType);
                        commandtrans.Parameters.AddWithValue("@NoOfBags", NoOfBags);
                        commandtrans.Parameters.AddWithValue("@TruckChallanNo", TruckChallanNo);
                        commandtrans.Parameters.AddWithValue("@TruckNo", TruckNo);
                        commandtrans.Parameters.AddWithValue("@IssuedFrom", IssuedFrom);
                        commandtrans.Parameters.AddWithValue("@TruckChallanDate", TruckChallanDate);
                        commandtrans.Parameters.AddWithValue("@userid", userid);
                        commandtrans.Parameters.AddWithValue("@datetimestamp", datetimestamp);
                        commandtrans.Parameters.AddWithValue("@opration", opration);
                        commandtrans.Parameters.AddWithValue("@Locked", Locked);
                        commandtrans.Parameters.AddWithValue("@ip", ip);
                        int x = commandtrans.ExecuteNonQuery();

                    }



                }
                statu_cntr = 53;
                trans.Commit();
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
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();
        }

    }




    //08112012
    [WebMethod(Description = "19. Insert TransportMaster   -Off2Onn")]
    public void InsertTransportMaster(DataSet dsTransportMaster, string SocietyID, bool ChkPaddyUser)
    {
        string LogID = "";
        string date = "";
        try
        {
            if (connection != null)
            {
                connection.Open();
                commandtrans = connection.CreateCommand();
                trans = connection.BeginTransaction();
                commandtrans.Connection = connection;
                commandtrans.Transaction = trans;
                date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                LogID = SocietyID.ToString() + date;
                foreach (DataRow dr in dsTransportMaster.Tables[0].Rows)
                {
                    string Transporter_ID = dr["Transporter_ID"].ToString();
                    string District_ID = dr["District_ID"].ToString();
                    string SocietyCode = dr["SocietyCode"].ToString();
                    commandtrans.CommandType = CommandType.Text;
                    string selectqry = "select count(*)  from TransportMaster where  Transporter_ID='" + Transporter_ID + "' and District_ID='" + District_ID + "' and SocietyCode='" + SocietyCode + "'";
                    commandtrans.CommandText = selectqry;
                    int res = Convert.ToInt16(commandtrans.ExecuteScalar());
                    if (res <= 0)
                    {

                        string Transporter_Name = dr["Transporter_Name"].ToString();
                        string NoOfTrucs = dr["NoOfTrucs"].ToString();
                        string Address = dr["Address"].ToString();
                        string MobileNo = dr["MobileNo"].ToString();
                        string User_ID = dr["User_ID"].ToString();
                        string DateTimeStamp = chkDate(dr["DateTimeStamp"].ToString());
                        string Opration = dr["Opration"].ToString();
                        string Locked = dr["Locked"].ToString();
                        string IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();

                        commandtrans.Connection = connection;
                        commandtrans.CommandType = CommandType.StoredProcedure;
                        commandtrans.Parameters.Clear();
                        commandtrans.CommandText = "Proc_InsertTransportMaster";
                        commandtrans.Parameters.AddWithValue("@Transporter_ID", Transporter_ID);
                        commandtrans.Parameters.AddWithValue("@Transporter_Name", Transporter_Name);
                        commandtrans.Parameters.AddWithValue("@NoOfTrucs", NoOfTrucs);
                        commandtrans.Parameters.AddWithValue("@Address", Address);
                        commandtrans.Parameters.AddWithValue("@MobileNo", MobileNo);
                        commandtrans.Parameters.AddWithValue("@User_ID", User_ID);
                        commandtrans.Parameters.AddWithValue("@DateTimeStamp", DateTimeStamp);
                        commandtrans.Parameters.AddWithValue("@Opration", Opration);
                        commandtrans.Parameters.AddWithValue("@Locked", Locked);
                        commandtrans.Parameters.AddWithValue("@SocietyCode", SocietyCode);
                        commandtrans.Parameters.AddWithValue("@District_ID", District_ID);
                        commandtrans.Parameters.AddWithValue("@IP", IP);
                        int x = commandtrans.ExecuteNonQuery();

                    }
                }
                statu_cntr = 69;
                trans.Commit();
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
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();
        }
    }



    #endregion


    #region Online To Offline

    //19102012
    [WebMethod(Description = "15.  Select Farmers ,Whose Society Is Updated in  Online by runner -Onn2Off")]
    public DataSet SelectSocietyChangeFarmerRegInfo(string DistrictId, string societyID, ChkPaddyUser userpwd)
    {
        string LogID = "";
        string date = "";
        try
        {
            dataset = new DataSet();
            if (connection != null && userpwd.username == "pp" && userpwd.password == "pp")
            {
                DataSet dsFarmerList = new DataSet();
                date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                LogID = societyID.ToString() + date;
                connection.Open();
                commandtrans = connection.CreateCommand();
                trans = connection.BeginTransaction();
                commandtrans.Connection = connection;
                commandtrans.Transaction = trans;
                commandtrans.CommandType = CommandType.Text;
                commandtrans.CommandText = "select Farmer_id From SocietyChangeLog where NEW_PC_ID='" + societyID + "'  and District_ID='" + DistrictId + "'";
                commandtrans.Parameters.Clear();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandtrans);
                dataAdapter.Fill(dsFarmerList);
                if (dsFarmerList != null)
                {
                    if (dsFarmerList.Tables[0].Rows.Count > 0)
                    {

                        string finalfarmerids = "";
                        foreach (DataRow dr in dsFarmerList.Tables[0].Rows)
                        {
                            string farmerid = dr["Farmer_Id"].ToString();
                            finalfarmerids = finalfarmerids + ",'" + farmerid + "'" + "";

                        }
                        string myid = finalfarmerids.Remove(0, 1);
                        commandtrans.CommandType = CommandType.Text;
                        string qry = "select * from FarmerRegistration where Farmer_Id in (" + myid + ") and District_Id='" + DistrictId + "'";
                        commandtrans.CommandText = qry;
                        SqlDataAdapter daFarSocChange = new SqlDataAdapter(commandtrans);
                        daFarSocChange.Fill(dataset);

                    }
                }
            }
        }
        catch (Exception ex)
        {
            trans.Rollback();
            connection.Close();
        }
        finally
        {
            connection.Close();
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();
        }
        return dataset;


    }



    //19102012
    [WebMethod(Description = "15.  Select FarmersLand Record ,Whose Society Is Updated in  Online by runner -Onn2Off")]
    public DataSet SelectSocietyChangeFarmerLandInfo(string DistrictId, string societyID, ChkPaddyUser userpwd)
    {
        string LogID = "";
        string date = "";
        try
        {
            dataset = new DataSet();
            if (connection != null && userpwd.username == "pp" && userpwd.password == "pp")
            {
                DataSet dsFarmerList = new DataSet();
                date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                LogID = societyID.ToString() + date;
                connection.Open();
                commandtrans = connection.CreateCommand();
                trans = connection.BeginTransaction();
                commandtrans.Connection = connection;
                commandtrans.Transaction = trans;
                commandtrans.CommandType = CommandType.Text;
                commandtrans.CommandText = "select Farmer_id From SocietyChangeLog where NEW_PC_ID='" + societyID + "'  and District_ID='" + DistrictId + "'";
                commandtrans.Parameters.Clear();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandtrans);
                dataAdapter.Fill(dsFarmerList);
                if (dsFarmerList != null)
                {
                    if (dsFarmerList.Tables[0].Rows.Count > 0)
                    {

                        string finalfarmerids = "";
                        foreach (DataRow dr in dsFarmerList.Tables[0].Rows)
                        {
                            string farmerid = dr["Farmer_Id"].ToString();
                            finalfarmerids = finalfarmerids + ",'" + farmerid + "'" + "";

                        }
                        string myid = finalfarmerids.Remove(0, 1);
                        commandtrans.CommandType = CommandType.Text;
                        string qry = "select * from Farmer_LandRecordDescription where Farmer_Id in (" + myid + ") ";
                        commandtrans.CommandText = qry;
                        SqlDataAdapter daFarSocChange = new SqlDataAdapter(commandtrans);
                        daFarSocChange.Fill(dataset);

                    }
                }
            }
        }
        catch (Exception ex)
        {
            trans.Rollback();
            connection.Close();
        }
        finally
        {
            connection.Close();
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();
        }
        return dataset;

    }



    //19102012
    [WebMethod(Description = "15.  Select CommodityRate From Online -Onn2Off")]
    public DataSet SelectCommodityRate(string DistrictId, string societyID, ChkPaddyUser userpwd)
    {
        string LogID = "";
        string date = "";

        try
        {
            dataset = new DataSet();

            if (connection != null && userpwd.username == "pp" && userpwd.password == "pp")
            {
                date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                LogID = societyID.ToString() + date;
                connection.Open();
                commandtrans = connection.CreateCommand();
                trans = connection.BeginTransaction();
                commandtrans.Connection = connection;
                commandtrans.Transaction = trans;
                commandtrans.CommandType = CommandType.StoredProcedure;
                commandtrans.CommandText = "Proc_SelectCommodityRate";
                commandtrans.Parameters.Clear();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandtrans);
                dataAdapter.Fill(dataset);
                statu_cntr = 55;
                trans.Commit();
            }
        }
        catch (Exception ex)
        {
            trans.Rollback();
            connection.Close();
        }
        finally
        {
            connection.Close();
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();
        }
        return dataset;

    }



    //19102012
    [WebMethod(Description = "Insert Farmers In Offline -Onn2Off")]
    public DataSet SelectNewFarmerInformation(ChkPaddyUser userpwd, string DistrictId, string societyID)
    {
        string LogID = "";
        string date = "";

        try
        {
            if (connection != null && userpwd.username == "pp" && userpwd.password == "pp")
            {
                connection.Open();

                DataSet dscommFarmer = new DataSet();
                date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                LogID = societyID.ToString() + date;
                commandtrans = connection.CreateCommand();
                trans = connection.BeginTransaction();
                commandtrans.Connection = connection;
                commandtrans.Transaction = trans;
                commandtrans.CommandType = CommandType.Text;
                string qry = "select Farmer_Id from NewFarmerRegistrationInOnline_Log where District_Id='" + DistrictId + "' and  Society_Id='" + societyID + "' and PC_Id='" + societyID + "'";
                // string qry = "select Farmer_Id from FarmerRegistration where District_Id='" + DistrictId + "'and PC_Id='" + societyID + "'";
                commandtrans.CommandText = qry;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandtrans);
                dataAdapter.Fill(dscommFarmer);

                if (dscommFarmer != null)
                {
                    if (dscommFarmer.Tables[0].Rows.Count > 0)
                    {
                        string finalfarmerids = "";
                        foreach (DataRow dr in dscommFarmer.Tables[0].Rows)
                        {
                            string farmerid = dr["Farmer_Id"].ToString();
                            finalfarmerids = finalfarmerids + ",'" + farmerid + "'" + "";
                        }
                        dscommFarmer.Dispose();
                        string myfarmerid = finalfarmerids.Remove(0, 1);
                        commandtrans.CommandType = CommandType.Text;
                        commandtrans.CommandText = "select * from FarmerRegistration where Farmer_Id in(" + myfarmerid + ") and District_Id='" + DistrictId + "' and PC_ID='" + societyID + "' ";
                        SqlDataAdapter da = new SqlDataAdapter(commandtrans);
                        dataset = new DataSet();
                        da.Fill(dataset);

                    }
                }

                statu_cntr = 60;
                trans.Commit();
            }
        }
        catch (Exception ex)
        {
            trans.Rollback();
            connection.Close();
        }
        finally
        {
            connection.Close();
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();
        }
        return dataset;


    }



    //19102012
    [WebMethod(Description = "Insert FarmersLand Information In Offline -Onn2Off")]
    public DataSet SelectNewFarmerLandInformtion(ChkPaddyUser userpwd, string DistrictId, string societyID)
    {
        string LogID = "";
        string date = "";
        try
        {
            if (connection != null && userpwd.username == "pp" && userpwd.password == "pp")
            {
                connection.Open();

                DataSet dscommFarmer = new DataSet();
                date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                LogID = societyID.ToString() + date;
                commandtrans = connection.CreateCommand();
                trans = connection.BeginTransaction();
                commandtrans.Connection = connection;
                commandtrans.Transaction = trans;
                commandtrans.CommandType = CommandType.Text;
                string qry = "select Farmer_Id from NewFarmerRegistrationInOnline_Log where District_Id='" + DistrictId + "' and  Society_Id='" + societyID + "' and PC_Id='" + societyID + "'";
                // string qry = "select Farmer_Id from FarmerRegistration where District_Id='" + DistrictId + "'and PC_Id='" + societyID + "'";
                commandtrans.CommandText = qry;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandtrans);
                dataAdapter.Fill(dscommFarmer);

                if (dscommFarmer != null)
                {
                    if (dscommFarmer.Tables[0].Rows.Count > 0)
                    {
                        string finalfarmerids = "";
                        foreach (DataRow dr in dscommFarmer.Tables[0].Rows)
                        {
                            string farmerid = dr["Farmer_Id"].ToString();
                            finalfarmerids = finalfarmerids + ",'" + farmerid + "'" + "";
                        }
                        dscommFarmer.Dispose();
                        string myfarmerid = finalfarmerids.Remove(0, 1);
                        commandtrans.CommandType = CommandType.Text;
                        commandtrans.CommandText = "select * from Farmer_LandRecordDescription where Farmer_Id in(" + myfarmerid + ") ";
                        SqlDataAdapter da = new SqlDataAdapter(commandtrans);
                        dataset = new DataSet();
                        da.Fill(dataset);
                    }
                }
                statu_cntr = 65;
                trans.Commit();
            }
        }
        catch (Exception ex)
        {
            trans.Rollback();
            connection.Close();
        }
        finally
        {
            connection.Close();
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();
        }
        return dataset;

    }



    //19102012
    [WebMethod(Description = "17.1  Select FarmerDCCBLoan From Online -Onn2Off ")]
    public DataSet SelectDCCBLoanOfFarmer(string DistrictId, string societyID, ChkPaddyUser userpwd, DataSet dsSocietyFarmerList)
    {
        string LogID = "";
        string date = "";
        try
        {
            dataset = new DataSet();

            if (connection != null && userpwd.username == "pp" && userpwd.password == "pp")
            {
                connection.Open();
                date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                LogID = societyID.ToString() + date;

                if (dsSocietyFarmerList != null)
                {
                    if (dsSocietyFarmerList.Tables[0].Rows.Count > 0)
                    {


                        string finalfarmerids = "";
                        foreach (DataRow dr in dsSocietyFarmerList.Tables[0].Rows)
                        {
                            string farmerid = dr["Farmer_Id"].ToString();
                            finalfarmerids = finalfarmerids + ",'" + farmerid + "'" + "";

                        }
                        string myid = finalfarmerids.Remove(0, 1);
                        commandtrans = connection.CreateCommand();
                        trans = connection.BeginTransaction();
                        commandtrans.Connection = connection;
                        commandtrans.Transaction = trans;
                        commandtrans.CommandType = CommandType.Text;
                        string qry = "SELECT [FarmerRegNumber],[District_Id],[Tehsil_Id],[Village_Id],[LoanAmount],[CreatedDate],[Updateddate] FROM [DCCBLoanOfFarmer] where [FarmerRegNumber] in (" + myid + ") and District_Id='" + DistrictId + "'";
                        commandtrans.CommandText = qry;
                        //commandtrans.CommandType = CommandType.StoredProcedure;
                        //commandtrans.CommandText = "Proc_SelectDCCBLoanOfFarmer";
                        commandtrans.Parameters.Clear();
                        //commandtrans.Parameters.AddWithValue("@FarmerRegNumber", myid);
                        //commandtrans.Parameters.AddWithValue("@District_Id", DistrictId);
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(commandtrans);
                        dataAdapter.Fill(dataset);
                        statu_cntr = 70;
                        trans.Commit();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            trans.Rollback();
            connection.Close();
        }
        finally
        {
            connection.Close();
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();
        }
        return dataset;

    }



    //19102012
    [WebMethod(Description = "17.2  Select FarmerIrrigationLoan From Online -Onn2Off")]
    public DataSet SelectIrrigationLoanOfFarmer(string DistrictId, string societyID, ChkPaddyUser userpwd, DataSet dsSocietyFarmerList)
    {
        string LogID = "";
        string date = "";
        try
        {
            dataset = new DataSet();

            if (connection != null && userpwd.username == "pp" && userpwd.password == "pp")
            {
                connection.Open();
                date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                LogID = societyID.ToString() + date;

                if (dsSocietyFarmerList != null)
                {
                    if (dsSocietyFarmerList.Tables[0].Rows.Count > 0)
                    {

                        string finalfarmerids = "";
                        foreach (DataRow dr in dsSocietyFarmerList.Tables[0].Rows)
                        {
                            string farmerid = dr["Farmer_Id"].ToString();
                            finalfarmerids = finalfarmerids + ",'" + farmerid + "'" + "";

                        }
                        string myid = finalfarmerids.Remove(0, 1);
                        commandtrans = connection.CreateCommand();
                        trans = connection.BeginTransaction();
                        commandtrans.Connection = connection;
                        commandtrans.Transaction = trans;
                        commandtrans.CommandType = CommandType.Text;
                        string qry = "SELECT [FarmerRegNumber],[District_Id],[Tehsil_Id] ,[Village_Id],[LoanAmount],[CreatedDate] FROM [IrrigationLoanOfFarmer] where [FarmerRegNumber] in (" + myid + ") and  District_Id='" + DistrictId + "'";
                        commandtrans.CommandText = qry;

                        //commandtrans.CommandType = CommandType.StoredProcedure;
                        // commandtrans.CommandText = "Proc_SelectIrrigationLoanOfFarmer";
                        commandtrans.Parameters.Clear();
                        //  commandtrans.Parameters.AddWithValue("@FarmerRegNumber", finalfarmerids);
                        //commandtrans.Parameters.AddWithValue("@District_Id", DistrictId);
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(commandtrans);
                        dataAdapter.Fill(dataset);
                        statu_cntr = 72;
                        trans.Commit();

                    }
                }
            }
        }
        catch (Exception ex)
        {
            trans.Rollback();
            connection.Close();
        }
        finally
        {
            connection.Close();
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();
        }
        return dataset;

    }


    //19102012
    [WebMethod(Description = "17.3  Select FarmerSocietyLoan From Online -Onn2Off")]
    public DataSet SelectSocietyLoanOfFarmer(string DistrictId, string societyID, ChkPaddyUser userpwd, DataSet dsSocietyFarmerList)
    {
        string LogID = "";
        string date = "";
        try
        {

            dataset = new DataSet();

            if (connection != null && userpwd.username == "pp" && userpwd.password == "pp")
            {
                connection.Open();
                date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                LogID = societyID.ToString() + date;
                if (dsSocietyFarmerList != null)
                {
                    if (dsSocietyFarmerList.Tables[0].Rows.Count > 0)
                    {
                        string finalfarmerids = "";
                        foreach (DataRow dr in dsSocietyFarmerList.Tables[0].Rows)
                        {
                            string farmerid = dr["Farmer_Id"].ToString();
                            finalfarmerids = finalfarmerids + ",'" + farmerid + "'" + "";

                        }
                        string myid = finalfarmerids.Remove(0, 1);
                        commandtrans = connection.CreateCommand();
                        trans = connection.BeginTransaction();
                        commandtrans.Connection = connection;
                        commandtrans.Transaction = trans;
                        commandtrans.CommandType = CommandType.Text;
                        string qry = "SELECT [FarmerRegNumber],[District_Id],[tehsil_code],[Village_Id],[Society_Id],[LoanAmount],[CreatedDate] FROM  [SocietyLoanOfFarmer] where [FarmerRegNumber] in (" + myid + ") and District_Id='" + DistrictId + "' and Society_Id='" + societyID + "' ";
                        commandtrans.CommandText = qry;

                        //commandtrans.CommandType = CommandType.StoredProcedure;
                        //commandtrans.CommandText = "Proc_SelectSocietyLoanOfFarmer";
                        commandtrans.Parameters.Clear();
                        //commandtrans.Parameters.AddWithValue("@FarmerRegNumber", finalfarmerids);
                        //commandtrans.Parameters.AddWithValue("@District_Id", DistrictId);
                        //commandtrans.Parameters.AddWithValue("@Society_Id", societyID);
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(commandtrans);
                        dataAdapter.Fill(dataset);
                        statu_cntr = 75;
                        trans.Commit();
                    }
                }
            }

        }
        catch (Exception ex)
        {
            trans.Rollback();
            connection.Close();
        }
        finally
        {
            connection.Close();
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();
        }
        return dataset;
    }


    //19102012
    [WebMethod(Description = "16.  Select TehsilYeild From Online -Onn2Off ")]
    public DataSet SelectTehsilYield(string DistrictId, string societyID, ChkPaddyUser userpwd)
    {
        string LogID = "";
        string date = "";

        try
        {
            dataset = new DataSet();

            if (connection != null && userpwd.username == "pp" && userpwd.password == "pp")
            {
                connection.Open();
                date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                LogID = societyID.ToString() + date;

                commandtrans = connection.CreateCommand();
                trans = connection.BeginTransaction();
                commandtrans.Connection = connection;
                commandtrans.Transaction = trans;
                commandtrans.CommandType = CommandType.StoredProcedure;
                commandtrans.CommandText = "Proc_SelectTehsilYield";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandtrans);
                dataAdapter.Fill(dataset);
                statu_cntr = 80;
                trans.Commit();
            }
        }
        catch (Exception ex)
        {
            trans.Rollback();
            connection.Close();
        }
        finally
        {
            connection.Close();
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();
        }
        return dataset;

    }



    //19102012
    [WebMethod(Description = "18.  Select New Village From Online -Onn2Off")]
    public DataSet SelectNewVillage(string DistrictId, string societyID, ChkPaddyUser userpwd)
    {

        string LogID = "";
        string date = "";

        try
        {
            dataset = new DataSet();

            if (connection != null && userpwd.username == "pp" && userpwd.password == "pp")
            {
                date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                LogID = societyID.ToString() + date;
                connection.Open();
                commandtrans = connection.CreateCommand();
                trans = connection.BeginTransaction();
                commandtrans.Connection = connection;
                commandtrans.Transaction = trans;
                commandtrans.CommandType = CommandType.StoredProcedure;
                commandtrans.CommandText = "Proc_SelectVillageMaster";
                commandtrans.Parameters.Clear();
                commandtrans.Parameters.AddWithValue("@District_Id", DistrictId);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandtrans);
                dataAdapter.Fill(dataset);
                statu_cntr = 85;
                trans.Commit();
            }

        }
        catch (Exception ex)
        {
            trans.Rollback();
            connection.Close();
        }
        finally
        {
            connection.Close();
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();
        }
        return dataset;
    }



    //19102012
    [WebMethod(Description = "0.1  Select DateControl From Onnline -Onn2Off")]
    public DataSet SelectDateControl(string DistrictId, string societyID, ChkPaddyUser userpwd)
    {

        string LogID = "";
        string date = "";

        try
        {
            dataset = new DataSet();

            if (connection != null && userpwd.username == "pp" && userpwd.password == "pp")
            {
                date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                LogID = societyID.ToString() + date;
                connection.Open();
                commandtrans = connection.CreateCommand();
                trans = connection.BeginTransaction();
                commandtrans.Connection = connection;
                commandtrans.Transaction = trans;
                commandtrans.CommandType = CommandType.StoredProcedure;
                commandtrans.CommandText = "Proc_SelectDateControl";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandtrans);
                dataAdapter.Fill(dataset);
                statu_cntr = 100;
                trans.Commit();


            }
        }
        catch (Exception ex)
        {
            trans.Rollback();
            connection.Close();
        }
        finally
        {
            connection.Close();
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();
        }
        return dataset;

    }



    //19102012
    [WebMethod(Description = "0.1  Select tbl_mpwlc_godownstorage From Onnline -Onn2Off")]
    public DataSet Selectgodownstorage(string DistrictId, string societyID, ChkPaddyUser userpwd)
    {

        string LogID = "";
        string date = "";

        try
        {
            dataset = new DataSet();

            if (connection != null && userpwd.username == "pp" && userpwd.password == "pp")
            {
                date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                LogID = societyID.ToString() + date;
                connection.Open();
                commandtrans = connection.CreateCommand();
                trans = connection.BeginTransaction();
                commandtrans.Connection = connection;
                commandtrans.Transaction = trans;
                commandtrans.CommandType = CommandType.Text;
                commandtrans.CommandText = "SELECT *  FROM tbl_MPWLC_Godown_Storage";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandtrans);
                dataAdapter.Fill(dataset);
                statu_cntr = 100;
                trans.Commit();

            }
        }
        catch (Exception ex)
        {
            trans.Rollback();
            connection.Close();
        }
        finally
        {
            connection.Close();
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();
        }
        return dataset;

    }



    //19102012
    [WebMethod(Description = "New  Select TransportMaster From Online  -Onn2Off")]
    public DataSet SelectTransportMaster(string DistrictId, string societyID, ChkPaddyUser userpwd)
    {
        string LogID = "";
        string date = "";
        try
        {
            dataset = new DataSet();
            if (connection != null && userpwd.username == "pp" && userpwd.password == "pp")
            {
                date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                LogID = societyID.ToString() + date;
                connection.Open();
                commandtrans = connection.CreateCommand();
                trans = connection.BeginTransaction();
                commandtrans.Connection = connection;
                commandtrans.Transaction = trans;
                commandtrans.CommandType = CommandType.Text;
                commandtrans.CommandText = "SELECT *  FROM TransportMaster where District_ID='" + DistrictId + "' and SocietyCode='" + societyID + "'";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandtrans);
                dataAdapter.Fill(dataset);
                statu_cntr = 97;
                trans.Commit();

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
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();
        }
        return dataset;


    }



    //15112012 
    [WebMethod(Description = "Change UserPassword From online  -Onn2Off")]
    public DataSet SelectUserPassword(string DistrictId, string societyID, ChkPaddyUser userpwd)
    {
        string LogID = "";
        string date = "";
        try
        {
            dataset = new DataSet();
            if (connection != null && userpwd.username == "pp" && userpwd.password == "pp")
            {
                date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                LogID = societyID.ToString() + date;
                connection.Open();
                commandtrans = connection.CreateCommand();
                trans = connection.BeginTransaction();
                commandtrans.Connection = connection;
                commandtrans.Transaction = trans;
                commandtrans.CommandType = CommandType.Text;
                commandtrans.CommandText = "SELECT *  FROM UserPassword ";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandtrans);
                dataAdapter.Fill(dataset);
                statu_cntr = 97;
                trans.Commit();
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
            UploadDataStatus(statu_cntr, LogID);
            trans.Dispose();
            commandtrans.Dispose();
        }
        return dataset;
    }
    #endregion


    #region Common Covert Function

    private String getDate_MDY(string inDate)
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

    private float CheckNullFloat(string Val)
    {
        float rval = 0.0F;
        if (Val.Trim() == "" || Val.ToLower().Contains("n") || Val == null)
        {
            rval = 0;
        }
        else
        {
            rval = float.Parse(Val);
        }
        return rval;
    }

    private Int32 CheckNullInt(string Val)
    {
        Int32 rval = 0;
        if (Val.Trim() == "" || Val.ToLower().Contains("n") || Val == null)
        {
            rval = 0;
        }
        else
        {
            rval = Int32.Parse(Val);
        }
        return rval;
    }


    private string chkDate(string Date)
    {
        string retDate = "";
        if (Date != "")
        {
            DateTime imd_date = Convert.ToDateTime(Date);
            retDate = imd_date.ToString("MM/dd/yyyy");
        }
        return retDate;

    }

    #endregion

}


public class ChkPaddyUser
: System.Web.Services.Protocols.SoapHeader
{
    public string username;
    public string password;
}



