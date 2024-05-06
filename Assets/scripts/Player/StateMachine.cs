using UnityEngine;


namespace Player
{
    public class StateMachine : MonoBehaviour
    {
        public State CurrentState { get; private set; }
        public State LastState { get; private set; }

        public float delay;
        public State nextState;

        public void Init(State startingState)
        {
            CurrentState = startingState;
            LastState = null;
            startingState.Enter();
        }

        public void ChangeState(State newState)
        {
            //Debug.Log("Changing state to " + newState);
            CurrentState.Exit();

            LastState = CurrentState;
            CurrentState = newState;
            newState.Enter();
        }

        public void ChangeState( State newState, State nextState, float delay )
        {
            ChangeState(newState);
            this.delay = delay;
            this.nextState = nextState;
        }

        public State GetState()
        {
            return CurrentState;
        }

    }
}