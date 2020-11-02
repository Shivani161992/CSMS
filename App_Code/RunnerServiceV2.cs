using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
using System.Configuration;


/// <summary>
/// Summary description for RunnerServiceV2
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class RunnerServiceV2 : System.Web.Services.WebService
{
    private SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_test"].ToString());
    private SqlConnection connectionNEW = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_test"].ToString());
    private SqlConnection connectionMASTER = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online"].ToString());
    private SqlCommand command, cmd;
    private SqlDataAdapter dataAdapter;
    private DataSet dataset;
    private DataTable datatable;
    private string societyIDloan = "";
    public RunnerServiceV2()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public bool Login(String username, String password)
    {


        bool result = false;
        return result;
    }

    [WebMethod]
    public bool InsertINITAIAL(DataSet dsInitial)
    {
        bool result = false;
        try
        {
            if (connectionMASTER != null)
            {
                connectionMASTER.Open();
                if (dsInitial != null)
                {
                    if (dsInitial.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsInitial.Tables[0].Rows)
                        {
                            // command.Dispose();
                            //  if (ds.tables[0].rows.count > 0)
                            string District_ID = dr["District_ID"].ToString();
                            string Society_Id = dr["Society_Id"].ToString();

                            string PC_Id = (dr["PC_Id"].ToString());


                            cmd = new SqlCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "select * from Initial where District_ID='" + District_ID + "' and Society_Id='" + Society_Id + "' and PC_Id='" + PC_Id + "'";
                            cmd.Connection = connectionMASTER;
                            SqlDataReader drsc;
                            drsc = cmd.ExecuteReader();
                            if (drsc.Read())
                            {//update the stock
                                drsc.Close();
                                try
                                {
                                    command = new SqlCommand();
                                    command.Connection = connectionMASTER;
                                    command.CommandType = CommandType.Text;
                                    command.CommandText = "UPDATE  [Initial] SET [District_Name] = @District_Name,[SocietyName] = @SocietyName,[PC_NAME] = @PC_NAME,[AgencyId] = @AgencyId,[AgencyName] = @AgencyName,[MarketingSeasonId] = @MarketingSeasonId,[MarketingSeason] = @MarketingSeason,[CropYear] = @CropYear,[OpeningStockOfGunny] = @OpeningStockOfGunny,[Password1] = @Password1,[BankName] = @BankName,[AccNO] = @AccNO,[BranchName] = @BranchName,[ManagerName] = @ManagerName,[VersionNo] = @VersionNo,[NoOfToulKanta] = @NoOfToulKanta,[DailySc_Capacity] = @DailySc_Capacity,[UpdationDate] = @UpdationDate,[Societycreditlimit] = @Societycreditlimit,[OneFarmerLimit] = @OneFarmerLimit,[MgrMobileNo] = @MgrMobileNo,[SocBandaranCapacity] = @SocBandaranCapacity,[DateTimeOfInstall] = @DateTimeOfInstall WHERE Society_Id=@Society_Id and PC_Id=@PC_Id and District_ID=@District_ID";
                                    command.Parameters.AddWithValue("@District_ID", dr["District_ID"].ToString());
                                    command.Parameters.AddWithValue("@District_Name", dr["District_Name"].ToString());
                                    command.Parameters.AddWithValue("@Society_Id", dr["Society_Id"].ToString());
                                    command.Parameters.AddWithValue("@SocietyName", dr["SocietyName"].ToString());
                                    command.Parameters.AddWithValue("@PC_Id", dr["PC_Id"].ToString());
                                    command.Parameters.AddWithValue("@PC_NAME", dr["PC_NAME"].ToString());
                                    command.Parameters.AddWithValue("@AgencyId", dr["AgencyId"].ToString());
                                    command.Parameters.AddWithValue("@AgencyName", dr["AgencyName"].ToString());
                                    command.Parameters.AddWithValue("@MarketingSeasonId", dr["MarketingSeasonId"].ToString());
                                    command.Parameters.AddWithValue("@MarketingSeason", dr["MarketingSeason"].ToString());
                                    command.Parameters.AddWithValue("@CropYear", dr["CropYear"].ToString());

                                    command.Parameters.AddWithValue("@OpeningStockOfGunny", dr["OpeningStockOfGunny"].ToString());
                                    command.Parameters.AddWithValue("@Password1", dr["Password1"].ToString());
                                    command.Parameters.AddWithValue("@BankName", dr["BankName"].ToString());
                                    command.Parameters.AddWithValue("@AccNO", dr["AccNO"].ToString());
                                    command.Parameters.AddWithValue("@BranchName", dr["BranchName"].ToString());
                                    command.Parameters.AddWithValue("@ManagerName", dr["ManagerName"].ToString());
                                    command.Parameters.AddWithValue("@VersionNo", dr["VersionNo"].ToString());
                                    command.Parameters.AddWithValue("@NoOfToulKanta", dr["NoOfToulKanta"].ToString());
                                    command.Parameters.AddWithValue("@DailySc_Capacity", dr["DailySc_Capacity"].ToString());
                                    command.Parameters.AddWithValue("@UpdationDate", getDate_MDY(dr["UpdationDate"].ToString()));

                                    command.Parameters.AddWithValue("@Societycreditlimit", dr["Societycreditlimit"].ToString());
                                    command.Parameters.AddWithValue("@OneFarmerLimit", dr["OneFarmerLimit"].ToString());
                                    command.Parameters.AddWithValue("@MgrMobileNo", dr["MgrMobileNo"].ToString());
                                    command.Parameters.AddWithValue("@SocBandaranCapacity", dr["SocBandaranCapacity"].ToString());
                                    command.Parameters.AddWithValue("@DateTimeOfInstall", getDate_MDY(dr["DateTimeOfInstall"].ToString()));

                                    command.ExecuteNonQuery();
                                    result = true;
                                }
                                catch (Exception ex)
                                {
                                }
                                finally
                                {
                                    cmd.Dispose();
                                }
                            }
                            else
                            {
                                //insert the stock
                                drsc.Close();

                                try
                                {
                                    command = new SqlCommand();
                                    command.Connection = connectionMASTER;
                                    command.CommandType = CommandType.Text;
                                    command.CommandText = "INSERT INTO [Initial]([District_ID],[District_Name],[Society_Id],[SocietyName],[PC_Id],[PC_NAME],[AgencyId],[AgencyName],[MarketingSeasonId],[MarketingSeason],[CropYear],[OpeningStockOfGunny],[Password1],[BankName],[AccNO],[BranchName],[ManagerName],[VersionNo],[NoOfToulKanta],[DailySc_Capacity],[UpdationDate],[Societycreditlimit],[OneFarmerLimit],[MgrMobileNo] ,[SocBandaranCapacity],[DateTimeOfInstall]) VALUES (@District_ID ,@District_Name,@Society_Id,@SocietyName,@PC_Id,@PC_NAME,@AgencyId,@AgencyName,@MarketingSeasonId,@MarketingSeason,@CropYear,@OpeningStockOfGunny,@Password1,@BankName,@AccNO,@BranchName,@ManagerName,@VersionNo,@NoOfToulKanta,@DailySc_Capacity,@UpdationDate,@Societycreditlimit,@OneFarmerLimit,@MgrMobileNo ,@SocBandaranCapacity,@DateTimeOfInstall)";
                                    command.Parameters.AddWithValue("@District_ID", dr["District_ID"].ToString());
                                    command.Parameters.AddWithValue("@District_Name", dr["District_Name"].ToString());
                                    command.Parameters.AddWithValue("@Society_Id", dr["Society_Id"].ToString());
                                    command.Parameters.AddWithValue("@SocietyName", dr["SocietyName"].ToString());
                                    command.Parameters.AddWithValue("@PC_Id", dr["PC_Id"].ToString());
                                    command.Parameters.AddWithValue("@PC_NAME", dr["PC_NAME"].ToString());
                                    command.Parameters.AddWithValue("@AgencyId", dr["AgencyId"].ToString());
                                    command.Parameters.AddWithValue("@AgencyName", dr["AgencyName"].ToString());
                                    command.Parameters.AddWithValue("@MarketingSeasonId", dr["MarketingSeasonId"].ToString());
                                    command.Parameters.AddWithValue("@MarketingSeason", dr["MarketingSeason"].ToString());
                                    command.Parameters.AddWithValue("@CropYear", dr["CropYear"].ToString());

                                    command.Parameters.AddWithValue("@OpeningStockOfGunny", dr["OpeningStockOfGunny"].ToString());
                                    command.Parameters.AddWithValue("@Password1", dr["Password1"].ToString());
                                    command.Parameters.AddWithValue("@BankName", dr["BankName"].ToString());
                                    command.Parameters.AddWithValue("@AccNO", dr["AccNO"].ToString());
                                    command.Parameters.AddWithValue("@BranchName", dr["BranchName"].ToString());
                                    command.Parameters.AddWithValue("@ManagerName", dr["ManagerName"].ToString());
                                    command.Parameters.AddWithValue("@VersionNo", dr["VersionNo"].ToString());
                                    command.Parameters.AddWithValue("@NoOfToulKanta", dr["NoOfToulKanta"].ToString());
                                    command.Parameters.AddWithValue("@DailySc_Capacity", dr["DailySc_Capacity"].ToString());
                                    command.Parameters.AddWithValue("@UpdationDate", getDate_MDY(dr["UpdationDate"].ToString()));

                                    command.Parameters.AddWithValue("@Societycreditlimit", dr["Societycreditlimit"].ToString());
                                    command.Parameters.AddWithValue("@OneFarmerLimit", dr["OneFarmerLimit"].ToString());
                                    command.Parameters.AddWithValue("@MgrMobileNo", dr["MgrMobileNo"].ToString());
                                    command.Parameters.AddWithValue("@SocBandaranCapacity", dr["SocBandaranCapacity"].ToString());
                                    command.Parameters.AddWithValue("@DateTimeOfInstall", getDate_MDY(dr["DateTimeOfInstall"].ToString()));

                                    command.ExecuteNonQuery();
                                    result = true;
                                }
                                catch (Exception ex)
                                {
                                    //throw ex;
                                }
                                finally
                                {
                                    command.Dispose();
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
        finally { connectionMASTER.Close(); }
        return result;
    }

    [WebMethod]
    public bool InsertNewFarmerDetails(DataSet dsFarmer)
    {
        string FID = "";
        bool result = false;
        //try
        //{
        //    if (connection != null)
        //    {
        //        connection.Open();
        //        foreach (DataRow dr in dsFarmer.Tables[0].Rows)
        //        {
        //            string table = "";
        //            string farmerid = dr["Farmer_Id"].ToString();
        //            string pscoiety = dr["Procured_SocietyID"].ToString();
        //            FID = dr["Farmer_Id"].ToString();
        //            SqlCommand cmd = new SqlCommand();
        //            cmd = new SqlCommand();
        //            cmd.CommandTimeout = 7800;
        //            cmd.CommandType = CommandType.Text;
        //            cmd.CommandText = "Select * from FarmerRegistration where Farmer_Id='" + farmerid + "' and Procured_SocietyID ='" + pscoiety + "'";
        //            cmd.Connection = connection;
        //            SqlDataReader drc;
        //            drc = cmd.ExecuteReader();
        //            if (drc.Read())
        //            {
        //                drc.Close();
        //            }
        //            else
        //            {
        //                SqlTransaction trans = null;
        //                try
        //                {
        //                    drc.Close();
        //                    string DistrictId = dr["District_Id"].ToString();
        //                    string Village_Id = dr["Village_Id"].ToString();
        //                    string VillageName = chkSha(dr["VillageName"].ToString());
        //                    string Tehsil_Id = dr["Tehsil_Id"].ToString();
        //                    string FarmerName = chkSha(dr["FarmerName"].ToString());

        //                    string FatherHusName = chkSha(dr["FatherHusName"].ToString());
        //                    string Gram_Panchayat = dr["Gram_Panchayat"].ToString();
        //                    string PatwariHalkaNo = dr["PatwariHalkaNo"].ToString();
        //                    string Mobileno = dr["Mobileno"].ToString();
        //                    string Category = dr["Category"].ToString();

        //                    string RinPustikaNo = dr["RinPustikaNo"].ToString();
        //                    string Farmer_EID_UID_No = dr["Farmer_EID_UID_No"].ToString();
        //                    string Farmer_BankName = chkSha(dr["Farmer_BankName"].ToString());
        //                    string Farmer_BankAccountNo = dr["Farmer_BankAccountNo"].ToString();
        //                    string PC_ID = dr["PC_ID"].ToString();
        //                    string Procured_SocietyID = dr["Procured_SocietyID"].ToString();

        //                    string Procured_Dist_ID = dr["Procured_Dist_ID"].ToString();
        //                    string Procured_Place = dr["Procured_Place"].ToString();
        //                    float Col_MaxQty = CheckNullFloat(dr["Collecter_MaxQty"].ToString());

        //                    string createdate = dr["CreatedDate"].ToString();
        //                    DateTime cdt = Convert.ToDateTime(createdate);
        //                    string cdate = cdt.ToString("MM/dd/yyyy");

        //                    string CropExpectedDate = dr["CropExpected_Date"].ToString();
        //                    DateTime cExpd = Convert.ToDateTime(CropExpectedDate);
        //                    string cropExpDate = cExpd.ToString("MM/dd/yyyy");

        //                    string registrationdate = dr["registrationdate"].ToString();
        //                    DateTime rdate = Convert.ToDateTime(registrationdate);
        //                    string regdate = rdate.ToString("MM/dd/yyyy");




        //                    command = new SqlCommand();
        //                    trans = connection.BeginTransaction();
        //                    table = "NewFarmerRegistration_Log";
        //                    command.Connection = connection;
        //                    command.CommandType = CommandType.StoredProcedure;
        //                    command.Transaction = trans;
        //                    command.CommandTimeout = 7800;
        //                    command.CommandText = "proc_insertrunner_newfarmer";
        //                    command.Parameters.AddWithValue("@district_id", DistrictId);
        //                    command.Parameters.AddWithValue("@society_id", Procured_SocietyID);
        //                    command.Parameters.AddWithValue("@farmer_id", farmerid);
        //                    command.Parameters.AddWithValue("@pc_id", PC_ID);
        //                    command.Parameters.AddWithValue("@createddate", cdate);
        //                    command.Parameters.AddWithValue("@createdby", "offline");
        //                    command.Parameters.AddWithValue("@status", dr["status"].ToString());
        //                    command.ExecuteNonQuery();

        //                    string nfarmer = "Insert into FarmerRegistration  (District_Id,Village_Id,VillageName ,Tehsil_Id ,Farmer_Id ,FarmerName ,FatherHusName ,Gram_Panchayat ,PatwariHalkaNo, Mobileno,Category,RinPustikaNo,Farmer_EID_UID_No,Farmer_BankAccountNo,Procured_SocietyID,Procured_Dist_ID ,Procured_Place ,Collecter_MaxQty,CropExpected_Date,UserID,Status,CreatedDate,ip,RegistrationDate,Farmer_BankName_New,Farmer_BankBranchName,updatedDate)   values  (@District_Id,@Village_Id,@VillageName ,@Tehsil_Id ,@Farmer_Id ,@FarmerName ,@FatherHusName ,@Gram_Panchayat ,@PatwariHalkaNo,@Mobileno,@Category,@RinPustikaNo,@Farmer_EID_UID_No,@Farmer_BankAccountNo,@Procured_SocietyID,@Procured_Dist_ID ,@Procured_Place ,@Collecter_MaxQty,@CropExpected_Date,@UserID,@Status,@CreatedDate,@ip,@RegistrationDate,@Farmer_BankName_New,@Farmer_BankBranchName,getdate())";
        //                    command.Dispose();
        //                    command = new SqlCommand();
        //                    command.Connection = connection;
        //                    table = "";
        //                    table = "FarmerRegistration";
        //                    command.Transaction = trans;
        //                    command.CommandTimeout = 7800;
        //                    //command.CommandType = CommandType.StoredProcedure;
        //                    //command.CommandText = "proc_insertfarmernew";
        //                    command.CommandType = CommandType.Text;
        //                    command.CommandText = nfarmer;
        //                    command.Parameters.AddWithValue("@district_id", dr["district_id"].ToString());
        //                    command.Parameters.AddWithValue("@village_id", dr["village_id"].ToString());
        //                    command.Parameters.AddWithValue("@villagename", chkSha(dr["villagename"].ToString()));
        //                    command.Parameters.AddWithValue("@tehsil_id", dr["tehsil_id"].ToString());
        //                    command.Parameters.AddWithValue("@farmer_id", dr["farmer_id"].ToString());
        //                    command.Parameters.AddWithValue("@farmername", chkSha(dr["farmername"].ToString()));
        //                    command.Parameters.AddWithValue("@fatherhusname", chkSha(dr["fatherhusname"].ToString()));
        //                    command.Parameters.AddWithValue("@gram_panchayat", dr["gram_panchayat"].ToString());
        //                    command.Parameters.AddWithValue("@patwarihalkano", dr["patwarihalkano"].ToString());
        //                    command.Parameters.AddWithValue("@mobileno", dr["mobileno"].ToString());
        //                    command.Parameters.AddWithValue("@category", dr["category"].ToString());
        //                    command.Parameters.AddWithValue("@rinpustikano", dr["rinpustikano"].ToString());
        //                    command.Parameters.AddWithValue("@farmer_eid_uid_no", dr["farmer_eid_uid_no"].ToString());
        //                    command.Parameters.AddWithValue("@farmer_bankaccountno", dr["farmer_bankaccountno"].ToString());
        //                    command.Parameters.AddWithValue("@procured_societyid", dr["procured_societyid"].ToString());
        //                    command.Parameters.AddWithValue("@procured_dist_id", dr["procured_dist_id"].ToString());
        //                    command.Parameters.AddWithValue("@procured_place", dr["procured_place"].ToString());
        //                    command.Parameters.AddWithValue("@collecter_maxqty", dr["collecter_maxqty"].ToString());
        //                    command.Parameters.AddWithValue("@CropExpected_Date", cropExpDate);
        //                    command.Parameters.AddWithValue("@userid", dr["userid"].ToString());
        //                    command.Parameters.AddWithValue("@status", dr["status"].ToString());
        //                    command.Parameters.AddWithValue("@ip", dr["ip"].ToString());
        //                    command.Parameters.AddWithValue("@createddate", cdate);
        //                    command.Parameters.AddWithValue("@registrationdate", rdate);
        //                    command.Parameters.AddWithValue("@farmer_bankname_new", chkSha(dr["farmer_bankname_new"].ToString()));
        //                    command.Parameters.AddWithValue("@farmer_bankbranchname", chkSha(dr["farmer_bankbranchname"].ToString()));
        //                    command.ExecuteNonQuery();
        //                    result = true;
        //                    trans.Commit();
        //                }
        //                catch (Exception Ex)
        //                {
        //                    trans.Rollback();
        //                    command.Dispose();
        //                    command = new SqlCommand();
        //                    command.Connection = connection;
        //                    command.CommandTimeout = 7800;
        //                    command.CommandType = CommandType.Text;
        //                    command.CommandText = "INSERT INTO ExceptionHandling ([TableName],[Exmsg],[Date],[Farmer_Id]) VALUES (@TableName,@Exmsg,@Date,@Farmer_Id)";
        //                    command.Parameters.AddWithValue("@TableName", table.ToString());
        //                    command.Parameters.AddWithValue("@Exmsg", Ex.ToString());
        //                    command.Parameters.AddWithValue("@Date", System.DateTime.Now.ToString("MM/dd/yyyy"));
        //                    command.Parameters.AddWithValue("@Farmer_Id", dr["farmer_id"].ToString());
        //                    command.ExecuteNonQuery();
        //                }
        //                finally { }
        //            }
        //        }
        //    }
        //}
        //catch (Exception Ex)
        //{

        //    SqlCommand command1 = new SqlCommand();
        //    command1.Connection = connection;
        //    command1.CommandTimeout = 7800;
        //    command1.CommandType = CommandType.Text;
        //    command1.CommandText = "INSERT INTO ExceptionHandling ([TableName],[Exmsg],[Date],[Farmer_Id]) VALUES (@TableName,@Exmsg,@Date,@Farmer_Id)";
        //    command1.Parameters.AddWithValue("@TableName", "While searching");
        //    command1.Parameters.AddWithValue("@Exmsg", Ex.ToString());
        //    command1.Parameters.AddWithValue("@Date", System.DateTime.Now.ToString("MM/dd/yyyy"));
        //    command1.Parameters.AddWithValue("@Farmer_Id", FID);
        //    command1.ExecuteNonQuery();
        //}
        //finally { connection.Close(); }
        return result;
    }

    [WebMethod]
    public bool InsertNewFarmer_LandRecordDescription(DataSet dsFarmer)
    {
        string farmerid = "";
        bool result = false;
        //try
        //{
        //    if (connection != null)
        //    {


        //        connection.Open();

        //        try
        //        {
        //            string finalfarmerids = "";
        //            foreach (DataRow dr in dsFarmer.Tables[0].Rows)
        //            {

        //                farmerid = dr["Farmer_Id"].ToString();

        //                finalfarmerids = finalfarmerids + "'" + farmerid + "'" + ",";

        //            }
        //            if (dsFarmer.Tables[0].Rows.Count != 0)
        //            {
        //                int fid = finalfarmerids.LastIndexOf(",");
        //                string ff = finalfarmerids.Remove(fid);
        //                command = new SqlCommand();
        //                command.CommandTimeout = 7800;
        //                string del = "delete from Farmer_LandRecordDescription where Farmer_Id in (" + ff + ")";
        //                command.CommandType = CommandType.Text;
        //                command.CommandText = del;
        //                command.Connection = connection;
        //                command.ExecuteNonQuery();
        //                //command.Dispose();
        //                //command = new SqlCommand();
        //                //string delrec = "delete from Farmer_LandRecordDescription_Log where Farmer_Id in (" + ff + ")";
        //                //command.CommandType = CommandType.Text;
        //                //command.CommandTimeout = 4800;
        //                //command.CommandText = delrec;
        //                //command.Connection = connection;
        //                //command.ExecuteNonQuery();
        //            }
        //            result = true;
        //        }

        //        catch (Exception ex)
        //        {
        //        }
        //        finally
        //        {
        //            connection.Close();
        //        }




        //        connection.Open();
        //        try
        //        {
        //            foreach (DataRow dr in dsFarmer.Tables[0].Rows)
        //            {
        //                //SqlDataReader drc;
        //                //drc = cmd.ExecuteReader();
        //                //if (drc.Read())
        //                //{
        //                command.Dispose();
        //                command = new SqlCommand();
        //                command.Connection = connection;
        //                command.CommandTimeout = 7800;
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = "proc_insert_farmer_land_record";
        //                command.Parameters.AddWithValue("@farmer_id", dr["farmer_id"].ToString());
        //                command.Parameters.AddWithValue("@village_id", dr["village_id"].ToString());
        //                command.Parameters.AddWithValue("@villagename", dr["villagename"].ToString());
        //                command.Parameters.AddWithValue("@crop_id", dr["crop_id"].ToString());
        //                command.Parameters.AddWithValue("@landowner_name", dr["landowner_name"].ToString());
        //                command.Parameters.AddWithValue("@landowner_rinpustikano", dr["landowner_rinpustikano"].ToString());
        //                command.Parameters.AddWithValue("@landtype", dr["landtype"].ToString());
        //                command.Parameters.AddWithValue("@rakba", dr["rakba"].ToString());
        //                command.Parameters.AddWithValue("@khasarano", dr["khasarano"].ToString());
        //                command.Parameters.AddWithValue("@rakba_crop_sinchit", dr["rakba_crop_sinchit"].ToString());
        //                command.Parameters.AddWithValue("@rakba_crop_asinchit", dr["rakba_crop_asinchit"].ToString());
        //                command.Parameters.AddWithValue("@rakba_crop_sinchit_qty", dr["rakba_crop_sinchit_qty"].ToString());
        //                command.Parameters.AddWithValue("@rakba_crop_asinchit_qty", dr["rakba_crop_asinchit_qty"].ToString());
        //                command.Parameters.AddWithValue("@procured_qty", dr["procured_qty"].ToString());
        //                command.Parameters.AddWithValue("@crpcode", dr["crpcode"].ToString());
        //                command.ExecuteNonQuery();
        //                result = true;
        //                // }
        //                //else
        //                //{
        //                //    drc.Close();
        //                //}

        //            }
        //        }
        //        catch (Exception Ex)
        //        {

        //            command.Dispose();
        //            command = new SqlCommand();
        //            command.Connection = connection;
        //            command.CommandTimeout = 7800;
        //            command.CommandType = CommandType.Text;
        //            command.CommandText = "INSERT INTO ExceptionHandling ([TableName],[Exmsg],[Date],[Farmer_Id]) VALUES (@TableName,@Exmsg,@Date,@Farmer_Id)";
        //            command.Parameters.AddWithValue("@TableName", "Farmer_LandRecord");
        //            command.Parameters.AddWithValue("@Exmsg", Ex.ToString());
        //            command.Parameters.AddWithValue("@Date", System.DateTime.Now.ToString("MM/dd/yyyy"));
        //            command.Parameters.AddWithValue("@Farmer_Id", farmerid);
        //            command.ExecuteNonQuery();
        //        }
        //        finally
        //        {
        //            connection.Close();
        //        }
        //    }
        //}
        //catch (Exception Ex)
        //{
        //    // connectionMASTER.Close();
        //}
        //finally
        //{
        //    connection.Close();
        //}
        return result;

    }

    [WebMethod]
    public bool InsertUpdateFarmerRegistration(DataSet dsFarmerRegistration)
    {
        DataSet dsdb = new DataSet();
        DataSet dsmdb = new DataSet();
        DataSet dsnewval = new DataSet();
        bool result = false;
        //try
        //{
        //    if (connection != null)
        //    {
        //        connection.Open();
        //        string society = "";
        //        string finalfarmerids = "";
        //        //foreach (DataRow dr in dsFarmerRegistration.Tables[0].Rows)
        //        //{
        //        //    string farmerid = dr["Farmer_Id"].ToString();
        //        //    society = dr["Procured_SocietyID"].ToString();
        //        //    finalfarmerids = finalfarmerids + "'" + farmerid + "'" + ",";
        //        //}
        //        if (dsFarmerRegistration.Tables[0] != null)
        //        {
        //            if (dsFarmerRegistration.Tables[0].Rows.Count != 0)
        //            {
        //                society = dsFarmerRegistration.Tables[0].Rows[0]["Procured_SocietyID"].ToString();
        //                //int fid = finalfarmerids.LastIndexOf(",");
        //                //string ff = finalfarmerids.Remove(fid);
        //                SqlCommand cmd = new SqlCommand();
        //                cmd = new SqlCommand();
        //                cmd.Connection = connection;
        //                cmd = new SqlCommand();
        //                cmd.CommandTimeout = 7800;
        //                cmd.CommandType = CommandType.Text;
        //                cmd.CommandText = "Select farmer_id from FarmerRegistration_Log where Procured_SocietyID='" + society + "'";
        //                SqlDataAdapter da = new SqlDataAdapter(cmd);
        //                cmd.Connection = connection;
        //                da.Fill(dsmdb);
        //            }
        //        }


        //        //dtThirdTable is the table which will hold unmatched rows of dtFirstTable and dtSecondTable
        //        DataTable dtThirdTable = new DataTable();
        //        dtThirdTable.Columns.Add("farmer_id", Type.GetType("System.String"));

        //        for (int j = 0; j < dsFarmerRegistration.Tables[0].Rows.Count; j++)
        //        {
        //            bool matched = false;

        //            for (int i = 0; i < dsmdb.Tables[0].Rows.Count; i++)
        //            {
        //                if (dsFarmerRegistration.Tables[0].Rows[j]["farmer_id"].ToString() == dsmdb.Tables[0].Rows[i]["farmer_id"].ToString())
        //                {
        //                    matched = true;
        //                }
        //            }

        //            if (!matched)
        //            {
        //                DataRow drUnMatchedRow = dtThirdTable.NewRow();
        //                drUnMatchedRow["farmer_id"] = dsFarmerRegistration.Tables[0].Rows[j]["farmer_id"].ToString();
        //                string farmerid = dsFarmerRegistration.Tables[0].Rows[j]["farmer_id"].ToString();
        //                string societyid = dsFarmerRegistration.Tables[0].Rows[j]["Procured_SocietyID"].ToString();
        //                try
        //                {
        //                    connection.Open();
        //                    SqlCommand cmd = new SqlCommand();
        //                    cmd = new SqlCommand();
        //                    cmd.CommandType = CommandType.Text;
        //                    cmd.CommandTimeout = 7800;
        //                    cmd.CommandText = "Select * from FarmerRegistration where Farmer_Id='" + farmerid + "' and Procured_SocietyID='" + societyid + "'";
        //                    DataSet dsnew = new DataSet();
        //                    SqlDataAdapter da1 = new SqlDataAdapter(cmd);
        //                    cmd.Connection = connection;
        //                    da1.Fill(dsnew);
        //                    cmd.Dispose();

        //                    command = new SqlCommand();
        //                    command.Connection = connection;
        //                    command.CommandTimeout = 7800;
        //                    command.CommandType = CommandType.StoredProcedure;
        //                    command.CommandText = "proc_update_farmerregistration";
        //                    //command.Parameters.AddWithValue("@district_id", dsFarmerRegistration.Tables[0].Rows[j]["District_Id"].ToString());
        //                    // command.Parameters.AddWithValue("@village_id", dsFarmerRegistration.Tables[0].Rows[j]["village_id"].ToString());
        //                    command.Parameters.AddWithValue("@villagename", dsFarmerRegistration.Tables[0].Rows[j]["villagename"].ToString());
        //                    // command.Parameters.AddWithValue("@tehsil_id", dsFarmerRegistration.Tables[0].Rows[j]["tehsil_id"].ToString());
        //                    command.Parameters.AddWithValue("@farmer_id", dsFarmerRegistration.Tables[0].Rows[j]["farmer_id"].ToString());
        //                    //  command.Parameters.AddWithValue("@farmername", dsFarmerRegistration.Tables[0].Rows[j]["farmername"].ToString());
        //                    command.Parameters.AddWithValue("@Farmer_BankName", dsnew.Tables[0].Rows[0]["Farmer_BankName"].ToString());
        //                    //  command.Parameters.AddWithValue("@fatherhusname", dsFarmerRegistration.Tables[0].Rows[j]["fatherhusname"].ToString());
        //                    command.Parameters.AddWithValue("@gram_panchayat", dsFarmerRegistration.Tables[0].Rows[j]["gram_panchayat"].ToString());
        //                    command.Parameters.AddWithValue("@patwarihalkano", dsFarmerRegistration.Tables[0].Rows[j]["patwarihalkano"].ToString());
        //                    command.Parameters.AddWithValue("@mobileno", dsFarmerRegistration.Tables[0].Rows[j]["mobileno"].ToString());
        //                    command.Parameters.AddWithValue("@category", dsFarmerRegistration.Tables[0].Rows[j]["category"].ToString());
        //                    command.Parameters.AddWithValue("@rinpustikano", dsFarmerRegistration.Tables[0].Rows[j]["rinpustikano"].ToString());
        //                    command.Parameters.AddWithValue("@farmer_eid_uid_no", dsFarmerRegistration.Tables[0].Rows[j]["farmer_eid_uid_no"].ToString());
        //                    command.Parameters.AddWithValue("@farmer_bankaccountno", dsFarmerRegistration.Tables[0].Rows[j]["farmer_bankaccountno"].ToString());
        //                    command.Parameters.AddWithValue("@procured_societyid", dsFarmerRegistration.Tables[0].Rows[j]["procured_societyid"].ToString());
        //                    command.Parameters.AddWithValue("@procured_dist_id", dsFarmerRegistration.Tables[0].Rows[j]["procured_dist_id"].ToString());
        //                    command.Parameters.AddWithValue("@procured_place", dsFarmerRegistration.Tables[0].Rows[j]["procured_place"].ToString());
        //                    command.Parameters.AddWithValue("@collecter_maxqty", dsFarmerRegistration.Tables[0].Rows[j]["collecter_maxqty"].ToString());
        //                    command.Parameters.AddWithValue("@cropexpected_date", (dsFarmerRegistration.Tables[0].Rows[j]["cropexpected_date"].ToString()));
        //                    command.Parameters.AddWithValue("@userid", dsFarmerRegistration.Tables[0].Rows[j]["userid"].ToString());
        //                    command.Parameters.AddWithValue("@status", dsFarmerRegistration.Tables[0].Rows[j]["status"].ToString());
        //                    command.Parameters.AddWithValue("@pc_id", dsFarmerRegistration.Tables[0].Rows[j]["pc_id"].ToString());
        //                    command.Parameters.AddWithValue("@ip", dsFarmerRegistration.Tables[0].Rows[j]["ip"].ToString());
        //                    command.Parameters.AddWithValue("@createddate", (dsFarmerRegistration.Tables[0].Rows[j]["createddate"].ToString()));
        //                    command.Parameters.AddWithValue("@updateddate", (dsFarmerRegistration.Tables[0].Rows[j]["updateddate"].ToString()));
        //                    command.Parameters.AddWithValue("@registrationdate", (dsFarmerRegistration.Tables[0].Rows[j]["registrationdate"].ToString()));
        //                    command.Parameters.AddWithValue("@farmer_bankname_new", dsFarmerRegistration.Tables[0].Rows[j]["farmer_bankname_new"].ToString());
        //                    command.Parameters.AddWithValue("@Farmer_BankBranchName", dsFarmerRegistration.Tables[0].Rows[j]["Farmer_BankBranchName"].ToString());
        //                    command.ExecuteNonQuery();
        //                    command.Dispose();
        //                    command = new SqlCommand();
        //                    command.Connection = connection;
        //                    command.CommandType = CommandType.StoredProcedure;
        //                    command.CommandTimeout = 7800;
        //                    command.CommandText = "proc_insertfarmerregistration_log";
        //                    command.Parameters.AddWithValue("@district_id", dsnew.Tables[0].Rows[0]["District_Id"].ToString());
        //                    command.Parameters.AddWithValue("@village_id", dsnew.Tables[0].Rows[0]["village_id"].ToString());
        //                    command.Parameters.AddWithValue("@villagename", dsnew.Tables[0].Rows[0]["villagename"].ToString());
        //                    command.Parameters.AddWithValue("@tehsil_id", dsnew.Tables[0].Rows[0]["tehsil_id"].ToString());
        //                    command.Parameters.AddWithValue("@farmer_id", dsnew.Tables[0].Rows[0]["farmer_id"].ToString());
        //                    command.Parameters.AddWithValue("@farmername", dsnew.Tables[0].Rows[0]["farmername"].ToString());
        //                    //command.Parameters.AddWithValue("@Farmer_BankName", dsnew.Tables[0].Rows[0]["Farmer_BankName"].ToString());
        //                    command.Parameters.AddWithValue("@fatherhusname", dsnew.Tables[0].Rows[0]["fatherhusname"].ToString());
        //                    command.Parameters.AddWithValue("@gram_panchayat", dsnew.Tables[0].Rows[0]["gram_panchayat"].ToString());
        //                    command.Parameters.AddWithValue("@patwarihalkano", dsnew.Tables[0].Rows[0]["patwarihalkano"].ToString());
        //                    command.Parameters.AddWithValue("@mobileno", dsnew.Tables[0].Rows[0]["mobileno"].ToString());
        //                    command.Parameters.AddWithValue("@category", dsnew.Tables[0].Rows[0]["category"].ToString());
        //                    command.Parameters.AddWithValue("@rinpustikano", dsnew.Tables[0].Rows[0]["rinpustikano"].ToString());
        //                    command.Parameters.AddWithValue("@farmer_eid_uid_no", dsnew.Tables[0].Rows[0]["farmer_eid_uid_no"].ToString());
        //                    command.Parameters.AddWithValue("@farmer_bankaccountno", dsnew.Tables[0].Rows[0]["farmer_bankaccountno"].ToString());
        //                    command.Parameters.AddWithValue("@procured_societyid", dsnew.Tables[0].Rows[0]["procured_societyid"].ToString());
        //                    command.Parameters.AddWithValue("@procured_dist_id", dsnew.Tables[0].Rows[0]["procured_dist_id"].ToString());
        //                    command.Parameters.AddWithValue("@procured_place", dsnew.Tables[0].Rows[0]["procured_place"].ToString());
        //                    command.Parameters.AddWithValue("@collecter_maxqty", dsnew.Tables[0].Rows[0]["collecter_maxqty"].ToString());
        //                    command.Parameters.AddWithValue("@cropexpected_date", (dsnew.Tables[0].Rows[0]["cropexpected_date"].ToString()));
        //                    command.Parameters.AddWithValue("@userid", dsnew.Tables[0].Rows[0]["userid"].ToString());
        //                    command.Parameters.AddWithValue("@status", dsnew.Tables[0].Rows[0]["status"].ToString());
        //                    command.Parameters.AddWithValue("@pc_id", dsnew.Tables[0].Rows[0]["pc_id"].ToString());
        //                    command.Parameters.AddWithValue("@ip", dsnew.Tables[0].Rows[0]["ip"].ToString());
        //                    command.Parameters.AddWithValue("@createddate", (dsnew.Tables[0].Rows[0]["createddate"].ToString()));
        //                    command.Parameters.AddWithValue("@updateddate", (dsnew.Tables[0].Rows[0]["updateddate"].ToString()));
        //                    command.Parameters.AddWithValue("@registrationdate", (dsnew.Tables[0].Rows[0]["registrationdate"].ToString()));
        //                    command.Parameters.AddWithValue("@farmer_bankname_new", dsnew.Tables[0].Rows[0]["farmer_bankname_new"].ToString());
        //                    command.Parameters.AddWithValue("@Farmer_BankBranchName", dsnew.Tables[0].Rows[0]["Farmer_BankBranchName"].ToString());
        //                    command.ExecuteNonQuery();
        //                    result = true;

        //                }
        //                catch (Exception Ex)
        //                {
        //                    connection.Close();
        //                }
        //                finally
        //                {
        //                    connection.Close();
        //                }
        //            }
        //        }

        //        dsnewval.Tables.Add(dtThirdTable);

        //    }
        //}
        //catch (Exception Ex)
        //{
        //    connection.Close();
        //}
        //finally
        //{
        //    connection.Close();
        //}
        return result;
    }

    [WebMethod]
    public bool InsertSOCIETYUpdateFarmerRegistration(DataSet dsFarmerRegistrationSOCIETY)
    {
        bool result = false;
        //try
        //{
        //    if (connection != null)
        //    {
        //        connection.Open();
        //        foreach (DataRow dr in dsFarmerRegistrationSOCIETY.Tables[0].Rows)
        //        {
        //            string farmerid = dr["Farmer_Id"].ToString();
        //            string society = dr["procured_societyid"].ToString();
        //            string District_Id = dr["District_Id"].ToString();
        //            SqlCommand cmd = new SqlCommand();
        //            cmd = new SqlCommand();
        //            cmd.CommandTimeout = 120000;
        //            cmd.CommandType = CommandType.Text;
        //            cmd.CommandText = "Select * from FarmerRegistration where Farmer_Id='" + farmerid + "'";
        //            DataSet dsnew = new DataSet();
        //            SqlDataAdapter da1 = new SqlDataAdapter(cmd);
        //            cmd.Connection = connection;
        //            da1.Fill(dsnew);
        //            cmd.Dispose();

        //            cmd.Connection = connection;
        //            cmd = new SqlCommand();
        //            cmd.CommandType = CommandType.Text;
        //            cmd.CommandText = "Select * from FarmerRegistrationSocietyChange_Log where Farmer_Id='" + farmerid + "' and procured_societyid='" + society + "' and District_Id ='" + District_Id + "'";
        //            DataSet dsn = new DataSet();
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            cmd.Connection = connection;
        //            cmd.CommandTimeout = 120000;
        //            da.Fill(dsn);
        //            cmd.Dispose();
        //            cmd.Connection = connection;
        //            SqlDataReader drc;
        //            drc = cmd.ExecuteReader();
        //            if (drc.Read())
        //            {
        //                drc.Close();
        //                //command.Dispose();
        //                //command = new SqlCommand();
        //                //command.Connection = connection;
        //                //command.CommandType = CommandType.StoredProcedure;
        //                //command.CommandText = "proc_update_farmerregistration";
        //                //command.CommandTimeout = 120000;
        //                ////command.Parameters.AddWithValue("@district_id", dr["District_Id"].ToString());
        //                ////command.Parameters.AddWithValue("@village_id", dr["village_id"].ToString());
        //                //command.Parameters.AddWithValue("@villagename", dr["villagename"].ToString());
        //                //// command.Parameters.AddWithValue("@tehsil_id", dr["tehsil_id"].ToString());
        //                //command.Parameters.AddWithValue("@farmer_id", dr["farmer_id"].ToString());
        //                //// command.Parameters.AddWithValue("@farmername", dr["farmername"].ToString());
        //                ////  command.Parameters.AddWithValue("@fatherhusname", dr["fatherhusname"].ToString());
        //                //command.Parameters.AddWithValue("@gram_panchayat", dr["gram_panchayat"].ToString());
        //                //command.Parameters.AddWithValue("@patwarihalkano", dr["patwarihalkano"].ToString());
        //                //command.Parameters.AddWithValue("@mobileno", dr["mobileno"].ToString());
        //                //command.Parameters.AddWithValue("@category", dr["category"].ToString());
        //                //command.Parameters.AddWithValue("@rinpustikano", dr["rinpustikano"].ToString());
        //                //command.Parameters.AddWithValue("@farmer_eid_uid_no", dr["farmer_eid_uid_no"].ToString());
        //                //command.Parameters.AddWithValue("@farmer_bankaccountno", dr["farmer_bankaccountno"].ToString());
        //                //command.Parameters.AddWithValue("@procured_societyid", dr["procured_societyid"].ToString());
        //                //command.Parameters.AddWithValue("@procured_dist_id", dr["procured_dist_id"].ToString());
        //                //command.Parameters.AddWithValue("@pc_id", dr["pc_id"].ToString());
        //                //command.Parameters.AddWithValue("@procured_place", dr["procured_place"].ToString());
        //                //command.Parameters.AddWithValue("@collecter_maxqty", dr["collecter_maxqty"].ToString());
        //                //command.Parameters.AddWithValue("@cropexpected_date", (dr["cropexpected_date"].ToString()));
        //                //command.Parameters.AddWithValue("@userid", dr["userid"].ToString());
        //                //command.Parameters.AddWithValue("@status", dr["status"].ToString());
        //                //command.Parameters.AddWithValue("@ip", dr["ip"].ToString());
        //                //command.Parameters.AddWithValue("@createddate", (dr["createddate"].ToString()));
        //                //command.Parameters.AddWithValue("@registrationdate", (dr["registrationdate"].ToString()));
        //                //command.Parameters.AddWithValue("@updateddate", (dr["updateddate"].ToString()));
        //                ////command.Parameters.AddWithValue("@updatedip",nupdatedip );
        //                //command.Parameters.AddWithValue("@farmer_bankname_new", dr["farmer_bankname_new"].ToString());
        //                //command.Parameters.AddWithValue("@farmer_bankbranchname", dr["farmer_bankbranchname"].ToString());
        //                //command.ExecuteNonQuery();

        //                ////command.Dispose();
        //                ////command = new SqlCommand();
        //                ////command.Connection = connection;
        //                ////command.CommandType = CommandType.Text;
        //                ////command.CommandText = "UPDATE [FarmerRegistration_Log] SET [District_Id] = @District_Id,[Village_Id] = @Village_Id,[VillageName] = @VillageName,[Tehsil_Id] = @Tehsil_Id,[Farmer_Id] = @Farmer_Id,[FarmerName] = @FarmerName,[FatherHusName] = @FatherHusName,[Gram_Panchayat] = @Gram_Panchayat,[PatwariHalkaNo] = @PatwariHalkaNo,[Mobileno] = @Mobileno,[Category] = @Category,[RinPustikaNo] = @RinPustikaNo,[Farmer_EID_UID_No] = @Farmer_EID_UID_No,[Farmer_BankName] = @Farmer_BankName,[Farmer_BankAccountNo] = @Farmer_BankAccountNo,[PC_ID] = @PC_ID,[Procured_SocietyID] = @Procured_SocietyID,[Procured_Dist_ID] = @Procured_Dist_ID,[Procured_Place] = @Procured_Place,[Collecter_MaxQty] = @Collecter_MaxQty,[CropExpected_Date] = @CropExpected_Date,[UserID] = @UserID,[Status] = @Status,[CreatedDate] = @CreatedDate,[updatedDate] = @updatedDate,[UpdatedIP] = @UpdatedIP,[RegistrationDate] = @RegistrationDate,[Farmer_Bank_BranchName] = @Farmer_Bank_BranchName,[Farmer_BankName_New] = @Farmer_BankName_New WHERE Farmer_Id=@Farmer_Id";
        //                ////command.Parameters.AddWithValue("@district_id", dsn.Tables[0].Rows[0]["District_Id"].ToString());
        //                ////command.Parameters.AddWithValue("@village_id", dsn.Tables[0].Rows[0]["village_id"].ToString());
        //                ////command.Parameters.AddWithValue("@villagename", dsn.Tables[0].Rows[0]["villagename"].ToString());
        //                ////command.Parameters.AddWithValue("@tehsil_id", dsn.Tables[0].Rows[0]["tehsil_id"].ToString());
        //                ////command.Parameters.AddWithValue("@farmer_id", dsn.Tables[0].Rows[0]["farmer_id"].ToString());
        //                ////command.Parameters.AddWithValue("@farmername", dsn.Tables[0].Rows[0]["farmername"].ToString());
        //                ////command.Parameters.AddWithValue("@Farmer_BankName", dsn.Tables[0].Rows[0]["Farmer_BankName"].ToString());
        //                ////command.Parameters.AddWithValue("@fatherhusname", dsn.Tables[0].Rows[0]["fatherhusname"].ToString());
        //                ////command.Parameters.AddWithValue("@gram_panchayat", dsn.Tables[0].Rows[0]["gram_panchayat"].ToString());
        //                ////command.Parameters.AddWithValue("@patwarihalkano", dsn.Tables[0].Rows[0]["patwarihalkano"].ToString());
        //                ////command.Parameters.AddWithValue("@mobileno", dsn.Tables[0].Rows[0]["mobileno"].ToString());
        //                ////command.Parameters.AddWithValue("@category", dsn.Tables[0].Rows[0]["category"].ToString());
        //                ////command.Parameters.AddWithValue("@rinpustikano", dsn.Tables[0].Rows[0]["rinpustikano"].ToString());
        //                ////command.Parameters.AddWithValue("@farmer_eid_uid_no", dsn.Tables[0].Rows[0]["farmer_eid_uid_no"].ToString());
        //                ////command.Parameters.AddWithValue("@farmer_bankaccountno", dsn.Tables[0].Rows[0]["farmer_bankaccountno"].ToString());
        //                ////command.Parameters.AddWithValue("@procured_societyid", dsn.Tables[0].Rows[0]["procured_societyid"].ToString());
        //                ////command.Parameters.AddWithValue("@procured_dist_id", dsn.Tables[0].Rows[0]["procured_dist_id"].ToString());
        //                ////command.Parameters.AddWithValue("@procured_place", dsn.Tables[0].Rows[0]["procured_place"].ToString());
        //                ////command.Parameters.AddWithValue("@collecter_maxqty", dsn.Tables[0].Rows[0]["collecter_maxqty"].ToString());
        //                ////command.Parameters.AddWithValue("@cropexpected_date", (dsn.Tables[0].Rows[0]["cropexpected_date"].ToString()));
        //                ////command.Parameters.AddWithValue("@userid", dsn.Tables[0].Rows[0]["userid"].ToString());
        //                ////command.Parameters.AddWithValue("@status", dsn.Tables[0].Rows[0]["status"].ToString());
        //                ////command.Parameters.AddWithValue("@pc_id", dsn.Tables[0].Rows[0]["pc_id"].ToString());
        //                ////command.Parameters.AddWithValue("@UpdatedIP", dsn.Tables[0].Rows[0]["UpdatedIP"].ToString());
        //                ////command.Parameters.AddWithValue("@createddate", (dsn.Tables[0].Rows[0]["createddate"].ToString()));
        //                ////command.Parameters.AddWithValue("@updateddate", (dsn.Tables[0].Rows[0]["updateddate"].ToString()));
        //                ////command.Parameters.AddWithValue("@registrationdate", (dsn.Tables[0].Rows[0]["registrationdate"].ToString()));
        //                ////command.Parameters.AddWithValue("@farmer_bankname_new", dsn.Tables[0].Rows[0]["farmer_bankname_new"].ToString());
        //                ////command.Parameters.AddWithValue("@Farmer_Bank_BranchName", dsn.Tables[0].Rows[0]["Farmer_Bank_BranchName"].ToString());
        //                ////command.ExecuteNonQuery();


        //                //command.Dispose();
        //                //command = new SqlCommand();
        //                //command.Connection = connection;
        //                //command.CommandType = CommandType.Text;
        //                //command.CommandTimeout = 120000;
        //                //command.CommandText = "UPDATE [FarmerRegistrationSocietyChange_Log] SET [VillageName] = @VillageName,[Farmer_Id] = @Farmer_Id,[Gram_Panchayat] = @Gram_Panchayat,[PatwariHalkaNo] = @PatwariHalkaNo,[Mobileno] = @Mobileno,[Category] = @Category,[RinPustikaNo] = @RinPustikaNo,[Farmer_EID_UID_No] = @Farmer_EID_UID_No,[Farmer_BankName] = @Farmer_BankName,[Farmer_BankAccountNo] = @Farmer_BankAccountNo,[PC_ID] = @PC_ID,[Procured_SocietyID] = @Procured_SocietyID,[Procured_Dist_ID] = @Procured_Dist_ID,[Procured_Place] = @Procured_Place,[Collecter_MaxQty] = @Collecter_MaxQty,[CropExpected_Date] = @CropExpected_Date,[UserID] = @UserID,[Status] = @Status,[CreatedDate] = @CreatedDate,[updatedDate] = @updatedDate,[RegistrationDate] = @RegistrationDate,[farmer_bankbranchname] = @farmer_bankbranchname,[Farmer_BankName_New] = @Farmer_BankName_New WHERE Farmer_Id=@Farmer_Id and [Procured_SocietyID] = @Procured_SocietyID";
        //                ////command.Parameters.AddWithValue("@district_id", dsn.Tables[0].Rows[0]["District_Id"].ToString());
        //                ////command.Parameters.AddWithValue("@village_id", dsn.Tables[0].Rows[0]["village_id"].ToString());
        //                //command.Parameters.AddWithValue("@villagename", dsn.Tables[0].Rows[0]["villagename"].ToString());
        //                ////command.Parameters.AddWithValue("@tehsil_id", dsn.Tables[0].Rows[0]["tehsil_id"].ToString());
        //                //command.Parameters.AddWithValue("@farmer_id", dsn.Tables[0].Rows[0]["farmer_id"].ToString());
        //                ////command.Parameters.AddWithValue("@farmername", dsn.Tables[0].Rows[0]["farmername"].ToString());
        //                //command.Parameters.AddWithValue("@Farmer_BankName", dsn.Tables[0].Rows[0]["Farmer_BankName"].ToString());
        //                //// command.Parameters.AddWithValue("@fatherhusname", dsn.Tables[0].Rows[0]["fatherhusname"].ToString());
        //                //command.Parameters.AddWithValue("@gram_panchayat", dsn.Tables[0].Rows[0]["gram_panchayat"].ToString());
        //                //command.Parameters.AddWithValue("@patwarihalkano", dsn.Tables[0].Rows[0]["patwarihalkano"].ToString());
        //                //command.Parameters.AddWithValue("@mobileno", dsn.Tables[0].Rows[0]["mobileno"].ToString());
        //                //command.Parameters.AddWithValue("@category", dsn.Tables[0].Rows[0]["category"].ToString());
        //                //command.Parameters.AddWithValue("@rinpustikano", dsn.Tables[0].Rows[0]["rinpustikano"].ToString());
        //                //command.Parameters.AddWithValue("@farmer_eid_uid_no", dsn.Tables[0].Rows[0]["farmer_eid_uid_no"].ToString());
        //                //command.Parameters.AddWithValue("@farmer_bankaccountno", dsn.Tables[0].Rows[0]["farmer_bankaccountno"].ToString());
        //                //command.Parameters.AddWithValue("@procured_societyid", dsn.Tables[0].Rows[0]["procured_societyid"].ToString());
        //                //command.Parameters.AddWithValue("@procured_dist_id", dsn.Tables[0].Rows[0]["procured_dist_id"].ToString());
        //                //command.Parameters.AddWithValue("@procured_place", dsn.Tables[0].Rows[0]["procured_place"].ToString());
        //                //command.Parameters.AddWithValue("@collecter_maxqty", dsn.Tables[0].Rows[0]["collecter_maxqty"].ToString());
        //                //command.Parameters.AddWithValue("@cropexpected_date", (dsn.Tables[0].Rows[0]["cropexpected_date"].ToString()));
        //                //command.Parameters.AddWithValue("@userid", dsn.Tables[0].Rows[0]["userid"].ToString());
        //                //command.Parameters.AddWithValue("@status", dsn.Tables[0].Rows[0]["status"].ToString());
        //                //command.Parameters.AddWithValue("@pc_id", dsn.Tables[0].Rows[0]["pc_id"].ToString());
        //                ////command.Parameters.AddWithValue("@UpdatedIP", dsn.Tables[0].Rows[0]["UpdatedIP"].ToString());
        //                //command.Parameters.AddWithValue("@createddate", (dsn.Tables[0].Rows[0]["createddate"].ToString()));
        //                //command.Parameters.AddWithValue("@updateddate", (dsn.Tables[0].Rows[0]["updateddate"].ToString()));
        //                //command.Parameters.AddWithValue("@registrationdate", (dsn.Tables[0].Rows[0]["registrationdate"].ToString()));
        //                //command.Parameters.AddWithValue("@farmer_bankname_new", dsn.Tables[0].Rows[0]["farmer_bankname_new"].ToString());
        //                //command.Parameters.AddWithValue("@farmer_bankbranchname", dsn.Tables[0].Rows[0]["farmer_bankbranchname"].ToString());
        //                //command.ExecuteNonQuery();
        //                //result = true;
        //            }
        //            else
        //            {
        //                drc.Close();
        //                //command.Dispose();
        //                command = new SqlCommand();
        //                command.Connection = connection;
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = "proc_update_farmerregistration";
        //                command.CommandTimeout = 120000;
        //                //command.Parameters.AddWithValue("@district_id", dr["District_Id"].ToString());
        //                //command.Parameters.AddWithValue("@village_id", dr["village_id"].ToString());
        //                command.Parameters.AddWithValue("@villagename", dr["villagename"].ToString());
        //                // command.Parameters.AddWithValue("@tehsil_id", dr["tehsil_id"].ToString());
        //                command.Parameters.AddWithValue("@farmer_id", dr["farmer_id"].ToString());
        //                // command.Parameters.AddWithValue("@farmername", dr["farmername"].ToString());
        //                // command.Parameters.AddWithValue("@fatherhusname", dr["fatherhusname"].ToString());
        //                command.Parameters.AddWithValue("@gram_panchayat", dr["gram_panchayat"].ToString());
        //                command.Parameters.AddWithValue("@patwarihalkano", dr["patwarihalkano"].ToString());
        //                command.Parameters.AddWithValue("@mobileno", dr["mobileno"].ToString());
        //                command.Parameters.AddWithValue("@category", dr["category"].ToString());
        //                command.Parameters.AddWithValue("@rinpustikano", dr["rinpustikano"].ToString());
        //                command.Parameters.AddWithValue("@farmer_eid_uid_no", dr["farmer_eid_uid_no"].ToString());
        //                command.Parameters.AddWithValue("@farmer_bankaccountno", dr["farmer_bankaccountno"].ToString());
        //                command.Parameters.AddWithValue("@procured_societyid", dr["procured_societyid"].ToString());
        //                command.Parameters.AddWithValue("@procured_dist_id", dr["procured_dist_id"].ToString());
        //                command.Parameters.AddWithValue("@pc_id", dr["pc_id"].ToString());
        //                command.Parameters.AddWithValue("@procured_place", dr["procured_place"].ToString());
        //                command.Parameters.AddWithValue("@collecter_maxqty", dr["collecter_maxqty"].ToString());
        //                command.Parameters.AddWithValue("@cropexpected_date", (dr["cropexpected_date"].ToString()));
        //                command.Parameters.AddWithValue("@userid", dr["userid"].ToString());
        //                command.Parameters.AddWithValue("@status", dr["status"].ToString());
        //                command.Parameters.AddWithValue("@ip", dr["ip"].ToString());
        //                command.Parameters.AddWithValue("@createddate", (dr["createddate"].ToString()));
        //                command.Parameters.AddWithValue("@registrationdate", (dr["registrationdate"].ToString()));
        //                command.Parameters.AddWithValue("@updateddate", (dr["updateddate"].ToString()));
        //                //command.Parameters.AddWithValue("@updatedip",nupdatedip );
        //                command.Parameters.AddWithValue("@farmer_bankname_new", dr["farmer_bankname_new"].ToString());
        //                command.Parameters.AddWithValue("@farmer_bankbranchname", dr["farmer_bankbranchname"].ToString());
        //                command.ExecuteNonQuery();

        //                //command.Dispose();
        //                //command = new SqlCommand();
        //                //command.Connection = connection;
        //                //command.CommandType = CommandType.StoredProcedure;
        //                //command.CommandText = "proc_insertfarmerregistration_log";
        //                //command.Parameters.AddWithValue("@district_id", dr["District_Id"].ToString());
        //                //command.Parameters.AddWithValue("@village_id", dr["village_id"].ToString());
        //                //command.Parameters.AddWithValue("@villagename", dr["villagename"].ToString());
        //                //command.Parameters.AddWithValue("@tehsil_id", dr["tehsil_id"].ToString());
        //                //command.Parameters.AddWithValue("@farmer_id", dr["farmer_id"].ToString());
        //                //command.Parameters.AddWithValue("@farmername", dr["farmername"].ToString());
        //                //command.Parameters.AddWithValue("@fatherhusname", dr["fatherhusname"].ToString());
        //                //command.Parameters.AddWithValue("@gram_panchayat", dr["gram_panchayat"].ToString());
        //                //command.Parameters.AddWithValue("@patwarihalkano", dr["patwarihalkano"].ToString());
        //                //command.Parameters.AddWithValue("@mobileno", dr["mobileno"].ToString());
        //                //command.Parameters.AddWithValue("@category", dr["category"].ToString());
        //                //command.Parameters.AddWithValue("@rinpustikano", dr["rinpustikano"].ToString());
        //                //command.Parameters.AddWithValue("@farmer_eid_uid_no", dr["farmer_eid_uid_no"].ToString());
        //                //command.Parameters.AddWithValue("@farmer_bankaccountno", dr["farmer_bankaccountno"].ToString());
        //                //command.Parameters.AddWithValue("@procured_societyid", dr["procured_societyid"].ToString());
        //                //command.Parameters.AddWithValue("@procured_dist_id", dr["procured_dist_id"].ToString());
        //                //command.Parameters.AddWithValue("@pc_id", dr["pc_id"].ToString());
        //                //command.Parameters.AddWithValue("@procured_place", dr["procured_place"].ToString());
        //                //command.Parameters.AddWithValue("@collecter_maxqty", dr["collecter_maxqty"].ToString());
        //                //command.Parameters.AddWithValue("@cropexpected_date", (dr["cropexpected_date"].ToString()));
        //                //command.Parameters.AddWithValue("@userid", dr["userid"].ToString());
        //                //command.Parameters.AddWithValue("@status", dr["status"].ToString());
        //                //command.Parameters.AddWithValue("@ip", dr["ip"].ToString());
        //                //command.Parameters.AddWithValue("@createddate", (dr["createddate"].ToString()));
        //                //command.Parameters.AddWithValue("@registrationdate", (dr["registrationdate"].ToString()));
        //                //command.Parameters.AddWithValue("@updateddate", (dr["updateddate"].ToString()));
        //                ////command.Parameters.AddWithValue("@updatedip",nupdatedip ); Proc_InsertFarmerRegistrationSocietyChange_Log
        //                //command.Parameters.AddWithValue("@farmer_bankname_new", dr["farmer_bankname_new"].ToString());
        //                //command.Parameters.AddWithValue("@farmer_bankbranchname", dr["farmer_bankbranchname"].ToString());
        //                //command.ExecuteNonQuery();


        //                command.Dispose();
        //                command = new SqlCommand();
        //                command.Connection = connection;
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandTimeout = 120000;
        //                command.CommandText = "Proc_InsertFarmerRegistrationSocietyChange_Log";
        //                command.Parameters.AddWithValue("@district_id", dr["District_Id"].ToString());
        //                command.Parameters.AddWithValue("@village_id", dr["village_id"].ToString());
        //                command.Parameters.AddWithValue("@villagename", dr["villagename"].ToString());
        //                command.Parameters.AddWithValue("@tehsil_id", dr["tehsil_id"].ToString());
        //                command.Parameters.AddWithValue("@farmer_id", dr["farmer_id"].ToString());
        //                command.Parameters.AddWithValue("@farmername", dr["farmername"].ToString());
        //                command.Parameters.AddWithValue("@fatherhusname", dr["fatherhusname"].ToString());
        //                command.Parameters.AddWithValue("@gram_panchayat", dr["gram_panchayat"].ToString());
        //                command.Parameters.AddWithValue("@patwarihalkano", dr["patwarihalkano"].ToString());
        //                command.Parameters.AddWithValue("@mobileno", dr["mobileno"].ToString());
        //                command.Parameters.AddWithValue("@category", dr["category"].ToString());
        //                command.Parameters.AddWithValue("@rinpustikano", dr["rinpustikano"].ToString());
        //                command.Parameters.AddWithValue("@farmer_eid_uid_no", dr["farmer_eid_uid_no"].ToString());
        //                command.Parameters.AddWithValue("@farmer_bankaccountno", dr["farmer_bankaccountno"].ToString());
        //                command.Parameters.AddWithValue("@procured_societyid", dr["procured_societyid"].ToString());
        //                command.Parameters.AddWithValue("@procured_dist_id", dr["procured_dist_id"].ToString());
        //                command.Parameters.AddWithValue("@pc_id", dr["pc_id"].ToString());
        //                command.Parameters.AddWithValue("@procured_place", dr["procured_place"].ToString());
        //                command.Parameters.AddWithValue("@collecter_maxqty", dr["collecter_maxqty"].ToString());
        //                command.Parameters.AddWithValue("@cropexpected_date", (dr["cropexpected_date"].ToString()));
        //                command.Parameters.AddWithValue("@userid", dr["userid"].ToString());
        //                command.Parameters.AddWithValue("@status", dr["status"].ToString());
        //                command.Parameters.AddWithValue("@ip", dr["ip"].ToString());
        //                command.Parameters.AddWithValue("@createddate", (dr["createddate"].ToString()));
        //                command.Parameters.AddWithValue("@registrationdate", (dr["registrationdate"].ToString()));
        //                command.Parameters.AddWithValue("@updateddate", (dr["updateddate"].ToString()));
        //                //command.Parameters.AddWithValue("@updatedip",nupdatedip ); Proc_InsertFarmerRegistrationSocietyChange_Log
        //                command.Parameters.AddWithValue("@farmer_bankname_new", dr["farmer_bankname_new"].ToString());
        //                command.Parameters.AddWithValue("@Farmer_BankBranchName", dr["Farmer_BankBranchName"].ToString());
        //                command.ExecuteNonQuery();
        //                //command.Dispose();
        //                //command = new SqlCommand();
        //                //command.Connection = connection;
        //                //command.CommandType = CommandType.Text;
        //                //command.CommandText = "insert into FarmerRegistrationSocietyChange_Log  select * from FarmerRegistration where Farmer_Id='" + dr["farmer_id"].ToString() + "'";
        //                //command.ExecuteNonQuery();
        //                result = true;
        //            }
        //        }
        //    }
        //}
        //catch (Exception Ex)
        //{
        //    connection.Close();
        //}
        //finally
        //{
        //    connection.Close();
        //}
        return result;
    }

    [WebMethod]
    public bool DeleteFarmerDetails(DataSet dsFarmerDelete)
    {
        bool result = false;
        //try
        //{
        //    if (connection != null)
        //    {
        //        connection.Open();
        //        foreach (DataRow dr in dsFarmerDelete.Tables[0].Rows)
        //        {
        //            string farmerid = dr["Farmer_Id"].ToString();
        //            string District_Id = dr["District_Id"].ToString();
        //            SqlCommand cmd = new SqlCommand();
        //            cmd = new SqlCommand();
        //            cmd.CommandType = CommandType.Text;
        //            cmd.CommandText = "Select * from FarmerRegistration where Farmer_Id='" + farmerid + "'";
        //            cmd.Connection = connection;
        //            SqlDataReader drc;
        //            drc = cmd.ExecuteReader();
        //            if (drc.Read())
        //            {
        //                drc.Close();
        //                command = new SqlCommand();
        //                command.Connection = connection;
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = "Proc_InsertDeletedFarmerNew";
        //                command.CommandTimeout = 5600;
        //                command.Parameters.AddWithValue("@district_id", dr["district_id"].ToString());
        //                command.Parameters.AddWithValue("@village_id", dr["village_id"].ToString());
        //                command.Parameters.AddWithValue("@villagename", dr["villagename"].ToString());
        //                command.Parameters.AddWithValue("@tehsil_id", dr["tehsil_id"].ToString());
        //                command.Parameters.AddWithValue("@farmer_id", dr["farmer_id"].ToString());
        //                command.Parameters.AddWithValue("@farmername", dr["farmername"].ToString());
        //                command.Parameters.AddWithValue("@fatherhusname", dr["fatherhusname"].ToString());
        //                command.Parameters.AddWithValue("@gram_panchayat", dr["gram_panchayat"].ToString());
        //                command.Parameters.AddWithValue("@patwarihalkano", dr["patwarihalkano"].ToString());
        //                command.Parameters.AddWithValue("@mobileno", dr["mobileno"].ToString());
        //                command.Parameters.AddWithValue("@category", dr["category"].ToString());
        //                command.Parameters.AddWithValue("@rinpustikano", dr["rinpustikano"].ToString());
        //                command.Parameters.AddWithValue("@farmer_eid_uid_no", dr["farmer_eid_uid_no"].ToString());
        //                command.Parameters.AddWithValue("@farmer_bankaccountno", dr["farmer_bankaccountno"].ToString());
        //                command.Parameters.AddWithValue("@procured_societyid", dr["procured_societyid"].ToString());
        //                command.Parameters.AddWithValue("@procured_dist_id", dr["procured_dist_id"].ToString());
        //                command.Parameters.AddWithValue("@procured_place", dr["procured_place"].ToString());
        //                command.Parameters.AddWithValue("@collecter_maxqty", dr["collecter_maxqty"].ToString());
        //                command.Parameters.AddWithValue("@CropExpected_Date", (dr["CropExpected_Date"].ToString()));
        //                command.Parameters.AddWithValue("@userid", dr["userid"].ToString());
        //                command.Parameters.AddWithValue("@status", dr["status"].ToString());
        //                command.Parameters.AddWithValue("@ip", dr["ip"].ToString());
        //                command.Parameters.AddWithValue("@createddate", (dr["CreatedDate"].ToString()));
        //                command.Parameters.AddWithValue("@registrationdate", (dr["registrationdate"].ToString()));
        //                command.Parameters.AddWithValue("@farmer_bankname_new", dr["farmer_bankname_new"].ToString());
        //                command.Parameters.AddWithValue("@farmer_bankbranchname", dr["farmer_bankbranchname"].ToString());
        //                command.ExecuteNonQuery();
        //                string del = "delete from FarmerRegistration where Farmer_Id='" + farmerid + "' and District_Id ='" + District_Id + "'";
        //                command.Dispose();
        //                command.CommandType = CommandType.Text;
        //                command.CommandText = del;
        //                command.Connection = connection;
        //                command.ExecuteNonQuery();
        //                result = true;
        //            }
        //            else
        //            {
        //                drc.Close();
        //                //command.Connection = connection;
        //                //command.CommandType = CommandType.StoredProcedure;
        //                //command.CommandText = "proc_insertrunner_newfarmer";
        //                //command.Parameters.AddWithValue("@district_id", DistrictId);
        //                //command.Parameters.AddWithValue("@society_id", Procured_SocietyID);
        //                //command.Parameters.AddWithValue("@farmer_id", farmerid);
        //                //command.Parameters.AddWithValue("@pc_id", PC_ID);
        //                //command.Parameters.AddWithValue("@createddate", getDate_MDY(dr["CreatedDate"].ToString()));
        //                //command.Parameters.AddWithValue("@createdby", (dr["createdby"].ToString()));
        //                //command.Parameters.AddWithValue("@status", dr["status"].ToString());
        //                //command.ExecuteNonQuery();
        //                //command.Dispose();
        //            }
        //        }
        //    }
        //}
        //catch (Exception Ex)
        //{ }
        //finally
        //{
        //    connection.Close();
        //}
        return result;
    }

    [WebMethod]
    public bool DeleteFarmer_LandRecordDescription(DataSet dsFarmer_LandRecordDescriptionDelete)
    {
        bool result = false;
        //try
        //{
        //    if (connection != null)
        //    {
        //        connection.Open();
        //        foreach (DataRow dr in dsFarmer_LandRecordDescriptionDelete.Tables[0].Rows)
        //        {
        //            try
        //            {
        //                string farmerid = dr["Farmer_Id"].ToString();
        //                command = new SqlCommand();
        //                string del = "delete from Farmer_LandRecordDescription where Farmer_Id='" + farmerid + "'";
        //                command.CommandType = CommandType.Text;
        //                command.CommandText = del;
        //                command.Connection = connection;
        //                command.ExecuteNonQuery();
        //                result = true;
        //            }
        //            catch (Exception ex)
        //            { }
        //            finally
        //            {
        //                connection.Close();
        //            }
        //        }
        //        foreach (DataRow dr in dsFarmer_LandRecordDescriptionDelete.Tables[0].Rows)
        //        {
        //            try
        //            {
        //                string farmerid = dr["Farmer_Id"].ToString();
        //                string LandOwner_Name = dr["LandOwner_Name"].ToString();
        //                string KhasaraNo = dr["KhasaraNo"].ToString();
        //                string Rakba = dr["Rakba"].ToString();
        //                SqlCommand cmd = new SqlCommand();
        //                cmd = new SqlCommand();
        //                cmd.CommandType = CommandType.Text;
        //                cmd.CommandText = "SELECT [Farmer_Id],[Village_ID],[VillageName],[Crop_ID],[LandOwner_Name],[LandOwner_RinPustikaNo],[LandType],[KhasaraNo],[Rakba],[Rakba_crop_sinchit],[Rakba_crop_asinchit],[Rakba_crop_sinchit_qty],[Rakba_crop_asinchit_qty],[Procured_qty],[crpcode] FROM [DelFarmer_LandRecordDescription_Log] where [Farmer_Id]=@Farmer_Id  and  [Village_ID]=@Village_ID  and  [VillageName]=@VillageName  and  [Crop_ID]=@Crop_ID  and  [LandOwner_Name]=@LandOwner_Name  and  [LandOwner_RinPustikaNo]=@LandOwner_RinPustikaNo  and  [LandType]=@LandType  and  [KhasaraNo]=@KhasaraNo  and  [Rakba]=@Rakba  and  [Rakba_crop_sinchit]=@Rakba_crop_sinchit  and  [Rakba_crop_asinchit]=@Rakba_crop_asinchit  and  [Rakba_crop_sinchit_qty]=@Rakba_crop_sinchit_qty  and  [Rakba_crop_asinchit_qty]=@Rakba_crop_asinchit_qty  and  [Procured_qty]=@Procured_qty  and  [crpcode]=@crpcode";
        //                cmd.Parameters.AddWithValue("@farmer_id", dr["farmer_id"].ToString());
        //                cmd.Parameters.AddWithValue("@village_id", dr["village_id"].ToString());
        //                cmd.Parameters.AddWithValue("@villagename", dr["villagename"].ToString());
        //                cmd.Parameters.AddWithValue("@crop_id", dr["crop_id"].ToString());
        //                cmd.Parameters.AddWithValue("@landowner_name", dr["landowner_name"].ToString());
        //                cmd.Parameters.AddWithValue("@landowner_rinpustikano", dr["landowner_rinpustikano"].ToString());
        //                cmd.Parameters.AddWithValue("@landtype", dr["landtype"].ToString());
        //                cmd.Parameters.AddWithValue("@rakba", dr["rakba"].ToString());
        //                cmd.Parameters.AddWithValue("@khasarano", dr["khasarano"].ToString());
        //                cmd.Parameters.AddWithValue("@rakba_crop_sinchit", dr["rakba_crop_sinchit"].ToString());
        //                cmd.Parameters.AddWithValue("@rakba_crop_asinchit", dr["rakba_crop_asinchit"].ToString());
        //                cmd.Parameters.AddWithValue("@rakba_crop_sinchit_qty", dr["rakba_crop_sinchit_qty"].ToString());
        //                cmd.Parameters.AddWithValue("@rakba_crop_asinchit_qty", dr["rakba_crop_asinchit_qty"].ToString());
        //                cmd.Parameters.AddWithValue("@procured_qty", dr["procured_qty"].ToString());
        //                cmd.Parameters.AddWithValue("@crpcode", dr["crpcode"].ToString());
        //                cmd.Connection = connection;
        //                DataSet dsl = new DataSet();
        //                SqlDataAdapter dala = new SqlDataAdapter(cmd);
        //                cmd.Connection = connection;
        //                dala.Fill(dsl);
        //                if (dsl.Tables[0].Rows.Count == 0)
        //                {
        //                    //SqlDataReader drc;
        //                    //drc = cmd.ExecuteReader();
        //                    //if (drc.Read())
        //                    //{
        //                    //    drc.Close();
        //                    //}
        //                    //else
        //                    //{
        //                    //drc.Close();
        //                    command = new SqlCommand();
        //                    command.Connection = connection;
        //                    command.CommandType = CommandType.StoredProcedure;
        //                    command.CommandText = "[Proc_DelFarmer_LandRecordDescription_Log]";
        //                    command.Parameters.AddWithValue("@farmer_id", dr["farmer_id"].ToString());
        //                    command.Parameters.AddWithValue("@village_id", dr["village_id"].ToString());
        //                    command.Parameters.AddWithValue("@villagename", dr["villagename"].ToString());
        //                    command.Parameters.AddWithValue("@crop_id", dr["crop_id"].ToString());
        //                    command.Parameters.AddWithValue("@landowner_name", dr["landowner_name"].ToString());
        //                    command.Parameters.AddWithValue("@landowner_rinpustikano", dr["landowner_rinpustikano"].ToString());
        //                    command.Parameters.AddWithValue("@landtype", dr["landtype"].ToString());
        //                    command.Parameters.AddWithValue("@rakba", dr["rakba"].ToString());
        //                    command.Parameters.AddWithValue("@khasarano", dr["khasarano"].ToString());
        //                    command.Parameters.AddWithValue("@rakba_crop_sinchit", dr["rakba_crop_sinchit"].ToString());
        //                    command.Parameters.AddWithValue("@rakba_crop_asinchit", dr["rakba_crop_asinchit"].ToString());
        //                    command.Parameters.AddWithValue("@rakba_crop_sinchit_qty", dr["rakba_crop_sinchit_qty"].ToString());
        //                    command.Parameters.AddWithValue("@rakba_crop_asinchit_qty", dr["rakba_crop_asinchit_qty"].ToString());
        //                    command.Parameters.AddWithValue("@procured_qty", dr["procured_qty"].ToString());
        //                    command.Parameters.AddWithValue("@crpcode", dr["crpcode"].ToString());
        //                    command.ExecuteNonQuery();
        //                    result = true;
        //                }
        //                // }
        //                else
        //                { }
        //            }
        //            catch (Exception ex)
        //            { }
        //            finally
        //            {
        //                connection.Close();
        //            }
        //        }
        //    }

        //}




        //catch (Exception Ex)
        //{ }
        //finally { connection.Close(); }
        return result;
    }

    [WebMethod]
    public bool InsertUpdateFarmer_LandRecordDescription(DataSet dsFarmer_LandRecordDescription)
    {
        bool result = false;
        //try
        //{
        //    if (connection != null)
        //    {


        //        connection.Open();

        //        try
        //        {
        //            string finalfarmerids = "";
        //            foreach (DataRow dr in dsFarmer_LandRecordDescription.Tables[0].Rows)
        //            {

        //                string farmerid = dr["Farmer_Id"].ToString();

        //                finalfarmerids = finalfarmerids + "'" + farmerid + "'" + ",";

        //            }
        //            if (dsFarmer_LandRecordDescription.Tables[0].Rows.Count != 0)
        //            {
        //                int fid = finalfarmerids.LastIndexOf(",");
        //                string ff = finalfarmerids.Remove(fid);
        //                command = new SqlCommand();
        //                command.CommandTimeout = 5600;
        //                string del = "delete from Farmer_LandRecordDescription where Farmer_Id in (" + ff + ")";
        //                command.CommandType = CommandType.Text;
        //                command.CommandText = del;
        //                command.Connection = connection;
        //                command.ExecuteNonQuery();
        //                command.Dispose();
        //                command = new SqlCommand();
        //                string delrec = "delete from Farmer_LandRecordDescription_Log where Farmer_Id in (" + ff + ")";
        //                command.CommandType = CommandType.Text;
        //                command.CommandTimeout = 5600;
        //                command.CommandText = delrec;
        //                command.Connection = connection;
        //                command.ExecuteNonQuery();
        //            }
        //            //command = new SqlCommand();
        //            //string delrec = "delete from Farmer_LandRecordDescription_Log where Farmer_Id='" + farmerid + "'";
        //            //command.CommandType = CommandType.Text;
        //            //command.CommandTimeout = 4800;
        //            //command.CommandText = delrec;
        //            //command.Connection = connection;
        //            //command.ExecuteNonQuery();


        //            //command.Dispose();
        //            //command = new SqlCommand();
        //            //string delrecsoc = "delete from Farmer_LandRecordDescriptionSocietyChange_Log where Farmer_Id='" + farmerid + "'";
        //            //command.CommandType = CommandType.Text;
        //            //command.CommandText = delrecsoc;
        //            //command.Connection = connection;
        //            //command.ExecuteNonQuery();
        //            result = true;
        //        }

        //        catch (Exception ex)
        //        {
        //        }
        //        finally
        //        {
        //            connection.Close();
        //        }




        //        connection.Open();
        //        try
        //        {
        //            foreach (DataRow dr in dsFarmer_LandRecordDescription.Tables[0].Rows)
        //            {


        //                string farmerid = dr["Farmer_Id"].ToString();

        //                command = new SqlCommand();
        //                command.CommandType = CommandType.Text;
        //                command.CommandText = "INSERT INTO [Farmer_LandRecordDescription_Log]([Farmer_Id],[Village_ID],[VillageName],[Crop_ID],[LandOwner_Name],[LandOwner_RinPustikaNo],[LandType],[KhasaraNo],[Rakba],[Rakba_crop_sinchit],[Rakba_crop_asinchit],[Rakba_crop_sinchit_qty],[Rakba_crop_asinchit_qty],[Procured_qty],[crpcode]) VALUES (@Farmer_Id,@Village_ID,@VillageName,@Crop_ID,@LandOwner_Name,@LandOwner_RinPustikaNo,@LandType,@KhasaraNo,@Rakba,@Rakba_crop_sinchit,@Rakba_crop_asinchit,@Rakba_crop_sinchit_qty,@Rakba_crop_asinchit_qty,@Procured_qty,@crpcode)";
        //                command.Connection = connection;
        //                command.CommandTimeout = 5600;
        //                command.Parameters.AddWithValue("@farmer_id", dr["farmer_id"].ToString());
        //                command.Parameters.AddWithValue("@village_id", dr["village_id"].ToString());
        //                command.Parameters.AddWithValue("@villagename", dr["villagename"].ToString());
        //                command.Parameters.AddWithValue("@crop_id", dr["crop_id"].ToString());
        //                command.Parameters.AddWithValue("@landowner_name", dr["landowner_name"].ToString());
        //                command.Parameters.AddWithValue("@landowner_rinpustikano", dr["landowner_rinpustikano"].ToString());
        //                command.Parameters.AddWithValue("@landtype", dr["landtype"].ToString());
        //                command.Parameters.AddWithValue("@rakba", dr["rakba"].ToString());
        //                command.Parameters.AddWithValue("@khasarano", dr["khasarano"].ToString());
        //                command.Parameters.AddWithValue("@rakba_crop_sinchit", dr["rakba_crop_sinchit"].ToString());
        //                command.Parameters.AddWithValue("@rakba_crop_asinchit", dr["rakba_crop_asinchit"].ToString());
        //                command.Parameters.AddWithValue("@rakba_crop_sinchit_qty", dr["rakba_crop_sinchit_qty"].ToString());
        //                command.Parameters.AddWithValue("@rakba_crop_asinchit_qty", dr["rakba_crop_asinchit_qty"].ToString());
        //                command.Parameters.AddWithValue("@procured_qty", dr["procured_qty"].ToString());
        //                command.Parameters.AddWithValue("@crpcode", dr["crpcode"].ToString());
        //                command.ExecuteNonQuery();
        //                //SqlDataReader drc;
        //                //drc = cmd.ExecuteReader();
        //                //if (drc.Read())
        //                //{
        //                command.Dispose();
        //                command = new SqlCommand();
        //                command.Connection = connection;
        //                command.CommandTimeout = 5600;
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = "proc_insert_farmer_land_record";
        //                command.Parameters.AddWithValue("@farmer_id", dr["farmer_id"].ToString());
        //                command.Parameters.AddWithValue("@village_id", dr["village_id"].ToString());
        //                command.Parameters.AddWithValue("@villagename", dr["villagename"].ToString());
        //                command.Parameters.AddWithValue("@crop_id", dr["crop_id"].ToString());
        //                command.Parameters.AddWithValue("@landowner_name", dr["landowner_name"].ToString());
        //                command.Parameters.AddWithValue("@landowner_rinpustikano", dr["landowner_rinpustikano"].ToString());
        //                command.Parameters.AddWithValue("@landtype", dr["landtype"].ToString());
        //                command.Parameters.AddWithValue("@rakba", dr["rakba"].ToString());
        //                command.Parameters.AddWithValue("@khasarano", dr["khasarano"].ToString());
        //                command.Parameters.AddWithValue("@rakba_crop_sinchit", dr["rakba_crop_sinchit"].ToString());
        //                command.Parameters.AddWithValue("@rakba_crop_asinchit", dr["rakba_crop_asinchit"].ToString());
        //                command.Parameters.AddWithValue("@rakba_crop_sinchit_qty", dr["rakba_crop_sinchit_qty"].ToString());
        //                command.Parameters.AddWithValue("@rakba_crop_asinchit_qty", dr["rakba_crop_asinchit_qty"].ToString());
        //                command.Parameters.AddWithValue("@procured_qty", dr["procured_qty"].ToString());
        //                command.Parameters.AddWithValue("@crpcode", dr["crpcode"].ToString());
        //                command.ExecuteNonQuery();
        //                result = true;
        //                // }
        //                //else
        //                //{
        //                //    drc.Close();
        //                //}

        //            }
        //        }
        //        catch (Exception ex)
        //        { }
        //        finally
        //        {
        //            connection.Close();
        //        }
        //    }
        //}
        //catch (Exception Ex)
        //{
        //    connection.Close();
        //}
        //finally
        //{
        //    connection.Close();
        //}
        return result;
    }

    [WebMethod]
    public bool InsertUpdateSOCIETYFarmer_LandRecordDescription(DataSet dsSOCIETYFarmer_LandRecordDescription)
    {
        bool result = false;
        //try
        //{
        //    if (connection != null)
        //    {

        //        string finalfarmerids = "";
        //        connection.Open();
        //        try
        //        {
        //            foreach (DataRow dr in dsSOCIETYFarmer_LandRecordDescription.Tables[0].Rows)
        //            {
        //                string farmerid = dr["Farmer_Id"].ToString();
        //                finalfarmerids = finalfarmerids + "'" + farmerid + "'" + ",";
        //            }
        //            if (dsSOCIETYFarmer_LandRecordDescription.Tables[0].Rows.Count != 0)
        //            {
        //                int fid = finalfarmerids.LastIndexOf(",");
        //                string ff = finalfarmerids.Remove(fid);
        //                command = new SqlCommand();
        //                command.CommandTimeout = 5600;
        //                string del = "delete from Farmer_LandRecordDescription where Farmer_Id in (" + ff + ")";
        //                command.CommandType = CommandType.Text;
        //                command.CommandText = del;
        //                command.Connection = connection;
        //                command.ExecuteNonQuery();
        //                command.Dispose();
        //                command = new SqlCommand();
        //                string delrec = "delete from Farmer_LandRecordDescriptionSocietyChange_Log where Farmer_Id in (" + ff + ")";
        //                command.CommandType = CommandType.Text;
        //                command.CommandTimeout = 5600;
        //                command.CommandText = delrec;
        //                command.Connection = connection;
        //                command.ExecuteNonQuery();
        //            }
        //        }
        //        catch (Exception ex)
        //        { }
        //        finally
        //        {
        //            connection.Close();
        //        }

        //        try
        //        {
        //            connection.Open();
        //            foreach (DataRow dr in dsSOCIETYFarmer_LandRecordDescription.Tables[0].Rows)
        //            {
        //                string farmerid = dr["Farmer_Id"].ToString();

        //                command.Dispose();
        //                command = new SqlCommand();
        //                command.Connection = connection;
        //                command.CommandTimeout = 5600;
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = "Proc_insert_farmer_land_Record";
        //                command.Parameters.AddWithValue("@farmer_id", dr["farmer_id"].ToString());
        //                command.Parameters.AddWithValue("@village_id", dr["village_id"].ToString());
        //                command.Parameters.AddWithValue("@villagename", dr["villagename"].ToString());
        //                command.Parameters.AddWithValue("@crop_id", dr["crop_id"].ToString());
        //                command.Parameters.AddWithValue("@landowner_name", dr["landowner_name"].ToString());
        //                command.Parameters.AddWithValue("@landowner_rinpustikano", dr["landowner_rinpustikano"].ToString());
        //                command.Parameters.AddWithValue("@landtype", dr["landtype"].ToString());
        //                command.Parameters.AddWithValue("@rakba", dr["rakba"].ToString());
        //                command.Parameters.AddWithValue("@khasarano", dr["khasarano"].ToString());
        //                command.Parameters.AddWithValue("@rakba_crop_sinchit", dr["rakba_crop_sinchit"].ToString());
        //                command.Parameters.AddWithValue("@rakba_crop_asinchit", dr["rakba_crop_asinchit"].ToString());
        //                command.Parameters.AddWithValue("@rakba_crop_sinchit_qty", dr["rakba_crop_sinchit_qty"].ToString());
        //                command.Parameters.AddWithValue("@rakba_crop_asinchit_qty", dr["rakba_crop_asinchit_qty"].ToString());
        //                command.Parameters.AddWithValue("@procured_qty", dr["procured_qty"].ToString());
        //                command.Parameters.AddWithValue("@crpcode", dr["crpcode"].ToString());
        //                command.ExecuteNonQuery();


        //                command.Dispose();
        //                command = new SqlCommand();
        //                command.Connection = connection;
        //                command.CommandTimeout = 5600;
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = "Proc_insert_farmer_land_Record_SOCIETY";
        //                command.Parameters.AddWithValue("@farmer_id", dr["farmer_id"].ToString());
        //                command.Parameters.AddWithValue("@village_id", dr["village_id"].ToString());
        //                command.Parameters.AddWithValue("@villagename", dr["villagename"].ToString());
        //                command.Parameters.AddWithValue("@crop_id", dr["crop_id"].ToString());
        //                command.Parameters.AddWithValue("@landowner_name", dr["landowner_name"].ToString());
        //                command.Parameters.AddWithValue("@landowner_rinpustikano", dr["landowner_rinpustikano"].ToString());
        //                command.Parameters.AddWithValue("@landtype", dr["landtype"].ToString());
        //                command.Parameters.AddWithValue("@rakba", dr["rakba"].ToString());
        //                command.Parameters.AddWithValue("@khasarano", dr["khasarano"].ToString());
        //                command.Parameters.AddWithValue("@rakba_crop_sinchit", dr["rakba_crop_sinchit"].ToString());
        //                command.Parameters.AddWithValue("@rakba_crop_asinchit", dr["rakba_crop_asinchit"].ToString());
        //                command.Parameters.AddWithValue("@rakba_crop_sinchit_qty", dr["rakba_crop_sinchit_qty"].ToString());
        //                command.Parameters.AddWithValue("@rakba_crop_asinchit_qty", dr["rakba_crop_asinchit_qty"].ToString());
        //                command.Parameters.AddWithValue("@procured_qty", dr["procured_qty"].ToString());
        //                command.Parameters.AddWithValue("@crpcode", dr["crpcode"].ToString());
        //                //command.Parameters.AddWithValue("@SocietyID", dr["SocietyID"].ToString());
        //                command.ExecuteNonQuery();
        //                result = true;
        //                // }
        //                //else
        //                //{
        //                //    drc.Close();
        //                //}

        //            }
        //        }
        //        catch (Exception ex)
        //        { }
        //        finally
        //        {
        //            connection.Close();
        //        }
        //    }
        //}
        //catch (Exception Ex)
        //{
        //    connection.Close();
        //}
        //finally
        //{
        //    connection.Close();
        //}
        return result;
    }


    [WebMethod]
    public bool InsertCommodityReceivedFromFarmer(DataSet dsCommodity)
    {
        bool result = false;
        //string receivedid = "";
        //try
        //{
        //    if (connectionMASTER != null)
        //    {
        //        connectionMASTER.Open();
        //        if (dsCommodity != null)
        //        {
        //            if (dsCommodity.Tables[0].Rows.Count > 0)
        //            {
        //                foreach (DataRow dr in dsCommodity.Tables[0].Rows)
        //                {
        //                    receivedid = dr["receivedid"].ToString();
        //                    string SocietyID = dr["SocietyID"].ToString();
        //                    string DistrictId = dr["DistrictId"].ToString();
        //                    SqlCommand cmd = new SqlCommand();
        //                    cmd = new SqlCommand();
        //                    cmd.CommandType = CommandType.Text;
        //                    cmd.CommandText = "select * from commodityreceivedfromfarmer where receivedid='" + receivedid + "' and SocietyID='" + SocietyID + "' and DistrictId='" + DistrictId + "' ";
        //                    cmd.Connection = connectionMASTER;
        //                    cmd.CommandTimeout = 7800;
        //                    SqlDataReader drc;
        //                    drc = cmd.ExecuteReader();
        //                    if (drc.Read())
        //                    {
        //                        cmd.Dispose();
        //                        drc.Close();
        //                    }
        //                    else
        //                    {

        //                        try
        //                        {
        //                            drc.Close();

        //                            string Date_Of_Receipt = dr["Date_Of_Receipt"].ToString();
        //                            DateTime dor = Convert.ToDateTime(Date_Of_Receipt);
        //                            string dateor = dor.ToString("MM/dd/yyyy");

        //                            string Date_Of_Creation = dr["Date_Of_Creation"].ToString();
        //                            DateTime doc = Convert.ToDateTime(Date_Of_Creation);
        //                            string dateoc = doc.ToString("MM/dd/yyyy");

        //                            //string Date_Of_Updation = dr["Date_Of_Updation"].ToString();
        //                            //DateTime dou = Convert.ToDateTime(Date_Of_Updation);
        //                            //string dateofu = dou.ToString("MM/dd/yyyy");
        //                            //command.Dispose();
        //                            command = new SqlCommand();
        //                            command.Connection = connectionMASTER;
        //                            command.CommandTimeout = 7800;
        //                            command.CommandType = CommandType.StoredProcedure;
        //                            command.CommandText = "proc_insertrunnercommodityreceivedfromfarmer";
        //                            command.Parameters.AddWithValue("@ReceivedID", dr["ReceivedID"].ToString());
        //                            command.Parameters.AddWithValue("@DistrictId", dr["DistrictId"].ToString());
        //                            command.Parameters.AddWithValue("@FarmerID", dr["FarmerID"].ToString());
        //                            command.Parameters.AddWithValue("@Proc_AgID", dr["Proc_AgID"].ToString());
        //                            command.Parameters.AddWithValue("@SocietyID", dr["SocietyID"].ToString());
        //                            command.Parameters.AddWithValue("@PCID", dr["PCID"].ToString());
        //                            command.Parameters.AddWithValue("@CropYear", dr["CropYear"].ToString());
        //                            command.Parameters.AddWithValue("@MarketingSeasonId", dr["MarketingSeasonId"].ToString());
        //                            command.Parameters.AddWithValue("@TotaAmountPayableToFarmer", dr["TotaAmountPayableToFarmer"].ToString());
        //                            command.Parameters.AddWithValue("@TaulPatrakNo", dr["TaulPatrakNo"].ToString());
        //                            command.Parameters.AddWithValue("@FarmerLoanFromSc", CheckNullFloat(dr["FarmerLoanFromSc"].ToString()));
        //                            command.Parameters.AddWithValue("@FarmerLoanFromBank", CheckNullFloat(dr["FarmerLoanFromBank"].ToString()));
        //                            command.Parameters.AddWithValue("@AmtAgainstSCCredit", CheckNullFloat(dr["AmtAgainstSCCredit"].ToString()));
        //                            command.Parameters.AddWithValue("@AmtAgainstBankCredit", CheckNullFloat(dr["AmtAgainstBankCredit"].ToString()));
        //                            command.Parameters.AddWithValue("@Irrigation_Loan", CheckNullFloat(dr["Irrigation_Loan"].ToString()));
        //                            command.Parameters.AddWithValue("@AmtAgIrg_Loan", CheckNullFloat(dr["AmtAgIrg_Loan"].ToString()));
        //                            command.Parameters.AddWithValue("@NetAmountPayableToFarmer", CheckNullFloat(dr["NetAmountPayableToFarmer"].ToString()));
        //                            command.Parameters.AddWithValue("@Date_Of_Receipt", dateor);
        //                            command.Parameters.AddWithValue("@Date_Of_Creation", dateoc);
        //                            command.Parameters.AddWithValue("@Date_Of_Updation", (dr["Date_Of_Updation"].ToString()));
        //                            command.Parameters.AddWithValue("@Status", dr["Status"].ToString());
        //                            command.Parameters.AddWithValue("@UserId", dr["UserId"].ToString());
        //                            command.ExecuteNonQuery();
        //                            result = true;
        //                        }
        //                        catch (Exception Ex)
        //                        {
        //                            connectionNEW.Open();
        //                            command.Dispose();
        //                            command = new SqlCommand();
        //                            command.Connection = connectionNEW;
        //                            command.CommandTimeout = 7800;
        //                            command.CommandType = CommandType.Text;
        //                            command.CommandText = "INSERT INTO ExceptionHandling ([TableName],[Exmsg],[Date],[Farmer_Id]) VALUES (@TableName,@Exmsg,getdate(),@Farmer_Id)";
        //                            command.Parameters.AddWithValue("@TableName", "CommodityReceivedFromFarmer");
        //                            command.Parameters.AddWithValue("@Exmsg", Ex.ToString());
        //                            //command.Parameters.AddWithValue("@Date", System.DateTime.Now.ToString("MM/dd/yyyy"));
        //                            command.Parameters.AddWithValue("@Farmer_Id", receivedid);
        //                            command.ExecuteNonQuery();
        //                            connectionNEW.Close();
        //                        }
        //                        finally { }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        //catch (Exception Ex)
        //{
        //    connectionNEW.Open();
        //    command.Dispose();
        //    command = new SqlCommand();
        //    command.Connection = connectionNEW;
        //    command.CommandTimeout = 7800;
        //    command.CommandType = CommandType.Text;
        //    command.CommandText = "INSERT INTO ExceptionHandling ([TableName],[Exmsg],[Date],[Farmer_Id]) VALUES (@TableName,@Exmsg,getdate(),@Farmer_Id)";
        //    command.Parameters.AddWithValue("@TableName", "Commodity Searching");
        //    command.Parameters.AddWithValue("@Exmsg", Ex.ToString());
        //    //command.Parameters.AddWithValue("@Date", System.DateTime.Now.ToString("MM/dd/yyyy"));
        //    command.Parameters.AddWithValue("@Farmer_Id", receivedid);
        //    command.ExecuteNonQuery();
        //    connectionNEW.Close();
        //}
        //finally { connectionMASTER.Close(); }
        return result;
    }

    [WebMethod]
    public bool InsertCommodityReceived_Transaction(DataSet dsCommodityTrans)
    {
        string receivedid = "";
        bool result = false;
        //try
        //{
        //    if (connectionMASTER != null)
        //    {
        //        connectionMASTER.Open();
        //        if (dsCommodityTrans != null)
        //        {
        //            if (dsCommodityTrans.Tables[0].Rows.Count > 0)
        //            {
        //                foreach (DataRow dr in dsCommodityTrans.Tables[0].Rows)
        //                {
        //                    string ReceivedID = dr["ReceivedID"].ToString();
        //                    string SocietyID = dr["SocietyID"].ToString();
        //                    string DistrictId = dr["DistrictId"].ToString();
        //                    SqlCommand cmd = new SqlCommand();
        //                    cmd = new SqlCommand();
        //                    cmd.CommandType = CommandType.Text;
        //                    cmd.CommandText = "select * from commodityreceived_transaction where ReceivedID='" + ReceivedID + "' and SocietyID='" + SocietyID + "' ";
        //                    cmd.Connection = connectionMASTER;
        //                    cmd.CommandTimeout = 7800;
        //                    SqlDataReader drc;
        //                    drc = cmd.ExecuteReader();
        //                    if (drc.Read())
        //                    {
        //                        drc.Close();
        //                    }
        //                    else
        //                    {
        //                        try
        //                        {
        //                            drc.Close();
        //                            //command.Dispose();
        //                            string CreatDate = dr["CreatDate"].ToString();
        //                            DateTime cdate = Convert.ToDateTime(CreatDate);
        //                            string datec = cdate.ToString("MM/dd/yyyy");

        //                            string Date_Of_Receipt = dr["Date_Of_Receipt"].ToString();
        //                            DateTime dor = Convert.ToDateTime(Date_Of_Receipt);
        //                            string dateofr = dor.ToString("MM/dd/yyyy");

        //                            command = new SqlCommand();
        //                            command.Connection = connectionMASTER;
        //                            command.CommandTimeout = 7800;
        //                            command.CommandType = CommandType.StoredProcedure;
        //                            command.CommandText = "proc_insertrunnercommodityreceived_transaction";
        //                            command.Parameters.AddWithValue("@ReceivedID", dr["ReceivedID"].ToString());
        //                            command.Parameters.AddWithValue("@DistrictId", dr["DistrictId"].ToString());
        //                            command.Parameters.AddWithValue("@Proc_AgID", dr["Proc_AgID"].ToString());
        //                            command.Parameters.AddWithValue("@PCID", dr["PCID"].ToString());
        //                            command.Parameters.AddWithValue("@SocietyID", dr["SocietyID"].ToString());
        //                            //command.Parameters.AddWithValue("@CropYear", dr["CropYear"].ToString());
        //                            //command.Parameters.AddWithValue("@MarketingSeasonId", dr["MarketingSeasonId"].ToString());
        //                            command.Parameters.AddWithValue("@FarmerID", dr["FarmerID"].ToString());
        //                            command.Parameters.AddWithValue("@CommodityId", dr["CommodityId"].ToString());

        //                            command.Parameters.AddWithValue("@QtyReceived", CheckNullFloat(dr["QtyReceived"].ToString()));
        //                            command.Parameters.AddWithValue("@Bags", CheckNullInt(dr["Bags"].ToString()));
        //                            command.Parameters.AddWithValue("@TotalAmount", CheckNullFloat(dr["TotalAmount"].ToString()));

        //                            command.Parameters.AddWithValue("@CreatDate", datec);
        //                            command.Parameters.AddWithValue("@Date_Of_Receipt", dateofr);

        //                            command.ExecuteNonQuery();
        //                            result = true;
        //                        }
        //                        catch (Exception Ex)
        //                        {
        //                            connectionNEW.Open();
        //                            command.Dispose();
        //                            command = new SqlCommand();
        //                            command.Connection = connectionNEW;
        //                            command.CommandTimeout = 7800;
        //                            command.CommandType = CommandType.Text;
        //                            command.CommandText = "INSERT INTO ExceptionHandling ([TableName],[Exmsg],[Date],[Farmer_Id]) VALUES (@TableName,@Exmsg,getdate(),@Farmer_Id)";
        //                            command.Parameters.AddWithValue("@TableName", "CommodityReceivedFromTransaction");
        //                            command.Parameters.AddWithValue("@Exmsg", Ex.ToString());
        //                            //command.Parameters.AddWithValue("@Date", System.DateTime.Now.ToString("MM/dd/yyyy"));
        //                            command.Parameters.AddWithValue("@Farmer_Id", receivedid);
        //                            command.ExecuteNonQuery();
        //                            connectionNEW.Close();
        //                        }
        //                        finally { }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        //catch (Exception Ex)
        //{
        //    connectionNEW.Open();
        //    command.Dispose();
        //    command = new SqlCommand();
        //    command.Connection = connectionNEW;
        //    command.CommandTimeout = 7800;
        //    command.CommandType = CommandType.Text;
        //    command.CommandText = "INSERT INTO ExceptionHandling ([TableName],[Exmsg],[Date],[Farmer_Id]) VALUES (@TableName,@Exmsg,getdate(),@Farmer_Id)";
        //    command.Parameters.AddWithValue("@TableName", "SearchTransaction");
        //    command.Parameters.AddWithValue("@Exmsg", Ex.ToString());
        //    //command.Parameters.AddWithValue("@Date", System.DateTime.Now.ToString("MM/dd/yyyy"));
        //    command.Parameters.AddWithValue("@Farmer_Id", receivedid);
        //    command.ExecuteNonQuery();
        //    connectionNEW.Close();
        //}
        //finally { connectionMASTER.Close(); }
        return result;
    }

    [WebMethod]
    public bool InsertGunnyBagsRemaining(DataSet dsGunnyBagsRemaining)
    {
        bool result = false;
        try
        {
            if (connectionMASTER != null)
            {
                connectionMASTER.Open();
                if (dsGunnyBagsRemaining != null)
                {
                    if (dsGunnyBagsRemaining.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsGunnyBagsRemaining.Tables[0].Rows)
                        {
                            // command.Dispose();

                            string commodity_id = dr["commodity_id"].ToString();
                            SqlCommand cmd = new SqlCommand();
                            cmd = new SqlCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "select * from GunnyBagsRemaining where commodity_id='" + commodity_id + "'";
                            cmd.Connection = connectionMASTER;
                            SqlDataReader drc;
                            drc = cmd.ExecuteReader();
                            if (drc.Read())
                            {
                                drc.Close();
                            }
                            else
                            {
                                drc.Close();
                                command = new SqlCommand();
                                command.Connection = connectionMASTER;
                                command.CommandType = CommandType.StoredProcedure;
                                command.CommandText = "proc_insertrunnergunnybagsremaining";
                                command.Parameters.AddWithValue("@commodity_id", dr["commodity_id"].ToString());
                                command.Parameters.AddWithValue("@commodity_name", dr["commodity_name"].ToString());
                                command.Parameters.AddWithValue("@currentstock", dr["currentstock"].ToString());
                                command.Parameters.AddWithValue("@datetimestamp", (dr["datetimestamp"].ToString()));
                                command.Parameters.AddWithValue("@opration", dr["opration"].ToString());
                                command.Parameters.AddWithValue("@transferred", dr["transferred"].ToString());
                                command.ExecuteNonQuery();
                                result = true;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
        finally { connectionMASTER.Close(); }
        return result;
    }

    [WebMethod]
    public bool Insertcurruntstockofcomoodity(DataSet dscurruntstockofcomoodity)
    {
        bool result = false;
        //try
        //{
        //    if (connection != null)
        //    {
        //        connection.Open();
        //        if (dscurruntstockofcomoodity != null)
        //        {
        //            if (dscurruntstockofcomoodity.Tables[0].Rows.Count > 0)
        //            {
        //                foreach (DataRow dr in dscurruntstockofcomoodity.Tables[0].Rows)
        //                {
        //                    // command.Dispose();
        //                    //  if (ds.tables[0].rows.count > 0)
        //                    string commodityid = dr["commodityid"].ToString();
        //                    string commodityname = dr["commodityname"].ToString();
        //                    string createddate = (dr["createddate"].ToString());
        //                    string currentstock = (dr["currentstock"].ToString());
        //                    string districtid = (dr["districtid"].ToString());
        //                    string proc_agid = dr["proc_agid"].ToString();
        //                    string pcid = dr["pcid"].ToString();
        //                    string societyid = dr["societyid"].ToString();
        //                    string cropyear = dr["cropyear"].ToString();
        //                    string marketingseasonid = dr["marketingseasonid"].ToString();
        //                    cmd = new SqlCommand();
        //                    cmd.CommandType = CommandType.Text;
        //                    cmd.CommandText = "select * from curruntstockofcomoodity where commodityid='" + commodityid + "' and pcid='" + pcid + "' and districtid='" + districtid + "'";
        //                    cmd.Connection = connection;
        //                    SqlDataReader drsc;
        //                    drsc = cmd.ExecuteReader();
        //                    if (drsc.Read())
        //                    {//update the stock
        //                        drsc.Close();
        //                        try
        //                        {
        //                            command = new SqlCommand();
        //                            command.Connection = connection;
        //                            command.CommandType = CommandType.StoredProcedure;
        //                            command.CommandText = "proc_updaterunnercurruntstockofcomoodity";
        //                            command.Parameters.AddWithValue("@commodityid", commodityid);
        //                            command.Parameters.AddWithValue("@commodityname", commodityname);
        //                            command.Parameters.AddWithValue("@createddate", createddate);
        //                            command.Parameters.AddWithValue("@currentstock", currentstock);
        //                            command.Parameters.AddWithValue("@districtid", districtid);
        //                            command.Parameters.AddWithValue("@proc_agid", proc_agid);
        //                            command.Parameters.AddWithValue("@pcid", pcid);
        //                            command.Parameters.AddWithValue("@societyid", societyid);
        //                            //command.Parameters.AddWithValue("@cropyear", cropyear);
        //                            // command.Parameters.AddWithValue("@marketingseasonid", marketingseasonid);
        //                            //command.Parameters.AddWithValue("@crtstock", crtstock);
        //                            command.ExecuteNonQuery();
        //                            result = true;
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                        }
        //                        finally
        //                        {
        //                            cmd.Dispose();
        //                        }
        //                    }
        //                    else
        //                    {
        //                        //insert the stock
        //                        drsc.Close();

        //                        try
        //                        {
        //                            command = new SqlCommand();
        //                            command.Connection = connection;
        //                            command.CommandType = CommandType.StoredProcedure;
        //                            command.CommandText = "proc_insertrunnercurruntstockofcomoodity";
        //                            command.Parameters.AddWithValue("@commodityid", commodityid);
        //                            command.Parameters.AddWithValue("@commodityname", commodityname);
        //                            command.Parameters.AddWithValue("@createddate", createddate);
        //                            command.Parameters.AddWithValue("@currentstock", currentstock);
        //                            command.Parameters.AddWithValue("@districtid", districtid);
        //                            command.Parameters.AddWithValue("@proc_agid", proc_agid);
        //                            command.Parameters.AddWithValue("@pcid", pcid);
        //                            command.Parameters.AddWithValue("@societyid", societyid);
        //                            //command.Parameters.AddWithValue("@cropyear", cropyear);
        //                            //command.Parameters.AddWithValue("@marketingseasonid", marketingseasonid);
        //                            //command.Parameters.AddWithValue("@crtstock", crtstock);
        //                            command.ExecuteNonQuery();
        //                            result = true;
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            //throw ex;
        //                        }
        //                        finally
        //                        {
        //                            command.Dispose();
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{

        //}
        //finally { connection.Close(); }
        return result;
    }

    [WebMethod]
    public bool Insertcurruntstockofgunnybags(DataSet dscurruntstockofgunnybags)
    {
        bool result = false;
        //try
        //{
        //    if (connection != null)
        //    {
        //        connection.Open();
        //        if (dscurruntstockofgunnybags != null)
        //        {
        //            if (dscurruntstockofgunnybags.Tables[0].Rows.Count > 0)
        //            {
        //                foreach (DataRow dr in dscurruntstockofgunnybags.Tables[0].Rows)
        //                {
        //                    // command.Dispose();
        //                    //  if (ds.tables[0].rows.count > 0)
        //                    string districtid = dr["districtid"].ToString();
        //                    string proc_agid = dr["proc_agid"].ToString();
        //                    string pcid = dr["pcid"].ToString();
        //                    string societyid = dr["societyid"].ToString();
        //                    string cropyear = dr["cropyear"].ToString();
        //                    string marketingseasonid = dr["marketingseasonid"].ToString();
        //                    string createddate = (dr["createddate"].ToString());
        //                    string prvstock = dr["prvstock"].ToString();
        //                    string crtstock = dr["crtstock"].ToString();
        //                    cmd = new SqlCommand();
        //                    cmd.CommandType = CommandType.Text;
        //                    cmd.CommandText = "select * from curruntstockofgunnybags where pcid='" + pcid + "' and societyid='" + societyid + "' and districtid='" + districtid + "'";
        //                    cmd.Connection = connection;
        //                    SqlDataReader drsc;
        //                    drsc = cmd.ExecuteReader();
        //                    if (drsc.Read())
        //                    {//update the stock
        //                        drsc.Close();
        //                        try
        //                        {
        //                            command = new SqlCommand();
        //                            command.Connection = connection;
        //                            command.CommandType = CommandType.StoredProcedure;
        //                            command.CommandText = "proc_updaterunnercurruntstockofgunnybags";
        //                            command.Parameters.AddWithValue("@pcid", pcid);
        //                            command.Parameters.AddWithValue("@districtid", districtid);
        //                            command.Parameters.AddWithValue("@proc_agid", proc_agid);

        //                            command.Parameters.AddWithValue("@societyid", societyid);
        //                            command.Parameters.AddWithValue("@cropyear", cropyear);
        //                            command.Parameters.AddWithValue("@marketingseasonid", marketingseasonid);
        //                            command.Parameters.AddWithValue("@createddate", createddate);
        //                            command.Parameters.AddWithValue("@prvstock", prvstock);
        //                            command.Parameters.AddWithValue("@crtstock", crtstock);
        //                            command.ExecuteNonQuery();
        //                            result = true;
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                        }
        //                        finally
        //                        {

        //                        }
        //                    }
        //                    else
        //                    {
        //                        //insert the stock
        //                        drsc.Close();

        //                        try
        //                        {
        //                            command = new SqlCommand();
        //                            command.Connection = connection;
        //                            command.CommandType = CommandType.StoredProcedure;
        //                            command.CommandText = "proc_insertrunnercurruntstockofgunnybags";
        //                            command.Parameters.AddWithValue("@pcid", pcid);
        //                            command.Parameters.AddWithValue("@districtid", districtid);
        //                            command.Parameters.AddWithValue("@proc_agid", proc_agid);

        //                            command.Parameters.AddWithValue("@societyid", societyid);
        //                            command.Parameters.AddWithValue("@cropyear", cropyear);
        //                            command.Parameters.AddWithValue("@marketingseasonid", marketingseasonid);
        //                            command.Parameters.AddWithValue("@createddate", createddate);
        //                            command.Parameters.AddWithValue("@prvstock", prvstock);
        //                            command.Parameters.AddWithValue("@crtstock", crtstock);
        //                            //command.Parameters.AddWithValue("@cropyear", cropyear);
        //                            //command.Parameters.AddWithValue("@marketingseasonid", marketingseasonid);
        //                            //command.Parameters.AddWithValue("@crtstock", crtstock);
        //                            command.ExecuteNonQuery();
        //                            result = true;
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            //throw ex;
        //                        }
        //                        finally
        //                        {

        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{

        //}
        //finally { connection.Close(); }
        return result;
    }

    [WebMethod]
    public bool InsertGunnyBagsReceipt(DataSet dsGunnyBagsReceipt)
    {
        bool result = false;
        try
        {
            if (connectionMASTER != null)
            {
                connectionMASTER.Open();
                if (dsGunnyBagsReceipt != null)
                {
                    if (dsGunnyBagsReceipt.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsGunnyBagsReceipt.Tables[0].Rows)
                        {
                            string GReceiptNo = dr["GReceiptNo"].ToString();
                            SqlCommand cmd = new SqlCommand();
                            cmd = new SqlCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "select * from gunnybagsreceipt where GReceiptNo='" + GReceiptNo + "'";
                            cmd.Connection = connectionMASTER;
                            SqlDataReader drc;
                            drc = cmd.ExecuteReader();
                            if (drc.Read())
                            {
                                drc.Close();
                            }
                            else
                            {
                                drc.Close();
                                //command.Dispose();
                                command = new SqlCommand();
                                command.Connection = connectionMASTER;
                                command.CommandType = CommandType.StoredProcedure;
                                command.CommandText = "proc_insertrunnergunnybagsreceipt";
                                command.Parameters.AddWithValue("@GReceiptNo", dr["GReceiptNo"].ToString());
                                command.Parameters.AddWithValue("@District_ID", dr["District_ID"].ToString());
                                command.Parameters.AddWithValue("@SocietyCode", dr["SocietyCode"].ToString());
                                command.Parameters.AddWithValue("@PC_Id", dr["PC_Id"].ToString());

                                command.Parameters.AddWithValue("@GunnyType", dr["GunnyType"].ToString());

                                command.Parameters.AddWithValue("@NoOfBags", CheckNullInt(dr["NoOfBags"].ToString()));
                                command.Parameters.AddWithValue("@PrvOldBags", CheckNullInt(dr["PrvOldBags"].ToString()));
                                command.Parameters.AddWithValue("@TruckChallanNo", dr["TruckChallanNo"].ToString());
                                command.Parameters.AddWithValue("@TruckNo", dr["TruckNo"].ToString());
                                command.Parameters.AddWithValue("@ReceivedFrom", dr["ReceivedFrom"].ToString());
                                command.Parameters.AddWithValue("@DateOfRecv", (dr["DateOfRecv"].ToString()));
                                command.Parameters.AddWithValue("@TruckChallanDate", (dr["TruckChallanDate"].ToString()));
                                command.Parameters.AddWithValue("@userid", dr["userid"].ToString());
                                command.Parameters.AddWithValue("@datetimestamp", (dr["datetimestamp"].ToString()));
                                command.Parameters.AddWithValue("@opration", dr["opration"].ToString());
                                command.Parameters.AddWithValue("@Locked", dr["Locked"].ToString());
                                command.ExecuteNonQuery();
                                result = true;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
        finally { connectionMASTER.Close(); }
        return result;
    }

    [WebMethod]
    public bool InsertGunnyBagsIssueTable(DataSet dsGunnyBagsIssueTable)
    {
        bool result = false;
        try
        {
            if (connectionMASTER != null)
            {
                connectionMASTER.Open();
                if (dsGunnyBagsIssueTable != null)
                {
                    if (dsGunnyBagsIssueTable.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsGunnyBagsIssueTable.Tables[0].Rows)
                        {
                            string IssueNo = dr["IssueNo"].ToString();
                            SqlCommand cmd = new SqlCommand();
                            cmd = new SqlCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "select * from gunnybagsissuetable where IssueNo='" + IssueNo + "'";
                            cmd.Connection = connectionMASTER;
                            SqlDataReader drc;
                            drc = cmd.ExecuteReader();
                            if (drc.Read())
                            {
                                drc.Close();
                            }
                            else
                            {
                                drc.Close();
                                //command.Dispose();
                                command = new SqlCommand();
                                command.Connection = connectionMASTER;
                                command.CommandType = CommandType.StoredProcedure;
                                command.CommandText = "proc_insertrunnergunnybagsissuetable";
                                command.Parameters.AddWithValue("@IssueNo", dr["IssueNo"].ToString());
                                command.Parameters.AddWithValue("@District_ID", dr["District_ID"].ToString());
                                command.Parameters.AddWithValue("@SocietyCode", dr["SocietyCode"].ToString());
                                command.Parameters.AddWithValue("@PC_Id", dr["PC_Id"].ToString());

                                command.Parameters.AddWithValue("@GunnyType", dr["GunnyType"].ToString());
                                command.Parameters.AddWithValue("@typeofbags", dr["typeofbags"].ToString());
                                command.Parameters.AddWithValue("@DateOfIssue", (dr["DateOfIssue"].ToString()));
                                command.Parameters.AddWithValue("@NoOfBags", CheckNullInt(dr["NoOfBags"].ToString()));
                                command.Parameters.AddWithValue("@PrvOldBags", CheckNullInt(dr["PrvOldBags"].ToString()));
                                command.Parameters.AddWithValue("@TruckChallanNo", dr["TruckChallanNo"].ToString());
                                command.Parameters.AddWithValue("@TruckNo", dr["TruckNo"].ToString());
                                command.Parameters.AddWithValue("@IssuedFrom", dr["IssuedFrom"].ToString());

                                command.Parameters.AddWithValue("@TruckChallanDate", (dr["TruckChallanDate"].ToString()));
                                command.Parameters.AddWithValue("@userid", dr["userid"].ToString());
                                command.Parameters.AddWithValue("@datetimestamp", (dr["datetimestamp"].ToString()));
                                command.Parameters.AddWithValue("@opration", dr["opration"].ToString());
                                command.Parameters.AddWithValue("@Locked", dr["Locked"].ToString());
                                command.ExecuteNonQuery();
                                result = true;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
        finally { connectionMASTER.Close(); }
        return result;
    }

    [WebMethod]
    public bool InsertIssueToSangrahanKendra_MP(DataSet dsIssueToSangrahanKendra_MP)
    {
        bool result = false;
        try
        {
            if (connectionMASTER != null)
            {
                connectionMASTER.Open();
                if (dsIssueToSangrahanKendra_MP != null)
                {
                    if (dsIssueToSangrahanKendra_MP.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsIssueToSangrahanKendra_MP.Tables[0].Rows)
                        {
                            string IssueID = dr["IssueID"].ToString();
                            SqlCommand cmd = new SqlCommand();
                            cmd = new SqlCommand();
                            cmd.CommandTimeout = 7800;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "select * from issuetosangrahankendra_mp where IssueID='" + IssueID + "'";
                            cmd.Connection = connectionMASTER;
                            SqlDataReader drc;
                            drc = cmd.ExecuteReader();
                            if (drc.Read())
                            {
                                drc.Close();
                            }
                            else
                            {
                                try
                                {
                                    drc.Close();
                                    //command.Dispose();
                                    command = new SqlCommand();
                                    command.CommandTimeout = 7800;
                                    command.Connection = connectionMASTER;
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.CommandText = "proc_insertrunnerissuetosangrahankendra_mp";
                                    command.Parameters.AddWithValue("@IssueID", dr["IssueID"].ToString());
                                    command.Parameters.AddWithValue("@DistrictId", dr["DistrictId"].ToString());
                                    command.Parameters.AddWithValue("@Proc_AgID", dr["Proc_AgID"].ToString());
                                    command.Parameters.AddWithValue("@SocietyID", dr["SocietyID"].ToString());
                                    command.Parameters.AddWithValue("@PCID", dr["PCID"].ToString());
                                    command.Parameters.AddWithValue("@CropYear", dr["CropYear"].ToString());
                                    command.Parameters.AddWithValue("@MarketingSeasonId", dr["MarketingSeasonId"].ToString());
                                    command.Parameters.AddWithValue("@DateOfIssue", (dr["DateOfIssue"].ToString()));
                                    command.Parameters.AddWithValue("@wheretosend", dr["wheretosend"].ToString());
                                    command.Parameters.AddWithValue("@BardanaUsed", dr["BardanaUsed"].ToString());
                                    command.Parameters.AddWithValue("@TransporterId", dr["TransporterId"].ToString());
                                    command.Parameters.AddWithValue("@TruckChalanNo", dr["TruckChalanNo"].ToString());
                                    command.Parameters.AddWithValue("@DriverName", dr["DriverName"].ToString());
                                    command.Parameters.AddWithValue("@TaulPtrakNo", dr["TaulPtrakNo"].ToString());
                                    command.Parameters.AddWithValue("@CreatedDate", (dr["CreatedDate"].ToString()));
                                    command.Parameters.AddWithValue("@UpdatedDate", (dr["UpdatedDate"].ToString()));
                                    command.Parameters.AddWithValue("@UserId", dr["UserId"].ToString());
                                    command.Parameters.AddWithValue("@TruckNo", dr["TruckNo"].ToString());
                                    command.ExecuteNonQuery();
                                    result = true;
                                }
                                catch (Exception Ex)
                                {
                                    connectionNEW.Open();
                                    command.Dispose();
                                    command = new SqlCommand();
                                    command.Connection = connectionNEW;
                                    command.CommandTimeout = 7800;
                                    command.CommandType = CommandType.Text;
                                    command.CommandText = "INSERT INTO ExceptionHandling ([TableName],[Exmsg],[Date],[Farmer_Id]) VALUES (@TableName,@Exmsg,getdate(),@Farmer_Id)";
                                    command.Parameters.AddWithValue("@TableName", "SangrahanKendra_MP");
                                    command.Parameters.AddWithValue("@Exmsg", Ex.ToString());
                                    //command.Parameters.AddWithValue("@Date", System.DateTime.Now.ToString("MM/dd/yyyy"));
                                    command.Parameters.AddWithValue("@Farmer_Id", dr["IssueID"].ToString());
                                    command.ExecuteNonQuery();
                                    connectionNEW.Close();
                                }
                                finally { }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
        finally { connectionMASTER.Close(); }
        return result;
    }

    [WebMethod]
    public bool InsertIssueByRailRack(DataSet dsIssueByRailRack)
    {
        bool result = false;
        try
        {
            if (connectionMASTER != null)
            {
                connectionMASTER.Open();
                if (dsIssueByRailRack != null)
                {
                    if (dsIssueByRailRack.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsIssueByRailRack.Tables[0].Rows)
                        {
                            string IssueID = dr["IssueID"].ToString();
                            SqlCommand cmd = new SqlCommand();
                            cmd = new SqlCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "select * from issuebyrailrack where IssueID='" + IssueID + "'";
                            cmd.Connection = connectionMASTER;
                            SqlDataReader drc;
                            drc = cmd.ExecuteReader();
                            if (drc.Read())
                            {
                                drc.Close();
                            }
                            else
                            {
                                try
                                {
                                    drc.Close();
                                    //command.Dispose();

                                    command = new SqlCommand();
                                    command.Connection = connectionMASTER;
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.CommandText = "proc_insertrunnerissuebyrailrack";
                                    command.Parameters.AddWithValue("@IssueID", dr["IssueID"].ToString());
                                    command.Parameters.AddWithValue("@DistrictId", dr["DistrictId"].ToString());
                                    command.Parameters.AddWithValue("@Proc_AgID", dr["Proc_AgID"].ToString());
                                    command.Parameters.AddWithValue("@PCID", dr["PCID"].ToString());

                                    command.Parameters.AddWithValue("@SocietyID", dr["SocietyID"].ToString());
                                    command.Parameters.AddWithValue("@CropYear", dr["CropYear"].ToString());
                                    command.Parameters.AddWithValue("@MarketingSeasonId", dr["MarketingSeasonId"].ToString());
                                    command.Parameters.AddWithValue("@RailRackOf", dr["RailRackOf_ID"].ToString());
                                    command.Parameters.AddWithValue("@SendingPlace", dr["SendingPlace"].ToString());
                                    command.Parameters.AddWithValue("@RecievingPlace", dr["RecievingPlace"].ToString());
                                    command.Parameters.AddWithValue("@CommodityId", dr["CommodityId"].ToString());
                                    command.Parameters.AddWithValue("@DateOfIssue", (dr["DateOfIssue"].ToString()));
                                    command.Parameters.AddWithValue("@Bags", CheckNullInt(dr["Bags"].ToString()));
                                    command.Parameters.AddWithValue("@QtyTransffer", CheckNullFloat(dr["QtyTransffer"].ToString()));
                                    command.Parameters.AddWithValue("@CreatedDate", (dr["CreatedDate"].ToString()));
                                    command.Parameters.AddWithValue("@UpdatedDate", (dr["UpdatedDate"].ToString()));
                                    command.ExecuteNonQuery();
                                    result = true;
                                }
                                catch (Exception Ex)
                                {
                                    connectionNEW.Open();
                                    command.Dispose();
                                    command = new SqlCommand();
                                    command.Connection = connectionNEW;
                                    command.CommandTimeout = 7800;
                                    command.CommandType = CommandType.Text;
                                    command.CommandText = "INSERT INTO ExceptionHandling ([TableName],[Exmsg],[Date],[Farmer_Id]) VALUES (@TableName,@Exmsg,getdate(),@Farmer_Id)";
                                    command.Parameters.AddWithValue("@TableName", "issuebyrailrack");
                                    command.Parameters.AddWithValue("@Exmsg", Ex.ToString());
                                    //command.Parameters.AddWithValue("@Date", System.DateTime.Now.ToString("MM/dd/yyyy"));
                                    command.Parameters.AddWithValue("@Farmer_Id", dr["IssueID"].ToString());
                                    command.ExecuteNonQuery();
                                    connectionNEW.Close();
                                }
                                finally { }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
        finally { connectionMASTER.Close(); }
        return result;
    }

    [WebMethod]
    public bool InsertIssueToCapeGodown(DataSet dsIssueToCapeGodown)
    {
        bool result = false;
        try
        {
            if (connectionMASTER != null)
            {
                connectionMASTER.Open();
                if (dsIssueToCapeGodown != null)
                {
                    if (dsIssueToCapeGodown.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsIssueToCapeGodown.Tables[0].Rows)
                        {
                            string IssueID = dr["IssueID"].ToString();
                            SqlCommand cmd = new SqlCommand();
                            cmd = new SqlCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "select * from issuetocapegodown where IssueID='" + IssueID + "'";
                            cmd.Connection = connectionMASTER;
                            SqlDataReader drc;
                            drc = cmd.ExecuteReader();
                            if (drc.Read())
                            {
                                drc.Close();
                            }
                            else
                            {
                                try
                                {
                                    drc.Close();
                                    // command.Dispose();
                                    command = new SqlCommand();
                                    command.Connection = connectionMASTER;
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.CommandText = "proc_insertrunnerissuetocapegodown";
                                    command.Parameters.AddWithValue("@IssueID", dr["IssueID"].ToString());
                                    command.Parameters.AddWithValue("@DistrictId", dr["DistrictId"].ToString());
                                    command.Parameters.AddWithValue("@Proc_AgID", dr["Proc_AgID"].ToString());
                                    command.Parameters.AddWithValue("@PCID", dr["PCID"].ToString());

                                    command.Parameters.AddWithValue("@SocietyID", dr["SocietyID"].ToString());
                                    command.Parameters.AddWithValue("@CropYear", dr["CropYear"].ToString());
                                    command.Parameters.AddWithValue("@MarketingSeasonId", dr["MarketingSeasonId"].ToString());
                                    command.Parameters.AddWithValue("@CapGodownId", dr["CapGodownId"].ToString());
                                    command.Parameters.AddWithValue("@CapGodownCenterID", dr["CapGodownCenterID"].ToString());
                                    command.Parameters.AddWithValue("@Place", dr["Place"].ToString());
                                    command.Parameters.AddWithValue("@CommodityId", dr["CommodityId"].ToString());
                                    command.Parameters.AddWithValue("@Bags", CheckNullInt(dr["Bags"].ToString()));
                                    command.Parameters.AddWithValue("@qtytransffer", CheckNullFloat(dr["QtyTransffer"].ToString()));
                                    command.Parameters.AddWithValue("@DateOfIssue", (dr["DateOfIssue"].ToString()));

                                    command.Parameters.AddWithValue("@CreatedDate", (dr["CreatedDate"].ToString()));
                                    command.Parameters.AddWithValue("@UpdatedDate", (dr["UpdatedDate"].ToString()));
                                    command.ExecuteNonQuery();
                                    result = true;
                                }
                                catch (Exception Ex)
                                {
                                    connectionNEW.Open();
                                    command.Dispose();
                                    command = new SqlCommand();
                                    command.Connection = connectionNEW;
                                    command.CommandTimeout = 7800;
                                    command.CommandType = CommandType.Text;
                                    command.CommandText = "INSERT INTO ExceptionHandling ([TableName],[Exmsg],[Date],[Farmer_Id]) VALUES (@TableName,@Exmsg,getdate(),@Farmer_Id)";
                                    command.Parameters.AddWithValue("@TableName", "issuetocapegodown");
                                    command.Parameters.AddWithValue("@Exmsg", Ex.ToString());
                                    //command.Parameters.AddWithValue("@Date", System.DateTime.Now.ToString("MM/dd/yyyy"));
                                    command.Parameters.AddWithValue("@Farmer_Id", dr["IssueID"].ToString());
                                    command.ExecuteNonQuery();
                                    connectionNEW.Close();
                                }
                                finally { }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
        finally { connectionMASTER.Close(); }
        return result;
    }

    [WebMethod]
    public bool InsertIssueToFCI(DataSet dsIssueToFCI)
    {
        bool result = false;
        try
        {
            if (connectionMASTER != null)
            {
                connectionMASTER.Open();
                if (dsIssueToFCI != null)
                {
                    if (dsIssueToFCI.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsIssueToFCI.Tables[0].Rows)
                        {
                            string IssueID = dr["IssueID"].ToString();
                            SqlCommand cmd = new SqlCommand();
                            cmd = new SqlCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "select * from issuetofci where IssueID='" + IssueID + "'";
                            cmd.Connection = connectionMASTER;
                            SqlDataReader drc;
                            drc = cmd.ExecuteReader();
                            if (drc.Read())
                            {
                                drc.Close();
                            }
                            else
                            {
                                try
                                {
                                    drc.Close();
                                    //command.Dispose();
                                    command = new SqlCommand();
                                    command.Connection = connectionMASTER;
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.CommandText = "proc_insertrunnerissuetofci";
                                    command.Parameters.AddWithValue("@IssueID", dr["IssueID"].ToString());
                                    command.Parameters.AddWithValue("@DistrictId", dr["DistrictId"].ToString());
                                    command.Parameters.AddWithValue("@Proc_AgID", dr["Proc_AgID"].ToString());
                                    command.Parameters.AddWithValue("@PCID", dr["PCID"].ToString());

                                    command.Parameters.AddWithValue("@SocietyID", dr["SocietyID"].ToString());
                                    command.Parameters.AddWithValue("@CropYear", dr["CropYear"].ToString());
                                    command.Parameters.AddWithValue("@MarketingSeasonId", dr["MarketingSeasonId"].ToString());
                                    command.Parameters.AddWithValue("@FCIGodownId", dr["FCIGodownId"].ToString());

                                    command.Parameters.AddWithValue("@Place", dr["Place"].ToString());
                                    command.Parameters.AddWithValue("@CommodityId", dr["CommodityId"].ToString());
                                    command.Parameters.AddWithValue("@Bags", CheckNullInt(dr["Bags"].ToString()));
                                    command.Parameters.AddWithValue("@QtyTransffer", CheckNullFloat(dr["QtyTransffer"].ToString()));
                                    command.Parameters.AddWithValue("@DateOfIssue", (dr["DateOfIssue"].ToString()));

                                    command.Parameters.AddWithValue("@CreatedDate", (dr["CreatedDate"].ToString()));
                                    command.Parameters.AddWithValue("@UpdatedDate", (dr["UpdatedDate"].ToString()));
                                    command.ExecuteNonQuery();
                                    result = true;
                                }
                                catch (Exception Ex)
                                {
                                    connectionNEW.Open();
                                    command.Dispose();
                                    command = new SqlCommand();
                                    command.Connection = connectionNEW;
                                    command.CommandTimeout = 7800;
                                    command.CommandType = CommandType.Text;
                                    command.CommandText = "INSERT INTO ExceptionHandling ([TableName],[Exmsg],[Date],[Farmer_Id]) VALUES (@TableName,@Exmsg,getdate(),@Farmer_Id)";
                                    command.Parameters.AddWithValue("@TableName", "issuetofci");
                                    command.Parameters.AddWithValue("@Exmsg", Ex.ToString());
                                    //command.Parameters.AddWithValue("@Date", System.DateTime.Now.ToString("MM/dd/yyyy"));
                                    command.Parameters.AddWithValue("@Farmer_Id", dr["IssueID"].ToString());
                                    command.ExecuteNonQuery();
                                    connectionNEW.Close();
                                }
                                finally { }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
        finally { connectionMASTER.Close(); }
        return result;
    }

    [WebMethod]
    public bool InsertIssueToInDistrict(DataSet dsIssueToInDistrict)
    {
        bool result = false;
        try
        {
            if (connectionMASTER != null)
            {
                connectionMASTER.Open();
                if (dsIssueToInDistrict != null)
                {
                    if (dsIssueToInDistrict.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsIssueToInDistrict.Tables[0].Rows)
                        {
                            // command.Dispose();
                            string IssueID = dr["IssueID"].ToString();
                            SqlCommand cmd = new SqlCommand();
                            cmd = new SqlCommand();
                            cmd.CommandTimeout = 7800;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "select * from issuetoindistrict where IssueID='" + IssueID + "'";
                            cmd.Connection = connectionMASTER;
                            SqlDataReader drc;
                            drc = cmd.ExecuteReader();
                            if (drc.Read())
                            {
                                drc.Close();
                            }
                            else
                            {
                                try
                                {
                                    drc.Close();
                                    command = new SqlCommand();
                                    command.CommandTimeout = 7800;
                                    command.Connection = connectionMASTER;
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.CommandText = "proc_insertrunnerissuetoindistrict";
                                    command.Parameters.AddWithValue("@IssueID", dr["IssueID"].ToString());
                                    command.Parameters.AddWithValue("@DistrictId", dr["DistrictId"].ToString());
                                    command.Parameters.AddWithValue("@Proc_AgID", dr["Proc_AgID"].ToString());
                                    command.Parameters.AddWithValue("@PCID", dr["PCID"].ToString());

                                    command.Parameters.AddWithValue("@SocietyID", dr["SocietyID"].ToString());
                                    command.Parameters.AddWithValue("@CropYear", dr["CropYear"].ToString());
                                    command.Parameters.AddWithValue("@MarketingSeasonId", dr["MarketingSeasonId"].ToString());
                                    command.Parameters.AddWithValue("@GodownTypeId", dr["GodownTypeId"].ToString());
                                    command.Parameters.AddWithValue("@GodownCenterId", dr["GodownCenterId"].ToString());

                                    command.Parameters.AddWithValue("@Place", dr["Place"].ToString());
                                    command.Parameters.AddWithValue("@CommodityId", dr["CommodityId"].ToString());
                                    command.Parameters.AddWithValue("@Bags", CheckNullInt(dr["Bags"].ToString()));
                                    command.Parameters.AddWithValue("@QtyTransffer", CheckNullFloat(dr["QtyTransffer"].ToString()));
                                    command.Parameters.AddWithValue("@DateOfIssue", (dr["DateOfIssue"].ToString()));

                                    command.Parameters.AddWithValue("@CreatedDate", (dr["CreatedDate"].ToString()));
                                    command.Parameters.AddWithValue("@UpdatedDate", (dr["UpdatedDate"].ToString()));
                                    command.ExecuteNonQuery();
                                    result = true;
                                }
                                catch (Exception Ex)
                                {
                                    connectionNEW.Open();
                                    command.Dispose();
                                    command = new SqlCommand();
                                    command.Connection = connectionNEW;
                                    command.CommandTimeout = 7800;
                                    command.CommandType = CommandType.Text;
                                    command.CommandText = "INSERT INTO ExceptionHandling ([TableName],[Exmsg],[Date],[Farmer_Id]) VALUES (@TableName,@Exmsg,getdate(),@Farmer_Id)";
                                    command.Parameters.AddWithValue("@TableName", "issuetoindistrict");
                                    command.Parameters.AddWithValue("@Exmsg", Ex.ToString());
                                    //command.Parameters.AddWithValue("@Date", System.DateTime.Now.ToString("MM/dd/yyyy"));
                                    command.Parameters.AddWithValue("@Farmer_Id", dr["IssueID"].ToString());
                                    command.ExecuteNonQuery();
                                    connectionNEW.Close();
                                }
                                finally { }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
        finally { connectionMASTER.Close(); }
        return result;
    }

    [WebMethod]
    public bool InsertIssueToOtherDistrict(DataSet dsIssueToOtherDistrict)
    {
        bool result = false;
        try
        {
            if (connectionMASTER != null)
            {
                connectionMASTER.Open();
                if (dsIssueToOtherDistrict != null)
                {
                    if (dsIssueToOtherDistrict.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsIssueToOtherDistrict.Tables[0].Rows)
                        {
                            string IssueID = dr["IssueID"].ToString();
                            SqlCommand cmd = new SqlCommand();
                            cmd = new SqlCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "select * from IssueToOtherDistrict where IssueID='" + IssueID + "'";
                            cmd.Connection = connectionMASTER;
                            SqlDataReader drc;
                            drc = cmd.ExecuteReader();
                            if (drc.Read())
                            {
                                drc.Close();
                            }
                            else
                            {
                                try
                                {
                                    drc.Close();
                                    // command.Dispose();
                                    command = new SqlCommand();
                                    command.Connection = connectionMASTER;
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.CommandText = "proc_insertrunnerissuetootherdistrict";
                                    command.Parameters.AddWithValue("@IssueID", dr["IssueID"].ToString());
                                    command.Parameters.AddWithValue("@DistrictId", dr["DistrictId"].ToString());
                                    command.Parameters.AddWithValue("@Proc_AgID", dr["Proc_AgID"].ToString());
                                    command.Parameters.AddWithValue("@PCID", dr["PCID"].ToString());

                                    command.Parameters.AddWithValue("@SocietyID", dr["SocietyID"].ToString());
                                    command.Parameters.AddWithValue("@CropYear", dr["CropYear"].ToString());
                                    command.Parameters.AddWithValue("@MarketingSeasonId", dr["MarketingSeasonId"].ToString());
                                    command.Parameters.AddWithValue("@SenderDistId", dr["SendingDistId"].ToString());
                                    command.Parameters.AddWithValue("@centerid", dr["centerid"].ToString());
                                    command.Parameters.AddWithValue("@Place", dr["Place"].ToString());
                                    command.Parameters.AddWithValue("@CommodityId", dr["CommodityId"].ToString());
                                    command.Parameters.AddWithValue("@Bags", CheckNullInt(dr["Bags"].ToString()));
                                    command.Parameters.AddWithValue("@QtyTransffer", CheckNullFloat(dr["QtyTransffer"].ToString()));
                                    command.Parameters.AddWithValue("@DateOfIssue", (dr["DateOfIssue"].ToString()));

                                    command.Parameters.AddWithValue("@CreatedDate", (dr["CreatedDate"].ToString()));
                                    command.Parameters.AddWithValue("@UpdatedDate", (dr["UpdatedDate"].ToString()));
                                    command.ExecuteNonQuery();
                                    result = true;
                                }
                                catch (Exception Ex)
                                {
                                    connectionNEW.Open();
                                    command.Dispose();
                                    command = new SqlCommand();
                                    command.Connection = connectionNEW;
                                    command.CommandTimeout = 7800;
                                    command.CommandType = CommandType.Text;
                                    command.CommandText = "INSERT INTO ExceptionHandling ([TableName],[Exmsg],[Date],[Farmer_Id]) VALUES (@TableName,@Exmsg,getdate(),@Farmer_Id)";
                                    command.Parameters.AddWithValue("@TableName", "IssueToOtherDistrict");
                                    command.Parameters.AddWithValue("@Exmsg", Ex.ToString());
                                    //command.Parameters.AddWithValue("@Date", System.DateTime.Now.ToString("MM/dd/yyyy"));
                                    command.Parameters.AddWithValue("@Farmer_Id", dr["IssueID"].ToString());
                                    command.ExecuteNonQuery();
                                    connectionNEW.Close();
                                }
                                finally { }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
        finally { connectionMASTER.Close(); }
        return result;
    }

    [WebMethod]
    public bool Inserttransportmaster(DataSet dstransportmaster)
    {
        bool result = false;
        try
        {
            if (connectionMASTER != null)
            {
                connectionMASTER.Open();
                if (dstransportmaster != null)
                {
                    if (dstransportmaster.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dstransportmaster.Tables[0].Rows)
                        {
                            string transporter_id = dr["transporter_id"].ToString();
                            SqlCommand cmd = new SqlCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "select * from TransportMaster where transporter_id='" + transporter_id + "'";
                            cmd.Connection = connectionMASTER;
                            SqlDataReader dre;
                            dre = cmd.ExecuteReader();
                            if (dre.Read())
                            {
                                cmd.Dispose();
                                dre.Close();
                            }
                            else
                            {
                                dre.Close();
                                //command.Dispose();
                                command = new SqlCommand();
                                command.Connection = connectionMASTER;
                                command.CommandType = CommandType.StoredProcedure;
                                command.CommandText = "proc_insertrunner_tranportmaster";
                                command.Parameters.AddWithValue("@transporter_id", dr["transporter_id"].ToString());
                                command.Parameters.AddWithValue("@transporter_name", dr["transporter_name"].ToString());
                                command.Parameters.AddWithValue("@nooftrucs", dr["nooftrucs"].ToString());
                                command.Parameters.AddWithValue("@address", dr["address"].ToString());
                                command.Parameters.AddWithValue("@mobileno", dr["mobileno"].ToString());
                                command.Parameters.AddWithValue("@user_id", dr["user_id"].ToString());
                                command.Parameters.AddWithValue("@datetimestamp", (dr["datetimestamp"].ToString()));
                                command.Parameters.AddWithValue("@opration", dr["opration"].ToString());
                                command.Parameters.AddWithValue("@locked", dr["locked"].ToString());
                                command.Parameters.AddWithValue("@societycode", dr["societycode"].ToString());
                                command.Parameters.AddWithValue("@district_id", dr["district_id"].ToString());
                                command.ExecuteNonQuery();
                                result = true;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
        finally { connectionMASTER.Close(); }
        return result;
    }

    [WebMethod]
    public bool InsertFarmerSMS(DataSet dsFarmerSMS)
    {
        bool result = false;
        //try
        //{
        //    if (connectionMASTER != null)
        //    {
        //        connectionMASTER.Open();
        //        if (dsFarmerSMS != null)
        //        {
        //            if (dsFarmerSMS.Tables[0].Rows.Count > 0)
        //            {
        //                foreach (DataRow dr in dsFarmerSMS.Tables[0].Rows)
        //                {
        //                    string SMS_ID = dr["SMS_ID"].ToString();
        //                    string Society_ID = dr["Society_ID"].ToString();
        //                    string District_Code = dr["District_Code"].ToString();
        //                    SqlCommand cmd = new SqlCommand();
        //                    cmd.CommandType = CommandType.Text;
        //                    cmd.CommandText = "select * from FarmerSMS where SMS_ID='" + SMS_ID + "' and Society_ID='" + Society_ID + "'";
        //                    cmd.Connection = connectionMASTER;
        //                    cmd.CommandTimeout = 5600;
        //                    SqlDataReader dre;
        //                    dre = cmd.ExecuteReader();
        //                    if (dre.Read())
        //                    {
        //                        cmd.Dispose();
        //                        dre.Close();
        //                    }
        //                    else
        //                    {
        //                        dre.Close();
        //                        //command.Dispose();
        //                        command = new SqlCommand();
        //                        command.Connection = connectionMASTER;
        //                        command.CommandType = CommandType.Text;
        //                        command.CommandText = "INSERT INTO [FarmerSMS] ([SMS_ID],[Farmer_Id],[District_Code],[Society_ID],[PC_ID],[DateOfCallingFarmer],[MobileNo],[DateOfCreation],[CropExpectedDate],[OneFarmerLimit],[NextCropExpectedDate],[RemainingCrop],[SocietyLimit]) VALUES (@SMS_ID,@Farmer_Id,@District_Code,@Society_ID,@PC_ID,@DateOfCallingFarmer,@MobileNo,@DateOfCreation,@CropExpectedDate,@OneFarmerLimit,@NextCropExpectedDate,@RemainingCrop,@SocietyLimit)";
        //                        command.Parameters.AddWithValue("@SMS_ID", dr["SMS_ID"].ToString());
        //                        command.Parameters.AddWithValue("@Farmer_Id", dr["Farmer_Id"].ToString());
        //                        command.Parameters.AddWithValue("@District_Code", dr["District_Code"].ToString());
        //                        command.Parameters.AddWithValue("@Society_ID", dr["Society_ID"].ToString());
        //                        command.Parameters.AddWithValue("@PC_ID", dr["PC_ID"].ToString());
        //                        command.Parameters.AddWithValue("@DateOfCallingFarmer", (dr["DateOfCollingFarmer"].ToString()));
        //                        command.Parameters.AddWithValue("@MobileNo", (dr["MobileNo"].ToString()));
        //                        command.Parameters.AddWithValue("@CropExpectedDate", dr["CropExpectedDate"].ToString());
        //                        command.Parameters.AddWithValue("@DateOfCreation", dr["DateOfCreation"].ToString());
        //                        command.Parameters.AddWithValue("@OneFarmerLimit", dr["OneFarmerLimit"].ToString());
        //                        command.Parameters.AddWithValue("@NextCropExpectedDate", dr["NextCropExpectedDate"].ToString());
        //                        command.Parameters.AddWithValue("@RemainingCrop", dr["RemainingCrop"].ToString());
        //                        command.Parameters.AddWithValue("@SocietyLimit", dr["SocietyLimit"].ToString());
        //                        command.ExecuteNonQuery();
        //                        result = true;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{

        //}
        //finally { connectionMASTER.Close(); }
        return result;
    }

    [WebMethod]
    public bool Insertcapgodown(DataSet dscapgodown)
    {
        bool result = false;
        try
        {
            if (connection != null)
            {
                connection.Open();
                if (dscapgodown != null)
                {
                    if (dscapgodown.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dscapgodown.Tables[0].Rows)
                        {
                            string capid = dr["capid"].ToString();
                            SqlCommand cmd = new SqlCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "select * from capgodown where capid=" + capid;
                            cmd.Connection = connection;
                            SqlDataReader dre;
                            dre = cmd.ExecuteReader();
                            if (dre.Read())
                            {
                                cmd.Dispose();
                                dre.Close();
                            }
                            else
                            {
                                dre.Close();
                                command.Dispose();
                                command = new SqlCommand();
                                command.Connection = connection;
                                command.CommandType = CommandType.StoredProcedure;
                                command.CommandText = "proc_insertrunnercapgodown";
                                command.Parameters.AddWithValue("@capid", dr["capid"].ToString());
                                command.Parameters.AddWithValue("@capgodam", dr["capgodam"].ToString());
                                command.Parameters.AddWithValue("@capgodamorder", dr["capgodamorder"].ToString());
                                command.ExecuteNonQuery();
                                command.Dispose();
                                result = true;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {

            connection.Close();
        }
        return result;
    }

    [WebMethod]
    public bool InsertRunnerLOG(DataSet dsRunnerlog)
    {
        bool result = false;
        try
        {
            if (connectionMASTER != null)
            {
                connectionMASTER.Open();
                if (dsRunnerlog != null)
                {
                    if (dsRunnerlog.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsRunnerlog.Tables[0].Rows)
                        {
                            string society = dr["Society_Id"].ToString();
                            string district = dr["District_ID"].ToString();
                            command = new SqlCommand();
                            command.Connection = connectionMASTER;
                            command.CommandType = CommandType.Text;
                            command.CommandText = "INSERT INTO [RunnerLog] ([LogID],[LogIP],[LogDate] ,[Pc_Id],[District_Code],[Society_Id],[RunnerID],[Status]) VALUES (@LogID,@LogIP,getdate(),@Pc_Id,@District_Code,@Society_Id,@RunnerID,@Status)";
                            command.Parameters.AddWithValue("@LogID", society + System.DateTime.Now.ToString("MM/dd/yyyy"));
                            command.Parameters.AddWithValue("@LogIP", "log");
                            //command.Parameters.AddWithValue("@LogDate", dr["capgodamorder"].ToString());
                            command.Parameters.AddWithValue("@Pc_Id", society);
                            command.Parameters.AddWithValue("@District_Code", district);
                            command.Parameters.AddWithValue("@Society_Id", society);
                            command.Parameters.AddWithValue("@RunnerID", society);
                            command.Parameters.AddWithValue("@Status", "yes");
                            command.ExecuteNonQuery();
                            command.Dispose();
                            result = true;
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {

        }
        finally
        {

            connectionMASTER.Close();
        }
        return result;
    }

    [WebMethod]
    public DataSet Selecttbl_fci_master()
    {
        DataSet dataset = new DataSet();
        try
        {
            if (connectionMASTER != null)
            {
                connectionMASTER.Open();
                command = new SqlCommand();
                String qrySelect = "SELECT * FROM tbl_fci_master ";
                command.CommandText = qrySelect;
                command.Connection = connectionMASTER;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dataset);
                command.Dispose();

            }
        }
        catch (Exception ex)
        {

        }
        finally
        {

            connectionMASTER.Close();
        }
        return dataset;
    }

    [WebMethod]
    public DataSet Selecttbl_MPWLC_DEPOT()
    {
        DataSet dataset = new DataSet();
        try
        {
            if (connectionMASTER != null)
            {
                try
                {
                    connectionMASTER.Open();
                    command = new SqlCommand();
                    String qrySelect = "SELECT * FROM tbl_MPWLC_DEPOT ";
                    command.CommandText = qrySelect;
                    command.Connection = connectionMASTER;
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(dataset);
                    command.Dispose();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    connectionMASTER.Close();

                }
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {


        }
        return dataset;
    }

    [WebMethod]
    public DataSet Selecttbl_MPWLC_Godown_Storage()
    {
        DataSet dataset = new DataSet();
        try
        {
            if (connectionMASTER != null)
            {
                connectionMASTER.Open();
                try
                {
                    command = new SqlCommand();
                    String qrySelect = "SELECT * FROM tbl_MPWLC_Godown_Storage where GodwonStatus ='True'";
                    command.CommandText = qrySelect;
                    command.Connection = connectionMASTER;
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(dataset);
                    command.Dispose();
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    connectionMASTER.Close();
                }
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {

        }
        return dataset;
    }

    [WebMethod]
    public DataSet SelectDCCBLoanOfFarmer(string district, string Society)
    {
        DataSet dataset = new DataSet();
        //DataSet datasetsocloan = new DataSet();
        //try
        //{
        //    if (connectionMASTER != null)
        //    {
        //        connectionMASTER.Open();
        //        try
        //        {
        //            command = new SqlCommand();
        //            String str = "select Farmer_Id from FarmerRegistration where Procured_SocietyID='" + Society + "'";
        //            command.CommandText = str;
        //            command.Connection = connectionMASTER;
        //            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
        //            dataAdapter.Fill(datasetsocloan);
        //            command.Dispose();
        //        }
        //        catch (Exception ex)
        //        { }
        //        finally
        //        { connectionMASTER.Close(); }

        //        connectionMASTER.Open();
        //        try
        //        {
        //            string finalfarmerids = "";
        //            foreach (DataRow dr in datasetsocloan.Tables[0].Rows)
        //            {
        //                string farmerid = dr["Farmer_Id"].ToString();
        //                finalfarmerids = finalfarmerids + "'" + farmerid + "'" + ",";
        //            }
        //            if (datasetsocloan.Tables[0].Rows.Count != 0)
        //            {
        //                int fid = finalfarmerids.LastIndexOf(",");
        //                string ff = finalfarmerids.Remove(fid);
        //                command = new SqlCommand();
        //                String qrySelect = "select * from DCCBLoanOfFarmer where FarmerRegNumber in (" + ff + ") ";
        //                command.CommandText = qrySelect;
        //                command.Connection = connectionMASTER;
        //                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
        //                dataAdapter.Fill(dataset);
        //                command.Dispose();
        //            }
        //        }
        //        catch (Exception ex)
        //        { }
        //        finally
        //        { connectionMASTER.Close(); }
        //    }
        //}
        //catch (Exception ex)
        //{

        //}
        //finally
        //{

        //}
        return dataset;
    }

    [WebMethod]
    public DataSet SelectSocietyLoanOfFarmer(string district, string Society)
    {
        DataSet dataset = new DataSet();
        //DataSet datasetsocloan = new DataSet();
        //try
        //{
        //    if (connectionMASTER != null)
        //    {
        //        try
        //        {
        //            connectionMASTER.Open();
        //            command = new SqlCommand();
        //            String str = "select Farmer_Id from FarmerRegistration where Procured_SocietyID='" + Society + "'";
        //            command.CommandText = str;
        //            command.Connection = connectionMASTER;
        //            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
        //            dataAdapter.Fill(datasetsocloan);
        //            command.Dispose();
        //        }
        //        catch (Exception ex)
        //        { }
        //        finally
        //        {
        //            connectionMASTER.Close();
        //        }

        //        //DataSet dataset = new DataSet();
        //        try
        //        {

        //            if (connectionMASTER != null)
        //            {
        //                try
        //                {
        //                    connectionMASTER.Open();
        //                    string finalfarmerids = "";
        //                    foreach (DataRow dr in datasetsocloan.Tables[0].Rows)
        //                    {
        //                        string farmerid = dr["Farmer_Id"].ToString();
        //                        finalfarmerids = finalfarmerids + "'" + farmerid + "'" + ",";
        //                    }
        //                    if (datasetsocloan.Tables[0].Rows.Count != 0)
        //                    {
        //                        int fid = finalfarmerids.LastIndexOf(",");
        //                        string ff = finalfarmerids.Remove(fid);
        //                        command = new SqlCommand();
        //                        String qrySelect = "select * from SocietyLoanOfFarmer where FarmerRegNumber in (" + ff + ") ";
        //                        command.CommandText = qrySelect;
        //                        command.Connection = connectionMASTER;
        //                        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
        //                        dataAdapter.Fill(dataset);
        //                        command.Dispose();
        //                    }
        //                }
        //                catch (Exception ex)
        //                { }
        //                finally
        //                {
        //                    //connectionMASTER.Close();
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        { }
        //        finally
        //        { connectionMASTER.Close(); }
        //    }
        //}
        //catch (Exception ex)
        //{

        //}
        //finally
        //{

        //}
        return dataset;
    }

    [WebMethod]
    public DataSet SelectIrrigationLoanOfFarmer(string district, string Society)
    {

        DataSet dataset = new DataSet();
        //DataSet datasetsocloan = new DataSet();

        //if (connectionMASTER != null)
        //{
        //    connectionMASTER.Open();
        //    try
        //    {
        //        command = new SqlCommand();
        //        String str = "select Farmer_Id from FarmerRegistration where Procured_SocietyID='" + Society + "'";
        //        command.CommandText = str;
        //        command.Connection = connectionMASTER;
        //        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
        //        dataAdapter.Fill(datasetsocloan);
        //        command.Dispose();
        //    }
        //    catch (Exception ex)
        //    { }
        //    finally
        //    { connectionMASTER.Close(); }
        //}
        //try
        //{
        //    if (connectionMASTER != null)
        //    {
        //        connectionMASTER.Open();

        //        string finalfarmerids = "";
        //        foreach (DataRow dr in datasetsocloan.Tables[0].Rows)
        //        {
        //            string farmerid = dr["Farmer_Id"].ToString();
        //            finalfarmerids = finalfarmerids + "'" + farmerid + "'" + ",";
        //        }
        //        if (datasetsocloan.Tables[0].Rows.Count != 0)
        //        {
        //            int fid = finalfarmerids.LastIndexOf(",");
        //            string ff = finalfarmerids.Remove(fid);
        //            command = new SqlCommand();
        //            String qrySelect = "select * from IrrigationLoanOfFarmer where FarmerRegNumber in(" + ff + ")";
        //            command.CommandText = qrySelect;
        //            command.Connection = connectionMASTER;
        //            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
        //            dataAdapter.Fill(dataset);
        //            command.Dispose();
        //        }

        //    }
        //}
        //catch (Exception ex)
        //{

        //}
        //finally
        //{

        //    connectionMASTER.Close();
        //}
        return dataset;
    }

    [WebMethod]
    public DataSet SelectDelFarmerRegistration_Log(string district, string Society)
    {
        DataSet dataset = new DataSet();
        //try
        //{
        //    if (connectionNEW != null)
        //    {
        //        connectionNEW.Open();
        //        command = new SqlCommand();
        //        String qrySelect = "select * from DelFarmerRegistration_Log where District_Id ='" + district + "' and Procured_Dist_ID='" + Society + "'";
        //        command.CommandText = qrySelect;
        //        command.Connection = connectionNEW;

        //        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
        //        dataAdapter.Fill(dataset);
        //        command.Dispose();

        //    }
        //}
        //catch (Exception ex)
        //{

        //}
        //finally
        //{

        //    connectionNEW.Close();
        //}
        return dataset;
    }

    [WebMethod]
    public DataSet SelectTehsilYeild(string district)
    {
        DataSet dataset = new DataSet();
        try
        {
            if (connectionMASTER != null)
            {
                connectionMASTER.Open();
                command = new SqlCommand();
                String qrySelect = "select * from TehsilYeild where District_Code ='" + district + "'";
                command.CommandText = qrySelect;
                command.Connection = connectionMASTER;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dataset);
                command.Dispose();

            }
        }
        catch (Exception ex)
        {

        }
        finally
        {

            connectionMASTER.Close();
        }
        return dataset;
    }

    [WebMethod]
    public DataSet SelectSocietyLoanMaster(string district)
    {
        DataSet dataset = new DataSet();
        try
        {
            if (connectionNEW != null)
            {
                connectionNEW.Open();
                command = new SqlCommand();
                String qrySelect = "select * from SocietyLoanMaster where DistrictId ='" + district + "'";
                command.CommandText = qrySelect;
                command.Connection = connectionNEW;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dataset);
                command.Dispose();

            }
        }
        catch (Exception ex)
        {

        }
        finally
        {

            connectionNEW.Close();
        }
        return dataset;
    }

    [WebMethod]
    public DataSet SelectCommodityMaster()
    {
        DataSet dataset = new DataSet();
        try
        {
            if (connectionNEW != null)
            {
                //connectionNEW.Open();
                //command = new SqlCommand();
                //String qrySelect = "select * from CommodityMaster";
                //command.CommandText = qrySelect;
                //command.Connection = connectionNEW;
                //SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                //dataAdapter.Fill(dataset);
                //command.Dispose();

            }
        }
        catch (Exception ex)
        {

        }
        finally
        {

            connectionNEW.Close();
        }
        return dataset;
    }

    [WebMethod]
    public DataSet SelectCommodityRate()
    {
        DataSet dataset = new DataSet();
        try
        {
            if (connectionNEW != null)
            {
                //connectionNEW.Open();
                //command = new SqlCommand();
                //String qrySelect = "select * from CommodityRate";
                //command.CommandText = qrySelect;
                //command.Connection = connectionNEW;

                //SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                //dataAdapter.Fill(dataset);
                //command.Dispose();

            }
        }
        catch (Exception ex)
        {

        }
        finally
        {

            connectionNEW.Close();
        }
        return dataset;
    }

    [WebMethod]
    public DataSet selectFarmerDeleteSoc(string district, string Society)
    {
        DataSet dataset = new DataSet();
        //try
        //{
        //    if (connectionNEW != null)
        //    {
        //        connectionNEW.Open();
        //        command = new SqlCommand();
        //        String qrySelect = "select * from DelFarmerIfSocietyChangeOnline where OldPrcuredSociety_Id='" + Society + "'";
        //        command.CommandText = qrySelect;
        //        command.Connection = connectionNEW;
        //        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
        //        dataAdapter.Fill(dataset);
        //        command.Dispose();
        //    }
        //}
        //catch (Exception ex)
        //{

        //}
        //finally
        //{

        //    connectionNEW.Close();
        //}
        return dataset;
    }

    [WebMethod]
    public DataSet SelectNewFarmerRegistration_Log(string district, string Society)
    {
        DataSet dsfarmerlogNew = new DataSet();
        DataTable dsfarmernew = new DataTable();
        DataSet dataset = new DataSet();
        //try
        //{
        //    if (connectionNEW != null)
        //    {
        //dsfarmernew.Columns.Add("District_Id");
        //dsfarmernew.Columns.Add("Village_Id");
        //dsfarmernew.Columns.Add("VillageName");
        //dsfarmernew.Columns.Add("Tehsil_Id");
        //dsfarmernew.Columns.Add("Farmer_Id");
        //dsfarmernew.Columns.Add("FarmerName");
        //dsfarmernew.Columns.Add("FatherHusName");
        //dsfarmernew.Columns.Add("Gram_Panchayat");
        //dsfarmernew.Columns.Add("PatwariHalkaNo");
        //dsfarmernew.Columns.Add("Mobileno");
        //dsfarmernew.Columns.Add("Category");
        //dsfarmernew.Columns.Add("RinPustikaNo");
        //dsfarmernew.Columns.Add("Farmer_EID_UID_No");
        //dsfarmernew.Columns.Add("Farmer_BankName");
        //dsfarmernew.Columns.Add("Farmer_BankAccountNo");
        //dsfarmernew.Columns.Add("PC_ID");
        //dsfarmernew.Columns.Add("Procured_SocietyID");
        //dsfarmernew.Columns.Add("Procured_Dist_ID");
        //dsfarmernew.Columns.Add("Procured_Place");
        //dsfarmernew.Columns.Add("Collecter_MaxQty");
        //dsfarmernew.Columns.Add("CropExpected_Date");
        //dsfarmernew.Columns.Add("Status");
        //dsfarmernew.Columns.Add("CreatedDate");
        //dsfarmernew.Columns.Add("updatedDate");
        //dsfarmernew.Columns.Add("ip");
        //dsfarmernew.Columns.Add("userid");
        //dsfarmernew.Columns.Add("RegistrationDate");
        //dsfarmernew.Columns.Add("Farmer_BankName_New");
        //dsfarmernew.Columns.Add("Farmer_BankBranchName");

        //connectionNEW.Open();
        //command = new SqlCommand();
        //String qrySelect = "select * from NewFarmerRegistration_Log where District_Id='" + district + "' and Society_Id='" + Society + "'";
        //command.CommandText = qrySelect;
        //command.Connection = connectionNEW;

        //SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
        //dataAdapter.Fill(dataset);

        //DataRow dr;
        //if (dataset != null)
        //{
        //    if (dataset.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow drn in dataset.Tables[0].Rows)
        //        {
        //            string Farmer_Id = drn["Farmer_Id"].ToString();
        //            SqlCommand cmd = new SqlCommand();
        //            cmd.CommandText = "select * from FarmerRegistration where Farmer_Id='" + Farmer_Id + "'";
        //            SqlDataAdapter das;
        //            cmd.Connection = connectionNEW;
        //            das = new SqlDataAdapter(cmd);
        //            DataSet dsnew = new DataSet();
        //            das.Fill(dsnew);
        //            dr = dsfarmernew.NewRow();
        //            foreach (DataRow dr1 in dsnew.Tables[0].Rows)
        //            {
        //                dr["District_Id"] = dr1["District_Id"].ToString();
        //                dr["Village_Id"] = dr1["Village_Id"].ToString();
        //                dr["VillageName"] = dr1["VillageName"].ToString();
        //                dr["Tehsil_Id"] = dr1["Tehsil_Id"].ToString();
        //                dr["Farmer_Id"] = dr1["Farmer_Id"].ToString();
        //                dr["FarmerName"] = dr1["FarmerName"].ToString();
        //                dr["FatherHusName"] = dr1["FatherHusName"].ToString();
        //                dr["Gram_Panchayat"] = dr1["Gram_Panchayat"].ToString();
        //                dr["PatwariHalkaNo"] = dr1["PatwariHalkaNo"].ToString();
        //                dr["Mobileno"] = dr1["Mobileno"].ToString();
        //                dr["Category"] = dr1["Category"].ToString();
        //                dr["RinPustikaNo"] = dr1["RinPustikaNo"].ToString();
        //                dr["Farmer_EID_UID_No"] = dr1["Farmer_EID_UID_No"].ToString();
        //                dr["Farmer_BankName"] = dr1["Farmer_BankName"].ToString();
        //                dr["Farmer_BankAccountNo"] = dr1["Farmer_BankAccountNo"].ToString();
        //                dr["PC_ID"] = dr1["PC_ID"].ToString();
        //                dr["Procured_SocietyID"] = dr1["Procured_SocietyID"].ToString();
        //                dr["Procured_Dist_ID"] = dr1["Procured_Dist_ID"].ToString();
        //                dr["Procured_Place"] = dr1["Procured_Place"].ToString();
        //                dr["Collecter_MaxQty"] = dr1["Collecter_MaxQty"].ToString();
        //                dr["CropExpected_Date"] = dr1["CropExpected_Date"].ToString();
        //                dr["Status"] = dr1["Status"].ToString();
        //                dr["userid"] = dr1["userid"].ToString();
        //                dr["CreatedDate"] = dr1["CreatedDate"].ToString();
        //                dr["updatedDate"] = dr1["updatedDate"].ToString();
        //                dr["ip"] = dr1["ip"].ToString();
        //                dr["RegistrationDate"] = dr1["RegistrationDate"].ToString();
        //                dr["Farmer_BankName_New"] = dr1["Farmer_BankName_New"].ToString();
        //                dr["Farmer_BankBranchName"] = dr1["Farmer_BankBranchName"].ToString();
        //                //  
        //                dsfarmernew.Rows.Add(dr);
        //            }
        //        }
        //        dsfarmerlogNew.Tables.Add(dsfarmernew);
        //    }
        //}
        //command.Dispose();
        //    }
        //}
        //catch (Exception ex)
        //{

        //}
        //finally
        //{
        //    connectionNEW.Close();
        //}
        return dsfarmerlogNew;
    }

    [WebMethod]
    public DataSet selectNEWFarmer_LandRecordDescription_Log(string district, string Society)
    {
        DataTable dsfarmerLANDnew = new DataTable();
        DataSet dsfarmerLand = new DataSet();
        DataSet dataset = new DataSet();
        //try
        //{
        //    if (connectionNEW != null)
        //    {
        //        dsfarmerLANDnew.Columns.Add("farmer_id");
        //        dsfarmerLANDnew.Columns.Add("village_id");
        //        dsfarmerLANDnew.Columns.Add("villagename");
        //        dsfarmerLANDnew.Columns.Add("crop_id");
        //        dsfarmerLANDnew.Columns.Add("landowner_name");
        //        dsfarmerLANDnew.Columns.Add("landowner_rinpustikano");
        //        dsfarmerLANDnew.Columns.Add("landtype");
        //        dsfarmerLANDnew.Columns.Add("khasarano");
        //        dsfarmerLANDnew.Columns.Add("rakba");
        //        dsfarmerLANDnew.Columns.Add("rakba_crop_sinchit");
        //        dsfarmerLANDnew.Columns.Add("rakba_crop_asinchit");
        //        dsfarmerLANDnew.Columns.Add("rakba_crop_sinchit_qty");
        //        dsfarmerLANDnew.Columns.Add("rakba_crop_asinchit_qty");
        //        dsfarmerLANDnew.Columns.Add("procured_qty");
        //        dsfarmerLANDnew.Columns.Add("crpcode");
        //        connectionNEW.Open();
        //        command = new SqlCommand();
        //        String qrySelect = "select * from NewFarmerRegistration_Log where District_Id='" + district + "' and Society_Id='" + Society + "'";
        //        command.CommandText = qrySelect;
        //        command.Connection = connectionNEW;

        //        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
        //        dataAdapter.Fill(dataset);

        //        DataRow dr;
        //        if (dataset != null)
        //        {
        //            if (dataset.Tables[0].Rows.Count > 0)
        //            {
        //                foreach (DataRow drn in dataset.Tables[0].Rows)
        //                {
        //                    string Farmer_Id = drn["Farmer_Id"].ToString();
        //                    SqlCommand cmd = new SqlCommand();
        //                    cmd.CommandText = "select * from Farmer_LandRecordDescription where Farmer_Id='" + Farmer_Id + "'";
        //                    SqlDataAdapter das;
        //                    cmd.Connection = connectionNEW;
        //                    das = new SqlDataAdapter(cmd);
        //                    DataSet dsnew = new DataSet();
        //                    das.Fill(dsnew);
        //                    foreach (DataRow dr1 in dsnew.Tables[0].Rows)
        //                    {
        //                        dr = dsfarmerLANDnew.NewRow();
        //                        dr["farmer_id"] = dr1["farmer_id"].ToString();
        //                        dr["village_id"] = dr1["village_id"].ToString();
        //                        dr["villagename"] = dr1["villagename"].ToString();
        //                        dr["crop_id"] = dr1["crop_id"].ToString();
        //                        dr["landowner_name"] = dr1["landowner_name"].ToString();
        //                        dr["landowner_rinpustikano"] = dr1["landowner_rinpustikano"].ToString();
        //                        dr["landtype"] = dr1["landtype"].ToString();
        //                        dr["khasarano"] = dr1["khasarano"].ToString();
        //                        dr["rakba"] = dr1["rakba"].ToString();
        //                        dr["rakba_crop_sinchit"] = dr1["rakba_crop_sinchit"].ToString();
        //                        dr["rakba_crop_asinchit"] = dr1["rakba_crop_asinchit"].ToString();
        //                        dr["rakba_crop_sinchit_qty"] = dr1["rakba_crop_sinchit_qty"].ToString();
        //                        dr["rakba_crop_asinchit_qty"] = dr1["rakba_crop_asinchit_qty"].ToString();
        //                        dr["procured_qty"] = dr1["procured_qty"].ToString();
        //                        dr["crpcode"] = dr1["crpcode"].ToString();
        //                        dsfarmerLANDnew.Rows.Add(dr);
        //                    }
        //                }
        //                dsfarmerLand.Tables.Add(dsfarmerLANDnew);
        //            }
        //        }
        //        command.Dispose();
        //    }
        //}
        //catch (Exception ex)
        //{

        //}
        //finally
        //{

        //    connectionNEW.Close();
        //}
        return dsfarmerLand;

    }

    [WebMethod]
    public DataSet SelectUPDATEFarmerRegistration_Log(string district, string Society)
    {
        //DataSet dsfarmerlogUpdate = new DataSet();
        //DataTable dsfarmerUpdate = new DataTable();
        DataSet dataset = new DataSet();
        //try
        //{
        //    if (connectionNEW != null)
        //    {
        //        connectionNEW.Open();
        //        command = new SqlCommand();
        //        command.CommandTimeout = 5600;
        //        String qrySelect = "select * from FarmerRegistrationSocietyChange_Log where Procured_SocietyID='" + Society + "'";
        //        command.CommandText = qrySelect;
        //        command.Connection = connectionNEW;
        //        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
        //        dataAdapter.Fill(dataset);
        //    }
        //}
        //catch (Exception ex)
        //{

        //}
        //finally
        //{

        //    connectionNEW.Close();
        //}
        return dataset;
    }

    [WebMethod]
    public DataSet selectUPDATEFarmer_LandRecordDescription_Log(string Society)
    {
        DataTable dsfarmerLANDUPDATE = new DataTable();
        DataSet dsfarmerLandRecord = new DataSet();
        DataSet dataset = new DataSet();
        DataSet dataset1 = new DataSet();
        //try
        //{
        //    if (connectionNEW != null)
        //    {
        //        //dsfarmerLANDUPDATE.Columns.Add("farmer_id");
        //        //dsfarmerLANDUPDATE.Columns.Add("village_id");
        //        //dsfarmerLANDUPDATE.Columns.Add("villagename");
        //        //dsfarmerLANDUPDATE.Columns.Add("crop_id");
        //        //dsfarmerLANDUPDATE.Columns.Add("landowner_name");
        //        //dsfarmerLANDUPDATE.Columns.Add("landowner_rinpustikano");
        //        //dsfarmerLANDUPDATE.Columns.Add("landtype");
        //        //dsfarmerLANDUPDATE.Columns.Add("khasarano");
        //        //dsfarmerLANDUPDATE.Columns.Add("rakba");
        //        //dsfarmerLANDUPDATE.Columns.Add("rakba_crop_sinchit");
        //        //dsfarmerLANDUPDATE.Columns.Add("rakba_crop_asinchit");
        //        //dsfarmerLANDUPDATE.Columns.Add("rakba_crop_sinchit_qty");
        //        //dsfarmerLANDUPDATE.Columns.Add("rakba_crop_asinchit_qty");
        //        //dsfarmerLANDUPDATE.Columns.Add("procured_qty");
        //        //dsfarmerLANDUPDATE.Columns.Add("crpcode");
        //        //connectionNEW.Open();
        //        //command = new SqlCommand();
        //        //command.CommandTimeout = 5600;
        //        //String qrySelect = "select Farmer_Id from FarmerRegistrationSocietyChange_Log where Procured_SocietyID='" + Society + "'";
        //        //command.CommandText = qrySelect;
        //        //command.Connection = connectionNEW;
        //        //SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
        //        //dataAdapter.Fill(dataset1);
        //        //DataRow dr2;
        //        //DataRow dr;

        //        //string finalfarmerids = "";
        //        //foreach (DataRow dr1 in dataset1.Tables[0].Rows)
        //        //{
        //        //    string farmerid = dr1["Farmer_Id"].ToString();
        //        //    finalfarmerids = finalfarmerids + "'" + farmerid + "'" + ",";
        //        //}
        //        //if (dataset1.Tables[0].Rows.Count != 0)
        //        //{
        //        //    int fid = finalfarmerids.LastIndexOf(",");
        //        //    string ff = finalfarmerids.Remove(fid);
        //        //    command = new SqlCommand();
        //        //    String qrySelectdata = "select * from Farmer_LandRecordDescriptionSocietyChange_Log where farmer_id in (" + ff + ")";
        //        //    command.CommandText = qrySelectdata;
        //        //    command.Connection = connectionNEW;
        //        //    SqlDataAdapter dataAdapter1 = new SqlDataAdapter(command);
        //        //    dataAdapter1.Fill(dataset);
        //        //}

        //        //if (dataset1 != null)
        //        //{
        //        //    if (dataset1.Tables[0].Rows.Count > 0)
        //        //    {
        //        //        foreach (DataRow drn1 in dataset1.Tables[0].Rows)
        //        //        {
        //        //            string Farmer_Id = drn1["Farmer_Id"].ToString();
        //        //            SqlCommand cmd = new SqlCommand();
        //        //            cmd.CommandText = "select farmer_id,village_id,villagename,crop_id,landowner_name,landowner_rinpustikano,landtype,khasarano,rakba,rakba_crop_sinchit,rakba_crop_asinchit,rakba_crop_sinchit_qty,rakba_crop_asinchit_qty,procured_qty,crpcode from Farmer_LandRecordDescription where Farmer_Id='" + Farmer_Id + "'";
        //        //            SqlDataAdapter das;
        //        //            cmd.CommandTimeout = 8000;
        //        //            cmd.Connection = connectionNEW;
        //        //            das = new SqlDataAdapter(cmd);
        //        //            DataSet dsnew = new DataSet();
        //        //            das.Fill(dsnew);
        //        //            foreach (DataRow dr1 in dsnew.Tables[0].Rows)
        //        //            {
        //        //                dr = dsfarmerLANDUPDATE.NewRow();
        //        //                dr["farmer_id"] = dr1["farmer_id"].ToString();
        //        //                dr["village_id"] = dr1["village_id"].ToString();
        //        //                dr["villagename"] = dr1["villagename"].ToString();
        //        //                dr["crop_id"] = dr1["crop_id"].ToString();
        //        //                dr["landowner_name"] = dr1["landowner_name"].ToString();
        //        //                dr["landowner_rinpustikano"] = dr1["landowner_rinpustikano"].ToString();
        //        //                dr["landtype"] = dr1["landtype"].ToString();
        //        //                dr["khasarano"] = dr1["khasarano"].ToString();
        //        //                dr["rakba"] = dr1["rakba"].ToString();
        //        //                dr["rakba_crop_sinchit"] = dr1["rakba_crop_sinchit"].ToString();
        //        //                dr["rakba_crop_asinchit"] = dr1["rakba_crop_asinchit"].ToString();
        //        //                dr["rakba_crop_sinchit_qty"] = dr1["rakba_crop_sinchit_qty"].ToString();
        //        //                dr["rakba_crop_asinchit_qty"] = dr1["rakba_crop_asinchit_qty"].ToString();
        //        //                dr["procured_qty"] = dr1["procured_qty"].ToString();
        //        //                dr["crpcode"] = dr1["crpcode"].ToString();
        //        //                dsfarmerLANDUPDATE.Rows.Add(dr);
        //        //            }
        //        //            cmd.Dispose();
        //        //        }
        //        //        dsfarmerLandRecord.Tables.Add(dsfarmerLANDUPDATE);

        //        //    }
        //        // }
        //        //1
        //        command.Dispose();

        //    }
        //}
        //catch (Exception ex)
        //{

        //}
        //finally
        //{
        //    connectionNEW.Close();
        //}

        return dataset;

    }

    [WebMethod]
    public DataSet SelectRunnerInfo(string district, string Society)
    {
        DataSet dataset = new DataSet();
        try
        {
            if (connectionNEW != null)
            {
                //connectionNEW.Open();
                //command = new SqlCommand();
                //String qrySelect = "select * from RunnerRegistration where District_ID='" + district + "' and SocietyID='" + Society + "'";
                //command.CommandText = qrySelect;
                //command.Connection = connectionNEW;
                //SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                //dataAdapter.Fill(dataset);
                //command.Dispose();

            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            connectionNEW.Close();
        }
        return dataset;
    }

    [WebMethod]
    public DataSet SelectSentSmsStatus()
    {
        DataSet dataset = new DataSet();
        try
        {
            if (connectionNEW != null)
            {
                //connectionNEW.Open();
                //command = new SqlCommand();
                //String qrySelect = "select * from SentSmsStatus";
                //command.CommandText = qrySelect;
                //command.Connection = connectionNEW;
                //SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                //dataAdapter.Fill(dataset);
                //command.Dispose();

            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            connectionNEW.Close();
        }
        return dataset;
    }

    [WebMethod]
    public DataSet SelectUpdateCommodityReceivedFromFarmer(string district, string Society)
    {
        DataSet dataset = new DataSet();
        try
        {
            if (connectionNEW != null)
            {
                connectionNEW.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from FarmerRegistration_Log where District_Id='" + district + "' and SocietyID ='" + Society + "'";
                cmd.Connection = connectionNEW;
                SqlDataReader dre;
                dre = cmd.ExecuteReader();
                if (dre.Read())
                {
                    //command = new SqlCommand();
                    //String qrySelect = "select * from CommodityReceivedFromFarmer where FarmerID='" + FarmerID + "'";                                        
                    //command.CommandText = qrySelect;
                    //command.Connection = connection;

                    //SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    //dataAdapter.Fill(dataset);
                    //command.Dispose();
                    //cmd.Dispose();
                    //dre.Close();
                }
                else
                {
                    dre.Close();
                }
                command.Dispose();

            }
        }
        catch (Exception ex)
        {

        }
        finally
        {

            connectionNEW.Close();
        }
        return dataset;
    }

    [WebMethod]
    public DataSet selecttransportermaster(string district, string Society)
    {

        DataSet dataset = new DataSet();
        try
        {
            if (connectionMASTER != null)
            {
                connectionMASTER.Open();
                command = new SqlCommand();
                String qrySelect = "select * from TransportMaster where SocietyCode='" + Society + "' and District_ID='" + district + "'";
                command.CommandText = qrySelect;
                command.Connection = connectionNEW;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dataset);
                command.Dispose();

            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            connectionMASTER.Close();
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

    private string chkSha(string St)
    {
        string St2 = null;

        if (St.Contains("'"))
        {
            St2 = St.Replace("'", " ");
        }
        else if (St.IndexOf("'") > 0)
        {
            St2 = St.Replace("'", " ");
        }
        else if (St.IndexOf(".") > 0)
        {
            St2 = St.Replace(".", " ");
        }
        else
        {
            St2 = St;
        }
        return St2;
    }



}


