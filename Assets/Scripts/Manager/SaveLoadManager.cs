using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveLoadManager
{

    public static void Save()
    {
        PlayerPrefs.SetString("Level", SceneManager.GetActiveScene().name);
    }

    public static string Load()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("Level"));
            return "HASLEVEL";
        } else
        {
            return "NOLEVEL";
        }
    }
}
