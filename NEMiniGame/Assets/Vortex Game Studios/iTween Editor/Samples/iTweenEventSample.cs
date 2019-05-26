using UnityEngine;
using System.Collections;

public class iTweenEventSample : MonoBehaviour {

	public void OnStartEvent() {
		Debug.Log( "Tween Started!" );
	}

	public void OnCompleteEvent() {
		Debug.Log( "Tween Completed!" );
	}

}
