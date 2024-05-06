
using UnityEngine;
namespace Player
{
    public class JumpingState : State
    {


        // constructor
        public JumpingState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.rb.velocity = new Vector2(player.rb.velocity.x, 10);
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

            if( JumpEnded() )
                sm.ChangeState(player.standingState);
        }

        bool JumpEnded()
        {
            if (player.rb.velocity.y < 0)
            {
                if (player.IsGrounded() == true)
                    return true;
            }
                return false;
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}