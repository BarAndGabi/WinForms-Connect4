﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Net.WebRequestMethods;

namespace WinForms_Connect4
{
    internal class ServerSide
    {
        Connect4Game game;
        HttpClient httpClient;
        //API ADRESS : https://localhost:7148/api/ApiController/GetTest

        public ServerSide(Connect4Game game)
        {
            this.game = game;
            this.httpClient = new HttpClient();
            
        }
        public  async Task<HttpResponseMessage>  getNextTurn()
        {
            string url = "https://localhost:7148/api/b/GetServerTurn";
            string boardJson = this.game.BoardToJson();
              return await httpClient.PostAsJsonAsync(url, boardJson);
              
        }
    
        // Test function to send GET request to API and check if it returns a 200 OK status code and the response body contains the expected value (66).
        public async Task<bool> TestGetRequest()
        {
            string url = "https://localhost:7148/api/b/GetTest";
            HttpResponseMessage response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                //messagebox of response body 
                MessageBox.Show(responseBody);
                if (responseBody.Contains("66"))
                {
              
                    return true; // The test passed.
                }
            }

            return false; // The test failed.
        }

        internal int PlayerLogIn()
        {
            bool notLoggedIn = true;
            int serverPlayerId = -1;
            while (notLoggedIn)
            {
                //open login form as dialog
                loginForm loginForm = new loginForm();
                loginForm.ShowDialog();
                serverPlayerId=this.sendLoginRequestToServer(loginForm.userIdInput.Value,loginForm.userNameTextBox.Text);
                if (serverPlayerId != -1)
                {
                    notLoggedIn=false;
                }
            }
          

            //open the login/sign up razor page

            //get player id from server after login
            //messgebox - not implemented error 
            MessageBox.Show("PlayerLogIn(serverside) not implemented");
            return -1;

        }

        private int sendLoginRequestToServer(decimal value, string text)
        {
            throw new NotImplementedException();
        }
    }
}
