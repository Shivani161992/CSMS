<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="RcptEntry_RejCMR.aspx.cs" Inherits="PaddyMilling_RcptEntry_RejCMR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" lang="javascript" src="Scripts/jquery-2.1.1.js"></script>
    <script type="text/javascript" lang="javascript" src="Scripts/jquery-ui.js"></script>
    <script type="text/javascript" lang="javascript" src="Scripts/Number2D.js"></script>

    <script type="text/javascript" language="javascript">
        function validate() {
            if (Page_ClientValidate())
                return confirm('क्या आप इस CMR को Reject करना चाहते है? यदि हाँ, तो OK पर क्लिक करे अन्यथा Cancel पर क्लिक करे|');
        }
    </script>

    <script type="text/javascript" language="javascript">
        function validate1() {
            if (Page_ClientValidate())
                return confirm('क्या आप इस CMR को Accept करना चाहते है? यदि हाँ, तो OK पर क्लिक करे अन्यथा Cancel पर क्लिक करे|');
        }
    </script>

    <%--For Calendar Controls--%>
    <link href="calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="calendar/calendar.js"></script>

    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
        }

        .auto-style2 {
            font-size: small;
        }
    </style>

    <table align="center" style="width: 850px; border-style: solid; text-align: left; font-size: medium" border="1" cellspacing="0" cellpadding="2">
        <tr>
            <td colspan="6" style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600"><strong>Receipt Entry For Rejected CMR</strong>
                <input id="hdfGodown" type="hidden" runat="server" />
                <input id="hdfBranch" type="hidden" runat="server" />
            </td>
        </tr>

        <tr>
            <td rowspan="14">&nbsp;</td>
            <td colspan="4" style="text-align: center; background-color: #CCFF99">
                <asp:Label ID="Label2" runat="server" Style="text-align: center; font-weight: 700; color: #FF0000;" ForeColor="Blue" Visible="False"></asp:Label>
            </td>
            <td rowspan="14"></td>
        </tr>
        <tr>
            <td>Crop Year</td>
            <td>
                <asp:DropDownList ID="ddlCropYear" runat="server" Width="149px" AutoPostBack="True" OnSelectedIndexChanged="ddlCropYear_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
            <td>Issue Centre</td>
            <td>

                <asp:TextBox ID="txtIC" runat="server" Width="145px" Style="margin-left: 0px" ReadOnly="True" Enabled="False"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtIC" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td>Miller&#39;s Name</td>
            <td>
                <asp:DropDownList ID="ddlMillName" runat="server" Height="27px" Width="230px" AutoPostBack="True" OnSelectedIndexChanged="ddlMillName_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlMillName" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

            </td>
            <td>Rejection No.</td>
            <td>
                <asp:DropDownList ID="ddlRejectionNo" runat="server" Height="27px" Width="149px" AutoPostBack="True" OnSelectedIndexChanged="ddlRejectionNo_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
        </tr>

        <tr>
            <td>Inspection Date</td>
            <td>
                <asp:TextBox ID="txtInspectionDate" runat="server" Width="145px" AutoComplete="off" MaxLength="14" Enabled="false" ReadOnly="True"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ControlToValidate="txtInspectionDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
            <td>Rejected Total CMR <strong>(Qtls)</strong></td>
            <td>
                <asp:TextBox ID="txtRejQty" runat="server" Width="145px" AutoComplete="off" MaxLength="14" Enabled="false" ReadOnly="True"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtRejQty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td>Issued Total CMR<strong> (Qtls)</strong></td>
            <td>
                <asp:TextBox ID="txtIssuedQty" runat="server" Width="145px" AutoComplete="off" MaxLength="14" Enabled="false" ReadOnly="True"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtIssuedQty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
            <td>Recd. Total CMR <strong>(Qtls)</strong></td>
            <td>
                <asp:TextBox ID="txtRecdTotalCMRQty" runat="server" Width="145px" AutoComplete="off" MaxLength="14" Enabled="false" ReadOnly="True"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ControlToValidate="txtRecdTotalCMRQty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td>Godown</td>
            <td>
                <asp:TextBox ID="txtGodown" runat="server" Width="145px" AutoComplete="off" MaxLength="14" Enabled="false" ReadOnly="True"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="txtGodown" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
            <td>Stack No.</td>
            <td>
                <asp:TextBox ID="txtStackNo" runat="server" Width="145px" AutoComplete="off" MaxLength="14" Enabled="false" ReadOnly="True"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtStackNo" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td colspan="4" style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600; border-style: none; background-color: #CC6600;"><b style="color: #FFFF66">Receipt Details</b></td>
        </tr>


        <tr>
            <td><strong>Recd. CMR </strong><span class="auto-style1"><strong>(Qtls)</strong></span></td>
            <td>
                <asp:TextBox ID="txtRecdQty" runat="server" AutoComplete="off" MaxLength="10" onblur="extractNumber(this,2,true);" onfocus="this.select();" onkeypress="return blockNonNumbers(this, event, true, true);" onkeyup="extractNumber(this,2,true);" onmouseup="return false;" Style="text-align: right" Width="145px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator23" runat="server" ControlToValidate="txtRecdQty" Display="Dynamic" ErrorMessage="NAN" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="txtRecdQty" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td><strong>Recd. Bags</strong></td>
            <td>
                <asp:TextBox ID="txtBags" runat="server" AutoComplete="off" class="Number" MaxLength="6" onfocus="this.select();" onmouseup="return false;" Style="text-align: right" Width="145px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="txtBags" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td><strong>Type of Bags</strong></td>
            <td colspan="3">
                <asp:RadioButton ID="rdbNewJute" runat="server" GroupName="Bags" Text="New Jute(SBT)" />
                <asp:RadioButton ID="rdbOldJute" runat="server" GroupName="Bags" Text="Old Jute(SBT)" Style="margin-left: 8px;" />
                <asp:RadioButton ID="rdbOnceJute" runat="server" GroupName="Bags" Text="Once Used Jute(SBT)" Style="margin-left: 8px;" />
                <br />
                <asp:RadioButton ID="rdbNewPP" runat="server" GroupName="Bags" Text="New PP(HDPE)" />
                <asp:RadioButton ID="rdbOldPP" runat="server" GroupName="Bags" Text="Old PP(HDPE)" Style="margin-left: 4px;" />
                <asp:RadioButton ID="rdbOncePP" runat="server" GroupName="Bags" Text="Once Used PP(HDPE)" Style="margin-left: 4px;" />
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
                <asp:TextBox ID="txtTagNo" runat="server" AutoComplete="off" class="Number" MaxLength="6" onfocus="this.select();" onmouseup="return false;" Style="text-align: right" Width="145px"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td><strong>Truck Number</strong></td>
            <td>
                <asp:TextBox ID="txtTruckNo" runat="server" AutoComplete="off" Class="TruckNumber" MaxLength="14" onfocus="this.select();" onmouseup="return false;" Width="145px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTruckNo" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td><strong>Truck Number1</strong></td>
            <td>
                <asp:TextBox ID="txtTruckNo0" runat="server" AutoComplete="off" Class="TruckNumber" MaxLength="14" onfocus="this.select();" onmouseup="return false;" Width="145px"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td><strong>Recd. CMR Date</strong></td>
            <td>
                <asp:TextBox ID="txtDate" runat="server" ReadOnly="True" Width="145px"></asp:TextBox>
                <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtDate' , 'expiry=true,elapse=-250,restrict=true,close=true' )" /><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDate" ErrorMessage="RequiredFieldValidator" Font-Bold="False" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td><strong>Toul Parchi No.</strong></td>
            <td>
                <asp:TextBox ID="txtToulReceiptNo" runat="server" Width="145px" AutoComplete="off" Class="TruckNumber" MaxLength="14" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtToulReceiptNo" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
        </tr>

        <tr>
            <td><strong>Type of CMR</strong></td>
            <td>

                <asp:DropDownList ID="ddlCommodity" runat="server" Width="149px">
                </asp:DropDownList></td>
            <td class="auto-style2">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td colspan="4" style="background-color: #CCFF99; color: #0000FF;"></td>
        </tr>

        <script lang="javascript" src="Scripts/TruckNo.js" type="text/javascript">      </script>
        <script lang="javascript" src="Scripts/Number.js" type="text/javascript"> </script>
        <script lang="javascript" src="Scripts/Alphabets.js" type="text/javascript"> </script>

        <tr>
            <td colspan="6" style="text-align: center; font-size: large;">
                <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />

                <asp:Button ID="btnAccept" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Accept" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" OnClick="btnAccept_Click" OnClientClick="return validate1();" Enabled="False" />

                <%--<asp:Button ID="btnReject" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Reject" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" OnClick="btnReject_Click" Enabled="False" OnClientClick="return validate();" />--%>

                <asp:Button ID="btnPrint" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Print" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" OnClick="btnPrint_Click" CausesValidation="False" Enabled="False" />

                <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" OnClick="btnRecptClose_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

