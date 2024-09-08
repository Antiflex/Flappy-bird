using System.Collections;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    private const float MAX_VERTICAL_GAP_INCREASE = 0.07f;
    private const float MAX_VERTICAL_OFFSET = 0.3f;
    private const string TAG_PIPE_INSTANCE = "Pipe Instance";
    private float _timeBetweenSpawns = 1f;

    [SerializeField]
    public GameObject pipePrefab;

    private bool _spawnCoroutineEnabled;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nameof(SpawnCorountine));
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerBehavior.GameDuration < 10)
        {
            PipeMovement.Speed = 1.2f;
            _timeBetweenSpawns = 1f;

        }
        else if (PlayerBehavior.GameDuration < 20)
        {
            PipeMovement.Speed = 1.7f;
            _timeBetweenSpawns = 1.15f;
        }
        else
        {
            PipeMovement.Speed = 2f;
            _timeBetweenSpawns = 1.3f;
        }
    }

    IEnumerator SpawnCorountine()
    {
        while (true)
        {
            if (_spawnCoroutineEnabled)
            {
                SpawnPipe();
            }
            yield return new WaitForSeconds(_timeBetweenSpawns);
        }
    }

    public void StartSpawning()
    {
        _spawnCoroutineEnabled = true;
    }

    public void StopSpawning()
    {
        _spawnCoroutineEnabled = false;
    }

    public void SpawnPipe()
    {
        GameObject pipe = Instantiate(pipePrefab);
        pipe.transform.position = new Vector3(0.7f, Random.Range(-MAX_VERTICAL_OFFSET, MAX_VERTICAL_OFFSET), -1);
        pipe.transform.Find("pipe-green-up").position += Vector3.down * Random.Range(0, MAX_VERTICAL_GAP_INCREASE);
        pipe.transform.Find("pipe-green-down").position += Vector3.up * Random.Range(0, MAX_VERTICAL_GAP_INCREASE);
    }


    public void DestroyAllPipes()
    {
        GameObject[] pipes = GameObject.FindGameObjectsWithTag(TAG_PIPE_INSTANCE);
        Debug.Log(pipes.Length);
        foreach (GameObject pipe in pipes)
        {
            Destroy(pipe);
        }
    }
}