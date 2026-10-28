using UnityEngine;
using UnityEngine.SceneManagement;

// Put on an empty GameObject called "GameManager".
// It is the ONE place that decides what happens when the game ends.
// Other scripts (Obstacle, FinishLine) just tell it "the player died" or "the player won".
//
// SINGLETON: there is only ever one GameManager, and any script can reach it with
// GameManager.Instance, with no Inspector slot and no Find needed.
public class GameManager : MonoBehaviour
{
    // 'static' = belongs to the CLASS, not to one object, so it's reachable from anywhere
    // as GameManager.Instance.
    // 'private set' = anyone can READ it, only this script can CHANGE it.
    public static GameManager Instance { get; private set; }

    private PlayerMovement playerMovement;


    [SerializeField] private float restartDelay = 1.5f;
    [SerializeField] private float winDelay = 0.5f;

    // Has the game already ended? Stops GameOver() from running twice
    // if the ball touches two walls at once.
    private bool gameEnded = false;

    // Awake runs before every Start, so Instance is ready
    // before any other script tries to use it.
    private void Awake()
    {
        // If a GameManager already exists, this one is a duplicate: remove it.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;   // 'this' = this GameManager object

        // No DontDestroyOnLoad here: we WANT a fresh GameManager after each
        // restart, so it gets fresh references to this scene's ball and texts.
    }

    private void Start()
    {
        playerMovement = FindFirstObjectByType<PlayerMovement>();
    }

    // PUBLIC: other scripts need to call it.
    public void GameOver()
    {
        if (gameEnded) return;   // already ended, ignore
        gameEnded = true;

        playerMovement.SetCanMove(false);

        // Call Restart() after 'restartDelay' seconds.
        // nameof(Restart) instead of "Restart": a typo becomes a compile error.
        Invoke(nameof(Restart), restartDelay);
    }

    public void Win()
    {
        if (gameEnded) return;
        gameEnded = true;

        playerMovement.SetCanMove(false);

        Invoke(nameof(LoadNextLevel), winDelay);
    }

    // PRIVATE: only this script uses it (Invoke can still call it).
    private void Restart()
    {
        // The scene must be in File > Build Profiles > Scene List.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadNextLevel()
    {
        // The scene must be in File > Build Profiles > Scene List.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}