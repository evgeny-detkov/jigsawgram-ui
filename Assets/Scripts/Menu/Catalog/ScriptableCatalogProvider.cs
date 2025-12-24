using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Jigsawgram.UI
{
    public class ScriptableCatalogProvider : IPuzzleCatalogProvider
    {
        private readonly PuzzleImageDatabase _database;
        private readonly string _resourcesImagesFolder;

        public ScriptableCatalogProvider(PuzzleImageDatabase database, string resourcesImagesFolder = "Images")
        {
            _database = database;
            _resourcesImagesFolder = resourcesImagesFolder;
        }

        public CatalogModel LoadCatalog()
        {
            if (_database != null && _database.categories != null && _database.categories.Count > 0)
                return BuildFromDatabase(_database);

            return null;
        }

        public UniTask<CatalogModel> LoadCatalogAsync()
        {
            return UniTask.FromResult(LoadCatalog());
        }

        private CatalogModel BuildFromDatabase(PuzzleImageDatabase db)
        {
            var categories = new List<PuzzleCategoryModel>();
            for (var i = 0; i < db.categories.Count; i++)
            {
                var entry = db.categories[i];
                if (entry == null) continue;

                var puzzles = new List<PuzzleModel>();
                var puzzlesList = entry.puzzleResList ?? new List<PuzzleImageDatabase.PuzzleRes>();
                for (var j = 0; j < puzzlesList.Count; j++)
                {
                    var puzzleRes = puzzlesList[j];
                    var puzzleId = puzzleRes.ID;
                    var viewSprite = puzzleRes.ViewSprite;
                    var accessType = puzzleRes.AccessType;
                    var price = puzzleRes.Price;
                    puzzles.Add(new PuzzleModel(
                        string.IsNullOrEmpty(puzzleId) ? $"puzzle-{entry.id}-{j}" : puzzleId,
                        accessType,
                        price,
                        viewSprite));
                }

                var viewSpriteCategory = puzzlesList.Count > 0 ? puzzlesList[0].ViewSprite : null;
                categories.Add(new PuzzleCategoryModel(
                    string.IsNullOrEmpty(entry.id) ? $"category-{i}" : entry.id,
                    string.IsNullOrEmpty(entry.name) ? $"Category {i + 1}" : entry.name,
                    puzzles,
                    viewSpriteCategory));
            }

            return new CatalogModel(categories);
        }
    }
}