using DarkRift;

namespace MeatInc.ActionGunnersShared.Character
{
    public struct CharacterDespawnData : IDarkRiftSerializable
    {
        public ushort ClientId { get; private set; }
        public CharacterDespawnData(ushort playerId)
        {
            ClientId = playerId;
        }
        public void Deserialize(DeserializeEvent e)
        {
            ClientId = e.Reader.ReadUInt16();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(ClientId);
        }
    }
}
