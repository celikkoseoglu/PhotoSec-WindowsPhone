﻿using System;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.IsolatedStorage;

namespace PhotoSec
{
    class AdvancedTextIO
    {
        IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication();
        string fileName;
        string textToWrite;
        string filePath;

        public void SaveTextToSpecifiedLocation(string fileName, string textToWrite, string filePath)
        {
            storage.CreateDirectory(filePath);
            StreamWriter writer = new StreamWriter(new IsolatedStorageFileStream(filePath + "/" + fileName, FileMode.Create, FileAccess.Write, storage));
            writer.WriteLine(textToWrite);
            writer.Dispose();
        }

        public short FindTextInLine(string stringToSearch)
        {
            short foundStringIn = 0;
            string[] fileNames = storage.GetFileNames();
            while (fileNames[foundStringIn] != stringToSearch)
            {
                foundStringIn++;
            }            
            fileNames = null;
            return foundStringIn;
        }

        public short FindTextInLineWithSpecifiedLocation(string stringToSearch, string specifiedFolderName)
        {
            short foundStringIn = 0;
            string[] fileNames = storage.GetFileNames(specifiedFolderName + "\\*");
            while (fileNames[foundStringIn] != stringToSearch)
            {
                foundStringIn++;
            }
            fileNames = null;
            return foundStringIn;
        }

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public string TextToWrite
        {
            get { return textToWrite; }
            set { textToWrite = value; }
        }

        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }
    }
}
