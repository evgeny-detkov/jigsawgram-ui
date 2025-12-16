using Cysharp.Threading.Tasks;

namespace Jigsawgram.UI
{
    public interface IPuzzleCatalogProvider
    {
        CatalogModel LoadCatalog();
        UniTask<CatalogModel> LoadCatalogAsync();
    }
}