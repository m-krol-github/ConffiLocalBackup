using UnityEngine;

namespace Conffi.UI
{
    public abstract class BaseView : MonoBehaviour
    {
        protected virtual void ShowView()
        {
            this.gameObject.SetActive(true);
        }

        protected virtual void HideView()
        {
            this.gameObject.SetActive(false);
        }
    }
}