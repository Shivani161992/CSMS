<!--
	// This file contains the data validation JavaScript functions
	// It is included in the HTML pages with forms that need these
	// data validation routines.


// DEFINE VARIABLES

// whitespace characters
var whitespace = " \t\n\r";
var defaultEmptyOK = true;

			/*
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	Name		:	getDateStatus
	Purpose		:	This function returns the no of days in particular month of particular year.
	Usage		:	getDateStatus(2,2,2002)
	Arguments	:	month,year
	Return		:	true/false
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
	function getDateStatus(day,month,year)
	{
		if(year<0 )
			return false;
		else
		{
			switch(month)
			{
				case 1:
				case 3:
				case 5:
				case 7:
				case 8:
				case 10:
				case 12:
				{
					if (day <1 || day > 31)
						return false;
					else
						return true;
				}
				case 4:
				case 6:
				case 9:
				case 11:
				{
					if (day <1 || day > 30)
						return false;
					else
						return true;
				}
				case 2:
				{
					if(((year%4==0)&&(year%100!=0))||(year%400==0))
					{
						if(day<0 || day>29)
							return false;
						else
							return true;
					}
					else
					{
						if(day < 0 || day>28)
							return false;
						else
							return true;
					}
				}
				default :
					return false;
			}
		}
	}
			/*
	        ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	        Name		:	validateDate
	        Purpose		:	This function checks for valid date.
	        Usage		:	validateDate("22/09/2001") checks within range of 1900-2099
	        Arguments	:	dateVal	-	String. A String representing a date in whichever locale according to need.
	        Return		:	true/false.
	        ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        */
        function validateDate(oSrc, args)
        {	            
	        var dateString=args.Value;
	        var delimeter = "/";
	        var dayStr;
	        var monthStr;
	        var yearStr;
	        var	strDateArray;
        	
	        alert(args.Value);
	        alert('dateString.indexOf(delimeter) = ' +dateString.indexOf(delimeter));

	        if (dateString.indexOf(delimeter) != -1)
	        {
		        strDateArray = dateString.split(delimeter);
		        alert('strDateArray.length = ' +strDateArray.length );
		        if (strDateArray.length != 3)
			        args.IsValid = false;
		        else
		        {
			        dayStr		= strDateArray[0];
			        monthStr	= strDateArray[1];
			        yearStr		= strDateArray[2];
        			   
		        }
	        }
	        else
		        args.IsValid = false;
		        
		    alert('dayStr.length = ' +dayStr.length);
		    alert('monthStr.length = ' +monthStr.length);
		    alert('yearStr.length = ' +yearStr.length);
		    
	        if(dayStr.length == 0 || monthStr.length == 0 || yearStr.length != 4 )
		        args.IsValid = false;
	        if(isNaN(dayStr))
		        args.IsValid = false;
	        if(isNaN(monthStr))
		        args.IsValid = false;
	        if(isNaN(yearStr))
		        args.IsValid = false;
	        // SR#1 Start
	        // Convert strings to ints .
	        var day   = parseInt(dayStr,10);
	        var month = parseInt(monthStr,10);
	        var year  = parseInt(yearStr,10);
	        // SR#1 End
	        alert('getDateStatus(day,month,year) = ' +getDateStatus(day,month,year));
	        args.IsValid = getDateStatus(day,month,year);
        	
        }
 /*
	        ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	        Author      :   Neha Sah
	        Name		:	DateDif
	        Purpose		:	This function compares two Dates.
	        Arguments	:	DateVal1,DateVal2	 Strings representing the dates which we have to compare.
	        Return		:	true/false.
	        ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        */
function DateDif(objField, objField1)
{	
	
	var strFieldArray = objField.value.split("/");
	var strField1Array = objField1.value.split("/");
	if(parseInt(strFieldArray[2],10)>parseInt(strField1Array[2],10))	//year2 is less than year1
    {	
		//alert("From Date must be greater than To Date");
		objField.focus();
		objField.select();
		return false;
	}else if(parseInt(strFieldArray[2],10)==parseInt(strField1Array[2],10))  //year2 = year1
		{
			
			if(parseInt(strFieldArray[1],10)>parseInt(strField1Array[1],10))	//mon2 is less than mon1
			{
				//alert("From Date must be greater than To Date");
				objField.focus();
				objField.select();
				return false;
			}else
				{																//mon2 = mon1
					if(parseInt(strFieldArray[1],10)==parseInt(strField1Array[1],10))
					{														//date2 is less than date1				
						if(parseInt(strFieldArray[0],10)>parseInt(strField1Array[0],10))
						{		
							//alert("From Date must be greater than To Date");
							objField.focus();
							objField.select();
							return false;
						}
					}
				}
			}
return true;
}
/****************************************************************/

