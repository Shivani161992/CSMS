<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Transport_Order.aspx.cs" Inherits="District_Transport_Order" Title="Rransport Order" %>
 
 
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
                        <td  style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; height: 21px;">
                            &nbsp;&nbsp;
                        </td>
                        <td  style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; color: olive; height: 21px;" align="center" colspan="4">
                            <strong>Quantity Pending to be Supplied Against FCI R.O.<strong>&nbsp;</strong></strong></td>
                        <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; height: 21px;">
                            &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                        </td>
                    </tr>
     <tr>
         <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; height: 21px">
         </td>
         <td align="center" colspan="4" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; color: olive; border-bottom: 1px solid; height: 21px">
             <p class="MsoNormal" style="margin: 0in 0in 0pt">
                 <i><span style="color: darkblue">(Monitoring Screen For D.M. MPSCSC)<?xml namespace=""
                     ns="urn:schemas-microsoft-com:office:office" prefix="o" ?><o:p></o:p></span></i></p>
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; height: 21px">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; width: 90px; background-color: white; height: 26px;">
             District Logged in</td>
         <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; width: 90px; background-color: white; height: 26px;">
             <asp:TextBox ID="txtdistrict" runat="server" Width="100px"></asp:TextBox></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; width: 90px; background-color: white; height: 26px;">
             &nbsp; &nbsp;&nbsp;
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; width: 90px; background-color: white; height: 26px;">
             &nbsp; &nbsp;
         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; width: 90px; background-color: white; height: 26px;">
             &nbsp; &nbsp;&nbsp;
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; width: 90px; background-color: white; height: 26px;">
             &nbsp; &nbsp;
         </td>
     </tr>
                    <tr>
                       
                          
                                
                                
                                        <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 90px; height: 26px; background-color: white;">
                                            RO No.</td>
                                        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 90px; height: 26px; background-color: white;">
                                            <asp:DropDownList ID="ddlrono" runat="server" Width="110px" OnSelectedIndexChanged="ddlrono_SelectedIndexChanged"
                                                AutoPostBack="True">
                                                
                                            </asp:DropDownList>
                                        </td>
                                        <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 90px; height: 26px; background-color: white;">
                                            RO Qty.</td>
                                        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 90px; height: 26px; background-color: white;" align="left">
                                            <asp:TextBox ID="txtroqty" runat="server" Width="110px" ></asp:TextBox>&nbsp;</td>
                                        <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 90px; height: 26px; background-color: white;">
                                            Validity Date</td>
                                        <td class="tdmarginro" align ="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 90px; height: 26px; background-color: white;" >
                                            <asp:TextBox ID="txtrodate" runat="server" Width="100px" ></asp:TextBox></td>
                                    </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; width: 90px; background-color: white; height: 26px;">
             &nbsp; &nbsp;&nbsp;
         </td>
         <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; width: 90px; background-color: white; height: 26px;">
             &nbsp; &nbsp;&nbsp;
         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; width: 90px; background-color: white; height: 26px;">
             Commodity</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; width: 90px; background-color: white; height: 26px;">
             <asp:TextBox ID="txtcommodity" runat="server" Width="110px"></asp:TextBox></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; width: 90px; background-color: white; height: 26px;">
             Scheme</td>
         <td align="left" class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; border-bottom: 1px solid; width: 90px; background-color: white; height: 26px;">
             <asp:TextBox ID="txtscheme" runat="server" Width="100px"></asp:TextBox></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; width: 90px; background-color: white; height: 26px;">
             &nbsp; &nbsp; &nbsp;
         </td>
         <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: white; height: 26px;">
             &nbsp; &nbsp;
         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; width: 90px; background-color: white; height: 26px;">
             Cumulative Lifted Qty.</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: white; height: 26px;">
             <asp:TextBox ID="txtcumlqty" runat="server" Width="110px"></asp:TextBox></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; width: 90px; background-color: white; height: 26px;">
             Pending Qty for T.O.</td>
         <td align="left" class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; border-bottom: 1px solid; width: 90px; background-color: white; height: 26px;">
                                            <asp:TextBox ID="txtbalqty" runat="server" ReadOnly="True" Width="100px"></asp:TextBox></td>
     </tr>
     <tr>
         <td align="left" class="tdmarginro" style="height: 24px">
         </td>
         <td class="tdmarginro" style="height: 24px">
         </td>
         <td class="tdmarginro" style="height: 24px">
             <asp:Button ID="btnclose" runat="server" OnClick="btnclose_Click" Text="Close " Width="83px" /></td>
         <td class="tdmarginro" colspan="2" style="height: 24px">
         </td>
         <td class="tdmarginro" style="height: 24px">
         </td>
     </tr>
                                    <tr>
                                        <td class="tdmarginro" align="left">
                                            <asp:Label ID="Label2" runat="server" Visible="False"></asp:Label></td>
                                        <td  class="tdmarginro">
                                            <asp:TextBox ID="txttorderno" runat="server" Width="90px" Visible="False"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txttorderno"
                                                ErrorMessage="Transport Order No Required " Font-Bold="True" ValidationGroup="1"
                                                Width="5px">*</asp:RequiredFieldValidator></td>
                                        <td class="tdmarginro">
             <asp:TextBox ID="txtsendqty" runat="server" Width="58px" Visible="False"></asp:TextBox></td>
                                            <td  class="tdmarginro" colspan="2">
                                                &nbsp;
                                            
                                                <asp:TextBox ID="DaintyDate1" runat="server"></asp:TextBox>
                                                 <script type  ="text/javascript">
                                                	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate1'
	    });
	     </script>
                                            
                                            </td>
                                            <td class="tdmarginro">
                                                &nbsp; &nbsp;&nbsp;<asp:DropDownList ID="ddltransport" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddltransport_SelectedIndexChanged" Width="60px" Visible="False">
                                                </asp:DropDownList>&nbsp; 
                                                </td>
                                    </tr>
     <tr>
         <td align="left" class="tdmarginro">
             <asp:DropDownList ID="ddldistrict" runat="server" Width="61px" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged"
                                                AutoPostBack="True" Visible="False">
             </asp:DropDownList></td>
         <td class="tdmarginro" style="width: 111px;">
             <asp:Button ID="btnsave" runat="server" OnClick="btnsave_Click" Text="Submit" Width="72px" ValidationGroup="1" Visible="False" /></td>
         <td class="tdmarginro">
             </td>
         <td align="left" class="tdmarginro" colspan="2">
             &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
             <asp:DropDownList ID="ddlissue" runat="server" Width="76px" Visible="False" >
             </asp:DropDownList></td>
         <td class="tdmarginro">
             &nbsp; &nbsp; &nbsp;&nbsp;
         </td>
     </tr>
     <tr>
         <td colspan="6">
             <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                 ValidationGroup="1" Width="408px" Height="30px" />
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
          AllowPaging="True" PageSize="5" PagerSettings-Visible ="true"  CellPadding="2" ForeColor="Black" GridLines="None" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" OnPageIndexChanging="dgridchallan_PageIndexChanging" Height="180px" Width="644px"  >
        <HeaderStyle  CssClass="gridheader" BackColor="Tan" Font-Bold="True"   />
        <Columns>
            <asp:CommandField ShowSelectButton="True" >
                <ItemStyle CssClass="griditem" />
            </asp:CommandField>
                       
            <asp:BoundField DataField="RO_No" HeaderText="R.O.No." ReadOnly="True" SortExpression="RO_No" >
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="TO_Number" HeaderText="T.O. No." ReadOnly="True" SortExpression="TO_Number" >
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="T.O. Date">
     
                <ItemTemplate>
                <asp:Label ID="lblChallan" runat="server" 
                Text='<%# Eval("TO_Date").ToString()%>'>
                </asp:Label>
                </ItemTemplate>
               <HeaderStyle CssClass="gridlarohead" />
                <ItemStyle CssClass="griditemlaro" />
                 </asp:TemplateField>
            <asp:BoundField DataField="Quantity" HeaderText="T.O. Qty." SortExpression="Quantity" >
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Cumulative_Qty" HeaderText="Lifted Qty." SortExpression="Cumulative_Qty">
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Pending_Qty" HeaderText="Pending Qty." SortExpression="Pending_Qty">
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="DepotName" HeaderText="for Depot" SortExpression="DepotName">
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            
            <asp:BoundField DataField="Tname" HeaderText="Transporter Name" ReadOnly="True" SortExpression="Tname" >
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
           
        </Columns>
                                                <FooterStyle BackColor="Tan" />
                                                <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                                <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                                                <AlternatingRowStyle BackColor="PaleGoldenrod" />
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

