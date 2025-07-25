using HelixJump.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HelixJump.UI
{
    public class GameUIManager : MonoBehaviour
    {
        [SerializeField] private GameEvents _gameEvents;
        [SerializeField] private TMP_Text _hudScoreText;
        [SerializeField] private GameObject _resultPanel;
        [SerializeField] private GameObject _hudPanel;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private TMP_Text _finalScoreText;
        [SerializeField] private TMP_Text _resultText;

        private void OnEnable()
        {
            if (_restartButton != null)
                _restartButton.onClick.AddListener(RestartGame);

            if (_restartButton != null)
                _exitButton.onClick.AddListener(ExitGame);
        }

        private void OnDisable()
        {
            if (_restartButton != null)
                _restartButton.onClick.RemoveListener(RestartGame);

            if (_restartButton != null)
                _exitButton.onClick.RemoveListener(ExitGame);
        }

        private void Start()
        {
            if (_resultPanel != null)
                _resultPanel.SetActive(false);

            if (_hudPanel != null)
                _hudPanel.SetActive(true);
        }

        public void UpdateScoreUI(int score)
        {
            if (_hudScoreText != null)
                _hudScoreText.text = score.ToString("D2");
        }

        public void ShowResultPanel(bool isLevelComplete, int finalScore)
        {
            if (_resultPanel != null)
                _resultPanel.SetActive(true);

            if (_hudPanel != null)
                _hudPanel.SetActive(false);

            if (isLevelComplete)
                _resultText.text = "Level is complete!";
            else
                _resultText.text = "Level failed!";

            // Update final score
            if (_finalScoreText != null)
                _finalScoreText.text = "Final Score: " + finalScore.ToString();
        }

        private void RestartGame() => _gameEvents.RestartGameEvent.RaiseEvent();

        private void ExitGame() => _gameEvents.ExitGameEvent.RaiseEvent();
    }
}