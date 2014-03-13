﻿using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Linq;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Content.Taxonomy;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.UserControls;
using DotNetNuke.Web.UI.WebControls;
using R7.University;

namespace R7.University.Launchpad
{
	public partial class EditDivision : PortalModuleBase
	{
		// ALT: private int itemId = Null.NullInteger;
		private int? itemId = null;

		#region Handlers

		/// <summary>
		/// Handles Init event for a control.
		/// </summary>
		/// <param name="e">Event args.</param>
		protected override void OnInit (EventArgs e)
		{
			base.OnInit (e);

			// set url for Cancel link
			linkCancel.NavigateUrl = Globals.NavigateURL ();

			// wireup event handlers
			buttonUpdate.Click += buttonUpdate_Click;
			buttonDelete.Click += buttonDelete_Click;

			// add confirmation dialog to delete button
			buttonDelete.Attributes.Add ("onClick", "javascript:return confirm('" + Localization.GetString ("DeleteItem") + "');");

			var ctrl = new LaunchpadController ();

			// parse QueryString
			itemId = Utils.ParseToNullableInt (Request.QueryString["division_id"]);
		
			// fill divisions dropdown
			comboParentDivisions.AddItem(Localization.GetString("NotSelected.Text", LocalResourceFile), Null.NullInteger.ToString());
			foreach (var division in ctrl.GetObjects<DivisionInfo>("ORDER BY [Title] ASC")) 
			{
				// remove current division from a list
				if (itemId == null || itemId != division.DivisionID)
					comboParentDivisions.AddItem (division.ShortTitle, division.DivisionID.ToString ());
			}

			/*
			var termCtrl = new TermController ();
			var terms = termCtrl.GetTermsByVocabulary("Structure");
			tsDivisionTerm.PortalId = this.PortalId;
			tsDivisionTerm.Terms = terms.ToList();
			tsDivisionTerm.DataBind();

			tsDivisionTerm.IncludeTags = true;
			tsDivisionTerm.IncludeSystemVocabularies = true;
			*/

			// Fill terms list
			var termCtrl = new TermController ();
			// TODO: Org. structure vocabulary name must be set in settings
			var terms = termCtrl.GetTermsByVocabulary("Structure").ToList(); 

			// add default term, 
			// TermId = Null.NullInteger is setting in cstor
			// TODO: Localize "-- not selected --" string
			terms.Insert(0, new Term (Localization.GetString("NotSelected.Text", LocalResourceFile)));

			// setup treeview (from TermsList.cs)
			treeDivisionTerms.DataTextField = "Name";
			treeDivisionTerms.DataValueField = "TermId";
			treeDivisionTerms.DataFieldID = "TermId";
			treeDivisionTerms.DataFieldParentID = "ParentTermId";

			treeDivisionTerms.DataSource = terms;
			treeDivisionTerms.DataBind ();

			/*
			// fill terms dropdown
			ddlDivisionTerm.Items.Add (new ListItem (Localization.GetString("NotSelected.Text", LocalResourceFile), Null.NullInteger.ToString()));


			foreach (var term in new TermController ().GetTermsByVocabulary("Structure")) 
				ddlDivisionTerm.Items.Add (new ListItem (term.Name, term.TermId.ToString()));
			*/

		}

		/// <summary>
		/// Handles Load event for a control.
		/// </summary>
		/// <param name="e">Event args.</param>
		protected override void OnLoad (EventArgs e)
		{
			base.OnLoad (e);
			
			try {
				// parse querystring parameters
				itemId = Utils.ParseToNullableInt (Request.QueryString ["division_id"]);
      
				if (!IsPostBack) {
					// load the data into the control the first time we hit this page

					// check we have an item to lookup
					// ALT: if (!Null.IsNull (itemId) 
					if (itemId != null) {
						// load the item
						var ctrl = new LaunchpadController ();
						var item = ctrl.Get<DivisionInfo> (itemId.Value);

						if (item != null) {


							txtTitle.Text = item.Title;
							txtShortTitle.Text = item.ShortTitle;
							txtWebSite.Text = item.WebSite;
							txtEmail.Text = item.Email;
							txtSecondaryEmail.Text = item.SecondaryEmail;
							txtLocation.Text = item.Location;
							txtPhone.Text = item.Phone;
							txtFax.Text = item.Fax;
							txtWorkingHours.Text = item.WorkingHours;

							// select parent division
							comboParentDivisions.Select (item.ParentDivisionID.ToString(), false);

							// select taxonomy term
							var treeNode = treeDivisionTerms.FindNodeByValue(item.DivisionTermID.ToString());
							if (treeNode != null)
							{
								treeNode.Selected = true;

								// expand all parent nodes
								treeNode = treeNode.ParentNode;
								while (treeNode != null)
								{
									treeNode.Expanded = true;
									treeNode = treeNode.ParentNode;
								} 
							}
							else
								treeDivisionTerms.Nodes[0].Selected = true;

							// set HomePage url
							if (!string.IsNullOrWhiteSpace(item.HomePage))
								urlHomePage.Url = item.HomePage;
							else
								// or set to "None", if Url is empty
								urlHomePage.UrlType = "N";

							// setup audit control
							ctlAudit.CreatedByUser = Utils.GetUserDisplayName(item.CreatedByUserID);
							ctlAudit.CreatedDate = item.CreatedOnDate.ToLongDateString ();
							ctlAudit.LastModifiedByUser = Utils.GetUserDisplayName(item.LastModifiedByUserID);
							ctlAudit.LastModifiedDate = item.LastModifiedOnDate.ToLongDateString();

						} else
							Response.Redirect (Globals.NavigateURL (), true);
					} else {
						buttonDelete.Visible = false;
						ctlAudit.Visible = false;
					}
				}
			} catch (Exception ex) {
				Exceptions.ProcessModuleLoadException (this, ex);
			}
		}

