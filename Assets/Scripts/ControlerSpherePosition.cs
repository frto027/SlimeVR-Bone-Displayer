using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlerSpherePosition : MonoBehaviour
{
    public enum Position{
        None, Front,Back,Up,Down,Left,Right
    }
    public static Position pos = Position.None;
    public static int distanceMultiply = 1;

    public Transform mirror;

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Vector3.zero;
        switch (pos)
        {
            case Position.None:dir = Vector3.zero;break;
            case Position.Front:dir = Vector3.forward;break;
            case Position.Back:dir = Vector3.back;break;
            case Position.Up:dir = Vector3.up;break;
            case Position.Down:dir = Vector3.down;break;
            case Position.Left:dir = Vector3.left;break;
            case Position.Right:dir = Vector3.right;break;
        }
        dir = 0.01f * dir;
        dir = distanceMultiply * dir;
        transform.localPosition = dir;
        if (mirror)
        {
            mirror.localPosition = dir;
        }
    }
}
