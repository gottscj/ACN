using Citp.IO;

namespace Citp.Packets.SDmx
{
    public class SDmxSetExternalSource:SDmxHeader
    {
        public const string PacketType = "SXSr";

        #region Setup and Initialisation

        public SDmxSetExternalSource()
            : base(PacketType)
        {    
        }

        #endregion

        #region Packet Content

        public string ConnectionString { get; set; }

        #endregion

        #region Read/Write

        public override void ReadData(CitpBinaryReader data)
        {
            base.ReadData(data);
            ConnectionString = data.ReadUcs1();
        }

        public override void WriteData(CitpBinaryWriter data)
        {
            base.WriteData(data);
            data.WriteUcs1(ConnectionString);
        }

        #endregion
    }
}
