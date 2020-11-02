<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Receipt_Entry_CMR.aspx.cs" Inherits="IssueCenter_Receipt_Entry_CMR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-ui.js"></script>

    <script type="text/javascript" language="javascript">
        function validate() {
            if (Page_ClientValidate())
                return confirm('क्या आप इस लॉट को Reject करना चाहते है? यदि हाँ, तो OK पर क्लिक करे अन्यथा Cancel पर क्लिक करे|');
        }
    </script>

    <script type="text/javascript" language="javascript">
        function validate1() {
            if (Page_ClientValidate())
                return confirm('क्या आप इस लॉट को Accept करना चाहते है? यदि हाँ, तो OK पर क्लिक करे अन्यथा Cancel पर क्लिक करे|');
        }
    </script>

    <script type="text/javascript">
        function Check(textBox, maxLength) {
            if (textBox.value.length > maxLength) {
                alert("Maximum " + maxLength + " Characters Allowed");
                textBox.value = textBox.value.substr(0, maxLength);
            }
        }

        function addCommonDhan() {

            var x = $("#ctl00_ContentPlaceHolder1_chkCommon").prop('checked');
            if (x) {
                $("#ctl00_ContentPlaceHolder1_TxtTotaS").val($("#ctl00_ContentPlaceHolder1_LblTotaS").text());
                $("#ctl00_ContentPlaceHolder1_TxtChoteToteS").val($("#ctl00_ContentPlaceHolder1_LblChoteToteS").text());
                $("#ctl00_ContentPlaceHolder1_txtVijatiyeS").val($("#ctl00_ContentPlaceHolder1_LblVijatiyeS").text());
                $("#ctl00_ContentPlaceHolder1_txtDamageDaaneS").val($("#ctl00_ContentPlaceHolder1_LblDamageDaaneS").text());
                $("#ctl00_ContentPlaceHolder1_txtBadrangDaaneS").val($("#ctl00_ContentPlaceHolder1_LblBadrangDaaneS").text());
                $("#ctl00_ContentPlaceHolder1_txtChaakiDaaneS").val($("#ctl00_ContentPlaceHolder1_LblChaakiDaaneS").text());
                $("#ctl00_ContentPlaceHolder1_txtLaalDaaneS").val($("#ctl00_ContentPlaceHolder1_LblLaalDaaneS").text());
                $("#ctl00_ContentPlaceHolder1_txtOtherS").val($("#ctl00_ContentPlaceHolder1_LblOtherS").text());
                $("#ctl00_ContentPlaceHolder1_txtChokarDaaneS").val($("#ctl00_ContentPlaceHolder1_LblChokarDaaneS").text());
                $("#ctl00_ContentPlaceHolder1_txtNamiS").val($("#ctl00_ContentPlaceHolder1_LblNamiS").text());
            }
            else {
                $("#ctl00_ContentPlaceHolder1_TxtTotaS").val("");
                $("#ctl00_ContentPlaceHolder1_TxtChoteToteS").val("");
                $("#ctl00_ContentPlaceHolder1_txtVijatiyeS").val("");
                $("#ctl00_ContentPlaceHolder1_txtDamageDaaneS").val("");
                $("#ctl00_ContentPlaceHolder1_txtBadrangDaaneS").val("");
                $("#ctl00_ContentPlaceHolder1_txtChaakiDaaneS").val("");
                $("#ctl00_ContentPlaceHolder1_txtLaalDaaneS").val("");
                $("#ctl00_ContentPlaceHolder1_txtOtherS").val("");
                $("#ctl00_ContentPlaceHolder1_txtChokarDaaneS").val("");
                $("#ctl00_ContentPlaceHolder1_txtNamiS").val("");
            }
        }

        function addGradeADhan() {

            var x = $("#ctl00_ContentPlaceHolder1_chkGradeA").prop('checked');
            if (x) {
                $("#ctl00_ContentPlaceHolder1_TxtTotaGA").val($("#ctl00_ContentPlaceHolder1_LblTotaGA").text());
                $("#ctl00_ContentPlaceHolder1_TxtChoteToteGA").val($("#ctl00_ContentPlaceHolder1_LblChoteToteGA").text());
                $("#ctl00_ContentPlaceHolder1_txtVijatiyeGA").val($("#ctl00_ContentPlaceHolder1_LblVijatiyeGA").text());
                $("#ctl00_ContentPlaceHolder1_txtDamageDaaneGA").val($("#ctl00_ContentPlaceHolder1_LblDamageDaaneGA").text());
                $("#ctl00_ContentPlaceHolder1_txtBadrangDaaneGA").val($("#ctl00_ContentPlaceHolder1_LblBadrangDaaneGA").text());
                $("#ctl00_ContentPlaceHolder1_txtChaakiDaaneGA").val($("#ctl00_ContentPlaceHolder1_LblChaakiDaaneGA").text());
                $("#ctl00_ContentPlaceHolder1_txtLaalDaaneGA").val($("#ctl00_ContentPlaceHolder1_LblLaalDaaneGA").text());
                $("#ctl00_ContentPlaceHolder1_txtOtherGA").val($("#ctl00_ContentPlaceHolder1_LblOtherGA").text());
                $("#ctl00_ContentPlaceHolder1_txtChokarDaaneGA").val($("#ctl00_ContentPlaceHolder1_LblChokarDaaneGA").text());
                $("#ctl00_ContentPlaceHolder1_txtNamiGA").val($("#ctl00_ContentPlaceHolder1_LblNamiGA").text());
            }
            else {
                $("#ctl00_ContentPlaceHolder1_TxtTotaGA").val("");
                $("#ctl00_ContentPlaceHolder1_TxtChoteToteGA").val("");
                $("#ctl00_ContentPlaceHolder1_txtVijatiyeGA").val("");
                $("#ctl00_ContentPlaceHolder1_txtDamageDaaneGA").val("");
                $("#ctl00_ContentPlaceHolder1_txtBadrangDaaneGA").val("");
                $("#ctl00_ContentPlaceHolder1_txtChaakiDaaneGA").val("");
                $("#ctl00_ContentPlaceHolder1_txtLaalDaaneGA").val("");
                $("#ctl00_ContentPlaceHolder1_txtOtherGA").val("");
                $("#ctl00_ContentPlaceHolder1_txtChokarDaaneGA").val("");
                $("#ctl00_ContentPlaceHolder1_txtNamiGA").val("");
            }
        }
    </script>


    <style type="text/css">
        .ButtonClass
        {
            cursor: pointer;
        }

        .auto-styleNSC
        {
            width: 622px;
        }

        .auto-style1
        {
            color: red;
        }
    </style>

    <script type="text/javascript" lang="javascript">
        function SetTarget() {
            document.forms[0].target = "_blank";
        }
    </script>

    <%--Allow Only Alphabets using class="alphaNumericWithSpecial" --%>
    <script type="text/javascript">
        window.onload = function () {
            var scrollY = parseInt('<%=Request.Form["scrollY"] %>');
            if (!isNaN(scrollY)) {
                window.scrollTo(0, scrollY);
            }
        };
        window.onscroll = function () {
            var scrollY = document.body.scrollTop;
            if (scrollY == 0) {
                if (window.pageYOffset) {
                    scrollY = window.pageYOffset;
                }
                else {
                    scrollY = (document.body.parentElement) ? document.body.parentElement.scrollTop : 0;
                }
            }
            if (scrollY > 0) {
                var input = document.getElementById("scrollY");
                if (input == null) {
                    input = document.createElement("input");
                    input.setAttribute("type", "hidden");
                    input.setAttribute("id", "scrollY");
                    input.setAttribute("name", "scrollY");
                    document.forms[0].appendChild(input);
                }
                input.value = scrollY;
            }
        };
    </script>

    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/Number2D.js"></script>

    <%--Allow Only Alphabets using class="alphaNumericWithSpecial" --%>
    <link href="../PaddyMilling/calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="../PaddyMilling/calendar/calendarLot.js"></script>

    <table align="center" style="width: 100%; text-align: left; border-style: solid; border-width: 1px; font-size: medium" border="1" cellspacing="0" cellpadding="2">
        <tr>
            <input id="hdfLotNO" type="hidden" runat="server" />
            <input id="hdfMillID" type="hidden" runat="server" />
            <input id="hdfDO_NO" type="hidden" runat="server" />
            <input id="hdfBookOnlyNumber" type="hidden" runat="server" />
            <input id="hdfAcpt_Reject_No" type="hidden" runat="server" />
            <input id="hdfPaddy_AgrmtDist" type="hidden" runat="server" />
            <input id="hdfAdjustCMRDO" type="hidden" runat="server" />
            <td colspan="4" style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600; border-style: none"><b>सीएमआर (चावल) की गुणवत्ता परिक्षण रिपोर्ट एवं चावल की प्राप्ति</b></td>
        </tr>

        <tr>
            <td colspan="4" style="text-align: center; background-color: #CCFF99">
                <asp:Label ID="Label2" runat="server" Style="text-align: center; font-weight: 700;" ForeColor="Blue" Visible="False"></asp:Label>
            </td>

        </tr>
        <tr>
            <td>District</td>
            <td>
                <asp:TextBox ID="txtDistrict" runat="server" Width="137px" Enabled="False" ReadOnly="True"></asp:TextBox>
                <b>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator45" runat="server" ControlToValidate="txtDistrict" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                </b></td>
            <td>Crop Year</td>
            <td>
                <%--<asp:TextBox ID="txtYear" runat="server" Width="137px" Enabled="False" ReadOnly="True"></asp:TextBox>
                <b>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server" ControlToValidate="txtYear" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                </b>--%>
                 <asp:DropDownList ID="ddlCropyear" runat="server" Height="27px" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlCropyear_SelectedIndexChanged">
                </asp:DropDownList>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlCropyear" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>


            </td>
        </tr>

        <tr>
            <td>Branch Name</td>
            <td>
                <asp:DropDownList ID="ddlBranch" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
            <td>Godown Name</td>
            <td>
                <asp:DropDownList ID="ddlGodown" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
        </tr>
         <tr>
             <td>
                Stack Number
            </td>
            <td>
                <asp:DropDownList ID="ddlstackNumber" runat="server" Width="141px" >
                </asp:DropDownList>

            </td>
           
        </tr>
        <tr>
            <td colspan="4" style="background-color: #CCFF99;"></td>
        </tr>
        <tr>
            <td style="text-align: left">CMR Deposit Order No.</td>
            <td class="auto-style8" style="text-align: left">
                <asp:DropDownList ID="ddlCMRDONo" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlCMRDONo_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
            <td style="text-align: left">CMR Submission Date</td>
            <td style="text-align: left">
                <asp:TextBox ID="CMRDODate" runat="server" Enabled="False" ReadOnly="True" Width="137px"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td>Lot Number</td>
            <td>
                <asp:TextBox ID="txtLotNo" runat="server" Enabled="False" ReadOnly="True" Width="137px"></asp:TextBox>
                <b>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator53" runat="server" ControlToValidate="txtLotNo" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                </b>
            </td>
            <td>Agreement Number</td>
            <td>
                <asp:TextBox ID="txtAgrmtNo" runat="server" Enabled="False" ReadOnly="True" Width="137px"></asp:TextBox>
                <b>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator54" runat="server" ControlToValidate="txtAgrmtNo" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                </b>
            </td>
        </tr>

        <tr>
            <td>Mill Name</td>
            <td colspan="3">
                <asp:TextBox ID="txtMillName" runat="server" Enabled="False" ReadOnly="True" Width="599px"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>अनुबंधित धान की मात्रा</td>
            <td>
                <asp:TextBox ID="txtAgrmtQty" runat="server" Enabled="False" ReadOnly="True" Width="137px"></asp:TextBox>
                <span class="auto-style1"><strong>(Qtls)</strong></span></td>
            <td>मिलिंग का प्रकार</td>
            <td>
                <asp:TextBox ID="txtMillingType" runat="server" Enabled="False" Height="20px" ReadOnly="True" Width="137px"></asp:TextBox>
                <b>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator47" runat="server" ControlToValidate="txtMillingType" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                </b></td>
        </tr>

        <tr>
            <td colspan="3">अनुबंधित धान की मात्रा तथा मिलिंग के प्रकार के अनुसार मिलर्स से प्राप्ति योग्य चावल की मात्रा</td>
            <td>
                <asp:TextBox ID="txtExpectedRice" runat="server" Enabled="False" Width="137px" ReadOnly="True"></asp:TextBox>
                <span class="auto-style1"><strong>(Qtls)</strong></span></td>
        </tr>

        <tr>
            <td colspan="3">अनुबंधित धान की मात्रा तथा मिलिंग के प्रकार के अनुसार मिलर्स से प्राप्ति योग्य चावल की शेष मात्रा</td>
            <td>
                <asp:TextBox ID="txtRemAgrmtCMRQty" runat="server" Enabled="False" Width="137px" ReadOnly="True"></asp:TextBox>
                <span class="auto-style1"><strong>(Qtls)</strong></span></td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600; border-style: none; background-color: #CC6600;"><b style="color: #FFFF66">Receipt Details</b></td>
        </tr>


        <tr>
            <td><strong>Recd. CMR Qty </strong><span class="auto-style1"><strong>(Qtls)</strong></span></td>
            <td>
                <asp:TextBox ID="txtRecdQty" runat="server" AutoComplete="off" MaxLength="6" onblur="extractNumber(this,2,true);" onfocus="this.select();" onkeypress="return blockNonNumbers(this, event, true, true);" onkeyup="extractNumber(this,2,true);" onmouseup="return false;" Style="text-align: right" Width="137px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator23" runat="server" ControlToValidate="txtRecdQty" Display="Dynamic" ErrorMessage="NAN" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="txtRecdQty" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td><strong>Recd. No. of Bags</strong></td>
            <td>
                <asp:TextBox ID="txtBags" runat="server" AutoComplete="off" class="Number" MaxLength="3" onfocus="this.select();" onmouseup="return false;" Style="text-align: right" Width="137px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="txtBags" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td><strong>Type of Bags</strong></td>
            <td colspan="3">
                <%--<asp:RadioButton ID="rdbNewJute" runat="server" GroupName="Bags" Text="New Jute(SBT)" />
                <asp:RadioButton ID="rdbOldJute" runat="server" GroupName="Bags" Text="Old Jute(SBT)" Style="margin-left: 8px;" />
                <asp:RadioButton ID="rdbOnceJute" runat="server" GroupName="Bags" Text="Once Used Jute(SBT)" Style="margin-left: 8px;" />
                <br />
                <asp:RadioButton ID="rdbNewPP" runat="server" GroupName="Bags" Text="New PP(HDPE)" />
                <asp:RadioButton ID="rdbOldPP" runat="server" GroupName="Bags" Text="Old PP(HDPE)" Style="margin-left: 4px;" />
                <asp:RadioButton ID="rdbOncePP" runat="server" GroupName="Bags" Text="Once Used PP(HDPE)" Style="margin-left: 4px;" />--%>

                 <asp:DropDownList ID="ddlbagstype" runat="server" Width="141px" >
                </asp:DropDownList>
            </td>
        </tr>



        <tr>
            <td><strong>Miller Tags</strong></td>
            <td>
                <asp:RadioButton ID="rdbTagYes" runat="server" GroupName="Tag" Text="Yes" AutoPostBack="True" OnCheckedChanged="rdbTagYes_CheckedChanged" />
                <asp:RadioButton ID="rdbTagNo" runat="server" GroupName="Tag" Text="No" Style="margin-left: 10px;" AutoPostBack="True" OnCheckedChanged="rdbTagNo_CheckedChanged" />

            </td>
            <td><strong>Number of Tags</strong></td>
            <td>
                <asp:TextBox ID="txtTagNo" runat="server" AutoComplete="off" class="Number" MaxLength="3" onfocus="this.select();" onmouseup="return false;" Style="text-align: right" Width="137px"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td colspan="4" style="background-color: #99FF66">यदि चावल दो ट्रकों में आया है, तो कृपया दोनों ट्रकों का नंबर लिखें|</td>

            <tr>
                <td><strong>Truck Number</strong></td>
                <td>
                    <asp:TextBox ID="txtTruckNo" runat="server" AutoComplete="off" Class="TruckNumber" MaxLength="14" onfocus="this.select();" onmouseup="return false;" Width="137px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtTruckNo" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                </td>
                <td><strong>Truck Number1</strong></td>
                <td>
                    <asp:TextBox ID="txtTruckNo0" runat="server" AutoComplete="off" Class="TruckNumber" MaxLength="14" onfocus="this.select();" onmouseup="return false;" Width="137px"></asp:TextBox>
                </td>
            </tr>

            <%--Allow Only Alphabets using class="alphaNumericWithSpecial" --%>
            <script lang="javascript" src="../PaddyMilling/Scripts/TruckNo.js" type="text/javascript">      </script>

            <script lang="javascript" src="../PaddyMilling/Scripts/Number.js" type="text/javascript">

            </script>


            <tr>
                <td><strong>Recd. CMR Date</strong></td>
                <td>
                    <asp:TextBox ID="txtDate" runat="server" ReadOnly="True" Width="137px"></asp:TextBox>
                    <%--<img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtDate' , 'expiry=true,elapse=-150,restrict=true,close=true' )" />--%><asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtDate" ErrorMessage="RequiredFieldValidator" Font-Bold="False" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                    <%--<img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtDate' , 'expiry=true,elapse=-150,restrict=false,close=true')" />--%>

                    <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtDate' , 'expiry=true,elapse=-150,restrict=true,close=true')" />
                </td>
                <td><strong>Toul Parchi No.</strong></td>
                <td>
                    <asp:TextBox ID="txtToulReceiptNo" runat="server" Width="137px" AutoComplete="off" Class="TruckNumber" MaxLength="14" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtToulReceiptNo" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </tr>
    </table>

    <br />


    <table align="center" border="1" style="border-spacing: 1px; text-align: center; width: 894px; font-size: small">
        <tr>
            <td style="width: 31px">क्रम सं.</td>
            <td colspan="2">अपवर्तन</td>

            <td style="width: 101px">अधिकतम सीमा (प्रतिशत)<br />
                <b>ग्रेड-ए</b><br />
            </td>
            <td style="width: 114px">गुणवत्ता परिक्षण उपरांत परिणाम<br />
                <b>ग्रेड-ए<asp:CheckBox ID="chkGradeA" runat="server" onclick="addGradeADhan();" Visible="False" />
                </b></td>
            <td style="width: 114px">अधिकतम सीमा (प्रतिशत)<br />
                <b>कामन</b></td>
            <td style="width: 114px">गुणवत्ता परिक्षण उपरांत परिणाम<br />
                <b>कामन</b><asp:CheckBox ID="chkCommon" runat="server" onclick="addCommonDhan();" Visible="true" />
            </td>
            <td>रिमार्क</td>

        </tr>
        <tr>
            <td rowspan="2" style="width: 31px">1.</td>
            <td style="text-align: left; height: 51px;">टोटा<br />
            </td>
            <td style="height: 51px">
                <asp:Label ID="LblMType" runat="server"></asp:Label>
            </td>
            <td style="width: 101px; height: 51px;">
                <asp:Label ID="LblTotaGA" runat="server"></asp:Label>
                <br />
            </td>
            <td style="width: 114px; height: 51px;">
                <asp:TextBox ID="TxtTotaGA" runat="server" Width="104px" Style="text-align: right" class="NumberDecimal" MaxLength="5" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" Enabled="False" ValidationGroup="GradeA"></asp:TextBox>
            </td>
            <td style="width: 114px; height: 51px;">
                <asp:Label ID="LblTotaS" runat="server"></asp:Label>
            </td>
            <td style="width: 114px; height: 51px;">
                <asp:TextBox ID="TxtTotaS" runat="server" Style="text-align: right" class="NumberDecimal" MaxLength="5" Width="104px" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" ValidationGroup="Common" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtTotaS" ErrorMessage="Invalid Number" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtTotaS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td style="height: 51px">
                <asp:TextBox ID="txtRmkTota" runat="server" Width="150px" CssClass="alphaNumericWithSpecial" TextMode="MultiLine" Style="margin: 3px 2px 1px 2px" onKeyUp="javascript:Check(this, 40);" onChange="javascript:Check(this, 40);"></asp:TextBox>
            </td>

        </tr>

        <tr>

            <td style="text-align: left; height: 51px;">छोटे टोटे</td>
            <td style="height: 51px">
                <asp:Label ID="LblMType0" runat="server"></asp:Label>
            </td>
            <td style="height: 51px">&nbsp;<asp:Label ID="LblChoteToteGA" runat="server"></asp:Label>
                &nbsp;</td>
            <td style="height: 51px">
                <asp:TextBox ID="TxtChoteToteGA" runat="server" Width="104px" Style="text-align: right" class="NumberDecimal" MaxLength="5" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" Enabled="False" ValidationGroup="GradeA"></asp:TextBox>
            </td>
            <td style="height: 51px">
                <asp:Label ID="LblChoteToteS" runat="server"></asp:Label>
            </td>
            <td style="height: 51px">
                <asp:TextBox ID="TxtChoteToteS" runat="server" Style="text-align: right" class="NumberDecimal" MaxLength="5" Width="104px" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" ValidationGroup="Common" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TxtChoteToteS" ErrorMessage="Invalid Number" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtChoteToteS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td style="height: 51px">
                <asp:TextBox ID="txtRmkCTote" runat="server" Width="150px" CssClass="alphaNumericWithSpecial" TextMode="MultiLine" Style="margin: 3px 2px 1px 2px" onKeyUp="javascript:Check(this, 40);" onChange="javascript:Check(this, 40);"></asp:TextBox>
            </td>

        </tr>

        <tr>
            <td style="width: 31px; height: 51px;">2.</td>
            <td style="text-align: left; height: 51px;">विजातीय तत्व **</td>
            <td style="height: 51px">
                <asp:Label ID="LblMType1" runat="server"></asp:Label>
            </td>
            <td style="width: 101px; height: 51px;">
                <asp:Label ID="LblVijatiyeGA" runat="server"></asp:Label>
            </td>
            <td style="width: 114px; height: 51px;">
                <asp:TextBox ID="txtVijatiyeGA" runat="server" Width="104px" Style="text-align: right" class="NumberDecimal" MaxLength="5" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" Enabled="False" ValidationGroup="GradeA"></asp:TextBox>

            </td>
            <td style="width: 114px; height: 51px;">
                <asp:Label ID="LblVijatiyeS" runat="server"></asp:Label>
            </td>
            <td style="width: 114px; height: 51px;">
                <asp:TextBox ID="txtVijatiyeS" runat="server" Style="text-align: right" class="NumberDecimal" MaxLength="5" Width="104px" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" ValidationGroup="Common" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtVijatiyeS" ErrorMessage="Invalid Number" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtVijatiyeS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td style="height: 51px">
                <asp:TextBox ID="txtRmkVijatiye" runat="server" Width="150px" CssClass="alphaNumericWithSpecial" TextMode="MultiLine" Style="margin: 3px 2px 1px 2px" onKeyUp="javascript:Check(this, 40);" onChange="javascript:Check(this, 40);"></asp:TextBox>
            </td>

        </tr>
        <tr>
            <td style="width: 31px">3.</td>
            <td style="text-align: left">
                <asp:RadioButton ID="rbCDaane" runat="server" GroupName="check" Text="क्षतिग्रस्त दाने (C)" Style="font-weight: 700" />
                <br />
                <asp:RadioButton ID="rbMCDaane" runat="server" GroupName="check" Text="मामूली क्षतिग्रस्त दाने" Style="font-weight: 700" />
                &nbsp;</td>
            <td>
                <asp:Label ID="LblMType2" runat="server"></asp:Label>
            </td>
            <td style="width: 101px">
                <asp:Label ID="LblDamageDaaneGA" runat="server"></asp:Label>
            </td>
            <td style="width: 114px">
                <asp:TextBox ID="txtDamageDaaneGA" runat="server" Width="104px" Style="text-align: right" class="NumberDecimal" MaxLength="5" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" Enabled="False" ValidationGroup="GradeA"></asp:TextBox>
            </td>
            <td style="width: 114px">
                <asp:Label ID="LblDamageDaaneS" runat="server"></asp:Label>
            </td>
            <td style="width: 114px">
                <asp:TextBox ID="txtDamageDaaneS" runat="server" Style="text-align: right" class="NumberDecimal" MaxLength="5" Width="104px" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" ValidationGroup="Common" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtDamageDaaneS" ErrorMessage="Invalid Number" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDamageDaaneS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:TextBox ID="txtRmkDaane" runat="server" Width="150px" CssClass="alphaNumericWithSpecial" TextMode="MultiLine" Style="margin: 3px 2px 1px 2px" onKeyUp="javascript:Check(this, 40);" onChange="javascript:Check(this, 40);"></asp:TextBox>
            </td>

        </tr>
        <tr>
            <td style="width: 31px; height: 51px;">4.</td>
            <td style="text-align: left; height: 51px;">बदरंग दाने</td>
            <td style="height: 51px">
                <asp:Label ID="LblMType3" runat="server"></asp:Label>
            </td>
            <td style="width: 101px; height: 51px;">
                <asp:Label ID="LblBadrangDaaneGA" runat="server"></asp:Label>
            </td>
            <td style="width: 114px; height: 51px;">
                <asp:TextBox ID="txtBadrangDaaneGA" runat="server" Width="104px" Style="text-align: right" class="NumberDecimal" MaxLength="5" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" Enabled="False" ValidationGroup="GradeA"></asp:TextBox>
            </td>
            <td style="width: 114px; height: 51px;">
                <asp:Label ID="LblBadrangDaaneS" runat="server"></asp:Label>
            </td>
            <td style="width: 114px; height: 51px;">
                <asp:TextBox ID="txtBadrangDaaneS" runat="server" Style="text-align: right" class="NumberDecimal" MaxLength="5" Width="104px" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" ValidationGroup="Common" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtBadrangDaaneS" ErrorMessage="Invalid Number" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtBadrangDaaneS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td style="height: 51px">
                <asp:TextBox ID="txtRmkBadrang" runat="server" Width="150px" CssClass="alphaNumericWithSpecial" TextMode="MultiLine" Style="margin: 3px 2px 1px 2px" onKeyUp="javascript:Check(this, 40);" onChange="javascript:Check(this, 40);"></asp:TextBox>
            </td>

        </tr>
        <tr>
            <td style="width: 31px; height: 51px;">5.</td>
            <td style="text-align: left; height: 51px;">चाकी दाने</td>
            <td style="height: 51px">
                <asp:Label ID="LblMType4" runat="server"></asp:Label>
            </td>
            <td style="width: 101px; height: 51px;">
                <asp:Label ID="LblChaakiDaaneGA" runat="server"></asp:Label>
            </td>
            <td style="width: 114px; height: 51px;">
                <asp:TextBox ID="txtChaakiDaaneGA" runat="server" Width="104px" Style="text-align: right" class="NumberDecimal" MaxLength="5" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" Enabled="False" ValidationGroup="GradeA"></asp:TextBox>
            </td>
            <td style="width: 114px; height: 51px;">
                <asp:Label ID="LblChaakiDaaneS" runat="server"></asp:Label>
            </td>
            <td style="width: 114px; height: 51px;">
                <asp:TextBox ID="txtChaakiDaaneS" runat="server" Style="text-align: right" class="NumberDecimal" MaxLength="5" Width="104px" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" ValidationGroup="Common" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="txtChaakiDaaneS" ErrorMessage="Invalid Number" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtChaakiDaaneS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td style="height: 51px">
                <asp:TextBox ID="txtRmkChaki" runat="server" Width="150px" CssClass="alphaNumericWithSpecial" TextMode="MultiLine" Style="margin: 3px 2px 1px 2px" onKeyUp="javascript:Check(this, 40);" onChange="javascript:Check(this, 40);"></asp:TextBox>
            </td>

        </tr>
        <tr>
            <td style="width: 31px; height: 51px;">6.</td>
            <td style="text-align: left; height: 51px;">लाल दाने</td>
            <td style="height: 51px">
                <asp:Label ID="LblMType5" runat="server"></asp:Label>
            </td>
            <td style="width: 101px; height: 51px;">
                <asp:Label ID="LblLaalDaaneGA" runat="server"></asp:Label>
            </td>
            <td style="width: 114px; height: 51px;">
                <asp:TextBox ID="txtLaalDaaneGA" runat="server" Width="104px" Style="text-align: right" class="NumberDecimal" MaxLength="5" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" Enabled="False" ValidationGroup="GradeA"></asp:TextBox>
            </td>
            <td style="width: 114px; height: 51px;">
                <asp:Label ID="LblLaalDaaneS" runat="server"></asp:Label>
            </td>
            <td style="width: 114px; height: 51px;">
                <asp:TextBox ID="txtLaalDaaneS" runat="server" Style="text-align: right" class="NumberDecimal" MaxLength="5" Width="104px" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" ValidationGroup="Common" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ControlToValidate="txtLaalDaaneS" ErrorMessage="Invalid Number" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtLaalDaaneS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td style="height: 51px">
                <asp:TextBox ID="txtRmkLaal" runat="server" Width="150px" CssClass="alphaNumericWithSpecial" TextMode="MultiLine" Style="margin: 3px 2px 1px 2px" onKeyUp="javascript:Check(this, 40);" onChange="javascript:Check(this, 40);"></asp:TextBox>
            </td>

        </tr>
        <tr>
            <td style="width: 31px; height: 51px;">7.</td>
            <td style="text-align: left; height: 51px;">निम्न श्रेणी का सम्मिश्रण</td>
            <td style="height: 51px">
                <asp:Label ID="LblMType6" runat="server"></asp:Label>
            </td>
            <td style="width: 101px; height: 51px;">
                <asp:Label ID="LblOtherGA" runat="server"></asp:Label>
            </td>
            <td style="width: 114px; height: 51px;">
                <asp:TextBox ID="txtOtherGA" runat="server" Width="104px" Style="text-align: right" class="NumberDecimal" MaxLength="5" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" Enabled="False" ValidationGroup="GradeA"></asp:TextBox>
            </td>
            <td style="width: 114px; height: 51px;">
                <asp:Label ID="LblOtherS" runat="server"></asp:Label>
            </td>
            <td style="width: 114px; height: 51px;">
                <asp:TextBox ID="txtOtherS" runat="server" Style="text-align: right" class="NumberDecimal" MaxLength="5" Width="104px" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" ValidationGroup="Common" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server" ControlToValidate="txtOtherS" ErrorMessage="Invalid Number" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtOtherS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td style="height: 51px">
                <asp:TextBox ID="txtRmkShreni" runat="server" Width="150px" CssClass="alphaNumericWithSpecial" TextMode="MultiLine" Style="margin: 3px 2px 1px 2px" onKeyUp="javascript:Check(this, 40);" onChange="javascript:Check(this, 40);"></asp:TextBox>
            </td>

        </tr>
        <tr>
            <td style="width: 31px; height: 51px;">8.</td>
            <td style="text-align: left; height: 51px;">चोकर सहित दाने</td>
            <td style="height: 51px">
                <asp:Label ID="LblMType7" runat="server"></asp:Label>
            </td>
            <td style="width: 101px; height: 51px;">
                <asp:Label ID="LblChokarDaaneGA" runat="server"></asp:Label>
            </td>
            <td style="width: 114px; height: 51px;">
                <asp:TextBox ID="txtChokarDaaneGA" runat="server" Width="104px" Style="text-align: right" class="NumberDecimal" MaxLength="5" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" Enabled="False" ValidationGroup="GradeA"></asp:TextBox>
            </td>
            <td style="width: 114px; height: 51px;">
                <asp:Label ID="LblChokarDaaneS" runat="server"></asp:Label>
            </td>
            <td style="width: 114px; height: 51px;">
                <asp:TextBox ID="txtChokarDaaneS" runat="server" Style="text-align: right" class="NumberDecimal" MaxLength="5" Width="104px" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" ValidationGroup="Common" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server" ControlToValidate="txtChokarDaaneS" ErrorMessage="Invalid Number" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtChokarDaaneS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td style="height: 51px">
                <asp:TextBox ID="txtRmkChokar" runat="server" Width="150px" TextMode="MultiLine" CssClass="alphaNumericWithSpecial" Style="margin: 3px 2px 1px 2px" onKeyUp="javascript:Check(this, 40);" onChange="javascript:Check(this, 40);"></asp:TextBox>
            </td>

        </tr>
        <tr>
            <td style="width: 31px; height: 51px;">9.</td>
            <td style="text-align: left; height: 51px;">नमी तत्व (R)</td>
            <td style="height: 51px">
                <asp:Label ID="LblMType8" runat="server"></asp:Label>
            </td>
            <td style="width: 101px; height: 51px;">
                <asp:Label ID="LblNamiGA" runat="server"></asp:Label>
            </td>
            <td style="width: 114px; height: 51px;">
                <asp:TextBox ID="txtNamiGA" runat="server" Width="104px" Style="text-align: right" class="NumberDecimal" MaxLength="5" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" Enabled="False" ValidationGroup="GradeA"></asp:TextBox>
            </td>
            <td style="width: 114px; height: 51px;">
                <asp:Label ID="LblNamiS" runat="server"></asp:Label>
            </td>
            <td style="width: 114px; height: 51px;">
                <asp:TextBox ID="txtNamiS" runat="server" class="NumberDecimal" Style="text-align: right" MaxLength="5" Width="104px" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" ValidationGroup="Common" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator20" runat="server" ControlToValidate="txtNamiS" ErrorMessage="Invalid Number" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtNamiS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td style="height: 51px">
                <asp:TextBox ID="txtRmkNami" runat="server" Width="150px" TextMode="MultiLine" CssClass="alphaNumericWithSpecial" Style="margin: 3px 2px 1px 2px" onKeyUp="javascript:Check(this, 40);" onChange="javascript:Check(this, 40);"></asp:TextBox>
            </td>

        </tr>


        <tr>
            <td colspan="4" style="height: 40px; font-size: medium">Inspected By
                
            </td>
            <td colspan="4" style="height: 40px;">

                <asp:DropDownList ID="ddl_IC" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddl_IC_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
        </tr>




        <tr>
            <td colspan="8" style="height: 40px;">
                <asp:Button ID="btnQuilityTested" runat="server" BackColor="Silver" ForeColor="Red" BorderColor="#333300" BorderStyle="Solid" CssClass="ButtonClass" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Submit For Quility Inspection" Width="205px" OnClick="btnQuilityTested_Click" Enabled="False" />
            </td>
        </tr>


        <caption>
            <%--Allow Only Alphabets using class="alphaNumericWithSpecial" --%>
            <script lang="javascript" src="../PaddyMilling/Scripts/Alphabets.js" type="text/javascript">

            </script>


        </caption>


    </table>

    <table align="center" class="auto-styleNSC">
        <tr>

            <td colspan="4">
                <asp:Label ID="LblMsg" runat="server" Style="color: #FF0000" Visible="False"></asp:Label>
            </td>


        </tr>
        <tr>

            <td>
                <asp:Button ID="btnNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnNew_Click" />

            </td>

            <td>
                <asp:Button ID="btnAccept" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Accept" Width="115px" CssClass="ButtonClass" OnClick="btnAccept_Click" OnClientClick="return validate1();" Enabled="False" />

            </td>

            <td>
                <asp:Button ID="btnReject" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Reject" Width="115px" CssClass="ButtonClass" OnClick="btnReject_Click" Enabled="False" OnClientClick="return validate();" />
            </td>

            <td>
                <asp:Button ID="btnPrint" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Print" Width="115px" CssClass="ButtonClass" OnClick="btnPrint_Click" CausesValidation="False" Enabled="False" />
            </td>

            <td>
                <asp:Button ID="Close" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="Close_Click" />
            </td>
        </tr>
    </table>

</asp:Content>

