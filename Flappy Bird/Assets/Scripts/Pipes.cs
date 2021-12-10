using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : MonoBehaviour
{
    private Vector3 changeHeightVector3;
    public float currentXSpeed = 3f;
    public float currentYSpeed = 0.5f;

    private float currentYPosition = 0.0f;
    private float minHeight = -1.2f;
    private float maxHeight = 3f;

    private float changeHeight;
    private float leftEdge;

    private bool movingOnY = false;
    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
    }

    public void changeSpeed(float speed)
    {
        this.currentXSpeed = speed;
    }
    void Update()
    {
        transform.position += Vector3.left * currentXSpeed * Time.deltaTime;
        if (this.movingOnY)
        {
            Debug.Log("step");
            float step = currentYSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, changeHeightVector3, step);
        }
        if (movingOnY && ((transform.position.y < -Mathf.Abs(this.changeHeightVector3.y)) || (transform.position.y > Mathf.Abs(this.changeHeightVector3.y))))
        {
            isMovingOnY(true);
        }
        if (transform.position.x < leftEdge)
        {
            changeHeight = 0;
            if (movingOnY)
                changeHeightVector3 = new Vector3(0, 0, 0);
            movingOnY = false;
            Destroy(gameObject);
        }
    }

    public void isMovingOnY(bool move)
    {
        this.movingOnY = move;
        if (this.movingOnY)
        {
            currentYPosition = transform.position.y;
            changeHeight = Random.Range(minHeight, maxHeight);
            changeHeightVector3 = new Vector3(transform.position.x, changeHeight, transform.position.z);
            this.currentYSpeed = Random.Range(0.3f, 0.8f);
        }
    }
}
