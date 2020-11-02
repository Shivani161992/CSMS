<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="CSC_Procurement.aspx.cs" Inherits="IssueCenter_CSC_Procurement" Title="Receipt From Purchase Center" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="hedl"><asp:Label ID="lber" runat="server" Text=""  Visible="false" ForeColor="red" Font-Size="9px"></asp:Label> </div>
<div>
<center>

<table style="width: 630px; font-size: 12px;">

<tr>
 <td style="width: 838px" align="left">
 <table style="width: 593px">
     <tr>
         <td align="center" colspan="4" style="border-right: darkseagreen 1px solid; border-top: darkseagreen 1px solid;
             border-left: darkseagreen 1px solid; border-bottom: darkseagreen 1px solid; background-color: #cccccc;">
             <asp:Label ID="lbldispprocure" runat="server" Font-Bold="True" Font-Size="16px" Text="Dispatch From Purchase Center (Procurement)"></asp:Label></td>
     </tr>
     <tr>
         <td align="left" style="background-color: #ffffcc; font-size: 10pt; position: static;">
     <asp:Label ID="lblDistrictName" runat="server" Text="District(Logged in)" Width="94px" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #ffffcc; font-size: 10pt; position: static;">
     <asp:TextBox ID="txtdist" runat="server" Width ="140px" Font-Bold="True" Font-Italic="True" ForeColor="Navy"></asp:TextBox></td>
         <td align="left" style="background-color: #ffffcc; font-size: 10pt; position: static;">
         <asp:Label ID="lblNameDepot" runat="server" Text="IssueCenter " Width="85px" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #ffffcc; font-size: 10pt; position: static;">
     <asp:TextBox ID="txtissue" runat="server"  Width ="140px" Font-Bold="True" Font-Italic="True" ForeColor="Navy"></asp:TextBox></td>
     </tr>
     
     <tr>
         <td  style="background-color: #ffffcc; font-size: 10pt; position: static;">
             <asp:Label ID="lblCommodity" runat="server" Text="Commodity" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #ffffcc; font-size: 10pt; position: static;">
      <asp:DropDownList ID="ddlcomdty" runat="server"  Width ="140px" OnSelectedIndexChanged="ddlcomdty_SelectedIndexChanged" AutoPostBack="true">
       
      </asp:DropDownList></td>
         <td  style="background-color: #ffffcc; font-size: 10pt; position: static;">
            <asp:Label ID="lblRecFromDist" runat="server" Text="Sending District" Width="84px" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #ffffcc; font-size: 10pt; position: static;">
      <asp:DropDownList ID="ddldistpdy" runat="server" Width="140px" AutoPostBack="True" OnSelectedIndexChanged="ddldistpdy_SelectedIndexChanged">
             </asp:DropDownList></td>
         
     </tr>
     <%--<asp:Panel ID="pnlwht" runat="server" Visible="false">
    
     <tr>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblRecFromDist1" runat="server" Text="Sending District" Width="84px" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:DropDownList ID="ddldistrict" runat="server" Width="153px" AutoPostBack="True" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
             </asp:DropDownList></td>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblpcname1" runat="server" Text="Purchase Center" Width="80px" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         <asp:DropDownList ID="ddlissuecenter" runat="server" Width="241px" OnSelectedIndexChanged="ddlissuecenter_SelectedIndexChanged" Font-Size="11px">
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblChallanNumber1" runat="server" Text="Truck Challan No." Width="94px" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:TextBox ID="txttrukcno" runat="server"  Width ="146px"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txttrukcno"
         ErrorMessage="Challan Number Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
         <td   style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblTruckNumber1" runat="server" Text="Truck No." Width="76px" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:TextBox ID="txttruckno" runat="server" Width="146px" MaxLength="12"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txttruckno"
         ErrorMessage="Truck No.  Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
     </tr>
     <tr>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblTrans1" runat="server" Text="Transport Name" Width="108px" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
     <asp:DropDownList ID="ddltransporter" runat="server" Width="153px">
     </asp:DropDownList></td>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblDateOfChallan1" runat="server" Text="Date Of Dispatch" Width="89px" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
              
         <asp:TextBox ID="DaintyDate1" runat="server" Width="119px"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate1'
	    });
	     </script>
         </td>
     </tr>
     
  <tr>
  <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
      <asp:Label ID="lblIssuedBags1" runat="server" Text="NO.Of Bags Dispatched" Font-Size="12px" Width="78px"></asp:Label></td>
 <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
     <asp:TextBox ID="txtbagno" runat="server" Width="128px" MaxLength="5"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtbagno"
         ErrorMessage="No of Bags Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
     <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         <asp:Label ID="lblQuantity1" runat="server" Text="Net Qty. Dispatched"
             Width="55px" Font-Size="12px"></asp:Label></td>
  <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;"> <asp:TextBox ID="txtquant" runat="server" Width="138px" MaxLength="13"></asp:TextBox>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtquant"
          ErrorMessage="Quantity Required" ValidationGroup="1">*</asp:RequiredFieldValidator>
      <asp:Label ID="lblKgs" runat="server" Font-Size="8pt" Text="(Qtls.)"></asp:Label></td>
 
  
  </tr>
    </asp:Panel>--%>
     
     <%--<asp:Panel ID="pnlpdy" runat="server" Visible="false" Width="580px">--%>
    
     <tr>
         <td  style="background-color: #ffffcc; font-size: 10pt; position: static; text-align: center;">
             <asp:Label ID="lblissdat" runat="server" Text="Date of Issue" Width="89px" Font-Size="12px" Visible="False"></asp:Label>             
             </td>
         <td align="center" style="background-color: #ffffcc; font-size: 10pt; position: static; text-align: center;">
         <asp:TextBox ID="txtisdate" runat="server" Width="119px" Visible="False"></asp:TextBox>
             <asp:Label ID="lblpcname" runat="server" Text="Purchase Center" Width="80px" Font-Size="12px"></asp:Label><%--<script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_txtisdate'
	    });
	     </script>--%></td>
         <td style="background-color: #ffffcc; font-size: 10pt; position: static; text-align: center;" colspan="2">
         <asp:DropDownList ID="ddluparjan" runat="server" Width="295px" Font-Size="11px" AutoPostBack="True" OnSelectedIndexChanged="ddluparjan_SelectedIndexChanged">
                 
             </asp:DropDownList></td>
     </tr>
      <tr>
            <td colspan="4">
                <asp:Panel ID="pnlgrd" runat="server" ScrollBars="Vertical" Width="800px" Height="160px" Visible="false">
                
            <asp:GridView ID="dgridchallan" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="dgridchallan_SelectedIndexChanged" OnRowDataBound="dgridchallan_RowDataBound" 
            PageSize="1" PagerSettings-Visible ="true" ShowFooter = "True" CellPadding="1" ForeColor="#333333" GridLines="None" 
            OnPageIndexChanging="dgridchallan_PageIndexChanging" Width="800px" OnPageIndexChanged="dgridchallan_PageIndexChanged"  >
        <HeaderStyle   BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Height="18px" />
        <Columns>
           <asp:CommandField HeaderText="Action" ShowSelectButton="True" SelectText="Click to Receive" >
                <ItemStyle  ForeColor="Blue" />
            </asp:CommandField>
                  <asp:BoundField DataField="DateOfIssue" HeaderText="Date Of Dispatch" SortExpression="DateOfIssue">
            </asp:BoundField>   
             <asp:BoundField DataField="IssueID" HeaderText="IssueID" SortExpression="IssueID">
                <ItemStyle   HorizontalAlign="Left" ForeColor="Maroon" />
                 <HeaderStyle ForeColor="White" />
            </asp:BoundField>          
            <asp:BoundField DataField="TruckChalanNo" HeaderText="TC No." ReadOnly="True" SortExpression="TruckChalanNo" >
                <ItemStyle  HorizontalAlign="Right"/>
                <HeaderStyle  HorizontalAlign="Right"/>
            </asp:BoundField>
            <asp:BoundField DataField="TruckNo" HeaderText="Truck No." ReadOnly="True"
                SortExpression="TruckNo" >
                <ItemStyle  HorizontalAlign="Right"/>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:BoundField>
            
           
            <asp:BoundField DataField="Bags" HeaderText="Bags" ReadOnly="True" SortExpression="Bags" >
                <ItemStyle  HorizontalAlign="Right"/>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="QtyTransffer" HeaderText="Qty" ReadOnly="True"
                SortExpression="QtyTransffer" >
                <ItemStyle HorizontalAlign="Right" />
                <HeaderStyle  HorizontalAlign="Right"/>
            </asp:BoundField>
            <asp:BoundField DataField="Transporter_Name" HeaderText="Transport Name" ReadOnly="True" SortExpression="Transporter_Name" >
                <ItemStyle HorizontalAlign="Right"/>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:BoundField>
             <asp:BoundField DataField="TransporterId" HeaderText="TransporterId" ReadOnly="True" SortExpression="TransporterId">
                <ItemStyle HorizontalAlign="Right" Width="0px" Font-Size="0px"/>
                <HeaderStyle HorizontalAlign="Right" Width="0px" Font-Size="0px" />
            </asp:BoundField>                     
           
        </Columns>
             <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
             <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" Font-Bold="True" />
             <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
             <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <EditRowStyle BackColor="#999999" />
    </asp:GridView>
    </asp:Panel>
            </td>
        </tr>
      <asp:Panel ID="pnlrcvdtl" runat="server" ScrollBars="Vertical" Width="800px"  Visible="false">  
     <tr style=" height:15px;">
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;"  align="left">
            <asp:Label ID="lblCropYear" runat="server" Text="CropYear" Width="80px" Font-Size="12px"></asp:Label> </td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;" >
             <asp:DropDownList ID="ddlcropyear" runat="server"  Width="140px" OnSelectedIndexChanged="ddlcropyear_SelectedIndexChanged" >
                                               
                                            </asp:DropDownList>
             
             </td>
             <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblissid" runat="server" Text="Issue_ID" Width="110px" Font-Size="12px"></asp:Label> &nbsp;</td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         <asp:TextBox ID="txtissueId" runat="server" Width="180px" Font-Size="12px"></asp:TextBox>
            
     </td>
         
     </tr>
     <tr style="height:15px;">
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblChallanNumber" runat="server" Text="Send Truck Challan No." Width="105px" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:TextBox ID="txtchlnno" runat="server"  Width ="142px"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtchlnno"
         ErrorMessage="Challan Number Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
         <td   style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblTruckNumber" runat="server" Text="Sending Truck No." Width="86px" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:TextBox ID="txttrucknopady" runat="server" Width="140px" MaxLength="12"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txttrucknopady"
         ErrorMessage="Truck No. Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
     </tr>
     <tr>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblTrans" runat="server" Text="Transport Name" Width="108px" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
     <asp:DropDownList ID="ddlpdyTransporter" runat="server" Width="140px">
     </asp:DropDownList></td>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
            <asp:Label ID="lblDateOfChallan" runat="server" Text="Date Of Dispatch" Width="108px" Font-Size="12px"></asp:Label> </td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
              
         <asp:TextBox ID="DaintyDate1P" runat="server" Width="119px"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate1p'
	    });
	     </script>
         </td>
     </tr>
     
  <tr>
  <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
      <asp:Label ID="lblIssuedBags" runat="server" Text="NO.Of Bags Dispatched" Font-Size="12px" Width="78px"></asp:Label></td>
 <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
     <asp:TextBox ID="txtissubag" runat="server" Width="128px" MaxLength="5"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtissubag"
         ErrorMessage="No of Bags Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
     <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         <asp:Label ID="lblQuantity" runat="server" Text="Net Qty. Dispatched"
             Width="55px" Font-Size="12px"></asp:Label></td>
  <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
   <asp:TextBox ID="txtissueqty" runat="server" Width="138px" MaxLength="13"></asp:TextBox>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtissueqty"
          ErrorMessage="Quantity Required" ValidationGroup="1">*</asp:RequiredFieldValidator>
      <asp:Label ID="lblKgs" runat="server" Font-Size="8pt" Text="(Qtls.)"></asp:Label></td>
 
  
  </tr>
 </asp:Panel>
     
      <tr>
         <td  style="background-color: #cccccc; height: 26px;">
             &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
             <asp:Label ID="Label14" runat="server" Text="Book No." Visible="False"></asp:Label>&nbsp;</td>
         <td align="center" style="background-color: #cccccc; height: 26px;" colspan="2">
             &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
             <asp:Label ID="lblRecepDetail" runat="server" Text="Receipt Details" Width="120px" Font-Bold="True" Font-Size="13px"></asp:Label></td>
         <td align="left" style="background-color: #cccccc; height: 26px;">
             &nbsp; &nbsp;&nbsp;&nbsp;
     <asp:TextBox ID="txtbookno" runat="server" Width="146px" Visible="False"></asp:TextBox>&nbsp;</td>
     </tr>
     <tr>
         <td   style="background-color: cornflowerblue; font-size: 10pt; position: static;">
             <asp:Label ID="lblReceiptDate" runat="server" Text="Recd. Date" Width="73px" Font-Size="12px"></asp:Label></td>
         <td  colspan="2" style="background-color: cornflowerblue; font-size: 10pt; position: static;">
             <asp:TextBox ID="DaintyDate3" runat="server" Width="119px"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate3'
	    });
	     </script>
             &nbsp;
         </td>
         <td align="left" style="background-color: cornflowerblue; font-size: 10pt; position: static;">
             &nbsp;&nbsp;
             <asp:DropDownList ID="ddlscheme" Visible="false" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlscheme_SelectedIndexChanged"
                 Width="53px" >
                 <asp:ListItem Selected="True" Value="101">-Select-</asp:ListItem>
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td style="font-size: 10pt; position: static; background-color: cornflowerblue">
             <asp:Label ID="Label3" runat="server" Font-Size="12px" Text="Receving TC Number"
                 Width="131px" BackColor="White"></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: cornflowerblue">
             <asp:TextBox ID="txtrec_tcnumber" runat="server" MaxLength="13" Width="110px"></asp:TextBox></td>
         <td style="font-size: 10pt; position: static; background-color: cornflowerblue">
             <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Size="12px" Text="Receving Truck Number"
                 Width="141px" BackColor="White"></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: cornflowerblue">
             <asp:TextBox ID="txtRec_TruckNumber" runat="server" MaxLength="13" 
                 Width="120px"></asp:TextBox></td>
     </tr>
     <tr>
         <td   style="background-color: cornflowerblue; font-size: 10pt; position: static;">
             <asp:Label ID="lbltotalReceivedBags" runat="server" Text="Recd Bags." Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: cornflowerblue; font-size: 10pt; position: static;">
             <asp:TextBox ID="txtrecdbags" runat="server" MaxLength="13" Width="146px"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtrecdbags"
          ErrorMessage="Bags Required" ValidationGroup="1">*</asp:RequiredFieldValidator>
          

          </td>
         <td  style="background-color: cornflowerblue; font-size: 10pt; position: static;">
             <asp:Label ID="lblTotalQuantityReceived" runat="server" Text="Recd. Qty." Width="71px" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: cornflowerblue; font-size: 10pt; position: static;">
             <asp:TextBox ID="txtrecdqty" runat="server" MaxLength="13" Width="146px" ></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtrecdqty"
          ErrorMessage="Quantity Required" ValidationGroup="1">*</asp:RequiredFieldValidator>
             <asp:Label ID="lblqntl" runat="server" Font-Size="8pt" Text="(Qtls.)"></asp:Label>
             </td>
     </tr>
     <tr>
         <td style="font-size: 10pt; position: static; height: 24px; background-color: cornflowerblue">
             <asp:Label ID="Label2" runat="server" Font-Size="12px" Text="Receiving District Godown"></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; height: 24px; background-color: cornflowerblue">
             <asp:DropDownList ID="ddlRec_DistGodown" runat="server"  Width ="140px" AutoPostBack="true" OnSelectedIndexChanged="ddlRec_DistGodown_SelectedIndexChanged">
             </asp:DropDownList></td>
         <td style="font-size: 10pt; position: static; height: 24px; background-color: cornflowerblue">
         </td>
         <td align="left" style="font-size: 10pt; position: static; height: 24px; background-color: cornflowerblue">
         </td>
     </tr>
      <tr>
         <td  style="background-color: cornflowerblue; font-size: 10pt; position: static;">
             <asp:Label ID="lbldpo" runat="server" Text="Branch" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: cornflowerblue; font-size: 10pt; position: static;">
             <asp:DropDownList ID="ddlbranchwlc" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbranchwlc_SelectedIndexChanged"
                 Width="153px">
             </asp:DropDownList></td>
         <td  style="background-color: cornflowerblue; font-size: 10pt; position: static;">
             <asp:Label ID="lblGodownNo" runat="server" Font-Size="12px" Text="Godown/Own N." Width="110px"></asp:Label></td>
         <td align="left" style="background-color: cornflowerblue; font-size: 10pt; position: static;">
             <asp:DropDownList ID="ddlgodown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged"
                 Width="153px">
                  <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
             </asp:DropDownList></td>
             
     </tr>
     <tr>
         <td  style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lblhirty" runat="server" Text="Hired_Type" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:TextBox ID="txthhty" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Navy"
                 Width="140px" Enabled="false"></asp:TextBox></td>
         <td  style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lblMaxCap" runat="server" Text="MaxCapacity" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:TextBox ID="txtmaxcap" runat="server" Font-Bold="True" Font-Italic="False" ForeColor="#0000C0"
                 ReadOnly="True" Width="144px" Enabled="false"></asp:TextBox></td>
     </tr>
     <tr>
         <td  style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lblCurStackCap" runat="server" Text="Current Cap." Font-Size="12px" Width="71px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:TextBox ID="txtcurntcap" runat="server" Font-Bold="True" Font-Italic="False"
                 ForeColor="#0000C0" ReadOnly="True" Width="145px" Enabled="false"></asp:TextBox></td>
         <td  style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lblAvailable" runat="server" Text="Available" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:TextBox ID="txtavalcap" runat="server" Font-Bold="True" Font-Italic="False"
                 ForeColor="#0000C0" ReadOnly="True" Width="143px" Enabled="false"></asp:TextBox></td>
     </tr>
     <tr>
         <td  style="background-color: #cfdcc8; font-size: 10pt; position: static;">
         </td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lblbalanceqty" runat="server" Text="Current Stock Position" Visible="False" Width="144px" Font-Size="12px"></asp:Label></td>
         <td  style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:TextBox ID="txtbalqty" runat="server" Visible="False" Width="64px"></asp:TextBox></td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
         </td>
     </tr>
     <tr>
         <td style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             </td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:TextBox ID="txtaccptno" runat="server" Width="146px" MaxLength="13" Visible="False"></asp:TextBox>
             </td>
         <td  style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             </td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
                      </td>
     </tr>
     <tr>
         <td  style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             &nbsp;&nbsp;&nbsp;&nbsp;
         </td>
         <td align="right" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
         <asp:Button ID="btnsavePaddy" runat="server" Text="Save" OnClick="btnsavePaddy_Click"  Visible="false" ValidationGroup="1" Width="103px"/>&nbsp;
     </td>
         <td  style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" Width="91px"/></td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Button ID="btnaddnew" runat="server" OnClick="btnaddnew_Click" Text="New" Width="137px" /></td>
     </tr>
     
     
     
     <tr>
         <td align="left" colspan="2" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                 ValidationGroup="1" Width="299px" ShowSummary="False" />
             &nbsp;&nbsp; &nbsp;</td>
         <td  style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             &nbsp; &nbsp;
         </td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
          <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="#" Visible="False" Width="139px">Print Receipt Detail</asp:HyperLink>
             &nbsp;
         </td>
     </tr>
     <tr>
         <td  colspan="4" style="border-right: lightskyblue 1px solid;
             border-top: lightskyblue 1px solid; border-left: lightskyblue 1px solid; border-bottom: lightskyblue 1px solid">
             <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label></td>
     </tr>
     <tr>
         <td  colspan="4" style="border-right: lightskyblue 1px solid;
             border-top: lightskyblue 1px solid; border-left: lightskyblue 1px solid; border-bottom: lightskyblue 1px solid; height: 3px;">
     <asp:Label ID="Label9" runat="server" Visible="False"></asp:Label></td>
     </tr>
    
 </table>
 
 </td>
