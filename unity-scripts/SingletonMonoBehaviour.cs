using System;
using UnityEngine;

namespace HkUniBase
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        #region Member Datas

        public static readonly string CommonTag = "Common";

        public static T Instance
        {
            get
            {
                lock (_lockObject)
                {
                    if (_instance != null)
                    {
                        return _instance;
                    }

                    _instance = (T)FindObjectOfType(typeof(T));

                    if (_instance != null)
                    {
                        return _instance;
                    }

                    _instance = CreateSelfByPrefab();

                    if (_instance != null)
                    {
                        return _instance;
                    }

                    _instance = CreateSelf();

                    if (_instance != null)
                    {
                        return _instance;
                    }

                    Debug.LogError($"{typeof(T).Name}‚ÌŽæ“¾‚ÉŽ¸”s‚µ‚Ü‚µ‚½");
                    return null;
                }
            }
        }

        static object _lockObject = new();
        static T _instance;

        #endregion

        #region Unity Event Functions

        void Awake()
        {
            var commonGmObjs = GameObject.FindGameObjectsWithTag(CommonTag);
            var selfGmObjs = Array.FindAll(commonGmObjs, gmObj => gmObj.GetComponent<T>() != null);

            if (selfGmObjs.Length > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        #endregion

        #region Non Public Functions

        static T CreateSelfByPrefab()
        {
            var prefab = (GameObject)Resources.Load($"Singleton/{typeof(T).Name}");
            return Instantiate(prefab, Vector3.zero, Quaternion.identity).GetComponent<T>();
        }

        static T CreateSelf()
        {
            return new GameObject(typeof(T).Name, new Type[] { typeof(T) }).GetComponent<T>();
        }

        #endregion
    }
}
