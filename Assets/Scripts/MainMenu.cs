using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public GameObject optionsScreen;
    public GameObject characterselectionScreen;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void StartGame()
    {
      characterselectionScreen.SetActive(true);
    }   
    public void CloseCharacterselectionscreen()
    {
      characterselectionScreen.SetActive(false);
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
