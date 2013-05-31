<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="index.aspx.cs" Inherits="index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="Form1" runat="server">
    <h1>Najnoviji projekti</h1>       
    
    <div runat="server" id="projekti_novi" style="height:594px;overflow:auto;">
    </div>

    <!-- služi da bi spriječio preklapanje novih i starih projekata -->
    <div style="margin:auto;clear:both;"></div>

    <hr class="druga_lin" />

    <h1>Projekti pred istekom vremena za donaciju</h1>

    <div runat="server" id="projekti_stari" style="height:594px;overflow:auto;">    
    </div>

    <a href="userPage.aspx">Administracija mojih projekata</a>
    </form>
    
</asp:Content>
