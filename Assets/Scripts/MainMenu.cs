using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string CharacterSelectionScreen;
    void Start()
    {
        SceneManager.LoadScene(CharacterSelectionScreen);
    }

    void Update()
    {
        
    }
    public void StartGame()
    {

    }

    public void OpenOptions()
    {

    }
    
    public void CloseOptions()
    {

    }

    public void ExitGame()
    {
       Application.Quit();
       Debug.Log("Quitting");
    }
}
