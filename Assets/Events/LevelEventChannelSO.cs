using UnityEngine;
using UnityEngine.Events;
using System;

namespace EventChannels
{
    [CreateAssetMenu(fileName = "LevelEventChannel", menuName = "Events/Level Event Channel")]
    public class LevelEventChannelSO : ScriptableObject
    {
        public event Action OnKnifeDeflected;
        public UnityAction OnLogDestroyed;
        public UnityAction OnAppleDestroyed;

        public void DeflectKnife() => OnKnifeDeflected?.Invoke();
        public void DestroyLog() => OnLogDestroyed?.Invoke();
        public void DestroyApple() => OnAppleDestroyed?.Invoke();
    }
}