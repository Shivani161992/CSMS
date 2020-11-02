<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Deletefrm_Procurement.aspx.cs" Inherits="District_Deletefrm_Procurement" Title="Delete Procurement Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
 <script type="text/javascript">
        
        
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
</script>
 
 <center>
    <table style="border: thin groove black; width: 802px; height: 420px; ">
        <tr>
            <td colspan="4" style="border-right: black thin groove; width: 100px; text-align: center; background-color: #cccc99;">
                <asp:Label ID="Label1" runat="server" Text="Delete Receipt frm Procurement" 
                    Width="357px" Height="16px"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="4" 
                
                style="border-right: black thin groove; width: 700px; border-bottom: black thin groove; text-align: center; height: 2px; font-size: small; font-weight: 700; color: #CC0000;">
               
                सम्बंधित एंट्री को डिलीट करने की पूर्ण
                    जवाबदारी प्रबंधक एवं उपयोगकर्ता की रहेगी, कृपया जांच
                कर ही डिलीट करें</td>
        </tr>
        <tr>
            <td style="height: 1px; border-right: black thin groove; border-bottom: black thin groove;" colspan="2">
                <asp:Label ID="Label2" runat="server" Text="Issue Center Name" Width="163px"></asp:Label></td>
            <td style="height: 1px; border-right: black thin groove; border-bottom: black thin groove;" colspan="2">
                <asp:DropDownList ID="ddlissuecenter" runat="server" Width="166px" Enabled="False" 
                   >
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td colspan="2" style="height: 1px; border-right: black thin groove; border-bottom: black thin groove;">
                <asp:Label ID="Label7" runat="server" Text="Receiving Date"></asp:Label></td>
            <td colspan="2" style="height: 1px; border-right: black thin groove; border-bottom: black thin groove;">
            
            <asp:TextBox ID="Recdate" runat="server" Width="129px" ></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
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
                <center>
                                       <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                        BackColor="#CCCCCC" BorderColor="#999999" 
                 BorderStyle="Solid" BorderWidth="3px" 
                                        CellPadding="4" Font-Bold="False" Font-Size="Small" Width="750px" 
                                           CellSpacing="2" ForeColor="Black" >
                                        <RowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="TC_Number" HeaderText="Challan No" 
                                                SortExpression="TC_Number">
                                            <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                           
                                            <asp:BoundField DataField="Truck_Number" HeaderText="Truck No" 
                                                SortExpression="Truck_Number">
                                          <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                            
                                             <asp:BoundField DataField="Recd_Bags" HeaderText="Recd Bags" 
                                                SortExpression="Recd_Bags">
                                          <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="Recd_Qty" HeaderText="Rec Qty" 
                                                SortExpression="Recd_Qty">
                                            <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="GodownName" HeaderText="Godown Name" >
                                            <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                                                                       
                                        </Columns>
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="Black" Font-Bold="True" Font-Size="Small"  ForeColor="White" />
                                     
                                    </asp:GridView>

</center>
                                     </td>
        </tr>
        <tr>
            <td style="border-right: black thin groove; border-bottom: black thin groove; text-align: left;" colspan="4">
                <asp:Label ID="lblerror" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 127px; border-right: black thin groove; border-bottom: black thin groove; text-align: left; height: 1px;">
                <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" Font-Bold="True" ForeColor="Blue" Width="68px" /></td>
            <td style="width: 109px; border-right: black thin groove; border-bottom: black thin groove; text-align: left; height: 1px;">
                <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" Font-Bold="True" ForeColor="Blue" Width="86px" /></td>
            <td style="width: 88px; border-right: black thin groove; border-bottom: black thin groove; text-align: left; height: 1px;">
                <asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" 
                    Font-Bold="True" ForeColor="Blue" Width="73px" style="height: 26px" /></td>
            <td style="width: 132px; border-right: black thin groove; border-bottom: black thin groove; text-align: left; height: 1px;">
                <asp:Label ID="lblchallan" runat="server" Width="147px" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
    </center>
</asp:Content>

