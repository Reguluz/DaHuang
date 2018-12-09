using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using System.Data;  
using MySql.Data.MySqlClient;

namespace Database.Login
{
	public class RegisterService  
    {  
    	public string Service(string name, string password, string email)  
    	{  
    		string sql = "insert into userinfo(name,password,email) values('{0}','{1}','{2}');";//userInfo
            string gamesql = "insert into gameinformation(money, playerHp, playerSpeed, playerArmor, fortressHp) values('{0}','{1}','{2}','{3}','{4}');";//gameinformation
    		sql = string.Format(sql, name, password, email);//还是要赋值
            gamesql = string.Format(gamesql, "500","0","0","0","0");
    		try  
    		{  
    			MySqlConnection conn = GetConnection();  
    			MySqlCommand comd = new MySqlCommand(sql, conn);
    			comd.ExecuteNonQuery(); 

			    MySqlCommand comd2 = new MySqlCommand(gamesql, conn);
                comd2.ExecuteNonQuery();
    			return "注册成功";  
    		}  
    		catch (System.Exception e)  
    		{  
    			Debug.Log(e.Message);  
    			return "注册失败";
    		}  
    	} 
	    
      
    	public MySqlConnection GetConnection()  
    	{  
		    MySqlConnection conn = new MySqlConnection("Server=bj-cdb-rhhshji8.sql.tencentcdb.com;Port=63254;UserId=root;Password=LIzhen123;Database=shj");
    		conn.Open();  
    		return conn;
    	}  
      
    }  
}

