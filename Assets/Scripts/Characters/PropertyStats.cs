using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HL.Character
{

    /// <summary>
    /// Abstract property class for one single character
    /// </summary>
    public abstract class PropertyStats : MonoBehaviour
    {
        

        protected float[] propertyStats = new float[5];


        protected virtual void Awake()
        {
            InitializeStats();
        }


        /// <summary>
        /// Initializes property stats. Can be overridden in derived classes for custom initialization.
        /// </summary>
        protected virtual void InitializeStats()
        {
            // Default initialization logic (e.g., all zeros)
            for (int i = 0; i < propertyStats.Length; i++)
            {
                propertyStats[i] = 0f;
            }
        }

        /// <summary>
        /// Gets the stat value for the specified property.
        /// </summary>
        /// <param name="property">The property index to access.</param>
        /// <returns>The value of the specified property stat.</returns>
        public float GetPersonalityStat(int property)
        {
            if (property < 0 || property >= propertyStats.Length)
            {
                Debug.LogError("Property index out of range.");
                return 0f;
            }
            return propertyStats[property];
        }
    }
}