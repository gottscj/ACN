﻿using System.IO;
using Citp.Packets.Msex;
using Citp.Packets.CaEx;
using Citp.Sockets;
using Citp.Packets.PInf;
using Citp.Packets.SDmx;
using Citp.Packets.FPtc;
using Citp.Packets.FSel;

namespace Citp.Packets
{
    public class CitpPacketBuilder
    {
        internal static bool TryBuild(CitpReceiveData data,out CitpPacket packet)
        {
            CitpHeader header = new CitpHeader(string.Empty);
            packet = header;

            try
            {
                //We have read all the data.
                if (data.EndOfData())
                {
                    data.Reset();
                    return false;
                }

                //Check we have enough data to construct the header.
                if (data.Length - data.ReadPosition < CitpHeader.PacketSize)
                    return false;

                //Read the packet header.
                header.ReadData(data.GetReader());

                //Ensure the header packet is valid
                if (!header.IsValid())
                {
                    //Purge data as it is probably corrupted.
                    data.Reset();        //Reset position to start so we dump the data.
                    return false;
                }

                //Read the sub packet
                switch (header.ContentType)
                {
                    case CitpPInfHeader.PacketType:
                        packet = BuildPInf(data);
                        break;
                    case SDmxHeader.PacketType:
                        packet = BuildSDmx(data);
                        break;
                    case FPtcHeader.PacketType:
                        packet = BuildFPtc(data);
                        break;
                    case FSelHeader.PacketType:
                        packet = BuildFSel(data);
                        break;
                    case CitpMsexHeader.PacketType:
                        packet = BuildMsex(data);
                        break;
                    
                    case CaExHeader.PacketType:
                        packet = BuildCaEx(data);
                        break;
                    default:
                        return false;
                }
            }
            catch (EndOfStreamException)
            {
                return false;
            }

            return packet != null;
        }

        private static CitpPacket BuildPInf(CitpReceiveData data)
        {
            CitpPInfHeader header = new CitpPInfHeader();
            header.ReadData(data.GetReader());

            switch (header.LayerContentType)
            {
                case CitpPInfPeerLocation.PacketType:
                    return new CitpPInfPeerLocation(data.GetReader());
                case CitpPInfPeerName.PacketType:
                    return new CitpPInfPeerName(data.GetReader());
            }

            return null;

        }

        private static CitpPacket BuildFPtc(CitpReceiveData data)
        {
            FPtcHeader header = new FPtcHeader(string.Empty);
            header.ReadData(data.GetReader());

            CitpPacket packet = null;
            switch (header.ContentType)
            {
                case FPtcPatch.PacketType:
                    packet = new FPtcPatch();
                    break;
                case FPtcUnpatch.PacketType:
                    packet = new FPtcUnpatch();
                    break;
                case FPtcSendPatch.PacketType:
                    packet = new FPtcSendPatch();
                    break;
                default:
                    return null;
            }

            packet.ReadData(data.GetReader());
            return packet;
        }

        private static CitpPacket BuildFSel(CitpReceiveData data)
        {
            FSelHeader header = new FSelHeader(string.Empty);
            header.ReadData(data.GetReader());

            CitpPacket packet = null;
            switch (header.ContentType)
            {
                case FSelSelect.PacketType:
                    packet = new FSelSelect();
                    break;
                case FSelDeselect.PacketType:
                    packet = new FSelDeselect();
                    break;
                default:
                    return null;
            }

            packet.ReadData(data.GetReader());
            return packet;
        }

        private static CitpPacket BuildSDmx(CitpReceiveData data)
        {
            SDmxHeader header = new SDmxHeader(string.Empty);
            header.ReadData(data.GetReader());

            CitpPacket packet = null;
            switch (header.ContentType)
            {
                case SDmxEncryptionIdentifier.PacketType:
                    packet = new SDmxEncryptionIdentifier();
                    break;
                case SDmxUniverseName.PacketType:
                    packet = new SDmxUniverseName();
                    break;
                case SDmxChannelBlock.PacketType:
                    packet = new SDmxChannelBlock();
                    break;
                case SDmxSetExternalSource.PacketType:
                    packet = new SDmxSetExternalSource();
                    break;
                default:
                    return null;
            }

            packet.ReadData(data.GetReader());
            return packet;
        }

