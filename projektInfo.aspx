<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="projektInfo.aspx.cs" Inherits="projektInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<form runat="server">
<asp:MultiView ID="MultiView1" runat="server">
    <asp:View ID="ViewAll" runat="server">
        <div id="projectContainer" runat="server">
            <div id="naslov" runat ="server">
                <h1 id="title" runat="server"></h1>
            </div>
            <br /><br />
            <div id="projekti" runat="server" style="overflow:auto;"></div>
        </div>
    </asp:View>
    <asp:View ID="ViewProject" runat="server">
        <div id="projectDisplay" runat = "server"></div>
        <div id="comments" runat="server"></div>
    </asp:View>
</asp:MultiView>
</form>
</asp:Content>

