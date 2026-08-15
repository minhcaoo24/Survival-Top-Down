using UnityEngine;
using STD.Utils;

namespace STD.Core.UI
{
    public class SkillWButton : AbstractButton
    {
        public override void HandleClick()
        {
            Observer.Publish("OnWClick");
        }
    }
}