</tr>
</table>
<table >
 <tr>
         <td align="left" colspan="6">
         </td>
     </tr>
</table>
 <div>
        <table border ="0" cellpadding ="0" cellspacing ="0" class="gridfooter">
        <tr>
                        <td>
                            <div >
                                
                             
                            </div>
                        </td>
                    </tr>
        </table>
        </div>
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
function IsNumericProcQty(key,txt)
{
  var keycode = (key.which) ? key.which : key.keyCode;
    var num=txt.value;
    var len=num.length;
    var indx=-1;
    indx=num.indexOf('.');
    
            var dgt=num.substr(indx,len);
            var count= dgt.length;
      
    if(keycode==08)
    {
        return true;
    }
     else if (keycode == 59 || keycode  == 32)
     {            
            alert('Semi Colon (;) & Blank Space Not Allowed ...');
            return false;
     } 
     else if (keycode =="36" || keycode =="37" || keycode =="38" || keycode =="40" || keycode =="41" || keycode =="43" || keycode =="92" || keycode =="124" || keycode =="34" || keycode =="39" || keycode =="60" || keycode =="62" || keycode =="44" || keycode =="64" || keycode =="61" || keycode=="63")
     {
            alert('Do not use SQL Key-Words, Semi Colon(;) and Special Characters(&,%,$)..etc');
	        return false;
     }
    
    else if (keycode==09)
    {
             if(num>99999)
            {
                 alert(' मात्रा 100000 से अधिक प्रविष्ट नहीं कर सकते हें|');
                 txt.value="0";
                 return false;
            }
    } 
    
    else if (num>99999)
    {
            alert('मात्रा 100000 से अधिक प्रविष्ट नहीं कर सकते हें|');
            txt.value="0";
            return false;
    }      
    
    else  if (count > 5)  
    {
                alert("दशमलव के बाद 5 अंक ही आ सकते है");
                return false;
    }
    
    else if(keycode==46)
    {
         if (num.split(".").length>1)
         {    
            alert('दशमलव एक ही बार आ सकता है |');
            return false;
         }
    }
    else if (keycode >= 48 && keycode <= 58 )
    {
        return true;
    }  
    else 
    {
        alert('कृपया संख्या ही प्रविष्ट करें |');
        return false;
    }}


