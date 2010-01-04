<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CuyAccordion.ascx.cs" Inherits="Cuyahoga.Modules.CuyAccordion.Web.CuyAccordion" %>
<asp:Label ID="lblMessages" runat="server" Text=""></asp:Label>
<asp:Panel ID="pnlAccordion" CssClass="basic" runat="server">
    <asp:Repeater ID="rptAccordion" runat="server">
        <HeaderTemplate>
        </HeaderTemplate>
        <ItemTemplate>
	        <h3><%# Eval("Title") %></h3>
            <div><%# Eval("HTML") %></div>
        </ItemTemplate>
        <FooterTemplate>
        </FooterTemplate>
    </asp:Repeater>
</asp:Panel>