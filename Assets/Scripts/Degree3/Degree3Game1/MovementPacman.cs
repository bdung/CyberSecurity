using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementPacman : MonoBehaviour
{
    public float speed = 8f;
    public float speedMultiplier = 1f;
    public Vector2 initialDirection;
    public LayerMask obstacleLayer;

    public new Rigidbody2D rigidbody { get; private set; }
    public Vector2 direction { get; private set; }
    public Vector2 nextDirection { get; private set; }
    public Vector3 startingPosition { get; private set; }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        speedMultiplier = 1f;
        direction = initialDirection;
        nextDirection = Vector2.zero;
        transform.position = startingPosition;
        rigidbody.isKinematic = false;
        enabled = true;
    }

    private void Update()
    {
        // Try to move in the next direction while it's queued to make movementPacmans
        // more responsive
        if (nextDirection != Vector2.zero)
        {
            SetDirection(nextDirection);
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * speed * speedMultiplier * Time.fixedDeltaTime;

        rigidbody.MovePosition(position + translation);
    }

    public void SetDirection(Vector2 direction, bool forced = false)
    {
        // Only set the direction if the tile in that direction is available
        // otherwise we set it as the next direction so it'll automatically be
        // set when it does become available
        if (forced || !Occupied(direction))
        {
            this.direction = direction;
            nextDirection = Vector2.zero;
        }
        else
        {
            nextDirection = direction;
        }
    }

    public bool Occupied(Vector2 direction)
    {
        // If no collider is hit then there is no obstacle in that direction
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0f, direction, 1.25f, obstacleLayer);
        return hit.collider != null;
    }
    public void Btn_AgainPlay()
    {
        Time.timeScale = 1;
        Debug.Log("BtnAgain");
        // LevelSystemManager.Instance.CurrentLevel = LevelSystemManager.Instance.getCurrentLevel();
        int level = LevelSystemManager.Instance.CurrentLevel + 1;
        //set the CurrentLevel, we subtract 1 as level data array start from 0
        SceneManager.LoadScene("Chase_" + level);
        //load the level
        // Reload the current scene
        // SceneManager.LoadScene("Game1");
    }
    public void Btn_NextLevel()
    {
        Debug.Log("btn next level");
        Time.timeScale = 1;
        LevelSystemManager.Instance.CurrentLevel += 1;
        int level = LevelSystemManager.Instance.CurrentLevel + 1;
        //set the CurrentLevel, we subtract 1 as level data array start from 0
        SceneManager.LoadScene("Chase_" + level);

    }
    public void Btn_Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Degree3Game1MenuLevel");
    }
}
