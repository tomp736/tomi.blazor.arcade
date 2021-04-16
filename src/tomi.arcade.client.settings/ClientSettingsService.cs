using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace tomi.arcade.client.settings
{
    public class ClientSettingsService
    {
        private readonly DarkModeJsInterop _darkModeJsInterp;

        public ClientSettingsService(DarkModeJsInterop darkModeJsInterp)
        {
            _darkModeJsInterp = darkModeJsInterp;
            InitializeSettings();
        }

        private void InitializeSettings()
        {
            // load from persistant store
            DarkMode = true;
        }

        public string BackgroundColor1
        {
            get
            {
                if (_darkMode)
                {
                    return "#182227";
                }
                return "#f0f8ff";
            }
        }
        public string TextColor1
        {
            get
            {
                if (_darkMode)
                {
                    return "#f0f8ff";
                }
                return "#182227";
            }
        }

        private bool _darkMode = true;
        public bool DarkMode
        {
            get
            {
                return _darkMode;
            }
            set
            {
                _darkMode = value;
                if (value == true)
                {
                    _ = _darkModeJsInterp.SetDarkMode();
                }
                else
                {
                    _ = _darkModeJsInterp.SetLightMode();
                }
            }
        }
    }
}
