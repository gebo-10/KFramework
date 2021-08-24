using GameFramework.Network;
namespace LuaFramework
{
    class PacketHandler : IPacketHandler
    {
        public int Id => 0;

        public void Handle(object sender, Packet packet)
        {
            ProtoPacket p = packet as ProtoPacket;
            GameEntry.Lua.Message(MessageType.Proto, ("name", p.protoName), ("buffer", p.content));
        }
    }
}
