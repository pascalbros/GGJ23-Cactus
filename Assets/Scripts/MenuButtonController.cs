using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonController : MonoBehaviour {
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (Forces.current.FreeRoots() > 0) { return; }
        Collider2D[] colliders = new Collider2D[5];
        rb.GetContacts(colliders);
        HashSet<string> parents = new();
        foreach (var collider in colliders) {
            if (collider) {
                parents.Add(collider.transform.parent.name);
            }
        }
        if (parents.Contains("Root1") && parents.Contains("Root2")) {
            SceneManager.LoadScene("Main");
        }
    }
}
