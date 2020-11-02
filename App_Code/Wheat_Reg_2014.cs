using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for Wheat_Reg_2014
/// </summary>
[WebService(Namespace = "http://microsoft.co.in/", Name = "Farmer_Registration_Transferring_Services_Wheat_2014", Description = "Transfer Registation Data during procurement center initialization, Hosted Date :: 10/12/2013 ")]

[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Wheat_Reg_2014 : System.Web.Services.WebService 
{
    private SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_WPMS2014"].ToString());
    private SqlCommand command, cmd;
    private SqlDataAdapter dataAdapter;
    private DataSet dataset;
    private SqlTransaction trans = null;
    private SqlCommand commandt = null;
    private int count = 0;
    public string LogID = "";

    public Wheat_Reg_2014 () 
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    #region Security

    public Security_Farmer_Reg_Wheat_2014 Security_Farmer_Reg_Wheat_2014;
    [SoapHeader("Security_Farmer_Reg_Wheat_2014")]

    [WebMethod(Description = "This Method Is Used For Security Check in")]
    public bool chkSecurityFarmer(Security_Farmer_Reg_Wheat_2014 S)
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

    #region Insertion in online From Offline database of farmer Registration

    #region In Farmer Basic Information and Landrecord Description Tables

    [WebMethod(Description = "This Method Is Used For Inserting  Farmer Registration Information")]
    public void InsertFarmerInfo(DataSet dsFarmerInfo)
    {
        string farmerid = "";
        try
        {
            if (connection != null)
            {
                OpenConnection();
                trans = connection.BeginTransaction();
                if (dsFarmerInfo != null)
                {
                    foreach (DataRow dr in dsFarmerInfo.Tables[0].Rows)
                    {
                        try
                        {
                            farmerid = dr["Farmer_Id"].ToString();
                            string District_Id = dr["District_Id"].ToString();
                            string societyid = dr["Procured_SocietyID"].ToString();
                            command = new SqlCommand();
                            command = connection.CreateCommand();
                            command.Transaction = trans;
                            command.CommandType = CommandType.Text;
                            command.CommandText = "Select count(*) from FarmerRegistration where Farmer_Id='" + farmerid + "' and  District_Id='" + District_Id + "'";
                            command.Connection = connection;
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
                                string MarketingSeasonId = "R";
                                string CropYear = "2014-15";

                                cmd = new SqlCommand();
                                cmd = connection.CreateCommand();
                                cmd.Transaction = trans;
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.CommandText = "Insert_FarmerRegistration";
                                cmd.Parameters.AddWithValue("@Farmer_Id", farmerid);
                                cmd.Parameters.AddWithValue("@District_Id", District_Id);
                                cmd.Parameters.AddWithValue("@Village_Id", Village_Id);
                                cmd.Parameters.AddWithValue("@VillageName", VillageName);

                                cmd.Parameters.AddWithValue("@Tehsil_Id", Tehsil_Id);
                               
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
                                int x = cmd.ExecuteNonQuery();
                                cmd.Dispose();
                            }
                           
                        }
                        catch
                        {
                            ////////////
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
    }

    [WebMethod(Description = "This Method Is Used For Inserting Farmer Land Record Information")]
    public void InsertFarmerLandInfo(DataSet dsFarmerLandInfo)
    {
       
        try
        {
            if (connection != null)
            {
                OpenConnection();
                cmd = new SqlCommand();
                cmd = connection.CreateCommand();
                cmd.Transaction = trans;
              
                if (dsFarmerLandInfo != null)
                {
                    if (dsFarmerLandInfo.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drs in dsFarmerLandInfo.Tables[0].Rows)
                        {
                            trans = connection.BeginTransaction();
                            try
                            {
                                string socityID = drs["TransID"].ToString().Substring(0, 7);
                                string TransID = drs["TransID"].ToString();
                                string Farmer_Id = drs["Farmer_Id"].ToString();
                                string query = "select count(TransID) from Farmer_LandRecordDescription where TransID='" + TransID + "' and Farmer_Id='" + Farmer_Id + "'";
                                cmd = new SqlCommand(query, connection,trans);
                                int ret = Convert.ToInt16(cmd.ExecuteScalar());
                                cmd.Parameters.Clear();
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

                                   
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "Insert_FarmerLandRecordDescription";

                                    cmd.Parameters.AddWithValue("@TransID", TransID);
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
                                    catch
                                    {
                                    }
                                    cmd.Parameters.Clear();
                                    cmd.Dispose();
                                }
                            }
                            catch
                            {
                                trans.Rollback();
                                CloseConnection();
                            }
                            trans.Commit();
                        }
                       
                    }
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

    [WebMethod(Description = "This Method Is Used For Inserting Farmer Land Record Initial Information")]
    public void Insert_FarmerLand_Intial(DataSet dsFarmerLandInfo, string S)
    {

        try
        {
            if (connection != null)
            {
                OpenConnection();
                trans = connection.BeginTransaction();
                if (dsFarmerLandInfo != null)
                {
                    if (dsFarmerLandInfo.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drs in dsFarmerLandInfo.Tables[0].Rows)
                        {
                            try
                            {
                                string socityID = S.ToString();
                                string TransID = drs["TransID"].ToString();
                                string Farmer_Id = drs["Farmer_Id"].ToString();
                                command = new SqlCommand();
                                command = connection.CreateCommand();
                                command.Transaction = trans;
                                command.CommandType = CommandType.Text;
                                command.CommandText = "select count(TransID) from Farmer_LandRecord_initial where Farmer_LandRecord_initial.TransID='" + TransID + "' and Farmer_Id='" + Farmer_Id + "'";
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
                                  
                                    string crpcode = drs["crpcode"].ToString();

                                    cmd = new SqlCommand();
                                    cmd = connection.CreateCommand();
                                    cmd.Transaction = trans;
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "Insert_FarmerLandRecord_Initial";

                                    cmd.Parameters.AddWithValue("@TransID", TransID);
                                   
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
                                  
                                    cmd.Parameters.AddWithValue("@crpcode", crpcode);
                                    try
                                    {
                                        int rl = cmd.ExecuteNonQuery();
                                    }
                                    catch
                                    {
                                    }
                                    cmd.Dispose();
                                }
                            }
                            catch
                            {
                                ///////////
                            }
                        }
                        trans.Commit();
                    }
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
                        string PC_Id = dr["PC_Id"].ToString();
                        string CreatedDate = getDate_MDY(mmddyyyy(dr["CreatedDate"].ToString()));
                        string CreatedBy = dr["CreatedBy"].ToString();
                        string Status = dr["Status"].ToString();
                        string Pre_FarmerID = dr["Pre_FarmerID"].ToString();
                        commandt = connection.CreateCommand();
                        commandt.Transaction = trans;
                        commandt.CommandType = CommandType.Text;
                        commandt.CommandText = "select count(Farmer_Id) from  NewFarmerRegistration_Log where NewFarmerRegistration_Log.Farmer_Id='" + Farmer_Id + "' and District_Id='" + D + "' and Society_Id='" + S + "'";
                        int re = Convert.ToInt32(commandt.ExecuteScalar());
                        commandt.Dispose();
                        if (re <= 0)
                        {
                            try
                            {
                                commandt = connection.CreateCommand();
                                commandt.Transaction = trans;
                                commandt.CommandType = CommandType.Text;
                                commandt.CommandText = "Insert Into NewFarmerRegistration_Log(Farmer_Id,District_Id,Society_Id,PC_Id,CreatedDate,CreatedBy,Status,Pre_FarmerID) values('" + Farmer_Id + "','" + D + "','" + S + "','" + PC_Id + "','" + CreatedDate + "','" + CreatedBy + "','" + Status + "','" + Pre_FarmerID + "')";
                                int renq = commandt.ExecuteNonQuery();
                                commandt.Dispose();
                            }
                            catch
                            {
                                //////////////////
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
                            commandt.CommandText = "select count(*) from FarmerUpdationLog where DisrictID='" + DistrictId + "' and SocietyID='" + SocietyId + "' and  FarmerId='" + FarmerId + "' and UpdationDate_Off ='" + UpdationDate_Off + "'";
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
                                cmd.CommandText = "In_FarmerUpdationRequest";
                                cmd.Parameters.AddWithValue("@FarmerId", FarmerId);
                                cmd.Parameters.AddWithValue("@DisrictID", DistrictId);
                                cmd.Parameters.AddWithValue("@SocietyID", SocietyId);
                                cmd.Parameters.AddWithValue("@IsFRUpdated", IsFRUpdated);
                                cmd.Parameters.AddWithValue("@IsLRUpdated", IsLRUpdated);
                                cmd.Parameters.AddWithValue("@UpdationDate_Off", UpdationDate_Off);
                                cmd.Parameters.AddWithValue("@UpdationDate_Onn", UpdationDate_Onn);
                                try
                                {
                                    int req = cmd.ExecuteNonQuery();
                                }
                                catch
                                {
                                    //////
                                }
                                cmd.Dispose();
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
                                cmd.CommandText = "Insert_FarmerDeleteRequest";
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
                                    cmd.CommandText = "Insert_FarmerRegistration_del";
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@District_Id", District_Id);
                                    cmd.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                                    cmd.Parameters.AddWithValue("@Procured_SocietyID", Procured_SocietyID);
                                    int reqs = cmd.ExecuteNonQuery();
                                    cmd.Dispose();

                                    if (reqs > 0)
                                    {
                                        string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                                        cmd = connection.CreateCommand();
                                        cmd.Transaction = trans;
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.CommandText = "Insert_Farmer_LandRecordDescription_Del";
                                        cmd.Parameters.Clear();
                                        cmd.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                                        cmd.Parameters.AddWithValue("@ip", ip);
                                        int reqd = cmd.ExecuteNonQuery();
                                        cmd.Dispose();
                                    }

                                    cmd = connection.CreateCommand();
                                    cmd.Transaction = trans;
                                    cmd.Parameters.Clear();
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "Del_FarmerBasic_Info";
                                    cmd.Parameters.AddWithValue("@District_Id", District_Id);
                                    cmd.Parameters.AddWithValue("@Procured_SocietyID", Procured_SocietyID);
                                    cmd.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                                    int reqfr = cmd.ExecuteNonQuery();
                                    cmd.Dispose();

                                    if (reqfr > 0)
                                    {
                                        cmd = connection.CreateCommand();
                                        cmd.Transaction = trans;
                                        cmd.Parameters.Clear();
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.CommandText = "Delete_FarmerLandRecord_Info";
                                        cmd.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                                        int reqFL = cmd.ExecuteNonQuery();
                                        cmd.Dispose();
                                    }
                                }
                            }
                        }
                        catch
                        {
                            ///////////////////
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
                                    cmd.CommandText = "Update_FarmerSocietyOnline";
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@Farmer_Id", Farmer_ID);
                                    cmd.Parameters.AddWithValue("@District_ID", District_ID);
                                    cmd.Parameters.AddWithValue("@OLD_PC_ID", OLD_PC_ID);
                                    cmd.Parameters.AddWithValue("@NEW_PC_ID", NEW_PC_ID);
                                    int renq = cmd.ExecuteNonQuery();
                                    cmd.Dispose();
                                }
                            }
                        }
                        catch
                        {
                            /////////////
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

    #region Update Farmer Basic Information and Landrecord Description

    [WebMethod(Description = "This Method Is Used For Updating Farmer Basic Information")]
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
                    cmd = new SqlCommand();
                    cmd = connection.CreateCommand();
                    cmd.Transaction = trans;
                    string Query = "";
                    foreach (DataRow dr in dsUpFarmer.Tables[0].Rows)
                    {
                        trans = connection.BeginTransaction();
                        try
                        {
                            string Farmer_Id = dr["Farmer_Id"].ToString();
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

                            Query = "select COUNT(*) from FarmerRegistration_Log where District_Id = '" + DisrictID + "' and Procured_SocietyID ='" + SocietyID + "' and Farmer_Id='" + Farmer_Id + "'";
                            cmd = new SqlCommand(Query, connection, trans);
                            int count_que = Convert.ToInt32(cmd.ExecuteScalar());
                            cmd.Parameters.Clear();
                            if (count_que <= 0)
                            {
                               
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "In_FarmerRegistration_Upd_Log";
                                cmd.Parameters.AddWithValue("@District_Id", DisrictID);
                                cmd.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                                cmd.Parameters.AddWithValue("@Procured_SocietyID", SocietyID);
                                int req = cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();

                                //update
                                if (req > 0)
                                {
                                   
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "Update_FarmerBasic_Info";
                                    cmd.Parameters.AddWithValue("@District_Id", DisrictID);
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
                                    cmd.Parameters.AddWithValue("@Procured_SocietyID", Procured_SocietyID);
                                    cmd.Parameters.AddWithValue("@Procured_Place", Procured_Place);
                                    cmd.Parameters.AddWithValue("@CropExpected_Date", CropExpected_Date);
                                    cmd.Parameters.AddWithValue("@UserID", UserID);
                                    cmd.Parameters.AddWithValue("@updatedDate", updatedDate);
                                    cmd.Parameters.AddWithValue("@ip", HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString());
                                    cmd.Parameters.AddWithValue("@IsApproved", IsApproved);
                                    cmd.Parameters.AddWithValue("@Status", Status);
                                    int _req = cmd.ExecuteNonQuery();
                                    cmd.Parameters.Clear();
                                    cmd.Dispose();
                                }
                            }
                            else
                            {
                                Query = "";
                                Query = "select COUNT(Farmer_Id) from FarmerRegistration where Farmer_Id='" + Farmer_Id + "' and District_Id = '" + DisrictID + "' and Procured_SocietyID ='" + SocietyID + "' and Village_Id = '" + Village_Id + "' and Villagename = N'" + Villagename + "' and Tehsil_Id = '" + Tehsil_Id + "' and Gram_Panchayat= N'" + Gram_Panchayat + "' and PatwariHalkaNo = N'" + PatwariHalkaNo + "' and Mobileno = '" + Mobileno + "' and Category = '" + Category + "' and RinPustikaNo = N'" + RinPustikaNo + "' and Farmer_EID_UID_No = N'" + Farmer_EID_UID_No + "' and Farmer_KCCNo = N'" + Farmer_KCCNo + "'and Farmer_BankName_New = N'" + Farmer_BankName_New + "'and Farmer_BankAccountNo = N'" + Farmer_BankAccountNo + "'and Farmer_EID_UID_No = N'" + Farmer_EID_UID_No + "' ";
                                cmd = new SqlCommand(Query, connection, trans);
                                int count_query = Convert.ToInt32(cmd.ExecuteScalar());
                                cmd.Parameters.Clear();
                                if (count_query <= 0)
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "In_FarmerRegistration_Upd_Log";
                                    cmd.Parameters.AddWithValue("@District_Id", DisrictID);
                                    cmd.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                                    cmd.Parameters.AddWithValue("@Procured_SocietyID", SocietyID);
                                    int req = cmd.ExecuteNonQuery();
                                    cmd.Parameters.Clear();
                                    //update
                                    if (req > 0)
                                    {
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.CommandText = "Update_FarmerBasic_Info";
                                        cmd.Parameters.AddWithValue("@District_Id", DisrictID);
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
                                        cmd.Parameters.AddWithValue("@Procured_SocietyID", Procured_SocietyID);
                                        cmd.Parameters.AddWithValue("@Procured_Place", Procured_Place);
                                        cmd.Parameters.AddWithValue("@CropExpected_Date", CropExpected_Date);
                                        cmd.Parameters.AddWithValue("@UserID", UserID);
                                        cmd.Parameters.AddWithValue("@updatedDate", updatedDate);
                                        cmd.Parameters.AddWithValue("@ip", HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString());
                                        cmd.Parameters.AddWithValue("@IsApproved", IsApproved);
                                        cmd.Parameters.AddWithValue("@Status", Status);
                                        int _req = cmd.ExecuteNonQuery();
                                        cmd.Parameters.Clear();
                                        cmd.Dispose();
                                    }
                                }
                            }
                        }
                        catch
                        {
                            trans.Rollback();
                            CloseConnection();
                        }
                        trans.Commit();
                    }
                  result = true;
                }
            }
        }
        catch (Exception EX)
        {
            CloseConnection();
        }
        finally
        {
            CloseConnection();
        }
        return result;
    }

    [WebMethod(Description = "This Method Is Used For Updating Farmer Land Record Information")]
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
                    cmd = connection.CreateCommand();
                    cmd.Transaction = trans;
                    string Query = "";
                    foreach (DataRow dr in dsUpFarmerLandRecord.Tables[0].Rows)
                    {
                        trans = connection.BeginTransaction();
                        try
                        {
                            string TransID = dr["TransID"].ToString();
                            string Farmer_Id = dr["Farmer_Id"].ToString();
                            string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                            string Tehsil_ID = dr["Tehsil_ID"].ToString();
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
                          
                            Query = "select COUNT(TransID) from Farmer_LandRecordDescription_Log where TransID='" + TransID + "' and Farmer_Id='" + Farmer_Id + "'";
                            cmd = new SqlCommand(Query, connection, trans);
                            int rexs = Convert.ToInt32(cmd.ExecuteScalar());
                            cmd.Parameters.Clear();
                            if (rexs <= 0)
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "Insert_Farmer_LandRecordDescription_Upd_Log";
                                cmd.Parameters.AddWithValue("@TransID", TransID);
                                cmd.Parameters.AddWithValue("@ip", ip);
                                cmd.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                                int req = cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                                if (req > 0)
                                {
                                    string Procured_qty = dr["Procured_qty"].ToString();
                                    string crpcode = dr["crpcode"].ToString();
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "Update_FarmerLandRecord_Description";
                                    cmd.Parameters.AddWithValue("@TransID", TransID);
                                    cmd.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                                    cmd.Parameters.AddWithValue("@Tehsil_ID", Tehsil_ID);
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
                                    cmd.Parameters.Clear();
                                    cmd.Dispose();
                                }

                            }
                            else
                            {

                                Query = "select COUNT(TransID) from Farmer_LandRecordDescription where TransID='" + TransID + "' and Farmer_Id='" + Farmer_Id + "' and Tehsil_ID = '" + Tehsil_ID + "'  and Village_ID = '" + Village_ID + "' and VillageName =N'" + VillageName + "' and LandOwner_Name = N'" + LandOwner_Name + "' and LandOwner_RinPustikaNo = N'" + LandOwner_RinPustikaNo + "' and LandType = '" + LandType + "' and KhasaraNo= N'" + KhasaraNo + "' and Rakba = N'" + Rakba + "' and Rakba_crop_sinchit = N'" + Rakba_crop_sinchit + "' and Rakba_crop_asinchit = N'" + Rakba_crop_asinchit + "' and Rakba_crop_sinchit_qty = N'" + Rakba_crop_sinchit_qty + "' and Rakba_crop_asinchit_qty = N'" + Rakba_crop_asinchit_qty + "'";
                                cmd = new SqlCommand(Query, connection, trans);
                                int res_count = Convert.ToInt32(cmd.ExecuteScalar());
                                cmd.Parameters.Clear();
                                if (res_count <= 0)
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "Insert_Farmer_LandRecordDescription_Upd_Log";
                                    cmd.Parameters.AddWithValue("@TransID", TransID);
                                    cmd.Parameters.AddWithValue("@ip", ip);
                                    cmd.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                                    int req = cmd.ExecuteNonQuery();
                                    cmd.Parameters.Clear();
                                    if (req > 0)
                                    {
                                        string Procured_qty = dr["Procured_qty"].ToString();
                                        string crpcode = dr["crpcode"].ToString();
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.CommandText = "Update_FarmerLandRecord_Description";
                                        cmd.Parameters.AddWithValue("@TransID", TransID);
                                        cmd.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                                        cmd.Parameters.AddWithValue("@Tehsil_ID", Tehsil_ID);
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
                                        cmd.Parameters.Clear();
                                        cmd.Dispose();
                                    }
                                }
                            }
                        }
                        catch
                        {
                            trans.Rollback();
                            CloseConnection();
                        }
                        trans.Commit();
                    }
                  
                    result = false;
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
            commandt.CommandText = "select distinct NewFarmerRegistration_Log.Farmer_Id from NewFarmerRegistration_Log  where NewFarmerRegistration_Log.District_Id='" + D + "' and NewFarmerRegistration_Log.Society_Id='" + S + "'";
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

    [WebMethod(Description = "This Method Is Used For Taken Output of New Farmer Registration Request checking in offline database Information ")]
    public DataSet OpFarmer_Update_Request(string D, string S)
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
            commandt.CommandText = "select distinct FarmerRegistration.Farmer_Id from FarmerRegistration  where FarmerRegistration.District_Id='" + D + "' and FarmerRegistration.Procured_SocietyID='" + S + "'";
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
            commandt.CommandText = "select distinct FarmerUpdationLog.FarmerId from FarmerUpdationLog  where FarmerUpdationLog.DisrictID='" + D + "' and FarmerUpdationLog.SocietyID='" + S + "'";
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

    #region Commonly Used Functions

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
}
public class Security_Farmer_Reg_Wheat_2014 : System.Web.Services.Protocols.SoapHeader
{
    public bool IsActive;
    public String Password;
    public String UserName;
    public String RunnerId;
    public String RunnerPassword;
}

