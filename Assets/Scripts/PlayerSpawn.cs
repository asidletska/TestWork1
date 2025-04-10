using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject[] characterPrefabs; // Додай сюди всі префаби персонажів

    void Start()
    {
        int index = PlayerPrefs.GetInt("SelectPlayer");

        if (index >= 0 && index < characterPrefabs.Length)
        {
            Instantiate(characterPrefabs[index], transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Invalid player index");
        }
    }
}
