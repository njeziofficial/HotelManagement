using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.Models;
using Stateless;

namespace BusinessLogicLayer
{
   public class State_Machine
    {

        private readonly StateMachine<RoomState, Client> _stateMachine;
        public State_Machine(RoomState room)
        {
            //_stateMachine = new StateMachine<Room, Client>(room);
            _stateMachine = new StateMachine<RoomState, Client>(() => room, s => room = s);

            _stateMachine.Configure(RoomState.Free)
                .Permit(Client.BookedRoom, RoomState.Reserved)
                .Permit(Client.Registered, RoomState.Reserved)
                .Permit(Client.Registered, RoomState.Occupied)
                .Permit(Client.Check_In, RoomState.Occupied)
                .PermitReentry(Client.Check_Out);
            //Email should be fired here too.

            _stateMachine.Configure(RoomState.Cleaned)
                .Ignore(Client.Check_In)
                .Permit(Client.BookedRoom, RoomState.Reserved)
                .PermitReentry(Client.Check_Out);


            _stateMachine.Configure(RoomState.Occupied)
                .Permit(Client.Check_Out, RoomState.Free)
                .Ignore(Client.BookedRoom)
                .PermitReentry(Client.Check_In);


            _stateMachine.Configure(RoomState.Reserved)
                .Permit(Client.Check_In, RoomState.Occupied)
                .Ignore(Client.Check_Out)
                .PermitReentry(Client.BookedRoom);
            //Email should be fired here too.

            _stateMachine.Configure(RoomState.Unavailable)
                .Ignore(Client.BookedRoom)
                .Ignore(Client.Check_In)
                .Ignore(Client.Check_Out);

        }

        public void Registered(DataAccessLayer.Models.User user)
        {
            if(user.ClientState == (int)Client.Registered)
            {
                SendNotificationEmailRequest reqEmail = new SendNotificationEmailRequest
                {
                    MailTo = new List<string>(),
                    Config = new EmailConfig(),
                    Credentials = new EmailCredentials(),
                    MailFrom = "Dandi and Sons Hotels",
                    MailWebsite = $"https://www.dandiandsons.ng"
                };
                reqEmail.Config.Host = "from database";
                reqEmail.Config.Port = 12345;
                reqEmail.Credentials.UserName = "contact@dandiandsons.com";
                reqEmail.Credentials.Password = "from webConfig";
                reqEmail.MailTo.Add(user.Email);
                reqEmail.Subject = "Welcome to ";
                UserBLL.SendNotificationEmail(reqEmail);
                user.isEmailSent = true;
            }
            _stateMachine.Fire(Client.Registered);
        }

        public void BookedRoom()
        {
            _stateMachine.Fire(Client.BookedRoom);
        }

        public void Check_In()
        {
            _stateMachine.Fire(Client.Check_In);
        }

        public void Check_Out(User user)
        {
            if (user.ClientState == (int)Client.Check_Out)
            {
                SendNotificationEmailRequest reqEmail = new SendNotificationEmailRequest
                {
                    MailTo = new List<string>(),
                    Config = new EmailConfig(),
                    Credentials = new EmailCredentials(),
                    MailFrom = "Dandi and Sons Hotels",
                    MailWebsite = $"https://www.dandiandsons.ng"
                };
                reqEmail.Config.Host = "from database";
                reqEmail.Config.Port = 12345;
                reqEmail.Credentials.UserName = "contact@dandiandsons.com";
                reqEmail.Credentials.Password = "from webConfig";
                reqEmail.MailTo.Add(user.Email);
                reqEmail.Subject = "Bye bye from ";
                UserBLL.SendNotificationEmail(reqEmail);
                user.isEmailSent = true;
            }
            _stateMachine.Fire(Client.Check_Out);
        }
    }
}
