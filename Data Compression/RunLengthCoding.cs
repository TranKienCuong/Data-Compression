﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Compression
{
    public class RunLengthCoding
    {
        public CompressedFileInfo Encode(string sourcePath, string destPath, bool losslessJPEG)
        {
            long originalLength = new FileInfo(sourcePath).Length;
            long compressedLength;
            if (!losslessJPEG)
            {
                StreamReader reader = File.OpenText(sourcePath);
                string result = "";
                while (!reader.EndOfStream)
                {
                    int c = reader.Read();
                    int count = 1;
                    while (!reader.EndOfStream && reader.Read() == c)
                        count++;
                    result += (((char)c).ToString() + count.ToString() + " ");
                }
                reader.Close();
                StreamWriter writer = File.CreateText(destPath);
                writer.WriteLine("3");
                writer.WriteLine(Path.GetExtension(sourcePath));
                writer.Write(result);
                writer.Close();
                compressedLength = new FileInfo(destPath).Length;
            }
            else
            {
                compressedLength = new FileInfo(destPath).Length;
            }
            return new CompressedFileInfo(Path.GetFileName(destPath), "Run-Length Coding", originalLength, compressedLength);
        }

        public void Decode(string sourcePath, string destPath)
        {

        }
    }
}
