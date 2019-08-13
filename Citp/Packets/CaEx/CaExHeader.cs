using Citp.IO;

namespace Citp.Packets.CaEx
{
    public class CaExHeader:CitpHeader
    {
        public const string PacketType = "CAEX";

        #region Setup and Initialisation

        public CaExHeader(CaExContentCodes contentCode)
            : base(PacketType)
        {
            ContentCode = contentCode;
        }

        #endregion

        #region Packet Content

        public CaExContentCodes ContentCode { get; set; }

        #endregion

        #region Read/Write

        public override void ReadData(CitpBinaryReader data)
        {
            base.ReadData(data);

            ContentCode = (CaExContentCodes) data.ReadUInt32();
        }

        public override void WriteData(CitpBinaryWriter data)
        {
            base.WriteData(data);
            data.Write((uint) ContentCode);

        }

        #endregion
    }
}
