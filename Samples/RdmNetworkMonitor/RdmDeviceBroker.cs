using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using Acn.Sockets;
using Acn.Rdm;
using Acn.Rdm.Packets;
using Acn.Rdm.Packets.Net;
using Acn.RdmNet.Sockets;

namespace RdmNetworkMonitor
{
    public class RdmDeviceBroker
    {
        RdmNetSocket socket = null;

        public event EventHandler PortsChanged;

        public RdmDeviceBroker(RdmNetSocket socket, UId id,IPAddress address)
        {
            Id = id;
            Address = address;
            this.socket = socket;

            socket.NewRdmPacket += new EventHandler<NewPacketEventArgs<RdmPacket>>(socket_NewRdmPacket);
        }

        void socket_NewRdmPacket(object sender, NewPacketEventArgs<RdmPacket> e)
        {
            EndpointList.Reply ports = e.Packet as EndpointList.Reply;
            if (ports != null)
            {
                Ports = ports.EndpointIDs;
                if (PortsChanged != null)
                    PortsChanged(this, EventArgs.Empty);
            }
        }

        public UId Id { get; set; }

        public IPAddress Address { get; set; }

        public List<short> Ports { get; set; }

        public void Identify()
        {
            EndpointList.Get getPorts = new EndpointList.Get();
            socket.SendRdm(getPorts, new RdmEndPoint(Address),  Id);
        }
    }
}
