<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Dispatch_FCI_byRack.aspx.cs" Inherits="IssueCenter_Dispatch_FCI_byRack" Title="Dispatch By Rack" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="hedl"> &nbsp;</div>
<div id ="ronewmargin">
<center>

<table cellpadding ="0" cellspacing ="0" class ="tablelayout" style="width: 587px">

<tr>
 <td style="width: 830px">
 <table border ="0" cellpadding ="0" cellspacing ="0" style="width: 750px; height: 657px;">
     <tr>
         <td align="center"  colspan="4" style="border-right: lightblue 1px solid;
             border-top: lightblue 1px solid; border-left: lightblue 1px solid; border-bottom: lightblue 1px solid;
             background-color: lightslategray">
    <asp:Label ID="lbltransrailhead" runat="server" Text="Dispatch to FCI by Rack" Font-Bold="True" ForeColor="White" Width="357px" Font-Size="13px"></asp:Label></td>
     </tr>
     <tr>
         <td  colspan="4" style="border-right: lightblue 1px solid; border-top: lightblue 1px solid;
             border-left: lightblue 1px solid; border-bottom: lightblue 1px solid; background-color: #cccccc" align="center">
    <asp:Label ID="lbltransferdepot" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#004000"
        Text="To be entered By Sending Issue Center" Width="300px"></asp:Label></td>
     </tr>
