using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Key : MonoBehaviour
{
    public string identification = "key";

    private bool hasBeenCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController playerControllerCheck))
        {
            FindObjectOfType<LevelManager>().UnlockDoor();
            hasBeenCollected = true;
            GetComponent<SpriteRenderer> ().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void Awake()
    {
        if (GameManager.instance.loading)
        {
            LoadBoolResult result = SaveLoadManager.LoadBool(identification);
            Debug.Log("Loading");
            if (result.success)
            {
                if (result.result)
                {
                    hasBeenCollected = result.result;
                    FindObjectOfType<LevelManager>().UnlockDoor();
                    GetComponent<SpriteRenderer>().enabled = false;
                    GetComponent<BoxCollider2D>().enabled = false;
                }

            }
        }
    }

    private void OnEnable()
    {
        GameManager.instance.savePressed.AddListener(savePressed);
    }

    private void savePressed()
    {
        SaveLoadManager.SaveBool(identification, hasBeenCollected);
        Debug.Log("Save Pressed");
    }

    private void OnDisable()
    {
        GameManager.instance.savePressed.RemoveListener(savePressed);
    }
}
