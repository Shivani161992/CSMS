<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="nnn.aspx.cs" Inherits="State_nnn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>

    <script type="text/javascript">

        //Function for jQuery
        $(function () {
            $('#ctl00_ContentPlaceHolder1_btnUsingjQuery').click(function () {
                alert('Alert using jQuery Function!');
            });
        });

        $(document).ready(function () {
            $('#ctl00_ContentPlaceHolder1_btnStart').click(function () {
                //define your time in second
                var c = 300;
                var t;
                timedCount();

                function timedCount() {

                    var hours = parseInt(c / 3600) % 24;
                    var minutes = parseInt(c / 60) % 60;
                    var seconds = c % 60;

                    var result = (hours < 10 ? "0" + hours : hours) + ":" + (minutes < 10 ? "0" + minutes : minutes) + ":" + (seconds < 10 ? "0" + seconds : seconds);


                    $('#timer').html(result);
                    if (c == 0) {
                        //setConfirmUnload(false);
                        //$("#quiz_form").submit();
                        window.location = "Timer1.aspx";
                    }
                    c = c - 1;
                    t = setTimeout(function () {
                        timedCount()
                    },
                    1000);
                }
            });
        });



    </script>

            
        <h3 style="color: #FF0000" align="center">You will be logged out in : <span id='timer'></span>
        </h3>

    <div>
        <input type="button" id="btnStart" value="Input" runat="server" />
        <input id="btnUsingjQuery" type="button" value="Using jQuery" runat="server" />
    </div>



</asp:Content>

