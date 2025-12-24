using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Jigsawgram.UI
{
    public class CatalogService
    {
        private readonly IPuzzleCatalogProvider _provider;
        private CatalogModel _cached;

        public CatalogService(IPuzzleCatalogProvider provider)
        {
            _provider = provider;
        }

        public CatalogModel Current => _cached;

        public async UniTask<CatalogModel> LoadCatalogAsync(bool forceReload = false)
        {
            if (!forceReload && _cached != null) return _cached;

            if (_provider == null)
            {
                _cached = new CatalogModel(new List<PuzzleCategoryModel>());
                return _cached;
            }

            var catalog = await _provider.LoadCatalogAsync();
            _cached = catalog ?? new CatalogModel(new List<PuzzleCategoryModel>());
            return _cached;
        }
    }
}