using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonController : MonoBehaviour {

    public GameObject fadeOut;
    Rigidbody2D rb;

    private bool canReact = true;
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!canReact) { return; }
        Collider2D[] colliders = new Collider2D[5];
        rb.GetContacts(colliders);
        HashSet<string> parents = new();
        foreach (var collider in colliders) {
            if (collider) {
                parents.Add(collider.transform.parent.name);
            }
        }
        if (parents.Contains("Root1") || parents.Contains("Root2")) {
            var fade = Instantiate(fadeOut);
            var fadeController = fade.GetComponent<Fade>();
            if (name == "MenuButton") {
                fadeController.nextScene = "Menu";
            } else if (name == "PlayButton") {
                fadeController.nextScene = "Main";
            } else if (name == "CreditsButton") {
                fadeController.nextScene = "Credits";
            }
            canReact = false;
        }
    }
}
