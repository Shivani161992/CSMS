<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="PM_RemainingCMR_EntryReceipt.aspx.cs" Inherits="IssueCenter_PM_RemainingCMR_EntryReceipt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

        <table runat="server" style="width: 1000px; background-color: #FFFACD;" border="1">
            <tr>
                <td colspan="4" style="background-color: #556B2F; color: #F0E68C; font-size: 20px;">
                    <strong>Paddy Milling Remaining CMR Receipt  </strong>
                </td>
            </tr>
            <tr runat="server" id="trAftSub" visible="false">
                <td colspan="4">
                    <asp:Label ID="Label2" runat="server" Style="text-align: center; font-weight: 700;" Font-Size="X-Large" Font-Bold="true" ForeColor="Black" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4"></td>
            </tr>
            <tr>
                <td style="width: 200px; color: #556B2F; text-align: left; background-color: #FFFACD; font-size: large;">
                    <strong>Agreement District</strong>
                </td>
                <td style="width: 300px; color: #556B2F; text-align: left; background-color: #FFFAF0; font-size: medium;">
                    <asp:DropDownList ID="ddlAgreeDist" runat="server" Width="300px" CssClass="tddl" Visible="false" AutoPostBack="True" OnSelectedIndexChanged="ddlDelidist_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td style="width: 200px; color: #556B2F; text-align: left; background-color: #FFFAF0; font-size: large;">
                    <strong>Crop Year</strong>
                </td>
                <td style="width: 300px; color: #556B2F; text-align: left; background-color: #FFFAF0; font-size: medium;">
                    <asp:TextBox ID="txtCropYear" CssClass="tcolumn" Width="300px" Enabled="false" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 200px; color: #556B2F; text-align: left; background-color: #FFFACD; font-size: large;">
                    <strong>Miller Name</strong>
                </td>
                <td colspan="3" style="width: 300px; color: #556B2F; text-align: left; background-color: #FFFAF0; font-size: medium;">
                    <asp:DropDownList ID="ddlMillName" runat="server" Width="600px" CssClass="tddl" Visible="false" AutoPostBack="True" OnSelectedIndexChanged="ddlDelidist_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>

            </tr>
            <tr>
                <td colspan="4"></td>
            </tr>
            <tr>
                <td style="width: 200px; color: #556B2F; text-align: left; background-color: #FFFACD; font-size: large;">
                    <strong>Agreement Number</strong>
                </td>
                <td style="width: 300px; color: #556B2F; text-align: left; background-color: #FFFAF0; font-size: medium;">
                    <asp:DropDownList ID="ddlAgreeNo" runat="server" Width="300px" CssClass="tddl" Visible="false" AutoPostBack="True" OnSelectedIndexChanged="ddlDelidist_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td style="width: 200px; color: #556B2F; text-align: left; background-color: #FFFAF0; font-size: medium;">
                    <strong>CMR DO Number</strong>
                </td>
                <td style="width: 300px; color: #556B2F; text-align: left; background-color: #FFFAF0; font-size: medium;">
                    <asp:DropDownList ID="ddlCMRDO" runat="server" Width="300px" CssClass="tddl" Visible="false" AutoPostBack="True" OnSelectedIndexChanged="ddlDelidist_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 200px; color: #556B2F; text-align: left; background-color: #FFFACD; font-size: large;">
                    <strong>Rem CMR Quantity(Qtls)</strong>
                </td>
                <td colspan="2" style="width: 200px; color: #556B2F; text-align: left; background-color: #FFFACD; font-size: large;">
                    <asp:TextBox ID="txtRemainingCMR" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>
                    <asp:Button ID="bttAdd" runat="server" Text="Add" OnClick="bttAdd_Click" />

                </td>
            </tr>
            <tr runat="server" id="trGrid" visible="false">
                <td colspan="4" style="width: 1000px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False" ShowFooter="True" CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                        OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField ReadOnly="true" HeaderText="S. No." />
                            <asp:BoundField DataField="fromdisttext" HeaderText="Miller Name" />
                            <asp:BoundField DataField="fromdistval" HeaderText="Agreement Number" />
                            <asp:BoundField DataField="todisttext" HeaderText="CMR DO Number" />
                            <asp:BoundField DataField="todistval" HeaderText="Remaining Quantity(Qtls)" />
                            <asp:TemplateField HeaderText="Remove">
                                <ItemTemplate>
                                    <asp:LinkButton ID="iddelete" runat="server" CausesValidation="false" CommandName="Delete" OnClientClick="return confirm('Are You Sure You Want To Remove?');" Text="Remove"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#556B2F" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#556B2F" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    </asp:GridView>

                </td>
            </tr>
            <tr>
                <td colspan="4"></td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: center; font-size: large; text-decoration: underline; color: #F0E68C; border-style: none; background-color: #800000;"><b style="color: #FFFF66">Receipt Details</b></td>
            </tr>
            <tr>
                <td style="width: 200px; color: #556B2F; text-align: left; background-color: #FFFACD; font-size: large;">
                    <strong>Recd. CMR Quantity(Qtls)</strong>
                </td>
                <td style="width: 300px; color: #556B2F; text-align: left; background-color: #FFFAF0; font-size: medium;">
                    <asp:TextBox ID="txtCmrQuanitity" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>
                </td>
                <td style="width: 200px; color: #556B2F; text-align: left; background-color: #FFFACD; font-size: large;">
                    <strong>Recd. No. of Bags</strong>
                </td>
                <td style="width: 300px; color: #556B2F; text-align: left; background-color: #FFFAF0; font-size: medium;">
                    <asp:TextBox ID="txtNoOfBags" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan="2" style="width: 200px; color: #556B2F; text-align: left; background-color: #FFFACD; font-size: large;">
                    <strong>Types of Bags</strong>
                </td>
                <td style="width: 200px; color: #8B0000; text-align: left; background-color: #FFFACD; font-size: medium;">
                    <asp:RadioButton ID="rdbNewJute" runat="server" GroupName="Bags" Text="New Jute(SBT)" />
                    <br />
                </td>
                <td style="width: 200px; color: #8B0000; text-align: left; background-color: #FFFACD; font-size: large;">
                    <asp:RadioButton ID="rdbOldJute" runat="server" GroupName="Bags" Text="Old Jute(SBT)" Style="margin-left: 8px;" />
                </td>
                <td style="width: 200px; color: #8B0000; text-align: left; background-color: #FFFACD; font-size: large;">
                    <asp:RadioButton ID="rdbOnceJute" runat="server" GroupName="Bags" Text="Once Used Jute(SBT)" Style="margin-left: 8px;" />
                </td>
            </tr>
            <tr>
                <td style="width: 200px; color: #8B0000; text-align: left; background-color: #FFFACD; font-size: medium;">

                    <asp:RadioButton ID="rdbNewPP" runat="server" GroupName="Bags" Text="New PP(HDPE)" />
                </td>
                <td style="width: 200px; color: #8B0000; text-align: left; background-color: #FFFACD; font-size: large;">
                    <asp:RadioButton ID="rdbOldPP" runat="server" GroupName="Bags" Text="Old PP(HDPE)" Style="margin-left: 4px;" />
                </td>
                <td style="width: 200px; color: #8B0000; text-align: left; background-color: #FFFACD; font-size: large;">
                    <asp:RadioButton ID="rdbOncePP" runat="server" GroupName="Bags" Text="Once Used PP(HDPE)" Style="margin-left: 4px;" />
                </td>
            </tr>
            <tr>
                <td style="width: 200px; color: #556B2F; text-align: left; background-color: #FFFACD; font-size: large;">
                    <strong>Miller Tags</strong>
                </td>
                <td style="width: 300px; color: #8B0000; text-align: left; background-color: #FFFAF0; font-size: medium;">
                    <asp:RadioButton ID="rdbTagYes" runat="server" GroupName="Tag" Text="Yes" AutoPostBack="True" OnCheckedChanged="rdbTagYes_CheckedChanged" />
                    <asp:RadioButton ID="rdbTagNo" runat="server" GroupName="Tag" Text="No" Style="margin-left: 10px;" AutoPostBack="True" OnCheckedChanged="rdbTagNo_CheckedChanged" />

                </td>
                <td style="width: 200px; color: #556B2F; text-align: left; background-color: #FFFACD; font-size: large;">
                    <strong>Number of Tags</strong>
                </td>
                <td style="width: 200px; color: #556B2F; text-align: left; background-color: #FFFACD; font-size: large;">
                    <asp:TextBox ID="txtTagNo" runat="server" AutoComplete="off" class="Number" MaxLength="3" onfocus="this.select();" onmouseup="return false;" Style="text-align: right" Width="137px"></asp:TextBox>

                </td>

            </tr>
            <tr>
                <td colspan="4" style="background-color: #FFFACD; color: #800000;">यदि चावल दो ट्रकों में आया है, तो कृपया दोनों ट्रकों का नंबर लिखें|</td>
                </tr>

                <tr>
                    <td style="width: 200px; color: #556B2F; text-align: left; background-color: #FFFACD; font-size: large;">
                        <strong>Truck Number</strong></td>
                    <td style="width: 200px; color: #556B2F; text-align: left; background-color: #FFFACD; font-size: large;">
                        <asp:TextBox ID="txtTruckNo" runat="server" AutoComplete="off" Class="TruckNumber" MaxLength="14" onfocus="this.select();" onmouseup="return false;" Width="137px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtTruckNo" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 200px; color: #556B2F; text-align: left; background-color: #FFFACD; font-size: large;">
                        <strong>Truck Number1</strong></td>
                    <td style="width: 200px; color: #556B2F; text-align: left; background-color: #FFFACD; font-size: large;">
                        <asp:TextBox ID="txtTruckNo0" runat="server" AutoComplete="off" Class="TruckNumber" MaxLength="14" onfocus="this.select();" onmouseup="return false;" Width="137px"></asp:TextBox>
                    </td>
                </tr>
                <script lang="javascript" src="../PaddyMilling/Scripts/TruckNo.js" type="text/javascript">      </script>

                <script lang="javascript" src="../PaddyMilling/Scripts/Number.js" type="text/javascript">

                </script>
                <tr>
                    <td style="width: 200px; color: #556B2F; text-align: left; background-color: #FFFACD; font-size: large;">
                        <strong>Recd. CMR Date </strong></td>
                    <td style="width: 200px; color: #556B2F; text-align: left; background-color: #FFFACD; font-size: large;">
                        <asp:TextBox ID="txtDate" runat="server" ReadOnly="True" Width="137px"></asp:TextBox>
                        <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtDate' , 'expiry=true,elapse=-150,restrict=true,close=true' )" /><asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtDate" ErrorMessage="RequiredFieldValidator" Font-Bold="False" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 200px; color: #556B2F; text-align: left; background-color: #FFFACD; font-size: large;"><strong>Toul Parchi No.</strong></td>
                    <td style="width: 200px; color: #556B2F; text-align: left; background-color: #FFFACD; font-size: large;">
                        <asp:TextBox ID="txtToulReceiptNo" runat="server" Width="137px" AutoComplete="off" Class="TruckNumber" MaxLength="14" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtToulReceiptNo" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                
                        </td></tr>
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
                </b>
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
             </b>
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
             </b>
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
                </b>
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
                </b>
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

