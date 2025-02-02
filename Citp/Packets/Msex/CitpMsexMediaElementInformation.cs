﻿using System;
using System.Collections.Generic;
using Citp.IO;

namespace Citp.Packets.Msex
{
    public class CitpMsexMediaElementInformation:CitpMsexHeader
    {
        public const string PacketType = "MEIn";

        #region Constructors

        public CitpMsexMediaElementInformation()
        {
            LayerContentType = PacketType;
        }

        public CitpMsexMediaElementInformation(CitpBinaryReader data)
        {
            ReadData(data);
        }

        #endregion

        #region Packet Content

        public byte LibraryNumber 
        {
            get{ return LibraryId.ToNumber(); }
        }

        private CitpMsexLibraryId libraryId = new CitpMsexLibraryId();

        public CitpMsexLibraryId LibraryId
        {
            get { return libraryId; }
            set { libraryId = value; }
        }

        private List<MediaInformation> elements = new List<MediaInformation>();

        public List<MediaInformation> Elements
        {
            get { return elements; }
        }

        public class MediaInformation : CitpMsexHeader
        {
            public MediaInformation()
            {
            }

            public MediaInformation(CitpBinaryReader data)
            {
                ReadData(data);
            }

            public byte Number;

            public UInt32 SerialNumber;

            public byte DmxRangeMin;

            public byte DmxRangeMax;

            public string MediaName;

            public UInt64 MediaVersionTimestamp;

            public UInt16 MediaWidth;

            public UInt16 MediaHeight;
            
            public UInt32 MediaLength;

            public byte MediaFPS;

            public override void ReadData(CitpBinaryReader data)
            {
                Number = data.ReadByte();
                DmxRangeMin = data.ReadByte();
                DmxRangeMax = data.ReadByte();
                MediaName = data.ReadUcs2();
                MediaVersionTimestamp = data.ReadUInt64();
                MediaWidth = data.ReadUInt16();
                MediaHeight = data.ReadUInt16();
                MediaLength = data.ReadUInt32();
                MediaFPS = data.ReadByte();
            }

            public override void WriteData(CitpBinaryWriter data)
            {
                data.Write(Number);
                data.Write(DmxRangeMin);
                data.Write(DmxRangeMax);
                data.WriteUcs2(MediaName);
                data.Write(MediaVersionTimestamp);
                data.Write(MediaWidth);
                data.Write(MediaHeight);
                data.Write(MediaLength);
                data.Write(MediaFPS);
            }
        }

        #endregion

        #region Read/Write

        public override void ReadData(CitpBinaryReader data)
        {
            base.ReadData(data);

            if (MsexVersion < 1.1)
                LibraryId.ParseNumber(data.ReadByte());
            else
                LibraryId = data.ReadMsexLibraryId();

            int elementCount = MsexVersion < 1.2 ? data.ReadByte() : data.ReadUInt16();

            for (int n = 0; n < elementCount; n++)
                Elements.Add(new MediaInformation(data));
        }

        public override void WriteData(CitpBinaryWriter data)
        {
            base.WriteData(data);

            if (MsexVersion < 1.1)
                data.Write(LibraryNumber);
            else
                data.WriteMsexLibraryId(LibraryId);

            if (MsexVersion < 1.2)
                data.Write((byte)Elements.Count);
            else
                data.Write((UInt16)Elements.Count);

            foreach (MediaInformation info in Elements)
                info.WriteData(data);
        }

        #endregion
    }
}
