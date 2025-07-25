using HelixJump.Events;
using HelixJump.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HelixJump.Core
{
    public class GameManager : MonoBehaviour
    {
        [Header("Game Settings")]
        [SerializeField] private GameEvents _gameEvents;
        [SerializeField] private GameUIManager _gameUIManager;
        [SerializeField] private int _score = 1;
        [SerializeField] private int _consecutiveScore = 10;
        [SerializeField] private int _consecutiveRange = 3;

        private bool _isGameActive = false;
        private int _currentScore = 0;
        private int _consecutives = 0;

        private void OnEnable()
        {
            _gameEvents.ScoreEvent.OnEventRaised += AddScore;
            _gameEvents.BounceEvent.OnEventRaised += ResetConsecutive;
            _gameEvents.GameCompleteEvent.OnEventRaised += WinGame;
            _gameEvents.LoseGameEvent.OnEventRaised += LoseGame;
            _gameEvents.RestartGameEvent.OnEventRaised += RestartGame;
            _gameEvents.ExitGameEvent.OnEventRaised += ExitGame;
        }

        private void OnDisable()
        {
            _gameEvents.ScoreEvent.OnEventRaised -= AddScore;
            _gameEvents.BounceEvent.OnEventRaised -= ResetConsecutive;
            _gameEvents.GameCompleteEvent.OnEventRaised -= WinGame;
            _gameEvents.LoseGameEvent.OnEventRaised -= LoseGame;
            _gameEvents.RestartGameEvent.OnEventRaised -= RestartGame;
            _gameEvents.ExitGameEvent.OnEventRaised -= ExitGame;
        }

        private void Start()
        {
            _gameEvents.GameActiveEvent.RaiseEvent(true);
            _gameUIManager.UpdateScoreUI(_currentScore);
        }

        private void ResetConsecutive() => _consecutives = 0;

        private void AddScore()
        {
            if (!_isGameActive)
            {
                if (_consecutives >= _consecutiveRange)
                    _currentScore += _consecutiveScore;
                else
                    _currentScore += _score;

                _gameUIManager.UpdateScoreUI(_currentScore);
                _consecutives++;
            }
        }

        private void LoseGame()
        {
            if (_isGameActive) return;

            _isGameActive = true;

            _gameUIManager.ShowResultPanel(false, _currentScore);

            _gameEvents.GameActiveEvent.RaiseEvent(false);
        }

        private void WinGame()
        {
            if (_isGameActive) return;

            _isGameActive = true;

            _gameUIManager.ShowResultPanel(true, _currentScore);

            _gameEvents.GameActiveEvent.RaiseEvent(false);
        }

        private void RestartGame() => SceneManager.LoadScene(1);
        private void ExitGame() => Application.Quit();
    }
}