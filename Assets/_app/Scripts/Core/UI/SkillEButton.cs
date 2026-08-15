using STD.Utils;
using UnityEngine;

namespace STD.Core.UI
{
    public class SkillEButton : AbstractButton
    {
        public override void HandleClick()
        {
            Observer.Publish("OnEClick");
        }
    }
}
