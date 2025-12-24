using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jigsawgram.UI
{
    public class CategoryItemView : MonoBehaviour
    {
        [SerializeField] private Image previewImage;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private Button button;

        public void Render(string title, Sprite viewSprite, Action onClick)
        {
            if (previewImage != null) previewImage.sprite = viewSprite;

            if (titleText != null) titleText.text = title;

            if (button != null)
            {
                button.onClick.RemoveAllListeners();
                if (onClick != null) button.onClick.AddListener(onClick.Invoke);
            }
        }
    }
}