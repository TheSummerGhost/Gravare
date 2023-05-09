using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator anim;

    private bool isUnlocked;
   
    public void OpenDoor()
    {
        isUnlocked = true;
        anim.SetBool("isUnlocked", isUnlocked);
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterDoor()
    {


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController playerControllerCheck))
        {
            if (isUnlocked)
            {
                FindObjectOfType<LevelManager>().AdvanceLevel();
            }
        }
    }
}
