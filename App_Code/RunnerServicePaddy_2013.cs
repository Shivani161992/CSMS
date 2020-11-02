using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;



/// <summary>
/// Summary description for RunnerServicePaddy_2013
/// </summary>
[WebService(Namespace = "http://microsoft.co.in/", Name = "FarmerTransferringServicePaddy2013", Description = "Transfer farmer data during procurement center initialization, Hosted Date :: 31/08/2013 ")]

[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class RunnerServicePaddy_2013 : System.Web.Services.WebService
{
    private SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_PPMS2014"].ToString());
    private SqlCommand command, cmd;
    private SqlDataAdapter dataAdapter;
    private DataSet dataset;
    private SqlTransaction trans = null;
    private SqlCommand commandt = null;
    private int count = 0;
    public string LogID = "";

    public RunnerServicePaddy_2013()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    #region Security

    public SecurityFarmerPaddy2013 SecurityFarmerPaddy2013;
    [SoapHeader("SecurityFarmerPaddy2013")]

    [WebMethod(Description = "This Method Is Used For Security Check in")]
    public bool chkSecurityFarmer(SecurityFarmerPaddy2013 S)
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

        return rtev;
    }

    [WebMethod(Description = "This Method Is Used For Inserting Check Runner Login ID")]
    public bool chkRunnerLogin(SecurityFarmerPaddy2013 S)
    {
        bool rtev = false;
        if (S != null)
        {
            OpenConnection();
            SqlCommand chkcmd = new SqlCommand();
            DataSet cds = new DataSet();
            chkcmd.Connection = connection;
            chkcmd.CommandType = CommandType.StoredProcedure;
            chkcmd.CommandText = "proc_chkRunnerLogin";
            chkcmd.Parameters.Clear();
            chkcmd.Parameters.AddWithValue("@RunnerId", S.RunnerId);
            chkcmd.Parameters.AddWithValue("@RunnerPassword", S.RunnerPassword);
            string ret = Convert.ToString(chkcmd.ExecuteScalar());
            if (Convert.ToInt16(ret) > 0)
            {
                rtev = true;

            }
            else
            {
                rtev = false;
            }
            CloseConnection();
            return rtev;


        }
        else
        {
            return rtev;
        }


    }

    #endregion

    #region Insertion in online From Offline database of farmer Registration

    #region In Farmer Basic Information and Landrecord Description Tables

    [WebMethod(Description = "This Method Is Used For Inserting  Farmer Registration Information")]
    public void InsertFarmerInfo(DataSet dsFarmerInfo)
    {
        string LogID = "";
        string farmerid = "";
        try
        {
            if (connection != null)
            {
                connection.Open();
                if (dsFarmerInfo != null)
                {

                    foreach (DataRow dr in dsFarmerInfo.Tables[0].Rows)
                    {
                        farmerid = dr["Farmer_Id"].ToString();
                        string District_Id = dr["District_Id"].ToString();
                        string societyid = dr["Procured_SocietyID"].ToString();

                        command = new SqlCommand("Select count(*) from FarmerRegistration_New where NewFarmer_Id='" + farmerid + "' and  District_Id='" + District_Id + "'", connection);
                        string res = command.ExecuteScalar().ToString();
                        if (Convert.ToInt32(res) <= 0)
                        {

                            string Village_Id = dr["Village_Id"].ToString();
                            string VillageName = dr["VillageName"].ToString();
                            string Tehsil_Id = dr["Tehsil_Id"].ToString();

                            string FarmerName = dr["FarmerName"].ToString();
                            string FatherHusName = dr["FatherHusName"].ToString();
                            string Gram_Panchayat = dr["Gram_Panchayat"].ToString();
                            string PatwariHalkaNo = dr["PatwariHalkaNo"].ToString();
                            if (PatwariHalkaNo.Length > 10)
                            {
                                PatwariHalkaNo = PatwariHalkaNo.Substring(0, 10);
                            }
                            else
                            {

                            }

                            string Mobileno = dr["Mobileno"].ToString();
                            string Category = dr["Category"].ToString();
                            string RinPustikaNo = dr["RinPustikaNo"].ToString();
                            string Farmer_EID_UID_No = dr["Farmer_EID_UID_No"].ToString();

                            string Farmer_KCCNo = dr["Farmer_KCCNo"].ToString();
                            string Farmer_BankName_New = dr["Farmer_BankName_New"].ToString();
                            string Farmer_BankBranchName = dr["Farmer_BankBranchName"].ToString();
                            string Farmer_BankAccountNo = dr["Farmer_BankAccountNo"].ToString();
                            string Procured_SocietyID = dr["Procured_SocietyID"].ToString();


                            string Procured_Place = dr["Procured_Place"].ToString();
                            string CropExpected_Date = getDate_MDY(mmddyyyy(dr["CropExpected_Date"].ToString()));
                            string UserID = dr["UserID"].ToString();

                            string CreatedDate = getDate_MDY(mmddyyyy(dr["CreatedDate"].ToString()));
                            string updatedDate = getDate_MDY(mmddyyyy(dr["updatedDate"].ToString()));
                            string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                            string RegistrationDate = getDate_MDY(mmddyyyy(dr["RegistrationDate"].ToString()));
                            string IsApproved = dr["IsApproved"].ToString();
                            string Status = dr["Status"].ToString();
                            string MarketingSeasonId = "K";
                            string CropYear = "2013-14";


                            //command.Dispose();

                            cmd = new SqlCommand();
                            cmd.Connection = connection;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "InFarmerRegistration_R_New";
                            cmd.Parameters.AddWithValue("@Farmer_Id", farmerid);
                            cmd.Parameters.AddWithValue("@District_Id", District_Id);
                            cmd.Parameters.AddWithValue("@Village_Id", Village_Id);
                            cmd.Parameters.AddWithValue("@VillageName", VillageName);
                            cmd.Parameters.AddWithValue("@Tehsil_Id", Tehsil_Id);
                            cmd.Parameters.AddWithValue("@NewFarmer_Id", farmerid);


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

                            cmd.Parameters.AddWithValue("@Procured_SocietyID", Procured_SocietyID);
                            cmd.Parameters.AddWithValue("@Procured_Place", Procured_Place);
                            cmd.Parameters.AddWithValue("@CropExpected_Date", CropExpected_Date);
                            cmd.Parameters.AddWithValue("@UserID", UserID);
                            cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                            cmd.Parameters.AddWithValue("@updatedDate", updatedDate);
                            cmd.Parameters.AddWithValue("@RegistrationDate", RegistrationDate);

                            cmd.Parameters.AddWithValue("@ip", ip);

                            cmd.Parameters.AddWithValue("@IsApproved", IsApproved);

                            cmd.Parameters.AddWithValue("@Status", Status);
                            cmd.Parameters.AddWithValue("@MarketingSeasonId", MarketingSeasonId);
                            cmd.Parameters.AddWithValue("@CropYear", CropYear);
                            try
                            {
                                 int x = cmd.ExecuteNonQuery();
                            }
                            catch { }
                           
                        }
                        //Keshav Lidoriya 27/08/2013

                        string date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                        LogID = societyid + date;
                        ///////////////////////////
                    }
                    count = 45;

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
            UploadDataStatus(count, LogID);
        }
    }

    [WebMethod(Description = "This Method Is Used For Inserting Farmer Land Record Information")]
    public void InsertFarmerLandInfo(DataSet dsFarmerLandInfo)
    {
        string LogID = "";
        try
        {
            if (connection != null)
            {
                connection.Open();

                if (dsFarmerLandInfo != null)
                {
                    if (dsFarmerLandInfo.Tables[0].Rows.Count > 0)
                    {
                       
                        foreach (DataRow drs in dsFarmerLandInfo.Tables[0].Rows)
                        {
                            string socityID = drs["TransID"].ToString().Substring(0, 7);
                            string TransID = drs["TransID"].ToString();
                            string Farmer_Id = drs["Farmer_Id"].ToString();
                            command = new SqlCommand();
                            command.CommandText = "select count(*) from Farmer_LandRecordDescription_New where TransID='" + TransID + "' and NewFarmer_Id='" + Farmer_Id + "'";
                            command.Connection = connection;
                            int ret = Convert.ToInt16(command.ExecuteScalar());
                            if (ret <= 0)
                            {
                               
                                string Tehsil_ID = drs["Tehsil_ID"].ToString();
                                string Village_ID = drs["Village_ID"].ToString();
                                string VillageName = drs["VillageName"].ToString();
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
                                cmd.CommandText = "InFarmerLandRecordDescription_R_New";
                                
                                cmd.Parameters.AddWithValue("@TransID", TransID);
                                cmd.Parameters.AddWithValue("@NewFarmer_Id", Farmer_Id);
                                cmd.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                                
                                cmd.Parameters.AddWithValue("@Tehsil_ID", Tehsil_ID);
                                cmd.Parameters.AddWithValue("@Village_ID", Village_ID);
                                cmd.Parameters.AddWithValue("@VillageName", VillageName);
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
                                try
                                {
                                    int rl = cmd.ExecuteNonQuery();
                                }
                                catch { }
                               
                                       
                            }
                            string date = getDate_MDY(System.DateTime.Now.ToShortTimeString());
                            LogID = socityID + date;
                        }

                        count = 75;
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
            UploadDataStatus(count, LogID);
            count = 0;
        }

    }

    #endregion

    #region In Farmer Registration,Updation and Deletion Request Tables

    [WebMethod(Description = "This Method Is Used For Inserting New Farmer Registration Request Information ")]
    public bool InNewFarmerRequest(DataSet dsNewFarmerRequest, string D, string S)
    {
        bool result = false;
        try
        {
            if (dsNewFarmerRequest != null)
            {
                if (dsNewFarmerRequest.Tables[0].Rows.Count > 0)
                {
                    OpenConnection();
                    trans = connection.BeginTransaction();
                    foreach (DataRow dr in dsNewFarmerRequest.Tables[0].Rows)
                    {
                        string Farmer_Id = dr["Farmer_Id"].ToString();
                        string District_Id = D;
                        string Society_Id = S;
                        commandt = connection.CreateCommand();
                        commandt.Transaction = trans;
                        commandt.CommandType = CommandType.Text;
                        commandt.CommandText = "select count(*) from  NewFarmerRegistrationRequest where Farmer_Id='" + Farmer_Id + "' and District_Id='" + D + "' and Society_Id='" + S + "'";
                        int re = Convert.ToInt32(commandt.ExecuteScalar());
                        commandt.Dispose();
                        if (re <= 0)
                        {
                            commandt = connection.CreateCommand();
                            commandt.Transaction = trans;
                            commandt.CommandType = CommandType.Text;
                            commandt.CommandText = "Insert Into NewFarmerRegistrationRequest(Farmer_Id,District_Id,Society_Id,CreatedDate) values('" + Farmer_Id + "','" + D + "','" + S + "',getDate())";
                            int renq = commandt.ExecuteNonQuery();
                            commandt.Dispose();
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

    [WebMethod(Description = "This Method Is Used For Inserting  Farmer Registration Updation Request Information")]
    public bool InFarmerUpdationRequest(DataSet dsFarmer, string SocietyId, string DistrictId)
    {
        bool result = false;
        try
        {
            if (dsFarmer != null)
            {
                if (dsFarmer.Tables[0].Rows.Count > 0)
                {

                    OpenConnection();
                    trans = connection.BeginTransaction();
                    foreach (DataRow dr in dsFarmer.Tables[0].Rows)
                    {
                        try
                        {
                            string FarmerId = dr["FarmerId"].ToString();
                            string UpdationDate_Off = getDate_MDY(mmddyyyy(dr["UpdationDate"].ToString()));

                            commandt = connection.CreateCommand();
                            commandt.Transaction = trans;
                            commandt.CommandType = CommandType.Text;
                            commandt.CommandText = "select count(*) from FarmerUpdationRequest_New where DisrictID='" + DistrictId + "' and SocietyID='" + SocietyId + "' and  FarmerId='" + FarmerId + "' and UpdationDate_Off ='" + UpdationDate_Off + "'";
                            Int64 res = Convert.ToInt64(commandt.ExecuteScalar());
                            commandt.Dispose();
                            if (res <= 0)
                            {
                                string UpdationDate_Onn = getDate_MDY(mmddyyyy(System.DateTime.Now.ToShortDateString()));
                                string IsFRUpdated = dr["IsFRUpdated"].ToString();
                                string IsLRUpdated = dr["IsLRUpdated"].ToString();
                                cmd = connection.CreateCommand();
                                cmd.Transaction = trans;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Clear();
                                cmd.CommandText = "In_FarmerUpdationRequest_New";
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
                        catch
                        {
                            ///////////
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

    [WebMethod(Description = "This Method Is Used For Delete Farmer Registration Request Information")]
    public bool InFarmerDeleteRequest(DataSet dsFarmerDeleteRequest)
    {
        bool result = false;
        try
        {
            if (dsFarmerDeleteRequest != null)
            {
                if (dsFarmerDeleteRequest.Tables[0].Rows.Count > 0)
                {
                    OpenConnection();
                    trans = connection.BeginTransaction();
                    foreach (DataRow dr in dsFarmerDeleteRequest.Tables[0].Rows)
                    {
                        try
                        {
                            string District_Id = dr["District_Id"].ToString();
                            string Procured_SocietyID = dr["SocietyID"].ToString();
                            string Farmer_Id = dr["Farmer_Id"].ToString();

                            commandt = connection.CreateCommand();
                            commandt.Transaction = trans;
                            commandt.Parameters.Clear();
                            commandt.CommandType = CommandType.Text;
                            commandt.CommandText = "select count(*) from FarmerDeleteRequest where District_Id='" + District_Id + "' and SocietyID='" + Procured_SocietyID + "' and  Farmer_Id='" + Farmer_Id + "' ";
                            Int64 res = Convert.ToInt64(commandt.ExecuteScalar());
                            commandt.Dispose();
                            if (res <= 0)
                            {
                                string Reason = dr["Reason"].ToString();
                                string DeletedDate = getDate_MDY(mmddyyyy(dr["DeletedDate"].ToString()));
                                string DeletedBy = dr["DeletedBy"].ToString();
                                string IsDeleteRequest = dr["IsDeleteRequest"].ToString();
                                cmd = connection.CreateCommand();
                                cmd.Transaction = trans;
                                cmd.Parameters.Clear();
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "In_FarmerDeleteRequest_New";
                                cmd.Parameters.AddWithValue("@District_Id", District_Id);
                                cmd.Parameters.AddWithValue("@SocietyID", Procured_SocietyID);
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
                                    cmd.CommandText = "In_delete_Farmer_New";
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@District_Id", District_Id);
                                    cmd.Parameters.AddWithValue("@NewFarmer_Id", Farmer_Id);
                                    cmd.Parameters.AddWithValue("@Procured_SocietyID", Procured_SocietyID);
                                    int reqs = cmd.ExecuteNonQuery();
                                    cmd.Dispose();

                                    if (reqs > 0)
                                    {
                                        string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                                        cmd = connection.CreateCommand();
                                        cmd.Transaction = trans;
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.CommandText = "In_Delete_FarmerLandRecordDesc_New_Log";
                                        cmd.Parameters.Clear();
                                        cmd.Parameters.AddWithValue("@NewFarmer_Id", Farmer_Id);
                                        cmd.Parameters.AddWithValue("@ip", ip);
                                        int reqd = cmd.ExecuteNonQuery();
                                        cmd.Dispose();
                                    }



                                    cmd = connection.CreateCommand();
                                    cmd.Transaction = trans;
                                    cmd.Parameters.Clear();
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "Del_Farmer_New";
                                    cmd.Parameters.AddWithValue("@District_Id", District_Id);
                                    cmd.Parameters.AddWithValue("@Procured_SocietyID", Procured_SocietyID);
                                    cmd.Parameters.AddWithValue("@NewFarmer_Id", Farmer_Id);
                                    int reqfr = cmd.ExecuteNonQuery();
                                    cmd.Dispose();
                                    if (reqfr > 0)
                                    {
                                        cmd = connection.CreateCommand();
                                        cmd.Transaction = trans;
                                        cmd.Parameters.Clear();
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.CommandText = "Delete_FarmerLandRecord_New";
                                        cmd.Parameters.AddWithValue("@NewFarmer_Id", Farmer_Id);
                                        //cmd.Parameters.AddWithValue("@Procured_SocietyID", Procured_SocietyID);
                                        int reqFL = cmd.ExecuteNonQuery();
                                        cmd.Dispose();
                                    }
                                }
                            }
                        }
                        catch { }

                    }
                    trans.Commit();
                    result = true;
                }
            }
        }
        catch (Exception ex)
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

    #region Update Farmer Basic Information and Landrecord Description

    [WebMethod]
    public bool UpFarmer(DataSet dsUpFarmer)
    {
        bool result = false;
        try
        {

            if (dsUpFarmer != null)
            {
                if (dsUpFarmer.Tables[0].Rows.Count > 0)
                {
                    OpenConnection();
                    trans = connection.BeginTransaction();

                    foreach (DataRow dr in dsUpFarmer.Tables[0].Rows)
                    {
                        try
                        {
                            string NewFarmer_Id = dr["Farmer_Id"].ToString();
                            string DisrictID = dr["District_Id"].ToString();
                            string SocietyID = dr["Procured_SocietyID"].ToString();
                            string Village_Id = dr["Village_Id"].ToString();
                            string Villagename = dr["VillageName"].ToString();
                            string Tehsil_Id = dr["Tehsil_Id"].ToString();

                            string Gram_Panchayat = dr["Gram_Panchayat"].ToString();
                            string PatwariHalkaNo = dr["PatwariHalkaNo"].ToString();
                            if (PatwariHalkaNo.Length > 10)
                            {
                                PatwariHalkaNo = PatwariHalkaNo.Substring(0, 10);
                            }
                            else
                            {

                            }
                            string Mobileno = dr["Mobileno"].ToString();
                            string Category = dr["Category"].ToString();
                            string RinPustikaNo = dr["RinPustikaNo"].ToString();
                            string Farmer_EID_UID_No = dr["Farmer_EID_UID_No"].ToString();
                            string Farmer_KCCNo = dr["Farmer_KCCNo"].ToString();
                            string Farmer_BankName_New = dr["Farmer_BankName_New"].ToString();
                            string Farmer_BankBranchName = dr["Farmer_BankBranchName"].ToString();
                            string Farmer_BankAccountNo = dr["Farmer_BankAccountNo"].ToString();
                            string Procured_SocietyID = dr["Procured_SocietyID"].ToString();
                            string Procured_Place = dr["Procured_Place"].ToString();
                            string CropExpected_Date = getDate_MDY(mmddyyyy(dr["CropExpected_Date"].ToString()));
                            string UserID = dr["UserID"].ToString();
                            string updatedDate = getDate_MDY(mmddyyyy(dr["updatedDate"].ToString()));
                            string ip = dr["ip"].ToString();
                            string IsApproved = dr["IsApproved"].ToString();
                            string Status = dr["Status"].ToString();

                            cmd = connection.CreateCommand();
                            cmd.Transaction = trans;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "select COUNT(*) from FarmerRegistration_New_Upd_Log where NewFarmer_Id='" + NewFarmer_Id + "' and District_Id = '" + DisrictID + "' and Procured_SocietyID ='" + SocietyID + "'";
                            //cmd.CommandText = "select COUNT(*) from FarmerRegistration_New where NewFarmer_Id='" + NewFarmer_Id + "' and District_Id = '" + DisrictID + "' and Procured_SocietyID ='" + SocietyID + "' and Village_Id = '" + Village_Id + "' and Villagename = N'" + Villagename + "' and Tehsil_Id = '" + Tehsil_Id + "' and Gram_Panchayat= N'" + Gram_Panchayat + "' and PatwariHalkaNo = N'" + PatwariHalkaNo + "' and Mobileno = '" + Mobileno + "' and Category = '" + Category + "' and RinPustikaNo = N'" + RinPustikaNo + "' and Farmer_EID_UID_No = N'" + Farmer_EID_UID_No + "' and Farmer_KCCNo = N'" + Farmer_KCCNo + "'and Farmer_BankName_New = N'" + Farmer_BankName_New + "'and Farmer_BankAccountNo = '" + Farmer_BankAccountNo + "'and Farmer_EID_UID_No = '" + Farmer_EID_UID_No + "' ";  
                            cmd.Parameters.Clear();
                            int count_que = Convert.ToInt32(cmd.ExecuteScalar());
                            if (count_que <= 0)
                            {
                                cmd = connection.CreateCommand();
                                cmd.Transaction = trans;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "In_Update_Farmer_Info_New";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@District_Id", DisrictID);
                                cmd.Parameters.AddWithValue("@NewFarmer_Id", NewFarmer_Id);
                                cmd.Parameters.AddWithValue("@Procured_SocietyID", SocietyID);
                                int req = cmd.ExecuteNonQuery();
                                cmd.Dispose();

                                //update
                                if (req > 0)
                                {

                                    cmd = connection.CreateCommand();
                                    cmd.Transaction = trans;
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "Update_Farmer_R_New";
                                    cmd.Parameters.Clear();

                                    cmd.Parameters.AddWithValue("@District_Id", DisrictID);
                                    cmd.Parameters.AddWithValue("@Village_Id", Village_Id);
                                    cmd.Parameters.AddWithValue("@Tehsil_Id", Tehsil_Id);
                                    cmd.Parameters.AddWithValue("@NewFarmer_Id", NewFarmer_Id);
                                    //cmd.Parameters.AddWithValue("@Farmer_Id", NewFarmer_Id);
                                    cmd.Parameters.AddWithValue("@Gram_Panchayat", Gram_Panchayat);
                                    cmd.Parameters.AddWithValue("@PatwariHalkaNo", PatwariHalkaNo);
                                    cmd.Parameters.AddWithValue("@Mobileno", Mobileno);
                                    cmd.Parameters.AddWithValue("@RinPustikaNo", RinPustikaNo);
                                    cmd.Parameters.AddWithValue("@Farmer_EID_UID_No", Farmer_EID_UID_No);
                                    cmd.Parameters.AddWithValue("@Farmer_KCCNo", Farmer_KCCNo);
                                    cmd.Parameters.AddWithValue("@Farmer_BankName_New", Farmer_BankName_New);
                                    cmd.Parameters.AddWithValue("@Farmer_BankBranchName", Farmer_BankBranchName);
                                    cmd.Parameters.AddWithValue("@Farmer_BankAccountNo", Farmer_BankAccountNo);
                                    cmd.Parameters.AddWithValue("@Procured_SocietyID", Procured_SocietyID);
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
                            }
                            else
                            {
                                cmd = new SqlCommand();
                                cmd = connection.CreateCommand();
                                cmd.Transaction = trans;
                                cmd.CommandType = CommandType.Text;
                                cmd.CommandText = "select COUNT(*) from FarmerRegistration_New_Upd_Log where NewFarmer_Id='" + NewFarmer_Id + "' and District_Id = '" + DisrictID + "' and Procured_SocietyID ='" + SocietyID + "' and Village_Id = '" + Village_Id + "' and Villagename = N'" + Villagename + "' and Tehsil_Id = '" + Tehsil_Id + "' and Gram_Panchayat= N'" + Gram_Panchayat + "' and PatwariHalkaNo = N'" + PatwariHalkaNo + "' and Mobileno = '" + Mobileno + "' and Category = '" + Category + "' and RinPustikaNo = N'" + RinPustikaNo + "' and Farmer_EID_UID_No = N'" + Farmer_EID_UID_No + "' and Farmer_KCCNo = N'" + Farmer_KCCNo + "'and Farmer_BankName_New = N'" + Farmer_BankName_New + "'and Farmer_BankAccountNo = N'" + Farmer_BankAccountNo + "'and Farmer_EID_UID_No = N'" + Farmer_EID_UID_No + "' ";
                                cmd.Parameters.Clear();
                                int count_query = Convert.ToInt32(cmd.ExecuteScalar());
                                if (count_query <= 0)
                                {
                                    cmd = new SqlCommand();
                                    cmd = connection.CreateCommand();
                                    cmd.Transaction = trans;
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "In_Update_Farmer_Info_New";
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@District_Id", DisrictID);
                                    cmd.Parameters.AddWithValue("@NewFarmer_Id", NewFarmer_Id);
                                    cmd.Parameters.AddWithValue("@Procured_SocietyID", SocietyID);
                                    int req = cmd.ExecuteNonQuery();
                                    cmd.Dispose();
                                    //update
                                    if (req > 0)
                                    {

                                        cmd = connection.CreateCommand();
                                        cmd.Transaction = trans;
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.CommandText = "Update_Farmer_R_New";
                                        cmd.Parameters.Clear();

                                        cmd.Parameters.AddWithValue("@District_Id", DisrictID);
                                        cmd.Parameters.AddWithValue("@Village_Id", Village_Id);
                                        cmd.Parameters.AddWithValue("@Tehsil_Id", Tehsil_Id);
                                        cmd.Parameters.AddWithValue("@NewFarmer_Id", NewFarmer_Id);
                                        //cmd.Parameters.AddWithValue("@Farmer_Id", NewFarmer_Id);
                                        cmd.Parameters.AddWithValue("@Gram_Panchayat", Gram_Panchayat);
                                        cmd.Parameters.AddWithValue("@PatwariHalkaNo", PatwariHalkaNo);
                                        cmd.Parameters.AddWithValue("@Mobileno", Mobileno);
                                        cmd.Parameters.AddWithValue("@RinPustikaNo", RinPustikaNo);
                                        cmd.Parameters.AddWithValue("@Farmer_EID_UID_No", Farmer_EID_UID_No);
                                        cmd.Parameters.AddWithValue("@Farmer_KCCNo", Farmer_KCCNo);
                                        cmd.Parameters.AddWithValue("@Farmer_BankName_New", Farmer_BankName_New);
                                        cmd.Parameters.AddWithValue("@Farmer_BankBranchName", Farmer_BankBranchName);
                                        cmd.Parameters.AddWithValue("@Farmer_BankAccountNo", Farmer_BankAccountNo);
                                        cmd.Parameters.AddWithValue("@Procured_SocietyID", Procured_SocietyID);
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
                                }
                            }
                        }
                        catch
                        {
 
                        }
                    }
                    trans.Commit();
                    result = true;
                }
            }
        }
        catch (Exception EX)
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
    public bool UpFarmerLandRecord(DataSet dsUpFarmerLandRecord, string DistrictId)
    {
        bool result = false;
        try
        {

            if (dsUpFarmerLandRecord != null)
            {
                if (dsUpFarmerLandRecord.Tables[0].Rows.Count > 0)
                {

                    OpenConnection();
                    trans = connection.BeginTransaction();
                    foreach (DataRow dr in dsUpFarmerLandRecord.Tables[0].Rows)
                    {
                        try
                        {

                            string TransID = dr["TransID"].ToString();
                            string NewFarmer_Id = dr["Farmer_Id"].ToString();
                            string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                            string Village_ID = dr["Village_ID"].ToString();
                            string VillageName = dr["VillageName"].ToString();

                            string LandOwner_Name = dr["LandOwner_Name"].ToString();
                            string LandOwner_RinPustikaNo = dr["LandOwner_RinPustikaNo"].ToString();
                            string LandType = dr["LandType"].ToString();
                            string KhasaraNo = dr["KhasaraNo"].ToString();
                            string Rakba = dr["Rakba"].ToString();
                            string Rakba_crop_sinchit = dr["Rakba_crop_sinchit"].ToString();
                            string Rakba_crop_asinchit = dr["Rakba_crop_asinchit"].ToString();
                            string Rakba_crop_sinchit_qty = dr["Rakba_crop_sinchit_qty"].ToString();
                            string Rakba_crop_asinchit_qty = dr["Rakba_crop_asinchit_qty"].ToString();
                            cmd = connection.CreateCommand();
                            cmd.Transaction = trans;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "select COUNT(*) from Farmer_LandRecordDescription_New_Upd_Log where TransID='" + TransID + "' and  NewFarmer_Id='" + NewFarmer_Id + "'";
                           
                            cmd.Parameters.Clear();
                            int rexs = Convert.ToInt32(cmd.ExecuteScalar());
                            if (rexs <= 0)
                            {
                                cmd = connection.CreateCommand();
                                cmd.Transaction = trans;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "In_Update_FarmerLandRecord_NEW";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@TransID", TransID);
                                cmd.Parameters.AddWithValue("@ip", ip);

                                cmd.Parameters.AddWithValue("@NewFarmer_Id", NewFarmer_Id);
                                int req = cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                if (req > 0)
                                {
                                    string Procured_qty = dr["Procured_qty"].ToString();
                                    string crpcode = dr["crpcode"].ToString();

                                    cmd = connection.CreateCommand();
                                    cmd.Transaction = trans;
                                    cmd.Parameters.Clear();
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "Up_FarmerLandRecord_New";
                                    cmd.Parameters.AddWithValue("@TransID", TransID);
                                    cmd.Parameters.AddWithValue("@NewFarmer_Id", NewFarmer_Id);
                                    cmd.Parameters.AddWithValue("@Village_ID", Village_ID);
                                    cmd.Parameters.AddWithValue("@VillageName", VillageName);

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

                            }
                            else
                            {
                                cmd = new SqlCommand();
                                cmd = connection.CreateCommand();
                                cmd.Transaction = trans;
                                cmd.CommandType = CommandType.Text;
                                cmd.CommandText = "select COUNT(*) from Farmer_LandRecordDescription_New_Upd_Log where TransID='" + TransID + "' and  NewFarmer_Id='" + NewFarmer_Id + "' and Village_ID = '" + Village_ID + "' and VillageName =N'" + VillageName + "' and LandOwner_Name = N'" + LandOwner_Name + "' and LandOwner_RinPustikaNo = N'" + LandOwner_RinPustikaNo + "' and LandType = '" + LandType + "' and KhasaraNo= N'" + KhasaraNo + "' and Rakba = N'" + Rakba + "' and Rakba_crop_sinchit = N'" + Rakba_crop_sinchit + "' and Rakba_crop_asinchit = N'" + Rakba_crop_asinchit + "' and Rakba_crop_sinchit_qty = N'" + Rakba_crop_sinchit_qty + "' and Rakba_crop_asinchit_qty = N'" + Rakba_crop_asinchit_qty + "'";
                                cmd.Parameters.Clear();
                                int res_count = Convert.ToInt32(cmd.ExecuteScalar());
                                if (res_count <= 0)
                                {
                                    cmd = new SqlCommand();
                                    cmd = connection.CreateCommand();
                                    cmd.Transaction = trans;
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "In_Update_FarmerLandRecord_NEW";
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@TransID", TransID);
                                    cmd.Parameters.AddWithValue("@ip", ip);

                                    cmd.Parameters.AddWithValue("@NewFarmer_Id", NewFarmer_Id);
                                    int req = cmd.ExecuteNonQuery();
                                    cmd.Dispose();
                                    if (req > 0)
                                    {
                                        string Procured_qty = dr["Procured_qty"].ToString();
                                        string crpcode = dr["crpcode"].ToString();

                                        cmd = connection.CreateCommand();
                                        cmd.Transaction = trans;
                                        cmd.Parameters.Clear();
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.CommandText = "Up_FarmerLandRecord_New";
                                        cmd.Parameters.AddWithValue("@TransID", TransID);
                                        cmd.Parameters.AddWithValue("@NewFarmer_Id", NewFarmer_Id);
                                        cmd.Parameters.AddWithValue("@Village_ID", Village_ID);
                                        cmd.Parameters.AddWithValue("@VillageName", VillageName);

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
                                }
                            }
                        }
                        catch
                        { 

                        }
                    }
                    trans.Commit();
                    result = false;
                }
            }


        }
        catch (Exception ex)
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

    #region Society Basic Information and Runner Process information

    [WebMethod(Description = "This is knowing status of inserting data ")]
    public void UploadDataStatus(int cntr, string LogID)
    {
        try
        {
            if (connection != null)
            {
                connection.Open();


                command = new SqlCommand();
                string selectqry = "select count(LogID) as TotalCount from RunnerLog where  LogID='" + LogID + "'";
                command.CommandText = selectqry;
                command.Connection = connection;
                int res = Convert.ToInt16(command.ExecuteScalar());
                string logcount = "";
                if (res == 0)
                {
                    logcount = Convert.ToString(1);
                }
                else
                {
                    logcount = res.ToString();
                }


                //command = new SqlCommand();
                //string qry = "update RunnerLog set Progress_Status='" + count + "'  where LogID='" + LogID + "' and DayCount='" + logcount + "'";
                //command.CommandText = qry;
                //command.Connection = connection;
                //int x = command.ExecuteNonQuery();
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

    [WebMethod(Description = "This Method Is Used For Inserting Runner Registration Information ")]
    public bool InRunnerData(string RunnerName, string MobileNo, string RunnerPassword, string EmailId, string Address)
    {
        bool result = false;
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.Transaction = trans;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "prc_InRunnerRegistration";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@RunnerID", MobileNo.Trim());
            cmd.Parameters.AddWithValue("@RunnerName", RunnerName.Trim());
            cmd.Parameters.AddWithValue("@MobileNo", MobileNo);
            cmd.Parameters.AddWithValue("@RunnerPassword", RunnerPassword.Trim());
            cmd.Parameters.AddWithValue("@EmailId", EmailId.Trim());
            cmd.Parameters.AddWithValue("@Address", Address.Trim());
            cmd.Parameters.AddWithValue("@IP", HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString());

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
            CloseConnection();
        }
        finally
        {
            CloseConnection();
        }
        return result;
    }

    [WebMethod(Description = "This Method Is Used For Inserting Runner Soceity Information ")]
    public bool InRunnerSocietyInfo(string RunnerId, string District_ID, string RunnerSocietiIds)
    {
        bool result = false;
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.Transaction = trans;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "prco_InRunnerSocietyInfo";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@RunnerId", RunnerId);
            cmd.Parameters.AddWithValue("@District_ID", District_ID);
            cmd.Parameters.AddWithValue("@RunnerSocietiIds", RunnerSocietiIds);
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
            CloseConnection();
        }
        finally
        {

            CloseConnection();
        }
        return result;




    }

    [WebMethod(Description = "This Method Is Used For Inserting Farmer Soceity Change Information ")]
    public bool InSocietyChangeLog(DataSet dsSocietyChangeLog)
    {
        bool result = false;
        try
        {
            if (dsSocietyChangeLog != null)
            {
                if (dsSocietyChangeLog.Tables[0].Rows.Count > 0)
                {
                    OpenConnection();
                    trans = connection.BeginTransaction();
                    foreach (DataRow dr in dsSocietyChangeLog.Tables[0].Rows)
                    {
                        try
                        {
                            string District_ID = dr["District_ID"].ToString();
                            string Farmer_ID = dr["Farmer_ID"].ToString();
                            string OLD_PC_ID = dr["OLD_PC_ID"].ToString();
                            string NEW_PC_ID = dr["NEW_PC_ID"].ToString();
                            commandt = connection.CreateCommand();
                            commandt.Transaction = trans;
                            commandt.Parameters.Clear();
                            commandt.CommandType = CommandType.Text;
                            commandt.CommandText = "select count(*) from SocietyChangeLog where District_ID='" + District_ID + "' and   Farmer_ID='" + Farmer_ID + "' and OLD_PC_ID='" + OLD_PC_ID + "' and NEW_PC_ID='" + NEW_PC_ID + "'";
                            Int64 res = Convert.ToInt64(commandt.ExecuteScalar());
                            commandt.Dispose();
                            if (res <= 0)
                            {
                                string UpdationDate = getDate_MDY(mmddyyyy(dr["UpdationDate"].ToString()));
                                cmd = connection.CreateCommand();
                                cmd.Transaction = trans;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "IN_SocieyChangeLog_New";
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
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "Up_FarmerSocietyOnline_New";
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@NewFarmer_Id", Farmer_ID);
                                    cmd.Parameters.AddWithValue("@District_ID", District_ID);
                                    cmd.Parameters.AddWithValue("@OLD_PC_ID", OLD_PC_ID);
                                    cmd.Parameters.AddWithValue("@NEW_PC_ID", NEW_PC_ID);
                                    int renq = cmd.ExecuteNonQuery();
                                    cmd.Dispose();
                                }
                            }
                        }
                        catch { }
                       
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

    #endregion

    #region Output for offline database

    [WebMethod(Description = "This Method Is Used For Taken Output of New Farmer Registration Request checking in offline database Information ")]
    public DataSet OpNewFarmerRegistrationRequest(string D, string S)
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
            commandt.CommandText = "select distinct NewFarmerRegistrationRequest.Farmer_Id from NewFarmerRegistrationRequest  where NewFarmerRegistrationRequest.District_Id='" + D + "' and NewFarmerRegistrationRequest.Society_Id='" + S + "'";
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

    [WebMethod(Description = "This Method Is Used For Checking Farmer Soceity Change Request in offline database")]
    public DataSet OpFarmerSchangeRequest(string D, string S)
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
            commandt.CommandText = "select distinct SocietyChangeLog.Farmer_ID from SocietyChangeLog  where SocietyChangeLog.District_ID='" + D + "' and SocietyChangeLog.OLD_PC_ID='" + S + "'";
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

    [WebMethod(Description = "This Method Is Used For Checking Farmer Registration delete request in offline database")]
    public DataSet OpFarmerDeleteRequest(string D, string S)
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
            commandt.CommandText = "select distinct FarmerDeleteRequest.Farmer_Id from FarmerDeleteRequest  where FarmerDeleteRequest.District_Id='" + D + "' and FarmerDeleteRequest.SocietyID='" + S + "'";
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

    [WebMethod(Description = "This Method Is Used For Checking Farmer Registration Updation request in offline database")]
    public DataSet OpFarmerUpdationRequest(string D, string S)
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
            commandt.CommandText = "select distinct FarmerUpdationRequest_New.FarmerId from FarmerUpdationRequest_New  where FarmerUpdationRequest_New.DisrictID='" + D + "' and FarmerUpdationRequest_New.SocietyID='" + S + "'";
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

    #region Check Connection

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

    #endregion


    [WebMethod(Description = "This Method Is Used For Inserting  Farmer LandRecord Updation ReQuest Information")]
    public bool InFarmerLandRecord(DataSet dsFarmerLandRecord, string SocietyId, string DistrictId)
    {
        bool result = false;
        try
        {
            if (dsFarmerLandRecord != null)
            {
                if (dsFarmerLandRecord.Tables[0].Rows.Count > 0)
                {
                    OpenConnection();
                    trans = connection.BeginTransaction();

                    foreach (DataRow dr in dsFarmerLandRecord.Tables[0].Rows)
                    {
                        string Farmer_Id = dr["Farmer_Id"].ToString();
                        string TransID = dr["TransID"].ToString();

                        commandt = connection.CreateCommand();
                        commandt.Transaction = trans;
                        commandt.CommandType = CommandType.Text;
                        commandt.CommandText = "select count(*) from Farmer_LandRecordDescription_New where TransID='" + TransID + "' and NewFarmer_Id='" + Farmer_Id + "' ";
                        Int64 resFL = Convert.ToInt64(commandt.ExecuteScalar());
                        commandt.Dispose();
                        if (resFL <= 0)
                        {

                            string Village_ID = dr["Village_ID"].ToString();
                            string VillageName = dr["VillageName"].ToString();
                            string Tehsil_ID = dr["Tehsil_ID"].ToString();
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
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "In_FarmerLandRecord_R_New";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@TransID", TransID);
                            cmd.Parameters.AddWithValue("@NewFarmer_Id", Farmer_Id);
                            cmd.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);

                            cmd.Parameters.AddWithValue("@Tehsil_ID", Tehsil_ID);
                            cmd.Parameters.AddWithValue("@Village_ID", Village_ID);
                            cmd.Parameters.AddWithValue("@VillageName", VillageName);
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
                            try
                            {
                                int req = cmd.ExecuteNonQuery();
                            }
                            catch { }

                            cmd.Dispose();
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

    [WebMethod(Description = "This Method Is Used For Checking Farmer Registration SoceityWise in offline database")]
    public DataSet OpFarmerRegistrationSocietyWise(string D, string S, string farmerid)
    {
        dataset = new DataSet();
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.StoredProcedure;
            commandt.Parameters.Clear();
            commandt.CommandText = "proc_selectfarmerId";
            commandt.Parameters.AddWithValue("@District_Id", D);
            commandt.Parameters.AddWithValue("@Procured_SocietyID", S);
            commandt.Parameters.AddWithValue("@farmer_id", farmerid);
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

    [WebMethod(Description = "This Method Is Used For Checking Farmer Landrecords SoceityWise in offline database")]
    public DataSet OpFarmerLandRecordDescriptionSocietyWise(string Procured_SocietyID, string District_Id, string farmerid)
    {
        dataset = new DataSet();
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.StoredProcedure;
            commandt.CommandText = "proc_SelectFarmerLandReocrd";
            commandt.Parameters.AddWithValue("@Procured_SocietyID", Procured_SocietyID);
            commandt.Parameters.AddWithValue("@District_Id", District_Id);
            commandt.Parameters.AddWithValue("@farmer_id", farmerid);
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

}
public class SecurityFarmerPaddy2013 : System.Web.Services.Protocols.SoapHeader
{
    public bool IsActive;
    public String Password;
    public String UserName;
    public String RunnerId;
    public String RunnerPassword;
}




