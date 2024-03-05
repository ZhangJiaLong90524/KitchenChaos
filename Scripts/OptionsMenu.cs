using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject rebindingScreen;

    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private TextMeshProUGUI soundEffectsValueText;
    [SerializeField] private Button musicButton;
    [SerializeField] private TextMeshProUGUI musicValueText;
    [SerializeField] private Button closeButton;


    [SerializeField] private KeyRebindingPair moveUpKeyRebindingPair;
    [SerializeField] private KeyRebindingPair moveDownKeyRebindingPair;
    [SerializeField] private KeyRebindingPair moveLeftKeyRebindingPair;
    [SerializeField] private KeyRebindingPair moveRightKeyRebindingPair;
    [SerializeField] private KeyRebindingPair interactKeyRebindingPair;
    [SerializeField] private KeyRebindingPair interactAlternateKeyRebindingPair;

    public static OptionsMenu Instance { get; private set; }


    private void Awake()
    {
        Instance = this;


        soundEffectsButton.onClick.AddListener(()=>
        {
            SoundManager.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(()=>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        closeButton.onClick.AddListener(()=>
        {
            Hide();


            PauseMenu.Instance.Show();
        });


        moveUpKeyRebindingPair.rebindButton.onClick.AddListener(()=>{ RebindBinding(GameInput.Binding.MoveUp); });
        moveDownKeyRebindingPair.rebindButton.onClick.AddListener(()=>{ RebindBinding(GameInput.Binding.MoveDown); });
        moveLeftKeyRebindingPair.rebindButton.onClick.AddListener(()=>{ RebindBinding(GameInput.Binding.MoveLeft); });
        moveRightKeyRebindingPair.rebindButton.onClick.AddListener(()=>{ RebindBinding(GameInput.Binding.MoveRight); });
        interactKeyRebindingPair.rebindButton.onClick.AddListener(()=>{ RebindBinding(GameInput.Binding.Interact); });
        interactAlternateKeyRebindingPair.rebindButton.onClick.AddListener(()=>
        {
            RebindBinding(GameInput.Binding.InteractAlternate);
        });
    }


    private void Start()
    {
        GameManager.Instance.OnGameUnPaused += (_, _)=>Hide();


        UpdateVisual();


        HideRebindingScreen();


        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectsValueText.text = "Sound Effects: " + SoundManager.Volume;
        musicValueText.text = "Music: " + MusicManager.Volume;


        moveUpKeyRebindingPair.boundKeyName.text = GameInput.Instance.GetBindingKeyName(GameInput.Binding.MoveUp);
        moveDownKeyRebindingPair.boundKeyName.text =
            GameInput.Instance.GetBindingKeyName(GameInput.Binding.MoveDown);
        moveLeftKeyRebindingPair.boundKeyName.text =
            GameInput.Instance.GetBindingKeyName(GameInput.Binding.MoveLeft);
        moveRightKeyRebindingPair.boundKeyName.text =
            GameInput.Instance.GetBindingKeyName(GameInput.Binding.MoveRight);
        interactKeyRebindingPair.boundKeyName.text =
            GameInput.Instance.GetBindingKeyName(GameInput.Binding.Interact);
        interactAlternateKeyRebindingPair.boundKeyName.text =
            GameInput.Instance.GetBindingKeyName(GameInput.Binding.InteractAlternate);
    }

    private void RebindBinding(GameInput.Binding binding)
    {
        ShowRebindingScreen();


        GameInput.Instance.RebindBinding(binding, ()=>
        {
            HideRebindingScreen();


            UpdateVisual();
        });
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowRebindingScreen()
    {
        rebindingScreen.SetActive(true);
    }

    private void HideRebindingScreen()
    {
        rebindingScreen.SetActive(false);
    }
}