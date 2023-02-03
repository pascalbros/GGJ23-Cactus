using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMaker : MonoBehaviour {

    public int playerNumber = 1;
    public int numberOfPieces = 13;
    public Vector2 piecesOffset;
    public Material lineMaterial;
    public float maxForce = 0.11f;

    GameObject tip;
    GameObject draggerTip;

    void Start() {
        var parent = gameObject;
        var pieces = CreatePieces(parent.transform);
        DrawLine(pieces, parent.transform);
        SetupInput(parent.GetComponent<InputController>(), tip, draggerTip, maxForce);
    }

    GameObject CreateFirstPiece(Transform parent) {
        var piece = RootPiecePrototype();
        piece.name = "Bone 0";
        piece.transform.parent = parent;
        piece.transform.localPosition = Vector3.zero;

        var hinge = piece.GetComponent<HingeJoint2D>();
        hinge.connectedBody = parent.parent.GetComponent<Rigidbody2D>();
        return piece;
    }

    Transform[] CreatePieces(Transform parent) {
        var currentPiece = CreateFirstPiece(parent.transform);
        var pieces = new List<Transform> {
            currentPiece.transform
        };
        for (int i = 1; i < numberOfPieces; i++) {
            currentPiece = CreatePiece(parent, i, currentPiece.GetComponent<Rigidbody2D>());
            if (i == numberOfPieces - 2) {
                ConfigureTip(currentPiece);
            }
            pieces.Add(currentPiece.transform);
        }
        ConfigureLastPiece(currentPiece);
        pieces.RemoveAt(pieces.Count - 1);
        return pieces.ToArray();
    }

    GameObject CreatePiece(Transform parent, int index, Rigidbody2D connectedBody) {
        var piece = RootPiecePrototype();
        piece.name = "Bone " + index;
        piece.transform.parent = parent;
        piece.transform.localPosition = piecesOffset * index;

        var hinge = piece.GetComponent<HingeJoint2D>();
        hinge.connectedBody = connectedBody;
        return piece;
    }

    GameObject RootPiecePrototype() {
        var rootPiece = new GameObject {
            layer = LayerMask.NameToLayer("Root")
        };
        var rb = rootPiece.AddComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.mass = 0.5f;
        var hinge = rootPiece.AddComponent<HingeJoint2D>();
        hinge.autoConfigureConnectedAnchor = true;
        hinge.enableCollision = true;
        var collider = rootPiece.AddComponent<CircleCollider2D>();
        collider.radius = 0.1f;
        return rootPiece;
    }

    void ConfigureTip(GameObject currentPiece) {
        currentPiece.GetComponent<HingeJoint2D>().enableCollision = true;
        var tipController = currentPiece.AddComponent<TipController>();
        tipController.player = playerNumber;
        tip = currentPiece;
        var trigger = currentPiece.AddComponent<CircleCollider2D>();
        trigger.radius = 0.18f;
        trigger.isTrigger = true;
    }

    void ConfigureLastPiece(GameObject piece) {
        DestroyImmediate(piece.GetComponent<CircleCollider2D>());
        draggerTip = piece;
    }

    void DrawLine(Transform[] points, Transform parent) {
        var lineRenderer = parent.gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        var playerColors = new Color[] { Color.red, Color.blue, Color.yellow, Color.green };
        Gradient gradient = new();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(new Color(0.62f, 0.48f, 0.44f), 0.9f), new GradientColorKey(playerColors[playerNumber - 1], 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.9f), new GradientAlphaKey(1.0f, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;
        var rootRenderer = parent.gameObject.AddComponent<LegRenderer>();
        rootRenderer.points = points;
    }

    void SetupInput(InputController input, GameObject tip, GameObject draggerTip, float maxForce) {
        input.boneHinge = draggerTip.GetComponent<HingeJoint2D>();
        input.boneRigidbody = draggerTip.GetComponent<Rigidbody2D>();
        input.tip = tip.GetComponent<TipController>();
        input.maxForce = maxForce;
    }
}
