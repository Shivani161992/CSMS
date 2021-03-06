<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="Print_PaymentFPS.aspx.cs" Inherits="IssueCenter_Print_PaymentFPS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Print Payment Received</title>
    
    <link rel="Stylesheet" href="../MyCss/Comon.css" type="text/css" />
    <link rel="stylesheet" href="../MyCss/menu.css" type="text/css" />

    <script language="javascript" type="text/javascript">
    
function CallPrint(strid)
{
 var prtContent = document.getElementById(strid);
 var WinPrint = window.open('','','letf=0,top=0,width=600px,height=800px,toolbar=0,scrollbars=0,status=0');
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
            <img src="../Images/print.jpg" alt="print" style="width: 47px; height: 34px" /><strong>&nbsp;</strong></a>
        <div id="printdiv">
        
<center>
    
	
	<table style="width: 680px">
	<tr>
	<td style="text-align: right">
        परिशिष्ट - आठ</td>
	
	</tr>
	
	<tr>
	<td style="text-align: left">
        प्रति</td>
	
	</tr>
	<tr>
	<td style="text-align: left">
        प्रदाय केंद्र प्रभारी,<asp:Label ID="lblissue" runat="server"></asp:Label></td>
	
	</tr>
	<tr>
	<td style="text-align: left">
        एम पी स्टेट सिविल सप्लाईज कारपोरेसन ली.</td>
	
	</tr>
	<tr>
	<td style="text-align: left">
        जिला
        <asp:Label ID="lbldistrict" runat="server"></asp:Label></td>
	
	</tr>
	<tr>
	<td>
	
	</td>
	
	</tr>
	<tr>
	<td style="text-align: left">
        विषय : - द्वार प्रदाय योजनान्तर्गत स्कंध प्रदाय हेतु डी. डी./ओन लाइन से फंड भुगतान
        के सम्बन्ध में |</td>
	
	</tr>
	<tr>
	<td style="text-align: left">
        उपरोक्त के सम्बन्ध में लेख है की माह
        <asp:Label ID="Label1" runat="server"></asp:Label>
        के आवंटन अंतर्गत स्कंध प्रदाय हेतु ,</td>
	
	</tr>
	<tr>
	<td>
	
	</td>
	
	</tr>
	
	<tr>
	<td style="text-align: left">
        डी डी नंबर
        <asp:Label ID="Label2" runat="server"></asp:Label>
        दिनांक
        <asp:Label ID="Label3" runat="server"></asp:Label>
        राशि
        <asp:Label ID="Label4" runat="server"></asp:Label>
        जो की
        <asp:Label ID="Label5" runat="server"></asp:Label></td>
	
	</tr>
	
	<tr>
	<td style="text-align: left">
        बैंक की शाखा पर देय है , संलग्न प्रस्तुत है |</td>
	
	</tr>
        <tr>
            <td style="text-align: center">
                या</td>
        </tr>
	
	<tr>
	<td style="text-align: left">
        हमारे
        <asp:Label ID="Label6" runat="server"></asp:Label>
        बैंक में संचालित खाता&nbsp; क्रमांक
        <asp:Label ID="Label7" runat="server"></asp:Label>
        से निगम के स्टेट बैंक आफ इंडिया
	
	</td>
	
	</tr>
	
	<tr>
	<td style="text-align: left">
        <asp:Label ID="Label8" runat="server"></asp:Label>
        शाखा के साख सीमा खाता क्रमांक
        <asp:Label ID="Label9" runat="server"></asp:Label>
        में राशि
        <asp:Label ID="Label10" runat="server"></asp:Label>
        ओन लाइन
	
	</td>
	
	</tr>
	
	<tr>
	<td style="text-align: left">
        फंड हस्तांतरण की गयी है | बैंक प्रमाण पत्र की मूल प्रति संलग्न प्रस्तुत है |</td>
	
	</tr>
        <tr>
            <td style="text-align: center">
                कृपया निम्न विवरण अनुसार द्वार प्रदाय योजनान्तर्गत स्कंध हमारी उ मु दू पर
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                पहुचाने का कष्ट करे -</td>
        </tr>
        <tr>
            <td style="text-align: left">
                लिंक समिति का नाम -
                <asp:Label ID="Label11" runat="server"></asp:Label></td>
        </tr>
	
	<tr>
	<td>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="670px"
                                                FooterStyle-ForeColor="green" ShowFooter="True" >
                                                <Columns>
                                                    
                                                    <asp:TemplateField HeaderText="fps_name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblfps" runat="server" Text='<%# Eval("fps_name").ToString()%>' Font-Size="10pt">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        
                                                        <HeaderStyle CssClass="gridtotsize"  Font-Size="10pt"/>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                   
                                                    
                                                    <asp:BoundField DataField="commodity_name" HeaderText="Discription of Food grains/Sugar/Commidity"
                                                        ReadOnly="True" SortExpression="commodity_name">
                                                        <HeaderStyle CssClass="gridheader" Font-Size="10pt" />
                                                  
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="scheme_name" HeaderText="Sheme" ReadOnly="True" SortExpression="scheme_name">
                                                        <HeaderStyle CssClass="gridtotsize" Font-Size="10pt"/>
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Quntity  to be Issued (in Qtls)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQty" runat="server" Text='<%# Eval("quantity").ToString()%>' Font-Size="10pt">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                           <%-- <asp:Label ID="lblTotalQty" runat="server" Font-Size="10pt"></asp:Label>--%>
                                                        
                                                        </FooterTemplate>
                                                        <HeaderStyle CssClass="gridtotsize"  Font-Size="10pt"/>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="rate_per_qtls" HeaderText="Rate per Quantal(Rs.)" ReadOnly="True"
                                                        SortExpression="rate_per_qtls">
                                                        <HeaderStyle CssClass="gridtotsize" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Amount(Rs.)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Totalamt").ToString()%>' Font-Size="10pt">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                           <%-- <asp:Label ID="lblTotal" runat="server" Font-Size="10pt"></asp:Label>--%>
                                                        </FooterTemplate>
                                                        <HeaderStyle CssClass="gridtotsize" Font-Size="10pt"/>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    
                                                </Columns>
                                                <FooterStyle ForeColor="Green" />
                                            </asp:GridView>
	</td>
	
	</tr>
        <tr>
            <td style="text-align: left">
                संलग्न : उपरोक्तानुसार</td>
        </tr>
        <tr>
            <td style="text-align: left">
                दिनांक : &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; हस्ताक्षर एवं मुद्रा</td>
        </tr>
        <tr>
            <td style="text-align: right">
                लिंक समिति&nbsp; / उ मु&nbsp; दू&nbsp; (अधिकृत&nbsp; हस्ताक्षर)</td>
        </tr>
	
	
	
	</table>

  </center>
 
 </div>
   
    </form>
</body>
 </html>
