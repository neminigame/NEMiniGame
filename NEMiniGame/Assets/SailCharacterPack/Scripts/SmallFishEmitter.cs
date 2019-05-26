using UnityEngine;
using System.Collections;

public class SmallFishEmitter : MonoBehaviour {

	public GameObject SmallFishPrefab;

	private float timer;

	// Use this for initialization
	void Start () {
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer < 0)
		{
			GameObject fishInstance = (GameObject)
			GameObject.Instantiate (SmallFishPrefab, gameObject.transform.position, Quaternion.identity);
			timer = 0.25f;

			Vector3 actualForce;
			actualForce.x = Random.Range (-2f, 2f);
			actualForce.y = Random.Range (5f, 8f);
			actualForce.z = Random.Range (-2f, 2f);
			Vector3 actualTorque;
			actualTorque.x = Random.Range (-2, 2);
			actualTorque.y = Random.Range (-2, 2);
			actualTorque.z = Random.Range (-2, 2);
			
			fishInstance.GetComponent<Rigidbody>().AddRelativeForce (actualForce, ForceMode.Impulse);
			fishInstance.GetComponent<Rigidbody>().AddRelativeTorque (actualTorque, ForceMode.Impulse);

			Destroy (fishInstance, 4);
		}
	}
}
