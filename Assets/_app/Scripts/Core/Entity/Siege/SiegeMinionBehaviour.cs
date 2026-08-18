using STD.Utils.BehaviourTree;
using STD.Core.Player;
using UnityEngine;
using System.Collections.Generic;
using STD.Utils;
using System.Threading.Tasks;

namespace STD.Core.Entity.SiegeMinion
{
    using static STD.Utils.Constants.SiegeMinion;
    public class SiegeMinionBehaviour : MonoBehaviour
    {
        [SerializeField] private PlayerScript player;

        [SerializeField] private float minDistanceToAttack = 3f;
        [SerializeField] private SiegeMinionAnimationState animationState;

        private Node rootNode;

        private float nextAttackTime = 0f;
        [SerializeField] private float attackCooldown = 1f;

        private bool isAttacking;
        private float attackEndTime;

        private void Awake()
        {
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
            var isNearPlayer = new ConditionNode(IsNearPlayer);
            var attackPlayer = new ActionNode(AttackPlayer);

            var isTired = new ConditionNode(IsTired);
            var rest = new ActionNode(Rest);

            var approachPlayer = new ActionNode(ApproachPlayer);

            rootNode = new SelectorNode(new List<Node>
        {
            // 1. Đã bắn → nghỉ
            new SequenceNode(new List<Node>
            {
                isTired,
                rest
            }),

            // 2. Gần player → bắn
            new SequenceNode(new List<Node>
            {
                isNearPlayer,
                attackPlayer
            }),

            // 3. Chưa gần → tiến tới
            approachPlayer
        });
        }

        private bool IsTired()
        {
            return Time.time < nextAttackTime;
        }

        private bool IsNearPlayer()
        {
            if (player == null)
                return false;

            return Vector3.Distance(
                transform.position,
                player.transform.position
            ) <= minDistanceToAttack;
        }

        private NodeState AttackPlayer()
        {
            if (player == null)
            {
                Debug.LogError("Cannot find Player");
                return NodeState.FAILURE;
            }

            if (!isAttacking)
            {
                isAttacking = true;

                animationState.CheckAttack();
                Observer.Publish("Canon_Shoot");

                attackEndTime = Time.time + 1f;

                return NodeState.RUNNING;
            }

            if (Time.time < attackEndTime)
            {
                return NodeState.RUNNING;
            }

            isAttacking = false;

            nextAttackTime = Time.time + attackCooldown;

            return NodeState.SUCCESS;
        }

        private NodeState Rest()
        {
            animationState.ChangeAnimation("Idle1");

            if (Time.time < nextAttackTime)
            {
                return NodeState.RUNNING;
            }

            return NodeState.SUCCESS;
        }

        private NodeState ApproachPlayer()
        {
            if (player == null)
            {
                Debug.LogError("Cannot find Player");
                return NodeState.FAILURE;
            }

            float distance = Vector3.Distance(
                transform.position,
                player.transform.position
            );

            Vector3 direction =
                player.transform.position - transform.position;

            direction.y = 0f;

            if (distance <= minDistanceToAttack)
            {
                return NodeState.SUCCESS;
            }

            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }

            animationState.ChangeAnimation("Run");

            transform.position = Vector3.MoveTowards(
                transform.position,
                player.transform.position,
                SIEGE_MOVE_SPEED * Time.deltaTime
            );

            return NodeState.RUNNING;
        }
    }
}