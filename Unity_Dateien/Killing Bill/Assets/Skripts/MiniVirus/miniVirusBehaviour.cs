using UnityEngine;

public class miniVirusBehaviour : MonoBehaviour
{
    private float speed = 5;
    public float baseSpeed = 5;
    public float maxSpeed = 30;
    public float acceleration = 15f;
    private float timeElapsed;
    private bool gogoMinivirus = false;
    private GameObject[] enemies;
    private GameObject closestEnemy = null;
    
    // Start is called before the first frame update
    void Start()
    {

        enemies = GameObject.FindGameObjectsWithTag("Shield");
        float x = Random.Range(0.0f, 300.0f);
        float y = Random.Range(0.0f, 300.0f);
        float z = Random.Range(0.0f, 300.0f);
        Vector3 force = new Vector3(x,y,z);
        gameObject.GetComponent<Rigidbody>().AddForce(force);
       
        timeElapsed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Shield");
        if (gogoMinivirus)
        {
            updateSeek(closestEnemy);
        }
        else
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed > 1.0f)
            {
                //Destroy(GetComponent<Rigidbody>());
                initSeek();
            }
        }
    }

    void initSeek()
    {
        speed = baseSpeed;
        gogoMinivirus = true;
        if (enemies.Length > 0)
        {
            // Wählt das nächste 'Enemy'-GameObject aus

            closestEnemy = null;
            float closestDistance = Mathf.Infinity;

            foreach (GameObject enemy in enemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }

        }
        else
        {
            gogoMinivirus = false;
        }
    }

    void updateSeek(GameObject target)
    {
        if( target != null)
        {
            if(speed < maxSpeed)
                speed += acceleration * Time.deltaTime;

            Vector3 enemyPosition = closestEnemy.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, enemyPosition, speed * Time.deltaTime);
        }
        else
        {
            initSeek();
        }
    }

}
