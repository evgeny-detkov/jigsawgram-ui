using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jigsawgram.UI
{
    public class DialogPanelView : MonoBehaviour, IManagedWindow
    {
        [SerializeField] private string windowId = "dialog";
        [SerializeField] private GameObject dialogRoot;
        [SerializeField] private TextMeshProUGUI dialogBody;
        [SerializeField] private Image viewImage;
        [SerializeField] private Button dialogPrimaryButton;
        [SerializeField] private TextMeshProUGUI dialogPrimaryLabel;
        [SerializeField] private Button dialogCloseButton;

        public string Id => windowId;
        public bool IsOverlay => true;
        public GameObject Root => dialogRoot != null ? dialogRoot : gameObject;

        public void ShowDialog(PuzzleCategoryModel category, PuzzleModel puzzle, PuzzleAccessViewData viewData,
            Action onPrimary)
        {
            if (dialogRoot == null || dialogBody == null || dialogPrimaryLabel == null) return;

            Show();

            dialogBody.text = viewData != null ? viewData.Body : string.Empty;
            dialogPrimaryLabel.text = viewData != null ? viewData.Primary : string.Empty;

            if (viewImage != null) viewImage.sprite = puzzle.ViewSprite;

            if (dialogPrimaryButton != null)
            {
                dialogPrimaryButton.onClick.RemoveAllListeners();
                dialogPrimaryButton.onClick.AddListener(() => onPrimary?.Invoke());
            }

            if (dialogCloseButton != null)
            {
                dialogCloseButton.onClick.RemoveAllListeners();
                dialogCloseButton.onClick.AddListener(Hide);
            }
        }

        public void CloseDialog()
        {
            Hide();
        }

        public void Show()
        {
            if (Root != null) Root.SetActive(true);
        }

        public void Hide()
        {
            if (Root != null) Root.SetActive(false);
        }
    }
}