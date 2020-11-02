<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Edit_Rack_Receipt_Dtl.aspx.cs" Inherits="District_Edit_Rack_Receipt_Dtl" Title="Rack Receipt Details" %>
 
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        
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
var coda = event.keyCode
 if(coda == 46)
 {
    alert('Decimal cannot come twice');
    event.cancelBubble = true;
    event.returnValue = false;
 }
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
 <table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: green 3px double; border-top: green 3px double; border-left: green 3px double; border-bottom: green 3px double; width: 602px;" >
     <tr>
         <td class="tdmarginro" style="width: 124px">
         </td>
         <td  colspan="3" style="color: navy; font-style: italic; font-weight: bold;" align="left">
             Rack Receipt Details </td>
     </tr>
     <tr>
         <td class="tdmarginro" colspan="4">
             <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>
             <asp:Label ID="lblcmdty" runat="server" Visible="False"></asp:Label></td>
     </tr>
   <tr>
   <td class ="tdmarginro" style="width: 124px"> Sending District</td>
     <td class ="tdmarginro" style="width: 87px"><asp:DropDownList ID="ddldistrict" runat="server"  Width ="153px" AutoPostBack="True" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged" Enabled="False">
                            
                        </asp:DropDownList></td>
       <td class ="tdmarginro">
             <asp:Label ID="lblrackno" runat="server" Text="Rack Number"></asp:Label></td>
         <td class ="tdmarginro"> &nbsp;<asp:TextBox ID="txtrackno" runat="server" AutoPostBack="True" OnTextChanged="txtrackno_TextChanged" Width="72px" ReadOnly="True"></asp:TextBox></td>
   </tr>
     <tr>
         <td class="tdmarginro" style="width: 124px">
             <asp:Label ID="lblrecddist" runat="server" Text="Receiving District"></asp:Label></td>
         <td class="tdmarginro" style="width: 87px">
             <asp:DropDownList ID="ddlrecddistrict" runat="server"  Width ="153px" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged" Enabled="False">
             </asp:DropDownList></td>
         <td class="tdmarginro">
             <asp:Label ID="lblrecdrh" runat="server" Text="Receiving Rail Head"
                 Width="128px"></asp:Label></td>
         <td class="tdmarginro">
             <asp:TextBox ID="txtrecrailh" runat="server" Font-Italic="True" ForeColor="Navy" Width="146px" ReadOnly="True"></asp:TextBox></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="width: 124px">
             <asp:Label ID="Label4" runat="server" Text="Rack Dispatch Qty." Width="119px"></asp:Label></td>
         <td class="tdmarginro" style="width: 87px">
             <asp:TextBox ID="txtrackqty" runat="server" Width="146px" Font-Italic="True" ForeColor="Navy"></asp:TextBox></td>
         <td class="tdmarginro">
             <asp:Label ID="Label5" runat="server" Text="Recd. Quantity " Width="127px"></asp:Label></td>
         <td class="tdmarginro">
             <br />
             <asp:TextBox ID="txtrecdqty" runat="server" Font-Italic="True" ForeColor="Navy"
             Width="146px" MaxLength="13" ReadOnly="True"></asp:TextBox></td>
     </tr>
    <tr>
   <td class ="tdmarginro" style="width: 124px"> 
   </td>
     <td class ="tdmarginro" style="width: 87px">   &nbsp;</td>
       <td class ="tdmarginro"> Rack Recd On</td>
         <td class ="tdmarginro">                   
             <asp:TextBox ID="DaintyDate1" runat="server" ReadOnly="True"></asp:TextBox>
          
         </td>
   </tr>
     <tr>
         <td class="tdmarginro" style="width: 124px">
         </td>
         
         <td  colspan="3" style="color: navy; font-style: italic;" align="left">
             <strong>Details of&nbsp; Issue from Receiving Rail Head</strong></td>
     </tr>
     <tr>
         <td class="tdrailhead" colspan="2" style="height: 15px">
             </td>
         <td class="tdmarginro" style="height: 15px"></td>
         <td class="tdrailhead" style="height: 15px">
             </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="width: 124px">
             Issued Quantity</td>
         <td class="tdmarginro" style="width: 87px">
             <asp:TextBox ID="txtissqty" runat="server" Font-Italic="True" ForeColor="Navy" Width="146px" OnTextChanged="txtissqty_TextChanged"></asp:TextBox></td>
         <td class="tdmarginro">
         </td>
         <td class="tdmarginro">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="width: 124px">
             TruckChallan No. &nbsp;&nbsp;
         </td>
         <td class="tdmarginro" style="width: 87px">
             <asp:TextBox ID="tctchallanno" runat="server" Width="146px" ForeColor="#0000C0" ReadOnly="True"></asp:TextBox></td>
         <td class="tdmarginro">
             TC Date</td>
         <td class="tdmarginro">
                
             <asp:TextBox ID="DaintyDate3" runat="server"></asp:TextBox>             
              <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate3'
	    });
