using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    #region singelton pattern
    public static GameManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    #endregion

    private void Start()
    {
        loading = false;
        Debug.Log("GAME MANAGER START");
    }

    public bool loading;

    public UnityEvent savePressed;







}
