using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIWorkflow.Events.Editor
{
    static class Tools
    {
        [MenuItem("Tools/UI Workflow/Add cullings for all TMPGUI")]
        private static void AddCullToAllTMP()
        {
            var objects = FindAllObjectsOfTypeExpensive<TMP_Text>().ToArray();

            foreach (var text in objects)
            {
                var value = text.GetComponent<UIEventReceiver>();

                if (value == null)
                {
                    value = text.gameObject.AddComponent<UIEventReceiver>();
                    EditorUtility.SetDirty(text.gameObject);
                }
                var value1 = text.GetComponent<TMP_Culling>();

                if (value1 == null)
                {
                    value1 = text.gameObject.AddComponent<TMP_Culling>();
                    EditorUtility.SetDirty(text.gameObject);
                }
            }
        }
        
        public static IEnumerable<GameObject> GetAllRootGameObjects()
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                GameObject[] rootObjs = SceneManager.GetSceneAt(i).GetRootGameObjects();
                foreach (GameObject obj in rootObjs)
                    yield return obj;
            }
        }
 
        public static IEnumerable<T> FindAllObjectsOfTypeExpensive<T>()
            where T : MonoBehaviour
        {
            foreach (GameObject obj in GetAllRootGameObjects())
            {
                foreach (T child in obj.GetComponentsInChildren<T>(true))
                    yield return child;
            }
        }
    }
}