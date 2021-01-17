using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAsteroid : MonoBehaviour
{
    public _GameManager GM;
    public GameObject ship;
    public Animator animator;
    Vector3 targetDirection;
    float _projectileSpeed = 10f;

    string[] names = { "Brown", "Purple", "Green", "Blue" };

    bool enableMove = true;

    public AudioSource audioSource;
    public AudioClip[] audio;
    // Start is called before the first frame update
    void Start()
    {
        CheckStartingAnim();
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<_GameManager>();
        if(GM.isMultiplayer)
        {
            GameObject[] ships = GameObject.FindGameObjectsWithTag("Player");
            ship = ships[Random.Range(0, 2)];
        }
        else
        {
            ship = GameObject.FindGameObjectWithTag("Player");
        }

        DetermineSpeed();
        DetermineRotation();
        ShootAsteroid();

    }

    // Update is called once per frame
    void Update()
    {
        if(enableMove)
        {
            changeAsteroidPosition();
        }

    }

    void CheckStartingAnim()
    {
        int index = Random.Range(0, 4);
        if(index != 0)
        {
            animator.SetBool(names[index], true);
        }

    }

    void DetermineSpeed()
    {
        _projectileSpeed = Random.Range(1f, 4f);
    }

    void DetermineRotation()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Random.Range(0f, 360f));
    }

    void ShootAsteroid()
    {
        if(ship)
        {
            targetDirection = (ship.transform.position - transform.position).normalized;
        }
    }

    void changeAsteroidPosition()
    {
        transform.position += targetDirection * _projectileSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        string tagName = collision.collider.tag;
        if (tagName == "Wall")
        {
            DestroyAsteroid();
        }
        else if (tagName == "Player" || tagName == "Bullet" || tagName == "Asteroid")
        {
            audioSource.PlayOneShot(audio[0]);
            enableMove = false;
            GetComponent<BoxCollider>().enabled = false;
            animator.SetBool("Explode", true);
            AssignPoints(tagName);
        }
        
    }

    void AssignPoints(string tagName)
    {
        if(tagName == "Bullet")
        {
            GM.UpdateScore(5);
        }
        else if(tagName == "Asteroid" )
        {
            GM.UpdateScore(10);
        }
    }

    public void DestroyAsteroid()
    {
        Destroy(gameObject);
    }

}
