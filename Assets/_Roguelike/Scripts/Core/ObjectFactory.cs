using UnityEngine;

namespace Roguelike
{
    public static class ObjectFactory
    {
        public static T Create<T>(string prefabPath, Transform parent = null)
        {
            var go = (GameObject)Object.Instantiate(Resources.Load(prefabPath));

            if (go == null)
            {
                throw new System.NullReferenceException($"Cant create prefab with path {prefabPath}");
            }

            if (parent != null)
            {
                go.transform.SetParent(parent, false);
            }

            var result = go.GetComponent<T>() ?? go.GetComponentInChildren<T>();

            return result;
        }
    }
}
