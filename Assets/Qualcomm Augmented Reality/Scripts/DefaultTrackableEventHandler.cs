/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Qualcomm Connected Experiences, Inc.
==============================================================================*/

using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface.
/// </summary>
public class DefaultTrackableEventHandler : MonoBehaviour,
                                            ITrackableEventHandler
{
    #region PRIVATE_MEMBER_VARIABLES
 
	private TrackableBehaviour mTrackableBehaviour;
	
	List<AudioSource> SorgentiAudio;
	VideoPlaybackBehaviour Video;
	bool PrimoTrakingFound = true;
    
    #endregion // PRIVATE_MEMBER_VARIABLES



    #region UNTIY_MONOBEHAVIOUR_METHODS
    
    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    #endregion // UNTIY_MONOBEHAVIOUR_METHODS



    #region PUBLIC_METHODS

    /// <summary>
    /// Implementation of the ITrackableEventHandler function called when the
    /// tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
        }
        else
        {
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS



    #region PRIVATE_METHODS


    private void OnTrackingFound()
    {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        // Enable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = true;
        }

        // Enable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = true;
        }

		if (PrimoTrakingFound) {
			SorgentiAudio = new List<AudioSource>(GetComponents<AudioSource> ());
			SorgentiAudio.AddRange(GameObject.Find("Fontana").GetComponents<AudioSource>());
			SorgentiAudio.AddRange(GameObject.Find("SuonoAmbiente1").GetComponents<AudioSource>());
			SorgentiAudio.AddRange(GameObject.Find("SuonoAmbiente2").GetComponents<AudioSource>());
			SorgentiAudio.AddRange(GameObject.Find("SuonoAmbiente3").GetComponents<AudioSource>());
			SorgentiAudio.AddRange(GameObject.Find("SuonoAmbiente4").GetComponents<AudioSource>());
			SorgentiAudio.AddRange(GameObject.Find("Sottofondo").GetComponents<AudioSource>());
			
			Video = (VideoPlaybackBehaviour)FindObjectOfType(typeof(VideoPlaybackBehaviour));
			
			PrimoTrakingFound = false;
		}
		
		if (mTrackableBehaviour.TrackableName == "Cattivissimo_Me_2")
						Video.VideoPlayer.Play (false, Video.VideoPlayer.GetCurrentPosition ());
		
		if (mTrackableBehaviour.TrackableName == "Taj_Mahal")
			foreach (AudioSource Audio in SorgentiAudio)
				Audio.Play ();
		
		Debug.Log ("--------------------------"+mTrackableBehaviour.TrackableName+"--------------------------");
		

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
    }


    private void OnTrackingLost()
    {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        // Disable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = false;
        }

        // Disable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = false;
        }

		if (mTrackableBehaviour.TrackableName == "Cattivissimo_Me_2")
						Video.VideoPlayer.Pause ();
		
		if(mTrackableBehaviour.TrackableName == "Taj_Mahal")
			foreach (AudioSource Audio in SorgentiAudio)
				Audio.Pause ();

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
    }

    #endregion // PRIVATE_METHODS
}
