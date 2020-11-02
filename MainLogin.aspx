<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainLogin.aspx.cs" Inherits="MainLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title> MPSCSC Portal</title> 
    <link rel="shortcut icon" type="image/png" href="images/mpscsc_logo.jpg" />    
       
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

        .style1
        {
            color: #FFFFFF;
        }

        </style>

 <script type="text/javascript">
    window.history.forward(0); 
    </script>
    
    <script language = "Javascript">
    /**
 * DHTML textbox character counter script. Courtesy of SmartWebby.com (http://www.smartwebby.com/dhtml/)
 */

var maxL=15;
var bName = navigator.appName;
function taLimit(taObj) {
	if (taObj.value.length==maxL) return false;
	return true;
}

function taCount(taObj, Cnt) 
{ 
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
   
    <table border="0" cellspacing="0" cellpadding="0" width="100%" style="height: 95%">
  <tr>
	<td width="50%" background="images/bg.gif"><img src="images/px1.gif" width="1" height="1" alt="" border="0"></td>
	<td valign="bottom" background="images/bg_left.gif"><img src="images/bg_left.gif" alt="" width="17" height="16" border="0"></td>
	<td>
	<table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td>
          <img src="Images/csmsheader.jpg" style="width: 1271px; height: 149px;"></td>
        </tr>
      </table>
     
      <center>
      <table border="0" cellspacing="0" cellpadding="0" width="100%" align="center" 
              style="font-family: calibri">
        <tr>
            <td align="center" 
                style="border: thin groove #FF3300; background-color: #DDBBFF">
                 <table style="border-right: black thin double; border-top: black thin double; border-left: black thin double; width: "100%"; border-bottom: black thin double">
                <tr>
                <td align="center" colspan="4" style="color: white; background-color: #4892FF">
                    <asp:Label ID="Label1" runat="server" Text="SELECT  LOGIN  TYPE" Font-Bold="True" Font-Italic="True" Width="614px"></asp:Label></td>
                
                </tr>
                    <tr>
                        <td align="left" style="width: 276px; background-color: white;">
                            <asp:RadioButton ID="rdbissue" runat="server" AutoPostBack="True" Checked="True"
                                Font-Bold="True" ForeColor="#000099" GroupName="1" TabIndex="1" Text="Issue Center"
                                ValidationGroup="1" Font-Italic="False" Font-Names="Georgia" 
                                Font-Size="Larger" /></td>
                        <td colspan="3" style="width: 143px; background-color: #FFFFFF">
                      
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid; width: 276px;">
                            <asp:Label ID="Label6" runat="server" Visible="False"></asp:Label></td>
                        <td colspan="3" align="left" style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid">
                        
                            <asp:Panel ID="pnl_issue" runat="server" Visible="False">
                            <table style="width: 350px">
                                <tr>
                                    <td align="center" colspan="2" style="color: white; background-color: #336699">
                                        <asp:Label ID="Label9" runat="server" Text="Select Role" Width="428px"></asp:Label></td>
                                </tr>
                            <tr>
                            
                            <td style="background-color: #cfdcc8">
                                <asp:Label ID="Label5" runat="server" Font-Italic="False" Text="Select Role"></asp:Label></td>
                            <td align="left" colspan="1" style="background-color: #cfdcc8">  
                                <asp:DropDownList ID="ddl_ICrole" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_ICrole_SelectedIndexChanged"
                                    Width="172px">
                                    <asp:ListItem Value="01">--Select--</asp:ListItem>
                                    <asp:ListItem Value="BM">Issue Centre Manager</asp:ListItem>
                                    <asp:ListItem Value="OP">Operator</asp:ListItem>
                                    
                                    <asp:ListItem Value="DMO">Markfed</asp:ListItem>
                                </asp:DropDownList></td>
                            </tr> 
                                <tr>
                                    <td align="left" style="background-color: #cfdcc8;">
                                        <asp:Label ID="lbldist" runat="server" Font-Italic="False" Text="District " Visible="False"></asp:Label></td>
                                    <td style="background-color: #cfdcc8;">
                                        <asp:DropDownList ID="ddldistrict" runat="server" Width ="171px" AutoPostBack ="true" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged" TabIndex="2" Visible="False">
                        <asp:ListItem Value ="01" Selected ="True" >--Select--</asp:ListItem>
                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td align="left" style="background-color: #cfdcc8;">
                                        <asp:Label ID="lblissue" runat="server" Font-Italic="False" Text="Issue Centre" Visible="False"
                                            Width="86px"></asp:Label></td>
                                    <td align="left" style="background-color: #cfdcc8;">
                                        <asp:DropDownList ID="ddlissue" runat="server" Width ="171px" TabIndex="3" Visible="False" AutoPostBack="True" OnSelectedIndexChanged="ddlissue_SelectedIndexChanged">
                        <asp:ListItem Value ="01" Selected ="True" >--Select--</asp:ListItem>
                        </asp:DropDownList></td>
                                </tr>
                            
                            <tr>
                            
                            <td align="left" style="background-color: #cfdcc8;">
                                &nbsp;<asp:Label ID="lbloper" runat="server" Font-Italic="False" Text="Operator ID"
                                    Visible="False" Width="79px"></asp:Label></td>
                            <td style="background-color: #cfdcc8;">
                             <asp:DropDownList ID="DDLOperetorId" runat="server" Width="170px" Visible="False" OnSelectedIndexChanged="DDLOperetorId_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
                            
                              </td>
                            </tr> 
                            
                            
                            
                            
                            </table> 
                            </asp:Panel> 
                        
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 276px; background-color: #4892FF; height: 22px;">
                            <asp:RadioButton ID="rdbdistt" runat="server" AutoPostBack="True" ForeColor="Navy"
                              GroupName="1" TabIndex="4" Text="DM MPSCSC" ValidationGroup="1" 
                                Font-Bold="True" Font-Italic="False" Font-Names="Georgia" Font-Size="13px" 
                                style="color: #FFFFFF" /></td>
                        <td colspan="3" style="width: 214px; background-color: #4892FF; height: 22px;">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid; width: 276px;">
                        </td>
                        <td colspan="3" align="left" style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid">
                        
                         <asp:Panel ID="pnl_dm" runat="server" Visible="False">
                            <table style="width: 350px">
                                <tr>
                                    <td align="center" colspan="2" style="color: white; background-color: #336699">
                                        <asp:Label ID="Label4" runat="server" Text="Select Role" Width="435px"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                        width: 157px; border-bottom: white 1px solid; background-color: #cfdcdc; height: 26px;">
                                        <asp:Label ID="Label8" runat="server" Text="Select Role"></asp:Label></td>
                                    <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                        border-bottom: white 1px solid; width: 157px; background-color: #cfdcdc; height: 26px;">
                                        <asp:DropDownList ID="ddl_DMrole" runat="server" OnSelectedIndexChanged="ddl_DMrole_SelectedIndexChanged"
                                            Width="147px" AutoPostBack="True">
                                            <asp:ListItem>--Select--</asp:ListItem>
                                            <asp:ListItem Value="DM">District Manager</asp:ListItem>
                                            <asp:ListItem Value="OP">Operator</asp:ListItem>
                                             <asp:ListItem Value="DMO">Markfed</asp:ListItem>
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                        width: 157px; border-bottom: white 1px solid; background-color: #cfdcdc;">
                                        <asp:Label ID="lbldmdist" runat="server" Text="District " Visible="False"></asp:Label></td>
                                    <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                        border-bottom: white 1px solid; width: 157px; background-color: #cfdcdc;">
                            
                            <asp:DropDownList ID="ddldistoff" runat="server" Width="148px" TabIndex="5" Visible="False" AutoPostBack="True" OnSelectedIndexChanged="ddldistoff_SelectedIndexChanged">
      </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                        width: 157px; border-bottom: white 1px solid; background-color: #cfdcdc;">
                                        <asp:Label ID="lbldmop" runat="server" Text="Operator ID" Visible="False"></asp:Label></td>
                                    <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                        border-bottom: white 1px solid; width: 157px; background-color: #cfdcdc;">
                             <asp:DropDownList ID="ddl_DMoperatorID" runat="server" Width="148px" Visible="False" AutoPostBack="True" OnSelectedIndexChanged="ddl_DMoperatorID_SelectedIndexChanged">
                </asp:DropDownList></td>
                                </tr>
                            
                            
                            
                            
                            </table> 
                            </asp:Panel> 
                        
                        
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="background-color:white; width: 276px;">
                            <asp:RadioButton ID="rdbregion" runat="server" AutoPostBack="True" ForeColor="Maroon"
                                GroupName="1" TabIndex="6" Text="Regional Office" ValidationGroup="1" 
                                Font-Bold="True" Font-Italic="False" Font-Names="Georgia" 
                                Font-Size="14px" /></td>
                        <td style="background-color: white; width: 6px;" align="left">
                            <asp:DropDownList ID="ddlregion" runat="server" TabIndex="7" Visible="False" Width="149px">
                            </asp:DropDownList></td>
                        <td style="background-color: white; " colspan="2">
                        
                            <asp:HyperLink ID="HyperLink5" runat="server" 
                                NavigateUrl="~/Miller_Registeration/Miller_registration.aspx">Miller Registration</asp:HyperLink>
                        
                            
                        
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="background-color: white; width: 276px;">
                            <asp:RadioButton ID="rdbho" runat="server" AutoPostBack="True" ForeColor="Maroon"
                                GroupName="1" TabIndex="8" Text="HO MPSCSC" ValidationGroup="1" 
                                Width="118px" Font-Bold="True" Font-Italic="False" 
                                Font-Names="Georgia" Font-Size="14px" /></td>
                        <td style="background-color: white" align="left">
                            <asp:DropDownList ID="ddlho" runat="server" TabIndex="9" Visible="False" Width="148px">
                                <asp:ListItem Value="HO">HO</asp:ListItem>
                            </asp:DropDownList></td>
                        <td style="background-color: white">
                         <asp:HyperLink ID="hyp_mLog" runat="server" 
                                NavigateUrl="~/Miller_Registeration/Miller_Login.aspx">Miller Login</asp:HyperLink>
                        &nbsp;
                        
                        </td>
                        <td style="background-color: white;">
                        
                        <asp:HyperLink ID="hyp_insp" runat="server" 
                                NavigateUrl="~/PcGdn_Insp/Login.aspx">Inspection Module</asp:HyperLink>
                        
                        </td>
                    </tr>
                     <tr>
                         <td align="left" style="width: 276px; height: 21px; background-color: white">
                             <asp:RadioButton ID="rbdirfood" runat="server" AutoPostBack="True" 
                                 ForeColor="Maroon" GroupName="1"
                                TabIndex="10" Text="Directorate Food Office" ValidationGroup="1" 
                                 Font-Bold="True" Font-Italic="False" Font-Names="Georgia" 
                                 Font-Size="14px" Width="194px" /></td>
                         <td align="left" style="height: 21px; background-color: white; width: 6px;">
                             <asp:DropDownList ID="drpdirfood" runat="server" TabIndex="9" Visible="False" Width="148px">
                                 <asp:ListItem Value="HO">Diretorate Food Office</asp:ListItem>
                             </asp:DropDownList></td>
                        <td style="background-color: white">
                          <asp:HyperLink ID="HyperLink2" runat="server" 
                                NavigateUrl="~/Mf_2018/Mf_PaddyTransportation/MfCMRBillingLogin.aspx">CMR Billing For Markfed</asp:HyperLink>
                        &nbsp;
                         </td>
                         <td style="width: 6px; height: 21px; background-color: white">
                         
                         <asp:HyperLink ID="HyperLink3" runat="server" 
                                NavigateUrl="~/CSMSSurveyorLogin/SurveyorLogin_Godown.aspx">Godown Surveyor Login</asp:HyperLink>
                             <asp:Image ID="Image3" runat="server" Height="19px" 
                                ImageUrl="~/IssueCenter/img/blinking_new.gif" Width="39px" />
                         </td>
                     </tr>
                     <tr>
                         <td align="left" style="width: 276px; height: 21px; background-color: white">
                             <asp:RadioButton ID="rbCollector" runat="server" AutoPostBack="True" 
                                 ForeColor="Maroon" GroupName="1"
                                TabIndex="10" Text="Collector and DIO's" ValidationGroup="1" 
                                 Font-Bold="True" Font-Italic="False" Font-Names="Georgia" 
                                 Font-Size="14px" /><br />
                             <asp:Label ID="Label12" runat="server" Visible="False"></asp:Label></td>
                         <td colspan="2" style="height: 21px; background-color: white">
                             <asp:Panel ID="panelCollector" runat="server">
                                 <table style="width: 350px">
                                     <tr>
                                         <td align="center" colspan="2" style="color: white; background-color: white">
                                             <asp:Label ID="Label7" runat="server" Text="Select Role" Width="435px"></asp:Label></td>
                                     </tr>
                                     <tr>
                                         <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                        width: 71px; border-bottom: white 1px solid; background-color: #cfdcdc; height: 26px;" align="left">
                                             <asp:Label ID="Label10" runat="server" Text="Select Role"></asp:Label></td>
                                         <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                        border-bottom: white 1px solid; width: 157px; background-color: #cfdcdc; height: 26px;">
                                             <asp:DropDownList ID="ddl_Collector" runat="server" OnSelectedIndexChanged="ddl_Collector_SelectedIndexChanged"
                                            Width="147px" AutoPostBack="True">
                                                 <asp:ListItem Value="Collector">Collector</asp:ListItem>
                                                 <asp:ListItem Value="DIO">DIO</asp:ListItem>
                                             </asp:DropDownList></td>
                                     </tr>
                                     <tr>
                                         <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                        width: 71px; border-bottom: white 1px solid; background-color: #cfdcdc;" align="left">
                                             <asp:Label ID="Label11" runat="server" Text="District "></asp:Label></td>
                                         <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                        border-bottom: white 1px solid; width: 157px; background-color: #cfdcdc;">
                                             <asp:DropDownList ID="ddl_dist" runat="server" Width="148px" TabIndex="5" AutoPostBack="True" OnSelectedIndexChanged="ddl_dist_SelectedIndexChanged">
                                             </asp:DropDownList></td>
                                     </tr>
                                 </table>
                             </asp:Panel>
                         </td>
                         <td style="width: 6px; height: 21px; background-color: white">
                         </td>
                     </tr>
                    <tr>
                        <td align="left" style="background-color: white; width: 276px;">
                            <asp:RadioButton ID="rdbdm" runat="server" AutoPostBack="True" 
                                ForeColor="Maroon" GroupName="1"
                                TabIndex="10" Text="District Food Office" ValidationGroup="1" 
                                Font-Bold="True" Font-Italic="False" Font-Names="Georgia" 
                                Font-Size="14px" Width="183px" /></td>
                        <td style="background-color: white; width: 6px;" align="left">
                            <asp:DropDownList ID="ddldistrictdm" runat="server" TabIndex="11" Visible="False"
                                Width="148px">
                            </asp:DropDownList></td>
                        <td style="background-color: white; width: 6px;">
                            &nbsp;</td>
                        <td style="background-color: white; width: 6px;">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="background-color: white; width: 276px;">
                            <asp:RadioButton ID="rdbdccb" runat="server" AutoPostBack="True" ForeColor="Maroon"
                                GroupName="1" TabIndex="12" Text="DCCB Login" ValidationGroup="1" 
                                Font-Bold="True" Font-Italic="False" Font-Names="Georgia" 
                                Font-Size="14px" Width="157px" /></td>
                        <td style="background-color: white; width: 6px;" align="left">
                            <asp:DropDownList ID="dccbdistrict" runat="server" TabIndex="13" Visible="False"
                                Width="148px">
                            </asp:DropDownList></td>
                        <td style="background-color: white; width: 6px;">
                           <a href="User_Manual/Mdm_manual.pptx" target="_search" >     <span style="font-weight:bold;">MDM Manual</span> 
                        
                            
                        </td>
                        <td style="background-color: white; width: 6px;">
                        
                            
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="background-color: white; width: 276px;">
                            <asp:RadioButton ID="rdbadmin" runat="server" AutoPostBack="True" ForeColor="Maroon"
                                GroupName="1" TabIndex="12" Text="Admin Login" ValidationGroup="1" 
                                Font-Bold="True" Font-Italic="False" Font-Names="Georgia" 
                                Font-Size="14px" /></td>
                        <td style="background-color: white;" align="left">
                            <asp:DropDownList ID="ddladmin" runat="server" TabIndex="13" Visible="False" Width="149px">
                            </asp:DropDownList></td>
                        <td style="background-color: white;">
                         <p style="color: mediumblue; font-family: 'Georgia'">
                    <a href="../csms/User_Manual/User_Mannual.pdf" target="_search" ><span style="color: #000066; font-size: 10pt; text-decoration: underline;">User Manual English</span></a> 
                </p>
                        
                        </td>
                        <td style="background-color: white;">
                        </td>
                    </tr>
                     <tr>
                         <td align="left" style="background-color: #4892FF; width: 276px;">
                             <asp:RadioButton ID="rdbagency" runat="server" AutoPostBack="True" ForeColor="Navy"
                                GroupName="1" TabIndex="12" Text="Agency Login" ValidationGroup="1" 
                                 Font-Bold="True" Font-Italic="False" Font-Names="Georgia" Font-Size="13px" 
                                 style="color: #FFFFCC" /></td>
                         <td align="left" style="background-color: #4892FF">
                             <asp:DropDownList ID="ddlAgency" runat="server" TabIndex="13" Visible="False" Width="149px">
                             </asp:DropDownList></td>
                         <td style="background-color: #4892FF">
                             <p style="color: mediumblue; font-family: 'Georgia'">
                    <a href="User_Manual/CSMS_UMHindi.pdf" target="_search" >
                                 <span style="font-size: 10pt; text-decoration: underline;" class="style1">User Manual Hindi</span></a><span 
                                     class="style1"> </span> 
                </p>
                         </td>
                         <td style="background-color: #4892FF">
                         </td>
                     </tr>
                    <tr>
                        <td align="left" style="background-color: #cccccc; width: 276px;">
                            <asp:Label ID="lblerror" runat="server" Text="" Visible="false"></asp:Label></td>
                        <td style="background-color: #cccccc" align="left">
                            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="#C00000"
                                Visible="False"></asp:Label></td>
                        
                        <td style="background-color: #cfdcdc; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid; height: 1px;" 
                            colspan="2">
                         <%--<p style="color: mediumblue; font-family: 'Georgia'">
                    <a href="../csms/User_Manual/CSMS_DO_Manual.pdf" target="_search" ><span style="color: #000066; font-size: 10pt; text-decoration: underline;">User Manual New</span></a> 
                </p>--%>
                        
                         <p style="color: mediumblue; font-family: 'Georgia'; text-align: left;">
                             &nbsp;</p>
                        
                        </td>
                       
                        
                    </tr>
                    <tr>
                        <td align="left" style="background-color: #cccccc; width: 276px;">
                            <asp:Label ID="Label2" runat="server" Text="Enter Password" Font-Bold="True" Font-Italic="True" Font-Names="Georgia" ForeColor="Green"></asp:Label></td>
                        <td style="background-color: #cccccc" align="left">
                            <asp:TextBox ID="txtpwd" runat="server" OnTextChanged="txtpwd_TextChanged" TabIndex="14"
                                TextMode="Password" Width="150px" BorderColor="Blue" BorderStyle="Groove"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtpwd"
                                ErrorMessage="Please Enter Password" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
                       
                        <td style="background-color: #cfdcdc; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid; height: 1px;" colspan="2">
                         <p style="color: mediumblue; font-family: 'Georgia'; text-align: left;">
                             &nbsp;</p>
                        
                        </td>
                        
                        
                           
                    </tr>
                    <tr>
                        <td align="left" style="background-color: #cccccc; width: 276px;">
                        
                You have <B><SPAN id=myCounter>15</SPAN></B> characters remaining
                        
                        
                        
                        </td>
                        <td style="background-color: #cccccc">
                            <asp:ImageButton ID="btnlogin" runat="server" ImageUrl="~/Images/logimage.bmp" OnClick="btnlogin_Click"
                                TabIndex="15" ValidationGroup="1" BorderColor="Black" BorderStyle="Groove" 
                                style="margin-top: 0px" /></td>
                        <td style="background-color: #cccccc">
                            <asp:CheckBox ID="chkhindi" runat="server" ForeColor="Navy" Text="Hindi Version"
                                Width="107px" /></td>
                        <td style="background-color: #cccccc; width: 6px;">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 276px; background-color: #cfdcdc;">
                            &nbsp;</td>
                        <td style="width: 214px; background-color: #cfdcdc">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                ValidationGroup="1" />
                        </td>
                        <td style="width: 214px; background-color: #cfdcdc">
                        
                         
                        
                        </td>
                        <td style="width: 214px; background-color: #cfdcdc;">
                        </td>
                    </tr>
                
                </table>
               
               
               
               </td> 
            </tr> 
            </table>
            </center>
      
      
      
      
      
      
       

<table border="1" cellspacing="0" cellpadding="0" width="100%" align="center" 
            
            style="border-right: gray 1px solid; border-top: gray 1px solid; border-left: gray 1px solid; border-bottom: gray 1px solid; height: 66px; background-color: #CCFFCC; border-style: groove;">
    <tr>
        <td style="border: thin groove #99CCFF; height: 10px; font-weight: bold; font-size: 12px; color: green; background-color: #FFCC66;" 
            align="center">
             Site Designed and Hosted By:</td>
        <td style="border: thin groove #99CCFF; height: 10px; font-weight: bold; font-size: 12px; color: green; background-color: #FFCC66;" 
            align="center">
           Contents Provided By:</td>
    </tr>
<tr>

<center>
	<td style="height: 82px; text-align: center;"><p>
        <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="Arial Narrow" Font-Size="12px"
            ForeColor="Navy" Height="47px" Width="320px" Font-Bold="True">National Informatics Centre, Madhya Pradesh
Ministry of Communications and Information Technology
Department of Information Technology</asp:HyperLink>&nbsp;</p></td>
</center><center>
	      <td style="height: 82px; text-align: center;"> 
            <p class="bot"><b> <a href="">
                <asp:HyperLink ID="HyperLink4" runat="server" Font-Names="Arial Narrow" Font-Size="12px"
                    ForeColor="Navy" Height="35px" Width="338px">Madhya Pradesh State Civil Supplies Corporation Ltd. Block-1, 3rd Floor, Paryavas Bhavan, Jail Road, Bhopal- 462011 India  Phones : 91-755-2768590, Fax :91-755-2677847 Email : mpscsc@bsnl.in </asp:HyperLink></a></b></p>
	</td></center>
</tr>
</table>
	<td valign="bottom" background="images/bg_right.gif"><img src="images/bg_right.gif" alt="" width="17" height="16" border="0"></td>
	<td width="50%" background="images/bg.gif"><img src="images/px1.gif" width="1" height="1" alt="" border="0"></td>
    </tr> 
	</table> 
    
    </form>
	
	
</body>
</html>
