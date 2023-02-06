using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    public string nextScene;
    public Canvas canvas;
    public Image image;
    public float time;
    public bool fadeIn;
    public float delay;

    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = time;
        image.gameObject.SetActive(true);
        if (!fadeIn) {
            canvas.worldCamera = Camera.main;
            canvas.planeDistance = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (delay > 0) {
            delay -= Time.deltaTime;
            return;
        }
        var alpha = Mathf.Lerp(fadeIn ? 0f : 1f, fadeIn ? 1f : 0f, timer / time);
        image.color = new Color(0, 0, 0, alpha);
        timer -= Time.deltaTime;
        if (timer < 0) {
            if (!fadeIn && nextScene != null) {
                SceneManager.LoadScene(nextScene);
            }
            if (fadeIn) {
                DestroyImmediate(gameObject);
            }
        }
    }
}
