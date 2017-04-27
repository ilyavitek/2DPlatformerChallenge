using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {

	#region Editor Variables

	[SerializeField] GameObject m_gameOverPanel;
	[SerializeField] Transform m_enemyParent;
	[SerializeField] GameObject m_enemyPrefab;

	[SerializeField] float m_minTimeForSpawn;
	[SerializeField] float m_maxTimeForSpawn;

	#endregion

	#region Private Variables

	Timer m_enemySpawnTimer;

	#endregion

	#region Behaviour Overrides

	void Start () {
		Time.timeScale = 1f;
		RestartTimer ();
	}

	void Update () {
		if (m_enemySpawnTimer.Launch ()) {
			m_enemyPrefab.Spawn (m_enemyParent).GetComponent<EnemyControl> ().Reset ();
			RestartTimer ();
		}
	}

	#endregion

	#region Public Methods

	public void GameOver () {
		Time.timeScale = 0f;
		m_gameOverPanel.SetActive (true);


		if (PlayerPrefs.HasKey (StaticManager.HIGHSCORE_KEY)) {
			float bestTime = PlayerPrefs.GetFloat (StaticManager.HIGHSCORE_KEY);
			if (Time.timeSinceLevelLoad > bestTime) {
				PlayerPrefs.SetFloat (StaticManager.HIGHSCORE_KEY, Time.timeSinceLevelLoad);
			}
		}
	}

	public void RestartScene () {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	#endregion

	#region Private Methods

	void RestartTimer () {
		m_enemySpawnTimer = new Timer (Random.Range (m_minTimeForSpawn, m_maxTimeForSpawn));
		m_enemySpawnTimer.Play ();
	}

	#endregion
}
