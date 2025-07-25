using HelixJump.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HelixJump.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Toggle _musicToggle;
        [SerializeField] private Toggle _sfxToggle;
        [SerializeField] private GameObject _sfxToggleOnGraphic;
        [SerializeField] private GameObject _sfxToggleOffGraphic;
        [SerializeField] private GameObject _musicToggleOnGraphic;
        [SerializeField] private GameObject _musicToggleOffGraphic;
        [SerializeField] private Slider _musicVolumeSlider;
        [SerializeField] private Slider _sfxVolumeSlider;
        [SerializeField] private AudioManager _audioManager;

        private void OnEnable()
        {
            _musicToggle.onValueChanged.AddListener(ToggleMusic);
            _sfxToggle.onValueChanged.AddListener(ToggleSFX);
            _musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
            _sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
        }

        private void Start()
        {
            _musicToggle.isOn = _audioManager.IsMusicOn();
            _sfxToggle.isOn = _audioManager.IsSFXOn();
            _musicVolumeSlider.value = _audioManager.GetMusicVolume();
            _sfxVolumeSlider.value = _audioManager.GetSFXVolume();

            UpdateMusicGraphics(_musicToggle.isOn);
            UpdateSFXGraphics(_sfxToggle.isOn);
        }

        private void OnDisable()
        {
            _musicToggle.onValueChanged.RemoveListener(ToggleMusic);
            _sfxToggle.onValueChanged.RemoveListener(ToggleSFX);
            _musicVolumeSlider.onValueChanged.RemoveListener(SetMusicVolume);
            _sfxVolumeSlider.onValueChanged.RemoveListener(SetSFXVolume);
        }

        public void StartGame() => SceneManager.LoadSceneAsync(1);

        public void Exit() => Application.Quit();

        public void SetMusicVolume(float volume) => _audioManager.SetMusicVolume(volume);

        public void SetSFXVolume(float volume) => _audioManager.SetSFXVolume(volume);

        public void ToggleMusic(bool isOn)
        {
            _audioManager.ToggleMusic(isOn);
            UpdateMusicGraphics(isOn);
        }

        public void ToggleSFX(bool isOn)
        {
            _audioManager.ToggleSFX(isOn);
            UpdateSFXGraphics(isOn);
        }

        private void UpdateMusicGraphics(bool isOn)
        {
            _musicToggleOnGraphic.SetActive(isOn);
            _musicToggleOffGraphic.SetActive(!isOn);
        }

        private void UpdateSFXGraphics(bool isOn)
        {
            _sfxToggleOnGraphic.SetActive(isOn);
            _sfxToggleOffGraphic.SetActive(!isOn);
        }
    }
}