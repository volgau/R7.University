﻿//
// ViewEmployeeDetails.ascx.cs
//
// Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
// Copyright (c) 2014-2015 
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Linq;
using System.Data;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using R7.University;
using R7.University.ControlExtensions;

namespace R7.University.Employee
{
    public partial class ViewEmployeeDetails : EmployeePortalModuleBase, IActionable
	{
		#region Properties

        protected bool InPopup 
        {
            get { return Request.QueryString ["popup"] != null; }
        }

        protected bool InViewModule 
        {
            get { return ModuleConfiguration.ModuleDefinition.DefinitionName == "R7.University.EmployeeDetails"; }
        }

        private EmployeeInfo _employee;

        public EmployeeInfo Employee
        {
            get
            {
                if (_employee == null)
                {
                    if (InViewModule)
                    {
                        // use module settings
                        _employee = GetEmployee ();
                    }
                    else
                    {
                        // try get employee id from querystring first
                        var employeeId = Utils.ParseToNullableInt (Request.QueryString ["employee_id"]);

                        if (employeeId != null)
                        {
                            // get employee by querystring param
                            _employee = EmployeeController.Get<EmployeeInfo> (employeeId.Value);
                        }
                        else if (ModuleConfiguration.ModuleDefinition.DefinitionName == "R7.University.Employee")
                        {
                            // if employee id is not in the querystring, 
                            // use module settings (Employee module only)
                            _employee = GetEmployee ();
                        }
                    }
                }

                return _employee;
            }
        }

		#endregion

        #region IActionable implementation

        public ModuleActionCollection ModuleActions
        {
            get
            {
                // create a new action to add an item, this will be added 
                // to the controls dropdown menu
                var actions = new ModuleActionCollection ();

                actions.Add (
                    GetNextActionID (), 
                    LocalizeString ("AddEmployee.Action"),
                    ModuleActionType.AddContent,
                    "",
                    "", 
                    Utils.EditUrl (this, "EditEmployee"),
                    false, 
                    DotNetNuke.Security.SecurityAccessLevel.Edit,
                    Employee == null, 
                    false
                );

                if (Employee != null)
                {
                    // otherwise, add "edit" action
                    actions.Add (
                        GetNextActionID (), 
                        LocalizeString ("EditEmployee.Action"),
                        ModuleActionType.EditContent, 
                        "", 
                        "", 
                        Utils.EditUrl (this, "EditEmployee", "employee_id", Employee.EmployeeID.ToString ()),
                        false, 
                        DotNetNuke.Security.SecurityAccessLevel.Edit,
                        true, 
                        false
                    );

                    actions.Add (
                        GetNextActionID (), 
                        LocalizeString ("VCard.Action"),
                        ModuleActionType.ContentOptions, 
                        "", 
                        "", 
                        Utils.EditUrl (this, "VCard", "employee_id", Employee.EmployeeID.ToString ()),
                        false,
                        DotNetNuke.Security.SecurityAccessLevel.View,
                        true,
                        true
                    );
                }

                return actions;
            }
        }

        #endregion

		#region Handlers

		/// <summary>
		/// Handles Init event for a control
		/// </summary>
		/// <param name="e">Event args.</param>
		protected override void OnInit (EventArgs e)
		{
			base.OnInit (e);

            if (InPopup)
            {
                linkReturn.Attributes.Add ("onclick", "javascript:return " +
                    UrlUtils.ClosePopUp (refresh: false, url: "", onClickEvent: true));
            }
            else if (InViewModule)
            {
                linkReturn.Visible = false;
            }
            else
            {
                linkReturn.NavigateUrl = Globals.NavigateURL ();
            }

            gridEduPrograms.LocalizeColumns (LocalResourceFile);
		}

