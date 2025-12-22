using System.Collections.Generic;

namespace Jigsawgram.UI
{
    public class WindowManager
    {
        private readonly Dictionary<string, IManagedWindow> _windows = new Dictionary<string, IManagedWindow>();

        public WindowManager(IEnumerable<IManagedWindow> windows)
        {
            if (windows == null)
            {
                return;
            }

            foreach (var window in windows)
            {
                if (window == null || string.IsNullOrEmpty(window.Id))
                {
                    continue;
                }

                _windows[window.Id] = window;
                window.Hide();
            }
        }

        public void ShowWindow(string id)
        {
            foreach (var kvp in _windows)
            {
                var window = kvp.Value;
                if (window.IsOverlay)
                {
                    window.Hide();
                    continue;
                }

                if (kvp.Key == id)
                {
                    window.Show();
                }
                else
                {
                    window.Hide();
                }
            }
        }

        public void ShowOverlay(string id)
        {
            if (_windows.TryGetValue(id, out var window))
            {
                window.Show();
            }
        }

        public void HideOverlay(string id)
        {
            if (_windows.TryGetValue(id, out var window) && window.IsOverlay)
            {
                window.Hide();
            }
        }

        public void HideOverlays()
        {
            foreach (var window in _windows.Values)
            {
                if (window.IsOverlay)
                {
                    window.Hide();
                }
            }
        }
    }
}