using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchScene : MonoBehaviour
{
    public Button transitionButton; // Reference to the UI Button
    public string targetSceneName = "";  // Name of the scene to transition to
    public GameObject[] objectsToSave; // The objects whose positions will be saved
    public SavePosition[] savePositions;

    void Start()
    {
        // Assign the TransitionToNewScene method to the button's onClick event
        transitionButton.onClick.AddListener(TransitionToNewScene);
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
        // Load the new scene
        FindObjectOfType<SSMovement>().SavePlayerPosition();
        SceneManager.LoadScene(targetSceneName);
    }
}
