using TMPro;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    // Collision Variables
    private Rigidbody2D _rigidbody;
    private const string TAG_GROUND = "Ground";
    private const string TAG_PIPE = "Pipe";
    private const string TAG_GAP = "Gap";

    // UI variables
    private GameObject _playButton;
    private TextMeshProUGUI _scoreText;
    private GameObject _objGameOver;

    // Game Logic
    private PipeSpawner _pipe_Spawner;
    private bool _isPlaying = false;
    public bool IsPlaying { get => _isPlaying; set => _isPlaying = value; }
    private int _score;
    private float _force = 100.0f;
    public float Force
    {
        get => _force;
        set => _force = value;
    }
    private static float _gameDuration;
    public static float GameDuration { get => _gameDuration; private set => _gameDuration = value; }

    // Bird animation and sounds
    private Animator _animator;
    private AudioSource _audioSource;
    public AudioClip _jumpAudioClip;
    public AudioClip _pointAudioClip;
    public AudioClip _hurtAudioClip;


    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = 0;
        _objGameOver = GameObject.Find("gameover");
        _scoreText = GameObject.Find("Score Text").GetComponent<TextMeshProUGUI>();
        _scoreText.text = "Score: 0";
        _playButton = GameObject.Find("Play Button");
        _pipe_Spawner = GameObject.Find("Pipe Controller").GetComponent<PipeSpawner>();
        Debug.Log(_pipe_Spawner);
        _playButton.SetActive(true);
        _objGameOver.SetActive(false);
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        GameDuration = 0;
    }

    public void EndGame()
    {
        _rigidbody.gravityScale = 0;
        _objGameOver.SetActive(true);
        _animator.enabled = false;
        IsPlaying = false;
        _playButton.SetActive(true);
        _pipe_Spawner.StopSpawning();
    }

    public void StartGame()
    {
        GameDuration = 0;
        _score = 0;
        _scoreText.text = "Score: 0";
        _playButton.SetActive(false);
        IsPlaying = true;
        this.gameObject.transform.position = new Vector3(0, 0, -1);
        _rigidbody.gravityScale = 1;
        _objGameOver.SetActive(false);
        _animator.enabled = true;
        _pipe_Spawner.DestroyAllPipes();
        _pipe_Spawner.StartSpawning();
    }

    // Update is called once per frame
    void Update()
    {
        GameDuration += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!IsPlaying)
            {
                StartGame();
            }
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.AddForce(new Vector2(0, Force));
            _audioSource.PlayOneShot(_jumpAudioClip);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.gameObject.CompareTag(TAG_GROUND) || other.gameObject.CompareTag(TAG_PIPE)) && IsPlaying)
        {
            _audioSource.PlayOneShot(_hurtAudioClip);
            EndGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TAG_GAP) && IsPlaying)
        {
            _score++;
            _audioSource.PlayOneShot(_pointAudioClip);
            _scoreText.text = string.Format("score: {0}", _score);
        }
    }
}
