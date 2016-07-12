using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

    public GameObject shotPrefab;
    public Vector3 shotSpawnOffset = new Vector3(0, 0, .5f);
    public float frequency = .1f;
    public float delay = .5f;
    public bool fullAuto = true;
    public Vector3 shotVelocity = new Vector3(0, 0, 3);
    private Vector3 shootDirectionVelocity;

    private float nextShootTime = 0f;
    private bool shootingAuto = false;
    private bool semiFired = false;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        SenseShoot();
	}

    void SenseShoot() {
        float lTrig = Input.GetAxis("LeftTrigger");
        float rTrig = Input.GetAxis("RightTrigger");
        if (!fullAuto) {
            if (lTrig > .3 && rTrig > .3) {
                Fire();
                semiFired = true;
            } else {
                semiFired = false;
            }
        } else {
            if (shootingAuto) {
                if (lTrig > .3 && rTrig > .3 && Time.time > nextShootTime){
                    Fire();
                    nextShootTime = Time.time + frequency;
                } else if(Time.time > nextShootTime){
                    shootingAuto = false;
                }
            } else {
                if (lTrig > .3 && rTrig > .3){
                    nextShootTime = Time.time + delay;
                    shootingAuto = true;
                }
            }
        }
        
        
    }

    void Fire() {
        shootDirectionVelocity = transform.TransformDirection(shotVelocity);
        GameObject instance = (GameObject)Instantiate(shotPrefab, transform.position + transform.TransformDirection(shotSpawnOffset), transform.rotation);
        instance.GetComponent<Rigidbody>().AddForce(shootDirectionVelocity);
    }
}
