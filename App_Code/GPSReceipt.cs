using System;
using System.Data;
using System.Configuration;
using System.Web;
using DataAccess;
using System.Data.SqlClient;

namespace Data  {

  public class GPSReceipt {

     private Common cCommon; 
     private DbClass db;
     public String query;
     public Boolean found;
     public String SNo; 
     public String Godown_ID; 
     public String MobileNo; 
     public String Photo; 
     public String Date_Time; 
     public String Latitude; 
     public String Longitude; 
     public String Altitude; 
     public String Flag; 
     public String Flag1; 
     public String Operation; 
     public String User_id; 
     public String Transferred; 
     public String DateTimeStamp;
      public byte[] bPhoto;
 public GPSReceipt(Common cCommon) {

    db = new DbClass(cCommon.Connection);
 }

 public void select() {

     found=false;
     String qrySelect="SELECT * FROM GPSReceipt where SNo =@SNo";
     db.DbParameters.Clear();
     db.DbParameters.Add("@SNo",SNo);
    db.Query = qrySelect;
    DataSet ds = db.SelectinDataSet();
    if (ds != null) {

    if (ds.Tables[0].Rows.Count > 0) {

     found=true;
     DataRow dr = ds.Tables[0].Rows[0];
     SNo = dr["SNo"].ToString() ; 
     Godown_ID = dr["Godown_ID"].ToString() ; 
     MobileNo = dr["MobileNo"].ToString() ; 
     Photo = dr["Photo"].ToString() ; 
        //Add by me
       bPhoto=(byte[])dr["Photo"];  
        //
     Date_Time = dr["Date_Time"].ToString() ; 
     Latitude = dr["Latitude"].ToString() ; 
     Longitude = dr["Longitude"].ToString() ; 
     Altitude = dr["Altitude"].ToString() ; 
     Flag = dr["Flag"].ToString() ; 
     Flag1 = dr["Flag1"].ToString() ; 
     Operation = dr["Operation"].ToString() ; 
     User_id = dr["User_id"].ToString() ; 
     Transferred = dr["Transferred"].ToString() ; 
     DateTimeStamp = dr["DateTimeStamp"].ToString() ; 
    }
   }
   ds.Dispose();
 }

 public DataSet select(String where) {

     found=false;
     String qrySelect="SELECT * FROM GPSReceipt where " + where ;
     db.DbParameters.Clear();
     db.Query = qrySelect;
    DataSet ds = db.SelectinDataSet();
    return ds;
}

 public DataSet selectAll() {

     found=false;
     String qrySelect="SELECT * FROM GPSReceipt";
     db.DbParameters.Clear();
     db.Query = qrySelect;
    DataSet ds = db.SelectinDataSet();
    return ds;
}

 public DataSet selectAll(String where) {

     found=false;
     String qrySelect="SELECT * FROM GPSReceipt " + where ;
     db.DbParameters.Clear();
     db.Query = qrySelect;
    DataSet ds = db.SelectinDataSet();
    return ds;
}

 public DataSet selectAny(String selectstring) {

     found=false;
     String qrySelect= selectstring ;
     db.DbParameters.Clear();
     db.Query = qrySelect;
    DataSet ds = db.SelectinDataSet();
    return ds;
}

 public int insert() {

   String qryInsert ="insert into  GPSReceipt (SNo,Godown_ID,MobileNo,Photo,Date_Time,Latitude,Longitude,Altitude,Flag,Flag1,Operation,User_id,Transferred,DateTimeStamp) values(@SNo,@Godown_ID,@MobileNo,@Photo,@Date_Time,@Latitude,@Longitude,@Altitude,@Flag,@Flag1,@Operation,@User_id,@Transferred,@DateTimeStamp)";
   db.DbParameters.Clear();
    db.DbParameters.Add("@SNo",SNo);
    db.DbParameters.Add("@Godown_ID",Godown_ID);
    db.DbParameters.Add("@MobileNo",MobileNo);
    db.DbParameters.Add("@Photo",Photo);
    db.DbParameters.Add("@Date_Time",Date_Time);
    db.DbParameters.Add("@Latitude",Latitude);
    db.DbParameters.Add("@Longitude",Longitude);
    db.DbParameters.Add("@Altitude",Altitude);
    db.DbParameters.Add("@Flag",Flag);
    db.DbParameters.Add("@Flag1",Flag1);
    db.DbParameters.Add("@Operation",Operation);
    db.DbParameters.Add("@User_id",User_id);
    db.DbParameters.Add("@Transferred",Transferred);
    db.DbParameters.Add("@DateTimeStamp",DateTimeStamp);
   db.Query = qryInsert;
   return db.ExecuteNonQuery();
 }

