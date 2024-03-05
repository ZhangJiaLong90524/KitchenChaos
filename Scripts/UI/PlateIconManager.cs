using KitchenObject;
using UnityEngine;

namespace UI
{
    public class PlateIconManger : MonoBehaviour
    {
        [SerializeField] private Plate plate;
        [SerializeField] private GameObject iconTemplate;

        private void Start()
        {
            while (transform.childCount > 0)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }


            plate.OnIngredientAdded += (_, args)=>
            {
                var icon = Instantiate(iconTemplate, transform);
                icon.GetComponent<PlateIcon>().UpdateIconImage(args.AddedKitchenObjectProperties);
            };
        }
    }
}