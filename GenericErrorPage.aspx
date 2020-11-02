<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GenericErrorPage.aspx.cs" Inherits="GenericErrorPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>MPSCSC Portal</title>     
    <link rel="stylesheet" href ="MyCss/xp_comon.css" type ="text/css"  />    
    <link rel="stylesheet" type="text/css" href="MyCSS/style.css">
    <link rel="stylesheet" href ="MyCss/xp_menu.css" type ="text/css" />
    <link rel="stylesheet" type="text/css" href="sdmenu/sdmenu.css" />
     <script type="text/javascript" src ="js/chksql.js"></script>
       <script type="text/javascript" src="js/md5.js"></script>
    <script type="text/javascript">
    //var browser=navigator.appName;
    //var b_version=navigator.userAgent.toLowerCase();
    //var version=parseFloat(b_version);
//alert(navigator.javascriptEnabled);
//var str =document.getElementById('HyperLink1').id ;
//alert( b_version);
//str.blink();
//var uri="my test.asp?name=ståle&car=saab";
//document.write(encodeURI(uri)+ "<br />");
//document.write(decodeURI(uri));
//document.write("Java enabled: " + navigator.javaEnabled());

    var sheets = document.styleSheets ;   
    sheets[0].disabled = true ; 
    sheets[1].disabled = true ;   
    sheets[2].disabled = true ; 
    sheets[3].disabled = true ;       
        var windt=navigator.userAgent.toLowerCase();
        var winindx='null';
        var win_xp='null';
        winindx=windt.match('win');        
        win_xp=windt.match('nt 5.1');           
        if(win_xp == 'nt 5.1' && winindx=='win')
        {
             sheets[0].disabled = false;
             sheets[2].disabled = false;             
        }
        else
        {           
             sheets[0].disabled = false;
             sheets[2].disabled = false;             
        }    
    
    </script> 
    
    <style type="text/css">
#marqueecontainer{
position:notset;
width: 180px; /*marquee width */
height: 200px; /*marquee height */
background-color: white;
overflow: hidden;
border: 1px solid maroon;
padding: 2px;
padding-left: 4px;
}

</style>

 <script type="text/javascript">
    window.history.forward(0); 
    </script>
    
    <script language = "Javascript">
    /**
 * DHTML textbox character counter script. Courtesy of SmartWebby.com (http://www.smartwebby.com/dhtml/)
 */

var maxL=10;
var bName = navigator.appName;
function taLimit(taObj) {
	if (taObj.value.length==maxL) return false;
	return true;
}

function taCount(taObj,Cnt) { 
	objCnt=createObject(Cnt);
	objVal=taObj.value;
	if (objVal.length>maxL) objVal=objVal.substring(0,maxL);
	if (objCnt) {
		if(bName == "Netscape"){	
			objCnt.textContent=maxL-objVal.length;}
		else{objCnt.innerText=maxL-objVal.length;}
	}
	return true;
}
function createObject(objId) {
	if (document.getElementById) return document.getElementById(objId);
	else if (document.layers) return eval("document." + objId);
	else if (document.all) return eval("document.all." + objId);
	else return eval("document." + objId);
}
</script>  
    
    
<script type="text/javascript">

/***********************************************
* Cross browser Marquee II- © Dynamic Drive (www.dynamicdrive.com)
* This notice MUST stay intact for legal use
* Visit http://www.dynamicdrive.com/ for this script and 100s more.
***********************************************/

var delayb4scroll=2000 //Specify initial delay before marquee starts to scroll on page (2000=2 seconds)
var marqueespeed=1 //Specify marquee scroll speed (larger is faster 1-10)
var pauseit=1 //Pause marquee onMousever (0=no. 1=yes)?

////NO NEED TO EDIT BELOW THIS LINE////////////

var copyspeed=marqueespeed
var pausespeed=(pauseit==0)? copyspeed: 0
var actualheight=''

