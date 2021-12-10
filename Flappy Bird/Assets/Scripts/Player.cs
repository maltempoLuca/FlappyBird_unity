using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool isPlaying = false;
    private SpriteRenderer spriteRenderer;
    private Vector3 direction;
    private float topEdge;

    public GameManager gameManager;
    public Sprite[] sprites;
    private float halfHeight;
    private int spriteIndex = 0;
    public float gravity = -9.8f;
    public float stength = 5f;

    public float rotationSpeed = 115.0f;
    public float maxAngleRotation = 23.0f;
    private float angle = 0;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.20f, 0.5f, Camera.main.nearClipPlane + 1f));
        gameManager = FindObjectOfType<GameManager>();
        halfHeight = spriteRenderer.bounds.size.y / 2;
        topEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height - 110, 0)).y;
    }

    private void Start()
    {
        InvokeRepeating(nameof(animateSprite), 0.15f, 0.15f);

    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    private void Update()
    {
        if (!isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))   // 0 == left
            {
                gameManager.startGame();
                isPlaying = true;
                direction = Vector3.up * stength;
                moveBird();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))   // 0 == left
            {
                if (transform.position.y < topEdge)
                    direction = Vector3.up * stength;
            }
            moveBird();
        }
    }

    private void animateSprite()
    {
        spriteIndex = (spriteIndex + 1) % sprites.Length;
        spriteRenderer.sprite = sprites[spriteIndex];

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Scoring")
        {
            gameManager.increaseScore();
        }
        else if (other.tag == "Obstacle")
            gameManager.gameOver();
    }

    private void moveBird()
    {
        direction.y += gravity * Time.deltaTime;

        if (direction.y > 0)
        {
            angle += rotationSpeed * Time.deltaTime;
            if (angle > maxAngleRotation)
                angle = maxAngleRotation;
        }
        else
        {
            angle -= rotationSpeed * Time.deltaTime;
            if (angle < -maxAngleRotation)
                angle = -maxAngleRotation;
        }

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        if (transform.position.y < topEdge)
        {
            transform.position += direction * Time.deltaTime;
        }
        else
        {
            transform.position += direction * Time.deltaTime;
        }
    }
}
