using System;
using System.Collections.Generic;
using UnityEngine;

namespace Jigsawgram.UI
{
    public class CategoryPanelView : MonoBehaviour, IManagedWindow
    {
        [SerializeField] private string windowId = "categories";
        [SerializeField] private RectTransform categoryContent;
        [SerializeField] private RectTransform categoryPanel;
        [SerializeField] private GameObject categoryPrefab;

        private UiObjectPool<CategoryItemView> _pool;

        public string Id => windowId;
        public bool IsOverlay => false;
        public GameObject Root => categoryPanel != null ? categoryPanel.gameObject : gameObject;

        public RectTransform CategoryContent => categoryContent;

        public RectTransform CategoryPanel => categoryPanel;

        public void SetCategoryPanelActive(bool isActive)
        {
            if (categoryPanel != null)
            {
                categoryPanel.gameObject.SetActive(isActive);
            }
        }

        public void RenderCategories(IEnumerable<PuzzleCategoryModel> categories,
            Action<PuzzleCategoryModel> onCategorySelected)
        {
            EnsurePool();
            if (_pool == null)
            {
                return;
            }

            _pool.ReleaseAll();

            if (categories == null)
            {
                return;
            }

            foreach (var category in categories)
            {
                var view = _pool.Get();
                var rect = view.transform as RectTransform;
                if (rect != null)
                {
                    rect.localScale = Vector3.one;
                }

                var viewSprite = category.ViewSprite;
                view.Render(category.Name, viewSprite, () => onCategorySelected?.Invoke(category));
            }
        }

        public void Show()
        {
            SetCategoryPanelActive(true);
        }

        public void Hide()
        {
            SetCategoryPanelActive(false);
        }

        private void EnsurePool()
        {
            if (_pool != null || categoryPrefab == null || categoryContent == null)
            {
                return;
            }

            var viewPrefab = categoryPrefab.GetComponent<CategoryItemView>();
            if (viewPrefab == null)
            {
                return;
            }

            _pool = new UiObjectPool<CategoryItemView>(viewPrefab, categoryContent);
        }
    }
}