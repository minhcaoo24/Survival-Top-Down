using UnityEngine;

namespace STD.Core.UI
{
    public class SkillRButton : AbstractButton
    {
        public override void HandleClick()
        {
            base.HandleClick();
            Debug.Log("RRR");
        }
    }
}
