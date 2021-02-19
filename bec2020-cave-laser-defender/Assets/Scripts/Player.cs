using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] GameObject laserShot;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float firePeriod = 1f;

    Coroutine fireCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    


    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
        

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
        
    }


    private void Fire()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            fireCoroutine = StartCoroutine(FireContinuousily());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine );
        }
        
    }

    public void ThrowProjectile()
    {
        GameObject laser = Instantiate(laserShot, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
    }

    IEnumerator FireContinuousily()
    {
        while (true)
        {
            ThrowProjectile();
            yield return new WaitForSeconds(firePeriod);
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        
        var newPosX = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newPosY = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newPosX, newPosY);   
    }
    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}
