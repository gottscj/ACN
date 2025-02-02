﻿using System;
using Citp.IO;

namespace Citp.Packets.Msex
{
    public class CitpMsexElementThumbnail:CitpMsexHeader
    {
        public const string PacketType = "EThn";

        #region Constructors

        public CitpMsexElementThumbnail()
        {
            LayerContentType = PacketType;
        }

        public CitpMsexElementThumbnail(CitpBinaryReader data)
        {
            ReadData(data);
        }

        #endregion

        #region Packet Content

        public MsexElementType LibraryType { get; set; }

        public byte LibraryNumber
        {
            get { return LibraryId.ToNumber(); }
        }

        private CitpMsexLibraryId libraryId = new CitpMsexLibraryId();

        public CitpMsexLibraryId LibraryId
        {
            get { return libraryId; }
            set { libraryId = value; }
        }

        public byte ElementNumber { get; set; }

        public string ThmbnailFormat { get; set; }

        public UInt16 ThumbnailWidth { get; set; }

        public UInt16 ThumbnailHeight { get; set; }

        public byte[] ThumbnailBuffer { get; set; }      

        #endregion

        #region Read/Write

        public override void ReadData(CitpBinaryReader data)
        {
            base.ReadData(data);

            LibraryType = (MsexElementType)data.ReadByte();
            
            if (MsexVersion < 1.1)
                LibraryId.ParseNumber(data.ReadByte());
            else
                LibraryId = data.ReadMsexLibraryId();
            ElementNumber = data.ReadByte();

            ThmbnailFormat = data.ReadCookie();
            ThumbnailWidth = data.ReadUInt16();
            ThumbnailHeight = data.ReadUInt16();

            int bufferSize = data.ReadUInt16();
            ThumbnailBuffer = data.ReadBytes(bufferSize);
        }

        public override void WriteData(CitpBinaryWriter data)
        {
            base.WriteData(data);

            data.Write((byte)LibraryType);

            if (MsexVersion < 1.1)
                data.Write(LibraryNumber);
            else
                data.WriteMsexLibraryId(LibraryId);
            data.Write(ElementNumber);

            data.WriteCookie(ThmbnailFormat);
            data.Write(ThumbnailWidth);
            data.Write(ThumbnailHeight);

            data.Write((UInt16) ThumbnailBuffer.Length);
            data.Write(ThumbnailBuffer);
        }

        #endregion
    }
}
