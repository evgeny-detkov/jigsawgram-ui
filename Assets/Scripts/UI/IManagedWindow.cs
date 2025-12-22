using UnityEngine;

namespace Jigsawgram.UI
{
    public interface IManagedWindow
    {
        string Id { get; }
        bool IsOverlay { get; }
        GameObject Root { get; }
        void Show();
        void Hide();
    }
}