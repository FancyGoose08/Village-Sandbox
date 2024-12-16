using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseBehavior : MonoBehaviour
{
    // Aim and shooting
    public LayerMask invisWall;

    // Change projectile
    public GameObject[] projectiles;
    //public GameObject[] humanProjectiles;
    private GameObject currentProjectile;
    public InputAction[] selectProj;

    Ray mouseRay;
    //private bool proj4 = false;
    //private bool projSelected;
    private void Start()
    {
        for (int i = 0; i < projectiles.Length; i++)
        {
            selectProj[i].Enable();
        }
        currentProjectile = projectiles[0];
    }
    private void ThrowTrajectory(Rigidbody rb)
    {
        mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouseRay, out RaycastHit hit, Mathf.Infinity, invisWall))
        {
            Vector3 hitPoint = hit.point;
            hitPoint.Normalize();
            rb.AddForce(hitPoint * currentProjectile.GetComponent<ProjectileBehavior>().throwForce, ForceMode.Impulse);
        }
    }
    private void ChangeProjectile(GameObject proj)
    {
        currentProjectile = proj;
    }
    
    private void Update()
    {
        // Shoot the projectile
        if (Input.GetMouseButtonDown(0))
        {
            GameObject thrownObject = Instantiate(currentProjectile, Camera.main.transform.position, currentProjectile.transform.rotation);
            Rigidbody thrownObjectRB = thrownObject.GetComponent<Rigidbody>();
            ThrowTrajectory(thrownObjectRB);
            
            //projSelected = false;

            if (currentProjectile != projectiles[1])
            {
                Quaternion angle = Quaternion.LookRotation(mouseRay.direction) * Quaternion.Euler(90, 0, 0);
                thrownObject.transform.rotation = angle;
            }
            else
            {
                Quaternion angle = Quaternion.LookRotation(mouseRay.direction) * Quaternion.Euler(0, 0, 0);
                thrownObject.transform.rotation = angle;
            }
            
            //Quaternion angle = Quaternion.LookRotation(mouseRay.direction) * Quaternion.Euler(90, 0, 0);
            //thrownObject.transform.rotation = angle;
        }

        // Change the projectile
        if (selectProj[0].IsPressed())
        {
            ChangeProjectile(projectiles[0]);
            //proj4 = false;
        }
        if (selectProj[1].IsPressed())
        {
            ChangeProjectile(projectiles[1]);
            //proj4 = false;
        }
        if (selectProj[2].IsPressed())
        {
            ChangeProjectile(projectiles[2]);
            //proj4 = false;
        }
        if (selectProj[3].IsPressed())
        {
            ChangeProjectile(projectiles[3]);
            //proj4 = false;
        }
        /*
        if (selectProj[4].IsPressed())
        {
            proj4 = true;
        }

        if (proj4 && !projSelected) {
            ChangeProjectile(humanProjectiles[Random.Range(0, humanProjectiles.Length)]);
            projSelected = true;
        }
        */
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(mouseRay);
    }
}
