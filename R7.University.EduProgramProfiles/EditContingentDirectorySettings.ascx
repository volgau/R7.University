﻿<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="EditContingentDirectorySettings.ascx.cs" Inherits="R7.University.EduProgramProfiles.EditContingentDirectorySettings" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<%@ Register TagPrefix="controls" TagName="DivisionSelector" Src="~/DesktopModules/MVC/R7.University/R7.University.Controls/DivisionSelector.ascx" %>

<dnn:DnnCssInclude runat="server" FilePath="~/DesktopModules/MVC/R7.University/R7.University/assets/css/admin.css" Priority="200" />
<dnn:DnnCssInclude runat="server" FilePath="~/DesktopModules/MVC/R7.University/R7.University.EduProgramProfiles/admin.css" Priority="200" />

<div class="dnnForm dnnClear eduvolume-directory-settings">
	<asp:Panel id="panelGeneralSettings" runat="server">
        <h2 class="dnnFormSectionHead"><a href="#"><%: LocalizeString ("GeneralSettings.Section") %></a></h2>
    	<fieldset>
    		<div class="dnnFormItem">
                <dnn:Label id="labelDivision" runat="server" ControlName="divisionSelector" />
                <controls:DivisionSelector id="divisionSelector" runat="server" />
            </div>
            <div class="dnnFormItem">
                <dnn:Label id="labelDivisionLevel" runat="server" ControlName="radioDivisionLevel" />
                <asp:RadioButtonList id="radioDivisionLevel" runat="server"
                    DataValueField="Value"
                    DataTextField="ValueLocalized"
                    RepeatDirection="Horizontal"
                    CssClass="dnn-form-control" />
            </div>
            <div class="dnnFormItem">
                <dnn:Label id="labelMode" runat="server" ControlName="comboMode" />
                <asp:DropDownList id="comboMode" runat="server"
                    DataValueField="Value"
                    DataTextField="ValueLocalized" />
            </div>
            <div class="dnnFormItem">
                <dnn:Label id="labelEduLevels" runat="server" ControlName="listEduLevels" />
                <asp:CheckBoxList id="listEduLevels" runat="server" CssClass="dnn-form-control" />
            </div>
		</fieldset>
	</asp:Panel>
</div>

<script type="text/javascript">
(function($, Sys) {
    function setupModule() {
        $(".dnn-select2").select2();
    };
    $(document).ready(function() {
        setupModule();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function() {
            setupModule();
        });
    });
} (jQuery, window.Sys));
</script>
