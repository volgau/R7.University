﻿using System;
using System.Web.UI.WebControls;
using System.Linq;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.UI.UserControls;
using R7.University;

namespace R7.University.Launchpad
{
	public partial class SettingsLaunchpad : ModuleSettingsBase
	{
		public void OnInit()
		{
			// fill PageSize combobox
			for (var i = 1; i <= 10; i++)
			{
				var strPageSize = (i * 5).ToString();
				comboPageSize.AddItem(strPageSize, strPageSize);
			}

			// fill tables list
			listTables.Items.Add (new Telerik.Web.UI.RadListBoxItem("Positions", "positions"));
			listTables.Items.Add (new Telerik.Web.UI.RadListBoxItem("Divisions", "divisions"));
			listTables.Items.Add (new Telerik.Web.UI.RadListBoxItem("Employees", "employees"));
		}

		/// <summary>
		/// Handles the loading of the module setting for this control
		/// </summary>
		public override void LoadSettings ()
		{ 
			try {
				if (!IsPostBack) {
					var settings = new LaunchpadSettings (this);

					// TODO: Allow select nearest pagesize value
					comboPageSize.Select (settings.PageSize.ToString(), false);

					// check table list items
					var tableNames = settings.Tables.Split(';');
					foreach (var tableName in tableNames)
					{
						var item = listTables.FindItemByValue(tableName);
						if (item != null) item.Checked = true;
					}

				}
			} catch (Exception ex) {
				Exceptions.ProcessModuleLoadException (this, ex);
			}
		}

		/// <summary>
		/// handles updating the module settings for this control
		/// </summary>
		public override void UpdateSettings ()
		{
			try {
				var settings = new LaunchpadSettings (this);
				
				settings.PageSize = int.Parse(comboPageSize.SelectedValue);

				settings.Tables = Utils.FormatList(";", listTables.CheckedItems.Select(i => i.Value).ToArray());

				// NOTE: update module cache (temporary fix before 7.2.0)?
				// more info: https://github.com/dnnsoftware/Dnn.Platform/pull/21
				var moduleController = new ModuleController();
				moduleController.ClearCache(TabId);

				Utils.SynchronizeModule(this);

			} catch (Exception ex) {
				Exceptions.ProcessModuleLoadException (this, ex);
			}
		}
	}
}

