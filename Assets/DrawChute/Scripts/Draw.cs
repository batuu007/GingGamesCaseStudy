using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    public void Initialize(Vector3 pos, Vector3 direction, float size)
    {
        transform.localPosition = pos;
        Vector3 localScale = transform.localScale;
        localScale.z = size;
        transform.localScale = localScale;

        transform.forward = direction;
    }
}
