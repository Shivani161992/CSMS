namespace changepwd
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using System.Data.OleDb;

    /// <summary>
    /// Summary description for Class1
    /// </summary>
    public class changepwd
    {
        public OleDbConnection connStr;
        public OleDbCommand cmd;
        public OleDbDataReader result;
        public Boolean dbopen;
        public Boolean found;
        public String oldpwd;
        public String newpwd;
        public String tbl;
        public String uid;

        public changepwd()
        {
            connStr = new OleDbConnection(ConfigurationManager.ConnectionStrings["conn"].ToString());
            dbopen = true;
            connStr.Open();
        }

        public void update(string str)
        {
            tbl = str;
            String sqlStr = "update"+" " +tbl+" "+ "set pwd='" + newpwd + "' where uid='" + uid + "'";
            cmd = new OleDbCommand(sqlStr,connStr);
            cmd.ExecuteNonQuery();
        
        }

    }
}