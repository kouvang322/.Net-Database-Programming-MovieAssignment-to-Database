using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibraryAssignment
{
    public class VideoList
    {
        List<Video> videos = new List<Video>();

        public VideoList()
        {
            ReadVideoFromFile();
        }

        public void ReadVideoFromFile()
        {
            string file = "MediaTypes\\Videos.csv";
            StreamReader fileReader = new StreamReader(file);
            string line = fileReader.ReadLine();

            if (File.Exists(file))
            {
                // read data from file

                try
                {
                    while (!fileReader.EndOfStream)
                    {
                        Video video = new Video();

                        line = fileReader.ReadLine();
                        int idx = line.IndexOf('"');

                        if (idx == -1)
                        {
                            string[] videoInfo = line.Split(",");

                            video.ID = (int.Parse(videoInfo[0]));

                            video.Title = (videoInfo[1]);

                            video.Format = (videoInfo[2]);

                            video.Length = (int.Parse(videoInfo[3]));

                            string regionSeparate = (videoInfo[4]);

                            video.Regions = regionSeparate.Split("|").Select(Int32.Parse).ToArray();

                        }

                        videos.Add(video);
                    }

                    fileReader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("File does not exist");
            }

        }
        public void DisplayVideos()
        {
            foreach (var video in videos)
            {
                video.Display();
            }
        }

        public List<Media> Get()
        {
            return new List<Media>(videos);
        }

        public Media Search(string searchString)
        {
            var results = videos.Where(x => x.Title.ToLower().Contains(searchString.ToLower()));
            return results.FirstOrDefault(); 
        }

    }

}