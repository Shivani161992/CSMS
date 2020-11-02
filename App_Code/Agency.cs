using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Data;
using DataAccess;
using System.Data.SqlClient;
public class Agency
{
    private Common cCommon;
    private DbClass db;
    public String query;
    public Boolean found;
    public String Agency_ID;
    public String AG_ID;
    public String AG_Name;
    public String Agency_Name;
    public String Password;
    public String Tname;
    public Agency(Common cCommon)
	{
        db = new DbClass(cCommon.Connection); 
	}

    public void select()
    {

        found = false;
        String qrySelect = "SELECT * FROM dbo.Agency_login where Agency_ID=@AG_ID";
        db.DbParameters.Clear();
        db.DbParameters.Add("@AG_ID", AG_ID);
        db.Query = qrySelect;
        DataSet ds = db.SelectinDataSet();
        if (ds != null)
        {

            if (ds.Tables[0].Rows.Count > 0)
            {

                found = true;
                DataRow dr = ds.Tables[0].Rows[0];
                Agency_ID = dr["Agency_ID"].ToString();
                Agency_Name = dr["Agency_Name"].ToString();
                Password = dr["Password"].ToString();
            }
        }
        ds.Dispose();
    }

    public DataSet selectAny(String selectstring)
    {

        found = false;
        String qrySelect = selectstring;
        db.DbParameters.Clear();
        db.Query = qrySelect;
        DataSet ds = db.SelectinDataSet();
        return ds;
    }
}
