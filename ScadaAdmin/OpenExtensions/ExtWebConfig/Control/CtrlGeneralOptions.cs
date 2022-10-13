﻿// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Admin.Project;
using Scada.Forms;
using Scada.Web.Config;

namespace Scada.Admin.Extensions.ExtWebConfig.Control
{
    /// <summary>
    /// Represents a control for editing general line options.
    /// <para>Представляет элемент управления для редактирования основных параметров.</para>
    /// </summary>
    public partial class CtrlGeneralOptions : UserControl
    {
        private IAdminContext adminContext;      // the Administrator context
        private WebApp webApp;                   // the web application in a project
        private bool changing;                   // controls are being changed programmatically


        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public CtrlGeneralOptions()
        {
            InitializeComponent();
            adminContext = null;
            webApp = null;
            changing = false;
        }


        /// <summary>
        /// Raises an OptionsChanged event.
        /// </summary>
        private void OnOptionsChanged()
        {
            OptionsChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Occurs when the options change.
        /// </summary>
        public event EventHandler OptionsChanged;


        private void CtrlGeneralOptions_Load(object sender, EventArgs e)
        {
            FormTranslator.Translate(this, GetType().FullName);
        }

        private void control_Changed(object sender, EventArgs e)
        {
            if (!changing)
                OnOptionsChanged();
        }

        /// <summary>
        /// Initializes the control.
        /// </summary>
        public void Init(IAdminContext adminContext, WebApp webApp)
        {
            this.adminContext = adminContext ?? throw new ArgumentNullException(nameof(adminContext));
            this.webApp = webApp ?? throw new ArgumentNullException(nameof(webApp));
        }

        /// <summary>
        /// Sets the controls according to the options.
        /// </summary>
        public void OptionsToControls(GeneralOptions generalOptions)
        {
            ArgumentNullException.ThrowIfNull(generalOptions, nameof(generalOptions));
            changing = true;

            txtDefaultCulture.Text = generalOptions.DefaultCulture;
            cbDefaultTimeZone.Text = generalOptions.DefaultTimeZone;
            txtDefaultStartPage.Text = generalOptions.DefaultStartPage;
            chkEnableCommands.Checked = generalOptions.EnableCommands;
            chkShareStats.Checked = generalOptions.ShareStats;
            numMaxLogSize.SetValue(generalOptions.MaxLogSize);

            changing = false;
        }

        /// <summary>
        /// Sets the configuration according to the controls.
        /// </summary>
        public void ControlsToOptions(GeneralOptions generalOptions)
        {
            ArgumentNullException.ThrowIfNull(generalOptions, nameof(generalOptions));

            generalOptions.DefaultCulture = txtDefaultCulture.Text;
            generalOptions.DefaultTimeZone = cbDefaultTimeZone.Text;
            generalOptions.DefaultStartPage = txtDefaultStartPage.Text;
            generalOptions.EnableCommands = chkEnableCommands.Checked;
            generalOptions.ShareStats = chkShareStats.Checked;
            generalOptions.MaxLogSize = Convert.ToInt32(numMaxLogSize.Value);
        }
    }
}
