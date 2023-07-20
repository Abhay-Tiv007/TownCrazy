using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

// attach to UI Text component (with the full text already there)

public class TypeWriterEffect : MonoBehaviour
{
	public UnityEvent startTrigger;
	public UnityEvent afterTextFade;

	private TextMeshProUGUI txt;
	private string story;

	[SerializeField]
	private float delay = 0.125f;
	[SerializeField]
	private float fadeDecay = 0.5f;
	[SerializeField]
	private bool hasExit = true;
	[SerializeField]
	private bool fadeEnd = true;
	private bool fullText = false;
	private bool isFaded = false;
	private float elapsed = 0.0f;

	void Awake()
	{
		txt = GetComponent<TextMeshProUGUI>();
		story = txt.text;
		txt.text = "";

		startTrigger.Invoke();

		// TODO: add optional delay when to start
		StartCoroutine("PlayText");
	}

    private void Update()
    {
		if (isFaded)
			return;

		if (!fullText && hasExit && Input.GetButtonUp("Submit"))
		{
			txt.text = story;
			fullText = true;
		}else if (!isFaded && fullText && hasExit && Input.GetButtonUp("Submit"))
        {
			if (fadeEnd) FadeOutText();
		}
	}

    IEnumerator PlayText()
	{
		foreach (char c in story)
		{
			if (fullText)
				break;
			txt.text += c;
			yield return new WaitForSeconds(Random.Range(delay/2, delay));
		}
		fullText = true;
		if(!isFaded && fadeEnd && !hasExit) FadeOutText();
	}

	IEnumerator FadeOutText(float duration)
	{
		

		while(elapsed <= duration)
        {
			elapsed += Time.deltaTime;
			txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, (duration - elapsed) / duration);
			yield return null;
		}
		txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, 0);
		yield return new WaitForSeconds(0.5f);
		isFaded = true;
		afterTextFade.Invoke();
		//Destroy(gameObject, duration);
		yield return true;
	}

	public bool isDone()
    {
		return fullText;
    }

	public void FadeOutText()
    {
		StartCoroutine(FadeOutText(fadeDecay));
    }

}