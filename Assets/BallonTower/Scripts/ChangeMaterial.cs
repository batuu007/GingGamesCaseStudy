using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    [SerializeField] private Material[] _materials;

    private void Start()
    {
        MaterialsChange();
    }
    private void MaterialsChange()
    {
        GameObject materials = transform.gameObject;
        materials.GetComponent<MeshRenderer>().material = _materials[Random.Range(0, 5)];
    }
}
