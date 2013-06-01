<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="index.aspx.cs" Inherits="index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="Form1" runat="server">
    <h1 id="naslovNovi" runat="server">Najnoviji projekti</h1>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">   
    <ContentTemplate>
    <asp:Button ID="scrollNewLeft" runat="server" Text="Nazad" onclick="scroll" Visible="False" />
    <asp:Button ID="scrollNewRight" runat="server" Text="Naprijed" onclick="scroll" />
    <div runat="server" id="projekti_novi" style="height:594px;">
    </div>
    </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="scrollNewRight" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="scrollNewLeft" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>

    <!-- služi da bi spriječio preklapanje novih i starih projekata -->
    <div style="margin:auto;clear:both;"></div>
    
    <hr class="druga_lin" />

    <h1 id="naslovStari" runat="server">Projekti pred istekom vremena za donaciju</h1>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
    <asp:Button ID="scrollOldLeft" runat="server" Text="Nazad" onclick="scroll" />
    <asp:Button ID="scrollOldRight" runat="server" Text="Naprijed" onclick="scroll" />
    <div runat="server" id="projekti_stari" style="height:594px;overflow:auto;">
    </div>
    </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="scrollOldLeft" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="scrollOldRight" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    </form>
</asp:Content>
