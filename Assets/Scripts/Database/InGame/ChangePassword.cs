using System;
using Database.Login;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;

namespace Database.InGame
{
    public class ChangePassword : MonoBehaviour {
        
        private int _userId = LoginService.userId;
        
        public InputField NewPasswordInput;
        public InputField RePasswordInput;
        public Button okButton; 
        public Text warning;
        
        // Use this for initialization
        void Start () {
            okButton.onClick.AddListener(Channge);
        }
	
        // Update is called once per frame
        void Update () {
		
        }
        
        void Channge()
        {
            MySqlConnection conn = GetConnection();
            MySqlCommand cmd;
            String newPwd = NewPasswordInput.text;

            if (newPwd != "" && newPwd == RePasswordInput.text )
            {
                cmd = new MySqlCommand("update userinfo set password = "+newPwd+" where userId ='"+_userId+"';", conn);
                cmd.ExecuteNonQuery();
                warning.text = "";
            }
            else
            {
                warning.text = "两次输入的密码不一致！";
            }
            
            if (RePasswordInput.text == "")
            {
                warning.text = "请重复输入新的密码！";
            }
            
            if (newPwd == "")
            {
                warning.text = "请输入新的密码！";
            }
        }
        
        public MySqlConnection GetConnection()  
        {  
            MySqlConnection conn = new MySqlConnection("Server=localhost;UserId=root;Password=;Database=shj");  
            conn.Open();  
            return conn;
        }  
    }

}