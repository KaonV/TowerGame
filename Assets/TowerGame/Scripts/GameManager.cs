     using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GamePiece> _piecesTypes = new List<GamePiece>();

    [SerializeField] private GamePiece _nextPiece = null;

    [SerializeField] private GamePiece _currentPiece = null;

    public GamePiece GetRandomPiece() => GetRandomObject(_piecesTypes);

    public static T GetRandomObject<T>(List<T> list)
    {
        if (list == null || list.Count == 0)
        {
            Debug.LogWarning("The list is null or empty.");
            return default;
        }

        int randomIndex = Random.Range(0, list.Count);
        return list[randomIndex];
    }
}

[Serializable]
public class GamePiece
{
    public Sprite Sprite;
    public GameObject Prefab;
}