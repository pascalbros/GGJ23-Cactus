using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipController : MonoBehaviour
{
    public bool canAttach = true;
    public int player;
    public bool isTouchingField = false;
    Rigidbody2D rb;

    LayerMask fieldLayer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fieldLayer = LayerMask.NameToLayer("Field");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!canAttach) {
            rb.bodyType = RigidbodyType2D.Dynamic;
        } else if (isTouchingField) {
            rb.bodyType = RigidbodyType2D.Static;
        }
        Forces.current.SetIsRootFree(isTouchingField || rb.bodyType == RigidbodyType2D.Static, player);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == fieldLayer) {
            isTouchingField = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.layer == fieldLayer) {
            isTouchingField = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.layer == fieldLayer) {
            isTouchingField = false;
        }
    }
}
