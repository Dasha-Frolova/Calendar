using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ListToBin {
    public sealed class ListToBin<T> {
        public string FilePath { get; }
        public bool Opened { get; private set; }


        private List<T> list;
        /// <exception cref="InvalidOperationException"></exception>
        public List<T> List {
            get => list;
            set {
                if (!Opened) {
                    throw new InvalidOperationException("Not opened yet.");
                }

                list = value ?? throw new ArgumentNullException();
            }
        }



        public ListToBin(string filePath) {
            FilePath = filePath;
        }



        /// <summary>
        /// Connects to file.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void Open() {
            if (File.Exists(FilePath)) {
                list = GetListFromFile();
            }
            else {
                list = new List<T>();
                WriteListToFile();
            }

            Opened = true;
        }


        /// <summary>
        /// Uploads <see cref="List{T}"/> from file.
        /// </summary>
        public void UpdateFromFile() {
            if (!Opened) {
                throw new InvalidOperationException("Not opened yet.");
            }

            list = GetListFromFile();
        }


        /// <summary>
        /// Save current bd condition on disk.
        /// </summary>
        public void Commit() {
            if (!Opened) {
                throw new IOException("Not opened yet.");
            }

            WriteListToFile();
        }


        /// <summary>
        /// Closes the file connection.
        /// </summary>
        public void Close() {
            list = null;
            Opened = false;
        }


        public void CommitAndClose() {
            Commit();
            Close();
        }



        private void WriteListToFile() {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(FilePath, FileMode.Create)) {
                formatter.Serialize(fs, list);
            }
        }
        private List<T> GetListFromFile() {
            try {
                using (FileStream fs = new FileStream(FilePath, FileMode.Open)) {
                    return (List<T>)new BinaryFormatter().Deserialize(fs);
                }
            }
            catch (SerializationException) {
                throw new Exception("File is corrupted.");

            }
        }

    }
}
