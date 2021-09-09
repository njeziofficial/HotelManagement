using System;
using System.Collections.Generic;
using System.Text;
using Stateless;

namespace BusinessLogicLayer.States
{
   public class State_Machine
    {

        private readonly StateMachine<RoomState, Client> _stateMachine;
        public State_Machine(RoomState room)
        {
            //_stateMachine = new StateMachine<Room, Client>(room);
            _stateMachine = new StateMachine<RoomState, Client>(() => room, s => room = s);

            _stateMachine.Configure(RoomState.Free)
                .PermitIf(Client.BookedRoom, RoomState.Reserved, )
                .Permit(Client.Check_In, RoomState.Occupied)
                .PermitReentry(Client.Check_Out);
            //Email should be fired here too.

            _stateMachine.Configure(RoomState.Cleaned)
                .Ignore(Client.Check_In)
                .Permit(Client.BookedRoom, RoomState.Reserved)
                .PermitReentry(Client.Registered);


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

        public void Registered()
        {
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

        public void Check_Out()
        {
            _stateMachine.Fire(Client.Check_Out);
        }
    }
}
