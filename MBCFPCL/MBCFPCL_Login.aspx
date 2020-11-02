<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MBCFPCL_Login.aspx.cs" Inherits="MBCFPCL_MBCFPCL_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body
        {
            background-color: rgba(204, 207, 208, 0.22);
        }

        .box
        {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            width: 1000px;
            height: 500px;
            padding: 40px;
            /*background: rgba(0,0,0,.8);*/
            background-color: rgba(71, 107, 119, 1);
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
            width: 50%;
            padding: 10px 0;
            font-size: 12px;
            color: #948686;
            padding-left: 20px;
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
            left: 224px;
            color: #03a9f4;
            font-size: 12px;
        }


        .box .inputBox #txtpassword:focus ~ #lblpass,
        .box .inputBox #txtpassword:valid ~ #lblpass
        {
            top: -20px;
            left: 224px;
            color: #03a9f4;
            font-size: 12px;
        }




        .bttsub
        {
            background: transparent;
            border: none;
            outline: none;
            color: #fff;
            background: #e74c3c;
            padding: 10px 20px;
            cursor: pointer;
            border-radius: 5px;
            letter-spacing: 4px;
            font-family: sans-serif;
            width: 200px;
            height: 35px;
        }

            .bttsub:hover
            {
                background: #e74c3c;
            }

        .sign
        {
            color: rgba(223,64,90,1);
            font-size: 15px;
            text-decoration: none;
            letter-spacing: 2px;
        }

        .ParaInfo
        {
            font-family: 'Bellefair';
            letter-spacing: 2px;
            font-size: 14px;
            color: #fff;
        }

        .btn
        {
            flex: 1 1 auto;
            margin: 10px;
            padding: 30px;
            text-align: center;
            text-transform: uppercase;
            transition: 0.5s;
            background-size: 200% auto;
            color: white;
            /* text-shadow: 0px 0px 10px rgba(0,0,0,0.2);*/
            box-shadow: 0 0 20px #eee;
            border-radius: 10px;
        }

            /* Demo Stuff End -> */

            /* <- Magic Stuff Start */

            .btn:hover
            {
                background-position: right center; /* change the direction of the change here */
            }

        .btn-1
        {
            background-image: linear-gradient(to right, #f6d365 0%, #fda085 51%, #f6d365 100%);
        }

        .btnMB
        {
            width: 200px;
            height: 35px;
            color: #fff;
            font-family: 'Bellefair';
            letter-spacing: 2px;
            font-size: 20px;
            font-weight: bolder;
        }
         .GSHeaderColumn
        {
            width: 100%;
            /*color: #e74c3c;*/
            color: rgba(71, 107, 119, 1);
            font-size: 25px;
            font-family: 'Bellefair';
            letter-spacing: 4px;
        }

    </style>

    <style>
        body
        {
            font-family: Arial, Helvetica, sans-serif;
        }

        /* The Modal (background) */
        .modal
        {
            display: none; /* Hidden by default */
            position: fixed; /* Stay in place */
            z-index: 1; /* Sit on top */
            padding-top: 10px; /* Location of the box */
            left: 0;
            top: 0;
            width: 100%; /* Full width */
            /*height: 100%;*/ /* Full height */
            height: 490px;
            overflow: auto; /* Enable scroll if needed */
            background-color: rgb(0,0,0); /* Fallback color */
            background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
            border-radius: 10px;
        }

        /* Modal Content */
        .modal-content
        {
            /*background-color: #fefefe;*/
            background: black;
            box-sizing: border-box;
            box-shadow: 0 15px 25px rgba(0,0,0,.5);
            border-radius: 10px;
            margin: auto;
            padding: 20px;
            border: 1px solid #888;
            width: 95%;
            height: 480px;
        }

        /* The Close Button */
        .close
        {
            color: #aaaaaa;
            float: right;
            font-size: 28px;
            font-weight: bold;
        }

            .close:hover,
            .close:focus
            {
                color: #e74c3c;
                text-decoration: none;
                cursor: pointer;
            }
              .sign
        {
            color: rgba(71, 107, 119, 1);
            font-size: 12px;
            text-decoration: none;
            letter-spacing: 2px;
        }
    </style>

   

 <script type="text/javascript" src="../js/chksql.js"></script>
    <script type="text/javascript" src="../js/md5.js"></script>
   <%-- <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>--%>
  <%--  <script type="text/javascript">
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

    </script>--%>
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
  
</head>
<body>
   
    <table style="width:100%" >
      
        <tr style="width:100%">
            <td style="width:5%; ">
                 <a href="../MainLogin.aspx" class="sign">&#9754 Back
                    </a>
            </td>
            <td style="width:95%; text-align:center" class="GSHeaderColumn">
   &nbsp;<asp:Image ID="Image3" ImageUrl="~/Images/g3409.png" Width="50px" Height="30px" CssClass="image" runat="server" /><br />
&nbsp;

                                Madhya Pradesh State Civil Supplies Corporation Ltd.
            </td>
        </tr>
    </table>


    <div class="box">
        <h2 style="color: #fff; font-size:14px;  font-family: 'Bellefair'; letter-spacing: 4px;">
            <asp:Image ID="Image1" ImageUrl="~/Images/MBCFPCL.png" CssClass="image"  runat="server" /><br />Madhya Bharat Consortium of Farmer Producers Company Limited</h2>

        
        <form runat="server" id="form2">
              <div class="inputBox" style="height:75px;">

                  <marquee direction = "up">
              <table style="width:100%; text-align:center;">
                  <tr>
                      <td style="width:33%;text-align:center;">
                           <asp:Image ID="Image4" ImageUrl="~/Images/chana.jpg" Height="150px" Width="250px" CssClass="image"  runat="server" />
                      </td>
                       <td style="width:33%; text-align:center;">
 <asp:Image ID="Image5" ImageUrl="~/Images/masoor.jpg" CssClass="image" Height="150px" Width="250px"  runat="server" />
                      </td>
                       <td style="width:33%; text-align:center;">
 <asp:Image ID="Image6" ImageUrl="~/Images/Sarso_farm.JPG" CssClass="image" Height="150px" Width="250px"  runat="server" />
                      </td>
                  </tr>

              </table>
                      </marquee>
            </div>
               <div class="inputBox" style="height:70px;">
                   </div>
            <div class="inputBox">
                <p class="ParaInfo">
                    Madhya Bharat Consortium of Farmer Producers Company Limited (MBCFPCL) was incorporated on 18th September 2014 at Bhopal of Madhya Pradesh State of India. It was registered under Part IXA and section 581B of Company Act 1956 which is also known as Company (amendment) Act 2002. It is an advance version of Cooperatives Act formally known as Producer Company Act 2002 which is hybrid of cooperatives and company Act. This organization was jointly promoted by Government Organization like Small Farmers Agribusiness Consortium (SFAC), MoA & Cooperartion, GoI, New Delhi, Dept of Farmer Welfare & Agri Development, Govt. of M.P, MP State Rural Livelihood Mission (MPSRLM) & Lead Dev. Organizations like Rabo Bank Foundation, ASA, Vrutti, ADS, IGS, MCM along with others.
                </p>
            </div>
           
         <div class="inputBox" style="height:8px;">
                   </div>
            <center>
                <table style="width:100%">
                    <tr>
                        <td style="width:100%; text-align:right" >
                              <%-- <asp:Button ID="bttsub" runat="server" Text="Login" class="btn btn-danger btn-default btnMB "  />--%>

                           <%-- <button id="myBtn">Open Modal</button>--%>
                            <input type="button" id="myBtn" class="bttsub btnMB" value="Login" />
                        </td>
                    </tr>
                </table>
             
           <div id="myModal" class="modal">

  <!-- Modal content -->
  <div class="modal-content">
    <span class="close">&times;</span>
       <asp:Image ID="Image2" ImageUrl="~/Images/MBCFPCL.png" CssClass="image" runat="server" />
      <br />
      <br />
      <br />
      <br />
      <br />
    <div class="inputBox">
         <%--<asp:TextBox ID="txtuser" runat="server" CssClass="txt" AutoComplete="off" MaxLength="10"></asp:TextBox>--%>
         <asp:DropDownList ID="txtuser" runat="server"  CssClass="txt" >
                    </asp:DropDownList>
                <%-- <label id="lbluser" class="lbl">Username</label>--%>
                <asp:Label runat="server" Text="Label" ID="lbluser" class="lbl"> District</asp:Label>
        </div>
       <div class="inputBox">
            <asp:TextBox ID="txtpassword" runat="server" TextMode="Password" CssClass="txt" Style="padding-left:20px;"></asp:TextBox>
                <%-- <label id="lblpass" class="lbl">Password</label>--%>
                <asp:Label runat="server" Text="Label" class="lbl" ID="lblpass">Password</asp:Label>
        </div>
       <center>

           
            <asp:Button ID="bttsub" runat="server" Text="Login" CssClass="bttsub" OnClick="bttsub_Click"  />
                 </center>
  </div>

</div>

<script>
    // Get the modal
    var modal = document.getElementById('myModal');

    // Get the button that opens the modal
    var btn = document.getElementById("myBtn");

    // Get the <span> element that closes the modal
    var span = document.getElementsByClassName("close")[0];

    // When the user clicks the button, open the modal 
    btn.onclick = function () {
        modal.style.display = "block";
    }

    // When the user clicks on <span> (x), close the modal
    span.onclick = function () {
        modal.style.display = "none";
    }

    // When the user clicks anywhere outside of the modal, close it
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }
</script>
          
                 </center>
        </form>
    </div>


</body>
</html>
