using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawEvents : MonoBehaviour
{
    public static Action<Vector3[]> onDrawingEnded;
}
