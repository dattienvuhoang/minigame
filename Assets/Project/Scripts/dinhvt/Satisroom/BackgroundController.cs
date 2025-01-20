using SR4BlackDev;
using UnityEngine;
using UnityEngine.UI;

namespace dinhvt
{
    public class BackgroundController : MonoBehaviour
    {
        private Image image;

        private void Awake()
        {   
            image = GetComponent<Image>();

            this.RegisterListener(EventID.UpdateBackground, ChangeSprite);
        }

        private void OnDestroy()
        {
            this.RemoveListener(EventID.UpdateBackground, ChangeSprite);
        }

        public void ChangeSprite(object send, object bg)
        {
            image.sprite = (Sprite)bg;
        }
    }
}
