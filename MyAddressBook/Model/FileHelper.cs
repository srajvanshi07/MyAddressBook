using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using System.IO;
using Newtonsoft.Json;
using Windows.System;
namespace MyAddressBook.Model
{
    public class FileHelper
    {
        public static ulong seekLocation = 0;
        public async Task<string> WriteTextFile(string filename, string contents)
        {
            //storage folder where we will save data
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            //storage for file
            StorageFile textFile = await localFolder.CreateFileAsync(filename,
               CreationCollisionOption.OpenIfExists);
            //copying data between 2 instrance of other stream
            using (IRandomAccessStream textStream = await textFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                //define parameter for seek location
                IOutputStream outputStream = textStream.GetOutputStreamAt(seekLocation);
                using (DataWriter textWriter = new DataWriter(outputStream))
                {
                    //write string value to output stream
                    textWriter.WriteString(contents);
                    //commit cache data
                    await textWriter.StoreAsync();
                }
            }
            return textFile.Path;
        }
        // Read the contents of a text file from the app's local folder.
        public async Task<string> ReadTextFile(string filename)
        {
            string contents;
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile textFile = await localFolder.GetFileAsync(filename);
            
            using (IRandomAccessStream textStream = await textFile.OpenReadAsync())
            {
                using (DataReader textReader = new DataReader(textStream))
                {
                    uint textLength = (uint)textStream.Size;
                    await textReader.LoadAsync(textLength);
                    contents = textReader.ReadString(textLength);
                }
            }
            return contents;
        }
    }
}
