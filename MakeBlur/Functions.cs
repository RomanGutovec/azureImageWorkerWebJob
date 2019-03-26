using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using AForge.Imaging.Filters;
using Microsoft.Azure.WebJobs;

namespace MakeBlur
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage([QueueTrigger("imagemessagequeue")] string message,
             [Blob("blobcontainer/{queueTrigger}", FileAccess.Read)] Stream myBlob,
             [Blob("blobcontainer/{queueTrigger}", FileAccess.Write)] Stream outBlob,
             TextWriter log)
        {                  
            Blur filter = new Blur();

            Bitmap bitmap = new Bitmap(myBlob);

            ImageFormat imageFormat = bitmap.RawFormat;
            filter.ApplyInPlace(bitmap);
            
            bitmap.Save(outBlob, imageFormat);            
        }
    }
}
