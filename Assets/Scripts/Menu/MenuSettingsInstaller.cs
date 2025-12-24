using UnityEngine;
using Zenject;

namespace Jigsawgram.UI
{
    [CreateAssetMenu(fileName = "MenuSettingsInstaller", menuName = "Installers/Menu Settings Installer")]
    public class MenuSettingsInstaller : ScriptableObjectInstaller<MenuSettingsInstaller>
    {
        [Header("Images")] [SerializeField] private PuzzleImageDatabase imageDatabase;
        [SerializeField] private ResourceSettings resourceSettings;

        public override void InstallBindings()
        {
            MenuInstallBinding.BindResources(Container, ref imageDatabase, ref resourceSettings);
        }
    }
}
