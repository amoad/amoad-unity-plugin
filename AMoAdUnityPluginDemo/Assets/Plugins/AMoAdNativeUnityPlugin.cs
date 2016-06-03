using UnityEngine;
using System.Runtime.InteropServices;

public class AMoAdNativeUnityPlugin {
	public const string VersionNo = "1.0.2";

	#if UNITY_IOS
	[DllImport("__Internal")]
	private static extern void amoad_native_load(string sid, string tag, int x, int y, int width, int height);

	[DllImport("__Internal")]
	private static extern void amoad_native_remove(string sid, string tag);

	[DllImport("__Internal")]
	private static extern void amoad_native_reload(string sid, string tag);

	[DllImport("__Internal")]
	private static extern void amoad_native_show(string sid, string tag);
	
	[DllImport("__Internal")]
	private static extern void amoad_native_hide(string sid, string tag);
	
	[DllImport("__Internal")]
	private static extern void amoad_native_start_rotation(string sid, string tag, int seconds);
	
	[DllImport("__Internal")]
	private static extern void amoad_native_stop_rotation(string sid, string tag);
	
	[DllImport("__Internal")]
	private static extern void amoad_native_set_html_url_string(string htmlUrlString);

	[DllImport("__Internal")]
	private static extern void amoad_native_load_with_option(string sid, string tag, int x, int y, int width, int height, string option);
	#elif UNITY_ANDROID
	private static object syncRoot = new object();
	private static AndroidJavaClass m_AndroidPlugin;
	private static AndroidJavaClass AndroidPlugin {
		get {
			if (m_AndroidPlugin == null) {
				lock (syncRoot) {
					if (m_AndroidPlugin == null) {
						m_AndroidPlugin = new AndroidJavaClass("com.amoad.unity.NativePlugin");
					}
				}
			}
			return m_AndroidPlugin;
		}
	}
	#endif
	
	/// <summary>
	/// 広告をロードする
	/// </summary>
	/// <param name="sid">管理画面から取得した64文字の英数字</param>
	/// <param name="tag">同じsidの複数広告を識別するための任意の文字列</param>
	/// <param name="x">広告のX座標</param> 
	/// <param name="y">広告のY座標</param> 
	/// <param name="width">広告の横幅</param> 
	/// <param name="height">広告の縦幅</param> 
	public static void Load(string sid, string tag, int x, int y, int width, int height) {
		#if UNITY_IOS
		amoad_native_load(sid:sid, tag:tag, x:x, y:y, width:width, height:height);
		#elif UNITY_ANDROID
		AndroidPlugin.CallStatic("load", sid, tag, x, y, width, height);
		#endif
	}

	/// <summary>
	/// 広告Viewを削除する（親ビューからremoveされます）
	/// </summary>
	/// <param name="sid">管理画面から取得した64文字の英数字</param>
	/// <param name="tag">同じsidの複数広告を識別するための任意の文字列</param>
	public static void Remove(string sid, string tag) {
		#if UNITY_IOS
		amoad_native_remove(sid:sid, tag:tag);
		#elif UNITY_ANDROID
		AndroidPlugin.CallStatic("remove", sid, tag);
		#endif
	}
	
	/// <summary>
	/// 広告をリロード（別の広告に更新）する
	/// </summary>
	/// <param name="sid">管理画面から取得した64文字の英数字</param>
	/// <param name="tag">同じsidの複数広告を識別するための任意の文字列</param>
	public static void Reload(string sid, string tag) {
		#if UNITY_IOS
		amoad_native_reload(sid:sid, tag:tag);
		#elif UNITY_ANDROID
		AndroidPlugin.CallStatic("reload", sid, tag);
		#endif
	}
	
	/// <summary>
	/// 広告Viewを表示する
	/// </summary>
	/// <param name="sid">管理画面から取得した64文字の英数字</param>
	/// <param name="tag">同じsidの複数広告を識別するための任意の文字列</param>
	public static void Show(string sid, string tag) {
		#if UNITY_IOS
		amoad_native_show(sid:sid, tag:tag);
		#elif UNITY_ANDROID
		AndroidPlugin.CallStatic("show", sid, tag);
		#endif
	}
	
	/// <summary>
	/// 広告Viewを非表示にする
	/// </summary>
	/// <param name="sid">管理画面から取得した64文字の英数字</param>
	/// <param name="tag">同じsidの複数広告を識別するための任意の文字列</param>
	public static void Hide(string sid, string tag) {
		#if UNITY_IOS
		amoad_native_hide(sid:sid, tag:tag);
		#elif UNITY_ANDROID
		AndroidPlugin.CallStatic("hide", sid, tag);
		#endif
	}
	
	/// <summary>
	/// ローテーションを開始する
	/// </summary>
	/// <param name="sid">管理画面から取得した64文字の英数字</param>
	/// <param name="tag">同じsidの複数広告を識別するための任意の文字列</param>
	/// <param name="seconds">ローテーション間隔（秒）</param>
	public static void StartRotation(string sid, string tag, int seconds) {
		#if UNITY_IOS
		amoad_native_start_rotation(sid:sid, tag:tag, seconds:seconds);
		#elif UNITY_ANDROID
		AndroidPlugin.CallStatic("startRotation", sid, tag, seconds);
		#endif
	}
	
	/// <summary>
	/// ローテーションを停止する
	/// </summary>
	/// <param name="sid">管理画面から取得した64文字の英数字</param>
	/// <param name="tag">同じsidの複数広告を識別するための任意の文字列</param>
	public static void StopRotation(string sid, string tag) {
		#if UNITY_IOS
		amoad_native_stop_rotation(sid:sid, tag:tag);
		#elif UNITY_ANDROID
		AndroidPlugin.CallStatic("stopRotation", sid, tag);
		#endif
	}
	
	/// <summary>
	/// 開発用
	/// </summary>
	/// <param name="htmlUrlString"></param>
	public static void SetHtmlUrlString(string htmlUrlString) {
		#if UNITY_IOS
		amoad_native_set_html_url_string(htmlUrlString:htmlUrlString);
		#elif UNITY_ANDROID
		AndroidPlugin.CallStatic("setHtmlUrlString", htmlUrlString);
		#endif
	}
	
	/// <summary>
	/// 広告をロードする (開発用)
	/// </summary>
	/// <param name="sid">管理画面から取得した64文字の英数字</param>
	/// <param name="tag">同じsidの複数広告を識別するための任意の文字列</param>
	/// <param name="x">広告のX座標</param> 
	/// <param name="y">広告のY座標</param> 
	/// <param name="width">広告の横幅</param> 
	/// <param name="height">広告の縦幅</param> 
	/// <param name="option">開発用</param> 
	public static void Load(string sid, string tag, int x, int y, int width, int height, string option) {
		#if UNITY_IOS
		amoad_native_load_with_option(sid:sid, tag:tag, x:x, y:y, width:width, height:height, option:option);
		#elif UNITY_ANDROID
		AndroidPlugin.CallStatic("load", sid, tag, x, y, width, height, option);
		#endif
	}
	
}
