using UnityEngine;
using UnityEngine.Events;

namespace EventChannels
{
    [CreateAssetMenu(fileName = "GameEventChannel", menuName = "Events/Game Event Channel")]
    public class GameEventChannelSO : ScriptableObject
    {
        public event UnityAction OnMenuOpened;
        public event UnityAction OnGameStarted;
        public event UnityAction OnGameFinished;
        public event UnityAction OnLevelStarted;
        public event UnityAction<bool> OnLevelFinished;

        public void OpenMenu() => OnMenuOpened?.Invoke();
        public void StartGame() => OnGameStarted?.Invoke();
        public void FinishGame() => OnGameFinished?.Invoke();
        public void StartLevel() => OnLevelStarted?.Invoke();
        public void FinishLevel(bool won) => OnLevelFinished?.Invoke(won);
    }
}