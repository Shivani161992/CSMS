<%@ Page Language="C#" AutoEventWireup="true" CodeFile="printRpt_N-2_leadwise_alloc.aspx.cs" Inherits="printRpt_N_2_leadwise_alloc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Report N_2 Allocation Lead-Wise</title>
    <script language="javascript"  type="text/javascript">
    
function CallPrint(strid)
{
 var prtContent = document.getElementById(strid);
 var WinPrint = window.open('','','letf=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0');
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
  
    <a style="cursor:pointer"  onclick ="javascript:CallPrint('print_alloc')"> <img src="Images/Printer-50x50.png"  alt ="print" style="width: 48px; height: 24px" /></a><div id ="print_alloc">
    <div  style="width: 7in; height: 9.69in; text-align: center; border-top-width: 1pt; border-left-width: 1pt; border-left-color: black; border-bottom-width: 1pt; border-bottom-color: black; border-top-color: black; border-right-width: 1pt; border-right-color: black;">
    <table style="color: black; background-color: white; border-right: black 1pt solid; border-top: black 1pt solid; border-left: black 1pt solid; border-bottom: black 1pt solid;" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid">
                    <strong style="font-size: 14pt">N-2 Allocation Report&nbsp; District -
                        <asp:Label ID="lbl_dist" runat="server" Font-Bold="True"></asp:Label></strong></td>
            </tr>
            <tr>
                <td align="center" style="border-top-width: 1px; border-left-width: 1px; border-left-color: black; border-bottom-width: 1px; border-bottom-color: black; border-top-color: black; border-right-width: 1px; border-right-color: black;">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left" style="width: 70px; background-color: white; font-size: 10pt; color: black; border-top-width: 1pt; border-left-width: 1pt; border-left-color: gray; border-bottom-width: 1pt; border-bottom-color: gray; border-top-color: gray; border-right-width: 1pt; border-right-color: gray;">
                    Month</td>
                            <td align="left" colspan="2" style="font-size: 10pt; color: black; background-color: white; width: 70px; border-top-width: 1pt; border-left-width: 1pt; border-left-color: gray; border-bottom-width: 1pt; border-bottom-color: gray; border-top-color: gray; border-right-width: 1pt; border-right-color: gray;">
                                <asp:Label ID="lbl_month" runat="server" Font-Bold="True"></asp:Label></td>
                            <td align="center" style="width: 47px; background-color: white; font-size: 10pt; color: black; border-top-width: 1pt; border-left-width: 1pt; border-left-color: gray; border-bottom-width: 1pt; border-bottom-color: gray; border-top-color: gray; border-right-width: 1pt; border-right-color: gray;">
                    Year</td>
                            <td align="left" style="width: 43px; background-color: white; font-size: 10pt; color: black; border-top-width: 1pt; border-left-width: 1pt; border-left-color: gray; border-bottom-width: 1pt; border-bottom-color: gray; border-top-color: gray; border-right-width: 1pt; border-right-color: gray;">
                                <asp:Label ID="lbl_year" runat="server" Font-Bold="True"></asp:Label></td>
                            <td align="right" style="background-color: white; font-size: 10pt; color: black; width: 70px; border-top-width: 1pt; border-left-width: 1pt; border-left-color: gray; border-bottom-width: 1pt; border-bottom-color: gray; border-top-color: gray; border-right-width: 1pt; border-right-color: gray;" colspan="2">
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 70px; background-color: white; font-size: 10pt; color: black; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                Lead Society</td>
                            <td align="left" colspan="6" style="background-color: white; font-size: 10pt; color: black; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:Label ID="lbl_lead" runat="server" Font-Bold="True" Font-Names="Kruti Dev 010" Font-Size="Medium"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 70px; background-color: white; font-size: 10pt; color: black; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid; text-align: left;">
                                Commodity</td>
                            <td align="center" style="width: 57px; background-color: white; font-size: 10pt; color: black; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid; text-align: center;">
                                Allotment (Qtls.)<br />
                                (1)</td>
                            <td align="center" style="font-size: 10pt; background-color: white; color: black; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid; text-align: center;">
                                Opening Balance<br />
                                (2)</td>
                            <td align="center" style="font-size: 10pt; width: 57px; background-color: white; color: black; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid; text-align: center;">
                                Received Quantity<br />
                                (3)</td>
                            <td align="left" style="font-size: 10pt; width: 57px; background-color: white; color: black; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid; text-align: center;">
                                Distributed Quantity<br />
                                (4)</td>
                            <td align="center" style="font-size: 10pt; background-color: white; color: black; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid; text-align: center;">
                                Stock<br />
                                (2+3-4)<br />
                                (5)</td>
                            <td align="center" style="background-color: white; font-size: 10pt; color: black; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid; text-align: center;">
                                Total<br />
                                Balance<br />
                                (1-5)</td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 70px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                Rice APL</td>
                            <td align="left" style="width: 57px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="rice_apl_allot" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 16px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="rice_apl_ope" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 47px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="rice_apl_rec" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 43px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="rice_apl_distr" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 26px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="rice_apl_st" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 40px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="rice_apl_bal" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 70px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                Rice BPL</td>
                            <td align="left" style="width: 57px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="rice_bpl_allot" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 16px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="rice_bpl_ope" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 47px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="rice_bpl_rec" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 43px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="rice_bpl_distr" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 26px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="rice_bpl_st" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 40px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="rice_bpl_bal" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 70px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                Rice AAY</td>
                            <td align="left" style="width: 57px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="rice_aay_allot" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 16px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="rice_aay_ope" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 47px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="rice_aay_rec" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 43px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="rice_aay_distr" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 26px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="rice_aay_st" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 40px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="rice_aay_bal" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 70px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                Wheat APL</td>
                            <td align="left" style="width: 57px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="wheat_apl_allot" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 16px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="wheat_apl_ope" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 47px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="wheat_apl_rec" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 43px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="wheat_apl_distr" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 26px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="wheat_apl_st" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 40px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="wheat_apl_bal" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 70px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                Wheat BPL</td>
                            <td align="left" style="width: 57px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="wheat_bpl_allot" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 16px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="wheat_bpl_ope" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 47px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="wheat_bpl_rec" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 43px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="wheat_bpl_distr" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 26px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="wheat_bpl_st" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 40px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="wheat_bpl_bal" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 70px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                Wheat AAY</td>
                            <td align="left" style="width: 57px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="wheat_aay_allot" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 16px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="wheat_aay_ope" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 47px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="wheat_aay_rec" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 43px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="wheat_aay_distr" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 26px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="wheat_aay_st" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 40px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="wheat_aay_bal" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 70px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                Sugar</td>
                            <td align="left" style="width: 57px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="sugar_allot" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 16px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="sugar_ope" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 47px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="sugar_rec" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 43px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="sugar_distr" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 26px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="sugar_st" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 40px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="sugar_bal" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 70px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                Kerosene</td>
                            <td align="left" style="width: 57px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="kerosene_allot" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 16px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="kerosene_ope" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 47px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="kerosene_rec" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 43px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="kerosene_distr" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 26px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="kerosene_st" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 40px; font-size: 10pt; color: black; background-color: white; border-right: gray 1pt solid; border-top: gray 1pt solid; border-left: gray 1pt solid; border-bottom: gray 1pt solid;">
                                <asp:TextBox ID="kerosene_bal" runat="server" Width="70px" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </div>
    </div>
    </form>
</body>
</html>
