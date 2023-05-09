using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField] private Button loadButton;

    private string levelFromLoad;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Level"))
        {
            loadButton.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Level0");
    }

    public void LoadGame()
    {
        GameManager.instance.loading = true;
        SaveLoadManager.LoadLevel();
    }

    public void Settings()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}
