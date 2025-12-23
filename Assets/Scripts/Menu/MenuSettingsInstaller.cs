using UnityEngine;
using Zenject;

namespace Jigsawgram.UI
{
    [CreateAssetMenu(fileName = "MenuSettingsInstaller", menuName = "Installers/Menu Settings Installer")]
    public class MenuSettingsInstaller : ScriptableObjectInstaller<MenuSettingsInstaller>
    {
        [Header("Images")] [SerializeField] private PuzzleImageDatabase imageDatabase;
        [SerializeField] private string resourcesImagesFolder = "Images";

        public override void InstallBindings()
        {
            var db = imageDatabase ?? Resources.Load<PuzzleImageDatabase>("Databases/PuzzleImageDatabase");
            Container.BindInstance(db).IfNotBound();

            var folder = string.IsNullOrWhiteSpace(resourcesImagesFolder) ? "Images" : resourcesImagesFolder;
            Container.BindInstance(folder).IfNotBound();
        }
    }
}
