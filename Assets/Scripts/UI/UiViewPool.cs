using System.Collections.Generic;
using UnityEngine;

namespace Jigsawgram.UI
{
    public class UiViewPool<T> where T : Component
    {
        private readonly T _prefab;
        private readonly RectTransform _parent;
        private readonly List<T> _items = new List<T>();

        public UiViewPool(T prefab, RectTransform parent)
        {
            _prefab = prefab;
            _parent = parent;
        }

        public T Get()
        {
            for (var i = 0; i < _items.Count; i++)
            {
                var item = _items[i];
                if (item != null && !item.gameObject.activeSelf)
                {
                    item.gameObject.SetActive(true);
                    item.transform.SetAsLastSibling();
                    return item;
                }
            }

            var instance = Object.Instantiate(_prefab, _parent);
            _items.Add(instance);
            return instance;
        }

        public void ReleaseAll()
        {
            for (var i = 0; i < _items.Count; i++)
            {
                var item = _items[i];
                if (item != null)
                {
                    item.gameObject.SetActive(false);
                }
            }
        }
    }
}
