<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Gunny_Bags_Opening_Bal.aspx.cs" Inherits="GunnyBags_Gunny_Bags_Opening_Bal" %>

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
            text-align: center;
            border-radius: 32px;
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
            width:300px;
            border-radius: 32px;
            height:25px;
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
            .bttnew
            {
                background-color:#004E4E;
                color: white!important;
            }

            </style>
    </head>
    <body>
        <form>
            <table runat="server" style="width: 800px; background-color: #004E4E; color: #FFFAF0;" border="1">
                <tr>
                    <td colspan="2" style="width: 800px; text-align: center; font-size: large; font-family: 'Lucida Fax';">
                        <strong>Gunny Bags Opening Balance </strong>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 800px; height:15px; text-align: center; background-color: #FFFAFA"></td>
                </tr>
                <tr>
                    <td style="width: 300px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                        <strong>District </strong>
                    </td>

                      <td style=" background-color: #FFFAF0; text-align:left; width:500px">

                        <asp:TextBox ID="txtDist" CssClass="tcolumn" Width="200px" Enabled="false" runat="server"></asp:TextBox>
                       
                    </td>
                </tr>
                <tr>
                     <td style="width: 300px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                        <strong>Society </strong>
                    </td>

                     <td style="width: 500px; background-color: #FFFAF0; text-align: left;">
                        <asp:DropDownList ID="ddlsociety"  CssClass="tddl" Width="500px" runat="server"  AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>
                

                <tr>
                    <td style="width: 400px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                        <strong>Bags Type </strong>
                    </td>

                      <td style=" background-color: #FFFAF0; text-align:left;">

                        <asp:DropDownList ID="ddlBagsType" AutoPostBack="true" CssClass="tddl" runat="server" Width="200px" OnSelectedIndexChanged="ddlBagsType_SelectedIndexChanged">
                            
                    <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>

                    <asp:ListItem Text="Jute(SBT)" Value="Jute"></asp:ListItem>

                    <asp:ListItem Text="PP" Value="PP"></asp:ListItem>                            
                        </asp:DropDownList>
                       
                    </td>
                </tr>

                 <tr>
                    <td colspan="2" style="width: 800px; height:15px; text-align: center; background-color: #FFFAFA"></td>
                </tr>


                
                <tr runat="server" id="trbagtype" visible="false">
                    <td colspan="2" style="text-align: center; background-color: #004E4E; font-size: medium; font-family: 'Lucida Fax'; color: #FFFAF0;">
                        <table id="Table1" runat="server" style="width: 800px; background-color: #004E4E; color: #FFFAF0;" border="1">

                            <tr>
                                <td style="width:200px">
                       <asp:RadioButton ID="rdbNew" runat="server"  GroupName="Gunny" style="font-weight:700; color: #FFFAF0" AutoPostBack="true" OnCheckedChanged="rdbNew_CheckedChanged" Text="New"  />

                                </td>
                                 <td style="width:400px">
                                       <asp:RadioButton ID="rdbCuttorn" Visible="false" runat="server" GroupName="Gunny" style="font-weight:700; color:#FFFAF0" AutoPostBack="true" OnCheckedChanged="rdbCuttorn_CheckedChanged" Text="Cut & Torn" />

                                </td>
                                 <td style="width:200px">
                                        <asp:RadioButton ID="rdbOld" runat="server" Visible="false" GroupName="Gunny" style="font-weight:700; color:#FFFAF0" AutoPostBack="true" OnCheckedChanged="rdbOld_CheckedChanged" Text="Old" />

                                </td>
                                
                            </tr>
                        </table>
                        </td>
                         
                   

                </tr>
                  <tr>
                    <td colspan="2" style="width: 800px; height:15px; text-align: center; background-color: #FFFAFA"></td>
                </tr>

                <tr id="trCropYear" runat="server" visible="false">
                    <td style="width: 400px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                        <strong>Crop Year </strong>
                    </td>
                     <td  style="width: 400px; text-align: left; background-color: #FFFAF0">

                        <asp:DropDownList ID="ddlCropYear" AutoPostBack="true" Width="200px" CssClass="tddl" runat="server" >             
                        </asp:DropDownList>


                    </td>
                     </tr>

                <tr>
                    <td style="width: 400px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                        <strong>Gunny Bags Per Bundle </strong>
                    </td>

                 <td style="width: 400px; background-color: #FFFAF0; text-align:left;">
                                                <asp:DropDownList ID="ddlBundleType" Width="200px" AutoPostBack="true" CssClass="tddl" runat="server" OnSelectedIndexChanged="ddlBundleType_SelectedIndexChanged"></asp:DropDownList>



                    </td>
                    </tr>
                <tr>
                     <td style="width: 400px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                        <strong>No. Of Bundle </strong>
                    </td>
                    <td style="width: 400px; background-color: #FFFAF0; text-align:left;">
                                                <asp:TextBox ID="txtNoOfBundle" Width="200px" OnTextChanged="txtNoOfBundle_TextChanged" AutoPostBack="true" CssClass="tcolumn" runat="server"></asp:TextBox>

                    </td>
                
                </tr>
                <tr>
                    <td  style="width: 200px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                        <strong>Quantity </strong></td>
                    <td  style="width: 200px; text-align: left; background-color: #FFFAF0">

                                                                        <asp:TextBox ID="txtQuantity" Width="200px" CssClass="tcolumn" runat="server"></asp:TextBox>                                               <asp:Button ID="bttAdd" runat="server" Text="Add"  OnClick="bttAdd_Click" />


                    </td>
                    </tr>
                 <tr>
                    <td colspan="2" style="width: 800px; height:15px; text-align: center; background-color: #FFFAFA"></td>
                </tr>
                 <tr runat="server" id="trGrid" visible="false">
                    <td colspan="2" style="width: 800px; height:15px; text-align: center; background-color: #FFFAFA">
                         <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False" ShowFooter="True"  CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                      OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting" >
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField ReadOnly="true" HeaderText="S. No." />
                        <asp:BoundField DataField="fromdisttext" HeaderText="Crop Year" />
                        <asp:BoundField DataField="fromdistval" HeaderText="Gunny Bags per Bundle" />
                         <asp:BoundField DataField="todisttext" HeaderText="No. Of Bundles" />
                         <asp:BoundField DataField="todistval" HeaderText="Quantity" />
                        <asp:TemplateField HeaderText="Remove">
                            <ItemTemplate>
                                <asp:LinkButton ID="iddelete" runat="server" CausesValidation="false" CommandName="Delete" OnClientClick="return confirm('Are You Sure You Want To Remove?');"  Text="Remove"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#004E4E" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                </asp:GridView>

                    </td>
                </tr>
                 <tr>
                    <td colspan="2" style="width: 800px; height:15px; text-align: center; background-color: #FFFAFA"></td>
                </tr>

                 <tr>
                    <td colspan="2" style="width: 800px; height:15px; text-align: center; background-color: #FFFAFA">
                           <asp:Button ID="bttSubmit" runat="server" Text="Submit" CssClass="bttn w3-grey" OnClick="bttSubmit_Click" />
                    </td>
                </tr>
                    
                  <tr>
                    <td colspan="2" style="width: 800px; height:15px; text-align: left; background-color: #FFFAFA">
                          <asp:Button ID="bttNew" runat="server" Text="New"  OnClick="bttNew_Click" />

                    </td>
                </tr>

                <tr>
                    <td colspan="2" style="width: 800px; height:15px; text-align: center; font-size: large; font-family: 'Lucida Fax';">
                       
                    </td>
                </tr>



            </table>
        </form>
    </body>
    </html>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

