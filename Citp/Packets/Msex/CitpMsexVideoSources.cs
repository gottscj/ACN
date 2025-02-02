﻿using System;
using System.Collections.Generic;
using Citp.IO;

namespace Citp.Packets.Msex
{
    public class CitpMsexVideoSources:CitpMsexHeader
    {
        public const string PacketType = "VSrc";

        #region Constructors

        public CitpMsexVideoSources():base()
        {
            LayerContentType = PacketType;
        }

        public CitpMsexVideoSources(CitpBinaryReader data)
        {
            ReadData(data);
        }

        #endregion

        #region Packet Content

        private List<SourceInformation> sources = new List<SourceInformation>();

        public List<SourceInformation> Sources
        {
            get { return sources; }
        }

        public class SourceInformation : CitpMsexHeader
        {
            public SourceInformation():base()
            {
            }

            public SourceInformation(CitpBinaryReader data)
            {
                ReadData(data);
            }

            public UInt16 SourceIdentifier;

            public string SourceName;

            public byte PhysicalOutput;

            public byte LayerNumber;

            public UInt16 Flags;

            public UInt16 Width;

            public UInt16 Height;

            public override void ReadData(CitpBinaryReader data)
            {

            }

            public override void WriteData(CitpBinaryWriter data)
            {
                data.Write(SourceIdentifier);
                data.WriteUcs2(SourceName);
                data.Write(PhysicalOutput);
                data.Write(LayerNumber);
                data.Write(Flags);
                data.Write(Width);
                data.Write(Height);
            }
        }

        #endregion

        #region Read/Write

        public override void ReadData(CitpBinaryReader data)
        {
            base.ReadData(data);
        }

        public override void WriteData(CitpBinaryWriter data)
        {
            base.WriteData(data);

            data.Write((UInt16) Sources.Count);
            foreach (SourceInformation source in Sources)
                source.WriteData(data);
        }

        #endregion
    }
}
