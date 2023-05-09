using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{

    private string levelFromLoad;

    // Start is called before the first frame update
    void Start()
    {
        
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
        SaveLoadManager.Load();
    }

    public void Settings()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}
