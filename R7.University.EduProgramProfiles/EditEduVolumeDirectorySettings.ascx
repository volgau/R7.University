<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="EditEduVolumeDirectorySettings.ascx.cs" Inherits="R7.University.EduProgramProfiles.EditEduVolumeDirectorySettings" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="JavaScriptLibraryInclude" Src="~/admin/Skins/JavaScriptLibraryInclude.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<%@ Register TagPrefix="controls" TagName="DivisionSelector" Src="~/DesktopModules/MVC/R7.University/R7.University.Controls/DivisionSelector.ascx" %>

<dnn:JavaScriptLibraryInclude runat="server" Name="Select2" />
<dnn:DnnCssInclude runat="server" FilePath="~/Resources/Libraries/Select2/04_00_13/css/select2.min.css" />

<dnn:DnnCssInclude runat="server" FilePath="~/DesktopModules/MVC/R7.University/R7.University/css/admin.css" Priority="200" />
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
    <h2 class="dnnFormSectionHead"><a href="#"><%: LocalizeString ("DisplaySettings.Section") %></a></h2>
    <fieldset>
        <div class="dnnFormItem">
            <dnn:Label id="labelShowTimeToLearnMonths" runat="server" ControlName="checkShowTimeToLearnMonths" />
            <asp:CheckBox id="checkShowTimeToLearnMonths" runat="server" />
        </div>
		<div class="dnnFormItem">
            <dnn:Label id="labelShowTimeToLearnHours" runat="server" ControlName="checkShowTimeToLearnHours" />
            <asp:CheckBox id="checkShowTimeToLearnHours" runat="server" />
        </div>
    </fieldset>
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
