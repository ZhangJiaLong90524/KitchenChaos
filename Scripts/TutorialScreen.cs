using TMPro;
using UnityEngine;

public class TutorialScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moveUpKeyLabel;
    [SerializeField] private TextMeshProUGUI moveDownKeyLabel;
    [SerializeField] private TextMeshProUGUI moveLeftKeyLabel;
    [SerializeField] private TextMeshProUGUI moveRightKeyLabel;
    [SerializeField] private TextMeshProUGUI interactKeyLabel;
    [SerializeField] private TextMeshProUGUI interactAlternateKeyLabel;

    private void Start()
    {
        GameInput.Instance.OnBindingRebind += (_, _)=>UpdateVisual();


        GameManager.Instance.OnStateChanged += (_, _)=>
        {
            if (!GameManager.Instance.IsWaitingToStart)
            {
                Hide();
            }
        };


        UpdateVisual();


        Show();
    }

    private void UpdateVisual()
    {
        moveUpKeyLabel.text = GameInput.Instance.GetBindingKeyName(GameInput.Binding.MoveUp);
        moveDownKeyLabel.text = GameInput.Instance.GetBindingKeyName(GameInput.Binding.MoveDown);
        moveLeftKeyLabel.text = GameInput.Instance.GetBindingKeyName(GameInput.Binding.MoveLeft);
        moveRightKeyLabel.text = GameInput.Instance.GetBindingKeyName(GameInput.Binding.MoveRight);
        interactKeyLabel.text = GameInput.Instance.GetBindingKeyName(GameInput.Binding.Interact);
        interactAlternateKeyLabel.text = GameInput.Instance.GetBindingKeyName(GameInput.Binding.InteractAlternate);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}