using Conffi.Data;
using UnityEngine;

namespace Conffi.Item
{
    public sealed class SideSegment : BaseItem
    {
        [SerializeField] private GameObject fatSide;
        [SerializeField] private GameObject thinSide;
        
        public override void ShowPillow(bool show)
        {
            base.ShowPillow(show);
        }

        public void SetSide(ElementSideType type)
        {
            if (type == ElementSideType.Large)
            {
                fatSide.SetActive(true);
                thinSide.SetActive(false);
                print("large");
            }
            else if (type == ElementSideType.Thin)
            {
                fatSide.SetActive(false);
                thinSide.SetActive(true);
                print("thin");
            }
        }
    }
}