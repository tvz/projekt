<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="projektInfo.aspx.cs" Inherits="projektInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<form id="Form1" runat="server">
<asp:MultiView ID="MultiView1" runat="server">
    <asp:View ID="ViewAll" runat="server">
        <div id="projectContainer" runat="server"></div>
    </asp:View>
    <asp:View ID="ViewProject" runat="server">
        <div id="projectDisplay" runat = "server"></div>
        <div id="comments" runat="server"></div>
    </asp:View>
</asp:MultiView>
</form>
</asp:Content>

