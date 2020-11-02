<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="PM_Inspection_ByOneMember.aspx.cs" Inherits="IssueCenter_PM_Inspection_ByOneMember" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        .auto-styleNSC
        {
            width: 622px;
        }


        .ButtonClass
        {
        }
        </style>
    <link href="../PaddyMilling/calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="../PaddyMilling/calendar/calendarLot.js"></script>

    <table id="tablemain" runat="server" style="width: 900px; border-style: solid; border-width: 2px; border:1px; border-color: black; background-color: burlywood" border="1">
        <tr>
            <td colspan="4" style="text-align: center; font-size: large; text-decoration: underline; color: white; background-color: navy; border-style: none">
                <strong>Quality Inspection Input Menu(Wheat/Rice) 
                </strong>

            </td>



        </tr>
        <tr id="trlabel" runat="server" visible="false">
            <td colspan="4" style="text-align: center; font-size: large; text-decoration: underline; color: white; background-color: red; border-style: none">
               <asp:Label ID="Label2" runat="server" Style="text-align: center; font-weight: 700;" ForeColor="white" Visible="true"></asp:Label>

            </td>



        </tr>

        <tr>
            <td><strong>Crop Year</strong></td>
            <td>
                <br />
                <asp:DropDownList ID="ddlCropYear" runat="server" Width="175" AutoPostBack="True">
                </asp:DropDownList>
                <br />
                <br />
            </td>
            <td><strong>Season</strong></td>
            <td>
                <br />
                <asp:DropDownList ID="ddlCommo" runat="server" Width="175" AutoPostBack="True" OnSelectedIndexChanged="ddlCommo_SelectedIndexChanged">
                    <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>

                    <asp:ListItem Text="Rabi" Value="Rabi"></asp:ListItem>

                    <asp:ListItem Text="Kharif" Value="Kharif"></asp:ListItem>

                </asp:DropDownList>
                <br />
                <br />
            </td>
        </tr>


        


        <tr>
            <td>
                <strong>District</strong>

            </td>
            <td>
                <br />
                <asp:DropDownList ID="ddldist" runat="server" Width="175px" AutoPostBack="True" OnSelectedIndexChanged="ddldist_SelectedIndexChanged" Height="25px">
                </asp:DropDownList>
                <br />
                <br />
            </td>

            <td>
                <strong>Issue Center</strong>

            </td>
            <td>
                <br />
                <asp:DropDownList ID="ddlIC" runat="server" Width="175px" AutoPostBack="True" OnSelectedIndexChanged="ddlIC_SelectedIndexChanged" Height="25px">
                </asp:DropDownList>
                <br />
                <br />
            </td>
        </tr>


        <tr>
            <td>
                <strong>Godown</strong>

            </td>
            <td>
                <br />
                <asp:DropDownList ID="ddlgd" runat="server" Width="175px" AutoPostBack="True" OnSelectedIndexChanged="ddlgd_SelectedIndexChanged">
                </asp:DropDownList>
                <br />
                <br />
            </td>

            <td>
                <strong>Stack</strong>

            </td>
            <td>
                <br />
                <asp:DropDownList ID="ddlSK" runat="server" Width="175" AutoPostBack="True">
                </asp:DropDownList>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <strong>Bags</strong>

            </td>
            <td>
                <br />
                <asp:TextBox ID="txtbags" runat="server" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" Width="175px" AutoComplete="off" MaxLength="45"></asp:TextBox>
                
                <br />
                <br />
            </td>
            <td>
                

            </td>
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              

                
              
                

                

            </td>
        </tr>
        <tr id="trLOT" runat="server" visible="false">
            <td colspan="4" style="text-align: center;">

                 
                <table style="width:1050px; " border="1" id="tbllot" runat="server"  >
                    <tr>
                        <td colspan="4"  style="text-align: center; font-size: large; text-decoration: underline; color: white; background-color: navy; border-style: none"> <strong> Lot Numbers in a stack</strong></td>
                    </tr>
                    <tr>
                        <td> <strong>1:- Lot No.</strong></td>
                        <td>  <asp:TextBox ID="txtLotNo1" runat="server"  onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" Width="137px" AutoComplete="off" MaxLength="4"></asp:TextBox>
                            
                        </td>
                        <td> <strong>5:- Lot No.</strong></td>
                        <td>  <asp:TextBox ID="txtLotNo5" runat="server" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" Width="137px" AutoComplete="off" MaxLength="45"></asp:TextBox>
                             
                        </td>
                    </tr>
                    <tr>
                        <td> <strong>2:- Lot No.</strong></td>
                        <td>   <asp:TextBox ID="txtLotNo2" runat="server" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" Width="137px" AutoComplete="off" MaxLength="45"></asp:TextBox>
                             
                        </td>
                        <td> <strong>6:- Lot No.</strong></td>
                        <td>   <asp:TextBox ID="txtLotNo6" runat="server" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" Width="137px" AutoComplete="off" MaxLength="45"></asp:TextBox>
                            
                        </td>
                    </tr>
                    <tr>
                        <td> <strong>3:- Lot No.</strong></td>
                        <td>  <asp:TextBox ID="txtLotNo3" runat="server" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" Width="137px" AutoComplete="off" MaxLength="45"></asp:TextBox>
                            
                        </td>
                        <td> <strong>7:- Lot No.</strong></td>
                        <td>  <asp:TextBox ID="txtLotNo7" runat="server" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" Width="137px" AutoComplete="off" MaxLength="45"></asp:TextBox>
                            
                        </td>
                    </tr>
                     <tr>
                        <td> <strong>4:- Lot No.</strong></td>
                        <td>  <asp:TextBox ID="txtLotNo4" runat="server" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" Width="137px" AutoComplete="off" MaxLength="45"></asp:TextBox>
                            
                        </td>
                        <td> <strong>8:- Lot No.</strong></td>
                        <td>  <asp:TextBox ID="txtLotNo8" runat="server" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" Width="137px" AutoComplete="off" MaxLength="45"></asp:TextBox>
                            
                        </td>
                    </tr>
                    
                </table>

               </td>

        </tr>
        <tr id="trMiller" runat="server" visible="false">
            <td>
                <br />

                <strong>Miller&#39;s District</strong>
                <br />
                <br />

            </td>
            <td>
                <asp:DropDownList ID="ddlmill_dist" runat="server" Width="175px" AutoPostBack="True" OnSelectedIndexChanged="ddlmill_dist_SelectedIndexChanged" Height="25px">
                </asp:DropDownList>
            </td>
            <td>
                <strong>Miller&#39;s Name</strong>
            </td>
            <td>
                <asp:DropDownList ID="ddlMillname" runat="server" Width="350px" AutoPostBack="True" Height="25px">
                </asp:DropDownList>

            </td>
        </tr>
        <tr id="trM_Miller" runat="server" visible="false">
            <td colspan="4" style="text-align: center; font-size: large; text-decoration: underline; color: white; background-color: navy; border-style: none">
                <strong>क्या एक से जायदा मिलर हैं?<asp:CheckBox ID="chkmill" runat="server" AutoPostBack="True" OnCheckedChanged="chkmill_CheckedChanged"  />
                </strong>

            </td>



        </tr>
        <tr id="trMillName" runat="server" visible="false" >
            <td colspan="4" style="text-align: center;">

                <table style="width:1050px; background-color:white;" border="1" runat="server" id="tblmillname">
                   <tr>
            <td>
                <br />

                <strong>&nbsp;Miller&#39;s District (1) </strong>
                <br />
                <br />

            </td>
            <td>
                <asp:DropDownList ID="ddlmill_dist1" runat="server" Width="175px" AutoPostBack="True" OnSelectedIndexChanged="ddlmill_dist1_SelectedIndexChanged" Height="25px">
                </asp:DropDownList>
            </td>
            <td>
                <strong>Miller&#39;s Name (1)</strong>
            </td>
            <td>
                <asp:DropDownList ID="ddlMillname1" runat="server" Width="350px" AutoPostBack="True" Height="25px">
                </asp:DropDownList>

            </td>
        </tr>
                    <tr>
            <td>
                <br />

                <strong>Miller&#39;s District (2)</strong>
                <br />
                <br />

            </td>
            <td>
                <asp:DropDownList ID="ddlmill_dist2" runat="server" Width="175px" AutoPostBack="True" OnSelectedIndexChanged="ddlmill_dist2_SelectedIndexChanged" Height="25px">
                </asp:DropDownList>
            </td>
            <td>
                <strong>Miller&#39;s Name (2)</strong>
            </td>
            <td>
                <asp:DropDownList ID="ddlMillname2" runat="server" Width="350px" AutoPostBack="True" Height="25px">
                </asp:DropDownList>

            </td>
        </tr>
                    <tr>
            <td>
                <br />

                <strong>Miller&#39;s District (3)</strong>
                <br />
                <br />

            </td>
            <td>
                <asp:DropDownList ID="ddlmill_dist3" runat="server" Width="175px" AutoPostBack="True" OnSelectedIndexChanged="ddlmill_dist3_SelectedIndexChanged" Height="25px">
                  

                   
                </asp:DropDownList>
            </td>
            <td>
                <strong>Miller&#39;s Name (3)</strong>
            </td>
            <td>
                <asp:DropDownList ID="ddlMillname3" runat="server" Width="350px" AutoPostBack="True" Height="25px">
                </asp:DropDownList>

            </td>
        </tr>
                    <tr>
            <td>
                <br />

                <strong>Miller&#39;s District (4)</strong>
                <br />
                <br />

            </td>
            <td>
                <asp:DropDownList ID="ddlmill_dist4" runat="server" Width="175px" AutoPostBack="True" OnSelectedIndexChanged="ddlmill_dist4_SelectedIndexChanged" Height="25px">
                </asp:DropDownList>
            </td>
            <td>
                <strong>Miller&#39;s Name (4)</strong>
            </td>
            <td>
                <asp:DropDownList ID="ddlMillname4" runat="server" Width="350px" AutoPostBack="True" Height="25px">
                </asp:DropDownList>

            </td>
        </tr>
                </table>

               
               </td>

        </tr>
        <tr>
            <td><strong>Date Of Inspection</strong></td>
            <td>
                <br />
                &nbsp;
                    <asp:TextBox ID="txtDate" runat="server" Width="137px" Height="22px" ReadOnly="True" AutoPostBack="true"></asp:TextBox>
                <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtDate' , 'expiry=true,elapse=-150,restrict=true,close=true' )" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtDate" ErrorMessage="RequiredFieldValidator" Font-Bold="False" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                <br />
                <br />
            </td>

            <td>
                <strong>Inspector Name</strong>

            </td>
            <td>
                <br />
                <asp:DropDownList ID="ddl_Insp" runat="server" Width="350px" AutoPostBack="True" OnSelectedIndexChanged="ddl_Insp_SelectedIndexChanged" Height="25px">
                </asp:DropDownList>
                <br />
                <br />
            </td>
        </tr>


        <tr>
            <td>
                <strong>Designation</strong>

            </td>
            <td>
                <br />
                <asp:TextBox ID="txtdesig" runat="server" Width="175px" AutoComplete="off" MaxLength="45" Enabled="false"></asp:TextBox>

                <br />
                <br />
            </td>

            <td>
                <strong>Mobile No.</strong>

            </td>
            <td>
                <asp:TextBox ID="txt_CountryCode" runat="server" Width="25px" onkeypress="return onlyNumbersbags(this);" Enabled="False" Style="margin-left: 7px;">+91</asp:TextBox>
                <asp:TextBox ID="txt_MobileNo" runat="server" Width="165px" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);"  MaxLength="10" AutoComplete="off" Style="margin-right: 2px"></asp:TextBox>
               
            </td>



        </tr>
    </table>
    <asp:Panel ID="panel2" Visible="false" runat="server">

        <table align="center" border="1" style="border-spacing: 1px; text-align: center; width: 894px; font-size: small" id="table2">

            <tr>

                <td colspan="4" style="color: white; width:1050px; background-color: navy; text-align: center; font-size: large; text-decoration: underline;"><strong>Quality Inspection By Team (Rice)</strong></td>
            </tr>
            <tr>


                <td style="width: 31px;">क्रम सं.</td>
                <td style="width: 31px; text-align: center;">अपवर्तन</td>
                <td style="width: 114px;">अधिकतम सीमा (प्रतिशत)<br />
                    <b>कामन</b></td>
                <td style="width: 114px;">गुणवत्ता परिक्षण उपरांत परिणाम<br />
                    <b>कामन</b>
                </td>
            </tr>
            <tr>


                <td rowspan="2" style="width: 31px">1.</td>
                <td style="text-align: left; height: 51px;">टोटा<br />
                </td>
                <td style="width: 114px; height: 51px;">
                    <asp:Label ID="LblTotaS" runat="server"></asp:Label>
                </td>
                <td style="width: 114px; height: 51px;">
                    <asp:TextBox ID="TxtTotaS" runat="server" Style="text-align: right" class="NumberDecimal" MaxLength="5" Width="104px" AutoComplete="off"
                        onblur="extractNumber(this,2,true);"
                        onkeyup="extractNumber(this,2,true);"
                        onkeypress="return blockNonNumbers(this, event, true, true);" ValidationGroup="Common" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtTotaS" ErrorMessage="Invalid Number" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                    <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtTotaS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>

                <td style="text-align: left; height: 51px;">छोटे टोटे</td>
                <td style="height: 51px">
                    <asp:Label ID="LblChoteToteS" runat="server"></asp:Label>
                </td>
                <td style="height: 51px">
                    <asp:TextBox ID="TxtChoteToteS" runat="server" Style="text-align: right" class="NumberDecimal" MaxLength="5" Width="104px" AutoComplete="off"
                        onblur="extractNumber(this,2,true);"
                        onkeyup="extractNumber(this,2,true);"
                        onkeypress="return blockNonNumbers(this, event, true, true);" ValidationGroup="Common" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TxtChoteToteS" ErrorMessage="Invalid Number" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                    <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtChoteToteS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                </td>
            </tr>



            <tr>
                <td style="width: 31px; height: 51px;">2.</td>
                <td style="text-align: left; height: 51px;">विजातीय तत्व **</td>
                <td style="width: 114px; height: 51px;">
                    <asp:Label ID="LblVijatiyeS" runat="server"></asp:Label>
                </td>
                <td style="width: 114px; height: 51px;">
                    <asp:TextBox ID="txtVijatiyeS" runat="server" Style="text-align: right" class="NumberDecimal" MaxLength="5" Width="104px" AutoComplete="off"
                        onblur="extractNumber(this,2,true);"
                        onkeyup="extractNumber(this,2,true);"
                        onkeypress="return blockNonNumbers(this, event, true, true);" ValidationGroup="Common" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtVijatiyeS" ErrorMessage="Invalid Number" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                    <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtVijatiyeS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td style="width: 31px">3.</td>
                <td style="text-align: left; height: 51px;">क्षतिग्रस्त दाने</td>
                <td style="width: 114px">
                    <asp:Label ID="LblDamageDaaneS" runat="server"></asp:Label>
                </td>
                <td style="width: 114px">
                    <asp:TextBox ID="txtDamageDaaneS" runat="server" Style="text-align: right" class="NumberDecimal" MaxLength="5" Width="104px" AutoComplete="off"
                        onblur="extractNumber(this,2,true);"
                        onkeyup="extractNumber(this,2,true);"
                        onkeypress="return blockNonNumbers(this, event, true, true);" ValidationGroup="Common" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtDamageDaaneS" ErrorMessage="Invalid Number" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                    <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDamageDaaneS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td style="width: 31px; height: 51px;">4.</td>
                <td style="text-align: left; height: 51px;">बदरंग दाने</td>
                <td style="width: 114px; height: 51px;">
                    <asp:Label ID="LblBadrangDaaneS" runat="server"></asp:Label>
                </td>
                <td style="width: 114px; height: 51px;">
                    <asp:TextBox ID="txtBadrangDaaneS" runat="server" Style="text-align: right" class="NumberDecimal" MaxLength="5" Width="104px" AutoComplete="off"
                        onblur="extractNumber(this,2,true);"
                        onkeyup="extractNumber(this,2,true);"
                        onkeypress="return blockNonNumbers(this, event, true, true);" ValidationGroup="Common" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtBadrangDaaneS" ErrorMessage="Invalid Number" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                    <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtBadrangDaaneS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                </td>
            </tr>



            <tr>
                <td style="width: 31px; height: 51px;">5.</td>
                <td style="text-align: left; height: 51px;">चाकी दाने</td>
                <td style="width: 114px; height: 51px;">
                    <asp:Label ID="LblChaakiDaaneS" runat="server"></asp:Label>
                </td>
                <td style="width: 114px; height: 51px;">
                    <asp:TextBox ID="txtChaakiDaaneS" runat="server" Style="text-align: right" class="NumberDecimal" MaxLength="5" Width="104px" AutoComplete="off"
                        onblur="extractNumber(this,2,true);"
                        onkeyup="extractNumber(this,2,true);"
                        onkeypress="return blockNonNumbers(this, event, true, true);" ValidationGroup="Common" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="txtChaakiDaaneS" ErrorMessage="Invalid Number" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                    <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtChaakiDaaneS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td style="width: 31px; height: 51px;">6.</td>
                <td style="text-align: left; height: 51px;">लाल दाने</td>
                <td style="width: 114px; height: 51px;">
                    <asp:Label ID="LblLaalDaaneS" runat="server"></asp:Label>
                </td>
                <td style="width: 114px; height: 51px;">
                    <asp:TextBox ID="txtLaalDaaneS" runat="server" Style="text-align: right" class="NumberDecimal" MaxLength="5" Width="104px" AutoComplete="off"
                        onblur="extractNumber(this,2,true);"
                        onkeyup="extractNumber(this,2,true);"
                        onkeypress="return blockNonNumbers(this, event, true, true);" ValidationGroup="Common" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ControlToValidate="txtLaalDaaneS" ErrorMessage="Invalid Number" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                    <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtLaalDaaneS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                </td>
            </tr>


            </tr>

             <td style="width: 31px; height: 51px;">7.</td>
            <td style="text-align: left; height: 51px;">नमी तत्व (R)</td>
            <td style="width: 114px; height: 51px;">
                <asp:Label ID="LblNamiS" runat="server"></asp:Label>
            </td>
            <td style="width: 114px; height: 51px;">
                <asp:TextBox ID="txtNamiS" runat="server" AutoComplete="off" class="NumberDecimal" MaxLength="5" onblur="extractNumber(this,2,true);" onfocus="this.select();" onkeypress="return blockNonNumbers(this, event, true, true);" onkeyup="extractNumber(this,2,true);" onmouseup="return false;" Style="text-align: right" ValidationGroup="Common" Width="104px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator20" runat="server" ControlToValidate="txtNamiS" Display="Dynamic" ErrorMessage="Invalid Number" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtNamiS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                </b></td>

            <tr>
                <td colspan="4" style="height: 40px; background-color: burlywood;">

                    <asp:Button ID="btnQuilityTested" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Submit For Quility Inspection (Paddy)" Width="254px" CssClass="ButtonClass" OnClick="btnQuilityTested_Click" OnClientClick="return validate1();" Enabled="False" />


                </td>
            </tr>


        </table>
    </asp:Panel>

    <asp:Panel ID="panel3" Visible="false" runat="server">

        <table align="center" class="auto-styleNSC" id="table3" style="background-color: #FFFACD;">
            <tr>

                <td style="background-color: burlywood;">

                    <asp:Button ID="btnNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnNew_Click" />
                </td>


                <td style="background-color: burlywood;">
                    <asp:Button ID="btnPass" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Pass" Width="115px" CssClass="ButtonClass" OnClick="btnPass_Click" OnClientClick="return validate1();" Enabled="False" />

                </td>

                <td style="background-color: burlywood;">
                    <asp:Button ID="btnfail" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Fail" Width="115px" CssClass="ButtonClass" OnClick="btnfail_Click" Enabled="False" OnClientClick="return validate();" />
                </td>
                
                <td style="background-color: burlywood;">
                    <asp:Button ID="Button1" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Print" Width="115px" CssClass="ButtonClass" OnClick="Button1_Click" Enabled="false" OnClientClick="return validate();" />
                </td>




            </tr>


        </table>
    </asp:Panel>










    <asp:Panel ID="panel4" Visible="false" runat="server">

        <table align="center" border="1" style="border-spacing: 1px; width:1050px; text-align: center; width: 894px; font-size: small" id="table4">
            <tr>
                <td colspan="4" style="color: white; background-color: navy; text-align: center; font-size: large; text-decoration: underline;"><strong>Inspection By Team (WHEAT)</strong></td>

            </tr>
            <tr>


                <td style="width: 31px">क्रम सं.</td>
                <td>अपवर्तन</td>
                <td style="width: 114px">अधिकतम सीमा (प्रतिशत)<br />
                    <b>कामन</b></td>

                <td style="width: 114px">गुणवत्ता परिक्षण उपरांत परिणाम<br />

                </td>

            </tr>
            <tr>
                <td style="width: 31px; height: 51px;">1.</td>
                <td style="text-align: left; height: 51px;">Foreign Matter</td>
                <td style="width: 31px; height: 51px; text-align: center">0.75</td>


                <td style="width: 114px; height: 51px;">
                    <asp:TextBox ID="txtFM" runat="server" class="NumberDecimal" Style="text-align: right" MaxLength="5" Width="104px" AutoComplete="off"
                        onblur="extractNumber(this,2,true);"
                        onkeyup="extractNumber(this,2,true);"
                        onkeypress="return blockNonNumbers(this, event, true, true);" ValidationGroup="Common" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNamiS" ErrorMessage="Invalid Number" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                    <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNamiS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            
                </td>


            </tr>
            <tr>
                <td style="width: 31px; height: 51px;">2.</td>
                <td style="text-align: left; height: 51px;">Other Food Grain</td>
                <td style="width: 31px; height: 51px;">2.0</td>

                <td style="width: 114px; height: 51px;">
                    <asp:TextBox ID="txtOFG" runat="server" class="NumberDecimal" Style="text-align: right" MaxLength="5" Width="104px" AutoComplete="off"
                        onblur="extractNumber(this,2,true);"
                        onkeyup="extractNumber(this,2,true);"
                        onkeypress="return blockNonNumbers(this, event, true, true);" ValidationGroup="Common" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtNamiS" ErrorMessage="Invalid Number" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                    <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNamiS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            
                </td>


            </tr>
            <tr>
                <td style="width: 31px; height: 51px;">3.</td>
                <td style="text-align: left; height: 51px;">Damaged Grains</td>
                <td style="width: 31px; height: 51px;">2.0</td>
                <td style="width: 114px; height: 51px;">
                    <asp:TextBox ID="txtDG" runat="server" class="NumberDecimal" Style="text-align: right" MaxLength="5" Width="104px" AutoComplete="off"
                        onblur="extractNumber(this,2,true);"
                        onkeyup="extractNumber(this,2,true);"
                        onkeypress="return blockNonNumbers(this, event, true, true);" ValidationGroup="Common" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtNamiS" ErrorMessage="Invalid Number" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                    <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtNamiS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            
                </td>


            </tr>
            <tr>
                <td style="width: 31px; height: 51px;">4.</td>
                <td style="text-align: left; height: 51px;">Slightly Damaged, Discoloured Grains</td>
                <td style="width: 31px; height: 51px;">4.0</td>
                <td style="width: 114px; height: 51px;">
                    <asp:TextBox ID="txtSDDG" runat="server" class="NumberDecimal" Style="text-align: right" MaxLength="5" Width="104px" AutoComplete="off"
                        onblur="extractNumber(this,2,true);"
                        onkeyup="extractNumber(this,2,true);"
                        onkeypress="return blockNonNumbers(this, event, true, true);" ValidationGroup="Common" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtNamiS" ErrorMessage="Invalid Number" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                    <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtNamiS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            
                </td>


            </tr>
            <tr>
                <td style="width: 31px; height: 51px;">5.</td>
                <td style="text-align: left; height: 51px;">Shrivilled & Immature Grains</td>
                <td style="width: 31px; height: 51px;">6.0</td>
                <td style="width: 114px; height: 51px;">
                    <asp:TextBox ID="txtSIG" runat="server" class="NumberDecimal" Style="text-align: right" MaxLength="5" Width="104px" AutoComplete="off"
                        onblur="extractNumber(this,2,true);"
                        onkeyup="extractNumber(this,2,true);"
                        onkeypress="return blockNonNumbers(this, event, true, true);" ValidationGroup="Common" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="txtNamiS" ErrorMessage="Invalid Number" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                    <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtNamiS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            
                </td>


            </tr>

            <tr>
                <td style="width: 31px; height: 51px;">6.</td>
                <td style="text-align: left; height: 51px;">Weevilled Grains </td>
                <td style="width: 31px; height: 51px;">1.0</td>
                <td style="width: 114px; height: 51px;">
                    <asp:TextBox ID="txtWGC" runat="server" class="NumberDecimal" Style="text-align: right" MaxLength="5" Width="104px" AutoComplete="off"
                        onblur="extractNumber(this,2,true);"
                        onkeyup="extractNumber(this,2,true);"
                        onkeypress="return blockNonNumbers(this, event, true, true);" ValidationGroup="Common" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ControlToValidate="txtNamiS" ErrorMessage="Invalid Number" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                    <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtNamiS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            
                </td>


            </tr>

            <tr>
                <td style="width: 31px; height: 51px;">7.</td>
                <td style="text-align: left; height: 51px;">Moisture Content </td>
                <td style="width: 31px; height: 51px;">12.0</td>
                <td style="width: 114px; height: 51px;">
                    <asp:TextBox ID="txtMC" runat="server" class="NumberDecimal" Style="text-align: right" MaxLength="5" Width="104px" AutoComplete="off"
                        onblur="extractNumber(this,2,true);"
                        onkeyup="extractNumber(this,2,true);"
                        onkeypress="return blockNonNumbers(this, event, true, true);" ValidationGroup="Common" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ControlToValidate="txtNamiS" ErrorMessage="Invalid Number" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                    <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtNamiS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            
                </td>


            </tr>
            <tr>
                <td colspan="4" style="height: 40px; background-color: burlywood;">

                    <asp:Button ID="buttqualityTestWheat" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Submit For Quility Inspection (Wheat)" Width="288px" CssClass="ButtonClass" OnClick="buttqualityTestWheat_Click" OnClientClick="return validate1();" Enabled="False" />


                </td>
            </tr>













        </table>
    </asp:Panel>


    <asp:Panel ID="panel5" Visible="false" runat="server">

        <table align="center" class="auto-styleNSC" id="table5">
            <tr>

                <td style="background-color: burlywood;">

                    <asp:Button ID="buttnewWheat" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="buttnewWheat_Click" />
                </td>


                <td style="background-color: burlywood;">
                    <asp:Button ID="buttpasswheat" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Pass" Width="115px" CssClass="ButtonClass" OnClick="buttpasswheat_Click" OnClientClick="return validate1();" Enabled="False" />

                </td>

                <td style="background-color: burlywood;">
                    <asp:Button ID="buttfailwheat" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Fail" Width="115px" CssClass="ButtonClass" OnClick="buttfailwheat_Click" Enabled="False" OnClientClick="return validate();" />
                </td>



            </tr>


        </table>
    </asp:Panel>
</asp:Content>

