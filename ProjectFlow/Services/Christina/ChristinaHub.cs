using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.OpenSsl;
using ProjectFlow.BLL;
using ProjectFlow.Login;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Web.Hosting;
using System.Collections.Generic;

namespace ProjectFlow.Services.Christina
{
    /// <summary>
    /// ISignal Websocket RPC for christina chat bot
    /// </summary>
    [HubName("christina")]
    public class ChristinaHub : Hub
    {

        public async System.Threading.Tasks.Task CreateRoom(int teamID, string roomName, string roomDescription, string[] attendees)
        {

            //Get Student creating this Room
            Student student = (Context.User.Identity as ProjectFlowIdentity).Student;

            //Get ProjectTeam
            ProjectTeamBLL projectTeamBLL = new ProjectTeamBLL();
            ProjectTeam projectTeam = projectTeamBLL.GetProjectTeamByTeamID(teamID);

            //Check if student belongs to the team
            StudentBLL studentBLL = new StudentBLL();
            if (!studentBLL.HaveProjectTeam(student, projectTeam))
            {
                //Illegal access
                return;
            }

            //Must have more than one attendees
            if (attendees.Length == 0) return;

            //Must have room name
            if (String.IsNullOrEmpty(roomName)) return;

            //Generate secure password
            byte[] password = new byte[256];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(password);

            //Hash the password
            byte[] salt = File.ReadAllBytes(HostingEnvironment.MapPath("~/Services/Christina/_Keys/salt.bin"));
            byte[] passwordWithSalt = ExclusiveOR(password, salt);

            byte[] key = File.ReadAllBytes(HostingEnvironment.MapPath("~/Services/Christina/_Keys/key.bin"));

            using (HMACSHA256 hmac = new HMACSHA256(key))
            {
                byte[] hashedPasswordWithSalt = hmac.ComputeHash(passwordWithSalt);
                //Store room in database
                Room room = new Room
                {
                    accessToken = hashedPasswordWithSalt,
                    teamID = teamID,
                    creationDate = DateTime.Now,
                    createdBy = student.aspnet_Users.UserId,
                    roomName = roomName,
                    roomDescription = (String.IsNullOrEmpty(roomDescription))? null : roomDescription
                };

                //Create room
                RoomBLL bll = new RoomBLL();
                bll.CreateRoom(room);

                //Insert attendees into table
                //Changing username into userid
                aspnet_UsersBLL aspnet_UsersBLL = new aspnet_UsersBLL();
                AttendeeBLL attendeeBLL = new AttendeeBLL();
                foreach (string username in attendees)
                {
                    aspnet_Users aspnet_Users = aspnet_UsersBLL.Getaspnet_UsersByUserName(username);
                    if (aspnet_Users == null) return;
                    attendeeBLL.CreateAttendeeInRoom(room, new Attendee
                    {
                        roomID = room.roomID,
                        attendeeUserId = aspnet_Users.UserId
                    });
                }

                //Send room id to client
                Clients.Caller.SendRoomID(room.roomID);

                //Store the project id and hashed password in redis 
                Global.Redis.GetDatabase().StringSet(Convert.ToBase64String(room.accessToken), $@"{{
                        ""roomID"": ""{room.roomID}"",
                        ""teamID"": ""{room.teamID}""
                }}", new TimeSpan(0, 5, 0));

                //Encrypt the hashed password with private key

                var publicKey = readPublicKey(HostingEnvironment.MapPath("~/Services/Christina/_Keys/rsa.public"));
                var e = new Pkcs1Encoding(new RsaEngine());
                e.Init(true, publicKey);

                int length = password.Length;
                int blockSize = e.GetInputBlockSize();
                List<byte> cipherTextBytes = new List<byte>();
                for (int chunkPosition = 0;
                    chunkPosition < length;
                    chunkPosition += blockSize)
                {
                    int chunkSize = Math.Min(blockSize, length - chunkPosition);
                    cipherTextBytes.AddRange(e.ProcessBlock(
                        password, chunkPosition, chunkSize
                    ));
                }

                byte[] encryptedBytes = cipherTextBytes.ToArray();

                //Send encrypted password back to client
                Clients.Caller.SendPassword(Convert.ToBase64String(encryptedBytes));

            }


        }

        static AsymmetricKeyParameter readPublicKey(string publicKeyFileName)
        {
            RsaKeyParameters keyParameter;

            using (var reader = File.OpenText(publicKeyFileName))
                keyParameter = (RsaKeyParameters)new PemReader(reader).ReadObject();

            return keyParameter;
        }

        private byte[] ExclusiveOR(byte[] ba1, byte[] ba2)
        {
            if(ba1.Length == ba2.Length)
            {
                byte[] result = new byte[ba1.Length];

                for(int i = 0; i < ba1.Length; i++)
                {
                    result[i] = (byte) (ba1[i] ^ ba2[i]);
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