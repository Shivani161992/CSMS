<%@ Page Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="SupplyOrder_frm.aspx.cs" Inherits="State_SupplyOrder_frm" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <script type="text/javascript">
function CheckCalDate(tx){
var AsciiCode = event.keyCode;
var txt=tx.value;
var txt2 = String.fromCharCode(AsciiCode);
var txt3=txt2*1;
if ((AsciiCode > 0))
{
//alert('Please Click on Calander Controll to Enter Date');
event.cancelBubble = true;
event.returnValue = false;
}
}
</script>
    <script language="javascript" type="text/javascript">
function GetDialog() 
{
if (confirm("सुरक्षित करने से पहले जांच ले ,संसोधन संभव नहीं हो सकेगा"))
 {
 return true;

} 
else 
{
return false;
}

}
</script>
    <table border="2" style="width: 650px; background-color: #ffffcc; height: 689px;">
        <tr>
            <td colspan="4" style="height: 25px; background-color: #ff6600;">
                <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Large"
                    Text="Sugar Supply Order (Dispatch to IssueCenters)" ForeColor="White"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 100px" align="left">
                <asp:Label ID="Label1" runat="server" Text="Supplier Name :"></asp:Label></td>
            <td style="width: 88px" align="left">
                <asp:DropDownList ID="ddlsupplier" runat="server" Width="230px" AutoPostBack="True" OnSelectedIndexChanged="ddlsupplier_SelectedIndexChanged2">
                </asp:DropDownList></td>
            <td style="width: 70px" align="left">
                <asp:Label ID="Label4" runat="server" Text="Financial Year :" Width="102px"></asp:Label></td>
            <td style="width: 69px">
                <asp:DropDownList ID="ddlcropyear" runat="server" Width="184px">
                <asp:ListItem Value="01" Selected="True">2014-2015</asp:ListItem>
                                                <asp:ListItem Value="02">2013-2014</asp:ListItem>
                                                <asp:ListItem Value="03">2012-2013</asp:ListItem>
                                                <asp:ListItem Value="04">2011-2012</asp:ListItem>
                                                <asp:ListItem Value="05">2010-2011</asp:ListItem>
                                                <asp:ListItem Value="06">2009-2010</asp:ListItem>
                                                <asp:ListItem Value="07">2008-2009</asp:ListItem>
                                                <asp:ListItem Value="08">2007-2008</asp:ListItem>
                                                <asp:ListItem Value="09">2006-2007</asp:ListItem>
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td style="width: 100px" align="left">
                <asp:Label ID="Label3" runat="server" Text="Order No :"></asp:Label></td>
            <td style="width: 88px" align="left">
                <asp:TextBox ID="txtorderno" runat="server" Width="130px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Order No Required"
                    ValidationGroup="1" ControlToValidate="txtorderno" Width="1px">*</asp:RequiredFieldValidator></td>
            <td style="width: 70px" align="left">
                <asp:Label ID="Label2" runat="server" Text="Order Date :" Width="81px"></asp:Label></td>
            <td style="width: 69px">
                <asp:TextBox ID="txtdate" runat="server"></asp:TextBox>
                 <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_txtdate'
	    });
	     </script>
                
                
                </td>
        </tr>
        <tr>
            <td align="left" style="width: 100px">
                <asp:Label ID="lblzonename" runat="server" Text="Zone Name :"></asp:Label></td>
            <td align="left" style="width: 88px">
                <asp:DropDownList ID="ddlzonecode" runat="server" AutoPostBack="True" Width="231px" OnSelectedIndexChanged="ddlzonecode_SelectedIndexChanged1">
                </asp:DropDownList>
                <asp:Label ID="lbl_zonename" runat="server" Visible="False"></asp:Label>
            </td>
            <td align="left" style="width: 70px">
                <asp:Label ID="Label11" runat="server" Text="Zone Code:" Width="79px"></asp:Label></td>
            <td align="left" style="width: 69px">
                <asp:TextBox ID="txtzonecodes" runat="server" Width="83px"></asp:TextBox></td>
        </tr>
       
        <tr>
            <td style="width: 100px" align="left">
                <asp:Label ID="Label5" runat="server" Text="Districts :"></asp:Label></td>
            <td style="width: 88px" align="left">
                <asp:DropDownList ID="ddldistricts" runat="server" Width="151px" AutoPostBack="True" OnSelectedIndexChanged="ddldistricts_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:Label ID="lbl_district" runat="server" Visible="False"></asp:Label>
            </td>
            <td style="width: 70px" align="left">
                <asp:Label ID="Label6" runat="server" Text="Issue Centers :" Width="93px"></asp:Label></td>
            <td style="width: 69px">
                <asp:DropDownList ID="ddlissuecenters" runat="server" Width="168px" AutoPostBack="True" Height="18px" OnSelectedIndexChanged="ddlissuecenters_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:Label ID="lbl_issuecentre" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 100px; height: 36px;" align="left">
                <asp:Label ID="Label7" runat="server" Text="Qty :" Width="77px"></asp:Label></td>
            <td style="width: 88px; height: 36px;" align="left">
                <asp:TextBox ID="txtqty" runat="server" MaxLength="5"></asp:TextBox>
                <asp:Label ID="Label10" runat="server" Font-Size="Small" Text="in MT"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtqty"
                    ErrorMessage="Qty Required" ValidationGroup="1">*</asp:RequiredFieldValidator>
                <asp:Label ID="lbl_Check" runat="server"></asp:Label>
            </td>
            <td style="width: 70px; height: 36px;" align="left">
                <asp:Label ID="Label12" runat="server" Text="Tender Rate:" Width="93px" Font-Bold="True" ForeColor="Red"></asp:Label></td>
            <td style="width: 69px; height: 36px;" align="left">
                <asp:TextBox ID="txtrate" runat="server" Width="122px"></asp:TextBox>
                <asp:Label ID="Label13" runat="server" Text="in Rupees Per Ton" Width="117px" Font-Size="Small"></asp:Label>&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 23px; background-color: #ff6600;">
                <div style="overflow:auto; height:304px; width:100%;" >
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        CellPadding="4" GridLines="Horizontal" Width="100%"  PageSize="8" 
                        Height="132px" BackColor="White" BorderColor="#336666" BorderStyle="Double" 
                        BorderWidth="3px" 
                        DataKeyNames="district_code,Depot_ID,S_name,Dispatch_Date,Orderno,ZoneCode,Financial_Year,Qty,TenderRate" 
                        EnableModelValidation="True" 
                        onselectedindexchanged="GridView1_SelectedIndexChanged" 
                        >
                    <RowStyle BackColor="White" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField HeaderText="Order No" DataField="Orderno" SortExpression="Orderno" />
                        <asp:BoundField HeaderText="Order Date" DataField="Dispatch_Date" SortExpression="Dispatch_Date" />
                        <asp:BoundField HeaderText="Supplier" DataField="S_name" SortExpression="S_name"/>
                        <asp:BoundField HeaderText="Zone Name" DataField="Zone" SortExpression="Zone" />
                        <asp:BoundField HeaderText="Districts Name" DataField="district_name" SortExpression="district_name" />
                        <asp:BoundField HeaderText="Depot Name" DataField="DepotName" SortExpression="DepotName" />
                        <asp:BoundField HeaderText="Qty (IN MT)"  DataField="Qty" SortExpression="Qty"/>
                        <asp:BoundField DataField="Depot_ID" HeaderText="Depot_ID" Visible="False" />
                        <asp:BoundField DataField="district_code" HeaderText="districtcode" 
                            Visible="False" />
                        <asp:CommandField ShowSelectButton="True" />
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#333333" />
                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                    <PagerSettings NextPageText="last" PreviousPageText="first" />
                </asp:GridView>
                </div>
                </td>
        </tr>
        <tr>
            <td style="width: 100px; height: 23px">
                <asp:Button ID="btnsave" runat="server" Text="Save Entry" Width="112px" OnClientClick="return GetDialog()" OnClick="btnsave_Click" ValidationGroup="1" /></td>
            <td style="width: 88px; height: 23px">
                <asp:Button ID="btn_update" runat="server" onclick="btn_update_Click" 
                    Text="Update" Visible="False" />
                <asp:Button ID="Button2" runat="server" PostBackUrl="~/State/State_Welcome.aspx"
                    Text="Close Form" Width="106px" /></td>
            <td style="width: 70px; height: 23px">
                </td>
            <td style="width: 69px; height: 23px">
                <asp:Button ID="Button4" runat="server" Text="Add New Entry" Width="105px" OnClick="Button4_Click" PostBackUrl="~/State/SupplyOrder_frm.aspx" /></td>
        </tr>
      
        <tr>
            <td style="width: 100px">
                <asp:Label ID="Label9" runat="server"></asp:Label></td>
            <td style="width: 88px">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ValidationGroup="1" Width="239px" />
            </td>
            <td style="width: 70px">
            </td>
            <td style="width: 69px">
            </td>
        </tr>
       
        <tr>
            <td colspan="4">
                <asp:HiddenField ID="hd_district" runat="server" />
                <asp:HiddenField ID="hd_zone" runat="server" />
                <asp:HiddenField ID="hd_issuecentre" runat="server" />
            </td>
        </tr>
    </table>
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
 
</asp:Content>

