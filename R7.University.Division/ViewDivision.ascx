﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewDivision.ascx.cs" Inherits="R7.University.Division.ViewDivision" %>
<%@ Register TagPrefix="dnn" TagName="jQuery" Src="~/Admin/Skins/jQuery.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

<dnn:jQuery runat="server" jQueryUI="true" DnnjQueryPlugins="true" />
<dnn:DnnJsInclude runat="server" FilePath="~/DesktopModules/R7.University/R7.University.Division/js/module.js" />
<asp:Panel id="panelDivision" runat="server" CssClass="university-division">
	<asp:Label id="labelTitle" runat="server" CssClass="_section" />
	<asp:HyperLink id="linkHomePage" runat="server" CssClass="_section" />

	<asp:HyperLink id="linkSearchByTerm" runat="server" resourcekey="linkSearchByTerm" CssClass="_section tag" />

	<div class="_section">
		<asp:HyperLink id="linkDocumentUrl" runat="server" CssClass="email" />
	</div>	 

	<div class="_section">
		<asp:Label id="labelPhone" runat="server" CssClass="_label" />
		<asp:Label id="labelFax" runat="server" CssClass="_label" />
	</div>

	<div class="_section">
		<asp:HyperLink id="linkEmail" runat="server" CssClass="_label email" />
		<asp:HyperLink id="linkSecondaryEmail" runat="server" CssClass="_label email" />
		<asp:HyperLink id="linkWebSite" runat="server" Target="_blank" CssClass="_label email" />
	</div>

	<div class="_section">
		<asp:Label id="labelLocation" runat="server" CssClass="_label" />
		<asp:Label id="labelWorkingHours" runat="server" CssClass="_label" />
	</div>

    <asp:Panel id="panelSubDivisions" runat="server" CssClass="panel-subdivisions">
        <p>
            <asp:Label id="labelSubDivisions" runat="server" resourcekey="labelSubDivisions" />
        </p>
        <asp:Repeater id="repeatSubDivisions" runat="server">
    		<HeaderTemplate><ul></HeaderTemplate>
    		<ItemTemplate>
    			<li class='<%# DataBinder.Eval(Container.DataItem, "CssClass") %>'>
                    <%# DataBinder.Eval(Container.DataItem, "HomePageLink") %>
    			</li>
    		</ItemTemplate>
    		<FooterTemplate></ul></FooterTemplate>
    	</asp:Repeater>
    </asp:Panel>
    <asp:HyperLink id="linkBarcode" runat="server" resourcekey="linkBarcode"
        CssClass="btn btn-default link-barcode" onclick="showDivisionBarcodeDialog(this)" />
    <div class="dialog-division-barcode" id="dialog-division-barcode-<%= ModuleId %>">
        <asp:Image id="imageBarcode" runat="server" resourcekey="imageBarcode" Style="margin-top:10px" />
    </div>
</asp:Panel>