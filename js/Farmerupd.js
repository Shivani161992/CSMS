// JScript File




function IsNumericMobile(key,txtBox)
{
    var keycode = (key.which) ? key.which : key.keyCode;
    var num=txtBox.value;
    var len=num.length;
    
    
    if(keycode==09 && num== 0)
    {
            alert('मोबाइल नम्बर 0 प्रविष्ट नहीं कर सकते |');
            txtBox.value="";
             return false;
    }
    if( keycode==09 && len <10)
    {
             alert('मोबाइल नम्बर 10 अंको का प्रविष्ट करें|');
             return false;
    }
    if(keycode==08 || keycode==09 )
    {
        return true;
    }
    else 
    {
        if (keycode >= 48 && keycode <= 58 )
        {
             return true;
        }
        else
        {
             alert('कृपया संख्या ही प्रविष्ट करें |');
             return false;
        }
    }
}

function IsNumericProcQty(key,txt)
{
  var keycode = (key.which) ? key.which : key.keyCode;
    var num=txt.value;
    var len=num.length;
    var indx=-1;
    indx=num.indexOf('.');
    
            var dgt=num.substr(indx,len);
            var count= dgt.length;
      
    if(keycode==08)
    {
        return true;
    }
     else if (keycode == 59 || keycode  == 32)
     {            
            alert('Semi Colon (;) & Blank Space Not Allowed ...');
            return false;
     } 
     else if (keycode =="36" || keycode =="37" || keycode =="38" || keycode =="40" || keycode =="41" || keycode =="43" || keycode =="92" || keycode =="124" || keycode =="34" || keycode =="39" || keycode =="60" || keycode =="62" || keycode =="44" || keycode =="64" || keycode =="61" || keycode=="63")
     {
            alert('Do not use SQL Key-Words, Semi Colon(;) and Special Characters(&,%,$)..etc');
	        return false;
     }
    
    else if (keycode==09)
    {
             if(num>500)
            {
                 alert('उपार्जन की मात्रा 500 से अधिक प्रविष्ट नहीं कर सकते हें|');
                 txt.value="0";
                 return false;
            }
    } 
    
    else if (num>500)
    {
            alert('उपार्जन की मात्रा 500 से अधिक प्रविष्ट नहीं कर सकते हें|');
            txt.value="0";
            return false;
    }      
    else  if (count > 3)  
    {
                alert("दशमलव के बाद 3 अंक ही आ सकते है");
                return false;
    }
    
    else if(keycode==46)
    {
         if (num.split(".").length>1)
         {    
            alert('दशमलव एक ही बार आ सकता है |');
            return false;
         }
    }
    else if (keycode >= 48 && keycode <= 58 )
    {
        return true;
    }  
    else 
    {
        alert('कृपया संख्या ही प्रविष्ट करें |');
        return false;
    }}

function IsNumericRakba(key,txt)
{
    var keycode = (key.which) ? key.which : key.keyCode;
    var num=txt.value;
    var len=num.length;
    var indx=-1;
    indx=num.indexOf('.');
    
            var dgt=num.substr(indx,len);
            var count= dgt.length;
            //alert (count);
           
  
//           
//    if(keycode==09 && num== 0)
//    {
//            alert('रकबा एवं उपार्जित मात्रा 0 प्रविष्ट नहीं कर सकते |');
//            txt.value="0";
//             return false;
//    } 
    if(keycode==08)
    {
        return true;
    }
     else if (keycode == 59 || keycode  == 32)
     {            
            alert('Semi Colon (;) & Blank Space Not Allowed ...');
            return false;
     } 
     else if (keycode =="36" || keycode =="37" || keycode =="38" || keycode =="40" || keycode =="41" || keycode =="43" || keycode =="92" || keycode =="124" || keycode =="34" || keycode =="39" || keycode =="60" || keycode =="62" || keycode =="44" || keycode =="64" || keycode =="61" || keycode=="63")
     {
            alert('Do not use SQL Key-Words, Semi Colon(;) and Special Characters(&,%,$)..etc');
	        return false;
     }
    
    else if (keycode==09)
    {
             if(num>100)
            {
                 alert('रकबा 100 से अधिक प्रविष्ट नहीं कर सकते हें|');
                 txt.value="0";
                 return false;
            }
    } 
    
    else if (num>100)
    {
            alert('रकबा 100 से अधिक प्रविष्ट नहीं कर सकते हें|');
            txt.value="0";
            return false;
    }      
    else  if (count > 3)  
    {
                alert("दशमलव के बाद 3 अंक ही आ सकते है");
                return false;
    }
    
    else if(keycode==46)
    {
         if (num.split(".").length>1)
         {    
            alert('दशमलव एक ही बार आ सकता है |');
            return false;
         }
    }
    else if (keycode >= 48 && keycode <= 58 )
    {
        return true;
    }  
    else 
    {
        alert('कृपया संख्या ही प्रविष्ट करें |');
        return false;
    }
}



