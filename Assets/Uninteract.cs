using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uninteract : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool canMove = false;
    private Vector2 storedVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; // Buat Rigidbody2D kinematik agar tidak bereaksi terhadap fisika default
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    private void move()
    {
        if (canMove)
        {
            rb.isKinematic = false; // Hanya biarkan objek bergerak jika bisa bergerak
        }
        else
        {
            rb.isKinematic = true; // Set kinematik jika tidak bisa bergerak untuk menghentikan semua gaya
            rb.velocity = Vector2.zero; // Pastikan kecepatan menjadi nol
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }
    }
}
