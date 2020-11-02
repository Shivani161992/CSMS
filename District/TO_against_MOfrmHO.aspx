<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="TO_against_MOfrmHO.aspx.cs" Inherits="District_TO_against_MOfrmHO" Title="TO against MO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript">
      function CheckIsNumeric(tx) {
          var AsciiCode = event.keyCode;
          var txt = tx.value;
          var txt2 = String.fromCharCode(AsciiCode);
          var txt3 = txt2 * 1;
          if ((AsciiCode < 46) || (AsciiCode > 57)) {
              alert('Please enter only numbers.');
              event.cancelBubble = true;
              event.returnValue = false;
          }

          var num = tx.value;
          var len = num.length;
          var indx = -1;
          indx = num.indexOf('.');
          if (indx != -1) 
          {
              var dgt = num.substr(indx, len);
              var count = dgt.length;
              //alert (count);
              if (count > 5)
               {
                  alert("Only 5 decimal digits allowed");
                  event.cancelBubble = true;
                  event.returnValue = false;
              }
          }
          else if (keycode == 46)
           {
              if (num.split(".").length > 1) {
                  alert('दशमलव एक ही बार आ सकता है |');
                  return false;
              }
          }

      }



    </script>
   
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

<center>

    <table border-color: #000000" style="width: 700px">
        <tr>
            <td colspan="2" style="color: #000099">
                <b>Issue Transport Order Against Movement Order (By Road)</b></td>
        </tr>
        <tr>
            <td style="width: 390px">
                Select Movement Order</td>
            <td>
                <asp:DropDownList ID="ddlmo" runat="server" Height="25px" Width="150px" 
                    AutoPostBack="True" onselectedindexchanged="ddlmo_SelectedIndexChanged">
                </asp:DropDownList>
                                                    </td>
        </tr>
        <tr>
            <td style="width: 390px">
                Select Receiving District(प्राप्ति जिला)</td>
            <td>
                <asp:DropDownList ID="ddlrecdist" runat="server" Height="25px" Width="150px" 
                    Enabled="False">
                </asp:DropDownList>
                                                    </td>
        </tr>
        <tr>
            <td style="width: 390px">
                Receiving Issue Center(प्राप्ति केंद्र)</td>
            <td>
                <asp:DropDownList ID="ddlrIC" runat="server" Height="25px" Width="150px">
                </asp:DropDownList>
                                                    </td>
        </tr>
        <tr>
            <td style="width: 390px">
                Receiving Branch(प्राप्ति ब्रांच)</td>
            <td>
                <asp:DropDownList ID="ddlrbranch" runat="server" Height="25px" Width="150px" AutoPostBack="True" 
                    onselectedindexchanged="ddlrbranch_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 390px">
                Receiving Godown(प्राप्ति गोदाम)</td>
            <td>
                <asp:DropDownList ID="ddlrgodown" runat="server" Height="25px" Width="150px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 390px">
                Select Commodity</td>
            <td>
                <asp:DropDownList ID="ddlcommodity" runat="server" Height="25px" Width="150px" 
                    Enabled="False">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 390px">
                Select Crop Year</td>
            <td>
                <asp:DropDownList ID="ddlcropyear" runat="server" Height="25px" Width="150px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 390px">
                Select Transporter Name</td>
            <td>
                <asp:DropDownList ID="ddltransporter" runat="server" Height="25px" 
                    Width="150px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 390px">
                End Date of Transportation</td>
            <td>
                <asp:TextBox ID="Recdate" runat="server"></asp:TextBox>
                
                <script type  ="text/javascript">
                    new tcal({
                        'formname': 'aspnetForm',
                        'controlname': 'ctl00_ContentPlaceHolder1_Recdate'
                    });

	          </script>
                
            </td>
        </tr>
        <tr>
            <td style="width: 390px">
                Quantity to be dispatch (in Qntls)</td>
            <td>
                <asp:TextBox ID="txtqty" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="background-color: #FFFF99;" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 390px; height: 21px;">
                Select Issue Center (जहा से भेजा जाना है)</td>
            <td style="height: 21px">
                <asp:DropDownList ID="ddlissuecenter" runat="server" Height="25px" 
                    Width="150px" OnSelectedIndexChanged="ddlissuecenter_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 390px">
                Select Branch (जहा से भेजा जाना है)</td>
            <td>
                <asp:DropDownList ID="ddlbranch" runat="server" Height="25px" Width="150px" 
                    AutoPostBack="True" onselectedindexchanged="ddlbranch_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 390px">
                Select Godown (जहा से भेजा जाना है)</td>
            <td>
                <asp:DropDownList ID="ddlgodown" runat="server" Height="25px" Width="150px" 
                    onselectedindexchanged="ddlgodown_SelectedIndexChanged" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 390px">
                Availble Stock of Commodity in Godown<asp:Label ID="lblmaxcap" runat="server" 
                    Visible="False"></asp:Label></td>
            <td>
                <asp:Label ID="lblstockbal" runat="server"></asp:Label>
                <asp:Label ID="lblcurr" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 390px">
                <asp:Label ID="lbltdate" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblTQty" runat="server" Visible="False"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
            <center>
                <asp:Button ID="btnadd" runat="server" Text="Add This" onclick="btnadd_Click" />
                </center>
            </td>
        </tr>
        <tr>
            <td colspan="2">
               <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                BackColor="#CCCCCC" BorderColor="#999999"  BorderStyle="Solid" BorderWidth="3px" 
                CellPadding="4" Font-Bold="False"
                 Font-Size="Small" CellSpacing="2" ForeColor="Black" DataKeyNames="commodityid,godownid,transp,isscen,branchid,RIC,RGdn" >
                
                <RowStyle BackColor="White" />
                                        <Columns>
                                        
                                        <asp:BoundField DataField="MO" HeaderText="Movement Order" 
                                                SortExpression="MO">
                                          <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                        
                                        <asp:BoundField DataField="Godown" HeaderText="Godown Name" 
                                                SortExpression="commodityid">
                                            <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                                                                
                                        <asp:BoundField DataField="distname" HeaderText="To District" 
                                                SortExpression="distname">
                                          <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                            
                                        <asp:BoundField DataField="cropyear" HeaderText="Crop Year" SortExpression="cropyear">
                                            <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                            
                                        <asp:BoundField DataField="commodity" HeaderText="commodity" >
                                            <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                        <asp:BoundField DataField="transname" HeaderText="Transporter" >
                                            
                                            <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                            
                                        <asp:BoundField DataField="qty" HeaderText="Quantity" >
                                            
                                            <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                            
                                            
                                            <asp:BoundField DataField="commodityid" HeaderText="commodityid"  Visible="False" />
                                            <asp:BoundField DataField="godownid" HeaderText="godownid" Visible="False" />
                                            <asp:BoundField DataField="transp" HeaderText="transp"  Visible="False" />
                                            <asp:BoundField DataField="isscen" HeaderText="isscen"  Visible="False" />
                                            <asp:BoundField DataField="branchid" HeaderText="branchid"  Visible="False" />
                                            <asp:BoundField DataField="todisid" HeaderText="todisid"  Visible="False" />
                                            
                                             <asp:BoundField DataField="RIC" HeaderText="RIC"  Visible="False" />
                                             
                                              <asp:BoundField DataField="RGdn" HeaderText="RGdn"  Visible="False" />
                                          
                                            
                                        
                                        </Columns>
                
                
                 <FooterStyle BackColor="#CCCCCC" />
                                        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="Black" Font-Bold="True" Font-Size="Small" 
                                            ForeColor="White" />
                                     
                                    </asp:GridView>
                
                
            </td>
        </tr>
        <tr>
            <td style="width: 390px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 390px">
                <asp:Button ID="btnNew" runat="server" Text="New" Width="75px" 
                    onclick="btnNew_Click" />
            </td>
            <td>
                <asp:Button ID="btnsave" runat="server" Text="Save" Width="75px" 
                    Enabled="False" onclick="btnsave_Click" />
            </td>
        </tr>
        <tr>
            <td style="width: 390px">
                &nbsp;</td>
            <td>
          <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="#" Visible="False" Width="139px">Print Receipt Detail</asp:HyperLink>
            </td>
        </tr>
    </table>

</center>
</asp:Content>

