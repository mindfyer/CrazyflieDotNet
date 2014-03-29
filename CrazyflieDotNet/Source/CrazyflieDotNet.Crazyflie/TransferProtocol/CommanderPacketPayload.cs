/* 
 *						 _ _  _     
 *		       ____ ___  ___  __________(_|_)(_)____
 *		      / __ `__ \/ _ \/ ___/ ___/ / _ \/ ___/
 *		     / / / / / /  __(__  |__  ) /  __/ /    
 *		    /_/ /_/ /_/\___/____/____/_/\___/_/  
 *
 *	     Copyright 2013 - Messier/Chris Karcz - ckarcz@gmail.com
 *
 *	This Source Code Form is subject to the terms of the Mozilla Public
 *	License, v. 2.0. If a copy of the MPL was not distributed with this
 *	file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

namespace CrazyflieDotNet.Crazyflie.TransferProtocol
{
	public class CommanderPacketPayload
		: ICommanderPacketPayload
	{
		public CommanderPacketPayload(byte[] payloadBytes)
		{
		}

		public CommanderPacketPayload(float roll, float pitch, float yaw, short thrust)
		{
			Roll = roll;
			Pitch = pitch;
			Yaw = yaw;
			Thurst = thrust;
		}

		public float Roll { get; private set; }

		public float Pitch { get; private set; }

		public float Yaw { get; private set; }

		public short Thurst { get; private set; }

		public byte[] GetBytes()
		{
			// Commander Payload Format:
			// Name   |  Index  |  Type  |  Size (bytes)
			// roll        0       float      4
			// pitch       4       float      4
			// yaw         8       float      4
			// thurst      12      short      2
			// .............................total: 14 bytes

			var rollByte = (byte)Roll;
			var pitchByte = (byte)Pitch;
			var yawByte = (byte)Yaw;
			var thrustByte = (byte)Thurst;

			var floatSize = sizeof(float);
			var shortSize = sizeof(short);
			var commanderPayloadSize = floatSize * 3 + shortSize;

			var commanderPayloadBytes = new byte[commanderPayloadSize];

			commanderPayloadBytes[0] = rollByte; // @ 0
			commanderPayloadBytes[floatSize] = pitchByte; // @ 4
			commanderPayloadBytes[floatSize * 2] = yawByte; // @ 8
			commanderPayloadBytes[floatSize * 3] = thrustByte; // @ 12

			return commanderPayloadBytes;
		}
	}
}