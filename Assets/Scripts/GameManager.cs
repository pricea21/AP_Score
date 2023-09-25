using UnityEngine;
using UnityEngine.SceneManagement;
using static Minimize;

public class GameManager : MonoBehaviour
{
	bool gameHasEnded = false;
	public float restartDelay = 2f;
	public GameObject completeLevelUI;
    public GameObject replay;
    bool instantReplay = false;
    GameObject player;
    float replayStartTime;

    private void OnEnable()
    {
        PlayerCollision.OnHitObstacle += EndGame;
        Enlarge.OnGetLarger += GetEnlarge;
        Minimize.OnGetSmaller += GetSmall;
    }

    private void OnDisable()
    {
        PlayerCollision.OnHitObstacle -= EndGame;
        Enlarge.OnGetLarger -= GetEnlarge;
        Minimize.OnGetSmaller -= GetSmall;
    }

    private void GetEnlarge(Collision enlarge)
    {
        if (enlarge != null)
        {
            player.transform.localScale = new Vector3(2, 2, 2);
        }
    }

    private void GetSmall(Collision minimize)
    {
        if (minimize != null)
        {
            player.transform.localScale = new Vector3(0.5f, 0.5f , 0.5f);
        }
    }
    void Start()
    {
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        player = playerMovement.gameObject;

        if (CommandLog.commands.Count > 0)
        {
            instantReplay = true;
            replayStartTime = Time.timeSinceLevelLoad;
        }
    }

    void Update()
    {
        if (instantReplay)
        {
            RunInstantReplay();
        }
    }
    public void CompleteLevel()
	{
		completeLevelUI.SetActive(true);
	}

    /*public void EndGame()
	{
		if (gameHasEnded == false)
		{
			gameHasEnded = true;
			Debug.Log("Game Over!");
			Invoke("Restart",restartDelay);
		}
	}*/

    public void EndGame(Collision collisionInfo)
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        PlayerCollision.OnHitObstacle -= EndGame;

        if (collisionInfo != null)
        {
            Debug.Log("Hit: " + collisionInfo.collider.name);
        }

        // this flag prevents responding to multiple hit events:
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Invoke("Restart", restartDelay);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void RunInstantReplay()
    {
        if (CommandLog.commands.Count == 0)
        {
            return;
        }
        replay.SetActive(true);
        Command command = CommandLog.commands.Peek();
        if (Time.timeSinceLevelLoad >= command.timestamp) // + replayStartTime)
        {
            command = CommandLog.commands.Dequeue();
            command._player = player.GetComponent<Rigidbody>();
            Invoker invoker = new Invoker();
            invoker.disableLog = true;
            invoker.SetCommand(command);
            invoker.ExecuteCommand();
        }
    }
}
