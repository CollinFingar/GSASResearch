using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public GameObject leftGun;
	public GameObject rightGun;

    public GameObject shotPrefab;
    public Vector3 leftShotSpawnOffset = new Vector3(0, 0, .5f);
	public Vector3 rightShotSpawnOffset = new Vector3(0, 0, .5f);
    public float frequency = .1f;
    public float delay = .5f;
    public bool fullAuto = true;
    public Vector3 shotVelocity = new Vector3(0, 0, 3);
    private Vector3 shootDirectionVelocity;

    private float nextShootTime = 0f;
    private bool shootingAuto = false;
    private bool semiFired = false;
    private bool fireable = false;

	public GameObject[] gunEnergyObjectArray;
	private SpriteRenderer[] gunEnergySpriteArray;
	public Color gunEnergyColor = new Color(1,1,1,1);
	private float alpha = 0f;
	private float alphaRate = 3f;

	public int energyCost = 2;
	public VRGUIHandler gui;

	public GameObject aimThresholdObject;
	private Vector3 aimThreshold;
	public float aimSideAngle = 1f;


    // Use this for initialization
    void Start () {
		InitializeGunEnergy ();
	}
	
	// Update is called once per frame
	void Update () {
        SenseAim();
	}

    void SenseShoot() {
        float rTrig = Input.GetAxis("RightTrigger");
        if (!fullAuto) {
			if (rTrig > .3 && Time.time > nextShootTime && !semiFired) {
				int energyLevel = gui.GetCurrentEnergyPercent();
				if (energyLevel >= energyCost) {
					Fire();
					nextShootTime = Time.time + frequency;
					gui.DecreaseEnergyLevel (energyCost);
					semiFired = true;
				}
			} else if(rTrig < .3){
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
			fireable = true;
			ChangeColor (true);
			Aim ();
		} else if(lTrig < .2){
			if (fireable) {
				fireable = false;
			}
			ChangeColor (false);
		}
		if (fireable) {
			SenseShoot();
		}
	}

	void Aim(){
		aimThreshold = aimThresholdObject.transform.position;

		Vector3 rightDirectionPostion = transform.position + transform.TransformDirection (new Vector3 (aimSideAngle, 0, 0));
		Vector3 leftDirectionPostion = transform.position + transform.TransformDirection (new Vector3 ( -1 * aimSideAngle, 0, 0));

		Vector3 centerDirection = aimThreshold - transform.position;
		centerDirection = new Vector3 (centerDirection.x, 0, centerDirection.z);

		Vector3 rightDirection = aimThreshold - rightDirectionPostion;
		rightDirection = new Vector3 (rightDirection.x, 0, rightDirection.z);

		Vector3 leftDirection = aimThreshold - leftDirectionPostion;
		leftDirection = new Vector3 (leftDirection.x, 0, leftDirection.z);

		Debug.DrawRay (aimThreshold, centerDirection * 8, Color.red);
		Debug.DrawRay (aimThreshold, leftDirection * 7, Color.magenta);
		Debug.DrawRay (aimThreshold, rightDirection * 7, Color.green);

		RaycastHit centerHit;
		RaycastHit rightHit;
		RaycastHit leftHit;

		bool centerDidHit = Physics.Raycast (aimThreshold, centerDirection, out centerHit);
		bool rightDidHit = Physics.Raycast (aimThreshold, rightDirection, out rightHit);
		bool leftDidHit = Physics.Raycast (aimThreshold, leftDirection, out leftHit);

		float centerDistance = centerHit.distance;
		float rightDistance = rightHit.distance;
		float leftDistance = leftHit.distance;

		ArrayList hitDistances = new ArrayList ();

		if (centerDidHit && centerHit.transform.gameObject.tag == "Enemy") {
			hitDistances.Add (centerDistance);
			//print ("Found: " + centerHit.transform.gameObject.name);
		}
		if (rightDidHit && rightHit.transform.gameObject.tag == "Enemy") {
			hitDistances.Add (rightDistance);
		}
		if (leftDidHit && leftHit.transform.gameObject.tag == "Enemy") {
			hitDistances.Add (leftDistance);
		}
		if (hitDistances.Count == 0) {
			return;
		}

		float minDistance = (float)hitDistances [0];
		for (int i = 0; i < hitDistances.Count; i++) {
			float thisDist = (float)hitDistances [i];
			if (thisDist < minDistance) {
				minDistance = thisDist;
			}
		}

		GameObject target;
		if (minDistance == centerDistance) {
			target = centerHit.transform.gameObject;
		} else if (minDistance == rightDistance) {
			target = rightHit.transform.gameObject;
		} else {
			target = leftHit.transform.gameObject;
		}

		print (target.name);
		float step = 1 * Time.deltaTime;
		Vector3 rightDir = Vector3.RotateTowards (rightGun.transform.forward, rightGun.transform.position - target.transform.position, step, 0.0F);
		Vector3 leftDir = Vector3.RotateTowards (leftGun.transform.forward, leftGun.transform.position - target.transform.position, step, 0.0F);

		rightGun.transform.rotation = Quaternion.LookRotation (rightDir);
		leftGun.transform.rotation = Quaternion.LookRotation (leftDir);

	}

    void Fire() {
        Vector3 shootDirectionVelocity1 = leftGun.transform.TransformDirection(shotVelocity) * -1;
		Vector3 shootDirectionVelocity2 = rightGun.transform.TransformDirection(shotVelocity) * -1;

		GameObject instance = (GameObject)Instantiate(shotPrefab, leftGun.transform.position + leftGun.transform.TransformDirection(leftShotSpawnOffset), leftGun.transform.rotation);
		instance.GetComponent<Rigidbody>().AddForce(shootDirectionVelocity1);

		GameObject instance2 = (GameObject)Instantiate(shotPrefab, rightGun.transform.position + rightGun.transform.TransformDirection(rightShotSpawnOffset), rightGun.transform.rotation);
		instance2.GetComponent<Rigidbody>().AddForce(shootDirectionVelocity2);
    }

	void InitializeGunEnergy(){
		gunEnergySpriteArray = new SpriteRenderer[gunEnergyObjectArray.Length];
		for (int i = 0; i < gunEnergyObjectArray.Length; i++) {
			SpriteRenderer gunEnergySprite = gunEnergyObjectArray [i].GetComponent<SpriteRenderer> ();
			gunEnergySpriteArray [i] = gunEnergySprite;
			gunEnergySprite.color = new Color (gunEnergyColor.r, gunEnergyColor.g, gunEnergyColor.b, 0f);
		}
	}

	void ChangeColor (bool aiming){
		if (aiming && alpha < 1) {
			float changeAmount = alphaRate * Time.deltaTime;
			if (alpha + changeAmount >= 1) {
				alpha = 1;
			} else {
				alpha += changeAmount;
			}
			UpdateColors ();
		} else if (!aiming && alpha > 0) {
			float changeAmount = alphaRate * Time.deltaTime;
			if (alpha - changeAmount <= 0) {
				alpha = 0;
			} else {
				alpha -= changeAmount;
			}
			UpdateColors ();
		}
	}

	void UpdateColors(){
		for (int i = 0; i < gunEnergySpriteArray.Length; i++) {
			gunEnergySpriteArray[i].color = new Color (gunEnergyColor.r, gunEnergyColor.g, gunEnergyColor.b, alpha);
		}
	}
}


