using System;
using System.Data;
using System.Configuration;
using System.Web;
using DataAccess;
using System.Data.SqlClient;

namespace Data  {

  public class DeliveryOrder {

     private Common cCommon; 
     private DbClass db;
     public String query;
     public Boolean found;
     public String DO_ID; 
     public String DC_ID; 
     public String Shop_Id; 
     public String Month; 
     public String Year; 
     public String GodownNo; 
     public String DateOfIssue; 
     public String ValidityDate; 
     public String DO_Issued; 
     public String DO_Executed; 
     public String User_ID; 
     public String DateTimeStamp; 
     public String Operation; 
     public String Transferred;

      public DeliveryOrder(Common cCommon)
      {

   // cCommon = new Common();
    db = new DbClass(cCommon.Connection);
 }

 public void select() {

     found=false;
     String qrySelect = "SELECT * FROM dbo.DeliveryOrder where DO_ID =@DO_ID";
     db.DbParameters.Clear();
     db.DbParameters.Add("@DO_ID",DO_ID);
    db.Query = qrySelect;
    DataSet ds = db.SelectinDataSet();
    if (ds != null) {

    if (ds.Tables[0].Rows.Count > 0) {

     found=true;
     DataRow dr = ds.Tables[0].Rows[0];
     DO_ID = dr["DO_ID"].ToString() ; 
     DC_ID = dr["DC_ID"].ToString() ; 
     Shop_Id = dr["Shop_Id"].ToString() ; 
     Month = dr["Month"].ToString() ; 
     Year = dr["Year"].ToString() ; 
     GodownNo = dr["GodownNo"].ToString() ; 
     DateOfIssue = dr["DateOfIssue"].ToString() ; 
     ValidityDate = dr["ValidityDate"].ToString() ; 
     DO_Issued = dr["DO_Issued"].ToString() ; 
     DO_Executed = dr["DO_Executed"].ToString() ; 
     User_ID = dr["User_ID"].ToString() ; 
     DateTimeStamp = dr["DateTimeStamp"].ToString() ; 
     Operation = dr["Operation"].ToString() ; 
     Transferred = dr["Transferred"].ToString() ; 
    }
   }
   ds.Dispose();
 }

 public DataSet select(String where) {

     found=false;
     String qrySelect="SELECT * FROM dbo.DeliveryOrder where " + where ;
     db.DbParameters.Clear();
     db.Query = qrySelect;
    DataSet ds = db.SelectinDataSet();
    return ds;
}

 public DataSet selectAll() {

     found=false;
     String qrySelect = "SELECT * FROM dbo.DeliveryOrder";
     db.DbParameters.Clear();
     db.Query = qrySelect;
    DataSet ds = db.SelectinDataSet();
    return ds;
}

      public DataSet selectAll(String where)
      {

          found = true;
          String qrySelect = "SELECT * FROM dbo.DeliveryOrder order by " + where;
          db.DbParameters.Clear();
          db.Query = qrySelect;
          DataSet ds = db.SelectinDataSet();
          return ds;
      }

      public DataSet selectAny(String selectStatement)
      {

          found = true;
          String qrySelect = selectStatement;
          db.DbParameters.Clear();
          db.Query = qrySelect;
          DataSet ds = db.SelectinDataSet();
          return ds;
      }


 public int insert() {

   String qryInsert ="insert into  DeliveryOrder (DO_ID,DC_ID,Shop_Id,Month,Year,GodownNo,DateOfIssue,ValidityDate,DO_Issued,DO_Executed,User_ID,DateTimeStamp,Operation,Transferred) values(@DO_ID,@DC_ID,@Shop_Id,@Month,@Year,@GodownNo,@DateOfIssue,@ValidityDate,@DO_Issued,@DO_Executed,@User_ID,@DateTimeStamp,@Operation,@Transferred)";
   db.DbParameters.Clear();
    db.DbParameters.Add("@DO_ID",DO_ID);
    db.DbParameters.Add("@DC_ID",DC_ID);
    db.DbParameters.Add("@Shop_Id",Shop_Id);
    db.DbParameters.Add("@Month",Month);
    db.DbParameters.Add("@Year",Year);
    db.DbParameters.Add("@GodownNo",GodownNo);
    db.DbParameters.Add("@DateOfIssue",DateOfIssue);
    db.DbParameters.Add("@ValidityDate",ValidityDate);
    db.DbParameters.Add("@DO_Issued",DO_Issued);
    db.DbParameters.Add("@DO_Executed",DO_Executed);
    db.DbParameters.Add("@User_ID",User_ID);
    db.DbParameters.Add("@DateTimeStamp",DateTimeStamp);
    db.DbParameters.Add("@Operation",Operation);
    db.DbParameters.Add("@Transferred",Transferred);
   db.Query = qryInsert;
   return db.ExecuteNonQuery();
 }

