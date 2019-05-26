using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Animator))]
public class FacialExpressions : MonoBehaviour {

	public Renderer FaceRenderer;

	private Material faceMaterial;
	private Vector2 uvOffset;
	private Animator animator;

	// Use this for initialization
	void Start () {
		uvOffset = Vector2.zero;
		faceMaterial = FaceRenderer.materials[1];
		animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		// This is hardcoded to set the correct face based on the Animator state
		AnimatorStateInfo animState = animator.GetCurrentAnimatorStateInfo (0);

		if (animState.IsName ("Idle"))
			uvOffset = new Vector2(0, 0);
		else if (animState.IsName ("Happy"))
			uvOffset = new Vector2(0.25f, 0);
		else if (animState.IsName ("Sad"))
			uvOffset = new Vector2(0, -0.25f);
		else
			uvOffset = new Vector2(0.25f, -0.25f);

		faceMaterial.SetTextureOffset ("_MainTex", uvOffset);
	}
}
