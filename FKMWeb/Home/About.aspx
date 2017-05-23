<%@ Page Title="About Us" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="false"
    CodeFile="About.aspx.cs" Inherits="About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        About
    </h2>
    <p>
        Put content here.
    </p>
    
        <div style="width:720PX;" ID="PHOTO">
        </div>
   <%--     <script src="js/jquery.js" type="text/javascript"></script>
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <script src="js/jquery.backstretch.js" type="text/javascript"></script>
    <script type="text/javascript">
        'use strict';

        /* ========================== */
        /* ::::::: Backstrech ::::::: */
        /* ========================== */
        // You may also attach Backstretch to a block-level element
        // $.backstretch("img/grey1.png");
        $("PHOTO").backstretch()
        [

            "img/blue1.png",
            "img/grey1.png",
            "img/blue2.png",
            "img/grey2.png"
        ],

        {
            duration: 4500,
            fade: 1500
        }
    );
    </script>--%>
</asp:Content>