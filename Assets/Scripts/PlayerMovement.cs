using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public _GameManager GM;
    public GameObject FirePoint;
    public GameObject Bullet;
    public Animator animator;
    public float maxSpeed;
    public float boostForce;
    public float turnSpeed;
    private Rigidbody myRb;
    private SpriteRenderer renderers;

    private bool isWrappingX;
    private bool isWrappingY;

    public float rightEdge;
    public float leftEdge;
    public float upperEdge;
    public float lowerEdge;

    bool enableMovement = true;
    public AudioSource audioSource;
    public AudioClip[] audio;

    public bool isPlayer1 = true;
    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody>();
        renderers = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enableMovement)
        {
            ScreenWrap();
            Movement();
            CheckShooting();
        }
        else
        {
            myRb.velocity = new Vector3(0,0,0);
        }
    }

    void Movement()
    {
        if(isPlayer1)
        {
            if (Input.GetKey(KeyCode.W))
            {
                animator.SetBool("Moving", true);
                myRb.AddRelativeForce(Vector3.up * boostForce * Time.deltaTime);
            }
            else
            {
                animator.SetBool("Moving", false);
                myRb.velocity = myRb.velocity * 0.99f;
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.forward * turnSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(-Vector3.forward * turnSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                animator.SetBool("Moving", true);
                myRb.AddRelativeForce(Vector3.up * boostForce * Time.deltaTime);
            }
            else
            {
                animator.SetBool("Moving", false);
                myRb.velocity = myRb.velocity * 0.99f;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(Vector3.forward * turnSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(-Vector3.forward * turnSpeed * Time.deltaTime);
            }
        }


    }

    void FixedUpdate()
    {
        if (myRb.velocity.magnitude > maxSpeed)
        {
            myRb.velocity = myRb.velocity.normalized * maxSpeed;
        }
    }

    bool CheckRenderers()
    { 
        if (renderers.isVisible)
        {
            return true;
        }
        return false;
    }

    void ScreenWrap()
    {
        var newPosition = transform.position;
        if (transform.position.y > upperEdge)
        {
            newPosition.y = lowerEdge;
        }
        else if (transform.position.y < lowerEdge)
        {
            newPosition.y = upperEdge;
        }

        if (transform.position.x > rightEdge)
        {
            newPosition.x = leftEdge;
        }
        else if (transform.position.x < leftEdge)
        {
            newPosition.x = rightEdge;
        }

        transform.position = newPosition;
    }

    void CheckShooting()
    {
        if(isPlayer1)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                audioSource.PlayOneShot(audio[0]);
                ShootBullet();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                audioSource.PlayOneShot(audio[0]);
                ShootBullet();
            }
        }

    }

    void ShootBullet()
    {
        Instantiate(Bullet, FirePoint.transform.position, Quaternion.Euler(transform.rotation.eulerAngles.x , transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 90), transform);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Asteroid" || collision.collider.tag == "Bullet")
        {
            audioSource.PlayOneShot(audio[1]);
            enableMovement = false;
            GetComponent<BoxCollider>().enabled = false;
            GM.StopScoreCount();
            animator.SetBool("Explode", true);
        }
    }

    public void DestroyPlayer()
    {
        GM.GameOver();
        Destroy(gameObject);
    }
}
