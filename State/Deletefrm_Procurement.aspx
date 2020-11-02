<%@ Page Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="Deletefrm_Procurement.aspx.cs" Inherits="District_Deletefrm_Procurement" Title="Delete Receipt Procurement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
 <%--<script type="text/javascript">
        
        
function CheckCalDate(tx){
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
</script>--%>

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


 <center>
    <table style="width: 740px; height: 420px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
        <tr>
            <td colspan="4" 
                style="border-right: black thin groove; width: 100px; background-color: #cccc99; height: 1px;">
                <asp:Label ID="Label1" runat="server" Text="Delete Receipt frm Procurement" 
                    Width="234px"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="4" style="border-right: black thin groove; width: 700px; border-bottom: black thin groove; text-align: center; height: 1px;">
                <span style="color: #0000cc">&nbsp;सम्बंधित एंट्री को डिलीट करने से पहले कृपया जांच
                    रिसिप्ट आई डी देख
                कर ही डिलीट करें</span></td>
        </tr>
        <tr>
            <td style="height: 1px; border-right: black thin groove; border-bottom: black thin groove;" colspan="2">
                District</td>
            <td style="height: 1px; border-right: black thin groove; border-bottom: black thin groove;" colspan="2">
             
                                    
                   <asp:DropDownList ID="ddldistrict" runat="server" Width ="171px" AutoPostBack ="true" 
                                            
                    OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged" TabIndex="2">
                        <asp:ListItem Value ="01" Selected ="True" >--Select--</asp:ListItem>
                        </asp:DropDownList>
                        
                       
                        </td>
        </tr>
        <tr>
            <td style="height: 1px; border-right: black thin groove; border-bottom: black thin groove;" colspan="2">
                <asp:Label ID="Label2" runat="server" Text="Select Issue Center" Width="163px"></asp:Label></td>
            <td style="height: 1px; border-right: black thin groove; border-bottom: black thin groove;" colspan="2">
                <asp:DropDownList ID="ddlissuecenter" runat="server" Width="166px" AutoPostBack="True" OnSelectedIndexChanged="ddlissuecenter_SelectedIndexChanged" 
                   >
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td colspan="2" style="height: 1px; border-right: black thin groove; border-bottom: black thin groove;">
                <asp:Label ID="Label7" runat="server" Text="Receiving Date"></asp:Label></td>
            <td colspan="2" style="height: 1px; border-right: black thin groove; border-bottom: black thin groove;">
            
            <asp:TextBox ID="Recdate" runat="server" Width="129px" ></asp:TextBox>
            <%-- <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_Recdate'
	    });
	     </script>--%>
	     
	      <script type  ="text/javascript">
	             new tcal ({
				            'formname': 'aspnetForm',
				            'controlname': 'ctl00_ContentPlaceHolder1_Recdate'
	                      });

	          </script>
	     
                </td>
        </tr>
        <tr>
            <td colspan="2" style="border-right: black thin groove; border-bottom: black thin groove;
                height: 1px">
                <asp:Label ID="Label10" runat="server" Text="Select Crop" Width="137px"></asp:Label></td>
            <td colspan="2" style="border-right: black thin groove; border-bottom: black thin groove;
                height: 1px">
                <asp:DropDownList ID="ddlcrop" runat="server" Width="142px" OnSelectedIndexChanged="ddlcrop_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="height: 1px; border-right: black thin groove; border-bottom: black thin groove;" colspan="2">
                <asp:Label ID="Label3" runat="server" Text="Select Issue ID" Width="147px"></asp:Label></td>
            <td style="height: 1px; border-right: black thin groove; border-bottom: black thin groove;" colspan="2">
                <asp:DropDownList ID="ddlIssueID" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlIssueID_SelectedIndexChanged"
                    Width="271px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="border-right: black thin groove; border-bottom: black thin groove; text-align: left;" 
                colspan="4">
                                       <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                        BackColor="#CCCCCC" BorderColor="#999999" 
                 BorderStyle="Solid" BorderWidth="3px" 
                                        CellPadding="4" Font-Bold="False" Font-Size="Small" Width="750px" 
                                           CellSpacing="2" ForeColor="Black" >
                                        <RowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="TruckChalanNo" HeaderText="Challan No" 
                                                SortExpression="TruckChalanNo">
                                            <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                           
                                            <asp:BoundField DataField="TruckNo" HeaderText="Truck No" 
                                                SortExpression="TruckNo">
                                          <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                            
                                             <asp:BoundField DataField="Recd_Bags" HeaderText="Recd Bags" 
                                                SortExpression="Recd_Bags">
                                          <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="Recv_Qty" HeaderText="Rec Qty" 
                                                SortExpression="Recv_Qty">
                                            <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                            
                                                                                       
                                        </Columns>
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="Black" Font-Bold="True" Font-Size="Small"  ForeColor="White" />
                                     
                                    </asp:GridView>

            </td>
        </tr>
        <tr>
            <td style="border-right: black thin groove; border-bottom: black thin groove; text-align: left;" colspan="4">
                <asp:Label ID="lblerror" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="4" style="border-right: black thin groove; border-bottom: black thin groove;
                text-align: left">
                <asp:Label ID="lbloperation" runat="server"></asp:Label>
                <asp:Label ID="lblwp_oper" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 127px; border-right: black thin groove; border-bottom: black thin groove; text-align: left; height: 1px;">
                <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" Font-Bold="True" ForeColor="Blue" Width="68px" /></td>
            <td style="width: 109px; border-right: black thin groove; border-bottom: black thin groove; text-align: left; height: 1px;">
                <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" Font-Bold="True" ForeColor="Blue" Width="86px" /></td>
            <td style="width: 88px; border-right: black thin groove; border-bottom: black thin groove; text-align: left; height: 1px;">
                <asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" Font-Bold="True" ForeColor="Blue" Width="73px" /></td>
            <td style="width: 132px; border-right: black thin groove; border-bottom: black thin groove; text-align: left; height: 1px;">
                <asp:Label ID="lblchallan" runat="server" Width="147px" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
    </center>
</asp:Content>

