using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;


public class SpikeGen : MonoBehaviour
{
    [Header("Inspector")] 
    public List<Obstacles> obstaclesList;
    public int count;
    public float spawnDelay;
    public int ticksBeforeSpawn;
    public static SpikeGen S;
    [Range(0, 100)] public int stairOdds;
    [Header("Runtime")] 
    private int _spikeNumber;
    private float _radius;
    private float _spawnRate;
    public float planetSpeed;
    private bool _spawnRotator;
    private float _spawnPoint;
    private float _plankDist;
    private Rotate _rotate;
    private IEnumerator coroutine;
    
    private void Awake()
    {
        S = this;
        _radius = GetComponent<CircleCollider2D>().radius;
        _rotate = GetComponent<Rotate>();
        _plankDist = Utils.plankDist;
        for (int i = 1; i < obstaclesList.Count; i++)
        {
            float prob = obstaclesList[i].probability;
            if(prob == 0)
                continue;
            obstaclesList[i].probability += obstaclesList[i - 1].probability;
        }
    }

    public void Play()
    {
        planetSpeed = _rotate.speed;
        _spawnRate = (360.0f / Mathf.Abs(planetSpeed)) / count;
        _spikeNumber = 0;
        coroutine = SpawnPeriodically();
        StartCoroutine(coroutine);
    }

    public void Stop()
    {
        StopCoroutine(coroutine);
    }

    IEnumerator SpawnPeriodically()
    {
        while (true)
        {
            _spikeNumber++;
            if (_spikeNumber <= ticksBeforeSpawn)
            {
                yield return new WaitForSeconds(_spawnRate);
            }

            if (_spawnRotator)
            {
                _spawnRotator = false;
                SpawnRotationChanger();
                yield return new WaitForSeconds(_spawnRate);
            
            }
            _spawnPoint = (planetSpeed > 0 ? 60 : 120 ) * Mathf.Deg2Rad;
            string obstName = "spike";
            float rand = Random.Range(0, 100);
            for (int i = 0; i < obstaclesList.Count; i++)
            {
                var obs = obstaclesList[i];
                if (rand < obs.probability && _spikeNumber > obs.spawnAfterTicks)
                {
                    obstName = obstaclesList[i].id;
                    break;
                }
            }

            switch (obstName)
            {
                case "spike":
                    Multiple(Random.Range(1, 4));
                    break;
                case "rotationChanger":
                    _spawnRotator = true;
                    break;
                case "trap":
                    SpawnTrap();
                    break;
                case "brd":
                    Brd();
                    break;
                case "planetChange":
                    PlanetChange();
                    break;
                case "shield":
                    Shield();
                    break;
                case "stair":
                    SpawnStair();
                    break;
            }
            yield return new WaitForSeconds(_spawnRate);
        }
    }
 
    private void Spawn(string name, float angle, float rad)
    {
        GameObject prefab = obstaclesList.Find(x => x.id == name).prefab;
        Vector3 position = Utils.GetPosition(angle) * rad + transform.position;
        GameObject go = Instantiate(prefab, position, Quaternion.Euler(0, 0, angle*Mathf.Rad2Deg - 90));
        go.transform.SetParent(transform);
        Vector3 scale = go.transform.localScale;
        go.transform.localScale = new Vector3(planetSpeed > 0 ? scale.x : -scale.x, scale.y, 1);
        Destroy(go, _spawnRate * 3);

    }
   

    public void Multiple(int count)
    {
        
        float spikedelay = (planetSpeed > 0 ? -1 : 1) * (2 * spawnDelay)* Mathf.Deg2Rad;
        float angle = _spawnPoint;
        for(int i = 0; i < count; i++)
        {
            Spawn("spike", angle, _radius);
            
            float airJumpAngle = angle + (spikedelay / 2.0f);

            Spawn("coin", angle + spikedelay, _radius + 1.8f * _plankDist);
            Spawn("airJump", airJumpAngle, _radius + _plankDist);
            angle += spikedelay;
        }
    }
    public void SpawnRotationChanger()
    {
        float angle = _spawnPoint;
        float delay = (planetSpeed > 0 ? 1 : -1) * spawnDelay * Mathf.Deg2Rad;
        Spawn("spike", angle, _radius);
        Spawn("coin", angle + delay, _radius + 2 * _plankDist);
        Spawn("rotationChanger", angle, _radius + _plankDist);
    }

    public void ChangeRotation()
    {
        planetSpeed = _rotate.speed;
        planetSpeed = -planetSpeed;
        _rotate.speed = planetSpeed;
    }

    private void ReverseSpike(float angle)
    {
        GameObject spikePref = obstaclesList.Find(x => x.id == "spike").prefab;
        Vector3 position = Utils.GetPosition(angle) * (_radius + 2 * _plankDist) + transform.position;
        GameObject go = Instantiate(spikePref, position, Quaternion.Euler(0, 0, angle*Mathf.Rad2Deg + 90));
        go.transform.SetParent(transform);
        Destroy(go, _spawnRate * 3);
    }   
    private void SpawnTrap()
    {
        if (Random.Range(0, 100) < 50)
        {
            ReverseSpike(_spawnPoint);
        }
        else
        {
            float delay = (planetSpeed > 0 ? -1 : 1) * (spawnDelay)* Mathf.Deg2Rad;
            Spawn("coin", _spawnPoint + delay, _radius + 2 * _plankDist);
        }
        Spawn("trap", _spawnPoint, _radius);
    }

    private void SpawnStair()
    {
        float spikedelay = (planetSpeed > 0 ? -1 : 1) * (2 * spawnDelay)* Mathf.Deg2Rad;
        float angle = _spawnPoint;
        float dist = _radius + _plankDist;
        for (int j = 0; j < 3; j++)
        {
            Spawn("airJump", angle + spikedelay/2.0f, dist);
            Spawn("coin", angle + 0.75f * spikedelay, dist + 0.8f * _plankDist);

            angle += spikedelay;
            dist += 0.5f;
        }
        Spawn("shield", angle, dist);
    }
    private void Brd()
    {
        Spawn("brd", _spawnPoint, _plankDist);
        float delay = (planetSpeed > 0 ? -1 : 1) * (spawnDelay)* Mathf.Deg2Rad;
        
        if (Random.Range(0, 100) < 50)
        {
            Spawn("coin", _spawnPoint + delay, _radius + _plankDist);
        }
    }

    private void PlanetChange()
    {
        float delay = (planetSpeed > 0 ? -1 : 1) * (spawnDelay / 2.0f)* Mathf.Deg2Rad;

        Spawn("spike", _spawnPoint, _radius - 0.2f);
        Spawn("spike", _spawnPoint + delay, _radius);
        Spawn("spike", _spawnPoint + 2*delay, _radius - 0.2f);
        Spawn("planetChange", _spawnPoint + delay, _radius + _plankDist);
        for (int i = 0; i < 3; i++)
        {
            Spawn("coin", _spawnPoint, _radius + 3 + i * Utils.plankDist * 2 );
        }

    }

    private void Shield()
    {
        Spawn("shield", _spawnPoint, _radius + Utils.plankDist);
    }

    
    public float GetRadius() => _radius;
}
