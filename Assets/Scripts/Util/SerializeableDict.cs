using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializeDict<K,V>
{
    [Serializable]
    struct ListEntry {
        public K key;
        public V value;
    }

    [SerializeField]
    private List<ListEntry> mapEntries;

    public Dictionary<K, V> ToDict() {

        Dictionary<K,V> returnDict = new Dictionary<K, V>();

        foreach(ListEntry entry in mapEntries) {
            returnDict[entry.key] = entry.value;
        }

        return returnDict;
    }
}
