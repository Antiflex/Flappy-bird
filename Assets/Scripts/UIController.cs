using UnityEngine;

public class UIController : MonoBehaviour
{
    private PlayerBehavior _playerBehavior;

    // Start is called before the first frame update
    void Start()
    {
        _playerBehavior = GameObject.FindObjectOfType<PlayerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        Debug.Log("hey");
        _playerBehavior.StartGame();
    }
}
