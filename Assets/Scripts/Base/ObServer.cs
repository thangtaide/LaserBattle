using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Base.DesignPattern
{
    public class ObServer : Singleton<ObServer>
    {
        public delegate void CallBackObserver(object data);
        Dictionary<string, HashSet<CallBackObserver>> observers = new Dictionary<string, HashSet<CallBackObserver>>();
        public void AddObserver(string topicName, CallBackObserver callBackObserver) {
            CreateListObserverForTopic(topicName).Add(callBackObserver);
        }
        public bool RemoveObserver(string topicName, CallBackObserver callBackObserver) {
            return CreateListObserverForTopic(topicName).Remove(callBackObserver);
        }
        
        public void Notify(string topicName, object data) 
        {
            HashSet<CallBackObserver> listObserver = CreateListObserverForTopic(topicName);
            foreach(CallBackObserver observer in listObserver) 
            {
                observer(data);
            }
        }
        public void Notify(string topicName)
        {
            HashSet<CallBackObserver> listObserver = CreateListObserverForTopic(topicName);
            foreach (CallBackObserver observer in listObserver)
            {
                observer(null);
            }
        }
        protected HashSet<CallBackObserver> CreateListObserverForTopic(string topicName)
        {
            if(!observers.ContainsKey(topicName)) {
                observers.Add(topicName, new HashSet<CallBackObserver>());
            }
            return observers[topicName];
        }
    }
}

