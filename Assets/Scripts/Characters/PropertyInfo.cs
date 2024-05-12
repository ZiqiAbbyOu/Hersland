using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HL.Character
{
    
    /// <summary>
    /// Enumeration of properties
    /// </summary>
    public enum Property
    {
        Jin,
        Mu,
        Shui,
        Huo,
        Tu
    }

    /// <summary>
    /// Abstract property class for one single character
    /// </summary>
    public abstract class PropertyInfo : MonoBehaviour
    {
        

        protected float[] propertyStats = new float[5];
        protected string[] propertyDiscription = new string[5];


        protected virtual void Awake()
        {
            InitializeStats();
        }


        /// <summary>
        /// Initializes property stats. Can be overridden in derived classes for custom initialization.
        /// </summary>
        protected virtual void InitializeStats()
        {
            // Default initialization 
            for (int i = 0; i < propertyStats.Length; i++)
            {
                propertyStats[i] = 0f;
                propertyDiscription[i] = string.Empty;
            }
            
        }

        /// <summary>
        /// Gets the stat value for the specified property.
        /// </summary>
        /// <param name="property">The property type to access.</param>
        /// <returns>The value of the specified property stat.</returns>
        public float GetPropertyStat(Property property)
        {
            int propertyArrayInt = GetPropertyArrayInt(property);

            if (property < 0 || propertyArrayInt >= propertyStats.Length)
            {
                Debug.LogError("Property index out of range.");
                return 0f;
            }
            return propertyStats[propertyArrayInt];
        }


        /// <summary>
        /// Get the discription for the specified property
        /// </summary>
        /// <param name="property">The property type to access</param>
        /// <returns>The discription for the specified property</returns>
        public string GetPropertyDiscription(Property property)
        {
            int propertyArrayInt = GetPropertyArrayInt(property);

            if (property < 0 || propertyArrayInt >= propertyStats.Length)
            {
                Debug.LogError("Property index out of range.");
                return "";
            }
            return propertyDiscription[propertyArrayInt];
        }


        /// <summary>
        /// Get property array int base on property name
        /// </summary>
        /// <param name="property">The property type to access</param>
        /// <returns>The property array int</returns>
        public int GetPropertyArrayInt(Property property)
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