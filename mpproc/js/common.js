/*
	###############################################################################################
	File Name : common.js
	Author    : Shailesh Verma
	Purpose   : This file contains some javascript functions that are used more than once
				in the project and hence are common.
	Comments  : Please do not include any javascript function in this file which is not used
	            more than once. Also, add functions to this file only after thorough testing.
	###############################################################################################

**********************************************************************
      Updation Log:
*****************************************************************************************************************
S.No.	Version	| Modified By	| Date		| Purpose				|  Modification Description 
-----------------------------------------------------------------------------------------------------
1.		BDSv1.1	| Naveen Dhall	|26-05-2003	| ValiDate Date				|  Update Validate Date function.
*****************************************************************************************************************
2.		BDSv1.1	| Prasanna   	|26-05-2003	| to show the filed name properly|
*****************************************************************************************************************

*/



// This function is used to align the row-height of two tables having exactly
// the same rows (not necessarily the columns).
function adjustTableRows( tblLeft, tblRight ){
	var IE5 = (document.all && document.getElementById);
	var NS6 = (document.getElementById && !document.all);

	if( IE5 ){
		tblLeft  = document.all[tblLeft];
		tblRight = document.all[tblRight];

		if( tblLeft && tblRight ){
			for( ctr_ATR=0; ctr_ATR<tblLeft.rows.length; ctr_ATR++ ){
				tblRight.rows[ctr_ATR].height = tblLeft.rows[ctr_ATR].offsetHeight;
			}
		}
	}else if( NS6 ){
		tblLeft  = document.getElementById( tblLeft );
		tblRight = document.getElementById( tblRight );

		if( tblLeft && tblRight ){
			for( ctr_ATR=0; ctr_ATR<tblLeft.rows.length; ctr_ATR++ ){
				tblRight.rows[ctr_ATR].cells[0].style.height = tblLeft.rows[ctr_ATR].offsetHeight;
			}
		}
	}
}


