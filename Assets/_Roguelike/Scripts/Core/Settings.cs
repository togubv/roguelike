using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Roguelike
{
    public class Settings : MonoBehaviour
    {
        public ScriptableObject[] pools;

        private static List<ScriptableObject> _pools;

        public void Init()
        {
            _pools = pools.ToList();
        }

        public static T Get<T>()
        {
            if (_pools == null)
            {
                return default;
            }

            foreach (var p in _pools.Where(p => p.GetType() == typeof(T)))
            {
                return (T)(object)p;
            }

            return default;
        }
    }
}