<%-- <tr>
 <td class ="tdmarginro" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;"></td>
 <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
     <asp:TextBox ID="txtrono" runat="server" Width ="148px" Visible="False"></asp:TextBox>
 </td>
     <td class ="tdmarginro" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
         </td>
  <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;"> <asp:TextBox ID="txtroqty" runat="server"  Width ="130px" Visible="False"></asp:TextBox></td>
 
 </tr>--%>
     <tr>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 162px;">
             <asp:Label ID="lblDistrictName" runat="server" Text="Sending District"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 194px;">
             <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Navy"
                 Width="157px"></asp:Label></td>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 108px;">
             <asp:Label ID="lblNameDepot" runat="server" Text="Sending Depot"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 232px;">
             <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Navy"
                 Width="128px"></asp:Label></td>
     </tr>
      <tr>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; width: 162px; border-bottom: silver 1px solid;
             position: static; background-color: #cfdcc8; height: 24px;">
             <asp:Label ID="lblrackno" runat="server" Text="Rack Number"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; width: 194px; border-bottom: silver 1px solid;
             position: static; background-color: #cfdcc8; height: 24px;">
             <asp:DropDownList ID="ddlrackno" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlrackno_SelectedIndexChanged"
                 Width="256px">
             </asp:DropDownList></td>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; width: 108px; border-bottom: silver 1px solid;
             position: static; background-color: #cfdcc8; height: 24px;">
             <asp:Label ID="lblstate" runat="server" Text="Receiving State" Width="149px"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; width: 232px; border-bottom: silver 1px solid;
             position: static; background-color: #cfdcc8; height: 24px;">
             <asp:DropDownList ID="ddlstate" runat="server" Width="227px" AutoPostBack="True" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged" Enabled="False">
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 162px; height: 24px;">
             <asp:Label ID="Label5" runat="server" Text="Stock Issued From" Width="136px"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 194px; height: 24px;">
             <asp:DropDownList ID="ddlsarrival" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlsarrival_SelectedIndexChanged"
                 Width="202px">
             </asp:DropDownList></td>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 108px; height: 24px;">
             &nbsp;
             <asp:Label ID="Label2" runat="server" Text="Dispatch Type" Width="128px" 
                 Height="16px"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 232px; height: 24px;">
             &nbsp;
             <asp:DropDownList ID="ddlDisType" runat="server" Width="151px" OnSelectedIndexChanged="ddlDisType_SelectedIndexChanged" AutoPostBack="True" Enabled="False">
                 
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 162px;">
             <asp:Label ID="lbltono" runat="server" Text="Transport Order No." Width="138px" Visible="False"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 194px;">
             <asp:DropDownList ID="ddltono" runat="server" OnSelectedIndexChanged="ddltono_SelectedIndexChanged"
                 Width="203px" AutoPostBack="True" Visible="False">
             </asp:DropDownList></td>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 108px;">
         </td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 232px;">
         </td>
     </tr>
    
     <tr>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; width: 162px; border-bottom: silver 1px solid;
             position: static; height: 24px; background-color: #cfdcc8">
             <asp:Label ID="lblrecdist" runat="server" Text="Receiving FCI District" Visible="False"
                 Width="149px"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; width: 194px; border-bottom: silver 1px solid;
             position: static; height: 24px; background-color: #cfdcc8">
             <asp:TextBox ID="txtrecDist" runat="server" Enabled="False" Visible="False" Width="192px"></asp:TextBox></td>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; width: 108px; border-bottom: silver 1px solid;
             position: static; height: 24px; background-color: #cfdcc8">
             <asp:Label ID="lblrecddist" runat="server" Text="Receiving FCI District" Width="149px" Visible="False"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; width: 232px; border-bottom: silver 1px solid;
             position: static; height: 24px; background-color: #cfdcc8">
             <asp:DropDownList ID="ddldistrict" runat="server" Width="227px" AutoPostBack="True" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged" Enabled="False" Visible="False">
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; width: 162px; border-bottom: silver 1px solid;
             position: static; background-color: #cfdcc8; height: 24px;">
             <asp:Label ID="lblCommodity" runat="server" Text="Commodity" Width="82px"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; width: 194px; border-bottom: silver 1px solid;
             position: static; background-color: #cfdcc8; height: 24px;">
      <asp:DropDownList ID="ddlcomdty" runat="server"  Width ="228px"  AutoPostBack ="True" 
                 Enabled="False" onselectedindexchanged="ddlcomdty_SelectedIndexChanged" >
       <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
      </asp:DropDownList></td>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; width: 108px; border-bottom: silver 1px solid;
             position: static; background-color: #cfdcc8; height: 24px;">
             <asp:Label ID="lblScheme" runat="server" Text="Scheme"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; width: 232px; border-bottom: silver 1px solid;
             position: static; background-color: #cfdcc8; height: 24px;">
     <asp:DropDownList ID="ddlscheme" runat="server"  Width ="196px" AutoPostBack="True" OnSelectedIndexChanged="ddlscheme_SelectedIndexChanged" Enabled="False">
      <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
     </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; width: 162px; border-bottom: silver 1px solid;
             position: static; height: 24px; background-color: #cfdcc8">
             <asp:Label ID="lblrailhead" runat="server" Text="FCI Rail Head Name" Visible="False"
                 Width="143px"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; width: 194px; border-bottom: silver 1px solid;
             position: static; height: 24px; background-color: #cfdcc8">
             <asp:DropDownList ID="ddlsenrailhead" runat="server" Width="175px" AutoPostBack="True" OnSelectedIndexChanged="ddlissuecenter_SelectedIndexChanged" Enabled="False" Visible="False">
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; width: 108px; border-bottom: silver 1px solid;
             position: static; height: 24px; background-color: #cfdcc8">
             <asp:Label ID="lblrailname" runat="server" Text="FCI Rail Head Name" Visible="False"
                 Width="143px"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; width: 232px; border-bottom: silver 1px solid;
             position: static; height: 24px; background-color: #cfdcc8">
             <asp:TextBox ID="txtrailheadname" runat="server" Enabled="False" Visible="False"
                 Width="192px"></asp:TextBox></td>
     </tr>
     <tr>
         <td class="tdmarginddl" colspan="4" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; height: 1px; background-color: #ccff66">
         </td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 162px;">
             <asp:Label ID="lbldisdate" runat="server" Text="Dispatch Date"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 194px;">
      
    <asp:TextBox ID="DaintyDate1" runat="server" Width="123px"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate1'
	    });
	     </script>
         </td>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 108px;">
                                <asp:Label ID="lbl_Branch" runat="server" Text="Branch"></asp:Label>
                            </td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 232px;">
                                <asp:DropDownList ID="ddl_branch" runat="server" AutoPostBack="True" 
                                    
                 onselectedindexchanged="ddl_branch_SelectedIndexChanged" Width="135px">
                                </asp:DropDownList>
                            </td>
     </tr>
     
     <tr>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 162px;">
             &nbsp;</td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 194px;">
      
             &nbsp;</td>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 108px;">
             <asp:Label ID="lblGodownNo" runat="server" Text="Dispatch Godown" Width="122px"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 232px;">
     <asp:DropDownList ID="ddlgodown" runat="server" Width="196px" AutoPostBack="True" 
                 onselectedindexchanged="ddlgodown_SelectedIndexChanged">
     <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
     </asp:DropDownList></td>
     </tr>
     
     <tr>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 162px;">
         </td>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 194px;">
             <asp:Label ID="lblbqty" runat="server" Text="Balance Quantity " Visible="False"></asp:Label></td>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 108px;">
             <asp:TextBox ID="txtbqty" runat="server" Visible="False" Width="121px"></asp:TextBox></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 232px;">
         </td>
     </tr>
          <tr>
         <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;" align="left">
             <asp:Label ID="Label6" runat="server" Text="Balance Quantity "></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;">
             <asp:TextBox ID="txt_balanceqty" runat="server" Width="145px"></asp:TextBox></td>
         <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;">
             <asp:Label ID="Label21" runat="server" Text="Current Bags" Width="115px"></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px; width: 224px;">
             <asp:TextBox ID="txtcurbags" runat="server" Width="146px"></asp:TextBox></td>
     </tr>
  <tr>
  <td class ="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 162px; height: 1px;">
      <asp:Label ID="lblBagNumber" runat="server" Text="NO.Of Bags"></asp:Label></td>
 <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 194px; height: 1px;" align="left">
     <asp:TextBox ID="txtbagno" runat="server" Width="146px"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtbagno"
         ErrorMessage="No. of Bags Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
     <td class ="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 108px; height: 1px;">
         <asp:Label ID="lblQuantity" runat="server" Text="Dispatch Qty."></asp:Label></td>
  <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 232px; height: 1px;"> <asp:TextBox ID="txtquant" runat="server" Width="146px"></asp:TextBox>Qtls.<asp:RequiredFieldValidator
      ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtquant" ErrorMessage="Quantity Required"
      ValidationGroup="1" Width="1px">*</asp:RequiredFieldValidator></td>
 
  
  </tr>
  <tr>
  <td class ="tdmarginro" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 162px; height: 28px;">
      <asp:Label ID="lblChallanNumber" runat="server" Text="Truck Challan No." Width="139px"></asp:Label></td>
 <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 194px; height: 28px;">
     <asp:TextBox ID="txttrukcno" runat="server"  Width ="146px"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txttrukcno"
         ErrorMessage="Challan Number Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
     <td class ="tdmarginro" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 108px; height: 28px;">
         <asp:Label ID="lblTruckNumber" runat="server" Text="Truck No."></asp:Label></td>
  <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 232px; height: 28px;"> 
  <asp:TextBox ID="txttruckno" runat="server" Width="146px"></asp:TextBox></td>
 
  </tr>
     <tr>
         <td class="tdmarginro" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 162px; height: 24px;">
             <asp:Label ID="lblTrans" runat="server" Text="Transporter Name " Width="130px" Visible="true"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 194px; height: 24px;">
     <asp:DropDownList ID="ddltransporter" runat="server" Width="252px">
     </asp:DropDownList></td>
         <td class="tdmarginro" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 108px; height: 24px;">
             <asp:Label ID="lblCropYear" runat="server" Text="Crop Year" Width="130px"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 232px; height: 24px;">
         <asp:DropDownList ID="ddlcropyear" runat="server" Width="151px" OnSelectedIndexChanged="ddlDisType_SelectedIndexChanged">
             <asp:ListItem Value="0">--Select--</asp:ListItem>
             <asp:ListItem Value="5">2015-16</asp:ListItem>
             <asp:ListItem Value="1">2014-15</asp:ListItem>
             <asp:ListItem Value="2">2013-14</asp:ListItem>
             <asp:ListItem Value="3">2012-13</asp:ListItem>
             <asp:ListItem Value="4">2011-12</asp:ListItem>
         </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginro" colspan="4" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; height: 8px; background-color: #ccccff">
             <span style="font-size: 11pt; color: red"><strong>स्वयं के जिले के अलावा परिवहन करता
                 का नाम जरुरी नहीं है|</strong></span></td>
     </tr>
     
     
     <tr>
         <td class="tdmarginro" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 162px; height: 18px;">
         </td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 194px; height: 18px;">
             <asp:Label ID="lbldist" runat="server" Visible="False"></asp:Label></td>
         <td class="tdmarginro" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 108px; height: 18px;">
             <asp:Label ID="lbldepo" runat="server" Visible="False"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 232px; height: 18px;">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 162px; height: 24px;">
             <asp:Label ID="lbldistime" runat="server" Text="Time Of Dispatch" Width="128px"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 194px; height: 24px;">
             
                                               
    <asp:DropDownList ID="ddlhour" runat="server">
                            <asp:ListItem Value="01">01</asp:ListItem>
                            <asp:ListItem Value="02">02</asp:ListItem>
                            <asp:ListItem Value="03">03</asp:ListItem>
                            <asp:ListItem Value="04">04</asp:ListItem>
                            <asp:ListItem Value="05">05</asp:ListItem>
                            <asp:ListItem Value="06">06</asp:ListItem>
                            <asp:ListItem Value="07">07</asp:ListItem>
                            <asp:ListItem Value="08">08</asp:ListItem>
                            <asp:ListItem Value="09">09</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="11">11</asp:ListItem>
                            <asp:ListItem Value="12">12</asp:ListItem>
                        </asp:DropDownList>
                        :

  
 
 
 <asp:DropDownList ID="ddlminute" runat="server">
                             <asp:ListItem Value="00">00</asp:ListItem>
                            <asp:ListItem Value="01">01</asp:ListItem>
                            <asp:ListItem Value="02">02</asp:ListItem>
                            <asp:ListItem Value="03">03</asp:ListItem>
                            <asp:ListItem Value="04">04</asp:ListItem>
                            <asp:ListItem Value="05">05</asp:ListItem>
                            <asp:ListItem Value="06">06</asp:ListItem>
                            <asp:ListItem Value="07">07</asp:ListItem>
                            <asp:ListItem Value="08">08</asp:ListItem>
                            <asp:ListItem Value="09">09</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="11">11</asp:ListItem>
                            <asp:ListItem Value="12">12</asp:ListItem>
                            <asp:ListItem Value="13">13</asp:ListItem>
                            <asp:ListItem Value="14">14</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Value="16">16</asp:ListItem>
                            <asp:ListItem Value="17">17</asp:ListItem>
                            <asp:ListItem Value="18">18</asp:ListItem>
                            <asp:ListItem Value="19">19</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="21">21</asp:ListItem>
                            <asp:ListItem Value="22">22</asp:ListItem>
                            <asp:ListItem Value="23">23</asp:ListItem>
                            <asp:ListItem Value="24">24</asp:ListItem>
                              <asp:ListItem Value="25">25</asp:ListItem>
                            <asp:ListItem Value="26">26</asp:ListItem>
                            <asp:ListItem Value="27">27</asp:ListItem>
                            <asp:ListItem Value="28">28</asp:ListItem>
                            <asp:ListItem Value="29">29</asp:ListItem>
                            <asp:ListItem Value="30">30</asp:ListItem>
                            <asp:ListItem Value="31">31</asp:ListItem>
                            <asp:ListItem Value="32">32</asp:ListItem>
                            <asp:ListItem Value="33">33</asp:ListItem>
                            <asp:ListItem Value="34">34</asp:ListItem>
                            <asp:ListItem Value="35">35</asp:ListItem>
                            <asp:ListItem Value="36">36</asp:ListItem>
                            <asp:ListItem Value="37">37</asp:ListItem>
                            <asp:ListItem Value="38">38</asp:ListItem>
                            <asp:ListItem Value="39">39</asp:ListItem>
                            <asp:ListItem Value="40">40</asp:ListItem>
                            <asp:ListItem Value="41">41</asp:ListItem>
                            <asp:ListItem Value="42">42</asp:ListItem>
                            <asp:ListItem Value="43">43</asp:ListItem>
                            <asp:ListItem Value="44">44</asp:ListItem>
                            <asp:ListItem Value="45">45</asp:ListItem>
                            <asp:ListItem Value="46">46</asp:ListItem>
                            <asp:ListItem Value="47">47</asp:ListItem>
                            <asp:ListItem Value="48">48</asp:ListItem>
                            <asp:ListItem Value="79">49</asp:ListItem>
                            <asp:ListItem Value="50">50</asp:ListItem>
                            <asp:ListItem Value="51">51</asp:ListItem>
                            <asp:ListItem Value="52">52</asp:ListItem>
                            <asp:ListItem Value="53">53</asp:ListItem>
                            <asp:ListItem Value="54">54</asp:ListItem>
                            <asp:ListItem Value="55">55</asp:ListItem>
                            <asp:ListItem Value="56">56</asp:ListItem>
                            <asp:ListItem Value="57">57</asp:ListItem>
                            <asp:ListItem Value="58">58</asp:ListItem>
                            <asp:ListItem Value="59">59</asp:ListItem>
                          
                        </asp:DropDownList>

