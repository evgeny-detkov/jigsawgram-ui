using System.Collections.Generic;
using UnityEngine;

namespace Jigsawgram.UI
{
    [CreateAssetMenu(fileName = "PuzzleImageDatabase", menuName = "Jigsawgram/Puzzle Image Database")]
    public class PuzzleImageDatabase : ScriptableObject
    {
        public List<CategoryRes> categories = new List<CategoryRes>();

        [System.Serializable]
        public class PuzzleRes
        {
            [SerializeField] private string id;
            [SerializeField] private Sprite viewSprite;
            [SerializeField] private PuzzleAccessType accessType;
            [SerializeField] private float price;

            public string ID => id;
            public Sprite ViewSprite => viewSprite;
            public PuzzleAccessType AccessType => accessType;
            public float Price => price;
        }

        [System.Serializable]
        public class CategoryRes
        {
            public string id;
            public string name;
            public List<PuzzleRes> puzzleResList = new List<PuzzleRes>();
        }
    }
}