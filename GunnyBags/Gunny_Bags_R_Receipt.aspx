<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Gunny_Bags_R_Receipt.aspx.cs" Inherits="GunnyBags_Gunny_Bags_R_Receipt" %>

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
                 color:black;
                  font-weight:500;
                 
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
                font-weight:500;
                color:black;
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
        </style>
        <link href="../PaddyMilling/calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
        <script type="text/javascript" lang="" src="../PaddyMilling/calendar/calendarLot.js"></script>
    </head>
    <body>
        <table id="Table1" runat="server" style="width: 1000px; background-color: #004E4E; color: #FFFAF0;" border="1">
            <tr>
                <td colspan="4" style="width: 1000px; text-align: center; font-size: large; font-family: 'Lucida Fax';">
                    <strong>Gunny Bags Railway Receipt </strong>
                </td>
            </tr>
            <tr runat="server" id="trID" visible="false">
                <td colspan="4" style="background-color:white; color:red; font-size:x-large; font-family: 'Lucida Fax';">
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
            </tr>

            <tr>
                <td colspan="4" style="width: 1000px; height: 15px; text-align: center; background-color: #FFFAFA"></td>
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
                    <asp:DropDownList ID="ddlCropYear" AutoPostBack="true" Width="200px" CssClass="tddl" runat="server" >
                    </asp:DropDownList>
                </td>
             </tr>

            <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Indent Number </strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:DropDownList ID="ddlIndentorNum" AutoPostBack="true" Width="250px" CssClass="tddl" runat="server" OnSelectedIndexChanged="ddlIndentorNum_SelectedIndexChanged" >
                    </asp:DropDownList>
                </td>
                 <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Rail Head </strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:DropDownList ID="ddlRailHead" AutoPostBack="true" Width="250px" CssClass="tddl" runat="server"  OnSelectedIndexChanged="ddlRailHead_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                
            </tr>
            <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Quantity (Bales)</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtQty" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>

                </td>
               <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Date Of Delivery</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtDateOfDelivery" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>

                </td>
                </tr>

            <tr>
                <td  style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Bags Type </strong>
                </td>
                <td  style="width:250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:DropDownList ID="ddlBagsType" AutoPostBack="true" CssClass="tddl" runat="server" Width="200px" Enabled="false"  >
                            
                    <%--<asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>--%>

                    <asp:ListItem Text="Jute(SBT)" Value="Jute" Selected="true"></asp:ListItem>

                    <asp:ListItem Text="PP" Value="PP"></asp:ListItem>                            
                        </asp:DropDownList>
                </td>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Date Of Receipt </strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">

                    <asp:TextBox ID="txtDateofReceipt" CssClass="tcolumn" Width="200px" Enabled="true" runat="server"></asp:TextBox>
                    <%--<img src="calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtToDate' , 'restrict=true,instance=single,limit=<%= DateLimit %>')" />--%>
                    <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtDateofReceipt' , 'expiry=true,elapse=-150,restrict=false,close=true')" />

                </td>


            </tr>

           
            <tr>
                <td colspan="4" style="width: 1000px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:Button ID="bttAddRR" runat="server" CssClass="bttnnew w3-grey" Text="New RR" OnClick="bttAddRR_Click" />

                </td>
            </tr>
             <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Supplier Name </strong>
                </td>
                <td colspan="3" style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:DropDownList ID="ddlSupplierName" AutoPostBack="true" Width="750px" CssClass="tddl" runat="server" >
                    </asp:DropDownList>
                </td>

            </tr>
            <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Railway Receipt(RR) No. </strong>
                </td>

                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtRailwayReceipt" CssClass="tcolumn" Width="250px" Enabled="true" runat="server"></asp:TextBox>
                </td>

                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>RR Number Qty(Bales) </strong>
                </td>

                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtRRQuantity" CssClass="tcolumn" Width="250px" Enabled="true" runat="server" AutoPostBack="true" OnTextChanged="txtRRQuantity_TextChanged"></asp:TextBox>


                </td>
            </tr>

            <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Wagon Number</strong>
                </td>

                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtWagonNo" CssClass="tcolumn" Width="250px" Enabled="true" runat="server"></asp:TextBox>
                </td>

                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Wagon Qty(Bales) </strong>
                </td>

                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtWagonQuantity" CssClass="tcolumn" Width="200px" Enabled="true" runat="server" OnTextChanged="txtWagonQuantity_TextChanged" AutoPostBack="true"></asp:TextBox>
                   
                </td>
            </tr>
            <tr>
               <td colspan="2" style="width: 250px; text-align: right; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong> Received Wagon Qty(Bales) </strong>
                </td>

                <td colspan="2" style="width: 250px; height: 15px; text-align: left; background-color: #FFFAFA">
                    <asp:TextBox ID="txtRcdWagQty" CssClass="tcolumn" Width="200px" Enabled="true" runat="server" OnTextChanged="txtRcdWagQty_TextChanged" AutoPostBack="true"></asp:TextBox>
                     <asp:Button ID="bttAdd" runat="server" Text="Add" OnClick="bttAdd_Click" />
                </td>
                
            </tr>



            <tr>
                <td colspan="4" style="width: 1000px; height: 15px; text-align: center; background-color: #FFFAFA"></td>
            </tr>

           
            <tr runat="server" id="trGrid" visible="false">
                <td colspan="4" style="width: 1000px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False" ShowFooter="True" CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                        OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField ReadOnly="true" HeaderText="S. No." />
                            
                             <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" />
                            <asp:BoundField DataField="fromdisttext" HeaderText="RR No." />
                            <asp:BoundField DataField="todisttext" HeaderText="RR Qty" />
                            <asp:BoundField DataField="fromdistval" HeaderText="Wagon No" />
                            <asp:BoundField DataField="quantity" HeaderText="Wagon Qty" />
                             <asp:BoundField DataField="RCDquantity" HeaderText="Recd Wagon Qty" />
                           

                            <asp:TemplateField HeaderText="Remove">
                                <ItemTemplate>
                                    <asp:LinkButton ID="iddelete" runat="server" CausesValidation="false" CommandName="Delete" OnClientClick="return confirm('Are You Sure You Want To Remove?');" Text="Remove"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#666666" Font-Bold="True" ForeColor="White" HorizontalAlign="center" VerticalAlign="Middle" />
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
                  <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Total Quantity</strong>
                </td>

                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txttotalquantity" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>
                </td>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Total Received Qty (Bales)</strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: left; background-color: #FFFAFA">
                    <asp:TextBox ID="txtTotalRec_Qty" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>


                </td>
            </tr>
            <tr>
                <td colspan="4" style="width: 1000px; height: 15px; text-align: left; background-color: #FFFAFA">
                     
                </td>

            </tr>

            <tr>
                <td colspan="4" style="width: 1000px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:Button ID="bttSubmit" runat="server" Text="Submit" CssClass="bttn w3-grey" Enabled="false" OnClick="bttSubmit_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 1000px; height: 15px; text-align: left; background-color: #FFFAFA">
                     <asp:Button ID="bttNew" runat="server" Text="New"  OnClick="bttNew_Click" />
                </td>
                 <td colspan="2" style="width: 1000px; height: 15px; text-align: right; background-color: #FFFAFA">
                     <asp:Button ID="bttClose" runat="server" Text="Close"  OnClick="bttClose_Click" />
                </td>


            </tr>
        </table>
    </body>
    </html>
</asp:Content>
