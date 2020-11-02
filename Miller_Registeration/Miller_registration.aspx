<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Miller_registration.aspx.cs" Inherits="Miller_Registeration_Miller_registration" MaintainScrollPositionOnPostback="true" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mill Registration</title>

    
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>

     

    <%--For Calendar Controls--%>
    <link href="../PaddyMilling/calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="../PaddyMilling/calendar/calendar.js"></script>
    <script type="text/javascript" src="../js/chksql.js"></script>

    <script type="text/javascript" src="../js/calendar_eu.js"></script>
    <link rel="stylesheet" type="text/css" href="../CSS/calendar.css" />

     <script type="text/javascript" language="javascript" >
         function validate() {
             if (Page_ClientValidate())
                 return confirm('क्या आपने सही जानकारी भरी है? यदि भरी है तो OK पर क्लिक करे अन्यथा Cancel पर क्लिक करे तथा भरी गयी जानकारी फिर से चेक कर ले|');
         }
        </script>
    
    <script type="text/javascript">
        window.onload = function () {
            var div = document.getElementById("dvScroll");
            var div_position = document.getElementById("div_position");
            var position = parseInt('<%=Request.Form["div_position"] %>');
            if (isNaN(position)) {
                position = 0;
            }
            div.scrollTop = position;
            div.onscroll = function () {
                div_position.value = div.scrollTop;
            };
        };
    </script>

    <script type="text/javascript">
        function Check(textBox, maxLength) {
            if (textBox.value.length > maxLength) {
                alert("Max characters allowed are " + maxLength);
                textBox.value = textBox.value.substr(0, maxLength);
            }
        }
</script>

    <script type="text/javascript">

        function onlyNumbers(evt) {
            var AsciiCode = event.keyCode;
            var txt = evt.value;
            var txt2 = String.fromCharCode(AsciiCode);
            var txt3 = txt2 * 1;
            if ((AsciiCode < 46) || (AsciiCode > 57)) {
                alert('Please enter only numbers.');
                event.cancelBubble = true;
                event.returnValue = false;
            }

            var num = evt.value;
            var len = num.length;
            var indx = -1;
            indx = num.indexOf('.');
            if (indx != -1) {
                var dgt = num.substr(indx, len);
                var count = dgt.length;
                //alert (count);

                if (AsciiCode == 46) {
                    if (num.split(".").length > 1) {
                        alert('दशमलव एक ही बार आ सकता है |');
                        return false;
                    }
                }

                if (count > 5) {
                    alert("Only 5 decimal digits allowed");
                    event.cancelBubble = true;
                    event.returnValue = false;
                }



            }

        }


        //    var e = event || evt; // for trans-browser compatibility
        //    var charCode = e.which || e.keyCode;
        //    if (charCode > 31 && (charCode < 46 || charCode > 57))
        //        return false;
        //    return true;
        //}      
        function onlyNumbersbags(evt) {
            var AsciiCode = event.keyCode;
            var txt = evt.value;
            var txt2 = String.fromCharCode(AsciiCode);
            var txt3 = txt2 * 1;
            if ((AsciiCode <= 46) || (AsciiCode > 57)) {
                alert('Please enter only numbers.');
                event.cancelBubble = true;
                event.returnValue = false;
            }
        }

    </script>

    <script type="text/javascript">
        function CheckCalDate(tx) {
            var AsciiCode = event.keyCode;
            var txt = tx.value;
            var txt2 = String.fromCharCode(AsciiCode);
            var txt3 = txt2 * 1;
            if ((AsciiCode > 0)) {
                alert('Please Click on Calander Controll to Enter Date');
                event.cancelBubble = true;
                event.returnValue = false;
            }
        }
    </script>

      <%-- <script type="text/javascript">
           function validateFileSize() {
               var uploadControl = document.getElementById('<%= FileUpload_millowner.ClientID %>');
               if (uploadControl.files[0].size > 3145728) {
                   document.getElementById('dvMsg').style.display = "block";
                   $('#btn_submit').attr('disabled', true);
                   return false;
               }
               else {
                   document.getElementById('dvMsg').style.display = "none";
                   $('#btn_submit').attr('disabled', false);
                   return true;
               }
           }

           function validateFileSize1() {
               var uploadControl = document.getElementById('<%= photo.ClientID %>');
               if (uploadControl.files[0].size > 3145728) {
                   document.getElementById('Div1').style.display = "block";
                   $('#btn_submit').attr('disabled', true);
                   return false;
               }
               else {
                   document.getElementById('Div1').style.display = "none";
                   $('#btn_submit').attr('disabled', false);
                   return true;
               }
           }

           function validateFileSize2() {
               var uploadControl = document.getElementById('<%= signature.ClientID %>');
               if (uploadControl.files[0].size > 3145728) {
                   document.getElementById('Div2').style.display = "block";
                   $('#btn_submit').attr('disabled', true);
                   return false;
               }
               else {
                   document.getElementById('Div2').style.display = "none";
                   $('#btn_submit').attr('disabled', false);
                   return true;
               }
           }
    </script>--%>

      <style type="text/css">
          .ButtonClass
          {
              cursor: pointer;
          }
      </style>

    <style type="text/css">
        .tooltip
        {
            position: relative;
            display: inline-block;
            border-bottom: 1px dotted black;
            font-size: x-large;
            color: blue;
        }

            .tooltip .tooltiptext
            {
                visibility: hidden;
                width: 500px;
                background-color: black;
                color: #fff;
                text-align: center;
                border-radius: 6px;
                padding: 5px 0;
                /* Position the tooltip */
                position: absolute;
                z-index: 1;
            }

            .tooltip:hover .tooltiptext
            {
                visibility: visible;
            }

        .tblnew
        {
            width: 1200px;
            border-collapse: collapse;
        }

            .tblnew th
            {
                font-weight: 700;
                border-bottom: 1px solid #ddd;
                text-align: center;
                border-top: 1px solid #ddd;
                border-right: 1px solid #ddd;
                background: #669999;
                color: #FFFFFF;
                font-family: georgia;
                font-size: 17px;
                border-style: solid;
                border-color: #66FFFF;
            }

            .tblnew tr
            {
                background: #fff !important;
                text-align: center;
            }

            .tblnew .alt-row
            {
                background: #f0fbff !important;
            }

            .tblnew td, .tblnew th
            {
                padding: 6px 2px;
                border-left: 1px solid #cbe2eb;
                border-right: 1px solid #cbe2eb;
                border-bottom: 1px solid #cbe2eb;
                font-size: 14px;
            }


                .tblnew td:first-child
                {
                    /*float:left;*/
                    text-align: center;
                    font-weight: 700;
                    font-size: 16px;
                }

        .auto-style1
        {
            font-weight: bold;
            color: #0000FF;
        }

        .auto-style2
        {
            font-weight: bold;
        }

        .auto-style4
        {
            color: #FF6600;
        }

        .auto-style5
        {
            color: #0000FF;
        }

        .auto-style6
        {
            font-weight: normal;
        }

        .auto-style7
        {
            color: #FF0000;
        }

        .auto-style8
        {
            text-align: left;
        }

        .auto-style10
        {
            color: #FF9900;
        }

        .auto-style11
        {
            text-decoration: underline;
        }
    </style>

