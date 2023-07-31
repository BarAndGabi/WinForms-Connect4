using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WinForms_Connect4
{
    internal class ServerSide
    {
        Connect4Game game;
        HttpClient httpClient;
        public string ip { get; set; }
        //API ADRESS : https://localhost:7148/api/ApiController/GetTest

        public ServerSide(Connect4Game game)
        {
            this.game = game;
            this.httpClient = new HttpClient();
            this.httpClient.DefaultRequestHeaders.ConnectionClose = false;

            this.ip = "localhost";
        }

        public async Task<HttpResponseMessage> getNextTurn()
        {
            string url = "https://" + this.ip + ":7148/api/b/GetServerTurn";
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

        private async Task<int> sendLoginRequestToServer(int playerId, string playerName)
        {
            string url = "https://" + this.ip + ":7148/api/loginController/login";
            var model = new LoginRequestModel
            {
                PlayerId = playerId,
                PlayerName = playerName
            };

            string jsonContent = JsonConvert.SerializeObject(model);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(url, httpContent);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                if (int.TryParse(responseBody, out int serverPlayerId))
                {
                    return serverPlayerId; // Return the server's response player ID.
                }
            }

            return -1; // Return -1 in case of an error or invalid response from the server.
        }

        internal async Task<int> PlayerLogIn()
        {
            bool notLoggedIn = true;
            int serverPlayerId = -1;
            while (notLoggedIn)
            {
                //open login form as dialog
                loginForm loginForm = new loginForm("localhost");
                loginForm.ShowDialog();
                serverPlayerId = await this.sendLoginRequestToServer(Decimal.ToInt32(loginForm.userIdInput.Value), loginForm.userNameTextBox.Text);
                if (serverPlayerId != -1)
                {
                    notLoggedIn = false;
                    return serverPlayerId;
                }
            }

            //open the login/sign up razor page

            //get player id from server after login
            //messgebox - not implemented error 
            // MessageBox.Show("PlayerLogIn(serverside) not implemented");
            return -1;
        }
    }

    // Create a model class to represent the login request data.
    public class LoginRequestModel
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
    }
}
