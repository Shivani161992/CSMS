<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Issue_RejectionNote.aspx.cs" Inherits="IssueCenter_Issue_RejectionNote" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <script type="text/javascript">


     function CheckCalDate(tx)
{
var AsciiCode = event.keyCode;
var txt=tx.value;
var txt2 = String.fromCharCode(AsciiCode);
var txt3=txt2*1;
if ((AsciiCode > 0))
{
alert('Please Click on Calander Controll to Enter Date');
event.cancelBubble = true;
event.returnValue = false;
}
}
</script>
      
 <table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: navy 3px double; border-top: navy 3px double; border-left: navy 3px double; border-bottom: navy 3px double; width: 700px;" >
     <tr>
         <td align="center"  colspan="4" style="background-color: #cccccc; height: 21px;">
             <asp:Label ID="lblissueacno" runat="server" Text="Issue Rejection Note Number" Width="261px" Font-Bold="True"></asp:Label></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label></td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; width: 250px;">
         </td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; width: 100px;">
         </td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         </td>
     </tr>
      <tr>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; height: 25px;">
             <asp:Label ID="lbltypacc" runat="server" Text="Source of Arrival" Width="113px"></asp:Label></td>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static; height: 25px; width: 250px;" align="left">
           <asp:DropDownList ID="ddltype" runat="server"  Width ="150px" AutoPostBack="true" OnSelectedIndexChanged="ddltype_SelectedIndexChanged">
                            
                        </asp:DropDownList>

         </td>
         <td align="right" class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; width: 100px; height: 25px;">
             <asp:Label ID="lblsesn" runat="server" Text="Crop" Width="100px" Visible="false"></asp:Label></td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; height: 25px;">
         <asp:DropDownList ID="ddlmarksesn" runat="server"  Width ="120px" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddlmarksesn_SelectedIndexChanged">
                            
                        </asp:DropDownList></td>
     </tr>
     <asp:Panel ID="pnlaccother" runat="server" Visible="false" >
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblDateOfDeposit" runat="server" Text="Date of Deposit" Width="113px"></asp:Label></td>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static;" align="left">
          <asp:TextBox ID="DaintyDate3" runat="server" Width="119px"></asp:TextBox>
  <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate3'
	    });
	     </script> 

             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DaintyDate3"
                 ErrorMessage="Please Select Date of Deposit" ValidationGroup="2">*</asp:RequiredFieldValidator></td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblpcname" runat="server" Text="Purchase Center" Width="64px"></asp:Label></td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         <asp:DropDownList ID="ddlpurchcenter" runat="server"  Width ="255px" >
                            
                        </asp:DropDownList></td>
     </tr>
     </asp:Panel>
     <asp:Panel ID="pnlaccprocment" runat="server" Visible="false">
         <tr>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblDateOfDepositP" runat="server" Text="Rejected Date" Width="113px"></asp:Label></td>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static;" align="left">
          <asp:TextBox ID="DaintyDate3P" runat="server" Width="119px"></asp:TextBox>
  <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate3P'
	    });
