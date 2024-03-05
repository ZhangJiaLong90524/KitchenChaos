using UnityEngine;

public interface IKitchenObjectHolder
{
    public Transform GetHolderTransform();

    public void SetKitchenObject(KitchenObject.KitchenObject kitchenObject);

    public KitchenObject.KitchenObject GetKitchenObject();

    public void ClearKitchenObject();

    public bool HasKitchenObject();
}