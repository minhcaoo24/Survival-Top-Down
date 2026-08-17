using STD.Utils.BehaviourTree;
using STD.Core.Player;
using UnityEngine;
using System.Collections.Generic;

namespace STD.Core.Entity.SiegeMinion
{
    using static STD.Utils.Constants.SiegeMinion;
    public class SiegeMinionBehaviour : AbstractEntity
    {
        [SerializeField] private PlayerScript player;
        private float minDistanceToAttack = 3f;

        [SerializeField] private SiegeMinionAnimationState animationState;
        private Node rootNode;

        protected override void Awake()
        {
            base.Awake();
            player = FindFirstObjectByType<PlayerScript>();
        }

        private void Start()
        {
            SetupBehaviourTree();
        }

        private void Update()
        {
            rootNode.Evaluate();
        }

        private void SetupBehaviourTree()
        {
            // var isNearPlayer = new ConditionNode(IsNearPlayer);
            // var attackPlayer = new ActionNode(AttackPlayer);

            var approachPlayer = new ActionNode(ApproachPlayer);
            rootNode = new SelectorNode(new List<Node>
            {
                approachPlayer
            });
        }

        private bool IsNearPlayer() => Vector3.Distance(this.transform.position, player.transform.position) <= minDistanceToAttack ? true : false;
        private NodeState AttackPlayer() { return NodeState.SUCCESS; }

        private NodeState ApproachPlayer()
        {
            float distance = Vector3.Distance(
                transform.position,
                player.transform.position
                );

            Vector3 direction = player.transform.position - transform.position;
            direction.y = 0f; // Không cho enemy cúi/ngửa theo Player

            // Đã đủ gần → dừng lại
            if (distance <= minDistanceToAttack)
            {
                // animationState.CheckAttack();
                return NodeState.SUCCESS;
            }

            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }

            animationState.ChangeAnimation("Run");
            // Chưa đủ gần → tiếp tục tiến tới
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.transform.position,
                SIEGE_MOVE_SPEED * Time.deltaTime
            );
            return NodeState.RUNNING;
        }
    }
}