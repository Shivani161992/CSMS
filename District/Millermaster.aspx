<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Millermaster.aspx.cs" Inherits="District_Miller_agreement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


  <script type="text/javascript">
      function CheckCalDate(tx) {
          var AsciiCode = event.keyCode;
          var txt = tx.value;
          var txt2 = String.fromCharCode(AsciiCode);
          var txt3 = txt2 * 1;
          if ((AsciiCode > 0)) {
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
    <div style="text-align: center">
        <table style="width: 650px">
            <tr>
                <td colspan="4" style="height: 21px; background-color: #C0C0C0">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" 
                        Text="Miller Agreement Master" Font-Italic="False" Font-Size="Large"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="4" 
                    style="height: 2px; background-color: #b0cff2; font-family: georgia; font-size: small;">
                    <table style="width: 759px; height: 1px" border="1" cellpadding="1">
                        <tr>
                            <td align="left" style="width: 171px; height: 24px;">
                                <asp:Label ID="Label5" runat="server" Font-Bold="True" Text="Crop Year :" 
                                    Width="120px"></asp:Label></td>
                            <td align="left" style="width: 98px; height: 24px;">
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
                            <td colspan="3">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 171px" align="left">
                                <asp:Label ID="lbl_contracted" runat="server" Text="Contracted Millers"></asp:Label>
                            </td>
                            <td align="left" colspan="2">
                                <asp:DropDownList ID="ddl_contracted" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddl_contracted_SelectedIndexChanged" Width="250px">
                                </asp:DropDownList>
                           </td>
                            <td align="left" colspan="2">
                                <asp:Button ID="btn_add" runat="server" onclick="btn_add_Click" 
                                    Text="Add New Miller" />
                           </td>
                        </tr>
                    
                       
                        <tr>
                            <td style="width: 171px" align="left">
                                <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="Miller Name:" 
                                    Width="133px"></asp:Label></td>
                            <td align="left" colspan="4">
                                <asp:TextBox ID="txt_millername" runat="server" Width="400px"></asp:TextBox>
                           </td>
                        </tr>
                    
                       
                        <tr>
                            <td style="width: 171px; height: 24px" align="left">
                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Mobile/Office No :"
                                    Width="129px"></asp:Label></td>
                            <td align="left" style="width: 98px; height: 24px">
                                <asp:TextBox ID="txt_mobileno" runat="server" MaxLength="12" Width="154px"></asp:TextBox>
                                </td>
                            <td align="left" style="height: 24px" colspan="3">
                              </td>
                        </tr>
                       
                       
                        <tr>
                            <td style="width: 171px; height: 24px" align="left">
                                <strong>Address</strong></td>
                            <td align="left" style="height: 24px" colspan="4">
            <asp:TextBox ID="txtbadds" runat="server" Width="400px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                        </tr>
                       
                        <tr>
                            <td style="height: 24px; text-align: center; background-color: #66ccff;" 
                                align="left" colspan="5">
                                <asp:Label ID="Label8" runat="server" Font-Bold="True" 
                                    Text="Contracted Milling quantity"></asp:Label></td>
                        </tr>
                        
                        <tr>
                            <td style="width: 171px; height: 24px;" align="left">
                                <asp:Label ID="Label21" runat="server" style="font-weight: 700" 
                                    Text="Paddy Common"></asp:Label>
                                (in Qtls)</td>
                            <td align="left" >
                                <asp:TextBox ID="txt_paddycommon" runat="server" ></asp:TextBox>
                                </td>
                            <td align="left" style="height: 24px;" colspan="2">
                                <asp:Label ID="Label9" runat="server" Font-Bold="True" Text="Paddy Grade A" 
                                    Width="113px"></asp:Label>(in Qtls)</td>
                            <td align="left" style="width: 98px; height: 24px;">
                                <asp:TextBox ID="txt_paddygradeA" runat="server"></asp:TextBox>
                                </td>
                        </tr>
                        
                        <tr>
                            <td style="height: 24px;" align="left" colspan="5">
                                &nbsp;</td>
                        </tr>
                        
                        <tr>
                            <td style="width: 171px; height: 21px;" align="left">
                                <asp:Label ID="Label10" runat="server" Font-Bold="True" Text="Milling Rate" 
                                    Width="87px"></asp:Label>(Per Qtl)</td>
                            <td align="left" style="width: 98px; height: 21px;">
                                <asp:TextBox ID="txt_millingrate" runat="server" MaxLength="6"></asp:TextBox>
                               
                               
                            
                                </td>
                            <td align="left" style="height: 21px" colspan="2">
                                <asp:Label ID="Label22" runat="server" style="font-weight: 700" 
                                    Text="Milling Capacity"></asp:Label>
                                <strong>Per day</strong></td>
                            <td align="left" style="width: 98px; height: 21px">
                             
                                <asp:TextBox ID="txt_millingcapacity" runat="server"></asp:TextBox>
                             
                                in Qtls</td>
                        </tr>
                        <tr>
                            <td style="width: 171px; height: 21px" align="left">
                                <asp:Label ID="Label14" runat="server" Font-Bold="True" 
                                    Text="Milling Date from" Width="127px"></asp:Label></td>
                            <td align="left" style="width: 98px; height: 21px">
                                <asp:TextBox ID="txtnfromdate" runat="server"></asp:TextBox>
                                 <script type  ="text/javascript">
                                     new tcal({
                                         'formname': '0',
                                         'controlname': 'ctl00_ContentPlaceHolder1_txtnfromdate'
                                     });
	                          </script>
                                
                                </td>
                            <td align="left" style="height: 21px" colspan="2">
                                <asp:Label ID="Label18" runat="server" Font-Bold="True" Text="Milling to Date" 
                                    Width="129px"></asp:Label></td>
                            <td align="left" style="width: 98px; height: 21px">
                                <asp:TextBox ID="txtntodate" runat="server"></asp:TextBox>
                                 <script type  ="text/javascript">
                                     new tcal({
                                         'formname': '0',
                                         'controlname': 'ctl00_ContentPlaceHolder1_txtntodate'
                                     });
	     </script>
                                
                                
                                
                                </td>
                        </tr>
                       
                     
                    
                       
                       
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="4" rowspan="1" style="height: 9px; background-color: #66ccff">
                    &nbsp;</td>
            </tr>
            <tr>
               
                <td style="width: 100px; background-color: #C0C0C0; height: 26px;" 
                    align="right">
                    <asp:Button ID="btnadd" runat="server" Text="Save" OnClick="btnadd_Click" Width="58px" ValidationGroup="1" />
                </td>
               
                <td style="width: 89px; background-color: #C0C0C0; height: 26px;" align="right">
            <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="53px" OnClick="btnUpdate_Click" Visible="False" /></td>
                <td align="right" style="height: 26px; background-color: #C0C0C0" colspan="2">
            <asp:Button ID="btnclose" runat="server" Text="Close" Width="65px" OnClick="btnclose_Click" /></td>
                
                
              
                
            </tr>
            
            <tr>
                <td colspan="4" style="background-color: #66ccff">
                    &nbsp;</td>
            </tr>
            
            <tr>
                <td colspan="4"><center>
                    <div style="width: 100%; height: 300px; overflow: scroll">
    <asp:GridView ID="Grd_milleragreement" runat="server" AutoGenerateColumns="False" 
                            OnSelectedIndexChanged="GridView1_SelectedIndexChanged" 
                            OnRowDataBound="GridView1_RowDataBound"  AllowPaging="True" PagerSettings-Visible  ="True" 
                            OnPageIndexChanging="GridView1_PageIndexChanging" BackColor="White" 
                            BorderColor="#CCCCCC" BorderWidth="1px" CellPadding="4" ForeColor="Black" 
                            GridLines="Horizontal" Width="700px" BorderStyle="None" Font-Size="12px" 
                            EnableModelValidation="True"  >
        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
        <Columns>
            <asp:CommandField HeaderText="Action" ShowSelectButton="True">
                <HeaderStyle Font-Size="12px" />
                <ItemStyle Font-Size="11px" />
            </asp:CommandField>
            <asp:BoundField DataField="Miller_ID" HeaderText="Miller ID" SortExpression="Miller_ID">
                <ItemStyle Font-Size="11px" />
                <HeaderStyle Font-Size="12px" />
            </asp:BoundField>
            <asp:BoundField DataField="Miller_Name" HeaderText="Miller Name" SortExpression="Miller_Name">
                <ItemStyle Font-Size="11px" />
                <HeaderStyle Font-Size="12px" />
            </asp:BoundField>
            <asp:BoundField DataField="contactno" HeaderText="Contact No." />
            <asp:BoundField DataField="millerAddress" HeaderText="Address" />
            <asp:BoundField DataField="crop_year" HeaderText="Crop year" />
            <asp:BoundField DataField="milling_quantity_common" 
                HeaderText="Milling quantity(Paddy Commen)" Visible="False" />
            <asp:BoundField DataField="milling_quantity_gradeA" 
                HeaderText="Milling quantity(Paddy Grade A)" Visible="False" />
            <asp:BoundField DataField="milling_rate" HeaderText="Milling rate(per day)" />
            <asp:BoundField DataField="milling_capacity" HeaderText="Milling Capacity" />
            <asp:BoundField DataField="milling_from" HeaderText="Milling from" DataFormatString="{0:dd-MM-yyyy}"  />
            <asp:BoundField DataField="milling_to" HeaderText="Milling To" DataFormatString="{0:dd-MM-yyyy}"  />
        </Columns>
        <SelectedRowStyle BackColor="#CC3333" ForeColor="White" Font-Bold="True" />
        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
     
    </asp:GridView></center>
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
                <td>
                </td>
                <td style="width: 100px">
                </td>
            </tr>
         
        </table>
    </div>
</asp:Content>

