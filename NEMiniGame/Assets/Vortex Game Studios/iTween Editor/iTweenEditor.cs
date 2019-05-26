// Module Name:		iTweenEditor.cs
// Project:			iTween Editor br Vortex Game Studios
// Version:			1.00.00
// Developed by:	Alexandre Ribeiro de Sá (@themonkeytail)
// Copyright(c) Vortex Game Studios LTDA ME.
// http://www.vortexstudios.com
// 
// iTween Editor To component
// Base component.
// 1.00.00 - First build
// 
// Check every tools and plugins we made for Unity at
// https://www.assetstore.unity3d.com/en/publisher/4888
// 
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.


using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class iTweenEditor : MonoBehaviour {
	public string name = "";
	public bool autoPlay = true;

	public float waitTime = 0.25f;
	public float tweenTime = 2.0f;

	public iTween.LoopType loopType = iTween.LoopType.none;
	public iTween.EaseType easeType = iTween.EaseType.linear;

	public bool ignoreTimescale = true;

	public virtual void iTweenPlay() { }

	public void LoadLevel( string screenName ) {
		Application.LoadLevel( screenName );
	}

	public void LoadLevelAdditive( string screenName ) {
		Application.LoadLevelAdditive( screenName );
	}

	public void EnableGameObject( GameObject go ) {
		go.SetActive( true );
	}

	public void DisableGameObject( GameObject go ) {
		go.SetActive( false );
	}

	public void DestroyGameObject( GameObject go ) {
		Destroy( go );
	}

	public void EnableMonoBehaviour( MonoBehaviour mb ) {
		mb.enabled = true;
	}

	public void DisableMonoBehaviour( MonoBehaviour mb ) {
		mb.enabled = false;
	}

	public void DestroyObject( Object obj) {
		Destroy( obj );
	}

	public void PlayTween( iTweenEditor tween ) {
		tween.iTweenPlay();
	} 
}
