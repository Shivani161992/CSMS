<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Partial_Rejection_Pdy2016.aspx.cs" Inherits="IssueCenter_Partial_Rejection_Pdy2016" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>

    <script type="text/javascript" lang="javascript" src="../js/calendarMvmt.js"></script>

    <%--For Calendar Controls--%>
    <link href="../PaddyMilling/calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="../js/calendarMvmt.js"></script>

    <%--Allow Only 2 Digit After Decimal--%>
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/Number2D.js"></script>

    <script type="text/javascript" language="javascript">
        function validate() {
            if (Page_ClientValidate())
                return confirm('क्या आपने सही जानकारी भरी हैं? यदि हाँ, तो OK पर क्लिक करे अन्यथा Cancel पर क्लिक करे तथा सही जानकारी भरें|');
        }
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("input").focus(function () {
                $(this).css("background-color", "#cccccc");
            });
            $("input").blur(function () {
                $(this).css("background-color", "#ffffff");
            });
        });
    </script>

    <%--Code For Check and Uncheck--%>
    <script type="text/javascript">
        $(function () {
            $("#ctl00_ContentPlaceHolder1_chk_faq").click(function () {
                if ($(this).is(":checked")) {
                    $("#ctl00_ContentPlaceHolder1_txt_faq_per").removeAttr("disabled");
                    $("#ctl00_ContentPlaceHolder1_txt_faq_per").focus();
                } else {
                    $("#ctl00_ContentPlaceHolder1_txt_faq_per").attr("disabled", "disabled");
                    $("#ctl00_ContentPlaceHolder1_txt_faq_per").val(0);
                }
            });
        });

        $(function () {
            $("#ctl00_ContentPlaceHolder1_chk_extra").click(function () {
                if ($(this).is(":checked")) {
                    $("#ctl00_ContentPlaceHolder1_txt_extra_per").removeAttr("disabled");
                    $("#ctl00_ContentPlaceHolder1_txt_extra_per").focus();
                } else {
                    $("#ctl00_ContentPlaceHolder1_txt_extra_per").attr("disabled", "disabled");
                    $("#ctl00_ContentPlaceHolder1_txt_extra_per").val(0);
                }
            });
        });

        $(function () {
            $("#ctl00_ContentPlaceHolder1_chk_damaged").click(function () {
                if ($(this).is(":checked")) {
                    $("#ctl00_ContentPlaceHolder1_txt_damage_per").removeAttr("disabled");
                    $("#ctl00_ContentPlaceHolder1_txt_damage_per").focus();
                } else {
                    $("#ctl00_ContentPlaceHolder1_txt_damage_per").attr("disabled", "disabled");
                    $("#ctl00_ContentPlaceHolder1_txt_damage_per").val(0);
                }
            });
        });

        $(function () {
            $("#ctl00_ContentPlaceHolder1_chk_brightness").click(function () {
                if ($(this).is(":checked")) {
                    $("#ctl00_ContentPlaceHolder1_txt_bright_per").removeAttr("disabled");
                    $("#ctl00_ContentPlaceHolder1_txt_bright_per").focus();
                } else {
                    $("#ctl00_ContentPlaceHolder1_txt_bright_per").attr("disabled", "disabled");
                    $("#ctl00_ContentPlaceHolder1_txt_bright_per").val(0);
                }
            });
        });

        $(function () {
            $("#ctl00_ContentPlaceHolder1_chk_partially").click(function () {
                if ($(this).is(":checked")) {
                    $("#ctl00_ContentPlaceHolder1_txt_partial_per").removeAttr("disabled");
                    $("#ctl00_ContentPlaceHolder1_txt_partial_per").focus();
                } else {
                    $("#ctl00_ContentPlaceHolder1_txt_partial_per").attr("disabled", "disabled");
                    $("#ctl00_ContentPlaceHolder1_txt_partial_per").val(0);
                }
            });
        });

        $(function () {
            $("#ctl00_ContentPlaceHolder1_chk_splited").click(function () {
                if ($(this).is(":checked")) {
                    $("#ctl00_ContentPlaceHolder1_txt_split_per").removeAttr("disabled");
                    $("#ctl00_ContentPlaceHolder1_txt_split_per").focus();
                } else {
                    $("#ctl00_ContentPlaceHolder1_txt_split_per").attr("disabled", "disabled");
                    $("#ctl00_ContentPlaceHolder1_txt_split_per").val(0);
                }
            });
        });

        $(function () {
            $("#ctl00_ContentPlaceHolder1_chk_moist").click(function () {
                if ($(this).is(":checked")) {
                    $("#ctl00_ContentPlaceHolder1_txt_moist_per").removeAttr("disabled");
                    $("#ctl00_ContentPlaceHolder1_txt_moist_per").focus();
                } else {
                    $("#ctl00_ContentPlaceHolder1_txt_moist_per").attr("disabled", "disabled");
                    $("#ctl00_ContentPlaceHolder1_txt_moist_per").val(0);
                }
            });
        });
    </script>

    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
            font-size: small;
        }

        .Qtls {
            font-size: small;
            color: #FF0000;
        }

        .hover_row {
            background-color: #A1DCF2;
        }
    </style>


    <table align="center" style="border-style: solid; border-width: 1px; text-align: left; font-size: medium; width: 850px" border="1" cellspacing="0" cellpadding="3">
        <tr>
            <td colspan="6" style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600"><strong>Partial Rejection Procurement 2016</strong></td>
        </tr>

        <tr>
            <td align="center" colspan="6"
                style="background-color: #cccccc; font-size: small; color: #CC0000;">
                <span style="color: #000099; font-size: small">
                    <span style="font-family: Verdana; text-align: left"><b>स्वीकृत की गयी मात्रा का पहले स्वीकृत पत्र जारी करें, फिर अस्वीकृत मात्रा का अस्वीकृत पत्र जारी करें|</b></span></span><asp:Image ID="Image3" runat="server" Height="16px" ImageUrl="~/IssueCenter/img/blinking_new.gif" Width="39px" />
            </td>

            <input id="hdfGodownCode" type="hidden" runat="server" />
            <input id="hdfOriginalBags" type="hidden" runat="server" />
            <input id="hdfOriginalQty" type="hidden" runat="server" />
        </tr>

        <tr>
            <td rowspan="21">&nbsp;</td>
            <td colspan="4" style="text-align: center; background-color: #CCFF99">
                <asp:Label ID="Label2" runat="server" Style="text-align: center; font-weight: 700; color: #FF0000;" ForeColor="Blue" Visible="False"></asp:Label>
            </td>
            <td rowspan="21"></td>
        </tr>




        <tr>
            <td>Issue Center</td>
            <td>
                <asp:TextBox ID="txtissue" runat="server" Width="110px" ReadOnly="True" Enabled="False"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="txtissue" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
            <td>Crop Year</td>
            <td>
                <asp:TextBox ID="txtYear" runat="server" Width="110px" ReadOnly="True" Enabled="False">2016-2017</asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="txtYear" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>



        <tr>
            <td>Date of Deposit</td>
            <td>
                <asp:TextBox ID="DaintyDate3" runat="server" Width="110px" Style="text-align: right"></asp:TextBox>
                <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_DaintyDate3' , 'expiry=true,elapse=-150,restrict=true,close=true')" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="DaintyDate3" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td>Commodity</td>
            <td>
                <asp:DropDownList ID="ddlcommodtiy" runat="server" Width="114px" AutoPostBack="true" Height="24px" OnSelectedIndexChanged="ddlcommodtiy_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
        </tr>




        <tr>
            <td>Receiving Godown</td>
            <td colspan="3">
                <asp:DropDownList ID="ddlgodown" runat="server" Height="21px" Width="607px" AutoPostBack="True" OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>




        <tr>
            <td>Issue Id / TC Number</td>
            <td colspan="3">
                <asp:DropDownList ID="ddlissueId" runat="server" Height="21px" Width="607px" AutoPostBack="True" OnSelectedIndexChanged="ddlissueId_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:Label ID="lblSocId" runat="server"
                    Visible="False"></asp:Label>
            </td>
        </tr>

        <tr>
            <td colspan="4" style="text-align: center; background-color: #CC9900; font-size: small; font-weight: 700;"></td>
        </tr>


        <tr style="font-size: small">
            <td>समिति का नाम</td>
            <td colspan="3">
                <asp:TextBox ID="txtSocName" runat="server" Height="21px" ReadOnly="True" Enabled="False" Width="603px"></asp:TextBox>
            </td>
        </tr>




        <tr style="font-size: small">
            <td>ट्रक नंबर</td>
            <td>
                <asp:TextBox ID="TxtTruckNumber" runat="server" Width="110px" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="TxtTruckNumber" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
            <td>टी सी नंबर</td>
            <td>
                <asp:TextBox ID="txtTcNumber" runat="server" Width="110px" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ControlToValidate="txtTcNumber" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>




        <tr style="font-size: small">
            <td>भेजे गए बोरे</td>
            <td>
                <asp:TextBox ID="txtsendbags" runat="server" Width="110px" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="txtsendbags" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
            <td>प्राप्त बोरे</td>
            <td>
                <asp:TextBox ID="txtrecbags" runat="server" Width="110px" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ControlToValidate="txtrecbags" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>




        <tr style="font-size: small">
            <td>भेजी गयी मात्रा</td>
            <td>
                <asp:TextBox ID="txtsendQty" runat="server" Width="110px" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="txtsendQty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
            <td>प्राप्त मात्रा</td>
            <td>
                <asp:TextBox ID="txtRecdQty" runat="server" Width="110px" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="txtRecdQty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>




        <tr style="font-size: small">
            <td>बोरो में अंतर</td>
            <td>
                <asp:TextBox ID="txtdiffBags" runat="server" Width="110px" AutoComplete="off" class="Number" MaxLength="5" Style="text-align: right" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="txtdiffBags" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
            <td>मात्रा में अंतर</td>
            <td>

                <asp:TextBox ID="txtqtyDiff" runat="server" Width="110px" Style="text-align: right" MaxLength="13" AutoComplete="off"
                    onblur="extractNumber(this,5,true);"
                    onkeyup="extractNumber(this,5,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ErrorMessage="NAN" ControlToValidate="txtqtyDiff" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,5})?))$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtqtyDiff" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td colspan="4" style="text-align: center; background-color: #CC9900; font-size: small; font-weight: 700;"></td>
        </tr>


        <tr style="font-size: small">
            <td>स्कंध एफ०ए०क्यू० नहीं है?</td>
            <td>
                <asp:CheckBox ID="chk_faq" runat="server" />

            </td>
            <td>कितने प्रतिशत एफ०ए०क्यू० नहीं है?</td>
            <td>
                <asp:TextBox ID="txt_faq_per" runat="server" MaxLength="5" Enabled="False" Width="110px" Style="text-align: right" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" onfocus="this.select();" onmouseup="return false;">0</asp:TextBox>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="NAN" ControlToValidate="txt_faq_per" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,5})?))$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ControlToValidate="txt_faq_per" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>



        <tr style="font-size: small">
            <td>बाह्य पदार्थ अधिक है?</td>
            <td>
                <asp:CheckBox ID="chk_extra" runat="server" />
            </td>
            <td>कितने प्रतिशत बाह्य पदार्थ अधिक है?</td>
            <td>
                <asp:TextBox ID="txt_extra_per" runat="server" MaxLength="5" Enabled="False" Width="110px" Style="text-align: right" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" onfocus="this.select();" onmouseup="return false;">0</asp:TextBox>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="NAN" ControlToValidate="txt_extra_per" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,5})?))$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ControlToValidate="txt_extra_per" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>



        <tr style="font-size: small">
            <td>दाने क्षतिग्रस्त है?</td>
            <td>
                <asp:CheckBox ID="chk_damaged" runat="server" />
            </td>
            <td>कितने प्रतिशत दाने क्षतिग्रस्त है?</td>
            <td>
                <asp:TextBox ID="txt_damage_per" runat="server" MaxLength="5" Enabled="False" Width="110px" Style="text-align: right" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" onfocus="this.select();" onmouseup="return false;">0</asp:TextBox>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="NAN" ControlToValidate="txt_damage_per" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,5})?))$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" ControlToValidate="txt_damage_per" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>



        <tr style="font-size: small">
            <td>स्कंध चमक विहीन है?</td>
            <td>
                <asp:CheckBox ID="chk_brightness" runat="server" />
            </td>
            <td>कितने प्रतिशत स्कंध चमक विहीन है?</td>
            <td>
                <asp:TextBox ID="txt_bright_per" runat="server" MaxLength="5" Enabled="False" Width="110px" Style="text-align: right" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" onfocus="this.select();" onmouseup="return false;">0</asp:TextBox>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="NAN" ControlToValidate="txt_bright_per" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,5})?))$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" ControlToValidate="txt_bright_per" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>



        <tr style="font-size: small">
            <td>आंशिक क्षतिग्रस्त है?</td>
            <td>
                <asp:CheckBox ID="chk_partially" runat="server" />
            </td>
            <td>कितने प्रतिशत आंशिक क्षतिग्रस्त है?</td>
            <td>
                <asp:TextBox ID="txt_partial_per" runat="server" MaxLength="5" Enabled="False" Width="110px" Style="text-align: right" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" onfocus="this.select();" onmouseup="return false;">0</asp:TextBox>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="NAN" ControlToValidate="txt_partial_per" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,5})?))$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server" ControlToValidate="txt_partial_per" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>



        <tr style="font-size: small">
            <td>टूटन व् सिकुड़े दाने है?</td>
            <td>
                <asp:CheckBox ID="chk_splited" runat="server" />
            </td>
            <td>कितने प्रतिशत टूटन व् सिकुड़े दाने है?</td>
            <td>
                <asp:TextBox ID="txt_split_per" runat="server" MaxLength="5" Enabled="False" Width="110px" Style="text-align: right" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" onfocus="this.select();" onmouseup="return false;">0</asp:TextBox>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="NAN" ControlToValidate="txt_split_per" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,5})?))$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server" ControlToValidate="txt_split_per" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>



        <tr style="font-size: small">
            <td>नमी का प्रतिशत अधिक है?</td>
            <td>
                <asp:CheckBox ID="chk_moist" runat="server" />
            </td>
            <td>कितने प्रतिशत नमी अधिक है?</td>
            <td>
                <asp:TextBox ID="txt_moist_per" runat="server" MaxLength="5" Enabled="False" Width="110px" Style="text-align: right" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" onfocus="this.select();" onmouseup="return false;">0</asp:TextBox>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="NAN" ControlToValidate="txt_moist_per" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,5})?))$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator44" runat="server" ControlToValidate="txt_moist_per" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>



        <tr style="font-size: small">
            <td>अन्य कारण</td>
            <td colspan="3">
                <asp:TextBox ID="txtreason" runat="server" TextMode="MultiLine" Width="601px" MaxLength="90" Class="alphaOnly"></asp:TextBox>
            </td>
        </tr>



        <tr>
            <td colspan="4" style="background-color: #CCFF99;"></td>
        </tr>

        <tr>
            <td colspan="6" style="text-align: center; font-size: large;">
                <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />

                <asp:Button ID="btnRecptSubmit" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Submit" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" OnClientClick="return validate();" OnClick="btnRecptSubmit_Click" />

                <asp:Button ID="btnPrint" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Print" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" Enabled="False" CausesValidation="False" OnClick="btnPrint_Click1" />

                <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" OnClick="btnRecptClose_Click" />
            </td>
        </tr>

        <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/Alphabets.js"></script>
        <script lang="javascript" src="../PaddyMilling/Scripts/Number.js" type="text/javascript">       </script>
    </table>
</asp:Content>
