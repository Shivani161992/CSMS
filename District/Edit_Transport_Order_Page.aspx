<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Edit_Transport_Order_Page.aspx.cs" Inherits="District_Edit_Transport_Order_Page" Title="Edit Transport Order Page" %>
 
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
        function SelectAll(id)
        {
            //get reference of GridView control
            var grid = document.getElementById("<%= dgridchallan.ClientID %>");
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
 <table cellpadding ="0" cellspacing ="0" border ="0" style="width: 622px">
                    <tr>
                        <td align="center" colspan="6" style="border-right: 1px solid; border-top: 1px solid;
                            border-left: 1px solid; color: white; border-bottom: 1px solid; background-color: lightslategray">
                            &nbsp;&nbsp;<asp:Label ID="Label3" runat="server" Text="Edit Transport Order Details"></asp:Label>
                            <strong style="color: navy"><strong style="color: navy">&nbsp;</strong></strong>&nbsp;
                            &nbsp; &nbsp;&nbsp;
                        </td>
                    </tr>
     <tr>
         <td class="tdmarginro" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; height: 26px; background-color: #cfdcdc">
         </td>
         <td style="border-right: silver 1px solid; border-top: silver 1px solid; font-size: 10pt;
             border-left: silver 1px solid; border-bottom: silver 1px solid; position: static;
             height: 26px; background-color: #cfdcdc">
         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             color: white; border-bottom: 1px solid; height: 26px; background-color: lightslategray">
             <asp:Label ID="Label4" runat="server" Text="District Logged in" Width="119px"></asp:Label></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             color: white; border-bottom: 1px solid; height: 26px; background-color: lightslategray">
             <asp:TextBox ID="txtdistrict" runat="server" Width="106px" BackColor="ActiveCaption"></asp:TextBox></td>
         <td class="tdmarginro" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; height: 26px; background-color: #cfdcdc">
         </td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; height: 26px; background-color: #cfdcdc">
         </td>
     </tr>
                    <tr>
                       
                          
                                
                                
                                        <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            RO No.</td>
                                        <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            <asp:DropDownList ID="ddlrono" runat="server" Width="110px" OnSelectedIndexChanged="ddlrono_SelectedIndexChanged"
                                                AutoPostBack="True" BackColor="ActiveCaption">
                                                
                                            </asp:DropDownList>
                                        </td>
                                        <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            RO Qty.</td>
                                        <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
                                            <asp:TextBox ID="txtroqty" runat="server" Width="110px" BackColor="ActiveCaption" ></asp:TextBox>&nbsp;</td>
                                        <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            Validity Date</td>
                                        <td class="tdmarginro" align ="left" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" >
                                            <asp:TextBox ID="txtrodate" runat="server" Width="100px" BackColor="ActiveCaption" ></asp:TextBox></td>
                                    </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             &nbsp; &nbsp;&nbsp;
         </td>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             &nbsp; &nbsp;&nbsp;
         </td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             Commodity</td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:TextBox ID="txtcommodity" runat="server" Width="110px" BackColor="ActiveCaption"></asp:TextBox></td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             Scheme</td>
         <td align="left" class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:TextBox ID="txtscheme" runat="server" Width="100px" BackColor="ActiveCaption"></asp:TextBox></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             &nbsp; &nbsp; &nbsp;
         </td>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             &nbsp; &nbsp;
         </td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             Cumulative Lifted Qty.</td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:TextBox ID="txtcumlqty" runat="server" Width="110px" BackColor="ActiveCaption"></asp:TextBox></td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             Pending Qty for T.O.</td>
         <td align="left" class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            <asp:TextBox ID="txtbalqty" runat="server" ReadOnly="True" Width="100px" BackColor="ActiveCaption"></asp:TextBox></td>
     </tr>
     <tr>
         <td align="left" class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            <asp:Label ID="Label2" runat="server" Visible="False"></asp:Label></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
         </td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:Button ID="btnclose" runat="server" OnClick="btnclose_Click" Text="Close " Width="118px" /></td>
         <td class="tdmarginro" colspan="2" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
         </td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
         </td>
     </tr>
                                    <tr>
                                        <td class="tdmarginddl" colspan="6">
                                            &nbsp;
                                            </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="6">
                                            <asp:GridView ID="dgridchallan" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="dgridchallan_SelectedIndexChanged" OnRowDataBound="dgridchallan_RowDataBound"
          AllowPaging="True" PageSize="5" PagerSettings-Visible ="true"  CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="dgridchallan_PageIndexChanging" Width="623px"  >
        <HeaderStyle  CssClass="gridheader" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"   />
        <Columns>
            <asp:CommandField ShowSelectButton="True" HeaderText="Action" SelectText="Update" >
                <ItemStyle Font-Size="11px" />
                <HeaderStyle Font-Size="12px" />
            </asp:CommandField>
            <asp:BoundField DataField="TO_Number" HeaderText="T.O. No." ReadOnly="True" SortExpression="TO_Number" >
                <ItemStyle Font-Size="11px" />
                <HeaderStyle Font-Size="12px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="T.O. Date">
     
                <ItemTemplate>
                <asp:Label ID="lblChallan" runat="server" 
                Text='<%# Eval("TO_Date").ToString()%>'>
                </asp:Label>
                </ItemTemplate>
               <HeaderStyle Font-Size="12px" />
                <ItemStyle Font-Size="11px" />
                 </asp:TemplateField>
            <asp:BoundField DataField="Quantity" HeaderText="T.O. Qty." SortExpression="Quantity" >
                <ItemStyle Font-Size="11px" />
                <HeaderStyle Font-Size="12px" />
            </asp:BoundField>
            <asp:BoundField DataField="Cumulative_Qty" HeaderText="Lifted Qty." SortExpression="Cumulative_Qty">
                <ItemStyle Font-Size="11px" />
                <HeaderStyle Font-Size="12px" />
            </asp:BoundField>
            <asp:BoundField DataField="Pending_Qty" HeaderText="Pending Qty." SortExpression="Pending_Qty">
                <ItemStyle Font-Size="11px" />
                <HeaderStyle Font-Size="12px" />
            </asp:BoundField>
            <asp:BoundField DataField="DepotName" HeaderText="for Depot" SortExpression="DepotName">
                <ItemStyle Font-Size="11px" />
                <HeaderStyle Font-Size="12px" />
            </asp:BoundField>
            
            <asp:BoundField DataField="Tname" HeaderText="Transporter Name" ReadOnly="True" SortExpression="Tname" >
                <ItemStyle Font-Size="11px" />
                <HeaderStyle Font-Size="12px" />
            </asp:BoundField>
            <asp:BoundField DataField="Trunsuction_Id" HeaderText="ID" SortExpression="Trunsuction_Id">
                <ItemStyle Font-Size="0px" />
                <HeaderStyle Font-Size="0px" />
            </asp:BoundField>
            <asp:BoundField DataField="RO_No" HeaderText="RO" SortExpression="RO_No">
                <HeaderStyle Font-Size="0px" />
                <ItemStyle Font-Size="0px" />
            </asp:BoundField>
           
        </Columns>
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" Font-Bold="True" />
                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <EditRowStyle BackColor="#999999" />
    </asp:GridView>
    </td>
                                    </tr>
                                
        <asp:Label ID="Label1" runat="server"></asp:Label></table> 
         
        <div>
        <table border ="0" cellpadding ="0" cellspacing ="0" class="gridfooter">
        <tr>
                        <td>
                            <div >
                                <asp:LinkButton ID="Firstbutton" Text='<img border="0" src="../images/firstpage.gif" align="absmiddle">'
                                    CommandArgument="0" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false"  />&nbsp;
                                <asp:LinkButton ID="Prevbutton" Text='<img border="0" src="../images/prevpage.gif" align="absmiddle">'
                                    CommandArgument="prev" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" />&nbsp;
                                <asp:LinkButton ID="Nextbutton" Text='<img border="0" src="../images/nextpage.gif" align="absmiddle">'
                                    CommandArgument="next" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" />&nbsp;
                                <asp:LinkButton ID="Lastbutton" Text='<img border="0" src="../images/lastpage.gif" align="absmiddle">'
                                    CommandArgument="last" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" />&nbsp;&nbsp;
                             
                            </div>
                        </td>
                    </tr>
        </table>
        </div>
        <script type="text/javascript">
function CheckIsNumeric(tx)
{
var AsciiCode = event.keyCode;
var txt=tx.value;
var txt2 = String.fromCharCode(AsciiCode);
var txt3=txt2*1;
if ((AsciiCode < 46) || (AsciiCode > 57))
{
alert('Please enter only numbers.');
event.cancelBubble = true;
event.returnValue = false;
}

var num=tx.value;
var len=num.length;
var indx=-1;
indx=num.indexOf('.');
if (indx != -1)
{
var dgt=num.substr(indx,len);
var count= dgt.length;
//alert (count);
if (count > 5)  
{
 alert("Only 5 decimal digits allowed");
 event.cancelBubble = true;
 event.returnValue = false;
}
}

}



    </script>
</asp:Content>