// PURPOSE:  Check to see if the string passed in is a valid time.
//	A valid time is defined as a string which is postfixed with either
//  "PM" or "AM".  Next it checks to see if there is a colon in the
//  string.  If there is, it makes sure that at least one digit preceeds
//  it and two proceed it.

	function IsTime(strTime)
	{
		var strTestTime = new String(strTime);
		strTestTime.toUpperCase();

		var bolTime = true;
		
		bolTime = /^([01][0-9]|[2][0-3])([:.][0-5][0-9])?$/.test(strTestTime);

		//Commneted By vijay on 15 Jan 05 for FCI project as Am PM was not required
		
		//if (strTestTime.indexOf("PM",1) != -1 || strTestTime.indexOf("AM",1))
		//	bolTime = true;

		//if (strTestTime.indexOf(":",0) == 0)
		//	bolTime = false;
		
		
		//var nColonPlace = strTestTime.indexOf(":",1);
		//if (bolTime && ((parseInt(nColonPlace) + 3) < (strTestTime.length - 1) || (parseInt(nColonPlace) + 2) > (strTestTime.length - 1)))
		//	bolTime = false;

		
		return bolTime;
	}

/****************************************************************/

function replaceAll (s, fromStr, toStr)
{
	var new_s = s;
	for (i = 0; i < 100 && new_s.indexOf (fromStr) != -1; i++)
	{
		new_s = new_s.replace (fromStr, toStr);
	}
	return new_s;
}

/****************************************************************/

/* PURPOSE:  Since we are using the single tick mark as the
	string delimiter to construct our SQL queries, a string with
	a tick mark in it will cause a SQL error.  Therefore we replace
	all "'" with "''", which eliminates the possibility of a SQL error.
*/

function sqlSafe (s)
{
	var new_s = s;
	new_s = replaceAll (new_s, "'", "|");
	new_s = replaceAll (new_s, "|", "''");
	new_s = replaceAll (new_s, "\"", "|");
	new_s = replaceAll (new_s, "|", "''");
	return new_s;
}

/****************************************************************/

function makeSafe (i)
{
	i.value = sqlSafe (i.value);
}

/****************************************************************/

// Check whether string s is empty.

function isEmpty(s)
{   return ((s == null) || (s.length == 0))
}

/****************************************************************/

// Returns true if string s is empty or 
// whitespace characters only.

function isWhitespace (s)

{   var i;

    // Is s empty?
    if (isEmpty(s)) return true;

    // Search through string's characters one by one
    // until we find a non-whitespace character.
    // When we do, return false; if we don't, return true.

    for (i = 0; i < s.length; i++)
    {   
	// Check that current character isn't whitespace.
	var c = s.charAt(i);

	if (whitespace.indexOf(c) == -1) return false;
    }

    // All characters are whitespace.
    return true;
}

/****************************************************************/

// isEmail (STRING s [, BOOLEAN emptyOK])
// 
// Email address must be of form a@b.c ... in other words:
// * there must be at least one character before the @
// * there must be at least one character before and after the .
// * the characters @ and . are both required
//
// For explanation of optional argument emptyOK,
// see comments of function isInteger.

function isEmail (s)
{  
	if (isEmpty(s)) 
       	if (isEmail.arguments.length == 1) return defaultEmptyOK;
       	else return (isEmail.arguments[1] == true);
   
    // is s whitespace?
    if (isWhitespace(s)) return false;
    
    // there must be >= 1 character before @, so we
    // start looking at character position 1 
    // (i.e. second character)
    var i = 1;
    var sLength = s.length;

    // look for @
    while ((i < sLength) && (s.charAt(i) != "@"))
    { i++
    }

    if ((i >= sLength) || (s.charAt(i) != "@")) return false;
    else i += 2;

    // look for .
    while ((i < sLength) && (s.charAt(i) != "."))
    { i++
    }

    // there must be at least one character after the .
    if ((i >= sLength - 1) || (s.charAt(i) != ".")) return false;
    else return true;
}