/*
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	Name		:	popUpWindow
	Purpose		:	This function pops up a new window.
	Usage		:	popUpWindow( "somepath/somefile.php", 500, 250, "_myWindow" );
	Arguments:
		url		-	String. The url that will open in the poped up window.
		width		-	integer. width of the window.
		height		-	integer. height of the window.
		[windName]	-	String. Pass this argument only if a fresh window is to be
					poped up. This argument	should not contain any spaces.
	Return		:	void.
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/

function adjust(){
	width  =  0;
	height = 0;
	if( navigator.appName.indexOf("Microsoft") != -1 ){ // Microsoft
		width  = document.body.scrollWidth+26;
		height = document.body.scrollHeight+30;
	}else{
		width  = document.width+26;
		height = document.height+30;
	}

	// Put condition here
	width  = ( width  > screen.availWidth  ? screen.availWidth  : width );
	height = ( height > screen.availHeight ? screen.availHeight : height );
	
	x = (screen.availWidth/2)  - (width/2);
	y = (screen.availHeight/2) - (height/2);

	window.moveTo( x, y );
	window.resizeTo( width, height );
}

function popUpWindow(url, width, height, windName){
	wind = window.open(url, (windName ? windName : "_sameWindow"), "width=" +width+ ",height=" +height+ ",scrollbars=yes");
	x = (screen.availWidth/2)  - (width/2);
	y = (screen.availHeight/2) - (height/2);
	wind.moveTo(x, y);
	wind.resizeTo( width, (height+23) );
	wind.focus();
}
function popUpWindowAdobe(url, width, height, windName)
{
	wind = window.open(url, (windName ? windName : "_sameWindow"), "width=" +width+ ",height=" +height+ ",scrollbars=yes,resizable=1");
	x = (screen.availWidth/2)  - (width/2);
	y = (screen.availHeight/2) - (height/2);
	wind.moveTo(x, y);
	wind.resizeTo( width, (height+23) );
	wind.focus();
}

/*
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	Name		:	validateRequireds
	Purpose		:	This function checks for any blank field. If a blank field is found
				then it alerts the user to fill it.
	Usage		:	validateRequireds(document.formName, 'firstName', 'lastName', 'emailID');
	Arguments	:	variable # of arguments with first argument being the <form> reference
				(Object) and rest of the arguments as form field names(String).
	Return		:	Boolean. true if all the fields contain some data, otherwise false.
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
function validateRequireds(){
	var vRFormName = "";
	var vMissingInfo= "";
	var vFocusCounter=0;

	if(arguments.length > 1){
		vRFormName = arguments[0];
		for(vRCounter=1; vRCounter<arguments.length; vRCounter++){
			if( vRFormName[arguments[vRCounter]] )
			{	
				//Added by pratihari to check the drop down list
				
				if(vRFormName[arguments[vRCounter]].type == "select-one")
				{
					if(vRFormName[arguments[vRCounter]].selectedIndex == 0)
					{
						vMissingInfo += "\n     -  "+convertVariable(vRFormName[arguments[vRCounter]].name);
						if(vFocusCounter==0)
						  vFocusCounter=vRCounter;						
					}
				}
				else if(vRFormName[arguments[vRCounter]].type == "text")
				{
					if(trimSpaces( vRFormName[arguments[vRCounter]].value).length == 0)
					{
						vMissingInfo += "\n     -  "+convertVariable(vRFormName[arguments[vRCounter]].name);
						if(vFocusCounter==0)
							vFocusCounter=vRCounter;
					}
				}
			}
		}
		if (vMissingInfo != ""){
			vMissingInfo ="_____________________________\n" + "You failed to fill in your required field(s):\n" +
			vMissingInfo +"\n_____________________________" + "\n Please re-enter and submit again!";
			alert(vMissingInfo);
			vRFormName[arguments[vFocusCounter]].focus();
			return false;
		}
	}
	return true;
}

//Added by pratihari
function validateRequiredsDotNet(){
	var vRFormName = "";
	var vMissingInfo= "";
	var vFocusCounter=0;

	if(arguments.length > 1){
		vRFormName = arguments[0];
		for(vRCounter=1; vRCounter<arguments.length; vRCounter++){
			if( vRFormName[arguments[vRCounter]] )
			{
				if(vRFormName[arguments[vRCounter]].type == "select-one")
				{
					if(vRFormName[arguments[vRCounter]].selectedIndex == 0)
					{
						vMissingInfo += "\n     -  "+convertVariableDotNet(vRFormName[arguments[vRCounter]].name);
						if(vFocusCounter==0)
						  vFocusCounter=vRCounter;						
					}
				}
				else if(vRFormName[arguments[vRCounter]].type == "text")
				{
					if(trimSpaces( vRFormName[arguments[vRCounter]].value).length == 0)
					{
						vMissingInfo += "\n     -  "+convertVariableDotNet(vRFormName[arguments[vRCounter]].name);
						if(vFocusCounter==0)
							vFocusCounter=vRCounter;
					}
				}
			}
		}
		if (vMissingInfo != ""){
			vMissingInfo ="_____________________________\n" + "You failed to fill in your required field(s):\n" +
			vMissingInfo +"\n_____________________________" + "\n Please re-enter and submit again!";
			alert(vMissingInfo);
			vRFormName[arguments[vFocusCounter]].focus();
			return false;
		}
	}
	return true;
}


/*
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	Name		:	validateText
	Purpose		:	This function checks for text.
	Usage		:	validateText(document.formName, 'firstName', 'lastName', 'emailID');
	Arguments	:	variable # of arguments with first argument being the <form> reference
					(Object) and rest of the arguments as form field names(String).
	Return		:	Boolean. true if all the fields contain valid text data, otherwise false.
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
function validateText(){
	var vRFormName = "";
	var vMissingInfo= "";
	var vFocusCounter=0;

	if(arguments.length > 1)
	{
		vRFormName = arguments[0];
		for(vRCounter=1; vRCounter<arguments.length; vRCounter++)
		{
			if(isValidText(trimSpaces(vRFormName[arguments[vRCounter]].value)) == false)
			{
				vMissingInfo += "\n     -  "+convertVariable(vRFormName[arguments[vRCounter]].name);
				if(vFocusCounter==0)
					vFocusCounter=vRCounter;
			}
		}
		if (vMissingInfo != "")
		{
			vMissingInfo ="_________________________________\n" + "You failed to fill correct Text values:\n" +
			vMissingInfo +"\n_________________________________" + "\n Please re-enter and submit again!";
			alert(vMissingInfo);
			vRFormName[arguments[vFocusCounter]].focus();
			return false;
		}
	}
	return true;
}


/*
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	Name		:	validateAlphaNumeric
	Purpose		:	This function checks for alphanumeric values. I
	Usage		:	validateAlphaNumeric(document.formName, 'firstName', 'lastName', 'emailID');
	Arguments	:	variable # of arguments with first argument being the <form> reference
					(Object) and rest of the arguments as form field names(String).
	Return		:	Boolean. true if all the fields contain alphanumeric data, otherwise false.
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
function validateAlphaNumeric(){
	var vRFormName = "";
	var vMissingInfo= "";
	var vFocusCounter=0;

	if(arguments.length > 1)
	{
		vRFormName = arguments[0];
		for(vRCounter=1; vRCounter<arguments.length; vRCounter++)
		{
			if(isValidAlphaNumeric(trimSpaces(vRFormName[arguments[vRCounter]].value)) == false)
			{
				vMissingInfo += "\n     -  "+convertVariable(vRFormName[arguments[vRCounter]].name);
				if(vFocusCounter==0)
					vFocusCounter=vRCounter;
			}
		}
		if (vMissingInfo != "")
		{
			vMissingInfo ="_________________________________\n" + "You failed to fill correct AlphaNumeric values:\n" +
			vMissingInfo +"\n_________________________________" + "\n Please re-enter and submit again!";
			alert(vMissingInfo);
			vRFormName[arguments[vFocusCounter]].focus();
			return false;
		}
	}
	return true;
}
/*
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	Name		:	validateAlphaNumericDotNet
	Purpose		:	This function checks for alphanumeric values. I
	Usage		:	validateAlphaNumeric(document.formName, 'firstName', 'lastName', 'emailID');
	Arguments	:	variable # of arguments with first argument being the <form> reference
					(Object) and rest of the arguments as form field names(String).
	Return		:	Boolean. true if all the fields contain alphanumeric data, otherwise false.
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
function validateAlphaNumericDotNet(){
	var vRFormName = "";
	var vMissingInfo= "";
	var vFocusCounter=0;

	if(arguments.length > 1)
	{
		vRFormName = arguments[0];
		for(vRCounter=1; vRCounter<arguments.length; vRCounter++)
		{
			if(isValidAlphaNumeric(trimSpaces(vRFormName[arguments[vRCounter]].value)) == false)
			{
				vMissingInfo += "\n     -  "+convertVariableDotNet(vRFormName[arguments[vRCounter]].name);
				if(vFocusCounter==0)
					vFocusCounter=vRCounter;
			}
		}
		if (vMissingInfo != "")
		{
			vMissingInfo ="_________________________________\n" + "You failed to fill correct AlphaNumeric values:\n" +
			vMissingInfo +"\n_________________________________" + "\n Please re-enter and submit again!";
			alert(vMissingInfo);
			vRFormName[arguments[vFocusCounter]].focus();
			return false;
		}
	}
	return true;
}
/*
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	Name		:	validatePhone
	Purpose		:	This function checks for Phone values. I
	Usage		:	validatePhone(document.formName, 'firstName', 'lastName', 'emailID');
	Arguments	:	variable # of arguments with first argument being the <form> reference
					(Object) and rest of the arguments as form field names(String).
	Return		:	Boolean. true if all the fields contain Phone data, otherwise false.
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
function validateMobile(){
	var vRFormName = "";
	var vMissingInfo= "";
	var vFocusCounter=0;

	if(arguments.length > 1)
	{
		vRFormName = arguments[0];
		for(vRCounter=1; vRCounter<arguments.length; vRCounter++)
		{
			if(isValidMobile(trimSpaces(vRFormName[arguments[vRCounter]].value)) == false)
			{
				vMissingInfo += "\n     -  "+convertVariable(vRFormName[arguments[vRCounter]].name);
				if(vFocusCounter==0)
					vFocusCounter=vRCounter;
			}
		}
		if (vMissingInfo != "")
		{
			vMissingInfo ="_________________________________\n" + "You failed to fill correct Mobile values:\n" +
			vMissingInfo +"\n_________________________________" + "\n Please re-enter and submit again!";
			alert(vMissingInfo);
			vRFormName[arguments[vFocusCounter]].focus();
			return false;
		}
	}
	return true;
}


/*
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	Name		:	validateDoubles
	Purpose		:	This function checks for integer and floating point values.
	Usage		:	validateDoubles(document.formName, 'dec', 'mortgageAmount', 'loanAmount')
	Arguments	:
			formObj		-	Object. The <form> object.
			arg_DataType	-	String. "int" for integer and "dec" for floating point values.
			****		-	String. Rest of the arguments should be the names of
						the form fields that have to be validated.
	Return		:	Boolean. true if all values are valid otherwise false.
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/

function validateDoubles(formObj, arg_DataType){
	vDSResult = true;
	arg_DataType = arg_DataType.toLowerCase();
	var vMissingInfo= "";
	var vFocusObject="";




	for(vDSCounter=2; vDSCounter<arguments.length; vDSCounter++)
	{
		vDSObj = formObj[arguments[vDSCounter]];
		if(vDSObj)
		{
			for(fvDSCounter=0; fvDSCounter<vDSObj.value.length; fvDSCounter++)
			{
				fObjVal = vDSObj.value.charAt(fvDSCounter);
				if(!(fObjVal >= '0' && fObjVal <= '9'))
				{ // If not in between 0-9
					if(arg_DataType == "dec" && fObjVal != ".")
					{
						vDSResult = false;
						vMissingInfo += "\n     -  "+convertVariable(vDSObj.name);
						if(vFocusObject=="")
							vFocusObject=vDSObj;
						break;
					}

					if(arg_DataType == "int")
					{
						vDSResult = false;
						vMissingInfo += "\n     -  "+convertVariable(vDSObj.name);
						if(vFocusObject=="")
							vFocusObject=vDSObj;
						break;
					}
				}
			}
		}
	}
	if (vMissingInfo != "")
	{
		vMissingInfo ="_________________________________\n" + "You failed to fill correct values:\n" +
		vMissingInfo +"\n_________________________________" + "\n Please re-enter and submit again!";
		alert(vMissingInfo);
		vFocusObject.focus();
		return false;
	}
	return true;
}

/*
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	Name		:	validateDoublesDotNet
	Purpose		:	This function checks for integer and floating point values.
	Usage		:	validateDoublesDotNet(document.formName, 'dec', 'mortgageAmount', 'loanAmount')
	Arguments	:
			formObj		-	Object. The <form> object.
			arg_DataType	-	String. "int" for integer and "dec" for floating point values.
			****		-	String. Rest of the arguments should be the names of
						the form fields that have to be validated.
	Return		:	Boolean. true if all values are valid otherwise false.
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/

function validateDoublesDotNet(formObj, arg_DataType){
	vDSResult = true;
	arg_DataType = arg_DataType.toLowerCase();
	var vMissingInfo= "";
	var vFocusObject="";




	for(vDSCounter=2; vDSCounter<arguments.length; vDSCounter++)
	{
		vDSObj = formObj[arguments[vDSCounter]];
		if(vDSObj)
		{
			for(fvDSCounter=0; fvDSCounter<vDSObj.value.length; fvDSCounter++)
			{
				fObjVal = vDSObj.value.charAt(fvDSCounter);
				if(!(fObjVal >= '0' && fObjVal <= '9'))
				{ // If not in between 0-9
					if(arg_DataType == "dec" && fObjVal != ".")
					{
						vDSResult = false;
						vMissingInfo += "\n     -  "+convertVariableDotNet(vDSObj.name);
						if(vFocusObject=="")
							vFocusObject=vDSObj;
						break;
					}

					if(arg_DataType == "int")
					{
						vDSResult = false;
						vMissingInfo += "\n     -  "+convertVariableDotNet(vDSObj.name);//changed the function convertvariable to convertvariabledotnet()::mitali 1/3/04
						if(vFocusObject=="")
							vFocusObject=vDSObj;
						break;
					}
				}
			}
		}
	}
	if (vMissingInfo != "")
	{
		vMissingInfo ="_________________________________\n" + "You failed to fill correct values:\n" +
		vMissingInfo +"\n_________________________________" + "\n Please re-enter and submit again!";
		alert(vMissingInfo);
		vFocusObject.focus();
		return false;
	}
	return true;
}
function validateMessage( strMessage , formObj, arg_DataType){
	vDSResult = true;
	arg_DataType = arg_DataType.toLowerCase();
	var vMissingInfo= "";
	var vFocusObject="";

	for(vDSCounter=2; vDSCounter<arguments.length; vDSCounter++)
	{
		vDSObj = formObj[arguments[vDSCounter]];
		if(vDSObj)
		{
			for(fvDSCounter=0; fvDSCounter<vDSObj.value.length; fvDSCounter++)
			{
				fObjVal = vDSObj.value.charAt(fvDSCounter);
				if(!(fObjVal >= '0' && fObjVal <= '9'))
				{ // If not in between 0-9
					if(arg_DataType == "dec" && fObjVal != ".")
					{
						vDSResult = false;
						vMissingInfo += "\n     -  "+strMessage;
						if(vFocusObject=="")
							vFocusObject=vDSObj;
						break;
					}

					if(arg_DataType == "int")
					{
						vDSResult = false;
						vMissingInfo += "\n     -  "+strMessage;
						if(vFocusObject=="")
							vFocusObject=vDSObj;
						break;
					}
				}
			}
		}
	}
	if (vMissingInfo != "")
	{
		vMissingInfo ="_________________________________\n" + "You failed to fill correct values:\n" +
		vMissingInfo +"\n_________________________________" + "\n Please re-enter and submit again!";
		alert(vMissingInfo);
		vFocusObject.focus();
		return false;
	}
	return true;
}
/*
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	Name		:	SSNValidation
	Purpose		:	This function checks for a valid SSN.
	Usage		:	SSNValidation(ssn);
	Arguments	:
	Return		:	Boolean. true if valid SSN address otherwise false.
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/

function SSNValidation(ssn) {
var matchArr = ssn.match(/^(\d{3})-?\d{2}-?\d{4}$/);
var numDashes = ssn.split('-').length - 1;
if (matchArr == null || numDashes == 1 || numDashes == 0) {
alert('Invalid SSN. Must be 9 digits in the format (###-##-####).');
msg = "does not appear to be valid";
return false;
}
else 
if (parseInt(matchArr[1],10)==0) {
alert("Invalid SSN: SSN's can't start with 000.");
return false;
}
return true;
}


/*
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	Name		:	validateEmail
	Purpose		:	This function checks for a valid email address.
	Usage		:	validateEmail( "some-id@some-domain.ext" );
	Arguments	:
			email	-	String. A String representing an email address.
	Return		:	Boolean. true if valid email address otherwise false.
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/

function validateEmail(email){
	invalidChars = " /:,;"
	if(email == ""){                 //email cannot be empty
		return false;
	}

	for(i=0; i<invalidChars.length; i++){ //check for invalid characters
		badChar = invalidChars.charAt(i);
		if(email.indexOf(badChar,0) != -1){
			return false;
		}
	}

	atPos = email.indexOf("@",1);         //there must be one "@" symbol
	if(atPos == -1){
		return false;
	}
	if(email.indexOf("@",atPos+1) != -1){ //check to make sure only one "@" symbol
		return false;
	}

	periodPos = email.indexOf(".",atPos);
	if (periodPos == -1){ // make sure there is one "." after the "@"
		return false;
	}

	if(periodPos+3 > email.length){ // must be at least 2 chars after the "."
		return false;
	}
	return true;
}


/*
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	Name		:	validateEmail
	Purpose		:	This function checks for a valid email address.
	Usage		:	validateEmail( "some-id@some-domain.ext" );
	Arguments	:
			email	-	String. A String representing an email address.
	Return		:	Boolean. true if valid email address otherwise false.
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/

function validateEmailAllowNull(email){
	invalidChars = " /:,;";
	for(i=0; i<invalidChars.length; i++){ //check for invalid characters
		badChar = invalidChars.charAt(i);
		if(email.indexOf(badChar,0) != -1){
			return false;
		}
	}

	atPos = email.indexOf("@",1);         //there must be one "@" symbol
	if(atPos == -1){
		return false;
	}
	if(email.indexOf("@",atPos+1) != -1){ //check to make sure only one "@" symbol
		return false;
	}

	periodPos = email.indexOf(".",atPos);
	if (periodPos == -1){ // make sure there is one "." after the "@"
		return false;
	}

	if(periodPos+3 > email.length){ // must be at least 2 chars after the "."
		return false;
	}
	return true;
}



/*
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	Name		:	validateSelects
	Purpose		:	This function forces selection of options other than first one in <select> controls.
				Prompts the user to choose another option if the user has selected the first option.
	Usage		:	validateSelects(document.formName, 'selectObj1', 'selectObj2', 'selectObjN');
	Arguments	:
			formObj	-	Object. A reference to the <form> object.
			***	-	String. Rest of the arguments should be <select> Object names(String).
	Return		:	Boolean. true if some other option is chosen but the first one in each of the
				<select> controls, otherwise false.
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/

function validateSelects(formObj){
	vSSResult = true;
	for(vSSCounter=1; vSSCounter<arguments.length; vSSCounter++){
		vSSObj = formObj[arguments[vSSCounter]];
		if( vSSObj && vSSObj.selectedIndex == 0 ){
			alert( "Please choose another option from '" +convertVariable(vSSObj.name)+ "'" );
			vSSObj.focus();
			vSSResult = false;
			break;
		}
	}
	return vSSResult;
}


/*
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	Name		:	validateDate
	Purpose		:	This function checks for valid date.
	Usage		:	validateDate("22/09/2001") checks within range of 1900-2099
	Arguments	:	dateVal	-	String. A String representing a date in whichever locale according to need.
	Return		:	true/false.
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


function validateDate(dateString)
{
	var delimeter = "/";
	var dayStr;
	var monthStr;
	var yearStr;
	var	strDateArray;

	if (dateString.indexOf(delimeter) != -1)
	{
		strDateArray = dateString.split(delimeter);
		if (strDateArray.length != 3)
			return false;
		else
		{
			dayStr		= strDateArray[1];
			monthStr	= strDateArray[0];
			yearStr		= strDateArray[2];
			
		}
	}
	else
		return false;

	if(dayStr.length == 0 || monthStr.length == 0 || yearStr.length != 4 )
		return false;
	if(isNaN(dayStr))
		return false;
	if(isNaN(monthStr))
		return false;
	if(isNaN(yearStr))
		return false;
	// SR#1 Start
	// Convert strings to ints .
	var day   = parseInt(dayStr,10);
	var month = parseInt(monthStr,10);
	var year  = parseInt(yearStr,10);
	// SR#1 End
	return getDateStatus(day,month,year);
}
*/
// This function validates a number of dates
function validateDates( arg_FormObj ){
	for( vDCounter = 1; vDCounter<arguments.length; vDCounter++ ){
		if( arg_FormObj[arguments[vDCounter]] && arg_FormObj[arguments[vDCounter]].value.length > 0 ){
			if( !validateDate(arg_FormObj[arguments[vDCounter]].value ) ){
				alert( "Invalid Date." ) ;
				arg_FormObj[arguments[vDCounter]].focus();
				return false;
			}
		}
	}
	return true;
}


