<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="PcGdn_Insp_Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
    
     <link rel="stylesheet" href ="../MyCSS/xp_comon.css" type ="text/css"  />    
    <link rel="stylesheet" type="text/css" href="../MyCSS/style.css">
    <link rel="stylesheet" href ="../MyCSS/xp_menu.css" type ="text/css" />
    <link rel="stylesheet" type="text/css" href="../sdmenu/sdmenu.css"/>
      <script type="text/javascript" src ="../js/chksql.js"></script>
       <script type="text/javascript" src="../js/md5.js"></script>
       
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

            var sheets = document.styleSheets;
            sheets[0].disabled = true;
            sheets[1].disabled = true;
            sheets[2].disabled = true;
            sheets[3].disabled = true;
            var windt = navigator.userAgent.toLowerCase();
            var winindx = 'null';
            var win_xp = 'null';
            winindx = windt.match('win');
            win_xp = windt.match('nt 5.1');
            if (win_xp == 'nt 5.1' && winindx == 'win') {
                sheets[0].disabled = false;
                sheets[2].disabled = false;
            }
            else {
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
            text-decoration: none;
        }
        .style2
        {
            font-family: Verdana;
            font-weight: bold;
        }

        .style4
        {
            height: 40px;
        }
        .style5
        {
            height: 36px;
        }

        .style6
        {
            height: 26px;
        }

        .style7
        {
            height: 34px;
        }

        </style>

    <script type="text/javascript">
     window.history.forward(0); 
    </script>
    
    <script language = "Javascript">
        /**
        * DHTML textbox character counter script. Courtesy of SmartWebby.com (http://www.smartwebby.com/dhtml/)
        */

        var maxL = 15;
        var bName = navigator.appName;
        function taLimit(taObj) {
            if (taObj.value.length == maxL) return false;
            return true;
        }

        function taCount(taObj, Cnt) {
            objCnt = createObject(Cnt);
            objVal = taObj.value;
            if (objVal.length > maxL) objVal = objVal.substring(0, maxL);
            if (objCnt) {
                if (bName == "Netscape") {
                    objCnt.textContent = maxL - objVal.length;
                }
                else { objCnt.innerText = maxL - objVal.length; }
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

    var delayb4scroll = 2000 //Specify initial delay before marquee starts to scroll on page (2000=2 seconds)
    var marqueespeed = 1 //Specify marquee scroll speed (larger is faster 1-10)
    var pauseit = 1 //Pause marquee onMousever (0=no. 1=yes)?

    ////NO NEED TO EDIT BELOW THIS LINE////////////

    var copyspeed = marqueespeed
    var pausespeed = (pauseit == 0) ? copyspeed : 0
    var actualheight = ''

    function scrollmarquee() {
        if (parseInt(cross_marquee.style.top) > (actualheight * (-1) + 8))
            cross_marquee.style.top = parseInt(cross_marquee.style.top) - copyspeed + "px"
        else
            cross_marquee.style.top = parseInt(marqueeheight) + 8 + "px"
    }

    function initializemarquee() {
        cross_marquee = document.getElementById("vmarquee")
        cross_marquee.style.top = 0
        marqueeheight = document.getElementById("marqueecontainer").offsetHeight
        actualheight = cross_marquee.offsetHeight
        if (window.opera || navigator.userAgent.indexOf("Netscape/7") != -1) { //if Opera or Netscape 7x, add scrollbars to scroll and exit
            cross_marquee.style.height = marqueeheight + "px"
            cross_marquee.style.overflow = "scroll"
            return
        }
        setTimeout('lefttime=setInterval("scrollmarquee()",30)', delayb4scroll)
    }




    if (window.addEventListener)
        window.addEventListener("load", initializemarquee, false)
    else if (window.attachEvent)
        window.attachEvent("onload", initializemarquee)
    else if (document.getElementById)
        window.onload = initializemarquee


</script>
    
    <style type="text/css">
        .style8
        {
            height: 17px;
        }
        .style10
        {
            height: 21px;
            width: 131px;
        }
        .style13
        {
            height: 23px;
            width: 131px;
        }
        .style16
        {
            height: 7px;
            text-align: right;
        }
        .style17
        {
            height: 5px;
        }
        .style18
        {
            height: 14px;
            width: 131px;
        }
        .style19
        {
            height: 23px;
            width: 361px;
        }
        .style20
        {
            height: 21px;
            width: 361px;
        }
        .style21
        {
            height: 14px;
            width: 361px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <center>
    <table style="border: thin groove #000000; width: 518px; height: 241px" >
        <tr>
            <td colspan = "2" style="color: #FFFFFF; background-color: #003399" 
                class="style8" align = "center" >
                &nbsp;Login</td>
        </tr>
        <tr>
            <td class="style13" >
                User Type</td>
            <td class="style19" >
                <asp:DropDownList ID="ddluser" runat="server" Width="250px" Height="34px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style10" >
                District</td>
            <td class="style20" >
                <asp:DropDownList ID="ddldistrict" runat="server" Width="250px" Height="35px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style10" >
                Password</td>
            <td class="style20" >
                <asp:TextBox ID="txtpassword" runat="server" TextMode="Password" Width="250px" 
                    BorderColor="Blue" BorderStyle="Groove" ontextchanged="txtpassword_TextChanged" Height="30px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="background-color: #FFFFCC" class="style17" >
                </td>
        </tr>
        <tr>
            <td class="style18" >
            </td>
            <td class="style21" >
                <asp:Button ID="btnlogin" runat="server" Text="Login"  Height="27px" 
                    Width="80px" onclick="btnlogin_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2" class="style16" >
                
                <asp:LinkButton ID="lnkmainpage" runat="server" onclick="lnkmainpage_Click">निगम की वेबसाइट पर वापस जाएँ  |</asp:LinkButton>
                
                </td>
        </tr>
    </table>
    </center>
    </div>
    </form>
</body>
</html>