function ForceEmail(objField){
	var strField = new String(objField.value);
	if (!isEmail(strField)) {
		alert("Please enter a valid E-Mail address");
		objField.focus();
		objField.select();
		return false;
	}
	return true;
}

/****************************************************************/

// Checks to see if a required field is blank.  If it is, a warning
// message is displayed...

function ForceEntry(objField, FieldName)
{


	var strField = new String(objField.value);
	
	if (isWhitespace(strField)) {
		alert("You need to enter information for " + FieldName);
		objField.focus();
		objField.select();
		
		return false;
	}

	return true;
}
		
/****************************************************************/

// Checks to see if 2 required fields are blank.  If it is, a warning
// message is displayed...

function CheckBlankEntry(objField, objField1)
{		 	 
	var strField = new String(objField.value);
	var strField1 = new String(objField1.value);
	if (isWhitespace(strField) && isWhitespace(strField1)) {
		alert("You need to enter the information for atleast one of the feilds ");
		objField.focus();
		objField.select();
		return false;
	}

	return true;
}
		
/****************************************************************/

// Checks to see if 2 required fields are both filled or not.  If it is, a warning
// message is displayed...

function CheckDoubleEntry(objField, objField1)
{		 
	var strField = new String(objField.value);
	var strField1 = new String(objField1.value);
	if (!isWhitespace(strField) && !isWhitespace(strField1)) {
		alert("You need to enter the information for only one of the feilds ");
		objField.focus();
		objField.select();
		return false;
	}

	return true;
}
		
/****************************************************************/

// Checks to see if a required field is blank.  If it is, a warning
// message is displayed...

function ForceTime(objField, FieldName)
{
	
	var strField = new String(objField.value);
	
	if (isWhitespace(strField)) {
		alert("You need to enter information for " + FieldName);
		objField.focus();
		objField.select();
		return false;
	}
	if(!IsTime(strField)) {
		alert("You need to enter The time in format Enter time in 24 hour format, eg 4:20 PM should be written as 16:20  for " + FieldName);
		objField.focus();
		objField.select();
		return false;
	}
	
	
	return true;
}


/****************************************************************/

// Returns true if the string passed in is a valid number
//  (no alpha characters), else it displays an error message

function ForceNumber(objField, FieldName)
{
	var strField = new String(objField.value);
	
	if (isWhitespace(strField)) return true;

	var i = 0;

	for (i = 0; i < strField.length; i++)
		if (strField.charAt(i) < '0' || strField.charAt(i) > '9') {
			alert(FieldName + " must be a valid numeric entry.  Please do not use commas or dollar signs or any non-numeric symbols.");
			objField.focus();
			return false;
		}

	return true;
}

/****************************************************************/

// Returns true if the string passed in is a valid money
//  (no alpha characters except a decimal place), 
//   else it displays an error message

function ForceMoney(objField, FieldName)
{
	var strField = new String(objField.value);
	
	if (isWhitespace(strField)) return true;

	var i = 0;

	for (i = 0; i < strField.length; i++)
		if ((strField.charAt(i) < '0' || strField.charAt(i) > '9') && (strField.charAt(i) != '.')) {
			alert(FieldName + " must be a valid numeric entry.  Please do not use commas or dollar signs or any non-numeric symbols.");
			objField.focus();
			objField.select();
			return false;
		}

	return true;
}


/****************************************************************/

// Right trims the string...  Useful for SQL datatypes of CHAR

function RTrim(strTrim)
{
	var str = new String(strTrim);
	var i = 0;
	var c = "";
	var endpos = 0

	for (i = str.length; i >= 0 && endpos == 0; i = i - 1) {
		c = str.charAt(i);
		if (whitespace.indexOf(c) == -1)
			endpos = i;
	}

	return str.substring(0,endpos+1);
}

/****************************************************************/

/* PURPOSE:  Returns true if the string is a valid date number.
	A method is passed in (1 = month, 2 = day).  If the string is
	nonnumeric, false is passed back.  If the day in the date string
	is greater than 31, false is returned.  If the month is greater
	than 12, an error is returned.
*/

