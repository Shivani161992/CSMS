<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Surveyor_Login.aspx.cs" Inherits="SurveyorLogin_Uparjan_Surveyor_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

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
        body
        {
            background: rgba(30,29,31,1);
            background: -moz-linear-gradient(-45deg, rgba(30,29,31,1) 0%, rgba(223,64,90,1) 100%);
            background: -webkit-gradient(left top, right bottom, color-stop(0%, rgba(30,29,31,1)), color-stop(100%, rgba(223,64,90,1)));
            background: -webkit-linear-gradient(-45deg, rgba(30,29,31,1) 0%, rgba(223,64,90,1) 100%);
            background: -o-linear-gradient(-45deg, rgba(30,29,31,1) 0%, rgba(223,64,90,1) 100%);
            background: -ms-linear-gradient(-45deg, rgba(30,29,31,1) 0%, rgba(223,64,90,1) 100%);
            background: linear-gradient(135deg, rgba(30,29,31,1) 0%, rgba(223,64,90,1) 100%);
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#1e1d1f', endColorstr='#df405a', GradientType=1 );
        }

        .main
        {
            position: fixed;
            width: 100%;
            background-color: silver;
            letter-spacing: 2px;
        }

        .header
        {
            color: #fff;
            font-size: 35px;
            font-family: sans-serif;
            text-align: center;
        }

        .menu
        {
            height: 23px;
            box-shadow: 10px 10px 15px rgba(0,0,0,0.5);
        }

        .box
        {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            width: 600px;
            padding: 40px;
            background: rgba(0,0,0,.8);
            box-sizing: border-box;
            box-shadow: 0 15px 25px rgba(0,0,0,.5);
            border-radius: 10px;
        }

            .box h2
            {
                margin: 0 0 30px;
                padding: 0;
                color: #fff;
                color: darkseagreen;
                text-align: center;
            }

            .box .inputBox
            {
                position: relative;
            }

        .txt
        {
            width: 100%;
            padding: 10px 0;
            font-size: 16px;
            color: #fff;
            margin-bottom: 30px;
            border: none;
            border-bottom: 1px solid #fff;
            outline: none;
            background: transparent;
            letter-spacing: 4px;
            font-family: sans-serif;
        }

        .lbl
        {
            position: absolute;
            top: 0;
            left: 0;
            padding: 10px 0;
            font-size: 16px;
            color: #fff;
            pointer-events: none;
            transition: .5s;
            letter-spacing: 4px;
            font-family: sans-serif;
        }



        .box .inputBox #txtuser:focus ~ #lbluser,
        .box .inputBox #txtuser:valid ~ #lbluser
        {
            top: -20px;
            left: 0;
            color: #03a9f4;
            font-size: 12px;
        }


        .box .inputBox #txtpassword:focus ~ #lblpass,
        .box .inputBox #txtpassword:valid ~ #lblpass
        {
            top: -20px;
            left: 0;
            color: #03a9f4;
            font-size: 12px;
        }




        .bttsub
        {
            background: transparent;
            border: none;
            outline: none;
            color: #fff;
            background: #3498db;
            padding: 10px 20px;
            cursor: pointer;
            border-radius: 5px;
            letter-spacing: 4px;
            font-family: sans-serif;
            width: 50%;
            height: 40px;
        }
    </style>
</head>
<body>

    <div>
      

    </div>

    <div class="box">
        <h2 style="color: #e74c3c; font-size: 35px; font-family: 'Bellefair'; letter-spacing: 4px;">Surveyor Login</h2>
        <form runat="server" id="form2">
            <div class="inputBox">
                <asp:TextBox ID="txtuser" runat="server" CssClass="txt" AutoComplete="off"></asp:TextBox>
                <%-- <label id="lbluser" class="lbl">Username</label>--%>
                <asp:Label runat="server" Text="Label" ID="lbluser" class="lbl"> Username</asp:Label>

            </div>
            <div class="inputBox">
                <asp:TextBox ID="txtpassword" runat="server" TextMode="Password" CssClass="txt"></asp:TextBox>
                <%-- <label id="lblpass" class="lbl">Password</label>--%>
                <asp:Label runat="server" Text="Label" class="lbl" ID="lblpass">Password</asp:Label>

            </div>
            <center>

           
            <asp:Button ID="bttsub" runat="server" Text="Login" CssClass="bttsub" OnClick="bttsub_Click" />
                 </center>
        </form>
    </div>

</body>
</html>
