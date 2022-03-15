using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

using EventChannels;
using UI;

public class UIManager : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] GameEventChannelSO gameEvents;

    [Header("UI")]
    [SerializeField] EventSystem eventSystem;
    [SerializeField] UIPanel menuPanel;
    [SerializeField] UIPanel levelPanel;
    [SerializeField] UIPanel gameOverPanel;
    [SerializeField] TextMeshProUGUI scoreLabel;
    [SerializeField] TextMeshProUGUI pointsLabel;
    [SerializeField] TextMeshProUGUI highscoreLabel;

    void OnEnable()
    {
        gameEvents.OnInputToggled += OnInputToggled;
        gameEvents.OnScoreUpdated += OnScoreUpdated;
        gameEvents.OnPointsUpdated += OnPointsUpdated;
        gameEvents.OnHighscoreUpdated += OnHighscoreUpdated;
    }

    void OnDisable()
    {
        gameEvents.OnInputToggled -= OnInputToggled;
        gameEvents.OnScoreUpdated -= OnScoreUpdated;
        gameEvents.OnPointsUpdated -= OnPointsUpdated;
        gameEvents.OnHighscoreUpdated -= OnHighscoreUpdated;
    }

    public void OpenMenuPanel() => menuPanel.Open();
    public void CloseMenuPanel() => menuPanel.Close();
    public void OpenLevelPanel() => levelPanel.Open();
    public void CloseLevelPanel() => levelPanel.Close();
    public void OpenGameOverPanel() => gameOverPanel.Open();
    public void CloseGameOverPanel() => gameOverPanel.Close();

    void OnInputToggled(bool enable)
    {
        eventSystem.enabled = enable;
    }

    void OnScoreUpdated(int score) => scoreLabel.text = $"{score}";

    void OnPointsUpdated(int points) => pointsLabel.text = $"{points}";

    void OnHighscoreUpdated(int highscore) => highscoreLabel.text = $"Highscore: {highscore}";
}