function isDateNumber(strNum,method)
{
	var str = new String(strNum);
	var i = 0;
	 
	if (isNaN(parseInt(str)) || parseInt(str) < 0) return false;

	if (method == 1)
		if (parseInt(str) > 31)
			return false;
	if (method == 2)
		if (parseInt(str) > 12)
			return false;

	for (i = 0; i < str.length; i++)
		if (str.charAt(i) < '0' || str.charAt(i) > '9')
			return false;


	return true;
}

/****************************************************************/

// Displays an alert box with the passed in string...

function PromptErrorMsg(Field,strError)
{
	alert("You have entered an invalid date for " + strError + ".  Please make sure your date format is in DD/MM/YYYY format.");
	Field.focus();
}

/****************************************************************/

/* PURPOSE: Checks to see if the string is a valid date.  A valid
	date is defined as any of the following:

		MM/DD/YY, MM/DD/YYYY, M/D/YY, M/D/YYYY,
		MM-DD-YY, MM-DD-YYYY, M-D-YY, M-D-YYYY
*/

function ForceDate(strDate,strField)
{
	var str = new String(strDate.value);
	 
	if (isWhitespace(str)) {
		return true;
		// if the field is empty, just return true...
	}

	var i = 0, count = str.length, j = 0;
	while ((str.charAt(i) != "/" && str.charAt(i) != "-") && i < count)
		i++;

	if (i == count || i > 2) {
		PromptErrorMsg(strDate,strField);
		return false;
	}

	var addOne = false;
	if (i == 2) addOne = true;

	if (!isDateNumber(str.substring(0,i),1)) {
		PromptErrorMsg(strDate,strField);
		return false;
	}

	j = i+1;
	i = 0;

	while ((str.charAt(i+j) != "/" && str.charAt(j+i) != "-") && i+j < count)
		i++;

	if (i+j == count || i > 2) {
		PromptErrorMsg(strDate,strField);
		return false;
	}

	if (!isDateNumber(str.substring(j,i+j),2)) {
		PromptErrorMsg(strDate,strField);
		return false;
	}

	j = i+3;
	i = 0;

	if (addOne) j++;

	while (i+j < count)
		i++;


	if (i != 2 && i != 4) {
		PromptErrorMsg(strDate,strField);
		return false;
	}

	if (!isDateNumber(str.substring(j,i+j),3)) {
		PromptErrorMsg(strDate,strField);
		return false;
	}

	return true;
}

/****************************************************************/

// This function determines if the string passed in is a valid
// US zip code.  It accepts either ##### or #####-####.  If the
// string is valid, it returns true, else false.

function isZipcode(strZip)
{
	var s = new String(strZip);

	if (s.length != 5 && s.length != 10)
		// inappropriate length
		return false;


	for (var i=0; i < s.length; i++)
		if ((s.charAt(i) < '0' || s.charAt(s) > '9') && s.charAt(i) != '-')
			return false;

	return true;
}

/****************************************************************/
function forceMonth(objField,FieldName)
{
     
	var strField = new String(objField.value);
	
	if (parseInt(strField)==0 )
	{
		alert("You have entered an invalid Month,Plz choose correct month and press Submit ");

		/*objField.focus();
		objField.select();
		*/

		return false;

	}
	else
	{
		
		return true;

	}
	
}
/***************************************************************/
// This function determines if the year passed in is a valid
// year  It accepts either ##### or #####-####.  If the
// string is valid, it returns true, else false.

function forceyear(objField,FieldName)
{
    var strField = new String(objField.value);
	
	if ( (parseInt(strField) >=1600) &&(parseInt(strField) <=9999) )
	{
		return true;
	}
	else
	{		// inappropriate length
		alert("You have entered an invalid year,Year must be numeric & greater than 1600 for  " + FieldName);
		objField.focus();
		objField.select();
		return false;
	}
	
}

/****************************************************************/


// This function ensures that a field is less than or equal to the
// Length passed in.  You must call this function with the element
// name in your form (for example: "ForceLength(document.forms[0].txtElement)"
// as opposed to "ForceLength(document.forms[0].txtElement.value)"
// If the field's value is too large, an error message is displayed
// and false is returned, else true is returned.
function ForceLength(objField, nLength, strWarning)
{
	var strField = new String(objField.value);

	if (strField.length > nLength) {
		alert(strWarning);
		return false;
	} else
		return true;
}

