﻿// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Xml;

namespace Scada.Server.Modules.ModActiveDirectory.Config
{
    /// <summary>
    /// Represents a module configuration.
    /// <para>Представляет конфигурацию модуля.</para>
    /// </summary>
    internal class ModuleConfig : ModuleConfigBase
    {
        /// <summary>
        /// The default configuration file name.
        /// </summary>
        public const string DefaultFileName = "ModActiveDirectory.xml";


        /// <summary>
        /// Gets or sets the domain controller path.
        /// </summary>
        public string LdapPath { get; set; }
        
        
        /// <summary>
        /// Sets the default values.
        /// </summary>
        protected override void SetToDefault()
        {
            LdapPath = "LDAP://localhost";
        }

        /// <summary>
        /// Loads the configuration from the specified reader.
        /// </summary>
        protected override void Load(TextReader reader)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(reader);
            LdapPath = xmlDoc.DocumentElement.GetChildAsString("LdapPath");
        }
        
        /// <summary>
        /// Saves the configuration to the specified writer.
        /// </summary>
        protected override void Save(TextWriter writer)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration xmlDecl = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(xmlDecl);

            XmlElement rootElem = xmlDoc.CreateElement("ModActiveDirectory");
            xmlDoc.AppendChild(rootElem);
            rootElem.AppendElem("LdapPath", LdapPath);

            xmlDoc.Save(writer);
        }
    }
}
