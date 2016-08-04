using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;

public class AMoAdUnityPlugin {
	public const string VersionNo = "1.4.0";

	/// <summary>
	/// 水平方向の広告表示位置
	/// </summary>
	public enum HorizontalAlign {
		None = 0,
		Left = 1,
		Center = 2,
		Right = 3,
	}

	/// <summary>
	/// 垂直方向の広告表示位置
	/// </summary>
	public enum VerticalAlign {
		None = 0,
		Top = 1,
		Middle = 2,
		Bottom = 3,
	}

	/// <summary>
	/// 広告サイズの調整
	/// </summary>
	public enum AdjustMode {
		Fixed = 0,
		Responsive = 1,
	}

	/// <summary>
	/// ローテーション時トランジション
	/// </summary>
	public enum RotateTransition {
		None = 0,
		FlipFromLeft = 1,
		FlipFromRight = 2,
		CurlUp = 3,
		CurlDown = 4,
	}

	/// <summary>
	/// Android用のローテーション時トランジション
	/// </summary>
	public enum AndroidRotateTransition {
		None = 0,
		Alpha = 1,
		Rotate = 2,
		Scale = 3,
		Translate = 4,
	}

	/// <summary>
	/// クリック時トランジション
	/// </summary>
	public enum ClickTransition {
		None = 0,
		Jump = 1,
	}

	/// <summary>
	/// Android用のクリック時トランジション
	/// </summary>
	public enum AndroidClickTransition {
		None = 0,
		Jump = 1,
	}

	#if UNITY_IOS
	[DllImport("__Internal")]
	private static extern void amoad_show(
		string sid,
		HorizontalAlign hAlign,
		VerticalAlign vAlign,
		AdjustMode adjustMode,
		RotateTransition rotateTrans,
		ClickTransition clickTrans,
		string imageName,
		int x,
		int y,
		int timeoutMillis
		);

	[DllImport("__Internal")]
	private static extern void amoad_hide(string sid);

	[DllImport("__Internal")]
	private static extern void amoad_dispose(string sid);

	[DllImport("__Internal")]
	private static extern void amoad_prepare_interstitial(string sid, int timeoutMillis);

	[DllImport("__Internal")]
	private static extern void amoad_set_interstitial_display_clickable(string sid, bool clickable);

	[DllImport("__Internal")]
	private static extern void amoad_set_interstitial_dialog_shown(string sid, bool shown);

	[DllImport("__Internal")]
	private static extern void amoad_set_interstitial_portrait_panel(string sid, string imageName);

	[DllImport("__Internal")]
	private static extern void amoad_set_interstitial_landscape_panel(string sid, string imageName);

	[DllImport("__Internal")]
	private static extern void amoad_set_interstitial_link_button(string sid, string imageName, string highlighted);

	[DllImport("__Internal")]
	private static extern void amoad_set_interstitial_close_button(string sid, string imageName, string highlighted);

	[DllImport("__Internal")]
	private static extern void amoad_show_interstitial(string sid);

	[DllImport("__Internal")]
	private static extern void amoad_close_interstitial(string sid);


	[DllImport("__Internal")]
	private static extern void amoad_set_interstitial_auto_reload(string sid, bool autoReload);

	[DllImport("__Internal")]
	private static extern void amoad_load_interstitial(string sid);

	[DllImport("__Internal")]
	private static extern bool amoad_is_interstitial_loaded(string sid);

	#elif UNITY_ANDROID
	private static object syncRoot = new object();
	private static AndroidJavaClass m_AndroidPlugin;
	private static AndroidJavaClass AndroidPlugin {
		get {
			if (m_AndroidPlugin == null) {
				lock (syncRoot) {
					if (m_AndroidPlugin == null) {
						m_AndroidPlugin = new AndroidJavaClass("com.amoad.unity.Plugin");
					}
				}
			}
			return m_AndroidPlugin;
		}
	}
	#endif

	class Plugin {
		private string sid;
		private HorizontalAlign hAlign;
		private VerticalAlign vAlign;
		private int x;
		private int y;
		private AdjustMode adjustMode;
		private RotateTransition rotateTrans;
		private ClickTransition clickTrans;
		private string imageName;
		private AndroidRotateTransition androidRotateTrans;
		private AndroidClickTransition androidClickTrans;
		private int timeoutMillis;