//*************************************************************

function ValidateTime(objField, FieldName)
{
	var strField = new String(objField.value);
	if (isWhitespace(strField)) {
		alert("You need to enter information for " + FieldName);
		objField.focus();
		objField.select();
		return false;
	}
	var bool = /^([01][0-9]|[2][0-3])(:[0-5][0-9])?$/.test(strField);
	if(!bool) {
		alert("You need to enter The time in format Enter time in 24 hour format, eg 4:20 PM should be written as 16:20  for " + FieldName);
		objField.focus();
		objField.select();
		return false;
	}
	return true;
}

//*************************************************************


// <FORM NAME="frmSiteRanking" METHOD="GET" ACTION="SiteRanking.asp" ONSUBMIT="return ValidateData(this.form);">
// usage method 
//      function ValidateData(form) {
//		************ Mehtod 1 **********************
//            return (
//            ForceEntry(form.elements["username"],"User Name")&&
//		ForceEmail(form.elements["email"])&& 
//		ForceNumber(form.elements["pw"],"Phone No")
//		)
//************ Mehtod 2 **********************
//	      var CanSubmit = false;
//
//           // Check to make sure that the full name field is not empty.
//           CanSubmit = ForceEntry(document.forms[0].txtName,"You supply a full name.");
//           // Check to make sure ranking is between 1 and 10
//           if (CanSumbit) CanSubmit = ValidRanking();
//
//           return CanSubmit;
//      }
// -->


/*
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	Name		:	validateDate
	Purpose		:	This function checks for valid date.
	Usage		:	validateDate("22/09/2001") checks within range of 1900-2099
	Arguments	:	dateVal	-	String. A String representing a date in whichever locale according to need.
	Return		:	true/false.
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/

function validateDate(objField, FieldName)
{
	var dateString = objField.value;
	var delimeter = "/";
	var dayStr;
	var monthStr;
	var yearStr;
	var	strDateArray;
	var bool;
	if (dateString.indexOf(delimeter) != -1)
	{
		strDateArray = dateString.split(delimeter);
		
		if (strDateArray.length != 3)
			bool = false;
		else
		{
			dayStr		= strDateArray[0];
			monthStr	= strDateArray[1];
			yearStr		= strDateArray[2];
		}
	}
	else
	{
		bool = false;
	}
	
	if(dayStr.length == 0 || monthStr.length == 0 || yearStr.length != 4 )
		bool = false;
	if(isNaN(dayStr))
		bool = false;
	if(isNaN(monthStr))
		bool = false;
	if(isNaN(yearStr))
		bool = false;
	// SR#1 Start
	// Convert strings to ints .
	var day   = parseInt(dayStr,10);
	var month = parseInt(monthStr,10);
	var year  = parseInt(yearStr,10);
	// SR#1 End
	bool = getDateStatus(day,month,year);
	if(!bool)
	{
		alert("Invalid Date format for the feild "+ FieldName);
		objField.focus();
		objField.select();
		return false;
	}
	return true;
}


/*
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	Name		:	getDateStatus
	Purpose		:	This function returns the no of days in particular month of particular year.
	Usage		:	getDateStatus(2,2,2002)
	Arguments	:	month,year
	Return		:	true/false
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
	function getDateStatus(day,month,year)
	{
		if(year<0 )
			return false;
		else
		{
			switch(month)
			{
				case 1:
				case 3:
				case 5:
				case 7:
				case 8:
				case 10:
				case 12:
				{
					if (day <1 || day > 31)
						return false;
					else
						return true;
				}
				case 4:
				case 6:
				case 9:
				case 11:
				{
					if (day <1 || day > 30)
						return false;
					else
						return true;
				}
				case 2:
				{
					if(((year%4==0)&&(year%100!=0))||(year%400==0))
					{
						if(day<0 || day>29)
							return false;
						else
							return true;
					}
					else
					{
						if(day < 0 || day>28)
							 
							return false;
						else
							return true;
					}
				}
				default :
					return false;
			}
		}
	}