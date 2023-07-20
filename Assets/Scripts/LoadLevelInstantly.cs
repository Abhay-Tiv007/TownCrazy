using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelInstantly : MonoBehaviour
{
    [SerializeField] private string level;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(level);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
