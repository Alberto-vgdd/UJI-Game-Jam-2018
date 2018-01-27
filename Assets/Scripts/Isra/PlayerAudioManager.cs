using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour {

	public AudioClip[] m_PlayerAudioClips;
	public AudioSource m_PlayerJumpAudioSource;
	public AudioSource m_PlayerDoubleJumpAudioSource;
	public AudioSource m_PlayerDeathAudioSource;
	public AudioSource m_PlayerScreamAudioSource;


	public void PlayJumpSound(){
		m_PlayerJumpAudioSource.Play();
	}

	public void PlayDoubleJumpSound(){
		m_PlayerDoubleJumpAudioSource.Play();
	}

	public void PlayDeathSound(){
		m_PlayerDeathAudioSource.Play();
	}

	public void PlayScreamSound(){
		m_PlayerScreamAudioSource.Play();
	}



}
