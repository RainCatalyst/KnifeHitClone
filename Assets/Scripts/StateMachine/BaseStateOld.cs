using UnityEngine;

namespace StateMachines
{
    public class BaseStateOld : MonoBehaviour
    {
        string stateName;
        protected StateMachine stateMachine;

        public void Initialize(string stateName, StateMachine stateMachine)
        {
            this.stateName = stateName;
            this.stateMachine = stateMachine;
            OnInitialized();
        }

        public string Name => stateName;

        protected virtual void OnInitialized() { }

        public virtual void Enter(BaseState fromState) { }
        public virtual void Exit() { }
        public virtual void InputUpdateState() { }
        public virtual void UpdateState() { }
    }
}