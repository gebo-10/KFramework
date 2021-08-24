using GameFramework.Network;
namespace LuaFramework
{
    public class ProtoPacket : Packet
    {
        public int packetLength;
        public string protoName;
        public byte[] content;

        public override int Id => 0;

        public override void Clear(){}
    }
}
