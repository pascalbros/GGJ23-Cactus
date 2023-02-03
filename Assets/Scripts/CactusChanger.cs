using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusChanger: MonoBehaviour
{
    public Rigidbody2D cactusRigidbody;
    public float cactusRbDrag;
    private float cactusRbDragInitialValue;

    public bool disableTipsEnabled = false;

    void Start() {
        cactusRbDragInitialValue = cactusRigidbody.drag;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name != "Cactus") { return; }
        cactusRigidbody.drag = cactusRbDrag;

        if (disableTipsEnabled) {
            var tips = cactusRigidbody.GetComponentsInChildren<InputController>();
            foreach (var tip in tips) {
                tip.enabled = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.name != "Cactus") { return; }
        cactusRigidbody.drag = cactusRbDragInitialValue;

        if (disableTipsEnabled) {
            var tips = cactusRigidbody.GetComponentsInChildren<InputController>();
            foreach (var tip in tips) {
                tip.enabled = true;
            }
        }
    }
}
