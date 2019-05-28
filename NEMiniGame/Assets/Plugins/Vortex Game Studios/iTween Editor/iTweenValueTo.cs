// Module Name:		iTweenValueTo.cs
// Project:			iTween Editor br Vortex Game Studios
// Version:			1.00.00
// Developed by:	Alexandre Ribeiro de Sá (@themonkeytail)
// Copyright(c) Vortex Game Studios LTDA ME.
// http://www.vortexstudios.com
// 
// iTween Value To component
// Use this component to create custom value transitions with the
// OnUpdate event.
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
using UnityEngine.Events;
using System;

public class iTweenValueTo : iTweenEditor {
	public float valueFrom = 0.0f;
	public float valueTo = 1.0f;

	[System.Serializable]
	public class OnStart : UnityEvent { };
	public OnStart onStart;

	[System.Serializable]
	public class OnUpdate : UnityEvent<float> { };
	public OnUpdate onUpdate;

	[System.Serializable]
	public class OnComplete : UnityEvent { };
	public OnComplete onComplete;

	// Use this for initialization
	void Awake() {
		if ( this.autoPlay )
			this.iTweenPlay();
	}

	public override void iTweenPlay() {
		Hashtable ht = new Hashtable();
		ht.Add( "from", this.valueFrom );
		ht.Add( "to", this.valueTo );
		ht.Add( "time", this.tweenTime );
		ht.Add( "delay", this.waitTime );

		ht.Add( "looptype", this.loopType );
		ht.Add( "easetype", this.easeType );

		ht.Add( "onstart", (Action<object>)( newVal => {
			if ( onStart != null ) {
				onStart.Invoke();
			}

			if ( onUpdate != null ) {
				onUpdate.Invoke( this.valueFrom );
			}
		} ) );
		ht.Add( "onupdate", (Action<object>)( newVal => {
			if ( onUpdate != null ) {
				onUpdate.Invoke( (float)newVal );
			}
		} ) );
		ht.Add( "oncomplete", (Action<object>)( newVal => {
			if ( onComplete != null ) {
				onComplete.Invoke();
			}
		} ) );
		ht.Add( "ignoretimescale", ignoreTimescale );

		iTween.ValueTo( this.gameObject, ht );
	}
}