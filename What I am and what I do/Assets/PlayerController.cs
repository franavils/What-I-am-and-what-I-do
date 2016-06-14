using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


    Rigidbody2D rb;

    //States
    public bool Square;
    public bool Circle;
    public bool Triangle;

    public GameObject SquareShape;
    public GameObject CircleShape;
    public GameObject TriangleShape;

    public BoxCollider2D SquareCollider;
    public CircleCollider2D CircleCollider;
    public PolygonCollider2D TriangleCollider;

    GameObject activeShape;
    //Movement
    public float HorizontalSpeed;
    float currentYVelocity;
    // Use this for initialization
    void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Square = true;

        SquareCollider = gameObject.GetComponent<BoxCollider2D>();
        CircleCollider = gameObject.GetComponent<CircleCollider2D>();
        TriangleCollider = gameObject.GetComponent<PolygonCollider2D>();

        SquareCollider.enabled = true;
        CircleCollider.enabled = false;
        TriangleCollider.enabled = false;

        SquareShape.SetActive(true);
        CircleShape.SetActive(false);
        TriangleShape.SetActive(false);

        activeShape = SquareShape;
    }
	
	// Update is called once per frame
	void Update () {
        currentYVelocity = rb.velocity.y;

        if (Input.GetButtonDown("Square"))
        {
            Square = true;
            Circle = false;
            Triangle = false;

            


            SquareShape.SetActive(true);
            CircleShape.SetActive(false);
            TriangleShape.SetActive(false);

            SquareCollider.enabled = true;
            CircleCollider.enabled = false;
            TriangleCollider.enabled = false;

            activeShape = SquareShape;
        }
        if (Input.GetButtonDown("Circle"))
        {
            Circle = true;
            Square = false;
            Triangle = false;

            SquareShape.SetActive(false);
            CircleShape.SetActive(true);
            TriangleShape.SetActive(false);

            SquareCollider.enabled = false;
            CircleCollider.enabled = true;
            TriangleCollider.enabled = false;

            activeShape = CircleShape;
        }
        if (Input.GetButtonDown("Triangle"))
        {
            Triangle = true;
            Square = false;
            Circle = false;

            SquareShape.SetActive(false);
            CircleShape.SetActive(false);
            TriangleShape.SetActive(true);

            SquareCollider.enabled = false;
            CircleCollider.enabled = false;
            TriangleCollider.enabled = true;

            activeShape = TriangleShape;
        }
        gameObject.transform.position = activeShape.transform.position;

        if (Square)
        {
            rb.gravityScale = 1.0f;
            float horizontalMovement = Input.GetAxis("Horizontal");

            rb.velocity = new Vector2(horizontalMovement * HorizontalSpeed, currentYVelocity);

        }
        if (Circle)
        {
            rb.gravityScale = -1.0f;
            
        }
        if (Triangle)
        {
            
            
        }
    }
}
