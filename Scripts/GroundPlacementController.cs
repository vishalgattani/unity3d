using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//  Camera cam = GameObject.Find("myObject").GetComponent<Camera>();
public class GroundPlacementController : MonoBehaviour

{
    public LayerMask groundLayer;
    public LayerMask obstacleLayer;
    public float heightAboveGround=0.25f;
    [SerializeField]
    private GameObject[] placeableObjectPrefabs;

    private GameObject currentPlaceableObject;

    private float mouseWheelRotation;
    private int currentPrefabIndex = -1;

    

    private void Update()
    {
        HandleNewObjectHotkey();

        if (currentPlaceableObject != null)
        {
            MoveCurrentObjectToMouse();
            RotateFromMouseWheel();
            ReleaseIfClicked();
        }
    }

    private void HandleNewObjectHotkey()
    {
        for (int i = 0; i < placeableObjectPrefabs.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + 1 + i))
            {
                if (PressedKeyOfCurrentPrefab(i))
                {
                    Destroy(currentPlaceableObject);
                    currentPrefabIndex = -1;
                }
                else
                {
                    if (currentPlaceableObject != null)
                    {
                        Destroy(currentPlaceableObject);
                    }

                    currentPlaceableObject = Instantiate(placeableObjectPrefabs[i]);
                    
                    currentPlaceableObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                    // currentPlaceableObject.GetComponent<Rigidbody>().useGravity = false;
                    currentPlaceableObject.GetComponent<BoxCollider>().enabled = false;
                    currentPrefabIndex = i;
                }

                break;
            }
        }
    }

    private bool PressedKeyOfCurrentPrefab(int i)
    {
        return currentPlaceableObject != null && currentPrefabIndex == i;
    }

    private void MoveCurrentObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        Vector3[] returnData = new Vector3[] { Vector3.zero, Vector3.zero };
        if (Physics.Raycast(ray, out hitInfo, groundLayer))
        {
            Debug.Log(hitInfo.point);
            Quaternion spawnRot = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            
            // Collider[] c = new Collider[1];
            // Vector3 sc = new Vector3(1f,1f,1f);
            // int numColliders  = Physics.OverlapBoxNonAlloc(hitInfo.point,sc,c,spawnRot,obstacleLayer);
            // if (numColliders==0){
            currentPlaceableObject.transform.position = new Vector3( hitInfo.point.x , hitInfo.point.y + heightAboveGround,  hitInfo.point.z);
            currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            // }
            
            
        }

        
    }

    private void RotateFromMouseWheel()
    {
        // Debug.Log(Input.mouseScrollDelta);
        mouseWheelRotation += Input.mouseScrollDelta.y;
        currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
    }

    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentPlaceableObject.GetComponent<Rigidbody>().useGravity = true;
            currentPlaceableObject.GetComponent<BoxCollider>().enabled = true;
            currentPlaceableObject.layer = LayerMask.NameToLayer("Default");
            currentPlaceableObject = null;
        }
    }

    private void OnTriggerEnter(Collider other) {
        ReleaseIfClicked();
    }

    private void OnTriggerExit(Collider other) {
        
    }
}