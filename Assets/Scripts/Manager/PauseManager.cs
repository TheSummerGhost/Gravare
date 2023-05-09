using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;

    [SerializeField] private Button loadButton;

    public bool isPaused { get; private set; }   

    
    // Start is called before the first frame update
    void Start()
    {
        pauseScreen.SetActive(false);
        if (!PlayerPrefs.HasKey("Level"))
        {
            loadButton.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0.0f;
            pauseScreen.SetActive(true);
        }
        else
        {
            Time.timeScale = 1.0f;
            pauseScreen.SetActive(false);   
        }
    }

    public void Save ()
    {
        SaveLoadManager.Save();
        loadButton.interactable= true;
    }

    public void Load ()
    {
        Time.timeScale = 1.0f;
        SaveLoadManager.Load();
    }

    public void Settings()
    {

    }

    public void Quit ()
    {
        Application.Quit();
    }

}
