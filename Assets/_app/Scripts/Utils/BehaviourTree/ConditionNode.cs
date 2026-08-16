using System;

namespace STD.Utils.BehaviourTree
{
    public class ConditionNode : Node
    {
        private Func<bool> condition; // Điều kiện để kiểm tra

        public ConditionNode(Func<bool> condition)
        {
            this.condition = condition;
        }

        public override NodeState Evaluate()
        {
            state = condition() ? NodeState.SUCCESS : NodeState.FAILURE;
            return state;
        }
    }
}