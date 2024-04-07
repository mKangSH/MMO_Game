using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Game
{
    public class Arrow : Projectile
    {
        // 피아식별을 위해서, Stat 등을 참고하여 데미지 계산을 하기 위해
        public GameObject Owner { get; set; }

        long _nextMoveTick = 0;

        public override void Update()
        {
            if(Owner == null || Room == null)
            {
                return;
            }
                
            if(_nextMoveTick >= Environment.TickCount64)
            {
                return;
            }

            _nextMoveTick = Environment.TickCount64 + 50;

            Vector2Int destPos = GetFrontCellPos();
            if(Room.Map.CanGo(destPos))
            {
                CellPos = destPos;

                S_Move movePacket = new S_Move();
                movePacket.ObjectId = Id;
                movePacket.PosInfo = PosInfo;
                Room.Broadcast(movePacket);

                Console.WriteLine("Move Arrow");
            }
            else
            {
                GameObject target = Room.Map.Find(destPos);
                if(target != null) 
                {
                    // TODO : 피격 판정
                }

                // 소멸
                Room.LeaveGame(Id);
            }
        }
    }
}
