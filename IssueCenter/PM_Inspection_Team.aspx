<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="PM_Inspection_Team.aspx.cs" Inherits="IssueCenter_PM_Inspection_Team" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <style type="text/css">
        
        .auto-styleNSC {
            width: 622px;
        }

        
         .ButtonClass
         {}

        
    </style>
     <link href="../PaddyMilling/calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
     <script type="text/javascript" lang="" src="../PaddyMilling/calendar/calendarLot.js"></script>

    <table id="table1" runat="server" style="width: 100%; border-style: solid; border-width: 2px; background-color:azure">
        <tr>
            <td colspan="4" style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600; border-style: none">
                <strong>
                    Team Inspection CMR
                </strong>

            </td>



        </tr>
          
        <tr>
                <td><strong>Crop Year</strong></td>
                <td>
                    <br />
                   <asp:DropDownList ID="ddlCropYear" runat="server" Width="150" AutoPostBack="True" >
                    </asp:DropDownList>
                    <br />
                    <br />
                    </td>
           <td><strong>Season</strong></td>
                <td>
                    <br />
                   <asp:DropDownList ID="ddlCommo" runat="server" Width="150" AutoPostBack="True" OnSelectedIndexChanged="ddlCommo_SelectedIndexChanged">
                        <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>

                        <asp:ListItem Text="Rabi" Value="Rabi"></asp:ListItem>

                        <asp:ListItem Text="Kharif" Value="Kharif"></asp:ListItem>

                    </asp:DropDownList>
                    <br />
                    <br />
                    </td>
             </tr>
            
         <tr>
                <td><strong>Date Of Inspection</strong></td>
                <td >
                    <br />
                    &nbsp;
                    <asp:TextBox ID="txtDate" runat="server"  Width="137px" Height="22px" ReadOnly="True" AutoPostBack="true"></asp:TextBox>
                    <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtDate' , 'expiry=true,elapse=-150,restrict=true,close=true' )" /><asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtDate" ErrorMessage="RequiredFieldValidator" Font-Bold="False" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                    <br />
                    <br />
                </td>
            
                <td >
                    <strong>Team Selection</strong>

                </td>
                <td >
                    <br />
                    <asp:DropDownList ID="ddlTS" runat="server" Width="149px" AutoPostBack="True"  Height="25px" OnSelectedIndexChanged="ddlTS_SelectedIndexChanged"  >
                    </asp:DropDownList>
                    <br />
                    <br />
                </td>
            </tr>
        <tr id="row1" runat="server" visible="false">

            
            


                <td style="width: 1046px; background-color:white " colspan="4">
                    
                     <strong>Inspector's Name</strong>
                <br />
                    <asp:Panel runat="server" ID="Panel1" Visible="false">
                    

                <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" 
                       CellPadding="4" 
                        ForeColor="#333333" GridLines="None" Width="621px" CssClass="gridheader" 
                         
                        EnableModelValidation="True" >

                    <Columns>
            
            <asp:BoundField DataField="Inspector_Name" HeaderText="Inspector Name" >
                <HeaderStyle Font-Size="14px" Font-Names="Arial" />
            </asp:BoundField>
                         
            <asp:BoundField DataField="Department_Name" HeaderText="Department"  >
                <HeaderStyle Font-Size="14px" Font-Names="Arial" />
            </asp:BoundField>
                        
                       
                       
                        </Columns>
                </asp:GridView>
                        </asp:Panel>




            </td>



            


        </tr>
        <tr>
                <td >
                    <strong>District</strong>

                </td>
                <td >
                    <br />
                    <asp:DropDownList ID="ddldist" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddldist_SelectedIndexChanged" Height="25px" >
                    </asp:DropDownList>
                    <br />
                    <br />
                </td>
            
                <td >
                    <strong>Issue Center</strong>

                </td>
                <td >
                    <br />
                    <asp:DropDownList ID="ddlIC" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlIC_SelectedIndexChanged" Height="25px" >
                    </asp:DropDownList>
                    <br />
                    <br />
                </td>
            </tr>


        <tr>
                <td >
                    <strong>Godown</strong>

                </td>
                <td >
                    <br />
                    <asp:DropDownList ID="ddlgd" runat="server" Width="150" AutoPostBack="True" OnSelectedIndexChanged="ddlgd_SelectedIndexChanged" >
                    </asp:DropDownList>
                    <br />
                    <br />
                </td>
           
                <td >
                    <strong>Stack</strong>

                </td>
                <td >
                    <br />
                    <asp:DropDownList ID="ddlSK" runat="server" Width="150" AutoPostBack="True"  >
                    </asp:DropDownList>
                    <br />
                    <br />
                </td>
            </tr>
        </table>
         <asp:Panel ID="panel2" Visible="false" runat="server">

         <table align="center" border="1" style="border-spacing: 1px; text-align: center; width: 894px; font-size: small" id="table2" >
        
       <tr>
           
           <td colspan="4"><strong>Quality Inspection By Team (Rice)</strong></td>
           </tr>
             <tr>

           
            <td style="width: 31px">क्रम सं.</td>
            <td >अपवर्तन</td>
            <td style="width: 114px">अधिकतम सीमा (प्रतिशत)<br />
                <b>कामन</b></td>
            <td style="width: 114px">गुणवत्ता परिक्षण उपरांत परिणाम<br />
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
            <td colspan="4" style="height: 40px;">
               
                <asp:Button ID="btnQuilityTested" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Submit For Quility Inspection (Paddy)" Width="254px" CssClass="ButtonClass" OnClick="btnQuilityTested_Click" OnClientClick="return validate1();" Enabled="False" />
            
            
            </td>
        </tr>
             

             </table>
     </asp:Panel>

     <asp:Panel ID="panel3" Visible="false" runat="server">

    <table align="center" class="auto-styleNSC" id="table3" >
        <tr>
            
            <td >
                
                <asp:Button ID="btnNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnNew_Click" />
                </td>
            

                  <td >
                <asp:Button ID="btnPass" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Pass" Width="115px" CssClass="ButtonClass" OnClick="btnPass_Click" OnClientClick="return validate1();" Enabled="False" />

            </td>

            <td >
                <asp:Button ID="btnfail" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Fail" Width="115px" CssClass="ButtonClass" OnClick="btnfail_Click" Enabled="False" OnClientClick="return validate();" />
            </td>



        </tr>


    </table>
    </asp:Panel>










        <asp:Panel ID="panel4" Visible="false" runat="server">
    
    <table align="center" border="1" style="border-spacing: 1px; text-align: center; width: 894px; font-size: small" id="table4" >
        <tr>
                 <td colspan="4"><strong> Inspection By Team (WHEAT)</strong></td>     

        </tr>
             <tr>


            <td style="width: 31px">क्रम सं.</td>
            <td >अपवर्तन</td>
                 <td style="width: 114px">अधिकतम सीमा (प्रतिशत)<br />
                <b>कामन</b></td>
            
            <td style="width: 114px">गुणवत्ता परिक्षण उपरांत परिणाम<br />
                
            </td>

        </tr>
        <tr>
             <td style="width: 31px; height: 51px;">1.</td>
            <td style="text-align: left; height: 51px;"> Foreign Matter</td>
           <td style="width: 31px; height: 51px; text-align:center">0.75</td>


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
             <td style="text-align: left; height: 51px;"> Other Food Grain</td>
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
             <td style="text-align: left; height: 51px;"> Damaged Grains</td>
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
             <td style="text-align: left; height: 51px;"> Slightly Damaged, Discoloured Grains</td>
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
             <td style="text-align: left; height: 51px;"> Shrivilled & Immature Grains</td>
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
            <td colspan="4" style="height: 40px;">
               
                <asp:Button ID="buttqualityTestWheat" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Submit For Quility Inspection (Wheat)" Width="288px" CssClass="ButtonClass" OnClick="buttqualityTestWheat_Click" OnClientClick="return validate1();" Enabled="False" />
            
            
            </td>
        </tr>
            












    </table>
    </asp:Panel>


    <asp:Panel ID="panel5" Visible="false" runat="server">

      <table align="center" class="auto-styleNSC" id="table5" >
        <tr>
            
            <td >
                
                <asp:Button ID="buttnewWheat" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="buttnewWheat_Click" />
                </td>
            

                  <td >
                <asp:Button ID="buttpasswheat" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Pass" Width="115px" CssClass="ButtonClass" OnClick="buttpasswheat_Click" OnClientClick="return validate1();" Enabled="False" />

            </td>

            <td >
                <asp:Button ID="buttfailwheat" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Fail" Width="115px" CssClass="ButtonClass" OnClick="buttfailwheat_Click" Enabled="False" OnClientClick="return validate();" />
            </td>



        </tr>


    </table>
    </asp:Panel>
</asp:Content>

