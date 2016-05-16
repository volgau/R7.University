﻿using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DotNetNuke.UI.UserControls;
using DotNetNuke.UI.WebControls;
using DotNetNuke.Web.UI.WebControls;
using R7.University;

namespace R7.University.Employee
{
	public partial class EditEmployee
	{
		protected ModuleAuditControl ctlAudit;
		protected LinkButton buttonUpdate;
		protected LinkButton buttonDelete;
		protected HyperLink linkCancel;
		protected LabelControl labelPhoto;
		protected DnnFilePickerUploader pickerPhoto;
        protected LinkButton buttonPhotoLookup;
		protected LabelControl labelUser;
		protected DnnUrlControl urlUser;
		protected LabelControl labelLastName;
		protected TextBox textLastName;
		protected LabelControl labelFirstName;
		protected TextBox textFirstName;
		protected LabelControl labelOtherName;
		protected TextBox textOtherName;
		protected LabelControl labelPhone;
		protected TextBox textPhone;
		protected LabelControl labelCellPhone;
		protected TextBox textCellPhone;
		protected LabelControl labelFax;
		protected TextBox textFax;
		protected LabelControl labelEmail;
		protected TextBox textEmail;
		protected LabelControl labelSecondaryEmail;
		protected TextBox textSecondaryEmail;
		protected LabelControl labelWebSite;
		protected TextBox textWebSite;
		protected LabelControl labelWebSiteLabel;
		protected TextBox textWebSiteLabel;
		protected LabelControl labelMessenger;
		protected TextBox textMessenger;
		protected LabelControl labelCustomWorkingHours;
		protected TextBox textWorkingHours;
		protected CheckBox checkAddToVocabulary;
		protected LabelControl labelWorkingHours;
		protected DropDownList comboWorkingHours;
		protected LabelControl labelWorkingPlace;
		protected TextBox textWorkingPlace;
		protected LabelControl labelExperienceYears;
		protected TextBox textExperienceYears;
		protected LabelControl labelExperienceYearsBySpec;
		protected TextBox textExperienceYearsBySpec;
		protected TextEditor textBiography;
        protected DnnDateTimePicker datetimeStartDate;
        protected DnnDateTimePicker datetimeEndDate;
		protected LinkButton buttonUserLookup;
		protected CheckBox checkIncludeDeletedUsers;
		protected TextBox textUserLookup;
		protected Label labelUserNames;
		protected LabelControl labelUserLookup;
		protected DropDownList comboUsers;

        protected GridView gridOccupiedPositions;
		protected LabelControl labelPositions;
		protected LabelControl labelDivisions;
        protected DropDownList comboPositions;
		protected LabelControl labelPositionTitleSuffix;
		protected TextBox textPositionTitleSuffix;
		protected DnnTreeView treeDivisions;
		protected LabelControl labelIsPrime;
		protected CheckBox checkIsPrime;
		protected LinkButton buttonAddPosition;
		protected LinkButton buttonUpdatePosition;
		protected LinkButton buttonCancelEditPosition;
		protected HiddenField hiddenOccupiedPositionItemID;

		protected GridView gridAchievements;
		protected DropDownList comboAchievementTypes;
		protected LabelControl labelYearBegin;
		protected TextBox textYearBegin;
		protected LabelControl labelYearEnd;
		protected TextBox textYearEnd;
		protected LinkButton buttonAddAchievement;
		protected LinkButton buttonUpdateAchievement;
		protected LinkButton buttonCancelEditAchievement;

		protected LabelControl labelAchievementTitle;
		protected TextBox textAchievementTitle;
		protected LabelControl labelAchievementShortTitle;
		protected TextBox textAchievementShortTitle;
		protected LabelControl labelAchievementDescription;
		protected TextBox textAchievementDescription;
		protected LabelControl labelIsTitle;
		protected CheckBox checkIsTitle;
		protected LabelControl labelDocumentURL;
		protected DnnUrlControl urlDocumentURL;
		protected HiddenField hiddenAchievementItemID;
		protected LabelControl labelAchievements;
        protected DropDownList comboAchievement;
		protected Panel panelAchievementTitle;
		protected Panel panelAchievementShortTitle;
		protected Panel panelAchievementTypes;
		protected LabelControl labelAchievementTitleSuffix;
		protected TextBox textAchievementTitleSuffix;

        protected GridView gridDisciplines;
        protected TextBox textDisciplines;
        protected DropDownList comboEduLevel;
        protected DropDownList comboEduProgramProfile;
        protected LinkButton buttonAddDisciplines;
        protected LinkButton buttonUpdateDisciplines;
        protected LinkButton buttonCancelEditDisciplines;
        protected HiddenField hiddenDisciplinesItemID;
        protected CustomValidator valEduProgramProfile;
	}
}
