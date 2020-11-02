<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Receipt_OtherDepot.aspx.cs" Inherits="IssueCenter_Receipt_OtherDepot" Title="Other Depot Receiving" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <script type="text/javascript">
      function CheckCalDate(tx) {
          var AsciiCode = event.keyCode;
          var txt = tx.value;
          var txt2 = String.fromCharCode(AsciiCode);
          var txt3 = txt2 * 1;
          if ((AsciiCode > 0)) {
              alert('Please Click on Calander Controll to Enter Date');
              event.cancelBubble = true;
              event.returnValue = false;
          }
      }
</script>
         <script type="text/javascript">
             function CheckIsNumeric(e, tx) {
                 var AsciiCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
                 if ((AsciiCode < 46 && AsciiCode != 8) || (AsciiCode > 57) || (AsciiCode == 47)) {
                     alert('Please enter only numbers.');
                     return false;
                 }
                 var num = tx.value;
                 var len = num.length;
                 var indx = -1;
                 indx = num.indexOf('.');
                 if (indx != -1) {
                     if ((AsciiCode == 46)) {
                         alert('Point must be apear only one time.');
                         return false;
                     }
                     var dgt = num.substr(indx, len);
                     var count = dgt.length;
                     //alert (count);
                     if (count > 5 && AsciiCode != 8) {
                         alert("Only 5 decimal digits allowed.");
                         return false;
                     }
                 }
             }
    </script>
  <center>
    <table style="border: thin groove #000000; width: 800px; height: 600px">
        <tr>
            <td>
                &nbsp;</td>
          <td align="center" colspan="2" style="background-color: lightslategray; height: 19px;">
             <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="White"
                 Text="Reciept Details" Width="155px"></asp:Label></td>
            <td>
                &nbsp;</td>
        </tr>
        <tr style="background-color: #CCFFCC">
            <td style="height: 22px">
      <asp:Label ID="lblChallanNumber" runat="server" Text="Challan No."></asp:Label></td>
            <td style="text-align: left; height: 22px;">
     <asp:DropDownList ID="ddlchallan" runat="server" Width="156px" AutoPostBack="True" 
                    Height="27px" onselectedindexchanged="ddlchallan_SelectedIndexChanged"  >
        
               
     </asp:DropDownList></td>
            <td style="height: 22px">
                </td>
            <td style="height: 22px">
                </td>
        </tr>
        <tr style="background-color: #9966FF">
            <td style="height: 19px">
             <asp:Label ID="lblchallanno" runat="server" Text="Enter Challan No." Width="125px"></asp:Label></td>
            <td style="text-align: left; height: 19px">
     <asp:TextBox ID="txtchallan" runat="server" Width="146px"  Height="24px" ></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtchallan"
         ErrorMessage="Challan No Requierd " ValidationGroup="1">*</asp:RequiredFieldValidator></td>
            <td style="height: 19px">
             <asp:Label ID="lblchallandate" runat="server" Text="EnterChallan Date" Width="123px"></asp:Label></td>
            <td style="text-align: left; height: 19px">
             <asp:TextBox ID="DaintyDate3" runat="server" Width="119px" Height="21px"></asp:TextBox>
                    
                    <script type  ="text/javascript">
                        new tcal({
                            'formname': '0',
                            'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate3'
                        });
	     </script>
             </td>
        </tr>
       <tr style="background-color: #9966FF">
            <td style="height: 26px">
             <asp:Label ID="lblRecFromDist" runat="server" Text="Sending District" Visible="False" Width="107px"></asp:Label></td>
            <td style="text-align: left; height: 26px;">
             <asp:DropDownList ID="ddldistrict" runat="server" Width="153px" 
                    AutoPostBack="True" Height="24px" 
                    onselectedindexchanged="ddldistrict_SelectedIndexChanged" >
                 
             </asp:DropDownList>
             </td>
            <td style="height: 26px">
             <asp:Label ID="lblNameDepot" runat="server" Text="Sending Depot" Visible="False" Width="105px"></asp:Label></td>
            <td style="text-align: left; height: 26px;">
             <asp:DropDownList ID="ddlissue" runat="server" Visible="False" Width="153px" 
                    Height="25px">
                 
             </asp:DropDownList>
             </td>
        </tr>
       <tr style="background-color: #9966FF">
            <td style="height: 24px">
         <asp:Label ID="lblCommodity" runat="server" Text="Commodity"></asp:Label></td>
            <td style="text-align: left; height: 24px">
      <asp:DropDownList ID="ddlcomdty" runat="server"  Width="147px"  Height="25px">
       
      </asp:DropDownList>
            </td>
            <td style="height: 24px">
      <asp:Label ID="lblScheme" runat="server" Text="Scheme"></asp:Label></td>
            <td style="text-align: left; height: 24px">
     <asp:DropDownList ID="ddlscheme" runat="server"  Width="176px" AutoPostBack="True" 
                    Height="25px">
     
     </asp:DropDownList>
             </td>
        </tr>
       <tr style="background-color: #9966FF">
            <td style="height: 14px">
          <asp:Label ID="lblBagNumber" runat="server" Text="No. Of Bags"></asp:Label></td>
            <td style="text-align: left; height: 14px">
                <asp:TextBox ID="txtnobags" runat="server" Width="146px" Height="26px" ></asp:TextBox></td>
            <td style="height: 14px">
      <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label></td>
            <td style="text-align: left; height: 14px">
                <asp:TextBox ID="txtqty" runat="server" Width="143px" Height="25px" ></asp:TextBox></td>
        </tr>
       <tr style="background-color: #9966FF">
            <td style="height: 20px">
          <asp:Label ID="Label6" runat="server" Text="Crop Year" Visible="False"></asp:Label></td>
            <td style="text-align: left; height: 20px">
           <asp:DropDownList ID="ddlcropyear" runat="server"  Width="157px"  Visible="False" Height="25px" >
                                                        
                                              
         </asp:DropDownList>
       </td>
            <td style="height: 20px">
                </td>
            <td style="height: 20px">
                </td>
        </tr>
        <tr style="background-color: #FFFFCC">
            <td style="height: 23px">
      <asp:Label ID="lblTrans" runat="server" Text="Transporter/Lead"></asp:Label></td>
            <td style="text-align: left; height: 23px">
     <asp:DropDownList ID="ddltransport" runat="server" Width="219px" Font-Size="11px"    Height="26px">
      
     </asp:DropDownList>
     <asp:Label ID="lbltid" runat="server" Visible="False"></asp:Label>
            </td>
            <td style="height: 23px">
         <asp:Label ID="lblTruckNumber" runat="server" Text="Vehicle No."></asp:Label></td>
            <td style="text-align: left; height: 23px">
                <asp:TextBox ID="txtvehleno" runat="server" Width="146px" Height="20px" ></asp:TextBox>
            </td>
        </tr>
        <tr style="background-color: #FFFFCC">
            <td style="height: 19px">
             <asp:Label ID="lblArrivalTime" runat="server" Text="Arrival Time"></asp:Label></td>
            <td style="text-align: left; height: 19px">
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
                :<asp:DropDownList ID="ddlminute" runat="server">
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
                </asp:DropDownList>
            </td>
            <td style="height: 19px">
                <asp:Label ID="lblReceiptDate" runat="server" Text="Arrival Date"></asp:Label>
            </td>
            <td style="text-align: left; height: 19px">
                 <asp:TextBox ID="DaintyDate1" runat="server" Width="118px" Height="20px"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate1'
	    });
	     </script>
            </td>
        </tr>
        <tr style="background-color: #FFFFCC">
            <td style="height: 17px">
             <asp:Label ID="lbltotalReceivedBags" runat="server" Text="Recd. Bags"></asp:Label></td>
            <td style="text-align: left; height: 17px">
      <asp:TextBox ID="txtrecbags" runat="server" Width="137px" MaxLength="5" Height="25px"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtrecbags"
          ErrorMessage="No of Bags Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
            <td style="height: 17px">
             <asp:Label ID="lblTotalQuantityReceived" runat="server" Text="Recd. Qty."></asp:Label></td>
            <td style="text-align: left; height: 17px">
      <asp:TextBox ID="txtrqty" runat="server" Width="118px" 
                    MaxLength="13" Height="25px"></asp:TextBox>Qtls<asp:RequiredFieldValidator
          ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtrqty" ErrorMessage="Recd Qty.  Required"
          ValidationGroup="1">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr style="background-color: #FFFFCC">
            <td style="height: 23px">
             <asp:Label ID="lblGodownNo0" runat="server" Text="Branch Name"></asp:Label></td>
            <td style="text-align: left; height: 23px">
                            <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="True" 
                                OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" Width="173px" 
                                Height="25px">
                            </asp:DropDownList>
                        </td>
            <td style="height: 23px">
             <asp:Label ID="lblGodownNo" runat="server" Text="Godown No."></asp:Label></td>
            <td style="text-align: left; height: 23px">
             <asp:DropDownList ID="ddlgodown" runat="server" Width="143px" Height="25px" 
                    AutoPostBack="True" onselectedindexchanged="ddlgodown_SelectedIndexChanged">
             </asp:DropDownList></td>
        </tr>
       <tr style="background-color: #FFFFCC">
            <td style="height: 11px">
             <asp:Label ID="lblMaxCap" runat="server" Text="MaxCapacity"></asp:Label></td>
            <td style="text-align: left; height: 11px">
             <asp:TextBox ID="txtmaxcap" runat="server" Font-Bold="True" Font-Italic="False" ForeColor="#0000C0"
                 ReadOnly="True" Width="170px" Height="24px"></asp:TextBox></td>
            <td style="height: 11px">
                </td>
            <td style="height: 11px">
                </td>
        </tr>
         <tr style="background-color: #FFFFCC">
            <td style="height: 12px">
             <asp:Label ID="lblCurStackCap" runat="server" Text="Current Cap."></asp:Label></td>
            <td style="text-align: left; height: 12px">
             <asp:TextBox ID="txtcurntcap" runat="server" Font-Bold="True" Font-Italic="False"
                 ForeColor="#0000C0" ReadOnly="True" Width="170px" Height="25px"></asp:TextBox></td>
            <td style="height: 12px">
             <asp:Label ID="lblAvailable" runat="server" Text="Available"></asp:Label></td>
            <td style="text-align: left; height: 12px">
             <asp:TextBox ID="txtavalcap" runat="server" Font-Bold="True" Font-Italic="False"
                 ForeColor="#0000C0" ReadOnly="True" Width="160px" Height="25px"></asp:TextBox></td>
        </tr>
        <tr style="background-color: #FFFFCC">
            <td style="height: 6px">
             <asp:Label ID="lblbalanceqty" runat="server" Text="Balance Qty Of Stock" Width="161px" Visible="False" BackColor="#FFC0C0" Font-Bold="False" ForeColor="Navy" Font-Italic="True"></asp:Label></td>
            <td style="text-align: left; height: 6px">
             <asp:TextBox ID="txtbalqty" runat="server" Width="168px" Visible="False" 
                    Height="25px"></asp:TextBox></td>
            <td style="height: 6px">
                </td>
            <td style="height: 6px">
                </td>
        </tr>
        <tr style="background-color: #FFFFCC">
            <td style="height: 15px">
          <asp:Label ID="Label2" runat="server" Text="WCM No."></asp:Label></td>
            <td style="text-align: left; height: 15px">
      <asp:TextBox ID="txtwcmno" runat="server" Width="170px" MaxLength="7" 
                    Height="25px" ></asp:TextBox></td>
            <td style="height: 15px">
       <asp:Label ID="Label3" runat="server" Text="Moisture %" Width="121px"></asp:Label></td>
            <td style="text-align: left; height: 15px">
      <asp:TextBox ID="txtmoisture" runat="server" Width="150px" Height="25px" ></asp:TextBox></td>
        </tr>
        <tr>
            <td style="height: 15px">
                &nbsp;</td>
            <td style="text-align: left; height: 15px">
                &nbsp;</td>
            <td style="height: 15px">
                &nbsp;</td>
            <td style="text-align: left; height: 15px">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: left; height: 17px">
           <asp:Button ID="btnnew" runat="server" Text="New" Width="114px" Font-Bold="True" 
                    Font-Italic="True" ForeColor="#004040" onclick="btnnew_Click" /></td>
            <td style="text-align: center; height: 17px">
                <asp:Button ID="btnsubmit" runat="server" Text="Submit"  ValidationGroup="1" 
                    Width="141px" Font-Bold="True" Font-Italic="True" ForeColor="#004040" 
                    onclick="btnsubmit_Click" /></td>
            <td style="height: 17px">
                &nbsp;</td>
            <td style="height: 17px">
                <asp:Button ID="btnclose" runat="server" Text="Close"  Width="110px" 
                    Font-Bold="True" Font-Italic="True" ForeColor="#004040" 
                    onclick="btnclose_Click" /> 
                </td>
        </tr>
        <tr>
            <td colspan="2" rowspan="2" style="text-align: left">
           <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
               ValidationGroup="1" ShowSummary="False" Width="194px" />
           <asp:Label ID="Label1" runat="server" Text="Label" Visible="False" Width="76px"></asp:Label></td>
            <td colspan="2" style="height: 6px">
           <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="#" Visible="False" Width="211px">Print Receipt Acknowledgement</asp:HyperLink></td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>
    </table>
    </center>
</asp:Content>

