using System.Collections;
using UnityEngine;

public class GunState : IGunState
{
    public GameObject bullet;
    public Transform playerCamera;
    public Vector3 bulletOrigin;
    public PlayerController player;

    public float force = 100f;
    public float gravityModifier = 1f;
    public float gunCooldown = 1f;

    float lastShot = 0f;

    public void Shoot()
    {
        bulletOrigin = player.bulletOrigin.position;
        GameObject currentBullet = Object.Instantiate(bullet, bulletOrigin, Quaternion.identity);
        Rigidbody rig = currentBullet.GetComponent<Rigidbody>();
        rig.AddForce(playerCamera.forward * force, ForceMode.Impulse);
        player.StartCoroutine(DestroyAfterDelay(currentBullet, 2));
    }
    IEnumerator DestroyAfterDelay(GameObject bullet, int delay)
    {
        yield return new WaitForSeconds(delay);
        Object.Destroy(bullet);
    }
    public void Enter(PlayerController _player)
    {
        Debug.Log("Holding Gun");
        player = _player;
        bullet = player.bulletPrefab;
        playerCamera = player.playerCamera;

        player.gun.SetActive(true);
    }
    public void Exit()
    {
        player.gun.SetActive(false);
    }
    public void HandleInput()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            player.SetState(new NoneState());
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            player.SetState(new DeradiatorState());
        }
    }
    public void Update()
    {
        if (Input.GetMouseButton(0) && Time.time > lastShot + gunCooldown)
        {
            Debug.Log("Bruh");
            Shoot();
            lastShot = Time.time;
        }   
    }
}