        private static CitpPacket BuildMsex(CitpReceiveData data)
        {
            CitpMsexHeader header = new CitpMsexHeader();
            header.ReadData(data.GetReader());

            switch (header.LayerContentType)
            {
                case CitpMsexClientInformation.PacketType:
                    return new CitpMsexClientInformation(data.GetReader());
                case CitpMsexServerInformation.PacketType:
                    return new CitpMsexServerInformation(data.GetReader());
                case CitpMsexLayerStatus.PacketType:
                    return new CitpMsexLayerStatus(data.GetReader());
                case CitpMsexNack.PacketType:
                    return new CitpMsexNack(data.GetReader());
                case CitpMsexGetElementLibraryInformation.PacketType:
                    return new CitpMsexGetElementLibraryInformation(data.GetReader());
                case CitpMsexElementLibraryInformation.PacketType:
                    return new CitpMsexElementLibraryInformation(data.GetReader());
                case CitpMsexElementLibraryUpdated.PacketType:
                    return new CitpMsexElementLibraryUpdated(data.GetReader());
                case CitpMsexGetElementInformation.PacketType:
                    return new CitpMsexGetElementInformation(data.GetReader());
                case CitpMsexMediaElementInformation.PacketType:
                    return new CitpMsexMediaElementInformation(data.GetReader());
                case CitpMsexEffectElementInformation.PacketType:
                    return new CitpMsexEffectElementInformation(data.GetReader());
                case CitpMsexGenericElementInformation.PacketType:
                    return new CitpMsexGenericElementInformation(data.GetReader());
                case CitpMsexGetElementLibraryThumbnail.PacketType:
                    return new CitpMsexGetElementLibraryThumbnail(data.GetReader());
                case CitpMsexElementLibraryThumbnail.PacketType:
                    return new CitpMsexElementLibraryThumbnail(data.GetReader());
                case CitpMsexGetElementThumbnail.PacketType:
                    return new CitpMsexGetElementThumbnail(data.GetReader());
                case CitpMsexElementThumbnail.PacketType:
                    return new CitpMsexElementThumbnail(data.GetReader());
                case CitpMsexGetVideoSources.PacketType:
                    return new CitpMsexGetVideoSources(data.GetReader());
                case CitpMsexVideoSources.PacketType:
                    return new CitpMsexVideoSources(data.GetReader());
                case CitpMsexRequestStream.PacketType:
                    return new CitpMsexRequestStream(data.GetReader());
                case CitpMsexStreamFrame.PacketType:
                    return new CitpMsexStreamFrame(data.GetReader());

            }

            return null;

        }

        private static CitpPacket BuildCaEx(CitpReceiveData data)
        {
            CaExHeader header = new CaExHeader(0x0);
            header.ReadData(data.GetReader());

            CitpPacket packet = null;
            switch (header.ContentCode)
            {
                case CaExContentCodes.Nack:
                    packet = new CaExNack();
                    break;
                case CaExContentCodes.GetLiveViewStatus:
                    packet = new CaExGetLiveViewStatus();
                    break;
                case CaExContentCodes.LiveViewStatus:
                    packet = new CaExLiveViewStatus();
                    break;
                case CaExContentCodes.GetLiveViewImage:
                    packet = new CaExGetLiveViewImage();
                    break;
                case CaExContentCodes.LiveViewImage:
                    packet = new CaExLiveViewImage();
                    break;
                case CaExContentCodes.SetCueRecordingCapabilities:
                    packet = new CaExSetCueRecordingCapabilities();
                    break;
                case CaExContentCodes.RecordCue:
                    packet = new CaExRecordCue();
                    break;
                case CaExContentCodes.SetRecorderClearingCapabilities:
                    packet = new CaExSetRecorderClearingCapabilities();
                    break;
                case CaExContentCodes.ClearRecorder:
                    packet = new CaExClearRecorder();
                    break;
                default:
                    return null;
            }

            packet.ReadData(data.GetReader());
            return packet;

        }
    }
}