</head>
<body runat="server">
       <div id="dvScroll" style="overflow-y: scroll; height: 600px;">
    <form id="form1" runat="server">
        <center>
            <table class="tblnew" >
                <tr>
                 
                    <td colspan="6" style="font-size:large; font-weight:700; text-align:center" class="auto-style4"><span class="auto-style11">Millers Registration Khariff Crop Year </span> <asp:Label ID="lblCropYear" runat="server" Text="" CssClass="auto-style11"></asp:Label></td>
                </tr>
                  <tr id="trmessage" runat="server" visible="true">
                 
                    <td colspan="6" style="font-size:large; font-weight:700; text-align:center; color:red;" class="auto-style4"><span class="auto-style11">Millers Registration की अवधि समाप्त हो चुकी है| </span> <asp:Label ID="Label1" runat="server" Text="" CssClass="auto-style11"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="6" style="text-align: center">
                        <asp:Label ID="lbl_passwordname" runat="server" Text="Your Registration No. is:"
                            Visible="False" CssClass="auto-style7"></asp:Label>
                        <asp:Label ID="lbl_registeredno" runat="server"
                            Style="font-weight: 700; font-size: large;" CssClass="auto-style7"></asp:Label>
                    </td>
                </tr>
        </center>

        <tr>
            <td class="auto-style2" style="text-align:left">
                <b>मिल का नाम</b> </td>
            <center>
                <td colspan="5" style="padding-left: 80px">
                    <asp:TextBox ID="millersname" runat="server" Width="519px" AutoComplete="off" MaxLength="45" Class="alphaOnly"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="millersname" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>
                </td>
        </tr>
        </center>
            <tr>
                <td class="auto-style2" style="text-align:left">मिल का पता </td>
                <td style="text-align: center; font-weight: normal">मिल का राज्य</td>
                <td colspan="4">
                    <asp:RadioButton ID="rbMPState" runat="server" Font-Bold="True" GroupName="State" Text="M.P State" AutoPostBack="True" ForeColor="Blue" OnCheckedChanged="rbMPState_CheckedChanged" />
                    <asp:RadioButton ID="rbOtherState" runat="server" Font-Bold="True" GroupName="State" Text="Other States" AutoPostBack="True" ForeColor="Blue" OnCheckedChanged="rbOtherState_CheckedChanged" />

                    <asp:DropDownList ID="ddlOtherStates" runat="server" Height="26px" Width="204px" AutoPostBack="True" OnSelectedIndexChanged="ddlOtherStates_SelectedIndexChanged" Visible="False" Style="margin-left: 10px">
                    </asp:DropDownList>
                    <%--   &nbsp;
                <asp:Label ID="lblOtherStates" runat="server" Font-Bold="True" ForeColor="Red" Text="**" Visible="False"></asp:Label>

                &nbsp;--%></td>

            </tr>
        <tr>
            <td rowspan="4">&nbsp;</td>

        </tr>
        <tr>

            <td style="text-align: center; font-weight: normal">जिला
            </td>
            <td>
                <asp:DropDownList ID="ddlMacersAddDist" runat="server" Width="204px"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlMacersAddDist_SelectedIndexChanged" Height="28px" Style="margin-left: 10px; margin-right: 20px">
                </asp:DropDownList>
            </td>
            <td style="text-align: center; font-weight: normal" colspan="2">तहसील</td>
            <td>
                <asp:DropDownList ID="ddlMacersAddDivision" runat="server" Width="204px" Height="28px" Style="margin-left: 10px; margin-right: 20px">
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td style="text-align: center; font-weight: normal">ग्राम / वार्ड
            </td>
            <td>
                <asp:TextBox ID="txt_village" runat="server" Width="200px" AutoComplete="off" Style="margin-left: 10px; margin-right: 2px" Class="alphaOnly" MaxLength="30"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server"
                    ControlToValidate="txt_village" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>
            </td>
            <td style="text-align: center; font-weight: normal" colspan="2">पिन कोड
            </td>
            <td>
                <asp:TextBox ID="txt_pincode" runat="server" Width="200px" onkeypress="return onlyNumbersbags(this);" AutoComplete="off" Style="margin-left: 10px; margin-right: 2px" MaxLength="6"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                    ControlToValidate="txt_pincode" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="text-align: center; font-weight: normal">मोबाइल नंबर</td>
            <td>
                <asp:TextBox ID="txt_CountryCode" runat="server" Width="25px" onkeypress="return onlyNumbersbags(this);" Enabled="False" Style="margin-left: 7px;">+91</asp:TextBox>
                <asp:TextBox ID="txt_MobileNo" runat="server" Width="165px" onkeypress="return onlyNumbersbags(this);" MaxLength="10" AutoComplete="off" Style="margin-right: 2px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server"
                    ControlToValidate="txt_MobileNo" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>
            </td>
            <td style="text-align: center; font-weight: normal" colspan="2">फोन न.<br />
                (STD कोड) के साथ
            </td>
            <td>
                <asp:TextBox ID="txt_millphone" runat="server" Width="200px" AutoComplete="off" Style="margin-left: 10px; margin-right: 2px" onkeypress="return onlyNumbersbags(this);" MaxLength="15" Class="alphaNumericWithoutSpace"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                    ControlToValidate="txt_millphone" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>
            </td>

        </tr>
        <tr>
            <td class="auto-style2" style="text-align:left">मिल का स्वामित्व किसका है </td>
            <td colspan="5" style="text-align: center; font-weight: bold; padding-left: 200px">
                <asp:RadioButton ID="rd_prototype" runat="server" Text="प्रोपाइटरशिप"
                    AutoPostBack="True" GroupName="n"
                    OnCheckedChanged="rd_prototype_CheckedChanged" Style="color: #0000FF" />
                <asp:RadioButton ID="rd_firm" runat="server" Text="फर्म" AutoPostBack="True"
                    GroupName="n" OnCheckedChanged="rd_firm_CheckedChanged" Style="padding-left: 50px; color: #0000FF;" />
                <asp:HiddenField ID="hd_millowner" runat="server" />
            </td>
        </tr>

        <tr>
            <td rowspan="5">&nbsp;</td>
            <td style="font-weight: bold">(i)यदि स्वामित्व प्रोप्राइटरशिप है तो </td>
            <td colspan="3" >प्रोपाइटर का नाम </td>
            <td>
                <asp:TextBox ID="txt_propaname" runat="server" Width="200px"  AutoComplete="off" MaxLength="30" Class="alphaOnly"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td rowspan="2">&nbsp;</td>
            <td colspan="3">प्रोपाइटर का पता </td>
            <td>
                <asp:TextBox ID="txt_propa_address" runat="server" TextMode="MultiLine"
                     Width="200px" AutoComplete="off" Class="alphaNumeric" onKeyUp="javascript:Check(this, 70);" onChange="javascript:Check(this, 70);"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="text-align: center; font-weight: normal">प्रोपाइटर का शहर </td>
            <td>
                <asp:TextBox ID="txt_propacity" runat="server" Width="200px"  AutoComplete="off" MaxLength="30" Class="alphaOnly"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="font-weight: bold; text-align: center">(ii)यदि फर्म है तो </td>
            <td colspan="3">फर्म का प्रकार </td>
            <td>
                <asp:DropDownList ID="ddl_firmtype" runat="server" Width="204px" Height="28px"
                    Enabled="False">
                    <asp:ListItem Value="0">--select----</asp:ListItem>
                    <asp:ListItem>पार्टनरशिप </asp:ListItem>
                    <asp:ListItem>पब्लिक लि.क.</asp:ListItem>
                    <asp:ListItem>प्रा.लि . क.</asp:ListItem>
                    <asp:ListItem>सोसाइटी </asp:ListItem>
                    <asp:ListItem>अन्य </asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td colspan="3">पंजीयन क्रमांक /अन्य </br> का उल्लेख करें </td>
            <td>
                <asp:TextBox ID="txt_registrationno" runat="server" Width="200px"
                    Enabled="False" AutoComplete="off" MaxLength="20" Class="alphaNumeric"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align:left">मिल संचालक का नाम  </td>
            <td>
                <asp:TextBox ID="millerowner_name" runat="server" Width="200px" AutoComplete="off" Style="margin-left: 10px; margin-right: 2px" MaxLength="30" Class="alphaOnly"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                    ControlToValidate="millerowner_name" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>
            </td>

            <td colspan="3" style="margin-right: 20px" ><b>मिल संचालक के पिता का नाम </b></td>
            <td>
                <asp:TextBox ID="miller_fathername" runat="server" Width="200px" AutoComplete="off" Style="margin-left: 15px" MaxLength="30" Class="alphaOnly"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                    ControlToValidate="miller_fathername" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator></td>
        </tr>


        <tr>
            <td style="text-align:left">मिल संचालक का स्थाई पता </td>
            <td colspan="5" style="padding-left: 11px; text-align: left;">
                <asp:TextBox ID="mill_owner_address" runat="server" Width="865px"
                    AutoComplete="off" Class="alphaNumeric" MaxLength="70"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                    ControlToValidate="mill_owner_address" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style2" style="text-align:left">भागीदार/भागीदारो का नाम </td>
            <td style="text-align: left">1)
                <asp:TextBox ID="miller_participate1" runat="server" Width="194px" AutoComplete="off" MaxLength="30" Class="alphaOnly"></asp:TextBox>
            </td>
            <td style="text-align: left">2)
                <asp:TextBox ID="miller_participate2" runat="server" Width="194px" AutoComplete="off" MaxLength="30" Class="alphaOnly"></asp:TextBox>
            </td>
            <td style="text-align: left" colspan="2">3)
                <asp:TextBox ID="miller_participate3" runat="server" Width="150px" AutoComplete="off" MaxLength="30" Class="alphaOnly"></asp:TextBox>
            </td>
            <td style="text-align: left">4)
                <asp:TextBox ID="miller_participate4" runat="server" Width="200px" AutoComplete="off" MaxLength="30" Class="alphaOnly"></asp:TextBox>
            </td>

        </tr>

        <tr>
            <td class="auto-style2" style="text-align:left">टेलीफ़ोन न. <span class="auto-style5">(with STD Codes)</span></td>
            <td style="text-align: left">1)
                <asp:TextBox ID="miller_telephone_home" runat="server" Width="194px" AutoComplete="off" onkeypress="return onlyNumbersbags(this);" MaxLength="15" Class="alphaNumericWithoutSpace"></asp:TextBox>
            </td>
            <td style="text-align: left">2)
                <asp:TextBox ID="miller_telephone_office" runat="server" Width="194px" AutoComplete="off" onkeypress="return onlyNumbersbags(this);" MaxLength="15" Class="alphaNumericWithoutSpace"></asp:TextBox>
            </td>
            <td style="text-align: left" colspan="2">3)
                <asp:TextBox ID="miller_moblie1" runat="server" Width="150px" AutoComplete="off" onkeypress="return onlyNumbersbags(this);" MaxLength="15" Class="alphaNumericWithoutSpace"></asp:TextBox>
            </td>
            <td style="text-align: left">4)
                <asp:TextBox ID="miller_mobile2" runat="server" Width="200px" AutoComplete="off" onkeypress="return onlyNumbersbags(this);" MaxLength="15" Class="alphaNumericWithoutSpace"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td class="auto-style2" style="text-align:left">मिल की रनिंग स्थिति  </td>
            <td>
                <asp:RadioButton ID="rd_millopen" runat="server" GroupName="mill"
                    Text="चालू है " CssClass="auto-style1" />
                <asp:RadioButton ID="rd_millclose" runat="server" GroupName="mill"
                    Text="बंद है " CssClass="auto-style1" Style="padding-left: 10px;" />
                <asp:HiddenField ID="hd_runningwork" runat="server" />
            </td>
            <td colspan="3"><b>मिल स्वयं की है या लीज़ पर है </b></td>

            <td style="text-align:left; padding-left:10px">
                <asp:RadioButton ID="rd_selfmill" runat="server" Text="स्वयं की है "
                    GroupName="leez" AutoPostBack="True"
                    OnCheckedChanged="rd_selfmill_CheckedChanged" CssClass="auto-style1" />
                <asp:RadioButton ID="rd_leezmill" runat="server" Text="लीज़ पर है "
                    GroupName="leez" AutoPostBack="True"
                    OnCheckedChanged="rd_leezmill_CheckedChanged" CssClass="auto-style1" Style="padding-left: 10px;" />
                <asp:HiddenField ID="hd_leez" runat="server" />
            </td>
        </tr>

        <tr>
            <td style="text-align:left">यदि लीज़ पर ली गयी है तो </td>
            <td>मिल मालिक का नाम
            </td>
            <td style="text-align: left; padding-left: 17px">
                <asp:TextBox ID="txt_mill_owner_name" runat="server" Width="194px"
                    Enabled="False" AutoComplete="off" MaxLength="30" Class="alphaOnly"></asp:TextBox>
            </td>
            <td colspan="2">लीज़ अनुबंध समाप्ति की तिथि 
            </td>
            <td style="text-align: left; padding-left: 17px">
                <asp:TextBox ID="txt_agreement_end" runat="server" Width="170px"
                    AutoComplete="off" ReadOnly="True" Enabled="False"></asp:TextBox>
 <%--               <script type="text/javascript">
                    new tcal({
                        'formname': '0',
                        'controlname': 'txt_agreement_end'
                    });
                </script>--%>

                <img id="calid" runat="server" src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'txt_agreement_end', 'expiry=true,elapse=0,restrict=false')" />
            </td>
        </tr>
        <tr>
            <td rowspan="1">&nbsp;</td>
            <td>मिल मालिक का पता </td>
            <td colspan="4" style="text-align: left; padding-left: 17px">
                <asp:TextBox ID="txt_millowner_address" runat="server" Width="590px"
                    Enabled="False" AutoComplete="off" Class="alphaNumeric" MaxLength="70" ></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td style="text-align:left" >मिलिंग क्षमता <span class="auto-style5">(मै०टन / प्रति घंटा)</span></td>
            <td>अरवा </td>
            <td style="text-align: left; padding-left: 17px">
                <asp:TextBox ID="txt_capacity_arwa" runat="server" onkeypress="return onlyNumbers(this);" Width="194px" AutoComplete="off" MaxLength="15" Class="alphaNumericWithoutSpace"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                    ControlToValidate="txt_capacity_arwa" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>
            </td>
            <td colspan="2">उसना </td>
            <td style="text-align: left; padding-left: 17px">
                <asp:TextBox ID="txt_capacity_usna" runat="server" onkeypress="return onlyNumbers(this);" Width="170px" AutoComplete="off" MaxLength="15" Class="alphaNumericWithoutSpace"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                    ControlToValidate="txt_capacity_usna" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator></td>
        </tr>


        <tr>
            <td colspan="2" style="text-align:left">मिलिंग अवधि में प्रतिदिन कितने शिफ्ट में काम किया जाना है?</td>
            <td style="text-align: left; padding-left: 17px">
                <asp:DropDownList ID="ddlCropYearshift" runat="server" Width="204px" Height="28px">
                    <asp:ListItem>--Select--</asp:ListItem>
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                </asp:DropDownList>
                </td>
            <td colspan="2">&nbsp;</td>
            <td style="text-align: left; padding-left: 17px">
                &nbsp;</td>
        </tr>


        <tr>
            <td style="text-align:left">GST नम्बर </td>
            <td>
                <asp:TextBox ID="salstax" runat="server" Width="200px" AutoComplete="off" Style="margin-left: 10px; margin-right: 2px" MaxLength="15" MinLength="15" Class="alphaNumericWithoutSpace"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                    ControlToValidate="salstax" ErrorMessage="RequiredFieldValidator">Please enter correct GST number. It should be 15 characters.</asp:RequiredFieldValidator>
            </td>
            <td colspan="3" ><b>आयकर पैन नम्बर </b></td>

            <td style="text-align: left; padding-left: 17px">
                <asp:TextBox ID="panno" runat="server" Width="170px" AutoComplete="off" MaxLength="20" Class="alphaNumericWithoutSpace"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server"
                    ControlToValidate="panno" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator></td>
        </tr>
           <tr>
               <td style="text-align:left">आधार कार्ड नम्बर </td>
               <td style="text-align:left">
                <asp:TextBox ID="txt_aadharCard" runat="server" Width="200px" AutoComplete="off" Style="margin-left: 10px; margin-right: 2px" MaxLength="12" MinLength="12" Class="alphaNumericWithoutSpace"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                    ControlToValidate="txt_aadharCard" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>
            

               </td>
              
               <td colspan="4">
                   </td>
               </tr>
        <tr>
            <td style="text-align:left">विद्युत विभाग द्वारा आवंटित सर्विस टैक्स  न. </td>
            <td style="text-align: left">
                <asp:TextBox ID="alloted_sevicetax" runat="server" Width="200px" AutoComplete="off" Style="margin-left: 10px; margin-right: 2px" MaxLength="20" Class="alphaNumericWithoutSpace"></asp:TextBox>
            </td>
            <td colspan="3">
                <b>विद्युत विभाग द्वारा की गयी वर्तमान रीडिंग न. </b>
            </td>
            <td style="text-align: left; padding-left: 17px">
                <asp:TextBox ID="current_reading" runat="server" Width="170px" AutoComplete="off" onkeypress="return onlyNumbersbags(this);" MaxLength="20" Class="alphaNumericWithoutSpace" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server"
                    ControlToValidate="current_reading" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator></td>

        </tr>

        <tr>
            <td style="text-align:left">मंडी प्रसंस्करण कर्ता लायसेंस न. </td>
            <td style="text-align:left">
                <asp:TextBox ID="prasans_lisanceno" runat="server" Width="200px" AutoComplete="off" Style="margin-left: 10px; margin-right: 2px" MaxLength="20" Class="alphaNumericWithoutSpace"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"
                    ControlToValidate="prasans_lisanceno" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>
            </td>
            <td colspan="3"><b>मंडी व्यापर लायसेंस न. </b></td>
            <td style="text-align: left; padding-left: 17px">
                <asp:TextBox ID="vyapar_lisanceno" runat="server" Width="170px" AutoComplete="off" MaxLength="20" Class="alphaNumericWithoutSpace"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server"
                    ControlToValidate="vyapar_lisanceno" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator></td>
        </tr>

        <tr>
            <td style="text-align:left">मिल की भोगौलिक स्थिति दर्ज करें </td>
            <td>अक्षांश
            </td>
            <td style="text-align: left; padding-left: 17px">
                <asp:TextBox ID="akshansh" runat="server" Width="195px" AutoComplete="off" onkeypress="return onlyNumbersbags(this);" MaxLength="20" Class="alphaNumericWithoutSpace"></asp:TextBox>
            </td>
            <td colspan="2">देशान्त
            </td>
            <td style="text-align: left; padding-left: 17px">
                <asp:TextBox ID="deshant" runat="server" Width="170px" AutoComplete="off" onkeypress="return onlyNumbersbags(this);" MaxLength="20" Class="alphaNumericWithoutSpace"></asp:TextBox></td>
        </tr>

        <tr>
            <td style="text-align:left">निकटतम संग्रहण केंद्र ,FCI,<br />
                CMR,NAN,CMR Center<span class="auto-style5"> (in 
                    Km)</span></td>
            <td class="auto-style5">
                <strong>संग्रहण केंद्र</strong></td>
            <td class="auto-style5"><strong>दूरी</strong></td>
            <td colspan="2" class="auto-style5">
                <strong>संग्रहण केंद्र</strong></td>
            <td><strong class="auto-style5">दूरी</strong></td>
        </tr>
        <tr>
            <td rowspan="2">&nbsp;</td>
            <td style="text-align:center">
                1)
                <asp:DropDownList ID="ddl_issuecenter" runat="server" Width="150px">
                </asp:DropDownList>
            </td>
            <td style="text-align: left; padding-left: 17px">
                <asp:TextBox ID="txt_ictocmr_distance" runat="server" onkeypress="return onlyNumbersbags(this);" AutoComplete="off" Width="194px" MaxLength="4" Class="alphaNumericWithoutSpace"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server"
                    ControlToValidate="txt_ictocmr_distance" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator></td>
            <td colspan="2">
                2)
                <asp:DropDownList ID="ddl_issuecenter3" runat="server" Width="150px">
                </asp:DropDownList>
            </td>
            <td style="text-align: left; padding-left: 17px">
                <asp:TextBox ID="txt_ictocmr_distance2" runat="server" onkeypress="return onlyNumbersbags(this);" AutoComplete="off" Width="170" MaxLength="4" Class="alphaNumericWithoutSpace"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server"
                    ControlToValidate="txt_ictocmr_distance2" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="text-align:center">
                <span class="auto-style6">3)</span>
                <asp:DropDownList ID="ddl_issuecenter1" runat="server" Width="150px">
                </asp:DropDownList>
            </td>
            <td style="text-align: left; padding-left: 17px">
                <asp:TextBox ID="txt_ictocmr_distance4" runat="server" onkeypress="return onlyNumbersbags(this);" AutoComplete="off" Width="194px" MaxLength="4" Class="alphaNumericWithoutSpace"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server"
                    ControlToValidate="txt_ictocmr_distance4" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator></td>
            <td colspan="2">
                4)
                <asp:DropDownList ID="ddl_issuecenter2" runat="server" Width="150px">
                </asp:DropDownList>
            </td>

            <td style="text-align: left; padding-left: 17px">
                <asp:TextBox ID="txt_ictocmr_distance1" runat="server" onkeypress="return onlyNumbersbags(this);" AutoComplete="off" Width="170px" MaxLength="4" Class="alphaNumericWithoutSpace"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server"
                    ControlToValidate="txt_ictocmr_distance1" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td style="text-align:left"> अप्रैल 17 से सितम्बर 17 तक मिलिंग के दौरान
                <br />
                खपत हुई बिजली बिल की यूनिट तथा
                <br />
                कर्मचारी की संख्या</td>
            <td class="auto-style5">
                <strong>Month</strong></td>
            <td colspan="2" class="auto-style5">
                <strong>Unit Consumption</strong></td>
            <td class="auto-style5">
                <strong>Number of Employed</strong></td>
            <td class="auto-style5">
                <strong>Number of Shift</strong></td>
        </tr>

        <tr>
            <td rowspan="6">&nbsp;</td>
            <td class="auto-style8">
               <span style="margin-left:100px;"><strong>April</strong></span></td>
            <td colspan="2">
                <asp:TextBox ID="txtAprUnit" runat="server" onkeypress="return onlyNumbersbags(this);" AutoComplete="off" Width="180px" MaxLength="6" Class="alphaNumericWithoutSpace"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server"
                    ControlToValidate="txtAprUnit" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtAprEmp" runat="server" onkeypress="return onlyNumbersbags(this);" AutoComplete="off" Width="110px" MaxLength="6" Class="alphaNumericWithoutSpace"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server"
                    ControlToValidate="txtAprEmp" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtAprShift" runat="server" onkeypress="return onlyNumbersbags(this);" AutoComplete="off" Width="180px" MaxLength="6" Class="alphaNumericWithoutSpace"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server"
                    ControlToValidate="txtAprShift" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>
                </td>
        </tr>
            
        <tr>
            <td>
                <span style=" text-align: left;">May</span></td>
            <td colspan="2">
                <asp:TextBox ID="txtMayUnit" runat="server" onkeypress="return onlyNumbersbags(this);" AutoComplete="off" Width="180px" MaxLength="6" Class="alphaNumericWithoutSpace"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server"
                    ControlToValidate="txtMayUnit" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtMayEmp" runat="server" onkeypress="return onlyNumbersbags(this);" AutoComplete="off" Width="110px" MaxLength="6" Class="alphaNumericWithoutSpace"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server"
                    ControlToValidate="txtMayEmp" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtMayShift" runat="server" onkeypress="return onlyNumbersbags(this);" AutoComplete="off" Width="180px" MaxLength="6" Class="alphaNumericWithoutSpace"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server"
                    ControlToValidate="txtMayShift" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>
                </td>
        </tr>
        
        <tr>
            <td>
                <span >June</span></td>
            <td colspan="2">
                <asp:TextBox ID="txtJuneUnit" runat="server" onkeypress="return onlyNumbersbags(this);" AutoComplete="off" Width="180px" MaxLength="6" Class="alphaNumericWithoutSpace"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server"
                    ControlToValidate="txtJuneUnit" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtJuneEmp" runat="server" onkeypress="return onlyNumbersbags(this);" AutoComplete="off" Width="110px" MaxLength="6" Class="alphaNumericWithoutSpace"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server"
                    ControlToValidate="txtJuneEmp" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtJuneShift" runat="server" onkeypress="return onlyNumbersbags(this);" AutoComplete="off" Width="180px" MaxLength="6" Class="alphaNumericWithoutSpace"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server"
                    ControlToValidate="txtJuneShift" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>
                </td>
        </tr>
        
        <tr>
            <td>
                <span >July</span></td>
            <td colspan="2">
                <asp:TextBox ID="txtJulyUnit" runat="server" onkeypress="return onlyNumbersbags(this);" AutoComplete="off" Width="180px" MaxLength="6" Class="alphaNumericWithoutSpace"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server"
                    ControlToValidate="txtJulyUnit" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtJulyEmp" runat="server" onkeypress="return onlyNumbersbags(this);" AutoComplete="off" Width="110px" MaxLength="6" Class="alphaNumericWithoutSpace"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server"
                    ControlToValidate="txtJulyEmp" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtJulyShift" runat="server" onkeypress="return onlyNumbersbags(this);" AutoComplete="off" Width="180px" MaxLength="6" Class="alphaNumericWithoutSpace"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server"
                    ControlToValidate="txtJulyShift" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>
                </td>
        </tr>
        
        <tr>
            <td>
                <span>August</span></td>
            <td colspan="2">
                <asp:TextBox ID="txtAugUnit" runat="server" onkeypress="return onlyNumbersbags(this);" AutoComplete="off" Width="180px" MaxLength="6" Class="alphaNumericWithoutSpace"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server"
                    ControlToValidate="txtAugUnit" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtAugEmp" runat="server" onkeypress="return onlyNumbersbags(this);" AutoComplete="off" Width="110px" MaxLength="6" Class="alphaNumericWithoutSpace"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server"
                    ControlToValidate="txtAugEmp" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtAugShift" runat="server" onkeypress="return onlyNumbersbags(this);" AutoComplete="off" Width="180px" MaxLength="6" Class="alphaNumericWithoutSpace"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server"
                    ControlToValidate="txtAugShift" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>
                </td>
        </tr>
        
        <tr>
            <td>
                <span>September</span></td>
            <td colspan="2">
                <asp:TextBox ID="txtSepUnit" runat="server" onkeypress="return onlyNumbersbags(this);" AutoComplete="off" Width="180px" MaxLength="6" Class="alphaNumericWithoutSpace"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server"
                    ControlToValidate="txtSepUnit" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtSepEmp" runat="server" onkeypress="return onlyNumbersbags(this);" AutoComplete="off" Width="110px" MaxLength="6" Class="alphaNumericWithoutSpace"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server"
                    ControlToValidate="txtSepEmp" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtSepShift" runat="server" onkeypress="return onlyNumbersbags(this);" AutoComplete="off" Width="180px" MaxLength="6" Class="alphaNumericWithoutSpace"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server"
                    ControlToValidate="txtSepShift" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td colspan="2"></td>
            <td colspan="4">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align:left">गत वर्ष की गयी कस्टम 
                    मिलिंग की मात्रा  <span class="auto-style5">(in MT)</span></td>
            <td>
                <asp:TextBox ID="lastyear_milling_quantity" runat="server" onkeypress="return onlyNumbers(this);" Width="200px" AutoComplete="off" Style="margin-left: 10px; margin-right: 2px" MaxLength="10" Class="alphaNumericWithoutSpace"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server"
                    ControlToValidate="lastyear_milling_quantity" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>
            </td>
            <td colspan="2" style="text-align:left"> <strong>आगामी
             16/01/2018 से 30/06/2018 तक कितनी मात्रा की मिलिंग कर सकेंगे  </strong> <span class="auto-style5">(in MT)</span>
            </td>
           <td colspan="2">
            <asp:TextBox ID="txt_upcomingsixmonths" runat="server" onkeypress="return onlyNumbers(this);" Width="200px" AutoComplete="off" Style="margin-left: 10px; margin-right: 2px" MaxLength="10" Class="alphaNumericWithoutSpace"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                    ControlToValidate="txt_upcomingsixmonths" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>
               <%--<div class="tooltip">?
  <span class="tooltiptext">

