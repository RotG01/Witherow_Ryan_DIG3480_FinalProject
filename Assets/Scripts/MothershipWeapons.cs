using System.Collections;
using UnityEngine;

public class MothershipWeapons : MonoBehaviour
{

    public GameObject shot;
    public GameObject Barrier;
    public Transform shotSpawnOne;
    public Transform shotSpawnTwo;
    public Transform shotSpawnThree;
    public Transform shotSpawnFour;
    public Transform shotSpawnFive;
    public Transform shotSpawnSix;
    public Transform BarrierSpawn1;
    public Transform BarrierSpawn2;
    public Transform BarrierSpawn3;
    public float fireRate;
    public float delay;

    private AudioSource audioSource;
    private Mothership mothership;
    private int barrierspawncount;

    void Start()
    {
        mothership = GetComponent<Mothership>();
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Fire", delay, fireRate);
    }

    void Fire()
    {
        Instantiate(shot, shotSpawnOne.position, shotSpawnOne.rotation);
        Instantiate(shot, shotSpawnTwo.position, shotSpawnTwo.rotation);

        if (mothership.lives < 16) {
            Instantiate(shot, shotSpawnThree.position, shotSpawnThree.rotation);
            Instantiate(shot, shotSpawnSix.position, shotSpawnSix.rotation);

            if (mothership.lives <= 10 && barrierspawncount != 1)
            {
                Instantiate(Barrier, BarrierSpawn1.position, BarrierSpawn1.rotation);
                Instantiate(Barrier, BarrierSpawn2.position, BarrierSpawn2.rotation);
                Instantiate(Barrier, BarrierSpawn3.position, BarrierSpawn3.rotation);
                barrierspawncount = barrierspawncount + 1;
            }
            if (mothership.lives < 6)
            {
                Instantiate(shot, shotSpawnFour.position, shotSpawnFour.rotation);
                Instantiate(shot, shotSpawnFive.position, shotSpawnFive.rotation);
            }
        }
        audioSource.Play();
    }
}