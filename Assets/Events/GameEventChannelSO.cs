using UnityEngine;
using UnityEngine.Events;

namespace EventChannels
{
    [CreateAssetMenu(fileName = "GameEventChannel", menuName = "Events/Game Event Channel")]
    public class GameEventChannelSO : ScriptableObject
    {
        public UnityAction OnGameStarted;
        public UnityAction OnGameFinished;
        public UnityAction OnLevelStarted;
        public UnityAction<bool> OnLevelFinished;

        public void StartGame() => OnGameStarted?.Invoke();
        public void FinishGame() => OnGameFinished?.Invoke();
        public void StartLevel() => OnLevelStarted?.Invoke();
        public void FinishLevel(bool won) => OnLevelFinished?.Invoke(won);
    }
}