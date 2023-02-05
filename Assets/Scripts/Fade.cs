using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Color color;
    public Canvas canvas;
    public Image image;
    public float time;
    public bool fadeIn;
    public float delay;

    private float timer;
    private Color finalColor;
    // Start is called before the first frame update
    void Start()
    {
        image.color = fadeIn ? Color.clear : color;
        finalColor = fadeIn ? color : Color.clear;
        timer = time;
        image.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (delay > 0) {
            delay -= Time.deltaTime;
            return;
        }
        image.color = Color.Lerp(finalColor, color, timer / time);
        timer -= Time.deltaTime;
        if (timer < 0) {
            DestroyImmediate(gameObject);
        }
    }
}
