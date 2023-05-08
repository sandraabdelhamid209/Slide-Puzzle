using UnityEngine;

public class TileScript : MonoBehaviour
{
//This script was attached to all of the tiles apart from the empty space not the group parent
//If the tile is in the correct position the tile will become green
    public Vector3 targetPosition;
    private Vector3 correctPosition;
    private SpriteRenderer _sprite;
    public int number;

    public bool IsCorrect => Vector3.Distance(targetPosition, correctPosition) < Mathf.Epsilon;

// Awake is called before start
    void Awake()
    {
      targetPosition = transform.position;
      correctPosition = transform.position;
      _sprite = GetComponent<SpriteRenderer>();
    }

// Update is called once per frame
    void Update()
    {
// This was done to ensure smooth movement when clicking the tiles. The tiles will move with the smoothment according to the speed for example 0.05f.
        transform.position = Vector3.Lerp( a: transform.position, b: targetPosition, t: 0.05f);
        if (IsCorrect)
        {
            _sprite.color = Color.green;

        }
        else
        {
//The tile will be white (plain/normal) when not in the correct position.
            _sprite.color = Color.white;

        }
    }
}
