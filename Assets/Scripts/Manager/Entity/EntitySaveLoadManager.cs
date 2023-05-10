using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySaveLoadManager : MonoBehaviour
{

    public List<Entity> entities = new List<Entity>();

    #region saving
    private void OnEnable()
    {
        GameManager.instance.savePressed.AddListener(savePressed);
        Debug.Log("entity state manager OnEnable listener");
    }

    private void savePressed()
    {
        Debug.Log(entities.Count);
        foreach (Entity e in entities)
        {
            e.SaveData();
        }
    }

    private void OnDisable()
    {
        GameManager.instance.savePressed.RemoveListener(savePressed);
    }

    #endregion
}
