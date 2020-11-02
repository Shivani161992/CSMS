<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="GunnyBags_IndentCreation.aspx.cs" Inherits="GunnyBags_GunnyBags_IndentCreation" %>

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
        </style>
        <link href="../PaddyMilling/calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
        <script type="text/javascript" lang="" src="../PaddyMilling/calendar/calendarLot.js"></script>
    </head>
    <body>
        <table runat="server" style="width: 1000px; background-color: #004E4E; color: #FFFAF0;" border="1">

            <tr>
                <td colspan="4" style="width: 1000px; text-align: center; font-size: large; font-family: 'Lucida Fax';">
                    <strong>Gunny Bags Indent Creation </strong>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="width: 1000px; height: 15px; text-align: center; background-color: #FFFAFA"></td>
            </tr>
            <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Indentor Name </strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtIndName" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>
                </td>
                <td colspan="2" style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA"></td>

            </tr>

            <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Indent Number </strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtIndNumber" CssClass="tcolumn" Width="250px" Enabled="true" runat="server"></asp:TextBox>
                </td>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Indent Date </strong>
                </td>

                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    
                    <asp:TextBox ID="txtIndDate" CssClass="tcolumn" Width="200px" Enabled="true" runat="server"></asp:TextBox>
                     <%--<img src="calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtToDate' , 'restrict=true,instance=single,limit=<%= DateLimit %>')" />--%>
                    <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtIndDate' , 'expiry=true,elapse=-150,restrict=false,close=true')" />

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtIndDate" ErrorMessage="RequiredFieldValidator" Font-Bold="False" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                 </td>

                
            </tr>

            <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Agency name </strong>
                </td>
                <td colspan="3" style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtAgeName" CssClass="tcolumn" Width="750px" Enabled="false" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Marketing season </strong>
                </td>


                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:DropDownList ID="ddlMarSeason" AutoPostBack="true" CssClass="tddl" runat="server" Width="200px" OnSelectedIndexChanged="ddlMarSeason_SelectedIndexChanged">

                        <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>

                        <asp:ListItem Text="KMS" Value="KMS"></asp:ListItem>

                        <asp:ListItem Text="RMS" Value="RMS"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Color Code </strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtColorCode" CssClass="tcolumn" Width="250px" Enabled="true" runat="server"></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td colspan="4" style="width: 1000px; height: 15px; text-align: center; background-color: #FFFAFA"></td>

            </tr>
            <tr>
                <td colspan="4" style="width: 1000px; text-align: center; font-size: large; font-family: 'Lucida Fax';">
                    <strong>Despatch Mode</strong>
                    <asp:Label ID="lbldespatchMode" Font-Size="large" Font-Bold="true" runat="server" Text="Rail"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="width: 1000px; height: 15px; text-align: center; background-color: #FFFAFA"></td>

            </tr>

            <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Indent Qty(Bales) </strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtqty" CssClass="tcolumn" Width="250px" Enabled="false"  runat="server"></asp:TextBox>
                </td>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Fund Required (Rupees) </strong>
                </td>

                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtFundReq" CssClass="tcolumn" Width="250px" OnTextChanged="txtFundReq_TextChanged"  Enabled="false" runat="server"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Confirm Qty(Bales) </strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtConQty" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>
                </td>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Delivery Date </strong>
                </td>

                <td style="width: 250px; height: 15px; text-align: left; background-color: #FFFAFA">
                    <asp:TextBox ID="txtDelyDate" CssClass="tcolumn" Width="200px" Enabled="true" runat="server"></asp:TextBox>
                    <%--<img src="calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtToDate' , 'restrict=true,instance=single,limit=<%= DateLimit %>')" />--%>
                    <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtDelyDate' , 'expiry=true,elapse=-150,restrict=false,close=true')" />

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDelyDate" ErrorMessage="RequiredFieldValidator" Font-Bold="False" SetFocusOnError="True">**</asp:RequiredFieldValidator>
          </td>
            </tr>


            <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Crop Year </strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:DropDownList ID="ddlCropYear" AutoPostBack="true" Width="200px" CssClass="tddl" runat="server" OnSelectedIndexChanged="ddlCropYear_SelectedIndexChanged" >             
                        </asp:DropDownList>
                </td>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Status</strong></td>

                <td style="width: 250px; height: 15px; text-align: left; background-color: #FFFAFA">
                    <asp:TextBox ID="txtStatus" CssClass="tcolumn" Width="200px" Enabled="false" runat="server"></asp:TextBox>

                </td>
            </tr>

            <tr>
                <td colspan="4" style="width: 1000px; height: 15px; text-align: center; background-color: #FFFAFA"></td>

            </tr>
            <tr>
                <td colspan="4" style="width: 1000px; text-align: center; font-size: large; font-family: 'Lucida Fax';">
                    <strong>Indent Consignment Allocation Detail</strong>
                </td>
            </tr>

            <tr>
                <td colspan="4" style="width: 1000px; height: 15px; text-align: center; background-color: #FFFAFA"></td>

            </tr>
            <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>District </strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:DropDownList ID="Ddldist" runat="server" Width="250px" AutoPostBack="True" CssClass="tddl" OnSelectedIndexChanged="ddldist_SelectedIndexChanged" Height="25px">
                    </asp:DropDownList>

                </td>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Destination (Rail Head) </strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:DropDownList ID="ddlDestination" runat="server" Width="250px" CssClass="tddl" AutoPostBack="True" Height="25px">
                    </asp:DropDownList>
                </td>
            </tr>

             <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Consignee Name </strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtConsigneeName" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>


                </td>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Quantity </strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtQuantity" CssClass="tcolumn" Width="200px" Enabled="true" runat="server"></asp:TextBox>
                    <asp:Button ID="bttAdd" runat="server" Text="Add"  OnClick="bttAdd_Click" />
                            </td>
                </tr>
            <tr>
                <td colspan="4" style="width: 1000px; height: 15px; text-align: center; background-color: #FFFAFA"></td>

            </tr>
            <tr runat="server" id="trGrid" visible="false">
                    <td colspan="4" style="width: 1000px; height:15px; text-align: center; background-color: #FFFAFA">
                         <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False" ShowFooter="True"  CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                      OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting" >
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField ReadOnly="true" HeaderText="S. No." />
                        <asp:BoundField DataField="fromdisttext" HeaderText="District" />
                        <asp:BoundField DataField="fromdistval" HeaderText="Destination" />
                        
                         <asp:BoundField DataField="todistval" HeaderText="Quantity" />
                        <asp:TemplateField HeaderText="Remove">
                            <ItemTemplate>
                                <asp:LinkButton ID="iddelete" runat="server" CausesValidation="false" CommandName="Delete" OnClientClick="return confirm('Are You Sure You Want To Remove?');"  Text="Remove"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#666666" Font-Bold="True" ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" />
                    <HeaderStyle BackColor="#004E4E" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                </asp:GridView>

                    </td>
                </tr>
            <tr>
                <td colspan="4" style="width: 1000px; height: 15px; text-align: center; background-color: #FFFAFA"></td>

            </tr>

            <tr>
                    <td colspan="4" style="width: 1000px; height:15px; text-align: center; background-color: #FFFAFA">
                           <asp:Button ID="bttSubmit" runat="server" Text="Submit" CssClass="bttn w3-grey" OnClick="bttSubmit_Click" />
                    </td>
                </tr>
                 <tr>
                <td colspan="4" style="width: 1000px; height: 15px; text-align: center; background-color: #FFFAFA"></td>

            </tr>
        </table>
    </body>
    </html>
</asp:Content>

