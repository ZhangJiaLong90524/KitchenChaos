using Scene;
using UnityEditor;
using UnityEngine;
using Button = UnityEngine.UI.Button;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(()=>{ SceneLoader.Load(SceneLoader.SceneName.GameScene); });


        quitButton.onClick.AddListener(()=>
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        });
    }
}