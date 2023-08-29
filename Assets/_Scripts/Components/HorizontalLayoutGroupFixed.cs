using GluonGui.WorkspaceWindow.Views.WorkspaceExplorer.Explorer.Operations;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Components
{
    [ExecuteAlways]
    public class HorizontalLayoutGroupFixed : MonoBehaviour
    {
        [SerializeField] private bool executeAlways;

        public float spacing;

        private RectTransform Transform;

        private void OnEnable()
        {
            if(Transform == null)
                Transform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            /*if(executeAlways)
                UpdateChildren();*/
        }

        public void UpdateChildren(RectTransform[] children)
        {
            int i = 0;
            RectTransform prevChild = null;
            foreach (RectTransform child in children)
            {
                if (i == 0)
                {
                    Vector2 pos = new Vector2((-Transform.rect.width / 2) + (child.rect.width / 2), 0);
                    child.transform.localPosition = pos;
                    prevChild = child;
                    i++;
                    continue;
                }

                Vector2 newPos = new Vector2(prevChild.localPosition.x + (prevChild.rect.width / 2) + spacing + (child.rect.width / 2), 0);
                child.transform.localPosition = newPos;

                prevChild = child;
                i++;
            }
        }

        public void UpdateChildren(List<GameObject> children)
        {
            List<RectTransform> newArr = new List<RectTransform>();
            foreach(GameObject child in children)
                newArr.Add(child.GetComponent<RectTransform>());

            UpdateChildren(newArr.ToArray());
        }
    }
}
