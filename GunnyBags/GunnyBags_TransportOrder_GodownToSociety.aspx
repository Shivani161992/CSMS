<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="GunnyBags_TransportOrder_GodownToSociety.aspx.cs" Inherits="GunnyBags_GunnyBags_TransportOrder_GodownToSociety" %>

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
                      <input id="hdfGodown" type="hidden" runat="server" />
                    <strong>Gunny Bags Transport Order (Godown To Society) </strong>
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
                    <asp:DropDownList ID="ddlCropYear" runat="server" Width="250px" CssClass="tddl" AutoPostBack="True" OnSelectedIndexChanged="ddlCropYear_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Marketing Season</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:DropDownList ID="ddlMSeason" Width="250px" CssClass="tddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMSeason_SelectedIndexChanged">
                        <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Rabi" Value="R"></asp:ListItem>
                        <asp:ListItem Text="Kharif" Value="K"></asp:ListItem>
                    </asp:DropDownList>
                </td>

                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>From Godown</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtFGodown" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>
                </td>

            </tr>

             <tr runat="server" id="trbal" visible="false">
                <td colspan="4" style="height: 10px;">
                     <asp:Label ID="Label3" runat="server"></asp:Label>
                </td>
            </tr>

            <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Society District</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:DropDownList ID="ddlSocDist" Width="250px" CssClass="tddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSocDist_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>

                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>To Society</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:DropDownList ID="ddlSociety" Width="250px" CssClass="tddl" runat="server">
                    </asp:DropDownList>
                </td>

                <%--<td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Godown</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                     <asp:TextBox ID="TextBox1" CssClass="tcolumn" Width="250px" Enabled="false" runat="server" ></asp:TextBox>
                </td>
                --%>
            </tr>

            <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Bags Type</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtBagsType" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>

                </td>

                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>End date Of Transportation </strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">

                    <asp:TextBox ID="txtDateofReceipt" CssClass="tcolumn" Width="200px" Enabled="true" runat="server"></asp:TextBox>
                    <%--<img src="calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtToDate' , 'restrict=true,instance=single,limit=<%= DateLimit %>')" />--%>
                    <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtDateofReceipt' , 'expiry=true,elapse=-150,restrict=false,close=true')" />

                </td>

            </tr>

            <tr>
                <td style="text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;" class="auto-style2">
                    <strong>Sector </strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: left; background-color: #FFFAFA">
                    <asp:DropDownList ID="ddlSector" CssClass="tddl" AutoPostBack="true" runat="server" Width="250px" Height="26px" OnSelectedIndexChanged="ddlSector_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td style="text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;" class="auto-style2">
                    <strong>Transporter </strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: left; background-color: #FFFAFA">
                    <asp:DropDownList ID="ddlTransporterName" CssClass="tddl" runat="server" Width="250px" Height="26px">
                    </asp:DropDownList>
                </td>
            </tr>


            <tr>
                <td colspan="4" style="text-align: center; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;" class="auto-style2">
                    <asp:Label ID="Label2" runat="server"></asp:Label>
                </td>

            </tr>
            <tr>
                <td colspan="4" style="height: 10px;"></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;" class="auto-style2">
                    <strong>Quantity (Bales) </strong>
                </td>
                <td colspan="2" style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtQuantity" CssClass="tcolumn" Width="250px" Enabled="true" runat="server"></asp:TextBox>
                </td>
            </tr>
              <tr id="Tr1" runat="server" style="height: 25px;">
                <td colspan="4" style="background-color: white; color: red; font-size: x-large; font-family: 'Lucida Fax';"></td>
            </tr>
            <tr>
                <td colspan="4" style="width: 1000px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:Button ID="bttSubmit" runat="server" Text="Submit" CssClass="bttn w3-grey" Enabled="false" OnClick="bttSubmit_Click" Visible="true"  />
                     <asp:Button ID="bttPrint" runat="server" Text="Print" CssClass="bttn w3-grey" Enabled="false" OnClick="bttPrint_Click" Visible="false" />
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

