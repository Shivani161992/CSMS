<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true"
    CodeFile="Truck_Chalan_FPS.aspx.cs" Inherits="IssueCenter_Truck_Chalan_FPS" Title="Truck Challan FPS Wise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
function CheckIsChar(tx)
{

var AsciiCode = event.keyCode;
var txt=tx.value;
var txt2 = String.fromCharCode(AsciiCode);
var txt3=txt2*1;
if ((AsciiCode < 65 && AsciiCode != 8 && AsciiCode != 32) || (AsciiCode > 122 ) && (AsciiCode == 91 )(AsciiCode == 92 )||(AsciiCode == 93 )||(AsciiCode == 94 )||(AsciiCode == 95 )||(AsciiCode == 96 ))
{
alert('कृपया सही नाम भरें');
event.cancelBubble = true;
event.returnValue = false;
}


}

    </script>

    <center>
        <div id="ronewmargin">
            <center>
                <table cellpadding="0" cellspacing="0" class="tablelayout" style="width: 620px">
                    <tr>
                        <td>
                            <table border="1px" cellpadding="0" cellspacing="0" width="100%">
                                <tr style="height: 25px">
                                    <td align="center" colspan="4" style="border-right: lightblue 1px solid; border-top: lightblue 1px solid;
                                        border-left: lightblue 1px solid; border-bottom: lightblue 1px solid; background-color: #cccccc;">
                                        <asp:Label ID="lbltransfer" runat="server" Text="Detail Of Truck Challan (Transfer Fair Price Shop)"
                                            Width="360px" Font-Bold="True" Font-Size="12px"></asp:Label></td>
                                </tr>
                                <tr style="height: 25px">
                                    <td style="border-right: lightblue 1px solid; border-top: lightblue 1px solid; border-left: lightblue 1px solid;
                                        border-bottom: lightblue 1px solid; background-color: lightslategray;" colspan="4"
                                        align="center">
                                        <asp:Label ID="lbltransferdepot" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Text="To be entered By Sending Issue Center" Width="300px"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid;" align="left">
                                        <asp:Label ID="lblpendqty" runat="server" Width="82px" Visible="False"></asp:Label></td>
                                    <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid;">
                                        <asp:TextBox ID="txtrono" runat="server" Width="148px" Visible="False"></asp:TextBox>
                                    </td>
                                    <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid;">
                                        <asp:Label ID="lbltid" runat="server" Visible="False"></asp:Label></td>
                                    <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid;">
                                        <asp:TextBox ID="txtroqty" runat="server" Width="130px" Visible="False"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 18px;" align="left">
                                        <asp:Label ID="lblDistrictName" runat="server" Text="Sending District"></asp:Label></td>
                                    <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 18px;">
                                      
                                        <asp:Label ID="lbldist" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Navy"></asp:Label></td>
                                    <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 18px;">
                                        <asp:Label ID="lblNameDepot" runat="server" Text="Sending Depot"></asp:Label></td>
                                    <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 18px;">
                                     
                                        <asp:Label ID="lbldepo" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Navy"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td align="left" class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid;
                                        font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
                                        position: static; background-color: #cfdcc8">
                                        <asp:Label ID="lblyear" runat="server" Text=" Sending Year"></asp:Label></td>
                                    <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
                                        font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
                                        position: static; background-color: #cfdcc8">
                                        <asp:DropDownList ID="ddlallot_year" runat="server" Width="153px">
                                        </asp:DropDownList></td>
                                    <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid;
                                        font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
                                        position: static; background-color: #cfdcc8">
                                        <asp:Label ID="lblmonth" runat="server" Text="Sending Month" Width="109px"></asp:Label></td>
                                    <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
                                        font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
                                        position: static; background-color: #cfdcc8">
                                        <asp:DropDownList ID="ddlalotmm" runat="server" Width="153px">
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
                                </tr>
                                <tr>
                                    <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid;" align="left">
                                        <asp:Label ID="lbl_Bookno" runat="server" Text="Book No." Width="138px"></asp:Label></td>
                                    <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid;">
                                        <asp:TextBox ID="txtbookno" runat="server" Width="148px" MaxLength="20"></asp:TextBox>
                                    </td>
                                    <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid;">
                                        <asp:Label ID="lbl_Saralno" runat="server" Text="Saral No."></asp:Label>
                                        &nbsp;
                                    </td>
                                    <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid;">
                                        <asp:TextBox ID="txt_Saral" runat="server" Width="148px" MaxLength="20"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_Saral"
                                            ErrorMessage="Saral Number Required" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid;" align="left">
                                        <asp:Label ID="lblbuilty" runat="server" Text="Builty No." Width="138px"></asp:Label></td>
                                    <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid;">
                                        <asp:TextBox ID="txtBuiltyno" runat="server" Width="148px" MaxLength="20"></asp:TextBox>
                                    </td>
                                    <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid;">
                                        <asp:Label ID="lblbuiltydate" runat="server" Text="Date" Width="97px"></asp:Label>
                                        &nbsp;
                                    </td>
                                    <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid;">
                                        <asp:TextBox ID="txtbuiltydate" runat="server" Width="100px"></asp:TextBox>

                                        <script type="text/javascript">
	                                           new tcal ({
				                                  'formname': '0',
				                                     'controlname': 'ctl00_ContentPlaceHolder1_txtbuiltydate'
	                                                    });
                                        </script>

                                       </td>
                                </tr>
                                <tr>
                                    <td align="Center" colspan="4" style="border-right: lightblue 1px solid; border-top: lightblue 1px solid;
                                        font-size: 10pt; border-left: lightblue 1px solid; border-bottom: lightblue 1px solid;
                                        position: static; height: 24px; background-color: lightslategray">
                                        <b>
                                            <asp:Label ID="lbl_comm_scheme" runat="server" Text="Commodity & Scheme Details"
                                                ForeColor="whiteSmoke"></asp:Label>
                                        </b>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 24px;" align="left">
                                        <asp:Label ID="lblCommodity" runat="server" Text="Commodity"></asp:Label></td>
                                    <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 24px;">
                                        <asp:DropDownList ID="ddlcomdty" runat="server" Width="153px" AutoPostBack="false">
                                         
                                        </asp:DropDownList></td>
                                    <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 24px;">
                                        <asp:Label ID="lblScheme" runat="server" Text="Scheme"></asp:Label></td>
                                    <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 24px;">
                                        <asp:DropDownList ID="ddlscheme" runat="server" Width="153px">
                                      
                                        </asp:DropDownList></td>
                                </tr>
                                 <tr>
                                    <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 24px;" align="left">
                                        <asp:Label ID="lbldispsource" runat="server" Text="Stock Issued From" Width="137px"></asp:Label></td>
                                    <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 24px;">
                                        <asp:DropDownList ID="ddlsarrival" runat="server" Width="153px">
                                        </asp:DropDownList></td>
                                    <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 24px;">
                                        <asp:Label ID="lblGodownNo" runat="server" Text="Dispatch Godown"></asp:Label></td>
                                    <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 24px;">
                                        <asp:DropDownList ID="ddlgodown" runat="server" Width="153px" OnSelectedIndexChanged="ddldispdipo_SelectedIndexChanged"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="01" Selected="True">-Select-</asp:ListItem>
                                        </asp:DropDownList></td>
                                </tr>
                                 <tr>
                                    <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 24px;" align="left">
                                        <asp:Label ID="lblbqty" runat="server" Text="Balance Quantity "></asp:Label></td>
                                    <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 24px;">
                                        <asp:TextBox ID="txtbqty" runat="server" Width="100px" BackColor="wheat" ReadOnly="true"></asp:TextBox>Qtls.</td>
                                    <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 24px;">
                                        <asp:Label ID="lbl_cur_bags" runat="server" Text="Current Bags" Width="115px"></asp:Label></td>
                                    <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 24px;">
                                        <asp:TextBox ID="txtcurbags" runat="server" Width="147px" BackColor="wheat" ReadOnly="true"></asp:TextBox></td>
                                </tr>
                                 <tr>
                                    <td align="Center" colspan="4" style="border-right: lightblue 1px solid; border-top: lightblue 1px solid;
                                        font-size: 10pt; border-left: lightblue 1px solid; border-bottom: lightblue 1px solid;
                                        position: static; height: 24px; background-color: lightslategray">
                                        <b>
                                            <asp:Label ID="lbl_Dispatch_Details" runat="server" Text=">>> Dispatching Details"
                                                ForeColor="whiteSmoke"></asp:Label>
                                        </b>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 24px;" align="left">
                                         <asp:Label ID="lblQuantity" runat="server" Text="Quantity Issue"></asp:Label>
                                        </td>
                                    <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 24px;">
                                        <asp:TextBox ID="txtquant" runat="server" Width="100px" MaxLength="8"></asp:TextBox>Qtls.
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtquant"
                                            ErrorMessage="Quantity Required" ValidationGroup="FPS">*</asp:RequiredFieldValidator></td>
                                       
                                       
                                    <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 24px;">
                                      <asp:Label ID="lblBagNumber" runat="server" Text="NO.Of Bags"></asp:Label>
                                        </td>
                                    <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 24px;">
                                        <asp:TextBox ID="txtbagno" runat="server" Width="150px" MaxLength="8" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtbagno"
                                            ErrorMessage="No. of Bags Required" ValidationGroup="FPS">*</asp:RequiredFieldValidator></td>
                                </tr>
                                 <tr>
                                    <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 24px;" align="left">
                                        <asp:Label ID="lbldisdate" runat="server" Text="Dispatch Date"></asp:Label></td>
                                    <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 24px;">
                                        <asp:TextBox ID="DaintyDate1" runat="server" Width="125px"></asp:TextBox>
                                         <script type="text/javascript">
	                                        new tcal ({
				                                'formname': '0',
				                             'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate1'
	                                                 });
                                        </script>
                                        </td>
                                    <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 24px;">
                                        <asp:Label ID="lbldistime" runat="server" Text="Time Of Dispatch"></asp:Label></td>
                                    <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 24px;">
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
                                        <asp:DropDownList ID="ddlampm" runat="server" Width="48px">
                                            <asp:ListItem Value="01">AM</asp:ListItem>
                                            <asp:ListItem Value="02">PM</asp:ListItem>
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td align="Center" colspan="4" style="border-right: lightblue 1px solid; border-top: lightblue 1px solid;
                                        font-size: 10pt; border-left: lightblue 1px solid; border-bottom: lightblue 1px solid;
                                        position: static; height: 24px; background-color: lightslategray">
                                        <b>
                                            <asp:Label ID="lbl_Receive_Details" runat="server" Text=">>> Receiving Details" ForeColor="whiteSmoke"></asp:Label>
                                        </b>
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 24px;" align="left">
                                        <asp:Label ID="lblrecddist" runat="server" Text="Receiving District"></asp:Label></td>
                                    <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 24px;">
                                        <asp:DropDownList ID="ddldistrict" runat="server" Width="153px" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                                        </asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddldistrict"
                                            ErrorMessage="Select Receiving District" InitialValue="--Select--" ValidationGroup="FPS">*</asp:RequiredFieldValidator></td>
                                    <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 24px;">
                                        <asp:Label ID="lblrecdepo" runat="server" Text="Receiving Depot"></asp:Label></td>
                                    <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 24px;">
                                        <asp:DropDownList ID="ddlissuecenter" runat="server" Width="153px" >
                                          
                                        </asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlissuecenter"
                                            ErrorMessage="Select Receiving Depot" InitialValue="--Select--" ValidationGroup="FPS">*</asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 24px;" align="left">
                                        <asp:Label ID="lbl_Block" runat="server" Text="Block"></asp:Label></td>
                                    <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 24px;">
                                        <asp:DropDownList ID="ddl_block" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_block_SelectedIndexChanged"
                                            Width="153px">
                                        </asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddl_block"
                                            ErrorMessage="Select FPS Block" InitialValue="--Select--" ValidationGroup="FPS">*</asp:RequiredFieldValidator>
                                        </td>
                                    <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 24px;">
                                        <asp:Label ID="lbl_fpsname" runat="server" Text="FPS"></asp:Label></td>
                                    <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 24px;">
                                       <asp:DropDownList ID="ddl_fps_name" runat="server" Width="153px">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddl_fps_name"
                                            ErrorMessage="Select FPS Name" InitialValue="--Select--" ValidationGroup="FPS">*</asp:RequiredFieldValidator></td>
                                </tr>
                            
                                <tr>
                                    <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 10px;" align="left" colspan="4">

                                       
                                        </td>
                                </tr>
                              
                                <tr>
                                    <td align="center"  style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 24px;"  colspan="4">
                                        <asp:Button ID="btnAdd_FPS" runat="server" Text="Add FPS"  Width="120px" Height="25px" OnClick="btnAdd_FPS_Click" ValidationGroup="FPS"  />
                                    </td>
                                </tr>
                                 <tr id="tr1" runat="server" visible="false">
                                    <td align="Center" colspan="4" style="border-right: lightblue 1px solid; border-top: lightblue 1px solid;
                                        font-size: 10pt; border-left: lightblue 1px solid; border-bottom: lightblue 1px solid;
                                        position: static; height: 24px; background-color: lightslategray">
                                        <b>
                                            <asp:Label ID="lbl_FPS_Details" runat="server" Text="Added FPS Details" ForeColor="whiteSmoke"></asp:Label>
                                        </b>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="4" style="border-right: silver 1px solid; border-top: silver 1px solid;
                                        border-left: silver 1px solid; border-bottom: silver 1px solid">
                                        <asp:Panel ID="pnl_Grid" runat="server" ScrollBars="Auto" Width="100%" Visible="False"
                                            Height="64px">
                                            <asp:GridView ID="Gv_FPS_Details" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Font-Bold="False"
                                                Font-Size="Small" Width="100%" Height="100px">
                                                <RowStyle ForeColor="#000066" />
                                                <Columns>
                                                    <asp:CommandField SelectText="Delete" ShowSelectButton="True">
                                                        <ItemStyle Font-Bold="False" />
                                                    </asp:CommandField>
                                                   <asp:BoundField HeaderText="fps_code" DataField="fps_code" Visible="False" />
                                                    <asp:BoundField DataField="fps_name" HeaderText="गंतव्य संग्रहण केंद्र/रैक पॉइंट" SortExpression="fps_name">
                                                        <ItemStyle Font-Bold="False" Font-Names="Arial Unicode MS" Font-Size="9pt" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="commodity_name" HeaderText="स्कंध का नाम" SortExpression="commodity_name">
                                                        <ItemStyle Font-Bold="False" />
                                                    </asp:BoundField>
                                                   
                                                    <asp:BoundField DataField="scheme_name" HeaderText="श्रेडी / किस्म" SortExpression="scheme_name">
                                                        <ItemStyle Font-Bold="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="qtyBags" HeaderText="लोड की गई मात्रा(बोरी में)" SortExpression="allot_qty" />
                                               
                                                    <asp:BoundField DataField="allot_qty" HeaderText="शुद्ध वजन(Qtls में)" SortExpression="rate_qtls">
                                                        <ItemStyle Font-Bold="False" />
                                                    </asp:BoundField>
                                                   
                                                </Columns>
                                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#006699" Font-Bold="False" Font-Size="Small" ForeColor="White" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="Center" colspan="4" style="border-right: lightblue 1px solid; border-top: lightblue 1px solid;
                                        font-size: 10pt; border-left: lightblue 1px solid; border-bottom: lightblue 1px solid;
                                        position: static; height: 24px; background-color: lightslategray">
                                        <b>
                                            <asp:Label ID="lbl_Head_Transport" runat="server" Text="Transport Details" ForeColor="whiteSmoke"></asp:Label>
                                        </b>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 24px;" align="left">
                                        <asp:Label ID="lblTrans" runat="server" Text="Transporter Name " Width="119px"></asp:Label></td>
                                    <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 24px;">
                                        <asp:DropDownList ID="ddltransporter" runat="server" Width="153px">
                                        </asp:DropDownList></td>
                                    <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 24px;">
                                        <asp:Label ID="lblchallanNumber" runat="server" Text="Truck Challan No."></asp:Label></td>
                                    <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 24px;">
                                        <asp:TextBox ID="txttrukcno" runat="server" Width="146px" MaxLength="20"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txttrukcno"
                                            ErrorMessage="Truck Challan Number Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
                                </tr>
                                <tr style="height: 24px">
                                    <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid;" align="left">
                                        <asp:Label ID="lblTruckNumber" runat="server" Text="Truck No."></asp:Label></td>
                                    <td style="font-size: 10pt; position: static; background-color: #cfdcc8; border-right: silver 1px solid;
                                        border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                        <asp:TextBox ID="txttruckno" runat="server" Width="146px" MaxLength="20"></asp:TextBox></td>
                                    <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid;">
                                         <asp:Label ID="lbl_Driver_Name" runat="server" Text="Driver Name"></asp:Label>
                                        </td>
                                    <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid;">
                                        <asp:TextBox ID="txt_Driver" runat="server" Width="146px" MaxLength="50"></asp:TextBox>
                                        </td>
                                </tr>
                             
                    
                                <tr style="height:50px">
                                    <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; " align="left">
                                        <asp:Label ID="lblRemark" runat="server" Text="Remark"></asp:Label></td>
                                    <td valign="top" align="left" colspan="3" rowspan="2" style="font-size: 10pt; position: static;
                                        background-color: #cfdcc8; border-right: silver 1px solid; border-top: silver 1px solid;
                                        border-left: silver 1px solid; border-bottom: silver 1px solid; ">
                                        <asp:TextBox ID="txtremark" runat="server" Width="440px" TextMode="MultiLine" Height="50px" MaxLength="2000"></asp:TextBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid; height: 10px;" align="left" colspan="4">

                                       
                                        </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 10pt; position: static; background-color: #cfdcc8; border-right: silver 1px solid;
                                        border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;
                                        height: 24px;" align="center" colspan="4">
                                        <asp:Button ID="btnsave" runat="server" Text="Submit" OnClick="btnsave_Click" ValidationGroup="1"
                                            Width="90px" />
                                        <asp:Button ID="btnaddnew" runat="server" Text="New" OnClick="btnaddnew_Click" Width="90px" />&nbsp;
                                        <asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" Width="90px" />
                                        <asp:HyperLink ID="lnkPrintPage" runat="server" NavigateUrl="#" Visible="False" Width="109px">Print Challan</asp:HyperLink></td>
                                </tr>
                                <tr>
                                    <td style="font-size: 10pt; position: static; background-color: #cfdcc8; border-right: silver 1px solid;
                                        border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;
                                        height: 24px;" align="center" colspan="4">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                            ValidationGroup="1" Width="217px" ShowSummary="false" />
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                                            ValidationGroup="FPS" Width="217px" ShowSummary="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="font-size: 10pt; position: static; background-color: #cfdcc8;
                                        height: 18px;" align="left">
                                        <asp:Label ID="Label1" runat="server" Text="Label" Width="404px" Visible="False"></asp:Label></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </center>
        </div>
    </center>
</asp:Content>
