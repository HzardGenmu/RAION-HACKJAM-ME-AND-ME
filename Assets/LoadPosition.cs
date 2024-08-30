using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPosition : MonoBehaviour
{
    public string objectID; // ID unik untuk setiap objek

    void Start()
    {
        // Check if the position was saved
        if (PlayerPrefs.HasKey(objectID + "_PosX"))
        {
            // Load the position
            float x = PlayerPrefs.GetFloat(objectID + "_PosX");
            float y = PlayerPrefs.GetFloat(objectID + "_PosY");
            float z = PlayerPrefs.GetFloat(objectID + "_PosZ");

            // Apply the position to this object
            transform.position = new Vector3(x, y, z);
        }
    }
}
