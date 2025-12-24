using System.Collections.Generic;

namespace Jigsawgram.UI
{
    public class PuzzleAccessPresenter
    {
        private readonly Dictionary<PuzzleAccessType, IPuzzleAccessPolicy> _policies;
        private readonly IPuzzleAccessPolicy _fallback;

        public PuzzleAccessPresenter(IEnumerable<IPuzzleAccessPolicy> policies, IPuzzleAccessPolicy fallback)
        {
            _policies = new Dictionary<PuzzleAccessType, IPuzzleAccessPolicy>();
            if (policies != null)
                foreach (var policy in policies)
                {
                    if (policy == null) continue;

                    _policies[policy.Type] = policy;
                }

            _fallback = fallback;
        }

        public PuzzleAccessViewData Build(PuzzleCategoryModel category, PuzzleModel puzzle)
        {
            var type = puzzle != null ? puzzle.AccessType : PuzzleAccessType.Free;
            if (!_policies.TryGetValue(type, out var policy)) policy = _fallback;

            return policy.Build(category, puzzle);
        }
    }
}