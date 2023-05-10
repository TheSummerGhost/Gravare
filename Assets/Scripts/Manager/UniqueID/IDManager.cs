using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public class IDManager : MonoBehaviour
{
    public List<Identifier> identifiers = new List<Identifier>();

    [ContextMenu("SetIDs")]
    public void FetchIDs()
    {
        identifiers = Resources.FindObjectsOfTypeAll<Identifier>().ToList();

        for (int i = 0; i < identifiers.Count; i++)
        {
            if (identifiers[i].identifier == "")
            {
                identifiers[i].identifier = System.Guid.NewGuid().ToString();
            }
        }
    }

    [ContextMenu("Reset all IDs")]
    public void ResetIDs()
    {
        identifiers = Resources.FindObjectsOfTypeAll<Identifier>().ToList();

        for (int i = 0; i < identifiers.Count; i++)
        {
            identifiers[i].identifier = "";
        }
    }
}
