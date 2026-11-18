using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject SettingsPanel;

    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button settingsButton;

    [SerializeField] private Button backButton;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [SerializeField] private string firstLevelSceneName = "GameScene";

    private InputAction cancelAction;

    private const string SAVE_LEVEL_KEY = "SavedLevel";

    private void Awake()
    {
        cancelAction = new InputAction("Cancel", binding: "<Keyboard>/escape");

        cancelAction.AddBinding("<Gamepad>/buttonEast");
    }

    private void OnEnable()
    {
        cancelAction.Enable();
        cancelAction.performed += OnCancelPerformed;
    }

    private void OnCancelPerformed(InputAction.CallbackContext obj)
    {
        cancelAction.performed -= OnCancelPerformed;
        cancelAction.Disable();
    }

    private void Start()
    {
        mainPanel.SetActive(true);
        SettingsPanel.SetActive(false);

        bool hasSave = PlayerPrefs.HasKey(SAVE_LEVEL_KEY);

        continueButton.interactable = hasSave;

        newGameButton.onClick.AddListener(OnNewGameClicked);
        continueButton.onClick.AddListener(OnContinueClicked);
        settingsButton.onClick.AddListener(OnSettingsClicked);
        quitButton.onClick.AddListener(OnQuitClicked);
        backButton.onClick.AddListener(OnBackClicked);

        musicSlider.value = PlayerPrefs.GetFloat("MusicVol", 0.75f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVol", 0.75f);

        musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXSliderChanged);

        SelectUIElement(newGameButton.gameObject);

    }

    private void OnNewGameClicked()
    {
        PlayerPrefs.DeleteKey("SavedTownData");
        PlayerPrefs.SetString(SAVE_LEVEL_KEY, firstLevelSceneName);
        PlayerPrefs.Save();
        SceneManager.LoadScene(firstLevelSceneName);
    }

    private void OnContinueClicked()
    {
        if(PlayerPrefs.HasKey(SAVE_LEVEL_KEY))
        {
            string sceneToLoad = PlayerPrefs.GetString(SAVE_LEVEL_KEY);
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private void OnSettingsClicked()
    {
        mainPanel.SetActive(false);
        SettingsPanel.SetActive(true);
        SelectUIElement(musicSlider.gameObject);
    }

    private void OnBackClicked()
    {
        SettingsPanel.SetActive(false);
        mainPanel.SetActive(true);
        SelectUIElement(newGameButton.gameObject);
    }

    private void OnQuitClicked()
    {
        Application.Quit();
    }

    private void OnMusicSliderChanged(float value)
    {
        if(AudioManager.Instance != null)
        {
            AudioManager.Instance.SetMusicVolume(value);
        }
    }

    private void OnSFXSliderChanged(float value)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetSFXVolume(value);
        }
    }

    private void SelectUIElement(GameObject element)
    {
        if (EventSystem.current != null && element != null)
        {
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(element);
        }
    }

}
