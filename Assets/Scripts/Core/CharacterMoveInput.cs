using Unity.Netcode;

namespace DZM.Core
{
    public struct CharacterMoveInput : INetworkSerializable
    {
        public float MoveX;
        public float MoveY;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer)
            where T : IReaderWriter
        {
            serializer.SerializeValue(ref MoveX);
            serializer.SerializeValue(ref MoveY);
        }
    }
}