		public Plugin(
			string sid,
			HorizontalAlign hAlign,
			VerticalAlign vAlign,
			AdjustMode adjustMode = AdjustMode.Responsive,
			RotateTransition rotateTrans = RotateTransition.None,
			ClickTransition clickTrans = ClickTransition.None,
			string imageName = null,
			int x = 0,
			int y = 0,
			AndroidRotateTransition androidRotateTrans = AndroidRotateTransition.None,
			AndroidClickTransition androidClickTrans = AndroidClickTransition.None,
			int timeoutMillis = 30*1000
			)
		{
			this.sid = sid;
			this.hAlign = hAlign;
			this.vAlign = vAlign;
			this.adjustMode = adjustMode;
			this.rotateTrans = rotateTrans;
			this.clickTrans = clickTrans;
			this.imageName = imageName;
			this.x = x;
			this.y = y;
			this.androidRotateTrans = androidRotateTrans;
			this.androidClickTrans = androidClickTrans;
			this.timeoutMillis = timeoutMillis;
		}

		public void Show() {
			#if UNITY_IOS
			amoad_show(
				sid: this.sid,
				hAlign: this.hAlign,
				vAlign: this.vAlign,
				adjustMode: this.adjustMode,
				rotateTrans: this.rotateTrans,
				clickTrans: this.clickTrans,
				imageName: this.imageName,
				x: this.x,
				y: this.y,
				timeoutMillis: this.timeoutMillis
				);
			#elif UNITY_ANDROID
			AMoAdUnityPlugin.AndroidPlugin.CallStatic("show",
				this.sid,
				(int)this.hAlign,
				(int)this.vAlign,
				(int)this.adjustMode,
				(int)this.androidRotateTrans,
				(int)this.androidClickTrans,
				this.imageName,
				this.x,
				this.y,
				this.timeoutMillis);
			#endif
		}

		public void Hide() {
			#if UNITY_IOS
			amoad_hide(sid: this.sid);
			#elif UNITY_ANDROID
			AMoAdUnityPlugin.AndroidPlugin.CallStatic("hide", this.sid);
			#endif
		}

		public void Dispose() {
			#if UNITY_IOS
			amoad_dispose(sid: this.sid);
			#elif UNITY_ANDROID
			AMoAdUnityPlugin.AndroidPlugin.CallStatic("dispose", this.sid);
			#endif
		}
	}

	private static Dictionary<string, Plugin> plugins = new Dictionary<string, Plugin>();

	/// <summary>
	/// プラグインを登録する
	/// </summary>
	/// <param name="sid">sid</param>
	/// <param name="horizontalAlign">水平方向の広告表示位置</param>
	/// <param name="verticalAlign">垂直方向の広告表示位置</param>
	/// <param name="adjustMode">広告サイズの調整</param>
	/// <param name="rotateTran">ローテーション時トランジション</param>
	/// <param name="clickTrans">クリック時トランジション</param>
	/// <param name="imageName">初期表示画像ファイル名</param>
	/// <param name="x">x座標（HorizontalAlign.Noneのときのみ）</param>
	/// <param name="y">y座標（VerticalAlign.Noneのときのみ）</param>
	/// <param name="timeoutMillis">タイムアウト時間（ミリ秒）を設定する：デフォルトは30,000ミリ秒</param>
	public static void Register(
		string sid,
		HorizontalAlign hAlign,
		VerticalAlign vAlign,
		AdjustMode adjustMode = AdjustMode.Responsive,
		RotateTransition rotateTrans = RotateTransition.None,
		ClickTransition clickTrans = ClickTransition.None,
		string imageName = null,
		int x = 0,
		int y = 0,
		AndroidRotateTransition androidRotateTrans = AndroidRotateTransition.None,
		AndroidClickTransition androidClickTrans = AndroidClickTransition.None,
		int timeoutMillis = 30*1000
		)
	{
		Plugin plugin = new Plugin (sid, hAlign, vAlign, adjustMode, rotateTrans, clickTrans, imageName, x, y, androidRotateTrans, androidClickTrans, timeoutMillis);
		AMoAdUnityPlugin.plugins [sid] = plugin;
	}

