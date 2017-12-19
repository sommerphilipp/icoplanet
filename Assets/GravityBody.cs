using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class GravityBody : MonoBehaviour
{
  GameObject planet;

  void Awake ()
  {
    planet = GameObject.FindGameObjectWithTag ("Planet");
  }

  void Start ()
  {
    GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotation;
    GetComponent<Rigidbody> ().useGravity = false;
    GetComponent<Rigidbody> ().isKinematic = false;
  }

  void FixedUpdate ()
  {
    Vector3 gravityUp = (GetComponent<Rigidbody> ().position - planet.transform.position).normalized;
    Vector3 localUp = GetComponent<Rigidbody> ().transform.up;
    GetComponent<Rigidbody> ().rotation = Quaternion.FromToRotation (localUp, gravityUp) * GetComponent<Rigidbody> ().rotation;
  }
}
