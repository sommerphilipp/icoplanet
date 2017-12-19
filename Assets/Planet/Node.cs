using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Node : MonoBehaviour
{
  
  public Vector3 center;

  public GameObject Parent { get; set; }

  List<GameObject> children;

  public List<Vector3> Vertices { get; set; }

  public int Depth { get; set; }

  GameObject player;

  void Start ()
  {
    gameObject.GetComponent<MeshRenderer> ().sharedMaterial = new Material (Shader.Find ("Standard"));
    player = GameObject.Find ("Capsule");
  }

  void Update ()
  {
    
      if (shouldSubdivide () && IsLeaf ())
      {
        subdivide ();
      }
//    else
//    {
//      collapse (); 
//    }
  }

  public void collapse ()
  {
    if (Parent != null)
    {
      transform.DetachChildren ();
      Parent.GetComponent<MeshRenderer> ().enabled = true;
      Destroy (gameObject);
    }
  }

  private void subdivide ()
  {
    Vector3 p1 = Vertices.ElementAt (0);
    Vector3 p2 = Vertices.ElementAt (1);
    Vector3 p3 = Vertices.ElementAt (2);

    Vector3 middlepoint1 = calculateMiddlepoint (p1, p2);
    Vector3 middlepoint2 = calculateMiddlepoint (p2, p3);
    Vector3 middlepoint3 = calculateMiddlepoint (p3, p1);

    List<List<Vector3>> subNodes = new List<List<Vector3>> () {
      new List<Vector3> () { p1, middlepoint1, middlepoint3 },
      new List<Vector3> () { p2, middlepoint2, middlepoint1 },
      new List<Vector3> () { p3, middlepoint3, middlepoint2 },
      new List<Vector3> () { middlepoint1, middlepoint2, middlepoint3 }
    };

    children = subNodes.ConvertAll (vertices =>
    {
      String gameObjectName = generateNodeName (vertices);
      GameObject go = new GameObject (gameObjectName, typeof(Node), typeof(MeshRenderer), typeof(MeshFilter), typeof(MeshCollider));
      go.transform.parent = gameObject.transform;
      go.GetComponent<Node> ().Depth = gameObject.GetComponent<Node> ().Depth + 1;
      go.GetComponent<Node> ().Vertices = vertices;
      go.GetComponent<Node> ().Parent = gameObject;

      go.GetComponent<MeshFilter> ().mesh.vertices = Vertices
        .ConvertAll (vertex => vertex.normalized * 100)
        .ToArray ();
      go.GetComponent<MeshFilter> ().mesh.triangles = new int[] { 0, 1, 2 };
      go.GetComponent<MeshFilter> ().mesh.RecalculateNormals ();
      go.GetComponent<MeshCollider>().sharedMesh = go.GetComponent<MeshFilter> ().mesh;
      gameObject.GetComponent<MeshRenderer>().enabled = false;
      return go;
    });

  }

  private bool shouldSubdivide ()
  {
    var dist = Vector3.Distance (player.transform.position, gameObject.GetComponent<MeshFilter>().mesh.bounds.ClosestPoint (player.transform.position));
    if (dist < 30)
    {
      //    Vector3 p1 = Vertices.ElementAt (0);
      //    Vector3 p2 = Vertices.ElementAt (1);
      //    Vector3 p3 = Vertices.ElementAt (2);

      //      bool edgeLengthTooLong = new List<float> () {
      //        Vector3.Distance (p1, p2),
      //        Vector3.Distance (p2, p3),
      //        Vector3.Distance (p3, p1)
      //      }
      //        .All (x =>
      //      {
      //        return x > 0.1f;
      //      });
      return Depth < 3;
    }

    return false;
  }

  private Vector3 calculateMiddlepoint (Vector3 point1, Vector3 point2)
  {
//    int indexPoint1 = this.Vertices.IndexOf (point1);
//    int indexPoint2 = this.Vertices.IndexOf (point2);
//
//    // check if we have it already
//    long smallerIndex = Convert.ToInt64 (Mathf.Min (indexPoint1, indexPoint2));
//    long greaterIndex = Convert.ToInt64 (Mathf.Max (indexPoint1, indexPoint2));
//    long key = (smallerIndex << 32) + greaterIndex;
//    int indexOfCachedMiddlepoint;
//    if (this.middlePointIndexCache.TryGetValue (key, out indexOfCachedMiddlepoint))
//    {
//      return indexOfCachedMiddlepoint;
//    }

    // not in cache, calculate it
    Vector3 middlepoint = Vector3.Lerp (point1, point2, 0.5f);

//    // add vertex makes sure point is on unit sphere
//    int indexOfMiddlepoint = addVertex (middlepoint);
//
//    // store it, return index
//    this.middlePointIndexCache.Add (key, indexOfMiddlepoint);
//    return indexOfMiddlepoint;
    return middlepoint;
  }

  private string generateNodeName (List<Vector3> verts)
  {
    return verts.ConvertAll ((Vector3 vertex) => vertex.sqrMagnitude.ToString ())
      .Aggregate ((string l, string r) => l + r);
  }

  bool IsLeaf ()
  {
    return children == null || children.Count () == 0;
  }
}

