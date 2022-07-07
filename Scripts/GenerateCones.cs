using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCones : MonoBehaviour
{
    // // Start is called before the first frame update
    // public GameObject cone;
    // public float x;
    // public float z;
    // public int num_cones;

    // public float rayCastDistance = 100f;
    // public GameObject objectToSpawn;
    
    // void Start(){
    //     StartCoroutine(dropCone());
    // }

    // IEnumerator dropCone(){
    //     while(num_cones<5){
    //         RaycastHit hit;
    //         if(Physics.Raycast(transform.position,Vector3.down,out hit, rayCastDistance)){
    //             Quaternion spawnRot = Quaternion.FromToRotation(Vector3.up,hit.normal);
    //             GameObject clone = Instantiate(cone,hit.point,spawnRot);
    //         }
    //         // x = Random.Range(1.0f,2.0f);
    //         // z = Random.Range(-3.0f,-5.0f);
    //         // Instantiate(cone,new Vector3(x,-1,z),Quaternion.identity);
    //         yield return new WaitForSeconds(0.0f);
    //         num_cones+=1;
    //     }
    // }
    
    public GameObject enemy;
    public float radiusSpawn=100.0f;
    public int numOfBasicEnemies=100;
    public float heightAboveGround=0.1f;
    public LayerMask groundLayer;
    Transform tr;
    RaycastHit hit;

    void Awake()
    {
        tr = transform;    
    }    

    void Start()
    {
        for (int i = 0; i < numOfBasicEnemies; i++)
        {
            Vector2 randomCircle = Random.insideUnitCircle * radiusSpawn;
            Vector3 v3rc = new Vector3(tr.position.x + randomCircle.x,tr.position.y, tr.position.z + randomCircle.y);
            if (Physics.Raycast(v3rc, Vector3.down, out hit, 150, groundLayer))
            {
                GameObject enemyRay = Instantiate(enemy) as GameObject;
                enemyRay.transform.SetPositionAndRotation(new Vector3(v3rc.x, hit.point.y + heightAboveGround, v3rc.z), tr.rotation);
                enemyRay.transform.parent = tr;
            }
        }
    }
}

