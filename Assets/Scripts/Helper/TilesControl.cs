using UnityEngine;
using System.Collections;

public class TilesControl : MonoBehaviour {

	#region Editor Variables

	[SerializeField] float m_speedCoeff;

	[SerializeField] GameObject[] m_objectsToTurnOn;

	#endregion

	#region Public Properties

	public float SpeedCoeff {
		get {
			return m_speedCoeff;
		}
		set {
			m_speedCoeff = value;
		}
	}

	#endregion

	#region Private Variables

	Animator m_animator;

	#endregion

	#region Behaviour Overrides

	void Start () {
		m_animator = GetComponent<Animator> ();		
	}

	void Update () {
		m_animator.speed = m_speedCoeff;
	}

	#endregion

	#region Public Methods

	public void ChangeOfTiles () {
		// something to happen
	}

	#endregion
}