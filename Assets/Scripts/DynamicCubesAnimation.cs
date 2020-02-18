using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCubesAnimation : MonoBehaviour {

      public float delta = 5f;  // left right amount
      public float speed = 2.0f;
      public Vector3 startPos;

      void Start () {
          startPos = transform.position;
      }

      void Update () {
          Vector3 v = startPos;
          v.z += delta * Mathf.Sin (Time.time * speed);
          transform.position = v;
      }
}
