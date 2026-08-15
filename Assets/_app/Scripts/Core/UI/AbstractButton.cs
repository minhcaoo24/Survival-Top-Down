using System;
using UnityEngine;

namespace STD.Core.UI
{
    public abstract class AbstractButton : MonoBehaviour
    {

        protected virtual void OnEnable() {}

        protected virtual void OnDisable() {}

        public virtual void HandleClick() {}
    }
}