 public int update() {

   String qryUpdate = "update DeliveryOrder set DO_ID=@DO_ID,DC_ID=@DC_ID,Shop_Id=@Shop_Id,Month=@Month,Year=@Year,GodownNo=@GodownNo,DateOfIssue=@DateOfIssue,ValidityDate=@ValidityDate,DO_Issued=@DO_Issued,DO_Executed=@DO_Executed,User_ID=@User_ID,DateTimeStamp=@DateTimeStamp,Operation=@Operation,Transferred=@Transferred where DO_ID =@DO_ID";
   db.DbParameters.Clear();
    db.DbParameters.Add("@DO_ID",DO_ID);
    db.DbParameters.Add("@DC_ID", DC_ID);
    db.DbParameters.Add("@Shop_Id", Shop_Id);
    db.DbParameters.Add("@Month", Month);
    db.DbParameters.Add("@Year", Year);
    db.DbParameters.Add("@GodownNo", GodownNo);
    db.DbParameters.Add("@DateOfIssue", DateOfIssue);
    db.DbParameters.Add("@ValidityDate", ValidityDate);
    db.DbParameters.Add("@DO_Issued", DO_Issued);
    db.DbParameters.Add("@DO_Executed", DO_Executed);
    db.DbParameters.Add("@User_ID", User_ID);
    db.DbParameters.Add("@DateTimeStamp", DateTimeStamp);
    db.DbParameters.Add("@Operation", Operation);
    db.DbParameters.Add("@Transferred", Transferred);
   db.Query = qryUpdate;
   return db.ExecuteNonQuery();
 }

 public int updateOnly(String str) {

   String stringTemp;
   String stringCal;
   String stringDuplicate;
   int count = 0;
   int pos;
   int start = 0;
   int i = 0;
   str = str+String.Concat(",");
   stringDuplicate = str;
   int length = str.Length;

while (length != 0){
    pos = str.IndexOf(",");
     count = count + 1;
     string stringCal1 = str.Substring(pos + 1);
     stringCal = stringCal1;
     length = length - (pos + 1);
     str = stringCal;
}
str = stringDuplicate;
length = str.Length;
string[] compareString = new string[count];
while (length != 0){
	pos = str.IndexOf(",");
	stringTemp = str.Substring(start, pos);
	compareString[i] = stringTemp;
	stringCal = str.Substring(pos + 1);
	length = length - (pos + 1);
	str = stringCal;
	i++;
}

for (int j = 0; j < compareString.Length; j++){
 if (compareString[j].Equals("DO_ID"))  
 query = query + compareString[j] + " = " + "@DO_ID,"; 
 if (compareString[j].Equals("DC_ID"))  
 query = query + compareString[j] + " = " + "@DC_ID,"; 
 if (compareString[j].Equals("Shop_Id"))  
 query = query + compareString[j] + " = " + "@Shop_Id,"; 
 if (compareString[j].Equals("Month"))  
 query = query + compareString[j] + " = " + "@Month,"; 
 if (compareString[j].Equals("Year"))  
 query = query + compareString[j] + " = " + "@Year,"; 
 if (compareString[j].Equals("GodownNo"))  
 query = query + compareString[j] + " = " + "@GodownNo,"; 
 if (compareString[j].Equals("DateOfIssue"))  
 query = query + compareString[j] + " = " + "@DateOfIssue,"; 
 if (compareString[j].Equals("ValidityDate"))  
 query = query + compareString[j] + " = " + "@ValidityDate,"; 
 if (compareString[j].Equals("DO_Issued"))  
 query = query + compareString[j] + " = " + "@DO_Issued,"; 
 if (compareString[j].Equals("DO_Executed"))  
 query = query + compareString[j] + " = " + "@DO_Executed,"; 
 if (compareString[j].Equals("User_ID"))  
 query = query + compareString[j] + " = " + "@User_ID,"; 
 if (compareString[j].Equals("DateTimeStamp"))  
 query = query + compareString[j] + " = " + "@DateTimeStamp,"; 
 if (compareString[j].Equals("Operation"))  
 query = query + compareString[j] + " = " + "@Operation,"; 
 if (compareString[j].Equals("Transferred"))  
 query = query + compareString[j] + " = " + "@Transferred,"; 
  }

query= query.Substring(0, query.Length - 1);
String sqlStr = "update DeliveryOrder set " + query + " where DO_ID =@DO_ID";
   db.DbParameters.Clear();
    db.DbParameters.Add("@DO_ID",DO_ID); 
    //db.DbParameters.Add("@DC_ID",DC_ID);
    //db.DbParameters.Add("@Shop_Id",Shop_Id);
    //db.DbParameters.Add("@Month",Month);
    //db.DbParameters.Add("@Year",Year);
    //db.DbParameters.Add("@GodownNo",GodownNo);
    //db.DbParameters.Add("@DateOfIssue",DateOfIssue);
    //db.DbParameters.Add("@ValidityDate",ValidityDate);
    db.DbParameters.Add("@DO_Issued",DO_Issued);
    //db.DbParameters.Add("@DO_Executed",DO_Executed);
    //db.DbParameters.Add("@User_ID",User_ID);
    //db.DbParameters.Add("@DateTimeStamp",DateTimeStamp);
    //db.DbParameters.Add("@Operation",Operation);
    //db.DbParameters.Add("@Transferred",Transferred);

   db.Query = sqlStr;
   return db.ExecuteNonQuery();
 }

