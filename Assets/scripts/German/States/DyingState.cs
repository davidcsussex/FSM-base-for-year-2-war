
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
namespace Hitler
{
    public class DyingState : State
    {

        float timeToThrow;
        float brightness;

        // constructor
        public DyingState(HitlerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();

            enemy.anim.SetBool("dying", true);

            brightness = 1;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

    

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            enemy.model.GetComponent<Renderer>().material.color = new Color(brightness, brightness, brightness, 1);

            if (brightness > 0)
            {
                brightness -= 0.001f;
            }
            else
            {
                GameObject.Destroy(enemy.gameObject);
                GameObject.Instantiate(enemy.explosionPrefab, enemy.gameObject.transform.position, Quaternion.identity);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}