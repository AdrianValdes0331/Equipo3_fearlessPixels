using MEC;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Custom.Indicators
{
    [RequireComponent(typeof(Canvas))]
    public class OffscreenIndicators : MonoBehaviour
    {
        public Camera activeCamera;
        public List<Indicator> targetIndicators;
        public GameObject indicatorPrefabP1, indicatorPrefabP2, indicatorPrefabP3, indicatorPrefabP4;
        public float checkTime = 0.1f;
        public Vector2 offset;

        private Transform _transform;
        private List<GameObject> players = new List<GameObject>();

        // Start is called before the first frame update
        void Start()
        {
            _transform = transform;
            Timing.RunCoroutine(WaitForPlayers());
            InstantiateIndicators();
            Timing.RunCoroutine(UpdateIndicators().CancelWith(gameObject));
        }

        private void InstantiateIndicators()
        {
            var count = 0;
            foreach (var targetIndicator in targetIndicators)
            {
                count ++;
                if (targetIndicator.indicatorUI == null)
                {
                    switch (count)
                    {
                        case 1:
                            targetIndicator.indicatorUI = Instantiate(indicatorPrefabP1).transform;
                            break;
                        case 2:
                            targetIndicator.indicatorUI = Instantiate(indicatorPrefabP2).transform;
                            break;
                        case 3:
                            targetIndicator.indicatorUI = Instantiate(indicatorPrefabP3).transform;
                            break;
                        case 4:
                            targetIndicator.indicatorUI = Instantiate(indicatorPrefabP4).transform;
                            break;
                    }
                    targetIndicator.indicatorUI.SetParent(_transform);
                }

                var rectTransform = targetIndicator.indicatorUI.GetComponent<RectTransform>();
                if (rectTransform == null)
                {
                    rectTransform = targetIndicator.indicatorUI.gameObject.AddComponent<RectTransform>();
                }

                targetIndicator.rectTransform = rectTransform;
            }
        }

        private void UpdatePosition(Indicator targetIndicator)
        {
            var rect = targetIndicator.rectTransform.rect;
            var indicatorPosition = activeCamera.WorldToScreenPoint(targetIndicator.target.position);

            if (indicatorPosition.z < 0)
            {
                indicatorPosition.y = -indicatorPosition.y;
                indicatorPosition.x = -indicatorPosition.x;
            }

            var newPosition = new Vector3(indicatorPosition.x, indicatorPosition.y, indicatorPosition.z);

            indicatorPosition.x = Mathf.Clamp(indicatorPosition.x, rect.width / 2, Screen.width - rect.width / 2) + offset.x;
            indicatorPosition.y = Mathf.Clamp(indicatorPosition.y, rect.height / 2, Screen.height - rect.height / 2) + offset.y;
            indicatorPosition.z = 0;

            targetIndicator.indicatorUI.up = (newPosition - indicatorPosition).normalized;
            targetIndicator.indicatorUI.position = indicatorPosition;
        }

        private IEnumerator<float> UpdateIndicators()
        {
            while (true)
            {
                for (int i = 0; i < players.Count; i ++)
                {
                    if (players[i])
                    {
                        UpdatePosition(targetIndicators[i]);
                    }
                    else
                    {
                        if (targetIndicators[i] != null)
                        {
                            targetIndicators[i].indicatorUI.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
                            targetIndicators[i] = null;
                        }
                    }
                }
                yield return Timing.WaitForSeconds(checkTime);
            }
        }

        private IEnumerator<float> WaitForPlayers()
        {
            yield return Timing.WaitForSeconds(0.205f);
            players.AddRange(GameObject.FindGameObjectsWithTag("Player"));
            foreach (var player in players)
            {
                targetIndicators.Add(new Indicator()
                {
                    target = player.transform
                });
            }
            InstantiateIndicators();
        }
    }

    [System.Serializable]
    public class Indicator
    {
        public Transform target;
        public Transform indicatorUI;
        [HideInInspector]
        public RectTransform rectTransform;
    }
}
