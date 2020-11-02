// JScript File

//Function to cmpare a date with the current date
//Author - brij raj singh
//MICROSOFT Inc.
//Date - 21st March, 2006
function CompareCurDate(Source,args)
  {
  var currentdate;
  var currenttime;
  
  currentdate = new Date();
  currenttime = Date.UTC(y2k(currentdate.getYear()),currentdate.getMonth(),currentdate.getDate());
   
  var InputDate=convertToMSFormat(args.Value);
  args.IsValid = (InputDate <= currenttime);
  }
  
//convert no. to y2k format  
function y2k(number)
 {
  return (number < 1000) ? number + 1900 : number; 
 }

//convert a dd/mm/yy date to y2k formatted date:time Format
function convertToMSFormat(db)
 {

  dateArr = db.split("/");
  dbDate = new Date(01/01/2004); 
  var len=dateArr[0].length;
  if(len > 2)
  { 
    dbDate.setYear(dateArr[0]);
    dbDate.setMonth(dateArr[1]-1);
    dbDate.setDate (dateArr[2]);
  } 
 
 else
  {
   dbDate.setYear(dateArr[2]);
   dbDate.setMonth(dateArr[1]-1); 
   dbDate.setDate(dateArr[0]);
  } 
   dbDate.setMinutes(0);
   dbDate.setHours(0); 
   dbDate.setSeconds (0);
   dbDate.setMilliseconds(0);
   return dbDate.getTime();
 }