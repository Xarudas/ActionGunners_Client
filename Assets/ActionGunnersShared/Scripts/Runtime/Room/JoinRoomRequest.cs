using DarkRift;

namespace MeatInc.ActionGunnersShared.Room
{
    public struct JoinRoomRequest : IDarkRiftSerializable
    {
        public ushort Id { get; private set; }

        public JoinRoomRequest(ushort id)
        {
            Id = id;
        }

        public void Deserialize(DeserializeEvent e)
        {
            Id = e.Reader.ReadUInt16();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Id);
        }
    }
}
