using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Auth.OAuth2;

using MailKit.Net.Smtp;
using MimeKit;


namespace aHoang.Services
{
    public class EmailService
    {
        public async Task<bool> SendEmail(string rcv,string subject, string body)
        {

            string _clientId= "1067509347522-t8et2j7mb9p3qge7bgburldfa8j4efhp.apps.googleusercontent.com";
            string _clientSecret= "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";
            var _refreshToken = "1//0eXNFcuxcSvzhCgYIARAAGA4SNwF-L9Irt7ewj3Q_6AVOOeoxmF65orjT-7B7zaBun4oi07NGQ43PzfB2gm256gGySM-wjNeq0uE";

            var tokenResponse = new TokenResponse
            {
                RefreshToken = _refreshToken
            };
            var credential = new UserCredential(new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = _clientId,
                    ClientSecret = _clientSecret
                }
            }), "lb3026723@gmail.com", tokenResponse);

            // Lấy access token mới
            await credential.RefreshTokenAsync(System.Threading.CancellationToken.None);
            string accessToken= credential.Token.AccessToken;

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("test", "lb3026723@gmail.com")); // Thay đổi tên và email
            message.To.Add(new MailboxAddress("", rcv));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = body };

            try
            {
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    
                    await client.AuthenticateAsync("lb3026723@gmail.com", accessToken); // Thay đổi email của bạn
                    await client.SendAsync(message, System.Threading.CancellationToken.None); 
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }


            return true;
        }
    }
}
