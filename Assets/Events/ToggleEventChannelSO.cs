using UnityEngine;
using UnityEngine.Events;

namespace EventChannels
{
    [CreateAssetMenu(fileName = "NewToggleEventChannel", menuName = "Events/Toggle Event Channel")]
    public class ToggleEventChannelSO : ScriptableObject
    {
        public event UnityAction<bool> OnEventRaised;

        public void RaiseEvent(bool toggle) => OnEventRaised?.Invoke(toggle);
    }
}