using DG.Tweening;
using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace VuTienDat
{
    public class DragController_Baking : MonoBehaviour
    {
        [Header("Drag")]
        [SerializeField] private LayerMask layerItem;
        public bool isDragging = false;
        public Vector3 lastPos;
        private GameObject itemParent, itemChild;
        private Camera cam;

        public int indexWvae = 1;
        [Header("Wave_1")]
        [SerializeField] private GameObject wave_1;
        [SerializeField] private List<SpriteRenderer> listSpriteWave_1;
        [SerializeField] private List<GameObject> itemWvae_1;
        [Header("Wave_2")]
        [SerializeField] private GameObject wave_2;
        [SerializeField] private List<SpriteRenderer> listSpriteWave_2;
        [SerializeField] private List<GameObject> itemWvae_2, imgWave_2;
        [SerializeField] private SpriteRenderer imgMix;
        [SerializeField] private CircleCollider2D box;
        public int indexMisson = 0;

        [Header("Wave_3")]
        [SerializeField] private GameObject wave_3;
        [SerializeField] private List<SpriteRenderer> listSpriteWave_3;
        [SerializeField] private GameObject item_icing_bag, cake;
        [SerializeField] private List<GameObject> listCake;
        private int indexCake = 0;
        [SerializeField] private GameObject potLid, potLidPos;
        [SerializeField] private List<Sprite> listSpriteCake;

        [Header("Hint")]
        public GameObject emojiLike;
        private bool isShowDone = false;
        public GameObject hint;
        public CanvasGroup cvHint;
        public Button btnContinue;
        private bool isPause;

        public static DragController_Baking instance;
        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            cam = Camera.main;
            btnContinue.onClick.RemoveAllListeners();
            btnContinue.onClick.AddListener(CloseHint);
            StartCoroutine(delay());
        }

        private void Update()
        {
            isPause = GameManager_Baking.instance.IsGamePause();
           /* if (isPause)
            {
                PopupManager.ShowToast("Lose");
            }*/
            if (Input.GetMouseButtonDown(0) && Input.touchCount == 1 && !isPause)
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward, Mathf.Infinity, layerItem);
                if (hit.collider != null)
                {
                    isDragging = true;
                    itemParent = hit.collider.gameObject;
                    itemChild = itemParent.transform.GetChild(0).gameObject;
                    lastPos = itemParent.transform.position;
                    MouseDown(1.2f);
                    if (itemParent.GetComponent<TagGameObject>() != null)
                    {
                        TagGameObject tag = itemParent.GetComponent<TagGameObject>();
                        if (tag.tagValue == "Item" || tag.tagValue == "Lock" || tag.tagValue == "Cake")
                        {
                            itemParent.GetComponent<Item_Baking>().Dragging();
                        }
                    }
                }

            }
            if (Input.GetMouseButtonUp(0))
            {
                if (itemParent != null)
                {
                    MouseUp();
                   
                    if (itemParent.GetComponent<TagGameObject>() != null)
                    {
                        TagGameObject tag = itemParent.GetComponent<TagGameObject>();
                        if (tag.tagValue != "Item" && tag.tagValue != "Lock" && tag.tagValue != "Cake")
                        {
                            itemParent.transform.DOMove(lastPos, 0.15f).OnComplete(() =>
                            {
                                itemParent = null;
                            });
                        }
                    }


                    isDragging = false;
                    if (indexMisson < 6 && indexWvae == 2)
                    {
                        if (itemParent == itemWvae_2[indexMisson].gameObject)
                        {
                            box.enabled = true;
                        }
                        else if (!isDragging)
                        {
                            box.enabled = false;
                            itemParent.transform.DOMove(lastPos, 0.3f);
                        }
                    }
                }
            }
            if (isDragging && itemParent != null)
            {
                Vector3 newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
                itemParent.transform.position = new Vector3(newPosition.x, newPosition.y, 0);
            }
            if (itemWvae_1.Count == 0 && indexWvae == 1)
            {
                indexWvae++;
                ShowDone();
                StartCoroutine(NextWave_2());
                StartCoroutine(setActive(wave_1, false));
            }
            if (indexMisson == 6 && indexWvae == 2)
            {
                ShowDone();
                indexWvae++;
                StartCoroutine(NextWave_3());
                StartCoroutine(setActive(wave_2, false));
            }
            if (indexCake == 6 && potLid.GetComponent<BoxCollider2D>().enabled == false)
            {
                indexCake++;
                ShowDone();
                potLid.GetComponent<BoxCollider2D>().enabled = true;
            }
            if (listCake.Count == 0 && indexWvae == 3)
            {
                Debug.Log("------------- WIN -------------");
                ShowDone();


                PopupManager.ShowToast("Winnnnn");

                GameManager_Baking.instance.setIsGamePause(true);

                indexWvae++;
            }
        }
        private void MouseDown(float scale)
        {
            itemParent.transform.DORotate(Vector3.zero, 0.15f);
            itemParent.transform.DOScale(scale, 0.15f);
            SpriteRenderer spriteRe = itemChild.GetComponent<SpriteRenderer>();
            spriteRe.sortingOrder = 10;
        }
        private void MouseUp()
        {
            TagGameObject tag = itemParent.GetComponent<TagGameObject>();
            if (tag != null && tag.tagValue == "Pos")
            {
                itemParent.transform.DOMove(lastPos, 0.15f);
            }
            itemParent.transform.DOScale(1, 0.15f);
            SpriteRenderer spriteRe = itemChild.transform.GetComponent<SpriteRenderer>();
            spriteRe.sortingOrder = 10;
        }
        public void setNullItem()
        {
            itemParent = null;
        }
        public void RemoveItem(GameObject gameObject)
        {
            itemWvae_1.Remove(gameObject);
        }
        IEnumerator NextWave_2()
        {
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < listSpriteWave_1.Count; i++)
            {
                listSpriteWave_1[i].DOFade(0, 0.5f);
            }
            for (int i = 0; i < listSpriteWave_2.Count; i++)
            {
                listSpriteWave_2[i].DOFade(1, 0.5f);
            }
            for (int i = 0; i < itemWvae_2.Count; i++)
            {
                itemWvae_2[i].GetComponent<BoxCollider2D>().enabled = true;
            }
        }
        IEnumerator NextWave_3()
        {
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < listSpriteWave_2.Count; i++)
            {
                listSpriteWave_2[i].DOFade(0, 0.5f);
            }
            for (int i = 0; i < itemWvae_2.Count; i++)
            {
                itemWvae_2[i].GetComponent<BoxCollider2D>().enabled = false;
            }
            box.enabled = false;
            imgMix.DOFade(0, 0.5f);
            for (int i = 0; i < listSpriteWave_3.Count; i++)
            {
                listSpriteWave_3[i].DOFade(1, 0.5f);
            }
            foreach (GameObject cake in listCake)
            {
                cake.GetComponent<BoxCollider2D>().enabled = true;
            }
            item_icing_bag.GetComponent<PolygonCollider2D>().enabled = true;
            cake.SetActive(true);
        }

        public void SetIndexCake()
        {
            indexCake++;
        }
        public void RemoveCake(GameObject cake)
        {
            listCake.Remove(cake);
        }
        public void ChangeSpriteCake()
        {
            for (int i = 0; i < listCake.Count; i++)
            {
                if (i % 2 == 0)
                {
                    listCake[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = listSpriteCake[0];
                }
                else
                {
                    listCake[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = listSpriteCake[1];
                }
                listCake[i].layer = 1;
            }
            StartCoroutine(Baking());
        }
        IEnumerator Baking()
        {
            yield return new WaitForSeconds(2f);
            potLid.transform.DOMoveY(-3.37f, 0.15f).OnComplete(() =>
            {
                for (int i = 0; i < listCake.Count; i++)
                {
                    listCake[i].GetComponent<BoxCollider2D>().enabled = true;
                }
            });
        }
        IEnumerator setActive(GameObject gameObject, bool active)
        {
            yield return new WaitForSeconds(2);
            gameObject.SetActive(active);
        }

        // Tool 
        public void FillMilk()
        {
            imgWave_2[0].transform.DOScale(1, 1f).SetDelay(0.3f);
        }
        public void ShowDone()
        {
            emojiLike.transform.DOScale(1, 0.2f).From(0).SetEase(Ease.OutBack).OnComplete(() =>
            {
                emojiLike.transform.DOScale(0, 0.2f).From(1).SetEase(Ease.InBack).SetDelay(1f);

            });
            emojiLike.transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(1, 0.2f).From(0).SetEase(Ease.OutBack).OnComplete(() =>
            {
                emojiLike.transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(0, 0.2f).From(1).SetEase(Ease.InBack).SetDelay(1f);
            });
        }
        public void OpenHint()
        {
            hint.SetActive(true);
            GameManager_Baking.instance.setIsGamePause(true);
            cvHint.DOFade(1, 0.3f).From(0).SetEase(Ease.OutBack);
            hint.transform.DOScale(1, 0.3f).From(0).SetEase(Ease.OutBack);
        }
        public void CloseHint()
        {
            cvHint.DOFade(0, 0.3f).From(1).SetEase(Ease.InBack).OnComplete(() =>
            {
                hint.SetActive(false);
                GameManager_Baking.instance.setIsGamePause(false);

            });
            hint.transform.DOScale(0, 0.3f).From(1).SetEase(Ease.InBack);

        }
        IEnumerator delay()
        {
            yield return new WaitForEndOfFrame();
            UiCotroller_Baking.instance.AddButton();
        }
    }
}
