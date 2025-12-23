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
        private MenuPresenter _menuPresenter;

        private void Awake()
        {
            if (imageDatabase == null)
            {
                imageDatabase = Resources.Load<PuzzleImageDatabase>("Databases/PuzzleImageDatabase");
            }

            if (categoryPanelView == null)
            {
                categoryPanelView = FindObjectOfType<CategoryPanelView>(true);
            }

            if (puzzlePanelView == null)
            {
                puzzlePanelView = FindObjectOfType<PuzzlePanelView>(true);
            }

            if (dialogPanelView == null)
            {
                dialogPanelView = FindObjectOfType<DialogPanelView>(true);
            }

            var catalogProvider = new ScriptableCatalogProvider(imageDatabase, resourcesImagesFolder);
            _catalogService = new CatalogService(catalogProvider);
            var windowManager = new WindowManager(new IManagedWindow[]
            {
                categoryPanelView,
                puzzlePanelView,
                dialogPanelView
            });
            var accessPresenter = new PuzzleAccessPresenter(new IPuzzleAccessPolicy[]
            {
                new FreeAccessPolicy(),
                new AdsAccessPolicy(),
                new PaywallAccessPolicy()
            }, new DefaultAccessPolicy());

            _menuPresenter = new MenuPresenter(_catalogService, windowManager, accessPresenter,
                categoryPanelView, puzzlePanelView, dialogPanelView);
        }

        private async UniTaskVoid Start()
        {
            if (_menuPresenter == null)
            {
                return;
            }

            await _menuPresenter.InitializeAsync();
        }
    }
}