	/// <summary>
	/// プラグインを削除する
	/// </summary>
	/// <param name="sid">sid</param>
	public static void Unregister(string sid) {
		AMoAdUnityPlugin.Dispose(sid);
		AMoAdUnityPlugin.plugins.Remove(sid);
	}

	/// <summary>
	/// すべてのプラグインを削除する
	/// </summary>
	public static void UnregisterAll() {
		foreach(string sid in AMoAdUnityPlugin.plugins.Keys) {
			Dispose(sid);
		}
		AMoAdUnityPlugin.plugins.Clear();
	}

	/// <summary>
	/// 広告を表示する
	/// </summary>
	/// <param name="sid">sid</param>
	public static void Show(string sid) {
		Plugin plugin = GetPlugin(sid);
		if (plugin != null) {
			plugin.Show();
		}
	}

	/// <summary>
	/// 広告を消す
	/// </summary>
	/// <param name="sid">sid</param>
	public static void Hide(string sid) {
		Plugin plugin = GetPlugin(sid);
		if (plugin != null) {
			plugin.Hide();
		}
	}

	/// <summary>
	/// 広告Viewを破棄する（Showで再表示できる）
	/// </summary>
	/// <param name="sid">sid</param>
	public static void Dispose(string sid) {
		Plugin plugin = GetPlugin(sid);
		if (plugin != null) {
			plugin.Dispose();
		}
	}

	private static Plugin GetPlugin(string sid) {
		try {
				return AMoAdUnityPlugin.plugins[sid];
			} catch (KeyNotFoundException e) {
				return null;
			}
	}

	/// <summary>
	/// 全画面広告を登録する
	/// </summary>
	/// <param name="sid">管理画面より取得したID</param>
	/// <param name="timeoutMillis">タイムアウト時間（ミリ秒）を設定する：デフォルトは30,000ミリ秒</param>
	public static void RegisterInterstitialAd(string sid, int timeoutMillis = 30*1000)
	{
		#if UNITY_IOS
		amoad_prepare_interstitial(sid, timeoutMillis);
		#elif UNITY_ANDROID
		AMoAdUnityPlugin.AndroidPlugin.CallStatic("registerInterstitialAd", sid, timeoutMillis);
		#endif
	}

	/// <summary>
	/// 広告面をクリックできるかどうかを設定する：デフォルトはYES
	/// </summary>
	/// <param name="sid">管理画面より取得したID</param>
	/// <param name="clickable">広告面をクリックできるかどうか</param>
	public static void SetInterstitialDisplayClickable(string sid, bool clickable)
	{
		#if UNITY_IOS
		amoad_set_interstitial_display_clickable(sid, clickable);
		#elif UNITY_ANDROID
		AMoAdUnityPlugin.AndroidPlugin.CallStatic("setInterstitialDisplayClickable", sid, clickable);
		#endif
	}

	/// <summary>
	/// 確認ダイアログを表示するかどうかを設定する：デフォルトはYES
	/// </summary>
	/// <param name="sid">管理画面より取得したID</param>
	/// <param name="shown">確認ダイアログを表示するかどうか</param>
	public static void SetInterstitialDialogShown(string sid, bool shown)
	{
		#if UNITY_IOS
		amoad_set_interstitial_dialog_shown(sid, shown);
		#elif UNITY_ANDROID
		AMoAdUnityPlugin.AndroidPlugin.CallStatic("setInterstitialDialogShown", sid, shown);
		#endif
	}

	/// <summary>
	/// 全画面広告のPortraitパネル画像を設定する
	/// </summary>
	/// <param name="sid">管理画面より取得したID</param>
	/// <param name="imageName">Portraitパネル画像のファイル名</param>
	public static void SetInterstitialPortraitPanel(string sid, string imageName)
	{
		#if UNITY_IOS
		amoad_set_interstitial_portrait_panel(sid, imageName);
		#elif UNITY_ANDROID
		AMoAdUnityPlugin.AndroidPlugin.CallStatic("setInterstitialPortraitPanel", sid, imageName);
		#endif
	}

