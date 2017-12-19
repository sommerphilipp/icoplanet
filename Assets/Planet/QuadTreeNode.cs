using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadTreeNode {

  enum NodeType {
    LEAF,
    ROOT
  };

  QuadTreeNode parent;

  QuadTreeNode[] children = new QuadTreeNode[4];

  QuadTreeNode[] neighbors = new QuadTreeNode[3];

  public Vector3[] Vertices = new Vector3[3];

  public QuadTreeNode(Vector3[] vertices) {
    vertices = vertices;
    parent = null;
  }
  
}
