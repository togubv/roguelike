using UnityEngine;

namespace Roguelike
{
    public class IView : MonoBehaviour
    {
        public void Init()
        {

        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
            AddHandlers();
        }

        public virtual void Hide()
        {
            RemoveHandlers();
            gameObject.SetActive(false);
        }

        protected virtual void AddHandlers() { }

        protected virtual void RemoveHandlers() { }
    }
}
