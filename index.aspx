<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href='http://fonts.googleapis.com/css?family=Chango&subset=latin,latin-ext' rel='stylesheet' type='text/css'>
</head>
<body>
    <form id="form1" runat="server">
    <header>
    <div id="logo">
    Logo
    </div>
    <div id="nav_login">
        <asp:HyperLink ID="RegLink" Font-Underline="False" runat="server" 
            NavigateUrl="~/login.aspx" CssClass="LogLink"><span>Registracija</span></asp:HyperLink>
        <asp:HyperLink ID="LogInLink" Font-Underline="False" runat="server" CssClass="LogLink" 
            NavigateUrl="~/login.aspx">Prijava</asp:HyperLink>
    </div>
    </header>
    
    
    
    <br />
    <br />
    <br />
    <br />
    <br />
    
    
    
    
    <div id="navigacija">
        <asp:HyperLink ID="IndexLink" Font-Underline="False" runat="server" 
            NavigateUrl="~/index.aspx" CssClass="NavLink">Početna</asp:HyperLink>
        <asp:HyperLink ID="NoviProjLink" Font-Underline="False" runat="server" 
            CssClass="NavLink" NavigateUrl="~/noviProjekt.aspx">Započni projekt</asp:HyperLink>
        <asp:HyperLink ID="PregledProjLink" Font-Underline="False" runat="server" 
            CssClass="NavLink" NavigateUrl="~/pregledProjekata.aspx">Pregled projekata</asp:HyperLink>
        <asp:HyperLink ID="OnamaLink" Font-Underline="False" runat="server" 
            CssClass="NavLink" NavigateUrl="~/Onama.aspx">O nama</asp:HyperLink>
    
    </div>
    
    <hr />
    </form>
</body>
</html>
