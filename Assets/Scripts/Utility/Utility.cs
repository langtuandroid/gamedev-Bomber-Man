using UnityEngine;

public class Utility
{
	public static void SendToClipboard(string text)
	{
		TextEditor textEditor = new TextEditor();
		textEditor.content = new GUIContent(text);
		textEditor.SelectAll();
		textEditor.Copy();
	}

	public static string ReceiveFromClipboard()
	{
		TextEditor textEditor = new TextEditor();
		textEditor.content = new GUIContent(string.Empty);
		textEditor.SelectAll();
		textEditor.Paste();
		return textEditor.content.text;
	}
}
