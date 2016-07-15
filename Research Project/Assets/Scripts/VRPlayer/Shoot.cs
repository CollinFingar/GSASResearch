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
    private bool fireable = false;

    private float minGunScale = .3f;
    private float maxGunScale = .6f;
    private float curGunScale = .3f;
    private float scaleSpeed = 1f;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        SenseAim();
	}

    void SenseShoot() {
        float rTrig = Input.GetAxis("RightTrigger");
        if (!fullAuto) {
            if (rTrig > .3) {
                Fire();
                semiFired = true;
            } else {
                semiFired = false;
            }
        } else {
            if (shootingAuto) {
                if (rTrig > .3 && Time.time > nextShootTime){
                    Fire();
                    nextShootTime = Time.time + frequency;
                } else if(Time.time > nextShootTime){
                    shootingAuto = false;
                }
            } else {
                if (rTrig > .3){
                    nextShootTime = Time.time + delay;
                    shootingAuto = true;
                }
            }
        }   
    }

    void SenseAim() {
        float lTrig = Input.GetAxis("LeftTrigger");
        if (lTrig > .2) {
            if (curGunScale >= maxGunScale && !fireable) {
                curGunScale = maxGunScale;
                fireable = true;
                transform.localScale = new Vector3(transform.localScale.x, curGunScale, transform.localScale.z);
            } else if(curGunScale < maxGunScale) {
                curGunScale += scaleSpeed * Time.deltaTime;
                transform.localScale = new Vector3(transform.localScale.x, curGunScale, transform.localScale.z);
            }
        } else if(lTrig < .2){
            if (fireable) {
                fireable = false;
            }
            if (curGunScale < minGunScale) {
                curGunScale = minGunScale;
                transform.localScale = new Vector3(transform.localScale.x, curGunScale, transform.localScale.z);
            } else if(curGunScale > minGunScale) {
                curGunScale -= scaleSpeed * Time.deltaTime;
                transform.localScale = new Vector3(transform.localScale.x, curGunScale, transform.localScale.z);
            }
        }
        if (fireable) {
            SenseShoot();
        }
    }

    void Fire() {
        shootDirectionVelocity = transform.TransformDirection(shotVelocity);
        GameObject instance = (GameObject)Instantiate(shotPrefab, transform.position + transform.TransformDirection(shotSpawnOffset), transform.rotation);
        instance.GetComponent<Rigidbody>().AddForce(shootDirectionVelocity);
    }
}
