<%@ Page Language="C#" AutoEventWireup="true" CodeFile="povratImeLozinka.aspx.cs" Inherits="povratImeLozinka" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<link href="css/style.css" rel="stylesheet" type="text/css" />   
    <title>Povrat korisničkog imena ili lozinke</title>
    <!-- evo malo posla za dizajnere :) -->
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="Panel1" runat="server">
        
        <asp:LinkButton ID="LinkButtonPassword" runat="server" 
            onclick="LinkButtonPassword_Click" CssClass="panel">Povrat lozinke</asp:LinkButton>
        
            <br />
            <br />
        
        <asp:LinkButton ID="LinkButtonUsername" runat="server" 
            onclick="LinkButtonUsername_Click" CssClass="panel">Povrat korisničkog imena</asp:LinkButton>
            
        <br />
        <hr />
        <asp:MultiView ID="MultiViewRetrieve" runat="server">
            <asp:View ID="ViewPassword" runat="server">
                <asp:Label ID="LabelEmailPassword" runat="server" Text="E-mail: " 
                    CssClass="labele"></asp:Label>
                <asp:TextBox ID="TextBoxEmailPassword" runat="server" CssClass="textbox"></asp:TextBox>
                <br />
                <asp:Label ID="LabelQuestion" runat="server" Text="Sigurnosno pitanje:" 
                    Visible="False" CssClass="labele"></asp:Label>
                <asp:TextBox ID="TextBoxQuestion" runat="server" Visible="False" 
                    Enabled="False" CssClass="textbox"></asp:TextBox>
                <br />
                <asp:Label ID="LabelAnswer" runat="server" Text="Sigurnosni odgovor:" 
                    Visible="False" CssClass="labele"></asp:Label>
                <asp:TextBox ID="TextBoxAnswer" runat="server" Visible="False" 
                    CssClass="textbox"></asp:TextBox>
                <br />
                <asp:Button ID="ButtonSubmitPassword" runat="server" Text="Pošalji" 
                    onclick="ButtonSubmitPassword_Click" CssClass="gumb3" />
            </asp:View>
            <asp:View ID="ViewUsername" runat="server">
                <asp:Label ID="LabelEmail" runat="server" Text="E-mail:" CssClass="labele"></asp:Label>
                <asp:TextBox ID="TextBoxEmail" runat="server" CssClass="textbox"></asp:TextBox>
                <asp:Button ID="ButtonSubmitUsername" runat="server" Text="Pošalji" 
                    onclick="ButtonSubmitUsername_Click" CssClass="gumb3" />
                <br />
            </asp:View>
        </asp:MultiView>
        </asp:Panel></div><p>
         <asp:Label ID="LabelStatus" runat="server" CssClass="linkovi"></asp:Label></p>
         </form>
    </body>
</html>