using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
	public PlayerMovement movement;

    public delegate void HitObstacle(Collision collisionInfo);
    public static event HitObstacle OnHitObstacle;

    void OnCollisionEnter (Collision collisionInfo)
	{
		if (collisionInfo.collider.tag == "Obstacle")
		{
			//movement.enabled = false;
			//FindObjectOfType<GameManager>().EndGame();

            if (OnHitObstacle != null)
            {
                OnHitObstacle(collisionInfo);
            }
        }
    }

}
