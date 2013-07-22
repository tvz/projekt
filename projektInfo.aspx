<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="projektInfo.aspx.cs" Inherits="projektInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<form id="Form1" runat="server">

<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</ajaxToolkit:ToolkitScriptManager>

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
        <div id="projectDisplay" runat = "server">
            <div id="header" runat="server" style="margin-bottom:3%"></div>
            <div id="multimedia" runat="server" style="float:left"></div>
            <div id="info" runat="server" style="float:left; margin-left:7%; height:380px; width:500px;overflow:auto;"></div>
            <div style="margin:auto; clear:both;"></div>
            <div id="long_description" runat="server"></div>
            <asp:Panel ID="PanelReport" runat="server" CssClass="gumb4"></asp:Panel><br />
            <asp:Panel ID="PanelReportOptions" runat="server">
            Razlog prijave:
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" 
                    ChildrenAsTriggers="False">
                <ContentTemplate>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                    onselectedindexchanged="RadioButtonList1_SelectedIndexChanged" 
                    AutoPostBack="True">
                    <asp:ListItem Value="0"><a href="" target="_blank">Neprimjeren sadržaj</a> u opisu ili komentarima projekta</asp:ListItem>
                    <asp:ListItem Value="1">Autor ovog projekta <a href="" target="_blank">spamma</a> moj e-mail pretinac</asp:ListItem>
                    <asp:ListItem Value="2">Autor ovog projekta dijeli <a href="" target="_blank">neprimjerene nagrade</a>.</asp:ListItem>
                </asp:RadioButtonList>
                <br />
                <asp:Label ID="Label1" runat="server" Visible="false" Text="Obrazložite svoju prijavu:"></asp:Label>
                <br />
                <asp:TextBox ID="TextBoxObjasnjenje" runat="server" Height="100px" 
                    TextMode="MultiLine" Width="450px" Visible="false"></asp:TextBox> <br />
                <asp:Button ID="ButtonPrijavi" runat="server" Text="PRIJAVI" CssClass="gumb4" 
                    onclick="ButtonPrijavi_Click" Visible="false" />            
            </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="RadioButtonList1" 
                            EventName="SelectedIndexChanged" />
                    </Triggers>
            </asp:UpdatePanel>
              
            </asp:Panel>
        </div>

        <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" 
            runat="server" CollapseControlID="PanelReport" Collapsed="True" 
            CollapsedText="Prijavi projekt" ExpandControlID="PanelReport" 
            ExpandedText="Odustani" TargetControlID="PanelReportOptions" 
            TextLabelID="PanelReport">
        </ajaxToolkit:CollapsiblePanelExtender>

        <script type="text/javascript">
            function pageLoad(sender, args) {
                smoothAnimation();
            }

            function smoothAnimation() {
                var collPanel = $find('<%= CollapsiblePanelExtender1.ClientID %>');
                collPanel._animation._fps = 60;
                collPanel._animation._duration = .70;
            }
</script>
        <%-- <div id="comments" runat="server">
            <div id="disqus_thread"></div>
        </div> --%>
    </asp:View>
</asp:MultiView>
</form>
</asp:Content>

