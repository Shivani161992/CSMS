<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Paddy_MPSCSC.master" AutoEventWireup="true" CodeFile="PaddyMillingCropYear.aspx.cs" Inherits="PaddyMilling_PaddyMillingCropYear" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <table align="center" class="auto-styleM5" backcolor="White" bordercolor="Black"
        borderwidth="1" forecolor="Black" gridlines="Both" borderstyle="Solid" style="width: 666px">
        <tr>
            <td>

                <style type="text/css">
                    .auto-styleNSC {
                        width: 300px;
                    }

                    .auto-style4 {
                        text-decoration: underline;
                    }

                    .ButtonClass {
                        cursor: pointer;
                    }


                    .auto-style7 {
                        width: 306px;
                    }

                    .auto-style8 {
                        height: 26px;
                        width: 288px;
                    }

                    .auto-style9 {
                        width: 306px;
                        height: 26px;
                    }

                    .auto-style10 {
                        text-decoration: underline;
                        font-weight: bold;
                        text-align: center;
                    }

                    .auto-style11 {
                        width: 100%;
                        border-style: solid;
                        border-width: 1px;
                    }

                    .auto-style12 {
                        height: 33px;
                    }
                    .auto-style13 {
                        width: 288px;
                    }
                    .auto-style14 {
                        width: 288px;
                        font-size: small;
                    }
                </style>

                <%--Main JQuery File--%>
                <script type="text/javascript" lang="javascript" src="Scripts/jquery-2.1.1.js"></script>

                  <script type="text/javascript" lang="javascript" src="Scripts/Number2D.js"></script>

                <%--Don't Allow Drop & Paste class="DragDrop"--%>
                <script type="text/javascript" lang="javascript" src="Scripts/DropPaste.js"></script>



                <table align="center" width="100%">
                    <tr>
                        <td style="text-align: center; font-weight: 700; font-size: x-large; text-decoration: underline">Paddy Milling Rs.</td>
                    </tr>
                </table>
                </br>


                <table align="center" class="DragDrop" width="100%" style="text-align: left">
                    <tr>
                        <td class="auto-style8">Paddy Milling CropYear</td>
                        <td align="left" class="auto-style9">
                            <asp:DropDownList ID="ddlCropYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCropYear_SelectedIndexChanged" Width="173px">
                            </asp:DropDownList>
                        </td>

                    </tr>
                    <tr>
                        <td class="auto-style13">अमानत राशि</td>
                        <td align="left" class="auto-style7">
                            <asp:TextBox ID="txtDepositMoney" runat="server" MaxLength="7" Style="text-align: right" AutoComplete="off"
                                onblur="extractNumber(this,2,true);"
                                onkeyup="extractNumber(this,2,true);"
                                onkeypress="return blockNonNumbers(this, event, true, true);"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Number" ControlToValidate="txtDepositMoney" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>

                            <b>&nbsp;<asp:RequiredFieldValidator ID="rfvArva0" runat="server" ControlToValidate="txtDepositMoney" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                                Rs.</b></td>

                    </tr>
                    <tr>
                        <td class="auto-style13">पेनाल्टी (Per Qtls./Day)</td>
                        <td align="left" class="auto-style7">
                            <asp:TextBox ID="txtPanility" runat="server" MaxLength="5" Style="text-align: right" AutoComplete="off"
                                onblur="extractNumber(this,2,true);"
                                onkeyup="extractNumber(this,2,true);"
                                onkeypress="return blockNonNumbers(this, event, true, true);"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Number" ControlToValidate="txtPanility" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>

                            <b>&nbsp;<asp:RequiredFieldValidator ID="rfvPanility" runat="server" ControlToValidate="txtPanility" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                                Rs.</b></td>

                    </tr>
                    <tr>
                        <td class="auto-style13">अरवा कस्टम मिलिंग (प्रोत्साहन राशि)</td>
                        <td align="left" class="auto-style7">
                            <asp:TextBox ID="txtArva" runat="server" MaxLength="5" Style="text-align: right" AutoComplete="off"
                                onblur="extractNumber(this,2,true);"
                                onkeyup="extractNumber(this,2,true);"
                                onkeypress="return blockNonNumbers(this, event, true, true);"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Invalid Number" ControlToValidate="txtArva" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                            <b>&nbsp;<asp:RequiredFieldValidator ID="rfvArva" runat="server" ControlToValidate="txtArva" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                                Rs./Qtls</b></td>

                    </tr>
                    <tr>
                        <td class="auto-style13">लॉट की सिक्यूरिटी राशि</td>
                        <td align="left" class="auto-style7">
                            <asp:DropDownList ID="ddlLot" runat="server" AutoPostBack="false"  Width="173px">
                            </asp:DropDownList>
                        </td>

                    </tr>
                    <tr>
                        <td class="auto-style13">&nbsp;</td>
                        <td align="left" class="auto-style7">
                            &nbsp;</td>

                    </tr>
                    <tr style="text-align: center; font-weight: 700;">
                        <td colspan="2" class="auto-style4">उष्णा कस्टम मिलिंग (प्रोत्साहन राशि)</td>

                    </tr>

                    <tr>
                        <td class="auto-style13">प्रथम 3 माह</td>
                        <td align="left" class="auto-style7">
                            <asp:TextBox ID="txtUshnaFirst3" runat="server"  MaxLength="5" Style="text-align: right" AutoComplete="off"
                                onblur="extractNumber(this,2,true);"
                                onkeyup="extractNumber(this,2,true);"
                                onkeypress="return blockNonNumbers(this, event, true, true);"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Invalid Number" ControlToValidate="txtUshnaFirst3" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                            <b>&nbsp;<asp:RequiredFieldValidator ID="rfvUshnaFirst3" runat="server" ControlToValidate="txtUshnaFirst3" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                                Rs./Qtls</b></td>

                    </tr>

                    <tr>
                        <td class="auto-style13">3 माह उपरांत</td>
                        <td align="left" class="auto-style7">
                            <asp:TextBox ID="txtUshnaAfter3" runat="server"  MaxLength="5" Style="text-align: right" AutoComplete="off"
                                onblur="extractNumber(this,2,true);"
                                onkeyup="extractNumber(this,2,true);"
                                onkeypress="return blockNonNumbers(this, event, true, true);"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Invalid Number" ControlToValidate="txtUshnaAfter3" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                            <b>&nbsp;<asp:RequiredFieldValidator ID="rfvUshnaAfter3" runat="server" ControlToValidate="txtUshnaAfter3" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                                Rs./Qtls</b></td>

                    </tr>

                    <tr>
                        <td class="auto-style13">&nbsp;</td>
                        <td align="left" class="auto-style7">&nbsp;</td>

                    </tr>

                    <tr>
                        <td colspan="2" class="auto-style10">प्रदायित धान की मात्रा के विरूद्ध</td>

                    </tr>

                    <tr>
                        <td class="auto-style13">अरवा चावल की दर</td>
                        <td align="left" class="auto-style7">
                            <asp:TextBox ID="txtArvaChawal" runat="server" MaxLength="5" Style="text-align: right" AutoComplete="off"
                                onblur="extractNumber(this,2,true);"
                                onkeyup="extractNumber(this,2,true);"
                                onkeypress="return blockNonNumbers(this, event, true, true);"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Invalid Number" ControlToValidate="txtArvaChawal" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                            <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtArvaChawal" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                                %</b></td>

                    </tr>

                    <tr>
                        <td class="auto-style13">उष्णा चावल की दर</td>
                        <td align="left" class="auto-style7">
                            <asp:TextBox ID="txtUshnaChawal" runat="server" MaxLength="5" Style="text-align: right" AutoComplete="off"
                                onblur="extractNumber(this,2,true);"
                                onkeyup="extractNumber(this,2,true);"
                                onkeypress="return blockNonNumbers(this, event, true, true);"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Invalid Number" ControlToValidate="txtUshnaChawal" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                            <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUshnaChawal" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                                %</b></td>

                    </tr>

                    <tr>
                        <td class="auto-style14">अनुबंध के विरुद्ध प्राप्त चावल की मात्रा का प्रतिशत</td>
                        <td align="left" class="auto-style7">
                            <asp:DropDownList ID="ddlReturnCMR" runat="server" AutoPostBack="false"  Width="173px">
                            </asp:DropDownList>
                        &nbsp;%</td>

                    </tr>

                    <tr>
                        <td class="auto-style13">&nbsp;</td>
                        <td align="left" class="auto-style7">&nbsp;</td>

                    </tr>

                    <tr>
                        <td colspan="2" class="auto-style10">धान की कीमत (Per Qtls.)</td>

                    </tr>

                    <tr>
                        <td class="auto-style13">कामन धान</td>
                        <td align="left" class="auto-style7">
                            <asp:TextBox ID="txtCommonDhanRs" runat="server"  MaxLength="10" Style="text-align: right" AutoComplete="off"
                                onblur="extractNumber(this,2,true);"
                                onkeyup="extractNumber(this,2,true);"
                                onkeypress="return blockNonNumbers(this, event, true, true);"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ErrorMessage="Invalid Number" ControlToValidate="txtCommonDhanRs" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                            <b>&nbsp;<asp:RequiredFieldValidator ID="rfvUshnaFirst4" runat="server" ControlToValidate="txtCommonDhanRs" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                                Rs.</b></td>

                    </tr>

                    <tr>
                        <td class="auto-style13">ग्रेड-ए धान</td>
                        <td align="left" class="auto-style7">
                            <asp:TextBox ID="txtGradeADhanRs" runat="server"  MaxLength="10" Style="text-align: right" AutoComplete="off"
                                onblur="extractNumber(this,2,true);"
                                onkeyup="extractNumber(this,2,true);"
                                onkeypress="return blockNonNumbers(this, event, true, true);"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ErrorMessage="Invalid Number" ControlToValidate="txtGradeADhanRs" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                            <b>&nbsp;<asp:RequiredFieldValidator ID="rfvUshnaFirst5" runat="server" ControlToValidate="txtGradeADhanRs" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                                Rs.</b></td>

                    </tr>
                </table>
                <br />
                <table align="center" class="auto-style11" style="text-align: center" border="1" cellspacing="0">
                    <tr style="text-align: center">
                        <td colspan="4"><b>सीएमआर (चावल) की गुणवत्ता परिक्षण</b></td>
                    </tr>
                    <tr>
                        <td>क्रम संख्या</td>
                        <td>अपवर्तन</td>
                        <td>अधिकतम सीमा (प्रतिशत)<br />
                            <b>ग्रेड-&#39;ए&#39;</b></td>
                        <td>अधिकतम सीमा (प्रतिशत)<br />
                            <b>साधारण</b></td>
                    </tr>
                    <tr>
                        <td rowspan="2">1.</td>
                        <td style="text-align: left">टोटा</td>
                        <td>
                            <asp:TextBox ID="TxtTotaGA" runat="server" Width="50px"  MaxLength="5" Style="text-align: right" AutoComplete="off"
                                onblur="extractNumber(this,2,true);"
                                onkeyup="extractNumber(this,2,true);"
                                onkeypress="return blockNonNumbers(this, event, true, true);"></asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ErrorMessage="Invalid Number" ControlToValidate="TxtTotaGA" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                            <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtTotaGA" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

                        </td>
                        <td>
                            <asp:TextBox ID="TxtTotaS" runat="server" Width="50px"  MaxLength="5" Style="text-align: right" AutoComplete="off"
                                onblur="extractNumber(this,2,true);"
                                onkeyup="extractNumber(this,2,true);"
                                onkeypress="return blockNonNumbers(this, event, true, true);"></asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ErrorMessage="Invalid Number" ControlToValidate="TxtTotaS" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                            <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtTotaS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: left">छोटे टोटे</td>
                        <td>
                            <asp:TextBox ID="TxtChoteToteGA" runat="server"  MaxLength="5" Style="text-align: right" Width="50px" AutoComplete="off"
                                onblur="extractNumber(this,2,true);"
                                onkeyup="extractNumber(this,2,true);"
                                onkeypress="return blockNonNumbers(this, event, true, true);"></asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ErrorMessage="Invalid Number" ControlToValidate="TxtChoteToteGA" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                            <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TxtChoteToteGA" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="TxtChoteToteS" runat="server"  MaxLength="5" Style="text-align: right" Width="50px" AutoComplete="off"
                                onblur="extractNumber(this,2,true);"
                                onkeyup="extractNumber(this,2,true);"
                                onkeypress="return blockNonNumbers(this, event, true, true);"></asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ErrorMessage="Invalid Number" ControlToValidate="TxtChoteToteS" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                            <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TxtChoteToteS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                        </td>

                    </tr>
                    <tr>
                        <td>2.</td>
                        <td style="text-align: left">विजातीय तत्व</td>
                        <td>
                            <asp:TextBox ID="txtVijatiyeGA" runat="server"  MaxLength="5" Style="text-align: right" Width="50px" AutoComplete="off"
                                onblur="extractNumber(this,2,true);"
                                onkeyup="extractNumber(this,2,true);"
                                onkeypress="return blockNonNumbers(this, event, true, true);"></asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ErrorMessage="Invalid Number" ControlToValidate="txtVijatiyeGA" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                            <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtVijatiyeGA" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtVijatiyeS" runat="server" Width="50px"  MaxLength="5" Style="text-align: right" AutoComplete="off"
                                onblur="extractNumber(this,2,true);"
                                onkeyup="extractNumber(this,2,true);"
                                onkeypress="return blockNonNumbers(this, event, true, true);"></asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" ErrorMessage="Invalid Number" ControlToValidate="txtVijatiyeS" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                            <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtVijatiyeS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                        </td>

                    </tr>
                    <tr>
                        <td>3.</td>
                        <td style="text-align: left">क्षतिग्रस्त दाने (C) /
                            <br />
                            मामूली क्षतिग्रस्त दाने</td>
                        <td>
                            <asp:TextBox ID="txtDamageDaaneGA" runat="server" Width="50px"  MaxLength="5" Style="text-align: right" AutoComplete="off"
                                onblur="extractNumber(this,2,true);"
                                onkeyup="extractNumber(this,2,true);"
                                onkeypress="return blockNonNumbers(this, event, true, true);"></asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server" ErrorMessage="Invalid Number" ControlToValidate="txtDamageDaaneGA" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                            <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDamageDaaneGA" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDamageDaaneS" runat="server" Width="50px"  MaxLength="5" Style="text-align: right" AutoComplete="off"
                                onblur="extractNumber(this,2,true);"
                                onkeyup="extractNumber(this,2,true);"
                                onkeypress="return blockNonNumbers(this, event, true, true);"></asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server" ErrorMessage="Invalid Number" ControlToValidate="txtDamageDaaneS" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                            <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtDamageDaaneS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                        </td>

                    </tr>
                    <tr>
                        <td class="auto-style12">4.</td>
                        <td style="text-align: left" class="auto-style12">बदरंग दाने</td>
                        <td class="auto-style12">
                            <asp:TextBox ID="txtBadrangDaaneGA" runat="server" Width="50px"  MaxLength="5" Style="text-align: right" AutoComplete="off"
                                onblur="extractNumber(this,2,true);"
                                onkeyup="extractNumber(this,2,true);"
                                onkeypress="return blockNonNumbers(this, event, true, true);"></asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server" ErrorMessage="Invalid Number" ControlToValidate="txtBadrangDaaneGA" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                            <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtBadrangDaaneGA" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                        </td>
                        <td class="auto-style12">
                            <asp:TextBox ID="txtBadrangDaaneS" runat="server" Width="50px"  MaxLength="5" Style="text-align: right" AutoComplete="off"
                                onblur="extractNumber(this,2,true);"
                                onkeyup="extractNumber(this,2,true);"
                                onkeypress="return blockNonNumbers(this, event, true, true);"></asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator19" runat="server" ErrorMessage="Invalid Number" ControlToValidate="txtBadrangDaaneS" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                            <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtBadrangDaaneS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                        </td>

                    </tr>
                    <tr>
                        <td>5.</td>
                        <td style="text-align: left">चाकी दाने</td>
                        <td>
                            <asp:TextBox ID="txtChaakiDaaneGA" runat="server" Width="50px"  MaxLength="5" Style="text-align: right" AutoComplete="off"
                                onblur="extractNumber(this,2,true);"
                                onkeyup="extractNumber(this,2,true);"
                                onkeypress="return blockNonNumbers(this, event, true, true);"></asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator20" runat="server" ErrorMessage="Invalid Number" ControlToValidate="txtChaakiDaaneGA" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                            <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtChaakiDaaneGA" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtChaakiDaaneS" runat="server" Width="50px"  MaxLength="5" Style="text-align: right" AutoComplete="off"
                                onblur="extractNumber(this,2,true);"
                                onkeyup="extractNumber(this,2,true);"
                                onkeypress="return blockNonNumbers(this, event, true, true);"></asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator21" runat="server" ErrorMessage="Invalid Number" ControlToValidate="txtChaakiDaaneS" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                            <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtChaakiDaaneS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                        </td>

                    </tr>
                    <tr>
                        <td>6.</td>
                        <td style="text-align: left">लाल दाने</td>
                        <td>
                            <asp:TextBox ID="txtLaalDaaneGA" runat="server" Width="50px"  MaxLength="5" Style="text-align: right" AutoComplete="off"
                                onblur="extractNumber(this,2,true);"
                                onkeyup="extractNumber(this,2,true);"
                                onkeypress="return blockNonNumbers(this, event, true, true);"></asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator22" runat="server" ErrorMessage="Invalid Number" ControlToValidate="txtLaalDaaneGA" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                            <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtLaalDaaneGA" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLaalDaaneS" runat="server" Width="50px"  MaxLength="5" Style="text-align: right" AutoComplete="off"
                                onblur="extractNumber(this,2,true);"
                                onkeyup="extractNumber(this,2,true);"
                                onkeypress="return blockNonNumbers(this, event, true, true);"></asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator23" runat="server" ErrorMessage="Invalid Number" ControlToValidate="txtLaalDaaneS" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                            <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtLaalDaaneS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                        </td>

                    </tr>
                    <tr>
                        <td>7.</td>
                        <td style="text-align: left">निम्न श्रेणी का सम्मिश्रण</td>
                        <td>
                            <asp:TextBox ID="txtOtherGA" runat="server" Width="50px"  MaxLength="5" Style="text-align: right" AutoComplete="off"
                                onblur="extractNumber(this,2,true);"
                                onkeyup="extractNumber(this,2,true);"
                                onkeypress="return blockNonNumbers(this, event, true, true);"></asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator24" runat="server" ErrorMessage="Invalid Number" ControlToValidate="txtOtherGA" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                            <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtOtherGA" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOtherS" runat="server" Width="50px"  MaxLength="5" Style="text-align: right" AutoComplete="off"
                                onblur="extractNumber(this,2,true);"
                                onkeyup="extractNumber(this,2,true);"
                                onkeypress="return blockNonNumbers(this, event, true, true);"></asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator25" runat="server" ErrorMessage="Invalid Number" ControlToValidate="txtOtherS" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                            <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtOtherS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                        </td>

                    </tr>
                    <tr>
                        <td>8.</td>
                        <td style="text-align: left">चोकर सहित दाने</td>
                        <td>
                            <asp:TextBox ID="txtChokarDaaneGA" runat="server" Width="50px"  MaxLength="5" Style="text-align: right" AutoComplete="off"
                                onblur="extractNumber(this,2,true);"
                                onkeyup="extractNumber(this,2,true);"
                                onkeypress="return blockNonNumbers(this, event, true, true);"></asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator26" runat="server" ErrorMessage="Invalid Number" ControlToValidate="txtChokarDaaneGA" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                            <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtChokarDaaneGA" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtChokarDaaneS" runat="server" Width="50px"  MaxLength="5" Style="text-align: right" AutoComplete="off"
                                onblur="extractNumber(this,2,true);"
                                onkeyup="extractNumber(this,2,true);"
                                onkeypress="return blockNonNumbers(this, event, true, true);"></asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator27" runat="server" ErrorMessage="Invalid Number" ControlToValidate="txtChokarDaaneS" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                            <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtChokarDaaneS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                        </td>

                    </tr>
                    <tr>
                        <td>9.</td>
                        <td style="text-align: left">नमी तत्व (R)</td>
                        <td>
                            <asp:TextBox ID="txtNamiGA" runat="server" Width="50px"  MaxLength="5" Style="text-align: right" AutoComplete="off"
                                onblur="extractNumber(this,2,true);"
                                onkeyup="extractNumber(this,2,true);"
                                onkeypress="return blockNonNumbers(this, event, true, true);"></asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator28" runat="server" ErrorMessage="Invalid Number" ControlToValidate="txtNamiGA" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                            <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtNamiGA" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNamiS" runat="server" Width="50px"  MaxLength="5" Style="text-align: right" AutoComplete="off"
                                onblur="extractNumber(this,2,true);"
                                onkeyup="extractNumber(this,2,true);"
                                onkeypress="return blockNonNumbers(this, event, true, true);"></asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator29" runat="server" ErrorMessage="Invalid Number" ControlToValidate="txtNamiS" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                            <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtNamiS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                        </td>

                    </tr>
                </table>
           <br />

                  <table align="center" class="auto-styleNSC">
                      <tr>

                          <td>
                              <asp:Button ID="btnSubmit" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Submit" Width="115px" CssClass="ButtonClass" OnClick="btnSubmit_Click" />
                          </td>

                          <td>
                              <asp:Button ID="Button4" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="Button4_Click" />
                          </td>
                      </tr>
                  </table>

            </td>
        </tr>

        <%--Allow Only Number & One Decimal using class="NumberDecimal" --%>
        <script type="text/javascript" lang="javascript" src="Scripts/NumberDecimal.js"></script>

    </table>
</asp:Content>

