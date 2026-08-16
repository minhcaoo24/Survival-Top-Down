using System;

namespace STD.Utils.BehaviourTree
{
    public class ActionNode : Node
    {
        private Func<NodeState> action; // Hành động để thực hiện

        public ActionNode(Func<NodeState> action)
        {
            this.action = action;
        }

        public override NodeState Evaluate()
        {
            state = action();
            return state;
        }
    }
}