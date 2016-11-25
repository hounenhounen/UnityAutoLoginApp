using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using NCMB;

//private InputField UserName;
//private InputField PassWord;

public class Loginsignin : MonoBehaviour
{
	public InputField UserName;
	public InputField PassWord;
	public Text Status;
	// Use this for initialization
	void Start ()
	{
		if (PlayerPrefs.GetString ("Id") == "" && PlayerPrefs.GetString ("PW") == "") {
			Dictionary<string, string> accountData = new Dictionary<string, string> ();
			accountData = GenerateIdPW ();
			UserName.text = accountData ["Id"];
			PassWord.text = accountData ["PW"];
			Status.text = "Generate Id&PW";
		} else {
			UserName.text = PlayerPrefs.GetString ("Id");
			PassWord.text = PlayerPrefs.GetString ("PW");
			Status.text = "Read Id&PW From PlayerPrefs";

		}

	}

	// Update is called once per frame
	void Update ()
	{

	}

	public void Login ()
	{
		print (UserName.text);
		print (PassWord.text);

		// ユーザー名とパスワードでログイン
		NCMBUser.LogInAsync (UserName.text, PassWord.text, (NCMBException e) => {
			if (e != null) {
				UnityEngine.Debug.Log ("ログインに失敗: " + e.ErrorMessage);
			} else {
				UnityEngine.Debug.Log ("ログインに成功！");
				Application.LoadLevel ("LogOut");
			}
		});

	}

	public void Signin ()
	{
		print (UserName.text);
		print (PassWord.text);


		//NCMBUserのインスタンス作成
		NCMBUser user = new NCMBUser ();

		//ユーザ名とパスワードの設定
		user.UserName = UserName.text;
		user.Password = PassWord.text;

		//会員登録を行う
		user.SignUpAsync ((NCMBException e) => {
			if (e != null) {
				UnityEngine.Debug.Log ("新規登録に失敗: " + e.ErrorMessage);
			} else {
				UnityEngine.Debug.Log ("新規登録に成功");
				PlayerPrefs.SetString ("Id",UserName.text);
				PlayerPrefs.SetString ("PW",PassWord.text);
				PlayerPrefs.Save();
				Application.LoadLevel ("LogOut");
			}
		});

	}
  //Id,PWの自動生成箇所
	public Dictionary<string,string> GenerateIdPW(){
		Dictionary<string, string> accountData = new Dictionary<string, string> ();
		System.Guid idGuid=System.Guid.NewGuid();
		System.Guid pwGuid=System.Guid.NewGuid();
		string Id_uuid=idGuid.ToString();
		string PW_uuid=pwGuid.ToString();
		accountData.Add ("Id",Id_uuid);
		accountData.Add ("PW",PW_uuid);
		return accountData;
	}

	public void DeletePlayerPrefs(){
		PlayerPrefs.DeleteAll();
		UserName.text = "";
		PassWord.text = "";
		Debug.Log("Delete All Data Of PlayerPrefs!!");
	}
}
