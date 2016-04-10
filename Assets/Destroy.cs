using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("DestroyThis", 3);
	}
	
    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
