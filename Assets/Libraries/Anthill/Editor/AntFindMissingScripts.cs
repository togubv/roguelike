namespace Anthill.Editor
{
	using UnityEngine;
	using UnityEditor;
	using System.Collections.Generic;

	public class FindMissingScripts : EditorWindow 
	{
		private static int _goCount = 0;
		private static int _componentsCount = 0;
		private static int _missingCount = 0;
		private static bool _hasResult;

		private static List<GameObject> _resultList;
	
		[MenuItem("Tools/Anthill/Find Missing Scripts")]
		public static void ShowWindow()
		{
			EditorWindow.GetWindow(typeof(FindMissingScripts), true, "Find Missing Scripts");
		}
	
		public void OnGUI()
		{
			if (_resultList != null && _resultList.Count > 0)
			{
				EditorGUILayout.LabelField("Searched", EditorStyles.largeLabel);
				EditorGUI.indentLevel++;
				EditorGUILayout.LabelField($"GameObjects: {_goCount}");
				EditorGUILayout.LabelField($"Components: {_componentsCount}");
				EditorGUILayout.LabelField($"Missing: {_missingCount}");
				EditorGUI.indentLevel--;
				GUILayout.Space(5.0f);

				EditorGUILayout.HelpBox("Found objects with missing scripts!", MessageType.Warning);

				GUILayout.Space(5.0f);
				EditorGUILayout.LabelField("Missing List", EditorStyles.largeLabel);
				
				EditorGUI.indentLevel++;
				for (int i = 0, n = _resultList.Count; i < n; i++)
				{
					EditorGUILayout.BeginHorizontal();
					{
						GUILayout.Space(10.0f);
						EditorGUILayout.ObjectField(_resultList[i], typeof(GameObject), false);
					}
					EditorGUILayout.EndHorizontal();
				}
				EditorGUI.indentLevel--;
			}
			else if (_hasResult)
			{
				EditorGUILayout.HelpBox("Everything looks good!", MessageType.Info);
			}
			else
			{
				EditorGUILayout.HelpBox("Select all or few objects on the scene and press button below.", MessageType.Info);
			}

			GUILayout.Space(10.0f);
			if (GUILayout.Button("Find Missing Scripts in selected GameObjects"))
			{
				FindInSelected();
				_hasResult = true;
			}

			if (_hasResult && GUILayout.Button("Reset Result"))
			{
				_hasResult = false;
				_resultList.Clear();
			}
		}

		private static void FindInSelected()
		{
			GameObject[] go = Selection.gameObjects;
			_goCount = 0;
			_componentsCount = 0;
			_missingCount = 0;
			_resultList = new List<GameObject>();

			foreach (GameObject g in go)
			{
				FindInGO(g);
			}
		}
	
		private static void FindInGO(GameObject g)
		{
			_goCount++;
			Component[] components = g.GetComponents<Component>();
			for (int i = 0; i < components.Length; i++)
			{
				_componentsCount++;
				if (components[i] == null)
				{
					_missingCount++;
					string s = g.name;
					Transform t = g.transform;
					while (t.parent != null) 
					{
						s = t.parent.name +"/"+s;
						t = t.parent;
					}

					_resultList.Add(g);
				}
			}

			// Now recurse through each child GO (if there are any):
			foreach (Transform childT in g.transform)
			{
				FindInGO(childT.gameObject);
			}
		}
	}
}