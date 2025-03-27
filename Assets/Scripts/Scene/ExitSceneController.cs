using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitSceneController : MonoBehaviour
{

    [SerializeField] string targetSceneName;
    [SerializeField] string targetSpawnPointId;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(targetSceneName);
            Debug.Log("player is running to scene " + targetSceneName);
        }
    }
}
