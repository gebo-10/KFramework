using System;
using System.IO;
using System.Net;
using System.Text;
using GameFramework.Event;
using GameFramework.Network;
using UnityGameFramework.Runtime;

namespace LuaFramework
{
    public class PacketHead : IPacketHeader
    {
        public int len;
        public int PacketLength => len;
        public PacketHead(int length)
        {
            len = length;
        }
    }
    public class NetworkHelper : INetworkChannelHelper
    {
        
        private INetworkChannel m_NetworkChannel = null;
        // <summary>
        /// 获取消息包头长度。
        /// </summary>
        public int PacketHeaderLength => 4;

        public void Initialize(INetworkChannel networkChannel)
        {
            m_NetworkChannel = networkChannel;
            networkChannel.HeartBeatInterval = 3;
            networkChannel.ResetHeartBeatElapseSecondsWhenReceivePacket = true;
            GameEntry.Event.Subscribe(UnityGameFramework.Runtime.NetworkConnectedEventArgs.EventId, OnNetworkConnected);
            GameEntry.Event.Subscribe(UnityGameFramework.Runtime.NetworkClosedEventArgs.EventId, OnNetworkClosed);
            GameEntry.Event.Subscribe(UnityGameFramework.Runtime.NetworkMissHeartBeatEventArgs.EventId, OnNetworkMissHeartBeat);
            GameEntry.Event.Subscribe(UnityGameFramework.Runtime.NetworkErrorEventArgs.EventId, OnNetworkError);
            GameEntry.Event.Subscribe(UnityGameFramework.Runtime.NetworkCustomErrorEventArgs.EventId, OnNetworkCustomError);
        }

        public void PrepareForConnecting()
        {
            m_NetworkChannel.Socket.ReceiveBufferSize = 1024 * 64;
            m_NetworkChannel.Socket.SendBufferSize = 1024 * 64;
        }

        public bool SendHeartBeat()
        {
            return true;
        }

        public Packet DeserializePacket(IPacketHeader packetHeader, Stream source, out object customErrorData)
        {
            customErrorData = null;
            ProtoPacket packet = new ProtoPacket();
            packet.packetLength = packetHeader.PacketLength;

            var reader = new BinaryReader(source);
            short len = (short)reader.ReadInt16();
            len = IPAddress.NetworkToHostOrder(len);

            byte[] str = new byte[len];
            str = reader.ReadBytes(len);
            packet.protoName = Encoding.UTF8.GetString(str);

            packet.content= reader.ReadBytes((int)(source.Length - source.Position));
            return packet;
        }

        public IPacketHeader DeserializePacketHeader(Stream source, out object customErrorData)
        {
            var reader = new BinaryReader(source);
            int messageLen = reader.ReadInt32();
            messageLen = IPAddress.NetworkToHostOrder(messageLen);
            customErrorData = null;
            return new PacketHead(messageLen);
        }


        public bool Serialize<T>(T packet, Stream destination) where T : Packet
        {
            //destination.Position = 0;
            ProtoPacket p = packet as ProtoPacket;

            ByteBuffer tmp = new ByteBuffer();
            tmp.WriteStringProto(p.protoName);
            tmp.WriteBytesPure(p.content);

            var writer = new BinaryWriter(destination);
            var tmpBytes = tmp.ToBytes();
            UInt32 msglen = (UInt32)tmpBytes.Length;
            writer.Write((int)IPAddress.HostToNetworkOrder((int)msglen));
            writer.Write(tmpBytes);
            writer.Flush();
            //destination.Position = 0;

            return true;
        }

        public void Shutdown()
        {
            GameEntry.Event.Unsubscribe(UnityGameFramework.Runtime.NetworkConnectedEventArgs.EventId, OnNetworkConnected);
            GameEntry.Event.Unsubscribe(UnityGameFramework.Runtime.NetworkClosedEventArgs.EventId, OnNetworkClosed);
            GameEntry.Event.Unsubscribe(UnityGameFramework.Runtime.NetworkMissHeartBeatEventArgs.EventId, OnNetworkMissHeartBeat);
            GameEntry.Event.Unsubscribe(UnityGameFramework.Runtime.NetworkErrorEventArgs.EventId, OnNetworkError);
            GameEntry.Event.Unsubscribe(UnityGameFramework.Runtime.NetworkCustomErrorEventArgs.EventId, OnNetworkCustomError);
        }

        private void OnNetworkConnected(object sender, GameEventArgs e)
        {
            UnityGameFramework.Runtime.NetworkConnectedEventArgs ne = (UnityGameFramework.Runtime.NetworkConnectedEventArgs)e;
            if (ne.NetworkChannel != m_NetworkChannel)
            {
                return;
            }

            Log.Info("Network channel '{0}' connected, local address '{1}', remote address '{2}'.", ne.NetworkChannel.Name, ne.NetworkChannel.Socket.LocalEndPoint.ToString(), ne.NetworkChannel.Socket.RemoteEndPoint.ToString());

            GameEntry.Lua.Message(MessageType.OnConnect, ("channel", ne.NetworkChannel));
        }

        private void OnNetworkClosed(object sender, GameEventArgs e)
        {
            UnityGameFramework.Runtime.NetworkClosedEventArgs ne = (UnityGameFramework.Runtime.NetworkClosedEventArgs)e;
            if (ne.NetworkChannel != m_NetworkChannel)
            {
                return;
            }

            Log.Info("Network channel '{0}' closed.", ne.NetworkChannel.Name);
            GameEntry.Lua.Message(MessageType.OnDisconnect, ("channel", ne.NetworkChannel));
        }

        private void OnNetworkMissHeartBeat(object sender, GameEventArgs e)
        {
            UnityGameFramework.Runtime.NetworkMissHeartBeatEventArgs ne = (UnityGameFramework.Runtime.NetworkMissHeartBeatEventArgs)e;
            if (ne.NetworkChannel != m_NetworkChannel)
            {
                return;
            }

            Log.Info("Network channel '{0}' miss heart beat '{1}' times.", ne.NetworkChannel.Name, ne.MissCount.ToString());

            if (ne.MissCount < 2)
            {
                return;
            }

           // ne.NetworkChannel.Close();
        }

        private void OnNetworkError(object sender, GameEventArgs e)
        {
            UnityGameFramework.Runtime.NetworkErrorEventArgs ne = (UnityGameFramework.Runtime.NetworkErrorEventArgs)e;
            if (ne.NetworkChannel != m_NetworkChannel)
            {
                return;
            }

            Log.Info("Network channel '{0}' error, error code is '{1}', error message is '{2}'.", ne.NetworkChannel.Name, ne.ErrorCode.ToString(), ne.ErrorMessage);

            //ne.NetworkChannel.Close();
            if(ne.ErrorCode == NetworkErrorCode.ConnectError)
            {
                GameEntry.Lua.Message(MessageType.OnConnectFail, ("channel", ne.NetworkChannel));
            }
            {
                GameEntry.Lua.Message(MessageType.OnException, ("channel", ne.NetworkChannel));
            }
            
        }

        private void OnNetworkCustomError(object sender, GameEventArgs e)
        {
            UnityGameFramework.Runtime.NetworkCustomErrorEventArgs ne = (UnityGameFramework.Runtime.NetworkCustomErrorEventArgs)e;
            if (ne.NetworkChannel != m_NetworkChannel)
            {
                return;
            }
        }
    }
}