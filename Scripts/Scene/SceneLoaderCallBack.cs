using UnityEngine;

namespace Scene
{
    public class SceneLoaderCallBack : MonoBehaviour
    {
        private bool _isFirstUpdate = true;

        private void Update()
        {
            if (_isFirstUpdate)
            {
                _isFirstUpdate = false;
                SceneLoader.CallBack();
            }
        }
    }
}