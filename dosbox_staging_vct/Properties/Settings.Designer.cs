﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace dosbox_staging_vct.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.10.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string UserConfFolderPath {
            get {
                return ((string)(this["UserConfFolderPath"]));
            }
            set {
                this["UserConfFolderPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string DosBoxStagingExeFilePath {
            get {
                return ((string)(this["DosBoxStagingExeFilePath"]));
            }
            set {
                this["DosBoxStagingExeFilePath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string GlobalConfFilePath {
            get {
                return ((string)(this["GlobalConfFilePath"]));
            }
            set {
                this["GlobalConfFilePath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("cboptions.json")]
        public string JsonComboBoxOptionsFileName {
            get {
                return ((string)(this["JsonComboBoxOptionsFileName"]));
            }
            set {
                this["JsonComboBoxOptionsFileName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ShowSaveConfWaring {
            get {
                return ((bool)(this["ShowSaveConfWaring"]));
            }
            set {
                this["ShowSaveConfWaring"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("dosbox-staging.conf")]
        public string GlobalConfFileName {
            get {
                return ((string)(this["GlobalConfFileName"]));
            }
            set {
                this["GlobalConfFileName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\users\\")]
        public string NonPortableFolderPathStart {
            get {
                return ((string)(this["NonPortableFolderPathStart"]));
            }
            set {
                this["NonPortableFolderPathStart"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\\AppData\\Local\\DOSBox\\")]
        public string NonPortableFolderPathEnd {
            get {
                return ((string)(this["NonPortableFolderPathEnd"]));
            }
            set {
                this["NonPortableFolderPathEnd"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string LaunchParameters {
            get {
                return ((string)(this["LaunchParameters"]));
            }
            set {
                this["LaunchParameters"] = value;
            }
        }
    }
}
