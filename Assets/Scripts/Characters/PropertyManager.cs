using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace HL.Character
{

    public enum Property
    {
        Jin,
        Mu,
        Shui,
        Huo,
        Tu
    }

    /// <summary>
    /// functions that manage all the character's personlaity
    /// </summary>
    public class PropertyManager : MonoBehaviour
    {
        public int GetPersonalityArrayInt(Property property)
        {
            int propertyArrayInt = 0;
            switch (property)
            {
                case Property.Jin:
                    propertyArrayInt = 0;
                    break;
                case Property.Mu:
                    propertyArrayInt = 1;
                    break;
                case Property.Shui:
                    propertyArrayInt = 2;
                    break;
                case Property.Huo:
                    propertyArrayInt = 3;
                    break;
                case Property.Tu:
                    propertyArrayInt = 4;
                    break;
            }
            return propertyArrayInt;
        }
    }

}