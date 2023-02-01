using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotController : MonoBehaviour
{
    public float initialForce = 150f;

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log(collision.collider.name);
        if (collision.collider.name == "Cactus") {
            var inputs = collision.collider.GetComponentsInChildren<InputController>();
            foreach (var input in inputs) {
                input.maxForce = initialForce * 1.8f;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.collider.name == "Cactus") {
            var inputs = collision.collider.GetComponentsInChildren<InputController>();
            foreach (var input in inputs) {
                input.maxForce = initialForce * 1.0f;
            }
        }
    }
}
