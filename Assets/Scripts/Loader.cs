using System.Collections;
using System.ComponentModel;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        MainMenuScene,
        GameScene,
        LoadingScene
    }

    private const Scene LoadingScene = Scene.LoadingScene;

    private static bool _isLoading;
    
    public static void LoadLoadingSceneAndTargetScene(Scene targetScene)
    {
        if (_isLoading) return; 

        _isLoading = true; 

        var asyncLoadLoadingScene = SceneManager.LoadSceneAsync(LoadingScene.ToString(), LoadSceneMode.Single);

        asyncLoadLoadingScene.completed += (operation) =>
        {
            var asyncLoadTargetScene = SceneManager.LoadSceneAsync(targetScene.ToString(), LoadSceneMode.Additive);
            
            asyncLoadTargetScene.completed += (op) => { OnTargetSceneLoaded(LoadingScene.ToString()); };
        };
    }

    private static void OnTargetSceneLoaded(string loadingSceneName)
    {
        SceneManager.UnloadSceneAsync(loadingSceneName);

        _isLoading = false; 
    }
}
