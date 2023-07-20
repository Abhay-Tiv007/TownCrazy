using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCursor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        lockCursor();
    }

    // Update is called once per frame
    public void lockCursor()
    {

        //lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
