using System.Collections.Generic;
using UnityEngine;

namespace Jigsawgram.UI
{
    public enum PuzzleAccessType
    {
        Free = 0,
        Paywall = 1,
        Ads = 2
    }

    public class PuzzleModel
    {
        public string Id { get; }
        public PuzzleAccessType AccessType { get; }
        public decimal Price { get; }
        public Sprite ViewSprite { get; }

        public PuzzleModel(string id, PuzzleAccessType accessType, float price, Sprite viewSprite)
        {
            Id = id;
            AccessType = accessType;
            Price = (decimal)price;
            ViewSprite = viewSprite;
        }
    }

    public class PuzzleCategoryModel
    {
        public string Id { get; }
        public string Name { get; }
        public IReadOnlyList<PuzzleModel> Puzzles { get; }
        public Sprite ViewSprite { get; }

        public PuzzleCategoryModel(string id, string name, IReadOnlyList<PuzzleModel> puzzles, Sprite viewSprite)
        {
            Id = id;
            Name = name;
            Puzzles = puzzles;
            ViewSprite = viewSprite;
        }
    }

    public class CatalogModel
    {
        public IReadOnlyList<PuzzleCategoryModel> Categories { get; }

        public CatalogModel(IReadOnlyList<PuzzleCategoryModel> categories)
        {
            Categories = categories;
        }
    }
}