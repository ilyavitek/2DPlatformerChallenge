using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {

	#region Editor Variables

	[SerializeField] float m_startPointX;
	[SerializeField] float m_endPointX;

	[SerializeField] float m_maxDeltaY;

	[SerializeField] float m_speedMin;
	[SerializeField] float m_speedMax;

	[SerializeField] int m_lifesMax;

	#endregion

	#region Private Variables

	float m_speed;
	int m_lifes;

	PlayerControl m_playerControl;

	#endregion

	#region Behaviour Overrides

	void Start () {
		Reset ();
		m_playerControl = FindObjectOfType<PlayerControl> ();
	}

	void Update () {
		if (transform.position.x > m_endPointX) {
			transform.position -= Vector3.right * m_speed * m_playerControl.EnvironmentSpeed * Time.deltaTime;
		} else {
			Reset ();
			gameObject.Recycle ();
		}
	}

	#endregion

	#region Public Methods

	public void Reset () {
		transform.position = new Vector3 (m_startPointX, Random.Range (-m_maxDeltaY, m_maxDeltaY), 0f);
		m_speed = Random.Range (m_speedMin, m_speedMax);
		m_lifes = Random.Range (1, m_lifesMax + 1);
		transform.localScale = new Vector3 (0.5f, 2 * m_lifes, 1f);
	}

	public void Hit () {
		if (m_lifes > 1) {
			m_lifes--;
		} else {
			Reset ();
			gameObject.Recycle ();
		}
	}

	#endregion
}