		/// <summary>
		/// Handles Load event for a control
		/// </summary>
		/// <param name="e">Event args.</param>
		protected override void OnLoad (EventArgs e)
		{
			base.OnLoad (e);

			try
			{
				if (!IsPostBack)
				{
					// can we display module content?
                    var displayContent = Employee != null && (IsEditable || Employee.IsPublished);

                    // can we display something (content or messages)?
                    var displaySomething = IsEditable || (Employee != null && Employee.IsPublished);

                    // something went wrong in popup mode - reload page
                    if (InPopup && !displaySomething)
                    {
                        ReloadPage ();
                        return;
                    }

                    if (InViewModule)
                    {
                        // display module only in edit mode
                        // only if we have published data to display
                        ContainerControl.Visible = displaySomething;
                    }

                    // display messages
                    if (IsEditable)
                    {
                        if (Employee == null)
                        {
                            // employee isn't set or not found
                            Utils.Message (this, "NothingToDisplay.Text", MessageType.Info, true);
                        }
                        else if (!Employee.IsPublished)
                        {
                            // employee don't published
                            Utils.Message (this, "EmployeeNotPublished.Text", MessageType.Warning, true);
                        }
                    }

                    panelEmployeeDetails.Visible = displayContent;

                    if (displayContent)
                    {
                        Display (Employee);
						
                        // don't show action buttons in view module
                        if (!InViewModule)
                        {
                            // show vCard button only for editors
                            if (IsEditable)
							{
								linkVCard.Visible = true;
                                linkVCard.NavigateUrl = Utils.EditUrl (this, "VCard", "employee_id", 
                                    Employee.EmployeeID.ToString ());
                            }

                            // show edit button only for editors or superusers (in popup)
                            if (IsEditable || UserInfo.IsSuperUser) 
                            {
                                linkEdit.Visible = true;
                                linkEdit.NavigateUrl = Utils.EditUrl (this, "EditEmployee", "employee_id", 
                                    Employee.EmployeeID.ToString ());
							}
                        }
                    }

				} // if (!IsPostBack)
			}
			catch (Exception ex)
			{
				Exceptions.ProcessModuleLoadException (this, ex);
			}
		}

		#endregion

        /// <summary>
        /// Reloads the page.
        /// </summary>
        protected void ReloadPage ()
        {
            // TODO: Move to extension methods
            Response.Redirect (Globals.NavigateURL (), false);
            Context.ApplicationInstance.CompleteRequest();
        }

		protected void Display (EmployeeInfo employee)
		{
        	var fullname = employee.FullName;

            if (InPopup)
            {
                // set popup title to employee name
                ((DotNetNuke.Framework.CDefault) this.Page).Title = fullname;
            }
            else if (InViewModule)
            {
                if (EmployeeSettings.AutoTitle)
                    UpdateModuleTitle (employee.FullName);
            }
            else
            {
                // display employee name in label
                literalFullName.Text = "<h2>" + fullname + "</h2>";
            }

			// occupied positions
			var occupiedPositions = EmployeeController.GetObjects<OccupiedPositionInfoEx> (
                "WHERE [EmployeeID] = @0 ORDER BY [IsPrime] DESC, [PositionWeight] DESC", employee.EmployeeID);
            
			if (occupiedPositions.Any ())
			{
				repeaterPositions.DataSource = OccupiedPositionInfoEx.GroupByDivision (occupiedPositions);
				repeaterPositions.DataBind ();
			}
			else
				repeaterPositions.Visible = false;
			
            EmployeePhotoLogic.Bind (employee, imagePhoto, EmployeeSettings.PhotoWidth);
					
			// Phone
			if (!string.IsNullOrWhiteSpace (employee.Phone))
				labelPhone.Text = employee.Phone;
			else
				labelPhone.Visible = false;

			// CellPhome
			if (!string.IsNullOrWhiteSpace (employee.CellPhone))
				labelCellPhone.Text = employee.CellPhone;
			else
				labelCellPhone.Visible = false;

			// Fax
			if (!string.IsNullOrWhiteSpace (employee.Fax))
				labelFax.Text = string.Format (Localization.GetString ("Fax.Format", LocalResourceFile), employee.Fax);
			else
				labelFax.Visible = false;

			// Messenger
			if (!string.IsNullOrWhiteSpace (employee.Messenger))
				labelMessenger.Text = employee.Messenger;
			else
				labelMessenger.Visible = false;

			// Working place and Hours
			var workingPlaceAndHours = Utils.FormatList (", ", employee.WorkingPlace, employee.WorkingHours);
			if (!string.IsNullOrWhiteSpace (workingPlaceAndHours))
				labelWorkingPlaceAndHours.Text = workingPlaceAndHours;
			else
				labelWorkingPlaceAndHours.Visible = false;

			// WebSite
			if (!string.IsNullOrWhiteSpace (employee.WebSite))
			{
				linkWebSite.NavigateUrl = employee.FormatWebSiteUrl;
				linkWebSite.Text = employee.FormatWebSiteLabel;
			}
			else
				linkWebSite.Visible = false;

			// Email
			if (!string.IsNullOrWhiteSpace (employee.Email))
			{
				linkEmail.NavigateUrl = "mailto:" + employee.Email;
				linkEmail.Text = employee.Email;
			}
			else
				linkEmail.Visible = false;

			// Secondary email
			if (!string.IsNullOrWhiteSpace (employee.SecondaryEmail))
			{
				linkSecondaryEmail.NavigateUrl = "mailto:" + employee.SecondaryEmail;
				linkSecondaryEmail.Text = employee.SecondaryEmail;
			}
			else
				linkSecondaryEmail.Visible = false;

			// Profile link
			if (!Utils.IsNull<int> (employee.UserID))
				linkUserProfile.NavigateUrl = Globals.UserProfileURL (employee.UserID.Value);
			else
				linkUserProfile.Visible = false;

			// about
			if (!string.IsNullOrWhiteSpace (employee.Biography))
				litAbout.Text = Server.HtmlDecode (employee.Biography);
			else
			{
				// hide entire About tab
				linkAbout.Visible = false;
			}
			
            Experience (employee);
            EduPrograms (employee);
            Barcode (employee);
		}

