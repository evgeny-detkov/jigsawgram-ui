using UnityEngine;
using Zenject;

namespace Jigsawgram.UI
{
    public static class MenuInstallBinding
    {
        public static void BindResources(DiContainer container, ref PuzzleImageDatabase db, ref ResourceSettings settings)
        {
            if (db == null)
            {
                db = Resources.Load<PuzzleImageDatabase>("Databases/PuzzleImageDatabase");
            }

            if (settings == null)
            {
                settings = ScriptableObject.CreateInstance<ResourceSettings>();
            }

            if (!container.HasBinding<PuzzleImageDatabase>())
            {
                container.BindInstance(db);
            }

            if (!container.HasBinding<ResourceSettings>())
            {
                container.BindInstance(settings);
            }
        }
    }
}
