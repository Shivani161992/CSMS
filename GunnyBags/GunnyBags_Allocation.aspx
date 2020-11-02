<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="GunnyBags_Allocation.aspx.cs" 
    Inherits="GunnyBags_GunnyBags_Allocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <html>
    <head>
        <style type="text/css">
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
                border-radius: 32px;
                text-align: center;
                font-family: 'Lucida Fax';
            }

            .bttn
            {
                width: 400px;
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
            .auto-style2 {
                width: 379px;
            }
            .auto-style3 {
                width: 202px;
            }
        </style>
        <link href="../PaddyMilling/calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
        <script type="text/javascript" lang="" src="../PaddyMilling/calendar/calendarLot.js"></script>
    </head>
    <body>
        <table runat="server" style="width: 1000px; background-color: #004E4E; color: #FFFAF0;" border="1">

            <tr>
                <td colspan="6" style="width: 1000px; text-align: center; font-size: large; font-family: 'Lucida Fax';">
                    <strong>Gunny Bags Godown  Allocation </strong>
                </td>
            </tr>
            <tr>
                <td colspan="6" style="width: 1000px; height: 15px; text-align: center; background-color: #FFFAFA"></td>
            </tr>
            <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Indent </strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                      <asp:DropDownList ID="ddlIndent" AutoPostBack="true" CssClass="tddl" runat="server" Width="200px" OnSelectedIndexChanged="ddlIndent_SelectedIndexChanged">

                       
                    </asp:DropDownList>
                </td>

               <td  style="text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;" class="auto-style2">
                    <strong>Rail Head </strong>
                </td>
                <td  colspan="3" style="width: 250px; height: 15px; text-align: left; background-color: #FFFAFA">
                    &nbsp;
                    <asp:DropDownList ID="ddlRailHead" AutoPostBack="true" CssClass="tddl" runat="server" Width="200px" OnSelectedIndexChanged="ddlRailHead_SelectedIndexChanged" >

                       
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Fund </strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                    <asp:TextBox ID="txtFund" CssClass="tcolumn" Width="200px" Enabled="false" runat="server"></asp:TextBox>
                </td>

               <td style="text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;" class="auto-style2">
                    <strong>Quantity </strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                   <asp:TextBox ID="txtQuantity" CssClass="tcolumn" Width="200px" Enabled="false" runat="server"></asp:TextBox>
                </td>

                
            </tr>
           <tr>
                <td style="text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;" class="auto-style3">
                    <strong>Date </strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                   <asp:TextBox ID="txtRailHeadDate" CssClass="tcolumn" Width="200px" Enabled="false" runat="server"></asp:TextBox>
                </td>
               <td colspan="2" style="text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;" class="auto-style3"></td>
               
           </tr>
            <tr>
                <td colspan="6" style="width: 1000px; height: 15px; text-align: center; background-color: #FFFAFA"></td>

            </tr>
            <tr>
                <td colspan="6" style="width: 1000px; text-align: center; font-size: large; font-family: 'Lucida Fax';">
                    <strong>Dispatch Details</strong>
                    <asp:Label ID="lbldespatchMode" Font-Size="large" Font-Bold="true" runat="server" Text="Rail"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="6" style="width: 1000px; height: 15px; text-align: center; background-color: #FFFAFA"></td>

            </tr>

            <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>District </strong>
                </td>
                <td style="width: 250px; height: 15px; text-align: center; background-color: #FFFAFA">
                      <asp:DropDownList ID="ddlDistrict" AutoPostBack="true" CssClass="tddl" runat="server" Width="200px"  OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" >

                       
                    </asp:DropDownList>
                </td>

                 <td  style="text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;" class="auto-style2">
                    <strong>Godown </strong>
                </td>
                <td  colspan="3" style="width: 250px; height: 15px; text-align: left; background-color: #FFFAFA">
                    <asp:DropDownList ID="ddlGodown" AutoPostBack="true"  CssClass="tddl" runat="server" Width="435px" Height="26px"  OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged">
                       
                    </asp:DropDownList>
                </td>
            
            </tr>

           
            <tr>
                <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                    <strong>Quantity </strong>
                </td>
                <td  style="height: 15px; text-align: center; background-color: #FFFAFA">
                     <asp:TextBox ID="txtGodownQuantity" CssClass="tcolumn" Width="200px" Enabled="false" runat="server"></asp:TextBox>
                  
                </td>
                
                <td colspan="4" style="width: 250px; height: 15px; text-align: left; background-color: #FFFAFA">                 
                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                 
                      <asp:Button ID="bttAdd" runat="server" Text="Add"  OnClick="bttAdd_Click" Height="29px" Width="118px" /> 

                </td>
            </tr>

           

         
            <tr>
                <td colspan="6" style="width: 1000px; height: 15px; text-align: center; background-color: #FFFAFA"></td>

            </tr>
            <tr runat="server" id="trGrid" visible="false">
                    <td colspan="6" style="width: 1000px; height:15px; text-align: center; background-color: #FFFAFA">
                         <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False" ShowFooter="True"  CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                      OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting" >
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField ReadOnly="true" HeaderText="S. No." />
                        <asp:BoundField DataField="District" HeaderText="District" />                                           
                        <asp:BoundField DataField="Godown" HeaderText="Godown Name" />                        
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                         <asp:BoundField DataField="QuantityinPercent" HeaderText="Quantity in Percent" />
                        <asp:TemplateField HeaderText="Remove">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfGodownDistrictID" runat="server" Value='<%#Eval("hfGodownDistrictID") %>' />
                                <asp:HiddenField ID="hfGodownID" runat="server" Value='<%#Eval("hfGodownID") %>' />
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
                <td colspan="6" style="width: 1000px; height: 15px; text-align: center; background-color: #FFFAFA"></td>

            </tr>

            <tr>
                    <td colspan="6" style="width: 1000px; height:15px; text-align: center; background-color: #FFFAFA">
                           <asp:Button ID="bttSubmit" runat="server" Text="Submit" CssClass="bttn w3-grey" OnClick="bttSubmit_Click" Height="33px" Width="273px" />
                    </td>
                </tr>
                 <tr>
                <td colspan="6" style="width: 1000px; height: 15px; text-align: center; background-color: #FFFAFA"></td>

            </tr>
        </table>
    </body>
    </html>
</asp:Content>

