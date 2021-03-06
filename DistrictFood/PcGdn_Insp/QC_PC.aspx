﻿<%@ Page Language="C#" MasterPageFile="~/PcGdn_Insp/InspMaster.master" AutoEventWireup="true" CodeFile="QC_PC.aspx.cs" Inherits="PcGdn_Insp_QC_PC" Title="PC Inspection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>
 <script type="text/javascript">
     function CheckIsNumeric(tx) {
         var AsciiCode = event.keyCode;
         var txt = tx.value;
         var txt2 = String.fromCharCode(AsciiCode);
         var txt3 = txt2 * 1;
         if ((AsciiCode < 46) || (AsciiCode > 57)) {
             alert('Please enter only numbers.');
             event.cancelBubble = true;
             event.returnValue = false;
         }

         var num = tx.value;
         var len = num.length;
         var indx = -1;
         indx = num.indexOf('.');
         if (indx != -1) {
             var coda = event.keyCode
             if (coda == 46) {
                 alert('Decimal cannot come twice');
                 event.cancelBubble = true;
                 event.returnValue = false;
             }
             var dgt = num.substr(indx, len);
             var count = dgt.length;
             //alert (count);
             if (count > 5) {
                 alert("Only 5 decimal digits allowed");
                 event.cancelBubble = true;
                 event.returnValue = false;
             }
         }

     }
     function IsNumericProcQty(key, txt) {
         var keycode = (key.which) ? key.which : key.keyCode;
         var num = txt.value;
         var len = num.length;
         var indx = -1;
         indx = num.indexOf('.');

         var dgt = num.substr(indx, len);
         var count = dgt.length;

         if (keycode == 08) {
             return true;
         }
         else if (keycode == 59 || keycode == 32) {
             alert('Semi Colon (;) & Blank Space Not Allowed ...');
             return false;
         }
         else if (keycode == "36" || keycode == "37" || keycode == "38" || keycode == "40" || keycode == "41" || keycode == "43" || keycode == "92" || keycode == "124" || keycode == "34" || keycode == "39" || keycode == "60" || keycode == "62" || keycode == "44" || keycode == "64" || keycode == "61" || keycode == "63") {
             alert('Do not use SQL Key-Words, Semi Colon(;) and Special Characters(&,%,$)..etc');
             return false;
         }

         else if (keycode == 09) {
             if (num > 99999) {
                 alert(' मात्रा 100000 से अधिक प्रविष्ट नहीं कर सकते हें|');
                 txt.value = "0";
                 return false;
             }
         }

         else if (num > 99999) {
             alert('मात्रा 100000 से अधिक प्रविष्ट नहीं कर सकते हें|');
             txt.value = "0";
             return false;
         }

         else if (count > 5) {
             alert("दशमलव के बाद 5 अंक ही आ सकते है");
             return false;
         }

         else if (keycode == 46) {
             if (num.split(".").length > 1) {
                 alert('दशमलव एक ही बार आ सकता है |');
                 return false;
             }
         }
         else if (keycode >= 48 && keycode <= 58) {
             return true;
         }
         else {
             alert('कृपया संख्या ही प्रविष्ट करें |');
             return false;
         }
     }


     function IsNumericProcQtyBag(key, txt) {
         var keycode = (key.which) ? key.which : key.keyCode;
         var num = txt.value;
         var len = num.length;
         var indx = -1;
         indx = num.indexOf('.');

         var dgt = num.substr(indx, len);
         var count = dgt.length;

         if (keycode == 08) {
             return true;
         }

         else if (keycode == 59 || keycode == 32) {
             alert('Semi Colon (;) & Blank Space Not Allowed ...');
             return false;
         }
         else if (keycode == "36" || keycode == "37" || keycode == "38" || keycode == "40" || keycode == "41" || keycode == "43" || keycode == "92" || keycode == "124" || keycode == "34" || keycode == "39" || keycode == "60" || keycode == "62" || keycode == "44" || keycode == "64" || keycode == "61" || keycode == "63") {
             alert('Do not use SQL Key-Words, Semi Colon(;) and Special Characters(&,%,$)..etc');
             return false;
         }

         else if (keycode == 09) {
             if (num > 10000) {
                 alert(' मात्रा 100000 से अधिक प्रविष्ट नहीं कर सकते हें|');
                 txt.value = "0";
                 return false;
             }
         }

         else if (num > 10000) {
             alert('मात्रा 100000 से अधिक प्रविष्ट नहीं कर सकते हें|');
             txt.value = "0";
             return false;
         }
         else if (keycode == 46) {
             if (num.split(".").length > 0) {
                 alert('दशमलव नहीं आ सकता है |');
                 return false;
             }
         }

         else if (keycode >= 48 && keycode <= 58) {
             return true;
         }
         else {
             alert('कृपया संख्या ही प्रविष्ट करें |');
             return false;
         }
     }

     function chkSqlKey(key, txt) {

         var keycode = (key.which) ? key.which : key.keyCode;

         if (keycode == 59 || keycode == 32) {
             alert('Semi Colon (;) & Blank Space Not Allowed ...');
             return false;
         }

         else if (keycode == "36" || keycode == "37" || keycode == "38" || keycode == "40" || keycode == "41" || keycode == "43" || keycode == "92" || keycode == "124" || keycode == "34" || keycode == "39" || keycode == "60" || keycode == "62" || keycode == "44" || keycode == "64" || keycode == "61" || keycode == "63") {
             alert('Do not use SQL Key-Words, Semi Colon(;) and Special Characters(&,%,$)..etc');
             return false;
         }
     }
     function ChkNotgrate() {
         var ctrlisqty = document.getElementById('ctl00_ContentPlaceHolder1_txtissueqty');
         var ctrlrvqty = document.getElementById('ctl00_ContentPlaceHolder1_txtrecdqty');
         var ctxisqty = ctrlisqty.value
         var ctxtrcvqty = ctrlrvqty.value

         if (ctxtrcvqty == txisqty * 100) {
             alert("NOT grdn 10 times");
             return false
         }
         return true
     }
    </script> 
 
  <script type="text/javascript">

      function TimerFunc() 
      {
          var c = 300;
          var t;
          timedCount();

          function timedCount() 
          {

              var hours = parseInt(c / 3600) % 24;
              var minutes = parseInt(c / 60) % 60;
              var seconds = c % 60;

              //var result = (hours < 10 ? "0" + hours : hours) + ":" + (minutes < 10 ? "0" + minutes : minutes) + ":" + (seconds < 10 ? "0" + seconds : seconds);
              var result = (minutes < 10 ? "0" + minutes : minutes) + ":" + (seconds < 10 ? "0" + seconds : seconds);

              $('#timer').html(result);
              if (c == 0)
               {
                  //setConfirmUnload(false);
                  //$("#quiz_form").submit();
                  window.location = "QC_PC.aspx";
              }
              c = c - 1;
              t = setTimeout(function() 
              {
                  timedCount()
              }, 1000);

              var GetLabelValue = document.getElementById('ctl00_ContentPlaceHolder1_lblmsg').innerText;
              if (GetLabelValue == 'Success') 
              {
                  c = -500;
              }
          }
      }

      $(document).ready(function() 
      {
      $('#ctl00_ContentPlaceHolder1_ChkOTP').click(function() 
          {

              var GenerateOTP = "<%=this.GenerateOTP%>";
              var EnteredOTP = document.getElementById('ctl00_ContentPlaceHolder1_txtOTP').value;

              alert(GenerateOTP);

              if (EnteredOTP == GenerateOTP) 
              {
                  alert('Success');
                  document.getElementById("ctl00_ContentPlaceHolder1_lblmsg").innerHTML = 'Success';
                  document.getElementById("ctl00_ContentPlaceHolder1_lblmsg").style.color = "#00CC00";
                  document.getElementById("ctl00_ContentPlaceHolder1_txtOTP").disabled = true;
                  document.getElementById("ctl00_ContentPlaceHolder1_btnsave").disabled = false;
                 
                  document.getElementById("ctl00_ContentPlaceHolder1_ChkOTP").disabled = true;

                  $('#timer').hide()
              }
              else {
                  alert('Failed');
                  document.getElementById("ctl00_ContentPlaceHolder1_lblmsg").innerHTML = 'Failed';
                  document.getElementById("ctl00_ContentPlaceHolder1_lblmsg").style.color = "Red";

                  document.getElementById("ctl00_ContentPlaceHolder1_btnsave").disabled = true;
                
              }

          });
      });
    </script>  
   

   <center>
   
  <table style="width: 1020px" >
        <tr>
            <td colspan="5" style="color: #000099; height: 35px;" align = "center">
                <b>खरीफ विपणन वर्ष 2016-17 में समर्थन मूल्य पर खरीदी हेतु स्थापित खरीदी केन्द्रों 
                का संयुक्त निरीक्षण प्रतिवेदन</b></td>
        </tr>
        <tr>
            <td style="color: #CC3300; height: 35px;">
                <b>क्र</b></td>
            <td style="width: 368px; color: #CC3300; height: 35px;" align = "center">
                <b>बिन्दु</b></td>
            <td colspan="3" style="color: #CC3300; height: 35px;">
                <b>निरीक्षण में पाई गयी स्थिति</b></td>
        </tr>
        <tr>
            <td style="color: #CC6600; height: 31px;">
                <b>1</b></td>
            <td style="width: 368px; color: #CC6600; height: 31px;" align = "center">
                <b>निरीक्षण दल के अधिकारियो के नाम</b></td>
            <td style="width: 201px; color: #CC6600; height: 31px;" align = "center">
                <b>पद</b></td>
            <td colspan="2" style="color: #CC6600; height: 31px;" align = "center">
                <b>मोबाइल नंबर</b></td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="width: 368px">
                <asp:TextBox ID="txtName_one" runat="server" Height="26px" Width="200px"></asp:TextBox>
            </td>
            <td style="width: 201px">
                <asp:TextBox ID="txtdesig_one" runat="server" Height="26px" Width="200px"></asp:TextBox>
            </td>
            <td colspan="2" style="text-align: center">
                <asp:TextBox ID="txtMob_one" runat="server" Height="26px" Width="200px" 
                    MaxLength="11"></asp:TextBox>
            &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="width: 368px">
                <asp:TextBox ID="txtName_two" runat="server" Height="25px" Width="200px"></asp:TextBox>
            </td>
            <td style="width: 201px">
                <asp:TextBox ID="txtdesig_two" runat="server" Height="26px" Width="200px"></asp:TextBox>
            </td>
            <td colspan="2" style="text-align: center">
                <asp:TextBox ID="txtMob_two" runat="server" Height="25px" Width="200px" 
                    MaxLength="11"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="width: 368px">
                <asp:TextBox ID="txtName_three" runat="server" Height="26px" Width="200px"></asp:TextBox>
            </td>
            <td style="width: 201px">
                <asp:TextBox ID="txtdesig_three" runat="server" Height="26px" Width="200px"></asp:TextBox>
            </td>
            <td colspan="2" style="text-align: center">
                <asp:TextBox ID="txtMob_three" runat="server" Height="26px" Width="200px" 
                    MaxLength="11"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="width: 368px">
                <asp:TextBox ID="txtName_four" runat="server" Height="26px" Width="200px"></asp:TextBox>
            </td>
            <td style="width: 201px">
                <asp:TextBox ID="txtdesig_four" runat="server" Height="26px" Width="200px"></asp:TextBox>
            </td>
            <td colspan="2" style="text-align: center">
                <asp:TextBox ID="txtMob_four" runat="server" Height="25px" Width="200px" 
                    MaxLength="11"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="width: 368px">
                <asp:TextBox ID="txtName_five" runat="server" Height="26px" Width="200px"></asp:TextBox>
            </td>
            <td style="width: 201px">
                <asp:TextBox ID="txtdesig_five" runat="server" Height="26px" Width="200px"></asp:TextBox>
            </td>
            <td colspan="2" style="text-align: center">
                <asp:TextBox ID="txtMob_five" runat="server" Height="25px" Width="200px" 
                    MaxLength="11"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                2</td>
            <td style="text-align: left; width: 368px">
                निरीक्षण की दिनाँक</td>
            <td colspan="2" style="text-align: left">
                <asp:TextBox ID="txtInsp_date" runat="server" Height="25px" Width="200px"></asp:TextBox>
                
                <script type  ="text/javascript">
                     new tcal({
                         'formname': '0',
                         'controlname': 'ctl00_ContentPlaceHolder1_txtInsp_date'
                     });
	     </script>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                3</td>
            <td style="text-align: left; width: 368px">
                जिले का नाम</td>
            <td colspan="2" style="text-align: left">
                <asp:DropDownList ID="ddlDistrict" runat="server" Height="35px" 
                    Width="219px">
                </asp:DropDownList>
                                                    </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                3.1</td>
            <td style="text-align: left; width: 368px">
                तहसील का नाम</td>
            <td colspan="2" style="text-align: left">
                <asp:DropDownList ID="ddlTehsil" runat="server" Height="35px" 
                    Width="219px">
                </asp:DropDownList>
                                                    </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="height: 30px">
                </td>
            <td style="text-align: left; width: 368px; height: 30px;">
                किस फसल के खरीदी केंद्र का निरक्षण किया जाना है </td>
            <td colspan="2" style="text-align: left; height: 30px;">
                <asp:DropDownList ID="ddlCrop" runat="server" AutoPostBack="True" Height="26px" 
                    onselectedindexchanged="ddlCrop_SelectedIndexChanged" Width="200px">
                    <asp:ListItem Value="0">-चुने-</asp:ListItem>
                    <asp:ListItem Value="22">गेहूँ</asp:ListItem>
                    <asp:ListItem Value="13">धान</asp:ListItem>
                    <asp:ListItem Value="12">मोटा अनाज</asp:ListItem>
                </asp:DropDownList>
                                                    </td>
            <td style="height: 30px">
                                                    </td>
        </tr>
        <tr>
            <td style="height: 36px">
                4</td>
            <td style="text-align: left; width: 368px; height: 36px;">
                खरीदी केंद्र का नाम</td>
            <td colspan="2" style="text-align: left; height: 36px;">
                <asp:DropDownList ID="ddlPCName" runat="server" Height="25px" 
                    Width="350px" AutoPostBack="True" 
                    onselectedindexchanged="ddlPCName_SelectedIndexChanged">
                </asp:DropDownList>
                                                    </td>
            <td style="height: 36px">
                </td>
        </tr>
        <tr>
            <td>
                5</td>
            <td style="text-align: left; width: 368px">
                समिति प्रबंधक का नाम</td>
            <td style="width: 201px">
                <asp:TextBox ID="txtPC_manager" runat="server" Height="25px" Width="200px"></asp:TextBox>
            </td>
            <td style="text-align: left; width: 175px">
                प्रबंधक का मोबाइल</td>
            <td>
                <asp:TextBox ID="txtPC_Mobile" runat="server" Height="25px" Width="175px" 
                    MaxLength="11"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                6</td>
            <td colspan="2" style="text-align: left; background-color: #CCFFCC;">
                कलेक्टर द्वारा नामित नोडल अधिकारी का नाम </td>
            <td style="background-color: #CCFFCC;" colspan="2">
                <asp:TextBox ID="txtNodal_officer" runat="server" Height="25px" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="text-align: left; width: 368px; background-color: #CCFFCC;">
                पद</td>
            <td style="width: 201px; background-color: #CCFFCC;">
                <asp:TextBox ID="txtNodal_desig" runat="server" Height="25px" Width="200px"></asp:TextBox>
            </td>
            <td style="width: 175px; background-color: #CCFFCC;">
                &nbsp;</td>
            <td style="background-color: #CCFFCC">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="text-align: left; width: 368px; background-color: #CCFFCC;">
                मोबाइल</td>
            <td style="width: 201px; background-color: #CCFFCC;">
                <asp:TextBox ID="txtNodal_Mobile" runat="server" Height="25px" Width="200px" 
                    MaxLength="11"></asp:TextBox>
            </td>
            <td style="width: 175px; background-color: #CCFFCC;">
                &nbsp;</td>
            <td style="background-color: #CCFFCC">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                7</td>
            <td style="text-align: left; width: 368px">
                पंजीकृत किसानो की संख्या</td>
            <td style="width: 201px">
                <asp:TextBox ID="txtregister_Farmer" runat="server" Height="26px" Width="200px"></asp:TextBox>
            </td>
            <td style="width: 175px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                8</td>
            <td colspan="4" style="text-align: left">
                समर्थन मूल्य योजनान्तर्गत खरीदी प्रारंभ होने की दिनाँक&nbsp;
                <asp:TextBox ID="txtProc_date" runat="server" Height="25px" Width="180px"></asp:TextBox>
                
                <script type  ="text/javascript">
                    new tcal({
                        'formname': '0',
                        'controlname': 'ctl00_ContentPlaceHolder1_txtProc_date'
                    });
	     </script>
                    
                    
                    </td>
        </tr>
        <tr>
            <td style="background-color: #BFE8E8">
                9</td>
            <td style="text-align: center; color: #CC3300; background-color: #BFE8E8;" 
                colspan="4">
                <b>खरीदी विवरण</b></td>
        </tr>
        <tr>
            <td style="background-color: #BFE8E8">
                9.1</td>
            <td colspan="2" style="text-align: left; background-color: #BFE8E8;">
                निरीक्षण के दिन की गई खरीदी(में टन )</td>
            <td colspan="2" style="text-align: left; background-color: #BFE8E8;">
                <asp:TextBox ID="txtInspDate_Kharidi" runat="server" Height="25px" 
                    Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="background-color: #BFE8E8">
                9.2</td>
            <td style="text-align: left; width: 368px; background-color: #BFE8E8;">
                प्रगतिशील खरीदी (में टन)</td>
            <td style="width: 201px; background-color: #BFE8E8;">
                &nbsp;</td>
            <td colspan="2" style="text-align: left; background-color: #BFE8E8;">
                <asp:TextBox ID="txt_totalkharidi" runat="server" Height="25px" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="background-color: #BFE8E8">
                9.3</td>
            <td style="text-align: left; width: 368px; background-color: #BFE8E8;">
                प्रगतिशील परिवहन (में टन )</td>
            <td style="width: 201px; background-color: #BFE8E8;">
                &nbsp;</td>
            <td colspan="2" style="text-align: left; background-color: #BFE8E8;">
                <asp:TextBox ID="txt_totalTransportation" runat="server" Height="25px" 
                    Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="height: 32px; background-color: #BFE8E8;">
                9.4</td>
            <td style="text-align: left; width: 368px; height: 32px; background-color: #BFE8E8;">
                खरीदी केंद्र पर परिवहन हेतु शेष स्कंध</td>
            <td style="width: 201px; height: 32px; background-color: #BFE8E8;">
                </td>
            <td colspan="2" style="text-align: left; height: 32px; background-color: #BFE8E8;">
                <asp:TextBox ID="txt_remainTransport" runat="server" Height="25px" 
                    Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                10</td>
            <td colspan="2" style="text-align: left">
                क्या खरीदी केंद्र पर FAQ सेम्पल रखा गया है ?</td>
            <td colspan="2" style="text-align: left">
                <asp:DropDownList ID="ddl_FaqSample" runat="server" Height="30px" 
                    Width="100px">
                    <asp:ListItem Value="Y">हैं</asp:ListItem>
                    <asp:ListItem Value="N">नहीं</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="height: 41px">
                11</td>
            <td colspan="4" align ="center" >
                <b>विश्लेषण के लिए अवश्यक निम्न तकनीक उपकरण की उपलब्द्ता होने पर हैं चुने 
                तथा ना होने पर नहीं चुने|</b></td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="width: 368px; background-color: #FFFFCC;">
                परखी</td>
            <td style="width: 201px; background-color: #FFFFCC;">
                <asp:DropDownList ID="ddl_Parkhi" runat="server" Height="30px" 
                    Width="100px">
                    <asp:ListItem Value="Y">हैं</asp:ListItem>
                    <asp:ListItem Value="N">नहीं</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width: 175px; background-color: #FFFFCC;">
                इलेक्ट्रोनिक बैलेंस</td>
            <td style="background-color: #FFFFCC">
                <asp:DropDownList ID="ddl_electronicBalance" runat="server" Height="30px" 
                    Width="100px">
                    <asp:ListItem Value="Y">हैं</asp:ListItem>
                    <asp:ListItem Value="N">नहीं</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="width: 368px; background-color: #FFFFCC;">
                प्लास्टिक कपडे की सेम्पल थेलिया</td>
            <td style="width: 201px; background-color: #FFFFCC;">
                <asp:DropDownList ID="ddl_plasticbag" runat="server" Height="30px" 
                    Width="100px">
                    <asp:ListItem Value="Y">हैं</asp:ListItem>
                    <asp:ListItem Value="N">नहीं</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width: 175px; background-color: #FFFFCC;">
                एजेंसी की पीतल की सील</td>
            <td style="background-color: #FFFFCC">
                <asp:DropDownList ID="ddl_seal" runat="server" Height="30px" 
                    Width="100px">
                    <asp:ListItem Value="Y">हैं</asp:ListItem>
                    <asp:ListItem Value="N">नहीं</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="width: 368px; background-color: #FFFFCC;">
                नमी मापक यन्त्र</td>
            <td style="width: 201px; background-color: #FFFFCC;">
                <asp:DropDownList ID="ddl_moistMachine" runat="server" Height="30px" 
                    Width="100px">
                    <asp:ListItem Value="Y">हैं</asp:ListItem>
                    <asp:ListItem Value="N">नहीं</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width: 175px; background-color: #FFFFCC;">
                एनेमल प्लेट</td>
            <td style="background-color: #FFFFCC">
                <asp:DropDownList ID="ddl_enamelplate" runat="server" Height="30px" 
                    Width="100px">
                    <asp:ListItem Value="Y">हैं</asp:ListItem>
                    <asp:ListItem Value="N">नहीं</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                12</td>
            <td style="width: 368px">
                क्या खरीदी केंद्र पर छन्ना उपलब्ध है ?</td>
            <td style="width: 201px">
                <asp:DropDownList ID="ddl_filter" runat="server" Height="30px" 
                    Width="100px">
                    <asp:ListItem Value="Y">हैं</asp:ListItem>
                    <asp:ListItem Value="N">नहीं</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width: 175px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                13</td>
            <td colspan="2" style="text-align: left">
                क्या खरीदी केंद्र पर थ्रेसर में उपयोग होने वाला ब्लोअर उपलब्ध है ?</td>
            <td style="width: 175px">
                <asp:DropDownList ID="ddl_blower" runat="server" Height="30px" 
                    Width="100px">
                    <asp:ListItem Value="Y">हैं</asp:ListItem>
                    <asp:ListItem Value="N">नहीं</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                14</td>
            <td colspan="2">
                तिरपाल एवं सिलाई मशीनों की पर्याप्त उपलब्धता है अथवा नहीं</td>
            <td style="width: 175px">
                <asp:DropDownList ID="ddl_TirplaSilai" runat="server" Height="30px" 
                    Width="100px">
                    <asp:ListItem Value="Y">हैं</asp:ListItem>
                    <asp:ListItem Value="N">नहीं</asp:ListItem>
                </asp:DropDownList>
                                                    </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                15</td>
            <td colspan="2">
                क्या क्रय स्कंध के बोरो की डबल सिलाई की जा रही है?</td>
            <td style="width: 175px">
                <asp:DropDownList ID="ddl_doubleSilai" runat="server" Height="30px" 
                    Width="100px">
                    <asp:ListItem Value="Y">हैं</asp:ListItem>
                    <asp:ListItem Value="N">नहीं</asp:ListItem>
                </asp:DropDownList>
                                                    </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                15.1</td>
            <td colspan="2">
                &nbsp;क्या डबल सिलाई न होने से ट्रक गोदाम से रिजेक्ट हुए | यदि हुए तो नीचे में 
                संख्या लिखे</td>
            <td style="width: 175px">
                <asp:DropDownList ID="ddl_reject_doubleSilai" runat="server" Height="30px" 
                    Width="100px">
                    <asp:ListItem Value="Y">हैं</asp:ListItem>
                    <asp:ListItem Value="N">नहीं</asp:ListItem>
                </asp:DropDownList>
                                                    </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="2">
                                डबल सिलाई न होने से गोदाम से रिजेक्ट हुए ट्रक की संख्या</td>
            <td style="width: 175px">
                <asp:TextBox ID="txt_truckreject_dueDoubleSilai" runat="server" Height="23px" 
                    Width="180px">0</asp:TextBox>
                                                    </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                16</td>
            <td colspan="2">
                क्या बोरो पर स्टेंसिल एवं टैग लगाये जा रहे है?&nbsp; </td>
            <td style="width: 175px">
                <asp:DropDownList ID="ddl_stencilTag" runat="server" Height="30px" 
                    Width="100px">
                    <asp:ListItem Value="Y">हैं</asp:ListItem>
                    <asp:ListItem Value="N">नहीं</asp:ListItem>
                </asp:DropDownList>
                                                    </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                16.1</td>
            <td colspan="2">
                स्टेंसिल एवं टैग ना होने से कितने ट्रक गोदाम से रिजेक्ट हुए |</td>
            <td style="width: 175px">
                <asp:TextBox ID="txt_truckreject_duestencil" runat="server" Height="26px" 
                    Width="200px">0</asp:TextBox>
                                                    </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                17</td>
            <td style="text-align: left;" colspan="2">
                क्या संस्था द्वारा कृषको के रिजेक्ट स्कंध के सेम्पल सुरक्षित रखे गए है तथा पंजी 
                में विवरण दर्ज किया गया है |</td>
            <td style="width: 175px">
                <asp:DropDownList ID="ddl_rejectSample" runat="server" Height="33px" 
                    Width="100px">
                    <asp:ListItem Value="Y">हैं</asp:ListItem>
                    <asp:ListItem Value="N">नहीं</asp:ListItem>
                </asp:DropDownList>
                                                    </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                18</td>
            <td style="text-align: left;" colspan="2">
                क्या खरीदी केंद्र पर वांछित अभिलेखों का संधारण किया जा रहा है ? यदि नहीं तो नीचे 
                विवरण लिखे</td>
            <td style="width: 175px">
                <asp:DropDownList ID="ddl_registerEntry" runat="server" Height="34px" 
                    Width="100px">
                    <asp:ListItem Value="Y">हैं</asp:ListItem>
                    <asp:ListItem Value="N">नहीं</asp:ListItem>
                </asp:DropDownList>
                                                    </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="text-align: left;" colspan="2">
                अभिलेखों का संधारण ना किये जाने का विवरण लिखे </td>
            <td style="width: 175px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                19</td>
            <td colspan="2">
                निरीक्षण के दौरान खरीदी केंद्र पर गुणवत्ता में कमी के कारण रिजेक्ट किये गए स्टॉक 
                की मात्रा तथा रिजेक्सन का कारण (एनालिसिस रिपोर्ट संलग्न करें)</td>
            <td style="width: 175px">
                <asp:TextBox ID="txt_rejectQty" runat="server" Height="30px" Width="200px">0</asp:TextBox>
                                                    </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                20</td>
            <td style="width: 368px; background-color: #CCCCFF;">
                वाररदाना गठानो की संख्या व गुणवत्ता | </td>
            <td style="width: 201px; background-color: #CCCCFF;">
                कुल प्राप्त वारदाना</td>
            <td style="width: 175px; background-color: #CCCCFF;">
                <asp:TextBox ID="txt_totalBardana" runat="server" Height="26px" Width="200px"></asp:TextBox>
            </td>
            <td style="background-color: #CCCCFF">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="width: 368px; background-color: #CCCCFF;">
                &nbsp;</td>
            <td style="width: 201px; background-color: #CCCCFF;">
                खरीदी में उपयोग</td>
            <td style="width: 175px; background-color: #CCCCFF;">
                <asp:TextBox ID="txt_usedBardana" runat="server" Height="25px" Width="200px"></asp:TextBox>
            </td>
            <td style="background-color: #CCCCFF">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="width: 368px; background-color: #CCCCFF;">
                &nbsp;</td>
            <td style="width: 201px; background-color: #CCCCFF;">
                शेष वारदाना गठान में </td>
            <td style="width: 175px; background-color: #CCCCFF;">
                <asp:TextBox ID="txt_remainBardana_Gathan" runat="server" Height="26px" 
                    Width="200px"></asp:TextBox>
            </td>
            <td style="background-color: #CCCCFF">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="width: 368px; background-color: #CCCCFF;">
                &nbsp;</td>
            <td style="width: 201px; background-color: #CCCCFF;">
                शेष वारदाना लूस में </td>
            <td style="width: 175px; background-color: #CCCCFF;">
                <asp:TextBox ID="txt_remain_bardana" runat="server" Height="26px" Width="200px"></asp:TextBox>
            </td>
            <td style="background-color: #CCCCFF">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                21</td>
            <td style="text-align: left;" colspan="2">
                क्या ख़रीदे स्टॉक का वजन मानक भर्ती का है ? यदि वजन में अंतर है तो कारण सहित 
                विवरण नीचे लिखे </td>
            <td style="width: 175px">
                <asp:DropDownList ID="ddl_ManikBharti" runat="server" Height="30px" 
                    Width="100px">
                    <asp:ListItem Value="Y">हैं</asp:ListItem>
                    <asp:ListItem Value="N">नहीं</asp:ListItem>
                </asp:DropDownList>
                                                    </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="text-align: left;" colspan="4">
                <asp:TextBox ID="txt_reason_manakbharti" runat="server" Height="35px" 
                    Width="900px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="height: 33px">
                22</td>
            <td style="height: 33px; text-align: left;" colspan="2">
                कृषको को भुगतान किस दिनाँक तक का किया गया है ?</td>
            <td style="width: 175px; height: 33px;">
                <asp:TextBox ID="txt_paymentDate" runat="server" Height="25px" Width="150px"></asp:TextBox>
                
                 <script type  ="text/javascript">
                     new tcal({
                         'formname': '0',
                         'controlname': 'ctl00_ContentPlaceHolder1_txt_paymentDate'
                     });
	     </script>
                                                    </td>
            <td style="height: 33px">
                                                    </td>
        </tr>
        <tr>
            <td>
                22.1</td>
            <td style="text-align: left;" colspan="2">
                क्या 3 दिन से अधिक विलम्ब हो रहा है ?</td>
            <td style="width: 175px">
                <asp:DropDownList ID="ddl_last3days" runat="server" Height="30px" 
                    Width="100px">
                    <asp:ListItem Value="Y">हैं</asp:ListItem>
                    <asp:ListItem Value="N">नहीं</asp:ListItem>
                </asp:DropDownList>
                                                    </td>
            <td>
                                                    </td>
        </tr>
        <tr>
            <td>
                23</td>
            <td style="text-align: left;" colspan="2">
                क्या इलेक्ट्रोनिक तौल काँटा तथा समीपस्थ धर्मकांटा का सत्यापन किया गया है ?</td>
            <td style="width: 175px">
                <asp:DropDownList ID="ddl_verify_taulkanta" runat="server" Height="30px" 
                    Width="100px">
                    <asp:ListItem Value="Y">हैं</asp:ListItem>
                    <asp:ListItem Value="N">नहीं</asp:ListItem>
                </asp:DropDownList>
                                                    </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                24</td>
            <td style="text-align: left;" colspan="2">
                धान उपार्जन की स्थिति में क्या बोरो को पलटकर धान की भर्ती की जा रही है ?</td>
            <td style="width: 175px">
                <asp:DropDownList ID="ddl_paddyBharti_return" runat="server" Height="30px" 
                    Width="100px">
                    <asp:ListItem Value="Y">हैं</asp:ListItem>
                    <asp:ListItem Value="N">नहीं</asp:ListItem>
                </asp:DropDownList>
                                                    </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                25</td>
            <td style="text-align: left;" colspan="2">
                निरीक्षण दिनाँक तक गोदाम से कितने ट्रक एवं मात्रा के अस्वीकृति पत्रक जारी किये 
                गए ?</td>
            <td style="width: 175px; text-align: left; margin-left: 40px;">
                ट्रक
                <asp:TextBox ID="txt_totalreject_truck" runat="server" Height="24px" 
                    Width="140px"></asp:TextBox>
            </td>
            <td style="text-align: left">
                मात्रा
                <asp:TextBox ID="txt_totalreject_qty" runat="server" Height="28px" 
                    Width="140px"></asp:TextBox>
                                                    </td>
        </tr>
        <tr>
            <td>
                26</td>
            <td style="text-align: left;" colspan="2">
                किस दिनाँक तक के स्वीकृति पत्रक प्राप्त किये गए है |</td>
            <td style="width: 175px">
                <asp:TextBox ID="txt_accept_recddate" runat="server" Height="25px" 
                    Width="120px"></asp:TextBox>
                    
                     <script type  ="text/javascript">
                         new tcal({
                             'formname': '0',
                             'controlname': 'ctl00_ContentPlaceHolder1_txt_accept_recddate'
                         });
                         

                     </script>
                    
                   
                                                    </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                27</td>
            <td style="text-align: left;" colspan="2">
                खरीदी केंद्र पर डबल सिलाई किये, स्टेंसिल एवं टैग लगे बोरे की संख्या जो परिवहन के 
                लिए तैयार है |</td>
            <td style="width: 175px">
                <asp:TextBox ID="txt_totalbags_allparameter" runat="server" Height="25px" 
                    Width="140px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                28</td>
            <td style="width: 368px; text-align: left;">
                अन्य कोई महत्वपूर्ण बिन्दु</td>
            <td style="text-align: left;" colspan="3">
                <asp:TextBox ID="txt_otherpoint" runat="server" Height="40px" Width="550px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="text-align: left;" colspan="4">
                <asp:Label ID="Label3" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="width: 368px; text-align: left;">
                
            </td>
            <td colspan="3">
             <input type="button" id="btnOTP" runat="server" onserverclick="btnOTP_Click" value="Send OTP" style="background-color: Silver; border-color: red; border-style: solid; font-weight: bold; height: 23px; width: 100px;" causesvalidation="false"/>
                <asp:TextBox ID="txtOTP" runat="server" AutoComplete="off" placeholder="Enter OTP Number" Style="border-color: black;" Enabled="False"></asp:TextBox>

                <span id='timer' style="color: #FF0000; font-weight: bold"></span>

                <input type="button" id="ChkOTP" runat="server" value="Check OTP" style="background-color: Silver; border-color: red; border-style: solid; font-weight: bold; height: 23px; width: 100px;" causesvalidation="false" disabled="disabled" />
                <asp:Label ID="lblmsg" runat="server" Text="" Style="font-weight: 700;"></asp:Label>
             </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="width: 368px; text-align: left;">
             <input id="hdfMobileNo" type="hidden" runat="server" />
            <input id="hdfEmpID" type="hidden" runat="server" />
            <input id="hdfOTP" type="hidden" runat="server" />
            <input id="hdfCheckSubMO" type="hidden" runat="server" />
            </td>
            <td colspan="3">
                <asp:Button ID="btnsave" runat="server" onclick="btnsave_Click" Text="Submit" 
                    Enabled="False" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="width: 368px; text-align: left;">
                &nbsp;</td>
            <td colspan="3" style="text-align: center">
                <asp:HyperLink ID="hlinkpdo" runat="server" Font-Size="11pt" NavigateUrl="#" Visible="False"
                    Width="142px" Height="22px" style="font-weight: 700">Print This</asp:HyperLink>
            </td>
        </tr>
    </table>
    
    </center>
    
</asp:Content>

