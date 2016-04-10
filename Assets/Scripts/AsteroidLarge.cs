using UnityEngine;
using System.Collections;

public class AsteroidLarge : MonoBehaviour {

    public float asteroidInitialMoveSpeedMin = 1f;          //Initial Asteroid Min Speed
    public float asteroidInitialMoveSpeedMax = 5f;          //Initial Asteroid Max Speed
    public float asteroidMoveSpeedIncrement = 0.05f;        //Asteroid Move Speed Increment
    public GameObject[] smallAsteroids;                     //List of small asteroids prefabs to spawn
    public float AsteroidRotateSpeed = 100.0f;              //Asteroid rotate speed

    public GameObject explosionPrefab;                      //Explosion animation prefab

    SpriteRenderer tailSprite;                              //reference to tail sprite in child
    Transform asteroidSprite;                               //reference to asteroid sprite in child
    float asteroidMoveSpeed;                                //asteroid move speed
    Transform motherShip;                                   //reference to mother ship
    

    void Start() {
        motherShip = GameObject.FindGameObjectWithTag("MotherShip").transform;
        asteroidMoveSpeed = Random.Range(asteroidInitialMoveSpeedMin, asteroidInitialMoveSpeedMax);
        tailSprite = GetChildByName("Tail").GetComponent<SpriteRenderer>();
        asteroidSprite = GetChildByName("Asteroid");

        FaceTowardsMothership();
    }

    // Update is called once per frame
    void Update() {
        if (GameManager.GetInstance().GetState() == GameState.Playing || GameManager.GetInstance().GetState() == GameState.GameOver)
        {
            MoveTowardsMothership();
            asteroidSprite.Rotate(0, 0, AsteroidRotateSpeed * Time.deltaTime);
        }
    }

    void FaceTowardsMothership()
    {
        Vector3 motherShipDirection = motherShip.position - transform.position;
        motherShipDirection.Normalize();
        float rotation = Mathf.Atan2(motherShipDirection.y, motherShipDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation + 270);
    }

    void MoveTowardsMothership()
    {
        transform.position = Vector3.MoveTowards(transform.position, motherShip.position, asteroidMoveSpeed * Time.deltaTime);
        asteroidMoveSpeed += asteroidMoveSpeedIncrement;
        float distanceFromMotherShip = (motherShip.position - transform.position).magnitude;
        tailSprite.color = new Color(tailSprite.color.r, tailSprite.color.g, tailSprite.color.b, 1 / distanceFromMotherShip);
    }

    Transform GetChildByName(string name)
    {
        foreach (Transform child in transform)
        {
            if (child.name == name)
            {
                return child;
            }
        }
        return null;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "MotherShip")
        {
            Destroy(gameObject);
            SpawnSmallerAsteroids(false);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            GameManager.GetInstance().ChangeState(3);
        }
        if (collider.tag == "Mine" || collider.tag == "SmallAsteroid")
        {
            Destroy(gameObject);
            SpawnSmallerAsteroids(true);
        }
    }

    void SpawnSmallerAsteroids(bool collidedWithMine)
    {
        GameManager.GetInstance().score++;
        GameManager.GetInstance().Blast();
        for (int i = 0; i < 5; i++)
        {
            GameObject smallAsteroid = (GameObject)Instantiate(smallAsteroids[Random.Range(0, smallAsteroids.Length)], transform.position, Quaternion.identity);
            smallAsteroid.GetComponent<AsteroidSmall>().collidedWithMine = collidedWithMine;
        }
    }
}
