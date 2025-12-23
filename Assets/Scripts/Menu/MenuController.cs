using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Jigsawgram.UI
{
    public class MenuController : MonoBehaviour
    {
        [Inject] private MenuPresenter _menuPresenter;

        private async UniTaskVoid Start()
        {
            if (_menuPresenter == null)
            {
                var sceneContext = FindObjectOfType<SceneContext>();
                if (sceneContext != null && sceneContext.Container != null &&
                    sceneContext.Container.HasBinding<MenuPresenter>())
                {
                    _menuPresenter = sceneContext.Container.Resolve<MenuPresenter>();
                }
            }

            if (_menuPresenter == null)
            {
                Debug.LogError("MenuPresenter is not injected. Ensure MenuInstaller is set in the scene.");
                return;
            }

            await _menuPresenter.InitializeAsync();
        }
    }
}
