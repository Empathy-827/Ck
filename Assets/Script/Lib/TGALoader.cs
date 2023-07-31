using System;
using System.IO;
using UnityEngine;
public static class TGALoader
{
    // Loads 32-bit (RGBA) uncompressed TGA. Actually, due to TARGA file structure, BGRA32 is good option...
    // Disabled mipmaps. Disabled read/write option, to release texture memory copy.
    //新的代码加入了RLE压缩，DH2的都会压缩。
    //有的tga从左下开始读取像素，有的从左上开始读取，这个信息通常保存在 TGA 文件的头部，使用两个位来表示。加入了判断
    public static Texture2D LoadTGA(string fileName)
    {
        try
        {
            using (BinaryReader reader = new BinaryReader(File.OpenRead(fileName)))
            {
                reader.BaseStream.Seek(2, SeekOrigin.Begin);
                byte data_type_code = reader.ReadByte();
                reader.BaseStream.Seek(12, SeekOrigin.Begin);
                short width = reader.ReadInt16();
                short height = reader.ReadInt16();
                reader.BaseStream.Seek(1, SeekOrigin.Current);
                byte image_descriptor = reader.ReadByte();

                bool isBottomLeft = ReadOrigin(image_descriptor);
                
                Texture2D texture = new Texture2D(width, height, TextureFormat.BGRA32, false);

                if (data_type_code == 10)
                {
                    byte[] rawData = new byte[width * height * 4];
                    int rawDataIndex = 0;

                    while (rawDataIndex < rawData.Length)
                    {
                        byte rlePacket = reader.ReadByte();
                        bool isRleData = (rlePacket >> 7) != 0;
                        int count = (rlePacket & 0x7F) + 1;

                        if (isRleData)
                        {
                            byte[] rawDataPacket = reader.ReadBytes(4);
                            for (int i = 0; i < count; i++)
                            {
                                Array.Copy(rawDataPacket, 0, rawData, rawDataIndex, 4);
                                rawDataIndex += 4;
                            }
                        }
                        else
                        {
                            byte[] rawDataPacket = reader.ReadBytes(4 * count);
                            Array.Copy(rawDataPacket, 0, rawData, rawDataIndex, 4 * count);
                            rawDataIndex += 4 * count;
                        }
                    }

                    if (isBottomLeft == false)
                    {
                        byte[] rawDataFlipped = FlipVertical(rawData, width, height);
                        texture.LoadRawTextureData(rawDataFlipped);
                    }
                    else
                    {
                        texture.LoadRawTextureData(rawData);
                    }
                }
                else if (data_type_code == 2)
                {
                    byte[] source = reader.ReadBytes(width * height * 4);

                    if (isBottomLeft == false)
                    {
                        byte[] sourceFlipped = FlipVertical(source, width, height);
                        texture.LoadRawTextureData(sourceFlipped);
                    }
                    else
                    {
                        texture.LoadRawTextureData(source);
                    }
                }
                else
                {
                    throw new Exception("Unsupported TGA data type: " + data_type_code);
                }

                texture.name = Path.GetFileName(fileName);
                texture.Apply(false, true);
                return texture;
            }
        }
        catch (Exception)
        {
            return Texture2D.blackTexture;
        }

        static bool ReadOrigin(byte image_descriptor)
        {
            return (image_descriptor & 0x20) == 0;
        }

        static byte[] FlipVertical(byte[] rawData, int width, int height)
        {
            int stride = width * 4;
            byte[] flippedData = new byte[rawData.Length];
            for (int i = 0; i < height; i++)
            {
                Array.Copy(rawData, i * stride, flippedData, (height - 1 - i) * stride, stride);
            }
            return flippedData;
        }
    }

    /*public static Texture2D LoadTGA(string fileName)
    {
        try
        {
            BinaryReader reader = new BinaryReader(File.OpenRead(fileName));
            reader.BaseStream.Seek(12, SeekOrigin.Begin);    
            short width = reader.ReadInt16();
            short height = reader.ReadInt16();
            reader.BaseStream.Seek(2, SeekOrigin.Current);
            byte[] source = reader.ReadBytes(width * height * 4);
            reader.Close();
            Texture2D texture = new Texture2D(width, height, TextureFormat.BGRA32, false);
            texture.LoadRawTextureData(source);
            texture.name = Path.GetFileName(fileName);
            texture.Apply(false, true);
            return texture;
        }
        catch (Exception)
        {
            return Texture2D.blackTexture;
        }
    }*/
}