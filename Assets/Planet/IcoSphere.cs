using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

public class IcoSphere
{
  static float t = (1.0f + Mathf.Sqrt (5.0f)) / 2.0f;

  public struct Face
  {
    public Vector3 V1;
    public Vector3 V2;
    public Vector3 V3;

    public Face (Vector3 v1, Vector3 v2, Vector3 v3)
    {
      V1 = v1;
      V2 = v2;
      V3 = v3;
    }
  }

  private int subdivisions;

  public List<Vector3> Vertices { get; set; }

  public List<Face> Faces { get; set; }

  public List<int> Triangles { get; set; }

  Dictionary<long, int> middlePointIndexCache;

  public IcoSphere (int subdivisions)
  {
    this.subdivisions = subdivisions;
    this.Faces = new List<Face>();
    this.Vertices = new List<Vector3>();
    this.Triangles = new List<int>();
    middlePointIndexCache = new Dictionary<long, int> ();
    Generate ();
  }


  private void Generate ()
  {
    generateVertices ();
    generateFaces ();

    for (int i = 0; i < this.subdivisions; i++)
    {
      //Vertices werden modifizert.. unschöne sideeffects
      Faces = Faces.ConvertAll (face =>
      {
        // check if near player
        int middlepoint1 = calculateMiddlepoint (face.V1, face.V2);
        int middlepoint2 = calculateMiddlepoint (face.V2, face.V3);
        int middlepoint3 = calculateMiddlepoint (face.V3, face.V1);

        return new List<Face> () {
          new Face (face.V1, Vertices.ElementAt (middlepoint1), Vertices.ElementAt (middlepoint3)),
          new Face (face.V2, Vertices.ElementAt (middlepoint2), Vertices.ElementAt (middlepoint1)),
          new Face (face.V3, Vertices.ElementAt (middlepoint3), Vertices.ElementAt (middlepoint2)),
          new Face (Vertices.ElementAt (middlepoint1), Vertices.ElementAt (middlepoint2), Vertices.ElementAt (middlepoint3))
        };
      })
        .SelectMany (nestedFaceList => nestedFaceList)
        .ToList ();
    }

    this.Triangles = this.Faces
      .ConvertAll (face =>
    {
      return new List<int> () { 
        this.Vertices.IndexOf (face.V1), 
        this.Vertices.IndexOf (face.V2), 
        this.Vertices.IndexOf (face.V3)
      };
    })
      .SelectMany (l => l)
      .ToList ();
  }
    
  private int calculateMiddlepoint (Vector3 point1, Vector3 point2)
  {
    int indexPoint1 = this.Vertices.IndexOf (point1);
    int indexPoint2 = this.Vertices.IndexOf (point2);

    // check if we have it already
    long smallerIndex = Convert.ToInt64 (Mathf.Min (indexPoint1, indexPoint2));
    long greaterIndex = Convert.ToInt64 (Mathf.Max (indexPoint1, indexPoint2));
    long key = (smallerIndex << 32) + greaterIndex;
    int indexOfCachedMiddlepoint;
    if (this.middlePointIndexCache.TryGetValue (key, out indexOfCachedMiddlepoint))
    {
      return indexOfCachedMiddlepoint;
    }

    // not in cache, calculate it
    Vector3 middlepoint = Vector3.Lerp (point1, point2, 0.5f);

    // add vertex makes sure point is on unit sphere
    int indexOfMiddlepoint = addVertex (middlepoint);

    // store it, return index
    this.middlePointIndexCache.Add (key, indexOfMiddlepoint);
    return indexOfMiddlepoint;
  }

  private int addVertex (Vector3 vertex)
  {
    this.Vertices.Add (vertex);
    return this.Vertices.IndexOf(vertex);
  }

  private void generateVertices ()
  {
    addVertex (new Vector3 (-1, t, 0));
    addVertex (new Vector3 (1, t, 0));
    addVertex (new Vector3 (-1, -t, 0));
    addVertex (new Vector3 (1, -t, 0));

    addVertex (new Vector3 (0, -1, t));
    addVertex (new Vector3 (0, 1, t));
    addVertex (new Vector3 (0, -1, -t));
    addVertex (new Vector3 (0, 1, -t));

    addVertex (new Vector3 (t, 0, -1));
    addVertex (new Vector3 (t, 0, 1));
    addVertex (new Vector3 (-t, 0, -1));
    addVertex (new Vector3 (-t, 0, 1));
  }

  private void generateFaces ()
  {
    
    this.Faces.Add (new Face (this.Vertices.ElementAt (0), this.Vertices.ElementAt (11), this.Vertices.ElementAt (5)));
    this.Faces.Add (new Face (this.Vertices.ElementAt (0), this.Vertices.ElementAt (5), this.Vertices.ElementAt (1)));
    this.Faces.Add (new Face (this.Vertices.ElementAt (0), this.Vertices.ElementAt (1), this.Vertices.ElementAt (7)));
    this.Faces.Add (new Face (this.Vertices.ElementAt (0), this.Vertices.ElementAt (7), this.Vertices.ElementAt (10)));
    this.Faces.Add (new Face (this.Vertices.ElementAt (0), this.Vertices.ElementAt (10), this.Vertices.ElementAt (11)));

    this.Faces.Add (new Face (this.Vertices.ElementAt (1), this.Vertices.ElementAt (5), this.Vertices.ElementAt (9)));
    this.Faces.Add (new Face (this.Vertices.ElementAt (5), this.Vertices.ElementAt (11), this.Vertices.ElementAt (4)));
    this.Faces.Add (new Face (this.Vertices.ElementAt (11), this.Vertices.ElementAt (10), this.Vertices.ElementAt (2)));
    this.Faces.Add (new Face (this.Vertices.ElementAt (10), this.Vertices.ElementAt (7), this.Vertices.ElementAt (6)));
    this.Faces.Add (new Face (this.Vertices.ElementAt (7), this.Vertices.ElementAt (1), this.Vertices.ElementAt (8)));

    this.Faces.Add (new Face (this.Vertices.ElementAt (3), this.Vertices.ElementAt (9), this.Vertices.ElementAt (4)));
    this.Faces.Add (new Face (this.Vertices.ElementAt (3), this.Vertices.ElementAt (4), this.Vertices.ElementAt (2)));
    this.Faces.Add (new Face (this.Vertices.ElementAt (3), this.Vertices.ElementAt (2), this.Vertices.ElementAt (6)));
    this.Faces.Add (new Face (this.Vertices.ElementAt (3), this.Vertices.ElementAt (6), this.Vertices.ElementAt (8)));
    this.Faces.Add (new Face (this.Vertices.ElementAt (3), this.Vertices.ElementAt (8), this.Vertices.ElementAt (9)));

    this.Faces.Add (new Face (this.Vertices.ElementAt (4), this.Vertices.ElementAt (9), this.Vertices.ElementAt (5)));
    this.Faces.Add (new Face (this.Vertices.ElementAt (2), this.Vertices.ElementAt (4), this.Vertices.ElementAt (11)));
    this.Faces.Add (new Face (this.Vertices.ElementAt (6), this.Vertices.ElementAt (2), this.Vertices.ElementAt (10)));
    this.Faces.Add (new Face (this.Vertices.ElementAt (8), this.Vertices.ElementAt (6), this.Vertices.ElementAt (7)));
    this.Faces.Add (new Face (this.Vertices.ElementAt (9), this.Vertices.ElementAt (8), this.Vertices.ElementAt (1)));
  }
}
