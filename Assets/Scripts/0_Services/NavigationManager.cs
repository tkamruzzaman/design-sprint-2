using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationManager : MonoBehaviour
{
    //private Scenes scenes;
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        print(scene.name + "   " + scene.buildIndex);
        print(loadSceneMode);
    }

    public Scenes? GetCurrentScene()
    {
        return (Scenes)SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadScene(Scenes scene)
    {
        SceneManager.LoadScene((int)scene);
    }
}

public enum Scenes
{
  MainMenu = 0,
  Gameplay = 1,
}
