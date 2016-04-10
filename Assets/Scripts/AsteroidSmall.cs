using UnityEngine;
using System.Collections;

public class AsteroidSmall : MonoBehaviour {

    GameObject mine;
    GameObject motherShip;
    public float moveSpeedMin = 1f;
    public float moveSpeedMax = 5f;
    Vector3 offset;
    float moveSpeed;
    Vector3 moveDirection;
    public bool collidedWithMine = true;
	
    void Start ()
    {
        mine = GameManager.GetInstance().Mine;
        motherShip = GameManager.GetInstance().MotherShip;
        moveSpeed = Random.Range(moveSpeedMin, moveSpeedMax);
        offset = new Vector3(Random.Range(-10, 10), Random.Range(-0.5f, 0.5f));
        
    }
	
	// Update is called once per frame
	void Update () {
        if (GameManager.GetInstance().GetState() == GameState.Playing || GameManager.GetInstance().GetState() == GameState.GameOver)
        {
            if (collidedWithMine)
            {
                transform.position = Vector3.MoveTowards(transform.position, mine.transform.position + offset, -moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, motherShip.transform.position + offset, -moveSpeed * Time.deltaTime);
            }

        }
    }
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Wall" || collider.tag == "Asteroid")
        {
            Destroy(gameObject);
        }
    }
}
