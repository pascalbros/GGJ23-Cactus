using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BubbleChanger : MonoBehaviour
{
    public Bubble bubble;
    public string text;
    public float fontSize;
    public bool showOnce = false;

    private float gracePeriod;
    private bool onTriggerEnterCalled;
    private bool onTriggerExitCalled;

    private void Update() {
        if (onTriggerEnterCalled) {
            gracePeriod += Time.deltaTime;
            if (gracePeriod > 3 && onTriggerExitCalled) {
                RemoveMe();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name != "Cactus") { return; }
        gracePeriod = 0;
        onTriggerExitCalled = false;
        onTriggerEnterCalled = true;
        bubble.transform.position = collision.transform.position + Vector3.up * 2;
        bubble.gameObject.SetActive(true);
        var label = bubble.GetComponentInChildren<TextMeshPro>();
        label.text = text;
        label.fontSize = fontSize;
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.name != "Cactus") { return; }
        if (!bubble.gameObject.activeSelf) {
            bubble.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.name != "Cactus") { return; }
        onTriggerExitCalled = true;
        if (gracePeriod >= 3) {
            RemoveMe();
        }
    }

    private void RemoveMe() {
        bubble.gameObject.SetActive(false);
        if (showOnce) {
            Destroy(gameObject);
        }
    }
}
