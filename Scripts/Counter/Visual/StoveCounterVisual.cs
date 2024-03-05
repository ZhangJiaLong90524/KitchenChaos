using UnityEngine;

namespace Counter.Visual
{
    public class StoveCounterVisual : MonoBehaviour
    {
        [SerializeField] private GameObject stoveOnVisual;
        [SerializeField] private GameObject sizzlingParticles;
        [SerializeField] private StoveCounter stoveCounter;

        private void Start()
        {
            stoveCounter.OnStateChanged += (_, args)=>
            {
                var showVisual = args.State is StoveCounter.FryingState.Frying or StoveCounter.FryingState.Burning;
                stoveOnVisual.SetActive(showVisual);
                sizzlingParticles.SetActive(showVisual);
            };
        }
    }
}