function chkSqlKey(key,txt)
{
 
    var keycode = (key.which) ? key.which : key.keyCode;
    
    if (keycode == 59  || keycode == 32 )
     {            
            alert('Semi Colon (;) & Blank Space Not Allowed ...');
            return false;
     } 
   
     else if (keycode =="36" || keycode =="37" || keycode =="38" || keycode =="40" || keycode =="41" || keycode =="43" || keycode =="92" || keycode =="124" || keycode =="34" || keycode =="39" || keycode =="60" || keycode =="62" || keycode =="44" || keycode =="64" || keycode =="61" || keycode=="63")
     {
            alert('Do not use SQL Key-Words, Semi Colon(;) and Special Characters(&,%,$)..etc');
	        return false;
     }
}

function chkString(key,txt)
{
 
    var keycode = (key.which) ? key.which : key.keyCode;
    
    if (keycode == 59 )
     {            
            alert('Semi Colon (;) & Blank Space Not Allowed ...');
            return false;
     } 
   
     else if (keycode =="36" || keycode =="37" || keycode =="38" || keycode =="40" || keycode =="41" || keycode =="43" || keycode =="92" || keycode =="124" || keycode =="34" || keycode =="39" || keycode =="60" || keycode =="62" || keycode =="44" || keycode =="64" || keycode =="61" || keycode=="63")
     {
            alert('Do not use SQL Key-Words, Semi Colon(;) and Special Characters(&,%,$)..etc');
	        return false;
     }
     else if(keycode >= 48 && keycode <= 58 )
     {
            return false;
     }
}


function chkFarmerId(key,txt)
{
 
    var keycode = (key.which) ? key.which : key.keyCode;
    
    if (keycode == 59 || keycode == 32 )
     {            
            alert('Semi Colon (;) & Blank Space Not Allowed ...');
            return false;
     } 
   
     else if (keycode =="36" || keycode =="37" || keycode =="38" || keycode =="40" || keycode =="41" || keycode =="43" || keycode =="92" || keycode =="124" || keycode =="34" || keycode =="39" || keycode =="60" || keycode =="62" || keycode =="44" || keycode =="64" || keycode =="61" || keycode=="63")
     {
            alert('Do not use SQL Key-Words, Semi Colon(;) and Special Characters(&,%,$)..etc');
	        return false;
     }
     else if(keycode==46)
     {
            return false;
     }
}

function chkSum(txt1,txt2,txt3)
{
    var one=   txt1.value;
    var  two=   txt2.value;
    var three= parseFloat(txt3.value);
    var  txtSum = parseFloat(one)+parseFloat(two);
    if(txtSum > 100)
    {
        alert('सिंचित एवं असिंचित रकबा का यौग 100 से ज्यादा नहीं हो सकता है |');
        txt1.value="0";
        txt2.value="0";
        return false;
    }
     if (three<txtSum)
    {
        alert('सिंचित एवं असिंचित रकबा का यौग  ='+txtSum +' है   \nजो  '+' कुल रकबा = '+three +' से ज्यादा नहीं हो सकता है |');
        txt1.value="0";
        txt2.value="0";
        //txt3.value="0";
        return false;
    }
}

function chkRkbaDot(txt)
{
        var va= txt.value;
        if(va=='.')
        {
            alert('मात्रा प्रविष्ट करें |');
            txt.value="0";
        }
     
}











