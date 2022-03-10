namespace StateMachines
{
    public class StateMachine
    {
        public BaseState CurrentState => currentState;
        BaseState previousState;
        BaseState currentState;
        BaseState nextState;

        public void Initialize(BaseState initial)
        {
            currentState = initial;
            nextState = currentState;
            currentState?.Enter(null);
        }

        public void Update() {
            // Switch states if needed
            if (nextState != currentState) {
                currentState.Exit();
                previousState = currentState;
                currentState = nextState;
                currentState.Enter(previousState);
            }

            currentState.Update();
        }

        public void InputUpdate() {
            currentState.InputUpdate();
        }

        public void ChangeState(BaseState newState) {
            if (newState != null)
                nextState = newState;
        }
    }
}