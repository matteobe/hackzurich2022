using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Backend.Models;

public class ArrowGenerator : MonoBehaviour
{
    public LineRenderer mainArrow;
    public LineRenderer middleArrow;

    void GenerateArrow(Node a, Node b) {
        mainArrow.positionCount = 2;
        mainArrow.SetPosition(0, new Vector3((float)a.X, (float)a.Y, (float)a.Z));
        mainArrow.SetPosition(1, new Vector3((float)b.X, (float)b.Y, (float)b.Z));

        Vector3 direction = mainArrow.GetPosition(1) - mainArrow.GetPosition(0);

        Vector3 side = Vector3.Cross(direction.normalized, Vector3.up);

        middleArrow.positionCount = 3;

        middleArrow.SetPosition(0, mainArrow.GetPosition(0) + direction * 0.5f - direction.normalized - side);
        middleArrow.SetPosition(1, mainArrow.GetPosition(0) + direction * 0.5f);
        middleArrow.SetPosition(2, mainArrow.GetPosition(0) + direction * 0.5f - direction.normalized + side);
    }
}