 public int update() {

   String qryUpdate = "update GPSReceipt set SNo=@SNo,Godown_ID=@Godown_ID,MobileNo=@MobileNo,Photo=@Photo,Date_Time=@Date_Time,Latitude=@Latitude,Longitude=@Longitude,Altitude=@Altitude,Flag=@Flag,Flag1=@Flag1,Operation=@Operation,User_id=@User_id,Transferred=@Transferred,DateTimeStamp=@DateTimeStamp where SNo =@SNo";
   db.DbParameters.Clear();
    db.DbParameters.Add("@SNo",SNo);
    db.DbParameters.Add("@Godown_ID",Godown_ID);
    db.DbParameters.Add("@MobileNo",MobileNo);
    db.DbParameters.Add("@Photo",Photo);
    db.DbParameters.Add("@Date_Time",Date_Time);
    db.DbParameters.Add("@Latitude",Latitude);
    db.DbParameters.Add("@Longitude",Longitude);
    db.DbParameters.Add("@Altitude",Altitude);
    db.DbParameters.Add("@Flag",Flag);
    db.DbParameters.Add("@Flag1",Flag1);
    db.DbParameters.Add("@Operation",Operation);
    db.DbParameters.Add("@User_id",User_id);
    db.DbParameters.Add("@Transferred",Transferred);
    db.DbParameters.Add("@DateTimeStamp",DateTimeStamp);
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
 if (compareString[j].Equals("SNo"))  
 query = query + compareString[j] + " = " + "@SNo,"; 
 if (compareString[j].Equals("Godown_ID"))  
 query = query + compareString[j] + " = " + "@Godown_ID,"; 
 if (compareString[j].Equals("MobileNo"))  
 query = query + compareString[j] + " = " + "@MobileNo,"; 
 if (compareString[j].Equals("Photo"))  
 query = query + compareString[j] + " = " + "@Photo,"; 
 if (compareString[j].Equals("Date_Time"))  
 query = query + compareString[j] + " = " + "@Date_Time,"; 
 if (compareString[j].Equals("Latitude"))  
 query = query + compareString[j] + " = " + "@Latitude,"; 
 if (compareString[j].Equals("Longitude"))  
 query = query + compareString[j] + " = " + "@Longitude,"; 
 if (compareString[j].Equals("Altitude"))  
 query = query + compareString[j] + " = " + "@Altitude,"; 
 if (compareString[j].Equals("Flag"))  
 query = query + compareString[j] + " = " + "@Flag,"; 
 if (compareString[j].Equals("Flag1"))  
 query = query + compareString[j] + " = " + "@Flag1,"; 
 if (compareString[j].Equals("Operation"))  
 query = query + compareString[j] + " = " + "@Operation,"; 
 if (compareString[j].Equals("User_id"))  
 query = query + compareString[j] + " = " + "@User_id,"; 
 if (compareString[j].Equals("Transferred"))  
 query = query + compareString[j] + " = " + "@Transferred,"; 
 if (compareString[j].Equals("DateTimeStamp"))  
 query = query + compareString[j] + " = " + "@DateTimeStamp,"; 
  }

query= query.Substring(0, query.Length - 1);
String sqlStr = "update GPSReceipt set " + query + " where SNo =@SNo";
   db.DbParameters.Clear();
    db.DbParameters.Add("@SNo",SNo);
    db.DbParameters.Add("@Godown_ID",Godown_ID);
    db.DbParameters.Add("@MobileNo",MobileNo);
    db.DbParameters.Add("@Photo",Photo);
    db.DbParameters.Add("@Date_Time",Date_Time);
    db.DbParameters.Add("@Latitude",Latitude);
    db.DbParameters.Add("@Longitude",Longitude);
    db.DbParameters.Add("@Altitude",Altitude);
    db.DbParameters.Add("@Flag",Flag);
    db.DbParameters.Add("@Flag1",Flag1);
    db.DbParameters.Add("@Operation",Operation);
    db.DbParameters.Add("@User_id",User_id);
    db.DbParameters.Add("@Transferred",Transferred);
    db.DbParameters.Add("@DateTimeStamp",DateTimeStamp);
   db.Query = sqlStr;
   return db.ExecuteNonQuery();
 }

 public int delete() {

    String qryDelete = " delete from GPSReceipt where SNo =@SNo";
    db.DbParameters.Clear();
    db.DbParameters.Add("@SNo",SNo);
    db.Query = qryDelete;
    return db.ExecuteNonQuery();
   }
  }
 }
