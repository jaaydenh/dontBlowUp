using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public AudioClip FlyAudioClip, DeathAudioClip, ScoredAudioClip, FireBallAudioClip;
    public Sprite GetReadySprite;
    public float RotateUpSpeed = 1, RotateDownSpeed = 1;
    public GameObject IntroGUI, DeathGUI;
    public float VelocityPerJump = 3;
    public float XSpeed = 1;

    PlayerYAxisTravelState playerYAxisTravelState;

    enum PlayerYAxisTravelState
    {
        GoingUp, GoingDown
    }

    //Vector3 playerRotation = Vector3.zero;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	public ShootAreaButton shootButton;
	public FlyAreaButton flyAreaButton;

	private float nextFire;
	private float touchVertical;
	private GameController gameController;

	void Start () {

		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void FireShot () {
		nextFire = Time.time + fireRate;
		Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		GetComponent<AudioSource>().PlayOneShot(FireBallAudioClip);
	}
		
    void Update()
    {
		if (shootButton.CanShoot () && Time.time > nextFire) {
			FireShot ();
		} 

//        if (GameStateManager.GameState == GameState.Intro)
//        {
//			MovePlayerOnXAxis();
//            if (WasTouchedOrClicked())
//            {
//                BoostOnYAxis();
//                GameStateManager.GameState = GameState.Playing;
//                IntroGUI.SetActive(false);
//                ScoreManagerScript.Score = 0;
//				StartCoroutine(gameController.SpawnEnemy());
//            }
//        } else
		
		if (GameStateManager.GameState == GameState.Playing) {
			MovePlayerOnXAxis();
            if (WasTouchedOrClicked())
			if (flyAreaButton.CanFly())
            {
                BoostOnYAxis();
            }
        } 
//		else if (GameStateManager.GameState == GameState.Dead) {
//            Vector2 contactPoint = Vector2.zero;
//
//            if (Input.touchCount > 0)
//                contactPoint = Input.touches[0].position;
//            if (Input.GetMouseButtonDown(0))
//                contactPoint = Input.mousePosition;
//        }
    }

	public void StartGame() {
		MovePlayerOnXAxis();
		BoostOnYAxis();
		GameStateManager.GameState = GameState.Playing;
		IntroGUI.SetActive(false);
		ScoreManagerScript.Score = 0;
		//StartCoroutine(gameController.SpawnEnemy());
		gameController.SpawnNextEncounter();
		//StartCoroutine(gameController.SpawnHazards());
	}

	public void RestartGame() {
		GameStateManager.GameState = GameState.Intro;
		SceneManager.LoadScene("mainGame");
	}

    void FixedUpdate()
    {
        //just jump up and down on intro screen
        if (GameStateManager.GameState == GameState.Intro)
        {
            if (GetComponent<Rigidbody2D>().velocity.y < -1) //when the speed drops, give a boost
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, GetComponent<Rigidbody2D>().mass * 5500 * Time.deltaTime));
        }
        else if (GameStateManager.GameState == GameState.Playing || GameStateManager.GameState == GameState.Dead)
        {
            //FixPlayerRotation();
        }
    }

    bool WasTouchedOrClicked()
    {
        if (Input.GetButtonUp("Jump") || Input.GetMouseButtonDown(0) || 
            (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended))
            return true;
        else
            return false;
    }

    void MovePlayerOnXAxis()
    {
        transform.position += new Vector3(Time.deltaTime * XSpeed, 0, 0);
    }

    void BoostOnYAxis()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, VelocityPerJump);
        GetComponent<AudioSource>().PlayOneShot(FlyAudioClip);
    }
		
//    private void FixPlayerRotation()
//    {
//        if (GetComponent<Rigidbody2D>().velocity.y > 0) playerYAxisTravelState = PlayerYAxisTravelState.GoingUp;
//        else playerYAxisTravelState = PlayerYAxisTravelState.GoingDown;
//
//        float degreesToAdd = 0;
//
//        switch (playerYAxisTravelState)
//        {
//            case PlayerYAxisTravelState.GoingUp:
//                degreesToAdd = 6 * RotateUpSpeed;
//                break;
//            case PlayerYAxisTravelState.GoingDown:
//                degreesToAdd = -3 * RotateDownSpeed;
//                break;
//            default:
//                break;
//        }
//        //solution with negative eulerAngles found here: http://answers.unity3d.com/questions/445191/negative-eular-angles.html
//
//        //clamp the values so that -90<rotation<45 *always*
//		playerRotation = new Vector3(0, 0, Mathf.Clamp(playerRotation.z + degreesToAdd, -90, 45));
//		transform.eulerAngles = playerRotation;
//    }

    /// <summary>
    /// check for collisions
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter2D(Collider2D col)
    {
		if (GameStateManager.GameState == GameState.Playing)
        {
            if (col.gameObject.tag == "Pipeblank")
            {
                GetComponent<AudioSource>().PlayOneShot(ScoredAudioClip);
                ScoreManagerScript.Score++;
            }
			else if (col.gameObject.tag == "Pipe" || col.gameObject.tag == "Enemy" || col.gameObject.tag == "EnemyShot" || col.gameObject.tag == "Hazard")
            {
				PlayerDestroyed();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
		if (GameStateManager.GameState == GameState.Playing)
        {
            if (col.gameObject.tag == "Enemy")
            {
				PlayerDestroyed();
            }
        }
    }

    void PlayerDestroyed()
    {
        GameStateManager.GameState = GameState.Dead;
        DeathGUI.SetActive(true);
        GetComponent<AudioSource>().PlayOneShot(DeathAudioClip);
    }
}
