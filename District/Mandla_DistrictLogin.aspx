<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Mandla_DistrictLogin.aspx.cs" Inherits="District_Mandla_DistrictLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <%-- <link rel="stylesheet" href ="../MyCSS/xp_comon.css" type ="text/css"  />    
    <link rel="stylesheet" type="text/css" href="../MyCSS/style.css">
    <link rel="stylesheet" href ="../MyCSS/xp_menu.css" type ="text/css" />
    <link rel="stylesheet" type="text/css" href="../sdmenu/sdmenu.css"/>--%>
    <script type="text/javascript" src="../js/chksql.js"></script>
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
    <script type="text/javascript">
        window.history.forward(0);
    </script>

    <script language="Javascript">
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
  
    
     <style>
        .Box
        {
            position: absolute;
           
            
            width: 500px;
            padding: 40px;
            background: rgba(8,80,120,0.11);
            border-radius: 10px;
        }

        .surtable
        {
            width: 500px;
            border-top: 6px groove silver;
            border-bottom: 6px groove silver;
            border-left: 6px groove silver;
            border-right: 6px groove silver;
            border-radius: 8px;
        }

        .surveLinks
        {
            text-decoration: none;
            font-family: 'Bellefair';
            letter-spacing: 2px;
            font-size: 16px;
        }

            .surveLinks:hover
            {
                text-decoration: dotted;
                color: #ff6b5b;
            }
        .insptxt
         {

             width: 45%;
            height: 25px;
            background-color:white;
               border-radius: 5px;
               border:none;
               outline: none;
               padding-left:20px;
color:gray;
     }
         .bttsub
        {
            background: transparent;
            border: none;
            outline: none;
            color: #fff;
            background: #3498db;
          
            cursor: pointer;
            border-radius: 5px;
            letter-spacing: 4px;
            font-family: sans-serif;
            width: 50%;
            height: 25px;
        }
    </style>
<div class="Box">
        <h2 style="color: #e67e22; font-size: 20px; font-family: 'Bellefair'; letter-spacing: 4px; text-decoration-style: wavy; text-align: center;">Mandla Login</h2>
        <table class="surtable">
             <tr>
                 <td style="text-align:center; height:25px" >
                 
                </td>
                    </tr>
            <tr>
                <td style="text-align:center; height:25px" >
                  <asp:TextBox ID="txtpassword" CssClass="insptxt" runat="server" TextMode="Password" placeholder="Enter Password"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtpassword" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator> 
                </td>
                </tr>
                <tr>
                 <td style="text-align:center; height:25px" >
                 
                </td>
                    </tr>
                     <tr>
                 <td style="text-align:center; height:25px" >
                    <asp:Button ID="bttsub" runat="server" Text="Login" CssClass="bttsub" OnClick="bttsub_Click" />
                </td>
            </tr>
            <tr>
                 <td style="text-align:center; height:25px" >
                 
                </td>
                    </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

