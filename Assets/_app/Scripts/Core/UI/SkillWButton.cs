using UnityEngine;

namespace STD.Core.UI
{
    public class SkillWButton : AbstractButton
    {
        public override void HandleClick()
        {
            base.HandleClick();
            Debug.Log("WWW");
        }
    }
}
