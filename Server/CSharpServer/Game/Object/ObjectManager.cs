using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Game
{
    public class ObjectManager
    {
        object _lock = new object();

        public static ObjectManager Instance { get; } = new ObjectManager();

        // 귓속말 등 다른 플레이어를 찾고 싶어서
        // 이동 등의 처리는 GameRoom에서 처리
        Dictionary<int, Player> _players = new Dictionary<int, Player>();

        // TODO : BitFlag 식으로 만들 수 있음
        int _counter = 0;

        public T Add<T>() where T : GameObject, new()
        {
            T gameObject = new T();

            lock (_lock)
            {
                gameObject.Id = GenerateId(gameObject.ObjectType);

                if (gameObject.ObjectType == GameObjectType.Player)
                {
                    _players.Add(gameObject.Id, gameObject as Player);
                }
            }

            return gameObject;
        }

        int GenerateId(GameObjectType type)
        {
            return ((int)type << 24 | (_counter++));
        }

        // TODO : id가 음수인 경우 2의 보수로 계산되어 원하지 않은 결과가 나올 수 있음.
        public static GameObjectType GetObjectTypeById(int id)
        {
            int type = (id >> 24) & 0x7F;

            return (GameObjectType)type;
        }

        public bool Remove(int objectId)
        {
            GameObjectType objectType = GetObjectTypeById(objectId);

            lock (_lock)
            {
                if (objectType == GameObjectType.Player)
                {
                    return _players.Remove(objectId);
                }
            }

            return false;
        }

        public Player Find(int objectId)
        {
            GameObjectType objectType = GetObjectTypeById(objectId);
            lock (_lock)
            {
                if (objectType == GameObjectType.Player)
                {
                    Player player = null;
                    if (_players.TryGetValue(objectId, out player))
                    {
                        return player;
                    }
                }

                return null;
            }
        }
    }
}
