using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int enemyHealth;
    public int maxEnemyHealth = 4;
    public Transform Player;
    public int MoveSpeed = 4;
    public int MaxDist = 100;
    public int MinDist = 1;
    public float fireRate = 1.5F;
    private float nextFire = 0.0F;
    public int AttackDist = 2;
    public GameManager gameManager;
    public GameObject gObj;
    public SpriteRenderer spRndrer;
    public List<GameObject> enemyMovePoint = new List<GameObject>();
    private string lookAtMe;
    private GameObject lookAtMeObj;
    public AudioSource deathSound;

    // Use this for initialization
    void Awake()
    {
        enemyHealth = 4;
        gObj = GameObject.FindWithTag("GameManager");
        gameManager = gObj.GetComponent<GameManager>();
        lookAtMe = "MainCamera";
        //SetupEnemy(GameObject.Find("WaypointOne").transform);
        //SetupEnemy(new List<GameObject>() {GameObject.Find("WaypointOne"), GameObject.Find("WaypointTwo") });
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.GamePaused && !gameManager.GameOver)
        {
            //Check if enemy health is greater than 0, if so update movement.
            //If not, then update player score and destroy this enemy.
            if (enemyHealth > 0)
            {
                if (enemyMovePoint.Count == 0) // if there are move enemy move points then look at camera
                {
                    Player = GameObject.FindWithTag("MainCamera").transform;
                }
                else // If there are still move point then look at the given move point
                {
                    Player = lookAtMeObj.transform;
                }

                this.transform.LookAt(Player); // Makes the enemy look at the game object refrenced above

                if (Vector3.Distance(this.transform.position, Player.position) >= MinDist)
                {
                    if (Vector3.Distance(this.transform.position, Player.position) <= MaxDist)
                    {
                        //Here Call any function U want Like Shoot at here or something
                        this.transform.position += this.transform.forward * MoveSpeed * Time.deltaTime;
                    }
                }

                //Attack player if close enough and no more move points
                if ((Vector3.Distance(this.transform.position, Player.position) <= AttackDist))
                {
                    if (enemyMovePoint.Count == 0 && (Time.time > nextFire))
                    {
                        nextFire = Time.time + fireRate;
                        spRndrer.enabled = false;
                        gameObject.SetActive(false);

                        //TODO: make enemy attack noise                       }

                        //Everything past the line below will not be run
                        gameManager.CharacterAttacked();
                    }
                    else //remove old emeny move point and set new one if available 
                    {
                        enemyMovePoint.RemoveAt(0);

                        if (enemyMovePoint.Count > 0)
                        {
                            lookAtMeObj = enemyMovePoint[0];
                        }
                    }
                }
            }
            else
            {
                //Needs: play Death sound
                StartCoroutine(EnemyDeathFlash(0.1f));
                gameManager.EnemyDestroyed();
                deathSound.Play();
                //Everything past the line below will not be run
               
            }
        }
        
    }

    //Simple function to update the current enemy's health
    public void EnemyTakeDamage()
    {
        Debug.Log("Hello");
        enemyHealth -= 1;
    }

    public IEnumerator EnemyDeathFlash(float x)
    {
        for (int i = 0; i < 10; i++)
        {
            spRndrer.enabled = false;
            yield return new WaitForSeconds(x);
            spRndrer.enabled = true;
            yield return new WaitForSeconds(x);

        }
        spRndrer.enabled = false;
        gameObject.SetActive(false);
    }


    //Sets up where the enemy will start and the path that he will follow before chasing the main Camera.
    //first parameter in the list will be the spawn point, then what ever is left in the list will become the waypoints.
    //the list should read from left to right list[startposition , first waypoint, second waypoint, ...]
    public void SetupEnemy(List<GameObject> DestinationList)
    {
        //move enemy to position
        gameObject.transform.position = DestinationList[0].transform.position;

        //Set health to full
        enemyHealth = maxEnemyHealth;

        //re-enable object and renderer
        gameObject.SetActive(true);
        spRndrer.enabled = true;

        //Set move points
        DestinationList.RemoveAt(0);
        enemyMovePoint = DestinationList;
        if (enemyMovePoint.Count > 0)
        {
            lookAtMeObj = enemyMovePoint[0];
        }
    }

    //Sets up enemy at a spawn point with full health ^w^ rawr
    public void SetupEnemy(Transform spawn)
    {
        //move enemy to position
        gameObject.transform.position = spawn.position;

        //Set health to full
        enemyHealth = maxEnemyHealth;

        //re-enable object and renderer
        gameObject.SetActive(true);

        spRndrer.enabled = true;
    }
}

