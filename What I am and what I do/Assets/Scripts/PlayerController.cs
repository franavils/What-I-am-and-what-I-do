using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


    Rigidbody2D rb;
    Animator anim;

    //States
    public bool Square;
    public bool Circle;
    public bool Triangle;

    public float SquareGravity;
    public float CircleGravity;
    public float TriangleGravity;


    public GameObject SquareShape;
    public GameObject CircleShape;
    public GameObject TriangleShape;

    public BoxCollider2D SquareCollider;
    public CircleCollider2D CircleCollider;
    public PolygonCollider2D TriangleCollider;

    //Lights
    public GameObject Light2D;
    public Color Blue = new Color32(84, 170, 255, 43);
    public Color Red = new Color32(255, 84, 84, 43);
    public Color Green = new Color32(84, 255, 170, 43);
    public float MaxLightRadius;

    GameObject activeShape;
    //Movement
    //Square
    public float HorizontalSpeedSquare;
    public float MaxHorizontalSpeedSquare;
    public bool Dashing;
    public bool DashingReady;
    public float DashReloadTime;
    public float CurrentDashReloadTime;
    public float DashingDuration;
    public float CurrentTimeDashingDuration;
    public float DashPush;
    public TrailRenderer DashTrail;

    //Circle
    public float HorizontalSpeedCircle;


    float currentYVelocity;
    // Use this for initialization
    void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Square = true;
        anim.SetBool("Square", true);
        DashingReady = true;
        DashTrail = gameObject.GetComponent<TrailRenderer>();
        DashTrail.enabled = false;



        SquareCollider = gameObject.GetComponent<BoxCollider2D>();
        CircleCollider = gameObject.GetComponent<CircleCollider2D>();
        TriangleCollider = gameObject.GetComponent<PolygonCollider2D>();

        SquareCollider.enabled = true;
        CircleCollider.enabled = false;
        TriangleCollider.enabled = false;

        //SquareShape.SetActive(true);
        //CircleShape.SetActive(false);
        //TriangleShape.SetActive(false);

        
    }
	
	// Update is called once per frame
	void Update () {
        //currentYVelocity = rb.velocity.y;
        

        if (Input.GetButtonDown("Square"))
        {
            Square = true;
            Circle = false;
            Triangle = false;
            anim.SetBool("Square", true);
            anim.SetBool("Circle", false);
            anim.SetBool("Triangle", false);




            //SquareShape.SetActive(true);
            //CircleShape.SetActive(false);
            //TriangleShape.SetActive(false);

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
            anim.SetBool("Square", false);
            anim.SetBool("Circle", true);
            anim.SetBool("Triangle", false);

            //SquareShape.SetActive(false);
            //CircleShape.SetActive(true);
            //TriangleShape.SetActive(false);

            SquareCollider.enabled = false;
            CircleCollider.enabled = true;
            TriangleCollider.enabled = false;

            
        }
        if (Input.GetButtonDown("Triangle"))
        {
            Triangle = true;
            Square = false;
            Circle = false;
            anim.SetBool("Square", false);
            anim.SetBool("Circle", false);
            anim.SetBool("Triangle", true);

            //SquareShape.SetActive(false);
            //CircleShape.SetActive(false);
            //TriangleShape.SetActive(true);

            SquareCollider.enabled = false;
            CircleCollider.enabled = false;
            TriangleCollider.enabled = true;

            
        }


        if (Square)
        {
            
            
            if (Light2D.GetComponent<DynamicLight>().LightRadius >= MaxLightRadius)
            {
                DashingReady = true;
            } else if (Light2D.GetComponent<DynamicLight>().LightRadius < MaxLightRadius)
            {
                DashingReady = false;
            }

            Light2D.GetComponent<DynamicLight>().LightMaterial.SetColor("_Color", Blue);

            //Dashing
            if (DashingReady && Input.GetButtonDown("X"))
            {
                Dashing = true;
                
            }
            if (Dashing == false)
            {
                rb.gravityScale = SquareGravity;
                if (Light2D.GetComponent<DynamicLight>().LightRadius < MaxLightRadius)
                {
                    Light2D.GetComponent<DynamicLight>().LightRadius += Time.deltaTime *4;
                }
            }
            
            if (Dashing)
            {

                rb.gravityScale = 0;
                DashTrail.enabled = true;
                Light2D.GetComponent<DynamicLight>().LightRadius -= 0.6f;
                CurrentTimeDashingDuration += Time.deltaTime;
            if (CurrentTimeDashingDuration >= DashingDuration)
            {
                    
                    Dashing = false;
                    DashTrail.enabled = false;
                    CurrentTimeDashingDuration = 0;
                    
            }
            }
           

            //float horizontalMovement = Input.GetAxis("Horizontal");


            //rb.velocity = new Vector2(horizontalMovement * HorizontalSpeed, currentYVelocity);

        }
        if (Triangle)
        {
            rb.gravityScale = TriangleGravity;
            Light2D.GetComponent<DynamicLight>().LightMaterial.SetColor("_Color", Green);
            if (Light2D.GetComponent<DynamicLight>().LightRadius < MaxLightRadius)
            {
                Light2D.GetComponent<DynamicLight>().LightRadius += Time.deltaTime * 4;
            }

        }
        if (Circle)
        {
            rb.gravityScale = CircleGravity;
            Light2D.GetComponent<DynamicLight>().LightMaterial.SetColor("_Color", Red);
            if (Light2D.GetComponent<DynamicLight>().LightRadius < MaxLightRadius)
            {
                Light2D.GetComponent<DynamicLight>().LightRadius += Time.deltaTime * 4;
            }

        }
        

    }

    void FixedUpdate()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        Vector2 movingHorizontal = new Vector2(horizontalMovement, 0);
        float currentYSpeed = rb.velocity.y;
        if (Square)
        {
            rb.isKinematic = false;
            
            if (Dashing)
            {
                Vector2 currentVelocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(currentVelocity.normalized * DashPush, ForceMode2D.Impulse);
            } 
            if (Dashing == false)
            {
                if (Mathf.Abs(rb.velocity.x) > MaxHorizontalSpeedSquare)
                {
                    if (rb.velocity.x > 0)
                    {
                        rb.velocity = new Vector2(MaxHorizontalSpeedSquare, currentYSpeed);
                    }
                    if (rb.velocity.x < 0)
                    {
                        rb.velocity = new Vector2(-MaxHorizontalSpeedSquare, currentYSpeed);
                    }

                }
            }
            

            

            if (Mathf.Abs(rb.velocity.y) > 0)
            {
                
            } else if (Mathf.Abs(rb.velocity.y) == 0)
            {
                rb.AddForce(movingHorizontal * HorizontalSpeedSquare);
            }



            

        }
        if (Circle)
        {
            rb.AddForce(movingHorizontal * HorizontalSpeedCircle);
            rb.isKinematic = false;

        }
        if (Triangle)
        {
            //Vector2 noForce = new Vector2(0, 0);
            //rb.velocity = noForce;
            //rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            //rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            
            rb.isKinematic = true;
            //rb.rotation += 10;
            

        }

    }
}