function IsNumericProcQtyBag(key,txt)
{
  var keycode = (key.which) ? key.which : key.keyCode;
    var num=txt.value;
    var len=num.length;
    var indx=-1;
    indx=num.indexOf('.');
    
            var dgt=num.substr(indx,len);
            var count= dgt.length;
      
    if(keycode==08)
    {
        return true;
    }
    
     else if (keycode == 59 || keycode  == 32 )
     {            
            alert('Semi Colon (;) & Blank Space Not Allowed ...');
            return false;
     } 
     else if (keycode =="36" || keycode =="37" || keycode =="38" || keycode =="40" || keycode =="41" || keycode =="43" || keycode =="92" || keycode =="124" || keycode =="34" || keycode =="39" || keycode =="60" || keycode =="62" || keycode =="44" || keycode =="64" || keycode =="61" || keycode=="63")
     {
            alert('Do not use SQL Key-Words, Semi Colon(;) and Special Characters(&,%,$)..etc');
	        return false;
     }
    
    else if (keycode==09)
    {
             if(num>10000)
            {
                 alert(' मात्रा 100000 से अधिक प्रविष्ट नहीं कर सकते हें|');
                 txt.value="0";
                 return false;
            }
    } 
    
    else if (num>10000)
    {
            alert('मात्रा 100000 से अधिक प्रविष्ट नहीं कर सकते हें|');
            txt.value="0";
            return false;
    }      
    else if(keycode==46)
    {
         if (num.split(".").length>0)
         {    
            alert('दशमलव नहीं आ सकता है |');
            return false;
         }
    }
    
    else if (keycode >= 48 && keycode <= 58 )
    {
        return true;
    }  
    else 
    {
        alert('कृपया संख्या ही प्रविष्ट करें |');
        return false;
    }}






function chkSqlKey(key,txt)
{
 
    var keycode = (key.which) ? key.which : key.keyCode;
    
    if (keycode == 59  || keycode == 32 )
     {            
            alert('Semi Colon (;) & Blank Space Not Allowed ...');
            return false;
     } 
   
     else if (keycode =="36" || keycode =="37" || keycode =="38" || keycode =="40" || keycode =="41" || keycode =="43" || keycode =="92" || keycode =="124" || keycode =="34" || keycode =="39" || keycode =="60" || keycode =="62" || keycode =="44" || keycode =="64" || keycode =="61" || keycode=="63")
     {
            alert('Do not use SQL Key-Words, Semi Colon(;) and Special Characters(&,%,$)..etc');
	        return false;
     }
}
function ChkNotgrate()
{
var ctrlisqty = document.getElementById('ctl00_ContentPlaceHolder1_txtissueqty');
var ctrlrvqty = document.getElementById('ctl00_ContentPlaceHolder1_txtrecdqty');
var ctxisqty = ctrlisqty.value 
var ctxtrcvqty = ctrlrvqty.value 

if(ctxtrcvqty == txisqty * 100  )
{
alert("NOT grdn 10 times");
return false
}
return true
}
    </script>   

  
</asp:Content>
