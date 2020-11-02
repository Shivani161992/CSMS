<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="TransportOrder_DoorStep.aspx.cs" Inherits="District_TransportOrder_DoorStep" Title="DPY TO Page" %>
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
   
   <script type="text/javascript">
    function CheckIsNumeric(e,tx)
    {         
        var AsciiCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;                        
        if ((AsciiCode < 46 && AsciiCode != 8) || (AsciiCode > 57 ) || (AsciiCode == 47 ))
        {
            alert('Please enter only numbers.');
            return false;
        }  
                      
        var num=tx.value;        
        var len=num.length;
        var indx=-1;
        indx=num.indexOf('.');
        if (indx != -1)
        {
            if ((AsciiCode == 46 ))
            {
                alert('Point must be apear only one time.');
                return false;
            }
            var dgt=num.substr(indx,len);
            var count= dgt.length;
            //alert (count);
            if (count > 5 && AsciiCode != 8)  
            {
                alert("Only 5 decimal digits allowed.");
                return false;
            }
        }
    }
    </script>
   
    <table style="width: 769px; height: 220px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
        <tr>
            <td colspan="4">
                <span style="color: #0000cc">
                Door Step Transport Order for Transporter</span></td>
        </tr>
        <tr>
            <td colspan="4">
                परिवहन आदेश जारी करने के पूर्व रूट चार्ट की प्रविष्टि कर ले |</td>
        </tr>
        <tr>
            <td style="width: 160px">
            </td>
            <td style="width: 174px">
            </td>
            <td style="width: 160px">
            </td>
            <td style="width: 190px">
            </td>
        </tr>
        <tr>
            <td style="width: 160px">
                <asp:Label ID="Label1" runat="server" Text="Select Issue Center" Width="140px"></asp:Label></td>
            <td colspan="2" style="text-align: left">
                <asp:DropDownList ID="ddlissueCenter" runat="server" Width="200px">
                </asp:DropDownList></td>
            <td style="width: 190px">
            </td>
        </tr>
      
        <tr>
            <td style="width: 160px">
                <asp:Label ID="Label4" runat="server" Text="Select Transporter Name" Width="182px"></asp:Label></td>
            <td style="width: 174px; text-align: left;">
                <asp:DropDownList ID="ddltransporter" runat="server" Width="240px" OnSelectedIndexChanged="ddltransporter_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList></td>
            <td style="width: 160px">
                </td>
            <td style="width: 190px; text-align: left;">
            
             
            
            </td>
        </tr>
        <tr>
            <td style="width: 160px">
                <asp:Label ID="Label6" runat="server" Text="Root Chart Number" Width="161px"></asp:Label></td>
            <td style="width: 174px; text-align: left;">
                <asp:DropDownList ID="ddlrootnumber" runat="server" Width="164px" OnSelectedIndexChanged="ddlrootnumber_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList></td>
            <td style="width: 160px">
                <asp:Label ID="Label7" runat="server" Text="Feed Number"></asp:Label></td>
            <td style="width: 190px; text-align: left;">
                <asp:DropDownList ID="ddlfeednumber" runat="server" Width="125px" AutoPostBack="True" OnSelectedIndexChanged="ddlfeednumber_SelectedIndexChanged">
                </asp:DropDownList></td>
        </tr>
        
          <tr>
            <td style="width: 160px; height: 24px;">
                <asp:Label ID="Label2" runat="server" Text="Allotment Month" Width="117px"></asp:Label></td>
            <td style="width: 174px; height: 24px; text-align: left;">
                <asp:DropDownList ID="ddlmonth" runat="server" Width="170px">
                
                                <asp:ListItem Value="1">January</asp:ListItem>
                                <asp:ListItem Value="2">February</asp:ListItem>
                                <asp:ListItem Value="3">March</asp:ListItem>
                                <asp:ListItem Value="4">April</asp:ListItem>
                                <asp:ListItem Value="5">May</asp:ListItem>
                                <asp:ListItem Value="6">June</asp:ListItem>
                                <asp:ListItem Value="7">July</asp:ListItem>
                                <asp:ListItem Value="8">August</asp:ListItem>
                                <asp:ListItem Value="9">September</asp:ListItem>
                                <asp:ListItem Value="10">October</asp:ListItem>
                                <asp:ListItem Value="11">November</asp:ListItem>
                                <asp:ListItem Value="12">December</asp:ListItem>
                
                </asp:DropDownList></td>
            <td style="width: 160px; height: 24px;">
                <asp:Label ID="Label3" runat="server" Text="Allotment Year"></asp:Label></td>
            <td style="width: 190px; height: 24px; text-align: left;">
                <asp:DropDownList ID="ddlyear" runat="server" Width="125px" OnSelectedIndexChanged="ddlyear_SelectedIndexChanged">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 160px; height: 24px">
                <asp:Label ID="Label5" runat="server" Text="Transportation Date" Width="135px"></asp:Label></td>
            <td style="width: 174px; height: 24px; text-align: left">
            
             <asp:TextBox ID="txtTranpDate" runat="server" Width="120px"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_txtTranpDate'
	    });
	     </script>
            
            </td>
            <td style="width: 160px; height: 24px">
            
            <asp:Label ID="lbltransvalid" runat="server" Text="Transportation Validity" Width="167px"></asp:Label>
            
            </td>
            <td style="width: 190px; height: 24px; text-align: left">
            
             <asp:TextBox ID="txtvalid" runat="server" Width="120px"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_txtvalid'
	    });
	     </script>
            
            </td>
        </tr>
        <tr>
            <td style="width: 160px; height: 24px">
                <asp:Label ID="Label10" runat="server" Text="Select FPS Name" Width="130px"></asp:Label></td>
            <td colspan="2" style="height: 24px; text-align: left">
                <asp:DropDownList ID="ddlFps" runat="server" Width="330px">
                </asp:DropDownList></td>
            <td style="width: 190px; height: 24px">
            </td>
        </tr>
        <tr>
            <td style="width: 160px; height: 24px">
                <asp:Label ID="Label11" runat="server" Text="Select Commodity" Width="130px"></asp:Label></td>
            <td ><asp:DropDownList ID="ddlcommodtiy" runat="server" Width="170px">
            </asp:DropDownList></td>
            <td style="width: 190px; height: 24px">
                &nbsp;<asp:Label ID="Label9" runat="server" Text="Allotment Quantity in Qntl" Width="169px"></asp:Label></td>
            
             <td style="width: 190px; height: 24px">
                 &nbsp;<asp:TextBox ID="txtAllotQty" runat="server" Width="120px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 160px; height: 24px">
            </td>
            <td>
            </td>
            <td style="width: 190px; height: 24px">
                <asp:Button ID="btnadd" runat="server" Text="Add this" Width="100px" OnClick="btnadd_Click"  /></td>
            <td style="width: 190px; height: 24px">
            </td>
        </tr>
        
        <tr>
            <td colspan="4">
             <asp:Panel ID="Panel2" runat="server" Visible="False">
             <center>
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="#CCCCCC"
                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2"
                    DataKeyNames="commodityid,FPSCode" Font-Bold="False" Font-Size="Small" ForeColor="Black"
                    Width="500px">
                    <RowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="FPSName" HeaderText="FPS Name" SortExpression="FPSName">
                            <ItemStyle Font-Bold="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="commodity" HeaderText="Commodity" />
                        <asp:BoundField DataField="qty_issue" HeaderText="Allotment Qunatity" SortExpression="qty">
                            <ItemStyle Font-Bold="False" />
                        </asp:BoundField>
                      
                       
                        <asp:BoundField DataField="commodityid" HeaderText="commodityid" Visible="False" />
                        <asp:BoundField DataField="FPSCode" HeaderText="FPS Code" Visible="False" />
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" Font-Size="Small" ForeColor="White" />
                </asp:GridView>
                </center>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td style="width: 160px">
                <asp:Button ID="btnNew" runat="server" Text="New" Width="70px" OnClick="btnNew_Click" /></td>
            <td colspan="2">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" OnClick="btnSave_Click" /></td>
            <td style="width: 190px">
                <asp:Button ID="btnCLose" runat="server" Text="Close" Width="60px" OnClick="btnCLose_Click" /></td>
        </tr>
        <tr>
            <td style="width: 160px">
            </td>
            <td style="width: 174px">
           <%-- <asp:Button ID="btnsms" runat="server" Text="Send SMS" Width="185px" OnClick="btnsms_Click" Visible="False" />--%>
            
            </td>
            <td style="width: 160px">
            </td>
            <td style="width: 190px">
                <asp:HyperLink ID="hlinkpdo" runat="server" Font-Size="11pt" NavigateUrl="#" Visible="False" Width="102px">Print This</asp:HyperLink></td>
        </tr>
        <tr>
            <td style="width: 160px">
            </td>
            <td style="width: 174px">
            </td>
            <td style="width: 160px">
            </td>
            <td style="width: 190px">
            </td>
        </tr>
    </table>
</asp:Content>

