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
/// Summary description for DCCB
/// </summary>
public class DCCB
{
    private Common cCommon;
    private DbClass db;
    public String query;
    public Boolean found;
    public DCCB(Common cCommon)
	{
        db = new DbClass(cCommon.Connection);
	}
    public DataSet selectAll()
    {

        found = false;
        String qrySelect = "SELECT * FROM dbo.DCCBLogin ";
        db.DbParameters.Clear();
        db.Query = qrySelect;
        DataSet ds = db.SelectinDataSet();
        return ds;
    }
    public DataSet selectAll(String where)
    {

        found = false;
        String qrySelect = "SELECT * FROM dbo.DCCBLogin " + where;
        db.DbParameters.Clear();
        db.Query = qrySelect;
        DataSet ds = db.SelectinDataSet();
        return ds;
    }
    public int insertDCCBFarmerLoan(String qryInsert)
    {
        db.Query = qryInsert;
        return db.ExecuteNonQuery();
    }
}
