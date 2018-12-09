using System.Collections;
using System.Collections.Generic;
using Database.Login;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;

namespace Database.InGame
{
	public class UserMenu : MonoBehaviour {
		
		public Text MoneyText;
		public Text PlayerHpText;
		public Text PlayerSpeedText;
		public Text PlayerArmorText;
		public Text FortressHpText;
		
		public Text Warning;

		private bool _mNeedMove = false;

		private int userId = LoginService.userId;


		void Start () {
			
			UserMenu user = new UserMenu();
			GameInformation gameinfo = user.GetInfo(userId);

			Debug.Log(userId);

			MoneyText.text = gameinfo.Money + "";
			PlayerHpText.text = gameinfo.PlayerHp + "";
			PlayerSpeedText.text = gameinfo.PlayerSpeed + "";
			PlayerArmorText.text = gameinfo.PlayerArmor + "";
			FortressHpText.text = gameinfo.FortressHp + "";
		}

		void Update(){
			if (_mNeedMove)
            {
            	UserMenu user = new UserMenu();
				GameInformation gameinfo = user.GetInfo(userId);
            	MoneyText.text = gameinfo.Money + "";
				PlayerHpText.text = gameinfo.PlayerHp + "";
				PlayerSpeedText.text = gameinfo.PlayerSpeed + "";
				PlayerArmorText.text = gameinfo.PlayerArmor + "";
				FortressHpText.text = gameinfo.FortressHp + "";
	            _mNeedMove = false;
				return;
            }
		}

		public GameInformation GetInfo(int userid)
		{
			GameInformation gameinfo = new GameInformation();

			MySqlConnection conn = GetConnection(); 
			MySqlCommand cmd = new MySqlCommand("select * from gameinformation where userId ='"+userid+"';", conn);
			MySqlDataReader reader = cmd.ExecuteReader();
			// 逐行读取数据
			while (reader.Read())
			{
				gameinfo.Money = reader.GetInt32("money");
				gameinfo.PlayerHp = reader.GetInt32("playerHp");
				gameinfo.PlayerSpeed = reader.GetInt32("playerSpeed");
				gameinfo.PlayerArmor = reader.GetInt32("playerArmor");
				gameinfo.FortressHp  = reader.GetInt32("fortressHp");
			}
			reader.Close();
			return gameinfo;
		}

		public void SetInfo(int value)
        {
        	MySqlConnection conn = GetConnection();
        	GameInformation gameinfo = GetInfo(userId);
        	MySqlCommand cmd;
	        int money = gameinfo.Money;
        	int playerHpNum = gameinfo.PlayerHp;
        	int playerSpeedNum = gameinfo.PlayerSpeed;
        	int playerArmorNum = gameinfo.PlayerArmor;
        	int fortressHpNum = gameinfo.FortressHp;

	        if (money >= 50)
	        {
		        switch (value)
		        {
			        case 1:
				        if (playerHpNum <= 6)
				        {
					        playerHpNum++;
					        money -= 50;
					        cmd = new MySqlCommand("update gameinformation set Money = "+money+", playerHp = "+playerHpNum+" where userId ='"+userId+"';", conn);
					        cmd.ExecuteNonQuery();
				        }
				        else
				        {
					        Warning.text = "该等级已满！";
				        }
				        break;
			        case 2:
				        if (playerHpNum <= 6)
					    {
					        playerSpeedNum++;
						    money -= 50;
					        cmd = new MySqlCommand("update gameinformation set Money = "+money+", playerSpeed = "+playerSpeedNum+" where userId ='"+userId+"';", conn);
					        cmd.ExecuteNonQuery();
				        }
				        else
				        {
					        Warning.text = "该等级已满！";
				        }
				        break;
			        case 3:
				        if (playerHpNum <= 6)
				        {
					        money -= 50;
					        playerArmorNum++;
					        cmd = new MySqlCommand("update gameinformation set Money = "+money+", playerArmor = "+playerArmorNum+" where userId = '"+userId+"';", conn);
					        cmd.ExecuteNonQuery();
				        }
				        else
				        {
					        Warning.text = "该等级已满！";
				        }
				        break;
			        case 4:
				        if (playerHpNum <= 6)
				        {
					        money -= 50;
					        fortressHpNum++;
					        cmd = new MySqlCommand("update gameinformation set Money = "+money+", fortressHp = "+fortressHpNum+" where userId ='"+userId+"';", conn);
					        cmd.ExecuteNonQuery();
				        }
				        else
				        {
					        Warning.text = "该等级已满！";
				        }

				        break;
			        case 5:
				        //int playerHpNum = gameinfo.PlayerHp + 1;
				        //sql = "update gameinformation set playerHp = "+playerHpNum+" where userId ='"+userId+"';";
				        break;
			        default:
				        Debug.LogError("!!!!!");
				        break;
		        }
	        }
	        else
	        {
		        Warning.text = "玉符不足！";
	        }

	        _mNeedMove = true;
        }
		
		public MySqlConnection GetConnection()  
		{  
			MySqlConnection conn = new MySqlConnection("Server=bj-cdb-rhhshji8.sql.tencentcdb.com;Port=63254;UserId=root;Password=LIzhen123;Database=shj");  
			conn.Open();  
			return conn;
		}  
	}

}