using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using System.Data;  
using MySql.Data.MySqlClient;  
using UnityEngine.UI;  
using UnityEngine.SceneManagement;  

namespace Database.Login
{
	public class RegisterAction : MonoBehaviour  
	{  
		public GameObject LoginWrapper;

		public InputField nameInput;  
		public InputField passwordInput;  
		public InputField emailInput;  
		public Button register;  
		public Button login;  
		public Text info;
  
		RegisterService service = new RegisterService();  

		void Start()  
		{  
			login.onClick.AddListener(Login);  
			register.onClick.AddListener(Register); 
		}  
  
		//返回登录界面
		public void Login()  
		{  
			LoginWrapper.SetActive(true);
			gameObject.SetActive(false);
			info.text = "";
		} 

		public void Register()  
		{  
			if(nameInput.text == ""){
				info.text = "请输入用户名!";
			}

			if(passwordInput.text == ""){
				info.text = "请输入密码!";
			}

			if(emailInput.text == ""){
				info.text = "请输入邮箱!";
			}

			if(nameInput.text != "" && passwordInput.text != "" && emailInput.text != ""){
				info.text = service.Service(nameInput.text, passwordInput.text, emailInput.text);  
			}
		}  
	}  
}
