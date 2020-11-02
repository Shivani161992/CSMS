// For Password TextBoxes
 function checksqlkey_psw(e,tx)
    {     
        var AsciiCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode; 
        var num=tx.value;
        
        //alert(AsciiCode);              
       if (AsciiCode == 59 || AsciiCode == 32)
        {            
            alert('Semi Colon (;) & Blank Space Not Allowed ...');
            return false;
        } 
       else if (AsciiCode=="33" || AsciiCode=="96" || AsciiCode=="94" || AsciiCode=="37" || AsciiCode=="38" || AsciiCode=="40" || AsciiCode=="41" || AsciiCode=="43" || AsciiCode=="92" || AsciiCode=="124" || AsciiCode=="34" || AsciiCode=="39" || AsciiCode=="60" || AsciiCode=="62" || AsciiCode=="63" || AsciiCode=="44" ||  AsciiCode=="61" || AsciiCode=="46")
        {
            alert('Do not use SQL Key-Words, Semi Colon(;) and Special Characters(&,%)..etc');
	        return false;
        }
       else if(num.length>15)
        {
            alert('Password Length Maximum 15 Characters ...');
            return false;
        }                          
    }
    
   // For General TextBoxes 
    
    function checksqlkey_gen(e,tx)
    {
        var AsciiCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode; 
        var num=tx.value;
                     
       if (AsciiCode == 59)
        {            
            alert('Semi Colon (;) Not Allowed ...');
            return false;
        }
       else if (AsciiCode=="36" || AsciiCode=="37" || AsciiCode=="38" || AsciiCode=="40" || AsciiCode=="41" || AsciiCode=="43" || AsciiCode=="92" || AsciiCode=="124" || AsciiCode=="34" || AsciiCode=="39" || AsciiCode=="60" || AsciiCode=="62" || AsciiCode=="44" || AsciiCode=="64" || AsciiCode=="61")
        {
            alert('Do not use SQL Key-Words, Semi Colon(;) and Special Characters(&,%,$)..etc');
	        return false;
        }                  
    }
    
    // For Copy/Paste Checking
    
    function checksqlkey_special(e,tx)
    {
        var AsciiCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode; 
        var num=tx.value;     
        //alert(AsciiCode);         
       if (AsciiCode == 17)
        {            
            alert('Copy/Paste Not Allowed ...');
            return false;
        }                  
    }
    
    
    // Check SQL For General TextBoxes
    
    function chksqltxt(txt_ctrl)
{	
    var txtval = txt_ctrl.value;   
     if (txtval=="$" || txtval=="+" || txtval=="(" || txtval==")" || txtval=="|")
   {
    txt_ctrl.value="";
    alert('Do not use SQL Key-Words, Semi Colon(;) and Special Characters(&,%,$)..etc');
	return false;
   }
    else
   { 
    txtval=txtval.toLowerCase();
    var sqlkeys= new Array()
    sqlkeys[0]="drop"; 
    sqlkeys[1]="select"; 
    sqlkeys[2]="delete"; 
    sqlkeys[3]="truncate"; 
    sqlkeys[4]="alter"; 
    sqlkeys[5]=";"; 
    sqlkeys[6]="update";
    sqlkeys[7]="create";   
    sqlkeys[8]=" ,";  
    sqlkeys[9]="%"; 
    sqlkeys[10]="@";    
    sqlkeys[11]="&"; 
    sqlkeys[12]="'"; 
    sqlkeys[13]='"'; 
    sqlkeys[14]="\"";
    sqlkeys[15]="\'"; 
    sqlkeys[16]="<"; 
    sqlkeys[17]=">";
    sqlkeys[18]="\\\\"; 
    sqlkeys[19]="lf ";
    sqlkeys[20]="cr "; 
    sqlkeys[21]="="; 
    var sqlstr=-1;
    var x=0;
    for (x=0; x<sqlkeys.length; x++) 
     { 
        sqlstr=txtval.search(sqlkeys[x]); 
        if(sqlstr >= 0)
         {
           break;
	     }
     }      
    if(sqlstr >= 0)
    {
    txt_ctrl.value="";
    alert('Do not use SQL Key-Words, Semi Colon(;) and Special Characters(&,%,$)..etc');
	return false;
	}	
  }
    
}

// Check SQL For Password TextBoxes

function chksqltxt_psw(txt_ctrl)
{	
    var txtval = txt_ctrl.value;    
    if (txtval=="")
    {
    alert('Please Enter Password ...');
    return false;
    }
    else if ( txtval=="+" || txtval=="(" || txtval==")" || txtval=="|")
    {
    txt_ctrl.value="";
    alert('Do not use SQL Key-Words, Semi Colon(;) and Special Characters(&,%)..etc');
	return false;
    }
    else if(txtval.length>15)
    {
     txt_ctrl.value="";
     alert('Password Length Maximum 15 Characters ...');
     return false;
    }  
    else
    {
    txtval=txtval.toLowerCase();
    var sqlkeys= new Array()
    sqlkeys[0]="drop"; 
    sqlkeys[1]="select"; 
    sqlkeys[2]="delete"; 
    sqlkeys[3]="truncate"; 
    sqlkeys[4]="alter"; 
    sqlkeys[5]=";"; 
    sqlkeys[6]="update";
    sqlkeys[7]="create";   
    sqlkeys[8]=" ";  
    sqlkeys[9]="%"; 
    sqlkeys[10]="=";    
    sqlkeys[11]="&"; 
    sqlkeys[12]="'"; 
    sqlkeys[13]='"'; 
    sqlkeys[14]="\"";
    sqlkeys[15]="\'"; 
    sqlkeys[16]="<"; 
    sqlkeys[17]=">";
    sqlkeys[18]=" ,";
    sqlkeys[19]="\\\\";         
    sqlkeys[20]="lf ";
    sqlkeys[21]="cr "; 
   
    var sqlstr=-1;
    var x=0;
    for (x=0; x<sqlkeys.length; x++) 
     { 
        sqlstr=txtval.search(sqlkeys[x]); 
        if(sqlstr >= 0)
         {
           break;
	     }
     }      
    if(sqlstr >= 0)
    {
    txt_ctrl.value="";
    alert('Do not use SQL Key-Words, Semi Colon(;) and Special Characters(&,%)..etc');
	return false;
	}
  }    
}

// For disabling Back Button

//window.history.forward(1);

function noBack()
{
window.history.forward()
}
noBack();
window.onload=noBack;
window.onpageshow=function(evt)
{
if(evt.persisted)
noBack()
}
window.onunload=function()
{
void(0)
}


// For disabling Right Click

var message="Right Click Disabled!";
function clickIE4(){
if (event.button==2){
//alert(message);
return false;
}
}

function clickNS4(e){
if (document.layers||document.getElementById&&!document.all){
if (e.which==2||e.which==3){
//alert(message);
return false;
}
}
}

if (document.layers){
document.captureEvents(Event.MOUSEDOWN);
document.onmousedown=clickNS4;
}
else if (document.all&&!document.getElementById){
document.onmousedown=clickIE4;
}

//document.oncontextmenu=new Function("alert(message);return false")
document.oncontextmenu=new Function("return false")


