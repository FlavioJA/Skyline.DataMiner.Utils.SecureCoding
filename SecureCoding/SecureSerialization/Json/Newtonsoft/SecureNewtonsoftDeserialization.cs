﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Skyline.DataMiner.Utils.SecureCoding.SecureSerialization.Json.Newtonsoft
{
    /// <summary>
    /// Provides methods to securely deserialize JSON.
    /// </summary>
    public static class SecureNewtonsoftDeserialization
    {
        /// <summary>
        /// Deserializes the <paramref name="json"/> string to an object of the specified type <paramref name="T"/>.
        /// This method will ignore the types specified in the json string to make the deserialization secure.
        /// Because of this, this method is limited to deserializing simple objects and will fail for complex objects that make use of polymorphism or inheritance.
        /// </summary>
        /// <typeparam name="T">The type of the object that will be deserialized.</typeparam>
        /// <param name="json">The json string representing the object that will be deserialized.</param>
        /// <returns>The deserialized object from the json string.</returns>
        /// <exception cref="ArgumentException">Thrown if the <paramref name="json"/> is null, empty or whitespace.</exception>
        /// <exception cref="JsonReaderException">Thrown if the <paramref name="json"/> is not a valid json string.</exception>
        /// <exception cref="JsonSerializationException">Thrown if the <paramref name="json"/> cannot be deserialized.</exception>
        public static T DeserializeObject<T>(string json)
        {
            if (json is null)
            {
                throw new ArgumentException($"'{nameof(json)}' cannot be null.", nameof(json));
            }

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.None,
            };

            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        /// <summary>
        /// Deserializes the json string to an object of the specified type using the provided deserialization settings.
        /// This method will override the typenamehandling setting and the serializationbinder setting to make the deserialization secure.
        /// Because of this, this method is limited to deserializing simple objects and will fail for complex objects that make use of polymorphism or inheritance.
        /// </summary>
        /// <typeparam name="T">The type of the object that will be deserialized.</typeparam>
        /// <param name="json">The json string representing the object that will be deserialized.</param>
        /// <param name="settings">The settings that will be used for deserialization.</param>
        /// <returns>The deserialized object from the json string.</returns>
        /// <exception cref="ArgumentException">Thrown if the <paramref name="json"/> is null, empty or whitespace.</exception>
        /// <exception cref="InsecureSerializationSettingsException">Thrown if the <paramref name="settings"/> typenamehandling is set to anything other than None.</exception>
        /// <exception cref="JsonReaderException">Thrown if the <paramref name="json"/> is not a valid json string.</exception>
        /// <exception cref="JsonSerializationException">Thrown if the <paramref name="json"/> cannot be deserialized.</exception>
        public static T DeserializeObject<T>(string json, JsonSerializerSettings settings)
        {
            if (json is null)
            {
                throw new ArgumentException($"'{nameof(json)}' cannot be null.", nameof(json));
            }

            if (settings is null)
            {
                settings = new JsonSerializerSettings();
            }

            if (settings.TypeNameHandling != TypeNameHandling.None)
            {
                throw new InsecureSerializationSettingsException($"Setting the TypeNameHandling to {settings.TypeNameHandling} may result in insecure deserialization.");
            }

            settings.SerializationBinder = null;

            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        /// <summary>
        /// Deserializes the json string to an object of the specified type.
        /// This method will make use of the types specified in the json to determine which objects should be constructed.
        /// Please note that JSON results may not be reliable if deserialization is attempted on an object that was serialized with TypeNameHandling set to 'None'.
        /// To make the deserialization secure, only the types specified in the knownTypes list will be resolved.
        /// Because of this, this method can handle more complex objects that make use of polymorphism or inheritance.
        /// </summary>
        /// <typeparam name="T">The type of the object that will be deserialized.</typeparam>
        /// <param name="json">The json string representing the object that will be deserialized.</param>
        /// <param name="knownTypes">The list of types that the deserializer will be able to resolve.</param>
        /// <returns>The deserialized object from the json string.</returns>
        /// <exception cref="ArgumentException">Thrown if the <paramref name="json"/> is null, empty or whitespace or if the <paramref name="knownTypes"/> list is null or empty.</exception>
        /// <exception cref="JsonSerializationException">Thrown if the <paramref name="json"/> contains a type that is not in the <paramref name="knownTypes"/>.</exception>
        /// <exception cref="KnownExploitableTypeException">Thrown if a type specified in the <paramref name="knownTypes"/> is known to be insecure for deserialization.</exception> 
        /// <exception cref="JsonReaderException">Thrown if the <paramref name="json"/> is not a valid json string.</exception>
        /// <exception cref="JsonSerializationException">Thrown if the <paramref name="json"/> cannot be deserialized.</exception>
        public static T DeserializeObject<T>(string json, IEnumerable<Type> knownTypes)
        {
            if (json is null)
            {
                throw new ArgumentException($"'{nameof(json)}' cannot be null.", nameof(json));
            }

            if (knownTypes is null || !knownTypes.Any())
            {
                throw new ArgumentException($"'{nameof(knownTypes)}' cannot be null or empty.", nameof(knownTypes));
            }

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                SerializationBinder = new KnownTypesSerializationBinder(knownTypes),
            };

            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        /// <summary>
        /// Deserializes the json string to an object of the specified type using the provided deserialization settings.
        /// This method will make use of the types specified in the json to determine which objects should be constructed. 
        /// Please note that JSON results may not be reliable if deserialization is attempted on an object that was serialized with TypeNameHandling set to 'None'.
        /// To make the deserialization secure, the serializationbinder setting will be overriden and only the types specified in the knownTypes list will be resolved.
        /// Because of this, this method can handle more complex objects that make use of polymorphism or inheritance.
        /// </summary>
        /// <typeparam name="T">The type of the object that will be deserialized.</typeparam>
        /// <param name="json">The json string representing the object that will be deserialized.</param>
        /// <param name="knownTypes">The list of types that the deserializer will be able to resolve.</param>
        /// <param name="settings">The settings that will be used for deserialization.</param>
        /// <returns>The deserialized object from the json string.</returns>
        /// <exception cref="ArgumentException">Thrown if the <paramref name="json"/> is null, empty or whitespace or if the <paramref name="knownTypes"/> list is null or empty.</exception>
        /// <exception cref="UnknownTypeException">Thrown if the <paramref name="json"/> contains a type that is not in the <paramref name="knownTypes"/>.</exception>
        /// <exception cref="KnownExploitableTypeException">Thrown if a type specified in the <paramref name="knownTypes"/> is known to be insecure for deserialization.</exception>
        /// <exception cref="JsonReaderException">Thrown if the <paramref name="json"/> is not a valid json string.</exception>
        /// <exception cref="JsonSerializationException">Thrown if the <paramref name="json"/> cannot be deserialized.</exception>
        public static T DeserializeObject<T>(string json, IEnumerable<Type> knownTypes, JsonSerializerSettings settings)
        {
            if (json is null)
            {
                throw new ArgumentException($"'{nameof(json)}' cannot be null.", nameof(json));
            }

            if (knownTypes is null || !knownTypes.Any())
            {
                throw new ArgumentException($"'{nameof(knownTypes)}' cannot be null or empty.", nameof(knownTypes));
            }

            if (settings is null)
            {
                settings = new JsonSerializerSettings();
            }

            settings.TypeNameHandling = TypeNameHandling.Auto;
            settings.SerializationBinder = new KnownTypesSerializationBinder(knownTypes);

            return JsonConvert.DeserializeObject<T>(json, settings);
        }
    }
}