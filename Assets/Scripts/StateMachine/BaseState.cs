namespace StateMachines
{
    public class BaseState
    {
        string name;
        protected StateMachine stateMachine;
        
        public BaseState(string name, StateMachine stateMachine)
        {
            this.name = name;
            this.stateMachine = stateMachine;
        }

        public string Name => name;

        public virtual void Enter(BaseState fromState) {}
        public virtual void Exit() { }
        public virtual void InputUpdate() {}
        public virtual void Update() {}
    }
}

