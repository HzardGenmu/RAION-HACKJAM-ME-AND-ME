using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VirtualTrigger : MonoBehaviour
{
    public string targetSceneName = "";  // Name of the scene to transition to
    public GameObject[] objectsToSave; // The objects whose positions will be saved
    public SavePosition[] savePositions;

    private bool playerInRange;

    private void Start()
    {
        foreach (SavePosition savePosition in savePositions)
        {
            savePosition.LoadObjectPosition();
        }
    }
    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            TransitionToNewScene();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    public void TransitionToNewScene()
    {
        if (objectsToSave != null)
        {
            foreach (SavePosition savePosition in savePositions)
            {
                savePosition.SaveObjectPosition();
            }
        }
        // Load new scene
        SceneManager.LoadScene(targetSceneName);
    }
}
