using UnityEngine.SceneManagement;

namespace Scene
{
    public static class SceneLoader
    {
        public enum SceneName
        {
            GameScene,
            MainMenuScene,
            LoadingScene
        }

        private static SceneName _targetSceneName;

        public static void Load(SceneName sceneName)
        {
            _targetSceneName = sceneName;


            SceneManager.LoadScene(SceneName.LoadingScene.ToString());
        }

        public static void CallBack()
        {
            SceneManager.LoadScene(_targetSceneName.ToString());
        }
    }
}