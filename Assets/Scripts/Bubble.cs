using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Bubble : MonoBehaviour
{
    SpriteShapeController shapeController;
    public Vector2 rootPosition;
    public Transform target;
    public Vector3 targetPositionOffset;
    public Vector3 initialPositionOffset;

    // Start is called before the first frame update
    void Start()
    {
        shapeController = gameObject.GetComponent<SpriteShapeController>();
    }

    // Update is called once per frame
    void Update()
    {
        //var spline = shapeController.spline;
        //spline.SetPosition(5, transform.InverseTransformPoint(target.position + targetPositionOffset));
        Show();
    }

    void Show() {
        transform.position = Vector3.Slerp(transform.position, target.position + initialPositionOffset, Time.deltaTime * 4);
    }
}