        void EduPrograms (EmployeeInfo employee)
        {
            // get employee edu programs
            var disciplines = EmployeeController.GetObjects<EmployeeDisciplineInfoEx> (
                "WHERE [EmployeeID] = @0", employee.EmployeeID).OrderBy (d => d.Code);

            if (disciplines.Any ())
            {
                gridEduPrograms.DataSource = DataTableConstructor.FromIEnumerable (disciplines);
                gridEduPrograms.DataBind ();
            }
            else
            {
                linkDisciplines.Visible = false;
            }
        }

        void Barcode (EmployeeInfo employee)
		{
            linkBarcode.Attributes.Add ("data-module-id", ModuleId.ToString ());
            linkBarcode.Attributes.Add ("data-dialog-title", employee.FullName);

			// barcode image
			const int barcodeWidth = 192;
            imageBarcode.ImageUrl = R7.University.Utilities.UrlUtils.FullUrl (string.Format (
                "/imagehandler.ashx?barcode=1&width={0}&height={1}&type=qrcode&encoding=UTF-8&content={2}",
				barcodeWidth, barcodeWidth, 
				Server.UrlEncode (employee.VCard.ToString ()
						.Replace ("+", "%2b")) // fix for "+" signs in phone numbers
            ));

			imageBarcode.ToolTip = LocalizeString ("imageBarcode.ToolTip");
			imageBarcode.AlternateText = LocalizeString ("imageBarcode.AlternateText");
		}

		void Experience (EmployeeInfo employee)
		{
			// experience years
			var exp1 = false;
			var exp2 = false;
			var noExpYears = false;
			
			// Общий стаж работы (лет): {0}
			// Общий стаж работы по специальности (лет): {0}
			// Общий стаж работы (лет): {0}, из них по специальности: {1}
			
			if (employee.ExperienceYears != null && employee.ExperienceYears.Value > 0)
				exp1 = true;
			
			if (employee.ExperienceYearsBySpec != null && employee.ExperienceYearsBySpec.Value > 0)
				exp2 = true;
			
			if (exp1 && !exp2)
			{
				labelExperienceYears.Text = string.Format (
					LocalizeString ("ExperienceYears.Format1"), employee.ExperienceYears.Value);
			}
			else if (!exp1 && exp2)
			{
				labelExperienceYears.Text = string.Format (
					LocalizeString ("ExperienceYears.Format2"), employee.ExperienceYearsBySpec);
			}
			else if (exp1 && exp2)
			{
				labelExperienceYears.Text = string.Format (
					LocalizeString ("ExperienceYears.Format3"), 
					employee.ExperienceYears.Value, employee.ExperienceYearsBySpec);
			}
			else
			{
				// hide label for experience years
				labelExperienceYears.Visible = false;
				
				// about to hide Experience tab
				noExpYears = true;
			}

			// get all empoyee achievements
			var achievements = EmployeeController.GetObjects<EmployeeAchievementInfo> (CommandType.Text, 
                "SELECT * FROM dbo.vw_University_EmployeeAchievements WHERE [EmployeeID] = @0", employee.EmployeeID);
	
			// employee titles
            var titles = achievements.Where (ach => ach.IsTitle)
                .Select (ach => Utils.FirstCharToLower (ach.Title));
            
			var strTitles = Utils.FormatList (", ", titles);
			if (!string.IsNullOrWhiteSpace (strTitles))
				labelAcademicDegreeAndTitle.Text = Utils.FirstCharToUpper (strTitles);
			else
				labelAcademicDegreeAndTitle.Visible = false;

			// get only experience-related achievements
            var experiences = achievements
                .Where (ach => ach.AchievementType == AchievementType.Education ||
                    ach.AchievementType == AchievementType.AcademicDegree ||
                    ach.AchievementType == AchievementType.Training ||
                    ach.AchievementType == AchievementType.Work)
                .OrderByDescending (exp => exp.YearBegin);

			if (experiences.Any ())
			{
				gridExperience.DataSource = AchievementsDataTable (experiences);
				gridExperience.DataBind ();
			}
			else if (noExpYears)
			{
				// hide experience tab
				linkExperience.Visible = false;
			}
		
			// get all other achievements
            achievements = achievements
                .Where (ach => ach.AchievementType != AchievementType.Education &&
                    ach.AchievementType != AchievementType.AcademicDegree &&
                    ach.AchievementType != AchievementType.Training &&
                    ach.AchievementType != AchievementType.Work)
                .OrderByDescending (ach => ach.YearBegin);
			
            if (achievements.Any ())
			{
				gridAchievements.DataSource = AchievementsDataTable (achievements);
				gridAchievements.DataBind ();
			}
			else
			{	
				// hide achievements tab
				linkAchievements.Visible = false;
			}
		}

