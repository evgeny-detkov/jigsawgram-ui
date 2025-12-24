using UnityEngine;

namespace Jigsawgram.UI
{
    [CreateAssetMenu(fileName = "ResourceSettings", menuName = "Jigsawgram/Resource Settings")]
    public class ResourceSettings : ScriptableObject
    {
        [SerializeField] private string imagesFolder = "Images";

        public string ImagesFolder => string.IsNullOrWhiteSpace(imagesFolder) ? "Images" : imagesFolder;
    }
}
