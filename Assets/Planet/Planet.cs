using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

[ExecuteInEditMode]
[RequireComponent (typeof(MeshRenderer), typeof(MeshFilter))]
public class Planet : MonoBehaviour
{
  public float Radius = 1f;
  
  [Range (0, 4)]
  public int Iterations = 0;

  private static int cachedIterations;

  IcoSphere icoSphere;

  [Range(0, 10)]
  float scale = 0.1f;

  void Awake() {
    icoSphere = new IcoSphere(Iterations);
    cachedIterations = Iterations;
  }

  void Update ()
  {
    if(cachedIterations != Iterations) {
      cachedIterations = Iterations;
      icoSphere = new IcoSphere(Iterations);
      Mesh mesh = new Mesh();
      MeshFilter meshFilter = gameObject.GetComponent<MeshFilter> ();
      meshFilter.sharedMesh = mesh;

      mesh.vertices = icoSphere
        .Vertices
        .ConvertAll(vertex => {
          return vertex.normalized * scale;
       })
        .ToArray();
      mesh.triangles = icoSphere.Triangles.ToArray ();
      mesh.RecalculateBounds();
      mesh.RecalculateNormals();
    }
  }

}
