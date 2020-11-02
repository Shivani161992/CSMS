<%@ Page Language="C#" MasterPageFile="~/MasterPage/AdminLogin.master" AutoEventWireup="true"
    CodeFile="Delete_ mpscsc_LARO.aspx.cs" Inherits="mpscsc_LARO" Title="Lifting Against Release Order" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat ="server">
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
                        <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 73px; background-color: thistle;">
                            &nbsp;
                        </td>
                        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 117px; background-color: thistle;">
                            &nbsp;
                        </td>
                        <td  colspan="2" align="center" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: thistle">
                            RO Information&nbsp;
                        </td>
                        <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 92px; background-color: thistle;">
                            &nbsp;
                        </td>
                        <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: thistle;">
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                            width: 73px; border-bottom: 1px solid; background-color: thistle">
                        </td>
                        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                            width: 117px; border-bottom: 1px solid; background-color: thistle">
                            District</td>
                        <td align="left" colspan="2" style="border-right: 1px solid; border-top: 1px solid;
                            border-left: 1px solid; border-bottom: 1px solid; background-color: thistle">
                            <asp:DropDownList ID="ddldistrictmp" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddldistrictmp_SelectedIndexChanged"
                                Width="173px">
                            </asp:DropDownList></td>
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
                                   <table border="0" cellpadding="0" cellspacing="0" style="width: 623px">
                                       <tr>
                                           <td class="tdmarginro" style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid; color: black; background-color: lightblue;">
                                               &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;
                                               <asp:Label ID="lbltid" runat="server" Visible="False" Width="51px"></asp:Label></td>
                                           <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid; color: black; background-color: lightblue;">
                                               &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                           </td>
                                           <td align="left"  colspan="3" style="color: black; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid; background-color: lightblue;">
                                               <strong>
                                               Transportation&nbsp; Information&nbsp;</strong>
                                           </td>
                                           <td class="tdmarginro" style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid; color: black; background-color: lightblue;">
                                               &nbsp;&nbsp; &nbsp;&nbsp;
                                           </td>
                                           <td colspan="4" style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; color: black; border-bottom: white 1px solid; background-color: lightblue;">
                                           </td>
                                       </tr>
                                       <tr>
                                           <td colspan="6" style="border-right: white 1px solid; border-top: white 1px solid;
                                               border-left: white 1px solid; color: black; border-bottom: white 1px solid; background-color: lightblue">
                                               <asp:Label ID="lbldisply" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="#C00000"
                                                   Visible="False"></asp:Label></td>
                                           <td colspan="4" style="border-right: white 1px solid; border-top: white 1px solid;
                                               border-left: white 1px solid; color: black; border-bottom: white 1px solid; background-color: lightblue">
                                           </td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                               border-bottom: white 1px solid; color: black; background-color: lightblue;">
                                               &nbsp;&nbsp;
                                           </td>
                                           <td  colspan="2" style="color: black; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid; background-color: lightblue;" >                                             
                                               <strong style="color: olive">
                                               Transport Order Number</strong></td>
                                           <td class="tdmarginro" colspan="2" style="border-right: white 1px solid; border-top: white 1px solid;
                                               border-left: white 1px solid; border-bottom: white 1px solid; color: black; background-color: lightblue;">
                                               <asp:DropDownList ID="ddltono" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddltono_SelectedIndexChanged1" Width="125px">
                                                   <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
                                               </asp:DropDownList></td>
                                           <td colspan="1" style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                               border-bottom: white 1px solid; color: black; background-color: lightblue;">
                                           </td>
                                           <td colspan="4" style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; color: black; border-bottom: white 1px solid; background-color: lightblue;">
                                           </td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" colspan="6" style="border-right: white 1px solid; border-top: white 1px solid;
                                               border-left: white 1px solid; color: black; border-bottom: white 1px solid; background-color: lightblue">
                                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"  Width="550px" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" OnRowDataBound ="GridView2_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None">
            <Columns>
                <asp:TemplateField>
                                
                     <AlternatingItemTemplate>
                        <asp:CheckBox  ID="cbSelectAll"  runat="server" AutoPostBack ="false" />
                    </AlternatingItemTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="cbSelectAll" runat="server"  AutoPostBack ="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="TO_Number" HeaderText="TO No." SortExpression="TO_Number" >
                    <ItemStyle CssClass="griditemlaro" Font-Size="8pt" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="District" HeaderText="FCI District" SortExpression="District" >
                    <ItemStyle CssClass="griditemlaro" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="DepoName" HeaderText="Depot" SortExpression="DepoName" >
                    <ItemStyle CssClass="griditemlaro" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="district_name" HeaderText="To District" SortExpression="district_name" >
                    <ItemStyle CssClass="griditemlaro" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="DepotName" HeaderText="IssueCenter" SortExpression="DepotName" >
                    <ItemStyle Font-Size="10pt" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="Quantity" HeaderText="Qty." SortExpression="Quantity" >
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
                <asp:BoundField DataField="toDistrict" HeaderText="DCode" SortExpression="toDistrict" >
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="toIssueCenter" HeaderText="ICode" SortExpression="toIssueCenter" >
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="Transporter_Name" HeaderText="Transporter" SortExpression="Transporter_Name" >
                    <ItemStyle CssClass="griditemlaro" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="Trunsuction_Id" HeaderText="ID" SortExpression="Trunsuction_Id">
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
                                           </td>
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
                                               &nbsp;<asp:Button ID="btnsubmit" runat="server" Text="Delete Record" OnClick="btnsubmit_Click" Width="154px" ValidationGroup="1" /></td>
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
       Width="654px" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="5" PagerSettings-Visible ="true" AllowPaging="True">
                                                   <Columns>
                                                       <asp:CommandField ShowSelectButton="True" />
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
      
      </td>
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
