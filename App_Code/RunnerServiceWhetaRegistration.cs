using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


/// <summary>
/// Summary description for RunnerServiceWhetaRegistration
/// 

///// </summary>
//

[WebService(Namespace = "http://microsoft.co.in/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

public class RunnerServiceWhetaRegistration : System.Web.Services.WebService
{
    private SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2013"].ToString());
    private SqlCommand command, cmd;
    private SqlDataAdapter dataAdapter;
    private DataSet dataset;
    private SqlTransaction trans = null;
    private SqlCommand commandt = null;

    public RunnerServiceWhetaRegistration()
    {

    }

    public chkWheatRunnerReg MyHeader;
    [WebMethod]
    [SoapHeader("MyHeader")]

    public bool chkInformation(chkWheatRunnerReg login)
    {
        bool result = false;
        result = login.chkSVC();
        return result;
    }


    #region Offline To Online

    [WebMethod(Description = "10012013")]
    public bool InRunner(string SocietyId, string RunnerVer, string DistrictId)
    {
        bool result = false;
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();


            commandt = new SqlCommand();
            commandt = connection.CreateCommand();
            //commandt.CommandTimeout = 0;
            commandt.Transaction = trans;
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "Select count(*) from RunnerRegistration where RunnerID='" + SocietyId + "'";
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


    [WebMethod]
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

                // string UpdationDate = dsInitial.Tables[0].Rows[0]["UpdationDate"].ToString();

                string Societycreditlimit = dsInitial.Tables[0].Rows[0]["Societycreditlimit"].ToString();
                string OneFarmerLimit = dsInitial.Tables[0].Rows[0]["OneFarmerLimit"].ToString();
                string MgrMobileNo = dsInitial.Tables[0].Rows[0]["MgrMobileNo"].ToString();
                string SocBandaranCapacity = dsInitial.Tables[0].Rows[0]["SocBandaranCapacity"].ToString();
                string DateTimeOfInstall = getRDate_MDY(dsInitial.Tables[0].Rows[0]["DateTimeOfInstall"].ToString());
                string Remarks = dsInitial.Tables[0].Rows[0]["Remarks"].ToString();


                cmd = connection.CreateCommand();
                cmd.Transaction = trans;
                // cmd.CommandTimeout = 0;
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
                if (req > 0)
                {
                    trans.Commit();
                    result = true;
                }

            }

            else
            {
                string VersionNo = dsInitial.Tables[0].Rows[0]["VersionNo"].ToString();
                cmd = connection.CreateCommand();
                cmd.Transaction = trans;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Clear();
                cmd.CommandText = "update Initial set VersionNo='" + VersionNo + "' where Society_Id='" + dsInitial.Tables[0].Rows[0]["Society_Id"].ToString() + "' ";
                int req = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (req > 0)
                {
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
    public bool InInstallationInfo(DataSet dsInstallationInfo)
    {
        bool result = false;
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            foreach (DataRow dr in dsInstallationInfo.Tables[0].Rows)
            {
                string SocietyID = dr["SocietyID"].ToString();
                string District_ID = dr["District_ID"].ToString();

                commandt = connection.CreateCommand();
                commandt.Transaction = trans;
                //commandt.CommandTimeout = 0;
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
                    //cmd.CommandTimeout = 0;
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
        return result;
    }

    [WebMethod]
    public bool InOperator(DataSet dsOperator)
    {
        bool result = false;
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            foreach (DataRow dr in dsOperator.Tables[0].Rows)
            {
                string Op_id = dr["Op_id"].ToString();
                string PCID = dr["PCID"].ToString();
                string SocietyID = dr["SocietyID"].ToString();
                string DistrictId = dr["DistrictId"].ToString();

                commandt = connection.CreateCommand();
                commandt.Transaction = trans;
                //commandt.CommandTimeout = 0;
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
                    // cmd.CommandTimeout = 0;
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


    [WebMethod(Description = "change on 04022013")]
    public bool InFarmer(DataSet dsFarmer)
    {
        bool result = false;
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            foreach (DataRow dr in dsFarmer.Tables[0].Rows)
            {
                string District_Id = dr["District_Id"].ToString();
                string Tehsil_Id = dr["Tehsil_Id"].ToString();
                string Village_Id = dr["Village_Id"].ToString();
                string Farmer_Id = dr["Farmer_Id"].ToString();
                commandt = connection.CreateCommand();
                commandt.Transaction = trans;
                // commandt.CommandTimeout = 3600 * 3;
                commandt.CommandType = CommandType.Text;
                commandt.CommandText = "select count(*) from FarmerRegistration where District_Id='" + District_Id + "' and Tehsil_Id='" + Tehsil_Id + "' and Village_Id='" + Village_Id + "' and Farmer_Id='" + Farmer_Id + "'";
                Int64 res = Convert.ToInt64(commandt.ExecuteScalar());
                commandt.Dispose();
                if (res <= 0)
                {

                    commandt = connection.CreateCommand();
                    commandt.Transaction = trans;
                    commandt.CommandType = CommandType.Text;
                    commandt.CommandText = "select count(*) from FarmerRegistration_After31Jan where District_Id='" + District_Id + "' and Tehsil_Id='" + Tehsil_Id + "' and Village_Id='" + Village_Id + "' and Farmer_Id='" + Farmer_Id + "'";
                    Int64 re = Convert.ToInt64(commandt.ExecuteScalar());
                    commandt.Dispose();
                    if (re <= 0)
                    {

                        string VillageName = dr["VillageName"].ToString();
                        string FarmerName = dr["FarmerName"].ToString();
                        string FatherHusName = dr["FatherHusName"].ToString();
                        string Gram_Panchayat = dr["Gram_Panchayat"].ToString();
                        string PatwariHalkaNo = dr["PatwariHalkaNo"].ToString();
                        string Mobileno = dr["Mobileno"].ToString();
                        string Category = dr["Category"].ToString();
                        string RinPustikaNo = dr["RinPustikaNo"].ToString();
                        string Farmer_EID_UID_No = dr["Farmer_EID_UID_No"].ToString();
                        string Farmer_KCCNo = dr["Farmer_KCCNo"].ToString();
                        string Farmer_BankName_New = dr["Farmer_BankName_New"].ToString();
                        string Farmer_BankBranchName = dr["Farmer_BankBranchName"].ToString();
                        string Farmer_BankAccountNo = dr["Farmer_BankAccountNo"].ToString();
                        string PC_ID = dr["PC_ID"].ToString();
                        string Procured_SocietyID = dr["Procured_SocietyID"].ToString();
                        string Procured_Dist_ID = dr["Procured_Dist_ID"].ToString();
                        string Procured_Place = dr["Procured_Place"].ToString();
                        //string CropExpected_Date = getDate_MDY(dr["CropExpected_Date"].ToString());
                        string CropExpected_Date = getRDate_MDY(dr["CropExpected_Date"].ToString());
                        string UserID = dr["UserID"].ToString();
                        //string CreatedDate = getDate_MDY(dr["CreatedDate"].ToString());
                        string CreatedDate = getRDate_MDY(dr["CreatedDate"].ToString());
                        string RegistrationDate = getRDate_MDY(dr["RegistrationDate"].ToString());
                        string ip = dr["ip"].ToString();
                        string IsApproved = dr["IsApproved"].ToString();
                        string Status = dr["Status"].ToString();

                        cmd = connection.CreateCommand();
                        cmd.Transaction = trans;
                        // cmd.CommandTimeout = 3600 * 3;
                        cmd.CommandType = CommandType.StoredProcedure;
                        // cmd.CommandText = "in_Farmer";
                        cmd.CommandText = "in_Farmer_After31Jan";
                        cmd.Parameters.Clear();

                        cmd.Parameters.AddWithValue("@District_Id", District_Id);
                        cmd.Parameters.AddWithValue("@Village_Id", Village_Id);
                        cmd.Parameters.AddWithValue("@VillageName", VillageName);
                        cmd.Parameters.AddWithValue("@Tehsil_Id", Tehsil_Id);
                        cmd.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                        cmd.Parameters.AddWithValue("@FarmerName", FarmerName);
                        cmd.Parameters.AddWithValue("@FatherHusName", FatherHusName);
                        cmd.Parameters.AddWithValue("@Gram_Panchayat", Gram_Panchayat);
                        cmd.Parameters.AddWithValue("@PatwariHalkaNo", PatwariHalkaNo);
                        cmd.Parameters.AddWithValue("@Mobileno", Mobileno);
                        cmd.Parameters.AddWithValue("@Category", Category);
                        cmd.Parameters.AddWithValue("@RinPustikaNo", RinPustikaNo);
                        cmd.Parameters.AddWithValue("@Farmer_EID_UID_No", Farmer_EID_UID_No);
                        cmd.Parameters.AddWithValue("@Farmer_KCCNo", Farmer_KCCNo);
                        cmd.Parameters.AddWithValue("@Farmer_BankName_New", Farmer_BankName_New);
                        cmd.Parameters.AddWithValue("@Farmer_BankBranchName", Farmer_BankBranchName);
                        cmd.Parameters.AddWithValue("@Farmer_BankAccountNo", Farmer_BankAccountNo);
                        cmd.Parameters.AddWithValue("@PC_ID", PC_ID);
                        cmd.Parameters.AddWithValue("@Procured_SocietyID", Procured_SocietyID);
                        cmd.Parameters.AddWithValue("@Procured_Dist_ID", Procured_Dist_ID);
                        cmd.Parameters.AddWithValue("@Procured_Place", Procured_Place);
                        cmd.Parameters.AddWithValue("@CropExpected_Date", CropExpected_Date);
                        cmd.Parameters.AddWithValue("@UserID", UserID);
                        cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                        cmd.Parameters.AddWithValue("@RegistrationDate", RegistrationDate);
                        cmd.Parameters.AddWithValue("@ip", HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString());
                        cmd.Parameters.AddWithValue("@IsApproved", IsApproved);
                        cmd.Parameters.AddWithValue("@Status", Status);
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


    [WebMethod(Description = "16012013,04022013")]
    public bool InFarmerLandRecord(DataSet dsFarmerLandRecord, string SocietyId, string DistrictId)
    {
        bool result = false;
        try
        {

            OpenConnection();
            trans = connection.BeginTransaction();

            foreach (DataRow dr in dsFarmerLandRecord.Tables[0].Rows)
            {
                string Farmer_Id = dr["Farmer_Id"].ToString();
                string TransID = dr["TransID"].ToString();
                string rakbasinchit = dr["Rakba_crop_sinchit"].ToString();
                commandt = connection.CreateCommand();
                commandt.Transaction = trans;
                // commandt.CommandTimeout = 3600 * 3;
                commandt.CommandType = CommandType.Text;
                //commandt.CommandText = "select count(*) from FarmerRegistration where PC_ID='" + SocietyId + "' and District_Id='" + DistrictId + "' and Farmer_Id='" + Farmer_Id + "'";
                //commandt.CommandText = "select count(*) from FarmerRegistration_After31Jan where PC_ID='" + SocietyId + "' and District_Id='" + DistrictId + "' and Farmer_Id='" + Farmer_Id + "'";
                commandt.CommandText = "select count(*) from Farmer_LandRecordDescription where TransID='" + TransID + "' and Farmer_Id='" + Farmer_Id + "' ";
                //Int64 resFR = Convert.ToInt64(commandt.ExecuteScalar());
                Int64 resFRL = Convert.ToInt64(commandt.ExecuteScalar());
                commandt.Dispose();

                if (resFRL <= 0)
                {
                    commandt = connection.CreateCommand();
                    commandt.Transaction = trans;
                    //commandt.CommandTimeout = 3600 * 3;
                    commandt.CommandType = CommandType.Text;
                    commandt.CommandText = "select count(*) from Farmer_LandRecordDescription_After31Jan where TransID='" + TransID + "' and Farmer_Id='" + Farmer_Id + "' ";
                    Int64 resFL = Convert.ToInt64(commandt.ExecuteScalar());
                    commandt.Dispose();
                    if (resFL <= 0)
                    {
                        string Procured_SocietyID = dr["Procured_SocietyID"].ToString();
                        string Village_ID = dr["Village_ID"].ToString();
                        string VillageName = dr["VillageName"].ToString();
                        string Crop_ID = dr["Crop_ID"].ToString();
                        string LandOwner_Name = dr["LandOwner_Name"].ToString();
                        string LandOwner_RinPustikaNo = dr["LandOwner_RinPustikaNo"].ToString();
                        string LandType = dr["LandType"].ToString();
                        string KhasaraNo = dr["KhasaraNo"].ToString();
                        string Rakba = dr["Rakba"].ToString();
                        string Rakba_crop_sinchit = dr["Rakba_crop_sinchit"].ToString();
                        string Rakba_crop_asinchit = dr["Rakba_crop_asinchit"].ToString();
                        string Rakba_crop_sinchit_qty = dr["Rakba_crop_sinchit_qty"].ToString();
                        string Rakba_crop_asinchit_qty = dr["Rakba_crop_asinchit_qty"].ToString();
                        string Procured_qty = dr["Procured_qty"].ToString();
                        string crpcode = dr["crpcode"].ToString();

                        cmd = connection.CreateCommand();
                        cmd.Transaction = trans;
                        //cmd.CommandTimeout = 3600 * 3;
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandText = "in_FarmerLandRecord";
                        cmd.CommandText = "in_FarmerLandRecord_After31Jan";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@TransID", TransID);
                        cmd.Parameters.AddWithValue("@District_Id", DistrictId);
                        cmd.Parameters.AddWithValue("@Procured_SocietyID", Procured_SocietyID);
                        cmd.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                        cmd.Parameters.AddWithValue("@Village_ID", Village_ID);
                        cmd.Parameters.AddWithValue("@VillageName", VillageName);
                        cmd.Parameters.AddWithValue("@Crop_ID", Crop_ID);
                        cmd.Parameters.AddWithValue("@LandOwner_Name", LandOwner_Name);
                        cmd.Parameters.AddWithValue("@LandOwner_RinPustikaNo", LandOwner_RinPustikaNo);
                        cmd.Parameters.AddWithValue("@LandType", LandType);
                        cmd.Parameters.AddWithValue("@KhasaraNo", KhasaraNo);
                        cmd.Parameters.AddWithValue("@Rakba", Rakba);
                        cmd.Parameters.AddWithValue("@Rakba_crop_sinchit", Rakba_crop_sinchit);
                        cmd.Parameters.AddWithValue("@Rakba_crop_asinchit", Rakba_crop_asinchit);
                        cmd.Parameters.AddWithValue("@Rakba_crop_sinchit_qty", Rakba_crop_sinchit_qty);
                        cmd.Parameters.AddWithValue("@Rakba_crop_asinchit_qty", Rakba_crop_asinchit_qty);
                        cmd.Parameters.AddWithValue("@Procured_qty", Procured_qty);
                        cmd.Parameters.AddWithValue("@crpcode", crpcode);
                        int req = cmd.ExecuteNonQuery();
                        cmd.Dispose();

                    }
                    else
                    {
                        cmd = connection.CreateCommand();
                        cmd.Transaction = trans;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "select * from Farmer_LandRecordDescription where  TransID='" + TransID + "' and Farmer_Id='" + Farmer_Id + "' and Procured_SocietyID ='" + SocietyId + "' and District_Id ='" + DistrictId + "' ";
                        cmd.Parameters.Clear();
                        dataAdapter = new SqlDataAdapter(cmd);
                        dataset = new DataSet();
                        dataAdapter.Fill(dataset);
                        dataAdapter.Dispose();
                        cmd.Dispose();

                        if (dataset != null)
                        {
                            if (dataset.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow datar in dataset.Tables[0].Rows)
                                {
                                    string _Rakba_crop_sinchit = datar["Rakba_crop_sinchit"].ToString();
                                    string _Rakba_crop_asinchit = datar["Rakba_crop_asinchit"].ToString();
                                    if (Convert.ToDouble(_Rakba_crop_sinchit) > 0)
                                    {
                                        if (_Rakba_crop_sinchit == _Rakba_crop_asinchit)
                                        {
                                            cmd = connection.CreateCommand();
                                            cmd.Transaction = trans;
                                            cmd.CommandType = CommandType.Text;
                                            cmd.Parameters.Clear();
                                            cmd.CommandText = "update Farmer_LandRecordDescription set Farmer_LandRecordDescription.Rakba_crop_sinchit='" + rakbasinchit + "' where TransID='" + TransID + "' and Farmer_Id='" + Farmer_Id + "' and Procured_SocietyID ='" + SocietyId + "' and District_Id ='" + DistrictId + "'";
                                            int rsr = cmd.ExecuteNonQuery();
                                            cmd.Dispose();
                                        }
                                    }
                                    else if (Convert.ToDouble(_Rakba_crop_sinchit) <= 0)
                                    {
                                        cmd = connection.CreateCommand();
                                        cmd.Transaction = trans;
                                        cmd.CommandType = CommandType.Text;
                                        cmd.Parameters.Clear();
                                        cmd.CommandText = "update Farmer_LandRecordDescription set Farmer_LandRecordDescription.Rakba_crop_sinchit='" + rakbasinchit + "' where TransID='" + TransID + "' and Farmer_Id='" + Farmer_Id + "' and Procured_SocietyID ='" + SocietyId + "' and District_Id ='" + DistrictId + "'";
                                        int rs = cmd.ExecuteNonQuery();
                                        cmd.Dispose();
                                    }
                                }
                            }
                        }
                        dataset.Dispose();
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


    [WebMethod]
    public bool InFarmerUpdationRequest(DataSet dsFarmer, string SocietyId, string DistrictId)
    {
        bool result = false;
        try
        {

            OpenConnection();
            trans = connection.BeginTransaction();

            foreach (DataRow dr in dsFarmer.Tables[0].Rows)
            {
                string FarmerId = dr["FarmerId"].ToString();
                string UpdationDate_Off = getRDate_MDY(dr["UpdationDate"].ToString());

                commandt = connection.CreateCommand();
                commandt.CommandTimeout = 600000;
                commandt.Transaction = trans;
                commandt.CommandType = CommandType.Text;
                commandt.CommandText = "select count(*) from FarmerUpdationRequest where DisrictID='" + DistrictId + "' and SocietyID='" + SocietyId + "' and  FarmerId='" + FarmerId + "' and UpdationDate_Off ='" + UpdationDate_Off + "'";
                Int64 res = Convert.ToInt64(commandt.ExecuteScalar());
                commandt.Dispose();
                if (res <= 0)
                {
                    string UpdationDate_Onn = getRDate_MDY(System.DateTime.Now.ToShortDateString());
                    string IsFRUpdated = dr["IsFRUpdated"].ToString();
                    string IsLRUpdated = dr["IsLRUpdated"].ToString();
                    cmd = connection.CreateCommand();
                    cmd.CommandTimeout = 600000;
                    cmd.Transaction = trans;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandText = "in_FarmerUpdationRequest";
                    cmd.Parameters.AddWithValue("@FarmerId", FarmerId);
                    cmd.Parameters.AddWithValue("@DisrictID", DistrictId);
                    cmd.Parameters.AddWithValue("@SocietyID", SocietyId);
                    cmd.Parameters.AddWithValue("@IsFRUpdated", IsFRUpdated);
                    cmd.Parameters.AddWithValue("@IsLRUpdated", IsLRUpdated);
                    cmd.Parameters.AddWithValue("@UpdationDate_Off", UpdationDate_Off);
                    cmd.Parameters.AddWithValue("@UpdationDate_Onn", UpdationDate_Onn);
                    int req = cmd.ExecuteNonQuery();
                    cmd.Dispose();
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


    [WebMethod]
    public bool UpFarmer(DataSet dsUpFarmer)
    {
        bool result = false;
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();

            foreach (DataRow dr in dsUpFarmer.Tables[0].Rows)
            {

                string FarmerId = dr["Farmer_Id"].ToString();
                string DisrictID = dr["District_Id"].ToString();
                string SocietyID = dr["PC_ID"].ToString();

                //Insert Into Updation log
               // cmd = connection.CreateCommand();
               // cmd.Transaction = trans;
             //   cmd.CommandType = CommandType.Text;
               // cmd.CommandTimeout = 600000;
               // cmd.CommandText = "select COUNT(*) from update_FarmerRegistrationLog where Farmer_Id='" + FarmerId + "' and District_Id='" + DisrictID + "' and PC_ID='" + SocietyID + "'";
               // int rexs = Convert.ToInt32(cmd.ExecuteScalar());
             //   if (rexs <= 0)
              //  {
                  //  cmd = connection.CreateCommand();
                //    cmd.Transaction = trans;
                 //   cmd.CommandType = CommandType.StoredProcedure;
                 //   cmd.CommandTimeout = 600000;
                  //  cmd.CommandText = "in_Update_Farmer";
                 //   cmd.Parameters.Clear();
                 //   cmd.Parameters.AddWithValue("@District_Id", DisrictID);
                 //   cmd.Parameters.AddWithValue("@Farmer_Id", FarmerId);
                 //   cmd.Parameters.AddWithValue("@PC_ID", SocietyID);
               //     int req = cmd.ExecuteNonQuery();
                  //  cmd.Dispose();

                    //update
               //     if (req > 0)
                //    {
                        string District_Id = dr["District_Id"].ToString();
                        string Village_Id = dr["Village_Id"].ToString();
                        string Tehsil_Id = dr["Tehsil_Id"].ToString();
                        string Farmer_Id = dr["Farmer_Id"].ToString();
                        string Gram_Panchayat = dr["Gram_Panchayat"].ToString();
                        string PatwariHalkaNo = dr["PatwariHalkaNo"].ToString();
                        string Mobileno = dr["Mobileno"].ToString();
                        string RinPustikaNo = dr["RinPustikaNo"].ToString();
                        string Farmer_EID_UID_No = dr["Farmer_EID_UID_No"].ToString();
                        string Farmer_KCCNo = dr["Farmer_KCCNo"].ToString();
                        string Farmer_BankName_New = dr["Farmer_BankName_New"].ToString();
                        string Farmer_BankBranchName = dr["Farmer_BankBranchName"].ToString();
                        string Farmer_BankAccountNo = dr["Farmer_BankAccountNo"].ToString();
                        string PC_ID = dr["PC_ID"].ToString();
                        string Procured_SocietyID = dr["Procured_SocietyID"].ToString();
                        string Procured_Dist_ID = dr["Procured_Dist_ID"].ToString();
                        string Procured_Place = dr["Procured_Place"].ToString();
                        string CropExpected_Date = getRDate_MDY(dr["CropExpected_Date"].ToString());
                        string UserID = dr["UserID"].ToString();
                        string updatedDate = getRDate_MDY(dr["updatedDate"].ToString());
                        string ip = dr["ip"].ToString();
                        string IsApproved = dr["IsApproved"].ToString();
                        string Status = dr["Status"].ToString();


                        cmd = connection.CreateCommand();
                        cmd.Transaction = trans;
                        cmd.CommandTimeout = 60000;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "up_Farmer";
                        cmd.Parameters.Clear();

                        cmd.Parameters.AddWithValue("@District_Id", District_Id);
                        cmd.Parameters.AddWithValue("@Village_Id", Village_Id);
                        cmd.Parameters.AddWithValue("@Tehsil_Id", Tehsil_Id);
                        cmd.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                        cmd.Parameters.AddWithValue("@Gram_Panchayat", Gram_Panchayat);
                        cmd.Parameters.AddWithValue("@PatwariHalkaNo", PatwariHalkaNo);
                        cmd.Parameters.AddWithValue("@Mobileno", Mobileno);
                        cmd.Parameters.AddWithValue("@RinPustikaNo", RinPustikaNo);
                        cmd.Parameters.AddWithValue("@Farmer_EID_UID_No", Farmer_EID_UID_No);
                        cmd.Parameters.AddWithValue("@Farmer_KCCNo", Farmer_KCCNo);
                        cmd.Parameters.AddWithValue("@Farmer_BankName_New", Farmer_BankName_New);
                        cmd.Parameters.AddWithValue("@Farmer_BankBranchName", Farmer_BankBranchName);
                        cmd.Parameters.AddWithValue("@Farmer_BankAccountNo", Farmer_BankAccountNo);
                        cmd.Parameters.AddWithValue("@PC_ID", PC_ID);
                        cmd.Parameters.AddWithValue("@Procured_SocietyID", Procured_SocietyID);
                        cmd.Parameters.AddWithValue("@Procured_Dist_ID", Procured_Dist_ID);
                        cmd.Parameters.AddWithValue("@Procured_Place", Procured_Place);
                        cmd.Parameters.AddWithValue("@CropExpected_Date", CropExpected_Date);
                        cmd.Parameters.AddWithValue("@UserID", UserID);
                        cmd.Parameters.AddWithValue("@updatedDate", updatedDate);
                        cmd.Parameters.AddWithValue("@ip", HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString());
                        cmd.Parameters.AddWithValue("@IsApproved", IsApproved);
                        cmd.Parameters.AddWithValue("@Status", Status);
                        int _req = cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
               // }

           // }
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


    [WebMethod]
    public bool UpFarmerLandRecord(DataSet dsUpFarmerLandRecord, string SocietyId, string DistrictId)
    {
        bool result = false;
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            foreach (DataRow dr in dsUpFarmerLandRecord.Tables[0].Rows)
            {
                string TransID = dr["TransID"].ToString();
                string Farmer_Id = dr["Farmer_Id"].ToString();
                string Procured_SocietyID = dr["Procured_SocietyID"].ToString();
                string District_Id = dr["District_Id"].ToString();


               // cmd = connection.CreateCommand();
           //     cmd.Transaction = trans;
            //    cmd.CommandType = CommandType.Text;
              //  cmd.CommandText = "select COUNT(*) from update_Farmer_LandRecordDescriptionLog where TransID='" + TransID + "' and  Farmer_Id='" + Farmer_Id + "' and District_Id='" + District_Id + "' and Procured_SocietyID='" + SocietyId + "'";
              //  cmd.CommandTimeout = 60000;
              //  cmd.Parameters.Clear();
            //    int rexs = Convert.ToInt32(cmd.ExecuteScalar());

              //  if (rexs <= 0)
              //  {
                   // cmd = connection.CreateCommand();
                  //  cmd.Transaction = trans;
                   // cmd.CommandType = CommandType.StoredProcedure;
                  //  cmd.CommandText = "in_Update_FarmerLandRecord";
                 //   cmd.CommandTimeout = 60000;
                  //  cmd.Parameters.Clear();
                   // cmd.Parameters.AddWithValue("@TransID", TransID);
                   // cmd.Parameters.AddWithValue("@District_Id", District_Id);
                 //   cmd.Parameters.AddWithValue("@Procured_SocietyID", SocietyId);
                  //  cmd.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                  //  int req = cmd.ExecuteNonQuery();
                  //  cmd.Dispose();
                 //   if (req > 0)
                 //   {
                        string Village_ID = dr["Village_ID"].ToString();
                        string VillageName = dr["VillageName"].ToString();
                        string Crop_ID = dr["Crop_ID"].ToString();
                        string LandOwner_Name = dr["LandOwner_Name"].ToString();
                        string LandOwner_RinPustikaNo = dr["LandOwner_RinPustikaNo"].ToString();
                        string LandType = dr["LandType"].ToString();
                        string KhasaraNo = dr["KhasaraNo"].ToString();
                        string Rakba = dr["Rakba"].ToString();
                        string Rakba_crop_sinchit = dr["Rakba_crop_sinchit"].ToString();
                        string Rakba_crop_asinchit = dr["Rakba_crop_asinchit"].ToString();
                        string Rakba_crop_sinchit_qty = dr["Rakba_crop_sinchit_qty"].ToString();
                        string Rakba_crop_asinchit_qty = dr["Rakba_crop_asinchit_qty"].ToString();
                        string Procured_qty = dr["Procured_qty"].ToString();
                        string crpcode = dr["crpcode"].ToString();

                        cmd = connection.CreateCommand();
                        cmd.Transaction = trans;
                        cmd.CommandTimeout = 600000;
                        cmd.Parameters.Clear();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "up_FarmerLandRecord";
                        cmd.Parameters.AddWithValue("@TransID", TransID);
                        cmd.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                        cmd.Parameters.AddWithValue("@Village_ID", Village_ID);
                        cmd.Parameters.AddWithValue("VillageName", VillageName);
                        cmd.Parameters.AddWithValue("@Crop_ID", Crop_ID);
                        cmd.Parameters.AddWithValue("@LandOwner_Name ", LandOwner_Name);
                        cmd.Parameters.AddWithValue("@LandOwner_RinPustikaNo ", LandOwner_RinPustikaNo);
                        cmd.Parameters.AddWithValue("@LandType", LandType);
                        cmd.Parameters.AddWithValue("@KhasaraNo", KhasaraNo);
                        cmd.Parameters.AddWithValue("@Rakba", Rakba);
                        cmd.Parameters.AddWithValue("@Rakba_crop_sinchit", Rakba_crop_sinchit);
                        cmd.Parameters.AddWithValue("@Rakba_crop_asinchit", Rakba_crop_asinchit);
                        cmd.Parameters.AddWithValue("@Rakba_crop_sinchit_qty", Rakba_crop_sinchit_qty);
                        cmd.Parameters.AddWithValue("@Rakba_crop_asinchit_qty", Rakba_crop_asinchit_qty);
                        cmd.Parameters.AddWithValue("@Procured_qty", Procured_qty);
                        cmd.Parameters.AddWithValue("@crpcode", crpcode);
                        int req_ = cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }

            //    }
           // }

            trans.Commit();
            result = false;
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
    public bool InSocietyChangeLog(DataSet dsSocietyChangeLog)
    {
        bool result = false;
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();

            foreach (DataRow dr in dsSocietyChangeLog.Tables[0].Rows)
            {
                string District_ID = dr["District_ID"].ToString();
                string Farmer_ID = dr["Farmer_ID"].ToString();
                string OLD_PC_ID = dr["OLD_PC_ID"].ToString();
                string NEW_PC_ID = dr["NEW_PC_ID"].ToString();


                commandt = connection.CreateCommand();
                commandt.Transaction = trans;
                //commandt.CommandTimeout = 0;
                commandt.Parameters.Clear();
                commandt.CommandType = CommandType.Text;
                commandt.CommandText = "select count(*) from SocietyChangeLog where District_ID='" + District_ID + "' and   Farmer_ID='" + Farmer_ID + "' and OLD_PC_ID='" + OLD_PC_ID + "' and NEW_PC_ID='" + NEW_PC_ID + "'";
                Int64 res = Convert.ToInt64(commandt.ExecuteScalar());
                commandt.Dispose();
                if (res <= 0)
                {
                    string UpdationDate = getRDate_MDY(dr["UpdationDate"].ToString());

                    cmd = connection.CreateCommand();
                    cmd.Transaction = trans;
                    //  cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "inSocieyChangeLog";
                    cmd.Parameters.Clear();

                    cmd.Parameters.AddWithValue("@Farmer_ID", Farmer_ID);
                    cmd.Parameters.AddWithValue("@District_ID", District_ID);
                    cmd.Parameters.AddWithValue("@OLD_PC_ID", OLD_PC_ID);
                    cmd.Parameters.AddWithValue("@NEW_PC_ID", NEW_PC_ID);
                    cmd.Parameters.AddWithValue("@UpdationDate", UpdationDate);
                    int req = cmd.ExecuteNonQuery();

                    if (req > 0)
                    {
                        cmd = connection.CreateCommand();
                        cmd.Transaction = trans;
                        //  cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "upFarmerSocietyOnline";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Farmer_ID", Farmer_ID);
                        cmd.Parameters.AddWithValue("@District_ID", District_ID);
                        cmd.Parameters.AddWithValue("@OLD_PC_ID", OLD_PC_ID);
                        cmd.Parameters.AddWithValue("@NEW_PC_ID", NEW_PC_ID);
                        int renq = cmd.ExecuteNonQuery();
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


    [WebMethod]
    public bool InFarmerDeleteRequest(DataSet dsFarmerDeleteRequest)
    {
        bool result = false;
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            foreach (DataRow dr in dsFarmerDeleteRequest.Tables[0].Rows)
            {
                string District_Id = dr["District_Id"].ToString();
                string SocietyID = dr["SocietyID"].ToString();
                string Farmer_Id = dr["Farmer_Id"].ToString();

                commandt = connection.CreateCommand();
                commandt.Transaction = trans;
                commandt.CommandTimeout = 600000;
                commandt.Parameters.Clear();
                commandt.CommandType = CommandType.Text;
                commandt.CommandText = "select count(*) from FarmerDeleteRequest where District_Id='" + District_Id + "' and SocietyID='" + SocietyID + "' and  Farmer_Id='" + Farmer_Id + "' ";
                Int64 res = Convert.ToInt64(commandt.ExecuteScalar());
                commandt.Dispose();
                if (res <= 0)
                {
                    string Reason = dr["Reason"].ToString();
                    string DeletedDate = getRDate_MDY(dr["DeletedDate"].ToString());
                    string DeletedBy = dr["DeletedBy"].ToString();
                    string IsDeleteRequest = dr["IsDeleteRequest"].ToString();
                    cmd = connection.CreateCommand();
                    cmd.Transaction = trans;
                    cmd.CommandTimeout = 600000;
                    cmd.Parameters.Clear();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "in_FarmerDeleteRequest";
                    cmd.Parameters.AddWithValue("@District_Id", District_Id);
                    cmd.Parameters.AddWithValue("@SocietyID", SocietyID);
                    cmd.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                    cmd.Parameters.AddWithValue("@Reason", Reason);
                    cmd.Parameters.AddWithValue("@DeletedDate", DeletedDate);
                    cmd.Parameters.AddWithValue("@DeletedBy", DeletedBy);
                    cmd.Parameters.AddWithValue("@IsDeleteRequest", IsDeleteRequest);
                    int req = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    if (req > 0)
                    {
                        cmd = connection.CreateCommand();
                        cmd.Transaction = trans;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 600000;
                        cmd.CommandText = "in_delete_Farmer";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@District_Id", District_Id);
                        cmd.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                        cmd.Parameters.AddWithValue("@PC_ID", SocietyID);
                        int reqs = cmd.ExecuteNonQuery();
                        cmd.Dispose();

                        if (reqs > 0)
                        {
                            cmd = connection.CreateCommand();
                            cmd.Transaction = trans;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "in_Delete_FarmerLandRecord";
                            cmd.CommandTimeout = 60000;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@District_Id", District_Id);
                            cmd.Parameters.AddWithValue("@Procured_SocietyID", SocietyID);
                            cmd.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                            int reqd = cmd.ExecuteNonQuery();
                            cmd.Dispose();
                        }



                        cmd = connection.CreateCommand();
                        cmd.Transaction = trans;
                        cmd.CommandTimeout = 600000;
                        cmd.Parameters.Clear();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "del_Farmer";
                        cmd.Parameters.AddWithValue("@District_Id", District_Id);
                        cmd.Parameters.AddWithValue("@PC_ID", SocietyID);
                        cmd.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                        int reqfr = cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        if (reqfr > 0)
                        {
                            cmd = connection.CreateCommand();
                            cmd.Transaction = trans;
                            cmd.CommandTimeout = 600000;
                            cmd.Parameters.Clear();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "del_FarmerLandRecord";
                            cmd.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                            cmd.Parameters.AddWithValue("@Procured_SocietyID", SocietyID);
                            int reqFL = cmd.ExecuteNonQuery();
                            cmd.Dispose();
                        }
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
            // commandt.CommandTimeout = 0;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "select * from UserPassword";
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
            // commandt.CommandTimeout = 0;
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
            //commandt.CommandTimeout = 0;
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

    [WebMethod]
    public void InRunnerUploadDataStatus(string SocietyId, string RunnerVer, string UploadStatus)
    {
        if (SocietyId != "")
        {
            if (RunnerVer != "")
            {

                try
                {
                    OpenConnection();
                    trans = connection.BeginTransaction();




                    string date = getRDate_MDY(System.DateTime.Now.ToShortTimeString());
                    string LogID = SocietyId.ToString() + date;
                    string LogIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    string LogDate = date;

                    commandt = connection.CreateCommand();
                    commandt.Transaction = trans;
                    commandt.CommandText = "Select count(*) from RunnerLog where LogID    ='" + LogID + "'";
                    commandt.CommandType = CommandType.Text;
                    string res = Convert.ToString(commandt.ExecuteScalar());
                    if (Convert.ToInt16(res) > 1)
                    {

                        //cmd = new SqlCommand();
                        //cmd.Connection = connection;
                        //cmd.Transaction = trans;


                        commandt.CommandType = CommandType.StoredProcedure;
                        commandt.CommandText = "up_RunnerStatus";
                        commandt.Parameters.Clear();
                        commandt.Parameters.AddWithValue("@RunnerID", SocietyId);
                        commandt.Parameters.AddWithValue("@LogID", LogID);
                        commandt.Parameters.AddWithValue("@RunnerVer", RunnerVer);
                        if (UploadStatus == "")
                        {
                            UploadStatus = "0";
                        }
                        commandt.Parameters.AddWithValue("@Status", UploadStatus);
                        commandt.Parameters.AddWithValue("@DayCount", " ");
                        int req = commandt.ExecuteNonQuery();



                        //cmd.Parameters.Add("@RunnerID", SqlDbType.VarChar, 10);
                        //cmd.Parameters["@RunnerID"].Value = SocietyId.Trim().ToString();
                        //cmd.Parameters.Add("@LogID", SqlDbType.VarChar, 25);
                        //cmd.Parameters["@LogID"].Value = LogID;
                        //cmd.Parameters.Add("@RunnerVer", SqlDbType.VarChar, 5);
                        //cmd.Parameters["@RunnerVer"].Value = RunnerVer;
                        //cmd.Parameters.Add("@Status", SqlDbType.VarChar, 10);
                        //if (UploadStatus == "")
                        //{
                        //    UploadStatus = "0";
                        //}
                        //cmd.Parameters["@Status"].Value = UploadStatus;
                        //cmd.Parameters.Add("@DayCount", SqlDbType.VarChar, 5);
                        //cmd.Parameters["@DayCount"].Value = res.ToString();
                        //int req = cmd.ExecuteNonQuery();
                        if (req > 0)
                        {
                            trans.Commit();
                        }


                    }
                    else
                    {
                        //cmd = new SqlCommand();
                        //cmd.Connection = connection;
                        //cmd.Transaction = trans;

                        commandt.CommandType = CommandType.StoredProcedure;
                        commandt.CommandText = "in_RunnerStatus";
                        commandt.Parameters.Clear();
                        commandt.Parameters.AddWithValue("@RunnerID", SocietyId);

                        commandt.Parameters.AddWithValue("@LogID", LogID);
                        commandt.Parameters.AddWithValue("@RunnerVer", RunnerVer);
                        commandt.Parameters.AddWithValue("@RunnerVer", RunnerVer);
                        if (UploadStatus == "")
                        {
                            UploadStatus = "0";
                        }
                        commandt.Parameters.AddWithValue("@Status", UploadStatus);
                        commandt.Parameters.AddWithValue("@DayCount", "1");
                        int req = commandt.ExecuteNonQuery();

                        //cmd.Parameters.Add("@RunnerID", SqlDbType.VarChar, 10);
                        //cmd.Parameters["@RunnerID"].Value = SocietyId;
                        //cmd.Parameters.Add("@LogID", SqlDbType.VarChar, 25);
                        //cmd.Parameters["@LogID"].Value = LogID;
                        //cmd.Parameters.Add("@RunnerVer", SqlDbType.VarChar, 5);
                        //cmd.Parameters["@RunnerVer"].Value = RunnerVer;
                        //cmd.Parameters.Add("@Status", SqlDbType.VarChar, 10);
                        //if (UploadStatus == "")
                        //{
                        //    UploadStatus = "0";
                        //}
                        //cmd.Parameters["@Status"].Value = UploadStatus;
                        //cmd.Parameters.Add("@DayCount", SqlDbType.VarChar, 5);
                        //cmd.Parameters["@DayCount"].Value = "1";
                        //int req = cmd.ExecuteNonQuery();
                        if (req > 0)
                        {
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

            }
        }
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


public class chkWheatRunnerReg :
System.Web.Services.Protocols.SoapHeader
{
    string _svcName = "";
    string _svcPassword = "";
    bool result = false;

    public string svcName
    {
        set { _svcName = value; }
        get { return _svcName; }
    }

    public string svcPassword
    {
        set { _svcPassword = value; }
        get { return _svcPassword; }
    }

    public Boolean chkSVC()
    {
        if (svcName == "fr" && svcPassword == "2013")
        {
            result = true;
        }
        return result;
    }

}







