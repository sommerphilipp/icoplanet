using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GravityAttractor : MonoBehaviour {
  private float gravity = -9.8f;

  List<Rigidbody> attractables;

  void Awake() {
    attractables = GameObject
      .FindGameObjectsWithTag("Attractable")
      .ToList()
      .ConvertAll(go => go.GetComponent<Rigidbody>());
  }

  void FixedUpdate() {
    attractables.ForEach(attractable => {
      Vector3 gravityUp = (attractable.position - transform.position).normalized;

		  // Apply downwards gravity to body
		  attractable.AddForce(gravityUp * gravity);
    });
  }

}