		private DataTable AchievementsDataTable (IEnumerable<EmployeeAchievementInfo> achievements)
		{
			var dt = new DataTable ();
			DataRow dr;
			
			dt.Columns.Add (new DataColumn (LocalizeString ("Years.Column"), typeof(string)));
			dt.Columns.Add (new DataColumn (LocalizeString ("Title.Column"), typeof(string)));
			dt.Columns.Add (new DataColumn (LocalizeString ("AchievementType.Column"), typeof(string)));
			dt.Columns.Add (new DataColumn (LocalizeString ("DocumentUrl.Column"), typeof(string)));
		
			// add description column (no need to localize as it's hidden)
            dt.Columns.Add (new DataColumn ("Description.Column", typeof(string)));
					
			foreach (DataColumn column in dt.Columns)
				column.AllowDBNull = true;

			var atTheMoment = LocalizeString ("AtTheMoment.Text");

			foreach (var achievement in achievements)
			{
				var col = 0;
				dr = dt.NewRow ();
				dr [col++] = achievement.FormatYears.Replace ("{ATM}", atTheMoment);
				dr [col++] = achievement.Title + " " + achievement.TitleSuffix;
				dr [col++] = LocalizeString (AchievementTypeInfo.GetResourceKey (achievement.AchievementType));
				dr [col++] = achievement.DocumentURL; 
				dr [col++] = achievement.Description;
					
				dt.Rows.Add (dr);
			}

			return dt;
		}

		protected void gridExperience_RowDataBound (object sender, GridViewRowEventArgs e)
		{
			// hide description column
			e.Row.Cells [4].Visible = false;
			
			// exclude header
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
                var description = e.Row.Cells [4].Text;
                if (!string.IsNullOrWhiteSpace (Server.HtmlDecode (description)))
                {
                    // convert to hyperlink
                    e.Row.Cells [1].Text = string.Format ("<a href=\"#\" "
                        + "data-module-id=\"{2}\" "
                        + "data-description=\"{1}\" " 
                        + "data-dialog-title=\"{0}\" "
                        + "onclick=\"showEmployeeAchievementDescriptionDialog(this)\">{0}</a>", 
                        e.Row.Cells [1].Text, description, ModuleId);
                }

                // make link to the document
				// WTF: empty DocumentURL's cells contains non-breakable spaces?
                var documentUrl = Server.HtmlDecode (e.Row.Cells [3].Text.Replace ("&nbsp;", ""));
                if (!string.IsNullOrWhiteSpace (documentUrl))
                {
                    e.Row.Cells [3].Text = string.Format ("<a href=\"{0}\" target=\"_blank\">{1}</a>", 
                        R7.University.Utilities.UrlUtils.LinkClickIdnHack (documentUrl, TabId, ModuleId),
                        LocalizeString ("DocumentUrl.Text"));
                }
			}
		}

		protected void repeaterPositions_ItemDataBound (object sender, RepeaterItemEventArgs e)
		{
            RepeaterPositionsLogic.ItemDataBound (this, sender, e);
		}
	}
	// class
}
// namespace

