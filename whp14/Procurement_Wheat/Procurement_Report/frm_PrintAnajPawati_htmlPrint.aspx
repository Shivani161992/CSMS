<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_PrintAnajPawati_htmlPrint.aspx.cs" Inherits="WHP14_Procurement_Wheat_Procurement_Report_frm_PrintAnajPawati_htmlPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
  
    <title>Wheat2014 Procurement Monitoring System</title>

    
    <script type="text/javascript">

function CallPrint(strid)
{
 var prtContent = document.getElementById(strid);
 var WinPrint = window.open('print.htm','PrintWindow','letf=0,top=0,width=800%,height=600,toolbar=1,scrollbars=1,status=1');
 WinPrint.document.write(prtContent.innerHTML);
 WinPrint.document.close();
 WinPrint.focus();
 WinPrint.print();
 WinPrint.close();
}

    </script>

    <style type="text/css">
        @media print
        {
            .nav
            {
                display: none;
            }
        }
    </style>

    <script type="text/javascript">
    window.history.forward(0); 
    </script>
</head>

<body>
    <form id="form1" runat="server">
    <table border="1"  cellpadding="0" cellspacing="0" height="700px" width="100%">
    <tr><td valign="top">
    <table cellpadding="0" cellspacing="0"   style="background-color:#ecf5d5; width:100%; border:double;border-color:#868f6f">
    <tr>
    <td style="font-weight: bold;font-size: 9pt;color: #595f4a;font-family: Verdana;height: 20px;text-align: center; width:20%">किसान से प्राप्त अनाज की जानकारी</td>
    <td style="font-weight: bold;font-size: 9pt;color: #595f4a;font-family: Verdana;height: 20px;text-align: right;width:20%"> 
        <asp:RadioButton ID="rb9" runat="server" AutoPostBack="True" Checked="True" GroupName="gg" OnCheckedChanged="rb9_CheckedChanged" Text="डी.एम.पी - 9 पिन" /><asp:RadioButton ID="rb12" runat="server" AutoPostBack="True" GroupName="gg" OnCheckedChanged="rb12_CheckedChanged" Text="डी.एम.पी - 24 पिन" /></td>
    <td style="font-weight: bold;font-size: 9pt;color: #595f4a;font-family: Verdana;height: 20px;text-align: left;width:40%">
        <asp:Label ID="lblfid" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblrid" runat="server" Visible="False"></asp:Label></td>
    
    <td style="font-weight: bold;font-size: 9pt;color: #595f4a;font-family: Verdana;height: 20px;text-align: right;width:30%"></td>
    </tr>
    </table>
      <asp:Panel runat="server" ID="pn" Visible="false">
                                    <div id="divPrint">
                                        <table style="width: 900px; background-position: center center; background-repeat: no-repeat; border-top-width: 1px; border-left-width: 1px; border-left-color: black; border-bottom-width: 1px; border-bottom-color: black; border-top-color: black; border-right-width: 1px; border-right-color: black;" cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td style="width: 274px; height: 35px; font-size: 14px; font-weight: bold; border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;" align="center">
                                                    खरीदी पावती</td>
                                                <td style="width: 624px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="border-right: black 1px solid; border-top: black 1px solid; font-weight: bold; font-size: 14px; border-left: black 1px solid; width: 274px; height: 20px">
                                                    <asp:Label ID="lbltdate" runat="server"></asp:Label></td>
                                                <td style="width: 624px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 274px; border-right: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;">
                                                    <asp:DataList ID="DataList1" runat="server">
                                                        <ItemTemplate>
                                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 274px; font-size: 12px;">
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px;">
                                                                        &nbsp;जिला :&nbsp;<asp:Label ID="lblDistrict_Name" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"District_Name") %>'></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px;">
                                                                        &nbsp;उपार्जन केंद्र :&nbsp;<asp:Label ID="lblSociety_Name" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Society_Name") %>'></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px;">
                                                                        &nbsp;प्राप्ति क्र.:&nbsp;<asp:Label ID="lblReceivedID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ReceivedID") %>'></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px;">
                                                                        &nbsp;खरीदी दिनांक :&nbsp;<asp:Label ID="lblDate_Of_Receipt" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Date_Of_Receipt") %>'></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px;">
                                                                        &nbsp;ऋण पुस्तिका क्र./ वनाधिकारी पट्टा न. :&nbsp;<asp:Label ID="lblRinPustikaNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"RinPustikaNo") %>'></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px; font-weight: bold;">
                                                                        &nbsp;किसान कोड :&nbsp;<asp:Label ID="lblFarmer_Id" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Farmer_Id") %>'></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px;">
                                                                        &nbsp;किसान का नाम :&nbsp;<asp:Label ID="lblFarmerName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FarmerName") %>'></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px;">
                                                                        &nbsp;पिता /पति का नाम :&nbsp;<asp:Label ID="lblFatherHusName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FatherHusName") %>'></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px;">
                                                                        &nbsp;मोबाइल नं.:&nbsp;<asp:Label ID="lblMobileno" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Mobileno") %>'></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px;">
                                                                        &nbsp;गाँव /वार्ड का नाम :&nbsp;<asp:Label ID="lblVillageName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"VillageName") %>'></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px;">
                                                                        &nbsp;बैंक का नाम :&nbsp;<asp:Label ID="lblFarmer_BankName_New" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Farmer_BankName_New") %>'></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px;">
                                                                        &nbsp;बैंक शाखा का नाम :&nbsp;<asp:Label ID="lblFarmer_BankBranchName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Farmer_BankBranchName") %>'></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px;">
                                                                        &nbsp;बैंक खाता क्र.:&nbsp;<asp:Label ID="lblFarmer_BankAccountNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Farmer_BankAccountNo") %>'></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px;">
                                                                        &nbsp;तोल पत्रक क्र.:&nbsp;<asp:Label ID="lblTaulPatrakNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TaulPatrakNo") %>'></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px">
                                                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 274px; border-top: black 1px solid; border-bottom: black 1px solid; font-size: 12px;">
                                                                            <tr>
                                                                                <td style="width: 25px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid;" align="center">
                                                                                    अनाज</td>
                                                                                <td colspan="2" style="width: 50px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid;" align="center">
                                                                                    उपार्जित मात्रा
                                                                                </td>
                                                                                <td colspan="3" style="width: 75px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid;" align="center">
                                                                                    राशि(रु.में)</td>
                                                                                <td style="width: 34px; height: 50px; border-bottom: black 1px solid;" align="center">
                                                                                    भुगतान योग्य राशि राशि(रु.में)</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 25px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid;">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td style="width: 25px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid; border-left-width: 1px; border-left-color: black;" align="center">
                                                                                    बोरी
                                                                                </td>
                                                                                <td style="width: 25px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid;" align="center">
                                                                                    वजन(क्वी.में)</td>
                                                                                <td style="width: 25px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid;" align="center">
                                                                                    समर्थन मूल्य</td>
                                                                                <td style="width: 25px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid;" align="center">
                                                                                    केंद्रीय बोनस</td>
                                                                                <td style="width: 25px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid;" align="center">
                                                                                    राज्य बोनस</td>
                                                                                <td style="width: 50px; height: 25px; border-bottom: black 1px solid;" align="center">
                                                                                    कुल भुगतान</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 25px; height: 25px; border-right: black 1px solid; border-top-width: 1px; border-top-color: black;" align="left">
                                                                                    <asp:Label ID="lblcrop" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"crop") %>'></asp:Label></td>
                                                                                <td style="width: 25px; height: 25px; border-right: black 1px solid;" align="right">
                                                                                    <asp:Label ID="lblBags" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Bags") %>'></asp:Label></td>
                                                                                <td style="width: 25px; height: 25px; border-right: black 1px solid;" align="right">
                                                                                    <asp:Label ID="lblQtyReceived" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"QtyReceived") %>'></asp:Label></td>
                                                                                <td style="width: 25px; height: 25px; border-right: black 1px solid;" align="right">
                                                                                    <asp:Label ID="lblMSPRate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"MSPRate") %>'></asp:Label></td>
                                                                                <td style="width: 25px; height: 25px; border-right: black 1px solid;" align="right">
                                                                                    <asp:Label ID="lblCentralBonus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CentralBonus") %>'></asp:Label></td>
                                                                                <td style="width: 25px; height: 25px; border-right: black 1px solid;" align="right">
                                                                                    <asp:Label ID="lblStateBonus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"StateBonus") %>'></asp:Label></td>
                                                                                <td style="width: 50px; height: 25px" align="right">
                                                                                    <asp:Label ID="lblTotaAmountPayableToFarmer" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TotaAmountPayableToFarmer") %>'></asp:Label></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px">
                                                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 274px">
                                                                            <tr>
                                                                                <td style="width: 224px; height: 25px">
                                                                                    <span style="font-weight: 400; font-size: 8pt; color: #000000; font-style: normal; font-family: Arial; text-decoration: none">&nbsp;सोसाइटी ऋण के विरुद्ध वसूली गयी राशि</span></td>
                                                                                <td style="width: 50px; height: 25px" align="right">
                                                                                    <asp:Label ID="lblAmtAgainstSCCredit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"AmtAgainstSCCredit") %>'></asp:Label>&nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 224px; height: 25px">
                                                                                    <span style="font-weight: 400; font-size: 8pt; color: #000000; font-style: normal; font-family: Arial; text-decoration: none">&nbsp;जिला केन्द्रीय सहकारी बैंक ऋण के विरुद्ध वसूली गयी राशि</span></td>
                                                                                <td style="width: 50px; height: 25px" align="right">
                                                                                    <asp:Label ID="lblAmtAgainstBankCredit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"AmtAgainstBankCredit") %>'></asp:Label>&nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 224px; height: 25px">
                                                                                    <span style="font-weight: 400; font-size: 8pt; color: #000000; font-style: normal; font-family: Arial; text-decoration: none">&nbsp;सिंचाई विभाग के बकाया के विरुद्ध वसूली गयी राशि</span></td>
                                                                                <td style="width: 50px; height: 25px" align="right">
                                                                                    <asp:Label ID="lblAmtAgIrg_Loan" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"AmtAgIrg_Loan") %>'></asp:Label>&nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 224px; height: 25px">
                                                                                    <span style="font-weight: 400; font-size: 8pt; color: #000000; font-style: normal; font-family: Arial; text-decoration: none">&nbsp;कुल वसूली राशि (रु.मे)</span></td>
                                                                                <td style="width: 50px; height: 25px" align="right">
                                                                                    <asp:Label ID="lblamt" runat="server"></asp:Label>&nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 224px; height: 25px">
                                                                                    <span style="font-weight: 700; font-size: 10pt; color: #000000; font-style: normal; font-family: Arial; text-decoration: none">&nbsp;शुद्ध भुगतान (रु.मे)</span></td>
                                                                                <td style="width: 50px; height: 25px" align="right">
                                                                                    <asp:Label ID="lbltotamt" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"NetAmountPayableToFarmer") %>'></asp:Label>&nbsp;</td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px; font-weight: bold;">
                                                                        &nbsp; नोट : इस रसीद के जारी होने के 7 दिन के भीतर आपके बैंक खाते मे भुगतान की राशि भेज दी जायेगी |</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 60px; font-weight: bold;" valign="top">
                                                                        &nbsp; किसान के हस्ताक्षर /अंगूठे का निशान
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 60px; font-weight: bold;" valign="top">
                                                                        &nbsp; उपार्जन केन्द्र प्रबंधक के हस्ताक्षर</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px; font-weight: bold;">
                                                                        &nbsp; अधिक जानकारी के लिए टोल फ्री न. पर संपर्क करे - 155343<br />
                                                                        &nbsp; खाद्य, नागरिक आपूर्ति एवं उपभोक्ता संरक्षण विभाग म.प्र.</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 274px; height: 25px; font-weight: bold;">
                                                                        &nbsp;
                                                                        <asp:Label ID="lbldate" runat="server"></asp:Label></td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:DataList></td>
                                                <td style="width: 624px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 274px">
                                                </td>
                                                <td style="width: 624px;">
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:Panel>
       
          <asp:Panel runat="server" ID="pn1" Visible="false">
                                 <div id="divPrint1">
                                    <table style="width: 900px; background-position: center center; background-repeat: no-repeat; border-top-width: 1px; border-left-width: 1px; border-left-color: black; border-bottom-width: 1px; border-bottom-color: black; border-top-color: black; border-right-width: 1px; border-right-color: black;" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td style="width: 900px; height: 35px; font-size: 18px; font-weight: bold; border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;" align="center" colspan="2">
                                                खरीदी पावती</td>
                                        </tr>
                                        <tr>
                                            <td style="border-right: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;" colspan="2">
                                                <asp:DataList ID="DataList2" runat="server">
                                                    <ItemTemplate>
                                                        <table border="0" cellpadding="0" cellspacing="0" style="font-size: 14px; width: 900px">
                                                            <tr>
                                                                <td style="width: 173px; height: 35px">
                                                                    &nbsp;जिला :</td>
                                                                <td style="width: 173px; height: 35px">
                                                                    <asp:Label ID="lblDistrict_Name" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"District_Name") %>'></asp:Label></td>
                                                                <td style="width: 20px; height: 35px">
                                                                </td>
                                                                <td style="width: 173px; height: 35px">
                                                                </td>
                                                                <td style="width: 173px; height: 35px">
                                                                </td>
                                                                <td style="width: 20px; height: 35px">
                                                                </td>
                                                                <td style="width: 173px; height: 35px" align="right">
                                                                    खरीदी दिनांक :</td>
                                                                <td style="width: 112px; height: 35px">
                                                                    &nbsp;<asp:Label ID="lblDate_Of_Receipt" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Date_Of_Receipt") %>'></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 173px; height: 35px">
                                                                    &nbsp;उपार्जन केंद्र :</td>
                                                                <td style="width: 173px; height: 35px">
                                                                    <asp:Label ID="lblSociety_Name" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Society_Name") %>'></asp:Label></td>
                                                                <td style="width: 20px; height: 35px">
                                                                </td>
                                                                <td style="width: 173px; height: 35px">
                                                                </td>
                                                                <td style="width: 173px; height: 35px">
                                                                </td>
                                                                <td style="width: 20px; height: 35px">
                                                                </td>
                                                                <td style="width: 173px; height: 35px">
                                                                </td>
                                                                <td style="width: 112px; height: 35px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 173px; height: 35px">
                                                                    &nbsp;प्राप्ति क्र.:&nbsp;</td>
                                                                <td style="width: 112px; height: 35px">
                                                                    <asp:Label ID="lblReceivedID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ReceivedID") %>'></asp:Label></td>
                                                                <td style="width: 20px; height: 35px">
                                                                </td>
                                                                <td style="width: 173px; height: 35px; font-weight: bold;">
                                                                    किसान कोड :</td>
                                                                <td style="width: 173px; height: 35px; font-weight: bold; font-family: Arial;">
                                                                    <asp:Label ID="lblFarmer_Id" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Farmer_Id") %>'></asp:Label></td>
                                                                <td style="width: 20px; height: 35px">
                                                                </td>
                                                                <td style="width: 173px; height: 35px">
                                                                </td>
                                                                <td style="width: 112px; height: 35px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 173px; height: 35px">
                                                                    &nbsp;किसान का नाम :</td>
                                                                <td style="width: 173px; height: 35px">
                                                                    <asp:Label ID="lblFarmerName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FarmerName") %>'></asp:Label></td>
                                                                <td style="width: 20px; height: 35px">
                                                                </td>
                                                                <td style="width: 173px; height: 35px">
                                                                    पिता /पति का नाम :</td>
                                                                <td style="width: 173px; height: 35px">
                                                                    <asp:Label ID="lblFatherHusName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FatherHusName") %>'></asp:Label></td>
                                                                <td style="width: 20px; height: 35px">
                                                                </td>
                                                                <td style="width: 173px; height: 35px">
                                                                    मोबाइल नं.:</td>
                                                                <td style="width: 112px; height: 35px">
                                                                    <asp:Label ID="lblMobileno" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Mobileno") %>'></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 173px; height: 35px">
                                                                    &nbsp;गाँव /वार्ड का नाम :</td>
                                                                <td style="width: 173px; height: 35px">
                                                                    <asp:Label ID="lblVillageName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"VillageName") %>'></asp:Label></td>
                                                                <td style="width: 20px; height: 35px">
                                                                </td>
                                                                <td style="width: 173px; height: 35px">
                                                                    बैंक का नाम :&nbsp;</td>
                                                                <td style="width: 173px; height: 35px">
                                                                    <asp:Label ID="lblFarmer_BankName_New" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Farmer_BankName_New") %>'></asp:Label></td>
                                                                <td style="width: 20px; height: 35px">
                                                                </td>
                                                                <td style="width: 173px; height: 35px">
                                                                    बैंक शाखा का नाम :</td>
                                                                <td style="width: 112px; height: 35px">
                                                                    <asp:Label ID="lblFarmer_BankBranchName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Farmer_BankBranchName") %>'></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 173px; height: 35px">
                                                                    &nbsp;बैंक खाता क्र.:&nbsp;</td>
                                                                <td style="width: 173px; height: 35px">
                                                                    <asp:Label ID="lblFarmer_BankAccountNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Farmer_BankAccountNo") %>'></asp:Label></td>
                                                                <td style="width: 20px; height: 35px">
                                                                </td>
                                                                <td style="width: 173px; height: 35px">
                                                                    ऋण पुस्तिका क्र./ वनाधिकारी पट्टा न. :&nbsp;</td>
                                                                <td style="width: 173px; height: 35px">
                                                                    <asp:Label ID="lblRinPustikaNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"RinPustikaNo") %>'></asp:Label></td>
                                                                <td style="width: 20px; height: 35px">
                                                                </td>
                                                                <td style="width: 173px; height: 35px">
                                                                    तोल पत्रक क्र.:&nbsp;</td>
                                                                <td style="width: 112px; height: 35px">
                                                                    <asp:Label ID="lblTaulPatrakNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TaulPatrakNo") %>'></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 900px; height: 35px" colspan="8">
                                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 900px; border-top: black 1px solid; border-bottom: black 1px solid; font-size: 14px;">
                                                                        <tr>
                                                                            <td style="width: 100px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid; font-weight: bold;" align="center">
                                                                                अनाज</td>
                                                                            <td colspan="2" style="width: 200px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid; font-weight: bold;" align="center">
                                                                                उपार्जित मात्रा
                                                                            </td>
                                                                            <td colspan="3" style="width: 200px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid; font-weight: bold;" align="center">
                                                                                राशि(रु.में)</td>
                                                                            <td style="width: 300px; height: 50px; border-bottom: black 1px solid; font-weight: bold;" align="center">
                                                                                भुगतान योग्य राशि राशि(रु.में)</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid;">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td style="width: 100px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid; border-left-width: 1px; border-left-color: black; font-weight: bold;" align="center">
                                                                                बोरी
                                                                            </td>
                                                                            <td style="width: 100px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid; font-weight: bold;" align="center">
                                                                                वजन(क्वी.में)</td>
                                                                            <td style="width: 100px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid; font-weight: bold;" align="center">
                                                                                समर्थन मूल्य</td>
                                                                            <td style="width: 100px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid; font-weight: bold;" align="center">
                                                                                केंद्रीय बोनस</td>
                                                                            <td style="width: 100px; height: 25px; border-right: black 1px solid; border-bottom: black 1px solid; font-weight: bold;" align="center">
                                                                                राज्य बोनस</td>
                                                                            <td style="width: 300px; height: 25px; border-bottom: black 1px solid; font-weight: bold;" align="center">
                                                                                कुल भुगतान</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px; height: 25px; border-right: black 1px solid; border-top-width: 1px; border-top-color: black;" align="center">
                                                                                <asp:Label ID="lblcrop" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"crop") %>'></asp:Label></td>
                                                                            <td style="width: 100px; height: 25px; border-right: black 1px solid;" align="right">
                                                                                <asp:Label ID="lblBags" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Bags") %>'></asp:Label>&nbsp;</td>
                                                                            <td style="width: 100px; height: 25px; border-right: black 1px solid;" align="right">
                                                                                <asp:Label ID="lblQtyReceived" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"QtyReceived") %>'></asp:Label>&nbsp;</td>
                                                                            <td style="width: 100px; height: 25px; border-right: black 1px solid;" align="right">
                                                                                <asp:Label ID="lblMSPRate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"MSPRate") %>'></asp:Label>&nbsp;</td>
                                                                            <td style="width: 100px; height: 25px; border-right: black 1px solid;" align="right">
                                                                                <asp:Label ID="lblCentralBonus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CentralBonus") %>'></asp:Label>&nbsp;</td>
                                                                            <td style="width: 100px; height: 25px; border-right: black 1px solid;" align="right">
                                                                                <asp:Label ID="lblStateBonus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"StateBonus") %>'></asp:Label>&nbsp;</td>
                                                                            <td style="width: 300px; height: 25px" align="right">
                                                                                <asp:Label ID="lblTotaAmountPayableToFarmer" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TotaAmountPayableToFarmer") %>'></asp:Label>&nbsp;</td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="8" style="width: 900px; height: 35px">
                                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 900px; font-size: 16px;">
                                                                        <tr>
                                                                            <td style="width: 700px; height: 35px">
                                                                                &nbsp;सोसाइटी ऋण के विरुद्ध वसूली गयी राशि</td>
                                                                            <td style="width: 200px; height: 35px" align="right">
                                                                                <asp:Label ID="lblAmtAgainstSCCredit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"AmtAgainstSCCredit") %>'></asp:Label>&nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 700px; height: 35px">
                                                                                &nbsp;जिला केन्द्रीय सहकारी बैंक ऋण के विरुद्ध वसूली गयी राशि</td>
                                                                            <td style="width: 200px; height: 35px" align="right">
                                                                                <asp:Label ID="lblAmtAgainstBankCredit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"AmtAgainstBankCredit") %>'></asp:Label>&nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 700px; height: 35px">
                                                                                &nbsp;सिंचाई विभाग के बकाया के विरुद्ध वसूली गयी राशि</td>
                                                                            <td style="width: 200px; height: 35px" align="right">
                                                                                <asp:Label ID="lblAmtAgIrg_Loan" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"AmtAgIrg_Loan") %>'></asp:Label>&nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 700px; height: 35px">
                                                                                &nbsp;कुल वसूली राशि (रु.मे)</td>
                                                                            <td style="width: 200px; height: 35px" align="right">
                                                                                <asp:Label ID="lblamt" runat="server"></asp:Label>&nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 700px; height: 35px; font-weight: bold; border-top: black 1px solid; border-bottom: black 1px solid;">
                                                                                &nbsp;शुद्ध भुगतान (रु.मे)</td>
                                                                            <td style="width: 200px; height: 35px; font-weight: bold; border-top: black 1px solid; border-bottom: black 1px solid; font-family: Arial;" align="right">
                                                                                <asp:Label ID="lbltotamt" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"NetAmountPayableToFarmer") %>'></asp:Label>&nbsp;</td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="8" style="width: 900px; height: 35px; font-weight: bold; border-bottom: black 1px solid;">
                                                                    &nbsp;नोट : इस रसीद के जारी होने के 7 दिन के भीतर आपके बैंक खाते मे भुगतान की राशि भेज दी जायेगी |</td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="8" style="width: 900px; height: 35px">
                                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 900px">
                                                                        <tr>
                                                                            <td style="width: 300px; height: 100px; font-weight: bold;" valign="top">
                                                                                &nbsp; किसान के हस्ताक्षर /अंगूठे का निशान</td>
                                                                            <td style="width: 300px; height: 100px">
                                                                            </td>
                                                                            <td align="right" style="width: 300px; height: 100px; font-weight: bold;" valign="top">
                                                                                उपार्जन केन्द्र प्रबंधक के हस्ताक्षर &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 300px; height: 35px">
                                                                                &nbsp; खाद्य, नागरिक आपूर्ति एवं उपभोक्ता संरक्षण विभाग म.प्र.</td>
                                                                            <td align="center" style="width: 300px; height: 35px">
                                                                                अधिक जानकारी के लिए टोल फ्री न. पर संपर्क करे - 155343<br />
                                                                            </td>
                                                                            <td align="right" style="width: 300px; height: 35px" valign="top">
                                                                                <asp:Label ID="lbldate" runat="server"></asp:Label>&nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 300px; height: 35px">
                                                                            </td>
                                                                            <td align="center" style="width: 300px; height: 35px">
                                                                            </td>
                                                                            <td align="right" style="width: 300px; height: 35px">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 274px">
                                            </td>
                                            <td style="width: 624px;">
                                            </td>
                                        </tr>
                                    </table>
                                    </div>
                                </asp:Panel>
        </td></tr>
       <tr>
           <td valign="top">
               <asp:Button ID="Button1" runat="server" Text="प्रिंट करें" />
               <asp:Button ID="Button2" runat="server" Text="प्रिंट करें" /></td>
       </tr>
        </table>
         <table border="1"  cellpadding="0" cellspacing="0" height="30px" width="100%" style="background-color:#9ca782">
         <tr><td height="20px">
         
         </td></tr>
         </table>
    </form>
</body>
</html>
