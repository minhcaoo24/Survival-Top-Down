using UnityEngine;

namespace STD.Core.UI
{
    public class SkillEButton : AbstractButton
    {
        public override void HandleClick()
        {
            base.HandleClick();
            Debug.Log("EEE");
        }
    }
}