/*
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	Name		:	validateRange
	Purpose		:	This function checks that firstNumber is greater than secondNumber.
	Usage		:	validateRange(document.formName, 'firstNumber', 'secondNumber' , 'thirdNumber', 'fourthNumber');
	Arguments	:	firstNumber,secondNumber,thirdNumber,fourthNumber and so on..
	Return		:	Boolean. true if firstNumber <= secondNumber, otherwise false.
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
function validateRange(){
	var vRFormName = "";
	var vMissingInfo= "";
	var vFocusCounter=0;

	if(arguments.length > 1)
	{
		vRFormName = arguments[0];
		for(vRCounter=1; vRCounter<arguments.length; vRCounter=vRCounter+2)
		{
			if(eval(trimSpaces(vRFormName[arguments[vRCounter]].value)) > eval(trimSpaces(vRFormName[arguments[vRCounter+1]].value)))
			{
				vMissingInfo += "\n     -  "+convertVariable(vRFormName[arguments[vRCounter]].name)+" should <= "+ convertVariable(vRFormName[arguments[vRCounter+1]].name);
				if(vFocusCounter==0)
					vFocusCounter=vRCounter;
			}
		}
		if (vMissingInfo != "")
		{
			vMissingInfo ="_____________________________\n" + "You failed to fill in required range:\n" +
			vMissingInfo +"\n_____________________________" + "\n Please re-enter and submit again!";
			alert(vMissingInfo);
			vRFormName[arguments[vFocusCounter]].focus();
			return false;
		}
	}
	return true;
}



/*
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	Name		:	validatePassword
	Purpose		:	This function checks that passwords validity
	Usage		:	validatePassword(document.formName, 'password1', 'password2');
	Arguments	:	password1,password2.
	Return		:	Boolean. true if passwords are equal and in alphanumeric format, otherwise false.
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
function validatePassword(){
	var vRFormName = "";
	var vMissingInfo= "";

	if(arguments.length > 1)
	{
		vRFormName = arguments[0];

	/*	if(isValidAlphaNumeric(trimSpaces(vRFormName[arguments[1]].value)==false)
			vMissingInfo += "\n     -  "+convertVariable(vRFormName[arguments[1]].name)+" is not AlphaNumeric.";
		if(isValidAlphaNumeric(trimSpaces(vRFormName[arguments[2]].value)==false)
			vMissingInfo += "\n     -  "+convertVariable(vRFormName[arguments[2]].name)+" is not AlphaNumeric.";
	*/
		if(trimSpaces(vRFormName[arguments[1]].value) != trimSpaces(vRFormName[arguments[2]].value))
			vMissingInfo += "\n     -  Passwords are not same.";

		if (vMissingInfo != "")
		{
			vMissingInfo ="_____________________________\n" + "You failed to fill in valid passwords:\n" +
			vMissingInfo +"\n_____________________________" + "\n Please re-enter and submit again!";
			alert(vMissingInfo);
			vRFormName[arguments[1]].focus();
			return false;
		}
	}
	return true;
}




