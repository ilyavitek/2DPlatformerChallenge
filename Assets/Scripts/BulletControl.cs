using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour {

	#region Public Variables

	public float m_speed;
	public bool m_isOn;

	public Transform m_player;

	#endregion

	#region Behaviour Overrides

	void Update () {
		if (m_isOn) {
			transform.position += Vector3.right * m_speed * Time.deltaTime;
		}

		if (transform.position.x >= 10) {
			m_isOn = false;
			gameObject.Recycle ();
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == StaticManager.ENEMY_TAG) {
			other.GetComponent<EnemyControl> ().Hit ();
		}

		m_isOn = false;
		gameObject.Recycle ();
	}

	#endregion
}
