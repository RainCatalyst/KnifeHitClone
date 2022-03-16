using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewBoundIntEventChannel", menuName = "Events/Bound Int Event Channel")]
public class BoundIntEventChannelSO : ScriptableObject
{
    public event UnityAction<int> OnValueUpdated;
    public event UnityAction<int> OnMaxValueUpdated;
    public event UnityAction<int> OnMinValueUpdated;

    public void UpdateValue(int value) => OnValueUpdated?.Invoke(value);
    public void UpdateMaxValue(int value) => OnMaxValueUpdated?.Invoke(value);
    public void UpdateMinValue(int value) => OnMinValueUpdated?.Invoke(value);
}