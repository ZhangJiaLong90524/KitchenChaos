using Counter;
using ScriptableObject;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace KitchenObject
{
    public class KitchenObject : MonoBehaviour
    {
        [FormerlySerializedAs("kitchenObjectSo")]
        [FormerlySerializedAs("kitchenObjectScriptableObject")]
        [SerializeField]
        private KitchenObjectProperties kitchenObjectProperties;

        private IKitchenObjectHolder _holder;

        public KitchenObjectProperties GetProperties()=>kitchenObjectProperties;

        protected void SetHolder(IKitchenObjectHolder kitchenObjectHolder)
        {
            _holder = kitchenObjectHolder;


            Transform transform1;
            (transform1 = transform).parent = kitchenObjectHolder.GetHolderTransform();


            if (kitchenObjectHolder.GetType() == typeof(StoveCounter))
            {
                transform.SetLocalPositionAndRotation(Vector3.zero, quaternion.identity);

                return;
            }


            SetRandomLocalPositionAndRotation(transform1);
        }

        public void ChangeHolder(IKitchenObjectHolder newHolder, IKitchenObjectHolder oldHolder)
        {
            SetHolder(newHolder);


            newHolder.SetKitchenObject(this);


            oldHolder.ClearKitchenObject();
        }

        public IKitchenObjectHolder GetHolder()=>_holder;

        public void DestroySelf(float delay = 0)
        {
            _holder.ClearKitchenObject();

            Destroy(gameObject, delay);
        }

        public static KitchenObject Spawn(KitchenObjectProperties properties, IKitchenObjectHolder holder)
        {
            var kitchenObject = Instantiate(properties.prefab, holder.GetHolderTransform())
                .GetComponent<KitchenObject>();


            kitchenObject.SetHolder(holder);


            holder.SetKitchenObject(kitchenObject);


            return kitchenObject;
        }


        private static void SetRandomLocalPositionAndRotation(Transform transform)
        {
            var randomPosition = new Vector3(Random.Range(-0.1f, 0.1f), 0, Random.Range(-0.1f, 0.1f));
            var randomRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
            transform.SetLocalPositionAndRotation(randomPosition, randomRotation);
        }
    }
}