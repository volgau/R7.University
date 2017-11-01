﻿using System.Web.UI.WebControls;
using DotNetNuke.UI.UserControls;

namespace R7.University.Controls
{
    public partial class EditScienceRecords
    {
        protected DropDownList comboScienceRecordType;
        protected TextEditor textDescription;
        protected TextBox textValue1;
        protected TextBox textValue2;
        protected Panel panelDescription;
        protected Panel panelValue1;
        protected Panel panelValue2;
        protected HiddenField hiddenScienceRecordID;
    }
}