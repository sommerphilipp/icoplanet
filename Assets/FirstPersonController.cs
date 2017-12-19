using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonController : MonoBehaviour {

  public float mouseSensitivity = 10f;
  public float speed = 10f;

  Camera cam;
  float verticalLookRotation;

  Vector3 moveAmount;
  Vector3 smoothMoveVelocity;

	void Awake() {
    cam = Camera.main;
	}
	
	void Update() {
    transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivity);
    verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivity;
    cam.transform.localEulerAngles = Vector3.left *  Mathf.Clamp(verticalLookRotation, -60, 60);

    Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
    Vector3 targetMoveAmount = direction * speed;
    moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, 0.15f);
	}

  void FixedUpdate() {
    GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveAmount) * Time.deltaTime);
  }
}
