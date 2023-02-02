using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesController: MonoBehaviour
{
    public Transform respawnPosition;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name == "Cactus") {
            collision.transform.position = respawnPosition.position;
            var tips = collision.GetComponentsInChildren<TipController>();
            Debug.Log(tips.Length);
            foreach (var tip in tips) {
                tip.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }
}
