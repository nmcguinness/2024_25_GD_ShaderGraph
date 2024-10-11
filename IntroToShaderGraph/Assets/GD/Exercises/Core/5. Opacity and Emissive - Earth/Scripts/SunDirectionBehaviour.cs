using System;
using UnityEngine;

public class SunDirectionBehaviour : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The mesh renderer of the surface of the Earth")]
    private MeshRenderer surfaceMeshRenderer;

    [SerializeField]
    [Tooltip("The directional light that represents the sun")]
    private Light sunLight;

    private int sunDirectionID;
    private Material surfaceMaterial;

    private void Start()
    {
        //get hash of the reference in the shader
        sunDirectionID = Shader.PropertyToID("_Sun_Direction");

        //get surface material
        surfaceMaterial = surfaceMeshRenderer.GetComponent<Material>();

        if (surfaceMaterial == null)
            throw new NullReferenceException("Surface material cannot be null!");
        if (sunLight == null)
            throw new NullReferenceException("Sun light cannot be null!");
    }

    private void Update()
    {
        //use hash rather than reference name to speed up setting each update
        surfaceMaterial?.SetVector(sunDirectionID, sunLight.transform.forward);
        // surfaceMaterial.SetVector("_Sun_Direction", sunLight.transform.forward);
    }
}