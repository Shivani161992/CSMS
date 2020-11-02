<%@ Page Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="ZoneMaster_frm.aspx.cs" Inherits="State_ZoneMaster_frm" Title="Zone Master" %>


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
  
  <script type="text/javascript">
        function CheckRow(objRef) {
            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;
            if (objRef.checked) {
                //Change the gridview row color when checkbox checked change
                row.style.backgroundColor = "#cccccc";
            }
            else {
                //If checkbox not checked change default row color
                if (row.rowIndex % 2 == 0) {
                    //Alternating Row Color
                    row.style.backgroundColor = "#AED6FF";
                }
                else {
                    row.style.backgroundColor = "white";
                }
            }

            //Get the reference of GridView
            var GridView = row.parentNode;

            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];

                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;

        }

        function checkAllRow(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        //If the header checkbox is checked
                        //check all checkboxes
                        //and highlight all rows
                        row.style.backgroundColor = "#5CADFF";
                        inputList[i].checked = true;
                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        //and change rowcolor back to original
                        if (row.rowIndex % 2 == 0) {
                            //Alternating Row Color
                            row.style.backgroundColor = "#AED6FF";
                        }
                        else {
                            row.style.backgroundColor = "white";
                        }
                        inputList[i].checked = false;
                    }
                }
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
    <div style="text-align: center">
        <table style="width: 664px">
            <tr>
                <td colspan="8" style="height: 21px; background-color: #ff6600">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" 
                        Text="Empaneled Sugar/Salt Supplier" Font-Italic="True" Font-Size="Large"></asp:Label></td>
                <td colspan="1" style="width: 100px; height: 21px; background-color: #ff6600">
                </td>
            </tr>
            <tr>
                <td colspan="8" style="height: 2px; background-color: #b0cff2">
                    <table style="width: 759px; height: 1px">
                        <tr>
                            <td align="left" style="width: 104px; height: 24px;">
                                <asp:Label ID="Label5" runat="server" Font-Bold="True" Text="Financial Year :" Width="120px"></asp:Label></td>
                            <td align="left" style="width: 95px; height: 24px;">
                                <asp:DropDownList ID="ddlfinancialyear" runat="server" Width="154px">
                                <asp:ListItem Value="01" Selected="True">2014-2015</asp:ListItem>
                                                <asp:ListItem Value="02">2013-2014</asp:ListItem>
                                                <asp:ListItem Value="03">2012-2013</asp:ListItem>
                                                <asp:ListItem Value="04">2011-2012</asp:ListItem>
                                                <asp:ListItem Value="05">2010-2011</asp:ListItem>
                                                <asp:ListItem Value="06">2009-2010</asp:ListItem>
                                                <asp:ListItem Value="07">2008-2009</asp:ListItem>
                                                <asp:ListItem Value="08">2007-2008</asp:ListItem>
                                                <asp:ListItem Value="09">2006-2007</asp:ListItem>
                                
                                </asp:DropDownList></td>
                            <td align="left" style="width: 100px; height: 24px;">
                                <strong>Filled Suppliers</strong></td>
                            <td align="left" style="width: 98px; height: 24px;">
                                <asp:DropDownList ID="ddl_filledsuplyer" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddl_filledsuplyer_SelectedIndexChanged" 
                                    style="height: 22px" Width="180px">
                                </asp:DropDownList>
                                </td>
                        </tr>
                        <tr>
                            <td style="width: 104px" align="left">
                                <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="Supplier Name:" Width="133px"></asp:Label></td>
                            <td align="left" colspan="3">
                                <asp:TextBox ID="txtsupplier" runat="server" Width="487px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtsupplier"
                                    ErrorMessage="Required Supplier Name" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
                        </tr>
                    
                       
                        <tr>
                            <td style="width: 104px" align="left">
                                <asp:Label ID="Label7" runat="server" Font-Bold="True" Text="Address :"></asp:Label></td>
                            <td align="left" colspan="3">
                                <asp:TextBox ID="txtaddress" runat="server" Width="487px" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtaddress"
                                    ErrorMessage="Required Address" ValidationGroup="1">*</asp:RequiredFieldValidator>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 104px; height: 24px" align="left">
                                <asp:Label ID="Label17" runat="server" Font-Bold="True" Text="Pin Code :"></asp:Label></td>
                            <td align="left" colspan="2" style="height: 24px">
                                <asp:TextBox ID="txtpincode" runat="server" MaxLength="6"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtpincode"
                                    ErrorMessage="Required Pin Code" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
                            <td align="left" style="width: 98px; height: 24px">
                            </td>
                        </tr>
                       
                        <tr>
                            <td style="width: 104px; height: 24px" align="left">
                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Mobile/Office No :"
                                    Width="129px"></asp:Label></td>
                            <td align="left" style="width: 95px; height: 24px">
                                &nbsp;<asp:TextBox ID="txtmobileno" runat="server" Width="145px" Height="16px" MaxLength="10"></asp:TextBox>
                                </td>
                            <td align="left" style="width: 100px; height: 24px">
                                <asp:Label ID="Label11" runat="server" Font-Bold="True" Text="Tender Rate :" Font-Size="Large" ForeColor="Red" Width="116px"></asp:Label></td>
                            <td align="left" style="width: 98px; height: 24px">
                                &nbsp;<asp:TextBox ID="txttenderrate" runat="server" MaxLength="6"></asp:TextBox>
                                <asp:Label ID="Label12" runat="server" Font-Size="Small" Text="Rupees Per Ton" Width="99px"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txttenderrate"
                                    ErrorMessage="Required Tender Rate" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
                        </tr>
                       
                        <tr>
                            <td style="width: 104px; height: 24px;" align="left">
                                <asp:Label ID="Label8" runat="server" Font-Bold="True" Text="Supplier State :"></asp:Label></td>
                            <td align="left" style="width: 95px; height: 24px;">
                                <asp:DropDownList ID="ddlstate" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged"
                                    Width="226px">
                                </asp:DropDownList></td>
                            <td align="left" style="width: 100px; height: 24px;">
                                <asp:Label ID="Label9" runat="server" Font-Bold="True" Text="Supplier District :" Width="151px"></asp:Label></td>
                            <td align="left" style="width: 98px; height: 24px;">
                                <asp:DropDownList ID="ddldistrict" runat="server" AutoPostBack="True" Width="176px" Height="21px">
                                </asp:DropDownList></td>
                        </tr>
                        
                        <tr>
                            <td style="width: 104px" align="left">
                                <asp:Label ID="Label15" runat="server" Font-Bold="True" Text="Zone  Name :" Width="111px"></asp:Label></td>
                            <td align="left" style="width: 95px">
                                <asp:DropDownList ID="ddlzonename" runat="server" AutoPostBack="True" Width="226px" OnSelectedIndexChanged="ddlzonename_SelectedIndexChanged" Height="20px">
                                </asp:DropDownList></td>
                            <td align="left" style="width: 100px">
                                <asp:Label ID="Label16" runat="server" Font-Bold="True" Text="Zone Code :"></asp:Label></td>
                            <td align="left" style="width: 98px">
                                &nbsp;<asp:TextBox ID="txtzonecode" runat="server" Width="67px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 104px; height: 21px;" align="left">
                                <asp:Label ID="Label10" runat="server" Font-Bold="True" Text="NIT No :" Width="76px"></asp:Label></td>
                            <td align="left" style="width: 95px; height: 21px;">
                                <asp:TextBox ID="txtbruitno" runat="server" MaxLength="6"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtbruitno"
                                    ErrorMessage="Required Bruit No" ValidationGroup="1">*</asp:RequiredFieldValidator>
                               
                            
                            </td>
                            <td align="left" style="width: 100px; height: 21px">
                                </td>
                            <td align="left" style="width: 98px; height: 21px">
                             
                             </td>
                        </tr>
                        <tr>
                            <td style="width: 104px; height: 21px" align="left">
                                <asp:Label ID="Label14" runat="server" Font-Bold="True" Text="निविदा अवधि से :" Width="127px"></asp:Label></td>
                            <td align="left" style="width: 95px; height: 21px">
                                <asp:TextBox ID="txtnfromdate" runat="server"></asp:TextBox>
                                 <script type  ="text/javascript">
	                   new tcal ({
				           'formname': '0',
				           'controlname': 'ctl00_ContentPlaceHolder1_txtnfromdate'
	                             });
	                          </script>
                                
                                </td>
                            <td align="left" style="width: 100px; height: 21px">
                                <asp:Label ID="Label18" runat="server" Font-Bold="True" Text="निविदा अवधि तक:" Width="129px"></asp:Label></td>
                            <td align="left" style="width: 98px; height: 21px">
                                <asp:TextBox ID="txtntodate" runat="server" Width="139px"></asp:TextBox>
                                 <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_txtntodate'
	    });
	     </script>
                                
                                
                                
                                </td>
                        </tr>
                       
                     
                    
                       
                       
                    </table>
                </td>
                <td colspan="1" style="width: 100px; height: 2px; background-color: #b0cff2">
                </td>
            </tr>
            <tr>
                <td colspan="8" rowspan="1" style="height: 9px; background-color: #66ccff">
                    <asp:Label ID="Label20" runat="server" Font-Bold="True" Text="Please Select Districts"></asp:Label></td>
                <td colspan="1" rowspan="1" style="width: 100px; height: 9px; background-color: #66ccff">
                </td>
            </tr>
            <tr>
                <td colspan="8" style="height: 221px">
                    <div style="width: 86%; height: 194px; overflow: scroll">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" GridLines="Horizontal" OnSelectedIndexChanged="GridView1_SelectedIndexChanged3" Width="698px" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" Height="136px">
                            
                            <RowStyle BackColor="White" ForeColor="#333333" />
                            <Columns>
                                <asp:TemplateField>
                                     <HeaderTemplate>
            <asp:CheckBox ID="CheckBox1" runat="server" 
                                       onclick="checkAllRow(this);" />
        </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" onclick="CheckRow(this);" OnCheckedChanged="CheckBox1_CheckedChanged2"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="District_code" DataField="district_code" SortExpression="district_code" />
                                <asp:BoundField HeaderText="Districts Name" DataField="district_name" SortExpression="district_name" />
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#333333" />
                            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                            <PagerSettings NextPageText="Last" PreviousPageText="First" />
                        </asp:GridView>
                    </div>
                </td>
                <td colspan="1" style="width: 100px; height: 221px">
                </td>
            </tr>
            <tr>
               
                <td style="width: 100px; background-color: #ff6600; height: 26px;" align="right">
                    <asp:Button ID="Button1" runat="server"  OnClick="Button1_Click1" Text="Save" Width="135px"  ValidationGroup="1"/></td>
               
                <td style="width: 89px; background-color: #ff6600; height: 26px;" align="right">
                    <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Add New Entry" Width="140px" /></td>
                <td align="right" style="width: 89px; height: 26px; background-color: #ff6600">
                    <asp:Button ID="Button2" runat="server" Text="Close" Width="105px" OnClick="Button2_Click1" /></td>
                
                
              
                
            </tr>
            <tr>
                <td colspan="8" style="background-color: #66ccff">
                    <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Italic="False" ForeColor="Black"
                        Text="You Have Selected Zone Name And Entered Tender Rate Per MT"></asp:Label></td>
                <td colspan="1" style="width: 100px; background-color: #66ccff">
                </td>
            </tr>
            
            <tr>
                <td colspan="8" align="center">
                    <div style="width: 92%; height: 361px; overflow: scroll">
                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="None" Width="721px" Height="99px">
                        
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField HeaderText="Supplier Name" DataField="Name" SortExpression="Supplier Name" />
                            <asp:BoundField HeaderText="Zone Code"  DataField="ZoneCode" SortExpression="Zone Code"/>
                            <asp:BoundField HeaderText="Zone Name"  DataField="Zone" SortExpression="Zone Name"/>
                            <asp:BoundField HeaderText="Tender" DataField="Tender_Rate" SortExpression="Tender" />
                        </Columns> 
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#999999" Font-Size="Small" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:GridView>
                    </div>
                </td>
                <td align="center" colspan="1" style="width: 100px">
                </td>
            </tr>
            <tr>
                <td colspan="8" style="background-color: #66ccff">
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="You Have Selected Zone Name and Distirct Name"></asp:Label></td>
                <td colspan="1" style="width: 100px; background-color: #66ccff">
                </td>
            </tr>
            
            <tr>
                <td colspan="8">
                    <div style="width: 96%; height: 382px; overflow: scroll">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="None" Width="767px">
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField HeaderText="Zone Name" DataField="Zone" SortExpression="Zone" />
                            <asp:BoundField HeaderText="District Name" DataField="district_name" SortExpression="district_name" />
                            <asp:BoundField HeaderText="NIT No" DataField="Bruit_No" SortExpression="Bruit_No" />
                            <asp:BoundField HeaderText="Tender Rate Per MT" DataField="Tender_Rate" SortExpression="Tender_Rate" />
                        </Columns>
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:GridView>
                    </div>
                    </td>
                <td colspan="1" style="width: 100px">
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    <asp:Label ID="Label3" runat="server"></asp:Label></td>
                <td colspan="2">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ValidationGroup="1" Width="315px" />
                </td>
                <td colspan="1" style="width: 76px">
                </td>
                <td colspan="1">
                </td>
                <td colspan="1">
                </td>
                <td colspan="1" style="width: 240px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
            </tr>
         
        </table>
    </div>

</asp:Content>

