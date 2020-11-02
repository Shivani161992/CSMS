using System;
using System.Data.SqlClient;
//using System.Windows.Forms;
using System.Text;
using System.Configuration;
using System.Data;

namespace DataAccess
{
    /// <summary>
    /// Summary description for Common.
    /// </summary>
    /// 

    public class MasterListItem
    {
        private string code;
        private string name;
        public MasterListItem(string sCode, string sName)
        {
            // 
            // TODO: Add constructor logic here
            //
            this.code = sCode;
            this.name = sName;
        }

        public string Code
        {
            get
            {
                return code;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }
    }

    public class Common
    {
        private SqlConnection  _Connection = null;
        private string  ConnectionStringName = null;
       // private static int SQL_SERVER = 1;
       // private static int ACCESS_DB = 2;
        //private Language _Language = null;

        //public Language Language
        //{
        //    get { return _Language; }
        //}
        public string conString
        {
            get { return ConnectionStringName; }
            set { ConnectionStringName = value; }
        }
        public SqlConnection Connection
        {
            get { return _Connection; }
        }

        public Common()
        {
            try
            {
                System.Diagnostics.Trace.WriteLine("Executing Static Constructor ");
                OpenConnection();
                //				if(objForm!=null)
                //					_Language = new Language(objForm);
            }
            catch (Exception e)
            {
                Error_Report(e);
            }
        }
        public Common(string conName)
        {
            ConnectionStringName  = conName;
            try
            {
                System.Diagnostics.Trace.WriteLine("Executing Static Constructor ");
                OpenConnection();
                //				if(objForm!=null)
                //					_Language = new Language(objForm);
            }
            catch (Exception e)
            {
                Error_Report(e);
            }
        }
        ~Common()
        {
            System.Diagnostics.Trace.WriteLine("Executing Static Destructor ");
            //CloseConnection();
        }

        public SqlConnection OpenConnection()
        {
            try
            {
                if (_Connection == null)
                {
                  //  string connStr = ConfigurationSettings.AppSettings[ConnectionStringName];                        
                   //ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                   //ConfigurationSettings.AppSettings["ConnectionString"];
                    string connStr = ConnectionStringName;   
                    _Connection = new SqlConnection(connStr);
                    _Connection.Open();
                    System.Diagnostics.Trace.WriteLine("Connection Openning ");
                }
                return _Connection;
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.Write(e.Message);
                return null;
            }
        }
        public void CloseConnection()
        {
            try
            {
                System.Diagnostics.Trace.Write("Closing the Connection ");
                if (_Connection.State != ConnectionState.Closed)
                    _Connection.Close();
                //_Connection.Dispose();

                {
                }
                    
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.Write(e.Message);
            }
            finally
            {
               
            }

        }

        /// <summary>
        /// 1st Variant:
        /// </summary>
        /// <param name="drow">the record to be used to access the value of field from.</param>
        /// <param name="dataField">The name of the field to be accessed from DataRow.</param>
        /// <returns>string field value</returns>
        public static string getString(object obj)
        {

            string x = "";

            if (obj != System.DBNull.Value)
            {
                x = obj.ToString();
            }

            return x;
        }

        /// <summary>
        /// 1st Variant:
        /// </summary>
        /// <param name="drow">the record to be used to access the value of field from.</param>
        /// <param name="dataField">The name of the field to be accessed from DataRow.</param>
        /// <returns>string field value</returns>
        public static string getString(DataRow drow, string dataField)
        {

            string x = "";

            if (drow[dataField] != null)
            {
                x = drow[dataField].ToString();
            }

            return x;
        }



        /// <summary>
        /// 2nd Variant:
        /// </summary>
        /// <param name="drow">the interface datareader to used to access the value of field from.</param>
        /// <param name="dataField">The name of the field to be accessed from DataRow.</param>
        /// <returns>string field value</returns>
        public static string getString(IDataReader drow, string dataField)
        {

            string x = "";
            int nCol;

            nCol = drow.GetOrdinal(dataField);	//getOrdinal gets the column position of dataField

            if (!drow.IsDBNull(nCol))			//isDbNull check for NULL value in the given columnIndex
            {
                x = drow.GetString(nCol);
            }

            return x;

        }



        /// <summary>
        /// 3rd Variant:
        /// </summary>
        /// <param name="drow"> the interface datareader to used to access the value of field from.</param>
        /// <param name="dataField">The ordinal number of the field to be accessed from DataRow.</param>
        /// <returns>string field value</returns>
        public static string getString(IDataReader drow, int dataField)
        {

            string x = "";

            if (!drow.IsDBNull(dataField))
            {
                x = drow.GetString(dataField);
            }

            return x;
        }



        /// <summary>
        /// 4th Variant:
        /// </summary>
        /// <param name="drow">the datarow to used to access the value of field from.</param>
        /// <param name="dataField">The name of the field to be accessed from DataRow.</param>
        /// <returns>string field value</returns>
        public static string getString(DataRow drow, int dataField)
        {

            string x = "";

            if (!drow.IsNull(dataField))
            {
                x = drow[dataField].ToString();		// x;drow.GetString(dataField);
            }

            return x;
        }

