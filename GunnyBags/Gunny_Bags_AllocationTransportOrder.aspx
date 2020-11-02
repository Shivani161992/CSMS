<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Gunny_Bags_AllocationTransportOrder.aspx.cs" Inherits="GunnyBags_Gunny_Bags_AllocationTransportOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <html>
    <head>
        <style>
            .column {
                height: 15px;
                color: #6B8E23;
                font-family: 'Lucida Fax';
                text-align: center;
                font-size: 35px;
            }

            .tcolumn {
                text-align: center;
                border-radius: 32px;
                font-size: 15px;
                font-family: 'Lucida Fax';
            }

                .tcolumn:focus {
                    box-shadow: 0 0 25px rgb(107,142,35);
                    padding: 3px 0px 3px 3px;
                    margin: 5px 1px 3px 0px;
                    border: 1px solid rgba(81, 203, 238, 1);
                }

            .tddl {
                width: 300px;
                border-radius: 32px;
                height: 25px;
                text-align: center;
                font-family: 'Lucida Fax';
            }

            .bttn {
                width: 400px;
                height: 30px;
                border-radius: 25px;
                color: #fff !important;
                background-color: #000 !important;
                border-radius: 32px;
            }

            .w3-grey, .w3-hover-grey:hover, .w3-gray, .w3-hover-gray:hover {
                color: white !important;
                background-color: #004E4E !important;
                font-family: 'Lucida Fax';
                font-size: 15px;
            }

            .bttn:active {
                background-color: yellow;
            }

            .txt {
            }

                .txt:focus {
                }

            .table {
                background-color: #1B2631;
                width: 500px;
                border-radius: 32px;
                margin-right: 35px;
                text-align: center;
            }

            .bttnew {
                background-color: #004E4E;
                color: white !important;
            }

            .auto-style1 {
                width: 800px;
            }
        </style>
    </head>
    <body>
        <form>
            <table runat="server" style="width: 800px; background-color: #004E4E; color: #FFFAF0;" border="1">
                <tr>
                    <td colspan="2" style="width: 800px; text-align: center; font-size: large; font-family: 'Lucida Fax';">
                        <strong>Gunny Bags Allocation Transport Order </strong>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 800px; height: 15px; text-align: center; background-color: #FFFAFA"></td>
                </tr>
                <tr id="trCropYear" runat="server" visible="true">
                    <td style="width: 400px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                        <strong>Crop Year </strong>
                    </td>
                    <td style="width: 400px; text-align: left; background-color: #FFFAF0">

                        <asp:DropDownList ID="ddlCropYear" AutoPostBack="true" Width="200px" CssClass="tddl" runat="server">
                        </asp:DropDownList>


                    </td>
                </tr>

                <tr>
                    <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                        <strong>Indent </strong>
                    </td>
                    <td style="width: 250px; height: 15px; text-align: left; background-color: #FFFAFA">
                        <asp:DropDownList ID="ddlIndent" AutoPostBack="true" CssClass="tddl" runat="server" Width="200px" OnSelectedIndexChanged="ddlIndent_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 300px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                        <strong>Rail Head </strong>
                    </td>

                    <td style="background-color: #FFFAF0; text-align: left; width: 500px">

                        <asp:DropDownList ID="ddlRailHead"  CssClass="tddl" runat="server" Width="200px"  Enabled ="false">
                        </asp:DropDownList>

                    </td>
                </tr>


                <tr runat="server" id="trGrid">
                    <td colspan="2" style="text-align: center; background-color: #FFFAFA" class="auto-style1">
                        <asp:Panel ID="Panel1" runat="server" Style="overflow-y: scroll" Visible="false" Height="234px">
                            <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="district_name" HeaderText="District" />
                                    <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="Godown_Name" HeaderText="Godown" />
                                    <asp:BoundField ItemStyle-HorizontalAlign="Right" DataField="godownQuantity" HeaderText="Quantity" />
                                    <asp:BoundField ItemStyle-HorizontalAlign="Right" DataField="godownQuantityPercentage" HeaderText="Quantity In Percent" />
                                </Columns>
                                <FooterStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#004E4E" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>

                <tr>
                    <td style="width: 300px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                        <strong>Rail Rack No. </strong>
                    </td>

                    <td style="background-color: #FFFAF0; text-align: left; width: 500px">

                        <asp:DropDownList ID="ddlRRRID"  CssClass="tddl" runat="server" Width="200px" >
                        </asp:DropDownList>
                      <%--  OnSelectedIndexChanged="ddlRRRID_SelectedIndexChanged"--%>
                    </td>
                </tr>

                <tr id="tr1" runat="server" visible="true">
                    <td style="width: 400px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                        <strong>Marketing Season </strong>
                    </td>
                    <td style="width: 400px; text-align: left; background-color: #FFFAF0">

                        <asp:DropDownList ID="ddlMSeason"  Width="200px" CssClass="tddl" runat="server">
                            <asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Rabi" Value="R"></asp:ListItem>
                            <asp:ListItem Text="Kharif" Value="K"></asp:ListItem>
                        </asp:DropDownList>


                    </td>
                </tr>

                <tr>
                    <td style="width: 400px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                        <strong>Bags Type </strong>
                    </td>

                    <td style="background-color: #FFFAF0; text-align: left;">

                        <asp:DropDownList ID="ddlBagsType" CssClass="tddl" runat="server" Width="200px">

                            <asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>

                            <asp:ListItem Text="Jute(SBT)" Value="Jute"></asp:ListItem>

                            <asp:ListItem Text="PP" Value="PP"></asp:ListItem>
                        </asp:DropDownList>

                    </td>
                </tr>

                <tr>
                    <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                        <strong>District </strong>
                    </td>
                    <td style="width: 250px; height: 15px; text-align: left; background-color: #FFFAFA">
                        <asp:DropDownList ID="ddlDistrict" AutoPostBack="true" CssClass="tddl" runat="server" Width="200px" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;" class="auto-style2">
                        <strong>Godown </strong>
                    </td>
                    <td colspan="3" style="width: 250px; height: 15px; text-align: left; background-color: #FFFAFA">
                        <asp:DropDownList ID="ddlGodown" AutoPostBack="true" CssClass="tddl" runat="server" Width="435px" Height="26px"
                            OnSelectedIndexChanged="ddlGodown_selected">
                        </asp:DropDownList>
                    </td>

                </tr>
                
                <tr>
                    <td style="width: 200px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                        <strong>Quantity </strong></td>
                    <td style="width: 200px; text-align: left; background-color: #FFFAF0">

                        <asp:TextBox ID="txtQuantity" Width="200px" CssClass="tcolumn" runat="server"></asp:TextBox>


                    </td>
                </tr>

                 <tr>
                    <td style="text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;" class="auto-style2">
                        <strong>Sector </strong>
                    </td>
                    <td colspan="3" style="width: 250px; height: 15px; text-align: left; background-color: #FFFAFA">
                        <asp:DropDownList ID="ddlSector" CssClass="tddl" AutoPostBack ="true" runat="server" Width="435px" Height="26px" OnSelectedIndexChanged ="ddlSector_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>

                </tr>

                <tr>
                    <td style="text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;" class="auto-style2">
                        <strong>Transporter </strong>
                    </td>
                    <td colspan="3" style="width: 250px; height: 15px; text-align: left; background-color: #FFFAFA">
                        <asp:DropDownList ID="ddlTransporterName" CssClass="tddl" runat="server" Width="435px" Height="26px">
                        </asp:DropDownList>
                    </td>

                </tr>
                <tr>
                    <td colspan="2" style="width: 800px; height: 15px; text-align: center; background-color: #FFFAFA"></td>
                </tr>

                <tr>
                    <td colspan="2" style="width: 800px; height: 15px; text-align: center; background-color: #FFFAFA"></td>
                </tr>

                <tr>
                    <td colspan="2" style="width: 800px; height: 15px; text-align: center; background-color: #FFFAFA">
                        <asp:Button ID="bttSubmit" runat="server" Text="Submit" CssClass="bttn w3-grey" OnClick="bttSubmit_Click" />
                    </td>
                </tr>

                <tr>
                    <td colspan="2" style="width: 800px; height: 15px; text-align: left; background-color: #FFFAFA">&nbsp;</td>
                </tr>

                <tr>
                    <td colspan="2" style="width: 800px; height: 15px; text-align: center; font-size: large; font-family: 'Lucida Fax';"></td>
                </tr>



            </table>
        </form>
    </body>
    </html>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

