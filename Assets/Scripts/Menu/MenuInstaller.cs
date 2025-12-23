using UnityEngine;
using Zenject;

namespace Jigsawgram.UI
{
    public class MenuInstaller : MonoInstaller
    {
        [Header("Images")] [SerializeField] private PuzzleImageDatabase imageDatabase;
        [SerializeField] private string resourcesImagesFolder = "Images";

        [Header("Views")] [SerializeField] private CategoryPanelView categoryPanelView;
        [SerializeField] private PuzzlePanelView puzzlePanelView;
        [SerializeField] private DialogPanelView dialogPanelView;

        public override void InstallBindings()
        {
            SetupViews();
            SetupImages();
            BindCatalog();
            BindAccessPolicies();
            BindPresenter();
        }

        private void SetupViews()
        {
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

            Container.BindInstance(categoryPanelView).IfNotBound();
            Container.BindInstance(puzzlePanelView).IfNotBound();
            Container.BindInstance(dialogPanelView).IfNotBound();

            Container.Bind<WindowManager>().AsSingle().WithArguments(new IManagedWindow[]
            {
                categoryPanelView,
                puzzlePanelView,
                dialogPanelView
            });
        }

        private void SetupImages()
        {
            if (!Container.HasBinding<PuzzleImageDatabase>())
            {
                var db = imageDatabase ?? Resources.Load<PuzzleImageDatabase>("Databases/PuzzleImageDatabase");
                if (db != null)
                {
                    Container.BindInstance(db);
                }
                else
                {
                    Debug.LogWarning("PuzzleImageDatabase is not assigned and could not be loaded from Resources/Databases.");
                }
            }

            var folder = string.IsNullOrWhiteSpace(resourcesImagesFolder) ? "Images" : resourcesImagesFolder;
            if (!Container.HasBinding<string>())
            {
                Container.BindInstance(folder);
            }
        }

        private void BindCatalog()
        {
            var db = imageDatabase ?? Resources.Load<PuzzleImageDatabase>("Databases/PuzzleImageDatabase");
            var folder = string.IsNullOrWhiteSpace(resourcesImagesFolder) ? "Images" : resourcesImagesFolder;

            Container.Bind<IPuzzleCatalogProvider>().FromMethod(_ =>
                new ScriptableCatalogProvider(db, folder)).AsSingle();
            Container.Bind<CatalogService>().AsSingle();
        }

        private void BindAccessPolicies()
        {
            Container.Bind<PuzzleAccessPresenter>().AsSingle().WithArguments(new IPuzzleAccessPolicy[]
            {
                new FreeAccessPolicy(),
                new AdsAccessPolicy(),
                new PaywallAccessPolicy()
            }, new DefaultAccessPolicy());
        }

        private void BindPresenter()
        {
            Container.Bind<MenuPresenter>().AsSingle();
        }
    }
}
