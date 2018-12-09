using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;
using MySql.Data.MySqlClient;
using MySql.Data;

namespace Database.Login
 {
	public class LoginService  
    {  
	    public static int userId;
    	public UserInfo Service(string name, string password)  
    	{  
    		UserInfo user = null;

    		string sql = "select * from userinfo where name='{0}'and password='{1}';";//userInfo  
    		sql = string.Format(sql, name, password);
    		try
    		{  
    			MySqlConnection conn = GetConnection();
    			MySqlCommand comd = new MySqlCommand(sql, conn);  
    			MySqlDataReader reader = comd.ExecuteReader();
    			if (reader.Read())
    			{  
    				user = new UserInfo();  
    				user.Id = reader.GetInt32(0);
    				user.Name = reader.GetString(1);
    				user.Psw = reader.GetString(2);
    				user.Email = reader.GetString(3);
    			}
			    userId = user.Id;
    			reader.Close();  
    			return user; 
    		}
    		catch (System.Exception e)  
    		{  
    			Debug.Log(e.Message);  
    			return null;  
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
