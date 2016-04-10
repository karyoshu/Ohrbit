using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum GameState
{
    MainMenu = 0,
    Playing = 1,
    Paused = 2,
    GameOver = 3
};

public class GameManager : MonoBehaviour {

    public BoxCollider2D topWall;
    public BoxCollider2D bottomWall;
    public BoxCollider2D leftWall;
    public BoxCollider2D rightWall;
    GameState gameState = GameState.MainMenu;   //initialize game state
    public int score = 0;
    public Text scoreUI;        //reference to score ui text
    public GameObject Mine;     //reference to mine object
    public GameObject MotherShip;   //reference to mother ship
    public Canvas PlayingCanvas;
    public Canvas GameOverCanvas;
    static GameManager gameManager;     //static instance to game manager


    public AudioClip[] BlastSound;

	// Use this for initialization
	void Start () {
        if (gameManager == null)
            gameManager = this;

        Camera mainCam = Camera.main;
        //walls are created to destroy small asteroids, so that they don't destroy any other asteroid outside the screen
        topWall.size = new Vector2(mainCam.ScreenToWorldPoint(new Vector3(Screen.width * 2f, 0f, 0f)).x, 1f);
        topWall.offset = new Vector2(0f, mainCam.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y + 0.5f);

        bottomWall.size = new Vector2(mainCam.ScreenToWorldPoint(new Vector3(Screen.width * 2, 0f, 0f)).x, 1f);
        bottomWall.offset = new Vector2(0f, mainCam.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).y - 0.5f);

        leftWall.size = new Vector2(1f, mainCam.ScreenToWorldPoint(new Vector3(0f, Screen.height * 2f, 0f)).y); ;
        leftWall.offset = new Vector2(mainCam.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).x - 0.5f, 0f);

        rightWall.size = new Vector2(1f, mainCam.ScreenToWorldPoint(new Vector3(0f, Screen.height * 2f, 0f)).y);
        rightWall.offset = new Vector2(mainCam.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x + 0.5f, 0f);
    }
	
	// Update is called once per frame
	void Update () {
        if (gameState == GameState.GameOver)
        {
            Time.timeScale = 0.5f;      //slows game on game over
            PlayingCanvas.gameObject.SetActive(false);
            GameOverCanvas.gameObject.SetActive(true);
        }
        else if(gameState == GameState.Playing)
        {
            scoreUI.text = "Score " + score;
            Time.timeScale = 1f;
        }
    }

    public static GameManager GetInstance()
    {
        return gameManager;
    }

    public void ChangeState(int state)
    {
        gameState = (GameState)state;
    }

    public GameState GetState()
    {
        return gameState;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ResetScore()
    {
        score = 0;
    }

    public void Blast()
    {
        GetComponent<AudioSource>().clip = BlastSound[Random.Range(0, BlastSound.Length)];
        GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
        GetComponent<AudioSource>().Play(); 
    }
}
