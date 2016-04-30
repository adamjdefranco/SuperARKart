using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoinSpawningSystem : MonoBehaviour
{
    [Range(0, 1)]
    public float powerupProbability;
    [Range(0, 50)]
    public int numCoinsOnScreen;

    public GameObject scoreCoin;
    public GameObject[] powerupCoins;

	public GameObject plusOneHundred;

    public float raycastY = 5f;
    public float coinHoverHeight = 0.5f;

    public GameObject playSpaceMesh;

    private Bounds bounds;
    private HashSet<Coin> visibleCoins = new HashSet<Coin>();
    public bool shouldSpawnCoins = false;

    [SerializeField]
    private bool isUsingPowerup = false;

    // Use this for initialization
    void Start()
    {
        //playSpaceMesh = GameObject.Find ("Primary Surface");
        bounds = playSpaceMesh.GetComponent<Renderer>().bounds;
        Debug.Log(bounds);
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldSpawnCoins && visibleCoins.Count < numCoinsOnScreen)
        {
            bounds = playSpaceMesh.GetComponent<Renderer>().bounds;
            float rand = Random.Range(0.0f, 1.0f);
            int randNum = -1;
            if (rand <= powerupProbability)
            {
                randNum = Random.Range(0, powerupCoins.Length);
            }
            spawnNewCoin(randNum);
        }
    }

    private void spawnNewCoin(int index)
    {
        Vector3 position = new Vector3(-1, -1, -1);
        int i = 0;
        for (i = 0; i < 5; i++)
        {
            position = new Vector3(Random.Range(bounds.min.x, bounds.max.x), raycastY, Random.Range(bounds.min.z, bounds.max.z));
            position = findGameSpacePosition(position);
            if (validSpawnPosition(position))
            {
                Debug.Log("Found a valid position");
                break;
            }
        }
        if (i == 6)
        {
            Debug.LogError("Ran out of attempts to find a spawn location.");
            return;
        }
        GameObject coin;
        if (index == -1)
        {
            coin = Instantiate(scoreCoin) as GameObject;
        }
        else {
            coin = Instantiate(powerupCoins[index]) as GameObject;
        }
        coin.transform.position = position;
        Coin cCoin = coin.GetComponent<Coin>();
        cCoin.setSpawner(this);
        visibleCoins.Add(cCoin);
    }

    private void removeCoin(Coin c)
    {
        visibleCoins.Remove(c);
        Destroy(c.gameObject);
    }

    public void setCoinSpawnEnabled(bool enabled)
    {
        this.shouldSpawnCoins = enabled;
    }

    private Vector3 findGameSpacePosition(Vector3 input)
    {
        RaycastHit hit;
        Physics.Raycast(input, Vector3.down, out hit, raycastY + 1, LayerMask.NameToLayer("Environment"));
        return new Vector3(input.x, hit.point.y + coinHoverHeight, input.z);
    }

    private bool validSpawnPosition(Vector3 position)
    {
        return position.y == bounds.min.y;
    }

    public void pickedUpCoin(Coin coin, SimpleCarController car)
    {
        //Not using a powerup, or we can pick up this coin while a powerup is going on
        if (!isUsingPowerup || coin.canPickupDuringPowerup())
        {
            removeCoin(coin);
            if (coin.isPowerupCoin())
            {
                StartCoroutine(powerupCoroutine(coin, car));
            }
            else {
                StartCoroutine(coin.triggerEffect(car));
            }
        }
    }

    private IEnumerator powerupCoroutine(Coin c, SimpleCarController car)
    {
        isUsingPowerup = true;
        yield return c.triggerEffect(car);
        isUsingPowerup = false;
    }
}
