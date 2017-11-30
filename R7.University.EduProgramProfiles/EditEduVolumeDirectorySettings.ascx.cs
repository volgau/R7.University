//
//  SettingsEduProgramProfileDirectory.ascx.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2015-2017 Roman M. Yagodin
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Affero General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Affero General Public License for more details.
//
//  You should have received a copy of the GNU Affero General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Linq;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using R7.Dnn.Extensions.ControlExtensions;
using R7.Dnn.Extensions.Utilities;
using R7.Dnn.Extensions.ViewModels;
using R7.University.ControlExtensions;
using R7.University.EduProgramProfiles.Models;
using R7.University.Models;
using R7.University.Modules;
using R7.University.Queries;
using R7.University.ViewModels;

namespace R7.University.EduProgramProfiles
{
    public partial class EditEduVolumeDirectorySettings: UniversityModuleSettingsBase<EduVolumeDirectorySettings>
    {
        #region Model context

        private UniversityModelContext modelContext;
        protected UniversityModelContext ModelContext
        {
            get { return modelContext ?? (modelContext = new UniversityModelContext ()); }
        }

        public override void Dispose ()
        {
            if (modelContext != null) {
                modelContext.Dispose ();
            }

            base.Dispose ();
        }

        #endregion

        ViewModelContext viewModelContext;
        protected ViewModelContext ViewModelContext
        {
            get { return viewModelContext ?? (viewModelContext = new ViewModelContext (this)); }
        }

        protected override void OnInit (EventArgs e)
        {
            base.OnInit (e);

            // TODO: Use EduVolumeDirectoryMode
            // fill display modes dropdown
            comboMode.DataSource = EnumViewModel<EduProgramProfileDirectoryMode>.GetValues (ViewModelContext, true);
            comboMode.DataBind ();

            // fill division levels
            radioDivisionLevel.DataSource = EnumViewModel<DivisionLevel>.GetValues (ViewModelContext, false);
            radioDivisionLevel.DataBind ();

            radioTimeToLearnDisplayMode.DataSource = EnumViewModel<TimeToLearnDisplayMode>.GetValues (ViewModelContext, false);
            radioTimeToLearnDisplayMode.DataBind ();

            // bind divisions
            divisionSelector.DataSource = new FlatQuery<DivisionInfo> (ModelContext).ListOrderBy (d => d.Title);
            divisionSelector.DataBind ();

            // fill edulevels list
            foreach (var eduLevel in new EduLevelQuery (ModelContext).List ()) {
                listEduLevels.AddItem (FormatHelper.FormatShortTitle (eduLevel.ShortTitle, eduLevel.Title), eduLevel.EduLevelID.ToString ());
            }
        }

        /// <summary>
        /// Handles the loading of the module setting for this control
        /// </summary>
        public override void LoadSettings ()
        {
            try {
                if (!IsPostBack) {

                    radioDivisionLevel.SelectByValue (Settings.DivisionLevel.ToString ());
                    radioTimeToLearnDisplayMode.SelectByValue (Settings.TimeToLearnDisplayMode.ToString ());
                    divisionSelector.DivisionId = Settings.DivisionId;
                    comboMode.SelectByValue (Settings.Mode);

                    // check edulevels list items
                    foreach (var eduLevelId in Settings.EduLevels) {
                        var item = listEduLevels.Items.FindByValue (eduLevelId.ToString ());
                        if (item != null) {
                            item.Selected = true;
                        }
                    }
                }
            }
            catch (Exception ex) {
                Exceptions.ProcessModuleLoadException (this, ex);
            }
        }

        /// <summary>
        /// handles updating the module settings for this control
        /// </summary>
        public override void UpdateSettings ()
        {
            try {
                EduProgramProfileDirectoryMode mode;
                Settings.Mode = Enum.TryParse<EduProgramProfileDirectoryMode> (comboMode.SelectedValue, out mode) ? 
                    (EduProgramProfileDirectoryMode?) mode : null;

                Settings.EduLevelIds = listEduLevels.Items.AsEnumerable ().Where (i => i.Selected).Select (i => int.Parse (i.Value));
                Settings.DivisionId = divisionSelector.DivisionId;
                Settings.DivisionLevel = (DivisionLevel) Enum.Parse (typeof (DivisionLevel), radioDivisionLevel.SelectedValue, true);
                Settings.TimeToLearnDisplayMode = (TimeToLearnDisplayMode) Enum.Parse (typeof (TimeToLearnDisplayMode), radioTimeToLearnDisplayMode.SelectedValue, true);

                SettingsRepository.SaveSettings (ModuleConfiguration, Settings);

                ModuleController.SynchronizeModule (ModuleId);

                CacheHelper.RemoveCacheByPrefix ("//r7_University/Modules/EduVolumeDirectory?ModuleId=" + ModuleId);
            }
            catch (Exception ex) {
                Exceptions.ProcessModuleLoadException (this, ex);
            }
        }
    }
}

