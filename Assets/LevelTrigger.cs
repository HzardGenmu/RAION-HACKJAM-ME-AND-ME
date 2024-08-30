using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTrigger : MonoBehaviour
{
    private bool isInTrigger;
    public string levelName = "";
    private void Update()
    {
        if(isInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("levelName");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInTrigger = true;
        }
    }
}