:
 
    <asp:DropDownList ID="ddlampm" runat="server">
    <asp:ListItem Value="01">AM</asp:ListItem>
    <asp:ListItem Value="02">PM</asp:ListItem>
    </asp:DropDownList></td>
         <td class="tdmarginro" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 108px; height: 24px;">
         </td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 232px; height: 24px;">
         </td>
     </tr>
  <tr>
  <td class ="tdmarginro" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 162px;" >
      <asp:Label ID="lblRemark" runat="server" Text="Remark" Width="109px"></asp:Label></td>
      <td align="left" colspan="3" rowspan="2" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
     <asp:TextBox ID="txtremark" runat="server"  Width ="559px" TextMode ="MultiLine" ></asp:TextBox>
      </td>
  </tr>
  <tr>
  <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 162px;"> </td>
  
  </tr>
     <tr>
         <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 162px; height: 1px;">
         </td>
         <td align="left" colspan="3" rowspan="1" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; height: 1px;">
             <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                 ValidationGroup="1" Width="423px" />
         </td>
     </tr>
  <tr>
   
 <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8; width: 162px;">
     <asp:Button ID="btnNew" runat="server" Text="New Entry" Width="109px" OnClick="btnNew_Click" /></td>
      <td align="left" colspan="3" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
      <asp:Button ID="btnsave" runat="server" Text="Submit" OnClick="btnsave_Click" ValidationGroup="1" Width="75px"/>&nbsp;
          <asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" Width="83px" />
          <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="#" Visible="False">Print Challan</asp:HyperLink></td>
     
      

  
  </tr>
     <tr>
         <td colspan="4" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 18px;">
             <asp:Label ID="Label1" runat="server" Text="Label" Width="785px" Height="22px"></asp:Label></td>
     </tr>
 </table>
 
 </td>
</tr>
</table>

</center>
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
var coda = event.keyCode;
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
</asp:Content>

