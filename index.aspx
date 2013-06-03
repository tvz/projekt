<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="index.aspx.cs" Inherits="index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="Form1" runat="server">
    <div id="noviProjekti">
    <h1 id="naslovNovi" runat="server">Najnoviji projekti</h1>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">   
    <ContentTemplate>
    <div style="margin:35px 0px -35px 520px; height:10px;">
    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
    <ProgressTemplate><img src="img/loader.gif" alt="Učitavam..." style="height:20px;width:50px;"/></ProgressTemplate>
    </asp:UpdateProgress>
    </div>
    <asp:ImageButton ID="scrollNewLeft" runat="server" ImageUrl="~/img/previous.png" 
            Width="70px" onclick="scroll" CssClass="previous"/>
    <asp:ImageButton ID="scrollNewRight" runat="server" ImageUrl="~/img/next.png" 
            Width="70px" onclick="scroll" CssClass="next"/>   
    <div runat="server" id="projekti_novi" style="height:534px;">
    </div>
    </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="scrollNewRight" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="scrollNewLeft" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    </div>
    <div style="margin:auto;clear:both;"></div>
    <div style="text-align:center;">
    <asp:LinkButton ID="showNewer" runat="server" onclick="showAll">Pregledaj sve</asp:LinkButton>
    </div>
    <!-- služi da bi spriječio preklapanje novih i starih projekata -->
    <div style="margin:auto;clear:both;"></div>
    
    <hr class="druga_lin" />

    <div id="stariProjekti">
    <h1 id="naslovStari" runat="server">Projekti pred istekom vremena za donaciju</h1>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
    <div style="margin:35px 0px -35px 520px; height:10px;">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
    <ProgressTemplate>
    <img src="img/loader.gif" alt="Učitavam..." style="height:20px;width:50px;"/>
    </ProgressTemplate>
    </asp:UpdateProgress>
    </div>
    <asp:ImageButton ID="scrollOldLeft" runat="server" ImageUrl="~/img/previous.png" 
            Width="70px" onclick="scroll" CssClass="previous"/>
    <asp:ImageButton ID="scrollOldRight" runat="server" ImageUrl="~/img/next.png" 
            Width="70px" onclick="scroll" CssClass="next"/> 
    <div runat="server" id="projekti_stari" style="height:534px;">
    </div>
    </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="scrollOldLeft" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="scrollOldRight" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <div style="margin:auto;clear:both;"></div>
    <div style="text-align:center;">
    <asp:LinkButton ID="showOlder" runat="server" onclick="showAll">Pregledaj sve</asp:LinkButton>
    </div>
    </div>
    </form>
</asp:Content>
