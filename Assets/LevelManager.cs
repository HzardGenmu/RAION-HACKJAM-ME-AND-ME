using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject tutorial;
    public string levelName;

    // Start is called before the first frame update
    void Start()
    {
        tutorial.SetActive(true);
    }

    public void ClosePanel()
    {
        if (tutorial.activeSelf)
        {
            tutorial.SetActive(false);
            SceneManager.LoadScene(levelName);
        }
    }
}
