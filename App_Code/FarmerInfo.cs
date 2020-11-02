using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


/// <summary>
/// Summary description for FarmerInfo
/// </summary>
/// 

[WebService(Namespace = "http://microsoft.co.in/", Name = "RunnerServiceWheatProcurement2013", Description = "Export Farmer Data (upload data on server)/Date: 30012013")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class FarmerInfo : System.Web.Services.WebService
{
    private SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2013"].ToString());
    private SqlCommand command, cmd;
    private SqlDataAdapter dataAdapter;
    private DataSet dataset;
    private SqlTransaction trans = null;
    private SqlCommand commandt = null;


    public FarmerInfo()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }


    #region Security

    public SecurityFarmer securityfarmer;
    [SoapHeader("securityfarmer")]
    [WebMethod]
    public bool chkSecurityFarmer(SecurityFarmer S)
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


    #region Export



    [WebMethod]
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


    [WebMethod]
    public bool InFarmer(DataSet dsFarmer)
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
                            cmd.CommandText = "in_Farmer";
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
                        string rakbasinchit = dr["Rakba_crop_sinchit"].ToString();

                        commandt = connection.CreateCommand();
                        commandt.Transaction = trans;
                        commandt.CommandType = CommandType.Text;
                        commandt.CommandText = "select count(*) from Farmer_LandRecordDescription where TransID='" + TransID + "' and Farmer_Id='" + Farmer_Id + "' ";
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
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "in_FarmerLandRecord";
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
                        string FarmerId = dr["FarmerId"].ToString();
                        string UpdationDate_Off = getRDate_MDY(dr["UpdationDate"].ToString());

                        commandt = connection.CreateCommand();
                        commandt.Transaction = trans;
                        commandt.CommandType = CommandType.Text;
                   //     commandt.CommandText = "select count(*) from FarmerUpdationRequest where DisrictID='" + DistrictId + "' and SocietyID='" + SocietyId + "' and  FarmerId='" + FarmerId + "' and UpdationDate_Off ='" + UpdationDate_Off + "'";
 
commandt.CommandText = "select count(*) from FarmerUpdationRequest_ProcMent where DisrictID='" + DistrictId + "' and SocietyID='" + SocietyId + "' and  FarmerId='" + FarmerId + "' and UpdationDate_Off ='" + UpdationDate_Off + "'";

                     
   Int64 res = Convert.ToInt64(commandt.ExecuteScalar());
                        commandt.Dispose();
                        if (res <= 0)
                        {
                            string UpdationDate_Onn = getRDate_MDY(System.DateTime.Now.ToShortDateString());
                            string IsFRUpdated = dr["IsFRUpdated"].ToString();
                            string IsLRUpdated = dr["IsLRUpdated"].ToString();
                            cmd = connection.CreateCommand();
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

                        string FarmerId = dr["Farmer_Id"].ToString();
                        string DisrictID = dr["District_Id"].ToString();
                        string SocietyID = dr["PC_ID"].ToString();

                        //Insert Into Updation log
                        cmd = connection.CreateCommand();
                        cmd.Transaction = trans;
                        cmd.CommandType = CommandType.Text;
//change01042013
                    //    cmd.CommandText = "select COUNT(*) from update_FarmerRegistrationLog where Farmer_Id='" + FarmerId + "' and District_Id='" + DisrictID + "' and PC_ID='" + SocietyID + "'";
                     cmd.CommandText = "select COUNT(*) from update_FarmerRegistrationLog_ProcMent where Farmer_Id='" + FarmerId + "' and District_Id='" + DisrictID + "' and PC_ID='" + SocietyID + "'";
    
  int rexs = Convert.ToInt32(cmd.ExecuteScalar());
                        if (rexs <= 0)
                        {
                            cmd = connection.CreateCommand();
                            cmd.Transaction = trans;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "in_Update_Farmer";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@District_Id", DisrictID);
                            cmd.Parameters.AddWithValue("@Farmer_Id", FarmerId);
                            cmd.Parameters.AddWithValue("@PC_ID", SocietyID);
                            int req = cmd.ExecuteNonQuery();
                            cmd.Dispose();

                            //update
                            if (req > 0)
                            {
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
    public bool UpFarmerLandRecord(DataSet dsUpFarmerLandRecord, string SocietyId, string DistrictId)
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
                        string TransID = dr["TransID"].ToString();
                        string Farmer_Id = dr["Farmer_Id"].ToString();
                        string Procured_SocietyID = dr["Procured_SocietyID"].ToString();
                        string District_Id = dr["District_Id"].ToString();


                        cmd = connection.CreateCommand();
                        cmd.Transaction = trans;
                        cmd.CommandType = CommandType.Text;
              //change on 01042013        
//  cmd.CommandText = "select COUNT(*) from update_Farmer_LandRecordDescriptionLog where TransID='" + TransID + "' and  Farmer_Id='" + Farmer_Id + "' and District_Id='" + District_Id + "' and Procured_SocietyID='" + SocietyId + "'";
   cmd.CommandText = "select COUNT(*) from update_Farmer_LandRecordDescriptionLog_ProcMent where TransID='" + TransID + "' and  Farmer_Id='" + Farmer_Id + "' and District_Id='" + District_Id + "' and Procured_SocietyID='" + SocietyId + "'";                      
cmd.Parameters.Clear();
                        int rexs = Convert.ToInt32(cmd.ExecuteScalar());

                        if (rexs <= 0)
                        {
                            cmd = connection.CreateCommand();
                            cmd.Transaction = trans;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "in_Update_FarmerLandRecord";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@TransID", TransID);
                            cmd.Parameters.AddWithValue("@District_Id", District_Id);
                            cmd.Parameters.AddWithValue("@Procured_SocietyID", SocietyId);
                            cmd.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                            int req = cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            if (req > 0)
                            {
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

                        }
                    }
                    trans.Commit();
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

    [WebMethod]
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
                            string UpdationDate = getRDate_MDY(dr["UpdationDate"].ToString());
                            cmd = connection.CreateCommand();
                            cmd.Transaction = trans;
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
                        string District_Id = dr["District_Id"].ToString();
                        string SocietyID = dr["SocietyID"].ToString();
                        string Farmer_Id = dr["Farmer_Id"].ToString();

                        commandt = connection.CreateCommand();
                        commandt.Transaction = trans;
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
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@District_Id", District_Id);
                                    cmd.Parameters.AddWithValue("@Procured_SocietyID", SocietyID);
                                    cmd.Parameters.AddWithValue("@Farmer_Id", Farmer_Id);
                                    int reqd = cmd.ExecuteNonQuery();
                                    cmd.Dispose();
                                }



                                cmd = connection.CreateCommand();
                                cmd.Transaction = trans;
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



    #region Output Information for FarmerRegistration


    [WebMethod]
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

    [WebMethod]
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

          //  commandt.CommandText = "select distinct FarmerUpdationRequest.FarmerId from FarmerUpdationRequest  where FarmerUpdationRequest.DisrictID='" + D + "' and FarmerUpdationRequest.SocietyID='" + S + "'";
         
 commandt.CommandText = "select distinct FarmerUpdationRequest_ProcMent.FarmerId from FarmerUpdationRequest_ProcMent  where FarmerUpdationRequest_ProcMent.DisrictID='" + D + "' and FarmerUpdationRequest_ProcMent.SocietyID='" + S + "'";

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

    [WebMethod]
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

public class SecurityFarmer : System.Web.Services.Protocols.SoapHeader
{
    public bool IsActive;
    public String ID;
}

