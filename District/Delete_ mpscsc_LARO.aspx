<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true"
    CodeFile="Delete_ mpscsc_LARO.aspx.cs" Inherits="mpscsc_LARO" Title="Lifting Against Release Order" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat ="server">
    <script type="text/javascript" language ="javascript "> 
    
    

function Highlight(chk) {

if (chk.checked) {

 $("#" + chk.id).parent("td").parent("tr").css("background-color", "Red");

}else

{

$("#" + chk.id).parent("td").parent("tr").css("background-color", "white");

}

}

</script>
    <div id="hedl">
        &nbsp;Lifting Against&nbsp; FCI Release Order
    </div>
    <div id="ronewmargin">
       
                        
                <table cellpadding ="0" cellspacing ="0" border ="0" class="laromargin">
                    <tr>
                        <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                            width: 73px; border-bottom: 1px solid; background-color: thistle">
                        </td>
                        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                            width: 117px; border-bottom: 1px solid; background-color: thistle">
                            </td>
                        <td align="left" colspan="2" style="border-right: 1px solid; border-top: 1px solid;
                            border-left: 1px solid; border-bottom: 1px solid; background-color: thistle">
                            </td>
                        <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                            width: 92px; border-bottom: 1px solid; background-color: thistle">
                        </td>
                        <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                            border-bottom: 1px solid; background-color: thistle">
                        </td>
                    </tr>
                    <tr>
                       
                          
                                
                                
                                        <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 73px; background-color: thistle;">
                                            RO No.</td>
                                        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 117px; background-color: thistle;">
                                            <asp:DropDownList ID="ddlrono" runat="server" Width="116px" OnSelectedIndexChanged="ddlrono_SelectedIndexChanged"
                                                AutoPostBack="True">
                                                
                                            </asp:DropDownList>
                                        </td>
                                        <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 60px; background-color: thistle;">
                                            RO Date</td>
                                        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: thistle;">
                                            <asp:TextBox ID="txtrodate" runat="server" Width="110px" ></asp:TextBox>
                                        </td>
                                        <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 92px; background-color: thistle;">
                                            RO Qty</td>
                                        <td align ="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: thistle;" >
                                            <asp:TextBox ID="txtroqty" runat="server" Width="110px" ></asp:TextBox>(Qtl.)
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdmarginddl" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 73px; background-color: thistle;">
                                            Commodity</td>
                                        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 117px; background-color: thistle;">
                                            <asp:TextBox ID="txtcomdty" runat="server" Width="110px" ></asp:TextBox>
                                        </td>
                                        <td class="tdmarginddl" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 60px; background-color: thistle;">
                                            Scheme</td>
                                        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: thistle;">
                                            <asp:TextBox ID="txtscheme" runat="server" Width="110px" ReadOnly="True" ></asp:TextBox>&nbsp;</td> 
                                        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 92px; background-color: thistle;">Bal Qty </td>
                                        <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: thistle;"> 
                                            <asp:TextBox ID="txtbalqty" runat="server" ReadOnly="True" Width="110px"></asp:TextBox>(Qtl.)</td>
                                    </tr>
                                    <tr>
                                        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 73px; background-color: thistle;">
                                        </td>
                                        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 117px; background-color: thistle;">
                                            <asp:Label ID="lblyear" runat="server" Visible="False"></asp:Label></td>
                                        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 60px; background-color: thistle;">
                                            <asp:Label ID="lblmonth" runat="server" Visible="False"></asp:Label></td>
                                        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: thistle;">
                                            <asp:Label ID="lblscheme" runat="server" Visible="False"></asp:Label></td>
                                        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 92px; background-color: thistle;">
                                            <asp:Label ID="lblcomdty" runat="server" Visible="False"></asp:Label></td>
                                        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: thistle;">
                                        </td>
                                    </tr>
                                
        <asp:Label ID="Label1" runat="server"></asp:Label></table> 
                      
               
            
      <table style="width: 658px">
      <tr>
      <td align="center">
          <asp:Label ID="Label2" runat="server" Text="Label" Font-Italic="True" ForeColor="#C00000" Visible="False" Width="375px"></asp:Label>
      </td>
      </tr>
      <tr>
      <td> 
       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound ="GridView1_RowDataBound" BackColor="LightGoldenrodYellow" 
       BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" 
       Width="654px" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="15" PagerSettings-Visible ="true" AllowPaging="True">
                                                   <Columns>
                                                       <asp:CommandField ShowSelectButton="True" HeaderText="Action" SelectText="Delete" />
                                                       <asp:BoundField DataField="Challan_No" HeaderText="TC NO." SortExpression="Challan_No">
                                                           <ItemStyle CssClass="griditemlaro" />
                                                           <HeaderStyle CssClass="gridlarohead" />
                                                       </asp:BoundField>
                                                    <asp:TemplateField HeaderText="TC Date">
     
                                                            <ItemTemplate>
                                                            <asp:Label ID="lblChallan" runat="server" 
                                                             Text='<%# Eval("Challan_Date").ToString()%>'>
                                                            </asp:Label>
                                                                     </ItemTemplate>
                                                                <HeaderStyle CssClass="gridlarohead" />
                                                        <ItemStyle CssClass="griditemlaro" />
                                                            </asp:TemplateField>
                                                       <asp:BoundField DataField="RO_No" HeaderText="RO No." ReadOnly="True" SortExpression="RO_No" >
                                                           <HeaderStyle CssClass="gridlarohead" />
                                                           <ItemStyle CssClass="griditemlaro" />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="TO_Number" HeaderText="TO NO." SortExpression="TO_Number">
                                                           <ItemStyle CssClass="griditemlaro" />
                                                           <HeaderStyle CssClass="gridlarohead" />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Depot_Name" HeaderText="IC Name" ReadOnly="True" SortExpression="Depot_Name" >
                                                           <HeaderStyle CssClass="gridlarohead" />
                                                           <ItemStyle CssClass="griditemlaro" />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Vehicle_No" HeaderText="Vehile No." ReadOnly="True" SortExpression="Vehicle_No" >
                                                           <HeaderStyle CssClass="gridlarohead" />
                                                           <ItemStyle CssClass="griditemlaro" />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="No_of_Bags" HeaderText="Bags" ReadOnly="True" SortExpression="No_of_Bags" >
                                                           <HeaderStyle CssClass="gridlarohead" />
                                                           <ItemStyle CssClass="griditemlaro" />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Qty_send" HeaderText="Qty" ReadOnly="True" SortExpression="Qty_send" >
                                                           <HeaderStyle CssClass="gridlarohead" />
                                                           <ItemStyle CssClass="griditemlaro" />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" ReadOnly="True" SortExpression="Commodity_Name" >
                                                           <HeaderStyle CssClass="gridlarohead" />
                                                           <ItemStyle CssClass="griditemlaro" />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="IsRecieved" HeaderText="Status of Deposit" ReadOnly="True"
                                                           SortExpression="IsRecieved" >
                                                           <HeaderStyle CssClass="gridlarohead" />
                                                           <ItemStyle CssClass="griditemlaro" />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Trunsuction_Id" HeaderText="Transuction" SortExpression="Trunsuction_Id">
                                                           <HeaderStyle CssClass="gridlarohead" />
                                                           <ItemStyle CssClass="griditemlaro" />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Issue_center" HeaderText="IC" SortExpression="Issue_center">
                                                           <HeaderStyle CssClass="gridlarohead" />
                                                           <ItemStyle CssClass="griditemlaro" />
                                                       </asp:BoundField>
                                                   </Columns>
                                                   <FooterStyle BackColor="Tan" />
                                                   <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                                   <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                                                   <HeaderStyle BackColor="Tan" Font-Bold="True" />
                                                   <AlternatingRowStyle BackColor="PaleGoldenrod" />
                                               </asp:GridView>
          &nbsp;&nbsp;
                                               <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Close" Width="235px" /></td>
      </tr>
      </table>
    </div>
     
         
        <div>
        <table border ="0" cellpadding ="0" cellspacing ="0" class="gridfooter">
        <tr>
                        <td style="height: 15px">
                            <div >
                                <asp:LinkButton ID="Firstbutton" Text='<img border="0" src="../images/firstpage.gif" align="absmiddle">'
                                    CommandArgument="0" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false"  />&nbsp;
                                <asp:LinkButton ID="Prevbutton" Text='<img border="0" src="../images/prevpage.gif" align="absmiddle">'
                                    CommandArgument="prev" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" />&nbsp;
                                <asp:LinkButton ID="Nextbutton" Text='<img border="0" src="../images/nextpage.gif" align="absmiddle">'
                                    CommandArgument="next" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" />&nbsp;
                                <asp:LinkButton ID="Lastbutton" Text='<img border="0" src="../images/lastpage.gif" align="absmiddle">'
                                    CommandArgument="last" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" OnClick="Lastbutton_Click" />&nbsp;&nbsp;
                             
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
