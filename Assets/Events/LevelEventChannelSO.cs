using UnityEngine;
using UnityEngine.Events;
using System;

namespace EventChannels
{
    [CreateAssetMenu(fileName = "LevelEventChannel", menuName = "Events/Level Event Channel")]
    public class LevelEventChannelSO : ScriptableObject
    {
        public UnityAction<int> OnMaxKnivesUpdated;
        public UnityAction<int> OnKnivesUpdated;
        public UnityAction OnKnifeThrown;
        public UnityAction OnKnifeHit;
        public event Action OnKnifeDeflected;
        public UnityAction OnLogDestroyed;
        public UnityAction OnAppleDestroyed;

        public void UpdateMaxKnives(int number) => OnMaxKnivesUpdated?.Invoke(number);
        public void UpdateKnives(int number) => OnKnivesUpdated?.Invoke(number);
        public void ThrowKnife() => OnKnifeThrown?.Invoke();
        public void HitKnife() => OnKnifeHit?.Invoke();
        public void DeflectKnife() => OnKnifeDeflected?.Invoke();
        public void DestroyLog() => OnLogDestroyed?.Invoke();
        public void DestroyApple() => OnAppleDestroyed?.Invoke();
    }
}