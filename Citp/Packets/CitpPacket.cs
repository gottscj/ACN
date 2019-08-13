using Citp.IO;

namespace Citp.Packets
{
    public abstract class CitpPacket
    {
        public abstract void ReadData(CitpBinaryReader data);

        public abstract void WriteData(CitpBinaryWriter data);

        

        

    }
}