function Table1_onclick() {

}

	     </script>
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="width: 124px">
             Truck No.</td>
         <td class="tdmarginro" style="width: 87px">
             <asp:TextBox ID="txttruckno" runat="server" Width="146px"></asp:TextBox></td>
         <td class="tdmarginro">
             Transporter</td>
         <td class="tdmarginro">
         <asp:DropDownList ID="ddltransporter" runat="server"  Width ="153px">
         </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="width: 124px">
             Destination District
         </td>
         <td class="tdmarginro" style="width: 87px">
             <asp:DropDownList ID="ddldestdist" runat="server"  Width ="153px" OnSelectedIndexChanged="ddldestdist_SelectedIndexChanged" AutoPostBack="True">
             </asp:DropDownList></td>
         <td class="tdmarginro">
             Destination Issue Center/Storage Place Name</td>
         <td class="tdmarginro">
             <asp:DropDownList ID="ddlissuename" runat="server"  Width ="153px" OnSelectedIndexChanged="ddlissuename_SelectedIndexChanged">
         </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="width: 124px">
             Dispatch Bags</td>
         <td class="tdmarginro" style="width: 87px">
             <asp:TextBox ID="txtdisbags" runat="server" Width="146px" MaxLength="6"></asp:TextBox></td>
         <td class="tdmarginro">
             Dispatch Qty.</td>
         <td class="tdmarginro">
             <asp:TextBox ID="txtdisqty" runat="server" Width="146px" MaxLength="13"></asp:TextBox></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="width: 124px">
             </td>
         <td class="tdmarginro" style="width: 87px">
             </td>
         <td class="tdmarginro">
             <asp:Button ID="btnaddmore" runat="server" Text="Add More" Width="95px" OnClick="btnaddmore_Click" Visible="False" /></td>
         <td class="tdmarginro">
             </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="width: 124px">
         </td>
         <td class="tdmarginro" style="width: 87px">
         </td>
         <td class="tdmarginro">
             <asp:DropDownList ID="ddlrackno" runat="server"  Width ="101px" AutoPostBack="True" OnSelectedIndexChanged="ddlrackno_SelectedIndexChanged" Visible="False">
         </asp:DropDownList></td>
         <td class="tdmarginro">
             <asp:Label ID="lblrhc" runat="server" Visible="False"></asp:Label></td>
     </tr>
 </table>
<table>
 
 <tr>
 <td> 
  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Width="597px" OnRowDeleting="GridView1_RowDeleting">
        <FooterStyle BackColor="Tan" />
        <Columns>
            <asp:CommandField HeaderText="Action" SelectText="Delete" ShowSelectButton="True" />
            <asp:BoundField DataField="Rack_No" HeaderText="Rack No." SortExpression="Rack_No">
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="IC_ID" HeaderText="I.C. ID" SortExpression="IC_ID">
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="IC_Name" HeaderText="I.C. Name" SortExpression="IC_Name">
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Challan_No" HeaderText="TC No." SortExpression="Challan_No">
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField HeaderText="TC Date" DataField="Challan_date" SortExpression="Challan_date">
                <HeaderStyle CssClass="gridlarohead" />
                <ItemStyle CssClass="griditemlaro" />
            </asp:BoundField>
            <asp:BoundField DataField="Transporter_Name" HeaderText="Transporter" SortExpression="Transporter_Name">
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField HeaderText="TID" DataField="Transporter_ID" SortExpression="Transporter_ID">
                <HeaderStyle CssClass="gridlarohead" />
                <ItemStyle CssClass="griditemlaro" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Truck No." DataField="Truck_No" SortExpression="Truck_No">
                <HeaderStyle CssClass="gridlarohead" />
                <ItemStyle CssClass="griditemlaro" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Disp Bags" DataField="Disp_Bags" SortExpression="Disp_Bags">
                <HeaderStyle CssClass="gridlarohead" />
                <ItemStyle CssClass="griditemlaro" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Disp. Qty." DataField="Disp_Qty" SortExpression="Disp_Qty">
                <HeaderStyle CssClass="gridlarohead" />
                <ItemStyle CssClass="griditemlaro" />
            </asp:BoundField>
        </Columns>
        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
        <HeaderStyle BackColor="Tan" Font-Bold="True" CssClass="gridheader" />
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
    </asp:GridView>
 </td>
 </tr>
 </table>
 <table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: green 3px double; border-top: green 3px double; border-left: green 3px double; border-bottom: green 3px double; width: 602px;" id="Table1" language="javascript" onclick="return Table1_onclick()" >
     <tr>
         <td colspan="4">
             <asp:Label ID="Label2" runat="server" Font-Italic="True" ForeColor="Maroon" Visible="False"></asp:Label></td>
     </tr>
      <tr>
         <td class="tdmarginro" style="width: 124px; height: 25px;">
         </td>
         <td class="tdmarginro" align="right" style="height: 25px">
         <asp:Button ID="btnSubmit" runat="server" Text="Update" Width="95px" OnClick="btnSubmit_Click" /></td>
         <td class="tdmarginro" style="height: 25px">
         <asp:Button ID="btnClose" runat="server" Text="Close" Width="95px" OnClick="btnClose_Click" /></td>
         <td class="tdmarginro" style="height: 25px">
         </td>
     </tr>
     </table> 
</asp:Content>


