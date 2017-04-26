using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeControl : MonoBehaviour {

	#region Editor Variables

	[SerializeField] Text m_bestTimeText;

	#endregion

	#region Private Variables

	Text m_currentTimeText;

	#endregion

	#region Behaviour Overrides

	void Start () {
		m_currentTimeText = GetComponent<Text> ();
		if (PlayerPrefs.HasKey (StaticManager.HIGHSCORE_KEY)) {
			float bestTime = PlayerPrefs.GetFloat (StaticManager.HIGHSCORE_KEY);
			ShowTime (m_bestTimeText, bestTime);
		}
	}

	void Update () {
		ShowTime (m_currentTimeText, Time.timeSinceLevelLoad);
	}

	#endregion

	#region Private Methods

	void ShowTime (Text t, float time) {
		int secs = (int)(time) % 60;
		int milisec = (int)(((time) * 100) % 6000) - secs * 100;

		t.text = string.Format ("{0:00}:{1:00}", secs, milisec);
	}

	#endregion
}
