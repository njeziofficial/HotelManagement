using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.States
{
    public enum RoomState
    {
        Free = 1,
        Occupied,
        Reserved,
        Cleaned,
        Unavailable
    }

    public enum Client
    {
        Registered,
        BookedRoom,
        Check_In,
        Check_Out
    }

}
