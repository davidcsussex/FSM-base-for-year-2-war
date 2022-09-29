
using UnityEngine;
namespace Player
{
    public class StandingState : State
    {


        // constructor
        public StandingState(PlayerScript player, StateMachine sm) : base(player, sm)
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

            if( player.ReadSpaceBar() == true )
            {
                //sm.ChangeState(player.jumpingState);  
                Debug.Log("changed to jumping state");
            }

        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}