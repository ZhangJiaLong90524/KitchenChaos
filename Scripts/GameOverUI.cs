using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredValue;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += (_, _)=>
        {
            if (GameManager.Instance.IsGameOver)
            {
                Show();
            }
            else
            {
                Hide();
            }
        };


        Hide();
    }

    private void Update()
    {
        recipesDeliveredValue.text = DeliveryManager.Instance.SuccessDeliveryCount.ToString();
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