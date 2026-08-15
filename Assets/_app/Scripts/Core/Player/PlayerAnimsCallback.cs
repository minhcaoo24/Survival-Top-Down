using STD.Utils;
using UnityEngine;

namespace STD.Core.Player
{
    public class PlayerAnimsCallback : MonoBehaviour
    {
        public void OnFinishedBaseATK() => Observer.Publish("OnFinishedBaseATK");
        public void OnFinishedDash() => Observer.Publish("OnFinishedDash");
    }
}