using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject[] characterPrefabs; // ����� ���� �� ������� ���������

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
