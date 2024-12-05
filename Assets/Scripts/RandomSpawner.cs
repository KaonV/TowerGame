using Fusion;
using System.Collections;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;

public class RandomSpawner : NetworkBehaviour
{
    public GameObject[] brickTypes;
    public bool _currentBlock { get; set; }
    public static float lastSpawn;

    public void Start()
    {
        _currentBlock = true;
        if (PhotonManager.Instance.currentTurnIndex == 0)
        {
            PhotonManager.Instance.NextTurn();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //if (!HasInputAuthority) return;

        if (collision.gameObject.CompareTag("Ground") && _currentBlock)
        {
            Debug.Log("Colidiu");
            int randomIndex = Random.Range(0, brickTypes.Length);
            Vector3 SpawnPosition = new Vector3(-30, 604, 0);
            _currentBlock = false;
            PhotonManager.Instance.NextTurn();
            Spawn();
            //Runner.Spawn(brickTypes[randomIndex], SpawnPosition, Quaternion.identity);
        }

        if (collision.gameObject.CompareTag("Bricks") && _currentBlock)
        {
            Debug.Log("Colidiu");
            int randomIndex = Random.Range(0, brickTypes.Length);
            Vector3 SpawnPosition = new Vector3(-30, 604, 0);
            _currentBlock = false;
            PhotonManager.Instance.NextTurn();
            Spawn();
            //Runner.Spawn(brickTypes[randomIndex], SpawnPosition, Quaternion.identity);
        }

        else if (collision.gameObject.CompareTag("Limits") && _currentBlock)
        {
            Debug.Log("Colidiu");
            int randomIndex = Random.Range(0, brickTypes.Length);
            Vector3 SpawnPosition = new Vector3(-30, 400, 0);
            _currentBlock = false;
            PhotonManager.Instance.NextTurn();
            Spawn();
            //Runner.Spawn(brickTypes[randomIndex], SpawnPosition, Quaternion.identity);

        }
    }

    private void Spawn()
    {
        RPC_Spawn();
    }

    private IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(1f);
        RPC_Spawn();
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    private void RPC_Spawn()
    {
        if (Time.time > lastSpawn + 6f)
        {
            lastSpawn = Time.time;
            int randomIndex = Random.Range(0, brickTypes.Length);
            Vector3 SpawnPosition = new Vector3(-30, 604, 0);
            Runner.Spawn(brickTypes[randomIndex], SpawnPosition, Quaternion.identity);
        }
        else
        {
            StartCoroutine(DelaySpawn());
        }
    }
}