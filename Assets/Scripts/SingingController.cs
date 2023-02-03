using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SingingController : MonoBehaviour
{
    public float multiplier = 2;
    public float amplitude;
    public SpriteRenderer smile;
    public Vector3 minScale;
    public Vector3 maxScale;

    public AudioSource audioSource;

    private List<float> vocalsData;

    SpriteRenderer spriteRenderer;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        vocalsData = getData();
        Debug.Log(vocalsData.Count);
    }

    void Update()
    {
        if (audioSource.isPlaying) {
            var currentIndex = (int)(audioSource.time * 3) % vocalsData.Count; // 3 samples per second
            var value = vocalsData[currentIndex];
            amplitude = value;
        } else {
            amplitude = 0;
        }
        if (amplitude < 0.01) {
            spriteRenderer.enabled = false;
            smile.enabled = true;
        } else {
            smile.enabled = false;
            spriteRenderer.enabled = true;
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.Lerp(minScale, maxScale, multiplier * amplitude - 0.01f + UnityEngine.Random.Range(0f, 1f)), Time.deltaTime * 20);
        }
    }

    public static List<float> getData() {
        List<float> data = new(477);
        TextAsset csv = Resources.Load("theme-song-1-vocal") as TextAsset;
        StringReader reader = new(csv.text);
        while (reader.Peek() != -1) {
            string line = reader.ReadLine();
            var value = float.Parse(line, System.Globalization.CultureInfo.InvariantCulture);
            data.Add(value);
        }
        return data;
    }
}
