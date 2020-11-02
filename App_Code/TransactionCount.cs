using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Xml;
using System.Text;


/// <summary>
/// Summary description for PublicDistributionSystem
/// </summary>
/// 

[WebService(Namespace = "http://microsoft.co.in/", Name = "TransactionCount")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

public class TransactionCount : System.Web.Services.WebService
{

    private SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionStringFarmerDB"].ToString());
    private SqlCommand command, cmd;
    private SqlDataAdapter dataAdapter;
    private DataSet dataset;
    private SqlTransaction trans = null;
    private SqlCommand commandt = null;

    public TransactionCount()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    #region Security

    public bool chkSecurityTransaction(string UserName, string Password)
    {
        bool rtev = false;
        if (UserName != null && Password != null)
        {
            OpenConnection();
            SqlCommand chkcmd = new SqlCommand();
            DataSet cds = new DataSet();
            chkcmd.Connection = connection;
            chkcmd.CommandType = CommandType.StoredProcedure;
            chkcmd.CommandText = "View_ServiceInformation";
            chkcmd.Parameters.Clear();
            chkcmd.Parameters.AddWithValue("@UserName", UserName);
            chkcmd.Parameters.AddWithValue("@SPasswordInClient", Password);
            SqlDataAdapter da = new SqlDataAdapter(chkcmd);
            da.Fill(cds);
            if (cds != null)
            {
                if (cds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in cds.Tables[0].Rows)
                    {
                        bool IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
                        if (IsActive == true)
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

    #region OutPut

    [WebMethod(Description = "eTransactionCount(TransactionDate,UserName,Password)")]
    public XmlDocument eTransactionCount(string TransactionDate, string UserName, string Password)
    {
        XmlDocument xmldoc = new XmlDocument();
        dataset = new DataSet("TransactionCount");
        try
        {
            if (chkSecurityTransaction(UserName, Password))
            {
                OpenConnection();
                trans = connection.BeginTransaction();
                commandt = connection.CreateCommand();
                commandt.Transaction = trans;
                commandt.Parameters.Clear();
                commandt.CommandType = CommandType.StoredProcedure;
                commandt.Parameters.Clear();
                commandt.CommandText = "prc_TransactionCount";
                commandt.Parameters.AddWithValue("@TransactionDate",getDate_MDY(TransactionDate));
                dataAdapter = new SqlDataAdapter(commandt);
                dataAdapter.Fill(dataset);
                commandt.Dispose();
                trans.Commit();
                xmldoc = DatTables(dataset.Tables[0]);
            }
            else
            {
                dataset = null;
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
        return xmldoc;
    }

    private XmlDocument DatTables(DataTable dt)
    {
        XmlDocument xml = new XmlDocument();
    XmlDeclaration xmlDeclaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
      xml.AppendChild(xmlDeclaration);

       XmlElement root = xml.CreateElement("eTaal");
                xml.AppendChild(root);
 //       StringBuilder sb = new StringBuilder();
//                sb.Append("<eTaal>");
        foreach (DataColumn dc in dt.Columns)
        {
          ///  sb.Append("<response ");
        ///    sb.Append("ServiceCode ='" + dc.ToString() + "' ");

            foreach (DataRow dr in dt.Rows)
            {
        ///        sb.Append("ServiceCount = '" + dr[dc].ToString() + "'");
       ///      sb.Append(" LocationCode = '23'");
         ///       sb.Append("/>");
               XmlElement row = xml.CreateElement("Response");
                    row.SetAttribute("ServiceCode",  dc.ToString());
                    row.SetAttribute("ServiceCount", dr[dc].ToString() );
                    root.AppendChild(row);
            }
        }
        
        //xml.LoadXml(sb.ToString());
        return xml;
    }
    #endregion

    #region Connection

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
    protected string getDate_MDY(string inDate)
    {

        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));

    }
}

