namespace Jigsawgram.UI
{
    public interface IPuzzleAccessPolicy
    {
        PuzzleAccessType Type { get; }
        PuzzleAccessViewData Build(PuzzleCategoryModel category, PuzzleModel puzzle);
    }

    public class FreeAccessPolicy : IPuzzleAccessPolicy
    {
        public PuzzleAccessType Type => PuzzleAccessType.Free;

        public PuzzleAccessViewData Build(PuzzleCategoryModel category, PuzzleModel puzzle)
        {
            var body = $"Start puzzle from \"{category?.Name}\". Press to play.";
            return new PuzzleAccessViewData(body, "Play");
        }
    }

    public class AdsAccessPolicy : IPuzzleAccessPolicy
    {
        public PuzzleAccessType Type => PuzzleAccessType.Ads;

        public PuzzleAccessViewData Build(PuzzleCategoryModel category, PuzzleModel puzzle)
        {
            return new PuzzleAccessViewData("Watch a short ad to unlock this puzzle.", "Watch Ad");
        }
    }

    public class PaywallAccessPolicy : IPuzzleAccessPolicy
    {
        public PuzzleAccessType Type => PuzzleAccessType.Paywall;

        public PuzzleAccessViewData Build(PuzzleCategoryModel category, PuzzleModel puzzle)
        {
            var price = puzzle != null ? puzzle.Price : 0;
            return new PuzzleAccessViewData($"This puzzle is paid. Pay {price}$ or pick another one.", $"Pay {price}$");
        }
    }

    public class DefaultAccessPolicy : IPuzzleAccessPolicy
    {
        public PuzzleAccessType Type => PuzzleAccessType.Free;

        public PuzzleAccessViewData Build(PuzzleCategoryModel category, PuzzleModel puzzle)
        {
            return new PuzzleAccessViewData("Puzzle unavailable.", "OK");
        }
    }
}