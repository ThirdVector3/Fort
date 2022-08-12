using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public Camera cam;
    public float standartSize;
    public float sizeSwitchTime;
    public bool canShoot = true;
    IEnumerator ShootDelay ()
    {
        yield return new WaitForSeconds(weaponDelay);
        canShoot = true;
    }

    public GameObject bulletPrefab;
    public Transform bulletPlace;


    [Range(-10, 10)]
    public float offset;
    public string weaponType;
    public float weaponMultiplierSpeed;
    public float weaponCamMultiplier;
    public bool weaponInHands;
    public float weaponDamage;
    public float weaponSpeed;
    public float weaponDelay;
    public float weaponSpread;
    public float weaponForce;
    public float weaponAmmo;

    public GameObject aimRifle;
    public GameObject aim;

    public GameObject weapon;

    public float speed = 1;
    public float runSpeed = 1;
    public float runMultiplier = 1;
    public float tmpSpeed;
    public Rigidbody2D rb;
    public Transform forcePos;

    IEnumerator camSizeSwitch()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(sizeSwitchTime / 10);
            cam.orthographicSize += (weaponCamMultiplier / 10);
        }
    }
    IEnumerator camSizeReturn()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(sizeSwitchTime / 10);
            cam.orthographicSize -= (weaponCamMultiplier / 10);
        }
    }

    private void Start()
    {
        cam.orthographicSize = standartSize;
        Cursor.visible = false;
        bulletPlace.position = new Vector2(bulletPlace.position.x + offset, bulletPlace.position.y);
        tmpSpeed = speed;
    }
    void Update()
    {
        Rotate();
        if(Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!weaponInHands)
            {
                speed = Mathf.Clamp(speed, 0, runSpeed);
                speed += runMultiplier;
            }
            if (weaponInHands)
            {
                speed = Mathf.Clamp(speed, 0, runSpeed);
                speed += runMultiplier;
            }
        }
        else
            speed = tmpSpeed;

        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (weaponInHands)
            {
                StartCoroutine(camSizeReturn());
                weapon.GetComponent<Weapon>().weaponAmmo = weaponAmmo;
                weapon.GetComponent<Weapon>().Drop();
                weaponInHands = false;
                weapon = null;
                weaponAmmo = 0;
                aimRifle.SetActive(false);
                aim.SetActive(true);
                speed = tmpSpeed;
            }

        }

        if(Input.GetMouseButton(0))
        {
            if (weaponAmmo >0)
            if (canShoot)
            {
                Shoot();
                canShoot = false;
                StartCoroutine(ShootDelay());
            }
        }
    }

    public void TeleportWeapon()
    {
        weapon.transform.position = transform.position;
        weapon.transform.Translate(new Vector2(offset, 0));
        if (weaponType == "rifle")
        {
            aimRifle.SetActive(true);
            aim.SetActive(false);
        }
        if (weaponType == "gun")
        {
            return;
        }
        if (weaponType == "shotgun")
        {
            return;
        }
        speed *= weaponMultiplierSpeed;

        StartCoroutine(camSizeSwitch());
    }
    private void Rotate()
    {
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.back);
        transform.rotation = rotation;
    }
    private void Shoot()
    {

        if (weapon != null)
        {
            weaponAmmo--;
            rb.AddRelativeForce(new Vector2(0, -weaponForce));
            bulletPrefab.GetComponent<Bullet>().speed = weaponSpeed;
            bulletPrefab.GetComponent<Bullet>().damage = weaponDamage;
            Instantiate(bulletPrefab, bulletPlace.position, gameObject.transform.rotation * Quaternion.Euler(0, 0, Random.Range(weaponSpread, -weaponSpread)));
        }
    }
}