function Table1_onclick() 
{

}

	     </script> 

             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DaintyDate3"
                 ErrorMessage="Please Select Date of Reject" ValidationGroup="2">*</asp:RequiredFieldValidator></td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblpcnameP" runat="server" Text="Sending District" Width="100px"></asp:Label></td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         <asp:DropDownList ID="ddldistproment" runat="server"  Width ="150px" AutoPostBack="true" OnSelectedIndexChanged="ddldistproment_SelectedIndexChanged" >
                            
        </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;" >
             <asp:Label ID="Label2" runat="server" Text="purchase Center" Width="113px"></asp:Label></td>
         
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; width:60px" colspan="2">
         <asp:DropDownList ID="ddlpurchcenterP" runat="server"  Width ="310px"  Font-Size="12px">
                            
                        </asp:DropDownList></td>
                        <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;" >
             <asp:Label ID="Label5" runat="server" Text="" Width="113px"></asp:Label></td>
     </tr>
     
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;" >
             <asp:Label ID="Label11" runat="server" Text="Godown Name" Width="113px"></asp:Label></td>
         
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; width:60px" colspan="2">
         <asp:DropDownList ID="ddlgodown" runat="server"  Width ="310px" Font-Size="12px">
                            
         </asp:DropDownList></td>
                        
                       
                        <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;" >
             <asp:Label ID="Label12" runat="server" Text="" Width="113px"></asp:Label></td>
             
             
     </tr>
     
     </asp:Panel>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; height: 25px;">
         </td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; height: 25px; width: 250px;">
             <asp:Button ID="btnviewdetails" runat="server" OnClick="btnviewdetails_Click" Text="Get Details"
                 Width="186px" ValidationGroup="2" /></td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; width: 100px; height: 25px;">
         </td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; height: 25px;">
         </td>
     </tr>
     <tr>
         <td align="center"  colspan="4" style="background-color: #cfdcdc; font-size: 10pt; position: static; height: 19px;">
             <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Maroon" 
                 Visible="False"></asp:Label></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;" colspan="4">
         <center style="text-align: center">
           <%-- <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"  CellPadding="3" 
            ForeColor="#333333" GridLines="None" ShowFooter="True" Height="200px" Width="500px" >
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="cbSelectAll" runat="server"  AutoPostBack ="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="IssueID" HeaderText="IssueID" SortExpression="IssueID" >
                    <ItemStyle CssClass="griditemlaro" Font-Size="6pt" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="TC_Number" HeaderText="TC No" SortExpression="TC_Number" >
                    <ItemStyle CssClass="griditemlaro" Font-Size="8pt" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="Truck_Number" HeaderText="Truck No." SortExpression="Truck_Number" >
                    <ItemStyle CssClass="griditemlaro" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" SortExpression="Commodity_Name" Visible = "False" >
                    <ItemStyle CssClass="griditemlaro" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
               
                                               
            </Columns>
                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <EditRowStyle BackColor="#999999" />
        </asp:GridView>    --%>
        
         <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"  Width="750px" CellPadding="4" 
            ForeColor="#333333" GridLines="None"  ShowFooter="True" Height="206px" >
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="cbSelectAll" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="IssueID" HeaderText="IssueID" SortExpression="IssueID" >
                    <ItemStyle CssClass="griditemlaro" Font-Size="8pt" Width="30px" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="TC_Number" HeaderText="TC No" SortExpression="TC_Number" >
                    <ItemStyle CssClass="griditemlaro" Font-Size="6pt" Width="40px" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="Truck_Number" HeaderText="Truck No." SortExpression="Truck_Number" >
                    <ItemStyle CssClass="griditemlaro" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" SortExpression="Commodity_Name" Visible = "False" >
                    <ItemStyle CssClass="griditemlaro" Width="50px" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
               
               <asp:BoundField DataField="RecdQty_Faq" HeaderText="Recd Qty_Faq" SortExpression="RecdQty_Faq" >
                    <ItemStyle CssClass="griditemlaro" Width="50px" HorizontalAlign="Center"/>
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                
                 <asp:BoundField DataField="RecdQty_Urs" HeaderText="Recd Qty_Urs" SortExpression="RecdQty_Urs" >
                    <ItemStyle CssClass="griditemlaro" Width="50px" HorizontalAlign="Center"/>
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                
                <asp:BoundField DataField="RecdBags_JuteNew" HeaderText="Recd Bags_JuteNew" SortExpression="RecdBags_JuteNew" >
                    <ItemStyle CssClass="griditemlaro" Width="50px" HorizontalAlign="Center"/>
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                
                <asp:BoundField DataField="RecdBags_PP" HeaderText="Recd Bags_PP" SortExpression="RecdBags_PP" >
                    <ItemStyle CssClass="griditemlaro" Width="50px" HorizontalAlign="Center"/>
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                
                <asp:BoundField DataField="RecdBags_JuteOld" HeaderText="Recd Bags_JuteOld" SortExpression="RecdBags_JuteOld" >
                    <ItemStyle CssClass="griditemlaro" Width="50px" HorizontalAlign="Center"/>
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
               
                <%--<asp:TemplateField HeaderText="Accept_Bags" SortExpression="Recd_Bags">
               <ItemTemplate>
              <ItemStyle  />
                    <HeaderStyle/>
                   <asp:TextBox ID="txtrcBags" runat="server" 
                     Text='<%# Bind("Recd_Bags") %>' ReadOnly="false"  BorderColor="Black" Width="50px" OnTextChanged = "txtrcBags_TextChanged" AutoPostBack="true" OnKeyPress=" return IsNumericProcQty(this);"> </asp:TextBox>
                     
                 </ItemTemplate>
                 
                 <FooterTemplate>
                                   
                    <asp:Label ID="lbl_CurrentDarj" runat="server"  ForeColor="White" Font-Names="Vani" Text = "Total"></asp:Label>
                    
                    </FooterTemplate>
                      <FooterStyle CssClass="FooterStyle" />
                 <HeaderStyle Font-Size="10px" />
                </asp:TemplateField>--%>
               
                 
                
                <%--<asp:TemplateField HeaderText="Accepted_Qty" SortExpression="Recd_Qty">
               <ItemTemplate>
              <ItemStyle  />
                    <HeaderStyle  />
                   <asp:TextBox ID="txtrcq" runat="server" Text='<%# Bind("Recd_Qty") %>' ReadOnly="false"  BorderColor="Black" Width="60px" OnTextChanged="txtrcq_TextChanged" AutoPostBack="true">           
                 </asp:TextBox>
                 </ItemTemplate>
                    <FooterTemplate>
                                   
                    <asp:Label ID="lbl_Qty" runat="server"  ForeColor="White" Font-Names="Vani" Text = "Total"></asp:Label>
                    
                    </FooterTemplate>
                      <FooterStyle CssClass="FooterStyle" />
                 <HeaderStyle Font-Size="10px" />
                </asp:TemplateField>--%>
                 
                <%-- <asp:TemplateField HeaderText="Reject_Qty" SortExpression="Reject_Qty">
                 <ItemTemplate>
              <ItemStyle CssClass="griditemlaro" />
                    <HeaderStyle CssClass="gridlarohead" />
                   <asp:TextBox ID="txtrjctqt" runat="server" 
                     Text="" ReadOnly="true"  BorderColor="Black" Width="60px" >           
                 </asp:TextBox>
                 </ItemTemplate>
                     <HeaderStyle Font-Size="10px" />
                 </asp:TemplateField>--%>
                 
                 <%--<asp:BoundField DataField="QtyTransffer" HeaderText="Sending Quantity" SortExpression="QtyTransffer">
                    <HeaderStyle Font-Size="10px" />
                </asp:BoundField>--%>
            </Columns>
                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <EditRowStyle BackColor="#999999" />
        </asp:GridView>
        
         </center>  
        
          </td>
     </tr>
     <tr>
         <td class="tdmarginro" colspan="4" style="font-size: 10pt; position: static; height: 17px;
             background-color: #cfdcdc">
             
             <table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: navy 3px double; border-top: navy 3px double; border-left: navy 3px double; border-bottom: navy 3px double; width: 750px;" id="Table1" onclick="return Table1_onclick()" >
                 <tr>
                     <td class="tdmarginro" style="font-size: 10pt; border-bottom: black thin groove;
                         position: static; height: 17px; background-color: #cfdcc8">
                         <asp:Label ID="Label10" runat="server" Text="Date of Rejection" Width="150px" Font-Bold="True"></asp:Label></td>
                     <td align="right" class="tdmarginro" style="font-size: 10pt; border-bottom: black thin groove;
                         position: static; height: 17px; background-color: #cfdcc8">
                         
                          <asp:TextBox ID="txtAccDate" runat="server" Width="119px"></asp:TextBox>
  <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_txtAccDate'
	    });
	    
	    </script>
                         
                     </td>
                     <td class="tdmarginro" style="font-size: 10pt; width: 90px; border-bottom: black thin groove;
                         position: static; height: 17px; background-color: #cfdcc8">
                     </td>
                     <td class="tdmarginro" style="font-size: 10pt; border-bottom: black thin groove;
                         position: static; height: 17px; background-color: #cfdcc8">
                     </td>
                 </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-bottom: black thin groove; height: 17px;" >
             <asp:Label ID="lblacno" runat="server" Text="Rejection  Note No." Width="164px" Visible="False"></asp:Label></td>
         <td align="right" class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-bottom: black thin groove; height: 17px;">
             <asp:TextBox ID="txtaccptno" runat="server" MaxLength="13" Width="149px" Visible="false" ForeColor="#0000C0"></asp:TextBox></td>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; width: 90px; border-bottom: black thin groove; height: 17px;">
             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtaccptno"
                 ErrorMessage="Acceptence Note No. Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-bottom: black thin groove; height: 17px;">
         </td>
     </tr>
      <tr>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; height: 12px;">
         <asp:Button ID="btnnw" runat="server" Text="New" Width="120px" OnClick="btnnw_Click" />
         </td>
         <td  align="center" style="background-color: #cfdcc8; font-size: 10pt; position: static; height: 12px;">
         <asp:Button ID="btnsubmit" runat="server" Text="Submit" Width="131px" OnClick="btnsubmit_Click"/></td>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; width: 90px; height: 12px;">
         <asp:Button ID="btnclose" runat="server" Text="Close" Width="136px" OnClick="btnclose_Click" /></td>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; height: 12px;">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" colspan="3" style="background-color: #cfdcc8; font-size: 10pt; position: static; height: 12px;">
             <asp:Label ID="Label3" runat="server" Text="Label" Visible="False"></asp:Label></td>
             <td align="right" class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; height: 12px;">
             <asp:HyperLink ID="hyprlnkprint" runat="server" NavigateUrl="#" Visible="False" Width="139px">Print Receipt Detail</asp:HyperLink></td>
     </tr>
     <tr>
         <td class="tdmarginro" colspan="4" style="height: 25px">
             <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                 ValidationGroup="1" Width="282px" />
             <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                 ValidationGroup="2" Width="283px" />
         </td>
     </tr>
     </table> 
             
         </td>
     </tr>
 </table>

</asp:Content>

