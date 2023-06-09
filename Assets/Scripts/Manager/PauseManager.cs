using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;

    [SerializeField] private Button loadButton;

    [SerializeField] private Button saveButton;

    [SerializeField] private GameObject settingsScreen;

    public bool isPaused { get; private set; }   

    
    // Start is called before the first frame update
    void Start()
    {
        settingsScreen.SetActive(false);
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
            PlayerController player = FindObjectOfType<PlayerController>();
            if (player == null)
            {
                saveButton.interactable = false;
            }
            else
            {
                saveButton.interactable = true;
            }
        }
        else
        {
            Time.timeScale = 1.0f;
            pauseScreen.SetActive(false);
        }
    }

    public void Save ()
    {
        
        GameManager.instance.savePressed.Invoke();
        SaveLoadManager.SaveLevel();
        loadButton.interactable= true;
    }

    public void Load ()
    {
        Time.timeScale = 1.0f;
        GameManager.instance.loading = true;
        SaveLoadManager.LoadLevel();
    }

    public void Settings()
    {
        settingsScreen.SetActive(true);
    }

    public void Quit ()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
