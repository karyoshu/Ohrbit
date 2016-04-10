using UnityEngine;
using System.Collections;

public class OrbiterManager : MonoBehaviour {
    public float currentOrbiterSpeed = 100.0f;
    public int orbiterDirection = 1;
    public float minOrbiterSpeed = 100.0f;
    public float maxOrbiterSpeed = 500.0f;
    public float orbiterSpeedIncrement = 20.0f;
    
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && GameManager.GetInstance().GetState() == GameState.Playing)
            ToggleDirection();
        if (Input.GetMouseButton(0) && GameManager.GetInstance().GetState() == GameState.Playing)
        {
            if (currentOrbiterSpeed < maxOrbiterSpeed)
                currentOrbiterSpeed += orbiterSpeedIncrement;
        }
        else
        {
            if (currentOrbiterSpeed > minOrbiterSpeed)
                currentOrbiterSpeed -= orbiterSpeedIncrement;
        }
        if(GameManager.GetInstance().GetState() == GameState.Playing || GameManager.GetInstance().GetState() == GameState.GameOver)
            transform.Rotate(0, 0, orbiterDirection * currentOrbiterSpeed * Time.deltaTime);
	}

    void ToggleDirection()
    {
        orbiterDirection = -orbiterDirection;
    }
}
