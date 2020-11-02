<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Surveyor_ChangePassword.aspx.cs" Inherits="SurveyorLogin_Uparjan_Surveyor_ChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <style>
        .Box
        {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            width: 500px;
            padding: 40px;
            background: #FFF;
            border-radius: 10px;
        }

        .surtable
        {
            width: 500px;
            border-top: 6px groove #D9D9D9;
            border-bottom: 6px groove #D9D9D9;
            border-left: 6px groove #D9D9D9;
            border-right: 6px groove #D9D9D9;
            border-radius: 8px;
        }

        .surveLinks
        {
            text-decoration: none;
            font-family: 'Bellefair';
            letter-spacing: 2px;
            font-size: 16px;
        }

        .InspColumn
        {
            width: 33%;
            color: #10321f;
            letter-spacing: 2px;
            font-family: Almendra;
            font-size: 13px;
            font-weight: bold;
        }

        .insptxt
        {
            width: 300px;
            height: 20px;
            letter-spacing: 2px;
            font-family: sans-serif;
            font-size: 12px;
            border-color: #03a9f4;
            border: 1px solid #03a9f4;
            border-radius: 8px;
            color: black;
            padding-left: 10px;
        }
        .insptxt:focus
        {
            border:none;
        }

        .insfixtext
        {
            width: 310px;
            height: 20px;
            letter-spacing: 2px;
            font-family: sans-serif;
            font-size: 12px;
            border-color: #03a9f4;
            border: 1px solid #03a9f4;
            border-radius: 8px;
            color: black;
            padding-left: 10px;
        }

        .bttsubother
        {
            background: transparent;
            border: none;
            outline: none;
            color: #fff;
            background: #00AAA0;
            padding: 10px 20px;
            cursor: pointer;
            border-radius: 5px;
            letter-spacing: 4px;
            font-family: Almendra;
            height: 30px;
            width: 150px;
        }

            .bttsubother:enabled, button[enabled]
            {
                background: #e74c3c;
            }

        .sign
        {
            color: #062946;
            font-size: 15px;
            text-decoration: none;
            letter-spacing: 2px;
        }
    </style>
    
    <script type="text/javascript">
        function CheckPassword(e, tx) {
            var AsciiCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;

            if (AsciiCode == 59 || AsciiCode == 32) {
                alert('Sorry ! Semicolon(;)/Blank Space Not Allowed.');
                event.cancelBubble = true;
                event.returnValue = false;
            }


        }
</script>
<script language = "Javascript">
    /**
     * DHTML textbox character counter script. Courtesy of SmartWebby.com (http://www.smartwebby.com/dhtml/)
     */

    var maxL = 10;
    var bName = navigator.appName;
    function taLimit(taObj) {
        if (taObj.value.length == maxL) return false;
        return true;
    }

    function taCount(taObj, Cnt) {
        objCnt = createObject(Cnt);
        objVal = taObj.value;
        if (objVal.length > maxL) objVal = objVal.substring(0, maxL);
        if (objCnt) {
            if (bName == "Netscape") {
                objCnt.textContent = maxL - objVal.length;
            }
            else { objCnt.innerText = maxL - objVal.length; }
        }
        return true;
    }
    function createObject(objId) {
        if (document.getElementById) return document.getElementById(objId);
        else if (document.layers) return eval("document." + objId);
        else if (document.all) return eval("document.all." + objId);
        else return eval("document." + objId);
    }
</script>

</head>
<body>
      <form runat="server" id="form1">
    <center>
        <table style="width:100%;">
            <tr>
                <td  style="width:100%; text-align:right">
                  <asp:LinkButton ID="LinkButton2" runat="server" Font-Bold="False" ForeColor="black" CssClass="sign" PostBackUrl="#" 
                      CausesValidation="False">Log Out</asp:LinkButton>
                </td>
            </tr>
        </table>
    </center>
     <center>
        <table style="width: 1100px; font-size: 12px;">
            <tr>
                <td style="text-align: left; width: 200px">
                    <a href="SurveyorLogin_Welcome.aspx" class="sign">&#9754 Back
                    </a>
                </td>
                <td style="text-align: center; width: 700px">
                    <%--<h2 style="color: #e74c3c; font-size: 20px; font-family: 'Bellefair'; letter-spacing: 4px;">Surveyor Quality Inspection</h2>
                    <input type="hidden" runat="server" id="hdfDist" />--%>
                   
                   
                </td>

                <td style="text-align: right; width: 200px">
                    <a href="Surveyor_ChangePassword.aspx" class="sign">&#8635 New
                    </a>
                </td>
            </tr>
        </table>
    </center>
     <div class="Box">
         
         <h2 style="color:black; font-size: 20px; font-family: 'Bellefair'; letter-spacing: 4px; text-decoration-style: wavy; text-align:center;">Change Password</h2>
        <table class="surtable" >
             <tr>
                <td style="height:10px;" class="InspColumn" >

                </td>
            </tr>
              <tr>
                <td style="height:10px; text-align: center"" class="InspColumn" >
                    You have <b><span id="myCounter">10</span></b> characters remaining
                </td>
            </tr>
            <tr>
                
                  <td class="InspColumn" style="padding-left: 90px; text-align: left">Old password
                    <br /> 
                    <asp:TextBox ID="txtoldpassword" CssClass="insptxt" runat="server" MaxLength="10" TextMode="Password" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtoldpassword" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
            </tr>
             <tr>
                
                  <td class="InspColumn" style="padding-left: 90px; text-align: left">New Password
                    <br />
                    <asp:TextBox ID="txtnewpassword" MaxLength="10" CssClass="insptxt" runat="server" TextMode="Password" ></asp:TextBox>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtnewpassword"
                ErrorMessage="New Password Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
                </td>
            </tr>
             <tr>
                
                  <td class="InspColumn" style="padding-left: 90px; text-align: left">Confirm Password
                    <br />
                    <asp:TextBox ID="txtconpassword" MaxLength="10" CssClass="insptxt" runat="server" TextMode="Password" ></asp:TextBox>
                     <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtnewpassword"
        ControlToValidate="txtconpassword" ErrorMessage="Password not Match" ValidationGroup="1">*</asp:CompareValidator></td>
                </td>
            </tr>
            <tr>
                <td style="height:10px;" class="InspColumn" >

                </td>
            </tr>
            <tr>
                <td style="height: 10px;">
                    <center>
                        <asp:Button ID="bttsub" runat="server" Text="Submit" CssClass="bttsubother" Visible="true" Enabled="true" OnClick="bttsub_Click" />
                    </center>
                </td>
            </tr>
            <tr>
                <td style="height:10px;" class="InspColumn" >

                </td>
            </tr>
        </table>
    </div>
        </form>
 
</body>
</html>
