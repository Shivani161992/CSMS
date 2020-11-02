using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml.Serialization;


[WebService(Namespace = "http://microsoft.co.in/", Name = "Import_Wheat_Kharidi_2014", Description = "Import Data from Offline Software to Online Database (upload data on server)/Date: 04/03/2014")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Import_Wht_Kharidi_2014 : System.Web.Services.WebService 
{
    public SqlConnection conware = new SqlConnection(ConfigurationManager.ConnectionStrings["connstorage"].ToString());
    private SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_WPMS2014"].ToString());
    private SqlDataAdapter dataAdapter;
    private DataSet dataset;
    private SqlCommand cmd = null;
    private SqlTransaction trans = null;
    string Query = "";

    public Import_Wht_Kharidi_2014 () 
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    #region Security

    public sec_Wheat_Kharidi_2014 sec_Wheat_Kharidi_2014;
    [SoapHeader("sec_Wheat_Kharidi_2014")]

    [WebMethod(Description = "This Method Is Used For Security Check in")]
    public bool chkSecurityImport(sec_Wheat_Kharidi_2014 S)
    {
        bool rtev = false;
        try
        {
            if (S != null)
            {
                OpenConnection();
                cmd = new SqlCommand();
                dataset = new DataSet();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "View_ServiceInformation";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@UserName", S.UserName);
                cmd.Parameters.AddWithValue("@SPasswordInClient", S.Password);
                dataAdapter = new SqlDataAdapter(cmd);
                dataAdapter.Fill(dataset);
                if (dataset != null)
                {
                    if (dataset.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataset.Tables[0].Rows)
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

    #region Export Data from Online To Offline.......

    [WebMethod(Description = "This Method Is Used For Update UserPassword Table in offline Database")]
    public DataSet OpUserPassword()
    {
        dataset = new DataSet();
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            cmd = connection.CreateCommand();
            cmd.Transaction = trans;
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from UserPassword where ischanged='Y'";
            dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dataset);
            cmd.Dispose();
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

    [WebMethod(Description = "This Method Is Used For Insert New Village in Offline Database")]
    public DataSet OpVillages(string DistrictId, string Villagecodes)
    {
        dataset = new DataSet();
        try
        {
            if (Villagecodes == "")
            {
                Villagecodes = "''";
            }
            OpenConnection();
            cmd = connection.CreateCommand();
            trans = connection.BeginTransaction();
            cmd.Transaction = trans;
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from VillageMaster where District_Id='" + DistrictId + "' and  VILLAGE_STATUS='N' and VILLAGE_CODE not in (" + Villagecodes + ") ";
            dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dataset);
            cmd.Dispose();
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

    [WebMethod(Description = "This Method Is Used For Insert New Bank in Offline Database")]
    public DataSet OpBankMaster()
    {
        dataset = new DataSet();
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            cmd = connection.CreateCommand();
            cmd.Transaction = trans;
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from BankMaster";
            dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dataset);
            cmd.Dispose();
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

    [WebMethod(Description = "This Method Is Used For Insert Soceity Loan in Offline Database")]
    public DataSet OpSocietyLoanMaster(string D, string socloanmids)
    {
        dataset = new DataSet();
        try
        {
            if (socloanmids == "")
            {
                socloanmids = "''";
            }
            OpenConnection();
            trans = connection.BeginTransaction();
            cmd = connection.CreateCommand();
            cmd.Transaction = trans;
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from SocietyLoanMaster where SocietyLoanMaster.DistrictId='" + D + "' and SocietyLoanMaster.Society_Id not in (" + socloanmids + ")";
            dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dataset);
            cmd.Dispose();
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

    [WebMethod(Description = "This Method Is Used For Soceity Loan output for offline database ")]
    public DataSet OpSocietyLoan(string districtid, string societyid, string farmerids)
    {
        dataset = new DataSet();
        try
        {
            if (farmerids == "")
            {
                farmerids = "''";
            }
            OpenConnection();
            trans = connection.BeginTransaction();
            cmd = connection.CreateCommand();
            cmd.Transaction = trans;
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from SocietyLoanOfFarmer where  District_Id='" + districtid + "' and PC_ID='" + societyid + "' and Farmer_Id not in (" + farmerids + ")";
            dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dataset);
            cmd.Dispose();
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

    [WebMethod(Description = "This Method Is Used For DCCB Loan output for offline database ")]
    public DataSet OpDCCBLoan(string districtid, string dccbloanids)
    {
        dataset = new DataSet();
        try
        {
            if (dccbloanids == "")
            {
                dccbloanids = "''";
            }
            OpenConnection();
            trans = connection.BeginTransaction();
            cmd = connection.CreateCommand();
            cmd.Transaction = trans;
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from DCCBLoanOfFarmer where DCCBLoanOfFarmer.District_Id='" + districtid + "' and DCCBLoanOfFarmer.Farmer_Id not in (" + dccbloanids + ")";
            dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dataset);
            cmd.Dispose();
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

    [WebMethod(Description = "This Method Is Used For Irrigation Loan output for offline database ")]
    public DataSet OpIrrigationLoan(string districtid, string irriloafarmerids)
    {
        dataset = new DataSet();
        try
        {
            if (irriloafarmerids == "")
            {
                irriloafarmerids = "''";
            }
            OpenConnection();
            trans = connection.BeginTransaction();
            cmd = connection.CreateCommand();
            cmd.Transaction = trans;
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from IrrLoanOfFarmer where IrrLoanOfFarmer.District_Id='" + districtid + "' and IrrLoanOfFarmer.Farmer_Id not in (" + irriloafarmerids + ")";
            dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dataset);
            cmd.Dispose();
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

    [WebMethod(Description = "This Method Is Used For Transport Master output for offline database ")]
    public DataSet OpTransportMaster(string districtid, string societyid)
    {
        dataset = new DataSet();
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            cmd = connection.CreateCommand();
            cmd.Transaction = trans;
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from TransportMaster where District_ID='" + districtid + "' and SocietyCode='" + societyid + "'";
            dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dataset);
            cmd.Dispose();
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

    [WebMethod(Description = "This Method Is Used For Tehsil Yield output for offline database ")]
    public DataSet OpTehsilYeild(string District_Code)
    {
        dataset = new DataSet();
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            cmd = connection.CreateCommand();
            cmd.Transaction = trans;
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from TehsilYield where District_Code='" + District_Code + "'";
            dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dataset);
            cmd.Dispose();
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

    [WebMethod(Description = "This Method Is Used For Godown Storage output for offline database ")]
    public DataSet OpGodownStorage()
    {
        dataset = new DataSet();
        try
        {
            conware.Open();
            trans = conware.BeginTransaction();
            cmd = conware.CreateCommand();
            cmd.Transaction = trans;
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from tbl_MetaData_GODOWN";
            dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dataset);
            cmd.Dispose();
            trans.Commit();
        }
        catch (Exception)
        {
            trans.Commit();
            conware.Close();
        }
        finally
        {
            conware.Close();
        }
        return dataset;
    }

    [WebMethod(Description = "This Method Is Used For MPWLC DEPOT output for offline database ")]
    public DataSet Optbl_MPWLC_DEPOT()
    {
        dataset = new DataSet();
        try
        {
            conware.Open();
            trans = conware.BeginTransaction();
            cmd = conware.CreateCommand();
            cmd.Transaction = trans;
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from tbl_MetaData_DEPOT";
            dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dataset);
            cmd.Dispose();
            trans.Commit();
        }
        catch (Exception)
        {
            trans.Commit();
            conware.Close();
        }
        finally
        {
            conware.Close();
        }
        return dataset;
    }

    [WebMethod(Description = "This Method Is Used For Acceptance_Note_Detail output for offline database ")]
    public DataSet OpAcceptance_Note_Detail(string districtid, string societyid)
    {
        dataset = new DataSet();
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            cmd = connection.CreateCommand();
            cmd.Transaction = trans;
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Acceptance_Note_Detail where Distt_ID ='" + districtid + "' and Purchase_Center ='" + societyid + "'";
            dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dataset);
            cmd.Dispose();
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

    [WebMethod(Description = "This Method Is Used For CropLoss output for offline database ")]
    public DataSet OpCropLoss(string districtid, string societyid)
    {
        dataset = new DataSet();
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            cmd = connection.CreateCommand();
            cmd.Transaction = trans;
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from CropLoss WHERE DistrictID = '" + districtid + "' AND SocietyID='" + societyid + "'";
            dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dataset);
            cmd.Dispose();
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

    [WebMethod(Description = "This Method Is Used For IssueCenterReceipt_Online output for offline database ")]
    public DataSet OpIssueCenterReceipt_Online(string districtid, string societyid)
    {
        dataset = new DataSet();
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            cmd = connection.CreateCommand();
            cmd.Transaction = trans;
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from IssueCenterReceipt_Online where DistrictId='" + districtid + "' and SocietyID ='" + societyid + "'";
            dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dataset);
            cmd.Dispose();
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

    #region Insertion in Correction Log

    [WebMethod(Description = "This Method Is Used For Gunny bags Correction Log output for online database ")]
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
                    cmd = connection.CreateCommand();
                    cmd.Transaction = trans;
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (DataRow dr in dsGunnyBags.Tables[0].Rows)
                    {
                        trans = connection.BeginTransaction();
                        try
                        {
                            string IssueNo_or_GReceiptNo = dr["IssueNo_or_GReceiptNo"].ToString();
                            string District_ID = dr["District_ID"].ToString();
                            string Society_Id = dr["Society_Id"].ToString();
                            string NoOfBags = dr["NoOfBags"].ToString();
                            string IsUpdated = dr["IsUpdated"].ToString();
                            string IsDeleted = dr["IsDeleted"].ToString();
                            Query = "select count(*) from GunnyBagsCorrectionLog where District_ID='" + District_ID + "'  and Society_Id= '" + Society_Id + "' and IssueNo_or_GReceiptNo='" + IssueNo_or_GReceiptNo + "' and NoOfBags='" + NoOfBags + "' and IsUpdated='" + IsUpdated + "' and IsDeleted='" + IsDeleted + "'";
                            cmd = new SqlCommand(Query, connection, trans);
                            Int64 res = Convert.ToInt64(cmd.ExecuteScalar());
                            cmd.Parameters.Clear();
                            if (res <= 0)
                            {
                                string UserId = dr["UserId"].ToString();
                                string IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                                cmd.CommandText = "in_GunnyBagsCorrectionLog";
                                cmd.Parameters.AddWithValue("@IssueNo_or_GReceiptNo", IssueNo_or_GReceiptNo);
                                cmd.Parameters.AddWithValue("@District_ID", District_ID);
                                cmd.Parameters.AddWithValue("@Society_Id", Society_Id);
                                cmd.Parameters.AddWithValue("@NoOfBags", NoOfBags);
                                cmd.Parameters.AddWithValue("@IsUpdated", IsUpdated);
                                cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                                cmd.Parameters.AddWithValue("@UserId", UserId);
                                cmd.Parameters.AddWithValue("@IP", IP);
                                int x = cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                            }

                        }
                        catch
                        {
                            trans.Rollback();
                        }
                        trans.Commit();
                    }
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

    [WebMethod(Description = "This Method Is Used For Praptee Correction Log output for online database ")]
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
                    cmd = connection.CreateCommand();
                    cmd.Transaction = trans;
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (DataRow dr in dsPrapteeCorrection.Tables[0].Rows)
                    {
                        trans = connection.BeginTransaction();
                        try
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
                            Query = "select count(*) from PrapteeCorrectionLog where District_Id='" + District_Id + "'  and Society_Id= '" + Society_Id + "' and ReceivedID='" + ReceivedID + "' and Old_Bags='" + Old_Bags + "' and New_Bags='" + New_Bags + "' and Old_QtyReceived='" + Old_QtyReceived + "' and New_QtyReceived='" + New_QtyReceived + "'  and  IsUpdated='" + IsUpdated + "' and IsDelete='" + IsDelete + "'";
                            cmd = new SqlCommand(Query, connection, trans);
                            Int64 res = Convert.ToInt64(cmd.ExecuteScalar());
                            cmd.Parameters.Clear();
                            if (res <= 0)
                            {
                                string Old_TotalAmount = dr["Old_TotalAmount"].ToString();
                                string New_TotalAmount = dr["New_TotalAmount"].ToString();
                                string UpdationDate = getRDate_MDY(dr["UpdationDate"].ToString());
                                string UserID = dr["UserID"].ToString();
                                string IP = dr["IP"].ToString();
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
                                int req = cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                            }
                        }
                        catch
                        {
                            trans.Rollback();
                        }
                        trans.Commit();
                    }   
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

    [WebMethod(Description = "This Method Is Used For Jaree correction log output for online database")]
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
                    cmd = connection.CreateCommand();
                    cmd.Transaction = trans;
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (DataRow dr in dsJareeCorrectionLog.Tables[0].Rows)
                    {
                        trans = connection.BeginTransaction();
                        try
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
                            Query = "select count(*) from JareeCorrectionLog where DistrictId='" + DistrictId + "'  and SocietyID= '" + SocietyID + "' and IssueID='" + IssueID + "' and Old_Bags='" + Old_Bags + "' and New_Bags='" + New_Bags + "' and Old_QtyTransffer='" + Old_QtyTransffer + "' and New_QtyTransffer='" + New_QtyTransffer + "'  and  IsDeleted='" + IsDeleted + "' and IsUpdated='" + IsUpdated + "'";
                            cmd = new SqlCommand(Query, connection, trans);
                            Int64 res = Convert.ToInt64(cmd.ExecuteScalar());
                            cmd.Parameters.Clear();
                            if (res <= 0)
                            {
                                string UserId = dr["UserId"].ToString();
                                string Ip = dr["Ip"].ToString();
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
                                int x = cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                            }
                        }
                        catch
                        {
                            trans.Rollback();
                        }
                        trans.Commit();
                    }
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

    [WebMethod(Description = "This Method Is Used For Gunny bags Correction output for offline database ")]
    public DataSet OpGunnyBagsCorrection(string D, string S)
    {
        dataset = new DataSet();
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            cmd = connection.CreateCommand();
            cmd.Transaction = trans;
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            //Here I use user id as unique id whic is a unique id in offline table when coreection module is created this is because of upade and delete both case handling
            cmd.CommandText = "select distinct GunnyBagsCorrectionLog.UserId from GunnyBagsCorrectionLog  where GunnyBagsCorrectionLog.District_ID='" + D + "' and GunnyBagsCorrectionLog.Society_Id='" + S + "'";
            dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dataset);
            cmd.Dispose();
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

    [WebMethod(Description = "This Method Is Used For Praptee Correction Log output for offline database ")]
    public DataSet OpPrapteeCorrection(string D, string S)
    {
        dataset = new DataSet();
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            cmd = connection.CreateCommand();
            cmd.Transaction = trans;
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            //Here I use user id as unique id whic is a unique id in offline table when coreection module is created this is because of upade and delete both case handling
            cmd.CommandText = "select distinct PrapteeCorrectionLog.UserId from PrapteeCorrectionLog  where PrapteeCorrectionLog.District_ID='" + D + "' and PrapteeCorrectionLog.Society_Id='" + S + "'";
            dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dataset);
            cmd.Dispose();
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

    [WebMethod(Description = "This Method Is Used For Jaree correction log output for offline database ")]
    public DataSet OpJareeCorrection(string D, string S)
    {
        dataset = new DataSet();
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            cmd = connection.CreateCommand();
            cmd.Transaction = trans;
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            //Here I use user id as unique id whic is a unique id in offline table when coreection module is created this is because of upade and delete both case handling
            cmd.CommandText = "select distinct JareeCorrectionLog.UserId from JareeCorrectionLog  where JareeCorrectionLog.DistrictId='" + D + "' and JareeCorrectionLog.SocietyID='" + S + "' ";
            dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dataset);
            cmd.Dispose();
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
    public DataSet OpCommodityRate()
    {
        dataset = new DataSet();
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            cmd = connection.CreateCommand();
            cmd.Transaction = trans;
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from CommodityRate where Status1='Change'";
            dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dataset);
            cmd.Dispose();
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
    public DataSet OpDateControl()
    {
        dataset = new DataSet();
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            cmd = connection.CreateCommand();
            cmd.Transaction = trans;
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from DateControl where status='Change'";
            dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dataset);
            cmd.Dispose();
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

   
   

    [WebMethod(Description = "This Method Is Used For SocietyChangeLog output for offline database ")]
    public DataSet OP_Soc_Change(string D, string S, string F_IDS)
    {
        dataset = new DataSet();
        try
        {
            if (F_IDS == "")
            {
                F_IDS = "''";
            }
            OpenConnection();
            trans = connection.BeginTransaction();
            cmd = connection.CreateCommand();
            cmd.Transaction = trans;
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select distinct SocietyChangeLog.Farmer_ID from SocietyChangeLog  where SocietyChangeLog.District_ID='" + D + "' and SocietyChangeLog.NEW_PC_ID='" + S + "' and SocietyChangeLog.Farmer_ID NOT IN (select Farmer_ID from FarmerDeleteRequest)";
            dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dataset);
            cmd.Dispose();
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

    [WebMethod(Description = "This Method Is Used For Jaree correction log output for offline database ")]
    public DataSet OP_Soc_Change_Farmer_Basic_Info(string D, string S, string F_IDS)
    {
        dataset = new DataSet();
        try
        {
            if (F_IDS == "")
            {
                F_IDS = "''";
            }
            OpenConnection();
            trans = connection.BeginTransaction();
            cmd = connection.CreateCommand();
            cmd.Transaction = trans;
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from FarmerRegistration where FarmerRegistration.District_Id='" + D + "' and FarmerRegistration.Procured_SocietyID='" + S + "' and FarmerRegistration.Farmer_Id IN (" + F_IDS + ")";
            dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dataset);
            cmd.Dispose();
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

    [WebMethod(Description = "This Method Is Used For Jaree correction log output for offline database ")]
    public DataSet OP_Soc_Change_Farmer_Land_Info(string F_IDS)
    {
        dataset = new DataSet();
        try
        {
            if (F_IDS == "")
            {
                F_IDS = "''";
            }
            OpenConnection();
            trans = connection.BeginTransaction();
            cmd = connection.CreateCommand();
            cmd.Transaction = trans;
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Farmer_LandRecordDescription where Farmer_LandRecordDescription.Farmer_Id IN (" + F_IDS + ")";
            dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dataset);
            cmd.Dispose();
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

public class sec_Wheat_Kharidi_2014 : System.Web.Services.Protocols.SoapHeader
{
    public bool IsActive;
    public String Password;
    public String UserName;
    public String RunnerId;
    public String RunnerPassword;
}
