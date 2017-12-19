using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent (typeof(Rigidbody))]
public class Planet2 : MonoBehaviour
{

  static float t = (1.0f + Mathf.Sqrt (5.0f)) / 2.0f;

  static List<Vector3> baseVertices = new List<Vector3> {
    new Vector3 (-1, t, 0),
    new Vector3 (1, t, 0),
    new Vector3 (-1, -t, 0),
    new Vector3 (1, -t, 0),

    new Vector3 (0, -1, t),
    new Vector3 (0, 1, t),
    new Vector3 (0, -1, -t),
    new Vector3 (0, 1, -t),

    new Vector3 (t, 0, -1),
    new Vector3 (t, 0, 1),
    new Vector3 (-t, 0, -1),
    new Vector3 (-t, 0, 1)
  };

  static List<List<Vector3>> rootNodesVertices = new List<List<Vector3>> () {
    new List<Vector3> { baseVertices.ElementAt (0), baseVertices.ElementAt (11), baseVertices.ElementAt (5) },
    new List<Vector3> { baseVertices.ElementAt (0), baseVertices.ElementAt (5), baseVertices.ElementAt (1) },
    new List<Vector3> { baseVertices.ElementAt (0), baseVertices.ElementAt (1), baseVertices.ElementAt (7) },
    new List<Vector3> { baseVertices.ElementAt (0), baseVertices.ElementAt (7), baseVertices.ElementAt (10) },
    new List<Vector3> { baseVertices.ElementAt (0), baseVertices.ElementAt (10), baseVertices.ElementAt (11) },
    
    new List<Vector3> { baseVertices.ElementAt (1), baseVertices.ElementAt (5), baseVertices.ElementAt (9) },
    new List<Vector3> { baseVertices.ElementAt (5), baseVertices.ElementAt (11), baseVertices.ElementAt (4) },
    new List<Vector3> { baseVertices.ElementAt (11), baseVertices.ElementAt (10), baseVertices.ElementAt (2) },
    new List<Vector3> { baseVertices.ElementAt (10), baseVertices.ElementAt (7), baseVertices.ElementAt (6) },
    new List<Vector3> { baseVertices.ElementAt (7), baseVertices.ElementAt (1), baseVertices.ElementAt (8) },
    
    new List<Vector3> { baseVertices.ElementAt (3), baseVertices.ElementAt (9), baseVertices.ElementAt (4) },
    new List<Vector3> { baseVertices.ElementAt (3), baseVertices.ElementAt (4), baseVertices.ElementAt (2) },
    new List<Vector3> { baseVertices.ElementAt (3), baseVertices.ElementAt (2), baseVertices.ElementAt (6) },
    new List<Vector3> { baseVertices.ElementAt (3), baseVertices.ElementAt (6), baseVertices.ElementAt (8) },
    new List<Vector3> { baseVertices.ElementAt (3), baseVertices.ElementAt (8), baseVertices.ElementAt (9) },
    
    new List<Vector3> { baseVertices.ElementAt (4), baseVertices.ElementAt (9), baseVertices.ElementAt (5) },
    new List<Vector3> { baseVertices.ElementAt (2), baseVertices.ElementAt (4), baseVertices.ElementAt (11) },
    new List<Vector3> { baseVertices.ElementAt (6), baseVertices.ElementAt (2), baseVertices.ElementAt (10) },
    new List<Vector3> { baseVertices.ElementAt (8), baseVertices.ElementAt (6), baseVertices.ElementAt (7) },
    new List<Vector3> { baseVertices.ElementAt (9), baseVertices.ElementAt (8), baseVertices.ElementAt (1) }
  };

  // Use this for initialization
  void Start ()
  {
    rootNodesVertices.ForEach (vertices =>
    {
      StartCoroutine (createRootNodes (vertices));
    });
  }

  IEnumerator createRootNodes (List<Vector3> vertices)
  {

    if (vertices.Count () == 0)
    {
      yield break;
    }

    GameObject go = new GameObject ("Root node", typeof(Node), typeof(MeshRenderer), typeof(MeshFilter), typeof(MeshCollider));
    go.GetComponent<Node> ().Vertices = vertices;
    go.GetComponent<MeshFilter>().mesh.vertices = vertices
      .ConvertAll (vertex => vertex.normalized * 100)
      .ToArray ();
    go.GetComponent<MeshFilter>().mesh.triangles = new int[] { 0, 1, 2 };
    go.GetComponent<MeshFilter>().mesh.RecalculateNormals ();
    go.GetComponent<MeshCollider>().sharedMesh = go.GetComponent<MeshFilter> ().mesh;

  }

}
