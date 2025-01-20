using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Trung
{
    public class Level20Controller : MonoBehaviour
    {
        private int cupSize;
        private int sugarAmount;
        private int iceAmount;
        private string type;
        public int status;
        public static Level20Controller instance;

        [Header("RequestPaper")]
        [SerializeField] private TextMeshProUGUI drinkTypeText;
        [SerializeField] private TextMeshProUGUI sugarAmountText;
        [SerializeField] private TextMeshProUGUI iceAmountText;   
        [SerializeField] private TextMeshProUGUI cupSizeText;

        [Header("Cup")]
        [SerializeField] private BoxCollider2D bigCup;
        [SerializeField] private BoxCollider2D normalCup;
        [SerializeField] private BoxCollider2D smallCup;

        [Header("Status 1")]
        [SerializeField] private Sugar sugar;
        private void Awake()
        {
            if(instance != null && instance != this)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }
        }
        private void Start()
        {
            status = 0;

            StartData();
            RandomType();
            SetRequirement();
        }
        private void Update()
        {
            if(status == 1)
            {
                if(sugarAmount == 0)
                {
                    GoNextStatus();
                }
                else if(sugarAmount == 1)
                {

                }
            }
        }
        public void GoNextStatus()
        {
            status++;
            CheckStatus();
        }

        private void CheckStatus()
        {
            if(status == 1)
            {
                sugar.enabled = true;
            }
        }

        private void StartData()
        {
            smallCup.enabled = false;
            normalCup.enabled = false;
            bigCup.enabled = false;
        }
        private void RandomType()
        {
            cupSize = Random.Range(1, 4);
            sugarAmount = Random.Range(0, 3);
            iceAmount = Random.Range(0, 4);
            if (Random.Range(0, 2) == 0)
            {
                type = "Mocha";
            }
            else
            {
                type = "Machiato";
            }
        }

        private void SetRequirement()
        {
            //Drink
            drinkTypeText.text = type;
            //Cup
            if(cupSize == 1)
            {
                cupSizeText.text = "Small cup";
                smallCup.enabled = true;
            }
            else if(cupSize == 2)
            {
                cupSizeText.text = "Normal cup";
                normalCup.enabled = true;
            }
            else if(cupSize == 3)
            {
                cupSizeText.text = "Big cup";
                bigCup.enabled = true;
            }
            //Sugar
            if(sugarAmount == 0)
            {
                sugarAmountText.text = "(No sugar";
            }
            else if (sugarAmount == 1)
            {
                sugarAmountText.text = "(Less sugar";
            }
            else if (sugarAmount == 2)
            {
                sugarAmountText.text = "(Normal sugar";
            }
            //Ice
            if (iceAmount == 0)
            {
                iceAmountText.text = "No ice)";
            }
            else if (iceAmount == 1)
            {
                iceAmountText.text = "Less ice)";
            }
            else if (iceAmount == 2)
            {
                iceAmountText.text = "Normal ice)";
            }
            else if (iceAmount == 3)
            {
                iceAmountText.text = "More ice)";
            }
        }
    }
}
