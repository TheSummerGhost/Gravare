using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class AudioManager : MonoBehaviour
{

    private AudioSource m_AudioSource;

    #region singelton pattern
    public static AudioManager instance { get; private set; }

    private float volume;

    private void Awake()
    {



        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            m_AudioSource = GetComponent<AudioSource>();
            m_AudioSource.Play();
            LoadFloatResult result = SaveLoadManager.LoadFloat("Music_Volume");
            if (result.success)
            {
                SetVolume(result.result);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVolume(float newValue)
    {
        m_AudioSource.volume = newValue;
        SaveLoadManager.SaveFloat("Music_Volume", m_AudioSource.volume);
    }

    public float GetVolume()
    {
        return m_AudioSource.volume;
    }
}
