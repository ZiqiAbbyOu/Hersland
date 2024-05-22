using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HL.Character
{
    
    /// <summary>
    /// Enumeration of properties
    /// </summary>
    public enum WuXingProperty
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
    public abstract class WuXingPropertyInfo : MonoBehaviour
    {
        

        [SerializeField] protected float[] wuXingPropertyStats = new float[5];
        [SerializeField] protected string[] wuXingPropertyDescriptions = new string[5];


        protected virtual void Awake()
        {
            InitializeInfos();
        }

        /// <summary>
        /// Initializes property stats. Can be overridden in derived classes for custom initialization.
        /// </summary>
        protected virtual void InitializeInfos()
        {
            // Default initialization 
            SetRandomPropertyStats();
            
        }

        /// <summary>
        /// Get property array int base on property name
        /// </summary>
        /// <param name="property">The property type to access</param>
        /// <returns>The property array int</returns>
        public int GetPropertyArrayInt(WuXingProperty property)
        {
            return (int)property;
        }

        /// <summary>
        /// Gets the stat value for the specified property.
        /// </summary>
        /// <param name="property">The property type to access.</param>
        /// <returns>The value of the specified property stat.</returns>
        public float GetPropertyStat(WuXingProperty property)
        {
            int propertyArrayInt = GetPropertyArrayInt(property);

            if (property < 0 || propertyArrayInt >= wuXingPropertyStats.Length)
            {
                Debug.LogError("Property index out of range.");
                return 0f;
            }
            return wuXingPropertyStats[propertyArrayInt];
        }


        /// <summary>
        /// Get the discription for the specified property
        /// </summary>
        /// <param name="property">The property type to access</param>
        /// <returns>The discription for the specified property</returns>
        public string GetPropertyDescription(WuXingProperty property)
        {
            int propertyArrayInt = GetPropertyArrayInt(property);

            if (property < 0 || propertyArrayInt >= wuXingPropertyStats.Length)
            {
                Debug.LogError("Property index out of range.");
                return "";
            }
            return wuXingPropertyDescriptions[propertyArrayInt];
        }

        /// <summary>
        /// Set the stat value for the specified property
        /// </summary>
        /// <param name="property">The property type to access</param>
        /// <param name="newValue">The new property value</param>
        public void SetPropertyStat(WuXingProperty property, float newValue)
        {
            int propertyArrayInt = GetPropertyArrayInt(property);

            if (property < 0 || propertyArrayInt >= wuXingPropertyStats.Length)
            {
                Debug.LogError("Property index out of range.");
            }
            wuXingPropertyStats[propertyArrayInt] = newValue;
        }

        /// <summary>
        /// Set the description value for the specified property
        /// </summary>
        /// <param name="property">The property type to access</param>
        /// <param name="newDescription">The new property description</param>
        public void SetPropertyDescription(WuXingProperty property,string newDescription )
        {
            int propertyArrayInt = GetPropertyArrayInt(property);

            if (property < 0 || propertyArrayInt >= wuXingPropertyStats.Length)
            {
                Debug.LogError("Property index out of range.");
            }
            wuXingPropertyDescriptions[propertyArrayInt] = newDescription;
        }
    
        public void SetRandomPropertyStats()
        {
            for (int i = 0; i < wuXingPropertyStats.Length; i++)
            {
                wuXingPropertyStats[i] = UnityEngine.Random.Range(-1f, 1f);
            }
        }
    }
}