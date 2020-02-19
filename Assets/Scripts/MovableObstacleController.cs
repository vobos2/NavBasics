using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MovableObstacleController : MonoBehaviour {
  private NavMeshObstacle obstacle;

  public float speed = 20f;


  void Start() {
    obstacle = GetComponent<NavMeshObstacle>();

    this.GetComponent<Renderer>().material.color = Color.white;

  }


   void FixedUpdate () {
       Vector3 pos = transform.position;
         if (Input.GetKey (KeyCode.RightArrow)) {
             pos.x += speed * Time.deltaTime;
         }
         if (Input.GetKey (KeyCode.LeftArrow)) {
             pos.x -= speed * Time.deltaTime;
         }
         if (Input.GetKey (KeyCode.UpArrow)) {
             pos.z += speed * Time.deltaTime;
         }
         if (Input.GetKey (KeyCode.DownArrow)) {
             pos.z -= speed * Time.deltaTime;
         }

       transform.position = pos;
   }
}
