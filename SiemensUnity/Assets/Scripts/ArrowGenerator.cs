using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Backend.Models;

public class ArrowGenerator : MonoBehaviour
{
    public LineRenderer mainArrow;
    public LineRenderer middleArrow;
    public LineRenderer frontArrow;

    public Vector3 startPos, endPos;
    public float scale = 1f;

    public void GenerateArrow(Node a, Node b) {
        startPos = new Vector3((float)a.X, (float)a.Y, (float)a.Z);
        endPos = new Vector3((float)b.X, (float)b.Y, (float)b.Z);

        GenerateArrow(startPos, endPos);
    }

    public void GenerateArrow(Vector3 start, Vector3 end) {
        mainArrow.positionCount = 2;
        mainArrow.SetPosition(0, startPos);
        mainArrow.SetPosition(1, endPos);

        Vector3 direction = mainArrow.GetPosition(1) - mainArrow.GetPosition(0);

        Vector3 side = Vector3.Cross(direction.normalized, Vector3.up);

        middleArrow.positionCount = 3;

        middleArrow.SetPosition(0, mainArrow.GetPosition(0) + direction * 0.5f + scale*(- direction.normalized - side));
        middleArrow.SetPosition(1, mainArrow.GetPosition(0) + direction * 0.5f);
        middleArrow.SetPosition(2, mainArrow.GetPosition(0) + direction * 0.5f + scale * (- direction.normalized + side));

        frontArrow.positionCount = 3;
        frontArrow.SetPosition(0, mainArrow.GetPosition(1) +scale * (-direction.normalized - side));
        frontArrow.SetPosition(1, mainArrow.GetPosition(1) );
        frontArrow.SetPosition(2, mainArrow.GetPosition(1) + scale * (- direction.normalized + side));
    }

    private void Update() {
        GenerateArrow(startPos, endPos);
    }
}
