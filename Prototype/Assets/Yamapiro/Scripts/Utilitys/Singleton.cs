using System;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T _Instance;

	public static T Instance
	{
		get
		{
			if (_Instance == null)
			{
				_Instance = FindObjectOfType<T>();
				if (_Instance == null)
				{
					Log.Error(_Instance.GetType(), typeof(T) + "が追加されているGameObjectが存在しません。");
				}
			}

			return _Instance;
		}
	}

	virtual protected void Awake()
	{
		if (this != Instance)
		{
			Destroy(this);
			Log.Info(GetType(), typeof(T) +
				" は既に他のGameObjectに 追加されているため、コンポーネントを破棄しました。\n" +
				" アタッチされているGameObjectは " + Instance.gameObject.name + " です。", Instance.gameObject);
			return;
		}
	}
}