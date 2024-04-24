using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public GameObject cameraObj;
    public GameObject gameOverCanvas;
    private float topScore = 0.0f;
    public Text scoreText;
    public float movementSpeed = 10f;
    public float movement = 0f;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal") * movementSpeed;
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -3.30f, 3.30f),
            transform.position.y, transform.position.z);
        if(movement < 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (screenPosition.y < 0)
        {
            Destroy(this.gameObject);
            cameraObj.GetComponent<CameraFollow>().enabled = false;
            gameOverCanvas.SetActive(true);
        }
        if(rb.velocity.y > 0 && transform.position.y > topScore)
        {
           topScore = transform.position.y;
        }
        scoreText.text = "Score: " + Mathf.Round(topScore).ToString();
    }

    void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = movement;
        rb.velocity = velocity;
    }
}
