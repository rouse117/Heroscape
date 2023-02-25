using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.Diagnostics;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public static class Serializer {
    public static void Serialize<T>(string filePath, T objectToWrite, bool append = false) {
        using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create)) {
            var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            binaryFormatter.Serialize(stream, objectToWrite);
        }
    }

    /// <summary>
    /// Reads an object instance from a binary file.
    /// </summary>
    /// <typeparam name="T">The type of object to read from the binary file.</typeparam>
    /// <param name="filePath">The file path to read the object instance from.</param>
    /// <returns>Returns a new instance of the object read from the binary file.</returns>
    public static T Deserialize<T>(string filePath) {
        using (Stream stream = File.Open(filePath, FileMode.Open)) {
            var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            return (T)binaryFormatter.Deserialize(stream);
        }
    }
}
