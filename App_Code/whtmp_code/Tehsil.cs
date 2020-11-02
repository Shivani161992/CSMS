using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DataAccess;

/// <summary>
/// Summary description for Tehsil
/// </summary>
public class Tehsil
{
    private Common cCommon;
    private DbClass db;
    public String query;
    public Boolean found;
    public Tehsil(Common cCommon)
	{
        db = new DbClass(cCommon.Connection);
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
    public int insertTehsilYeild(String qryInsert)
    {
        db.Query = qryInsert;
        return db.ExecuteNonQuery();
    }
    
}
