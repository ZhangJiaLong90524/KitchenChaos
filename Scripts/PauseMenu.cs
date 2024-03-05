using Scene;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;
    public static PauseMenu Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += (_, _)=>Show();


        GameManager.Instance.OnGameUnPaused += (_, _)=>Hide();


        resumeButton.onClick.AddListener(GameManager.Instance.ToggleGamePause);


        mainMenuButton.onClick.AddListener(()=>
        {
            SceneLoader.Load(SceneLoader.SceneName.MainMenuScene);


            Time.timeScale = 1;
        });


        optionsButton.onClick.AddListener(()=>
        {
            OptionsMenu.Instance.Show();


            Hide();
        });


        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}