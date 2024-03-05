using ScriptableObject;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlateIcon : MonoBehaviour
    {
        [SerializeField] private Image image;

        public void UpdateIconImage(KitchenObjectProperties kitchenObjectProperties)
        {
            image.sprite = kitchenObjectProperties.sprite;
        }
    }
}