1:-, वर्ष 2017-18 में माह जून 18 तक दिए गए टारगेट की मिलिंग न कर पाने की दशा में रू0 5000/- प्रति टन की दर से पेनाल्टी अधिरोपित की जावेगी | <br />

2:- मिलर को मिलिंग चार्जेस का 50% भुगतान तत्समय किया का सकेगा | शेष समस्त राशि का भुगतान धान की सम्पूर्ण मात्रा की मिलिंग के पश्चात की जावेगी  <br />
</span>
</div>--%>
            </td>
        </tr>

        <tr>
                    <td  colspan="6" style="font-size:large; font-weight:700; text-align:center" class="auto-style4">
                        <span>
                            क्या गत वर्ष मध्य प्रदेश राज्य की मिलिंग की गयी है?
                            <asp:CheckBox ID="txt_millingofPreviousYear" Checked="false" runat="server" AutoPostBack="true" OnCheckedChanged="txt_millingofPreviousYear_CheckedChanged" />
                        </span>

                    </td>
                </tr>
         <tr style="text-align:center" visible="false" id="trone" runat="server">
             <td colspan="2">
                 समस्त जिलो में मिलर की कुल अनुबंध की मात्रा (MT)
             </td>
             <td>
                                             <asp:TextBox ID="txt_total_agreeQty" runat="server" AutoComplete="off" MaxLength="30"  Width="300px"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server"
                    ControlToValidate="txt_total_agreeQty" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>
             </td>
             <td colspan="2">
                <strong>  निरिक्षण में पाई गयी कुल BRL चावल की मात्रा (MT)</strong>
                  
             </td>
             <td>
                                             <asp:TextBox ID="txt_InsBRLQty" runat="server" AutoComplete="off" MaxLength="30"  Width="300px" AutoPostBack="true" OnTextChanged="txt_InsBRLQty_TextChanged"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server"
                    ControlToValidate="txt_InsBRLQty" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>
             </td>
             </tr>
         <tr style="text-align:center" visible="false" id="trtwo" runat="server">
             <td colspan="2">
                 Upgraded/बदली गयी BRL चावल की मात्रा (MT)
             </td>
             <td>
                                             <asp:TextBox ID="txt_Changed_BRLQty" runat="server" AutoComplete="off" MaxLength="30" AutoPostBack="true"  Width="300px" OnTextChanged="txt_Changed_BRLQty_TextChanged"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server"
                    ControlToValidate="txt_Changed_BRLQty" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>
             </td>
             <td colspan="2">
                <strong> Upgradation/बदलने हेतु शेष मात्रा (MT) </strong>
             </td>
             <td>
                                             <asp:TextBox ID="txt_remQty" Enabled="false" runat="server" AutoComplete="off" MaxLength="30"  Width="300px"></asp:TextBox>

             </td>
                
             </tr>
        <tr style="text-align:center" visible="false" id="trthree" runat="server">
            <td colspan="3">
                 पुराने वारदाने (Bales)
            </td>
            <td colspan="3">
                 <asp:TextBox ID="txtgunnyBags" runat="server" AutoComplete="off" MaxLength="30" AutoPostBack="true"  Width="300px" OnTextChanged="txt_Changed_BRLQty_TextChanged"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator44" runat="server"
                    ControlToValidate="txtgunnyBags" ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr style="text-align:center">
            <td colspan="6"">
                <strong>संचालक के द्वारा अधिकृत प्रतिनिधि </strong>

                <table align="center" width="100%" border="1">
                    <tr style="text-align:center">
                        <td class="auto-style10">क्रमांक </td>
                        <td class="auto-style10">प्रतिनिधि का नाम </td>
                        <td class="auto-style10">परिचय पत्र का स्वरुप </td>
                        <td class="auto-style10">प्रतिनिधि का पता </td>
                       <%-- <td class="auto-style10">प्रतिनिधि का छायाचित्र </td>--%>
                       <%-- <td class="auto-style10">प्रतिनिधि के हस्ताक्षर का छायाचित्र </td>--%>
                    </tr>
                    <tr style="text-align:center">
                        <td>1</td>
                        
                        <td>
                            <asp:TextBox ID="pratinidhi" runat="server" AutoComplete="off" MaxLength="30" Class="alphaOnly" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddidentity" runat="server" Width="150px">
                                <asp:ListItem>आधार कार्ड</asp:ListItem>
                                <asp:ListItem>पैन कार्ड</asp:ListItem>
                                
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="address" runat="server" AutoComplete="off" Class="alphaNumeric" MaxLength="70" Width="300px"></asp:TextBox>
                        </td>
                  <%--      <td>
                            <asp:FileUpload ID="photo" runat="server" onchange="validateFileSize1();"/>
                             <div id="Div1" style="background-color:Red; color:White; width:250px; padding:1px; display:none;" >
        छायाचित्र 3 MB ज्यादा नहीं होना चाहिए, कृपया छायाचित्र बदले|
        </div>
                        </td>--%>
        <%--                <td>
                            <asp:FileUpload ID="signature" runat="server" onchange="validateFileSize2();" />
                             <div id="Div2" style="background-color:Red; color:White; width:250px; padding:1px; display:none;" >
        छायाचित्र 3 MB ज्यादा नहीं होना चाहिए, कृपया छायाचित्र बदले|
        </div>
                        </td>--%>
                    </tr>
                </table>

                <%--<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                    EnableModelValidation="True" style="text-align:left; margin-left:75px" >
                    <Columns>
                        <asp:BoundField HeaderText="क्रमांक " DataField="mill_id" />
                        <asp:TemplateField HeaderText="चुनें " Visible="False">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox6" runat="server" AutoComplete="off"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chk_select" runat="server" AutoPostBack="True"
                                    OnCheckedChanged="chk_select_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="प्रतिनिधि का नाम ">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" AutoComplete="off"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txt_pratinidhi" runat="server" Width="170px" Enabled="true" AutoComplete="off"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="परिचय पत्र का स्वरुप ">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" AutoComplete="off"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:DropDownList ID="ddl_identityfomate" runat="server" Width="150px">
                                    <asp:ListItem>ड्राइविंग लाइसेंस </asp:ListItem>
                                    <asp:ListItem>मतदाता पहचान पत्र</asp:ListItem>
                                    <asp:ListItem>पैन कार्ड</asp:ListItem>
                                    <asp:ListItem>राशन कार्ड </asp:ListItem>
                                    <asp:ListItem>उपलब्ध नहीं </asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="प्रतिनिधि का पता ">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" AutoComplete="off"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txt_address" runat="server" Width="170px" Enabled="true" AutoComplete="off"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="प्रतिनिधि का छायाचित्र ">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server" AutoComplete="off"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:FileUpload ID="fileupload_photo" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="प्रतिनिधि के हस्ताक्षर का छायाचित्र ">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox5" runat="server" AutoComplete="off"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:FileUpload ID="FileUpload_signature" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>--%>

            </td>
        </tr>
        <tr style="text-align:center">
            <td colspan="6" style="text-align:center">
                <asp:Label ID="lbl_passwordname1" runat="server" Text="Your Registration No. is:"
                            Visible="False" CssClass="auto-style7"></asp:Label>
                        <asp:Label ID="lbl_registeredno1" runat="server"
                            Style="font-weight: 700; font-size: large;" CssClass="auto-style7"></asp:Label></td>
        </tr>

        <tr style="text-align:center">
            <td colspan="6" style="text-align:center">
                <asp:Button ID="btn_new" runat="server" Style="background-color: #FF6937;" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btn_new_Click" />

                    <asp:Button ID="btn_submit" runat="server" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Enabled="false" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Submit" Width="115px" CssClass="ButtonClass" Style="margin-left: 30px; background-color: #FF6937;" OnClientClick="return validate();"  OnClick="btn_submit_Click" />

                <asp:Button ID="btnPrint" runat="server" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Print" Width="115px" CssClass="ButtonClass" Style="margin-left: 30px; background-color: #FF6937;"  CausesValidation="False" OnClick="btnPrint_Click" Enabled="False" />

                    <asp:Button ID="btn_close" runat="server" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 30px; background-color: #FF6937;" OnClick="btn_close_Click" />

            </td>
        </tr>
       
      <%--<tr style="text-align:center">
             <td colspan="6" style="text-align:center; color:red;" ><span>
                 

1:- वर्ष 2017-18 में माह जून 18 तक दिए गए टारगेट की मिलिंग न कर पाने की दशा में रू0 5000/- प्रति टन की दर से पेनाल्टी अधिरोपित की जावेगी | <br />

2:- मिलर को मिलिंग चार्जेस का 50% भुगतान तत्समय किया का सकेगा | शेष समस्त राशि का भुगतान धान की सम्पूर्ण मात्रा की मिलिंग के पश्चात की जावेगी  <br />
                 </span>
                 </td>
            </tr>--%>
         <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/Alphabets.js"></script>


        </table>
    
        </form>
    </div>

    <input type="hidden" id="div_position" name="div_position" />
      
    
</body>
</html>
