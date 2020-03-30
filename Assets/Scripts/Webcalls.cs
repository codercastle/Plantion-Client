using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net.Http;
using System;

public class Webcalls 
{
    /// <summary>
    /// URL of the API
    /// </summary>
    private const string URL = "http://localhost:4000/";

    /// <summary>
    /// Client used to connect to the API
    /// </summary>
    private HttpClient client;

    public Webcalls()
    {
        client = new HttpClient
        {
            BaseAddress = new Uri(URL) 
        };
    }

    public string Login(User user)
    {
        try
        {
            HttpResponseMessage response = client.PostAsJsonAsync("api/user/register", user).Result;
            return response.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            return ex.Message;
        }
    }
}
