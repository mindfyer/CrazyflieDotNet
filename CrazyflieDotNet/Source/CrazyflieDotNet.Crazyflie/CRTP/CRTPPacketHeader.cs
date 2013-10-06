/* 
 *										 _ _  _     
 *			   ____ ___  ___  __________(_|_)(_)____
 *			  / __ `__ \/ _ \/ ___/ ___/ / _ \/ ___/
 *			 / / / / / /  __(__  |__  ) /  __/ /    
 *			/_/ /_/ /_/\___/____/____/_/\___/_/  
 *
 *				Copyright 2013 - www.messier.com
 *
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

namespace CrazyflieDotNet.Crazyflie.CRTP
{
	public sealed class CRTPPacketHeader
	{
		private byte? _bytesCached;

		public CRTPPacketHeader(byte packetHeaderByte)
		{
			// TODO
		}

		public CRTPPacketHeader(CRTPPort port)
			: this(port, DefaultChannel)
		{
		}

		public CRTPPacketHeader(CRTPPort port, CRTPChannel channel)
		{
			Port = port;
			Channel = channel;
		}

		public CRTPChannel Channel { get; private set; }

		public CRTPPort Port { get; private set; }

		public static CRTPChannel DefaultChannel = CRTPChannel.Channel0;

		internal byte HeaderByte
		{
			get { return (_bytesCached ?? (_bytesCached = GetByte(this))).Value; }
		}

		public static byte GetByte(CRTPPacketHeader packetHeader)
		{
			var port = (byte)packetHeader.Port;
			var channel = (byte)packetHeader.Channel;

			// Header Format (1 byte):
			//  7  6  5  4  3  2  1  0
			// [   Port   ][Res. ][Ch.]
			// Res. = reserved for transfer layer.

			byte portByte = (byte)port;
			byte portByteAnd15 = (byte)(portByte & 0x0F);
			byte portByteAnd15LeftShifted4 = (byte)(portByteAnd15 << 4);
			byte reservedLeftShifted2 = (byte)(0x03 << 2);
			byte channelByte = (byte)channel;
			byte channelByteAnd3 = (byte)(channel & 0x03);

			return (byte)(portByteAnd15LeftShifted4 | reservedLeftShifted2 | channelByteAnd3);
		}
	}
}