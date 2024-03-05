using System;
using System.Collections.Generic;
using UnityEngine;

namespace Counter.Visual
{
    public class PlatesCounterVisual : MonoBehaviour
    {
        [SerializeField] private PlatesCounter platesCounter;
        [SerializeField] private GameObject plateVisualPrefab;

        [SerializeField] private Transform platesCounterTopPoint;
        [SerializeField] private float plateHeight;

        private readonly Stack<GameObject> _plateVisuals = new();

        private void Start()
        {
            platesCounter.OnPlateAmountChanged += (_, args)=>
            {
                if (args.PlateAmount > _plateVisuals.Count)
                {
                    var plateVisual = Instantiate(plateVisualPrefab, platesCounterTopPoint.position,
                        platesCounterTopPoint.rotation,
                        platesCounterTopPoint);

                    plateVisual.transform.position += (args.PlateAmount - 1) * plateHeight * Vector3.up;


                    _plateVisuals.Push(plateVisual);
                }
                else if (args.PlateAmount < _plateVisuals.Count)
                {
                    var plateVisual = _plateVisuals.Pop();
                    Destroy(plateVisual);
                }
                else
                {
                    throw new Exception("PlateAmount is not changed");
                }
            };
        }
    }
}