<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print_Truckchalan_FPS.aspx.cs"
    Inherits="IssueCenter_Print_Truckchalan_FPS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Print Truck Challan</title>

    <script language="javascript" type="text/javascript">
    function CallPrint(strid)
{
 var prtContent = document.getElementById(strid);
 var WinPrint = window.open('','','left=150,top=0,width=700,height=700,toolbar=0,scrollbars=0,status=0');
 WinPrint.document.write(prtContent.innerHTML);
 WinPrint.document.close();		
 WinPrint.focus();
 WinPrint.print();
 WinPrint.close();
 prtContent.innerHTML=strOldOne;
}
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <a style="cursor: pointer" onclick="javascript:CallPrint('printdiv')">
            <img alt="print" src="../Images/print.jpg" /><strong>&nbsp;</strong></a>
        <center>
            <div id="printdiv" style="margin-left:100px">
                <table cellpadding="0" cellspacing="0" border="0" width="600px">
                    <tr>
                        <td colspan="3" align="center">
                            <asp:Label ID="titlempscsc" runat="server" Text="एम.पी.स्टेट सिविल सप्लाईज कर्पोरेशन लिमीटेड."
                                ForeColor="Maroon" Font-Size="25px" Font-Names="Aparajita" Width="500px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="center" style="font-size:10pt">
                            जिला कार्यालय -
                        
                                <asp:Label ID="lbldist" runat="server" Text="" Font-Bold ="true" Font-Size="10pt"></asp:Label>
                           </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="height: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px" align="center">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/main1_03.jpg" Width="60px" Height="50px" />
                        </td>
                        <td align="center" valign="middle" style="width: 200px;">
                            <fieldset>
                                <table style="height: 25px; width: 150px; border-color: Black">
                                    <tr>
                                        <td align="center" valign="middle" style="font-size:10pt">
                                            <strong>ट्रक चालान मेमों</strong>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td style="width: 200px;font-size:10pt" align="right" valign="bottom">
                            बुक क्रमांक : <asp:Label ID="lbl_Book_no" runat="server" Text="" Font-Bold ="true"></asp:Label>
                            <br />
                     
                             दिनांक : <asp:Label ID="lbl_date2" runat="server" Text="" Font-Bold ="true"></asp:Label>&nbsp;&nbsp;
                             
                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="height: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="bottom" style="font-size:10pt">
                            संग्रहण केंद्र / रैक पॉइंट
                        </td>
                        <td>
                        </td>
                        <td align="right" valign="bottom" style="font-size:10pt">
                            सरल क्रमांक : <asp:Label ID="lbl_order_no" runat="server" Text="" Font-Bold ="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <table cellpadding="0" cellspacing="0" border="1px" style="border-color: Gray" width="100%"
                                frame="box">
                                <tr style="height: 25px">
                                    <td align="Center" valign="middle" style="width: 7%;font-size:10pt">
                                        क्र.
                                    </td>
                                    <td align="Center" style="width: 93%;font-size:10pt">
                                        (अ) स्कंध प्रेषण का विवरण
                                    </td>
                                </tr>
                                <tr style="height: 25px">
                                    <td align="Center" valign="middle" style="font-size:10pt">
                                        (1)
                                    </td>
                                    <td align="left" style="width: 200px;font-size:10pt">
                                        प्रेषणकर्ता खरीदी डीपो / बेस डीपो - <asp:Label ID="lbl_Depot" runat="server" Text="" Font-Bold ="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 25px">
                                    <td align="Center" valign="middle" style="font-size:10pt">
                                        (2)
                                    </td>
                                    <td align="left" style="font-size:10pt">
                                        परिवाहनकर्ता का नाम - <asp:Label ID="lbl_Transporter" runat="server" Text="" Font-Bold ="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 25px">
                                    <td align="Center" valign="middle" style="font-size:10pt">
                                        (3)
                                    </td>
                                    <td align="left" style="font-size:10pt">
                                        बिल्टी क्रमांक - .....<asp:Label ID="lbl_builtyno" runat="server" Text="" Font-Bold ="true"></asp:Label>..... &nbsp;&nbsp;&nbsp;&nbsp;
                                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; दिनांक
                                        - ...<asp:Label ID="lbl_builtydate" runat="server" Text="" Font-Bold ="true"></asp:Label>...
                                    </td>
                                </tr>
                                <tr style="height: 25px">
                                    <td align="Center" valign="middle" style="font-size:10pt">
                                        (4)
                                    </td>
                                    <td align="left" style="font-size:10pt">
                                        ट्रक क्रमांक - <asp:Label ID="lbl_Truckno" runat="server" Text="" Font-Bold ="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 25px">
                                    <td align="Center" valign="middle" style="font-size:10pt">
                                        (5)
                                    </td>
                                    <td align="left" style="font-size:10pt">
                                        ड्राईवर का नाम - <asp:Label ID="lbl_DriverName" runat="server" Text="" Font-Bold ="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 25px">
                                    <td align="Center" valign="middle" style="font-size:10pt">
                                        (6)
                                    </td>
                                    <td align="left" style="font-size:10pt">
                                        स्कंध लोड करने की दिनांक - ..........<asp:Label ID="lbl_LoadDate" runat="server" Text="" Font-Bold ="true"></asp:Label>............. &nbsp; समय -
                                        ....<asp:Label ID="lbl_LoadTime" runat="server" Text="" Font-Bold ="true"></asp:Label>....बजे
                                    </td>
                                </tr>
                                <tr>
                                    <td align="Center" valign="middle" colspan="2" style="font-size:8pt">
                                        <asp:GridView ID="gv_fps_Details" runat="server" AutoGenerateColumns="False" Width="600px"
                                            FooterStyle-ForeColor="green" ShowFooter="True" OnRowDataBound="gv_fps_Details_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="fps_name" HeaderText="गंतव्य उ.मूल्य.दुकान का विवरण" ReadOnly="True"
                                                    SortExpression="fps_name" FooterText="Total">
                                                    <HeaderStyle CssClass="gridheader" Font-Size="8pt" Width="200px" />
                                                    <ItemStyle  Font-Size="8pt"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Commodity_name" HeaderText="स्कंध का नाम/श्रेणी/किस्म" ItemStyle-HorizontalAlign="Center"
                                                    ReadOnly="True" SortExpression="commodity_name">
                                                    <HeaderStyle CssClass="gridheader"  Font-Size="8pt" Width="150px" />
                                                    <ItemStyle  Font-Size="8pt"  />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Scheme_name" HeaderText="स्किम" ReadOnly="True" SortExpression="scheme_name">
                                                    <HeaderStyle CssClass="gridtotsize" Font-Size="8pt" Width="50px" />
                                                    <ItemStyle  Font-Size="8pt"/>
                                                </asp:BoundField>
                                                 <asp:TemplateField HeaderText="लोड की गई मात्रा (बोरी मे)" >
                                                    <ItemTemplate >
                                                        <asp:Label ID="lblAmount" Font-Size="8pt" runat="server" Text='<%# Eval("Bags").ToString()%>'>
                                                        </asp:Label>
                                                    </ItemTemplate >
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotal" runat="server" Font-Size="8pt"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle CssClass="gridtotsize" Font-Size="8pt" Width="100px"/>
                                                    <ItemStyle  Font-Size="8pt"/>
                                                </asp:TemplateField>
                                                <%-- <asp:BoundField DataField="Bags" HeaderText="लोड की गई मात्रा (बोरी मे)" ReadOnly="True"
                                                    SortExpression="rate_per_qtls">
                                                    <HeaderStyle CssClass="gridtotsize" />
                                                </asp:BoundField>--%>
                                                <asp:TemplateField HeaderText="शुद्ध वजन (Qtls में)" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQty" Font-Size="8pt" runat="server" Text='<%# Eval("Qty_send").ToString()%>'>
                                                        </asp:Label>
                                                    </ItemTemplate >
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotalQty" runat="server" Font-Size="8pt"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle CssClass="gridtotsize" Font-Size="8pt" Width="80px" />
                                                    <ItemStyle  Font-Size="8pt"/>
                                                </asp:TemplateField>
                                               
                                                 
                                            </Columns>
                                            <FooterStyle Font-Bold="True" ForeColor="Green" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr style="height: 100px">
                                    <td align="Center" valign="middle" colspan="2" style="font-size:10pt">
                                        <table width="100%">
                                            <tr>
                                                <td align="Center" valign="middle" style="font-size:10pt" >
                                                    ट्रक ड्राईवर / परिवाहनकर्ता<br />
                                                    &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; के हस्ताक्षर
                                                    <br />
                                                    नाम...........................................
                                                </td>
                                                <td align="Center" valign="middle" style="font-size:10pt">
                                                    हस्ताक्षर प्रेषणकर्ता
                                                    <br />
                                                    नाम...........................................
                                                    <br />
                                                    सील...........................................
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr style="height: 25px">
                                    <td align="Center" valign="middle" style="width: 2%;font-size:10pt">
                                        क्र.
                                    </td>
                                    <td align="Center" style="width: 98%;font-size:10pt">
                                        (ब) स्कंध प्राप्ति का विवरण
                                    </td>
                                </tr>
                                <tr style="height: 25px">
                                    <td align="Center" valign="middle" style="font-size:10pt">
                                        (1)
                                    </td>
                                    <td align="left" style="width: 200px;font-size:10pt">
                                        प्राप्तकर्ता सग्रहण केंद्र / रैक पॉइंट -
                                    </td>
                                </tr>
                                <tr style="height: 25px">
                                    <td align="Center" valign="middle" style="font-size:10pt">
                                        (2)
                                    </td>
                                    <td align="left" style="font-size:10pt">
                                        स्कंध प्राप्ति की दिनांक - ........................................ &nbsp; समय -
                                        ............................बजे
                                    </td>
                                </tr>
                                <tr style="height: 25px">
                                    <td align="Center" valign="middle" style="font-size:10pt">
                                        (3)
                                    </td>
                                    <td align="left" style="font-size:10pt">
                                        ट्रक क्रमांक -
                                    </td>
                                </tr>
                                <tr style="height: 25px">
                                    <td align="Center" valign="middle" style="font-size:10pt"> 
                                        (4)
                                    </td>
                                    <td align="left" style="font-size:10pt;font-size:10pt">
                                        ड्राईवर का नाम -
                                    </td>
                                </tr>
                                <tr style="height: 25px">
                                    <td align="Center" valign="middle" style="font-size:10pt">
                                        (5)
                                    </td>
                                    <td align="left" style="font-size:10pt">
                                        प्राप्त स्कंध (बोरी में) - ................................................................. &nbsp; वजन -
                                        ............................Qtls में
                                    </td>
                                </tr>
                                <tr style="height: 25px">
                                    <td align="Center" valign="middle" style="font-size:10pt">
                                        (6)
                                    </td>
                                    <td align="left" style="font-size:10pt">
                                        परिवहन कमी(बोरी में) - ............................................................... &nbsp; वजन - ............................Qtls
                                        में
                                    </td>
                                </tr>
                                <tr style="height: 25px">
                                    <td align="Center" valign="middle" style="font-size:10pt"> 
                                        (7)
                                    </td>
                                    <td align="left" style="font-size:10pt">
                                        अमान्य किया गया स्कंध(बोरी में) -............................................ &nbsp; वजन - ............................Qtls
                                        में
                                    </td>
                                </tr>
                                <tr style="height: 100px">
                                    <td align="Center" valign="middle" colspan="2">
                                        <table width="100%">
                                            <tr>
                                                <td align="center" valign="middle" style="font-size:10pt">
                                                    ट्रक ड्राईवर / परिवाहनकर्ता<br />
                                                    &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; के हस्ताक्षर
                                                    <br />
                                                    नाम...........................................
                                                </td>
                                                <td align="Center" valign="middle" style="font-size:10pt">
                                                    हस्ताक्षर प्रेषणकर्ता
                                                    <br />
                                                    नाम...........................................
                                                    <br />
                                                    सील...........................................
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </center>
    </form>
</body>
</html>
