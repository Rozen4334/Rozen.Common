using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Terraria
{
    /// <summary>
    ///     Represents a packet generator for sending packets over the net client of the game.
    /// </summary>
    public class PacketFactory
    {
        private readonly MemoryStream _memoryStream;

        public BinaryWriter Writer;

        /// <summary>
        ///     Creates a new packet factory with the provided offset check.
        /// </summary>
        /// <param name="writeOffset"></param>
        public PacketFactory(bool writeOffset = true)
        {
            _memoryStream = new MemoryStream();
            Writer = new BinaryWriter(_memoryStream);
            if (writeOffset)
            {
                Writer.BaseStream.Position = 3L;
            }
        }

        /// <summary>
        ///     Sets the packet type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public PacketFactory SetType(short type)
        {
            long currentPosition = Writer.BaseStream.Position;
            Writer.BaseStream.Position = 2L;
            Writer.Write(type);
            Writer.BaseStream.Position = currentPosition;
            return this;
        }

        /// <summary>
        ///     Adds a boolean value to the factory.
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public PacketFactory PackBool(bool flag)
        {
            Writer.Write(flag);
            return this;
        }

        /// <summary>
        ///     Adds a int8 value to the factory.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public PacketFactory PackByte(byte num)
        {
            Writer.Write(num);
            return this;
        }

        /// <summary>
        ///     Adds an int4 value to the factory.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public PacketFactory PackSByte(sbyte num)
        {
            Writer.Write(num);
            return this;
        }

        /// <summary>
        ///     Adds an int16 to the factory.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public PacketFactory PackInt16(short num)
        {
            Writer.Write(num);
            return this;
        }

        /// <summary>
        ///     Adds an unsigned int16 to the factory.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public PacketFactory PackUInt16(ushort num)
        {
            Writer.Write(num);
            return this;
        }

        /// <summary>
        ///     Adds an int32 to the factory.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public PacketFactory PackInt32(int num)
        {
            Writer.Write(num);
            return this;
        }

        /// <summary>
        ///     Adds an unsigned int32 to the factory.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public PacketFactory PackUInt32(uint num)
        {
            Writer.Write(num);
            return this;
        }

        /// <summary>
        ///     Adds an int64 to the factory.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public PacketFactory PackInt64(long num)
        {
            Writer.Write(num);
            return this;
        }

        /// <summary>
        ///     Adds an unsigned int64 to the factory.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public PacketFactory PackUInt64(ulong num)
        {
            Writer.Write(num);
            return this;
        }

        /// <summary>
        ///     Adds a floating point int32 to the factory.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public PacketFactory PackSingle(float num)
        {
            Writer.Write(num);
            return this;
        }

        /// <summary>
        ///     Adds a string to the factory.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public PacketFactory PackString(string str)
        {
            Writer.Write(str);
            return this;
        }

        /// <summary>
        ///     Adds a whole byte array to the factory.
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public PacketFactory PackBuffer(byte[] buffer)
        {
            Writer.Write(buffer);
            return this;
        }

        private void UpdateLength()
        {
            long currentPosition = Writer.BaseStream.Position;
            Writer.BaseStream.Position = 0L;
            Writer.Write((short)currentPosition);
            Writer.BaseStream.Position = currentPosition;
        }

        public byte[] GetByteData()
        {
            UpdateLength();
            return _memoryStream.ToArray();
        }

        public byte[] ToArray()
        {
            return _memoryStream.ToArray();
        }
    }
}