function scrollmarquee(){
if (parseInt(cross_marquee.style.top)>(actualheight*(-1)+8))
cross_marquee.style.top=parseInt(cross_marquee.style.top)-copyspeed+"px"
else
cross_marquee.style.top=parseInt(marqueeheight)+8+"px"
}

function initializemarquee(){
cross_marquee=document.getElementById("vmarquee")
cross_marquee.style.top=0
marqueeheight=document.getElementById("marqueecontainer").offsetHeight
actualheight=cross_marquee.offsetHeight
if (window.opera || navigator.userAgent.indexOf("Netscape/7")!=-1){ //if Opera or Netscape 7x, add scrollbars to scroll and exit
cross_marquee.style.height=marqueeheight+"px"
cross_marquee.style.overflow="scroll"
return
}
setTimeout('lefttime=setInterval("scrollmarquee()",30)', delayb4scroll)
}




if (window.addEventListener)
window.addEventListener("load", initializemarquee, false)
else if (window.attachEvent)
window.attachEvent("onload", initializemarquee)
else if (document.getElementById)
window.onload=initializemarquee


</script>
</head>
<body leftmargin=0 topmargin=0 bgcolor="#ffffff"> 

    <form id="form1" runat="server">
   
    <table border="0" cellspacing="0" cellpadding="0" width="100%" height="100%">
  <tr>
	<td width="50%" background="images/bg.gif"><img src="images/px1.gif" width="1" height="1" alt="" border="0"></td>
	<td valign="bottom" background="images/bg_left.gif"><img src="images/bg_left.gif" alt="" width="17" height="16" border="0"></td>
	<td>
	<table width="780" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td>
              <img height="126" src="Images/CH2.jpg" style="width: 776px" /></td>
        </tr>
        <tr>
            <td align="center">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/GenErro.jpg" /></td>
        </tr>
        <tr>
            <td align="center" style="color: white; background-color: firebrick; height: 21px;">
                &nbsp;<asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="True" Font-Italic="True"
                    ForeColor="ButtonHighlight" OnClick="LinkButton1_Click">Go To The Login Page</asp:LinkButton></td>
        </tr>
      </table>
<div class="px" align="center"><img src="images/bot01.jpg" width="780" height="9" alt="" border="0"></div>
<table border="0" cellspacing="0" cellpadding="0" width="780" align="center" style="border-right: gray 1px solid; border-top: gray 1px solid; border-left: gray 1px solid; border-bottom: gray 1px solid">
    <tr>
        <td style="height: 10px; font-weight: bold; font-size: 12px; color: green;" align="center">
             Site Designed and Hosted By:</td>
        <td style="height: 10px; font-weight: bold; font-size: 12px; color: green;" align="center">
           Contents Provided By:</td>
    </tr>
<tr>
	<td><p>
        <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="Arial Narrow" Font-Size="12px"
            ForeColor="Navy" Height="50px" Width="310px" Font-Bold="True">National Informatics Centre, Madhya Pradesh
Ministry of Communications and Information Technology
Department of Information Technology</asp:HyperLink>&nbsp;</p></td>
	      <td height="50"> 
            <p class="bot"><b> <a href="">
                <asp:HyperLink ID="HyperLink4" runat="server" Font-Names="Arial Narrow" Font-Size="12px"
                    ForeColor="Navy" Height="40px" Width="338px">Madhya Pradesh State Civil Supplies Corporation Ltd. Block-1, 3rd Floor, Paryavas Bhavan, Jail Road, Bhopal- 462011 India  Phones : 91-755-2551539, Fax :91-755-2551289 Email : mpscsc@sancharnet.in </asp:HyperLink></a></b></p>
	</td>
</tr>
</table>
	<td valign="bottom" background="images/bg_right.gif"><img src="images/bg_right.gif" alt="" width="17" height="16" border="0"></td>
	<td width="50%" background="images/bg.gif"><img src="images/px1.gif" width="1" height="1" alt="" border="0"></td>
    </tr> 
	</table> 
    
    </form>
	
	
</body>
</html>
