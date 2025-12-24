using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jigsawgram.UI
{
    public class PuzzlePanelView : MonoBehaviour, IManagedWindow
    {
        [SerializeField] private string windowId = "puzzles";
        [SerializeField] private RectTransform puzzlePanel;
        [SerializeField] private RectTransform puzzleContent;
        [SerializeField] private Button backButton;
        [SerializeField] private TextMeshProUGUI categoryTitle;
        [SerializeField] private GameObject puzzlePrefab;

        private UiViewPool<PuzzleItemView> _pool;
        private Action _onBack;
        private Action<PuzzleModel> _onPuzzleSelected;
        private readonly Dictionary<PuzzleAccessType, string> _badgeCache = new Dictionary<PuzzleAccessType, string>();

        public string Id => windowId;
        public bool IsOverlay => false;
        public GameObject Root => puzzlePanel != null ? puzzlePanel.gameObject : gameObject;

        public void Bind(Action onShowCategories, Action<PuzzleModel> onPuzzleSelected)
        {
            _onBack = onShowCategories;
            _onPuzzleSelected = onPuzzleSelected;

            if (backButton != null)
            {
                backButton.onClick.RemoveAllListeners();
                backButton.onClick.AddListener(() => onShowCategories?.Invoke());
            }
        }

        public void SetPuzzlePanelActive(bool isActive)
        {
            if (puzzlePanel != null) puzzlePanel.gameObject.SetActive(isActive);
        }

        public void RenderPuzzles(PuzzleCategoryModel category)
        {
            if (categoryTitle != null) categoryTitle.text = category != null ? category.Name : string.Empty;

            EnsurePool();
            if (_pool == null) return;

            _pool.ReleaseAll();

            var list = category?.Puzzles;
            if (list == null) return;

            foreach (var puzzle in list)
            {
                var view = _pool.Get();
                var rect = view.transform as RectTransform;
                if (rect != null) rect.localScale = Vector3.one;

                var badge = ResolveBadge(puzzle);
                var viewSprite = puzzle.ViewSprite;

                view.Render(viewSprite, badge, () => _onPuzzleSelected?.Invoke(puzzle));
            }
        }

        public void Show()
        {
            SetPuzzlePanelActive(true);
        }

        public void Hide()
        {
            SetPuzzlePanelActive(false);
        }

        private void EnsurePool()
        {
            if (_pool != null || puzzlePrefab == null || puzzleContent == null) return;

            var viewPrefab = puzzlePrefab.GetComponent<PuzzleItemView>();
            if (viewPrefab == null) return;

            _pool = new UiViewPool<PuzzleItemView>(viewPrefab, puzzleContent);
        }

        private string ResolveBadge(PuzzleModel puzzle)
        {
            var type = puzzle.AccessType;
            if (_badgeCache.TryGetValue(type, out var cached))
            {
                return cached;
            }

            var badge = type switch
            {
                PuzzleAccessType.Free => "Free",
                PuzzleAccessType.Ads => "Ad",
                PuzzleAccessType.Paywall => $"{puzzle.Price}$",
                _ => string.Empty
            };

            _badgeCache[type] = badge;
            return badge;
        }
    }
}
