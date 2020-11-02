<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Rack_Receipt_Dtl_Sugar.aspx.cs" Inherits="District_Rack_Receipt_Dtl_Sugar" Title="Rack Receipt Details" %>
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
         <td class="tdmarginro" style="color: white; background-color: lightslategray;">
         </td>
         <td  colspan="3" style="color: white; background-color: lightslategray;" align="left">
             &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
             <strong>
             Rake &nbsp;Receipt&nbsp; Details (From Sugar Factory)</strong></td>
     </tr>
     <tr>
         <td class="tdmarginro" colspan="4" style="color: white; background-color: lightslategray">
             <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>
             <asp:Label ID="lblcmdty" runat="server" Visible="False"></asp:Label></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8">
         <asp:Label ID="lblrhc" runat="server" Visible="False"></asp:Label></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8">
         </td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8">
         </td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8">
         </td>
     </tr>
   <tr>
   <td class ="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;"> 
       <asp:Label ID="Label14" runat="server" Text="Rack No."></asp:Label></td>
     <td class ="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;"> <asp:DropDownList ID="ddlrackno" runat="server"  Width ="153px" AutoPostBack="True" OnSelectedIndexChanged="ddlrackno_SelectedIndexChanged">
         </asp:DropDownList></td>
       <td class ="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lblrackno" runat="server" Text="Rack Number" Visible="False"></asp:Label></td>
         <td style="background-color: #cfdcc8; font-size: 10pt; position: static;" align="left"> &nbsp;<asp:TextBox ID="txtrackno" runat="server" Visible="False" AutoPostBack="True" OnTextChanged="txtrackno_TextChanged" Width="141px"></asp:TextBox></td>
   </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lblrecddist" runat="server" Text="Receiving District" Visible="False" Width="114px"></asp:Label></td>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:DropDownList ID="ddlrecddistrict" runat="server"  Width ="153px" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged" Enabled="False" Visible="False">
             </asp:DropDownList></td>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lblrecdrh" runat="server" Text="Receiving Rail Head" Visible="False"
                 Width="128px"></asp:Label></td>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:DropDownList ID="ddldesrailhead" runat="server" Visible="False" Width="154px" AutoPostBack="True" OnSelectedIndexChanged="ddldesrailhead_SelectedIndexChanged">
                 <asp:ListItem Selected="True" Text="--Select--" Value="01"></asp:ListItem>
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lblrecddistrict" runat="server" Text="Receiving District " Width="114px"></asp:Label></td>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:TextBox ID="txtrecdist" runat="server" Font-Italic="True" ForeColor="Navy"
                 Width="146px" OnTextChanged="txtrecdist_TextChanged"></asp:TextBox></td>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lblrecdrailhed" runat="server" Text="Receiving Rail Head" Width="136px"></asp:Label></td>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:TextBox ID="txtrecrailh" runat="server" Font-Italic="True" ForeColor="Navy" Width="146px"></asp:TextBox></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="Label4" runat="server" Text="Rack Dispatch Qty." Width="131px"></asp:Label></td>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:TextBox ID="txtrackqty" runat="server" Width="146px" Font-Italic="True" ForeColor="Navy"></asp:TextBox></td>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="Label5" runat="server" Text="Rack Dispatch Date" Width="130px"></asp:Label></td>
         <td  style="background-color: #cfdcc8; font-size: 10pt; position: static;" align="left">
             <asp:TextBox ID="txtrackddate" runat="server" Font-Italic="True" ForeColor="Navy"
                 Width="120px" OnTextChanged="txtrackddate_TextChanged"></asp:TextBox><br />
             <asp:Panel ID="Panel1" runat="server">
            
             <asp:TextBox ID="DaintyDate2" runat="server" Width="126px"></asp:TextBox>
              <script type  ="text/javascript">
	             new tcal ({
				            'formname': 'aspnetForm',
				            'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate2'
	                      });
	          </script>    
	           </asp:Panel>         
             </td>
     </tr>
    <tr>
   <td class ="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;"> 
       <asp:Label ID="Label13" runat="server" Text="Recd. Quantity " Width="118px"></asp:Label></td>
     <td class ="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;">   &nbsp;<asp:TextBox ID="txtrecdqty" runat="server" Font-Italic="True" ForeColor="Navy"
             Width="146px" MaxLength="13"></asp:TextBox></td>
       <td  class ="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;"> 
           <asp:Label ID="Label15" runat="server" Text="Rack Recd. On" Width="130px"></asp:Label></td>
         <td  style="background-color: #cfdcc8; font-size: 10pt; position: static;" align="left"> 
             <asp:TextBox ID="DaintyDate1" runat="server" Width="126px"></asp:TextBox>
             
              <script type  ="text/javascript">
	             new tcal ({
				            'formname': 'aspnetForm',
				            'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate1'
	                      });
	          </script>
             </td>
   </tr>
     <tr>
         <td class="tdmarginro" style="color: white; background-color: lightslategray; width: 124px;">
         </td>
         
         <td  colspan="3" style="color: white; background-color: lightslategray;" align="left">
             Details of&nbsp; Issue from Receiving Rail Head</td>
     </tr>
     <tr>
         <td class="tdrailhead" colspan="2" style="background-color: #cfdcdc; font-size: 10pt; width: 165px; position: static;">
             </td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; width: 165px; position: static;"></td>
         <td class="tdrailhead" style="background-color: #cfdcdc; font-size: 10pt; width: 165px; position: static;">
             </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; width: 165px; position: static;">
         </td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; width: 165px; position: static;">
             <asp:Label ID="Label3" runat="server" Text="Challan Number" Width="110px"></asp:Label></td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; width: 165px; position: static;">
             <asp:DropDownList ID="ddlchallan" runat="server"  Width ="133px" AutoPostBack="True" OnSelectedIndexChanged="ddlchallan_SelectedIndexChanged">
             </asp:DropDownList></td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; width: 165px; position: static;">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; width: 165px; position: static;">
             <asp:Label ID="Label6" runat="server" Text="Issued Quantity" Width="108px"></asp:Label></td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; width: 165px; position: static;">
             <asp:TextBox ID="txtissqty" runat="server" Font-Italic="True" ForeColor="Navy" Width="146px"></asp:TextBox></td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; width: 165px; position: static;">
         </td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; width: 165px; position: static;">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; width: 165px; position: static;">
             <asp:Label ID="Label7" runat="server" Text="TruckChallan No.  " Width="115px"></asp:Label>&nbsp;
         </td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; width: 165px; position: static;">
             <asp:TextBox ID="tctchallanno" runat="server" Width="146px"></asp:TextBox></td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; width: 165px; position: static;">
             <asp:Label ID="Label10" runat="server" Text="TC Date"></asp:Label></td>
         <td  style="background-color: #cfdcdc; font-size: 10pt; width: 165px; position: static;" align="left">
             <asp:TextBox ID="DaintyDate3" runat="server" Width="126px"></asp:TextBox>
              <script type  ="text/javascript">
	             new tcal ({
				            'formname': 'aspnetForm',
				            'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate3'
	                      });
	          </script>
             
             </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; width: 165px; position: static;">
             <asp:Label ID="Label8" runat="server" Text="Truck No."></asp:Label></td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; width: 165px; position: static;">
             <asp:TextBox ID="txttruckno" runat="server" Width="146px"></asp:TextBox></td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; width: 165px; position: static;">
             <asp:Label ID="Label9" runat="server" Text="Transporter"></asp:Label></td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; width: 165px; position: static;">
         <asp:DropDownList ID="ddltransporter" runat="server"  Width ="153px">
         </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; width: 165px; position: static;">
             <asp:Label ID="Label11" runat="server" Text="Destnation District" Width="119px"></asp:Label></td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; width: 165px; position: static;"><asp:DropDownList ID="ddldestdistrict" runat="server"  Width ="153px" AutoPostBack="True" OnSelectedIndexChanged="ddldestdistrict_SelectedIndexChanged">
         </asp:DropDownList></td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; width: 165px; position: static;">
             <asp:Label ID="Label12" runat="server" Text="Destination  Issue Center/Storage Place Name"
                 Width="143px"></asp:Label></td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; width: 165px; position: static;">
             <asp:DropDownList ID="ddlissuename" runat="server"  Width ="153px">
         </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; width: 165px; position: static;">
             Dispatch Bags</td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; width: 165px; position: static;">
             <asp:TextBox ID="txtdisbags" runat="server" Width="146px" MaxLength="6"></asp:TextBox></td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; width: 165px; position: static;">
             Dispatch Qty.</td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; width: 165px; position: static;">
             <asp:TextBox ID="txtdisqty" runat="server" Width="126px" MaxLength="13"></asp:TextBox></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; width: 165px; position: static;">
             </td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; width: 165px; position: static;">
             </td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; width: 165px; position: static;">
             <asp:Button ID="btnaddmore" runat="server" Text="Add More" Width="95px" OnClick="btnaddmore_Click" /></td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; width: 165px; position: static;">
             </td>
     </tr>
 </table>
<table>
 
 <tr>
 <td> 
  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Width="597px" OnRowDeleting="GridView1_RowDeleting">
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
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
        <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" Font-Bold="True" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" CssClass="gridheader" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
      <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
      <EditRowStyle BackColor="#999999" />
    </asp:GridView>
 </td>
 </tr>
 </table>
 <table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: green 3px double; border-top: green 3px double; border-left: green 3px double; border-bottom: green 3px double; width: 602px;" id="Table1" >
     <tr>
         <td colspan="4">
             <asp:Label ID="Label2" runat="server" Font-Italic="True" ForeColor="Maroon" Visible="False"></asp:Label></td>
     </tr>
      <tr>
         <td class="tdmarginro" style="width: 124px; height: 25px;">
         </td>
         <td class="tdmarginro" align="right" style="height: 25px">
         <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="95px" OnClick="btnSubmit_Click" /></td>
         <td class="tdmarginro" style="height: 25px">
         <asp:Button ID="btnClose" runat="server" Text="Close" Width="95px" OnClick="btnClose_Click" /></td>
         <td class="tdmarginro" style="height: 25px">
         </td>
     </tr>
     </table> 
</asp:Content>


