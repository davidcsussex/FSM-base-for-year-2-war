
using UnityEngine;
namespace Player
{
    public class DelayState : State
    {


        // constructor
        public DelayState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
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

            sm.delay -= Time.deltaTime;
            if( sm.delay < 0 )
            {
                sm.ChangeState(sm.nextState);
            }

            Debug.Log("delay=" + sm.delay);
            Debug.Log("next state after delay=" + sm.nextState);


        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}