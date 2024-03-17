using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string CharacterSelectionScreen;

    public GameObject optionsScreen;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void StartGame()
    {
        SceneManager.LoadScene(CharacterSelectionScreen);
    }

    public void OpenOptions()
    {
       optionsScreen.SetActive(true);
    }
    
    public void CloseOptions()
    {
       optionsScreen.SetActive(false);
    }

    public void ExitGame()
    {
       Application.Quit();
       Debug.Log("Quitting");
    }
}
