using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Database.Login
{
	public class LoginAction : MonoBehaviour
	{
		public GameObject EnrollWrapper;
		public GameObject NameNotExistWrapper;

		public InputField usernameInput;
		public InputField passwordInput;
		public Button loginBtn;
		public Button register;
		public Text info;

		public Button okButton;

		LoginService service = new LoginService();

		// Use this for initialization
		void Start()
		{
			loginBtn.onClick.AddListener(Login);
			register.onClick.AddListener(Register);
			okButton.onClick.AddListener(Confirm);
		}

		public void Login()
		{
			//通知service层 去处理
			UserInfo user = service.Service(usernameInput.text, passwordInput.text);
			print(user);
			if (user == null)
			{
				NameNotExistWrapper.SetActive(true);
				okButton.gameObject.SetActive(true);
				gameObject.SetActive(false);
			}
			else
			{
				info.text = "登录成功!";
				SceneManager.LoadScene("UserMenu");
			}
		}

		public void Register()
		{
			EnrollWrapper.SetActive(true);
			gameObject.SetActive(false);
		}

		public void Confirm(){
			NameNotExistWrapper.SetActive(false);
			okButton.gameObject.SetActive(false);
			gameObject.SetActive(true);
		}
	}
}

