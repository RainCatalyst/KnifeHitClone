using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewIntEventChannel", menuName = "Events/Int Event Channel")]
public class IntEventChannelSO : ScriptableObject
{
    public event UnityAction<int> OnValueUpdated;
    public event UnityAction OnValueAdded;

    public void AddValue() => OnValueAdded?.Invoke();
    public void UpdateValue(int value) => OnValueUpdated?.Invoke(value);
}
