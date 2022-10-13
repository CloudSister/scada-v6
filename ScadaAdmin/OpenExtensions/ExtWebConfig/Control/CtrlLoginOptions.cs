﻿// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Admin.Project;
using Scada.Forms;
using Scada.Web.Config;

namespace Scada.Admin.Extensions.ExtWebConfig.Control
{
    /// <summary>
    /// Represents a control for editing login options.
    /// <para>Представляет элемент управления для редактирования основных параметров логирование.</para>
    /// </summary>
    public partial class CtrlLoginOptions : UserControl
    {
        private IAdminContext adminContext;      // the Administrator context
        private WebApp webApp;                   // the web application in a project
        private bool changing;                   // controls are being changed programmatically


        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public CtrlLoginOptions()
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



        private void CtrlLoginOptions_Load(object sender, EventArgs e)
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
        public void OptionsToControls(LoginOptions loginOptions)
        {
            ArgumentNullException.ThrowIfNull(loginOptions, nameof(loginOptions));
            changing = true;

            chkRequireCaptcha.Checked = loginOptions.RequireCaptcha;
            chkAllowRememberMe.Checked = loginOptions.AllowRememberMe;
            numRememberMeExpires.SetValue(loginOptions.RememberMeExpires);
            txtAutoLoginUsername.Text = loginOptions.AutoLoginUsername;
            txtAutoLoginPassword.Text = loginOptions.AutoLoginPassword;

            changing = false;
        }

        /// <summary>
        /// Sets the configuration according to the controls.
        /// </summary>
        public void ControlsToOptions(LoginOptions loginOptions)
        {
            ArgumentNullException.ThrowIfNull(loginOptions, nameof(loginOptions));

            loginOptions.RequireCaptcha = chkRequireCaptcha.Checked;
            loginOptions.AllowRememberMe = chkAllowRememberMe.Checked;
            loginOptions.RememberMeExpires = Convert.ToInt32(numRememberMeExpires.Value);
            loginOptions.AutoLoginUsername = txtAutoLoginUsername.Text;
            loginOptions.AutoLoginPassword = txtAutoLoginPassword.Text;
        }
    }
}
