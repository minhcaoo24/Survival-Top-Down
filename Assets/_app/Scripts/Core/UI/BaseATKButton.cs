using STD.Utils;
using UnityEngine;

namespace STD.Core.UI
{
    public class BaseATKButton : AbstractButton
    {
        public override void HandleClick()
        {
            Observer.Publish("OnBaseATKClick");
        }
    }
}
