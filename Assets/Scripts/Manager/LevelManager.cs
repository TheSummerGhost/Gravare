using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private GameObject player; // prefab of player
    [SerializeField] private float respawnTime;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject key;

    private string levelToMoveTo;

    private string currentLevel;

    private int currentLevelNumber;

    private float respawnTimeStart;

    private bool respawn;

    [SerializeField] private bool isLastLevel;
    
    private CinemachineVirtualCamera CVC;

    private void Start()
    {
        {
            CVC = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();
            currentLevel = SceneManager.GetActiveScene().name.Substring(5);
            currentLevelNumber = int.Parse(currentLevel);
            levelToMoveTo = "Level" + (currentLevelNumber+1).ToString();
            Debug.Log(currentLevel + levelToMoveTo);
            GameManager.instance.loading = false;
        }
    }

    private void Update()
    {
        CheckRespawn();
    }

    public void Respawn() 
    {
        respawnTimeStart = Time.time;
        respawn = true;
    }

    private void CheckRespawn()
    {
        if (Time.time >= respawnTimeStart + respawnTime && respawn)
        {
            var playerTemp = Instantiate(player, respawnPoint);
            CVC.m_Follow = playerTemp.transform;
            respawn = false;
        }
    }

    public void UnlockDoor()
    {   
        door.GetComponent<Door>().OpenDoor();
    }

    public void AdvanceLevel()
    {
        if (!isLastLevel)
        {
            if (levelToMoveTo != null || levelToMoveTo != "")
            {
                SceneManager.LoadScene(levelToMoveTo);
                //StartCoroutine(LoadYourAsyncScene(levelToMoveTo));
                //SceneManager.LoadSceneAsync(levelToMoveTo);


            }
        }
        else
        {
            levelToMoveTo = "EndGame";
            SceneManager.LoadScene(levelToMoveTo); //Async level load
            //StartCoroutine(LoadYourAsyncScene(levelToMoveTo));
        }

    }

    //IEnumerator LoadYourAsyncScene(string level)
    //{
    //    // The Application loads the Scene in the background as the current Scene runs.
    //    // This is particularly good for creating loading screens.
    //    // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
    //    // a sceneBuildIndex of 1 as shown in Build Settings.

    //    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(level);

    //    // Wait until the asynchronous scene fully loads
    //    while (!asyncLoad.isDone)
    //    {
    //        Debug.Log(asyncLoad.progress);
    //        yield return null;
    //    }
    //}




}
