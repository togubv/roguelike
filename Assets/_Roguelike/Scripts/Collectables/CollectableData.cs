using UnityEngine;

namespace Roguelike
{
    public class CollectableData : MonoBehaviour, ICollectable
    {
        public Transform trans { get { return transform; } }
    }
}
