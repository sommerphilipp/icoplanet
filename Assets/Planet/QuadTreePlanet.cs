using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuadTreePlanet : MonoBehaviour
{

  static float t = (1.0f + Mathf.Sqrt (5.0f)) / 2.0f;

  static Vector3[] baseVertices = new Vector3[12] {
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

  static QuadTreeNode[] rootNodes = new QuadTreeNode[] {
    new QuadTreeNode (new Vector3[] { baseVertices [0], baseVertices [11], baseVertices [5] }),
//    new List<Vector3> { baseVertices.ElementAt (0), baseVertices.ElementAt (5), baseVertices.ElementAt (1) },
//    new List<Vector3> { baseVertices.ElementAt (0), baseVertices.ElementAt (1), baseVertices.ElementAt (7) },
//    new List<Vector3> { baseVertices.ElementAt (0), baseVertices.ElementAt (7), baseVertices.ElementAt (10) },
//    new List<Vector3> { baseVertices.ElementAt (0), baseVertices.ElementAt (10), baseVertices.ElementAt (11) },
//
//    new List<Vector3> { baseVertices.ElementAt (1), baseVertices.ElementAt (5), baseVertices.ElementAt (9) },
//    new List<Vector3> { baseVertices.ElementAt (5), baseVertices.ElementAt (11), baseVertices.ElementAt (4) },
//    new List<Vector3> { baseVertices.ElementAt (11), baseVertices.ElementAt (10), baseVertices.ElementAt (2) },
//    new List<Vector3> { baseVertices.ElementAt (10), baseVertices.ElementAt (7), baseVertices.ElementAt (6) },
//    new List<Vector3> { baseVertices.ElementAt (7), baseVertices.ElementAt (1), baseVertices.ElementAt (8) },
//
//    new List<Vector3> { baseVertices.ElementAt (3), baseVertices.ElementAt (9), baseVertices.ElementAt (4) },
//    new List<Vector3> { baseVertices.ElementAt (3), baseVertices.ElementAt (4), baseVertices.ElementAt (2) },
//    new List<Vector3> { baseVertices.ElementAt (3), baseVertices.ElementAt (2), baseVertices.ElementAt (6) },
//    new List<Vector3> { baseVertices.ElementAt (3), baseVertices.ElementAt (6), baseVertices.ElementAt (8) },
//    new List<Vector3> { baseVertices.ElementAt (3), baseVertices.ElementAt (8), baseVertices.ElementAt (9) },
//
//    new List<Vector3> { baseVertices.ElementAt (4), baseVertices.ElementAt (9), baseVertices.ElementAt (5) },
//    new List<Vector3> { baseVertices.ElementAt (2), baseVertices.ElementAt (4), baseVertices.ElementAt (11) },
//    new List<Vector3> { baseVertices.ElementAt (6), baseVertices.ElementAt (2), baseVertices.ElementAt (10) },
//    new List<Vector3> { baseVertices.ElementAt (8), baseVertices.ElementAt (6), baseVertices.ElementAt (7) },
//    new List<Vector3> { baseVertices.ElementAt (9), baseVertices.ElementAt (8), baseVertices.ElementAt (1) }
  };

  // Use this for initialization
  void Start ()
  {
    rootNodes
      .ToList ()
      .ConvertAll (quadTreeNode => quadTreeNode.Vertices);
  }
	
  // Update is called once per frame
  void Update ()
  {
		
  }
}
