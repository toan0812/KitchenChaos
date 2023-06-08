using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader 
{
    public enum scene
    {
        MainMenuScene,
        GameScene,
        LoadingScene,
    }
    private static scene tagetScene;

    public static void Load(scene tagetStringscene)
    {
        Loader.tagetScene = tagetStringscene;
        SceneManager.LoadScene(Loader.scene.LoadingScene.ToString());
        
    }

    public static void loaderCallBack()
    {
        SceneManager.LoadScene(tagetScene.ToString());
    }
}
