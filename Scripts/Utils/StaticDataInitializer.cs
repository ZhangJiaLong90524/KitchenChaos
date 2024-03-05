using Counter;
using UnityEngine;

namespace Utils
{
    public class StaticDataInitializer : MonoBehaviour
    {
        private void Awake()
        {
            Counter.Counter.InitializeStaticData();
            CuttingCounter.InitializeStaticData();
            TrashCounter.InitializeStaticData();
        }
    }
}