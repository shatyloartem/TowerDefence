using UnityEngine;

namespace TD.Components
{
    public class HorizontalLayoutGroupFixed : MonoBehaviour
    {
        public float spacing;

        private RectTransform Transform;

        private void OnEnable()
        {
            if(Transform == null)
                Transform = GetComponent<RectTransform>();
        }

        public void UpdateChildren()
        {
            int i = 0;
            RectTransform prevChild = null;
            foreach (RectTransform child in Transform)
            {
                if (i == 0)
                {
                    child.transform.localPosition = new Vector2((-Transform.rect.width / 2) + (child.rect.width / 2), 0);
                    prevChild = child;
                    i++;
                    continue;
                }

                child.transform.localPosition = new Vector2(prevChild.localPosition.x + (prevChild.rect.width / 2) + spacing + (child.rect.width / 2), 0);

                prevChild = child;
                i++;
            }
        }
    }
}