	/// <summary>
	/// 全画面広告のLandscapeパネル画像を設定する
	/// </summary>
	/// <param name="sid">管理画面より取得したID</param>
	/// <param name="imageName">Landscapeパネル画像のファイル名</param>
	public static void SetInterstitialLandscapePanel(string sid, string imageName)
	{
		#if UNITY_IOS
		amoad_set_interstitial_landscape_panel(sid, imageName);
		#elif UNITY_ANDROID
		AMoAdUnityPlugin.AndroidPlugin.CallStatic("setInterstitialLandscapePanel", sid, imageName);
		#endif
	}

	/// <summary>
	/// 全画面広告のリンクボタン画像を設定する
	/// </summary>
	/// <param name="sid">管理画面より取得したID</param>
	/// <param name="imageName">リンクボタン画像のファイル名</param>
	/// <param name="highlighted">押下時のリンクボタン画像のファイル名</param>
	public static void SetInterstitialLinkButton(string sid, string imageName, string highlighted)
	{
		#if UNITY_IOS
		amoad_set_interstitial_link_button(sid, imageName, highlighted);
		#elif UNITY_ANDROID
		AMoAdUnityPlugin.AndroidPlugin.CallStatic("setInterstitialLinkButton", sid, imageName, highlighted);
		#endif
	}

	/// <summary>
	/// 全画面広告の閉じるボタン画像を設定する
	/// </summary>
	/// <param name="sid">管理画面より取得したID</param>
	/// <param name="imageName">閉じるボタン画像のファイル名</param>
	/// <param name="highlighted">押下時の閉じるボタン画像のファイル名</param>
	public static void SetInterstitialCloseButton(string sid, string imageName, string highlighted)
	{
		#if UNITY_IOS
		amoad_set_interstitial_close_button(sid, imageName, highlighted);
		#elif UNITY_ANDROID
		AMoAdUnityPlugin.AndroidPlugin.CallStatic("setInterstitialCloseButton", sid, imageName, highlighted);
		#endif
	}

	/// <summary>
	/// 全画面広告を表示する
	/// </summary>
	/// <param name="sid">管理画面より取得したID</param>
	public static void ShowInterstitialAd(string sid)
	{
		#if UNITY_IOS
		amoad_show_interstitial(sid);
		#elif UNITY_ANDROID
		AMoAdUnityPlugin.AndroidPlugin.CallStatic("showInterstitialAd", sid);
		#endif
	}

	/// <summary>
	/// 全画面広告を閉じる
	/// </summary>
	/// <param name="sid">管理画面より取得したID</param>
	public static void CloseInterstitialAd(string sid)
	{
		#if UNITY_IOS
		amoad_close_interstitial(sid);
		#elif UNITY_ANDROID
		AMoAdUnityPlugin.AndroidPlugin.CallStatic("closeInterstitialAd", sid);
		#endif
	}

	/// <summary>
	/// 自動リロードフラグを設定する
	/// </summary>
	/// <param name="sid">管理画面より取得したID</param>
	/// <param name="autoReload">自動リロードフラグ</param>
	public static void SetInterstitialAutoReload(string sid, bool autoReload)
	{
		#if UNITY_IOS
		amoad_set_interstitial_auto_reload(sid, autoReload);
		#elif UNITY_ANDROID
		AMoAdUnityPlugin.AndroidPlugin.CallStatic("setInterstitialAutoReload", sid, autoReload);
		#endif
	}

	/// <summary>
	/// 全画面広告をロードする
	/// </summary>
	/// <param name="sid">管理画面より取得したID</param>
	public static void LoadInterstitialAd(string sid)
	{
		#if UNITY_IOS
		amoad_load_interstitial(sid);
		#elif UNITY_ANDROID
		AMoAdUnityPlugin.AndroidPlugin.CallStatic("loadInterstitialAd", sid);
		#endif
	}

	/// <summary>
	/// 全画面広告がロードされているかどうかを判定する
	/// </summary>
	/// <param name="sid">管理画面より取得したID</param>
	public static bool IsLoadedInterstitialAd(string sid)
	{
		bool loaded = false;
		#if UNITY_IOS
		loaded = amoad_is_interstitial_loaded(sid);
		#elif UNITY_ANDROID
		loaded = AMoAdUnityPlugin.AndroidPlugin.CallStatic<bool>("isLoadedInterstitialAd", sid);
		#endif
		return loaded;
	}
}
