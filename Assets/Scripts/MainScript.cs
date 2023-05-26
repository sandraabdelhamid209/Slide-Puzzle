using UnityEngine;

public class MainScript : MonoBehaviour
{

    [SerializeField] private Transform emptySpace = null;
    private int emptySpaceIndex = 15; 
    private Camera _camera;
//The tiles were added inside of unity in the camera size array which can be found in the inspector.
    [SerializeField] private TileScript[] tiles;
    
    
// Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        Shuffle();
    }

// Update is called once per frame
    void Update()
    {
  //      if (IsSolved())
   //         return;
        
//The mouse button will be used to click and move tiles around (0) means that you are using the left mouse button
       if (Input.GetMouseButtonDown(0)) 
       {
           Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
           RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
           SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.Click);
            if (hit)
            {
                if (Vector2.Distance(a: emptySpace.position, b:hit.transform.position) < 1.3)
                {
//Here you are getting information from the <TileScript> so that when the tiles move, the tiles change colour. 
                   Vector2 lastEmptySpacePosition = emptySpace.position;
                   TileScript thisTile = hit.transform.GetComponent<TileScript>();
                   emptySpace.position = thisTile.targetPosition;
                   thisTile.targetPosition = lastEmptySpacePosition;
                   int tileIndex = findIndex(thisTile);
                   tiles[emptySpaceIndex] = tiles[tileIndex];
                   tiles[tileIndex] = null;
                   emptySpaceIndex = tileIndex;
                }
            }
       }


    }
//The tiles wil be shuffled with each other however the empty space will not be shuffled and will remain in the same place.
    public void Shuffle()
    {
//The code in the "do" section is ran then the condition in the "while" is checked and the the "do" code keeps repeating till the condition is false. The "do" code will run at least once.
    if (emptySpaceIndex != 15)
    {
        var tileOn15LastPos = tiles[15].targetPosition;
        tiles[15].targetPosition = emptySpace.position;
        emptySpace.position = tileOn15LastPos;
        tiles[emptySpaceIndex] = tiles[15];
        tiles[15] = null;
        emptySpaceIndex = 15;
    }
    int invertion;
//The puzzle will be shuffled at least 1 time.
    do
    {
        for (int i = 0; i <= 14; i++ )
        {
            

//The array was done so that the tiles are placed in them and are identified that those are the objects that are going to be randomized with a range of 0 to 14.
                var lastPos = tiles[i].targetPosition;
                int randomIndex = Random.Range(0, 14);
                tiles[i].targetPosition = tiles[randomIndex].targetPosition;
                tiles[randomIndex].targetPosition = lastPos;
                var tile = tiles[i];
//When tiles are shuffled they are also shuffled in the array
                tiles[i] = tiles[randomIndex];
                tiles[randomIndex] = tile; 
            }
        invertion = GetInversions();
        Debug.Log ("Puzzle Shuffled");
    }
//This will repeat the shuffling if the inversion number is not even and will check for puzzle solvability. 0 means even and 1 means odd.
     while (invertion % 2 != 0); 
    }

        

//When a tile is moved by clicking on it, it will also move in the index of the array
   public int findIndex(TileScript ts)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i] != null)
            {
                if (tiles[i] == ts)
                {
                    return i;
                }
            }
        }
        return -1;
    }
//This will help puzzle solvability
    int GetInversions()
    {
        int inversionsSum = 0;
        for (int i = 0;i < tiles.Length;i++)
        {
            int thisTileInvertion = 0;
            for (int j = i; j < tiles.Length; j++)
            {
                if (tiles[j] != null)
                {
                if (tiles[i].number > tiles[j].number)
                {
                    thisTileInvertion++;
                }
                }
            }
            inversionsSum += thisTileInvertion;
        }
        return inversionsSum;
    }

 //   public bool IsSolved()
 //   {
 //       foreach (TileScript tile in tiles)
  //      {
  //          if (tile.IsCorrect == false)
  //              return false;
 //       }

 //       return true;
  //  }
}
