﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewEduProgramProfileDirectory.ascx.cs" 
    Inherits="R7.University.EduProgramProfileDirectory.ViewEduProgramProfileDirectory" %>
<div class="dnnForm dnnClear eduprogramprofile-directory">
    <asp:MultiView id="mviewEduProgramProfileDirectory" runat="server">
        <asp:View runat="server">
            <asp:Label runat="server" resourcekey="NotConfigured.Text" CssClass="dnnFormMessage dnnFormInfo" />
        </asp:View>
        <asp:View runat="server">
            <div class="table-responsive">
                <asp:GridView id="gridEduProgramProfileObrnadzorEduForms" runat="server" AutoGenerateColumns="false" 
                        UseAccessibleHeader="true"
                        CssClass="table table-bordered table-stripped table-hover grid-eduprogramprofile-obrnadzor-eduforms"
                        GridLines="None" OnRowDataBound="gridEduProgramProfileObrnadzorEduForms_RowDataBound"
                        OnRowCreated="gridEduProgramProfileObrnadzorEduForms_RowCreated"
                        >
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink id="linkEdit" runat="server">
                                    <asp:Image id="iconEdit" runat="server" />
                                </asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="IndexString" />
                        <asp:BoundField DataField="Code" HtmlEncode="false" />
                        <asp:BoundField DataField="Title" />
                        <asp:BoundField DataField="EduLevelString" HtmlEncode="false" />
                        <asp:BoundField DataField="TimeToLearnFullTimeString" HtmlEncode="false" HeaderText="TimeToLearnFullTime" />
                        <asp:BoundField DataField="TimeToLearnPartTimeString" HtmlEncode="false" HeaderText="TimeToLearnPartTime" />
                        <asp:BoundField DataField="TimeToLearnExtramuralString" HtmlEncode="false" HeaderText="TimeToLearnExtramural" />
                        <asp:BoundField DataField="AccreditedToDateString" HtmlEncode="false" HeaderText="AccreditedToDate" />
                        <asp:BoundField DataField="CommunityAccreditedToDate" DataFormatString="{0:d}" HeaderText="CommunityAccreditedToDate" />
                    </Columns>
                </asp:GridView>
            </div>
        </asp:View>
        <asp:View runat="server">
            <div class="table-responsive">
                <asp:GridView id="gridEduProgramProfileObrnadzorDocuments" runat="server" AutoGenerateColumns="false" 
                        UseAccessibleHeader="true"
                        CssClass="table table-bordered table-stripped table-hover grid-eduprogramprofile-obrnadzor-documents"
                        GridLines="None" OnRowDataBound="gridEduProgramProfileObrnadzorDocuments_RowDataBound">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink id="linkEdit" runat="server">
                                    <asp:Image id="iconEdit" runat="server" />
                                </asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="IndexString" HeaderText="Index" />
                        <asp:BoundField DataField="Code" HtmlEncode="false" />
                        <asp:BoundField DataField="EduProgramLink" HtmlEncode="false" HeaderText="Title" />
                        <asp:BoundField DataField="EduPlanLinks" HtmlEncode="false" HeaderText="EduPlan" />
                        <asp:BoundField DataField="WorkProgramAnnotationLink" HtmlEncode="false" HeaderText="WorkProgramAnnotation" />
                        <asp:BoundField DataField="EduScheduleLinks" HtmlEncode="false" HeaderText="EduSchedule" />
                        <asp:BoundField DataField="WorkProgramOfPracticeLinks" HtmlEncode="false" HeaderText="WorkProgramOfPractice" />
                        <asp:BoundField DataField="EduMaterialLink" HtmlEncode="false" HeaderText="EduMaterial" />
                        <asp:BoundField DataField="LanguagesString" HtmlEncode="false" HeaderText="Languages" />
                        <asp:BoundField DataField="ContingentLink" HtmlEncode="false" HeaderText="Contingent" />
                        <asp:BoundField DataField="ContingentMovementLink" HtmlEncode="false" HeaderText="ContingentMovement" />
                    </Columns>
                </asp:GridView>
            </div>
        </asp:View>
    </asp:MultiView>
</div>