      public int updateOnlyDO(String str)
      {

          String stringTemp;
          String stringCal;
          String stringDuplicate;
          int count = 0;
          int pos;
          int start = 0;
          int i = 0;
          str = str + String.Concat(",");
          stringDuplicate = str;
          int length = str.Length;

          while (length != 0)
          {
              pos = str.IndexOf(",");
              count = count + 1;
              string stringCal1 = str.Substring(pos + 1);
              stringCal = stringCal1;
              length = length - (pos + 1);
              str = stringCal;
          }
          str = stringDuplicate;
          length = str.Length;
          string[] compareString = new string[count];
          while (length != 0)
          {
              pos = str.IndexOf(",");
              stringTemp = str.Substring(start, pos);
              compareString[i] = stringTemp;
              stringCal = str.Substring(pos + 1);
              length = length - (pos + 1);
              str = stringCal;
              i++;
          }

          for (int j = 0; j < compareString.Length; j++)
          {
              if (compareString[j].Equals("DO_ID"))
                  query = query + compareString[j] + " = " + "@DO_ID,";
              if (compareString[j].Equals("DC_ID"))
                  query = query + compareString[j] + " = " + "@DC_ID,";
              if (compareString[j].Equals("Shop_Id"))
                  query = query + compareString[j] + " = " + "@Shop_Id,";
              if (compareString[j].Equals("Month"))
                  query = query + compareString[j] + " = " + "@Month,";
              if (compareString[j].Equals("Year"))
                  query = query + compareString[j] + " = " + "@Year,";
              if (compareString[j].Equals("GodownNo"))
                  query = query + compareString[j] + " = " + "@GodownNo,";
              if (compareString[j].Equals("DateOfIssue"))
                  query = query + compareString[j] + " = " + "@DateOfIssue,";
              if (compareString[j].Equals("ValidityDate"))
                  query = query + compareString[j] + " = " + "@ValidityDate,";
              if (compareString[j].Equals("DO_Issued"))
                  query = query + compareString[j] + " = " + "@DO_Issued,";
              if (compareString[j].Equals("DO_Executed"))
                  query = query + compareString[j] + " = " + "@DO_Executed,";
              if (compareString[j].Equals("User_ID"))
                  query = query + compareString[j] + " = " + "@User_ID,";
              if (compareString[j].Equals("DateTimeStamp"))
                  query = query + compareString[j] + " = " + "@DateTimeStamp,";
              if (compareString[j].Equals("Operation"))
                  query = query + compareString[j] + " = " + "@Operation,";
              if (compareString[j].Equals("Transferred"))
                  query = query + compareString[j] + " = " + "@Transferred,";
          }

          query = query.Substring(0, query.Length - 1);
          String sqlStr = "update DeliveryOrder set " + query + " where DO_ID =@DO_ID";
          db.DbParameters.Clear();
          db.DbParameters.Add("@DO_ID", DO_ID);
          db.DbParameters.Add("@DateOfIssue", DateOfIssue);
          db.DbParameters.Add("@ValidityDate", ValidityDate);         

          db.Query = sqlStr;
          return db.ExecuteNonQuery();
      }

 public int delete() {

     String qryDelete = " delete from dbo.DeliveryOrder where DO_ID =@DO_ID";
    db.DbParameters.Clear();
    db.DbParameters.Add("@DO_ID",DO_ID);
    db.Query = qryDelete;
    return db.ExecuteNonQuery();
   }
  }
 }
