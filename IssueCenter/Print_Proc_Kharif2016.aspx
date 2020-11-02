<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print_Proc_Kharif2016.aspx.cs" Inherits="IssueCenter_Print_Proc_Kharif2016" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <style type="text/css">
        .A4 {
            width: 210mm; /* You willmay need to reduce this to handle printer margins */
            margin: auto; /* This means it will be centred */
            height: 250mm;
            text-align: justify;
            font-size: medium;
        }

        .LineBreak {
            /*page-break-before: always;*/
            height: 0mm;
            overflow: hidden;
        }

        .auto-style1 {
            width: 100%;
            border-style: solid;
            border-width: 2px;
            border-color: black;
        }

        p {
            margin-right: 0in;
            margin-left: 0in;
            font-size: 12.0pt;
            font-family: "Times New Roman","serif";
        }

        @page {
            size: auto; /* auto is the current printer page size */
            margin: 25px 25px 0px 30px; /* this affects the margin in the printer settings */
        }

        .auto-style2 {
            text-decoration: underline;
        }

        .auto-style3 {
            width: 100%;
        }
    </style>

</head>
<body onload="window.print()">
    <form id="form1" runat="server">
        <br />
        <br />
        <div class="A4">
            <table align="center" class="auto-style1">

                <tr>
                    <td rowspan="3" style="vertical-align: top; text-align: right" cellspacing="0" cellpadding="0">
                        <asp:Image ID="Image1" runat="server" Height="120px" Width="120px" />
                    </td>
                    <td style="text-align: center" class="auto-style3">
                        <asp:Label ID="lblMFD" runat="server" Text="" Style="font-size: large"></asp:Label>
                    </td>
                    <td rowspan="3" style="vertical-align: top; text-align: right" cellspacing="0" cellpadding="0">
                        <asp:Image ID="ImgQRCode" runat="server" Height="125px" Width="125px" />
                    </td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3">District ....<asp:Label ID="lblDistManagerName" runat="server" Font-Bold="True" Style="font-size: medium"></asp:Label>....<asp:Label ID="lblCropYear" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3"><strong>&lt;&lt;<span class="auto-style2">&nbsp; <asp:Label ID="lblcomdty0" runat="server" Font-Bold="True" ></asp:Label>&nbsp;Receipt Acknowledgement </span>&gt;&gt;</strong></td>

                </tr>
            </table>
            <table align="center" class="auto-style1">

                <tr>
                    <td style="text-align: center" class="auto-style7" colspan="2">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: left" colspan="2">1) Receipt ID- ....<asp:Label ID="lblgno" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>



                <tr>
                    <td style="text-align: left">2) Sending District- ....<asp:Label ID="lblsenddist" runat="server" Font-Bold="True" Style="font-size: medium"></asp:Label>....</td>
                    <td style="text-align: right">Crop Year-&nbsp;....<asp:Label ID="lblcrop" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>



                <tr>
                    <td style="text-align: left">3) Challan No.- ....<asp:Label ID="lblchallanno" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td style="text-align: right">Challan Date-&nbsp;....<asp:Label ID="lblchallandt" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                 <tr>
                    <td style="text-align: left">4) Sending Bags- ....<asp:Label ID="lblsend_bagsNum" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td style="text-align: right">Sending Qty<strong> (Qtls)</strong>- ....<asp:Label ID="lblSend_Qtydisplay" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left">5) Commodity Name- ....<asp:Label ID="lblcomdty" runat="server" Font-Bold="True" Style="font-size: small"></asp:Label>....</td>
                    <td style="text-align: right">Truck  No.-&nbsp;....<asp:Label ID="lblvicln" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>


                <tr>
                    <td style="text-align: left" colspan="2">6) Purchase Center- ....<asp:Label ID="lblpccenter" runat="server" Font-Bold="True" Style="font-size: small"></asp:Label>....</td>
                </tr>



                <tr>
                    <td style="text-align: left" colspan="2">7) Transporter Name- ....<asp:Label ID="lbltransp" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                
               

                <tr>
                    <td style="text-align: left" colspan="2">&nbsp;</td>
                </tr>



                <tr>
                    <td style="text-align: left">8)&nbsp;&nbsp; Recd. Bags- ....<asp:Label ID="lblbagno" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td style="text-align: right">Recd. Qty<strong> (Qtls)- </strong>....<asp:Label ID="lblweight" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>



                <tr>
                    <td style="text-align: left">9)&nbsp;&nbsp; Recd. IssueCenter-&nbsp;....<asp:Label ID="lbldepon" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td style="text-align: right">Recd. Date- ....<asp:Label ID="lblmoisture" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>



                <tr>
                    <td style="text-align: left" colspan="2">10) Recd. Godown- ....<asp:Label ID="lblGodownNo" runat="server" Font-Bold="True"></asp:Label>.... </td>
                </tr>



                <tr>
                    <td style="text-align: left" colspan="2">11) Discription- ....................................................................................</td>
                </tr>

                <tr>
                    <td style="text-align: left" colspan="2">&nbsp;</td>
                </tr>

            </table>

            <table align="center" class="auto-style1">

                <tr>
                    <td style="text-align: left" colspan="3">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: right" colspan="3">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: right" colspan="3">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: right" colspan="3">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: center">Signature of 
                        <br />
                        Truck Driver</td>
                    <td style="text-align: center">Signature of 
                        <br />
                        Incharge</td>
                    <td style="text-align: center">Signature of 
                        <br />
                        Branch Manager</td>
                </tr>

            </table>

            <table>

                <table align="center" width="100%">

                    <tr class="auto-style4">
                        <td style="text-align: left">
                            <asp:Label ID="lblCurrentDateTime" runat="server" Font-Bold="True"></asp:Label></td>
                    </tr>

                </table>
        </div>
    </form>
</body>
</html>
