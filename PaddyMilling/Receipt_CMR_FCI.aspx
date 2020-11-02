<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Receipt_CMR_FCI.aspx.cs" Inherits="PaddyMilling_Receipt_CMR_FCI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function validate() {
            if (Page_ClientValidate())
                return confirm('क्या आपने सही लॉट चुना है? यदि हाँ, तो OK पर क्लिक करे अन्यथा Cancel पर क्लिक करे तथा सही लॉट की संख्या चुने|');
        }
    </script>

    <script type="text/javascript" lang="javascript" src="Scripts/jquery-2.1.1.js"></script>
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/Number2D.js"></script>

    <%--For Calendar Controls--%>
    <link href="calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="calendar/calendar.js"></script>

    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
        }

        .auto-style1 {
            color: #0000FF;
        }

        .auto-style3 {
            color: #FF0000;
        }
    </style>

    <table align="center" style="width: 870px; border-style: solid; text-align: left; font-size: medium" border="1" cellspacing="0" cellpadding="2">
        <tr>
            <td colspan="7" style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600"><strong>Receipt Entry CMR From FCI</strong>
            </td>
            <input id="hdfAgrmtDist" type="hidden" runat="server" />
            <input id="hdfAllQty" type="hidden" runat="server" />
            <input id="hdfAdjustCMRDO" type="hidden" runat="server" />
            <input id="hdfFCIState" type="hidden" runat="server" />
        </tr>

        <tr>
            <td rowspan="14">&nbsp;</td>
            <td colspan="5" style="text-align: center; background-color: #CCFF99">
                <asp:Label ID="Label2" runat="server" Style="text-align: center; font-weight: 700; color: #FF0000;" ForeColor="Blue" Visible="False"></asp:Label>
            </td>
            <td rowspan="14"></td>
        </tr>
        <tr>
            <td>District</td>
            <td>
                <asp:TextBox ID="txtDistManager" runat="server" Width="137px" Style="margin-left: 0px" ReadOnly="True" Enabled="False"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDistManager" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

            </td>
            <td colspan="2">Crop Year</td>
            <td>

                <asp:TextBox ID="txtYear" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtYear" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td>मिल का नाम</td>
            <td>
                <asp:DropDownList ID="ddlMillName" runat="server" Height="27px" Width="230px" AutoPostBack="True" OnSelectedIndexChanged="ddlMillName_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlMillName" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

            </td>
            <td colspan="2">अनुबंध नंबर</td>
            <td>
                <asp:DropDownList ID="ddlAgtmtNumber" runat="server" Height="27px" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlAgtmtNumber_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlAgtmtNumber" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td colspan="4">DO के अनुसार मिलर को प्रदायित धान का लॉट नंबर</td>
            <td>

                <asp:DropDownList ID="ddlLotNO" runat="server" Width="141px" OnSelectedIndexChanged="ddlLotNO_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="5" style="background-color: #CC9900; color: #999999;"></td>
        </tr>
        <tr style="font-size: small; background-color: #CCFF99;">
            <td colspan="5">लॉट
                क्र.<asp:Label ID="lbllot" runat="server" Text="" Style="font-weight: 700; color: #0000FF"></asp:Label>
                तथा DO क्र.<asp:Label ID="lblDONO" runat="server" Text="" Style="font-weight: 700; color: #0000FF"></asp:Label>
                &nbsp;के अनुसार मिलर द्वारा उठाई गयी
                <asp:Label ID="lblMillingType" runat="server" Text="" Style="font-weight: 700; color: #0000FF"></asp:Label>
                धान की मात्रा
                <asp:Label ID="lblDOQty" runat="server" Text="" Style="font-weight: 700; color: #0000FF"></asp:Label>
                <span class="auto-style1"><strong>Qtls</strong></span> तथा शेष धान की मात्रा
                <asp:Label ID="lblDORem" runat="server" Text="" Style="font-weight: 700; color: #0000FF"></asp:Label>
                <span class="auto-style1"><strong>Qtls</strong></span></td>
        </tr>
        <tr>
            <td colspan="5" style="background-color: #CC9900; color: #999999;"></td>
        </tr>

        <tr>
            <td><strong>Acceptance Note No.</strong></td>
            <td>
                <asp:TextBox ID="txtAcptNo" runat="server" AutoComplete="off" class="Number" MaxLength="10" onfocus="this.select();" onmouseup="return false;" Style="text-align: right" Width="137px" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ControlToValidate="txtAcptNo" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td colspan="2"><strong>Acceptance Note Date</strong></td>
            <td>
                <asp:TextBox ID="txtFromDate" runat="server" Width="137px" Style="margin-left: 0px" ReadOnly="True"></asp:TextBox>
                &nbsp;
                <img src="calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtFromDate' , 'expiry=true,elapse=-150,restrict=true,limit=0,close=true')" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFromDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td><strong>Weight Check Memo No.</strong></td>
            <td>
                <asp:TextBox ID="txtWeightMemo" runat="server" AutoComplete="off" class="TruckNumber" MaxLength="10" onfocus="this.select();" onmouseup="return false;" Style="text-align: right" Width="137px" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ControlToValidate="txtWeightMemo" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                </td>
            <td colspan="2"><strong>Weight Check Memo Date</strong></td>
            <td>
                <asp:TextBox ID="txtMemoDate" runat="server" Width="137px" Style="margin-left: 0px" ReadOnly="True"></asp:TextBox>
                &nbsp;
                <img src="calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtMemoDate' , 'expiry=true,elapse=-150,restrict=true,limit=0,close=true')" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMemoDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
        </tr>



        <tr>
            <td><strong>Recd. CMR Qty </strong><span class="auto-style3"><strong>(Qtls)</strong></span></td>
            <td>
                <asp:TextBox ID="txtRecdQty" runat="server" AutoComplete="off" MaxLength="6" onblur="extractNumber(this,2,true);" onfocus="this.select();" onkeypress="return blockNonNumbers(this, event, true, true);" onkeyup="extractNumber(this,2,true);" onmouseup="return false;" Style="text-align: right" Width="137px" Enabled="False"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator23" runat="server" ControlToValidate="txtRecdQty" Display="Dynamic" ErrorMessage="NAN" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="txtRecdQty" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td colspan="2"><strong>Recd. No. of Bags</strong></td>
            <td>
                <asp:TextBox ID="txtBags" runat="server" AutoComplete="off" class="Number" MaxLength="3" onfocus="this.select();" onmouseup="return false;" Style="text-align: right" Width="137px" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="txtBags" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td><strong>Truck Number</strong></td>
            <td>
                <asp:TextBox ID="txtTruckNo" runat="server" AutoComplete="off" Class="TruckNumber" MaxLength="14" onfocus="this.select();" onmouseup="return false;" Width="137px" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtTruckNo" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td colspan="2"><strong>Truck Number1</strong></td>
            <td>
                <asp:TextBox ID="txtTruckNo0" runat="server" AutoComplete="off" Class="TruckNumber" MaxLength="14" onfocus="this.select();" onmouseup="return false;" Width="137px" Enabled="False"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td><strong>Type of Bags</strong></td>
            <td colspan="4">
                <asp:RadioButton ID="rdbNewJute" runat="server" GroupName="Bags" Text="New Jute(SBT)" />
                <asp:RadioButton ID="rdbOldJute" runat="server" GroupName="Bags" Text="Old Jute(SBT)" Style="margin-left: 8px;" />
                <asp:RadioButton ID="rdbOnceJute" runat="server" GroupName="Bags" Text="Once Used Jute(SBT)" Style="margin-left: 9px;" />
                <br />
                <asp:RadioButton ID="rdbNewPP" runat="server" GroupName="Bags" Text="New PP(HDPE)" />
                <asp:RadioButton ID="rdbOldPP" runat="server" GroupName="Bags" Text="Old PP(HDPE)" Style="margin-left: 4px;" />
                <asp:RadioButton ID="rdbOncePP" runat="server" GroupName="Bags" Text="Once Used PP(HDPE)" Style="margin-left: 4px;" />
            </td>
        </tr>

        <tr>
            <td><strong>Recd. CMR FCI Dist</strong></td>
            <td colspan="2">

                <asp:DropDownList ID="ddlCMRDist" runat="server" Width="141px" AutoPostBack="false" Enabled="False" >
                </asp:DropDownList>
            </td>
            <td>

                <strong>WHR No.</strong></td>
            <td>

                <asp:TextBox ID="txtWhrNo" runat="server" AutoComplete="off" Class="TruckNumber" Enabled="False" MaxLength="25" onfocus="this.select();" onmouseup="return false;" Width="137px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" ControlToValidate="txtWhrNo" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td colspan="5" style="background-color: #CCFF99; color: #0000FF;"></td>
        </tr>
        
        <tr>
            <td colspan="7" style="text-align: center; font-size: large;">
                <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />

                <asp:Button ID="btnRecptSubmit" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Submit" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" OnClientClick="return validate();" Enabled="False" OnClick="btnRecptSubmit_Click1" />

                <asp:Button ID="btnPrint" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Print" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" OnClick="btnPrint_Click" CausesValidation="False" Enabled="False" />

                <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" OnClick="btnRecptClose_Click" />
            </td>
        </tr>

        <script lang="javascript" src="../PaddyMilling/Scripts/Alphabets.js" type="text/javascript">       </script>

        <script lang="javascript" src="../PaddyMilling/Scripts/TruckNo.js" type="text/javascript">      </script>

        <script lang="javascript" src="../PaddyMilling/Scripts/Number.js" type="text/javascript">        </script>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

