using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : MonoBehaviour
{
    public float currentSpeed = 3f;
    private float leftEdge;
    private void Start(){
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
    }

    public void changeSpeed(float speed){
        this.currentSpeed = speed;
    }
    void Update()
    {
        transform.position += Vector3.left * currentSpeed * Time.deltaTime;
        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }
}