/*
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	Name		:	trimSpaces
	Purpose		:	This function removes any leading & trailing blanks from a string.
	Usage		:	trimSpaces(document.formName.firstName.value);
	Arguments	:
			str	-	String. The string to be trimmed.
	Return		:	String. Having all leading and trailing blanks removed.
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/

function trimSpaces(str){
	var rtn = "";
	var len = str.length;
	var i = 0;
	var startLoc = 0;
	var endLoc = 0;
	var started = false;

	for (i=0; (i < len) && (!started) ; i++){
		if (str.charAt(i) != ' '){
			started = true;
			startLoc = i;
		}
	}

	if (!started) return rtn;

	started = false;
	for (i=len-1; (i > -1) && (!started) ; i--){
		if (str.charAt(i) != ' '){
			started = true;
			endLoc = i + 1;
		}
	}

	for (i=startLoc ; i<endLoc; i++){
		rtn = rtn+str.charAt(i);
	}
	return rtn;
}

/*
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	Name		:	convertVariable
	Purpose		:	This function formats the name of a variable so that it could be used
				in a message. e.g. it converts "userName" to "User Name"
				This function will mainly be used by another function in this file.
	Usage		:	convertVariable("userName");
	Arguments	:
			varName	-	String. The name of the variable to be formatted.
	Return		:	String. A formatted variable name.
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/

	function convertVariable(varName){
		varNewName = "";
		if( varName.indexOf("_") != -1 ){
			varName = varName.substring( varName.indexOf("_")+1 );
		}
		
		for(j=0; j<varName.length; j++){
			prevCh = varName.charAt(j-1);
			ch = varName.charAt(j);
			if( (ch >= 'A' && ch <= 'Z') && (j != 0) && !(prevCh >= 'A' && prevCh <= 'Z') ){
				varNewName += " " +ch;
			}else{
				varNewName += ch;
			}
		}
		varName = varNewName;
		varNewName = varName.charAt(0).toUpperCase();
		varNewName += varName.substring(1);
		return varNewName;
	}

	//Added By Pratihari on 19/12/2003
	function convertVariableDotNet(varName){
		varNewName = "";
		if( varName.indexOf("_") != -1 ){
			varName = varName.substring( varName.indexOf("_")+1 );
		}
		
		/*
		 ******************************************************
		 * Added by : Prasanna Pratihari                      *
		 * Added on : 19/12/2003                              *
		 * Desc     : For dotnet specific only                *
		 * if the field nameBeginig with txt(For text box),   *
		 * ddl(For DropDown List),chk(For Check box)          *
		 * rad(For Redi.buttonSmall) remove it from the name       *		
		 ******************************************************
		 */
		 
		var strExp
		var objRegExp = /(^txt)/;
		if(objRegExp.test(varName))
		{
			varName = varName.substring(3,varName.length);
		}
		
		objRegExp = /(^ddl)/;
		if(objRegExp.test(varName))
		{
			varName = varName.substring(3,varName.length);
		}

		objRegExp = /(^chk)/;
		if(objRegExp.test(varName))
		{
			varName = varName.substring(3,varName.length);
		}				

		objRegExp = /(^rad)/;
		if(objRegExp.test(varName))
		{
			varName = varName.substring(3,varName.length);
		}
		
		for(j=0; j<varName.length; j++){
			prevCh = varName.charAt(j-1);
			ch = varName.charAt(j);
			if( (ch >= 'A' && ch <= 'Z') && (j != 0) && !(prevCh >= 'A' && prevCh <= 'Z') ){
				varNewName += " " +ch;
			}else{
				varNewName += ch;
			}
		}
		varName = varNewName;
		varNewName = varName.charAt(0).toUpperCase();
		varNewName += varName.substring(1);
		return varNewName;
	}




