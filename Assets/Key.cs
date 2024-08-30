using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public string keyID;
    public static int keyCount;
    public GameObject panelClue; // Changed to GameObject to directly reference the panel
    [SerializeField] private int maxKey = 3;

    void Start()
    {
        if (KeyManager.collectedKeys.Contains(keyID))
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerSS"))
        {
            KeyManager.AddKey(keyID);
            keyCount++;

            // Activate the panel directly
            if (panelClue != null)
            {
                panelClue.SetActive(true); // Show the clue panel
            }

            Destroy(gameObject);
        }
    }
}
