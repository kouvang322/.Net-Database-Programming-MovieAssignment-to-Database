using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibraryAssignment
{
    public class ShowList
    {
        List<Show> shows = new List<Show>();

        public ShowList()
        {
            ReadShowFromFile();
        }

        public void ReadShowFromFile()
        {
            string file = "MediaTypes\\Shows.csv";
            StreamReader fileReader = new StreamReader(file);
            string line = fileReader.ReadLine();

            if (File.Exists(file))
            {
                // read data from file

                try
                {
                    while (!fileReader.EndOfStream)
                    {
                        Show show = new Show();

                        line = fileReader.ReadLine();
                        int idx = line.IndexOf('"');

                        if (idx == -1)
                        {
                            string[] showInfo = line.Split(",");

                            show.ID = (int.Parse(showInfo[0]));

                            show.Title = (showInfo[1]);

                            show.Season = (int.Parse(showInfo[2]));

                            show.Episode = (int.Parse(showInfo[3]));

                            string writersSeparate = showInfo[4];

                            show.Writers = writersSeparate.Split("|");

                        }

                        shows.Add(show);
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
        public void DisplayShows()
        {
            foreach (var show in shows)
            {
                show.Display();
            }
        }

        public List<Media> Get()
        {
            return new List<Media>(shows);
        }

        public Media Search(string searchString)
        {
            var results = shows.Where(x => x.Title.ToLower().Contains(searchString.ToLower()));
            return results.FirstOrDefault();
        }
    }

}
