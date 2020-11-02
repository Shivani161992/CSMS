<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="GunnyBags_DeliveryChallan.aspx.cs" Inherits="GunnyBags_GunnyBags_DeliveryChallan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <html>
    <head>
        <style>
            .column
            {
                height: 15px;
                color: #6B8E23;
                font-family: 'Lucida Fax';
                text-align: center;
                font-size: 35px;
            }

            .tcolumn
            {
                width: 400px;
                text-align: center;
                border-radius: 32px;
                height: 25px;
                font-size: 15px;
                font-family: 'Lucida Fax';
            }

                .tcolumn:focus
                {
                    box-shadow: 0 0 25px rgb(107,142,35);
                    padding: 3px 0px 3px 3px;
                    margin: 5px 1px 3px 0px;
                    border: 1px solid rgba(81, 203, 238, 1);
                }

            .tddl
            {
                width: 400px;
                border-radius: 32px;
                height: 25px;
                text-align: center;
                font-family: 'Lucida Fax';
            }

            .bttn
            {
                width: 400px;
                height: 30px;
                border-radius: 25px;
                color: #fff!important;
                background-color: #000!important;
                border-radius: 32px;
            }

            .w3-grey, .w3-hover-grey:hover, .w3-gray, .w3-hover-gray:hover
            {
                color: white!important;
                background-color: #004E4E!important;
                font-family: 'Lucida Fax';
                font-size: 15px;
            }

            .bttn:active
            {
                background-color: yellow;
            }

            .bttnnew
            {
                width: 200px;
                height: 25px;
                border-radius: 25px;
                color: #fff!important;
                background-color: #004E4E!important;
                border-radius: 32px;
            }

            .txt
            {
            }

                .txt:focus
                {
                }

            .table
            {
                background-color: #1B2631;
                width: 500px;
                border-radius: 32px;
                margin-right: 35px;
                text-align: center;
            }

            input::-webkit-input-placeholder
            { /* WebKit browsers */
                color: #004E4E;
                !important;
            }

            input:-moz-placeholder
            { /* Mozilla Firefox 4 to 18 */
                color: #004E4E;
                !important;
            }

            input::-moz-placeholder
            { /* Mozilla Firefox 19+ */
                color: #004E4E;
                !important;
            }

            input:-ms-input-placeholder
            { /* Internet Explorer 10+ */
                color: #004E4E;
                !important;
            }
        </style>
        <link href="../PaddyMilling/calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
        <script type="text/javascript" lang="" src="../PaddyMilling/calendar/calendarLot.js"></script>
    </head>
    <body>
        <table id="Table1" runat="server" style="width: 1000px; background-color: #004E4E; color: #FFFAF0;" border="1">
            <tr>
                <td colspan="4" style="width: 1000px; text-align: center; font-size: large; font-family: 'Lucida Fax';">
                    <strong>Gunny Bags Delivery Challan </strong>
                </td>
            </tr>
            <tr runat="server" id="trID" visible="false">
                <td colspan="4" style="background-color: white; color: red; font-size: x-large; font-family: 'Lucida Fax';">
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>District</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtdistrict" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>

                </td>


                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Crop Year</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtCropYear" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Receiving ID</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:DropDownList ID="ddlReceiID" runat="server" Width="250px" CssClass="tddl" AutoPostBack="True" OnSelectedIndexChanged="ddlReceiID_SelectedIndexChanged">
                    </asp:DropDownList>

                </td>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Bags Type</strong>
                </td>

                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <asp:TextBox ID="txtBagsType" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>

                </td>

            </tr>
            <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Total Received Qty (Bales)</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtReceivedQty" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>

                </td>


                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Remaining Qty(Bales)</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtRemainingQty" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>

                </td>
            </tr>
            0
            <tr>
                <td colspan="4" style="height: 25px;"></td>
            </tr>

            <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Truck Challan(TC) Number</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtTCNo" CssClass="tcolumn" Width="250px" runat="server"></asp:TextBox>

                </td>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Date Of Issue </strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">

                    <asp:TextBox ID="txtDateofReceipt" CssClass="tcolumn" Width="200px" Enabled="true" runat="server"></asp:TextBox>
                    <%--<img src="calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtToDate' , 'restrict=true,instance=single,limit=<%= DateLimit %>')" />--%>
                    <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtDateofReceipt' , 'expiry=true,elapse=-150,restrict=false,close=true')" />

                </td>
            </tr>

            <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Transporter Name(HLRT)</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:DropDownList ID="ddlTransporterName" runat="server" Width="250px" CssClass="tddl" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Truck Number</strong>
                </td>

                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtTruckNo" CssClass="tcolumn" Width="250px" runat="server"></asp:TextBox>

                </td>

            </tr>
            <tr>
                <td colspan="4" style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtQuantity" runat="server" placeholder="Enter Quantity(Bales)" class="tcolumn" AutoPostBack="true" OnTextChanged="txtQuantity_TextChanged"></asp:TextBox>

                </td>
            </tr>
            <tr id="Tr4" runat="server" style="height: 25px;">
                <td colspan="4" style="background-color: white; height: 25px; color: black; font-size: medium; font-family: 'Lucida Fax';">
                    <asp:Label ID="lblsending" runat="server">Sending Details</asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Sending District</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtSenDist" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>

                </td>


                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Issue Center</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtSenIC" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Branch</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:DropDownList ID="ddlSenBranch" runat="server" Width="250px" CssClass="tddl" AutoPostBack="True" OnSelectedIndexChanged="ddlRecBranch_SelectedIndexChanged">
                    </asp:DropDownList>

                </td>


                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>&nbsp;Godown</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:DropDownList ID="ddlSenGodown" runat="server" Width="250px" CssClass="tddl" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="tr6" runat="server" style="height: 25px;">
                <td colspan="4" style="background-color: white; height: 25px; color: red; font-size: x-large; font-family: 'Lucida Fax';"></td>
            </tr>
            <tr id="Tr3" runat="server" style="height: 25px;">
                <td colspan="4" style="background-color: white; height: 25px; color: black; font-size: medium; font-family: 'Lucida Fax';">
                    <asp:Label ID="lblreceivingDetails" runat="server">Receiving Details</asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:RadioButton ID="rdbOwnDist" runat="server" GroupName="District" Style="font-weight: 700; color: #FFFAF0" Checked="false" AutoPostBack="true" OnCheckedChanged="rdbOwnDist_CheckedChanged" Text="Own District" />

                </td>
                <td colspan="2">
                    <asp:RadioButton ID="rdbOtherDist" runat="server" GroupName="District" Style="font-weight: 700; color: #FFFAF0" AutoPostBack="true" Checked="false" OnCheckedChanged="rdbOtherDist_CheckedChanged" Text="Other District" />

                </td>
            </tr>
            <tr id="Tr1" runat="server" style="height: 25px;">
                <td colspan="4" style="background-color: white; height: 25px; color: red; font-size: x-large; font-family: 'Lucida Fax';"></td>
            </tr>

            <tr runat="server" id="trDistrictIC" visible="false">
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Receiving District</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtDelidist" CssClass="tcolumn" Width="250px" Enabled="false" Visible="false" runat="server"></asp:TextBox>
                    <asp:DropDownList ID="ddlDelidist" runat="server" Width="250px" CssClass="tddl" Visible="false" AutoPostBack="True" OnSelectedIndexChanged="ddlDelidist_SelectedIndexChanged">
                    </asp:DropDownList>

                </td>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Issue Center</strong>
                </td>

                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:DropDownList ID="ddlIC" runat="server" Width="250px" CssClass="tddl" AutoPostBack="True" OnSelectedIndexChanged="ddlIC_SelectedIndexChanged">
                    </asp:DropDownList>

                </td>
            </tr>
            <tr runat="server" id="trBranchGodown" visible="false">
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Branch</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:DropDownList ID="ddlBranch" runat="server" Width="250px" CssClass="tddl" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                    </asp:DropDownList>

                </td>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Godown</strong>
                </td>

                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:DropDownList ID="ddlGodown" runat="server" Width="250px" CssClass="tddl" AutoPostBack="True" OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged">
                    </asp:DropDownList>

                </td>
            </tr>
            <tr id="Tr2" runat="server" style="height: 25px;">
                <td colspan="4" style="background-color: white; color: red; font-size: x-large; font-family: 'Lucida Fax';"></td>
            </tr>
            <tr>
                <td colspan="4" style="width: 1000px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:Button ID="bttSubmit" runat="server" Text="Submit" CssClass="bttn w3-grey" Enabled="false" OnClick="bttSubmit_Click" />
                <asp:Button ID="bttprint" runat="server" Text="Print" CssClass="bttn w3-grey" Enabled="false" Visible="false" OnClick="bttprint_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 1000px; height: 15px; text-align: left; background-color: #FFFAFA">
                    <asp:Button ID="bttNew" runat="server" Text="New" OnClick="bttNew_Click" />
                </td>
                <td colspan="2" style="width: 1000px; height: 15px; text-align: right; background-color: #FFFAFA">
                    <asp:Button ID="bttClose" runat="server" Text="Close" OnClick="bttClose_Click" />
                </td>


            </tr>


        </table>
    </body>
    </html>
</asp:Content>

