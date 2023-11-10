using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnChance;
    }

    public SpawnableObject[] objects;

    public bool delete = true;

    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;

    private void OnEnable()
    {
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Spawn()
    {
        if (delete)
        {
            float spawnChance = Random.value;

            foreach (var obj in objects)
            {
                if (spawnChance < obj.spawnChance)
                {
                    GameObject obstacle = Instantiate(obj.prefab);
                    obstacle.transform.position += transform.position;
                    break;
                }

                spawnChance -= obj.spawnChance;
            }
            delete = false;
        }
        

        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

    public void Delete()
    {
        delete = true;
    }
}

