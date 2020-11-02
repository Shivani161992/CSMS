using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;





/// <summary>
/// Summary description for RunnerServicePaddy
/// </summary>
[WebService(Namespace = "http://microsoft.co.in/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class RunnerServicePaddy : System.Web.Services.WebService
{
    private static SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con_proc_online_PPMS"].ToString());
    private SqlCommand command, cmd;
    private SqlDataAdapter dataAdapter;
    private DataSet dataset;
    // private static SqlTransaction trans = null;



    public RunnerServicePaddy()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }


    public ChkUser MyHeader;
    [WebMethod(Description = "This method is used for checking username and password")]
    [SoapHeader("MyHeader")]
    public bool chkInformation(ChkUser userpwd)
    {
        bool rtev = false;
        if (userpwd != null)
        {
            //change on 16112012
            //if (userpwd.username == "paddyservice30" && userpwd.password == "paddy2012")
            if (userpwd.username == "16112012" && userpwd.password == "16112012")
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


    [WebMethod(Description = "This method is used for inserting runner log information")]
    public void InsertRunnerLog(string societyID, ChkUser userpwd)
    {
        try
        {
            if (connection != null && userpwd.username == "16112012" && userpwd.password == "16112012")
            {
                connection.Open();
                string date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                string LogID = societyID.ToString() + date;
                string LogIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                string LogDate = "";
                string Pc_Id = societyID.ToString();
                string District_Code = societyID.Substring(0, 4);
                string Society_Id = societyID.ToString();
                string RunnerID = societyID.ToString();
                string Status = "YES";
                string RunnerVer = "3.0";


                command = new SqlCommand();
                string qry = "Insert Into RunnerLog(LogID,LogIP,LogDate,Pc_Id,District_Code,Society_Id,RunnerID,Status,RunnerVer)";
                qry += " values('" + LogID + "','" + LogIP + "',getDate(),'" + Pc_Id + "','" + District_Code + "','" + Society_Id + "','" + RunnerID + "','" + Status + "','" + RunnerVer + "')";
                command.CommandText = qry;
                command.Connection = connection;
                int ret = command.ExecuteNonQuery();

            }
        }
        catch (Exception)
        {
            connection.Close();
        }
        finally
        {
            connection.Close();
        }

    }


    #region
    //[WebMethod(Description = "This method is used for runner registration")]
    //public bool ChkRunneLogin(string runnerid, string password)
    //{
    //    bool runnechk = false;

    //    try
    //    {

    //        if (connection != null )
    //        {
    //            connection.Open();
    //            command = new SqlCommand();
    //            string qry = "select count(*) from RunnerLogin where RunnerID='" + runnerid + "' and [Password]='" + password + "' ";
    //            command.CommandText = qry;
    //            command.Connection = connection;
    //            int ret = (int)command.ExecuteScalar();

    //            if (ret > 0)
    //            {
    //                runnechk = true;
    //                return runnechk;
    //            }
    //            else
    //            {
    //                runnechk = false;
    //                return runnechk;
    //            }

    //            //command.Dispose();
    //        }
    //        return runnechk;
    //    }
    //    catch (Exception)
    //    {
    //        connection.Close();
    //        return runnechk;
    //    }
    //    finally
    //    {
    //        connection.Close();

    //    }
    //}

    #endregion

    [WebMethod(Description = "This Method Is Used For Insert and Update Society Information")]
    public void InsertUpdateINITAIAL(DataSet dsInitial, bool chkuser)
    {
        try
        {
            if (connection != null && chkuser == true)
            {
                connection.Open();

                if (dsInitial != null)
                {
                    if (dsInitial.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow dr in dsInitial.Tables[0].Rows)
                        {
                            string Society_Id = dr["Society_Id"].ToString();
                            command = new SqlCommand("Select count(*) from Initial where Society_Id='" + Society_Id + "'", connection);
                            command.CommandType = CommandType.Text;
                            string res = command.ExecuteScalar().ToString();
                            if (Convert.ToInt16(res) <= 0)
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
                                string AccNO = dr["AccNO"].ToString();
                                string BranchName = dr["BranchName"].ToString();
                                string ManagerName = dr["ManagerName"].ToString();
                                string VersionNo = dr["VersionNo"].ToString();
                                Int32 NoOfToulKanta = CheckNullInt(dr["NoOfToulKanta"].ToString());
                                float DailySc_Capacity = CheckNullFloat(dr["DailySc_Capacity"].ToString());
                                string UpdationDate = dr["UpdationDate"].ToString();
                                float Societycreditlimit = CheckNullFloat(dr["Societycreditlimit"].ToString());
                                string MgrMobileNo = dr["MgrMobileNo"].ToString();
                                Int64 SocBandaranCapacity = CheckNullInt(dr["SocBandaranCapacity"].ToString());
                                string DateTimeOfInstall = getDate_MDY(dr["DateTimeOfInstall"].ToString());
                                string OpeningStockOfGunny = dr["OpeningStockOfGunny"].ToString();
                                string OneFarmerLimit = dr["OneFarmerLimit"].ToString();
                                string PC_Id = dr["PC_Id"].ToString();

                                string qryInsert = "Insert Into Initial(District_ID,District_Name,Society_Id,SocietyName,PC_NAME,AgencyId,AgencyName,MarketingSeasonId,MarketingSeason,CropYear,Password1,BankName,";
                                qryInsert += "AccNO,BranchName,ManagerName,VersionNo,NoOfToulKanta,DailySc_Capacity,Societycreditlimit,MgrMobileNo, SocBandaranCapacity,DateTimeOfInstall,OpeningStockOfGunny,OneFarmerLimit,PC_Id)";
                                qryInsert += " values('" + District_ID + "',N'" + District_Name + "','" + Society_Id + "',N'" + SocietyName + "',N'" + PC_NAME + "','" + AgencyId + "','" + AgencyName + "',N'" + MarketingSeasonId + "',N'" + MarketingSeason + "','" + CropYear + "','" + Password1 + "','" + BankName + "','" + AccNO + "',N'" + BranchName + "' ,";
                                qryInsert += " N'" + ManagerName + "','" + VersionNo + "'," + NoOfToulKanta + "," + DailySc_Capacity + "," + Societycreditlimit + ",'" + MgrMobileNo + "'," + SocBandaranCapacity + ",'" + DateTimeOfInstall + "','" + OpeningStockOfGunny + "','" + OneFarmerLimit + "','" + PC_Id + "' )";
                                cmd = new SqlCommand(qryInsert, connection);
                                int xins = cmd.ExecuteNonQuery();
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
                                string AccNO = dr["AccNO"].ToString();
                                string BranchName = dr["BranchName"].ToString();
                                string ManagerName = dr["ManagerName"].ToString();
                                string VersionNo = dr["VersionNo"].ToString();
                                Int32 NoOfToulKanta = CheckNullInt(dr["NoOfToulKanta"].ToString());
                                float DailySc_Capacity = CheckNullFloat(dr["DailySc_Capacity"].ToString());

                                //Change On 15092012 for stop farmerRegistration 
                                string UpdationDate = "";
                                if (dr["UpdationDate"].ToString() != null)
                                {
                                    UpdationDate = getDate_MDY(dr["UpdationDate"].ToString());
                                }
                                else
                                {
                                    UpdationDate = getDate_MDY("01/01/2012");

                                }
                                //CHANGE ON 15092012
                                float Societycreditlimit = CheckNullFloat(dr["Societycreditlimit"].ToString());
                                string MgrMobileNo = dr["MgrMobileNo"].ToString();
                                Int64 SocBandaranCapacity = CheckNullInt(dr["SocBandaranCapacity"].ToString());
                                string DateTimeOfInstall = getDate_MDY(dr["DateTimeOfInstall"].ToString());
                                string OpeningStockOfGunny = dr["OpeningStockOfGunny"].ToString();
                                string OneFarmerLimit = dr["OneFarmerLimit"].ToString();
                                string PC_Id = dr["PC_Id"].ToString();

                                string qryUpd = "Update Initial set Password1='" + Password1 + "',BankName=N'" + BankName + "', AccNO='" + AccNO + "',BranchName=N'" + BranchName + "',ManagerName=N'" + ManagerName + "',VersionNo='" + VersionNo + "',NoOfToulKanta=" + NoOfToulKanta + ",DailySc_Capacity=" + DailySc_Capacity + " ,MarketingSeasonId=N'" + MarketingSeasonId + "',MarketingSeason=N'" + MarketingSeason + "',Societycreditlimit='" + Societycreditlimit + "',MgrMobileNo='" + MgrMobileNo + "',SocBandaranCapacity=" + SocBandaranCapacity + ",UpdationDate='" + UpdationDate + "',DateTimeOfInstall='" + DateTimeOfInstall + "', OpeningStockOfGunny='" + OpeningStockOfGunny + "', OneFarmerLimit='" + OneFarmerLimit + "', PC_Id='" + PC_Id + "' where District_ID='" + District_ID + "' and Society_Id='" + Society_Id + "'";
                                cmd = new SqlCommand(qryUpd, connection);
                                int xupd = cmd.ExecuteNonQuery();
                            }
                        }
                    }

                }
            }

        }
        catch (Exception)
        {
            connection.Close();
        }
        finally
        {
            connection.Close();

        }

    }


    [WebMethod(Description = "This Method Is Used For Inserting  Farmer Registration Information")]
    public void InsertFarmerInfo(DataSet dsFarmerInfo, bool chkuser)
    {

        string farmerid = "";
        try
        {
            if (connection != null && chkuser == true)
            {
                connection.Open();
                if (dsFarmerInfo != null)
                {

                    foreach (DataRow dr in dsFarmerInfo.Tables[0].Rows)
                    {
                        farmerid = dr["Farmer_Id"].ToString();
                        string District_Id = dr["District_Id"].ToString();
                        
                        //change on 15092012------Insert Into Old FarmerRegistration Table On Server mm/dd/yyyy
                        string CreatedDate_16092012 = getDate_MDY(dr["CreatedDate"].ToString());
                        if (Convert.ToDateTime(CreatedDate_16092012) < Convert.ToDateTime("09/16/2012"))
                        {
                            command = new SqlCommand("Select count(*) from FarmerRegistration where Farmer_Id='" + farmerid + "' and  District_Id='" + District_Id + "'", connection);
                            string res = command.ExecuteScalar().ToString();
                            if (Convert.ToInt32(res) <= 0)
                            {
                                //  string District_Id = dr["District_Id"].ToString();
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
                                string CropExpected_Date = getDate_MDY(dr["CropExpected_Date"].ToString());
                                string UserID = dr["UserID"].ToString();
                                string CreatedDate = getDate_MDY(dr["CreatedDate"].ToString());
                                // ip = dr["ip"].ToString();
                                //string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                                //string ip = "test";
                                // string asr =  msgContext.getProperty(MessageContext.REMOTE_ADDR);
                                string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                                string RegistrationDate = getDate_MDY(dr["RegistrationDate"].ToString());
                                string IsApproved = dr["IsApproved"].ToString();
                                string Status = dr["Status"].ToString();

                                //command.Dispose();
                                cmd = new SqlCommand();
                                cmd.Connection = connection;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "Proc_InsertFarmerNew";
                                cmd.Parameters.AddWithValue("@District_Id", District_Id);
                                cmd.Parameters.AddWithValue("@Village_Id", Village_Id);
                                cmd.Parameters.AddWithValue("@VillageName", VillageName);

                                cmd.Parameters.AddWithValue("@Tehsil_Id", Tehsil_Id);
                                cmd.Parameters.AddWithValue("@Farmer_Id", farmerid);
                                cmd.Parameters.AddWithValue("@FarmerName", FarmerName);
                                cmd.Parameters.AddWithValue("@FatherHusName", FatherHusName);
                                cmd.Parameters.AddWithValue("@Gram_Panchayat", Gram_Panchayat);
                                cmd.Parameters.AddWithValue("@PatwariHalkaNo", PatwariHalkaNo);
                                cmd.Parameters.AddWithValue("@Mobileno", Mobileno);
                                cmd.Parameters.AddWithValue("@Category", Category);
                                cmd.Parameters.AddWithValue("@RinPustikaNo", RinPustikaNo);
                                cmd.Parameters.AddWithValue("@Farmer_EID_UID_No", Farmer_EID_UID_No);
                                cmd.Parameters.AddWithValue("@Farmer_BankName_New", Farmer_BankName_New);
                                cmd.Parameters.AddWithValue("@Farmer_BankBranchName", Farmer_BankBranchName);
                                cmd.Parameters.AddWithValue("@Farmer_BankAccountNo", Farmer_BankAccountNo);
                                cmd.Parameters.AddWithValue("@PC_ID", PC_ID);
                                cmd.Parameters.AddWithValue("@Procured_SocietyID", Procured_SocietyID);
                                cmd.Parameters.AddWithValue("@Procured_Dist_ID", Procured_Dist_ID);
                                cmd.Parameters.AddWithValue("@Procured_Place", Procured_Place);
                                cmd.Parameters.AddWithValue("@CropExpected_Date", CropExpected_Date);
                                cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                                cmd.Parameters.AddWithValue("@ip", ip);
                                cmd.Parameters.AddWithValue("@RegistrationDate", RegistrationDate);
                                cmd.Parameters.AddWithValue("@IsApproved", IsApproved);
                                cmd.Parameters.AddWithValue("@UserID", UserID);
                                cmd.Parameters.AddWithValue("@Status", Status);
                                int x = cmd.ExecuteNonQuery();
                            }
                            else
                            {

                            }
                        }
                        else
                        {

                            command = new SqlCommand("Select count(*) from FarmerRegistration_After15Sep where Farmer_Id='" + farmerid + "' and  District_Id='" + District_Id + "'", connection);
                            string res = command.ExecuteScalar().ToString();
                            if (Convert.ToInt32(res) <= 0)
                            {
                                //  string District_Id = dr["District_Id"].ToString();
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
                                string CropExpected_Date = getDate_MDY(dr["CropExpected_Date"].ToString());
                                string UserID = dr["UserID"].ToString();
                                string CreatedDate = getDate_MDY(dr["CreatedDate"].ToString());
                                // ip = dr["ip"].ToString();
                                //string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                                //string ip = "test";
                                // string asr =  msgContext.getProperty(MessageContext.REMOTE_ADDR);
                                string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                                string RegistrationDate = getDate_MDY(dr["RegistrationDate"].ToString());
                                string IsApproved = dr["IsApproved"].ToString();
                                string Status = dr["Status"].ToString();

                                //command.Dispose();
                                cmd = new SqlCommand();
                                cmd.Connection = connection;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "Proc_InsertFarmerNew_After15_Sep";
                                cmd.Parameters.AddWithValue("@District_Id", District_Id);
                                cmd.Parameters.AddWithValue("@Village_Id", Village_Id);
                                cmd.Parameters.AddWithValue("@VillageName", VillageName);

                                cmd.Parameters.AddWithValue("@Tehsil_Id", Tehsil_Id);
                                cmd.Parameters.AddWithValue("@Farmer_Id", farmerid);
                                cmd.Parameters.AddWithValue("@FarmerName", FarmerName);
                                cmd.Parameters.AddWithValue("@FatherHusName", FatherHusName);
                                cmd.Parameters.AddWithValue("@Gram_Panchayat", Gram_Panchayat);
                                cmd.Parameters.AddWithValue("@PatwariHalkaNo", PatwariHalkaNo);
                                cmd.Parameters.AddWithValue("@Mobileno", Mobileno);
                                cmd.Parameters.AddWithValue("@Category", Category);
                                cmd.Parameters.AddWithValue("@RinPustikaNo", RinPustikaNo);
                                cmd.Parameters.AddWithValue("@Farmer_EID_UID_No", Farmer_EID_UID_No);
                                cmd.Parameters.AddWithValue("@Farmer_BankName_New", Farmer_BankName_New);
                                cmd.Parameters.AddWithValue("@Farmer_BankBranchName", Farmer_BankBranchName);
                                cmd.Parameters.AddWithValue("@Farmer_BankAccountNo", Farmer_BankAccountNo);
                                cmd.Parameters.AddWithValue("@PC_ID", PC_ID);
                                cmd.Parameters.AddWithValue("@Procured_SocietyID", Procured_SocietyID);
                                cmd.Parameters.AddWithValue("@Procured_Dist_ID", Procured_Dist_ID);
                                cmd.Parameters.AddWithValue("@Procured_Place", Procured_Place);
                                cmd.Parameters.AddWithValue("@CropExpected_Date", CropExpected_Date);
                                cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                                cmd.Parameters.AddWithValue("@ip", ip);
                                cmd.Parameters.AddWithValue("@RegistrationDate", RegistrationDate);
                                cmd.Parameters.AddWithValue("@IsApproved", IsApproved);
                                cmd.Parameters.AddWithValue("@UserID", UserID);
                                cmd.Parameters.AddWithValue("@Status", Status);
                                int x = cmd.ExecuteNonQuery();


                            }


                        }
                    }
                }

            }
        }

        catch (Exception)
        {
            connection.Close();
        }
        finally
        {
            connection.Close();
        }
    }


    #region fOR fARMER uPDATION CODE


    //[WebMethod(Description = "This Method Is Used For Updating  Farmer Registration Information")]

    //public void UpdateFarmerInfo(DataSet ds )
    //{
    //    try
    //    {
    //        if (connection != null)
    //        {
    //            connection.Open();

    //            foreach (DataRow dr in ds.Tables[0].Rows)
    //            {
    //                string FarmerId = dr["FarmerId"].ToString();
    //                string Tehsil_Id = dr["Tehsil_Id"].ToString();
    //                string District_Id = dr["District_Id"].ToString();
    //                string Procured_SocietyID = dr["Procured_SocietyID"].ToString();







    //            }
    //        }

    //    }
    //    catch (Exception)
    //    {

    //        connection.Close();
    //    }

    //    finally {

    //        connection.Close();

    //        command.Dispose();
    //    }
    //}
    #endregion


    [WebMethod(Description = "This Method Is Used For Inserting Farmer Land Record Information")]
    public void InsertFarmerLandInfo(DataSet dsFarmerLandInfo, bool chkuser)
    {
        try
        {

            if (connection != null && chkuser == true)
            {
                connection.Open();


                if (dsFarmerLandInfo != null)
                {
                    if (dsFarmerLandInfo.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drs in dsFarmerLandInfo.Tables[0].Rows)
                        {

                            //change ON 15092012
                            command = new SqlCommand();
                            command.CommandText = "select count(Farmer_Id) from FarmerRegistration_After15Sep where Farmer_Id='" + drs["Farmer_Id"].ToString() + "' ";
                            command.Connection = connection;

                            int retv = Convert.ToInt16(command.ExecuteScalar());

                            if (Convert.ToInt16(retv) <= 0)
                            {

                                command = new SqlCommand();
                                //                            command.CommandText = "select count(*) from Farmer_LandRecordDescription where TransID='" + drs["TransID"].ToString() + "' and Farmer_Id='" + drs["Farmer_Id"].ToString() + "' and Village_ID='" + drs["Village_ID"].ToString() + "' ";  changed on 2/9/2012
                                command.CommandText = "select count(*) from Farmer_LandRecordDescription where TransID='" + drs["TransID"].ToString() + "' and Farmer_Id='" + drs["Farmer_Id"].ToString() + "' ";
                                command.Connection = connection;
                                int ret = Convert.ToInt16(command.ExecuteScalar());
                                if (ret <= 0)
                                {
                                    string TransID = drs["TransID"].ToString();
                                    string Farmer_Id = drs["Farmer_Id"].ToString();
                                    string Village_ID = drs["Village_ID"].ToString();
                                    string VillageName = drs["VillageName"].ToString();
                                    string Crop_ID = drs["Crop_ID"].ToString();
                                    string LandOwner_Name = drs["LandOwner_Name"].ToString();
                                    string LandOwner_RinPustikaNo = drs["LandOwner_RinPustikaNo"].ToString();
                                    string LandType = drs["LandType"].ToString();
                                    string KhasaraNo = drs["KhasaraNo"].ToString();
                                    string Rakba = Convert.ToString(CheckNullFloat(drs["Rakba"].ToString()));
                                    string Rakba_crop_sinchit = Convert.ToString(CheckNullFloat(drs["Rakba_crop_sinchit"].ToString()));
                                    string Rakba_crop_asinchit = Convert.ToString(CheckNullFloat(drs["Rakba_crop_asinchit"].ToString()));
                                    string Rakba_crop_sinchit_qty = Convert.ToString(CheckNullFloat(drs["Rakba_crop_sinchit_qty"].ToString()));
                                    string Rakba_crop_asinchit_qty = Convert.ToString(CheckNullFloat(drs["Rakba_crop_asinchit_qty"].ToString()));
                                    string Procured_qty = Convert.ToString(CheckNullFloat(drs["Procured_qty"].ToString()));
                                    string crpcode = drs["crpcode"].ToString();

                                    cmd = new SqlCommand();
                                    cmd.Connection = connection;
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "Proc_InsertLandRecord";
                                    cmd.Parameters.AddWithValue("@TransID", TransID);
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
                                    int rl = cmd.ExecuteNonQuery();
                                }
                                else
                                {

                                }
                            }




                            else
                            {

                                command = new SqlCommand();
                                //                            command.CommandText = "select count(*) from Farmer_LandRecordDescription where TransID='" + drs["TransID"].ToString() + "' and Farmer_Id='" + drs["Farmer_Id"].ToString() + "' and Village_ID='" + drs["Village_ID"].ToString() + "' ";  changed on 2/9/2012
                                command.CommandText = "select count(*) from Farmer_LandRecordDescription_After15Sep where TransID='" + drs["TransID"].ToString() + "' and Farmer_Id='" + drs["Farmer_Id"].ToString() + "' ";
                                command.Connection = connection;
                                int ret = Convert.ToInt16(command.ExecuteScalar());
                                if (ret <= 0)
                                {
                                    string TransID = drs["TransID"].ToString();
                                    string Farmer_Id = drs["Farmer_Id"].ToString();
                                    string Village_ID = drs["Village_ID"].ToString();
                                    string VillageName = drs["VillageName"].ToString();
                                    string Crop_ID = drs["Crop_ID"].ToString();
                                    string LandOwner_Name = drs["LandOwner_Name"].ToString();
                                    string LandOwner_RinPustikaNo = drs["LandOwner_RinPustikaNo"].ToString();
                                    string LandType = drs["LandType"].ToString();
                                    string KhasaraNo = drs["KhasaraNo"].ToString();
                                    string Rakba = Convert.ToString(CheckNullFloat(drs["Rakba"].ToString()));
                                    string Rakba_crop_sinchit = Convert.ToString(CheckNullFloat(drs["Rakba_crop_sinchit"].ToString()));
                                    string Rakba_crop_asinchit = Convert.ToString(CheckNullFloat(drs["Rakba_crop_asinchit"].ToString()));
                                    string Rakba_crop_sinchit_qty = Convert.ToString(CheckNullFloat(drs["Rakba_crop_sinchit_qty"].ToString()));
                                    string Rakba_crop_asinchit_qty = Convert.ToString(CheckNullFloat(drs["Rakba_crop_asinchit_qty"].ToString()));
                                    string Procured_qty = Convert.ToString(CheckNullFloat(drs["Procured_qty"].ToString()));
                                    string crpcode = drs["crpcode"].ToString();

                                    cmd = new SqlCommand();
                                    cmd.Connection = connection;
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "Proc_InsertLandRecord_After15_Sep";
                                    cmd.Parameters.AddWithValue("@TransID", TransID);
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
                                    int rl = cmd.ExecuteNonQuery();
                                }

                            }

                        }

                    }

                }

            }
        }
        catch (Exception)
        {
            connection.Close();
        }
        finally
        {
            connection.Close();
        }

    }


    [WebMethod(Description = "This Method Is Used For Selecting New Villages From Online")]
    public DataSet SelectNewVillage(string DistrictId, ChkUser chk)
    {
        try
        {
            dataset = new DataSet();
            if (connection != null && chk.username == "PPMS_02112012" && chk.password == "16112012")
            {

                connection.Open();
                command = new SqlCommand();
                //string qry = "select * from VillageMaster where District_Id='" + DistrictId + "' and Tehsil_ID='" + TehsilId + "' ";
                string qry = "select * from VillageMaster where District_Id='" + DistrictId + "' and VILLAGE_STATUS='N' ";
                command.CommandText = qry;
                command.Connection = connection;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dataset);
                command.Dispose();

            }

        }
        catch (Exception)
        {
            connection.Close();
        }
        finally
        {
            connection.Close();
        }
        return dataset;
    }

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

}

public class ChkUser
: System.Web.Services.Protocols.SoapHeader
{
    public string username;
    public string password;
}


