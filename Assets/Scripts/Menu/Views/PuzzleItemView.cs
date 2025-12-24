using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jigsawgram.UI
{
    public class PuzzleItemView : MonoBehaviour
    {
        [SerializeField] private Image viewImage;
        [SerializeField] private TextMeshProUGUI badgeText;
        [SerializeField] private Button button;

        public void Render(Sprite viewSprite, string badge, Action onClick)
        {
            if (viewImage != null) viewImage.sprite = viewSprite;

            if (badgeText != null)
            {
                badgeText.text = badge;
                badgeText.transform.parent.gameObject.SetActive(!string.IsNullOrEmpty(badge));
            }

            if (button != null)
            {
                button.onClick.RemoveAllListeners();
                if (onClick != null) button.onClick.AddListener(onClick.Invoke);
            }
        }
    }
}