using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Jigsawgram.UI
{
    public class MenuController : MonoBehaviour
    {
        [Header("Images")] [SerializeField] private PuzzleImageDatabase imageDatabase;
        [SerializeField] private string resourcesImagesFolder = "Images";

        [Header("Category UI")] [SerializeField]
        private CategoryPanelView categoryPanelView;

        [Header("Puzzle UI")] [SerializeField] private PuzzlePanelView puzzlePanelView;

        [Header("Dialog UI")] [SerializeField] private DialogPanelView dialogPanelView;

        private CatalogService _catalogService;
        private CatalogModel _catalog;

        private PuzzleCategoryModel _currentCategory;

        private void Awake()
        {
            if (imageDatabase == null)
            {
                imageDatabase = Resources.Load<PuzzleImageDatabase>("Databases/PuzzleImageDatabase");
            }

            var catalogProvider = new ScriptableCatalogProvider(imageDatabase, resourcesImagesFolder);
            _catalogService = new CatalogService(catalogProvider);
        }

        private async UniTaskVoid Start()
        {
            puzzlePanelView.Init(ShowCategories);

            _catalog = await LoadCatalogSafe();

            ShowCategories();
        }

        private async UniTask<CatalogModel> LoadCatalogSafe()
        {
            if (_catalogService == null)
            {
                return new CatalogModel(new List<PuzzleCategoryModel>());
            }

            var catalog = await _catalogService.LoadCatalogAsync();
            return catalog ?? new CatalogModel(new List<PuzzleCategoryModel>());
        }

        private void ShowCategories()
        {
            dialogPanelView.CloseDialog();

            if (categoryPanelView != null)
            {
                categoryPanelView.SetCategoryPanelActive(true);
            }

            if (puzzlePanelView != null)
            {
                puzzlePanelView.SetPuzzlePanelActive(false);
            }

            if (categoryPanelView == null)
            {
                return;
            }

            categoryPanelView.RenderCategories(_catalog?.Categories ?? new List<PuzzleCategoryModel>(),
                OnCategorySelected);
        }

        private void OnCategorySelected(PuzzleCategoryModel category)
        {
            _currentCategory = category;
            dialogPanelView.CloseDialog();

            if (categoryPanelView != null)
            {
                categoryPanelView.SetCategoryPanelActive(false);
            }

            if (puzzlePanelView != null)
            {
                puzzlePanelView.SetPuzzlePanelActive(true);
            }

            if (puzzlePanelView == null)
            {
                return;
            }

            puzzlePanelView.RenderPuzzles(category, OnPuzzleSelected);
        }

        private void OnPuzzleSelected(PuzzleModel puzzle)
        {
            if (_currentCategory == null)
            {
                return;
            }

            dialogPanelView.ShowDialog(_currentCategory, puzzle, () =>
            {
                Debug.Log($"Click puzzle {_currentCategory.Name} / {puzzle.Id}");
                dialogPanelView.CloseDialog();
            });
        }
    }
}