using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewToggleEventChannel", menuName = "Events/Toggle Event Channel")]
public class ToggleEventChannelSO : MonoBehaviour
{
    public event UnityAction<bool> OnEventRaised;

    public void RaiseEvent(bool toggle) => OnEventRaised?.Invoke(toggle);
}
