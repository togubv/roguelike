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
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
