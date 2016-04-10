using UnityEngine;
using System.Collections;

public class AsteroidSpawner : MonoBehaviour {

    public GameObject asteroidPrefab;   //prefab of asteroid to spawn
    public float spawnDelay = 2;        //delay after which to spawn new asteroid

    float spawnRadius = 0;              //units away from center to spawn asteroid
    float spawnAngle = 0;               //spawn angle assigned randomly
    Vector2 topLeftCorner;              //coorinates of top left corner, used to find out spawn radius
    
    
    void Start () {
        topLeftCorner = new Vector2(Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).x - 0.5f, Camera.main.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y + 0.5f);
        spawnRadius = (topLeftCorner - new Vector2(0, 0)).magnitude;
        StartCoroutine("SpawnAsteroid");
    }
	
    //this method spawns an asteroid at a random point on a circle of radius spawnRadius
    IEnumerator SpawnAsteroid()
    {
        if (spawnDelay > 0.5f)
        {
            spawnDelay -= 0.5f;   //reduce delay time with each spawn
        }
        while (true)
        {
            spawnAngle = Random.Range(0, 2 * Mathf.PI);
            if(GameManager.GetInstance().GetState() == GameState.Playing)
                Instantiate(asteroidPrefab, GetSpawnPointOnCircle(spawnAngle, spawnRadius), Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    //returns a point on a circle
    Vector2 GetSpawnPointOnCircle(float spawnAngle, float spawnRadius)
    {
        return new Vector2(spawnRadius * Mathf.Cos(spawnAngle), spawnRadius * Mathf.Sin(spawnAngle));
    }
}
