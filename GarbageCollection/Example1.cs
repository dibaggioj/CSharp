using System;
using System.Collections.Generic;
using System.IO; // this includes the file/stream methods
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GarbageCollection
{
    public class y
    {
        public void x()
        {
            var path = "c:\test.txt"; // placeholder location

            // one way to interact with a stream:
            using (var file = File.Open(path, FileMode.Open))
            {
                // TODO (an interaction with the file, e.g. writing, reading, parsing, etc. Afterwards, you'll need to release the stream)
            }
            //don't need file.Close() here, because once the using(){} block wrapped around the file is finished, it closes the stream and unlocks the file automatically
            // using() calls Dispose for you automatically

            // another way to interact with a stream:
            //var file = File.Open(path, FileMode.Open); // file returns a stream. Stream is a resouce that you want to release. This locks the file, so only you can interact with it, write to it, etc; you can't have it be indeterminant, you've got to know what's going on.
            //// TODO (an interaction with the file, e.g. writing, reading, parsing, etc. Afterwards, you'll need to release the stream)
            //file.Close(); // close the stream and unlock the file. If you don't close the stream, then the file remains locked (until a process might unlock it later on). If you were to try accessing again while it's still locked, you'd get an exception.
        }
    }
}
