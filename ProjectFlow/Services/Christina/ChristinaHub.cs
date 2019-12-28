using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.OpenSsl;
using ProjectFlow.BLL;
using ProjectFlow.Login;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Web.Hosting;

namespace ProjectFlow.Services.Christina
{
    /// <summary>
    /// ISignal Websocket RPC for christina chat bot
    /// </summary>
    [HubName("christina")]
    public class ChristinaHub : Hub
    {

        public async System.Threading.Tasks.Task CreateRoom(string projectID)
        {
            //Generate secure password
            byte[] password = new byte[256];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(password);

            //Hash the password
            byte[] salt = File.ReadAllBytes(HostingEnvironment.MapPath("~/Services/Christina/salt.bin"));
            byte[] passwordWithSalt = ExclusiveOR(password, salt);

            byte[] key = new byte[256];
            rng.GetBytes(key);

            using (HMACSHA256 hmac = new HMACSHA256(key))
            {
                byte[] hashedPasswordWithSalt = hmac.ComputeHash(passwordWithSalt);
                //Store room in database
                Room room = new Room
                {
                    accessToken = hashedPasswordWithSalt,
                    projectID = projectID,
                    creationDate = DateTime.Now,
                    createdBy = (Context.User.Identity as ProjectFlowIdentity).Student.studentID,
                    
                };
                RoomBLL bll = new RoomBLL();
                bll.CreateRoom(room);

                //Store the project id and hashed password in redis 
                Global.Redis.GetDatabase().StringSet(room.accessToken, $@"{{
                        'roomID': '{room.roomID}',
                        'projectID': '{room.projectID}'
                }}", new TimeSpan(0, 1, 0));

                //Encrypt the hashed password with private key

                var publicKey = readPublicKey(HostingEnvironment.MapPath("~/Services/Christina/rsa.public"));
                RsaEngine e = new RsaEngine();
                e.Init(true, publicKey);

                byte[] encryptedBytes = e.ProcessBlock(password, 0, password.Length);

                //Send encrypted password back to client
                Clients.Caller.SendPassword(Convert.ToBase64String(encryptedBytes));

            }


        }

        static AsymmetricKeyParameter readPublicKey(string publicKeyFileName)
        {
            AsymmetricKeyParameter keyParameter;

            using (var reader = File.OpenText(publicKeyFileName))
                keyParameter = (AsymmetricKeyParameter)new PemReader(reader).ReadObject();

            return keyParameter;
        }

        private byte[] ExclusiveOR(byte[] ba1, byte[] ba2)
        {
            if(ba1.Length == ba2.Length)
            {
                byte[] result = new byte[ba1.Length];

                for(int i = 0; i < ba1.Length; i++)
                {
                    result[i] = (byte) (ba1[i] ^ ba2[2]);
                }
                return result;
            }
            else
            {
                throw new ArgumentException("Invalid length");
            }
        }
    }
}