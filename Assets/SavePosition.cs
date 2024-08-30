using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePosition : MonoBehaviour
{
    public string objectID;

    public void SaveObjectPosition()
    {
        PlayerPrefs.SetFloat(objectID + "_PosX", transform.position.x);
        PlayerPrefs.SetFloat(objectID + "_PosY", transform.position.y);
        PlayerPrefs.SetFloat(objectID + "_PosZ", transform.position.z);
        PlayerPrefs.Save();
    }

    public void LoadObjectPosition()
    {
        if (PlayerPrefs.HasKey(objectID + "_PosX"))
        {
            float x = PlayerPrefs.GetFloat(objectID + "_PosX");
            float y = PlayerPrefs.GetFloat(objectID + "_PosY");
            float z = PlayerPrefs.GetFloat(objectID + "_PosZ");
            transform.position = new Vector3(x, y, z);
        }
    }
}
