<%@ Page Language="C#" MasterPageFile="~/MasterPage/AdminLogin.master" AutoEventWireup="true" CodeFile="UpdateReceipt.aspx.cs" Inherits="Admin_UpdateReceipt" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript" language ="javascript ">
        function SelectAll(id)
        {
            //get reference of GridView control
            var grid = document.getElementById("<%= GridView2.ClientID %>");
            //variable to contain the cell of the grid
            var cell;
            
            if (grid.rows.length > 0)
            {
                //loop starts from 1. rows[0] points to the header.
                for (i=1; i<grid.rows.length; i++)
                {
                    //get the reference of first column
                    cell = grid.rows[i].cells[0];
                    
                    //loop according to the number of childNodes in the cell
                    for (j=0; j<cell.childNodes.length; j++)
                    {           
                        //if childNode type is CheckBox                 
                        if (cell.childNodes[j].type =="checkbox")
                        {
                        //assign the status of the Select All checkbox to the cell checkbox within the grid
                            cell.childNodes[j].checked = document.getElementById(id).checked;
                        }
                    }
                }
            }
        }
    </script>
 <table border="0" cellpadding="0" cellspacing="0" style="width: 623px">
     <tr>
         <td class="tdmarginro" colspan="6" style="border-right: white 1px solid; border-top: white 1px solid;
             border-left: white 1px solid; color: black; border-bottom: white 1px solid; background-color: lightblue">
             <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Get Details"
                 Width="175px" /></td>
         <td colspan="4" style="border-right: white 1px solid; border-top: white 1px solid;
             border-left: white 1px solid; color: black; border-bottom: white 1px solid; background-color: lightblue">
         </td>
     </tr>
                                       <tr>
                                           <td class="tdmarginro" colspan="6" style="border-right: white 1px solid; border-top: white 1px solid;
                                               border-left: white 1px solid; color: black; border-bottom: white 1px solid; background-color: lightblue">
                                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"  Width="576px" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" OnRowDataBound ="GridView2_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None">
            <Columns>
                <asp:TemplateField>
                                
                     <AlternatingItemTemplate>
                        <asp:CheckBox  ID="cbSelectAll"  runat="server" AutoPostBack ="false" />
                    </AlternatingItemTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="cbSelectAll" runat="server"  AutoPostBack ="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Rdist" HeaderText="Recd Dist" SortExpression="Rdist" >
                    <ItemStyle CssClass="griditemlaro" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="Rrono" HeaderText="RO No." SortExpression="Rrono" >
                    <ItemStyle Font-Size="10pt" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="Receipt_ID" HeaderText="ReceiptID" SortExpression="Receipt_ID" >
                    <ItemStyle CssClass="griditemlaro" Font-Size="8pt" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="Rcomm" HeaderText="Recd Comm" SortExpression="Rcomm" >
                    <ItemStyle CssClass="griditemlaro" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="Rsch" HeaderText="Recd Sch." SortExpression="Rsch" >
                    <ItemStyle CssClass="griditemlaro" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="ROdist" HeaderText="RO Dist" SortExpression="ROdist" >
                    <ItemStyle CssClass="griditemlaro" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="ROrono" HeaderText="RO RONO" SortExpression="ROrono" >
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="ROcomm" HeaderText="RO Comm" SortExpression="ROcomm">
                    <ItemStyle CssClass="griditemlaro" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="ROsch" HeaderText="RO Sch" SortExpression="ROsch">
                    <ItemStyle CssClass="griditemlaro" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
            </Columns>
                                                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                    <AlternatingRowStyle BackColor="White" />
        </asp:GridView></td>
                                           <td colspan="4" style="border-right: white 1px solid; border-top: white 1px solid;
                                               border-left: white 1px solid; color: black; border-bottom: white 1px solid; background-color: lightblue">
                                           </td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="border-right: white 1px solid; border-top: white 1px solid;
                                               border-left: white 1px solid; color: black; border-bottom: white 1px solid; background-color: lightblue">
                                               <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></td>
                                           <td class="tdmarginro" colspan="2" style="border-right: white 1px solid; border-top: white 1px solid;
                                               border-left: white 1px solid; color: black; border-bottom: white 1px solid; background-color: lightblue">
                                           </td>
                                           <td class="tdmarginro" style="border-right: white 1px solid; border-top: white 1px solid;
                                               border-left: white 1px solid; color: black; border-bottom: white 1px solid; background-color: lightblue">
                                               </td>
                                           <td colspan="2" style="border-right: white 1px solid; border-top: white 1px solid;
                                               border-left: white 1px solid; color: black; border-bottom: white 1px solid; background-color: lightblue">
                                           </td>
                                           <td colspan="4" style="border-right: white 1px solid; border-top: white 1px solid;
                                               border-left: white 1px solid; color: black; border-bottom: white 1px solid; background-color: lightblue">
                                           </td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; color: black; border-bottom: white 1px solid; background-color: lightblue">
                                               </td>
                                           <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; color: black; border-bottom: white 1px solid; background-color: lightblue" >
                                               </td>
                                           <td align="right" colspan="2" style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; color: black; border-bottom: white 1px solid; background-color: lightblue">
                                               &nbsp;<asp:Button ID="btnsubmit" runat="server" Text="Update Recorde" OnClick="btnsubmit_Click" Width="154px" ValidationGroup="1" /></td>
                                           <td class="tdmarginro" style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                               border-bottom: white 1px solid; color: black; background-color: lightblue;">
                                               <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Close" Width="89px" /></td>
                                           <td align="left" colspan="1" style="border-right: white 1px solid; border-top: white 1px solid;
                                               border-left: white 1px solid; border-bottom: white 1px solid; color: black; background-color: lightblue;">
                                               </td>
                                           <td colspan="4" style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; color: black; border-bottom: white 1px solid; background-color: lightblue;">
                                           </td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                               border-bottom: white 1px solid; color: black; background-color: lightblue;">
                                           </td>
                                           <td align="left" colspan="3" style="border-right: white 1px solid; border-top: white 1px solid;
                                               border-left: white 1px solid; border-bottom: white 1px solid; color: black; background-color: lightblue;">
                                           </td>
                                           <td class="tdmarginro" colspan="2" style="border-right: white 1px solid; border-top: white 1px solid;
                                               border-left: white 1px solid; border-bottom: white 1px solid; color: black; background-color: lightblue;">
                                               <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                                   Width="199px" ShowSummary="False" ValidationGroup="1" />
                                           </td>
                                           <td colspan="4" style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; color: black; border-bottom: white 1px solid; background-color: lightblue;">
                                           </td>
                                       </tr>
                                </table>
</asp:Content>

