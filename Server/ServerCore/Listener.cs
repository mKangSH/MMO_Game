﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerCore
{
    public class Listener
    {
        Socket _listenerSocket;
        Func<Session> _sessionFactory;

        public void Init(IPEndPoint endPoint, Func<Session> sessionFactory, int register = 10, int backlog = 100)
        {
            _listenerSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            _sessionFactory += sessionFactory;

            // Bind
            _listenerSocket.Bind(endPoint);

            // 최대 대기수(10)
            _listenerSocket.Listen(backlog);

            for(int i = 0; i < register; i++) 
            {
                SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                args.Completed += new EventHandler<SocketAsyncEventArgs>(OnAcceptCompleted);
                RegisterAccept(args);
            }
        }

        void RegisterAccept(SocketAsyncEventArgs args)
        {
            args.AcceptSocket = null;

            bool pending = _listenerSocket.AcceptAsync(args);
            if (pending == false)
            {
                OnAcceptCompleted(null, args);
            }
        }

        void OnAcceptCompleted(object? sender, SocketAsyncEventArgs args)
        {
            if(args.SocketError == SocketError.Success)
            {
                Session session = _sessionFactory.Invoke();
                session.Start(args.AcceptSocket);
                session.OnConnected(args.AcceptSocket.RemoteEndPoint);
            }

            else
            {
                Console.WriteLine(args.SocketError.ToString());
            }

            RegisterAccept(args);
        }
    }
}
