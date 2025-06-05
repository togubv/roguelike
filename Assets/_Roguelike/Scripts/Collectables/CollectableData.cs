using UnityEngine;

namespace Roguelike
{
    public class CollectableObjectData : MonoBehaviour, ICollectable
    {
        public Transform trans { get { return transform; } }
    }
}
