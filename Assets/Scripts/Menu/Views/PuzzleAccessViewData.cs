namespace Jigsawgram.UI
{
    public class PuzzleAccessViewData
    {
        public string Body { get; }
        public string Primary { get; }

        public PuzzleAccessViewData(string body, string primary)
        {
            Body = body;
            Primary = primary;
        }
    }
}