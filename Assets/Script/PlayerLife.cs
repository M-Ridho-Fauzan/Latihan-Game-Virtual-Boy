using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private AudioSource deathSFX;

    private Vector3 startPosition; // Tambah variabel untuk menyimpan posisi awal

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        startPosition = transform.position; // Simpan posisi awal saat permainan dimulai
    }

    // Func menjalankan animasi Death saat terkena trap
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Trap")){
            Die();
        }
    }

    // Func agar saat sudah mati, tidak bisa di gerakan lagi
    private void Die()
    {
        deathSFX.Play();
        rb.bodyType = RigidbodyType2D.Static;// merubah Rigidbody bag sub "type" menjadi Static
        anim.SetTrigger("death");

        // Atur posisi pemain kembali ke posisi awal
        transform.position = startPosition;
    }

    // Func, saat player mati, scene di ulang dari awal
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
