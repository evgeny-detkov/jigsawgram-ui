using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jigsawgram.UI
{
    public class DialogPanelView : MonoBehaviour
    {
        [SerializeField] private GameObject dialogRoot;
        [SerializeField] private TextMeshProUGUI dialogBody;
        [SerializeField] private Image viewImage;
        [SerializeField] private Button dialogPrimaryButton;
        [SerializeField] private TextMeshProUGUI dialogPrimaryLabel;
        [SerializeField] private Button dialogCloseButton;

        public void ShowDialog(PuzzleCategoryModel category, PuzzleModel puzzle, Action onPrimary)
        {
            if (dialogRoot == null ||
                dialogBody == null ||
                dialogPrimaryLabel == null)
            {
                return;
            }

            dialogRoot.SetActive(true);
            dialogBody.text = puzzle.AccessType switch
            {
                PuzzleAccessType.Free => $"Start puzzle from \"{category.Name}\". Press to play.",
                PuzzleAccessType.Ads => "Watch a short ad to unlock this puzzle.",
                PuzzleAccessType.Paywall => $"This puzzle is paid. Pay {puzzle.Price}$ or pick another one.",
                _ => string.Empty
            };

            dialogPrimaryLabel.text = puzzle.AccessType switch
            {
                PuzzleAccessType.Free => "Play",
                PuzzleAccessType.Ads => "Watch Ad",
                PuzzleAccessType.Paywall => $"Pay {puzzle.Price}$",
                _ => "OK"
            };

            if (viewImage != null)
            {
                viewImage.sprite = puzzle.ViewSprite;
            }

            dialogPrimaryButton.onClick.RemoveAllListeners();
            dialogPrimaryButton.onClick.AddListener(() => onPrimary?.Invoke());

            if (dialogCloseButton != null)
            {
                dialogCloseButton.onClick.RemoveAllListeners();
                dialogCloseButton.onClick.AddListener(CloseDialog);
            }
        }

        public void CloseDialog()
        {
            SetDialogPanelActive(false);
        }

        private void SetDialogPanelActive(bool isActive)
        {
            if (dialogRoot != null)
            {
                dialogRoot.gameObject.SetActive(isActive);
            }
        }
    }
}