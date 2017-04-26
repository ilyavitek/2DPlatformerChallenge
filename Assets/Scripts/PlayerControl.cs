using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	#region Editor Variables

	[SerializeField] GameControl m_gameControl;

	[SerializeField] Transform m_bulletParent;
	[SerializeField] GameObject m_bulletPrefab;

	[SerializeField] TilesControl m_tiles;

	[SerializeField] float m_maxYPos;
	[SerializeField] float m_maxXPos;

	[SerializeField] float m_verticalSpeed;
	[SerializeField] float m_horizontalSpeed;

	[Range (0.1f, 2f)]
	[SerializeField] float m_maxHorizontalSpeedCoeff;
	[Range (0.1f, 2f)]
	[SerializeField] float m_minHorizontalSpeedCoeff;

	#endregion

	#region Public Properties

	public float EnvironmentSpeed {
		get {
			return m_environmentSpeed;
		}
	}

	#endregion

	#region Private Variables

	Vector3 m_startPos;

	float m_environmentSpeed;

	BulletControl m_bullet;

	#endregion

	#region Behaviour Overrides

	void Start () {
		m_startPos = transform.position;
		m_environmentSpeed = m_minHorizontalSpeedCoeff;

		RefreshBullet ();
	}

	void Update () {
		SetVerticalPosition ();
		SetHorizontalPosition ();

		if (Input.GetButtonDown ("Jump")) {
			m_bullet.transform.SetParent (m_bulletParent);
			m_bullet.m_isOn = true;
			RefreshBullet ();
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		m_gameControl.GameOver ();
	}

	#endregion

	#region Private Methods

	void RefreshBullet () {
		m_bullet = m_bulletPrefab.Spawn (transform).GetComponent<BulletControl> ();
	}

	void SetVerticalPosition () {
		float verticalAxis = Input.GetAxis ("Vertical");

		if (verticalAxis != 0) {
			transform.position = Vector3.Lerp (transform.position, m_startPos + Vector3.up * m_maxYPos * verticalAxis, Time.deltaTime * m_verticalSpeed);
		}
	}

	void SetHorizontalPosition () {
		float horizontalAxis = Input.GetAxis ("Horizontal");

		if (horizontalAxis != 0) {
			m_environmentSpeed = Mathf.Clamp (m_minHorizontalSpeedCoeff + (m_maxHorizontalSpeedCoeff - m_minHorizontalSpeedCoeff) * horizontalAxis * m_horizontalSpeed, m_minHorizontalSpeedCoeff, m_maxHorizontalSpeedCoeff);
			m_tiles.SpeedCoeff = m_environmentSpeed;
		}
	}

	#endregion
}
