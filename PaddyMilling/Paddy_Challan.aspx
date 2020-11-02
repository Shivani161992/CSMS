<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Paddy_Challan.aspx.cs" Inherits="PaddyMilling_Paddy_Challan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>

    <script type="text/javascript" lang="javascript" src="../js/calendarMvmt.js"></script>

    <%--For Calendar Controls--%>
    <link href="../PaddyMilling/calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="../js/calendarMvmt.js"></script>


    <%--Allow Only 2 Digit After Decimal--%>
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/Number2D.js"></script>

    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
        }

        .auto-style2 {
            font-size: small;
            font-weight: bold;
            color: #FF0000;
        }

        .auto-style4 {
            font-size: small;
            color: #FF0000;
        }

        .auto-style5 {
            font-size: small;
        }
    </style>

    <table align="center" style="width: 730px; border-style: solid; border-width: 1px; text-align: left; font-size: medium" border="1" cellspacing="0" cellpadding="2">
        <tr>
            <td colspan="6" style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600"><strong>Paddy Challan</strong>
                <input id="hdfAgrmtID" type="hidden" runat="server" />
                <input id="hdfMillCode" type="hidden" runat="server" />
                <input id="hdfMillingType" type="hidden" runat="server" />
                <input id="hdfDhanLot" type="hidden" runat="server" />
                <input id="hdfGodown" type="hidden" runat="server" />
            </td>
        </tr>

        <tr>
            <td rowspan="16">&nbsp;</td>
            <td colspan="4" style="text-align: center; background-color: #CCFF99">
                <asp:Label ID="Label2" runat="server" Style="text-align: center; font-weight: 700;" ForeColor="Blue" Visible="False"></asp:Label>
            </td>
            <td rowspan="16"></td>
        </tr>

        <tr>
            <td>Crop Year</td>
            <td>
                <asp:TextBox ID="txtYear" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox>

            </td>
            <td>Stock Issued From</td>
            <td>
                <asp:TextBox ID="SourceArival" runat="server" Width="137px" ReadOnly="True" Enabled="False">Procurement</asp:TextBox></td>
        </tr>

        <tr>
            <td colspan="4" style="text-align: center;background-color: #99FF66; font-size: small; font-weight: 700;">क्या आपके जिले की धान किसी अन्य जिले में रखी हुई है?
                <asp:CheckBox ID="chkChange" runat="server" AutoPostBack="True" OnCheckedChanged="chkChange_CheckedChanged" />
            </td>
        </tr>

        <tr id="trMsg" visible="false" runat="server" style="background-color: #99FF66;">
            <td style="font-weight: 700">District</td>
            <td>
                <asp:DropDownList ID="ddlFrmDist" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlFrmDist_SelectedIndexChanged">
                </asp:DropDownList>

            </td >
            <td style="font-weight: 700">Issue Centre</td>
            <td>
                <asp:DropDownList ID="ddlIssueCentre" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlIssueCentre_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
        </tr>

        <tr>
             <td>Type of Bags</td>
            <td>
               
                <%--<asp:RadioButton ID="rdbOldSBT" runat="server" GroupName="BagsGroup" Text="Old Jute(SBT)" CssClass="auto-style5" />
                    </center>

                <asp:RadioButton ID="rdbSBT" runat="server" GroupName="BagsGroup" Text="New Jute(SBT)" CssClass="auto-style5" />
                <asp:RadioButton ID="rdbHDPE" runat="server" GroupName="BagsGroup" Text="PP(HDPE)" Style="margin-left: 5px;" CssClass="auto-style5" />--%>

                     <asp:DropDownList ID="ddlbagstype" runat="server" Width="141px" OnSelectedIndexChanged="ddlbagstype_SelectedIndexChanged" AutoPostBack="true" >
                </asp:DropDownList>
            </td>
            <td>Branch Name</td>
            <td>
                <asp:DropDownList ID="ddlBranch" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
            
        </tr>
        <tr>
            <td>Godown Name</td>
            <td>
                <asp:DropDownList ID="ddlGodown" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
            <td>
                Stack Number
            </td>
            <td>
                <asp:DropDownList ID="ddlstackNumber" runat="server" Width="141px" >
                </asp:DropDownList>

            </td>
        </tr>

        <tr>
            <td>Bal. Qty In Godown</td>
            <td>
                <asp:TextBox ID="txtBalQtyInGodown" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox>

            </td>
            <td>Bal. Bags In Godown</td>
            <td>
                <asp:TextBox ID="txtBalBagInGodown" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox>

            </td>
        </tr>

        <tr>
            <td colspan="4" style="background-color: #CCFF99;"></td>
        </tr>

        <tr>
            <td>DO Number</td>
            <td>
                <asp:DropDownList ID="ddlDOnumber" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlDOnumber_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
            <td>Lot Number</td>
            <td>
                <asp:TextBox ID="txtLotNo" runat="server" Width="137px" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txtLotNo" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td>Mill Name</td>
            <td colspan="3">
                <asp:TextBox ID="txtMillName" runat="server" Width="495px" ReadOnly="True" Enabled="False"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtMillName" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td>DO Date</td>
            <td>
                <asp:TextBox ID="txtDODate" runat="server" Width="137px" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtDODate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
            <td>DO Last Date</td>
            <td>
                <asp:TextBox ID="txtDOLastDate" runat="server" Width="137px" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtDOLastDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>
        <tr>
            <td>Common Paddy<b><span class="auto-style4">(Qtls)</span></b></td>
            <td>
                <asp:TextBox ID="txtTotalCDhan" runat="server" class="NumberDecimal" MaxLength="10" Width="137px" AutoComplete="off" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>

                <span class="auto-style2">(Total)</span></td>
            <td>Grade-A Paddy<b><span class="auto-style4">(Qtls)</span></b></td>
            <td>
                <asp:TextBox ID="txtTotalGDhan" runat="server" class="NumberDecimal" MaxLength="10" Width="137px" AutoComplete="off" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>
                <span class="auto-style2">(Total)</span></td>
        </tr>

        <tr>
            <td>Common Paddy<b><span class="auto-style4">(Qtls)</span></b></td>
            <td>
                <asp:TextBox ID="txtRemCommonDhan" runat="server" class="NumberDecimal" MaxLength="10" Width="137px" AutoComplete="off" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>

                <span class="auto-style2">(Rem.)</span></td>
            <td>Grade-A Paddy<b><span class="auto-style4">(Qtls)</span></b></td>
            <td>
                <asp:TextBox ID="txtRemGradeADhan" runat="server" class="NumberDecimal" MaxLength="10" Width="137px" AutoComplete="off" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>
                <span class="auto-style2">(Rem.)</span></td>
        </tr>

        <tr>
            <td colspan="4" style="background-color: #CCFF99;"></td>
        </tr>

        <tr>
            <td>Issued Qty<b><span class="auto-style4"> (Qtls)</span></b></td>
            <td>
                <asp:TextBox ID="txtIssuedQty" runat="server" Width="137px" Style="text-align: right" MaxLength="8" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" onfocus="this.select();" onmouseup="return false;" AutoPostBack="true" OnTextChanged="txtIssuedQty_TextChanged"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="NAN" ControlToValidate="txtIssuedQty" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtIssuedQty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
            <td>Issued No. of Bags</td>
            <td>
                <asp:TextBox ID="txtIssuedBags" runat="server" Width="137px" AutoComplete="off" class="Number" MaxLength="4" Style="text-align: right" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtIssuedBags" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td>Date of Issue</td>
            <td>
                <asp:TextBox ID="txtIssuedDate" runat="server" Width="137px" ReadOnly="True"></asp:TextBox>
                <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtIssuedDate' , 'expiry=true,elapse=-150,restrict=true,close=true')" />

                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtIssuedDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td>Truck Number</td>
            <td>
                <asp:TextBox ID="txtTCNo" runat="server" Width="137px" AutoComplete="off" Class="TruckNumber" MaxLength="14" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtTCNo" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
             <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td colspan="6" style="background-color: #CCFF99;"></td>
        </tr>

        <tr>
            <td colspan="6" style="text-align: center; font-size: large;">
                <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />

                <asp:Button ID="btnRecptSubmit" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Submit" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" OnClick="btnRecptSubmit_Click" />

                <asp:Button ID="btnPrint" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Print" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" Enabled="False" CausesValidation="False" OnClick="btnPrint_Click" />

                <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" OnClick="btnRecptClose_Click" />
            </td>
        </tr>



        <%--Allow Only Alphabets using class="alphaNumericWithSpecial" --%>
        <script lang="javascript" src="../PaddyMilling/Scripts/TruckNo.js" type="text/javascript">      </script>

        <script lang="javascript" src="../PaddyMilling/Scripts/Number.js" type="text/javascript">       </script>

    </table>
</asp:Content>

