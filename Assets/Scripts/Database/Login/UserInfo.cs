using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;

namespace Database.Login
{
	public class UserInfo
    {  
    	public int Id { get; set; }  
    	public string Name { get; set; }  
    	public string Psw { get; set; }  
    	public string Email { get; set; }  
      
    	public UserInfo()  
    	{  

    	}  
      
    	public UserInfo(string name, string psw, string email)  
    	{  
    		this.Name = name;  
    		this.Psw = psw; 
    		this.Email = email;  
    	}  
    }
}