		/// <summary>
		/// Handles Click event for Update button
		/// </summary>
		/// <param name='sender'>
		/// Sender.
		/// </param>
		/// <param name='e'>
		/// Event args.
		/// </param>
		protected void buttonUpdate_Click (object sender, EventArgs e)
		{
			try {
				var ctrl = new LaunchpadController ();
				DivisionInfo item;

				// determine if we are adding or updating
				// ALT: if (Null.IsNull (itemId))
				if (!itemId.HasValue) {

					// to add new record
					item = new DivisionInfo ();

					item.Title = txtTitle.Text;
					item.ShortTitle = txtShortTitle.Text;
					item.Email = txtEmail.Text;
					item.SecondaryEmail = txtSecondaryEmail.Text;
					item.Phone = txtPhone.Text;
					item.Fax = txtFax.Text;
					item.Location = txtLocation.Text;
					item.WorkingHours = txtWorkingHours.Text;
					item.WebSite = txtWebSite.Text;

					item.ParentDivisionID = Utils.ParseToNullableInt(comboParentDivisions.SelectedValue);
					item.DivisionTermID = Utils.ParseToNullableInt(treeDivisionTerms.SelectedValue);
				
					item.HomePage = urlHomePage.Url;

					// update audit info
					item.CreatedByUserID = item.LastModifiedByUserID = this.UserId;
					item.CreatedOnDate = item.LastModifiedOnDate = DateTime.Now;

					ctrl.Add<DivisionInfo> (item);
				} else {

					// to update existing record
					item = ctrl.Get<DivisionInfo> (itemId.Value);

					item.Title = txtTitle.Text;
					item.ShortTitle = txtShortTitle.Text;
					item.Email = txtEmail.Text;
					item.SecondaryEmail = txtSecondaryEmail.Text;
					item.Phone = txtPhone.Text;
					item.Fax = txtFax.Text;
					item.Location = txtLocation.Text;
					item.WorkingHours = txtWorkingHours.Text;
					item.WebSite = txtWebSite.Text;
				
					item.ParentDivisionID = Utils.ParseToNullableInt(comboParentDivisions.SelectedValue);
					item.DivisionTermID = Utils.ParseToNullableInt(treeDivisionTerms.SelectedValue);

					item.HomePage = urlHomePage.Url;

					// update audit info
					item.LastModifiedByUserID = this.UserId;
					item.LastModifiedOnDate = DateTime.Now;

					ctrl.Update<DivisionInfo> (item);
				}

				Utils.SynchronizeModule(this);

				Response.Redirect (Globals.NavigateURL (), true);
			} catch (Exception ex) {
				Exceptions.ProcessModuleLoadException (this, ex);
			}
		}

		/// <summary>
		/// Handles Click event for Delete button
		/// </summary>
		/// <param name='sender'>
		/// Sender.
		/// </param>
		/// <param name='e'>
		/// Event args.
		/// </param>
		protected void buttonDelete_Click (object sender, EventArgs e)
		{
			try {
				// ALT: if (!Null.IsNull (itemId))
				if (itemId.HasValue) {
					var ctrl = new LaunchpadController ();
					ctrl.Delete<DivisionInfo> (itemId.Value);
					Response.Redirect (Globals.NavigateURL (), true);
				}
			} catch (Exception ex) {
				Exceptions.ProcessModuleLoadException (this, ex);
			}
		}

		#endregion
	}
}

