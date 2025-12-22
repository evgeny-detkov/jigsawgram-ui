using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Jigsawgram.UI
{
    public class MenuPresenter
    {
        private readonly CatalogService _catalogService;
        private readonly WindowManager _windowManager;
        private readonly PuzzleAccessPresenter _accessPresenter;
        private readonly CategoryPanelView _categoryView;
        private readonly PuzzlePanelView _puzzleView;
        private readonly DialogPanelView _dialogView;

        private CatalogModel _catalog;
        private PuzzleCategoryModel _currentCategory;

        public MenuPresenter(
            CatalogService catalogService,
            WindowManager windowManager,
            PuzzleAccessPresenter accessPresenter,
            CategoryPanelView categoryView,
            PuzzlePanelView puzzleView,
            DialogPanelView dialogView)
        {
            _catalogService = catalogService;
            _windowManager = windowManager;
            _accessPresenter = accessPresenter;
            _categoryView = categoryView;
            _puzzleView = puzzleView;
            _dialogView = dialogView;
        }

        public async UniTask InitializeAsync()
        {
            _categoryView.Bind(OnCategorySelected);
            _puzzleView.Bind(ShowCategories, OnPuzzleSelected);

            _catalog = await _catalogService.LoadCatalogAsync();
            _catalog ??= new CatalogModel(new List<PuzzleCategoryModel>());

            ShowCategories();
        }

        private void ShowCategories()
        {
            _dialogView.CloseDialog();
            _windowManager.ShowWindow(_categoryView.Id);
            _categoryView.RenderCategories(_catalog.Categories);
        }

        private void OnCategorySelected(PuzzleCategoryModel category)
        {
            if (category == null)
            {
                return;
            }

            _currentCategory = category;
            _dialogView.CloseDialog();
            _windowManager.ShowWindow(_puzzleView.Id);
            _puzzleView.RenderPuzzles(category);
        }

        private void OnPuzzleSelected(PuzzleModel puzzle)
        {
            if (_currentCategory == null || puzzle == null)
            {
                return;
            }

            var accessView = _accessPresenter.Build(_currentCategory, puzzle);

            _dialogView.ShowDialog(_currentCategory, puzzle, accessView, () =>
            {
                Debug.Log($"Click puzzle {_currentCategory.Name} / {puzzle.Id}");
                _dialogView.CloseDialog();
            });

            _windowManager.ShowOverlay(_dialogView.Id);
        }
    }
}
