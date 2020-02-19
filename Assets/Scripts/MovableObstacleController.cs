using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MovableObstacleController : MonoBehaviour {
  private NavMeshObstacle obstacle;

  public float speed = 20f;
  private bool isPicked;


  void Start() {
    obstacle = GetComponent<NavMeshObstacle>();

    isPicked = false;
    this.GetComponent<Renderer>().material.color = Color.white;

  }


   void Update () {
       Vector3 pos = transform.position;

       if (isPicked) {
         if (Input.GetKey ("d")) {
             pos.x += speed * Time.deltaTime;
         }
         if (Input.GetKey ("a")) {
             pos.x -= speed * Time.deltaTime;
         }
         if (Input.GetKey ("w")) {
             pos.z += speed * Time.deltaTime;
         }
         if (Input.GetKey ("s")) {
             pos.z -= speed * Time.deltaTime;
         }
       }
       transform.position = pos;
   }

   public void changeColor()
   {
       if (!isPicked)
       {
           this.GetComponent<Renderer>().material.color = Color.green;
           isPicked = true;
       }
       else
       {
           this.GetComponent<Renderer>().material.color = Color.white;
           isPicked = false;
       }
   }
}
