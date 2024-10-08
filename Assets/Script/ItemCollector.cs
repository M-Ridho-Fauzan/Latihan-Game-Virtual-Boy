using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int cherries = 0;

    [SerializeField] private Text cherriesText;
    [SerializeField] private AudioSource collectSFX;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Cherry")) {
            collectSFX.Play();
            Destroy(collision.gameObject);
            cherries++;
            // Debug.Log("Cherries: " + cherries);
            cherriesText.text = "Cherries: " + cherries;
        }
    }
}
