using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public static List<string> collectedKeys = new List<string>();

    public static void AddKey(string keyID)
    {
        if (!collectedKeys.Contains(keyID))
        {
            collectedKeys.Add(keyID);
        }
    }

    public static bool HasKey(string keyID)
    {
        return collectedKeys.Contains(keyID);
    }
}
