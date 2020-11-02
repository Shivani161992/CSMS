<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="Gunny_Bags_Supplier_Master.aspx.cs" Inherits="GunnyBags_Gunny_Bags_Supplier_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <p>
        <html>
        <head>
            <title></title>
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
        </head>
        <body>
            <table style="width: 800px; background-color: #F8F8F8;" border="1">

                <tr>
                    <td colspan="2" style="width: 800px; flex-align: center; font-family: Cambria; font-size: large; color: #004E4E;">
                        <strong>Gunny Bags Supplier Master
                        </strong>
                    </td>

                </tr>
                <tr>

                    <td colspan="2" style="width: 700px; background-color: #004E4E; height: 20px; flex-align: center"></td>

                </tr>
                <tr>
                    <td colspan="2" style="width: 800px; height: 20px; flex-align: center"></td>
                </tr>
                <tr>
                    <td style="width: 300px; text-align: left; font-family: Cambria; color: #004E4E;">
                        <b>Crop Year</b>
                    </td>
                    <td style="width: 500px; text-align: left;">
                        <asp:DropDownList ID="ddlCropYear" runat="server" Width="200px" AutoPostBack="True" CssClass="tddl" Height="25px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 300px; text-align: left; font-family: Cambria; color: #004E4E;">
                        <b>Gunny Bags Type</b>
                    </td>
                    <td style="width: 300px; text-align: left;">
                        <asp:DropDownList ID="ddlGunnyType" AutoPostBack="true" Width="200px" CssClass="tddl" OnSelectedIndexChanged="ddlGunnyType_SelectedIndexChanged" runat="server">
                            <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>

                            <asp:ListItem Text="Jute(SBT)" Value="Jute"></asp:ListItem>

                            <asp:ListItem Text="PP" Value="PP"></asp:ListItem>
                        </asp:DropDownList>

                    </td>
                </tr>



                <tr>
                    <td style="width: 300px; text-align: left; font-family: Cambria; color: #004E4E;">
                        <b>Supplier Name</b>
                    </td>
                    <td style="width: 500px; text-align: left;">
                        <asp:TextBox ID="txtSupplierName" runat="server" CssClass="tcolumn" Style="width: 500px;"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td style="width: 300px; text-align: left; font-family: Cambria; color: #004E4E;">
                        <b>Supplier State</b>
                    </td>
                    <td style="width: 500px; text-align: left">
                        <asp:DropDownList ID="ddlSuppState" runat="server" Width="300px" AutoPostBack="True" CssClass="tddl" OnSelectedIndexChanged="ddlSuppState_SelectedIndexChanged" Height="25px">
                        </asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td style="width: 300px; text-align: left; font-family: Cambria; color: #004E4E;">
                        <b>Supplier District</b>
                    </td>
                    <td style="width: 500px; text-align: left;">
                        <asp:DropDownList ID="Ddldist" runat="server" Width="300px" AutoPostBack="True" CssClass="tddl" Height="25px">
                        </asp:DropDownList>
                    </td>
                </tr>


                <tr>
                    <td colspan="2" style="width: 800px; height: 20px; flex-align: center"></td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 1000px; height: 15px; text-align: center; background-color: #FFFAFA">
                        <asp:Button ID="bttSubmit" runat="server" Text="Submit" CssClass="bttn w3-grey" OnClick="bttSubmit_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 800px; height: 20px; flex-align: center"></td>
                </tr>

                <tr>
                    <td colspan="2" style="width: 800px; text-align: center; font-family: Cambria; font-size: large; color: #004E4E;">
                        <strong>Gunny Bags Supplier Master Details
                        </strong>
                    </td>

                </tr>
                <tr>
                    <td colspan="2" style="width: 800px; flex-align: center">
                        <div style="height: 200px; width: 800px; overflow: auto;">

                            <asp:GridView ID="GridView1" Width="800px" runat="server" AutoGenerateColumns="False" ShowFooter="True" CellPadding="4" EnableModelValidation="True" ForeColor="#004E4E" GridLines="None"
                                DataKeyNames="SupplierID">
                                <AlternatingRowStyle BackColor="#F8F8F8" />
                                <Columns>


                                    <asp:BoundField DataField="CropYear" HeaderText="Crop Year" />
                                    <asp:BoundField DataField="BagsType" HeaderText="Bags Type " />
                                    <asp:BoundField DataField="Supplier_Name" HeaderText="Supplier Name" />
                                    <asp:BoundField DataField="State_Name" HeaderText="Supplier State" />
                                   
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="iddelete" runat="server" OnClick="lnkdelete_Click" Text="Delete"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#004E4E" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#004E4E" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />

                            </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr>

                    <td colspan="2" style="width: 700px; background-color: #004E4E; height: 20px; flex-align: center"></td>

                </tr>
            </table>
        </body>
        </html>
</asp:Content>

