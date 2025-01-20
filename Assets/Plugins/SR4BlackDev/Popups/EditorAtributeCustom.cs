using System;
using System.Collections;
using UnityEngine;

namespace SR4BlackDev.Editor
{
    public partial class EditorAtributeCustom
    {
        [AttributeUsage(AttributeTargets.All, Inherited = true)]
        public class ConditionEnumAttribute : PropertyAttribute
        {
            public string ConditionEnum = "";
            public bool Hidden = false;

            BitArray bitArray = new BitArray(32);
        
            public bool ContainsBitFlag(int enumValue)
            {
                return bitArray.Get(enumValue);
            }

            public ConditionEnumAttribute(string conditionBoolean, int enumValues)
            {
                this.ConditionEnum = conditionBoolean;
                this.Hidden = true;

                bitArray.Set(enumValues, true);
            }
        
            public ConditionEnumAttribute(string conditionBoolean, params int[] enumValues)
            {
                this.ConditionEnum = conditionBoolean;
                this.Hidden = true;

                for (int i = 0; i < enumValues.Length; i++)
                {
                    bitArray.Set(enumValues[i], true);
                }
            }
        }
        
        
        [AttributeUsage(AttributeTargets.All, Inherited = true)]
        public class ConditionBoolAttribute : PropertyAttribute
        {
            public string ConditionBoolean = "";
            public bool Hidden = false;

            public ConditionBoolAttribute(string conditionBoolean)
            {
                this.ConditionBoolean = conditionBoolean;
                this.Hidden = false;
            }

            public ConditionBoolAttribute(string conditionBoolean, bool hideInInspector)
            {
                this.ConditionBoolean = conditionBoolean;
                this.Hidden = hideInInspector;
            }
            
        }
    }
}