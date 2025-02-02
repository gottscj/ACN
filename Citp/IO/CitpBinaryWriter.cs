﻿using System;
using System.Text;
using System.IO;
using Citp.Packets.Msex;

namespace Citp.IO
{
    public class CitpBinaryWriter:BinaryWriter
    {
        public CitpBinaryWriter(Stream input)
            : base(input)
        { }

        public void WriteCookie(string cookie)
        {
            Write(Encoding.UTF8.GetBytes(cookie));
        }

        public void WriteUcs1(string value)
        {
            if (!string.IsNullOrEmpty(value)) 
                Write(Encoding.UTF8.GetBytes(value));
            Write((byte)0);
        }

        public void WriteUcs2(string value)
        {
            if(!string.IsNullOrEmpty(value)) 
                Write(Encoding.Unicode.GetBytes(value));
            Write((UInt16)0);
        }

        public void WriteMsexLibraryId(CitpMsexLibraryId id)
        {
            Write(id.Level);
            Write(id.Level1);
            Write(id.Level2);
            Write(id.Level3);
        }
    }
}
