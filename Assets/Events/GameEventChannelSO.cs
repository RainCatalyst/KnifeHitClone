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
        public UnityAction<bool> OnInputToggled;
        public UnityAction OnScoreAdded;
        public UnityAction OnPointAdded;
        public UnityAction<int> OnPointsUpdated;
        public UnityAction<int> OnScoreUpdated;
        public UnityAction<int> OnHighscoreUpdated;

        public void StartGame() => OnGameStarted?.Invoke();
        public void FinishGame() => OnGameFinished?.Invoke();
        public void StartLevel() => OnLevelStarted?.Invoke();
        public void FinishLevel(bool won) => OnLevelFinished?.Invoke(won);
        public void ToggleInput(bool enable) => OnInputToggled?.Invoke(enable);
        public void AddPoint() => OnPointAdded?.Invoke();
        public void AddScore() => OnScoreAdded?.Invoke();
        public void UpdateScore(int score) => OnScoreUpdated?.Invoke(score);
        public void UpdatePoints(int points) => OnPointsUpdated?.Invoke(points);
        public void UpdateHighscore(int highscore) => OnHighscoreUpdated?.Invoke(highscore);
    }
}