        public static string getString(IDataReader drow, string dataField, string sType)
        {

            string x = "";
            int nCol;

            nCol = drow.GetOrdinal(dataField);	//getOrdinal gets the column position of dataField
            if (!drow.IsDBNull(nCol))
            {
                if (sType == "N")
                    x = drow.GetDouble(nCol).ToString();
                else
                    x = drow.GetString(nCol);
            }

            return x;
        }




        //the getNumber function is defined to access a particular field
        //value from the record.It checks the field for NULL value.
        //in case of NULL value it returns an 0 (zero) , else
        //returns the value retrieved.
        //this function can be called whereever we want to access a string field from table.
        //three variants of this function defined:
        //-----------------------------------------------------------------------------------

        /// <summary>
        /// 1st variant
        /// </summary>
        /// <param name="drow">the record to be used to access the value of field from.</param>
        /// <param name="dataField"> The name of the field to be accessed from DataRow.</param>
        /// <returns>double field value</returns>
        public static DateTime getDate(object obj)
        {
            DateTime dt = new DateTime(1899, 12, 31);
            if (obj != System.DBNull.Value)
            {
                dt = Convert.ToDateTime(obj);
            }
            return dt;
        }



        /// <summary>
        /// 1st variant
        /// </summary>
        /// <param name="drow">the record to be used to access the value of field from.</param>
        /// <param name="dataField"> The name of the field to be accessed from DataRow.</param>
        /// <returns>double field value</returns>
        public static DateTime getDate(DataRow dr, string sCol)
        {
            DateTime dt = new DateTime(1899, 12, 31);
            if (dr[sCol] != null)
            {
                dt = Convert.ToDateTime(dr[sCol]);
            }
            return dt;
        }

        /// <summary>
        /// 1st variant
        /// </summary>
        /// <param name="drow">the record to be used to access the value of field from.</param>
        /// <param name="dataField"> The name of the field to be accessed from DataRow.</param>
        /// <returns>double field value</returns>
        public static double getNumber(object obj)
        {
            double x = 0, nTmp = 0;

            if (obj != System.DBNull.Value)
                if (obj.ToString() != string.Empty)
                    if (double.TryParse(obj.ToString(), System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture, out nTmp))
                        x = nTmp;
            return x;
        }

        /// <summary>
        /// 1st variant
        /// </summary>
        /// <param name="drow">the record to be used to access the value of field from.</param>
        /// <param name="dataField"> The name of the field to be accessed from DataRow.</param>
        /// <returns>double field value</returns>
        public static double getNumber(DataRow drow, string dataField)
        {
            double x = 0;

            if (drow[dataField] != null)
            {
                x = Double.Parse(drow[dataField].ToString());
            }

            return x;
        }

        /// <summary>
        /// 2nd Variant:
        /// </summary>
        /// <param name="drow">the interface datareader to used to access the value of field from.</param>
        /// <param name="dataField">The name of the field to be accessed from DataRow.</param>
        /// <returns>double field value</returns>
        public static double getNumber(IDataReader drow, string dataField)
        {

            double x = 0;
            int nCol;

            nCol = drow.GetOrdinal(dataField);

            if (!drow.IsDBNull(nCol))
            {
                x = drow.GetDouble(nCol);
            }

            return x;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="drow">the interface datareader to used to access the value of field from.</param>
        /// <param name="dataField">The ordinal number of the field to be accessed from DataRow.</param>
        /// <returns></returns>
        public static double getNumber(IDataReader drow, int dataField)
        {

            double x = 0;

            if (!drow.IsDBNull(dataField))
            {
                x = drow.GetDouble(dataField);
            }

            return x;
        }
        public static string GetDate_ToUpdate(string sDate)
        {
            DateTime dt = DateTime.ParseExact(sDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            return GetDate_ToUpdate(dt);
        }
        public static string GetDate_ToUpdate(DateTime dt)
        {
            string sRet = dt.Day.ToString();
            sRet += "/" + dt.ToString("MMM");
            sRet += "/" + dt.Year.ToString();
            return sRet;
        }
        public static string GetDate_ToUpdate(DateTime dt, bool bTime)
        {
            string sRet = GetDate_ToUpdate(dt);
            if (bTime)
            {
                sRet += " " + dt.Hour.ToString();
                sRet += ":" + dt.Minute.ToString();
                sRet += ":" + dt.Second.ToString();
            }
            return sRet;
        }
        public static void Error_Report(Exception e)
        {
            //MessageBox.Show(e.Message + "Contact NIC !!!", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void Error_Report(string sMsg)
        {
            //MessageBox.Show(sMsg + "Contact NIC !!!", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static byte[] StringToByteArray(string str, Encoding enc)
        {
            //			System.Text.ASCIIEncoding  encoding=new System.Text.ASCIIEncoding();
            return enc.GetBytes(str);
        }
        public static string ByteArrayToString(byte[] dBytes, Encoding enc)
        {
            //			System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return enc.GetString(dBytes);
        }
        public static string GetEncrypted(string str)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] abyte = StringToByteArray(str, Encoding.Unicode);
            byte[] byRes = md5.ComputeHash(abyte);
            string sRet = Common.ByteArrayToString(byRes, Encoding.Unicode);
            return sRet;
        }
    }
}