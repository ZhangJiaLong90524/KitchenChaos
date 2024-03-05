using UnityEngine;
using UnityEngine.Serialization;

namespace Counter.Visual
{
    public class CounterSelectedVisual : MonoBehaviour
    {
        [FormerlySerializedAs("counter")] [SerializeField]
        private Counter counterScript;

        [FormerlySerializedAs("visualGameObject")] [SerializeField]
        private GameObject selectedVisualGameObject;

        private void Start()
        {
            Player.Player.Instance.OnSelectedCounterChanged += PlayerOnOnSelectedCounterChanged;
        }

        private void PlayerOnOnSelectedCounterChanged(object sender, Player.Player.SelectedCounterChangedEventArgs e)
        {
            if (e.SelectedCounter == counterScript)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        private void Show()
        {
            selectedVisualGameObject.SetActive(true);
        }

        private void Hide()
        {
            selectedVisualGameObject.SetActive(false);
        }
    }
}