using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOnMouseClick : MonoBehaviour
{

    public GameObject barralPrefab;
    public LayerMask layermask;
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, layermask))
            {
                if(hit.collider.CompareTag("Ground"))
                    Instantiate(barralPrefab, hit.collider.transform.position, Quaternion.identity);

            }
        }
    }

    public void PutCoin(Vector2 mousePosition)
    {
        RaycastHit hit = RayFromCamera(mousePosition, 1000.0f);
    }

    public RaycastHit RayFromCamera(Vector3 mousePosition, float rayLength)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Physics.Raycast(ray, out hit, rayLength);
        return hit;
       
    }
}
