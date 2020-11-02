<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Gunny_Bags_Receipt_Entry.aspx.cs" Inherits="GunnyBags_Gunny_Bags_Receipt_Entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
        <table id="Table1" runat="server" style="width: 1100px; background-color: #004E4E; color: #FFFAF0;" border="1">
            <tr>
                <td colspan="4" style="width: 1000px; text-align: center; font-size: large; font-family: 'Lucida Fax';">
                    <strong>Gunny Bags Receipt Entry </strong>
                </td>
            </tr>
            <tr runat="server" id="trID" visible="false">
                <td colspan="4" style="background-color: white; color: red; font-size: x-large; font-family: 'Lucida Fax';">
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
            </tr>
             <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Receiving District</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtRecDist" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>

                </td>


                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Receiving Issue Center</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtRecIssueCenter" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Receiving Branch</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:DropDownList ID="ddlRecBranch" runat="server" Width="250px" CssClass="tddl" AutoPostBack="True" OnSelectedIndexChanged="ddlRecBranch_SelectedIndexChanged">
                    </asp:DropDownList>

                </td>


                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong> Receiving Godown</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
<asp:DropDownList ID="ddlRecGodown" runat="server" Width="250px" CssClass="tddl" AutoPostBack="True" OnSelectedIndexChanged="ddlRecGodown_SelectedIndexChanged">
                    </asp:DropDownList>                </td>
            </tr>
             <tr id="Tr3" runat="server" style="height: 25px;">
                <td colspan="4" style="background-color: white; height: 25px; color: red; font-size: x-large; font-family: 'Lucida Fax';"></td>
            </tr>
              <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Delivery Challan</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:DropDownList ID="ddlDeliveryChallan" runat="server" Width="250px" CssClass="tddl" AutoPostBack="True" OnSelectedIndexChanged="ddlDeliveryChallan_SelectedIndexChanged">
                    </asp:DropDownList>

                </td>


                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Crop Year</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtCropyear" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>
                </td>
            </tr>

                   <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Indent Number</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtIN" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>

                </td>


                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Rail Head</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                                        <asp:TextBox ID="txtRailHead" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>

                </td>
            </tr>

             <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>RR Receive ID</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txt_receiveID" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>

                </td>


                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Issued Date</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtIssuedDate" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong> Sent Quantity (Bales)</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtQuantity" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>

                </td>


                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Bags Type</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtbagsType" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>
                </td>
            </tr>

             <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>TC Number</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtTcNumber" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>

                </td>


                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Truck Number</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                                        <asp:TextBox ID="txtTruckNumber" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>

                </td>
            </tr>

<%--            <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Indent Number</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtIN" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>

                </td>


                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Rail Head</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                                        <asp:TextBox ID="txtRailHead" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>

                </td>
            </tr>--%>

            

            <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong> Transporter Name</strong>
                </td>
                <td colspan="3" style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtTransporter_Name" CssClass="tcolumn" Width="750px" Enabled="false" runat="server"></asp:TextBox>

                </td>

             
               <%-- <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Transporter Name</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="TextBox2" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>
                </td>--%>
            </tr>
           <%-- <tr id="Tr4" runat="server" style="height: 25px;">
                <td colspan="4" style="background-color: white; height: 25px; color: black; font-size:medium; font-family: 'Lucida Fax';">
                      <asp:Label ID="lblsending" runat="server">Sending Details</asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>  District</strong>
                </td>
                <td  style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtSenDist" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>

                </td>
                 <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                     <strong>  Issue Center</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                                        <asp:TextBox ID="txtSenIC" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>

                </td>
                </tr>

             <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong> Branch</strong>
                </td>
                <td  style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtSenBranch" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>

                </td>
                 <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                     <strong>  Godown</strong>
                </td>
                <td style="width: 350px; height: 15px; text-align: center; background-color: #FFFAFA">
                                        <asp:TextBox ID="txtSenGodown" CssClass="tcolumn" Width="350px" Enabled="false" runat="server"></asp:TextBox>

                </td>
                </tr>--%>



             <tr id="Tr1" runat="server" style="height: 25px;">
                <td colspan="4" style="background-color: white; height: 25px; color: black; font-size:medium; font-family: 'Lucida Fax';">
                      <asp:Label ID="lblreceivingDetails" runat="server">Receiving Details</asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Received Quantity Bales</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtRecQuantity" CssClass="tcolumn" Width="250px" Enabled="true" runat="server" OnTextChanged="txtRecQuantity_TextChanged" AutoPostBack="true"></asp:TextBox>

                </td>


              <%--  <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Received Bags Type</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                     <asp:DropDownList ID="ddlRecBagsType" AutoPostBack="true" CssClass="tddl" runat="server" Width="250px" OnSelectedIndexChanged="ddlRecBagsType_SelectedIndexChanged">
                            
                   <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>

                    <asp:ListItem Text="Jute(SBT)" Value="Jute" Selected="True"></asp:ListItem>

                    <asp:ListItem Text="PP" Value="PP"></asp:ListItem>                            
                        </asp:DropDownList>
                     <asp:TextBox ID="txtBags" CssClass="tcolumn" Width="250px" Enabled="true" runat="server"></asp:TextBox>

                </td>--%>
                 <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Date OF Receiving</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtDate_of_Receiving" CssClass="tcolumn" Width="200px" Enabled="true" runat="server"></asp:TextBox>
                    <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtDate_of_Receiving' , 'expiry=true,elapse=-150,restrict=true,close=true')" />
                </td>
            </tr>

            <tr>
               


                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Truck Number</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtRecTruckNumber" CssClass="tcolumn" Width="250px" Enabled="true" runat="server"></asp:TextBox>
                </td>
                 <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Toul Receipt</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtToul" CssClass="tcolumn" Width="250px" Enabled="true" runat="server"></asp:TextBox>
                </td>
            </tr>
             <tr id="Tr2" runat="server" style="height: 25px;">
                <td colspan="4" style="background-color: white; height: 25px; color: red; font-size: x-large; font-family: 'Lucida Fax';"></td>
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
</asp:Content>

