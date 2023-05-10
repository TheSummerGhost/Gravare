using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveLoadManager
{


    #region Levels

    public static void SaveLevel()
    {
        PlayerPrefs.SetString("Level", SceneManager.GetActiveScene().name);
    }

    public static string LoadLevel()
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

    #endregion

    #region Vector3

    public static void SaveVector3(string entityID, Vector3 data)
    {
        string toSave = data.x.ToString() + "," + data.y.ToString() + "," + data.z.ToString();
        PlayerPrefs.SetString("Vector3_" + entityID, toSave);
    }

    public static LoadVectorResult LoadVector3(string entityID)
    {
        if (PlayerPrefs.HasKey("Vector3_" + entityID))
        {
            string loadedString = PlayerPrefs.GetString("Vector3_" + entityID);
            string[] splitString = loadedString.Split(',');
            Vector3 output = new Vector3();
            output.x = float.Parse(splitString[0]);
            output.y = float.Parse(splitString[1]);
            output.z = float.Parse(splitString[2]);
            return new LoadVectorResult(output, true);
        }
        return new LoadVectorResult(Vector3.zero, false);
    }

    #endregion

    #region bool

    public static void SaveBool(string entityID, bool data)
    {
        PlayerPrefs.SetString("Bool_" + entityID, data.ToString());
    }

    public static LoadBoolResult LoadBool(string entityID)
    {
        if (PlayerPrefs.HasKey("Bool_" + entityID))
        {
            bool output = bool.Parse(PlayerPrefs.GetString("Bool_" + entityID));
            return new LoadBoolResult(output, true);
        }
        else
        {
            return new LoadBoolResult(false, false);
        }
    }

    #endregion

    #region float

    public static void SaveFloat(string entityID, float data)
    {
        PlayerPrefs.SetFloat("Float_" + entityID, data);
    }

    public static LoadFloatResult LoadFloat(string entityID)
    {
        if (PlayerPrefs.HasKey("Float_" + entityID))
        {
            float output = PlayerPrefs.GetFloat("Float_" + entityID);
            return new LoadFloatResult(output, true);
        }
        else
        {
            return new LoadFloatResult(0.0f, false);
        }
    }

    #endregion
}

public class LoadBoolResult
{
    public bool result;
    public bool success;

    public LoadBoolResult(bool result, bool success)
    {
        this.result = result;
        this.success = success;
    }
}


public class LoadVectorResult
{

    public Vector3 result;
    public bool success;

    public LoadVectorResult(Vector3 result, bool success)
    {
        this.result = result;
        this.success = success;
    }
}

public class LoadFloatResult
{

    public float result;
    public bool success;

    public LoadFloatResult(float result, bool success)
    {
        this.result = result;
        this.success = success;
    }
}