/*
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	Name		:	moveOptions
	Purpose		:	This function moves the option from one <select> object to the other one.
	Usage		:	moveOptions(document.formName.sourceSelectObject, document.formName.destinationSelectObject);
	Arguments	:
			objSource	-	Object. A reference to the source <select> object from which the selected
						option has to move to the destination <select> object.
			objDestination	-	Object. A reference to the destination <select> object to which the selected
						option has to come from the source <select> object.
	Return		:	void.
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/

function moveOptions( objSource, objDestination ){
	if( objSource.selectedIndex != -1){
		for( mo_Ctr=(objSource.options.length-1); mo_Ctr>=0; mo_Ctr-- ){
			if( objSource.options[mo_Ctr].selected ){
				t_Opt = objSource.options[mo_Ctr];
				option = new Option( t_Opt.text, t_Opt.value);

				objDestination.options.length++;
				objDestination.options[objDestination.options.length-1] = option;
				objSource.options[mo_Ctr] = null;
			}
		}
	}else{
		alert( "Please select an option." );
		objSource.focus();
	}
}

/*
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	Name		:	validatePeriod
	Purpose		:	This function checks whether the first date is after the second date.
	Usage		:	validatePeriod("22/09/2001", "22/09/2002")
	Arguments	:
			startDate	-	String. A String representing a date in UK locale.
			termDate	-	String. A String representing a date in UK locale.
	Return		:	Boolean. true if first date is before or equal to the second date otherwise false.
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/

function validatePeriod(startDate, termDate){
	date1 = new Date( swapDayMonth(startDate) );
	date2 = new Date( swapDayMonth(termDate) );
	return (((date2 - date1) < 0) ? false : true);
}


function validatePeriodNew(startDate, termDate){
	date1 = new Date( startDate );
	date2 = new Date( termDate );
	return (((date2 - date1) < 0) ? false : true);
}

/*
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	Name		:	swapDayMonth
	Purpose		:	This function swaps day and month in date.
				i.e., converts "22/02/2001 10:20:00" to "02/22/2001 10:20:00"
	Usage		:	swapDayMonth("22/09/2001")
	Arguments	:
			dateVal	-	String. A String representing a date in whichever locale according to need.
	Return		:	String. Represeting a date having its day and month swapped.
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/

function swapDayMonth(dateVal){
	firstIndex = dateVal.indexOf("/");
	dd = dateVal.substring(0, firstIndex);
	secondIndex = dateVal.indexOf("/", (firstIndex+1));
	mm = dateVal.substring((firstIndex+1), secondIndex);
	yy = dateVal.substring((secondIndex+1));
	return (mm+"/"+dd+"/"+yy);
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




/*
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	Name		:	isValidText
	Purpose		:	This function checks for text validation.
					This function will return a false value if the parameter passed
					in contains a non-text character
	Usage		:	isValidText("text")
	Arguments	:	text
	Return		:	true/false
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
function isValidText(aValue){
	var i=0;
	var temp="";
	var test="";

	for (i=0; i < aValue.length; i++)
  	{
    	temp = aValue.substring(i, i+1);
    	if (((temp<"a" || temp>"z") && (temp<"A" || temp>"Z") && temp != ' ') && temp != "-")
    		test ="no";
    }
    if (test != "" )
		return false;
	else
		return true;
}

/*
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	Name		:	isValidAlphaNumeric
	Purpose		:	This function checks for alphanumeric validation.
					This function will return a false value if the parameter passed
					in contains a non-alphanumeris character.
	Usage		:	isValidAlphaNumeric("text")
	Arguments	:	text
	Return		:	true/false
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
function isValidAlphaNumeric(aValue)
{
	var i=0;
	var temp="";
	var test="";

	for (i=0; i < aValue.length; i++)
  	{
    	temp = aValue.substring(i, i+1);
    	if (((temp < "0" || temp > "9") && (temp<"a" || temp>"z") && (temp<"A" || temp>"Z") && temp != ' ' && temp != '-'))
    		test ="no";
    }
    if (test != "" )
		return false;
	else
		return true;
}


/*
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	Name		:	isValidMobile
	Purpose		:	This function checks for Mobile validation.
					This function will return a false value if the parameter passed
					in contains a non-Phone character.
	Usage		:	isValidMobile("text")
	Arguments	:	text
	Return		:	true/false
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
function isValidMobile(aValue)
{
	var i=0;
	var temp="";
	var test="";

	for (i=0; i < aValue.length; i++)
  	{
    	temp = aValue.substring(i, i+1);
    	if (((temp < "0" || temp > "9") && temp != ' ' && temp != '-'))
    		test ="no";
    }
    if (test != "" )
		return false;
	else
		return true;
}


/*
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	Name		:	formatCurrency   [should use in html as ONBLUR="this.value = formatCurrency(this.value)"]
	Purpose		:	This function checks for Currncy validation
					This function will return the currency in thousand format delimited by COMA(,)
	Usage		:	formatCurrency(number)
	Arguments	:	number
	Return		:	currency in thousand format delimited by COMA(,)
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
function formatCurrency(num)
{
	num = num.toString().replace(/\$|\,/g,'');
	if(isNaN(num))
		num = "0";
	sign = (num == (num = Math.abs(num)));
	num = Math.floor(num*100+0.50000000001);
	cents = num%100;
	num = Math.floor(num/100).toString();
	if(cents<10)
		cents = "0" + cents;
	for (var i = 0; i < Math.floor((num.length-(1+i))/3); i++)
		num = num.substring(0,num.length-(4*i+3))+','+
			  num.substring(num.length-(4*i+3));
	return (num + '.' + cents);
}

/*
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	Name		:	changeCurrencyToNumber
	Purpose		:	This function converts currency to Number.
	Usage		:	changeCurrencyToNumber(currency)
	Arguments	:	currency
	Return		:	Number
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
function changeCurrencyToNumber()
{
	vRFormName = arguments[0];
	var sTxtValue=trimSpaces(vRFormName[arguments[1]].value);
	var nReturnNumber="";

	for (var i = 0; i < sTxtValue.length; i++)
	{
		temp = vRFormName[arguments[1]].value.charAt(i);
		if(temp!=",")
			nReturnNumber=nReturnNumber+temp;
	}
	vRFormName[arguments[1]].value=nReturnNumber;
}

// Usage: changeCurrenciesToNumber( document.form1, 'varName1', 'varName2', ... );
function changeCurrenciesToNumber(){
	vRFormName = arguments[0];
	
	for( argCounter = 1; argCounter<arguments.length; argCounter++ ){
		if( !vRFormName[arguments[argCounter]] ){
			continue;
		}
		var sTxtValue = trimSpaces( vRFormName[arguments[argCounter]].value );
		var nReturnNumber="";
		for (var i=0; i<sTxtValue.length; i++){
			temp = vRFormName[arguments[argCounter]].value.charAt(i);
			if(temp != ","){
				nReturnNumber += temp;
			}
		}
		vRFormName[arguments[argCounter]].value = nReturnNumber;
	}
}


// Enables or disables an object based on the value of state.
	function allow( targetObj, state ){
		targetObj.disabled = !state;
	}
	

	// Used to highlight rows according to "Due Date"
	function highlightRows( tableID, columnTitle ){
		var IE5 = (document.all && document.getElementById);
		var NS6 = (document.getElementById && !document.all);
		
		if( IE5 ){
			tableID = document.all[tableID];
		}else{
			tableID = document.getElementById( tableID );
		}

		cellIndex = -1;
		for( i=0; i<tableID.rows[0].cells.length; i++ ){
			if( trimSpaces( tableID.rows[0].cells[i].innerText ) == columnTitle ){
				cellIndex = i;
				break;
			}
		}

		if( cellIndex != -1 ){
			today = new Date();
			today = new Date( today.getYear(), today.getMonth(), today.getDate() );

			for( i=1; i<tableID.rows.length; i++ ){
				if( tableID.rows[i].cells[cellIndex] ){
					dueDate = trimSpaces( tableID.rows[i].cells[cellIndex].innerText );
					if( dueDate != "" ){
						dueDate = new Date( swapDayMonth( dueDate ) );
						diff = (dueDate - today);
						color = null;
						if( diff == 0 ){ // Due Today
							color = "#008000"; // green
						}else if( diff < 0 ){
							color = "#FF0000"; // red
						}else if( diff > 0 ){
							color = "#0000FF"; // blue
						}
						if( color != null ){
							for( j=0; j<tableID.rows[i].cells.length; j++ ){
								tableID.rows[i].cells[j].style.color = color;
							}
						}
					}
				}
			}
		}
	}


	//function getParameter
	function getParameter(parameter){
		href = location.href;
		index = href.indexOf("?");
		if(index == -1)
			return "";

		paramValue = href.substring( href.indexOf(parameter, (index+1)) );
		index = paramValue.indexOf(parameter);
		if(index == -1)
			return "";

		paramValue = paramValue.substring( ( index+(parameter.length+1) ) );
		index = paramValue.indexOf("&");
		paramValue = paramValue.substring(0, (index == -1 ? paramValue.length : index) );
		return paramValue;
	}
	

	function pagePrint(url,windName){
		url='../html_enq/print_page1.html';
		windName='kk';
		wind = window.open(url, (windName ? windName : "_sameWindow"), "width=0 ","height=0");
		x = 1;
		y = 1;
		wind.moveTo(x, y);
		wind.resizeTo( 1, 1 );
		wind.focus();
		wind.print();
		wind.close();
	//javascript:window.print('../html_enq/print_page1.html')"
	}
	/*
		~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		Name		:	chkEnable
		Purpose		:	This function enables/disables text box 
						depending on the state of associated text box.
		Usage		:	chkEnable(checkbox/radio.buttonSmall object, textbox object)
		Arguments	:	checkbox/radio.buttonSmall object, textbox object
		Return		:	
		~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	*/	
	function chkEnable(chkObj,txtObj){
		if(chkObj.checked == true){
			txtObj.disabled = false;
		}else{
			txtObj.value = "";
			txtObj.disabled = true;
		}
	}