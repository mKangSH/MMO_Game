﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyClient
{
    internal class SessionManager
    {
        static SessionManager _instance = new SessionManager();

        public static SessionManager Instance 
        { 
            get {  return _instance; } 
        }

        List<ServerSession> _sessions = new List<ServerSession>();
        object _lock = new object();
        Random _random = new Random();

        public void SendForEach()
        {
            lock (_lock)
            {
                foreach(ServerSession session in _sessions)
                {
                    C_Move movePacket = new C_Move();
                    movePacket.posX = _random.Next(-50, 50);
                    movePacket.posY = 0;
                    movePacket.posZ = _random.Next(-50, 50);

                    session.Send(movePacket.Write());
                }
            }
        }

        public ServerSession Generate()
        {
            lock(_lock)
            {
                ServerSession session = new ServerSession();
                _sessions.Add(session);
                return session;
            }
        }
    }
}
