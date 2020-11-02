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
/// Summary description for Irrigation
/// </summary>
public class Irrigation
{
    private Common cCommon;
    private DbClass db;
    public String query;
    public Boolean found;
    public String pwd;
    public Irrigation(Common cCommon)
	{
        db = new DbClass(cCommon.Connection);
	}
    public DataSet selectAll()
    {

        found = false;
        String qrySelect = "SELECT * FROM dbo.IrrigationLogin ";
        db.DbParameters.Clear();
        db.Query = qrySelect;
        DataSet ds = db.SelectinDataSet();
        return ds;
    }

    public DataSet selectAll(String where)
    {

        found = false;
        String qrySelect = "SELECT * FROM dbo.IrrigationLogin " + where;
        db.DbParameters.Clear();
        db.Query = qrySelect;
        DataSet ds = db.SelectinDataSet();
        return ds;
    }

    public int insertIrrigationFarmerLoan(String qryInsert)
    {
        db.Query = qryInsert;
        return db.ExecuteNonQuery();
    }
}
