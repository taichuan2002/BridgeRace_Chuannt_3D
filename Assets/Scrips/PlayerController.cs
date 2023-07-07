using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private Animator _anim;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask layer;
    [SerializeField] private Vector3[] brickW;


    [SerializeField] public GameObject brickBluePrefab;
    [SerializeField] public GameObject unbrickBluePrefab;
    [SerializeField] private List<GameObject> cloneBrick = new List<GameObject>();
    [SerializeField] private List<GameObject> uncloneBrick = new List<GameObject>();


    [SerializeField] private int countBrick;
    [SerializeField] private Transform unBrick;
    [SerializeField] private GameObject unBridge;


    private float brickHeight = 1f;


    private void Update()
    {
        CheckBrick();
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector3(_joystick.Horizontal * speed, rb.velocity.y, _joystick.Vertical * speed);
        if(_joystick.Horizontal !=0 || _joystick.Vertical !=0)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
            //_anim.SetBool("isRunning", true);
        }
        else
        {
            //xe_anim.SetBool("isRunning", false);
        }
       
    }


    IEnumerator RespawnObject(GameObject objectPrefab, Vector3 position, float minDelay, float maxDelay)
    {
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        GameObject newObject = Instantiate(objectPrefab, position, Quaternion.identity);
    }
    private void CheckBrick()
    {
        RaycastHit hit;
        Vector3 origin = transform.position + new Vector3(0, 0.5f, 0.4f);
        Debug.DrawRay(origin, Vector3.down, Color.blue, 10f);
        if (Physics.Raycast(origin, Vector3.down, out hit, Mathf.Infinity, layer))
        {
            if (hit.collider.CompareTag("brickB"))
            {
                countBrick++;
                Destroy(hit.transform.gameObject);
                GameObject newBrick = Instantiate(unbrickBluePrefab, transform);
                newBrick.transform.position = new Vector3(unBrick.position.x, brickHeight, unBrick.position.z);
                brickHeight += 0.1f;
                cloneBrick.Add(newBrick);
                StartCoroutine(RespawnObject(brickBluePrefab, hit.transform.position, 3f, 5f));
            }
        }
        if (Physics.Raycast(origin, transform.position, out hit, 2f))
        {
            if (hit.collider.CompareTag("unBrickB"))
            {
                if(cloneBrick.Count > 0)
                {
                    GameObject lastBrick = cloneBrick[cloneBrick.Count - 1];
                    Destroy(lastBrick.transform.gameObject);
                    unBridge.transform.position = new Vector3(unBridge.transform.position.x, unBridge.transform.position.y + 0.1f, unBridge.transform.position.z + 0.3f);
                    cloneBrick.RemoveAt(cloneBrick.Count -1);
                    GameObject line = Instantiate(unbrickBluePrefab, new Vector3(unBridge.transform.position.x , unBridge.transform.position.y - 0.5f, unBridge.transform.position.z - 0.6f ), Quaternion.Euler(0, 0, 0));
                    uncloneBrick.Add(lastBrick);
                }
                if(uncloneBrick.Count == 20)
                {
                    unBridge.SetActive(false);
                }
                
            }
        }
    }
   
}
