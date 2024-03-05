using System;
using System.Collections.Generic;
using System.Linq;
using KitchenObject;
using ScriptableObject;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance;
    [SerializeField] private List<OrderRecipe> availableOrderRecipes = new();

    [SerializeField] private int maxWaitingOrderRecipes = 4;

    [SerializeField] private float orderIntervalTime = 4f;
    private readonly List<OrderRecipe> _waitingOrderRecipes = new();
    private float _newOrderTimer;

    public int SuccessDeliveryCount { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (_waitingOrderRecipes.Count >= maxWaitingOrderRecipes ||
            !GameManager.Instance.IsGamePlaying)
        {
            return;
        }


        _newOrderTimer -= Time.deltaTime;


        if (_newOrderTimer <= 0)
        {
            _newOrderTimer = orderIntervalTime;


            _waitingOrderRecipes.Add(availableOrderRecipes[Random.Range(0, availableOrderRecipes.Count)]);


            OnRecipeListChanged?.Invoke(this, new RecipeListChangedEventArgs
            {
                IsNewOrder = true,
                OrderRecipe = _waitingOrderRecipes.Last()
            });
        }
    }

    public event EventHandler<RecipeListChangedEventArgs> OnRecipeListChanged;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    public void ReadyForPickUp(Plate plate)
    {
        var matchedOrder = _waitingOrderRecipes.FirstOrDefault(orderRecipe=>
            plate.KitchenObjectsInPlate.All(plateKitchenObject=>
                orderRecipe.kitchenObjectPropertiesList.Contains(plateKitchenObject)) &&
            orderRecipe.kitchenObjectPropertiesList.All(orderKitchenObject=>
                plate.KitchenObjectsInPlate.Contains(orderKitchenObject)));

        if (matchedOrder != null)
        {
            _waitingOrderRecipes.Remove(matchedOrder);


            SuccessDeliveryCount++;


            OnRecipeListChanged?.Invoke(this, new RecipeListChangedEventArgs
            {
                OrderRecipe = matchedOrder
            });


            OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        }
    }

    public List<OrderRecipe> GetWaitingOrderRecipes()=>Instance._waitingOrderRecipes;

    public class RecipeListChangedEventArgs
    {
        public bool IsNewOrder;
        public OrderRecipe OrderRecipe;
    }
}