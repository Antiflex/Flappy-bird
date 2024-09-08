using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    private PlayerBehavior _playerBehavior;
    private static float _speed = 1.2f;
    public static float Speed { get => _speed;  set => _speed = value; }

    private Renderer _thisRender;

    // Start is called before the first frame update
    public void Start()
    {
        _playerBehavior = GameObject.FindObjectOfType<PlayerBehavior>();
        _thisRender = GetComponentInChildren<Renderer>();
    }

    public void Awake()
    {
        _playerBehavior = GameObject.FindObjectOfType<PlayerBehavior>();
        _thisRender = GetComponentInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_playerBehavior.IsPlaying)
        {
            gameObject.transform.position += _speed * Time.deltaTime * Vector3.left;
        }

        if (!_thisRender.isVisible && gameObject.transform.position.x < -2)
        {
            Destroy(gameObject);
        }
    }
}
